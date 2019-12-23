using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class MedidaEtiqueta : BusinessObject<MedidaEtiqueta>	
	{
		#region Private Members
		private decimal _alturapapel; 
		private int _colunas; 
		private int _linhas; 
		private decimal _largurapapel; 
		private decimal _margemdireita; 
		private decimal _margemesquerda; 
		private decimal _margemsuperior; 
		private decimal _margeminferior; 
		private decimal _separacaohorizontal; 
		private decimal _separacaovertical;
        private decimal _alturaConteudo; 
		private string _nome; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public MedidaEtiqueta()
		{
			_alturapapel = 0; 
			_colunas = 0; 
			_linhas = 0; 
			_largurapapel = 0; 
			_margemdireita = 0; 
			_margemesquerda = 0; 
			_margemsuperior = 0; 
			_margeminferior = 0; 
			_separacaohorizontal = 0; 
			_separacaovertical = 0; 
			_nome = null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual decimal AlturaConteudo
        {
            get { return _alturaConteudo; }
            set { _alturaConteudo = value; }
        }
	
		public virtual decimal AlturaPapel
		{
			get { return _alturapapel; }
			set { _alturapapel = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual int Colunas
		{
			get { return _colunas; }
			set { _colunas = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual int Linhas
		{
			get { return _linhas; }
			set { _linhas = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal LarguraPapel
		{
			get { return _largurapapel; }
			set { _largurapapel = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal MargemDireita
		{
			get { return _margemdireita; }
			set { _margemdireita = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal MargemEsquerda
		{
			get { return _margemesquerda; }
			set { _margemesquerda = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal MargemSuperior
		{
			get { return _margemsuperior; }
			set { _margemsuperior = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal MargemInferior
		{
			get { return _margeminferior; }
			set { _margeminferior = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal SeparacaoHorizontal
		{
			get { return _separacaohorizontal; }
			set { _separacaohorizontal = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal SeparacaoVertical
		{
			get { return _separacaovertical; }
			set { _separacaovertical = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Nome
		{
			get { return _nome; }
			set	{_nome = value;}
		}
			#endregion 
		
        #region Advanced Properties
        public virtual decimal LarguraEtiqueta
        {
            get
            {
                return (this.LarguraPapel - ((this.Colunas - 1) * this.SeparacaoHorizontal) - this.MargemDireita
                    - this.MargemEsquerda) / this.Colunas;
            }
        }

        public virtual decimal AlturaEtiqueta
        {
            get
            {
                return (this.AlturaPapel - ((this.Linhas - 1) * this.SeparacaoVertical) - this.MargemSuperior
                    - this.MargemInferior) / this.Linhas;
            }
        }
        public virtual int EtiquetasPorPagina
        {
            get { return _linhas * _colunas; }
        }
        #endregion

        #region Public Methods

        public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select m.ID, m.Nome 
			from MedidaEtiqueta m  			
			order by m.Nome");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<MedidaEtiqueta> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from MedidaEtiqueta m  			
			order by m.Nome");
		
			return (List<MedidaEtiqueta>)query.List<MedidaEtiqueta>();
		}
		
		#endregion
		
		
		
	}
}
