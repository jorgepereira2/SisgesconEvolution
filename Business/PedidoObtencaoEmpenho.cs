using System;
using System.Collections.Generic;
using NHibernate;
using Shared.Common;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoObtencaoEmpenho : BusinessObject<PedidoObtencaoEmpenho>	
	{
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
        public PedidoObtencaoEmpenho()
		{
		  
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual string NumeroEmpenho { get; set; }
        public virtual string Lista { get; set; }
        public virtual string Comentario { get; set; }
        public virtual string NumeroLancamento { get; set; }
        public virtual string CodigoGestao { get; set; }
        public virtual Projeto Projeto { get; set; }
        public virtual PTRES PTRES { get; set; }
        public virtual string PTRESS { get; set; }
        public virtual PedidoObtencao PedidoObtencao { get; set; }
        //public virtual StatusPedidoObtencao Status { get; set; }
      
		#endregion 

//        public static List<PedidoObtencaoPagamento> Select(string texto, DateTime dataInicio, DateTime dataFim, int ano)
//        {
//            ISession session = NHibernateSessionManager.Instance.GetSession();
//            IQuery query = session.CreateQuery(
//            @"from AutorizacaoCompraPagamento p
//                inner join fetch p.AutorizacaoCompra a
//                inner join fetch a.Fornecedor f
//			where (f.RazaoSocial like :texto or a.Numero like :texto)
//			and dbo.DateIsInBetween(p.Data, :dataInicio, :dataFim) = 1
//            and dbo.GetYear(a.DataEmissao) = IsNull(:ano, dbo.GetYear(a.DataEmissao))			
//			order by a.ID");

//            query.SetString("texto", "%" + texto + "%");
//            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
//            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
//            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);

//            return (List<PedidoObtencaoPagamento>)query.List<PedidoObtencaoPagamento>();
//        }
    }
}

