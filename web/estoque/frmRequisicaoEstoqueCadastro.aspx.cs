using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
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

public partial class frmRequisicaoEstoqueCadastro : MarinhaPageBase
{
    #region Private Member
    [TransientPageState]
    protected RequisicaoEstoque _requisicao;

    [TransientPageState]
    protected RequisicaoEstoqueItem _item;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        this.dgItem.DeleteCommand += new DataGridCommandEventHandler(dgItem_DeleteCommand);
        this.dgItem.EditCommand += new DataGridCommandEventHandler(dgItem_EditCommand);
        this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
        btnEnviar.Click += new EventHandler(btnEnviar_Click);
    }

    void btnImprimir_Click(object sender, EventArgs e)
    {
        Anthem.AnthemClientMethods.Popup(this, "fchRequisicaoEstoque.aspx?id_requisicao=" + _requisicao.ID.ToString(), false, false, false, true, true, true,
        true, 40, 60, 700, 500);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillPage();
            if (Request["ID_Requisicao"] != null)
            {
                _requisicao = RequisicaoEstoque.Get(Convert.ToInt32(Request["ID_Requisicao"]));
                PopulateFields();
            }
            else
            {
                _requisicao = new RequisicaoEstoque();
            }
            Anthem.AnthemClientMethods.Redirect("frmRequisicaoEstoquePesquisa.aspx", btnVoltar);
            
        }
    }

    private void FillPage()
    {
        Util.FillDropDownList(ddlOrigemMaterial, typeof(OrigemMaterial), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlOrigemMaterialDestino, typeof(OrigemMaterial), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlTipoRequisicao, TipoRequisicaoEstoque.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlResponsavel, Servidor.List(null), ESCOLHA_OPCAO);
        
        ddlOrigemMaterial.Items.Remove(ddlOrigemMaterial.Items.FindByValue(Convert.ToInt32(OrigemMaterial.Obtencao).ToString()));
        ddlOrigemMaterialDestino.Items.Remove(ddlOrigemMaterialDestino.Items.FindByValue(Convert.ToInt32(OrigemMaterial.Obtencao).ToString()));
    }

    #endregion

    #region Events
    void btnSalvar_Click(object sender, EventArgs e)
    {
        if(!_requisicao.PodeSerAlterada)
        {
            ShowMessage("A requisição não pode mais ser alterada.");
            return;
        }
        if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
            SalvarRequisicaoEstoque();
        else if (TabStrip1.SelectedTab.ID == tabItem.ID)
            SalvarItemRequisicaoEstoque();
    }

    void btnNovo_Click(object sender, EventArgs e)
    {
        if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
        {
            ClearFields();
            _requisicao = new RequisicaoEstoque();
        }
        else if (TabStrip1.SelectedTab.ID == tabItem.ID)
            ClearFieldsItemRequisicaoEstoque();
    }
    
    [Anthem.Method]
    public void AbaAlterada()
    {
        if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
            btnSalvar.ValidationGroup = "DadosBasicos";
        else
            btnSalvar.ValidationGroup = "Item";
        btnSalvar.UpdateAfterCallBack = true;
    }
    #endregion

    #region RequisicaoEstoque
    private void SalvarRequisicaoEstoque()
    {
        FillObject();
        _requisicao.Save();
        
        lblNumero.Text = _requisicao.Numero.ToString();
        lblNumero.UpdateAfterCallBack = true;
        lblStatus.Text = Util.GetDescription(_requisicao.Status);
        lblStatus.UpdateAfterCallBack = true;
        
        ShowSuccessMessage();
    }

    private void PopulateFields()
    {
        txtDataEmissao.Text = ObjectReader.ReadDate(_requisicao.DataFinalizacao);
        txtObservacao.Text = _requisicao.Observacao;
        ddlTipoRequisicao.SelectedValue = _requisicao.TipoRequisicaoEstoque.ID.ToString();
        ddlResponsavel.SelectedValue = _requisicao.ServidorResponsavel.ID.ToString();
        lblNumero.Text = _requisicao.Numero.ToString();
        lblStatus.Text = Util.GetDescription(_requisicao.Status);
        ddlTipoRequisicao_SelectedIndexChanged(null, null);
        
        BindItemRequisicaoEstoque();
    }

    private void ClearFields()
    {
        txtDataEmissao.Text = "";
        txtObservacao.Text = "";
        ddlTipoRequisicao.SelectedValue = "0";
        ddlResponsavel.SelectedValue = "0";
        lblNumero.Text = "";

        txtDataEmissao.UpdateAfterCallBack = true;
        txtObservacao.UpdateAfterCallBack = true;
        ddlTipoRequisicao.UpdateAfterCallBack = true;
        ddlResponsavel.UpdateAfterCallBack = true;
        lblNumero.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _requisicao.DataFinalizacao = PageReader.ReadDate(txtDataEmissao);
        _requisicao.Observacao = PageReader.ReadString(txtObservacao);
        _requisicao.ServidorResponsavel = Servidor.Get(Convert.ToInt32(ddlResponsavel.SelectedValue));
        _requisicao.ServidorCadastro = Servidor.Get(ID_Servidor);
        _requisicao.TipoRequisicaoEstoque = TipoRequisicaoEstoque.Get(Convert.ToInt32(ddlTipoRequisicao.SelectedValue));
    }
    #endregion

    #region ItemRequisicaoEstoque
    private void BindItemRequisicaoEstoque()
    {
        dgItem.DataSource = _requisicao.Itens;
        dgItem.DataKeyField = "ID";
        dgItem.DataBind();
        dgItem.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dgItem_EditCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
        _item = _requisicao.Itens.Find(id);
        PopulateFieldsItemRequisicaoEstoque();
    }

    void dgItem_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
        RequisicaoEstoqueItem item = _requisicao.Itens.Find(id);
        _requisicao.Itens.Remove(item);
        item.Delete();
        BindItemRequisicaoEstoque();
    }

    private void PopulateFieldsItemRequisicaoEstoque()
    {
        ucBuscaMaterial.SelectedValue = ObjectReader.ReadID(_item.Material);
        ucBuscaMaterial.Text = _item.Material.DescricaoCompleta;
        txtQuantidade.Text = ObjectReader.ReadDecimal(_item.Quantidade);
        ddlOrigemMaterial.SelectedValue =  Convert.ToInt32(_item.OrigemMaterial).ToString();
        txtCodigoMaterial.Text = _item.Material.CodigoInterno;
        ddlOrigemMaterialDestino.SelectedValue = _item.OrigemMaterialDestino.HasValue ? Convert.ToInt32(_item.OrigemMaterialDestino.Value).ToString() : "0";
        
        RefreshFieldsItemRequisicaoEstoque();
    }

    private void FillObjectItemRequisicaoEstoque()
    {
        if (_item == null)
        {
            _item = new RequisicaoEstoqueItem();
        }
        _item.RequisicaoEstoque = _requisicao;
        _item.Material = ServicoMaterial.Get(Convert.ToInt32(ucBuscaMaterial.SelectedValue));
        _item.Quantidade = PageReader.ReadDecimal(txtQuantidade);
        _item.OrigemMaterial = (OrigemMaterial)Convert.ToInt32(ddlOrigemMaterial.SelectedValue);
        if(ddlOrigemMaterialDestino.SelectedValue != "0" && _requisicao.TipoRequisicaoEstoque.TipoRequisicaoEstoqueEnum == TipoRequisicaoEstoqueEnum.Transferencia)
            _item.OrigemMaterialDestino = (OrigemMaterial)Convert.ToInt32(ddlOrigemMaterialDestino.SelectedValue);
        
    }

    private void ClearFieldsItemRequisicaoEstoque()
    {
        ucBuscaMaterial.Reset();
        txtQuantidade.Text = "";
        ddlOrigemMaterial.SelectedIndex = -1;
        txtCodigoMaterial.Text = "";
        ddlOrigemMaterialDestino.SelectedIndex = -1;
        
        RefreshFieldsItemRequisicaoEstoque();
        _item = null;
    }

    private void RefreshFieldsItemRequisicaoEstoque()
    {
        txtCodigoMaterial.UpdateAfterCallBack = true;
        ucBuscaMaterial.UpdateAfterCallBack = true;
        txtQuantidade.UpdateAfterCallBack = true;
        ddlOrigemMaterial.UpdateAfterCallBack = true;
        ddlOrigemMaterialDestino.UpdateAfterCallBack = true;
    }

    private void SalvarItemRequisicaoEstoque()
    {
        FillObjectItemRequisicaoEstoque();

        bool IsNew = !_item.IsPersisted;
        _item.Save();
        if(IsNew)
            _requisicao.Itens.Add(_item);

        BindItemRequisicaoEstoque();
        ClearFieldsItemRequisicaoEstoque();
    }
    #endregion

    void btnEnviar_Click(object sender, EventArgs e)
    {
        _requisicao.Finalizar();

        lblStatus.Text = Shared.Common.Util.GetDescription(_requisicao.Status);
        lblStatus.UpdateAfterCallBack = true;
    }


    protected void txtCodigoMaterial_TextChanged(object sender, EventArgs e)
    {
        ServicoMaterial sm = ServicoMaterial.GetByCodigo(txtCodigoMaterial.Text, null);
        if (sm != null)
        {
            ucBuscaMaterial.FireEvent(sm.ID.ToString());
        }
    }

    protected void ddlTipoRequisicao_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.AnthemClientMethods.ShowHide(trDestinoMaterial, 
            Convert.ToInt32(ddlTipoRequisicao.SelectedValue) == Convert.ToInt32(TipoRequisicaoEstoqueEnum.Transferencia));
    }
}
