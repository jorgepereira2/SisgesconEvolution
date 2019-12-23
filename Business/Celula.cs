using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using Shared.Common;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class Celula : BusinessObject<Celula>, IComparable<Celula>
	{
        #region Private Members
        private string _codigo;
        private string _descricao;
        private bool _flagativo;
        private bool _flagcentrocusto;
        private bool _flagoficina;
        private string _finalidade;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public Celula()
        {
            _codigo = null;
            _descricao = null;
            _flagativo = false;
            _flagcentrocusto = false;
            _flagoficina = false;
            _finalidade = null;
            Delineadores = new Shared.NHibernateDAL.CustomList<Servidor>();
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>		
        public virtual string Codigo
        {
            get { return _codigo; }
            set
            {
                if (value != null)
                    if (value.Length > 5)
                        throw new ArgumentOutOfRangeException("Invalid value for Codigo", value, value.ToString());

                _codigo = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string Descricao
        {
            get { return _descricao; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for Descricao", value, value.ToString());

                _descricao = value;
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
        public virtual bool FlagCentroCusto
        {
            get { return _flagcentrocusto; }
            set { _flagcentrocusto = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual bool FlagOficina
        {
            get { return _flagoficina; }
            set { _flagoficina = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string Finalidade
        {
            get { return _finalidade; }
            set
            {
                if (value != null)
                    if (value.Length > 500)
                        throw new ArgumentOutOfRangeException("Invalid value for Finalidade", value, value.ToString());

                _finalidade = value;
            }
        }

	    public virtual ICustomList<Servidor> Delineadores { get; set; }
        public virtual bool FlagMergulho { get; set; }
	    
        #endregion

	    #region Advanced Properties

	    public virtual TipoCelula TipoCelula
	    {
	        get
	        {
	            if(_codigo.Contains("."))
	                return TipoCelula.Secao;
	            else
	            {
	                int numero = Int32.Parse(_codigo);
	                if(numero >= 10 && numero % 10 == 0 || (numero >= 1 && numero <=9))
	                    return TipoCelula.Departamento;
	                else
	                    return TipoCelula.Divisao;
	            } 
	        }
	    }

	    #endregion

		public static List<Celula> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery("from Celula c order by c.Codigo");

			return (List<Celula>) query.List<Celula>();
		}

        public static List<Celula> Select(TipoCelula tipo, bool? flagOficina)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery("from Celula c order by c.Codigo");

            return (List<Celula>)query.List<Celula>();
        }

        public static Dictionary<int, string> List()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                @"from Celula c 
                where c.FlagAtivo = 1
                order by c.Codigo");

            Dictionary<int, string> celulas = new Dictionary<int, string>();
            IList<Celula> list = query.List<Celula>();
            foreach (Celula celula in list)
            {

                celulas.Add(celula.ID, string.Format("{0}{1} - {2}", GetIdentacao(celula), celula.Codigo, celula.Descricao));
            }
            return celulas;
        }

        public static Dictionary<int, string> List(TipoCelula? tipo, bool? flagOficina)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from Celula c 
            where c.FlagOficina = IsNull(:flagOficina, c.FlagOficina)
            and c.FlagAtivo = 1
            order by c.Codigo"
            );

            query.SetParameter("flagOficina", BusinessHelper.IsNull(flagOficina), NHibernateUtil.Boolean);

            Dictionary<int, string> celulas = new Dictionary<int, string>();
            IList<Celula> list = query.List<Celula>();
            foreach (Celula celula in list)
            {
                if(tipo == null || celula.TipoCelula == tipo)
                    celulas.Add(celula.ID, string.Format("{0} - {1}", celula.Codigo, celula.Descricao));
            }
            return celulas;
        }

        public static Dictionary<int, string> List(params TipoCelula[] tipos)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from Celula c 
            where c.FlagAtivo = 1
            order by c.Codigo"
            );

            
            Dictionary<int, string> celulas = new Dictionary<int, string>();
            IList<Celula> list = query.List<Celula>();
            foreach (Celula celula in list)
            {
                foreach (TipoCelula tipo in tipos)
                {
                    if (celula.TipoCelula == tipo)
                    {
                        celulas.Add(celula.ID, string.Format("{2}{0} - {1}", celula.Codigo, celula.Descricao, GetIdentacao(celula)));
                        break;
                    }
                }   
            }
            return celulas;
        }

        public static Dictionary<int, string> ListCelulasSubordinadas(int id_servidor, bool? flagOficina)
        {
            return ListCelulasSubordinadas(id_servidor, flagOficina, null);
        }

        public static Dictionary<int, string> ListCelulasSubordinadas(int id_servidor, bool? flagOficina, bool? flagMergulho)
        {
            Servidor servidor = Servidor.Get(id_servidor);

            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                @"from Celula c 
                where c.FlagAtivo = 1
                and   c.Codigo like :codigoCelula 
                and  c.FlagOficina = IsNull(:flagOficina, c.FlagOficina)       
                and  c.FlagMergulho = IsNull(:flagMergulho, c.FlagMergulho)       
                        
                order by c.Codigo");

            int numeroLetras = 1;
            if (servidor.Celula.TipoCelula == TipoCelula.Divisao)
                numeroLetras = 2;
            else if (servidor.Celula.TipoCelula == TipoCelula.Secao)
                numeroLetras = 5;
            //pegamos a divisao caso seja celula
            if (servidor.Celula.TipoCelula == TipoCelula.Secao)
                query.SetString("codigoCelula", servidor.Celula.GetDivisao().Codigo.Substring(0, 2) + "%");
            else
                query.SetString("codigoCelula", servidor.Celula.Codigo.Substring(0, numeroLetras) + "%");

            query.SetParameter("flagOficina", BusinessHelper.IsNull(flagOficina), NHibernateUtil.Boolean);
            query.SetParameter("flagMergulho", BusinessHelper.IsNull(flagMergulho), NHibernateUtil.Boolean);
            Dictionary<int, string> celulas = new Dictionary<int, string>();
            IList<Celula> list = query.List<Celula>();
            foreach (Celula celula in list)
            {

                //Verifica se pode adicionar
                if (servidor.Celula.TipoCelula == TipoCelula.Divisao && celula.TipoCelula == TipoCelula.Departamento)
                    continue;
                if (servidor.Celula.TipoCelula == TipoCelula.Secao && (celula.TipoCelula == TipoCelula.Departamento))//if (servidor.Celula.TipoCelula == TipoCelula.Secao && (celula.TipoCelula == TipoCelula.Departamento || celula.TipoCelula == TipoCelula.Divisao))
                    continue;

                celulas.Add(celula.ID, string.Format("{0}{1} - {2}", GetIdentacao(celula), celula.Codigo, celula.Descricao));
            }
            return celulas;
        }

        private static string GetIdentacao(Celula celula)
        {   
            if (celula.TipoCelula == TipoCelula.Divisao)
                return "   ";
            else if (celula.TipoCelula == TipoCelula.Secao)
                return "      ";

            return "";
        }

        public override void Save()
        {
            //TODO: Validar se codigo ja existe e se está dentro do padrão
            base.Save();
        }

        /// <summary>
        /// Retorna a divisão a qual esta celula pertence
        /// </summary>
	    public virtual Celula GetDivisao()
	    {
	        if(this.TipoCelula == TipoCelula.Divisao)
	            return this;
	        else if(this.TipoCelula == TipoCelula.Secao)
	            return GetPeloCodigo(_codigo.Substring(0, _codigo.IndexOf(".")));
	        else
	            throw new Exception("Não é possível determinar a divisão.");
	    }
	    
	    private static Celula GetPeloCodigo(string codigo)
	    {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery("from Celula c where c.Codigo = :codigo");
	        query.SetString("codigo", codigo);
	        
            return (Celula)query.UniqueResult();
	    }

	    public virtual int CompareTo(Celula other)
	    {
            if (other == null) return 1;
	        return this._descricao.CompareTo(other._descricao);
	    }

        public override string ToString()
        {
            return _descricao;
        }

	    public virtual Celula GetDepartamento()
	    {
            if (this.TipoCelula == TipoCelula.Departamento)
                return this;
            else if (this.TipoCelula == TipoCelula.Divisao)
                return GetPeloCodigo(_codigo.Substring(0, 1) + "0");
            else if (this.TipoCelula == TipoCelula.Secao)
                return GetPeloCodigo(_codigo.Substring(0, _codigo.IndexOf(".")));
            else
                throw new Exception("Não é possível determinar o departamento.");
	    }
	}
}