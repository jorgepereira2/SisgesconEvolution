using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Web = System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Shared.Common;
using Marinha.Business;
using Shared.SessionState;
using Anthem;


public partial class BuscaCliente : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Manager.Register(this);
        string s = @"<script language='javascript' type='text/javascript'>
            function UpdateUserControl(userControlID, ID) {            
	            Anthem_InvokeControlMethod(
		            userControlID,
		            'FireEvent',
		            [ID],
		            function(result) {}
	            );
            }
            </script>";

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popupbusca", s);
        
        if(!Page.IsPostBack)
        {
            if(ShowNovo)
                AnthemClientMethods.Popup(btnNovo, "../cadastro/frmClienteCadastro.aspx?popup=true", false, false, false, true, true, true,
                    true, 10, 10, 760, 550, false);
            else
                btnNovo.Visible = false;


            AnthemClientMethods.Popup(btnBuscaAvancada, "../busca/frmClienteBusca.aspx?id_controle=" + this.ClientID, false, false, false, true, true, true,
            true, 10, 10, 760, 550, false);

            txtCliente.Style.Add("border", "solid 1px blue");
        }
    }

    public delegate void SelectedValueChangedHandler(object source, BuscaClienteEventArgs e);
    public event SelectedValueChangedHandler SelectedValueChanged;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        txtCliente.TextChanged += new Anthem.AutoSuggestBox.TextChangedEventHandler(txtCliente_TextChanged);
        txtCliente.SelectedValueChanged += new Anthem.AutoSuggestBox.SelectedValueChangedHandler(txtCliente_SelectedValueChanged);
        txtCliente.CallBackControlID = txtCliente.ClientID;
    }


    void txtCliente_SelectedValueChanged(object source, EventArgs e)
    {
        if (SelectedValueChanged != null)
        {
            FireEvent(txtCliente.SelectedValue);
        }
    }

    void txtCliente_TextChanged(object source, Anthem.AutoSuggestEventArgs e)
    {
        try
        {
            txtCliente.DataSource = Cliente.FastSearch(e.CurrentText);
            txtCliente.DataBind();
        }
        catch (Exception e1)
        {
            AnthemClientMethods.Alert(e1.Message, this.Page);
        }
    }


    public void Reset()
    {
        txtCliente.Text = String.Empty;
        txtCliente.SelectedValue = "0";
        txtCliente.UpdateAfterCallBack = true;
    }
	  
    
    public string ValidationGroup
    {
        get
        {
            EnsureChildControls();
            return txtCliente.ValidationGroup;
        }
        set
        {
            EnsureChildControls();
            txtCliente.ValidationGroup = value;
        }
    }

    public string ErrorMessage
    {
        get
        {
            EnsureChildControls();
            return txtCliente.ErrorMessage;
        }
        set
        {
            EnsureChildControls();
            txtCliente.ErrorMessage = value;
        }
    }

    public bool Required
    {
        get
        {
            EnsureChildControls();
            return txtCliente.Required;
        }
        set 
        {
            EnsureChildControls();
            txtCliente.Required = value;
        }
    }

    public bool UpdateAfterCallBack
    {
        get
        {
            EnsureChildControls();
            return txtCliente.UpdateAfterCallBack;
        }
        set
        {
            EnsureChildControls();
            txtCliente.UpdateAfterCallBack = value;
        }
    }

    public string SelectedValue
    {
        get 
        {
            EnsureChildControls();
            return txtCliente.SelectedValue == "" ? "0" : txtCliente.SelectedValue; 
        }
        set
        {
            EnsureChildControls();
            txtCliente.SelectedValue = value;
        }
    }

    public string Text
    {
        get
        {
            EnsureChildControls();
            return txtCliente.Text;
        }
        set
        {
            EnsureChildControls();
            txtCliente.Text = value;
        }
    }

    public bool AutoCallBack
    {
        get
        {
            EnsureChildControls();
            return txtCliente.AutoCallBack; 
        }
        set
        {
            EnsureChildControls();
            txtCliente.AutoCallBack = value;
        }
    }

    public bool ShowNovo
    {
        get
        {
            return ViewState["ShowNovo"] == null ? false : Convert.ToBoolean(ViewState["ShowNovo"]);
        }
        set
        {
            ViewState["ShowNovo"] = value;
        }
    }
	 
    [Anthem.Method]
    public void FireEvent(string id)
    {
        Cliente cliente = new Cliente();
        if (id != "" && id != "0")
            cliente = Cliente.Get(Convert.ToInt32(id));
        

        txtCliente.SelectedValue = id;
        txtCliente.Text = cliente.Descricao;

        txtCliente.UpdateAfterCallBack = true;
        
        if (SelectedValueChanged != null)
            SelectedValueChanged(this, new BuscaClienteEventArgs(cliente));
    }
	
}

public class BuscaClienteEventArgs : EventArgs
{
    private readonly Cliente _cliente;

    public BuscaClienteEventArgs(Cliente _cliente)
    {
        this._cliente = _cliente;
    }

    public Cliente Cliente
    {
        get { return _cliente; }
    }
}
