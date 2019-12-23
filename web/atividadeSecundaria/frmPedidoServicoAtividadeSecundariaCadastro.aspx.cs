using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Shared.NHibernateDAL;
using Marinha.Business;
using Shared.SessionState;
using Shared.Common;

public partial class frmPedidoServicoAtividadeSecundariaCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected PedidoServicoAtividadeSecundaria _pedido;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        this.btnEnviar.Click += BtnEnviar_OnClick;
        ucCliente.SelectedValueChanged += new BuscaCliente.SelectedValueChangedHandler(ucCliente_SelectedValueChanged);


        dgItem.UpdateCommand += new DataGridCommandEventHandler(dgItem_UpdateCommand);
        dgItem.CancelCommand += new DataGridCommandEventHandler(dgItem_CancelCommand);
        dgItem.DeleteCommand += new DataGridCommandEventHandler(dgItem_DeleteCommand);
        dgItem.EditCommand += new DataGridCommandEventHandler(dgItem_EditCommand);
        dgItem.ItemCommand += new DataGridCommandEventHandler(dgItem_ItemCommand);
        dgItem.ItemDataBound += new DataGridItemEventHandler(dgItem_ItemDataBound);
        btnNovoItem.Click += new EventHandler(btnNovoItem_Click);

        btnFatura.Click += new EventHandler(btnFatura_Click);
        btnRecalcularFatura.Click += new EventHandler(btnRecalcularFatura_Click);

        Anthem.Manager.Register(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        	FillPage();
            if (Request["ID_Pedido"] != null)
            {
                _pedido = PedidoServicoAtividadeSecundaria.Get(Convert.ToInt32(Request["ID_Pedido"]));
                PopulateFields();
            }
            else
            {
                _pedido = new PedidoServicoAtividadeSecundaria();
                
            }

            Anthem.AnthemClientMethods.Redirect("frmPedidoServicoAtividadeSecundariaPesquisa.aspx", btnVoltar);

            //Anthem.AnthemClientMethods.Popup(btnBuscarPedidoServicoMergulho, "frmPedidoServicoMergulhoBusca.aspx", false, false, false, true, true, true, true, 20, 50, 700, 500, false);

            if (_pedido.Itens.Count == 0)
            {
                dgItem.ShowFooter = true;
                BindItem();
            }

            if (Request["edit"] != null)
                btnEnviar.Visible = false;
        }
    }

	private void FillPage()
	{
        Util.FillDropDownList(ddlDivisao, Celula.List(), ESCOLHA_OPCAO);
	}
	
    private void PopulateFields()
    {
        FillPage();
        BindItem();

        UpdateLabels();

        SetFields(_pedido);
    }

    private void SetFields(PedidoServicoAtividadeSecundaria ps)
    {
        txtObservacao.Text = ps.Observacao;
        txtQuantidadeHH.Text = ps.QuantidadeHH.ToString();
        txtDataVencimento.Text = ps.DataVencimento.ToShortDateString();
        
        ucCliente.SelectedValue = ObjectReader.ReadID(ps.Cliente);
        ucCliente.Text = ps.Cliente.DescricaoCompleta;
        ucClientePagador.SelectedValue = ObjectReader.ReadID(ps.ClientePagador);
        ucClientePagador.Text = ps.ClientePagador.DescricaoCompleta;
        ddlDivisao.SelectedValue = ObjectReader.ReadID(ps.Celula);
    }

    private void UpdateLabels()
    {
        lblCodigo.Text = _pedido.CodigoComAno;
        lblDataEmissao.Text = ObjectReader.ReadDate(_pedido.DataEmissao);
        lblStatus.Text = _pedido.Status.Descricao;

        lblCodigo.UpdateAfterCallBack = true;
        lblDataEmissao.UpdateAfterCallBack = true;
        lblStatus.UpdateAfterCallBack = true;
    }

    private void ClearFields()
    {
        lblCodigo.Text = "";
        lblStatus.Text = "";
        lblDataEmissao.Text = "";
        ucCliente.Reset();
        ucClientePagador.Reset();
        
        txtObservacao.Text = "";
        txtQuantidadeHH.Text = "";
        txtDataVencimento.Text = "";
        ddlDivisao.SelectedIndex = -1;
        
        
        RefreshFields();
    }

    private void RefreshFields()
    {
        lblCodigo.UpdateAfterCallBack = true;
        lblDataEmissao.UpdateAfterCallBack = true;
        txtQuantidadeHH.UpdateAfterCallBack = true;
        txtObservacao.UpdateAfterCallBack = true;
        txtDataVencimento.UpdateAfterCallBack = true;
        ddlDivisao.UpdateAfterCallBack = true;
        
        lblStatus.UpdateAfterCallBack = true;
        
        ucClientePagador.UpdateAfterCallBack = true;
        ucCliente.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _pedido.Cliente = Cliente.Get(Convert.ToInt32(ucCliente.SelectedValue));
        _pedido.ClientePagador = Cliente.Get(Convert.ToInt32(ucClientePagador.SelectedValue));
        _pedido.Observacao = PageReader.ReadString(txtObservacao);
        _pedido.QuantidadeHH = PageReader.ReadInt(txtQuantidadeHH);
        _pedido.DataVencimento = PageReader.ReadDate(txtDataVencimento);
        
        //if (Request["edit"] == null)
        {
            _pedido.Celula = Celula.Get(Convert.ToInt32(ddlDivisao.SelectedValue));
        }
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        if (!ValidatePage())
            return;
        FillObject();
        _pedido.Save();
        UpdateLabels();
		ShowSuccessMessage();
    }
    
    private bool ValidatePage()
    {
        if(ucCliente.SelectedValue == "0")
        {
            ShowMessage("Campo Cliente obrigatório.");
            return false;
        }
        if (ucClientePagador.SelectedValue == "0")
        {
            ShowMessage("Campo Pagador obrigatório.");
            return false;
        }
        return true;
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _pedido = new PedidoServicoAtividadeSecundaria();
    }

    private void BtnEnviar_OnClick(object sender, EventArgs e)
    {
        FillObject();
        
        _pedido.Enviar(this.ID_Servidor);
        UpdateLabels();
        ShowSuccessMessage();
        
    }

    void ucCliente_SelectedValueChanged(object source, BuscaClienteEventArgs e)
    {
        if (e.Cliente.ClientePagador != null)
        {
            ucClientePagador.Text = e.Cliente.ClientePagador.Descricao;
            ucClientePagador.SelectedValue = e.Cliente.ClientePagador.ID.ToString();
            ucClientePagador.UpdateAfterCallBack = true;
        }
    }
    #endregion

    #region Item

    private void BindItem()
    {
        dgItem.DataSource = _pedido.Itens;
        dgItem.DataKeyField = "ID";
        dgItem.DataBind();

        dgItem.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dgItem_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        PedidoServicoAtividadeSecundariaItem psItem = _pedido.Itens.Find(Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]));
        psItem.Delete();
        _pedido.Itens.Remove(psItem);
        BindItem();
    }

    void dgItem_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        TextBox txtDescricaoServico = (TextBox)e.Item.FindControl("txtDescricaoServico");
        TextBox txtValor = (TextBox)e.Item.FindControl("txtValor");
        DropDownList ddlTipoAtividadeSecundaria = (DropDownList)e.Item.FindControl("ddlTipoAtividadeSecundaria");


        PedidoServicoAtividadeSecundariaItem psItem = _pedido.Itens.Find(Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]));
        psItem.DescricaoServico = txtDescricaoServico.Text;
        psItem.Valor = PageReader.ReadDecimal(txtValor);
        psItem.TipoAtividadeSecundaria = TipoAtividadeSecundaria.Get(Convert.ToInt32(ddlTipoAtividadeSecundaria.SelectedValue));

        psItem.Save();
        
        dgItem.ShowFooter = false;
        dgItem.EditItemIndex = -1;
        BindItem();

    }

    void dgItem_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            TextBox txtDescricaoServico = (TextBox)e.Item.FindControl("txtDescricaoServico");
            TextBox txtValor = (TextBox)e.Item.FindControl("txtValor");
            DropDownList ddlTipoAtividadeSecundaria = (DropDownList)e.Item.FindControl("ddlTipoAtividadeSecundaria");


            PedidoServicoAtividadeSecundariaItem psItem = new PedidoServicoAtividadeSecundariaItem();
            psItem.DescricaoServico = txtDescricaoServico.Text;
            psItem.Valor = PageReader.ReadDecimal(txtValor);
            psItem.TipoAtividadeSecundaria = TipoAtividadeSecundaria.Get(Convert.ToInt32(ddlTipoAtividadeSecundaria.SelectedValue));
            psItem.PedidoServicoAtividadeSecundaria = _pedido;

            if (!_pedido.IsPersisted)
                btnSalvar_Click(null, null);
            psItem.Save();
            _pedido.Itens.Add(psItem);

            BindItem();
            dgItem.ShowFooter = false;
        }
    }

    void dgItem_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlTipoAtividadeSecundaria = (DropDownList)e.Item.FindControl("ddlTipoAtividadeSecundaria");
            Util.FillDropDownList(ddlTipoAtividadeSecundaria, TipoAtividadeSecundaria.List());
            if(e.Item.ItemType == ListItemType.EditItem)
            {
                PedidoServicoAtividadeSecundariaItem psItem = (PedidoServicoAtividadeSecundariaItem)e.Item.DataItem;
                ddlTipoAtividadeSecundaria.SelectedValue = psItem.TipoAtividadeSecundaria.ID.ToString();
            }
        }
    }

    void dgItem_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgItem.EditItemIndex = e.Item.ItemIndex;
        dgItem.ShowFooter = false;
        BindItem();
    }

    void dgItem_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgItem.EditItemIndex = -1;
        dgItem.ShowFooter = false;
        BindItem();
    }

    void btnNovoItem_Click(object sender, EventArgs e)
    {
        dgItem.ShowFooter = true;
        BindItem();
        dgItem.UpdateAfterCallBack = true;
    }
    #endregion

   
    //[Anthem.Method]
    //public void CopiarPedidoServicoMergulho(int id)
    //{
    //    try
    //    {
    //        PedidoServicoMergulho ps = PedidoServicoMergulho.Get(id);
    //        SetFields(ps);
    //        RefreshFields();
          
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(GetCompleteErrorMessage(ex));
    //    }
    //}


    void btnRecalcularFatura_Click(object sender, EventArgs e)
    {
        _pedido.RecalcularFatura();
        ShowMessage("Fatura recalculada com sucesso.");
    }

    void btnFatura_Click(object sender, EventArgs e)
    {
        Anthem.AnthemClientMethods.Popup(this, "fchFaturamentoPSAS.aspx?id_pedido=" + _pedido.ID, false, false, false, true, true, true, true, 20, 50, 700, 500);
    }

}
