using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ChatManager
/// </summary>
public class ChatManager
{
	public ChatManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	private static List<User> OnlineUsers
	{
		get
		{
			return Nested._onlineUsers;
		}
	}

	/// <summary>
	/// Assists with ensuring thread-safe, lazy singleton
	/// </summary>
	private class Nested
	{
		static Nested() { }
		internal static readonly List<User> _onlineUsers = new List<User>();
	}

	public static bool Contains(int id_pessoa)
	{
		return OnlineUsers.Contains(new User(id_pessoa));
	}

	private static void AddUser(int id_pessoa)
	{
		if (!OnlineUsers.Contains(new User(id_pessoa)))
			OnlineUsers.Add(new User(id_pessoa));
	}


	public static void UpdateUserStatus(int id_pessoa)
	{
		foreach (User user in OnlineUsers)
		{
			if (user.ID == id_pessoa)
			{
				user.DataUltimoRequest = DateTime.Now;
				return;
			}
		}
		//Se não encontrou o usuário, temos que adicioná-lo
		AddUser(id_pessoa);
	}

	public static void Remove(int id_pessoa)
	{
		for (int i = 0; i < OnlineUsers.Count; i++)
		{
			if (OnlineUsers[i].ID == id_pessoa)
			{
				OnlineUsers.RemoveAt(i);
				return;
			}
		}
	}

	/// <summary>
	/// Tira da lista os usuarios que não fazem mais request
	/// </summary>
	public static void UpdateCollection()
	{
		try
		{
			List<int> index = new List<int>(OnlineUsers.Count);
			for (int i = 0; i < OnlineUsers.Count; i++)
			{
				if (OnlineUsers[i].DataUltimoRequest < DateTime.Now.AddMinutes(-1))
					index.Add(i);
			}

			for (int i = 0; i < index.Count; i++)
			{
				OnlineUsers.RemoveAt(index[i]);
			}
		}
		catch { }
	}

}
