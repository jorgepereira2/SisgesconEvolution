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
/// Summary description for User
/// </summary>
public class User
{
	public User(int id)
	{
		this._id = id;
		this._dataUltimoRequest = DateTime.Now;
	}

	private int _id;	
	private DateTime _dataUltimoRequest;

	public DateTime DataUltimoRequest
	{
		get { return _dataUltimoRequest; }
		set { _dataUltimoRequest = value; }
	}

	//ID da pessoa
	public int ID
	{
		get { return _id; }
		set { _id = value; }
	}

	public override bool Equals(object obj)
	{
		if (obj is User)
		{
			User other = (User)obj;
			return other.ID == this._id;
		}
		return base.Equals(obj);
	}

	public override int GetHashCode()
	{
		return this._id;
	}

}
