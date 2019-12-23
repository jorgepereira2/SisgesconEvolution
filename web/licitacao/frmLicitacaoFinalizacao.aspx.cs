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

public partial class frmLicitacaoFinalizacao : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected Licitacao _licitacao;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnFinalizar.Click += new EventHandler(btnFinalizar_Click);
        this.dgItem.ItemDataBound += new DataGridItemEventHandler(dgItem_ItemDataBound);
        btnSalvar.Click += new EventHandler(btnSalvar_Click);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {
        	FillPage();
            if (Request["ID_Licitacao"] != null)
            {
                _licitacao = Licitacao.Get(Convert.ToInt32(Request["ID_Licitacao"]));
                PopulateFields();
            }
            else
            {
                _licitacao = new Licitacao();
            }
            Anthem.AnthemClientMethods.Redirect("frmLicitacaoFinalizacaoPesquisa.aspx", btnVoltar);
           // Anthem.AnthemClientMethods.Popup(btnNovoFornecedor, "../cadastro/frmFornecedorCadastro.aspx?popup=true", false, false, false, true, true, true,
           // true, 10, 50, 770, 500, false);
            
        }
    }

    private void FillPage()
	{
	}

    #endregion

    private void PopulateFields()
    {
        txtDataPregao.Text = ObjectReader.ReadDate(_licitacao.DataPregao);
        txtNumeroPregao.Text = _licitacao.NumeroPregao;

        pnLicitacao.DataBind();
        dgItem.DataSource = _licitacao.Itens;
        dgItem.DataKeyField = "ID";
        dgItem.DataBind();
    }

    private void FillObject()
    {
        _licitacao.DataPregao = PageReader.ReadDate(txtDataPregao);
        _licitacao.NumeroPregao = txtNumeroPregao.Text;

        foreach (DataGridItem item in dgItem.Items)
        {
            if(item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                DropDownList ddlContrato = (DropDownList)item.FindControl("ddlContrato");
                Anthem.NumericTextBox txtValorFinal = (Anthem.NumericTextBox)item.FindControl("txtValorFinal");
                //Anthem.TextBox txtNumeroContratoAta = (Anthem.TextBox)item.FindControl("txtNumeroContratoAta");

                if (ddlContrato.SelectedValue != "0" && txtValorFinal.Text.Trim() != "")
                {
                    int id = Convert.ToInt32(dgItem.DataKeys[item.ItemIndex]);
                    LicitacaoItem itemLicitacao = _licitacao.Itens.Find(id);

                    itemLicitacao.Contrato = LicitacaoContrato.Get(Convert.ToInt32(ddlContrato.SelectedValue));
                    itemLicitacao.Fornecedor = itemLicitacao.Contrato.Fornecedor;
                    itemLicitacao.ValorFinalPregao = Convert.ToDecimal(txtValorFinal.Text);
                  //  itemLicitacao.NumeroContratoAta = PageReader.ReadString(txtNumeroContratoAta);
                }
            }
        }
    }

    private Dictionary<int, string> _listContrato;
    void dgItem_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            DropDownList ddlContrato = (DropDownList)e.Item.FindControl("ddlContrato");
            TextBox txtNumeroContratoAta = (TextBox)e.Item.FindControl("txtNumeroContratoAta");

            if (_listContrato == null)
                _listContrato = _licitacao.ListContratos();

            Util.FillDropDownList(ddlContrato, _listContrato, ESCOLHA_OPCAO);
            Anthem.NumericTextBox txtValorFinal = (Anthem.NumericTextBox)e.Item.FindControl("txtValorFinal");

            LicitacaoItem item = (LicitacaoItem) e.Item.DataItem;
            ddlContrato.SelectedValue = ObjectReader.ReadID(item.Contrato);
            if (item.ValorFinalPregao > 0)
                txtValorFinal.Text = ObjectReader.ReadDecimal(item.ValorFinalPregao);

            //txtNumeroContratoAta.Text = item.NumeroContratoAta;

        }
    }
    
    #region Events 
    void btnFinalizar_Click(object sender, EventArgs e)
    {
        FillObject();
        _licitacao.Finalizar();	
		ShowSuccessMessage();
    }

    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _licitacao.SalvarItens();
        ShowSuccessMessage();
    }
    #endregion
    
    //[Anthem.Method]
    //public void FornecedorAdicionado(string id, string razaoSocial)
    //{
    //    foreach (DataGridItem item in dgItem.Items)
    //    {
    //        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
    //        {
    //            Anthem.DropDownList ddlFornecedor = (Anthem.DropDownList)item.FindControl("ddlFornecedor");
    //            ddlFornecedor.Items.Add(new ListItem(razaoSocial, id));
    //            ddlFornecedor.UpdateAfterCallBack = true;    
    //        }
    //    }
    //}
}
