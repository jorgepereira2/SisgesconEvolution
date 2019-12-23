using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NHibernate;
using Shared.Common;
using Shared.DataAccessHelper;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoServicoMergulho : BusinessObject<PedidoServicoMergulho>, IComparable<PedidoServicoMergulho>	
	{
		#region Private Members
		private int _codigointerno; 
		private DateTime _dataemissao; 
		private Cliente _cliente; 
		private Cliente _clientepagador; 
		private string _codigopedidocliente; 
		private string _observacao; 
		private string _numeroregistro; 
		private string _contatos; 
		private string _telefonecontatos; 
		private StatusPedidoServicoMergulho _statusPedidoServicoMergulho; 
		private Celula _celula;
	    private bool _flagRecusado;
	    private bool _flagABordo;
	    private Servidor _servidorCancelamento;
	    private CategoriaServico _categoriaServico;
	    private MotivoCancelamento _motivoCancelamento;
	    private DateTime? _dataPronto;
	    private DateTime? _dataPlanejamentoPS;
	    private string _diversos;
	    private DateTime? _previsaoEntrega;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoServicoMergulho()
		{
			_codigointerno = 0; 
			_dataemissao = DateTime.MinValue; 
			_cliente =  null; 
			_clientepagador =  null; 
			_codigopedidocliente = null; 
			_observacao = null; 
			_numeroregistro = null; 
			_contatos = null; 
			_telefonecontatos = null; 
		    _statusPedidoServicoMergulho = null;
			_celula =  null;
		    _flagRecusado = false;
		    _flagABordo = false;
		    _servidorCancelamento = null;
		    
		    _historico = new CustomList<HistoricoPedidoServicoMergulho>();
            _itensOrcamento = new Shared.NHibernateDAL.CustomList<PedidoServicoMergulhoItemOrcamento>();
            _itensDelineamento = new Shared.NHibernateDAL.CustomList<PedidoServicoMergulhoDelineamento>();
		  
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual string MensagemSolicitacao { get; set; }
	    public virtual string Localizacao { get; set; }
        public virtual Prioridade Prioridade { get; set; }
        public virtual Embarcacao Embarcacao { get; set; }
        public virtual DateTime? DataPrevisaoInicio { get; set; }
        public virtual DateTime? DataPrevisaoFim { get; set; }

        public virtual decimal ValorHomemHoraMergulho { get; set; }
        public virtual decimal ValorDeslocamentoMergulho { get; set; }
        public virtual decimal ValorTaxaBoteMergulho { get; set; }
        public virtual decimal ValorTaxaCatamaraMergulho { get; set; }
        public virtual decimal TaxaMaoObraDiretaMergulho { get; set; }
        public virtual decimal TaxaMaoObraIndiretaMergulho { get; set; }
        public virtual decimal TaxaOperacionalMaoObraMergulho { get; set; }
        public virtual decimal TaxaContribuicaoOperacionalMergulho { get; set; }
        public virtual decimal DescontoFRE170Mergulho { get; set; }
        public virtual decimal DescontoFRE171172Mergulho { get; set; }
        public virtual decimal ValorTaxaMaterialMergulho { get; set; }

        public virtual string MensagemIndicacaoRecurso { get; set; }
        public virtual string NLPagamento { get; set; }


        public virtual DateTime? DataPronto
        {
            get { return _dataPronto; }
            set { _dataPronto = value; }
        }

        public virtual DateTime? DataPlanejamentoPS
        {
            get { return _dataPlanejamentoPS; }
            set { _dataPlanejamentoPS = value; }
        }

        public virtual string Diversos
        {
            get { return _diversos; }
            set { _diversos = value; }
        }

        public virtual MotivoCancelamento MotivoCancelamento
        {
            get { return _motivoCancelamento; }
            set { _motivoCancelamento = value; }
        }

        public virtual CategoriaServico CategoriaServico
        {
            get { return _categoriaServico; }
            set { _categoriaServico = value; }
        }
        
        public virtual Servidor ServidorCancelamento
        {
            get { return _servidorCancelamento; }
            set { _servidorCancelamento = value; }
        }
        
        public virtual bool FlagABordo
        {
            get { return _flagABordo; }
            set { _flagABordo = value; }
        }
      
		public virtual int CodigoInterno
		{
			get { return _codigointerno; }
			set	{
				_codigointerno = value;
			}
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
		public virtual Cliente Cliente
		{
			get { return _cliente; }
			set { _cliente = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Cliente ClientePagador
		{
			get { return _clientepagador; }
			set { _clientepagador = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string CodigoPedidoCliente
		{
			get { return _codigopedidocliente; }
			set	
			{
				if ( value != null )
					if( value.Length > 20)
						throw new ArgumentOutOfRangeException("Invalid value for CodigoPedidoCliente", value, value.ToString());
				
				_codigopedidocliente = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
        //public virtual Equipamento Equipamento
        //{
        //    get { return _equipamento; }
        //    set { _equipamento = value; }
        //}
			
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
		public virtual string NumeroRegistro
		{
			get { return _numeroregistro; }
			set	
			{
				if ( value != null )
					if( value.Length > 200)
						throw new ArgumentOutOfRangeException("Invalid value for NumeroRegistro", value, value.ToString());
				
				_numeroregistro = value;
			}
		}

	    public virtual string DefeitoReclamado { get; set; }
		
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Contatos
		{
			get { return _contatos; }
			set	
			{
				if ( value != null )
					if( value.Length > 200)
						throw new ArgumentOutOfRangeException("Invalid value for Contatos", value, value.ToString());
				
				_contatos = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string TelefoneContatos
		{
			get { return _telefonecontatos; }
			set	
			{
				if ( value != null )
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for TelefoneContatos", value, value.ToString());
				
				_telefonecontatos = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual StatusPedidoServicoMergulho Status
		{
			get { return _statusPedidoServicoMergulho; }
			set { _statusPedidoServicoMergulho = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Celula Celula
		{
			get { return _celula; }
			set { _celula = value; }
		}

	    public virtual int ID_PedidoServicoMergulho
	    {
	        get { return ID;}
	    }

	    public virtual bool FlagRecusado
        {
            get { return _flagRecusado; }
            set { _flagRecusado = value; }
        }

        public virtual DateTime? PrevisaoEntrega
        {
            get { return _previsaoEntrega; }
            set { _previsaoEntrega = value; }
        }
       
        #endregion 
   
        #region Advanced Properties
	    public virtual HistoricoPedidoServicoMergulho UltimoHistorico
	    {
	        get
	        {
	            if(Historico.Count > 0)
	                return Historico[Historico.Count - 1];
	            else
	                return null;
	        }
	    }

        public virtual int HomemHoraTotal
        {
            get
            {
                int valor = 0;
                foreach (PedidoServicoMergulhoDelineamento delineamento in _itensDelineamento)
                {
                    valor +=  delineamento.QuantidadeMergulhadores * delineamento.NumeroDias * delineamento.TempoFainaDiaria;
                }
                return valor;
            }
        }

        public virtual int NumeroDiasTotal
        {
            get
            {
                int valor = 0;
                foreach (PedidoServicoMergulhoDelineamento delineamento in _itensDelineamento)
                {
                    valor += delineamento.NumeroDias;
                }
                return valor;
            }
        }

        public virtual int QuantidadeMergulhadoresTotal
        {
            get
            {
                int valor = 0;
                foreach (PedidoServicoMergulhoDelineamento delineamento in _itensDelineamento)
                {
                    valor += delineamento.QuantidadeMergulhadores;
                }
                return valor;
            }
        }

        public virtual decimal ValorTotalItens
        {
            get
            {
                decimal valor = 0;
                foreach (PedidoServicoMergulhoItemOrcamento itemOrcamento in _itensOrcamento)
                {
                    valor += itemOrcamento.Valor * itemOrcamento.Quantidade;
                }
                return valor;
            }
        }

        public virtual string DescricaoCliente
	    {
            get { return Cliente.Descricao; }
	    }

        public virtual string DescricaoStatus
	    {
	        get { return this.Status.Descricao; }
	    }

	    protected internal virtual HistoricoPedidoServicoMergulho GetUltimoHistorico(int id_delineamentoOrcamento)
	    {
	        for(int i = Historico.Count -1; i >= 0;i--)
	        {
	            if(Historico[i].ID_DelineamentoOrcamento == id_delineamentoOrcamento)
	                return Historico[i];
	        }
	        return null;
	    }
        
	    public virtual string CodigoComAno
	    {
	        get{ return string.Format("{0}/{1}", _codigointerno, _dataemissao.Year); }
	    }

        public virtual string DescricaoServicos
        {
            get
            {
                string s = "";
                foreach (PedidoServicoMergulhoDelineamento delineamento in ItensDelineamento)
                {
                    s += delineamento.DescricaoServicoOficina + "<br />";
                }
                return s;
            }
        }


        #endregion
		
		#region Collections

        #region Collections
        private ICustomList<PedidoServicoMergulhoItemOrcamento> _itensOrcamento;
        private ICustomList<PedidoServicoMergulhoDelineamento> _itensDelineamento;


        public virtual ICustomList<PedidoServicoMergulhoDelineamento> ItensDelineamento
        {
            get { return _itensDelineamento; }
            set { _itensDelineamento = value; }
        }

        public virtual ICustomList<PedidoServicoMergulhoItemOrcamento> ItensOrcamento
        {
            get { return _itensOrcamento; }
            set { _itensOrcamento = value; }
        }
        
        #endregion

	    private ICustomList<HistoricoPedidoServicoMergulho> _historico;
	
	    public virtual ICustomList<HistoricoPedidoServicoMergulho> Historico
	    {
	        get { return _historico; }
	        set { _historico = value; }
	    }

	  


	    //public virtual bool ExisteOrcamentoNaoEnviado
        //{
        //    get
        //    {
        //        foreach (DelineamentoOrcamento orcamento in _orcamentos)
        //        {
        //            if(orcamento.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.AguardandoOrcamento)
        //                return true;
        //        }
        //        return false;
        //    }
        //}
	    #endregion
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select p.ID, p.Descricao 
			from PedidoServicoMergulho p  
			where p.FlagAtivo = 1
			order by p.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}

        public static List<PedidoServicoMergulho> Select(string texto, DateTime dataInicio, DateTime dataFim, int id_status, int id_equipamento, 
            string numeroPedidoCliente, string numeroRegistro, int id_celula)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from PedidoServicoMergulho p 
                    inner join fetch p.Cliente c                     
			where dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1
			and (dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			or c.Descricao like :texto)
			and p.Status.ID = IsNull(:id_status, p.Status.ID)			
            and IsNull(p.Celula.ID, -1) = IsNull(:id_celula, IsNull(p.Celula.ID, -1))			
            and ISNULL(p.CodigoPedidoCliente,'')  like :codigoPedidoCliente
            and IsNull(p.NumeroRegistro, '') like :numeroRegistro 
			order by p.CodigoInterno");

            query.SetParameter("texto", string.Format("%{0}%", texto));
            query.SetParameter("codigoPedidoCliente", string.Format("%{0}%", numeroPedidoCliente));
            query.SetParameter("numeroRegistro", string.Format("%{0}%", numeroRegistro));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            //query.SetParameter("id_equipamento", BusinessHelper.IsNullOrZero(id_equipamento), NHibernateUtil.Int32);
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
			return (List<PedidoServicoMergulho>)query.List<PedidoServicoMergulho>();
		}

        public static List<PedidoServicoMergulho> Select(string texto, DateTime dataInicio, DateTime dataFim, int id_status)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from PedidoServicoMergulho p 
                    inner join fetch p.Cliente c                     
                    inner join fetch p.Status s 	                    
			where dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1
			and (dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1)            
            and s.ID = IsNull(:id_status, s.ID)			
			order by dbo.GetYear(p.DataEmissao) DESC, p.CodigoInterno DESC");

            query.SetParameter("texto", string.Format("%{0}%", texto));
            //query.SetParameter("id_equipamento", BusinessHelper.IsNullOrZero(id_equipamento), NHibernateUtil.Int32);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            
            return (List<PedidoServicoMergulho>)query.List<PedidoServicoMergulho>();
        }

        public static List<PedidoServicoMergulho> SelectEditaveis(string texto, DateTime dataInicio, DateTime dataFim, int id_status, int id_celula, int ano)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from PedidoServicoMergulho p 
                inner join fetch p.Cliente c                 
                inner join fetch p.Status s
                left join fetch p.Celula cel                
			where dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1
			and (dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			or c.Descricao like :texto)
			and s.ID = IsNull(:id_status, s.ID)	
            and dbo.GetYear(p.DataEmissao) = IsNull(:ano, dbo.GetYear(p.DataEmissao))
            and IsNull(cel.ID, -1) = IsNull(:id_celula, IsNull(cel.ID, -1))	            
			order by p.CodigoInterno");

            query.SetParameter("texto", string.Format("%{0}%", texto));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);

            return (List<PedidoServicoMergulho>)query.List<PedidoServicoMergulho>();
        }

        /// <summary>
        /// Seleciona os pedidos que estao pendentes da ação de um servidor
        /// </summary>
        public static List<PedidoServicoMergulho> Select(int id_servidor, int id_status, string codigo, int id_oficinaDelineamento)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct p from PedidoServicoMergulho p inner join p.Status s inner join fetch p.Cliente c 
                inner join s.Responsaveis resp
			where resp.ID = :id_servidor
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			and s.FlagVinculoPorDivisao = 0
			and s.FlagSomenteGerente = 0
			and s.ID < :statusFinalizado
			and s.ID = IsNull(:status, s.ID)
            and s.ID != :statusCancelado
            and :id_oficina is null
			order by p.CodigoInterno");

			IQuery query2 = session.CreateQuery(
            @"select distinct p from PedidoServicoMergulho p inner join p.Status s inner join fetch p.Cliente c 
                inner join s.ResponsaveisDivisao divisao
			where divisao.Servidor.ID = :id_servidor
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			and divisao.Celula.ID = p.Celula.ID
			and s.FlagVinculoPorDivisao = 1
			and s.FlagSomenteGerente = 0
			and s.ID < :statusFinalizado
            and s.ID != :statusCancelado
			and s.ID = IsNull(:status, s.ID)
            and :id_oficina is null 
			order by p.CodigoInterno");

         
            query.SetInt32("id_servidor", id_servidor);
            query.SetInt32("statusFinalizado", Convert.ToInt32(StatusPedidoServicoMergulhoEnum.Finalizado));
            query.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoMergulhoEnum.Cancelado));
            query.SetParameter("status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("texto", string.Format("%{0}%", codigo));
            query.SetParameter("id_oficina", BusinessHelper.IsNullOrZero(id_oficinaDelineamento), NHibernateUtil.Int32);
            query2.SetInt32("id_servidor", id_servidor);
            query2.SetInt32("statusFinalizado", Convert.ToInt32(StatusPedidoServicoMergulhoEnum.Finalizado));
            query2.SetParameter("status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query2.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoMergulhoEnum.Cancelado));
            query2.SetParameter("texto", string.Format("%{0}%", codigo));
            query2.SetParameter("id_oficina", BusinessHelper.IsNullOrZero(id_oficinaDelineamento), NHibernateUtil.Int32);
        
            
            
            List<PedidoServicoMergulho> list1 = (List<PedidoServicoMergulho>)query.List<PedidoServicoMergulho>();
            List<PedidoServicoMergulho> list2 = (List<PedidoServicoMergulho>)query2.List<PedidoServicoMergulho>();
          
            list1.AddRange(list2);


            list1.Sort(new Comparison<PedidoServicoMergulho>(delegate(PedidoServicoMergulho p1, PedidoServicoMergulho p2)
                {
                    return p1.CodigoInterno.CompareTo(p2.CodigoInterno);
                }));
                
            return list1;
        }


        public static List<PedidoServicoMergulho> Select(int id_status, int id_celula, DateTime dataInicio, DateTime dataFim, string numeroRegistro, string observacao, int id_cliente, int ano, string numeroPS, DateTime dataPrevisaoEntregaInicio, 
            DateTime dataPrevisaoEntregaFim, DateTime dataRealizadoInicio, DateTime dataRealizadoFim, string codigoPedidoCliente)
        {
            string condicaoStatus = "";
            if (id_status == 0)
                condicaoStatus = "";
            else if (id_status == Int32.MinValue || id_status != Convert.ToInt32(StatusPedidoServicoMergulhoEnum.Cancelado))
                condicaoStatus = string.Format("and s.ID NOT IN ({0})", Convert.ToInt32(StatusPedidoServicoMergulhoEnum.Cancelado));

            if(dataRealizadoInicio != DateTime.MinValue)
                condicaoStatus += string.Format(@" and exists(from HistoricoPedidoServicoMergulho h where h.PedidoServicoMergulho.ID = p.ID and h.StatusPosterior = {0} 
                        and h.Data >= '{1}')", Convert.ToInt32(StatusPedidoServicoMergulhoEnum.Finalizado), dataRealizadoInicio.ToString("yyyy-MM-dd"));


            if (dataRealizadoFim != DateTime.MinValue)
                condicaoStatus += string.Format(@" and exists( from HistoricoPedidoServicoMergulho h where h.ID = p.ID and h.StatusPosterior = {0} 
                        and h.Data <= '{1}')", Convert.ToInt32(StatusPedidoServicoMergulhoEnum.Finalizado), dataRealizadoFim.ToString("yyyy-MM-dd"));

            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            string.Format(@"select distinct p from PedidoServicoMergulho p 
                inner join p.Status s 
                inner join fetch p.Cliente c 
                inner join fetch p.ClientePagador cp                                 
                left join fetch p.Celula cel                     
			where s.ID = IsNull(:id_status, s.ID)			
            and c.ID = IsNull(:id_cliente, c.ID)
			and dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1									
			and IsNull(cel.ID, -1) = IsNull(:id_celula, IsNull(cel.ID, -1))	
			and IsNull(p.NumeroRegistro, 'xxx') like IsNull(:numeroRegistro, IsNull(p.NumeroRegistro, 'xxx'))	
            and IsNull(p.CodigoPedidoCliente, 'xxx') like IsNull(:codigoPedidoCliente, IsNull(p.CodigoPedidoCliente, 'xxx'))	
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :numeroPS) = 1
			and IsNull(p.Observacao, 'xxx') like IsNull(:observacao, IsNull(p.Observacao, 'xxx'))	
            and dbo.GetYear(p.DataEmissao) = IsNull(:ano, dbo.GetYear(p.DataEmissao))
            and dbo.DateIsInBetween(p.PrevisaoEntrega, :dataPrevisaoEntregaInicio, :dataPrevisaoEntregaFim) = 1            
            
            {0}
            
			order by p.CodigoInterno", condicaoStatus));
         

            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
          // query.SetParameter("id_equipamento", BusinessHelper.IsNullOrZero(id_equipamento), NHibernateUtil.Int32);
            query.SetParameter("id_cliente", BusinessHelper.IsNullOrZero(id_cliente), NHibernateUtil.Int32);
            //query.SetInt32("statusAguardandoOrcamento", Convert.ToInt32(StatusPedidoServicoMergulhoEnum.AguardandoDelineamento));
            //query.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoMergulhoEnum.Cancelado));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            query.SetString("numeroRegistro", string.Format("%{0}%", numeroRegistro));
            query.SetString("codigoPedidoCliente", string.Format("%{0}%", codigoPedidoCliente));
            query.SetString("numeroPS", string.Format("%{0}%", numeroPS));
            query.SetString("observacao", string.Format("%{0}%", observacao));
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            query.SetParameter("dataPrevisaoEntregaInicio", BusinessHelper.IsNull(dataPrevisaoEntregaInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataPrevisaoEntregaFim", BusinessHelper.IsNull(dataPrevisaoEntregaFim), NHibernateUtil.DateTime);
          
            List<PedidoServicoMergulho> pedidos = (List<PedidoServicoMergulho>)query.List<PedidoServicoMergulho>();



            return pedidos.ToList();
        }
        
     

	    #endregion

        #region Delineamento
        public virtual void AddDelineamento(PedidoServicoMergulhoDelineamento delineamento)
        {
            delineamento.PedidoServicoMergulho = this;
            bool isNew = !delineamento.IsPersisted;

            delineamento.Data = DateTime.Now;
            delineamento.Save();

            if (isNew)
                _itensDelineamento.Add(delineamento);
        }

        public virtual void RemoveDelineamento(PedidoServicoMergulhoDelineamento delineamento)
        {
            delineamento.Delete();
            _itensDelineamento.Remove(delineamento);
        }
        #endregion

        #region Orçamento
        public virtual void AddItemOrcamento(PedidoServicoMergulhoItemOrcamento item)
        {
            item.PedidoServicoMergulho = this;
            item.Save();
            _itensOrcamento.Add(item);
        }

        public virtual void RemoveItemOrcamento(PedidoServicoMergulhoItemOrcamento item)
        {
            item.Delete();
            _itensOrcamento.Remove(item);
        }
        #endregion

        #region Relatorios CrossTable

        public static DataSet SelectStatusPorCelula(int ano)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[3];
            param[1] = ano;
            param[2] = null;

            return helper.ExecuteDataSet("PedidoServicoMergulho_SelectStatusPorCelula", param);
        }

        public static DataSet SelectStatusPorEquipamento(int ano, int id_equipamento)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[4];
            param[1] = ano;
            param[2] = null;
            param[3] = NullHelper.IsZero(id_equipamento);

            return helper.ExecuteDataSet("PedidoServicoMergulho_SelectStatusPorEquipamento", param);
        }

        public static DataSet SelectHistoricoStatus(int ano, int id_tipoCliente, int id_cliente, int id_celula)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[6];
            param[1] = ano;
            param[2] = null;
            param[3] = NullHelper.IsZero(id_tipoCliente);
            param[4] = NullHelper.IsZero(id_cliente);
            param[5] = NullHelper.IsZero(id_celula);

            return helper.ExecuteDataSet("PedidoServicoMergulho_SelectHistoricoStatus", param);
        }

        public static DataSet SelectProntificacaoEquipamento(int ano, int id_tipoCliente, int id_cliente, int id_celula)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[6];
            param[1] = ano;
            param[2] = null;
            param[3] = NullHelper.IsZero(id_tipoCliente);
            param[4] = NullHelper.IsZero(id_cliente);
            param[5] = NullHelper.IsZero(id_celula);

            return helper.ExecuteDataSet("PedidoServicoMergulho_ProntificacaoEquipamento", param);
        }

        public static DataSet SelectCustoRealizado( int id_equipamento, int id_status, int id_celula, 
            DateTime dataInicio, DateTime dataFim, string numeroRegistro, string observacao, int id_cliente, string numeroPS, int mes, int ano)
        {
            SQLHelper helper = new SQLHelper();
            helper.CmdTimeout = 500;
            object[] param = new object[14];
            param[1] = NullHelper.IsZero(0);
            param[2] = NullHelper.IsZero(id_status);
            param[3] = NullHelper.IsZero(id_equipamento);
            param[4] = NullHelper.IsZero(id_celula);
            param[5] = NullHelper.IsZero(id_cliente);
            param[6] = null;
            param[7] = NullHelper.IsNull(dataInicio);
            param[8] = NullHelper.IsNull(dataFim);
            param[9] = NullHelper.IsNull(numeroRegistro);
            param[10] = NullHelper.IsNull(numeroPS);
            param[11] = NullHelper.IsNull(observacao);
            param[12] = NullHelper.IsZero(mes);
            param[13] = NullHelper.IsZero(ano);

            return helper.ExecuteDataSet("PedidoServicoMergulho_SelectCustoRealizado", param);
        }

        public static DataSet SelectCustoRealizadoAC(int id_ac)
        {
            SQLHelper helper = new SQLHelper();
            helper.CmdTimeout = 500;
            object[] param = new object[2];
            param[1] = NullHelper.IsZero(id_ac);
            return helper.ExecuteDataSet("PedidoServicoMergulho_SelectCustoRealizadoAC", param);
        }
        #endregion

        public override void Save()
        {
            if(!this.IsPersisted)
                CriarNovo();
            else
                base.Save();
        }

       
        private void CriarNovo()
	    {
            ISession session = NHibernateSessionManager.Instance.GetSession();
	        IQuery query1 = session.CreateQuery(
	            @"from PedidoServicoMergulho p 
	            where p.CodigoPedidoCliente = :codigo
	            and p.Status.ID != :statusCancelado
	            and p.Cliente.ID = :id_cliente");
	        query1.SetString("codigo", this.CodigoPedidoCliente);
	        query1.SetInt32("id_cliente", this.Cliente.ID);
            query1.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoMergulhoEnum.Cancelado));
            if(query1.List<PedidoServicoMergulho>().Count > 0)
                throw new Exception("Já existe um PS com este número de pedido do cliente.");
	        
	        this.DataEmissao = DateTime.Today;
	        this.Status = StatusPedidoServicoMergulho.Get(StatusPedidoServicoMergulhoEnum.NaoEnviado);
	        
	        using (TransactionBlock tran = new TransactionBlock())
	        {
                
                IQuery query = session.CreateQuery(
                    @"select MAX(p.CodigoInterno) from PedidoServicoMergulho p where dbo.GetYear(p.DataEmissao) = :ano");
	            query.SetInt32("ano", _dataemissao.Year);

                object result = query.UniqueResult();
                if (result == null)
                    this.CodigoInterno = 1;
                else
                    this.CodigoInterno = Convert.ToInt32(result) + 1;
                    
                base.Save();
                    
	            tran.IsValid = true;
	        }
	    }

        #region Workflow

        #region Taxas

	    public virtual decimal MOD
	    {
	        get { return this.TaxaMaoObraDiretaMergulho*this.HomemHoraTotal; }
	    }

        public virtual decimal MOI
        {
            get { return this.MOD * this.TaxaMaoObraIndiretaMergulho; }
        }

        public virtual decimal TOMO
        {
            get { return (this.MOD + this.MOI) * this.TaxaOperacionalMaoObraMergulho; }
        }

        public virtual decimal MD
        {
            get { return this.ValorTaxaMaterialMergulho * this.NumeroDiasTotal * this.QuantidadeMergulhadoresTotal; }
        }

        public virtual decimal CEmbarcacao
        {
            get { return this.ValorTaxaBoteMergulho*this.NumeroDiasTotal; }
        }

        public virtual decimal DeslocamentoMG
        {
            get { return this.QuantidadeMergulhadoresTotal * this.NumeroDiasTotal * this.ValorDeslocamentoMergulho; }
        }

        public virtual decimal CustoIndireto
        {
            get { return this.ValorHomemHoraMergulho * this.HomemHoraTotal; }
        }

        public virtual decimal A123
        {
            get { return this.MOD + this.MOI + this.TOMO; }
        }

        public virtual decimal B12345
        {
            get { return this.MD + this.CEmbarcacao + this.DeslocamentoMG + this.CustoIndireto; }
        }

        public virtual decimal TCO
        {
            get { return this.A123 * 0.01m + this.B12345 * 0.05m; }
        }


        public virtual decimal TotalFRE170
        {
            get { return this.A123; }
        }

        public virtual decimal TotalFRE171172
        {
            get { return this.B12345 + this.TCO; }
        }

        public virtual decimal DescontoConcedidoFRE170
        {
            get { return this.TotalFRE170 * this.DescontoFRE170Mergulho / 100; }
        }

        public virtual decimal DescontoConcedidoFRE171172
        {
            get { return this.TotalFRE171172 * this.DescontoFRE171172Mergulho / 100; }
        }

        public virtual decimal TotalAPagar
        {
            get { return this.TotalFRE170 + this.TotalFRE171172  - this.DescontoConcedidoFRE170 - this.DescontoConcedidoFRE171172; }
        }
	    
	    #endregion


        #region Metodos Ok para o BAC

        public virtual void Enviar(int id_servidor)
	    {
	        if(this.Status.StatusPedidoServicoMergulhoEnum != StatusPedidoServicoMergulhoEnum.NaoEnviado)
	            throw new Exception("Este pedido não pode mais ser enviado.");

          

            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServicoMergulho historico = GetHistorico(id_servidor, StatusPedidoServicoMergulhoEnum.AguardandoDetalhamentoParaSupervidor);

                historico.Save();
                this.Status = historico.StatusPosterior;
                _flagRecusado = false;
                base.Save();
                this._historico.Add(historico);
                tran.IsValid = true;
            }  
	    }


        public virtual void FinalizarDetalhamento(int id_servidor, string comentario)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                //Copia dados do parametro para a tabela de PSM
                CopiarTaxas();

                HistoricoPedidoServicoMergulho historico = GetHistorico(id_servidor, StatusPedidoServicoMergulhoEnum.AguardandoExecuçãoServiço);

                historico.Save();
                this.Status = historico.StatusPosterior;
                _flagRecusado = false;
                base.Save();
                this._historico.Add(historico);
                tran.IsValid = true;
            }  
        }

        public virtual void RecalcularTaxas()
        {
            CopiarTaxas();
            base.Save();
        }

        private void CopiarTaxas()
        {
            Parametro p = Parametro.Get();
            this.ValorDeslocamentoMergulho = p.ValorDeslocamentoMergulho;
            this.ValorTaxaBoteMergulho = p.ValorTaxaBoteMergulho;
            this.ValorTaxaCatamaraMergulho = p.ValorTaxaCatamaraMergulho;
            this.TaxaMaoObraDiretaMergulho = p.TaxaMaoObraDiretaMergulho;
            this.TaxaMaoObraIndiretaMergulho = p.TaxaMaoObraIndiretaMergulho;
            this.TaxaOperacionalMaoObraMergulho = p.TaxaOperacionalMaoObraMergulho;
            this.TaxaContribuicaoOperacionalMergulho = p.TaxaContribuicaoOperacionalMergulho;
            this.DescontoFRE170Mergulho = p.DescontoFRE170Mergulho;
            this.DescontoFRE171172Mergulho = p.DescontoFRE171172Mergulho;
            this.ValorHomemHoraMergulho = p.ValorHomemHoraMergulho;
            this.ValorTaxaMaterialMergulho = p.ValorTaxaMaterialMergulho;
        }

        public virtual void EfetuarPagamento(int id_servidor, string nlPagamento, string mensagemIndicacaoRecurso)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServicoMergulho historico = GetHistorico(id_servidor, StatusPedidoServicoMergulhoEnum.Finalizado);

                historico.Save();
                this.Status = historico.StatusPosterior;
                _flagRecusado = false;

                this.NLPagamento = nlPagamento;
                this.MensagemIndicacaoRecurso = mensagemIndicacaoRecurso;

                base.Save();
                this._historico.Add(historico);
                tran.IsValid = true;
            }
        }


        public virtual void FinalizarExecucao(int id_servidor, string comentario)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServicoMergulho historico = GetHistorico(id_servidor, StatusPedidoServicoMergulhoEnum.EmitirFaturamento);

                historico.Save();
                this.Status = historico.StatusPosterior;
                _flagRecusado = false;
                base.Save();
                this._historico.Add(historico);
                tran.IsValid = true;
            }
        }

        public virtual void EmitirFaturamento(int id_servidor, string comentario)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServicoMergulho historico = GetHistorico(id_servidor, StatusPedidoServicoMergulhoEnum.AguardandoPagamento);

                historico.Save();
                this.Status = historico.StatusPosterior;
                _flagRecusado = false;
                base.Save();
                this._historico.Add(historico);
                tran.IsValid = true;
            }
        }

        #endregion
	   

      
        
      
	    private HistoricoPedidoServicoMergulho GetHistorico(int id_servidor, StatusPedidoServicoMergulhoEnum novoStatus)
	    {
	        HistoricoPedidoServicoMergulho historico = new HistoricoPedidoServicoMergulho();
	        historico.Data = DateTime.Now;
	        historico.PedidoServicoMergulho = this;
	        historico.StatusAnterior = this.Status;
	        historico.Servidor = Servidor.Get(id_servidor);
	        historico.StatusPosterior = Business.StatusPedidoServicoMergulho.Get(novoStatus);
	        return historico;
	    }
	    
	    /// <summary>
	    /// Retorna para o status anterior
	    /// </summary>
	    public virtual void Recusar(int id_servidor, string  justificativa)
	    {
	        StatusPedidoServicoMergulhoEnum novoStatus = StatusPedidoServicoMergulho.GetAnterior(_statusPedidoServicoMergulho.StatusPedidoServicoMergulhoEnum);
	        HistoricoPedidoServicoMergulho historico = GetHistorico(id_servidor, novoStatus);
            historico.JustificativaRecusa = justificativa;
            
            using(TransactionBlock tran = new TransactionBlock())
            {
                historico.Save();

                this.Status = historico.StatusPosterior;
                _flagRecusado = true;

                base.Save();

                this._historico.Add(historico);
                tran.IsValid = true;
            }
	    }

        public virtual void Cancelar(int id_servidor, int id_motivo, string comentario)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServicoMergulho historico = GetHistorico(id_servidor, StatusPedidoServicoMergulhoEnum.Cancelado);
                historico.JustificativaRecusa = comentario;
                historico.Save();


                this.Status = historico.StatusPosterior;
                this.MotivoCancelamento = MotivoCancelamento.Get(id_motivo);
                _flagRecusado = false;
                base.Save();

                this._historico.Add(historico);

                tran.IsValid = true;
            }
        }
	    #endregion


        public static IEnumerable FastSearch(string texto)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct new PedidoServicoUI(p.ID, p.CodigoInterno, p.DataEmissao)
            from PedidoServicoMergulho p 
			where dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
            and ISNULL(p.Status.ID, 0) NOT IN (:status)
			order by p.ID ASC");

            query.SetMaxResults(20);
            query.SetString("texto", "%" + texto + "%");

            List<int> status = new List<int>(2);
            status.Add(Convert.ToInt32(StatusPedidoServicoEnum.Finalizado));
            status.Add(Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));
            query.SetParameterList("status", status, NHibernateUtil.Int32);
            return query.List<PedidoServicoUI>();
        }


        #region IComparable<PedidoServicoMergulho> Members

        public virtual int CompareTo(PedidoServicoMergulho other)
        {
            if(other == null) return -1;
            return ID.CompareTo(other.ID);
        }

        #endregion

        public override string ToString()
        {
            return CodigoComAno;
        }

	  


	    
	}
}
