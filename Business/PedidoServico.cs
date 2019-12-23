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
	public partial class PedidoServico : BusinessObject<PedidoServico>, IPedido, IComparable<PedidoServico>	
	{
		#region Private Members
		private int _codigointerno; 
		private DateTime _dataemissao; 
		private Cliente _cliente; 
		private Cliente _clientepagador; 
		private string _codigopedidocliente; 
		private string _observacao;
        private string _numeroregistro;
        private string _numeroCFN;
        private string _contatos; 
		private string _telefonecontatos; 
		private Servidor _servidorgerente; 
		private bool _flagprogem; 
		private StatusPedidoServico _statuspedidoservico; 
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
		public PedidoServico()
		{
			_codigointerno = 0; 
			_dataemissao = DateTime.MinValue; 
			_cliente =  null; 
			_clientepagador =  null; 
			_codigopedidocliente = null; 
			_observacao = null;
            _numeroregistro = null;
            _numeroCFN = null;
            _contatos = null; 
			_telefonecontatos = null; 
			_servidorgerente =  null; 
			_flagprogem = false;
		    _statuspedidoservico = null;
			_celula =  null;
		    _flagRecusado = false;
		    _flagABordo = false;
		    _servidorCancelamento = null;
		    
		    _historico = new CustomList<HistoricoPedidoServico>();
		    _orcamentos = new CustomList<DelineamentoOrcamento>();
            _equipamentos = new CustomList<PedidoServicoEquipamento>();
            Rotinas = new CustomList<Rotina>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual string Localizacao { get; set; }
        public virtual Prioridade Prioridade { get; set; }
        public virtual Licitacao Licitacao { get; set; }
	    
        //public virtual Rotina Rotina { get; set; }

	    public virtual bool UsaRotina
	    {
	        get { return this.Rotinas.Count > 0; }
	    }
        
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
                if (value != null)
                    if (value.Length > 200)
                        throw new ArgumentOutOfRangeException("Invalid value for NumeroRegistro", value, value.ToString());

                _numeroregistro = value;
            }
        }
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string NumeroCFN
		{
			get { return _numeroCFN; }
			set	
			{
				if ( value != null )
					if( value.Length > 200)
						throw new ArgumentOutOfRangeException("Invalid value for NumeroCFN", value, value.ToString());
				
				_numeroCFN = value;
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
		public virtual Servidor ServidorGerente
		{
			get { return _servidorgerente; }
			set { _servidorgerente = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagProgem
		{
			get { return _flagprogem; }
			set { _flagprogem = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual StatusPedidoServico Status
		{
			get { return _statuspedidoservico; }
			set { _statuspedidoservico = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Celula Celula
		{
			get { return _celula; }
			set { _celula = value; }
		}

	    public virtual int ID_PedidoServico
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
	    public virtual HistoricoPedidoServico UltimoHistorico
	    {
	        get
	        {
	            if(Historico.Count > 0)
	                return Historico[Historico.Count - 1];
	            else
	                return null;
	        }
	    }

        public virtual string DescricaoEquipamentos
        {
            get
            {
                string s = "";
                foreach (PedidoServicoEquipamento pedidoServicoEquipamento in this.Equipamentos)
                {
                    s += "- " + pedidoServicoEquipamento.Equipamento.Descricao;
                    s += "<br>";
                }

                if (s != "")
                    s = s.Substring(0, s.Length - 4);

                return s;
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

	    protected internal virtual HistoricoPedidoServico GetUltimoHistorico(int id_delineamentoOrcamento)
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

        //public virtual DelineamentoOrcamento GetOrcamentoNaoEnviado()
        //{
        //    foreach (DelineamentoOrcamento orcamento in _orcamentos)
        //    {
        //        if (orcamento.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoOrcamento)
        //            return orcamento;
        //    }
        //    return null;
        //}

        public virtual string StatusUltimoOrcamento
        {
            get
            {
                if (_orcamentos.Count == 0)
                    return this.Status.Descricao;
                else
                    return _orcamentos[_orcamentos.Count - 1].Status.Descricao;
            }
        }
        #endregion
		
		#region Collections

	    public virtual ICustomList<Rotina> Rotinas { get; set; }

	    private ICustomList<HistoricoPedidoServico> _historico;
	    private ICustomList<DelineamentoOrcamento> _orcamentos;
        private ICustomList<PedidoServicoEquipamento> _equipamentos;

        public virtual ICustomList<PedidoServicoEquipamento> Equipamentos
        {
            get { return _equipamentos; }
            set { _equipamentos = value; }
        }

	    public virtual ICustomList<DelineamentoOrcamento> Orcamentos
	    {
	        get { return _orcamentos; }
	        set { _orcamentos = value; }
	    }

	    public virtual ICustomList<HistoricoPedidoServico> Historico
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
        //            if(orcamento.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoOrcamento)
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
			from PedidoServico p  
			where p.FlagAtivo = 1
			order by p.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}

        public static List<PedidoServico> Select(string texto, DateTime dataInicio, DateTime dataFim, int id_status, int id_gerente, int id_equipamento, 
            string numeroPedidoCliente, string numeroRegistro, int id_celula)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from PedidoServico p 
                    inner join fetch p.Cliente c                     
			where dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1
			and (dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			or c.Descricao like :texto)
			and p.Status.ID = IsNull(:id_status, p.Status.ID)
			and IsNull(p.ServidorGerente.ID, -1) = IsNull(:id_gerente, IsNull(p.ServidorGerente.ID, -1))
            and IsNull(p.Celula.ID, -1) = IsNull(:id_celula, IsNull(p.Celula.ID, -1))			
            and p.CodigoPedidoCliente like :codigoPedidoCliente
            and IsNull(p.NumeroRegistro, '') like :numeroRegistro 
			order by p.CodigoInterno");

            query.SetParameter("texto", string.Format("%{0}%", texto));
            query.SetParameter("codigoPedidoCliente", string.Format("%{0}%", numeroPedidoCliente));
            query.SetParameter("numeroRegistro", string.Format("%{0}%", numeroRegistro));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("id_gerente", BusinessHelper.IsNullOrZero(id_gerente), NHibernateUtil.Int32);
            //query.SetParameter("id_equipamento", BusinessHelper.IsNullOrZero(id_equipamento), NHibernateUtil.Int32);
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
			return (List<PedidoServico>)query.List<PedidoServico>();
		}

        public static List<PedidoServico> Select(string texto, DateTime dataInicio, DateTime dataFim, int id_cliente, int id_status, int id_equipamento)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from PedidoServico p 
                    inner join fetch p.Cliente c                     
                    inner join fetch p.Status s 	                    
			where dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1
			and (dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1)            
            and s.ID = IsNull(:id_status, s.ID)
			and c.ID = :id_cliente
			order by dbo.GetYear(p.DataEmissao) DESC, p.CodigoInterno DESC");

            query.SetParameter("texto", string.Format("%{0}%", texto));
            //query.SetParameter("id_equipamento", BusinessHelper.IsNullOrZero(id_equipamento), NHibernateUtil.Int32);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetInt32("id_cliente", id_cliente);
            return (List<PedidoServico>)query.List<PedidoServico>();
        }

        public static List<PedidoServico> SelectEditaveis(string texto, DateTime dataInicio, DateTime dataFim, int id_status, int id_gerente, int id_celula, int ano)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from PedidoServico p 
                inner join fetch p.Cliente c                 
                inner join fetch p.Status s
                left join fetch p.Celula cel
                left join fetch p.ServidorGerente sg
			where dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1
			and (dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			or c.Descricao like :texto)
			and s.ID = IsNull(:id_status, s.ID)	
            and dbo.GetYear(p.DataEmissao) = IsNull(:ano, dbo.GetYear(p.DataEmissao))
            and IsNull(cel.ID, -1) = IsNull(:id_celula, IsNull(cel.ID, -1))	
            and IsNull(sg.ID, -1) = IsNull(:id_gerente, IsNull(sg.ID, -1))
			order by p.CodigoInterno");

            query.SetParameter("texto", string.Format("%{0}%", texto));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_gerente", BusinessHelper.IsNullOrZero(id_gerente), NHibernateUtil.Int32);
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);

            return (List<PedidoServico>)query.List<PedidoServico>();
        }

        /// <summary>
        /// Seleciona os pedidos que estao pendentes da ação de um servidor
        /// </summary>
        public static List<IPedido> Select(int id_servidor, int id_status, string codigo, int id_oficinaDelineamento, int id_gerente)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();


            // Aguardando orçamento - Por Servidor
            IQuery query = session.CreateQuery(
            @"select distinct p from PedidoServico p inner join p.Status s inner join fetch p.Cliente c 
                inner join s.Responsaveis resp

			where 

                resp.ID = :id_servidor
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			and s.FlagVinculoPorDivisao = 0
			and s.FlagSomenteGerente = 0
			and s.ID < :statusAguardandoOrcamento
			and s.ID = IsNull(:status, s.ID)
            and s.ID != :statusCancelado
            and :id_oficina is null

			order by p.CodigoInterno");


            // Aguardando orçamento - Por Divisão
            IQuery query2 = session.CreateQuery(
            @"select distinct p from PedidoServico p inner join p.Status s inner join fetch p.Cliente c 
                inner join s.ResponsaveisDivisao divisao

			where 

                divisao.Servidor.ID = :id_servidor
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			and divisao.Celula.ID = p.Celula.ID
			and s.FlagVinculoPorDivisao = 1
			and s.FlagSomenteGerente = 0
			and s.ID < :statusAguardandoOrcamento
            and s.ID != :statusCancelado
			and s.ID = IsNull(:status, s.ID)
            and :id_oficina is null 

			order by p.CodigoInterno");


            // Aguardando orçamento - Gerente
            IQuery query3 = session.CreateQuery(
            @"select distinct p from PedidoServico p inner join p.Status s inner join fetch p.Cliente c 

			where 

                s.FlagSomenteGerente = 1
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			and p.ServidorGerente.ID = :id_servidor
			and s.ID < :statusAguardandoOrcamento
			and s.ID = IsNull(:status, s.ID)
            and s.ID != :statusCancelado
            and :id_oficina is null

			order by p.CodigoInterno");


            // Orçamento Finalizado - Por servidor Usando Parametros de Busca e Não for Cancelado
            IQuery query4 = session.CreateQuery(
            @"select distinct d from DelineamentoOrcamento d 
                inner join fetch d.PedidoServico p 
                inner join fetch d.Status s 
                inner join fetch p.Cliente c                 
                inner join s.Responsaveis resp
                left join d.ItensDelineamento del

			where 

                resp.ID = :id_servidor
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			and s.FlagVinculoPorDivisao = 0
			and s.FlagSomenteGerente = 0
			and s.ID >= :statusOrcamentoFinalizado
			and s.ID = IsNull(:status, s.ID)
            and s.ID != :statusCancelado
            and del.Celula.ID = IsNull(:id_oficina, del.Celula.ID)

			order by p.CodigoInterno");


            // Orçamento Finalizado - Por Divisão Usando Parametros de Busca e Não for Cancelado
            IQuery query5 = session.CreateQuery(
            @"select distinct d from DelineamentoOrcamento d 
                inner join fetch d.PedidoServico p 
                inner join fetch d.Status s 
                inner join fetch p.Cliente c                 
                inner join s.ResponsaveisDivisao divisao
                left join d.ItensDelineamento del

			where 

                divisao.Servidor.ID = :id_servidor
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			and divisao.Celula.ID = p.Celula.ID
			and s.FlagVinculoPorDivisao = 1
			and s.FlagSomenteGerente = 0
			and s.ID >= :statusOrcamentoFinalizado
			and s.ID = IsNull(:status, s.ID)
            and s.ID != :statusCancelado
            and del.Celula.ID = IsNull(:id_oficina, del.Celula.ID)

			order by p.CodigoInterno");


            // Orçamento Finalizado - Por Gerente Usando Parametros de Busca e Não for Cancelado
            IQuery query6 = session.CreateQuery(
            @"select distinct d from DelineamentoOrcamento d 
                inner join fetch d.PedidoServico p 
                inner join fetch d.Status s 
                inner join fetch p.Cliente c               
                left join d.ItensDelineamento del  

			where 

                s.FlagSomenteGerente = 1
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			and p.ServidorGerente.ID = :id_servidor
			and s.ID >= :statusOrcamentoFinalizado
			and s.ID = IsNull(:status, s.ID)
            and s.ID != :statusCancelado
            and del.Celula.ID = IsNull(:id_oficina, del.Celula.ID)

			order by p.CodigoInterno");


            //Fase Aguardando Delineamento: pesquisa apenas usando a tabela DelineamentoOficina
            IQuery query7 = session.CreateQuery(
           @"select distinct d from DelineamentoOrcamento d 
                inner join fetch d.PedidoServico p 
                inner join fetch d.Status s 
                inner join fetch p.Cliente c                 
                inner join d.Delineamentos delineamento

			where 

                delineamento.Servidor.ID = :id_servidor
            and delineamento.Enviado = 0
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1			
			and s.ID = :statusAguardandoDelineamento
            and :id_oficina is null

			order by p.CodigoInterno");


            //Fase Aguardando Cotacao: pesquisa apenas usando o ID_ServidorCotador
            IQuery query8 = session.CreateQuery(
           @"select distinct d from DelineamentoOrcamento d 
                inner join fetch d.PedidoServico p 
                inner join fetch d.Status s 
                inner join fetch p.Cliente c                 
                inner join fetch d.ServidorCotador sc
                left join d.ItensDelineamento del  

			where 

                sc.ID = :id_servidor            
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1			
			and s.ID = :statusAguardandoCotacao
            and del.Celula.ID = IsNull(:id_oficina, del.Celula.ID)

			order by p.CodigoInterno");




            // PARAMETROS
            query.SetInt32("id_servidor", id_servidor);
            query.SetInt32("statusAguardandoOrcamento", Convert.ToInt32(StatusPedidoServicoEnum.AguardandoOrcamento));
            query.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));
            query.SetParameter("status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("texto", string.Format("%{0}%", codigo));
            query.SetParameter("id_oficina", BusinessHelper.IsNullOrZero(id_oficinaDelineamento), NHibernateUtil.Int32);


            // PARAMETROS
            query2.SetInt32("id_servidor", id_servidor);
            query2.SetInt32("statusAguardandoOrcamento", Convert.ToInt32(StatusPedidoServicoEnum.AguardandoOrcamento));
            query2.SetParameter("status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query2.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));
            query2.SetParameter("texto", string.Format("%{0}%", codigo));
            query2.SetParameter("id_oficina", BusinessHelper.IsNullOrZero(id_oficinaDelineamento), NHibernateUtil.Int32);


            // PARAMETROS
            query3.SetInt32("id_servidor", id_servidor);
            query3.SetInt32("statusAguardandoOrcamento", Convert.ToInt32(StatusPedidoServicoEnum.AguardandoOrcamento));
            query3.SetParameter("status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query3.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));
            query3.SetParameter("texto", string.Format("%{0}%", codigo));
            query3.SetParameter("id_oficina", BusinessHelper.IsNullOrZero(id_oficinaDelineamento), NHibernateUtil.Int32);


            // PARAMETROS
            query4.SetInt32("id_servidor", id_servidor);
            query4.SetParameter("texto", string.Format("%{0}%", codigo));
            query4.SetInt32("statusOrcamentoFinalizado", Convert.ToInt32(StatusPedidoServicoEnum.AguardandoOrcamento));
            query4.SetParameter("status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query4.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));
            query4.SetParameter("id_oficina", BusinessHelper.IsNullOrZero(id_oficinaDelineamento), NHibernateUtil.Int32);


            // PARAMETROS
            query5.SetInt32("id_servidor", id_servidor);
            query5.SetParameter("texto", string.Format("%{0}%", codigo));
            query5.SetInt32("statusOrcamentoFinalizado", Convert.ToInt32(StatusPedidoServicoEnum.AguardandoOrcamento));
            query5.SetParameter("status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query5.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));
            query5.SetParameter("id_oficina", BusinessHelper.IsNullOrZero(id_oficinaDelineamento), NHibernateUtil.Int32);


            // PARAMETROS
            query6.SetInt32("id_servidor", id_servidor);
            query6.SetInt32("statusOrcamentoFinalizado", Convert.ToInt32(StatusPedidoServicoEnum.AguardandoOrcamento));
            query6.SetParameter("status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query6.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));
            query6.SetParameter("texto", string.Format("%{0}%", codigo));
            query6.SetParameter("id_oficina", BusinessHelper.IsNullOrZero(id_oficinaDelineamento), NHibernateUtil.Int32);


            // PARAMETROS
            query7.SetInt32("id_servidor", id_servidor);
            query7.SetInt32("statusAguardandoDelineamento", Convert.ToInt32(StatusPedidoServicoEnum.EmDelineamento));
            query7.SetParameter("texto", string.Format("%{0}%", codigo));
            query7.SetParameter("id_oficina", BusinessHelper.IsNullOrZero(id_oficinaDelineamento), NHibernateUtil.Int32);


            // PARAMETROS
            query8.SetInt32("id_servidor", id_servidor);
            query8.SetInt32("statusAguardandoCotacao", Convert.ToInt32(StatusPedidoServicoEnum.AguardandoOrcamento));
            query8.SetParameter("texto", string.Format("%{0}%", codigo));
            query8.SetParameter("id_oficina", BusinessHelper.IsNullOrZero(id_oficinaDelineamento), NHibernateUtil.Int32);


            // LIST
            List<PedidoServico> list1 = (List<PedidoServico>)query.List<PedidoServico>();
            List<PedidoServico> list2 = (List<PedidoServico>)query2.List<PedidoServico>();
            List<PedidoServico> list3 = (List<PedidoServico>)query3.List<PedidoServico>();
            list1.AddRange(list2);
            list1.AddRange(list3);


            // LIST
            List<DelineamentoOrcamento> list4 = (List<DelineamentoOrcamento>)query4.List<DelineamentoOrcamento>();
            List<DelineamentoOrcamento> list5 = (List<DelineamentoOrcamento>)query5.List<DelineamentoOrcamento>();
            List<DelineamentoOrcamento> list6 = (List<DelineamentoOrcamento>)query6.List<DelineamentoOrcamento>();
            List<DelineamentoOrcamento> list7 = (List<DelineamentoOrcamento>)query7.List<DelineamentoOrcamento>();
            List<DelineamentoOrcamento> list8 = (List<DelineamentoOrcamento>)query8.List<DelineamentoOrcamento>();
            list4.AddRange(list5);
            list4.AddRange(list6);
            list4.AddRange(list7);
            list4.AddRange(list8);


            // LIST
            List<IPedido> list = new List<IPedido>();
            foreach (PedidoServico servico in list1)
            {
                list.Add(servico);
            }
            foreach (DelineamentoOrcamento orcamento in list4)
            {
                list.Add(orcamento);
            }
            if(id_gerente > 0)
            {
                list = list.Where(l => l.ServidorGerente != null & l.ServidorGerente.ID == id_gerente).ToList();
            }


            // RETURN
            return list.OrderBy(p => p.CodigoInterno).ToList();
        }


        public static List<DelineamentoOrcamento> Select(int id_gerente, int id_equipamento, int id_status, int id_celula, bool? flagProgem, 
            DateTime dataInicio, DateTime dataFim, string numeroRegistro, string observacao, int id_cliente, int ano, string numeroPS, DateTime dataPrevisaoEntregaInicio, 
            DateTime dataPrevisaoEntregaFim, DateTime dataRealizadoInicio, DateTime dataRealizadoFim, int id_cotador, int id_oficinaDelineamento, string codigoPedidoCliente,
            int id_statusHistorico, DateTime dataHistoricoInicio, DateTime dataHistoricoFim, bool apenasRecusado)
        {
            string condicaoStatus = "";
            if (id_status == 0)
                condicaoStatus = "";
            else if (id_status == Int32.MinValue || id_status != Convert.ToInt32(StatusPedidoServicoEnum.Cancelado))
                condicaoStatus = string.Format("and s.ID NOT IN ({0})", Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));

            if(dataRealizadoInicio != DateTime.MinValue)
                condicaoStatus += string.Format(@" and exists(from HistoricoPedidoServico h where h.PedidoServico.ID = p.ID and h.StatusPosterior.ID = {0} 
                        and h.Data >= '{1}')", Convert.ToInt32(StatusPedidoServicoEnum.Finalizado), dataRealizadoInicio.ToString("yyyy-MM-dd"));


            if (dataRealizadoFim != DateTime.MinValue)
                condicaoStatus += string.Format(@" and exists( from HistoricoPedidoServico h where h.ID = p.ID and h.StatusPosterior.ID = {0} 
                        and h.Data <= '{1}')", Convert.ToInt32(StatusPedidoServicoEnum.Finalizado), dataRealizadoFim.ToString("yyyy-MM-dd"));

            if (dataHistoricoInicio != DateTime.MinValue && dataHistoricoFim != DateTime.MinValue)
            {
                string recusado = "";
                if (apenasRecusado)
                    recusado += " and h1.StatusAnterior.ID > h1.StatusPosterior.ID";
                condicaoStatus += string.Format(@" and exists(from HistoricoPedidoServico h1 where h1.PedidoServico.ID = p.ID and (h1.StatusPosterior.ID = {0} OR h1.StatusAnterior.ID = {0})
                        and h1.Data >= :dataHistoricoInicio and h1.Data <= :dataHistoricoFim {1})", id_statusHistorico, recusado);

                
            }

//            if (dataHistoricoFim != DateTime.MinValue)
//                condicaoStatus += string.Format(@" and exists( from HistoricoPedidoServico h2 where h2.ID = p.ID and (h2.StatusPosterior.ID = {0} OR h2.StatusAnterior.ID = {0})
//                        and h2.Data <= '{1}')", id_statusHistorico, dataHistoricoFim.ToString("yyyy-MM-dd"));

            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            string.Format(@"select distinct p from PedidoServico p 
                inner join p.Status s 
                inner join fetch p.Cliente c 
                inner join fetch p.ClientePagador cp                 
                left join fetch p.ServidorGerente sg
                left join fetch p.Celula cel     
                inner join fetch p.Equipamentos e
			where s.ID = IsNull(:id_status, s.ID)			
            and c.ID = IsNull(:id_cliente, c.ID)
			and dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1			
			and (s.ID <= :statusAguardandoOrcamento OR s.ID = :statusCancelado)
			and p.FlagProgem = IsNull(:progem, p.FlagProgem)
			and IsNull(sg.ID, -1) = IsNull(:id_gerente, IsNull(sg.ID, -1))
			and IsNull(cel.ID, -1) = IsNull(:id_celula, IsNull(cel.ID, -1))	
			and IsNull(p.NumeroRegistro, 'xxx') like IsNull(:numeroRegistro, IsNull(p.NumeroRegistro, 'xxx'))	
            and IsNull(p.CodigoPedidoCliente, 'xxx') like IsNull(:codigoPedidoCliente, IsNull(p.CodigoPedidoCliente, 'xxx'))	
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :numeroPS) = 1
			and IsNull(p.Observacao, 'xxx') like IsNull(:observacao, IsNull(p.Observacao, 'xxx'))	
            and dbo.GetYear(p.DataEmissao) = IsNull(:ano, dbo.GetYear(p.DataEmissao))
            and dbo.DateIsInBetween(p.PrevisaoEntrega, :dataPrevisaoEntregaInicio, :dataPrevisaoEntregaFim) = 1
            and :id_oficina is null
            and :id_servidorCotador is null
            {0}
            
			order by p.CodigoInterno", condicaoStatus));
            IQuery query2 = session.CreateQuery(
            string.Format(@"select distinct d from DelineamentoOrcamento d 
                inner join fetch d.PedidoServico p 
                inner join fetch d.Status s 
                inner join fetch p.Cliente c 
                inner join fetch p.ClientePagador cp                 
                inner join fetch p.ServidorGerente sg
                inner join fetch p.Celula cel                                
                left join fetch  d.ServidorCotador sc
                left join d.ItensDelineamento del
                inner join fetch p.Equipamentos e                
			where s.ID = IsNull(:id_status, s.ID)			
            and c.ID = IsNull(:id_cliente, c.ID)
			and sg.ID = IsNull(:id_gerente, sg.ID)
			and cel.ID = IsNull(:id_celula, cel.ID)			
            and ISNULL(sc.ID, -1) = IsNULL(IsNull(:id_servidorCotador, sc.ID), -1)
			and dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1				
			and s.ID > :statusOrcamentoFinalizado
			and p.FlagProgem = IsNull(:progem, p.FlagProgem)
			and IsNull(p.NumeroRegistro, 'xxx') like IsNull(:numeroRegistro, IsNull(p.NumeroRegistro, 'xxx'))	
            and IsNull(p.CodigoPedidoCliente, 'xxx') like IsNull(:codigoPedidoCliente, IsNull(p.CodigoPedidoCliente, 'xxx'))	
            and dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :numeroPS) = 1
			and IsNull(p.Observacao, 'xxx') like IsNull(:observacao, IsNull(p.Observacao, 'xxx'))	
            and dbo.GetYear(p.DataEmissao) = IsNull(:ano, dbo.GetYear(p.DataEmissao))
            and dbo.DateIsInBetween(p.PrevisaoEntrega, :dataPrevisaoEntregaInicio, :dataPrevisaoEntregaFim) = 1
            and ISNULL(del.Celula.ID, -1) = ISNULL(ISNULL(:id_oficina, del.Celula.ID), -1)
            {0}
           
			order by p.CodigoInterno", condicaoStatus));
           

            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
          // query.SetParameter("id_equipamento", BusinessHelper.IsNullOrZero(id_equipamento), NHibernateUtil.Int32);
            query.SetParameter("id_cliente", BusinessHelper.IsNullOrZero(id_cliente), NHibernateUtil.Int32);
            query.SetInt32("statusAguardandoOrcamento", Convert.ToInt32(StatusPedidoServicoEnum.EmDelineamento));
            query.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("progem", BusinessHelper.IsNull(flagProgem), NHibernateUtil.Boolean);
            query.SetParameter("id_gerente", BusinessHelper.IsNullOrZero(id_gerente), NHibernateUtil.Int32);
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            query.SetString("numeroRegistro", string.Format("%{0}%", numeroRegistro));
            query.SetString("codigoPedidoCliente", string.Format("%{0}%", codigoPedidoCliente));
            query.SetString("numeroPS", string.Format("%{0}%", numeroPS));
            query.SetString("observacao", string.Format("%{0}%", observacao));
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            query.SetParameter("dataPrevisaoEntregaInicio", BusinessHelper.IsNull(dataPrevisaoEntregaInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataPrevisaoEntregaFim", BusinessHelper.IsNull(dataPrevisaoEntregaFim), NHibernateUtil.DateTime);
            query.SetParameter("id_oficina", BusinessHelper.IsNullOrZero(id_oficinaDelineamento), NHibernateUtil.Int32);
            query.SetParameter("id_servidorCotador", BusinessHelper.IsNullOrZero(id_cotador), NHibernateUtil.Int32);

            if (dataHistoricoInicio != DateTime.MinValue && dataHistoricoFim != DateTime.MinValue)
            {
                query.SetDateTime("dataHistoricoInicio", dataHistoricoInicio);
                query.SetDateTime("dataHistoricoFim", dataHistoricoFim);
            }


            query2.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
           // query2.SetParameter("id_equipamento", BusinessHelper.IsNullOrZero(id_equipamento), NHibernateUtil.Int32);
            query2.SetParameter("id_cliente", BusinessHelper.IsNullOrZero(id_cliente), NHibernateUtil.Int32);
            query2.SetInt32("statusOrcamentoFinalizado", Convert.ToInt32(StatusPedidoServicoEnum.EmDelineamento));
            query2.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query2.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query2.SetParameter("id_gerente", BusinessHelper.IsNullOrZero(id_gerente), NHibernateUtil.Int32);
            query2.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            query2.SetParameter("progem", BusinessHelper.IsNull(flagProgem), NHibernateUtil.Boolean);
            query2.SetString("numeroRegistro", string.Format("%{0}%", numeroRegistro));
            query2.SetString("codigoPedidoCliente", string.Format("%{0}%", codigoPedidoCliente));
            query2.SetString("numeroPS", string.Format("%{0}%", numeroPS));
            query2.SetString("observacao", string.Format("%{0}%", observacao));
            query2.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            query2.SetParameter("dataPrevisaoEntregaInicio", BusinessHelper.IsNull(dataPrevisaoEntregaInicio), NHibernateUtil.DateTime);
            query2.SetParameter("dataPrevisaoEntregaFim", BusinessHelper.IsNull(dataPrevisaoEntregaFim), NHibernateUtil.DateTime);
            query2.SetParameter("id_servidorCotador", BusinessHelper.IsNullOrZero(id_cotador), NHibernateUtil.Int32);
            query2.SetParameter("id_oficina", BusinessHelper.IsNullOrZero(id_oficinaDelineamento), NHibernateUtil.Int32);
            if (dataHistoricoInicio != DateTime.MinValue && dataHistoricoFim != DateTime.MinValue)
            {
                query2.SetDateTime("dataHistoricoInicio", dataHistoricoInicio);
                query2.SetDateTime("dataHistoricoFim", dataHistoricoFim);
            }

            List<PedidoServico> pedidos = (List<PedidoServico>)query.List<PedidoServico>();
            List<DelineamentoOrcamento> orcamentos = (List<DelineamentoOrcamento>)query2.List<DelineamentoOrcamento>();
            
            foreach (PedidoServico ps in pedidos)
            {
                DelineamentoOrcamento orcamento = new DelineamentoOrcamento();
                orcamento.PedidoServico = ps;
                orcamentos.Add(orcamento);
            }

            orcamentos.Sort(new Comparison<DelineamentoOrcamento>(delegate(DelineamentoOrcamento p1, DelineamentoOrcamento p2)
            {
                return p1.CodigoInterno.CompareTo(p2.PedidoServico.CodigoInterno);
            }));

            if(id_equipamento != 0)
            {
                return orcamentos.Distinct().Where(o => o.PedidoServico.Equipamentos.Where(e => e.Equipamento.ID == id_equipamento).Count() > 0).ToList();
            }

            return orcamentos.Distinct().ToList();
        }
        
        /// <summary>
        /// Retorna a lista de pedido onde a etapa id_statusPedidoServico demorou mais que x dias
        /// </summary>
        public static List<IPedido> Select(int id_gerente, int id_equipamento, int id_status, int id_celula, bool? flagProgem, 
            DateTime dataInicio, DateTime dataFim, int id_statusPedidoServico, int dias)
        {
            List<DelineamentoOrcamento> list = Select(id_gerente, id_equipamento, id_status, id_celula, flagProgem, dataInicio, dataFim, null, null, Int32.MinValue, Int32.MinValue, "", DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, 0, 0, null, 0, DateTime.MinValue, DateTime.MinValue, false);
            foreach (IPedido pedido in list)
            {
                //TODO: inserir logica aqui
            }

            return list.ConvertAll<IPedido>(delegate(DelineamentoOrcamento orcamento) { return (IPedido) orcamento; });
        }

	    #endregion

        #region Relatorios CrossTable

        public static DataSet SelectStatusPorCelula(int ano, bool? progem)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[3];
            param[1] = ano;
            param[2] = progem;

            return helper.ExecuteDataSet("PedidoServico_SelectStatusPorCelula", param);
        }

        public static DataSet SelectStatusPorEquipamento(int ano, bool? progem, int id_equipamento)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[4];
            param[1] = ano;
            param[2] = progem;
            param[3] = NullHelper.IsZero(id_equipamento);

            return helper.ExecuteDataSet("PedidoServico_SelectStatusPorEquipamento", param);
        }

        public static DataSet SelectHistoricoStatus(int ano, bool? progem, int id_tipoCliente, int id_cliente, int id_celula)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[6];
            param[1] = ano;
            param[2] = progem;
            param[3] = NullHelper.IsZero(id_tipoCliente);
            param[4] = NullHelper.IsZero(id_cliente);
            param[5] = NullHelper.IsZero(id_celula);

            return helper.ExecuteDataSet("PedidoServico_SelectHistoricoStatus", param);
        }

        public static DataSet SelectProntificacaoEquipamento(int ano, bool? progem, int id_tipoCliente, int id_cliente, int id_celula)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[6];
            param[1] = ano;
            param[2] = progem;
            param[3] = NullHelper.IsZero(id_tipoCliente);
            param[4] = NullHelper.IsZero(id_cliente);
            param[5] = NullHelper.IsZero(id_celula);

            return helper.ExecuteDataSet("PedidoServico_ProntificacaoEquipamento", param);
        }

        public static DataSet SelectCustoRealizado(int id_gerente, int id_equipamento, int id_status, int id_celula, bool? flagProgem,
            DateTime dataInicio, DateTime dataFim, string numeroRegistro, string observacao, int id_cliente, string numeroPS, int mes, int ano)
        {
            SQLHelper helper = new SQLHelper();
            helper.CmdTimeout = 500;
            object[] param = new object[14];
            param[1] = NullHelper.IsZero(id_gerente);
            param[2] = NullHelper.IsZero(id_status);
            param[3] = NullHelper.IsZero(id_equipamento);
            param[4] = NullHelper.IsZero(id_celula);
            param[5] = NullHelper.IsZero(id_cliente);
            param[6] = NullHelper.IsNull(flagProgem);
            param[7] = NullHelper.IsNull(dataInicio);
            param[8] = NullHelper.IsNull(dataFim);
            param[9] = NullHelper.IsNull(numeroRegistro);
            param[10] = NullHelper.IsNull(numeroPS);
            param[11] = NullHelper.IsNull(observacao);
            param[12] = NullHelper.IsZero(mes);
            param[13] = NullHelper.IsZero(ano);

            return helper.ExecuteDataSet("PedidoServico_SelectCustoRealizado", param);
        }

        public static DataSet SelectCustoRealizadoAC(int id_ac)
        {
            SQLHelper helper = new SQLHelper();
            helper.CmdTimeout = 500;
            object[] param = new object[2];
            param[1] = NullHelper.IsZero(id_ac);
            return helper.ExecuteDataSet("PedidoServico_SelectCustoRealizadoAC", param);
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
	            @"from PedidoServico p 
	            where p.CodigoPedidoCliente = :codigo
	            and p.Status.ID != :statusCancelado
	            and p.Cliente.ID = :id_cliente");
	        query1.SetString("codigo", this.CodigoPedidoCliente);
	        query1.SetInt32("id_cliente", this.Cliente.ID);
            query1.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));
            if(query1.List<PedidoServico>().Count > 0)
                throw new Exception("Já existe um PS com este número de pedido do cliente.");
	        
	        //this.DataEmissao = DateTime.Today;
	        this.Status = StatusPedidoServico.Get(StatusPedidoServicoEnum.NaoEnviado);
	        
	        using (TransactionBlock tran = new TransactionBlock())
	        {
                
                IQuery query = session.CreateQuery(
                    @"select MAX(p.CodigoInterno) from PedidoServico p where dbo.GetYear(p.DataEmissao) = :ano");
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

        #region Metodos Ok para o BAC
        
        public virtual void Enviar(int id_servidor)
	    {
	        if(this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.NaoEnviado)
	            throw new Exception("Este pedido não pode mais ser enviado.");

            if (this.Equipamentos.Count == 0)
                throw new Exception("Insira pelo menos um equipamento.");

            using (TransactionBlock tran = new TransactionBlock())
            {

                //Cria orcamento se eh a primeira vez que o PS eh enviado
                //if(!_flagRecusado && _orcamentos.Count == 0)
                {
                    DelineamentoOrcamento orcamento = new DelineamentoOrcamento();
                    orcamento.CriarNovoOrcamento(this, this.ServidorGerente, this.Rotinas.ToList());
                }

                HistoricoPedidoServico historico;
                if (this.UsaRotina)
                {
                    historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.OrcamentoFinalizado);
                }
                else
                    historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoDAC);

                historico.Save();
                this.Status = historico.StatusPosterior;
                _flagRecusado = false;
                base.Save();
                this._historico.Add(historico);
                tran.IsValid = true;
            }  
	    }

        public virtual void Registrar(int id_servidor)
        {
            HistoricoPedidoServico historico;

            if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoDAC)
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoEnvioParaDelineamento);
            }

            else if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEnvioParaDelineamento)
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoEnvioParaDelineamentoAprovar);
            }

            else if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEnvioParaDelineamentoAprovar)
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.EmDelineamento);
            }

            else
                throw new Exception("Status inexperado.");

            using (TransactionBlock tran = new TransactionBlock())
            {
                historico.Save();

                this.Status = historico.StatusPosterior;
                _flagRecusado = false;

                base.Save();

                this.Historico.Add(historico);
                tran.IsValid = true;
            }
        }

        public virtual void Reativar(int id_servidor, bool deletarOrcamentos)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {

                if(deletarOrcamentos)
                {
                    foreach (DelineamentoOrcamento delineamentoOrcamento in Orcamentos)
                    {
                        //delineamentoOrcamento.Status = StatusPedidoServico.Get(StatusPedidoServicoEnum.AguardandoTriagem);
                        delineamentoOrcamento.DeleteOrcamento();
                    }    
                }

                HistoricoPedidoServico historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.NaoEnviado);

                historico.Save();
                this.Status = historico.StatusPosterior;
                _flagRecusado = false;

                ISession session = NHibernateSessionManager.Instance.GetSession();
                IQuery query = session.CreateQuery(
                   @"select MAX(p.CodigoInterno) from PedidoServico p where dbo.GetYear(p.DataEmissao) = :ano");
                query.SetInt32("ano", DateTime.Today.Year);

                object result = query.UniqueResult();
                if (result == null)
                    this.CodigoInterno = 1;
                else
                    this.CodigoInterno = Convert.ToInt32(result) + 1;

                this.DataEmissao = DateTime.Today;

                base.Save();
                this._historico.Add(historico);
                tran.IsValid = true;
            }
        }
        #endregion
	    
      
	    private HistoricoPedidoServico GetHistorico(int id_servidor, StatusPedidoServicoEnum novoStatus)
	    {
	        HistoricoPedidoServico historico = new HistoricoPedidoServico();
	        historico.Data = DateTime.Now;
	        historico.PedidoServico = this;
	        historico.StatusAnterior = this.Status;
	        historico.Servidor = Servidor.Get(id_servidor);
	        historico.StatusPosterior = Business.StatusPedidoServico.Get(novoStatus);
	        return historico;
	    }
	    
	    /// <summary>
	    /// Retorna para o status anterior
	    /// </summary>
	    public virtual void Recusar(int id_servidor, string  justificativa)
	    {
	        StatusPedidoServicoEnum novoStatus = StatusPedidoServico.GetAnterior(_statuspedidoservico.StatusPedidoServicoEnum);
	        HistoricoPedidoServico historico = GetHistorico(id_servidor, novoStatus);
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
	    #endregion

//        public static IEnumerable FastSearch(string texto)
//        {
//            ISession session = NHibernateSessionManager.Instance.GetSession();
//            IQuery query = session.CreateQuery(
//            @"select distinct new PedidoServicoUI(p.ID, p.CodigoInterno, p.DataEmissao)
//            from PedidoServico p left join p.Orcamentos o
//			where dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1            
//			order by p.ID ASC");

//            query.SetMaxResults(20);
//            query.SetString("texto", "%" + texto + "%");
//            return query.List<PedidoServicoUI>();	        
//        }

          //Filtro por status
        public static IEnumerable FastSearch(string texto)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct new PedidoServicoUI(p.ID, p.CodigoInterno, p.DataEmissao)
            from PedidoServico p left join p.Orcamentos o
			where dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
            and ISNULL(o.Status.ID, 0) NOT IN (:status)
			order by p.ID ASC");

            query.SetMaxResults(20);
            query.SetString("texto", "%" + texto + "%");

            List<int> status = new List<int>(2);
            status.Add(Convert.ToInt32(StatusPedidoServicoEnum.Finalizado));
            status.Add(Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));
            query.SetParameterList("status", status, NHibernateUtil.Int32);
            return query.List<PedidoServicoUI>();
        }


       


        #region IComparable<PedidoServico> Members

        public virtual int CompareTo(PedidoServico other)
        {
            if(other == null) return -1;
            return ID.CompareTo(other.ID);
        }

        #endregion

        public override string ToString()
        {
            return CodigoComAno;
        }

	    public virtual void Cancelar(int id_servidor, int id_motivo, string comentario)
	    {
            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServico historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.Cancelado);
                historico.JustificativaRecusa = comentario;
                historico.Save();
                
                foreach (DelineamentoOrcamento orcamento in _orcamentos)
                {
                    if(orcamento.PedidoObtencao != null && orcamento.PedidoObtencao.Status.StatusPedidoObtencaoEnum != StatusPedidoObtencaoEnum.Cancelado)
                    {
                        //orcamento.PedidoObtencao.Cancelar(id_servidor, id_motivo);
                        throw new Exception(string.Format("Existe um PO ({0}) vinculado a este PS. Cancele o PO antes de cancelar o PS.", orcamento.PedidoObtencao.CodigoComAno));
                    }
                        

                    orcamento.Status = historico.StatusPosterior;
                    orcamento.Save();
                }

                this.Status = historico.StatusPosterior;
                this.MotivoCancelamento = MotivoCancelamento.Get(id_motivo);
                _flagRecusado = false;
                base.Save();

                this._historico.Add(historico);

                tran.IsValid = true;
            }   
	    }
	    
	    private DelineamentoOrcamento GetOrcamento(int id_orcamento)
	    {
	        foreach (DelineamentoOrcamento orcamento in _orcamentos)
	        {
	            if(orcamento.ID == id_orcamento)
	                return orcamento;
	        }
	        return null;
	    }
	    
	    public virtual List<Etapa> GetEtapas()
	    {
	        return GetEtapas(-1);
	    }
	    
	    protected internal virtual List<Etapa> GetEtapas(int id_delineamentoOrcamento)
	    {
	        List<Etapa> etapas = new List<Etapa>();
	        for(int i = 0; i < Historico.Count; i++)
	        {
	            if(id_delineamentoOrcamento <= 0 || 
	                Historico[i].ID_DelineamentoOrcamento == null ||
	                Historico[i].ID_DelineamentoOrcamento == id_delineamentoOrcamento )
	            {
	                Etapa etapa = new Etapa();
	                etapa.Tipo = "PS";
	                etapa.ID_PedidoServico = ID;
	                etapa.Data = Historico[i].Data;
	                etapa.Texto = Historico[i].Texto;
	                if (i + 1 < Historico.Count)
	                    etapa.DiasDemora = (Historico[i + 1].Data - etapa.Data).Days;
	                etapas.Add(etapa);
	            }
	        }

	        DelineamentoOrcamento orcamento = GetOrcamento(id_delineamentoOrcamento);
	        if(orcamento != null && orcamento.PedidoObtencao != null)
	        {
                for (int i = 0; i < orcamento.PedidoObtencao.Historico.Count; i++)
                {
                    Etapa etapa = new Etapa();
                    etapa.ID_PedidoServico = ID;
                    etapa.Tipo = "PO";
                    etapa.Data = orcamento.PedidoObtencao.Historico[i].Data;
                    etapa.Texto = orcamento.PedidoObtencao.Historico[i].Descricao;
                    if (i + 1 < orcamento.PedidoObtencao.Historico.Count)
                        etapa.DiasDemora = (orcamento.PedidoObtencao.Historico[i + 1].Data - etapa.Data).Days;
                    etapas.Add(etapa);
                    
                    
                }
	        }
	        return etapas;
	    }

	    public virtual string GetPOsAssociados()
	    {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select po from PedidoObtencao po 
                inner join po.DelineamentoOrcamento o 
            where o.PedidoServico.ID = :id	            
			order by po.ID ASC");

	        query.SetInt32("id", this.ID);
	        IList<PedidoObtencao> pos = query.List<PedidoObtencao>();

	        string result = "";
	        foreach (PedidoObtencao po in pos)
	        {
	            result += po.CodigoComAno + "  ";
	        }
	        return result;
	    }

        public virtual IList<PedidoObtencao> SelectPOsAssociados()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select po from PedidoObtencao po 
                inner join po.DelineamentoOrcamento o 
            where o.PedidoServico.ID = :id	            
			order by po.ID ASC");

            query.SetInt32("id", this.ID);
            return query.List<PedidoObtencao>();
        }

	    public static void RemoveLicitacao(int id_licitacao)
	    {
            foreach (PedidoServico ps in SelectByLicitacao(id_licitacao))
            {
                ps.Licitacao = null;
                ps.Save();
            }
	    }

        public static List<PedidoServico> SelectByLicitacao(int id_licitacao)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from PedidoServico ps inner join fetch ps.Licitacao l                 
            where l.ID = :id");

            query.SetInt32("id", id_licitacao);
            return (List<PedidoServico>) query.List<PedidoServico>();
        }
	}
}
