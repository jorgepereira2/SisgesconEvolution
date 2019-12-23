using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;
using Iesi.Collections.Generic;
using NHibernate.Criterion;
using Shared.Common;

namespace Marinha.Business
{
	[Serializable]
	public partial class Processo : BusinessObject<Processo>	
	{
		#region Private Members		

		private string _link;
	    private Processo _processopai;
 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Processo()
		{
			ID = 0; 
			_link = null; 
			
			_processopai =  null;
            _processos = new HashedSet<Processo>();            
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual bool FlagAtivo { get; set; }
		public virtual string Nome { get; set; }
		
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Link
		{
			get { return _link; }
			set	
			{
				_link = value;
			}
		}

	   
	    public virtual int Ordem { get; set; }

	   	public virtual Processo ProcessoPai
		{
			get { return _processopai; }
			set { _processopai = value; }
		}


	    private string _textoCaminho;
	    public virtual string TextoCaminho
	    {
	        get
	        {
	            if(string.IsNullOrEmpty(_textoCaminho))
	            {
	                _textoCaminho = GetCaminho(this, "");
	            }
	            return _textoCaminho.Remove(_textoCaminho.Length - 2);
	        }
	    }

        private string GetCaminho(Processo processo, string caminhoAtual)
        {
            caminhoAtual = processo.Nome + " \\ " + caminhoAtual;
            if (processo.ProcessoPai != null)
               return GetCaminho(processo.ProcessoPai, caminhoAtual);
            return caminhoAtual;
        }
        
		#endregion 
			
        #region Collections
        private Iesi.Collections.Generic.ISet<Processo> _processos;
		private Iesi.Collections.Generic.ISet<Servidor> _servidores;

        public virtual Iesi.Collections.Generic.ISet<Processo> Processos
        {
            get { return _processos; }
            set { _processos = value; }
        }

		public virtual Iesi.Collections.Generic.ISet<Servidor> Servidores
        {
            get { return _servidores; }
			set { _servidores = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Retorna uma lista dos processos do inicio da árvore.
        /// Cada processo possui a sua própria coleção com os processos filhos
        /// </summary>        
        public static ProcessoCollection Select()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            ICriteria crit = session.CreateCriteria(typeof(Processo))
                                .Add(Expression.IsNull("ProcessoPai"))     
                                .Add(Expression.Eq("FlagAtivo", true))
                                .AddOrder(Order.Asc("Ordem"));

            IList<Processo> list = crit.List<Processo>();

            ProcessoCollection processos = new ProcessoCollection();
            BusinessHelper.CloneIListToList<Processo>(list, processos);

            return processos;
        }

        /// <summary>
        /// Seleciona os processos que o usuário tem acesso
        /// Esta coleção não está na forma hierarquica
        /// </summary>
        public static ProcessoCollection SelectMenu(int id_servidor)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                @"select distinct p from Processo as p 
                inner join p.Servidores as servidores
            WHERE   servidores.ID = :id_servidor
            and p.FlagAtivo = 1
            ORDER BY p.ProcessoPai.ID, p.Ordem, p.Nome");

			query = query.SetInt32("id_servidor", id_servidor);
			
            
            ProcessoCollection processos = new ProcessoCollection();
            BusinessHelper.CloneIListToList<Processo>(query.List<Processo>(), processos);

            Servidor servidor = Servidor.Get(id_servidor);
            foreach (PerfilAcesso perfil in servidor.Perfis)
            {
                foreach (Processo processo in perfil.Processos)
                {
                    if(!processos.Contains(processo))
                        processos.Add(processo);
                }
            }

            return processos;      
        }

        /// <summary>
        /// Retorna uma coleção de processos baseado nos ids passados
        /// </summary>
        /// <param name="id_processos">List<string> contendo os IDs dos processos</param>        
        public static ICustomList<Processo> SelectByList(List<string> id_processos)
        {
            if(id_processos.Count == 0)
                return new CustomList<Processo>();
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery("from Processo p where p.ID IN (:Lista)");
            
            query = query.SetParameterList("Lista",  id_processos);

            IList<Processo> list = query.List<Processo>();
			CustomList<Processo> processos = new CustomList<Processo>();
			foreach (Processo p in list)
				processos.Add(p);
            return processos;
        }

        public static List<Processo> SelectPaginas()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                @"select distinct p from Processo as p                 
            WHERE   p.FlagAtivo = 1
            and p.Link is not null
            and p.Link != ''
            ORDER BY p.ProcessoPai.ID, p.Ordem, p.Nome");

            return (List<Processo>)query.List<Processo>();
        }

        /// <summary>
        /// Retorna uma coleção de processos baseado nos ids passados
        /// </summary>
        /// <param name="id_processos">List<string> contendo os IDs dos processos</param>        
        public static ICustomList<Processo> SelectByString(string id_processos)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();

            IQuery query = session.CreateQuery("@from Processo p where p.ID IN (:id_processos) order by p.Ordem asc");

            //query = query.SetParameterList("Lista", Util.LeLista(id_processos));
            query = query.SetString("id_processos", id_processos);

            IList<Processo> list = query.List<Processo>();
            CustomList<Processo> processos = new CustomList<Processo>();
            foreach (Processo p in list)
                processos.Add(p);
            return processos;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>		
        public virtual string Root(Processo processo)
        {
            string root = "";

            if (processo.ProcessoPai != null)
            {
                root = root + Root(processo.ProcessoPai);
            }

            return root + " > " + processo.Nome;
        }
    }
}