using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class NotaEntregaMaterialOMFItem : BusinessObject<NotaEntregaMaterialOMFItem>	
	{
		#region Private Members
		private NotaEntregaMaterialOMF _notaentregamaterialomf; 
		private ServicoMaterial _material; 
		private decimal _quantidade; 
		private decimal _valor; 
		private string _loc;
        private TipoTAV _tipoTAV; 
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public NotaEntregaMaterialOMFItem()
		{
			_notaentregamaterialomf =  null; 
			_material =  null; 
			_quantidade = 0; 
			_valor = 0; 
			_loc = null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual TipoTAV TipoTAV
        {
            get { return _tipoTAV; }
            set { _tipoTAV = value; }
        }

		public virtual NotaEntregaMaterialOMF NotaEntregaMaterialOMF
		{
			get { return _notaentregamaterialomf; }
			set { _notaentregamaterialomf = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual ServicoMaterial Material
		{
			get { return _material; }
			set { _material = value; }
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
		public virtual string LOC
		{
			get { return _loc; }
			set	
			{
				if ( value != null )
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for LOC", value, value.ToString());
				
				_loc = value;
			}
		}

	    #endregion 

        #region Advanced Properties
        public virtual decimal ValorTotal
        {
            get { return _quantidade * _valor; }
        }
        #endregion


        #region Public Methods

        public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select n.ID, n.Descricao 
			from NotaEntregaMaterialOMFItem n  
			where n.FlagAtivo = 1
			order by n.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<NotaEntregaMaterialOMFItem> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from NotaEntregaMaterialOMFItem n  			
			order by n.Descricao");
		
			return (List<NotaEntregaMaterialOMFItem>)query.List<NotaEntregaMaterialOMFItem>();
		}
		
		#endregion
		
		
		
	}
}
