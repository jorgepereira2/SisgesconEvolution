using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using Shared.Common;
using Shared.DataAccessHelper;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
    /// <summary>
    /// Esta classe na verdade representa Movimentacoes Financeiras, o nome adequado seria MovimentoFinanceiro, pois ela contem entradas e saidas
    /// </summary>
	[Serializable]
	public partial class EntradaValores : BusinessObject<EntradaValores>
	{
		#region Private Members
		private Projeto _projeto; 
        private NaturezaDespesa _naturezaDespesa;
        private FonteRecurso _fonteRecurso;
        private PTRES _ptres; 
        private Servidor _servidor; 
		private string _numeroDocumento; 
		private decimal _valor; 
		private DateTime _data;
        private TipoMovimentoFinanceiro _tipoMovimentoFinanciero;
        private AutorizacaoCompra _autorizacaoCompra;
        private TipoOperacaoFinanceira _tipoOperacaoFinanceira;

        #endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public EntradaValores()
		{
		    _ptres = null;
		    _fonteRecurso = null;
			_naturezaDespesa =  null; 
			_projeto = null; 
			_servidor = null;
		    _data = DateTime.Today;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual Projeto Projeto
        {
            get { return _projeto; }
            set { _projeto = value; }
        }
        	
		public virtual NaturezaDespesa NaturezaDespesa
		{
			get { return _naturezaDespesa; }
			set { _naturezaDespesa = value; }
		}

        public virtual FonteRecurso FonteRecurso
        {
            get { return _fonteRecurso; }
            set { _fonteRecurso = value; }
        }

        public virtual PTRES PTRES
        {
            get { return _ptres; }
            set { _ptres = value; }
        }

        public virtual Servidor Servidor
        {
            get { return _servidor; }
            set { _servidor = value; }
        }
		
		public virtual DateTime Data
		{
			get { return _data; }
			set { _data = value; }
		}

        public virtual decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public virtual string NumeroDocumento
        {
            get { return _numeroDocumento; }
            set { _numeroDocumento = value; }
        }

        public virtual TipoMovimentoFinanceiro TipoMovimentoFinanceiro
        {
            get { return _tipoMovimentoFinanciero; }
            set { _tipoMovimentoFinanciero = value; }
        }

        public virtual AutorizacaoCompra AutorizacaoCompra
        {
            get { return _autorizacaoCompra; }
            set { _autorizacaoCompra = value; }
        }

        public virtual TipoOperacaoFinanceira TipoOperacaoFinanceira
        {
            get {
                return _tipoOperacaoFinanceira;
            }
            set {
                _tipoOperacaoFinanceira = value;
            }
        }

        #endregion 
		
		
		#region Public Methods
		
		public static List<EntradaValores> SelectEntrada(string numeroDocumento)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from EntradaValores e inner join fetch e.NaturezaDespesa n inner join fetch e.Projeto p
			where e.NumeroDocumento like :numeroDocumento			
            and e.TipoMovimentoFinanceiro = 1
			order by e.Data");

		    query.SetString("numeroDocumento", "%" + numeroDocumento + "%");
			return (List<EntradaValores>)query.List<EntradaValores>();
		}

        public static List<EntradaValores> SelectExtrato(int id_projeto, int id_naturezaDespesa, int id_fonteRecurso, int id_ptres, DateTime dataInicio, DateTime dataFim)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from EntradaValores e 
			where e.NaturezaDespesa.ID = :id_naturezaDespesa
            and e.Projeto.ID = :id_projeto
            and e.PTRES.ID = :id_ptres
            and e.FonteRecurso.ID = :id_fonteRecurso
            and dbo.DateIsInBetween(e.Data, :dataInicio, :dataFim) = 1
            and e.TipoMovimentoFinanceiro != 2
			order by e.Data");

            query.SetInt32("id_naturezaDespesa", id_naturezaDespesa);
            query.SetInt32("id_projeto", id_projeto);
            query.SetInt32("id_ptres", id_ptres);
            query.SetInt32("id_fonteRecurso", id_fonteRecurso);
            query.SetDateTime("dataInicio", dataInicio);
            query.SetDateTime("dataFim", dataFim);

            return (List<EntradaValores>)query.List<EntradaValores>();
        }

        public static DataSet SelectSaldo(string lista)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[2];
            param[1] = lista;
            
            return helper.ExecuteDataSet("EntradaValores_SelectSaldo", param);
        }

        public static DataSet SelectAgrupado(int id_projeto, int id_naturezaDespesa, int id_fonteRecurso, int id_ptres, DateTime dataInicio, DateTime dataFim)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[7];
            param[1] = NullHelper.IsNull(dataInicio);
            param[2] = NullHelper.IsNull(dataFim);
            param[3] = NullHelper.IsZero(id_projeto);
            param[4] = NullHelper.IsZero(id_naturezaDespesa);
            param[5] = NullHelper.IsZero(id_fonteRecurso);
            param[6] = NullHelper.IsZero(id_ptres);


            return helper.ExecuteDataSet("EntradaValores_SelectAgrupado", param);
        }
		
		#endregion

        public static EntradaValores GetByAC(int id_ac)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from EntradaValores e 
			where e.AutorizacaoCompra.ID = :id_ac");

            query.SetInt32("id_ac", id_ac);
            return query.UniqueResult<EntradaValores>();
        }
	}
}
