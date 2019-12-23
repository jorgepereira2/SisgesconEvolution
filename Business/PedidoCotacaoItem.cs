using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoCotacaoItem : BusinessObject<PedidoCotacaoItem>	
	{
		#region Private Members
	    private PedidoCotacao _pedidoCotacao;
		private AutorizacaoCompra _autorizacaocompra; 
		private ServicoMaterial _servicomaterial;
        private decimal _quantidade; 
		private decimal _valor;
	    private LicitacaoItem _itemLicitacao;
	    
	    private decimal? _valorCotacao1;
        private decimal? _valorCotacao2;
        private decimal? _valorCotacao3;
        private decimal? _valorCotacao4;

	    private bool _flagRecebido;
	    private DateTime? _dataRecebimento;
	    private Servidor _servidorRecebimento;
	    private string _especificacao;

		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoCotacaoItem()
		{
		    _flagRecebido = false;
		    _dataRecebimento = null;
		    _servidorRecebimento = null;
		    _pedidoCotacao = null;
			_autorizacaocompra =  null; 
			_servicomaterial =  null; 
			_quantidade = 0; 
			_valor = 0;
		    _itemLicitacao = null;
		    _itensObtencao = new CustomList<PedidoObtencaoItem>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
        public virtual string Especificacao
        {
            get { return _especificacao; }
            set { _especificacao = value; }
        }

        public virtual Servidor ServidorRecebimento
        {
            get { return _servidorRecebimento; }
            set { _servidorRecebimento = value; }
        }
        public virtual DateTime? DataRecebimento
        {
            get { return _dataRecebimento; }
            set { _dataRecebimento = value; }
        }
        public virtual bool FlagRecebido
        {
            get { return _flagRecebido; }
            set { _flagRecebido = value; }
        }
        
        public virtual LicitacaoItem ItemLicitacao
        {
            get { return _itemLicitacao; }
            set { _itemLicitacao = value; }
        }

        public virtual decimal? ValorCotacao4
        {
            get { return _valorCotacao4; }
            set { _valorCotacao4 = value; }
        }

        public virtual decimal? ValorCotacao3
        {
            get { return _valorCotacao3; }
            set { _valorCotacao3 = value; }
        }

        public virtual decimal? ValorCotacao2
        {
            get { return _valorCotacao2; }
            set { _valorCotacao2 = value; }
        }

        public virtual decimal? ValorCotacao1
        {
            get { return _valorCotacao1; }
            set { _valorCotacao1 = value; }
        }
       
        public virtual PedidoCotacao PedidoCotacao
        {
            get { return _pedidoCotacao; }
            set { _pedidoCotacao = value; }
        }
        
		public virtual AutorizacaoCompra AutorizacaoCompra
		{
			get { return _autorizacaocompra; }
			set { _autorizacaocompra = value; }
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
			#endregion 
			
		#region Collections

	    private ICustomList<PedidoObtencaoItem> _itensObtencao;

	    public virtual ICustomList<PedidoObtencaoItem> ItensObtencao
	    {
	        get { return _itensObtencao; }
	        set { _itensObtencao = value; }
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

	    #endregion

        private void ValidaCotacao()
        {
            Parametro parametro = Parametro.Get();
            if ((_valorCotacao1 == null && parametro.NumeroMinimoCotacoesCompra > 0)
            || (_valorCotacao2 == null && parametro.NumeroMinimoCotacoesCompra > 1)
            || (_valorCotacao3 == null && parametro.NumeroMinimoCotacoesCompra > 2)
            || (_valorCotacao4 == null && parametro.NumeroMinimoCotacoesCompra > 3))
                throw new Exception(string.Format("O número mínimo de cotações é {0}.", parametro.NumeroMinimoCotacoesCompra));
            
        }
        
        public static List<PedidoCotacaoItem> GetUltimasCompras(int id_servicoMaterial, int quantidade)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select i from PedidoCotacaoItem i 
                inner join fetch i.AutorizacaoCompra a
			where i.ServicoMaterial.ID = :id_servicoMaterial
			and a.Status.ID = :statusFinalizado		
			order by a.ID DESC");

            query.SetInt32("statusFinalizado", Convert.ToInt32(StatusAutorizacaoCompraEnum.Finalizado));
            query.SetInt32("id_servicoMaterial", id_servicoMaterial);
            query.SetMaxResults(quantidade);

            return (List<PedidoCotacaoItem>) query.List<PedidoCotacaoItem>();
        }

        public virtual void CancelarItemAC(int id_servidor, string justificativa)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                foreach (PedidoObtencaoItem obtencaoItem in _itensObtencao)
                {
                    obtencaoItem.Cancelar(id_servidor, justificativa, this.AutorizacaoCompra);
                }

                this.Delete();
                tran.IsValid = true;
            }
        }
	}
}
