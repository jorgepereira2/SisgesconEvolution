using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Message
/// </summary>
public class Message
{
	public Message()
	{
		
	}

	
	private User _remetente;
	private User _destinatario;
	private string _mensagem;

	public string Mensagem
	{
		get { return _mensagem; }
		set { _mensagem = value; }
	}

	public User Destinatario
	{
		get { return _destinatario; }
		set { _destinatario = value; }
	}

	public User Remetente
	{
		get { return _remetente; }
		set { _remetente = value; }
	}

}
