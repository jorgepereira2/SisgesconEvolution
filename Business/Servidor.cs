using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using NHibernate;
using Shared.Common;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class Servidor : BusinessObject<Servidor>, IComparable<Servidor>
	{
        #region Private Members
      

	    #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public Servidor()
        {
           
            this.TipoServidor = TipoServidor.MilitarGola;
            
            _processos = new CustomList<Processo>();
            _perfis = new CustomList<PerfilAcesso>();
            _sjbLiberados = new CustomList<SJB>();
            _historicos = new CustomList<ServidorHistorico>();
            _dependentes = new CustomList<ServidorDependente>();
            _condecoracoes = new CustomList<ServidorCondecoracao>();
            _cursosMilitares = new CustomList<ServidorCursoMilitar>();
            Funcao = FuncaoServidor.Outras;
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Public Properties

	    public virtual string DiscriminacaoFuncao { get; set; }

	    public virtual bool FlagPodeFazerPOOutraCelula { get; set; }

	    public virtual bool FlagFazAtividadeDireta { get; set; }

	    public virtual bool FlagPodeVerPAOutraCelula { get; set; }

	    public virtual byte[] Foto { get; set; }

        public virtual string NIP { get; set; }
        
        public virtual string NomeCompleto { get; set; }

        public virtual string NomeGuerra { get; set; }

        public virtual DateTime? DataSaida { get; set; }

        public virtual TipoServidor TipoServidor { get; set; }
        
        public virtual string Graduacao{ get; set; }
        
        public virtual Celula Celula{ get; set; }

        public virtual string Login { get; set; }
        
        public virtual string Senha{ get; set; }
        
        public virtual string Email{ get; set; }
        
        public virtual bool FlagPrimeiroAcesso{ get; set; }
        
        public virtual string Telefone{ get; set; }
        
        public virtual string Celular{ get; set; }
        
        public virtual bool FlagUsuario{ get; set; }

        public virtual FuncaoServidor Funcao { get; set; }

        public virtual string Identidade { get; set; }
        public virtual string OrgaoEmissor { get; set; }
        public virtual string CPF { get; set; }
        public virtual string PASEP { get; set; }
        public virtual string NumeroTituloEleitoral { get; set; }
        public virtual string SecaoTituloEleitoral { get; set; }
        public virtual string ZonaTituloEleitoral { get; set; }
        public virtual string NumeroCarteiraMotorista { get; set; }
        public virtual bool DoadorOrgaos { get; set; }
        public virtual TipoSanguineo TipoSanguineo { get; set; }
        public virtual string FatorRH { get; set; }
        public virtual DateTime? DataNascimento { get; set; }
        public virtual string Naturalidade { get; set; }
        public virtual string NomePai { get; set; }
        public virtual string NomeMae { get; set; }
        public virtual string Endereco { get; set; }
        public virtual string Bairro { get; set; }
        public virtual Municipio Municipio { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual string CEP { get; set; }
        public virtual string TelefoneResidencial { get; set; }
        public virtual string NomePessoaContato { get; set; }
        public virtual string EnderecoContato { get; set; }
        public virtual string BairroContato { get; set; }
        public virtual Municipio MunicipioContato { get; set; }
        public virtual Estado EstadoContato { get; set; }
        public virtual string CEPContato { get; set; }
        public virtual string TelefoneContato { get; set; }
        public virtual string CelularContato { get; set; }
        public virtual EstadoCivil EstadoCivil { get; set; }
        public virtual string NomeConjuge { get; set; }
        public virtual DateTime? DataIncorporacaoMB { get; set; }
        public virtual Cliente OMIncorporacao { get; set; }
        public virtual DateTime? DataUltimaPromocao { get; set; }
        public virtual DateTime? DataApresentacao { get; set; }
        public virtual Cliente OMOrigem { get; set; }
        public virtual bool LinguaEstrangeira { get; set; }
        public virtual string DescricaoLinguaEstrangeira { get; set; }
        public virtual bool PraticaEsporte { get; set; }
        public virtual string EspecificacaoEsporte { get; set; }
        public virtual bool DesejaHorasFunebres { get; set; }
        public virtual string Religiao { get; set; }
        public virtual string OutrasHabilidades { get; set; }


        #endregion 

        #region Advanced Properties
        public virtual string Identificacao
        {
            get { return string.Format("{0} {1}", Graduacao, NomeGuerra); }
        }
        #endregion

        #region collection

        private ICustomList<Processo> _processos;
        private ICustomList<PerfilAcesso> _perfis;
        private ICustomList<SJB> _sjbLiberados;
        private ICustomList<ServidorHistorico> _historicos;
	    private bool _flagAcessaTodosMateriais;
        private ICustomList<ServidorDependente> _dependentes;
        private ICustomList<ServidorCondecoracao> _condecoracoes;
        private ICustomList<ServidorCursoMilitar> _cursosMilitares;

	    public virtual ICustomList<SJB> SJBLiberados
        {
            get { return _sjbLiberados; }
            set { _sjbLiberados = value; }
        }

        public virtual ICustomList<PerfilAcesso> Perfis
        {
            get { return _perfis; }
            set { _perfis = value; }
        }

		public virtual ICustomList<Processo> Processos
		{
			get { return _processos; }
			set { _processos = value; }
		}

	    public virtual bool FlagAcessaTodosMateriais
	    {
	        get { return _flagAcessaTodosMateriais; }
	        set { _flagAcessaTodosMateriais = value; }
	    }

        public virtual ICustomList<ServidorHistorico> Historicos
        {
            get { return _historicos; }
            set { _historicos = value; }
        }

        public virtual ICustomList<ServidorDependente> Dependentes
        {
            get { return _dependentes; }
            set { _dependentes = value; }
        }

        public virtual ICustomList<ServidorCondecoracao> Condecoracoes
        {
            get { return _condecoracoes; }
            set { _condecoracoes = value; }
        }

        public virtual ICustomList<ServidorCursoMilitar> CursosMilitares
        {
            get { return _cursosMilitares; }
            set { _cursosMilitares = value; }
        }

	    #endregion

		#region Public Methods
		/// <summary>
		/// Valida as credenciais do usuário.
		/// </summary>
		/// <param name="userName">Login do usuário</param>
		/// <param name="password">Senha do usuário</param>
		/// <returns>
		/// Caso as credenciais sejam válidas retorna a conta correspondente, 
		/// caso contrário retorna null
		/// </returns>
		public static Servidor Get(string userName, string password)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
				@"from Servidor s 
				where s.Login = :Login
				and s.Senha = :Senha
				and s.FlagUsuario = 1");
			query.SetString("Login", userName);
			query.SetString("Senha", GetSenhaCriptografada(password));

			Servidor servidor = query.UniqueResult<Servidor>();
			if (servidor == null || servidor.DataSaida.HasValue)
				return null;
			else
				return servidor;
		}

        public static Dictionary<int, string> List(FuncaoServidor? funcao)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                @"select s.ID, s.NomeGuerra, s.Graduacao from Servidor s
                where s.Funcao = IsNull(:funcao, s.Funcao) 				
                and s.DataSaida IS NULL
				order by s.Graduacao");

            query.SetParameter("funcao", BusinessHelper.IsNull(funcao.HasValue ? Convert.ToInt32(funcao) : Int32.MinValue), NHibernateUtil.Int32);
            return GetList(query);
        }

	    private static Dictionary<int, string> GetList(IQuery query)
	    {
	        Dictionary<int, string> servidores = new Dictionary<int, string>();
	        IList list = query.List();
	        foreach (object o in list)
	        {
	            object[] row = (object[]) o;
	            servidores.Add(Convert.ToInt32(row[0]), string.Format("{0} - {1}", row[2], row[1]));
	        }
	        return servidores;
	    }

        /// <summary>
        /// Retorna todos os servidores ligados a uma divisão
        /// </summary>
	    public static Dictionary<int, string> ListPorDivisao(Celula divisao)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                @"select s.ID, s.NomeGuerra, s.Graduacao from Servidor s
                where s.Celula.Codigo like :codigoCelula				
				order by s.NomeGuerra");

            query.SetString("codigoCelula", divisao.Codigo + "%");
            return GetList(query);
        }

        public static Dictionary<int, string> ListPorCelula(int id_celula, bool? flagFazAtividadeDireta)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                @"select s.ID, s.NomeGuerra, s.Graduacao from Servidor s
                where s.Celula.ID = :id_celula
                and s.FlagFazAtividadeDireta = IsNull(:flagFazAtividadeDireta, s.FlagFazAtividadeDireta)
                and s.DataSaida is null
				order by s.NomeGuerra");

            query.SetInt32("id_celula", id_celula);
            query.SetParameter("flagFazAtividadeDireta", BusinessHelper.IsNull(flagFazAtividadeDireta), NHibernateUtil.Boolean);
            return GetList(query);
        }


	    public static List<Servidor> Select(string texto, int id_divisao)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
                @"from Servidor s inner join fetch s.Celula c
				where (s.NomeCompleto like :texto
				OR s.NomeGuerra like :texto
				OR s.Graduacao like :texto)
                AND dbo.celulapertenceacelula(c.ID, :id_divisao) = 1
				order by s.NomeGuerra");
			query.SetString("texto", "%" + texto + "%");
            query.SetParameter("id_divisao", BusinessHelper.IsNullOrZero(id_divisao), NHibernateUtil.Int32);

			return (List<Servidor>)query.List<Servidor>();
		}

        public static List<Servidor> Select(string graduacao, int id_celula, bool? flagFazAtividadeDireta, bool? ativo)
        {
            string strAtivo = "";
            if (ativo.HasValue)
                if (ativo.Value)
                    strAtivo = " AND s.DataSaida Is Null ";
                else
                    strAtivo = " AND s.DataSaida Is Not Null ";

            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                @"from Servidor s 
                    inner join fetch s.Celula c
				where s.Graduacao like :graduacao
				and (c.ID = IsNull(:id_celula, c.ID) OR c.ID in (:celulasAcima))
                and s.FlagFazAtividadeDireta = IsNull(:flagFazAtividadeDireta, s.FlagFazAtividadeDireta)
                " + strAtivo + @" 
				order by s.NomeGuerra");

            //temos que mostrar os servidores das celulas acima da celula escolhida
            if(id_celula > 0)
            {
                Celula celula = Business.Celula.Get(id_celula);
                if(celula.TipoCelula == TipoCelula.Departamento)
                    query.SetInt32("celulasAcima", 0);
                else if (celula.TipoCelula == TipoCelula.Divisao)
                    query.SetInt32("celulasAcima", celula.GetDepartamento().ID);
                else
                {
                    List<int> celulasAcima = new List<int>(2);
                    celulasAcima.Add(celula.GetDivisao().ID);
                    celulasAcima.Add(celula.GetDivisao().GetDepartamento().ID);
                    query.SetParameterList("celulasAcima", celulasAcima, NHibernateUtil.Int32);
                }
            }
            else
                query.SetInt32("celulasAcima", 0);

            query.SetString("graduacao", "%" + graduacao + "%");
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            query.SetParameter("flagFazAtividadeDireta", BusinessHelper.IsNull(flagFazAtividadeDireta), NHibernateUtil.Boolean);

            return (List<Servidor>)query.List<Servidor>();
        }
		#endregion

        #region Fast Search
        public static IEnumerable<ServidorUI> FastSearch(string nome)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select new ServidorUI(s.ID, s.NomeCompleto)
            from Servidor s                
			where (s.NomeCompleto like :nome)
            and s.DataSaida IS NULL			
			order by s.NomeCompleto");

            query.SetMaxResults(20);
            query.SetString("nome", "%" + nome + "%");

            return query.List<ServidorUI>();
        }
        #endregion

	    #region Login/Senha

	    public virtual void AlterarSenha(string senha)
	    {
	        this.Senha = GetSenhaCriptografada(senha);
	        this.Save();
	    }

	    protected static string GetSenhaCriptografada(string clearText)
	    {
	        return EncDec.Encrypt(clearText, "xyks01wz");
	    }

	    public virtual string GetSenhaNaoCriptografada()
	    {
	        if (Senha == null)
	            return "";
	        return EncDec.Decrypt(Senha, "xyks01wz");
	    }

	    /// <summary>
	    /// Habilita o usuário a utilizar o sistema e envia sua senha por email
	    /// </summary>
	    public virtual void EnviarSenhaEmail()
	    {
	        if (String.IsNullOrEmpty(this.Email))
	            throw new Exception("A pessoa não possui um e-mail cadastrado.");

	        //Verifica se é a primeira vez 
	        if (String.IsNullOrEmpty(this.Senha))
	        {
	            this.Senha = GetSenhaCriptografada(RandomPassword.Generate(4, 6));
	            this.Login = GetLogin();
	            this.FlagPrimeiroAcesso = true;
	            this.FlagUsuario = true;
	            this.Save();


	        }

	        object[] campos = new object[3];
	        campos[0] = this.NomeCompleto;
	        campos[1] = this.Login;
	        campos[2] = this.GetSenhaNaoCriptografada();

	        string texto = string.Format(
	            "<html><body><br><b><font color=\"#000000\">Prezado(a) {0},</font></b><br><br>Seja bem-vindo ao Sistema.<br><br>Segue abaixo um lembrente de suas credenciais de acesso ao sistema:<br><br><blockquote>Login: {1}<br>Senha: {2}<br></blockquote><font>Acesse&nbsp; e utilize os dados acima para seu primeiro acesso, neste primeiro acesso por segurança você deverá alterar seu login e senha, desta forma inutilizando este e-mail.</font><br><br><br><font>Atenciosamente,<br><br><br></font></html></body>",
	            campos);


	        Email email = new Email();
	        email.Assunto = "Bem-vindo ao Sistema";
	        email.Remetente = ConfigurationManager.AppSettings["EmailSender"];
	        email.Destinatario = this.Email;
	        email.Texto = texto;
	        email.Enviar();
	    }

	    private string GetLogin()
	    {
	        string[] nomes = NomeCompleto.Split(' ');
	        if (nomes.Length == 1)
	        {
                return NomeCompleto;
	        }
	        else
	        {
	            string login = nomes[0].Substring(0, 1);
	            return login + nomes[nomes.Length - 1].ToString();
	        }
	    }

        public virtual void SalvarSenha(string login, string senha, bool flagPrimeiroAcesso)
        {
            FlagUsuario = true;
            FlagPrimeiroAcesso = flagPrimeiroAcesso;
            Senha = GetSenhaCriptografada(senha);
            Login = login;
            this.Save();
        }
	    #endregion

        public static DataTable GetDataTableUsuarios()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                @"select p.ID, p.NomeGuerra, p.Celula.ID, p.Graduacao 
				from Servidor p 
				where p.DataSaida is null
				and p.FlagUsuario = 1
				order by p.NomeGuerra");

            IList list = query.List();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID_Servidor", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Celula", typeof(int)));
            dt.Columns.Add(new DataColumn("NomeGuerra", typeof(string)));

            foreach (object[] obj in list)
            {
                DataRow row = dt.NewRow();
                row["ID_Servidor"] = Convert.ToInt32(obj[0]);
                row["NomeGuerra"] = string.Format("{0} - {1}", obj[3].ToString(), obj[1].ToString());
                row["ID_Celula"] = Convert.ToInt32(obj[2]);
                dt.Rows.Add(row);
            }
            return dt;
        }

        public virtual int CompareTo(Servidor other)
	    {
            if (other == null) return 1;
	        return Identificacao.CompareTo(other.Identificacao);
	    }

        public override string ToString()
        {
            return Identificacao;
        }

        public override void Save()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select count(*) from Servidor s 
			where s.NIP = :nip
            AND s.ID != :id_servidor");

            query.SetMaxResults(1);
            query.SetString("nip", this.NIP);
            query.SetInt32("id_servidor", this.ID);

            if (Convert.ToInt32(query.UniqueResult()) > 0)
                throw new Exception("Já existe servidor com o mesmo NIP.");

            base.Save();
        }

        /// <summary>
        /// Verifica no flag do servidor e nos perfis do servidor se ele pode fazer POs de outra celula
        /// </summary>
	    public virtual bool GetFlagPodeFazerPOOutraCelula()
	    {
	        if(FlagPodeFazerPOOutraCelula) return true;
            else
	        {
	            foreach (PerfilAcesso perfil in _perfis)
	            {
	                if(perfil.FlagPodeFazerPOOutraCelula) return true;
	            }
	            return false;
	        }
	    }

	    public static Servidor GetByNIP(string nip)
	    {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(@"from Servidor s where s.NIP = :nip");
            query.SetMaxResults(1);
            query.SetString("nip", nip);

	        return query.UniqueResult<Servidor>();

	    }

        /// <summary>
        /// Retorna a coleção de servidores
        /// </summary>        
        public static List<Servidor> SelectFromProcesso(string ids_processo, string nome)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();

            string Sql = @"select 
                                distinct s from Servidor s
					            left join s.Processos p
				            where 
                                s.NomeCompleto like :Nome
				            and p.ID IN (:ids_processo)
				            order by s.NomeCompleto";

            IQuery query = session.CreateQuery(Sql);


            //query.SetParameterList("ids_processo", Util.LeLista(ids_processo));
            query.SetString("Nome", "%" + nome + "%");
            query.SetString("ids_processo", ids_processo);

            return (List<Servidor>)query.List<Servidor>();
        }

        /// <summary>
        /// Retorna a coleção de pessoas
        /// </summary>        
        public static List<Servidor> SelectFromPerfilAcesso(string ids_perfil, string nome)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();

            string Sql = @"select 
                                distinct p from Servidor p 
					            left join fetch p.Perfis ppa
				            where 
                                p.NomeCompleto like :Nome
				            and ppa.ID IN (:ids_perfil)
				            order by p.NomeCompleto";            

            IQuery query = session.CreateQuery(Sql);

            query.SetParameterList("ids_perfil", Util.LeLista(ids_perfil));
            query.SetString("Nome", "%" + nome + "%");

            return (List<Servidor>)query.List<Servidor>();
        }
	}
}