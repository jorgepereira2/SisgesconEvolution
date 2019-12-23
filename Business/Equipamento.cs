using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class Equipamento : BusinessObject<Equipamento>, IComparable<Equipamento>
	{
		#region Private Members
		private SubTipoEquipamento _subtipoequipamento; 
		private string _descricao; 
		private string _codeq; 
		private string _descricaocodeq; 
		private bool _flagativo;
	    private TipoOperativo _tipoOpertivo;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Equipamento()
		{
			_subtipoequipamento =  null; 
			_descricao = null; 
			_codeq = null; 
			_descricaocodeq = null; 
			_flagativo = false;
		    _tipoOpertivo = TipoOperativo.Administrativo;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual TipoOperativo TipoOperativo
        {
            get { return _tipoOpertivo; }
            set { _tipoOpertivo = value; }
        }
        	
		public virtual SubTipoEquipamento SubTipoEquipamento
		{
			get { return _subtipoequipamento; }
			set { _subtipoequipamento = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Descricao
		{
			get { return _descricao; }
			set	
			{
				if ( value != null )
					if( value.Length > 70)
						throw new ArgumentOutOfRangeException("Invalid value for Descricao", value, value.ToString());
				
				_descricao = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Codeq
		{
			get { return _codeq; }
			set	
			{
				if ( value != null )
					if( value.Length > 15)
						throw new ArgumentOutOfRangeException("Invalid value for Codeq", value, value.ToString());
				
				_codeq = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string DescricaoCodeq
		{
			get { return _descricaocodeq; }
			set	
			{
				if ( value != null )
					if( value.Length > 70)
						throw new ArgumentOutOfRangeException("Invalid value for DescricaoCodeq", value, value.ToString());
				
				_descricaocodeq = value;
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
		
	    public virtual TipoEquipamento TipoEquipamento
	    {
	        get{ return _subtipoequipamento.TipoEquipamento;}
	    }

	    public virtual string DescricaoCompleta
	    {
            get { return string.Format("{0} - {1} - {2}", _subtipoequipamento.TipoEquipamento.Descricao, _subtipoequipamento.Descricao, _descricao); }
	    }

	    #endregion 
		
		
		#region Public Methods
		
		public static Dictionary<int, string> List(int id_subTipoEquipamento)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select e.ID, e.Descricao 
			from Equipamento e  
			where e.FlagAtivo = 1
			and e.SubTipoEquipamento.ID = :id_subTipoEquipamento
			order by e.Descricao");

		    query.SetInt32("id_subTipoEquipamento", id_subTipoEquipamento);
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<Equipamento> Select(int id_tipoEquipamento, string descricao, int id_subTipoEquipamento, int id_tipoOperativo)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from Equipamento e inner join fetch e.SubTipoEquipamento s inner join fetch s.TipoEquipamento t
			where t.ID = IsNull(:id_tipo, t.ID)
			and e.Descricao like :descricao
			and s.ID = IsNull(:id_subTipo, s.ID)
			and e.TipoOperativo = IsNull(:tipoOperativo, e.TipoOperativo)
			order by e.Descricao");

		    query.SetParameter("id_tipo", BusinessHelper.IsNullOrZero(id_tipoEquipamento), NHibernateUtil.Int32);
            query.SetParameter("id_subTipo", BusinessHelper.IsNullOrZero(id_subTipoEquipamento), NHibernateUtil.Int32);
            query.SetParameter("tipoOperativo", BusinessHelper.IsNullOrZero(id_tipoOperativo), NHibernateUtil.Int32);
		    query.SetString("descricao", "%" + descricao + "%");
			return (List<Equipamento>)query.List<Equipamento>();
		}
		
		#endregion
		
		#region Fast Search
        public static IEnumerable<EquipamentoUI> FastSearch(string descricao)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select new EquipamentoUI(e.ID, e.Descricao, t.Descricao, s.Descricao)
            from Equipamento e inner join e.SubTipoEquipamento s inner join s.TipoEquipamento t
			where (e.Descricao like :descricao
			or t.Descricao like :descricao
			or s.Descricao like :descricao)
			order by t.Descricao, s.Descricao, e.Descricao");

            query.SetMaxResults(20);
            query.SetString("descricao", "%" + descricao + "%");
            return query.List<EquipamentoUI>();
        }
		#endregion

	    public virtual int CompareTo(Equipamento other)
	    {
	        return this.Descricao.CompareTo(other.Descricao);
	    }

        public override string ToString()
        {
            return _descricao;
        }
	}
}
