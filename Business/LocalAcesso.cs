using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class LocalAcesso : BusinessObject<LocalAcesso>
	{
		#region Private Members		
		private string _descricao;
		private string _ipinicial;
		private string _ipfinal;
		private bool _flagativo;
		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public LocalAcesso()
		{			
			_descricao = null;
			_ipinicial = null;
			_ipfinal = null;
			_flagativo = false;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
		
		public virtual string Descricao
		{
			get { return _descricao; }
			set
			{
				if (value != null)
					if (value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for Descricao", value, value.ToString());

				_descricao = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual string IPInicial
		{
			get { return _ipinicial; }
			set
			{
				if (value != null)
					if (value.Length > 30)
						throw new ArgumentOutOfRangeException("Invalid value for IPInicial", value, value.ToString());

				_ipinicial = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual string IPFinal
		{
			get { return _ipfinal; }
			set
			{
				if (value != null)
					if (value.Length > 30)
						throw new ArgumentOutOfRangeException("Invalid value for IPFinal", value, value.ToString());

				_ipfinal = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagAtivo
		{
			get { return _flagativo; }
			set { _flagativo = value; }
		}
		#endregion

		#region Public Methods
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
				@"select l.ID, l.Descricao 
				from LocalAcesso l 
				where l.FlagAtivo = 1
				order by l.Descricao");
			
			return BusinessHelper.ExecuteList(query);
		}

		public static List<LocalAcesso> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
				@"from LocalAcesso l 				
				order by l.Descricao");
			
			return (List<LocalAcesso>)query.List<LocalAcesso>();
		}
		#endregion


	}
}