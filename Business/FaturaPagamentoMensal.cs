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
    /// Esta classe foi feita para o parsit
    /// </summary>
    [Serializable]
    public partial class FaturaPagamentoMensal : BusinessObject<FaturaPagamentoMensal>
    {
        #region Private Members

        #endregion

        #region Default ( Empty ) Class Constuctor

        /// <summary>
        /// default constructor
        /// </summary>
        public FaturaPagamentoMensal()
        {

        }

        #endregion // End of Default ( Empty ) Class Constuctor

        #region Public Properties

        public virtual Fornecedor Fornecedor { get; set; }

        public virtual Servidor Servidor { get; set; }

        public virtual DateTime DataEmissao { get; set; }
        public virtual DateTime DataVencimento { get; set; }

        public virtual decimal ValorTotal { get; set; }

        public virtual string NumeroFaturaOS { get; set; }

        public virtual int MesReferencia { get; set; }

        public virtual TipoFaturaPagamento TipoFaturaPagamento { get; set; }

        public virtual string NomeDestinatario
        {
            get
            {
                if (this.TipoFaturaPagamento.FlagDiaria)
                    return this.Servidor.NomeCompleto;
                else
                    return this.Fornecedor.RazaoSocial;
            }
        }
        #endregion


        #region Public Methods

        public static List<FaturaPagamentoMensal> Select(string numeroFatura, DateTime dataEmissaoInicio,
                                                                DateTime dataEmissaoFim, DateTime dataVencimentoInicio,
                                                                DateTime dataVencimentoFim)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                @"from FaturaPagamentoMensal f 
			where f.NumeroFaturaOS like :numeroFatura
            and dbo.DateIsInBetween(f.DataEmissao, :dataEmissaoInicio, :dataEmissaoFim) = 1
            and dbo.DateIsInBetween(f.DataVencimento, :dataVencimentoInicio, :dataVencimentoFim) = 1            
			order by f.DataEmissao");

            query.SetString("numeroFatura", string.Format("%{0}%", numeroFatura));
            query.SetParameter("dataEmissaoInicio", BusinessHelper.IsNull(dataEmissaoInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataEmissaoFim", BusinessHelper.IsNull(dataEmissaoFim), NHibernateUtil.DateTime);
            query.SetParameter("dataVencimentoInicio", BusinessHelper.IsNull(dataVencimentoInicio),
                               NHibernateUtil.DateTime);
            query.SetParameter("dataVencimentoFim", BusinessHelper.IsNull(dataVencimentoFim), NHibernateUtil.DateTime);

            return (List<FaturaPagamentoMensal>) query.List<FaturaPagamentoMensal>();
        }


        #endregion
    }
}
