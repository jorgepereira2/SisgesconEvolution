using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoCotacao : BusinessObject<PedidoCotacao>, ICompra
	{
		#region Private Members
		private Servidor _servidor; 
		private DateTime _dataemissao; 
		private bool _flagFinalizado; 
		private int _numero; 
		private string _observacao;
	    private bool _flagRecusado;
	    private string _justificativaRecusa;
		
	    private Fornecedor _fornecedorCotacao1;
        private Fornecedor _fornecedorCotacao2;
        private Fornecedor _fornecedorCotacao3;
        private Fornecedor _fornecedorCotacao4;

	    private TipoCompra _tipoCompra;
	    private NaturezaDespesa _naturezaDespesa;

		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoCotacao()
		{
			_servidor =  null; 
			_dataemissao = DateTime.MinValue; 
			_flagFinalizado =  false; 
			_numero = 0; 
			_observacao = null;
		    _flagRecusado = false;
		    _justificativaRecusa = null;
		    _itens = new CustomList<PedidoCotacaoItem>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
        public virtual NaturezaDespesa NaturezaDespesa
        {
            get { return _naturezaDespesa; }
            set { _naturezaDespesa = value; }
        }
        
        public virtual TipoCompra TipoCompra
        {
            get { return _tipoCompra; }
            set { _tipoCompra = value; }
        }
        
        public virtual string JustificativaRecusa
        {
            get { return _justificativaRecusa; }
            set { _justificativaRecusa = value; }
        }
        public virtual bool FlagRecusado
        {
            get { return _flagRecusado; }
            set { _flagRecusado = value; }
        }
        
        public virtual Fornecedor FornecedorCotacao4
        {
            get { return _fornecedorCotacao4; }
            set { _fornecedorCotacao4 = value; }
        }

        public virtual Fornecedor FornecedorCotacao3
        {
            get { return _fornecedorCotacao3; }
            set { _fornecedorCotacao3 = value; }
        }
        public virtual Fornecedor FornecedorCotacao2
        {
            get { return _fornecedorCotacao2; }
            set { _fornecedorCotacao2 = value; }
        }

        public virtual Fornecedor FornecedorCotacao1
        {
            get { return _fornecedorCotacao1; }
            set { _fornecedorCotacao1 = value; }
        }		
        	
		public virtual Servidor Servidor
		{
			get { return _servidor; }
			set { _servidor = value; }
		}
		
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime DataEmissao
		{
			get { return _dataemissao; }
			set { _dataemissao = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagFinalizado
		{
			get { return _flagFinalizado; }
			set { _flagFinalizado = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual int Numero
		{
			get { return _numero; }
			set { _numero = value; }
		}
			
		public virtual string Observacao
		{
			get { return _observacao; }
			set	
			{
				if ( value != null )
					if( value.Length > 500)
						throw new ArgumentOutOfRangeException("Invalid value for Observacao", value, value.ToString());
				
				_observacao = value;
			}
		}
		#endregion 
		
        #region Collections

        private ICustomList<PedidoCotacaoItem> _itens;

        public virtual ICustomList<PedidoCotacaoItem> Itens
        {
            get { return _itens; }
            set { _itens = value; }
        }
        #endregion
		
        #region Advanced Properties
        public virtual decimal ValorTotal
        {
            get
            {
                decimal valor = 0;
                foreach (PedidoCotacaoItem item in _itens)
                {
                    valor += item.ValorTotal;
                }
                return valor;
            }
        }

        public virtual bool PodeSerAlterado
        {
            get
            {
                return !_flagFinalizado;
            }
        }
        
	    public virtual bool FlagLicitacao
	    {
	        get
	        {
	            return _itens.Count > 0 && _itens[0].ItemLicitacao != null;
	        }
	    }
	    
	    public virtual string DescricaoFornecedores
	    {
	        get
	        {
	            string str = "";
	            if(_fornecedorCotacao1 != null)
	                str += "- " + _fornecedorCotacao1;
                if (_fornecedorCotacao2 != null)
                    str += "<br>- " + _fornecedorCotacao2;
                if (_fornecedorCotacao3 != null)
                    str += "<br>- " + _fornecedorCotacao3;
                if (_fornecedorCotacao4 != null)
                    str += "<br>- " + _fornecedorCotacao4;

	            return str;
	        }
	    }
        #endregion
		
		#region Public Methods
		
		public static Dictionary<int, string> List(int id_comprador)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from PedidoCotacao a  
			where a.FlagFinalizado = 0
			and a.Servidor.ID = :id_servidor			
			order by a.Numero");
		    
            query.SetInt32("id_servidor", id_comprador);
		    IList<PedidoCotacao> compras = query.List<PedidoCotacao>();
		    Dictionary<int, string> list = new Dictionary<int, string>(compras.Count);
		    foreach (PedidoCotacao compra in compras)
		    {
		        list.Add(compra.ID, string.Format("PC {0}", compra.Numero));
		    }
		    return list;
		}
		
		public static List<PedidoCotacao> Select(string numero, DateTime dataInicio, DateTime dataFim, bool? flagFinalizado, int id_comprador)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from PedidoCotacao a
			where a.Numero like :numero
			and dbo.DateIsInBetween(a.DataEmissao, :dataInicio, :dataFim) = 1  	  			
			and a.FlagFinalizado = IsNull(:flagFinalizado, a.FlagFinalizado)
			and a.Servidor.ID = IsNull(:id_comprador, a.Servidor.ID)
			order by a.Numero");

            query.SetString("numero", string.Format("%{0}%", numero));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("flagFinalizado", BusinessHelper.IsNull(flagFinalizado), NHibernateUtil.Boolean);
            query.SetParameter("id_comprador", BusinessHelper.IsNullOrZero(id_comprador), NHibernateUtil.Int32);
			return (List<PedidoCotacao>)query.List<PedidoCotacao>();
		}

        public static List<ICompra> SelectParaTransferencia(int id_comprador)
        {
            List<PedidoCotacao> cotacoes = Select("", DateTime.MinValue, DateTime.MinValue, false, id_comprador);
            List<AutorizacaoCompra> acs = AutorizacaoCompra.SelectACAtivas(id_comprador);

            List<ICompra> list = new List<ICompra>();
            foreach (PedidoCotacao cotacao in cotacoes)
            {
                list.Add((ICompra)cotacao);
            }

            foreach (AutorizacaoCompra ac in acs)
            {
                list.Add((ICompra)ac);
            }

            return list;
        }
		#endregion

        #region Salvar

        public override void Save()
        {
            Validar();
            base.Save();
        }

	    public virtual string Tipo
	    {
            get { return "PC"; }
	    }

	    private void Validar()
        {
            if (!this.PodeSerAlterado)
                throw new Exception("Este PO não pode ser alterado neste estágio.");
                
            try
            {
                Dictionary<int, int> list = new Dictionary<int, int>();
                if(_fornecedorCotacao1 != null)
                    list.Add(_fornecedorCotacao1.ID, _fornecedorCotacao1.ID);
                if (_fornecedorCotacao2 != null)
                    list.Add(_fornecedorCotacao2.ID, _fornecedorCotacao2.ID);
                if (_fornecedorCotacao3 != null)
                    list.Add(_fornecedorCotacao3.ID, _fornecedorCotacao3.ID);
                if (_fornecedorCotacao4 != null)
                    list.Add(_fornecedorCotacao4.ID, _fornecedorCotacao4.ID);
            }
            catch
            {
                throw new Exception("Não é posssível escolher o mesmo fornecedor.");
            }

        }

        //public static void CriarNovo(List<PedidoObtencaoItem> itens, bool usarSaldoLicitacao)
        //{
        //    if(itens.Count == 0) throw new Exception("Selecione pelo menos um item.");

        //    List<int> fornecedorList = new List<int>();

            
        //    using (TransactionBlock tran = new TransactionBlock())
        //    {
        //        if (itens[0].ItemLicitacaoDisponivel != null && usarSaldoLicitacao)
        //        {
        //            //ordena por id_fornecedor 
        //            Comparison<PedidoObtencaoItem> comparison = (delegate(PedidoObtencaoItem item1, PedidoObtencaoItem item2) { return item1.ItemLicitacaoDisponivel.Fornecedor.ID.CompareTo(item2.ItemLicitacaoDisponivel.Fornecedor.ID);});
        //            itens.Sort(comparison);
        //            foreach (PedidoObtencaoItem item in itens)
        //            {
        //                //cria um PC para cada fornecedor
        //                if (!fornecedorList.Contains(item.ItemLicitacaoDisponivel.Fornecedor.ID))
        //                {
        //                    fornecedorList.Add(item.ItemLicitacaoDisponivel.Fornecedor.ID);
        //                    PedidoCotacao cotacao = GetNovaCotacao(item.Comprador);
        //                    cotacao._fornecedorCotacao1 = item.ItemLicitacaoDisponivel.Fornecedor;

        //                    cotacao.Save();
        //                    List<PedidoObtencaoItem> itensFornecedor = new List<PedidoObtencaoItem>();
        //                    foreach (PedidoObtencaoItem itemFornecedor in itens)
        //                    {
        //                        if (itemFornecedor.ItemLicitacaoDisponivel.Fornecedor.ID == cotacao._fornecedorCotacao1.ID)
        //                            itensFornecedor.Add(itemFornecedor);
        //                    }
        //                    cotacao.InserirItensPC(itensFornecedor, usarSaldoLicitacao);
        //                    cotacao.VerificaSeItensEstaoNaMesmaLicitacao();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            PedidoCotacao cotacao = GetNovaCotacao(itens[0].Comprador);
        //            cotacao.Save();
        //            cotacao.InserirItensPC(itens, usarSaldoLicitacao);
        //        }
               
        //        tran.IsValid = true;
        //    }
            
        //}

        private static PedidoCotacao GetNovaCotacao(Servidor comprador)
        {
            PedidoCotacao cotacao= new PedidoCotacao();
            cotacao.DataEmissao = DateTime.Today;
            cotacao.FlagFinalizado = false;
            cotacao.Servidor = comprador;
            //cotacao.TipoCompra = Business.TipoCompra.Get(Convert.ToInt32(TipoCompraEnum.Industrial));
            cotacao.TipoCompra = Business.TipoCompra.Get(Convert.ToInt32(TipoCompraEnum.Dispensa));
            cotacao.Numero = GetNextNumber();
            return cotacao;
        }

        public static int GetNextNumber()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                @"select MAX(a.Numero) from PedidoCotacao a ");

            object result = query.UniqueResult();
            if (result == null)
                return 1;
            else
                return Convert.ToInt32(result) + 1;
        }

	    public virtual void InserirItens(List<PedidoObtencaoItem> itens, bool usarSaldoLicitacao)
        {
            if (itens.Count == 0) throw new Exception("Selecione pelo menos um item.");
           
           // if (itens[0].Comprador.ID != this.Servidor.ID) throw new Exception("Não é possível inserir itens em AC de outro comprador.");

            using (TransactionBlock tran = new TransactionBlock())
            {
                InserirItensPC(itens, usarSaldoLicitacao);

                VerificaSeItensEstaoNaMesmaLicitacao();
                tran.IsValid = true;
            }
        }


	    private void InserirItensPC(List<PedidoObtencaoItem> itens, bool usarSaldoLicitacao)
        {
            foreach (PedidoObtencaoItem obtencaoItem in itens)
            {
                PedidoCotacaoItem item = null;
                item = FindItemPorServicoMaterial(obtencaoItem.ServicoMaterial.ID, obtencaoItem.Especificacao);
                if(item == null)
                {
                    item = new PedidoCotacaoItem();
                    item.PedidoCotacao = this;
                    item.ServicoMaterial = obtencaoItem.ServicoMaterial;
                    item.Quantidade = obtencaoItem.Quantidade;
                    item.Especificacao = obtencaoItem.Especificacao;
                    
                    this._itens.Add(item);
                }
                else
                {
                    item.Quantidade += obtencaoItem.Quantidade;
                }
                
                if(usarSaldoLicitacao && obtencaoItem.ItemLicitacaoDisponivel != null)
                {
                    item.ItemLicitacao = obtencaoItem.ItemLicitacaoDisponivel;
                    item.ValorCotacao1 = item.ItemLicitacao.ValorFinalPregao;
                    obtencaoItem.ItemLicitacaoDisponivel.QuantidadeUtilizada += obtencaoItem.Quantidade;
                    obtencaoItem.ItemLicitacaoDisponivel.Save();
                }
                else if (!usarSaldoLicitacao && item.ItemLicitacao != null)
                {
                    throw new Exception("Já existiam itens licitados nesta AC, logo não é possível incluir itens não licitados.");
                }
                
                item.Save();

                //obtencaoItem.PedidoCotacaoItem = item;
                obtencaoItem.Save();
            }
        }
        
        private  void VerificaSeItensEstaoNaMesmaLicitacao()
        {
            LicitacaoItem itemLicitacao=null; 
            //Pega o primeiro item q tem licitacao
            foreach (PedidoCotacaoItem item in _itens)
            {
                if (item.ItemLicitacao != null)
                {
                    itemLicitacao = item.ItemLicitacao;
                    break;
                }
            }
            if(itemLicitacao == null) return;

            foreach (PedidoCotacaoItem item in _itens)
            {
                if (item.ItemLicitacao != null)
                {
                    if (item.ItemLicitacao.Licitacao.ID != itemLicitacao.Licitacao.ID)
                        throw new Exception("Todos os itens devem pertencer a mesma licitação.");
                    if(item.ItemLicitacao.Fornecedor.ID != itemLicitacao.Fornecedor.ID)
                        throw new Exception("Todos os itens devem ter o mesmo fornecedor.");
                }
            }
        }

	    private PedidoCotacaoItem FindItemPorServicoMaterial(int id, string especificacao)
	    {
	        foreach (PedidoCotacaoItem item in _itens)
	        {
	            if(item.ServicoMaterial.ID == id && item.Especificacao == especificacao)
	                return item;
	        }
	        return null;
	    }

	    #endregion
		
		public virtual void RetirarItemCotacao(int id_item)
		{
            PedidoCotacaoItem item = PedidoCotacaoItem.Get(id_item);
		    using(TransactionBlock tran = new TransactionBlock())
		    {
                if (item.ItemLicitacao != null && item.ItemLicitacao.QuantidadeUtilizada > 0)
                {
                    item.ItemLicitacao.QuantidadeUtilizada -= item.Quantidade;
                    item.ItemLicitacao.Save();
                }
		        foreach (PedidoObtencaoItem obtencaoItem in item.ItensObtencao)
		        {
		          //  obtencaoItem.PedidoCotacaoItem = null;
                    
		            obtencaoItem.Save();
		        }
                item.Delete();

		        tran.IsValid = true;
		    }
		    
		    _itens.Remove(_itens.Find(id_item));
		}
		
		public virtual void CriarAutorizacaoCompra(Dictionary<int, int> vencedores, string comentario)
		{
		    if(_flagFinalizado) throw new Exception("Esta cotação já foi finalizada.");
		    
		    List<AutorizacaoCompra> list = new List<AutorizacaoCompra>();
		    
		    try
		    {
		        using (TransactionBlock tran = new TransactionBlock())
		        {
		            foreach (KeyValuePair<int, int> pair in vencedores)
		            {
		                PedidoCotacaoItem item = PedidoCotacaoItem.Get(pair.Key);
		                Fornecedor fornecedor;
		                decimal valor = 0;
		                if (pair.Value == 1)
		                {
		                    fornecedor = _fornecedorCotacao1;
		                    valor = item.ValorCotacao1.Value;
		                }
		                else if (pair.Value == 2)
		                {
		                    fornecedor = _fornecedorCotacao2;
		                    valor = item.ValorCotacao2.Value;
		                }
		                else if (pair.Value == 3)
		                {
		                    fornecedor = _fornecedorCotacao3;
		                    valor = item.ValorCotacao3.Value;
		                }
		                else if (pair.Value == 4)
		                {
		                    fornecedor = _fornecedorCotacao4;
		                    valor = item.ValorCotacao4.Value;
		                }
		                else
		                    throw new Exception("Nenhum vencedor foi escolhido.");

		                if (fornecedor == null) throw new Exception("Nenhum vencedor foi escolhido.");

		                AutorizacaoCompra ac =
		                    list.Find(delegate(AutorizacaoCompra match) { return match.Fornecedor.ID == fornecedor.ID; });
		                if (ac == null)
		                {
		                    ac = new AutorizacaoCompra();
		                    ac.TipoCompra = this.TipoCompra;
		                    ac.DataEmissao = DateTime.Today;
		                    ac.Fornecedor = Fornecedor.Get(fornecedor.ID);
		                    ac.Servidor = Servidor.Get(_servidor.ID);
		                    ac.Observacao = this._observacao;
		                    ac.NaturezaDespesa = NaturezaDespesa.Get(this._naturezaDespesa.ID);
		                    ac.Status =
		                        StatusAutorizacaoCompra.Get(StatusAutorizacaoCompraEnum.AguardandoAprovacaoEncarregadoObtencao);
		                    if (item.ItemLicitacao != null)
		                        ac.Licitacao = item.ItemLicitacao.Licitacao;

		                    ac.Save();

                            //Criar historico
                            HistoricoAutorizacaoCompra historico = new HistoricoAutorizacaoCompra();
		                    historico.AutorizacaoCompra = ac;
		                    historico.Servidor = ac.Servidor;
		                    historico.Data = DateTime.Now;
		                    historico.Descricao = "AC Enviada";
		                    historico.Justificativa = comentario;
                            historico.Save();

		                    list.Add(ac);

		                    _flagFinalizado = true;
		                    _flagRecusado = false;
		                    base.Save();
		                }

		                item.AutorizacaoCompra = ac;
		                item.Valor = valor;
		                item.Save();
		            }

		            ValidaLicitacoes(list);

		            ValidaLimites(list);

		            tran.IsValid = true;
		        }
		    }
		    catch(Exception ex)
		    {
		        _flagFinalizado = false;
		        throw;
		    }
		}

	    private void ValidaLimites(List<AutorizacaoCompra> listaAc)
	    {
	        if(!Parametro.Get().FlagPermiteACComLimiteEstourado)
	        {
	            foreach (AutorizacaoCompra ac in listaAc)
	            {
	                SaldoFornecedor saldo = AutorizacaoCompra.GetSaldoComprasUtilizado(ac.Fornecedor.ID, ac.TipoCompra.ID, ac.DataEmissao.Year);
	                if(saldo.SaldoTotal + ac.ValorTotal > ac.TipoCompra.LimiteAnual)
	                    throw new Exception(string.Format("As compras para o fornecedor {0} ultrapassou o limite anual.", ac.Fornecedor.RazaoSocial));
	            }
	        }
	    }

	    private void ValidaLicitacoes(List<AutorizacaoCompra> list)
        {
	        Parametro parametro = Parametro.Get();
            foreach (AutorizacaoCompra ac in list)
            {
                if(ac.Licitacao == null) continue;
                
                foreach (PedidoCotacaoItem item in ac.Itens)
                {
                    //TODO: pegar o 1.25 de um parametro
                    if(item.ValorTotal > item.ItemLicitacao.ValorTotalFinalPregao * (1 + parametro.PercentualLimiteAcimaLicitacao/100))
                    {
                        throw new Exception(
                        string.Format("O valor total do item {0} não pode ser maior que {1}% do valor licitado.", item.ServicoMaterial.Descricao, parametro.PercentualLimiteAcimaLicitacao.ToString("N2")));
                    }
                }
            }
        }
	   
	}
}
