using System;
using System.Collections.Generic;
using NHibernate;
using Shared.Common;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoObtencaoPagamento : BusinessObject<PedidoObtencaoPagamento>, IPedidoObtencao	
	{
		
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
        public PedidoObtencaoPagamento()
		{
		  
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual string NumeroNF { get; set; }
        public virtual string NumeroOrdemBancaria { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual PedidoObtencao PedidoObtencao { get; set; }
        public virtual PedidoObtencaoEmpenho Empenho { get; set; }
        public virtual StatusPedidoObtencao Status { get; set; }
      

		#endregion 

        #region Interface IPedidoObtencao

        public virtual string CodigoComAno
        {
            get { return this.PedidoObtencao.CodigoComAno; }
        }

        public virtual string TipoPedidoSigla
        {
            get { return this.PedidoObtencao.TipoPedidoSigla; }
        }

        public virtual string Aplicacao
        {
            get { return this.PedidoObtencao.Aplicacao; }
        }

        public virtual DelineamentoOrcamento DelineamentoOrcamento
        {
            get { return this.PedidoObtencao.DelineamentoOrcamento; }
        }

        public virtual DateTime DataEmissao
        {
            get { return this.PedidoObtencao.DataEmissao; }
        }

        public virtual decimal ValorTotal
        {
            get { return this.Valor; }
        }

        public virtual Celula Celula
        {
            get { return this.PedidoObtencao.Celula; }
        }

        public virtual ICustomList<HistoricoPedidoObtencao> Historico
        {
            get { return this.PedidoObtencao.Historico; }
        }

        public virtual HistoricoPedidoObtencao UltimoHistorico
        {
            get { return this.PedidoObtencao.UltimoHistorico; }
        }

        public virtual int Numero
        {
            get { return this.PedidoObtencao.Numero; }
        }

        public virtual int ID_PedidoObtencao
        {
            get { return this.PedidoObtencao.ID; }
        }

        public virtual string DescricaoStatus
        {
            get { return this.Status.Descricao; }
        }
        #endregion



        //        public static List<PedidoObtencaoPagamento> Select(string texto, DateTime dataInicio, DateTime dataFim, int ano)
//        {
//            ISession session = NHibernateSessionManager.Instance.GetSession();
//            IQuery query = session.CreateQuery(
//            @"from AutorizacaoCompraPagamento p
//                inner join fetch p.AutorizacaoCompra a
//                inner join fetch a.Fornecedor f
//			where (f.RazaoSocial like :texto or a.Numero like :texto)
//			and dbo.DateIsInBetween(p.Data, :dataInicio, :dataFim) = 1
//            and dbo.GetYear(a.DataEmissao) = IsNull(:ano, dbo.GetYear(a.DataEmissao))			
//			order by a.ID");

//            query.SetString("texto", "%" + texto + "%");
//            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
//            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
//            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);

//            return (List<PedidoObtencaoPagamento>)query.List<PedidoObtencaoPagamento>();
        //        }


        #region workflow

        public virtual void ConfirmarLiquidacao(int id_servidor)
        {
            HistoricoPedidoObtencao historico = new HistoricoPedidoObtencao();
            historico.Data = DateTime.Now;
            historico.PedidoObtencao = this.PedidoObtencao;
            historico.Descricao = string.Format("Pagamento: {0} -> {1}", this.Status.Descricao, Util.GetDescription(StatusPedidoObtencaoEnum.AguardandoPagamentoPO));
            historico.Servidor = Servidor.Get(id_servidor);

            this.Status = StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.AguardandoPagamentoPO);

            using (TransactionBlock tran = new TransactionBlock())
            {
                this.Save();

                historico.Save();
                this.Status = StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.AguardandoPagamentoPO);
                base.Save();
                this.PedidoObtencao.Historico.Add(historico);
                tran.IsValid = true;
            }
        }

        public virtual void ConfirmarPagamento(int id_servidor, string numeroOrdemBancaria)
        {
            HistoricoPedidoObtencao historico = new HistoricoPedidoObtencao();
            historico.Data = DateTime.Now;
            historico.PedidoObtencao = this.PedidoObtencao;
            historico.Descricao = string.Format("Pagamento: {0} -> {1}", this.Status.Descricao, Util.GetDescription(StatusPedidoObtencaoEnum.Finalizado));
            historico.Servidor = Servidor.Get(id_servidor);

            this.Status = StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.Finalizado);
            this.NumeroOrdemBancaria = numeroOrdemBancaria;

            using (TransactionBlock tran = new TransactionBlock())
            {
                this.Save();
                
                historico.Save();
                // base.Save();
                this.PedidoObtencao.Historico.Add(historico);

                if (this.PedidoObtencao.ValorPagoFinalizado >= this.PedidoObtencao.ValorTotal)
                {
                    this.PedidoObtencao.IrParaProximoStatus(id_servidor, null);
                }

                tran.IsValid = true;
            }
        }


        public virtual void Recusar(int id_servidor, string justificativa)
        {
            HistoricoPedidoObtencao historico = new HistoricoPedidoObtencao();
            historico.Data = DateTime.Now;
            historico.Justificativa = justificativa;
            historico.PedidoObtencao = this.PedidoObtencao;
            historico.Servidor = Servidor.Get(id_servidor);
            historico.FlagReprovado = true;

            this.PedidoObtencao.Historico.Add(historico);
            
            using (TransactionBlock tran = new TransactionBlock())
            {
                if (Status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoPagamentoPO)
                {
                    historico.Descricao = string.Format("Pagamento: {0} -> {1}", this.Status.Descricao, Util.GetDescription(StatusPedidoObtencaoEnum.AguardandoLiquidacao));
                    this.Status = StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.AguardandoLiquidacao);
                    this.Save();
                }
                else if (Status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoLiquidacao)
                {
                    historico.Descricao = string.Format("Pagamento: {0} -> {1}", this.Status.Descricao, Util.GetDescription(StatusPedidoObtencaoEnum.AguardandoEntregaExecucao));
                    this.Status = StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.AguardandoEntregaExecucao);
                    PedidoObtencao.Pagamentos.Remove(this);
                    this.Delete();
                    
                }

                historico.Save();

                if(PedidoObtencao.Status.ID > this.Status.ID)
                {
                    PedidoObtencao.Status = this.Status;
                    PedidoObtencao.Alterar();
                }
                    



                tran.IsValid = true;
            }
        }
        #endregion

    }
}
