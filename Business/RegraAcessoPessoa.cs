using System;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class RegraAcessoServidor : BusinessObject<RegraAcessoServidor>
	{
		#region Private Members				
		private bool _flagcontrolahorario;
		private bool _flagcontrolalocalacesso;
		private DateTime? _horarioinicial;
		private DateTime? _horariofinal;
		private bool _flagdomingo;
		private bool _flagsegunda;
		private bool _flagterca;
		private bool _flagquarta;
		private bool _flagquinta;
		private bool _flagsexta;
		private bool _flagsabado;
		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public RegraAcessoServidor()
		{			
			_flagcontrolahorario = false;
			_flagcontrolalocalacesso = false;
			_horarioinicial = null;
			_horariofinal = null;
			_flagdomingo = false;
			_flagsegunda = false;
			_flagterca = false;
			_flagquarta = false;
			_flagquinta = false;
			_flagsexta = false;
			_flagsabado = false;
			_locais = new CustomList<LocalAcesso>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
		
		public virtual bool FlagControlaHorario
		{
			get { return _flagcontrolahorario; }
			set { _flagcontrolahorario = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagControlaLocalAcesso
		{
			get { return _flagcontrolalocalacesso; }
			set { _flagcontrolalocalacesso = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime? HorarioInicial
		{
			get { return _horarioinicial; }
			set { _horarioinicial = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime? HorarioFinal
		{
			get { return _horariofinal; }
			set { _horariofinal = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagDomingo
		{
			get { return _flagdomingo; }
			set { _flagdomingo = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagSegunda
		{
			get { return _flagsegunda; }
			set { _flagsegunda = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagTerca
		{
			get { return _flagterca; }
			set { _flagterca = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagQuarta
		{
			get { return _flagquarta; }
			set { _flagquarta = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagQuinta
		{
			get { return _flagquinta; }
			set { _flagquinta = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagSexta
		{
			get { return _flagsexta; }
			set { _flagsexta = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagSabado
		{
			get { return _flagsabado; }
			set { _flagsabado = value; }
		}
		#endregion

		#region collection

		private ICustomList<LocalAcesso> _locais;

		public virtual ICustomList<LocalAcesso> Locais
		{
			get { return _locais; }
			set { _locais = value; }
		}
		#endregion

	}
}