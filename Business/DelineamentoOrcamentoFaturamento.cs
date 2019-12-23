using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class DelineamentoOrcamentoFaturamento : BusinessObject<DelineamentoOrcamentoFaturamento>	
	{
		#region Private Members
		private DelineamentoOrcamento _delineamentoorcamento; 
		private string _validade; 
		private string _garantia; 
		private DateTime _data; 
		private decimal _valor; 
		private int _numero;
	    private string _observacao;

		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public DelineamentoOrcamentoFaturamento()
		{
			_delineamentoorcamento =  null; 
			_validade = null; 
			_garantia = null; 
			_data = DateTime.MinValue; 
			_valor = 0; 
			_numero = 0; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual string NumeroNL { get; set; }

        public virtual string Observacao
        {
            get { return _observacao; }
            set { _observacao = value; }
        }

		public virtual DelineamentoOrcamento DelineamentoOrcamento
		{
			get { return _delineamentoorcamento; }
			set { _delineamentoorcamento = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Validade
		{
			get { return _validade; }
			set	
			{
				if ( value != null )
					if( value.Length > 200)
						throw new ArgumentOutOfRangeException("Invalid value for Validade", value, value.ToString());
				
				_validade = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Garantia
		{
			get { return _garantia; }
			set	
			{
				if ( value != null )
					if( value.Length > 500)
						throw new ArgumentOutOfRangeException("Invalid value for Garantia", value, value.ToString());
				
				_garantia = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime Data
		{
			get { return _data; }
			set { _data = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal Valor
		{
			get { return _valor; }
			set { _valor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual int Numero
		{
			get { return _numero; }
			set { _numero = value; }
		}
			#endregion 

        #region Advanced Properties
        public virtual string CodigoComAno
        {
            get { return string.Format("{0}/{1}", _numero, _data.Year); }
        }

	 

	    #endregion

        public override void Save()
        {
            if(_valor == 0 && !_delineamentoorcamento.Desconto100Porcento)
                throw new Exception("Não é permitido criar um faturamento com valor 0,00.");
            if (!this.IsPersisted)
                CriarNovo();
            else
                base.Save();
        }

        private void CriarNovo()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            using (TransactionBlock tran = new TransactionBlock())
            {

                IQuery query = session.CreateQuery(
                    @"select MAX(f.Numero) from DelineamentoOrcamentoFaturamento f where dbo.GetYear(f.Data) = :ano ");

                query.SetInt32("ano", this.Data.Year);
                object result = query.UniqueResult();
                if (result == null)
                    this.Numero = 1;
                else
                    this.Numero = Convert.ToInt32(result) + 1;

                base.Save();

                tran.IsValid = true;
            }
        }

	    public static List<DelineamentoOrcamentoFaturamento> Select(string texto, int id_statusOrcamento, DateTime dataInicio, DateTime dataFim, int id_celula)
	    {
	        string filtroCelula = "";
            if(id_celula != 0)
	            filtroCelula = "and (:codigoCelula IS NULL OR dbo.celulapertenceadepartamento(ps.Celula.Codigo, :codigoCelula) = 1)";
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery( string.Format(
            @"select distinct f from DelineamentoOrcamentoFaturamento f 
                    inner join fetch f.DelineamentoOrcamento o 
                    inner join fetch o.PedidoServico ps                    
			where dbo.DateIsInBetween(f.Data, :dataInicio, :dataFim) = 1
			and (dbo.BuscaCodigoPS(ps.CodigoInterno, ps.DataEmissao, :texto) = 1
			or ps.Cliente.Descricao like :texto)
            {0}
			and o.Status.ID = IsNull(:id_status, o.Status.ID)			
			order by ps.CodigoInterno", filtroCelula));

	        string codigoCelula = null;
            if (id_celula != 0)
            {
                codigoCelula = Celula.Get(id_celula).Codigo;
                query.SetParameter("codigoCelula", codigoCelula, NHibernateUtil.String);
            }

	        query.SetTimeout(120);

            query.SetParameter("texto", string.Format("%{0}%", texto));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_statusOrcamento), NHibernateUtil.Int32);
            
            return (List<DelineamentoOrcamentoFaturamento>)query.List<DelineamentoOrcamentoFaturamento>();
	    }
	}
}
