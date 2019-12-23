using System;
using System.Text;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class MensagemChat : BusinessObject<MensagemChat>
	{
		#region Private Members
		private int _id_pessoaorigem;
		private string _nomepessoaorigem;
		private int _id_pessoadestino;
		private string _nomepessoadestino;
		private string _mensagem;
		private bool _flaglido;
		private DateTime _dataenvio;
		private DateTime? _datarecebido;
		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public MensagemChat()
		{
			_id_pessoaorigem = 0;
			_nomepessoaorigem = null;
			_id_pessoadestino = 0;
			_nomepessoadestino = null;
			_mensagem = null;
			_flaglido = false;
			_dataenvio = DateTime.MinValue;
			_datarecebido = null;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

		/// <summary>
		/// 
		/// </summary>		
		public virtual int ID_ServidorOrigem
		{
			get { return _id_pessoaorigem; }
			set { _id_pessoaorigem = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual string NomeServidorOrigem
		{
			get { return _nomepessoaorigem; }
			set
			{
				if (value != null)
					if (value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for NomeServidorOrigem", value, value.ToString());

				_nomepessoaorigem = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual int ID_ServidorDestino
		{
			get { return _id_pessoadestino; }
			set { _id_pessoadestino = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual string NomeServidorDestino
		{
			get { return _nomepessoadestino; }
			set
			{
				if (value != null)
					if (value.Length > 70)
						throw new ArgumentOutOfRangeException("Invalid value for NomeServidorDestino", value, value.ToString());

				_nomepessoadestino = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual string Mensagem
		{
			get { return _mensagem; }
			set
			{
				if (value != null)
					if (value.Length > 4000)
						throw new ArgumentOutOfRangeException("Invalid value for Mensagem", value, value.ToString());

				_mensagem = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagLido
		{
			get { return _flaglido; }
			set { _flaglido = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime DataEnvio
		{
			get { return _dataenvio; }
			set { _dataenvio = value; }
		}

		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime? DataRecebido
		{
			get { return _datarecebido; }
			set { _datarecebido = value; }
		}		
		#endregion

		/// <summary>
		/// Verifica se existem alguma msg não recebida em chats que não estejam ativos
		/// Finalidade: Utilizada para mostrar o popup na tela apenas se a janela de chat não estiver ativa
		/// </summary>
		/// <returns>Retorna uma Lista com os ids das pessoas que enviaram msgs</returns>
		public static List<int> SelectJanelasParaAbrir(int id_pessoa, List<int> chatsAtivos)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
				@"select m.ID_ServidorOrigem from MensagemChat m 
				where m.ID_ServidorDestino = :id_pessoaDestino
				and m.FlagLido = 0
				Group By m.ID_ServidorOrigem");

			query.SetInt32("id_pessoaDestino", id_pessoa);
			List<int> list = (List<int>)query.List<int>();

			List<int> listaServidors = new List<int>();
			foreach (int id in list)
			{
				if (!chatsAtivos.Contains(id))
					listaServidors.Add(id);
			}
			return listaServidors;
		}

		public static string ReadNewMessages(int id_pessoaOrigem, int id_pessoaDestino)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
				@"from MensagemChat m 
				where m.ID_ServidorDestino = :id_pessoaDestino
				and m.ID_ServidorOrigem = :id_pessoaOrigem
				and m.FlagLido = 0
				Order By m.DataEnvio");

			query.SetInt32("id_pessoaDestino", id_pessoaDestino);
			query.SetInt32("id_pessoaOrigem", id_pessoaOrigem);
			List<MensagemChat> list = (List<MensagemChat>)query.List<MensagemChat>();

			StringBuilder texto = new StringBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				//texto.Append("<table cellspacing='2' border='0' width='100%' style='WIDTH: 500px;'>");
				//texto.Append("<tr>");
				//texto.Append("<td align='left' >");

				texto.Append(GetFormatedMessage(list[i]));
				

				//texto.Append("</td>");
				//texto.Append("</tr>");
				//texto.Append("</table>");

				//Marca a mensagem como recebida
				if (!list[i].FlagLido && list[i].ID_ServidorDestino == id_pessoaDestino)
				{
					list[i].FlagLido = true;
					list[i].DataRecebido = DateTime.Now;
					list[i].Save();
				}
			}

			return texto.ToString();
		}

        public static string ReadOldMessages(int id_pessoaOrigem, int id_pessoaDestino)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                @"from MensagemChat m 
				where m.ID_ServidorDestino = :id_pessoaDestino
				and m.ID_ServidorOrigem = :id_pessoaOrigem
				and m.FlagLido = 1
				Order By m.DataEnvio");

            query.SetInt32("id_pessoaDestino", id_pessoaDestino);
            query.SetInt32("id_pessoaOrigem", id_pessoaOrigem);
            List<MensagemChat> list = (List<MensagemChat>)query.List<MensagemChat>();

            StringBuilder texto = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                texto.Append(GetFormatedMessage(list[i]));
            }

            return texto.ToString();
        }

		public static string GetFormatedMessage(MensagemChat msg)
		{
			StringBuilder texto = new StringBuilder();
			texto.Append("(").Append(msg.DataEnvio.ToString("dd/MM HH:mm")).Append(") ");
			texto.Append("<b>");
			texto.Append(msg.NomeServidorOrigem);
			texto.Append(" diz:<br />");
			texto.Append("</b>");
			texto.Append(msg.Mensagem);
			texto.Append("<br />");
			return texto.ToString();
		}
	}
}