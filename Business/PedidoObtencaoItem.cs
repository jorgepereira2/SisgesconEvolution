using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoObtencaoItem : BusinessObject<PedidoObtencaoItem>, IItemServicoMaterial
	{
		#region Private Members

        private PedidoObtencao _pedidoobtencao;
        private ServicoMaterial _servicomaterial;
        private decimal _quantidade; 
		private decimal _valor; 
		private OrigemMaterial _origemmaterial;
        private decimal _quantidadeEntregue;
	    private string _especificacao;
	    private string _rmc;
	    private Fornecedor _fornecedor;
	    private bool _flagExec;
        
		#endregion
		
		#region Default ( Empty ) Class Constuctor

		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoObtencaoItem()
		{
		    _quantidadeEntregue = 0;
            _pedidoobtencao = null;
            _servicomaterial = null;
			_quantidade = 0; 
			_valor = 0;
		   
			_origemmaterial =  Business.OrigemMaterial.PEP;
		}

		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual decimal ValorCotacao1 { get; set; }
        public virtual decimal ValorCotacao2 { get; set; }
        public virtual decimal ValorCotacao3 { get; set; }
        public virtual decimal ValorCotacao4 { get; set; }
        public virtual LicitacaoItem LicitacaoItem { get; set; }
        public virtual bool FlagRecebido { get; set; }
        public virtual DateTime? DataRecebimento { get; set; }
        public virtual Servidor ServidorRecebimento { get; set; }
        public virtual SubNaturezaDespesa SubNaturezaDespesa { get; set; }
        
        public virtual bool FlagExec
        {
            get { return _flagExec; }
            set { _flagExec = value; }
        }
        public virtual Fornecedor Fornecedor
        {
            get { return _fornecedor; }
            set { _fornecedor = value; }
        }

        public virtual string RMC
        {
            get { return _rmc; }
            set { _rmc = value; }
        }
        public virtual string Especificacao
        {
            get { return _especificacao; }
            set { _especificacao = value; }
        }
        public virtual decimal QuantidadeEntregue
        {
            get { return _quantidadeEntregue; }
            set { _quantidadeEntregue = value; }
        }
      
		public virtual PedidoObtencao PedidoObtencao
		{
			get { return _pedidoobtencao; }
			set { _pedidoobtencao = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual ServicoMaterial ServicoMaterial
		{
			get { return _servicomaterial; }
			set { _servicomaterial = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual decimal Quantidade
		{
			get { return _quantidade; }
			set { _quantidade = value; }
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
		public virtual OrigemMaterial OrigemMaterial
		{
			get { return _origemmaterial; }
			set { _origemmaterial = value; }
		}
			#endregion 
		
        #region Advanced Properties
        public virtual decimal ValorTotal
        {
            get
            {
                return _valor * _quantidade;
            }
        }
	    
	    private LicitacaoItem _itemLicitacaoDisponivel;
	    private bool _buscouLicitacao = false;
	    public virtual LicitacaoItem ItemLicitacaoDisponivel
	    {
	        get
	        {
	            if(!_buscouLicitacao && this.PedidoObtencao.Licitacao != null)
	            {
	                _itemLicitacaoDisponivel = LicitacaoItem.GetItemAberto(_servicomaterial.ID, this.PedidoObtencao.Licitacao.ID);
	                _buscouLicitacao = true;
	            }
	            return _itemLicitacaoDisponivel;
	        }
	    }

       
        #endregion
        
        #region SELECTS
        public static List<PedidoObtencaoItem> SelectItemCotacaoPendente(int id_comprador)
        {
            //TODO: Pegar apenas itens que não estao ligados a uma AC
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from PedidoObtencaoItem i inner join fetch i.PedidoObtencao p 
			where i.Comprador.ID = :id_comprador			
			and   i.PedidoCotacaoItem IS NULL
			order by p.Numero");

            query.SetInt32("id_comprador", id_comprador);
            return (List<PedidoObtencaoItem>)query.List<PedidoObtencaoItem>();
        }

        public static List<PedidoObtencaoItem> SelectItemCotacaoPendente(int id_comprador, int id_servicoMaterial)
        {
            //TODO: Pegar apenas itens que não estao ligados a uma AC
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from PedidoObtencaoItem i 
                    inner join fetch i.PedidoObtencao p 
                    inner join fetch i.ServicoMaterial s
                    inner join fetch i.Comprador c
			where c.ID = IsNull(:id_comprador, c.ID)
            and s.ID = IsNull(:id_servicoMaterial, s.ID)			
			and i.PedidoCotacaoItem IS NULL
			order by p.Numero");

            query.SetParameter("id_comprador", BusinessHelper.IsNullOrZero(id_comprador), NHibernateUtil.Int32);
            query.SetParameter("id_servicoMaterial", BusinessHelper.IsNullOrZero(id_servicoMaterial), NHibernateUtil.Int32);
            return (List<PedidoObtencaoItem>)query.List<PedidoObtencaoItem>();
        }
        
        #endregion
		
		public virtual void Cancelar(int id_servidor, string justificativa, AutorizacaoCompra ac)
		{
		    PedidoObtencaoItemCancelado item = new PedidoObtencaoItemCancelado();
		    item.Data = DateTime.Now;
		    item.ServicoMaterial = this._servicomaterial;
		    item.Quantidade = this._quantidade;
		    item.Justificativa = justificativa;
		    item.PedidoObtencao = this._pedidoobtencao;
		    item.Servidor = Servidor.Get(id_servidor);
		    item.AutorizacaoCompra = ac;
            
		    using(TransactionBlock tran = new TransactionBlock())
		    {
		        item.Save();
		        
                _pedidoobtencao.Itens.Remove(this);

                PedidoObtencaoItem itemPO = PedidoObtencaoItem.Get(this.ID);
                itemPO.Delete();
                
		        tran.IsValid = true;
		    }
		}

	    public virtual string GetDescricaoFornecedorOrcamento()
	    {
            if (_pedidoobtencao.DelineamentoOrcamento == null) return "";
	        foreach (PedidoServicoItemOrcamento itemOrcamento in _pedidoobtencao.DelineamentoOrcamento.ItensOrcamento)
	        {
                if (itemOrcamento.ServicoMaterial.ID == this.ServicoMaterial.ID)
                    return itemOrcamento.Fornecedor == null ? "" : itemOrcamento.Fornecedor.RazaoSocial;
	        }
	        return "";
	    }

        public virtual void Descartar(int id_servidor, string justificativa)
        {
            
            PedidoCotacaoItemDescartado item = new PedidoCotacaoItemDescartado();
            item.PedidoObtencao = this.PedidoObtencao;
            item.ServicoMaterial = this.ServicoMaterial;
            item.Servidor = Servidor.Get(id_servidor);
            item.Justificativa = justificativa;

            using(TransactionBlock tran = new TransactionBlock())
            {
                item.Save();
                
                this.Save();

                tran.IsValid = true;
            }
        }

        private decimal QuantidadeUtilizadaPM(int id_status)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select sum(i.Quantidade) from PedidoObtencaoItem i
			where i.PedidoObtencao.TipoPedido = :tipoPedido
            and i.PedidoObtencao.Celula.ID = :id_celula
            and i.ServicoMaterial.ID = :id_material
            and dbo.GetYear(i.PedidoObtencao.DataEmissao) = :ano
            and dbo.GetMonth(i.PedidoObtencao.DataEmissao) = :mes
			and i.PedidoObtencao.Status.ID != :statusCancelado
            and i.PedidoObtencao.Status.ID > :status
			");

            query.SetInt32("ano", this.PedidoObtencao.DataEmissao.Year);
            query.SetInt32("mes", this.PedidoObtencao.DataEmissao.Month);
            query.SetInt32("id_celula", this.PedidoObtencao.Celula.ID);
            query.SetInt32("id_material", this.ServicoMaterial.ID);
            query.SetInt32("tipoPedido", Convert.ToInt32(TipoPedido.PedidoMaterial));
            query.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoObtencaoEnum.Cancelado));
            query.SetInt32("status", Convert.ToInt32(id_status));

            return Convert.ToDecimal(query.UniqueResult());
        }

        public virtual int GetCota()
        {
            //decimal qtdUtilizada = this.QuantidadeUtilizadaPM(0);
            Celula celula;
            if(this.PedidoObtencao.Celula.TipoCelula == TipoCelula.Secao)
                celula = this.PedidoObtencao.Celula.GetDivisao();
            else
                celula = this.PedidoObtencao.Celula;
            return CotaMaterial.GetCota(this.ServicoMaterial.ID, celula.ID, this.PedidoObtencao.DataEmissao.Year, this.PedidoObtencao.DataEmissao.Month);

            //return cota - qtdUtilizada;
        }

        public virtual decimal GetQuantidadeAprovada()
        {
            return this.QuantidadeUtilizadaPM(Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoCreditoEmpenho));
            //decimal cota =  CotaMaterial.GetCota(this.ServicoMaterial.ID, this.PedidoObtencao.Celula.GetDivisao().ID, this.PedidoObtencao.DataEmissao.Year, this.PedidoObtencao.DataEmissao.Month);

            //return cota - qtdUtilizada;
        }
        
	}

    
}
