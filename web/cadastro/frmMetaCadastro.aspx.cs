using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Marinha.Business;
using Shared.SessionState;
using ComponentArt.Web.UI;
using Shared.Common;

public partial class frmMetaCadastro : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
        this.dgCadastro.ItemDataBound += new DataGridItemEventHandler(dgCadastro_ItemDataBound);
        this.dgCadastro.EditCommand += new DataGridCommandEventHandler(dgCadastro_EditCommand);
        this.dgCadastro.UpdateCommand += new DataGridCommandEventHandler(dgCadastro_UpdateCommand);
        this.dgCadastro.ItemCommand +=new DataGridCommandEventHandler(dgCadastro_ItemCommand);
        this.dgCadastro.CancelCommand += new DataGridCommandEventHandler(dgCadastro_CancelCommand);
        btnNovo.Click += new EventHandler(btnNovo_Click);
		this.RegisterSortingControl(this.dgCadastro);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(2008, DateTime.Today.Year + 1));
            Util.FillDropDownList(ddlConta, Conta.List());
            Util.FillDropDownList(ddlCelula, Celula.List(), "Todas");
            ddlAno.SelectedValue = DateTime.Today.Year.ToString();
        }
    }

   

   
    #endregion  
    
    protected override void Bind()
    {
        List<Meta> list = Meta.Select(Convert.ToInt32(ddlAno.SelectedValue), Convert.ToInt32(ddlConta.SelectedValue), Convert.ToInt32(ddlCelula.SelectedValue));
		
		this.Sort(list);
		dgCadastro.DataSource = list;
        dgCadastro.DataKeyField = "ID";
        dgCadastro.DataBind();
        dgCadastro.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

       // dgCadastro.Visible = list.Count > 0;
       // pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;

        lblTotal.Text = string.Format("Total: {0:C2}", list.Sum(m => m.Valor * m.Quantidade));
        lblTotal.AutoUpdateAfterCallBack = true;

    }

    void dgCadastro_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgCadastro.EditItemIndex = -1;
        dgCadastro.ShowFooter = false;
        Bind();
    }

    void btnNovo_Click(object sender, EventArgs e)
    {
        dgCadastro.ShowFooter = true;
        Bind();
        dgCadastro.UpdateAfterCallBack = true;
    }

    void dgCadastro_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Anthem.TextBox txtDescricao = (Anthem.TextBox)e.Item.FindControl("txtDescricao");
            Anthem.TextBox txtValor = (Anthem.TextBox)e.Item.FindControl("txtValor");
            DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");
            DropDownList ddlTipoMoeda = (DropDownList)e.Item.FindControl("ddlTipoMoeda");
            Anthem.TextBox txtQuantidade = (Anthem.TextBox)e.Item.FindControl("txtQuantidade");
            Anthem.TextBox txtUnidade = (Anthem.TextBox)e.Item.FindControl("txtUnidade");

            int id = Convert.ToInt32(dgCadastro.DataKeys[e.Item.ItemIndex]);

            Meta meta = Meta.Get(id);
            meta.Descricao = txtDescricao.Text;
            meta.Valor = Convert.ToDecimal(txtValor.Text);
            meta.TipoMoeda = TipoMoeda.Get(Convert.ToInt32(ddlTipoMoeda.SelectedValue));
            meta.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
            meta.Quantidade = Convert.ToInt32(txtQuantidade.Text);
            meta.UnidadeMedida = txtUnidade.Text;
            //meta.Conta = Conta.Get(Convert.ToInt32(ddlConta.SelectedValue));
            //meta.Ano = Convert.ToInt32(ddlAno.SelectedValue);
            meta.Save();
            dgCadastro.EditItemIndex = -1;
            Bind();
        }
        catch (Exception ex)
        {
            Anthem.AnthemClientMethods.Alert(ex.Message, this);
        }
    }

    void dgCadastro_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                Anthem.TextBox txtDescricao = (Anthem.TextBox)e.Item.FindControl("txtDescricao");
                Anthem.TextBox txtValor = (Anthem.TextBox)e.Item.FindControl("txtValor");
                DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");
                DropDownList ddlTipoMoeda = (DropDownList)e.Item.FindControl("ddlTipoMoeda");
                Anthem.TextBox txtQuantidade = (Anthem.TextBox)e.Item.FindControl("txtQuantidade");
                Anthem.TextBox txtUnidade = (Anthem.TextBox)e.Item.FindControl("txtUnidade");

                Meta meta = new Meta();
                meta.Descricao = txtDescricao.Text;
                meta.Valor = Convert.ToDecimal(txtValor.Text);
                meta.TipoMoeda = TipoMoeda.Get(Convert.ToInt32(ddlTipoMoeda.SelectedValue));
                meta.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
                meta.Conta = Conta.Get(Convert.ToInt32(ddlConta.SelectedValue));
                meta.Ano = Convert.ToInt32(ddlAno.SelectedValue);
                meta.Quantidade = Convert.ToInt32(txtQuantidade.Text);
                meta.UnidadeMedida = txtUnidade.Text;
                meta.Save();

                Bind();
                dgCadastro.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    void dgCadastro_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgCadastro.EditItemIndex = e.Item.ItemIndex;
        Bind();
    }

    void dgCadastro_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");
            Util.FillDropDownList(ddlCelula, Celula.List());
            DropDownList ddlTipoMoeda = (DropDownList)e.Item.FindControl("ddlTipoMoeda");
            Util.FillDropDownList(ddlTipoMoeda, TipoMoeda.List());
            //DropDownList ddlAno = (DropDownList)e.Item.FindControl("ddlAno");
            //Util.FillDropDownList(ddlAno, DateTimeManager.Anos(2008, DateTime.Today.Year + 1));

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                Meta meta = (Meta)e.Item.DataItem;
                ddlCelula.SelectedValue = meta.Celula.ID.ToString();
                ddlTipoMoeda.SelectedValue = meta.TipoMoeda.ID.ToString();
            }
        }
        else if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Meta meta = (Meta)e.Item.DataItem;
            DataSet ds = Meta.SelectSaldo(meta.ID, meta.Ano);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string saldo = string.Format( "Total Planejado: {0:C2}<br>Total em Execução: {1:C2}<br>Total Realizado: {2:C2}<br>Saldo: {3:C2}",
                        ds.Tables[0].Rows[0]["ValorPlanejado"], ds.Tables[0].Rows[0]["ValorEmExecucao"], ds.Tables[0].Rows[0]["ValorRealizado"],
                        Convert.ToDecimal(ds.Tables[0].Rows[0]["ValorPlanejado"]) - Convert.ToDecimal(ds.Tables[0].Rows[0]["ValorEmExecucao"]) - Convert.ToDecimal(ds.Tables[0].Rows[0]["ValorRealizado"]));

                e.Item.Cells[0].Attributes.Add("onmouseover", string.Format("Tip('{0}<br>', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                                                                        saldo));
            }
        }
    }

    void btnPesquisar_Click(object sender, EventArgs e)
    {
        Bind();
    }

    [Anthem.Method]
    public void Excluir(int id)
    {
        Meta meta = Meta.Get(id);
        meta.Delete();
        Bind();
    }
}
