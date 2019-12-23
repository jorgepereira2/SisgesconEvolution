using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
    public class PedidoAquisicaoManager
    {

        protected PedidoAquisicao _pedidoAquisicao;
        public PedidoAquisicaoManager()
        {
            
        }

        public  PedidoAquisicaoManager(PedidoAquisicao pa)
        {
            _pedidoAquisicao = pa;
        }


        #region Factory

        public static PedidoAquisicaoManager GetManager(PedidoAquisicao pa)
        {
            TipoPA tipo = (TipoPA) Convert.ToInt32(ConfigurationManager.AppSettings["TipoPA"]);
            switch (tipo)
            {
                case TipoPA.Parsit:
                    return new PedidoAquisicaoParsitManager(pa);
                default:
                    throw new Exception("TipoPA inesperado, verifique o web.config");
            }
        }
        #endregion


        public virtual void ValidarEnvio()
        {
            Parametro parametro = Parametro.Get();

            if (_pedidoAquisicao.ValorTotal > parametro.ValorMaximoSemOrcamentoPA)
            {
                if ((_pedidoAquisicao.Fornecedor2 == null && parametro.NumeroMinimoCotacoesCompra > 1)
                    || (_pedidoAquisicao.Fornecedor3 == null && parametro.NumeroMinimoCotacoesCompra > 2)
                    || (_pedidoAquisicao.Fornecedor4 == null && parametro.NumeroMinimoCotacoesCompra > 3))
                    throw new Exception(
                        string.Format("O número mínimo de cotações é {0}.", parametro.NumeroMinimoCotacoesCompra));

                foreach (PedidoAquisicaoItem item in _pedidoAquisicao.Itens)
                {
                    if (((!item.Valor2.HasValue || item.Valor2.Value == 0) && parametro.NumeroMinimoCotacoesCompra > 1)
                        ||
                        ((!item.Valor3.HasValue || item.Valor2.Value == 0) && parametro.NumeroMinimoCotacoesCompra > 2)
                        ||
                        ((!item.Valor2.HasValue || item.Valor2.Value == 0) && parametro.NumeroMinimoCotacoesCompra > 3))
                        throw new Exception(
                            "Não foi possível enviar. Verifique se todos os valores foram preenchidos corretamente.");
                }
            }
            try
            {
                //verifica se nao tem fornecedor repetido
                Dictionary<int, int> dic = new Dictionary<int, int>();
                dic.Add(_pedidoAquisicao.Fornecedor.ID, 0);
                if (_pedidoAquisicao.Fornecedor2 != null) dic.Add(_pedidoAquisicao.Fornecedor2.ID, 0);
                if (_pedidoAquisicao.Fornecedor3 != null) dic.Add(_pedidoAquisicao.Fornecedor3.ID, 0);
                if (_pedidoAquisicao.Fornecedor4 != null) dic.Add(_pedidoAquisicao.Fornecedor4.ID, 0);
            }
            catch
            {
                throw new Exception("Selecione fornecedores diferentes.");
            }
        }

        #region WorkFlow

        public virtual void Enviar(int id_servidor)
        {
            if (_pedidoAquisicao.Status.StatusPedidoAquisicaoEnum != StatusPedidoAquisicaoEnum.NaoEnviado
            && _pedidoAquisicao.Status.StatusPedidoAquisicaoEnum != StatusPedidoAquisicaoEnum.Reprovado)
                throw new Exception("Este pedido já foi enviado.");

            ValidarEnvio();

            if (_pedidoAquisicao.Itens.Count == 0)
                throw new Exception("Insira pelo menos um item.");

            IrParaProximoStatus(id_servidor, null);
        }

        public virtual void IrParaProximoStatus(int id_servidor, string justificativa)
        {
            if (!_pedidoAquisicao.IsPersisted)
                throw new Exception("Salve os dados do pedido antes de enviá-lo.");

            if (_pedidoAquisicao.Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.Finalizado)
                throw new Exception("Este pedido já foi finalizado.");

            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoAquisicao historico;
                if (_pedidoAquisicao.Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.NaoEnviado)
                    historico = GetHistorico(id_servidor, StatusPedidoAquisicaoEnum.AguardandoAprovacoes);
                else if (_pedidoAquisicao.Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.AguardandoAprovacoes)
                    historico = GetHistorico(id_servidor, StatusPedidoAquisicaoEnum.AguardandoEmpenho);
                else if (_pedidoAquisicao.Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.AguardandoEmpenho)
                    historico = GetHistorico(id_servidor, StatusPedidoAquisicaoEnum.AguardandoLiquidacao);
                else if (_pedidoAquisicao.Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.AguardandoLiquidacao)
                {
                    historico = GetHistorico(id_servidor, StatusPedidoAquisicaoEnum.Finalizado);
                    Finalizar(id_servidor);
                }
                else
                    historico = null;

                historico.FlagRecusado = false;
                historico.Justificativa = justificativa;
                historico.Save();

                _pedidoAquisicao.FlagRecusado = false;
                _pedidoAquisicao.Status = historico.StatusPosterior;
                _pedidoAquisicao.Update();

                _pedidoAquisicao.Historico.Add(historico);

                tran.IsValid = true;
            }
        }

        private void Finalizar(int id_servidor)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                MovimentoConta movimento = new MovimentoConta();
                movimento.Conta = _pedidoAquisicao.Conta;
                movimento.Data = DateTime.Today;
                movimento.NumeroDocumento = _pedidoAquisicao.NumeroEmpenho;
                movimento.Servidor = Servidor.Get(id_servidor);
                movimento.TipoMovimentoFinanceiro = TipoMovimentoFinanceiro.SaidaFinalizado;
                movimento.TipoOperacaoFinanceira = TipoOperacaoFinanceira.Saida;
                movimento.Valor = _pedidoAquisicao.ValorTotal;
                movimento.PedidoAquisicao = _pedidoAquisicao;
                movimento.Save();
                tran.IsValid = true;
            }
        }

        private HistoricoPedidoAquisicao GetHistorico(int id_servidor, StatusPedidoAquisicaoEnum novoStatus)
        {
            HistoricoPedidoAquisicao historico = new HistoricoPedidoAquisicao();
            historico.Data = DateTime.Now;
            historico.PedidoAquisicao = _pedidoAquisicao;
            historico.StatusAnterior = _pedidoAquisicao.Status;
            historico.Servidor = Servidor.Get(id_servidor);
            historico.StatusPosterior = Business.StatusPedidoAquisicao.Get(novoStatus);
            return historico;
        }

        //public virtual void Reprovar(int id_Servidor, string justificativa)
        //{
        //    if (justificativa.Trim() == "")
        //        throw new Exception("A justificativa é obrigatória.");

        //    StatusPedidoAquisicaoEnum novoStatus;
        //    //if(this._pedidoAquisicao.Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.AguardandoAprovacoes)
        //    novoStatus = StatusPedidoAquisicaoEnum.NaoEnviado;
        //    HistoricoPedidoAquisicao historico = GetHistorico(id_Servidor, novoStatus);
        //    historico.Justificativa = justificativa;
        //    historico.FlagRecusado = true;

        //    using (TransactionBlock tran = new TransactionBlock())
        //    {
        //        historico.Save();

        //        _pedidoAquisicao.FlagRecusado = true;
        //        _pedidoAquisicao.Status = historico.StatusPosterior;

        //        _pedidoAquisicao.Update();

        //        _pedidoAquisicao.Historico.Add(historico);
        //        tran.IsValid = true;
        //    }
        //}

        public virtual void Reprovar(int id_servidor, string justificativa)
        {
            if (_pedidoAquisicao.Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.Finalizado)
                throw new Exception("Este pedido já foi finalizado.");

            if (_pedidoAquisicao.Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.NaoEnviado)
                throw new Exception("Este pedido não foi enviado.");

            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoAquisicao historico;
                if (_pedidoAquisicao.Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.AguardandoAprovacoes)
                    historico = GetHistorico(id_servidor, StatusPedidoAquisicaoEnum.NaoEnviado);
                else if (_pedidoAquisicao.Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.AguardandoEmpenho)
                    historico = GetHistorico(id_servidor, StatusPedidoAquisicaoEnum.AguardandoAprovacoes);
                else if (_pedidoAquisicao.Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.AguardandoLiquidacao)
                    historico = GetHistorico(id_servidor, StatusPedidoAquisicaoEnum.AguardandoEmpenho);
                else
                    historico = null;

                historico.FlagRecusado = true;
                historico.Justificativa = justificativa;
                historico.Save();

                _pedidoAquisicao.FlagRecusado = true;
                _pedidoAquisicao.Status = historico.StatusPosterior;
                _pedidoAquisicao.Update();

                _pedidoAquisicao.Historico.Add(historico);

                tran.IsValid = true;
            }
        }


        public virtual void EmitirParecerFinanceiro(int id_Servidor, string justificativa, string numeroEmpenho)
        {
            _pedidoAquisicao.NumeroEmpenho = numeroEmpenho ?? "";

            this.IrParaProximoStatus(id_Servidor, justificativa);
        }

        public virtual void RetornarFinanceiro(int id_servidor, string justificativa)
        {
            if (justificativa.Trim() == "")
                throw new Exception("A justificativa é obrigatória.");

            StatusPedidoAquisicaoEnum novoStatus = StatusPedidoAquisicaoEnum.AguardandoParecerAgenteFinanceiro;
            HistoricoPedidoAquisicao historico = GetHistorico(id_servidor, novoStatus);
            historico.Justificativa = justificativa;

            using (TransactionBlock tran = new TransactionBlock())
            {
                historico.Save();

                _pedidoAquisicao.Status = historico.StatusPosterior;

                _pedidoAquisicao.Update();

                _pedidoAquisicao.Historico.Add(historico);
                tran.IsValid = true;
            }
        }
        #endregion

    }

    public enum TipoPA
    {
        Parsit = 1
    }
}
