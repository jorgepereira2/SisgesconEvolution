using System;
using Anthem;
using Marinha.Business;
using Web = System.Web.UI.WebControls;


public partial class BuscaFornecedor : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Manager.Register(this);
        string s =
            @"<script language='javascript' type='text/javascript'>
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

        if (!Page.IsPostBack)
        {
            if (ShowNovo)
                AnthemClientMethods.Popup(btnNovo, "../cadastro/frmFornecedorCadastro.aspx?popup=true", false, false,
                                          false, true, true, true,
                                          true, 10, 10, 760, 550, false);


            AnthemClientMethods.Popup(btnBuscaAvancada, "../busca/frmFornecedorBusca.aspx?id_controle=" + this.ClientID,
                                      false, false, false, true, true, true,
                                      true, 10, 10, 760, 550, false);

            txtFornecedor.Style.Add("border", "solid 1px blue");
        }
        
        btnNovo.Visible = ShowNovo;
        btnNovo.UpdateAfterCallBack = true;
    }

    public delegate void SelectedValueChangedHandler(object source, BuscaFornecedorEventArgs e);
    public event SelectedValueChangedHandler SelectedValueChanged;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        txtFornecedor.TextChanged += new Anthem.AutoSuggestBox.TextChangedEventHandler(txtFornecedor_TextChanged);
        txtFornecedor.SelectedValueChanged += new Anthem.AutoSuggestBox.SelectedValueChangedHandler(txtFornecedor_SelectedValueChanged);
        txtFornecedor.CallBackControlID = txtFornecedor.ClientID;
    }


    void txtFornecedor_SelectedValueChanged(object source, EventArgs e)
    {
        if (SelectedValueChanged != null)
        {
            FireEvent(txtFornecedor.SelectedValue);
        }
    }

    void txtFornecedor_TextChanged(object source, Anthem.AutoSuggestEventArgs e)
    {
        try
        {
            txtFornecedor.DataSource = Fornecedor.FastSearch(e.CurrentText);
            txtFornecedor.DataBind();
        }
        catch (Exception e1)
        {
            AnthemClientMethods.Alert(e1.Message, this.Page);
        }
    }


    public void Reset()
    {
        txtFornecedor.Text = String.Empty;
        txtFornecedor.SelectedValue = "0";
        txtFornecedor.UpdateAfterCallBack = true;
    }

    public bool Enabled
    {
        get
        {
            EnsureChildControls();
            return txtFornecedor.Enabled;
        }
        set
        {
            EnsureChildControls();
            txtFornecedor.Enabled = value;
        }
    }
    
    public string ValidationGroup
    {
        get
        {
            EnsureChildControls();
            return txtFornecedor.ValidationGroup;
        }
        set
        {
            EnsureChildControls();
            txtFornecedor.ValidationGroup = value;
        }
    }

    public string ErrorMessage
    {
        get
        {
            EnsureChildControls();
            return txtFornecedor.ErrorMessage;
        }
        set
        {
            EnsureChildControls();
            txtFornecedor.ErrorMessage = value;
        }
    }

    public bool Required
    {
        get
        {
            EnsureChildControls();
            return txtFornecedor.Required;
        }
        set 
        {
            EnsureChildControls();
            txtFornecedor.Required = value;
        }
    }

    public bool UpdateAfterCallBack
    {
        get
        {
            EnsureChildControls();
            return txtFornecedor.UpdateAfterCallBack;
        }
        set
        {
            EnsureChildControls();
            txtFornecedor.UpdateAfterCallBack = value;
        }
    }

    public string SelectedValue
    {
        get 
        {
            EnsureChildControls();
            return txtFornecedor.SelectedValue == "" ? "0" : txtFornecedor.SelectedValue; 
        }
        set
        {
            EnsureChildControls();
            txtFornecedor.SelectedValue = value;
        }
    }

    public string Text
    {
        get
        {
            EnsureChildControls();
            return txtFornecedor.Text;
        }
        set
        {
            EnsureChildControls();
            txtFornecedor.Text = value;
        }
    }

    public bool AutoCallBack
    {
        get
        {
            EnsureChildControls();
            return txtFornecedor.AutoCallBack; 
        }
        set
        {
            EnsureChildControls();
            txtFornecedor.AutoCallBack = value;
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
        Fornecedor fornecedor = new Fornecedor();
        if (id != "" && id != "0")
            fornecedor = Fornecedor.Get(Convert.ToInt32(id));
        

        txtFornecedor.SelectedValue = id;
        txtFornecedor.Text = fornecedor.RazaoSocial;

        txtFornecedor.UpdateAfterCallBack = true;
        
        if (SelectedValueChanged != null)
            SelectedValueChanged(this, new BuscaFornecedorEventArgs(fornecedor));
    }
	
}

public class BuscaFornecedorEventArgs : EventArgs
{
    private readonly Fornecedor _fornecedor;

    public BuscaFornecedorEventArgs(Fornecedor fornecedor)
    {
        _fornecedor = fornecedor;
    }

    public Fornecedor Fornecedor
    {
        get { return _fornecedor; }
    }
}
