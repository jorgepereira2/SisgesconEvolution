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


public partial class BuscaOrcamento : System.Web.UI.UserControl
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

        AnthemClientMethods.Popup(btnBuscaAvancada, "../busca/frmOrcamentoBusca.aspx?id_controle=" + this.ClientID, false, false, false, true, true, true,
           true, 10, 10, 760, 550, false);
        if (!Page.IsPostBack)
        {
            txtOrcamento.Style.Add("border", "solid blue 1px;");
        }
    }

    public delegate void SelectedValueChangedHandler(object source, BuscaOrcamentoEventArgs e);
    public event SelectedValueChangedHandler SelectedValueChanged;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        txtOrcamento.TextChanged += new Anthem.AutoSuggestBox.TextChangedEventHandler(txtOrcamento_TextChanged);
        txtOrcamento.SelectedValueChanged += new Anthem.AutoSuggestBox.SelectedValueChangedHandler(txtOrcamento_SelectedValueChanged);
        txtOrcamento.CallBackControlID = txtOrcamento.ClientID;
    }

    void txtOrcamento_SelectedValueChanged(object source, EventArgs e)
    {
        if (SelectedValueChanged != null)
        {
            FireEvent(txtOrcamento.SelectedValue);
        }
    }

    void txtOrcamento_TextChanged(object source, Anthem.AutoSuggestEventArgs e)
    {
        try
        {
            txtOrcamento.DataSource = DelineamentoOrcamento.FastSearch(e.CurrentText, this.Status);
            txtOrcamento.DataBind();
        }
        catch (Exception e1)
        {
            AnthemClientMethods.Alert(MarinhaPageBase.GetCompleteErrorMessage(e1), this.Page);
        }
    }

   
    public void Reset()
    {
        txtOrcamento.Text = String.Empty;
        txtOrcamento.SelectedValue = "0";
        txtOrcamento.UpdateAfterCallBack = true;
    }

    public StatusPedidoServicoEnum? Status
    {
        get
        {
            if (ViewState["Status"] == null)
                return null;
            else
                return (StatusPedidoServicoEnum)ViewState["Status"];
        }
        set { ViewState["Status"] = value; }
    }  
    
    public string ValidationGroup
    {
        get
        {
            EnsureChildControls();
            return txtOrcamento.ValidationGroup;
        }
        set
        {
            EnsureChildControls();
            txtOrcamento.ValidationGroup = value;
        }
    }

    public string ErrorMessage
    {
        get
        {
            EnsureChildControls();
            return txtOrcamento.ErrorMessage;
        }
        set
        {
            EnsureChildControls();
            txtOrcamento.ErrorMessage = value;
        }
    }

    public bool Required
    {
        get
        {
            EnsureChildControls();
            return txtOrcamento.Required;
        }
        set 
        {
            EnsureChildControls();
            txtOrcamento.Required = value;
        }
    }

    public bool UpdateAfterCallBack
    {
        get
        {
            EnsureChildControls();
            return txtOrcamento.UpdateAfterCallBack;
        }
        set
        {
            EnsureChildControls();
            txtOrcamento.UpdateAfterCallBack = value;
        }
    }

    public string SelectedValue
    {
        get 
        {
            EnsureChildControls();
            return txtOrcamento.SelectedValue == "" ? "0" : txtOrcamento.SelectedValue; 
        }
        set
        {
            EnsureChildControls();
            txtOrcamento.SelectedValue = value;
        }
    }

    public string Text
    {
        get
        {
            EnsureChildControls();
            return txtOrcamento.Text;
        }
        set
        {
            EnsureChildControls();
            txtOrcamento.Text = value;
        }
    }

    public bool AutoCallBack
    {
        get
        {
            EnsureChildControls();
            return txtOrcamento.AutoCallBack; 
        }
        set
        {
            EnsureChildControls();
            txtOrcamento.AutoCallBack = value;
        }
    }
	 
    [Anthem.Method]
    public void FireEvent(string id)
    {
        DelineamentoOrcamento orcamento = new DelineamentoOrcamento();
        if (id != "" && id != "0")
            orcamento = DelineamentoOrcamento.Get(Convert.ToInt32(id));
        

        txtOrcamento.SelectedValue = id;
        txtOrcamento.Text = orcamento.CodigoComAno;
        

        txtOrcamento.UpdateAfterCallBack = true;
        
        if (SelectedValueChanged != null)
            SelectedValueChanged(this, new BuscaOrcamentoEventArgs(orcamento));
    }
	
}

public class BuscaOrcamentoEventArgs : EventArgs
{
    private readonly DelineamentoOrcamento _delineamentoOrcamento;

    public BuscaOrcamentoEventArgs(DelineamentoOrcamento _delineamentoOrcamento)
    {
        this._delineamentoOrcamento = _delineamentoOrcamento;
    }

    public DelineamentoOrcamento DelineamentoOrcamento
    {
        get { return _delineamentoOrcamento; }
    }
}
