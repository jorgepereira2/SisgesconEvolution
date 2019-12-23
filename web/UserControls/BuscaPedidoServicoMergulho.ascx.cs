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


public partial class BuscaPedidoServicoMergulho : System.Web.UI.UserControl
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

        AnthemClientMethods.Popup(btnBuscaAvancada, "../busca/frmPSPSMBusca.aspx?id_controle=" + this.ClientID, false, false, false, true, true, true,
          true, 10, 10, 760, 550, false);
        if(!Page.IsPostBack)
        {
            txtPedidoServico.Style.Add("border", "solid blue 1px;");
        }
    }

    public delegate void SelectedValueChangedHandler(object source, BuscaPedidoServicoMergulhoEventArgs e);
    public event SelectedValueChangedHandler SelectedValueChanged;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        txtPedidoServico.TextChanged += new Anthem.AutoSuggestBox.TextChangedEventHandler(txtPedidoServico_TextChanged);
        txtPedidoServico.SelectedValueChanged += new Anthem.AutoSuggestBox.SelectedValueChangedHandler(txtPedidoServico_SelectedValueChanged);
        txtPedidoServico.CallBackControlID = txtPedidoServico.ClientID;
    }

    void txtPedidoServico_SelectedValueChanged(object source, EventArgs e)
    {
        if (SelectedValueChanged != null)
        {
            FireEvent(txtPedidoServico.SelectedValue);
        }
    }

    void txtPedidoServico_TextChanged(object source, Anthem.AutoSuggestEventArgs e)
    {
        try
        {
            txtPedidoServico.DataSource = PedidoServicoMergulho.FastSearch(e.CurrentText);
            txtPedidoServico.DataBind();
        }
        catch (Exception e1)
        {
            AnthemClientMethods.Alert(MarinhaPageBase.GetCompleteErrorMessage(e1), this.Page);
        }
    }

   
    public void Reset()
    {
        txtPedidoServico.Text = String.Empty;
        txtPedidoServico.SelectedValue = "0";
        txtPedidoServico.UpdateAfterCallBack = true;
    }
	  
    
    public string ValidationGroup
    {
        get
        {
            EnsureChildControls();
            return txtPedidoServico.ValidationGroup;
        }
        set
        {
            EnsureChildControls();
            txtPedidoServico.ValidationGroup = value;
        }
    }

    public string ErrorMessage
    {
        get
        {
            EnsureChildControls();
            return txtPedidoServico.ErrorMessage;
        }
        set
        {
            EnsureChildControls();
            txtPedidoServico.ErrorMessage = value;
        }
    }

    public bool Required
    {
        get
        {
            EnsureChildControls();
            return txtPedidoServico.Required;
        }
        set 
        {
            EnsureChildControls();
            txtPedidoServico.Required = value;
        }
    }

    public bool UpdateAfterCallBack
    {
        get
        {
            EnsureChildControls();
            return txtPedidoServico.UpdateAfterCallBack;
        }
        set
        {
            EnsureChildControls();
            txtPedidoServico.UpdateAfterCallBack = value;
        }
    }

    public string SelectedValue
    {
        get 
        {
            EnsureChildControls();
            return txtPedidoServico.SelectedValue == "" ? "0" : txtPedidoServico.SelectedValue; 
        }
        set
        {
            EnsureChildControls();
            txtPedidoServico.SelectedValue = value;
        }
    }

    public string Text
    {
        get
        {
            EnsureChildControls();
            return txtPedidoServico.Text;
        }
        set
        {
            EnsureChildControls();
            txtPedidoServico.Text = value;
        }
    }

    public bool AutoCallBack
    {
        get
        {
            EnsureChildControls();
            return txtPedidoServico.AutoCallBack; 
        }
        set
        {
            EnsureChildControls();
            txtPedidoServico.AutoCallBack = value;
        }
    }
	 
    [Anthem.Method]
    public void FireEvent(string id)
    {
        PedidoServicoMergulho ps = new PedidoServicoMergulho();
        if (id != "" && id != "0")
            ps = PedidoServicoMergulho.Get(Convert.ToInt32(id));
        

        txtPedidoServico.SelectedValue = id;
        txtPedidoServico.Text = ps.CodigoComAno;

        txtPedidoServico.UpdateAfterCallBack = true;
        
        if (SelectedValueChanged != null)
            SelectedValueChanged(this, new BuscaPedidoServicoMergulhoEventArgs(ps));
    }
	
}

public class BuscaPedidoServicoMergulhoEventArgs : EventArgs
{
    private readonly PedidoServicoMergulho _pedidoServico;

    public BuscaPedidoServicoMergulhoEventArgs(PedidoServicoMergulho _pedido)
    {
        this._pedidoServico = _pedido;
    }

    public PedidoServicoMergulho PedidoServico
    {
        get { return _pedidoServico; }
    }
}
