using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using Shared.Common;
using Shared.DataAccessHelper;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class Fornecedor : BusinessObject<Fornecedor>, IComparable<Fornecedor>
	{
		#region Private Members
		private TipoFornecedor _tipofornecedor; 
		private string _cnpj; 
		private string _razaosocial; 
		private string _observacao; 
		private string _numerobanco; 
		private string _contacorrente; 
		private string _agencia; 
		private string _endereco; 
		private string _bairro; 
		private Municipio _municipio; 
		private Estado _estado; 
		private string _cep; 
		private string _telefone; 
		private string _fax; 
		private string _email; 
		private string _homepage; 
		private DateTime? _validadecertidaoreceitafederal; 
		private DateTime? _validadecertidaofgts; 
		private DateTime? _validadecertidaodividauniao; 
		private string _descricaomaterialfornecido; 
		private bool _flagativo; 
		private string _motivoinativo;
	    private bool _flagOM;
	    private bool _flagOptanteSimples;
	    private TipoPessoa _tipoPessoa;

		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Fornecedor()
		{
			_tipofornecedor = null; 
			_cnpj = null; 
			_razaosocial = null; 
			_observacao = null; 
			_numerobanco = null; 
			_contacorrente = null; 
			_agencia = null; 
			_endereco = null; 
			_bairro = null; 
			_municipio =  null; 
			_estado =  null; 
			_cep = null; 
			_telefone = null; 
			_fax = null; 
			_email = null; 
			_homepage = null; 
			_validadecertidaoreceitafederal = DateTime.MinValue; 
			_validadecertidaofgts = DateTime.MinValue; 
			_validadecertidaodividauniao = DateTime.MinValue; 
			_descricaomaterialfornecido = null; 
			_flagativo = false;
		    _flagOM = false;
			_motivoinativo = null;
		    _contatos = new CustomList<FornecedorContato>();
		    DataCadastro = DateTime.Now;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual DateTime DataCadastro { get; set; }
        public virtual string NumeroContrato { get; set; }
        
        public virtual TipoPessoa TipoPessoa
	    {
	        get { return _tipoPessoa; }
	        set { _tipoPessoa = value; }
	    }
        public virtual bool FlagOptanteSimples
        {
            get { return _flagOptanteSimples; }
            set { _flagOptanteSimples = value; }
        }

        public virtual bool FlagOM
        {
            get { return _flagOM; }
            set { _flagOM = value; }
        }	
		public virtual TipoFornecedor TipoFornecedor
		{
			get { return _tipofornecedor; }
			set { _tipofornecedor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string CNPJ
		{
			get { return _cnpj; }
			set	
			{
				
                //if(!Util.ValidaCPF(_cnpj))
                //    throw new Exception("O CNPJ fornecido é inválido.");
				
				_cnpj = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string RazaoSocial
		{
			get { return _razaosocial; }
			set	
			{
				
				
				_razaosocial = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Observacao
		{
			get { return _observacao; }
			set	
			{
				
				_observacao = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string NumeroBanco
		{
			get { return _numerobanco; }
			set	
			{
				
				
				_numerobanco = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string ContaCorrente
		{
			get { return _contacorrente; }
			set	
			{
				
				
				_contacorrente = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Agencia
		{
			get { return _agencia; }
			set	
			{
			
				
				_agencia = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Endereco
		{
			get { return _endereco; }
			set	
			{
			
				_endereco = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Bairro
		{
			get { return _bairro; }
			set	
			{
				
				
				_bairro = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Municipio Municipio
		{
			get { return _municipio; }
			set { _municipio = value; }
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
		public virtual string CEP
		{
			get { return _cep; }
			set	
			{
				
				
				_cep = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Telefone
		{
			get { return _telefone; }
			set	
			{
				
				
				_telefone = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Fax
		{
			get { return _fax; }
			set	
			{
				
				
				_fax = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Email
		{
			get { return _email; }
			set	
			{
				
				_email = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string HomePage
		{
			get { return _homepage; }
			set	
			{
				
				
				_homepage = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime? ValidadeCertidaoReceitaFederal
		{
			get { return _validadecertidaoreceitafederal; }
			set { _validadecertidaoreceitafederal = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime? ValidadeCertidaoFGTS
		{
			get { return _validadecertidaofgts; }
			set { _validadecertidaofgts = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime? ValidadeCertidaoDividaUniao
		{
			get { return _validadecertidaodividauniao; }
			set { _validadecertidaodividauniao = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string DescricaoMaterialFornecido
		{
			get { return _descricaomaterialfornecido; }
			set	
			{
				_descricaomaterialfornecido = value;
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
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string MotivoInativo
		{
			get { return _motivoinativo; }
			set	
			{
				
				
				_motivoinativo = value;
			}
		}
			#endregion

	    #region Collections

	    private ICustomList<FornecedorContato> _contatos;
	    
	    public virtual ICustomList<FornecedorContato> Contatos
	    {
	        get { return _contatos; }
	        set { _contatos = value; }
	    }

	    public virtual string DescricaoContatos
	    {
	        get
	        {
	            string contatos = "";
	            foreach (FornecedorContato contato in _contatos)
	            {
	                contatos += contato.Nome + ",";
	            }
	            return contatos;
	        }
	    }
	    
	    #endregion

		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select f.ID, f.RazaoSocial 
			from Fornecedor f  
			where f.FlagAtivo = 1
			order by f.RazaoSocial");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<Fornecedor> Select(string texto, int id_tipoFornecedor, string materialFornecido, string numeroContrato, DateTime dataInicio, DateTime dataFim, int pageSize, int pageNumber)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from Fornecedor f 
			where (f.RazaoSocial like :texto
			OR f.CNPJ like :texto)
			and f.TipoFornecedor.ID = IsNull(:id_tipoFornecedor, f.TipoFornecedor.ID)
			and IsNull(f.DescricaoMaterialFornecido, 'xxx') like IsNull(:materialFornecido, IsNull(f.DescricaoMaterialFornecido, 'xxx'))
            and IsNull(f.NumeroContrato, 'xxx') like IsNull(:numeroContrato, IsNull(f.NumeroContrato, 'xxx'))
            and dbo.DateIsInBetween(f.DataCadastro, :dataInicio, :dataFim) = 1			
			order by f.RazaoSocial");

		    query.SetString("texto", string.Format("%{0}%", texto));
            query.SetString("materialFornecido", string.Format("%{0}%", materialFornecido));
            query.SetString("numeroContrato", string.Format("%{0}%", numeroContrato));
		    query.SetParameter("id_tipoFornecedor", BusinessHelper.IsNullOrZero(id_tipoFornecedor), NHibernateUtil.Int32);
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
		    pageSize = Math.Abs(pageSize);//conserta o bug do Int32.MaxValue
            query.SetMaxResults(pageNumber * pageSize + pageSize + 1);
		    query.SetFirstResult(pageNumber *pageSize);
			return (List<Fornecedor>)query.List<Fornecedor>();
		}
		
		#endregion

        #region Fast Search
        public static IEnumerable<FornecedorUI> FastSearch(string texto)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select new FornecedorUI(e.ID, e.RazaoSocial)
            from Fornecedor e 
			where (e.RazaoSocial like :texto or e.CNPJ like :texto)			
			order by e.RazaoSocial");

            query.SetMaxResults(20);
            query.SetString("texto", "%" + texto + "%");
            return query.List<FornecedorUI>();
        }
        #endregion

        public override void Save()
        {
            if(!_flagativo && MotivoInativo == null)
                throw new Exception("Preencha o campo Motivo Inativo.");

            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select count(*) from Fornecedor f 
			where f.CNPJ = :cnpj
            AND f.ID != :id_fornecedor");

            query.SetMaxResults(1);
            query.SetString("cnpj", this._cnpj);
            query.SetInt32("id_fornecedor", this.ID);

            if(Convert.ToInt32(query.UniqueResult()) > 0)
                throw new Exception("Já existe fornecedor com o mesmo CNPJ/CPF.");

            base.Save();
        }

        public virtual int CompareTo(Fornecedor other)
	    {
	        return _razaosocial.CompareTo(other.RazaoSocial);
	    }

        public override string ToString()
        {
            return _razaosocial;
        }

        public static DataTable SelectSaldo(int id_tipoCompra, int ano, string texto, int id_tipoFornecedor, string materialFornecido)
	    {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[6];
            param[1] = id_tipoCompra;
            param[2] = ano;
            param[3] = NullHelper.IsZero(id_tipoFornecedor);
            param[4] = NullHelper.IsNull(texto);
            param[5] = NullHelper.IsNull(materialFornecido);

            return helper.ExecuteDataTable("Fornecedor_SelectSaldo", param);
	    }
	}
}
