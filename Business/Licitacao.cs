using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Shared.Common;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class Licitacao : BusinessObject<Licitacao>	
	{
		#region Private Members
		private DateTime _dataemissao; 
		private string _objetivo; 
		private string _observacao; 
		private string _numeropregao; 
		private DateTime? _datapregao;
	    private StatusLicitacaoEnum _status;
        private MotivoCancelamento _motivoCancelamento;
	    private Servidor _servidorCancelamento;
	    private bool _flagDesativado;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Licitacao()
		{
			_dataemissao = DateTime.MinValue; 
			_objetivo = null; 
			_observacao = null; 
			_numeropregao = null; 
			_datapregao = null;
		    _itens = new CustomList<LicitacaoItem>();
		    _status = StatusLicitacaoEnum.CadastroInicial;
            _historico = new CustomList<HistoricoLicitacao>();
            Documentos = new CustomList<LicitacaoDocumento>();
            Contratos = new CustomList<LicitacaoContrato>();
            POs = new List<PedidoObtencao>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual List<PedidoObtencao> POs { get; set; }

	    public virtual TipoLicitacao TipoLicitacao { get; set; }
        public virtual ModalidadePregao ModalidadePregao { get; set; }
        public virtual Servidor ServidorFiscalContrato { get; set; }
        
        public virtual SistemaLicitatorio SistemaLicitatorio { get; set; }
        public virtual string NumeroCI { get; set; }
        public virtual string NUP { get; set; }
        public virtual ProcessoLicitatorio ProcessoLicitatorio { get; set; }

        public virtual MotivoCancelamento MotivoCancelamento
        {
            get { return _motivoCancelamento; }
            set { _motivoCancelamento = value; }
        }

        public virtual Servidor ServidorCancelamento
        {
            get { return _servidorCancelamento; }
            set { _servidorCancelamento = value; }
        }

        public virtual StatusLicitacaoEnum Status
        {
            get { return _status; }
            set { _status = value; }
        }
        
		public virtual DateTime DataEmissao
		{
			get { return _dataemissao; }
			set { _dataemissao = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Objetivo
		{
			get { return _objetivo; }
			set	
			{
				if ( value != null )
					if( value.Length > 500)
						throw new ArgumentOutOfRangeException("Invalid value for Objetivo", value, value.ToString());
				
				_objetivo = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Observacao
		{
			get { return _observacao; }
			set	
			{
				if ( value != null )
					if( value.Length > 1000)
						throw new ArgumentOutOfRangeException("Invalid value for Observacao", value, value.ToString());
				
				_observacao = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string NumeroPregao
		{
			get { return _numeropregao; }
			set	
			{
				if ( value != null )
					if( value.Length > 20)
						throw new ArgumentOutOfRangeException("Invalid value for NumeroPregao", value, value.ToString());
				
				_numeropregao = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime? DataPregao
		{
			get { return _datapregao; }
			set { _datapregao = value; }
		}

        public virtual bool FlagDesativado
        {
            get { return _flagDesativado; }
            set { _flagDesativado = value; }
        }
		#endregion 
		
		#region Collections

	    private ICustomList<LicitacaoItem> _itens;
	    public virtual ICustomList<LicitacaoContrato> Contratos { get; set; }
        private ICustomList<HistoricoLicitacao> _historico;
	    public virtual ICustomList<LicitacaoDocumento> Documentos { get; set; }

	    public virtual ICustomList<LicitacaoItem> Itens
	    {
	        get { return _itens; }
	        set { _itens = value; }
	    }

        public virtual ICustomList<HistoricoLicitacao> Historico
        {
            get { return _historico; }
            set { _historico = value; }
        }
		#endregion

        #region Advanced Properties
	    public virtual bool PodeSerAlterada
	    {
	        get{ return _status != StatusLicitacaoEnum.ContratoAssinado;}
	    }

        public virtual decimal ValorTotalEstimado
        {
            get
            {
                decimal valor = 0;
                foreach (LicitacaoItem item in _itens)
                {
                    valor += item.ValorMedioTotal;
                }
                return valor;
            }
        }



        public virtual decimal ValorTotalFinal
        {
            get
            {
                decimal valor = 0;
                foreach (LicitacaoItem item in _itens)
                {
                    valor += item.ValorTotalFinalPregao;
                }
                return valor;
            }
        }

        public virtual HistoricoLicitacao UltimoHistorico
        {
            get
            {
                if (Historico.Count > 0)
                    return Historico[Historico.Count - 1];
                else
                    return null;
            }
        }
	    #endregion

        #region Public Methods

        public static List<Licitacao> Select(DateTime dataInicio, DateTime dataFim, StatusLicitacaoEnum? status)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from Licitacao l  			
			where dbo.DateIsInBetween(l.DataEmissao, :dataInicio, :dataFim) = 1
			and l.Status = IsNull(:status, l.Status)
			order by l.DataEmissao DESC");

		    query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
		    query.SetParameter("status", BusinessHelper.IsNullOrZero(status.HasValue ? Convert.ToInt32(status) : Int32.MinValue), NHibernateUtil.Int32);
			return (List<Licitacao>)query.List<Licitacao>();
		}

        public static List<Licitacao> Select(string numero, int id_status, DateTime dataInicio, DateTime dataFim, int id_fornecedor, int id_material, int ano)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct l from Licitacao l 
                inner join l.Itens item 
                inner join item.Material m
                left join item.Fornecedor f
			where dbo.DateIsInBetween(l.DataEmissao, :dataInicio, :dataFim) = 1
            and dbo.GetYear(l.DataEmissao) = IsNull(:ano, dbo.GetYear(l.DataEmissao))
			and l.Status = IsNull(:id_status, l.Status)
			and m.ID = IsNull(:id_material, m.ID)
			and IsNull(f.ID, -1) = IsNull(:id_fornecedor, IsNull(f.ID, -1))
			and IsNull(l.NumeroPregao, '') like :numero
			order by l.DataEmissao");

            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("id_material", BusinessHelper.IsNullOrZero(id_material), NHibernateUtil.Int32);
            query.SetParameter("id_fornecedor", BusinessHelper.IsNullOrZero(id_fornecedor), NHibernateUtil.Int32);
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            query.SetString("numero", string.Format("%{0}%", numero));
            
            var result = (List<Licitacao>)query.List<Licitacao>();

            query = session.CreateQuery(
            @"select distinct po from PedidoObtencao po 
                inner join fetch po.Licitacao l 
                inner join l.Itens item 
                inner join item.Material m
                left join item.Fornecedor f
			where dbo.DateIsInBetween(l.DataEmissao, :dataInicio, :dataFim) = 1
            and dbo.GetYear(l.DataEmissao) = IsNull(:ano, dbo.GetYear(l.DataEmissao))
			and l.Status = IsNull(:id_status, l.Status)
			and m.ID = IsNull(:id_material, m.ID)
			and IsNull(f.ID, -1) = IsNull(:id_fornecedor, IsNull(f.ID, -1))
			and IsNull(l.NumeroPregao, '') like :numero
			order by l.DataEmissao");

            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("id_material", BusinessHelper.IsNullOrZero(id_material), NHibernateUtil.Int32);
            query.SetParameter("id_fornecedor", BusinessHelper.IsNullOrZero(id_fornecedor), NHibernateUtil.Int32);
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            query.SetString("numero", string.Format("%{0}%", numero));

            var pos = (List<PedidoObtencao>)query.List<PedidoObtencao>();
            
            foreach (PedidoObtencao po in pos)
            {
                var licitacao = result.FirstOrDefault(x => x.ID == po.Licitacao.ID);
                if(licitacao != null)
                    licitacao.POs.Add(po);
            }

            return result;
        }


        public static List<Licitacao> Select(int id_servidor, int id_status)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct l from Licitacao l, StatusLicitacao s 
                inner join s.Responsaveis resp
			where l.Status = s.ID
            and resp.ID = :id_servidor
			and s.ID = IsNull(:id_status, s.ID)
			order by l.ID");

            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetInt32("id_servidor", id_servidor);

            return (List<Licitacao>)query.List<Licitacao>();
        }

        public static Dictionary<string, string> ListParaPO()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from Licitacao l  			
			where l.Status = IsNull(:status, l.Status)
			order by l.DataEmissao DESC");
            //and dbo.DateIsInBetween(l.DataEmissao, :dataInicio, :dataFim) = 1
            
            //query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            //query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("status", Convert.ToInt32(StatusLicitacaoEnum.Finalizado));
            Dictionary<string, string> list = new Dictionary<string, string>();
            List<Licitacao> result = query.List<Licitacao>().ToList();
            foreach (Licitacao licitacao in result)
            {
                list.Add(licitacao.ID.ToString(), licitacao.NumeroCI);
            }
            return list;
        }
		#endregion

        #region Workflow

        private void SaveHistorico(int id_servidor, StatusLicitacaoEnum proximoStatus)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoLicitacao historico = GetHistorico(id_servidor, proximoStatus);

                historico.Save();

                this.Status = proximoStatus;
                base.Save();

                this._historico.Add(historico);

                tran.IsValid = true;
            }
        }

        private HistoricoLicitacao GetHistorico(int id_servidor, StatusLicitacaoEnum novoStatus)
        {
            HistoricoLicitacao historico = new HistoricoLicitacao();
            historico.Data = DateTime.Now;
            historico.Licitacao = this;
            historico.Descricao = string.Format("{0} -> {1}",  Util.GetDescription(this.Status), Util.GetDescription(novoStatus));
            historico.Servidor = Servidor.Get(id_servidor);
            return historico;
        }


        public virtual void ProximoStatus(int id_servidor, string justificativa)
        {
            if(this.Status == StatusLicitacaoEnum.ContratoAssinado)
                throw new Exception("esta licitação ja foi finalizada");

            StatusLicitacaoEnum proximoStatus = StatusLicitacao.GetNext(_status);

            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoLicitacao historico = GetHistorico(id_servidor, proximoStatus);

                historico.Justificativa = justificativa;
                historico.Save();

                this.Status = proximoStatus;
                base.Save();

                this._historico.Add(historico);

                tran.IsValid = true;
            }
        }

        /// <summary>
        /// Retorna para o status anterior
        /// </summary>
        public virtual void Recusar(int id_servidor, string justificativa)
        {
            StatusLicitacaoEnum proximoStatus = StatusLicitacao.GetAnterior(_status);

            HistoricoLicitacao historico = GetHistorico(id_servidor, proximoStatus);
            historico.Justificativa = justificativa;

            using (TransactionBlock tran = new TransactionBlock())
            {
                historico.FlagReprovado = true;
                historico.Save();

                this.Status = proximoStatus;

                base.Save();

                this._historico.Add(historico);
                tran.IsValid = true;
            }
        }

        #endregion

        public virtual void Finalizar()
	    {
            if (_status == StatusLicitacaoEnum.Finalizado)
                throw new Exception("Esta licitação já foi finalizada.");
            using (TransactionBlock tran = new TransactionBlock())
            {
                foreach (LicitacaoItem item in _itens)
                {
                    item.Save();
                }

                this._status = StatusLicitacaoEnum.Finalizado;
                base.Save();

                tran.IsValid = true;
            }
	    }

        public virtual void SalvarItens()
        {
            //if (_status == StatusLicitacaoEnum.ContratoAssinado)
            //    throw new Exception("Esta licitação já foi finalizada.");
            using (TransactionBlock tran = new TransactionBlock())
            {
                foreach (LicitacaoItem item in _itens)
                {
                    item.Save();
                }

                base.Save();
                tran.IsValid = true;
            }
        }

	    public virtual void Cancelar(int id_Servidor, int id_MotivoCancelamento)
	    {
	        this.Status = StatusLicitacaoEnum.Cancelado;
	        this._motivoCancelamento = MotivoCancelamento.Get(id_MotivoCancelamento);
	        this._servidorCancelamento = Servidor.Get(id_Servidor);
            this.Save();
	    }

        public virtual void Desativar()
        {   
            this._flagDesativado = true;
            this.Save();
        }

        public virtual Dictionary<int, string> ListContratos ()
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            foreach (LicitacaoContrato contrato in this.Contratos)
            {
                list.Add(contrato.ID, String.Format("{0} - {1}", contrato.NumeroContrato, contrato.Fornecedor.RazaoSocial));
            }
            return list;
        }
	}
}
