using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NHibernate;
using Shared.Common;
using Shared.DataAccessHelper;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoServicoAtividadeSecundaria : BusinessObject<PedidoServicoAtividadeSecundaria>, IComparable<PedidoServicoAtividadeSecundaria>	
	{
		#region Private Members
		private int _codigointerno; 
		private DateTime _dataemissao; 
		private Cliente _cliente; 
		private Cliente _clientepagador; 
		private string _observacao;
		private StatusPedidoServicoAtividadeSecundaria _statusPedidoServicoAtividadeSecundaria; 
		private Celula _celula;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoServicoAtividadeSecundaria()
		{
			_codigointerno = 0; 
			_dataemissao = DateTime.MinValue; 
			_cliente =  null; 
			_clientepagador =  null; 
		    
		    _historico = new CustomList<HistoricoPedidoServicoAtividadeSecundaria>();
            _itens = new Shared.NHibernateDAL.CustomList<PedidoServicoAtividadeSecundariaItem>();
		  
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual DateTime DataVencimento { get; set; }
	    public virtual int QuantidadeHH { get; set; }
        public virtual bool FlagRecusado { get; set; }
        public virtual Servidor Servidor { get; set; }
        public virtual MotivoCancelamento MotivoCancelamento { get; set; }

        public virtual decimal DescontoFRE170 { get; set; }
        public virtual decimal DescontoFRE171172 { get; set; }
        public virtual decimal ValorHomemHora { get; set; }

        public virtual decimal TaxaOperacionalMaoObra { get; set; }
        public virtual decimal TaxaOperacionalMaterialServico { get; set; }
        public virtual decimal TaxaContribuicaoOperacional { get; set; }

		public virtual int CodigoInterno
		{
			get { return _codigointerno; }
			set	{
				_codigointerno = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime DataEmissao
		{
			get { return _dataemissao; }
			set { _dataemissao = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Cliente Cliente
		{
			get { return _cliente; }
			set { _cliente = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Cliente ClientePagador
		{
			get { return _clientepagador; }
			set { _clientepagador = value; }
		}
			
	
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Observacao
		{
			get { return _observacao; }
			set	
			{
				if ( value != null )
					if( value.Length > 1000)
						throw new ArgumentOutOfRangeException("Invalid value for Observacao", value, value.ToString());
				
				_observacao = value;
			}
		}
			
		
		public virtual StatusPedidoServicoAtividadeSecundaria Status
		{
			get { return _statusPedidoServicoAtividadeSecundaria; }
			set { _statusPedidoServicoAtividadeSecundaria = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Celula Celula
		{
			get { return _celula; }
			set { _celula = value; }
		}

	    public virtual int ID_PedidoServicoAtividadeSecundaria
	    {
	        get { return ID;}
	    }

	    
        #endregion 
        
        #region Advanced Properties
	    public virtual HistoricoPedidoServicoAtividadeSecundaria UltimoHistorico
	    {
	        get
	        {
	            if(Historico.Count > 0)
	                return Historico[Historico.Count - 1];
	            else
	                return null;
	        }
	    }
        

        public virtual decimal ValorTotalItens
        {
            get
            {
                decimal valor = 0;
                foreach (PedidoServicoAtividadeSecundariaItem item in _itens)
                {
                    valor += item.Valor;
                }
                return valor;
            }
        }

        public virtual string DescricaoCliente
	    {
            get { return Cliente.Descricao; }
	    }

        public virtual string DescricaoStatus
	    {
	        get { return this.Status.Descricao; }
	    }

	    protected internal virtual HistoricoPedidoServicoAtividadeSecundaria GetUltimoHistorico(int id_delineamentoOrcamento)
	    {
	        for(int i = Historico.Count -1; i >= 0;i--)
	        {
	            if(Historico[i].ID_DelineamentoOrcamento == id_delineamentoOrcamento)
	                return Historico[i];
	        }
	        return null;
	    }
        
	    public virtual string CodigoComAno
	    {
	        get{ return string.Format("{0}/{1}", _codigointerno, _dataemissao.Year); }
	    }

        //public virtual DelineamentoOrcamento GetOrcamentoNaoEnviado()
        //{
        //    foreach (DelineamentoOrcamento orcamento in _orcamentos)
        //    {
        //        if (orcamento.Status.StatusPedidoServicoAtividadeSecundariaEnum == StatusPedidoServicoAtividadeSecundariaEnum.AguardandoOrcamento)
        //            return orcamento;
        //    }
        //    return null;
        //}

        #endregion

        #region Taxas

        public virtual decimal TotalFRE170
        {
            get { return this.QuantidadeHH * this.ValorHomemHora; }
        }

        public virtual decimal TotalFRE171172
        {
            get { return this.Itens.Sum(i => i.Valor); }
        }

        public virtual decimal TOMO
        {
            get { return this.TotalFRE170 * this.TaxaOperacionalMaoObra / 100 ; }
        }

        public virtual decimal TOMS
        {
            get { return this.TotalFRE171172 * this.TaxaOperacionalMaterialServico / 100; }
        }

        public virtual decimal SubTotal
        {
            get { return this.TotalFRE170 +  this.TotalFRE171172 + this.TOMO + this.TOMS; }
        }
        
        public virtual decimal TCO
        {
            get { return this.SubTotal * this.TaxaContribuicaoOperacional / 100; }
        }


        public virtual decimal DescontoConcedidoFRE170
        {
            get
            {
                 return (this.TotalFRE170 + this.TOMO) * this.DescontoFRE170 / 100; 
            }
        }

        public virtual decimal DescontoConcedidoFRE171172
        {
            get
            {
                return (this.TotalFRE171172 + this.TOMS) * this.DescontoFRE171172 / 100;
            }
        }

        public virtual decimal TotalPagarFRE170
        {
            get
            {
                return this.TotalFRE170 + this.TOMO - this.DescontoConcedidoFRE170;
            }
        }

        public virtual decimal TotalPagarFRE171172
        {
            get
            {
                return this.TotalFRE171172 + this.TOMS + this.TCO - this.DescontoConcedidoFRE171172;
            }
        }

        public virtual decimal TotalGeral
        {
            get
            {
                return this.TotalPagarFRE170 + this.TotalPagarFRE171172;
            }
        }
        #endregion

        #region Collections

        #region Collections
        private ICustomList<PedidoServicoAtividadeSecundariaItem> _itens;

        public virtual ICustomList<PedidoServicoAtividadeSecundariaItem> Itens
        {
            get { return _itens; }
            set { _itens = value; }
        }

        private ICustomList<HistoricoPedidoServicoAtividadeSecundaria> _historico;

        public virtual ICustomList<HistoricoPedidoServicoAtividadeSecundaria> Historico
        {
            get { return _historico; }
            set { _historico = value; }
        }
        #endregion

	 

	  


	    //public virtual bool ExisteOrcamentoNaoEnviado
        //{
        //    get
        //    {
        //        foreach (DelineamentoOrcamento orcamento in _orcamentos)
        //        {
        //            if(orcamento.Status.StatusPedidoServicoAtividadeSecundariaEnum == StatusPedidoServicoAtividadeSecundariaEnum.AguardandoOrcamento)
        //                return true;
        //        }
        //        return false;
        //    }
        //}
	    #endregion
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select p.ID, p.Descricao 
			from PedidoServicoAtividadeSecundaria p  
			where p.FlagAtivo = 1
			order by p.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}

        public static List<PedidoServicoAtividadeSecundaria> Select(string texto, DateTime dataInicio, DateTime dataFim, int id_status, int id_celula)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from PedidoServicoAtividadeSecundaria p 
                    inner join fetch p.Cliente c                     
			where dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1
			and (dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			or c.Descricao like :texto)
			and p.Status.ID = IsNull(:id_status, p.Status.ID)			
            and IsNull(p.Celula.ID, -1) = IsNull(:id_celula, IsNull(p.Celula.ID, -1))			
            order by p.CodigoInterno");

            query.SetParameter("texto", string.Format("%{0}%", texto));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
			return (List<PedidoServicoAtividadeSecundaria>)query.List<PedidoServicoAtividadeSecundaria>();
		}
       
        /// <summary>
        /// Seleciona os pedidos que estao pendentes da ação de um servidor
        /// </summary>
        public static List<PedidoServicoAtividadeSecundaria> Select(int id_servidor, int id_status, string codigo)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct p from PedidoServicoAtividadeSecundaria p inner join p.Status s inner join fetch p.Cliente c 
                inner join s.Responsaveis resp
			where resp.ID = :id_servidor
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			and s.FlagVinculoPorDivisao = 0
			and s.FlagSomenteGerente = 0
			and s.ID < :statusFinalizado
			and s.ID = IsNull(:status, s.ID)
            and s.ID != :statusCancelado            
			order by p.CodigoInterno");

			IQuery query2 = session.CreateQuery(
            @"select distinct p from PedidoServicoAtividadeSecundaria p inner join p.Status s inner join fetch p.Cliente c 
                inner join s.ResponsaveisDivisao divisao
			where divisao.Servidor.ID = :id_servidor
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			and divisao.Celula.ID = p.Celula.ID
			and s.FlagVinculoPorDivisao = 1
			and s.FlagSomenteGerente = 0
			and s.ID < :statusFinalizado
            and s.ID != :statusCancelado
			and s.ID = IsNull(:status, s.ID)            
			order by p.CodigoInterno");

         
            query.SetInt32("id_servidor", id_servidor);
            query.SetInt32("statusFinalizado", Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.Finalizado));
            query.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.Cancelado));
            query.SetParameter("status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("texto", string.Format("%{0}%", codigo));
            
            query2.SetInt32("id_servidor", id_servidor);
            query2.SetInt32("statusFinalizado", Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.Finalizado));
            query2.SetParameter("status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query2.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.Cancelado));
            query2.SetParameter("texto", string.Format("%{0}%", codigo));
            
        
            List<PedidoServicoAtividadeSecundaria> list1 = (List<PedidoServicoAtividadeSecundaria>)query.List<PedidoServicoAtividadeSecundaria>();
            List<PedidoServicoAtividadeSecundaria> list2 = (List<PedidoServicoAtividadeSecundaria>)query2.List<PedidoServicoAtividadeSecundaria>();
          
            list1.AddRange(list2);
            
            return list1.OrderBy(p => p.CodigoInterno).ToList();
        }


        public static List<PedidoServicoAtividadeSecundaria> Select(int id_status, int id_celula, DateTime dataInicio, DateTime dataFim, string numeroRegistro, string observacao, int id_cliente, int ano, string numeroPS, DateTime dataPrevisaoEntregaInicio, 
            DateTime dataPrevisaoEntregaFim, DateTime dataRealizadoInicio, DateTime dataRealizadoFim, string codigoPedidoCliente)
        {
            string condicaoStatus = "";
            if (id_status == 0)
                condicaoStatus = "";
            else if (id_status == Int32.MinValue || id_status != Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.Cancelado))
                condicaoStatus = string.Format("and s.ID NOT IN ({0})", Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.Cancelado));

            if(dataRealizadoInicio != DateTime.MinValue)
                condicaoStatus += string.Format(@" and exists(from HistoricoPedidoServicoAtividadeSecundaria h where h.PedidoServicoAtividadeSecundaria.ID = p.ID and h.StatusPosterior = {0} 
                        and h.Data >= '{1}')", Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.Finalizado), dataRealizadoInicio.ToString("yyyy-MM-dd"));


            if (dataRealizadoFim != DateTime.MinValue)
                condicaoStatus += string.Format(@" and exists( from HistoricoPedidoServicoAtividadeSecundaria h where h.ID = p.ID and h.StatusPosterior = {0} 
                        and h.Data <= '{1}')", Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.Finalizado), dataRealizadoFim.ToString("yyyy-MM-dd"));

            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            string.Format(@"select distinct p from PedidoServicoAtividadeSecundaria p 
                inner join p.Status s 
                inner join fetch p.Cliente c 
                inner join fetch p.ClientePagador cp                                 
                left join fetch p.Celula cel                     
			where s.ID = IsNull(:id_status, s.ID)			
            and c.ID = IsNull(:id_cliente, c.ID)
			and dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1									
			and IsNull(cel.ID, -1) = IsNull(:id_celula, IsNull(cel.ID, -1))	
			and IsNull(p.NumeroRegistro, 'xxx') like IsNull(:numeroRegistro, IsNull(p.NumeroRegistro, 'xxx'))	
            and IsNull(p.CodigoPedidoCliente, 'xxx') like IsNull(:codigoPedidoCliente, IsNull(p.CodigoPedidoCliente, 'xxx'))	
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :numeroPS) = 1
			and IsNull(p.Observacao, 'xxx') like IsNull(:observacao, IsNull(p.Observacao, 'xxx'))	
            and dbo.GetYear(p.DataEmissao) = IsNull(:ano, dbo.GetYear(p.DataEmissao))
            and dbo.DateIsInBetween(p.PrevisaoEntrega, :dataPrevisaoEntregaInicio, :dataPrevisaoEntregaFim) = 1            
            
            {0}
            
			order by p.CodigoInterno", condicaoStatus));
         

            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
          // query.SetParameter("id_equipamento", BusinessHelper.IsNullOrZero(id_equipamento), NHibernateUtil.Int32);
            query.SetParameter("id_cliente", BusinessHelper.IsNullOrZero(id_cliente), NHibernateUtil.Int32);
            //query.SetInt32("statusAguardandoOrcamento", Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.AguardandoDelineamento));
            //query.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.Cancelado));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            query.SetString("numeroRegistro", string.Format("%{0}%", numeroRegistro));
            query.SetString("codigoPedidoCliente", string.Format("%{0}%", codigoPedidoCliente));
            query.SetString("numeroPS", string.Format("%{0}%", numeroPS));
            query.SetString("observacao", string.Format("%{0}%", observacao));
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            query.SetParameter("dataPrevisaoEntregaInicio", BusinessHelper.IsNull(dataPrevisaoEntregaInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataPrevisaoEntregaFim", BusinessHelper.IsNull(dataPrevisaoEntregaFim), NHibernateUtil.DateTime);
          
            List<PedidoServicoAtividadeSecundaria> pedidos = (List<PedidoServicoAtividadeSecundaria>)query.List<PedidoServicoAtividadeSecundaria>();



            return pedidos.ToList();
        }
        
     

	    #endregion

        #region Orçamento
        public virtual void AddItem(PedidoServicoAtividadeSecundariaItem item)
        {
            item.PedidoServicoAtividadeSecundaria = this;
            item.Save();
            _itens.Add(item);
        }

        public virtual void RemoveItemOrcamento(PedidoServicoAtividadeSecundariaItem item)
        {
            item.Delete();
            _itens.Remove(item);
        }
        #endregion

        #region Relatorios CrossTable

        public static DataSet SelectStatusPorCelula(int ano)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[3];
            param[1] = ano;
            param[2] = null;

            return helper.ExecuteDataSet("PedidoServicoAtividadeSecundaria_SelectStatusPorCelula", param);
        }

        public static DataSet SelectHistoricoStatus(int ano, int id_tipoCliente, int id_cliente, int id_celula)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[6];
            param[1] = ano;
            param[2] = null;
            param[3] = NullHelper.IsZero(id_tipoCliente);
            param[4] = NullHelper.IsZero(id_cliente);
            param[5] = NullHelper.IsZero(id_celula);

            return helper.ExecuteDataSet("PedidoServicoAtividadeSecundaria_SelectHistoricoStatus", param);
        }
        #endregion

        public override void Save()
        {
            if(!this.IsPersisted)
                CriarNovo();
            else
                base.Save();
        }

       
        private void CriarNovo()
	    {
            ISession session = NHibernateSessionManager.Instance.GetSession();
//            IQuery query1 = session.CreateQuery(
//                @"from PedidoServicoAtividadeSecundaria p 
//	            where p.CodigoPedidoCliente = :codigo
//	            and p.Status.ID != :statusCancelado
//	            and p.Cliente.ID = :id_cliente");
//            query1.SetString("codigo", this.CodigoPedidoCliente);
//            query1.SetInt32("id_cliente", this.Cliente.ID);
//            query1.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.Cancelado));
//            if(query1.List<PedidoServicoAtividadeSecundaria>().Count > 0)
//                throw new Exception("Já existe um PS com este número de pedido do cliente.");
	        
	        this.DataEmissao = DateTime.Today;
	        this.Status = StatusPedidoServicoAtividadeSecundaria.Get(StatusPedidoServicoAtividadeSecundariaEnum.NaoEnviado);

            Parametro parametro = Parametro.Get();

            this.DescontoFRE170 = parametro.DescontoFRE170AtividadeSecundaria;
            this.DescontoFRE171172 = parametro.DescontoFRE171172AtividadeSecundaria;
            this.ValorHomemHora = parametro.ValorHomemHoraAtividadeSecundaria;
	        
	        using (TransactionBlock tran = new TransactionBlock())
	        {
                
                IQuery query = session.CreateQuery(
                    @"select MAX(p.CodigoInterno) from PedidoServicoAtividadeSecundaria p where dbo.GetYear(p.DataEmissao) = :ano");
	            query.SetInt32("ano", _dataemissao.Year);

                object result = query.UniqueResult();
                if (result == null)
                    this.CodigoInterno = 1;
                else
                    this.CodigoInterno = Convert.ToInt32(result) + 1;
                    
                base.Save();
                    
	            tran.IsValid = true;
	        }
	    }

        public virtual void RecalcularFatura()
        {
            Parametro parametro = Parametro.Get();

            this.DescontoFRE170 = parametro.DescontoFRE170AtividadeSecundaria;
            this.DescontoFRE171172 = parametro.DescontoFRE171172AtividadeSecundaria;
            this.ValorHomemHora = parametro.ValorHomemHoraAtividadeSecundaria;

            bool calcularTaxas = false;
            if (this.Itens.Count > 0)
                calcularTaxas = !this.Itens[0].TipoAtividadeSecundaria.SemTaxas;

            if (calcularTaxas)
            {
                this.TaxaOperacionalMaoObra = parametro.TaxaOperacionalMaoObraAtividadeSecundaria;
                this.TaxaOperacionalMaterialServico = parametro.TaxaOperacionalMaterialServicoAtividadeSecundaria;
                this.TaxaContribuicaoOperacional = parametro.TaxaContribuicaoOperacionalAtividadeSecundaria;
            }
            else
            {
                this.TaxaOperacionalMaoObra = 0;
                this.TaxaOperacionalMaterialServico = 0;
                this.TaxaContribuicaoOperacional = 0;
            }

            this.Save();
        }

        #region Workflow


        #region Metodos Ok para o BAC

        public virtual void Enviar(int id_servidor)
	    {
	        if(this.Status.StatusPedidoServicoAtividadeSecundariaEnum != StatusPedidoServicoAtividadeSecundariaEnum.NaoEnviado)
	            throw new Exception("Este pedido não pode mais ser enviado.");
            

            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServicoAtividadeSecundaria historico = GetHistorico(id_servidor, StatusPedidoServicoAtividadeSecundariaEnum.AguardandoPagamento);

                historico.Save();
                this.Status = historico.StatusPosterior;
                FlagRecusado = false;
                base.Save();
                this._historico.Add(historico);
                tran.IsValid = true;
            }  
	    }


    

        public virtual void Finalizar(int id_servidor, string comentario)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServicoAtividadeSecundaria historico = GetHistorico(id_servidor, StatusPedidoServicoAtividadeSecundariaEnum.Finalizado);

                historico.Save();
                this.Status = historico.StatusPosterior;
                FlagRecusado = false;
                base.Save();
                this._historico.Add(historico);
                tran.IsValid = true;
            }
        }

        #endregion
	   

      
        
      
	    private HistoricoPedidoServicoAtividadeSecundaria GetHistorico(int id_servidor, StatusPedidoServicoAtividadeSecundariaEnum novoStatus)
	    {
	        HistoricoPedidoServicoAtividadeSecundaria historico = new HistoricoPedidoServicoAtividadeSecundaria();
	        historico.Data = DateTime.Now;
	        historico.PedidoServicoAtividadeSecundaria = this;
	        historico.StatusAnterior = this.Status;
	        historico.Servidor = Servidor.Get(id_servidor);
	        historico.StatusPosterior = Business.StatusPedidoServicoAtividadeSecundaria.Get(novoStatus);
	        return historico;
	    }
	    
	    /// <summary>
	    /// Retorna para o status anterior
	    /// </summary>
	    public virtual void Recusar(int id_servidor, string  justificativa)
	    {
	        StatusPedidoServicoAtividadeSecundariaEnum novoStatus = StatusPedidoServicoAtividadeSecundaria.GetAnterior(_statusPedidoServicoAtividadeSecundaria.StatusPedidoServicoAtividadeSecundariaEnum);
	        HistoricoPedidoServicoAtividadeSecundaria historico = GetHistorico(id_servidor, novoStatus);
            historico.JustificativaRecusa = justificativa;
            
            using(TransactionBlock tran = new TransactionBlock())
            {
                historico.Save();

                this.Status = historico.StatusPosterior;
                FlagRecusado = true;

                base.Save();

                this._historico.Add(historico);
                tran.IsValid = true;
            }
	    }

        public virtual void Cancelar(int id_servidor, int id_motivo, string comentario)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServicoAtividadeSecundaria historico = GetHistorico(id_servidor, StatusPedidoServicoAtividadeSecundariaEnum.Cancelado);
                historico.JustificativaRecusa = comentario;
                historico.Save();


                this.Status = historico.StatusPosterior;
                this.MotivoCancelamento = MotivoCancelamento.Get(id_motivo);

                FlagRecusado = false;
                base.Save();

                this._historico.Add(historico);

                tran.IsValid = true;
            }
        }
	    #endregion


        public static IEnumerable FastSearch(string texto)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct new PedidoServicoUI(p.ID, p.CodigoInterno, p.DataEmissao)
            from PedidoServicoAtividadeSecundaria p 
			where dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
            and ISNULL(p.Status.ID, 0) NOT IN (:status)
			order by p.ID ASC");

            query.SetMaxResults(20);
            query.SetString("texto", "%" + texto + "%");

            List<int> status = new List<int>(2);
            status.Add(Convert.ToInt32(StatusPedidoServicoEnum.Finalizado));
            status.Add(Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));
            query.SetParameterList("status", status, NHibernateUtil.Int32);
            return query.List<PedidoServicoUI>();
        }


        #region IComparable<PedidoServicoAtividadeSecundaria> Members

        public virtual int CompareTo(PedidoServicoAtividadeSecundaria other)
        {
            if(other == null) return -1;
            return ID.CompareTo(other.ID);
        }

        #endregion

        public override string ToString()
        {
            return CodigoComAno;
        }

	  


	    
	}
}
