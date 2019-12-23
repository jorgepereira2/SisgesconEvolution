using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class MotivoCancelamento : BusinessObject<MotivoCancelamento>, IDescricao	
	{
		#region Private Members
		private string _descricao; 
		private bool _flagativo;
	    private bool _flagPS;
	    private bool _flagPO;
	    private bool _flagAC;
	    private bool _flagLicitacao;
	    private bool _flagItemComprador;
        private bool _flagOMF;

		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public MotivoCancelamento()
		{
			_descricao = null; 
			_flagativo = false; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
        public virtual bool FlagOMF
        {
            get { return _flagOMF; }
            set { _flagOMF = value; }
        }

	    public virtual bool FlagPedidoServicoMergulho { get; set; }
        public virtual bool FlagPedidoServicoAtividadeSecundaria { get; set; }

        public virtual bool FlagItemComprador
        {
            get { return _flagItemComprador; }
            set { _flagItemComprador = value; }
        }
        public virtual bool FlagAC
        {
            get { return _flagAC; }
            set { _flagAC = value; }
        }
        public virtual bool FlagPO
        {
            get { return _flagPO; }
            set { _flagPO = value; }
        }
        public virtual bool FlagPS
        {
            get { return _flagPS; }
            set { _flagPS = value; }
        }

		public virtual string Descricao
		{
			get { return _descricao; }
			set	
			{
				if ( value != null )
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for Descricao", value, value.ToString());
				
				_descricao = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagAtivo
		{
			get { return _flagativo; }
			set { _flagativo = value; }
		}

	    public virtual bool FlagLicitacao
	    {
	        get { return _flagLicitacao; }
	        set { _flagLicitacao = value; }
	    }

	    #endregion 
		
		
		#region Public Methods
		
		public static Dictionary<int, string> List(ObjetoCancelavel objetoCancelavel)
		{
		    string campo = "";
            switch(objetoCancelavel)
            {
                case ObjetoCancelavel.PedidoServico:
                    campo = "FlagPS";
                    break;
                case ObjetoCancelavel.PedidoObtencao:
                    campo = "FlagPO";
                    break;
                case ObjetoCancelavel.AutorizacaoCompra:
                    campo = "FlagAC";
                    break;
                case ObjetoCancelavel.Licitacao:
                    campo = "FlagLicitacao";
                    break;
                case ObjetoCancelavel.OMF:
                    campo = "FlagOMF";
                    break;
                case ObjetoCancelavel.PedidoServicoMergulho:
                    campo = "FlagPedidoServicoMergulho";
                    break;
                case ObjetoCancelavel.PedidoServicoAtividadeSecundaria:
                    campo = "FlagPedidoServicoAtividadeSecundaria";
                    break;
            }

			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(string.Format(
            @"select m.ID, m.Descricao 
			from MotivoCancelamento m  
			where m.FlagAtivo = 1
            and m.{0} = 1
			order by m.Descricao", campo));
                        
		    return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<MotivoCancelamento> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from MotivoCancelamento m  			
			order by m.Descricao");
		
			return (List<MotivoCancelamento>)query.List<MotivoCancelamento>();
		}
		
		#endregion

        public override string ToString()
        {
            return _descricao;
        }		
		
	}
}
