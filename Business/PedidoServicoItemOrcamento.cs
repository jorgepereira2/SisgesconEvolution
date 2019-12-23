using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoServicoItemOrcamento : BusinessObject<PedidoServicoItemOrcamento>, IItemServicoMaterial
	{
		#region Private Members
		private DelineamentoOrcamento _delineamentoOrcamento; 
		private ServicoMaterial _servicomaterial;
        private decimal _quantidade; 
		private decimal _valor; 
		private OrigemMaterial _origemMaterial;
	    private string _rmc;
        private decimal _quantidadeEntregue;
	    private Celula _celula;
	    private Fornecedor _fornecedor;
	    private string _observacao;
	    
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoServicoItemOrcamento()
		{
		    _celula = null;
			_delineamentoOrcamento =  null; 
			_servicomaterial =  null; 
			_quantidade = 0; 
			_valor = 0;
		    _rmc = null;
			_origemMaterial =  OrigemMaterial.Obtencao;
		    _quantidadeEntregue = 0;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual Servidor ServidorDelineamento { get; set; }

        /// <summary>
        /// Indica se o item ja pertence a um PO
        /// </summary>
	    public virtual bool FlagCotadoPO { get; set; }

        //Observacao para lancar quando for item de servico
        public virtual string Observacao
        {
            get { return _observacao; }
            set { _observacao = value; }
        }
        
        //Fornecedor para lancar quando for item de servico
        public virtual Fornecedor Fornecedor
        {
            get { return _fornecedor; }
            set { _fornecedor = value; }
        }

        public virtual Celula Celula
        {
            get { return _celula; }
            set { _celula = value; }
        }

        public virtual decimal QuantidadeEntregue
        {
            get { return _quantidadeEntregue; }
            set { _quantidadeEntregue = value; }
        }	
		
		public virtual DelineamentoOrcamento DelineamentoOrcamento
		{
			get { return _delineamentoOrcamento; }
			set { _delineamentoOrcamento = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual ServicoMaterial ServicoMaterial
		{
			get { return _servicomaterial; }
			set { _servicomaterial = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual decimal Quantidade
		{
			get { return _quantidade; }
			set { _quantidade = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal Valor
		{
			get { return _valor; }
			set { _valor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual OrigemMaterial OrigemMaterial
		{
			get { return _origemMaterial; }
			set { _origemMaterial = value; }
		}

        public virtual string RMC
        {
            get { return _rmc; }
            set { _rmc = value; }
        }

	    public virtual decimal ValorTotal
	    {
	        get{ return _valor*_quantidade;}
	    }

        public virtual string Especificacao
        {
            get { return _observacao; }
           
        }
        public virtual Servidor Comprador
        {
            get { return new Servidor(); }
        }

	    #endregion 
		
		#region Public Methods
		
		public static decimal GetUltimoOrcamento(int id_servicoMaterial)
		{
            ISession session = NHibernateSessionManager.Instance.GetSession();
		    IQuery query = session.CreateQuery(
		        @"select o.Valor from PedidoServicoItemOrcamento o 
		        where o.ServicoMaterial.ID = :id_servicoMaterial		        	        
		        order by o.DelineamentoOrcamento.Data DESC"
		        );
		    query.SetInt32("id_servicoMaterial", id_servicoMaterial);
		    query.SetMaxResults(1);

		    object valor = query.UniqueResult();
		    if(valor == null)
		        return 0;
		    else
		        return Convert.ToDecimal(valor);
		}
		
		#endregion
		
		
		
	}
}
