using System;
using System.Collections.Generic;
using System.Data;
using Iesi.Collections.Generic;
using NHibernate;
using Shared.Common;
using Shared.DataAccessHelper;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class ServicoMaterial : BusinessObject<ServicoMaterial>	
	{
		#region Private Members

		private SubClasseServicoMaterial _subclasseservicomaterial; 
		private Fabricante _fabricante; 
		private OrigemMaterial _origemmaterial; 
		private string _descricao; 
		private string _descricaosingra; 
		private string _codigointerno; 
		private string _numeroreferencia; 
		private Unidade _unidade; 
		private bool _flagativo;
	    private SJB _sjb;
	    private TipoServicoMaterial _tipoServicoMaterial;
	    private string _codigoSiasg;
	    private string _codigoBarras;
	    private int _quantidadeMinimaPEP;
        private int _quantidadeMinimaRodizio;
        private int _quantidadeMinimaSingra;
        private int _quantidadeMaximaPEP;
        private int _quantidadeMaximaRodizio;
        private int _quantidadeMaximaSingra;
        private string _numeroSerie; 

	    #endregion
		
		#region Default ( Empty ) Class Constuctor

		/// <summary>
		/// default constructor
		/// </summary>
		public ServicoMaterial()
		{
			_subclasseservicomaterial =  null; 
			_fabricante =  null; 
			_origemmaterial =  Business.OrigemMaterial.Obtencao; 
			_descricao = null; 
			_descricaosingra = null; 
			_codigointerno = null;
            _numeroreferencia = null;
            _numeroSerie = null;
            _unidade = null; 
			_flagativo = false;
		    _equipamentos = new HashedSet<Equipamento>();
            _localizacoes = new HashedSet<Localizacao>();
            _celulas = new HashedSet<Celula>();
		}

		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual decimal PrecoEstimadoVenda { get; set; }

        public virtual string CodigoBarras
        {
            get { return _codigoBarras; }
            set { _codigoBarras = value; }
        }
        public virtual string CodigoSiasg
        {
            get { return _codigoSiasg; }
            set { _codigoSiasg = value; }
        }
        public virtual TipoServicoMaterial TipoServicoMaterial
        {
            get { return _tipoServicoMaterial; }
            set { _tipoServicoMaterial = value; }
        }

        public virtual SJB SJB
        {
            get { return _sjb; }
            set { _sjb = value; }
        }

		public virtual SubClasseServicoMaterial SubClasseServicoMaterial
		{
			get { return _subclasseservicomaterial; }
			set { _subclasseservicomaterial = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Fabricante Fabricante
		{
			get { return _fabricante; }
			set { _fabricante = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual OrigemMaterial OrigemMaterial
		{
			get { return _origemmaterial; }
			set { _origemmaterial = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Descricao
		{
			get { return _descricao; }
			set	{ _descricao = value;}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string DescricaoSingra
		{
			get { return _descricaosingra; }
			set	
			{
				if ( value != null )
					if( value.Length > 200)
						throw new ArgumentOutOfRangeException("Invalid value for DescricaoSingra", value, value.ToString());
				
				_descricaosingra = value;
			}
		}
		
			
		public virtual string CodigoInterno
		{
			get { return _codigointerno; }
			set	
			{
				if ( value != null )
					if( value.Length > 20)
						throw new ArgumentOutOfRangeException("Invalid value for CodigoInterno", value, value.ToString());
				
				_codigointerno = value;
			}
		}

        /// <summary>
        /// 
        /// </summary>		
        public virtual string NumeroReferencia
        {
            get { return _numeroreferencia; }
            set
            {
                if (value != null)
                    if (value.Length > 20)
                        throw new ArgumentOutOfRangeException("Invalid value for NumeroReferencia", value, value.ToString());

                _numeroreferencia = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string NumeroSerie
        {
            get { return _numeroSerie; }
            set { _numeroSerie = value; }
        }

        public virtual int QuantidadeMinimaPEP
        {
            get { return _quantidadeMinimaPEP; }
            set { _quantidadeMinimaPEP = value; }
        }

        public virtual int QuantidadeMinimaRodizio
        {
            get { return _quantidadeMinimaRodizio; }
            set { _quantidadeMinimaRodizio = value; }
        }

        public virtual int QuantidadeMinimaSingra
        {
            get { return _quantidadeMinimaSingra; }
            set { _quantidadeMinimaSingra = value; }
        }

        public virtual int QuantidadeMaximaSingra
        {
            get { return _quantidadeMaximaSingra; }
            set { _quantidadeMaximaSingra = value; }
        }

        public virtual int QuantidadeMaximaRodizio
        {
            get { return _quantidadeMaximaRodizio; }
            set { _quantidadeMaximaRodizio = value; }
        }

        public virtual int QuantidadeMaximaPEP
        {
            get { return _quantidadeMaximaPEP; }
            set { _quantidadeMaximaPEP = value; }
        }	
		public virtual Unidade Unidade
		{
			get { return _unidade; }
			set { _unidade = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagAtivo
		{
			get { return _flagativo; }
			set { _flagativo = value; }
		}
	
        public virtual ClasseServicoMaterial ClasseServicoMaterial
        {
            get { return _subclasseservicomaterial.ClasseServicoMaterial; }
        }

        public virtual string DescricaoCompleta
        {
            get { return string.Format("{0} - {1} - {2}", _subclasseservicomaterial.ClasseServicoMaterial.Descricao, _subclasseservicomaterial.Descricao, _descricao); }
        }

	    public virtual NaturezaDespesa NaturezaDespesa { get; set; }
        public virtual SubNaturezaDespesa SubNaturezaDespesa { get; set; }
        
        #endregion

	    #region Collections

	    private Iesi.Collections.Generic.ISet<Equipamento> _equipamentos;
		
	    public virtual Iesi.Collections.Generic.ISet<Equipamento> Equipamentos
	    {
	        get { return _equipamentos; }
	        set { _equipamentos = value; }
	    }

        private Iesi.Collections.Generic.ISet<Localizacao> _localizacoes;

        public virtual Iesi.Collections.Generic.ISet<Localizacao> Localizacoes
        {
            get { return _localizacoes; }
            set { _localizacoes = value; }
        }

        private Iesi.Collections.Generic.ISet<Celula> _celulas;

        public virtual Iesi.Collections.Generic.ISet<Celula> Celulas
        {
            get { return _celulas; }
            set { _celulas = value; }
        }


	    #endregion

        #region Advanced properties

        private LicitacaoItem _itemLicitacaoDisponivel;
        private bool _buscouLicitacao = false;
        public virtual LicitacaoItem ItemLicitacaoDisponivel
        {
            get
            {
                if (!_buscouLicitacao)
                {
                    _itemLicitacaoDisponivel = LicitacaoItem.GetItemAberto(this.ID, 0);
                    _buscouLicitacao = true;
                }
                return _itemLicitacaoDisponivel;
            }
        }

        #endregion

        #region Public Methods

        public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select s.ID, s.Descricao 
			from ServicoMaterial s  
			where s.FlagAtivo = 1
			order by s.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}

        public static List<ServicoMaterial> Select(string listId)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from ServicoMaterial s  
			where s.ID in (:list)
			order by s.Descricao");

            query.SetParameterList("list", listId.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries), NHibernateUtil.Int32);
            return (List<ServicoMaterial>)query.List<ServicoMaterial>();
        }
		
		public static List<ServicoMaterial> Select(int id_tipoServicoMaterial, int id_classe, int id_subClasse, string texto, int id_unidade,
            string descricaoSingra, string codigoSiasg, int id_sjb, int id_naturezaDespesa, int pageSize, int pageNumber, bool? flagAtivo, List<int> sjbLiberados, bool? flagAcessaTodosMateriais)
		{
            if (sjbLiberados.Count == 0)
                sjbLiberados.Add(0);

			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from ServicoMaterial s 
			    inner join fetch s.SubClasseServicoMaterial sc 
			    inner join fetch sc.ClasseServicoMaterial c
			    inner join fetch s.Unidade u			    
                left join fetch s.NaturezaDespesa nd			    
                left join fetch s.SJB sjb
			where s.TipoServicoMaterial = IsNull(:id_tipoServicoMaterial, s.TipoServicoMaterial)
			and c.ID = IsNull(:id_classe, c.ID)
			and sc.ID = IsNull(:id_subClasse, sc.ID)
			and u.ID = IsNull(:id_unidade, u.ID)
			and (s.Descricao like :texto or s.CodigoInterno like :texto)
			and (IsNull(s.DescricaoSingra, 'xxxx') like :descricaoSingra)
            and (IsNull(s.CodigoSiasg, 'xxxx') like :codigoSiasg)
            and IsNull(sjb.ID, -1) = IsNull(:id_sjb, IsNull(sjb.ID, -1))
            and IsNull(nd.ID, -1) = IsNull(:id_naturezaDespesa, IsNull(nd.ID, -1))
            and s.FlagAtivo = IsNull(:flagAtivo, s.FlagAtivo)
            and (:flagAcessaTodosMateriais IS NULL OR :flagAcessaTodosMateriais = 0 OR sjb.FlagAcessoRestrito = 0 OR sjb.ID IN (:sjbLiberados))
			order by s.Descricao");

            query.SetParameterList("sjbLiberados", sjbLiberados, NHibernateUtil.Int32);
            query.SetParameter("flagAcessaTodosMateriais", BusinessHelper.IsNull(flagAcessaTodosMateriais), NHibernateUtil.Boolean);
		    query.SetParameter("id_tipoServicoMaterial", BusinessHelper.IsNullOrZero(id_tipoServicoMaterial), NHibernateUtil.Int32);
            query.SetParameter("id_classe", BusinessHelper.IsNullOrZero(id_classe), NHibernateUtil.Int32);
            query.SetParameter("id_subClasse", BusinessHelper.IsNullOrZero(id_subClasse), NHibernateUtil.Int32);
            query.SetParameter("id_unidade", BusinessHelper.IsNullOrZero(id_unidade), NHibernateUtil.Int32);
            query.SetString("texto", string.Format("%{0}%", PreparaStringParaBusca(texto)));
            query.SetString("descricaoSingra", string.Format("%{0}%", descricaoSingra));
            query.SetString("codigoSiasg", string.Format("%{0}%", codigoSiasg));
            query.SetParameter("id_sjb", BusinessHelper.IsNullOrZero(id_sjb), NHibernateUtil.Int32);
            query.SetParameter("id_naturezaDespesa", BusinessHelper.IsNullOrZero(id_naturezaDespesa), NHibernateUtil.Int32);
            query.SetParameter("flagAtivo", BusinessHelper.IsNull(flagAtivo), NHibernateUtil.Boolean);
            //query.SetMaxResults(maxResults);
            pageSize = Math.Abs(pageSize);//conseta o bug do Int32.MaxValue
            query.SetMaxResults(pageNumber * pageSize + pageSize + 1);
            query.SetFirstResult(pageNumber * pageSize);

			return (List<ServicoMaterial>)query.List<ServicoMaterial>();
		}

        private static string PreparaStringParaBusca(string texto)
        {
            if (String.IsNullOrEmpty(texto)) return texto;

            if(texto[0] == '"' && texto[texto.Length - 1] == '"')
                return texto.Substring(1, texto.Length - 2);
            else
                return texto.Replace(" ", "%");
        }


        public static DataTable SelectSaldo(int id_servicoMaterial, int ano)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[3];
            param[1] = NullHelper.IsZero(id_servicoMaterial);
            param[2] = ano;
            
            return helper.ExecuteDataTable("ServicoMaterial_SelectSaldo", param);
        }
		#endregion

        #region Fast Search
        public static IEnumerable<ServicoMaterialUI> FastSearch(string descricao, TipoServicoMaterial? tipo, bool mostraNaoEstocavel, List<int> sjbLiberados, 
            bool? flagAcessaTodosMateriais)
        {
            List<int> tiposPermitidos = new List<int>();
            tiposPermitidos.Add(Convert.ToInt32(Business.TipoServicoMaterial.Material));
            tiposPermitidos.Add(Convert.ToInt32(Business.TipoServicoMaterial.Servico));
            if (mostraNaoEstocavel)
            {
                tiposPermitidos.Add(Convert.ToInt32(Business.TipoServicoMaterial.MaterialNaoEstocavel));
                tiposPermitidos.Add(Convert.ToInt32(Business.TipoServicoMaterial.Patrimonio));
            }
            if(sjbLiberados.Count == 0)
                sjbLiberados.Add(0);

            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select new ServicoMaterialUI(e.ID, e.Descricao, t.Descricao, s.Descricao, e.Unidade.Descricao)
            from ServicoMaterial e 
                inner join e.SubClasseServicoMaterial s 
                inner join s.ClasseServicoMaterial t
                inner join e.SJB sjb
			where (e.Descricao like :descricao
			or t.Descricao like :descricao
			or s.Descricao like :descricao)
            and e.FlagAtivo = 1
			and e.TipoServicoMaterial = IsNull(:tipo, e.TipoServicoMaterial)
            and e.TipoServicoMaterial in (:tiposPermitidos)            
            and (:flagAcessaTodosMateriais IS NULL OR :flagAcessaTodosMateriais = 0 OR sjb.FlagAcessoRestrito = 0 OR sjb.ID IN (:sjbLiberados))
			order by t.Descricao, s.Descricao, e.Descricao");


            query.SetMaxResults(20);
            query.SetString("descricao", "%" + descricao + "%");
            query.SetParameterList("sjbLiberados", sjbLiberados, NHibernateUtil.Int32);
            query.SetParameter("flagAcessaTodosMateriais", BusinessHelper.IsNull(flagAcessaTodosMateriais), NHibernateUtil.Boolean);
            query.SetParameter("tipo", BusinessHelper.IsNull(tipo.HasValue ? Convert.ToInt32(tipo) : Int32.MinValue), NHibernateUtil.Int32);
            query.SetParameterList("tiposPermitidos", tiposPermitidos);
            
            return query.List<ServicoMaterialUI>();
        }
        #endregion

        #region Equipamentos & Localizacoes
	    public virtual void RemoveEquipamento(int id_equipamento)
	    {
	        Equipamento e = Equipamento.Get(id_equipamento);
	        this.Equipamentos.Remove(e);
	        this.Save();
	    }

	    public virtual void AddEquipamento(int id_equipamento)
	    {
            Equipamento e = Equipamento.Get(id_equipamento);
	        this.Equipamentos.Add(e);
	        this.Save();
	    }

        public virtual void RemoveLocalizacao(int id_localizacao)
        {
            Localizacao l = new Localizacao();
            foreach (Localizacao localizacao in _localizacoes)
            {
                if (localizacao.ID == id_localizacao)
                    l = localizacao;
            }

            this.Localizacoes.Remove(l);
            //l.Delete();
            this.Save();
        }

        public virtual void AddLocalizacao(int id_localizacao)
        {
            Localizacao l = Localizacao.Get(id_localizacao);
            this.Localizacoes.Add(l);
            this.Save();
        }


        public virtual void RemoveCelula(int id_celula)
        {
            Celula c = new Celula();
            foreach (Celula celula in _celulas)
            {
                if (celula.ID == id_celula)
                    c = celula;
            }

            this.Celulas.Remove(c);
            //l.Delete();
            this.Save();
        }

        public virtual void AddCelula(int id_celula)
        {
            Celula c = Celula.Get(id_celula);
            this.Celulas.Add(c);
            this.Save();
        }
        #endregion

        public static ServicoMaterial GetByCodigo(string codigo, TipoServicoMaterial? tipo)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from ServicoMaterial e 
                inner join fetch e.SubClasseServicoMaterial s 
                inner join fetch s.ClasseServicoMaterial t                
			where e.CodigoInterno = :codigo
			and e.TipoServicoMaterial = IsNull(:tipo, e.TipoServicoMaterial)");

            query.SetMaxResults(1);
            query.SetString("codigo", codigo);
            query.SetParameter("tipo", BusinessHelper.IsNull(tipo.HasValue ? Convert.ToInt32(tipo) : Int32.MinValue), NHibernateUtil.Int32);

            return query.UniqueResult<ServicoMaterial>();
        }

        public static List<ServicoMaterial> GetDuplicado(string descricao, string numeroSerie, TipoServicoMaterial? tipo)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from ServicoMaterial e 
                inner join fetch e.SubClasseServicoMaterial s 
                inner join fetch s.ClasseServicoMaterial t                
			where e.NumeroSerie = :numeroSerie
			and e.Descricao = :descricao
			and e.TipoServicoMaterial = IsNull(:tipo, e.TipoServicoMaterial)");

            query.SetMaxResults(1);
            query.SetString("descricao", descricao);
            query.SetString("numeroSerie", numeroSerie);
            query.SetParameter("tipo", BusinessHelper.IsNull(tipo.HasValue ? Convert.ToInt32(tipo) : Int32.MinValue), NHibernateUtil.Int32);

            return (List<ServicoMaterial>)query.List<ServicoMaterial>();
        }

        public override void Save()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select count(*) from ServicoMaterial s 
			where s.CodigoInterno = :codigoInterno
            AND s.ID != :id_servicoMaterial");

            query.SetString("codigoInterno", this._codigointerno);
            query.SetInt32("id_servicoMaterial", this.ID);

            if (Convert.ToInt32(query.UniqueResult()) > 0)
                throw new Exception("Já existe Serviço/Material com o mesmo Código Interno.");

            base.Save();
        }

	    public virtual int GetQuantidadeMinima(OrigemMaterial origemMaterial)
	    {
            if (origemMaterial == OrigemMaterial.PEP)
                return _quantidadeMinimaPEP;
            else if (origemMaterial == OrigemMaterial.Rodizio)
                return _quantidadeMinimaRodizio;

	        return 0;
	    }

        public virtual int GetQuantidadeMaxima(OrigemMaterial origemMaterial)
        {
            if (origemMaterial == OrigemMaterial.PEP)
                return _quantidadeMaximaPEP;
            else if (origemMaterial == OrigemMaterial.Rodizio)
                return _quantidadeMaximaRodizio;
           

            return 0;
        }
	}
}
