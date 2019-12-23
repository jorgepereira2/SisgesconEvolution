using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class LicitacaoContrato : BusinessObject<LicitacaoContrato>	
	{
		#region Private Members
		private Licitacao _licitacao; 
		private Fornecedor _fornecedor; 
		

		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public LicitacaoContrato()
		{
			_licitacao =  null; 
			_fornecedor =  null; 
		}

	    public LicitacaoContrato(Licitacao licitacao)
	    {
	        _licitacao = licitacao;
	    }

	    #endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual string NumeroContrato { get; set; }
        public virtual DateTime DataVigencia { get; set; }
        public virtual DateTime DataAssinatura { get; set; }
	
		public virtual Licitacao Licitacao
		{
			get { return _licitacao; }
			set { _licitacao = value; }
		}
			
		public virtual Fornecedor Fornecedor
		{
			get { return _fornecedor; }
			set { _fornecedor = value; }
		}
			
        #endregion

	    #region Advanced Properties

	    public virtual decimal ValorContrato
	    {
	        get
	        {
	            return
	                Licitacao.Itens.Where(x => x.Contrato != null && x.Contrato.ID == this.ID).Sum(
	                    x => x.ValorTotalFinalPregao);
	        }
	    }

	    #endregion
	    
	    #region Select
//        public static LicitacaoContrato GetContratoAberto(int id_material)
//        {
//            ISession session = NHibernateSessionManager.Instance.GetSession();
//            IQuery query = session.CreateQuery(
//            @"from LicitacaoContrato Contrato
//                inner join fetch Contrato.Licitacao l                
//                left join fetch Contrato.Fornecedor f
//			where l.Status = :id_status
//			and Contrato.Material.ID = :id_material
//            
//			and Contrato.Quantidade - Contrato.QuantidadeUtilizada > 0");

//        query.SetInt32("id_status", Convert.ToInt32(StatusLicitacaoEnum.Finalizado));
//            query.SetInt32("id_material", id_material);

//            IList<LicitacaoContrato> list = query.List<LicitacaoContrato>();
//            if(list.Count > 0)
//                return list[0];
//            else
//                return null;
//        }
	    #endregion

	}
}
