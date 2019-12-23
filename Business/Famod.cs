using System;
using System.Collections.Generic;
using System.Data;
using Marinha.Business.UI;
using NHibernate;
using Shared.Common;
using Shared.DataAccessHelper;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class Famod : BusinessObject<Famod>	
	{
		#region Private Members
		private Atividade _atividade; 
		private PedidoServico _pedidoservico; 
		private DateTime _data; 
		private int _horasapropriadas; 
		private Celula _oficina; 
		private Servidor _servidor; 
		private string _descricaodetalhada; 
		private Servidor _servidorcadastro;
	    private SituacaoFAMOD _situacaoFamod;
	    private decimal _valorCustoHH;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Famod()
		{
			_atividade =  null; 
			_pedidoservico =  null; 
			_data = DateTime.MinValue; 
			_horasapropriadas = 0; 
			_oficina =  null; 
			_servidor =  null; 
			_descricaodetalhada = null; 
			_servidorcadastro =  null;
		    _situacaoFamod = Business.SituacaoFAMOD.EmAberto;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual PedidoServicoMergulho PedidoServicoMergulho { get; set; }

        public virtual decimal ValorCustoHH
        {
            get { return _valorCustoHH; }
            set { _valorCustoHH = value; }
        }

        public virtual SituacaoFAMOD SituacaoFAMOD
        {
            get { return _situacaoFamod; }
            set { _situacaoFamod = value; }
        }
        
		public virtual Atividade Atividade
		{
			get { return _atividade; }
			set { _atividade = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual PedidoServico PedidoServico
		{
			get { return _pedidoservico; }
			set { _pedidoservico = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime Data
		{
			get { return _data; }
			set { _data = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual int HorasApropriadas
		{
			get { return _horasapropriadas; }
			set { _horasapropriadas = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Celula Oficina
		{
			get { return _oficina; }
			set { _oficina = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Servidor Servidor
		{
			get { return _servidor; }
			set { _servidor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string DescricaoDetalhada
		{
			get { return _descricaodetalhada; }
			set	
			{
				if ( value != null )
					if( value.Length > 500)
						throw new ArgumentOutOfRangeException("Invalid value for DescricaoDetalhada", value, value.ToString());
				
				_descricaodetalhada = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Servidor ServidorCadastro
		{
			get { return _servidorcadastro; }
			set { _servidorcadastro = value; }
		}

        public virtual string CodigoPSPSM
        {
            get
            {
                if (_pedidoservico != null)
                    return "PS " + _pedidoservico.CodigoComAno;
                else if(this.PedidoServicoMergulho != null)
                    return "PSM " + this.PedidoServicoMergulho.CodigoComAno;
                return "";
            }
            
        }

			#endregion 
		
		#region Public Methods
		
		public static List<Famod> Select(Celula divisao, int id_servidor, DateTime dataInicio, DateTime dataFim)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from Famod f inner join fetch f.Servidor s left join fetch f.PedidoServico p
			where dbo.DateIsInBetween(f.Data, :dataInicio, :dataFim) = 1
			and s.ID = IsNull(:id_servidor, s.ID)
			and s.Celula.Codigo like :codigoDivisao  			
			order by f.Data DESC");

		    query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_servidor", BusinessHelper.IsNullOrZero(id_servidor), NHibernateUtil.Int32);
		    query.SetString("codigoDivisao", divisao == null ? "%%" : divisao.Codigo + "%");
			return (List<Famod>)query.List<Famod>();
		}

        public static List<Famod> Select(int id_celula, int id_servidor, DateTime data)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from Famod f inner join fetch f.Servidor s left join fetch f.PedidoServico p
			where f.Data = :data
			and s.ID = IsNull(:id_servidor, s.ID)
			and s.Celula.ID = :id_celula
			order by f.Data DESC");

            query.SetDateTime("data", data);
            query.SetParameter("id_servidor", BusinessHelper.IsNullOrZero(id_servidor), NHibernateUtil.Int32);
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            
            return (List<Famod>)query.List<Famod>();
        }
		
		public static int GetHorasDia(DateTime data, int id_servidor, int id_famod)
		{
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select SUM(f.HorasApropriadas) from Famod f 
			where f.Data = :data
			and f.Servidor.ID = :id_servidor
			and f.ID != :id_famod");

            query.SetDateTime("data", data);
            query.SetInt32("id_servidor", id_servidor);
            query.SetInt32("id_famod", id_famod);
            
            object result = query.UniqueResult();
            if(result == null)
                return 0;
            else
                return Convert.ToInt32(result);
		}
		#endregion
		
		#region Select Agrupado
        public static List<FamodOficina> SelectAgrupado(int id_celula, int id_servidor, DateTime dataInicio, DateTime dataFim, 
                int id_situacao)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from Famod f 
                inner join fetch f.Servidor s 
                inner join fetch f.Oficina c
                left join fetch f.PedidoServico p
			where dbo.DateIsInBetween(f.Data, :dataInicio, :dataFim) = 1
			and s.ID = IsNull(:id_servidor, s.ID)			
            and f.SituacaoFAMOD = IsNull(:id_situacao, f.SituacaoFAMOD)
			and dbo.CelulaPertenceACelula(c.Codigo, :codigoCelula) = 1
			order by f.Data DESC");

            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_servidor", BusinessHelper.IsNullOrZero(id_servidor), NHibernateUtil.Int32);
            query.SetParameter("id_situacao", BusinessHelper.IsNullOrZero(id_situacao), NHibernateUtil.Int32);

            Celula celula = Celula.Get(id_celula);
            query.SetString("codigoCelula", celula.Codigo);

            IList<Famod> list = query.List<Famod>();
            
            return RetornaAgrupado(list);
        }

        public static List<FamodOficina> SelectAgrupadoParaAprovacao(int id_responsavelAprovacao, int id_oficina)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select f from EncarregadoCelula ec, Famod f 
                inner join fetch f.Servidor s 
                inner join fetch f.Oficina c
                left join fetch f.PedidoServico p
			where f.SituacaoFAMOD = :situacaoEmAberto
			and c.ID = ec.Celula.ID
			and ec.Servidor.ID = :id_responsavel
            and c.ID = ISNull(:id_oficina, c.ID)
			order by f.Data DESC");

            query.SetInt32("id_responsavel", id_responsavelAprovacao);
            query.SetParameter("id_oficina", BusinessHelper.IsNullOrZero(id_oficina), NHibernateUtil.Int32);
            query.SetInt32("situacaoEmAberto", Convert.ToInt32(Business.SituacaoFAMOD.EmAberto));

            IList<Famod> list = query.List<Famod>();

            return RetornaAgrupado(list);
        }

	    private static List<FamodOficina> RetornaAgrupado(IList<Famod> list)
	    {
            //Agrupa as famods for servidor, oficina e data
	        List<FamodServidor> servidores = new List<FamodServidor>();
	        foreach (Famod famod in list)
	        {
	            FamodServidor servidor = servidores.Find(delegate(FamodServidor match)
	                                                         {
	                                                             return
	                                                                 match.Servidor.ID == famod.Servidor.ID &&
	                                                                 match.Oficina.ID == famod.Oficina.ID &&
	                                                                 match.Data == famod.Data;
	                                                         });
                    
	            if(servidor == null)
	            {
	                servidor = new FamodServidor(famod.Servidor, famod.Oficina, famod.Data);
	                servidor.Famods.Add(famod);
	                servidores.Add(servidor);
	            }
	            else
	                servidor.Famods.Add(famod);
	        }

	        //Agrupa as famods for oficina e data
	        List<FamodOficina> oficinas = new List<FamodOficina>();
	        foreach (FamodServidor servidor in servidores)
	        {
	            FamodOficina oficina = oficinas.Find(delegate(FamodOficina match)
	                                                     {
	                                                         return
	                                                             match.Oficina.ID == servidor.Oficina.ID &&
	                                                             match.Data == servidor.Data;
	                                                     });
	            if(oficina == null)
	            {
	                oficina = new FamodOficina(servidor.Oficina, servidor.Data);
	                oficina.Servidores.Add(servidor);
	                oficinas.Add(oficina);
	            }
	            else
	                oficina.Servidores.Add(servidor);
	        }

	        return oficinas;
	    }

	    #endregion

        #region Relatorios CrossTable

        public static DataSet SelectHorasPorApropriacao(int id_servidor, int id_atividade, int id_apropriacao, DateTime dataInicio, DateTime dataFim)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[6];
            param[1] = NullHelper.IsNull(dataInicio);
            param[2] = NullHelper.IsNull(dataFim);
            param[3] = NullHelper.IsZero(id_apropriacao);
            param[4] = NullHelper.IsZero(id_atividade);
            param[5] = NullHelper.IsZero(id_servidor);

            helper.CmdTimeout = 3600;

            return helper.ExecuteDataSet("Famod_SelectHorasCelulaPorApropriacao", param);
        }

        public static DataSet SelectFamodPorCelula(int id_servidor, int id_atividade, int id_apropriacao, int id_divisao, DateTime dataInicio, DateTime dataFim)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[7];
            param[1] = NullHelper.IsZero(id_atividade);
            param[2] = NullHelper.IsZero(id_divisao);
            param[3] = NullHelper.IsZero(id_servidor);
            param[4] = NullHelper.IsZero(id_apropriacao);
            param[5] = NullHelper.IsNull(dataInicio);
            param[6] = NullHelper.IsNull(dataFim);

            helper.CmdTimeout = 3600;

            return helper.ExecuteDataSet("Famod_SelectFamodPorCelula", param);
        }

        public static DataSet SelectFamodPorServico(int id_cliente, int id_categoriaServico, int id_equipamento, DateTime dataInicio, DateTime dataFim, int id_divisao)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[7];
            param[1] = NullHelper.IsZero(id_cliente);
            param[2] = NullHelper.IsZero(id_categoriaServico);
            param[3] = NullHelper.IsZero(id_equipamento);
            param[4] = NullHelper.IsNull(dataInicio);
            param[5] = NullHelper.IsNull(dataFim);
            param[6] = NullHelper.IsZero(id_divisao);

            helper.CmdTimeout = 3600;
            
            return helper.ExecuteDataSet("Famod_SelectPorServico", param);
        }


        #endregion

        public override void Save()
        {
            Validar();
            base.Save();
        }

	    private void Validar()
	    {
	        Parametro parametro = Parametro.Get();
	        if(GetHorasDia(_data, _servidor.ID, ID) + _horasapropriadas > parametro.MaximoHorasFAMOD)
	            throw new Exception("A soma das horas apropriadas para esta data é maior que o limite permitido.");
	        
	        if(_data.DayOfWeek == DayOfWeek.Saturday || _data.DayOfWeek == DayOfWeek.Sunday)
                throw new Exception("Não é permitido lançar horas em finais de semana.");

	        Feriado feriado = Feriado.Get(_data);
	        if(feriado != null && !feriado.FlagMeioExpediente)
                throw new Exception("Não é permitido lançar horas em feriados.");

            if (feriado != null && feriado.FlagMeioExpediente && 
                (GetHorasDia(_data, _servidor.ID, ID) + _horasapropriadas) > parametro.MaximoHorasMeioExpedienteFAMOD)
                throw new Exception("A soma das horas apropriadas para esta data é maior que o limite permitido(meio expediente).");
                
            if(_atividade.FlagAtividadeDireta && _pedidoservico == null && PedidoServicoMergulho == null)
                throw new Exception("Informe o Pedido de Serviço.");

            if(_pedidoservico != null && _pedidoservico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.Finalizado)
                throw new Exception("Não é permitido lançar horas para um PS que já foi finalizado.");
	           
	    }

	    public static DataSet SelectPorOficina(int id_atividade, bool? flagAtividadeDireta, int id_oficina, int id_servidor, DateTime dataInicio, 
            DateTime dataFim, int id_situacao)
	    {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[8];
            param[1] = NullHelper.IsZero(id_atividade);
            param[2] = NullHelper.IsNull(flagAtividadeDireta);
            param[3] = NullHelper.IsZero(id_oficina);
            param[4] = NullHelper.IsZero(id_servidor);
            param[5] = NullHelper.IsNull(dataInicio);
            param[6] = NullHelper.IsNull(dataFim);
            param[7] = NullHelper.IsZero(id_situacao);

            return helper.ExecuteDataSet("Famod_SelectPorOficina", param);
	    }
	}
}

namespace Marinha.Business.UI
{
    public class FamodOficina
    {
        private List<FamodServidor> _servidores;
        private Celula _oficina;
        private DateTime _data;

        public virtual DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }
        public virtual Celula Oficina
        {
            get { return _oficina; }
            set { _oficina = value; }
        }
        
        
        public virtual List<FamodServidor> Servidores
        {
            get { return _servidores; }
            set { _servidores = value; }
        }

        public int TotalHoras
        {
            get
            {
                int total = 0;
                foreach (FamodServidor servidor in _servidores)
                {
                    total += servidor.TotalHoras;
                }
                return total;
            }
        }

        public FamodOficina(Celula oficina, DateTime data)
        {
            _oficina = oficina;
            _data = data;
            _servidores = new List<FamodServidor>();
        }
        
    }
    
    public class FamodServidor
    {
        private List<Famod> _famods;
        private Servidor _servidor;
        private Celula _oficina;
        private DateTime _data;

        public virtual DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }
        public virtual Celula Oficina
        {
            get { return _oficina; }
            set { _oficina = value; }
        }
        public Servidor Servidor
        {
            get { return _servidor; }
            set { _servidor = value; }
        }
        public List<Famod> Famods
        {
            get { return _famods; }
            set { _famods = value; }
        }

        public FamodServidor(Servidor servidor, Celula oficina, DateTime data)
        {
            _servidor = servidor;
            _oficina = oficina;
            _data = data;
            _famods = new List<Famod>();
        }
        
        public bool Aprovado
        {
            get
            {
                if(_famods.Count == 0) return false;
                foreach (Famod famod in _famods)
                {
                    if(famod.SituacaoFAMOD == SituacaoFAMOD.EmAberto)
                        return false;
                }
                return true;
            }
        }
        
        public int TotalHoras
        {
            get
            {
                int total = 0;
                foreach (Famod famod in _famods)
                {
                    total += famod.HorasApropriadas;
                }
                return total;
            }
        }
    }
}