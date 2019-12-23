using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class UserControls_MessageBox : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        winMessagebox.Width = (_width == Unit.Empty ? winMessagebox.Width : _width);
        winMessagebox.Height = (_height == Unit.Empty ? winMessagebox.Height : _height);
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        btnNao.Click += delegate { Close(MessageBoxResult.Nao); };
        btnSim.Click += delegate { Close(MessageBoxResult.Sim); };
    }
    
    private void Close(MessageBoxResult result)
    {
        if(MessageBoxClose != null)
            MessageBoxClose(this, new MessageBoxEventArgs(result, ViewState["MessageBox.Data"]));
        
        winMessagebox.Hide();
    }

    public delegate void MessageBoxEventHandler(object sender, MessageBoxEventArgs e);
    public event MessageBoxEventHandler MessageBoxClose;
    private Unit _width;
    private Unit _height;
    
    public virtual Unit Height
    {
        get { return _height; }
        set { _height = value; }
    }

    public Unit Width
    {
        get { return _width; }
        set { _width = value; }
    }

    public void Show(string message, object data)
    {
        lblMensagem.Text = message;
        lblMensagem.UpdateAfterCallBack = true;
        winMessagebox.Show();
        ViewState.Add("MessageBox.Data", data);
    }
}

public class MessageBoxEventArgs : EventArgs
{
    private readonly MessageBoxResult _result;
    private readonly object _data;

    public MessageBoxResult Result
    {
        get { return _result; }
    }

    public object Data
    {
        get { return _data; }
    }
    
    public MessageBoxEventArgs(MessageBoxResult result, object  data)
    {
        this._result = result;
        this._data = data;
    }
}

public enum MessageBoxResult
{
    Sim,
    Nao
}
