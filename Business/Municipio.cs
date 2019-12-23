using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class Municipio : BusinessObject<Municipio>, IComparable<Municipio>
	{
		#region Private Members
		
		private string _nome; 
		private Estado _estado; 
		private bool _flagativo; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Municipio()
		{
			_nome = null; 
			_estado = null; 
			_flagativo = false; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
				
		public virtual string Nome
		{
			get { return _nome; }
			set	
			{
				if ( value != null )
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for Municipio", value, value.ToString());
				
				_nome = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Estado Estado
		{
			get { return _estado; }
			set { _estado = value; }
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
		public static Dictionary<int, string> List(int id_estado)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
				@"select m.ID, m.Nome 
				from Municipio m
				where m.Estado.ID = :id_estado
				order by m.Nome");

			query.SetInt32("id_estado", id_estado);
			return BusinessHelper.ExecuteList(query);
		}

		#endregion

        public virtual int CompareTo(Municipio other)
	    {
	        if(other == null) return -1;
	        return _nome.CompareTo(other._nome);
	    }

        public override string ToString()
        {
            return _nome;
        }
	}
}
