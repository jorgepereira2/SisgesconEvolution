using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoObtencaoItemCancelado : BusinessObject<PedidoObtencaoItemCancelado>	
	{
		#region Private Members
		private PedidoObtencao _pedidoobtencao; 
		private ServicoMaterial _servicomaterial; 
		private DateTime _data; 
		private Servidor _servidor; 
		private string _justificativa;
        private decimal _quantidade; 
	    private OrigemMaterial _origemMaterial;
	    private AutorizacaoCompra _autorizacaoCompra;

	    #endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoObtencaoItemCancelado()
		{
			_pedidoobtencao =  null; 
			_servicomaterial = null; 
			_data = DateTime.MinValue; 
			_servidor =  null; 
			_justificativa = null; 
			_quantidade = 0; 
		    _origemMaterial = Business.OrigemMaterial.Rodizio;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual OrigemMaterial OrigemMaterial
        {
            get { return _origemMaterial; }
            set { _origemMaterial = value; }
        } 		
		public virtual PedidoObtencao PedidoObtencao
		{
			get { return _pedidoobtencao; }
			set { _pedidoobtencao = value; }
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
		public virtual DateTime Data
		{
			get { return _data; }
			set { _data = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Servidor Servidor
		{
			get { return _servidor; }
			set { _servidor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Justificativa
		{
			get { return _justificativa; }
			set	
			{
				if ( value != null )
					if( value.Length > 500)
						throw new ArgumentOutOfRangeException("Invalid value for Justificativa", value, value.ToString());
				
				_justificativa = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual decimal Quantidade
		{
			get { return _quantidade; }
			set { _quantidade = value; }
		}

	    public virtual AutorizacaoCompra AutorizacaoCompra
	    {
	        get { return _autorizacaoCompra; }
	        set { _autorizacaoCompra = value; }
	    }

	    #endregion 
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select p.ID, p.Descricao 
			from PedidoObtencaoItemCancelado p  
			where p.FlagAtivo = 1
			order by p.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<PedidoObtencaoItemCancelado> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from PedidoObtencaoItemCancelado p  			
			order by p.Descricao");
		
			return (List<PedidoObtencaoItemCancelado>)query.List<PedidoObtencaoItemCancelado>();
		}
		
		#endregion
		
	}
}
