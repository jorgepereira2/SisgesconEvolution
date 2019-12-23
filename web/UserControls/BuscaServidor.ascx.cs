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


public partial class BuscaServidor : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Manager.Register(this);
       
        
        if(!this.IsPostBack)
        {
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

            //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popupbusca", s);
            txtServidor.Style.Add("border", "solid 1px blue");
            //if(SearchURL == "")
            //    lnkBusca.Visible = false;
            //else
           
            
        }
        //string address = "../busca/frmServicoMaterialBusca.aspx?id_controle=" + this.ClientID;
        //if (TipoServicoMaterial.HasValue)
        //    address += "&tipoMaterial=" + Convert.ToInt32(TipoServicoMaterial.Value).ToString();

        //AnthemClientMethods.Popup(lnkBusca, address, false, false, false, true, true, true,
        //    true, 10, 10, 760, 550, false);
    }

    public delegate void SelectedValueChangedHandler(object source, BuscaServidorEventArgs e);
    public event SelectedValueChangedHandler SelectedValueChanged;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        txtServidor.TextChanged += new Anthem.AutoSuggestBox.TextChangedEventHandler(txtServidor_TextChanged);
        txtServidor.SelectedValueChanged += new Anthem.AutoSuggestBox.SelectedValueChangedHandler(txtServidor_SelectedValueChanged);
        txtServidor.CallBackControlID = txtServidor.ClientID;
    }

    void txtServidor_SelectedValueChanged(object source, EventArgs e)
    {
        if (SelectedValueChanged != null)
        {
            FireEvent(txtServidor.SelectedValue);
        }
    }

    void txtServidor_TextChanged(object source, Anthem.AutoSuggestEventArgs e)
    {
        try
        {
            txtServidor.DataSource = Servidor.FastSearch(e.CurrentText);
            txtServidor.DataBind();
        }
        catch (Exception e1)
        {
            AnthemClientMethods.Alert(MarinhaPageBase.GetCompleteErrorMessage(e1), this.Page);
        }
    }

   
    public void Reset()
    {
        txtServidor.Text = String.Empty;
        txtServidor.SelectedValue = "0";
        txtServidor.UpdateAfterCallBack = true;
    }
	  
    //public TipoServicoMaterial? TipoServicoMaterial
    //{
    //    get
    //    {
    //        if(ViewState["TipoServicoMaterial"] == null)
    //            return null;
    //        else
    //            return (Marinha.Business.TipoServicoMaterial) ViewState["TipoServicoMaterial"];
    //    }
    //    set { ViewState["TipoServicoMaterial"] = value;}
    //}

    //public bool MostraMaterialNaoEstocavel
    //{
    //    get
    //    {
    //        return ViewState["MostraMaterialNaoEstocavel"] != null ? Convert.ToBoolean(ViewState["MostraMaterialNaoEstocavel"]) : true;
    //    }
    //    set { ViewState["MostraMaterialNaoEstocavel"] = value; }
    //}

    //public string SearchURL
    //{
    //    get
    //    {
    //        return ViewState["SearchURL"] != null ? ViewState["SearchURL"].ToString() : "";
    //    }
    //    set { ViewState["SearchURL"] = value; }
    //}
    
    public string ValidationGroup
    {
        get
        {
            EnsureChildControls();
            return txtServidor.ValidationGroup;
        }
        set
        {
            EnsureChildControls();
            txtServidor.ValidationGroup = value;
        }
    }

    public string ErrorMessage
    {
        get
        {
            EnsureChildControls();
            return txtServidor.ErrorMessage;
        }
        set
        {
            EnsureChildControls();
            txtServidor.ErrorMessage = value;
        }
    }

    public bool Required
    {
        get
        {
            EnsureChildControls();
            return txtServidor.Required;
        }
        set 
        {
            EnsureChildControls();
            txtServidor.Required = value;
        }
    }

    public bool UpdateAfterCallBack
    {
        get
        {
            EnsureChildControls();
            return txtServidor.UpdateAfterCallBack;
        }
        set
        {
            EnsureChildControls();
            txtServidor.UpdateAfterCallBack = value;
        }
    }

    public string SelectedValue
    {
        get 
        {
            EnsureChildControls();
            return txtServidor.SelectedValue == "" ? "0" : txtServidor.SelectedValue; 
        }
        set
        {
            EnsureChildControls();
            txtServidor.SelectedValue = value;
        }
    }

    public string Text
    {
        get
        {
            EnsureChildControls();
            return txtServidor.Text;
        }
        set
        {
            EnsureChildControls();
            txtServidor.Text = value;
        }
    }

    public bool AutoCallBack
    {
        get
        {
            EnsureChildControls();
            return txtServidor.AutoCallBack; 
        }
        set
        {
            EnsureChildControls();
            txtServidor.AutoCallBack = value;
        }
    }
	 
    [Anthem.Method]
    public void FireEvent(string id)
    {
        Servidor servidor = new Servidor();
        if (id != "" && id != "0")
            servidor = Servidor.Get(Convert.ToInt32(id));

        txtServidor.SelectedValue = id;
        txtServidor.Text = servidor.NomeCompleto;

        txtServidor.UpdateAfterCallBack = true;
        if (SelectedValueChanged != null)
            SelectedValueChanged(this, new BuscaServidorEventArgs (servidor));
    }
	
}

public class BuscaServidorEventArgs : EventArgs
{
    private readonly Servidor _servidor;

    public BuscaServidorEventArgs(Servidor servidor)
    {
        this._servidor = servidor;
    }

    public Servidor Servidor
    {
        get { return _servidor; }
    }
}
