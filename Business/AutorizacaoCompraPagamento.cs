using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class AutorizacaoCompraPagamento : BusinessObject<AutorizacaoCompraPagamento>	
	{
		#region Private Members
		private AutorizacaoCompra _autorizacaocompra; 
		private string _numeronotafiscal; 
		private decimal _valor; 
		private DateTime _data; 
		private Servidor _servidor;
	    private string _ordemBancaria;
	    private decimal _valorImposto;
        private decimal _valorDesconto;
        private string _observacao; 
	    #endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public AutorizacaoCompraPagamento()
		{
		    _ordemBancaria = null;
			_autorizacaocompra =  null; 
			_numeronotafiscal = null; 
			_valor = 0; 
			_data = DateTime.MinValue; 
			_servidor =  null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual string OrdemBancaria
        {
            get { return _ordemBancaria; }
            set { _ordemBancaria = value; }
        }

        public virtual string Observacao
        {
            get { return _observacao; }
            set { _observacao = value; }
        }
        
		public virtual AutorizacaoCompra AutorizacaoCompra
		{
			get { return _autorizacaocompra; }
			set { _autorizacaocompra = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string NumeroNotaFiscal
		{
			get { return _numeronotafiscal; }
			set	
			{
				if ( value != null )
					if( value.Length > 20)
						throw new ArgumentOutOfRangeException("Invalid value for NumeroNotaFiscal", value, value.ToString());
				
				_numeronotafiscal = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal Valor
		{
			get { return _valor; }
			set { _valor = value; }
		}

        public virtual decimal ValorImposto
        {
            get { return _valorImposto; }
            set { _valorImposto = value; }
        }

        public virtual decimal ValorDesconto
        {
            get { return _valorDesconto; }
            set { _valorDesconto = value; }
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
		public virtual Servidor Servidor
		{
			get { return _servidor; }
			set { _servidor = value; }
		}


		#endregion 

	    public virtual decimal ValorPago
	    {
            get { return _valor + _valorImposto - _valorDesconto; }
	    }

        public virtual decimal ValorTotal
        {
            get { return _valor - (_valorImposto + _valorDesconto); }
        }

	    public virtual string CNPJ
	    {
            get { return _autorizacaocompra.Fornecedor.CNPJ; }
	    }

        public virtual string RazaoSocial
        {
            get { return _autorizacaocompra.Fornecedor.RazaoSocial; }
        }

        public static List<AutorizacaoCompraPagamento> Select(string texto, DateTime dataInicio, DateTime dataFim, int ano)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from AutorizacaoCompraPagamento p
                inner join fetch p.AutorizacaoCompra a
                inner join fetch a.Fornecedor f
			where (f.RazaoSocial like :texto or a.Numero like :texto)
			and dbo.DateIsInBetween(p.Data, :dataInicio, :dataFim) = 1
            and dbo.GetYear(a.DataEmissao) = IsNull(:ano, dbo.GetYear(a.DataEmissao))			
			order by a.ID");

            query.SetString("texto", "%" + texto + "%");
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);

            return (List<AutorizacaoCompraPagamento>)query.List<AutorizacaoCompraPagamento>();
        }
	}
}
