using System;
using System.Collections.Generic;
using System.Threading;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class LicitacaoItem : BusinessObject<LicitacaoItem>	
	{
		#region Private Members
		private Licitacao _licitacao; 
		private ServicoMaterial _material; 
		private decimal _quantidade; 
		private decimal _valor1; 
		private decimal _valor2; 
		private decimal _valor3; 
		private decimal _valorfinalpregao; 
		private Fornecedor _fornecedor; 
		private TipoCalculoLicitacaoItem _tipocalculo;
	    private decimal _quantidadeUtilizada;
	   
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public LicitacaoItem()
		{
			_licitacao =  null; 
			_material =  null; 
			_quantidade = 0; 
			_valor1 = 0; 
			_valor2 = 0; 
			_valor3 = 0; 
			_valorfinalpregao = 0; 
			_fornecedor =  null; 
			_tipocalculo = TipoCalculoLicitacaoItem.MenorValor;
		    _quantidadeUtilizada = 0;
		}

	    public LicitacaoItem(Licitacao licitacao)
	    {
	        _licitacao = licitacao;
	    }

	    #endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual string Observacao { get; set; }
        public virtual string NumeroContratoAta { get; set; }
        public virtual decimal Valor4 { get; set; }
        public virtual decimal Valor5 { get; set; }
        public virtual LicitacaoContrato Contrato{ get; set; }

        public virtual decimal QuantidadeUtilizada
        {
            get { return _quantidadeUtilizada; }
            set
            {
                //if(value > _quantidade)
                //    throw new Exception("A quantidade utilizada não pode ser maior que a quantidade licitada.");
                _quantidadeUtilizada = value;
            }
        }	
        	
		public virtual Licitacao Licitacao
		{
			get { return _licitacao; }
			set { _licitacao = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual ServicoMaterial Material
		{
			get { return _material; }
			set { _material = value; }
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
		public virtual decimal Valor1
		{
			get { return _valor1; }
			set { _valor1 = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal Valor2
		{
			get { return _valor2; }
			set { _valor2 = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal Valor3
		{
			get { return _valor3; }
			set { _valor3 = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal ValorFinalPregao
		{
			get { return _valorfinalpregao; }
			set { _valorfinalpregao = value; }
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
		public virtual TipoCalculoLicitacaoItem TipoCalculo
		{
			get { return _tipocalculo; }
			set { _tipocalculo = value; }
		}
		
        #endregion

	    #region Advanced Properties

	    public virtual decimal ValorMedio
	    {
	        get
	        {
	            if(_tipocalculo == TipoCalculoLicitacaoItem.MenorValor)
	            {
	                decimal valor = Valor1;
                    if (valor == 0)
                        valor = Valor2;
                    if (valor == 0)
                        valor = Valor3;


	                if(Valor2 < valor && Valor2 > 0)
	                    valor = Valor2;
	                if(Valor3 < valor &&Valor3 > 0)
	                    valor = Valor3;
                    if (Valor4 < valor && Valor4 > 0)
                        valor = Valor4;
                    if (Valor5 < valor && Valor5 > 0)
                        valor = Valor5;

	                return valor;
	            }
	            else
	            {
	                int quantidadeValida = 0;
                    if (_valor1 > 0) quantidadeValida++;
                    if (_valor2 > 0) quantidadeValida++;
                    if (_valor3 > 0) quantidadeValida++;
                    if (Valor4 > 0) quantidadeValida++;
                    if (Valor5 > 0) quantidadeValida++;
                    if (quantidadeValida == 0) return 0;
	                return (_valor1 + _valor2 + _valor3 + Valor4 + Valor5)/quantidadeValida;
	            }
	        }
	    }
	    
	    public virtual decimal ValorMedioTotal
	    {
	        get{ return ValorMedio*Quantidade;}
	    }

	    public virtual decimal ValorTotalFinalPregao
	    {
	        get { return ValorFinalPregao*Quantidade; }
	    }

        public virtual decimal QuantidadeDisponivel
	    {
	        get { return Quantidade - QuantidadeUtilizada;}
	    }

	    #endregion
	    
	    #region Select
        public static LicitacaoItem GetItemAberto(int id_material, int id_licitacao)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from LicitacaoItem item
                inner join fetch item.Licitacao l                
                left join fetch item.Fornecedor f
			where l.Status = :id_status
			and item.Material.ID = :id_material
            and l.ID = IsNull(:id_licitacao, l.ID)
			and item.Quantidade - item.QuantidadeUtilizada > 0");

            // l.DataEmissao >= :dataEmissao

            //Soh entra para o saldo os itens de licitacoes com sistema licitatorio SRP (email: 23 - abril - 2011)
            //linha retirada no bacs
            //and l.SistemaLicitatorio = 2

            //query.SetInt32("ano", DateTime.Today.Year);
            //query.SetDateTime("dataEmissao", DateTime.Today.AddYears(-1));//A validade da licitacao é de 1 ano. Este parâmetro pega as licitacoes com validade ativa
            query.SetInt32("id_status", Convert.ToInt32(StatusLicitacaoEnum.Finalizado));
            query.SetInt32("id_material", id_material);
            query.SetParameter("id_licitacao", BusinessHelper.IsNullOrZero(id_licitacao), NHibernateUtil.Int32);

            IList<LicitacaoItem> list = query.List<LicitacaoItem>();
            if(list.Count > 0)
                return list[0];
            else
                return null;
        }
	    #endregion

        public override void Save()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                @"select count(*) from LicitacaoItem i
                where i.Licitacao.ID = :id_licitacao
                and i.Material.ID = :id_material
                and i.ID != :id_item");
            query.SetInt32("id_licitacao", this.Licitacao.ID);
            query.SetInt32("id_item", this.ID);
            query.SetInt32("id_material", this.Material.ID);

            if (Convert.ToInt32(query.UniqueResult()) > 0)
                throw new Exception("O mesmo item já existe nesta licitação.");

            if (QuantidadeUtilizada > Quantidade)
                throw new Exception("A quantidade utilizada não pode ser maior que a quantidade licitada.");

            base.Save();
        }
	}
}
