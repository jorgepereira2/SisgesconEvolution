using System;
using System.Collections.Generic;
using NHibernate;
using Shared.Common;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class NotaEntregaMaterialOMF : BusinessObject<NotaEntregaMaterialOMF>	
	{
		#region Private Members
		private DateTime _dataentrega; 
		private string _numeronota; 
		private string _numeroempenho; 
		private Fornecedor _fornecedor; 
		private string _descriminacaomaterial; 
		private TipoEmprego _tipoemprego; 
		private Servidor _recebedor; 		
	    private StatusOMF _status;
        private string _mensagemSolicitacaoPericia;
        private string _notaLancamento;
	    private MotivoCancelamento _motivoCancelamento;
         
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public NotaEntregaMaterialOMF()
		{
			_dataentrega = DateTime.MinValue; 
			_numeronota = null; 
			_numeroempenho = null; 
			_fornecedor =  null; 
			_descriminacaomaterial = null; 
			_recebedor =  null; 
		    _itens = new CustomList<NotaEntregaMaterialOMFItem>();
		    _historico = new CustomList<HistoricoOMF>();
            _responsaveisPericia = new CustomList<NotaEntregaMaterialOMFResponsavelPericia>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
        public virtual MotivoCancelamento MotivoCancelamento
        {
            get { return _motivoCancelamento; }
            set { _motivoCancelamento = value; }
        }

        public virtual StatusOMF Status
        {
            get { return _status; }
            set { _status = value; }
        }
	
		public virtual DateTime DataEntrega
		{
			get { return _dataentrega; }
			set { _dataentrega = value; }
		}
			
        public virtual string MensagemSolicitacaoPericia
        {
            get { return _mensagemSolicitacaoPericia; }
            set { _mensagemSolicitacaoPericia = value; }
        }
        public virtual string NotaLancamento
        {
            get { return _notaLancamento; }
            set { _notaLancamento = value; }
        }

		public virtual string NumeroNota
		{
			get { return _numeronota; }
			set	
			{
				if ( value != null )
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for NumeroNota", value, value.ToString());
				
				_numeronota = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string NumeroEmpenho
		{
			get { return _numeroempenho; }
			set	
			{
				if ( value != null )
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for NumeroEmpenho", value, value.ToString());
				
				_numeroempenho = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Fornecedor Fornecedor
		{
			get { return _fornecedor; }
			set { _fornecedor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string DescriminacaoMaterial
		{
			get { return _descriminacaomaterial; }
			set	
			{
				if ( value != null )
					if( value.Length > 500)
						throw new ArgumentOutOfRangeException("Invalid value for DescriminacaoMaterial", value, value.ToString());
				
				_descriminacaomaterial = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual TipoEmprego TipoEmprego
		{
			get { return _tipoemprego; }
			set { _tipoemprego = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Servidor Recebedor
		{
			get { return _recebedor; }
			set { _recebedor = value; }
		}
			#endregion 
		
        #region Collections

        private ICustomList<NotaEntregaMaterialOMFItem> _itens;
        private ICustomList<HistoricoOMF> _historico;
        private ICustomList<NotaEntregaMaterialOMFResponsavelPericia> _responsaveisPericia;

        public virtual ICustomList<NotaEntregaMaterialOMFResponsavelPericia> ResponsaveisPericia
        {
            get { return _responsaveisPericia; }
            set { _responsaveisPericia = value; }
        }

        public virtual ICustomList<HistoricoOMF> Historico
        {
            get { return _historico; }
            set { _historico = value; }
        }
        public virtual ICustomList<NotaEntregaMaterialOMFItem> Itens
        {
            get { return _itens; }
            set { _itens = value; }
        }
        #endregion

        #region Advanced Properties
         public virtual  int Numero
         {
	        get{return ID;}
	    }

        public virtual decimal ValorTotal
	    {
	        get
	        {
	            decimal total = 0;
	            foreach (NotaEntregaMaterialOMFItem item in _itens)
	            {
	                total += item.ValorTotal;
	            }
	            return total;
	        }
	    }

        public virtual HistoricoOMF UltimoHistorico
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
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select n.ID, n.Descricao 
			from NotaEntregaMaterialOMF n  
			where n.FlagAtivo = 1
			order by n.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<NotaEntregaMaterialOMF> Select(string texto, int id_recebedor, int id_tipoEmprego, int id_status, 
            DateTime dataInicio, DateTime dataFim)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"select from n from NotaEntregaMaterialOMF n  			
                    inner join fetch n.Status s
                    inner join fetch n.Recebedor r
                    inner join fetch n.Fornecedor f
             where (n.NumeroNota like :texto or f.RazaoSocial like :texto)
             and    s.ID = IsNull(:id_status, s.ID)
             and    dbo.DateIsInBetween(n.DataEntrega, :dataInicio, :dataFim) = 1  			
             and    r.ID = IsNull(:id_recebedor, r.ID)
             and    n.TipoEmprego.ID = IsNull(:id_tipoEmprego, n.TipoEmprego.ID)                
			order by n.NumeroNota");

		    query.SetString("texto", string.Format("%{0}%", texto));
		    query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("id_recebedor", BusinessHelper.IsNullOrZero(id_recebedor), NHibernateUtil.Int32);
            query.SetParameter("id_tipoEmprego", BusinessHelper.IsNullOrZero(id_tipoEmprego), NHibernateUtil.Int32);
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);

		
			return (List<NotaEntregaMaterialOMF>)query.List<NotaEntregaMaterialOMF>();
        }
		
        public static List<NotaEntregaMaterialOMF> Select(int id_status, int id_tipoEmprego, int id_fornecedor, int id_recebedor, string numeroNota, string numeroEmpenho,
            DateTime dataInicio, DateTime dataFim)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select from n from NotaEntregaMaterialOMF n  			
                    inner join fetch n.Status s
                    inner join fetch n.Recebedor r
                    inner join fetch n.Fornecedor f
                    inner join fetch n.TipoEmprego t
             where n.NumeroNota like :numeroNota 
             and  n.NumeroEmpenho like :numeroEmpenho
             and    s.ID = IsNull(:id_status, s.ID)
             and    dbo.DateIsInBetween(n.DataEntrega, :dataInicio, :dataFim) = 1  			
             and    r.ID = IsNull(:id_recebedor, r.ID)
             and    f.ID = IsNull(:id_fornecedor, f.ID)                
             and    t.ID = IsNull(:id_tipoEmprego, t.ID)                
			order by n.NumeroNota");

            query.SetString("numeroNota", string.Format("%{0}%", numeroNota));
            query.SetString("numeroEmpenho", string.Format("%{0}%", numeroEmpenho));
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("id_recebedor", BusinessHelper.IsNullOrZero(id_recebedor), NHibernateUtil.Int32);
            query.SetParameter("id_tipoEmprego", BusinessHelper.IsNullOrZero(id_tipoEmprego), NHibernateUtil.Int32);
            query.SetParameter("id_fornecedor", BusinessHelper.IsNullOrZero(id_fornecedor), NHibernateUtil.Int32);
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);


            return (List<NotaEntregaMaterialOMF>)query.List<NotaEntregaMaterialOMF>();
        }


        public static List<NotaEntregaMaterialOMF> Select(int id_servidor, int id_status)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct a from NotaEntregaMaterialOMF a 
                inner join fetch a.Status s 
                inner join s.Responsaveis resp
			where resp.ID = :id_servidor
			and s.ID = IsNull(:id_status, s.ID)
			order by a.ID");

            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetInt32("id_servidor", id_servidor);

            return (List<NotaEntregaMaterialOMF>)query.List<NotaEntregaMaterialOMF>();
        }
		#endregion
		
        #region Salvar
        public override void Save()
        {   
            if (!this.IsPersisted)
                CriarNovo();
            else
                base.Save();
        }

        private void CriarNovo()
        {
            this.Status = StatusOMF.Get(StatusOMFEnum.NaoEnviado);
            base.Save();

        }
        #endregion

        #region Workflow
        // PASSO 1
        public virtual void Enviar(int id_servidor)
	    {
            if (!this.IsPersisted)
                throw new Exception("Salve os dados do pedido antes de enviá-lo.");


            StatusOMFEnum proximoStatus = StatusOMFEnum.EmissaoTAV;

            SaveHistoricoEOMF(id_servidor, proximoStatus);
	    }

        // PASSO 2
	    public virtual void EmitirTAV(int id_servidor)
        {
            if (this.Status.StatusOMFEnum != StatusOMFEnum.EmissaoTAV)
                throw new Exception("Status inválido");


            StatusOMFEnum proximoStatus = StatusOMFEnum.SolicitarPericiaMaterial;

            SaveHistoricoEOMF(id_servidor, proximoStatus);
        }

        // PASSO 3
        public virtual void SolicitarPericia(int id_servidor, string mensagemSolicitacaoPericia)
        {
            if (this.Status.StatusOMFEnum != StatusOMFEnum.SolicitarPericiaMaterial)
                throw new Exception("Status inválido");


            StatusOMFEnum proximoStatus = StatusOMFEnum.AguardandoResultadoPericia;
            this.MensagemSolicitacaoPericia = mensagemSolicitacaoPericia;

            SaveHistoricoEOMF(id_servidor, proximoStatus);
        }

        // PASSO 4
        public virtual void FinalizarPericia(int id_servidor)
        {
            if (this.Status.StatusOMFEnum != StatusOMFEnum.AguardandoResultadoPericia)
                throw new Exception("Status inválido");

            if(_responsaveisPericia.Count == 0)
                throw new Exception("Insira pelo menos um responsável na perícia.");

            foreach (NotaEntregaMaterialOMFResponsavelPericia pericia in _responsaveisPericia)
            {
                if(!pericia.TipoNotificacao.FlagAprovacao)
                    throw new Exception("Todos os responsaveis devem aprovar a perícia.");
            }

            using (TransactionBlock tran = new TransactionBlock())
            {
                foreach (NotaEntregaMaterialOMFItem item in _itens)
                {
                    MovimentoEstoque movimento = new MovimentoEstoque();
                    movimento.Data = DateTime.Today;
                    movimento.Material = item.Material;
                    //movimento.OrigemMaterial = OrigemMaterial.Singra;
                    movimento.Quantidade = item.Quantidade;
                    movimento.TipoMovimento = TipoMovimento.Entrada;
                    movimento.TipoOperacaoEstoque = TipoOperacaoEstoque.Normal;
                    movimento.ItemOMF = item;
                    movimento.Save();
                    
                }
                StatusOMFEnum proximoStatus = StatusOMFEnum.AguardandoProcessamento;

                SaveHistoricoEOMF(id_servidor, proximoStatus);

                tran.IsValid = true;
            }
        }

        // PASSO 5, 6, 7
        public virtual void IrParaProximoStatus(int id_servidor, string justificativa)
        {
            if (!this.IsPersisted)
                throw new Exception("Salve os dados do pedido antes de enviá-lo.");

            //if (this.Status.StatusOMFEnum == StatusOMFEnum.Finalizado)
            //    throw new Exception("Esta OMF já foi finalizado.");


            StatusOMFEnum proximoStatus = StatusOMF.GetNext(_status.StatusOMFEnum);

            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoOMF historico = GetHistorico(id_servidor, proximoStatus);

                historico.Justificativa = justificativa;
                historico.Save();

                this.Status = StatusOMF.Get(proximoStatus);
                base.Save();

                this._historico.Add(historico);

                tran.IsValid = true;
            }
        }


        private void SaveHistoricoEOMF(int id_servidor, StatusOMFEnum proximoStatus)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoOMF historico = GetHistorico(id_servidor, proximoStatus);

                historico.Save();

                this.Status = StatusOMF.Get(proximoStatus);
                base.Save();

                this._historico.Add(historico);

                tran.IsValid = true;
            }
        }


        private HistoricoOMF GetHistorico(int id_servidor, StatusOMFEnum novoStatus)
        {
            HistoricoOMF historico = new HistoricoOMF();
            historico.Data = DateTime.Now;
            historico.OMF = this;
            historico.Descricao = string.Format("{0} -> {1}", this.Status.Descricao, Util.GetDescription(novoStatus));
            historico.Servidor = Servidor.Get(id_servidor);
            return historico;
        }

        /// <summary>
        /// Retorna para o status anterior
        /// </summary>
        public virtual void Recusar(int id_servidor, string justificativa)
        {
            StatusOMFEnum proximoStatus = StatusOMF.GetAnterior(_status.StatusOMFEnum);

            HistoricoOMF historico = GetHistorico(id_servidor, proximoStatus);
            historico.Justificativa = justificativa;

            using (TransactionBlock tran = new TransactionBlock())
            {
                historico.FlagReprovado = true;
                historico.Save();

                this.Status = StatusOMF.Get(proximoStatus);

                base.Save();

                this._historico.Add(historico);
                tran.IsValid = true;
            }
        }


        public virtual void Cancelar(int id_servidor, int id_motivoCancelamento)
        {
            StatusOMFEnum proximoStatus = StatusOMFEnum.Cancelado;

            MotivoCancelamento motivo = Business.MotivoCancelamento.Get(id_motivoCancelamento);
            HistoricoOMF historico = GetHistorico(id_servidor, proximoStatus);
            historico.Justificativa = motivo.Descricao;
            this.MotivoCancelamento = motivo;

            using (TransactionBlock tran = new TransactionBlock())
            {
                historico.FlagReprovado = true;
                historico.Save();

                this.Status = StatusOMF.Get(proximoStatus);

                base.Save();

                this._historico.Add(historico);

                tran.IsValid = true;
            }
        }
        #endregion
		
		
	}
}
