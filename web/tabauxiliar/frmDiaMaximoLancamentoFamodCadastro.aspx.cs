using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;

public partial class frmDiaMaximoLancamentoFamodCadastro : SortingPageBase
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        this.dgCadastro.EditCommand += new DataGridCommandEventHandler(dgCadastro_EditCommand);
        this.dgCadastro.CancelCommand += new DataGridCommandEventHandler(dgCadastro_CancelCommand);
        this.dgCadastro.UpdateCommand += new DataGridCommandEventHandler(dgCadastro_UpdateCommand);
        this.dgCadastro.ItemCommand += new DataGridCommandEventHandler(dgCadastro_ItemCommand);
        this.dgCadastro.ItemDataBound += new DataGridItemEventHandler(dgCadastro_ItemDataBound);
		this.RegisterSortingControl(dgCadastro);
        dgCadastro.DeleteCommand += new DataGridCommandEventHandler(dgCadastro_DeleteCommand);
        ucMessageBox.MessageBoxClose += new UserControls_MessageBox.MessageBoxEventHandler(ucMessageBox_MessageBoxClose);
    }

    void dgCadastro_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlMes = (DropDownList)e.Item.FindControl("ddlMes");
            DropDownList ddlAno = (DropDownList)e.Item.FindControl("ddlAno");

            Util.FillDropDownList(ddlMes, DateTimeManager.Meses());
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(DateTime.Today.Year - 5, DateTime.Today.Year + 1));

            if(e.Item.ItemType == ListItemType.Footer)
            {
                ddlAno.SelectedValue = DateTime.Today.Year.ToString();
            }
            else
            {
                DiaMaximoLancamentoFamod dia = (DiaMaximoLancamentoFamod) e.Item.DataItem;
                ddlAno.SelectedValue = dia.Ano.ToString();
                ddlMes.SelectedValue = dia.Mes.ToString();
            }
        }
    }

  

    void dgCadastro_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        ucMessageBox.Show("Deseja excluir este registro?", dgCadastro.DataKeys[e.Item.ItemIndex]);
    }

    void ucMessageBox_MessageBoxClose(object sender, MessageBoxEventArgs e)
    {
        if(e.Result == MessageBoxResult.Sim)
        {
            DiaMaximoLancamentoFamod d = DiaMaximoLancamentoFamod.Get(Convert.ToInt32(e.Data));
            d.Delete();
            Bind();
        }
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Bind();
			//RegisterDeleteScript();
        }
    }

    #endregion

    #region Bind
    protected override void Bind()
    {
        List<DiaMaximoLancamentoFamod> list = DiaMaximoLancamentoFamod.Select();
		this.Sort(list);	

		dgCadastro.DataSource = list; 
        dgCadastro.DataKeyField = "ID";
        dgCadastro.DataBind();
        dgCadastro.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    #endregion

    #region DataGrid

    void dgCadastro_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                TextBox txtData = (TextBox)e.Item.FindControl("txtData");
                DropDownList ddlMes = (DropDownList)e.Item.FindControl("ddlMes");
                DropDownList ddlAno = (DropDownList)e.Item.FindControl("ddlAno");

                DiaMaximoLancamentoFamod dia = new DiaMaximoLancamentoFamod(); 
                dia.Data = Convert.ToDateTime(txtData.Text);
                dia.Mes = Convert.ToInt32(ddlMes.SelectedValue);
                dia.Ano = Convert.ToInt32(ddlAno.SelectedValue);
				dia.Save();

                Bind();
                dgCadastro.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    void dgCadastro_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            TextBox txtData = (TextBox)e.Item.FindControl("txtData");
            DropDownList ddlMes = (DropDownList)e.Item.FindControl("ddlMes");
            DropDownList ddlAno = (DropDownList)e.Item.FindControl("ddlAno");

            int id = Convert.ToInt32(dgCadastro.DataKeys[e.Item.ItemIndex]);
            DiaMaximoLancamentoFamod dia = DiaMaximoLancamentoFamod.Get(id);
            dia.Data = Convert.ToDateTime(txtData.Text);
            dia.Mes = Convert.ToInt32(ddlMes.SelectedValue);
            dia.Ano = Convert.ToInt32(ddlAno.SelectedValue);
            dia.Save();
			dgCadastro.EditItemIndex = -1;
            Bind();
        }
        catch (Exception ex)
        {
            Anthem.AnthemClientMethods.Alert(ex.Message, this);
        }
    }

    void dgCadastro_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgCadastro.EditItemIndex = -1;
        dgCadastro.ShowFooter = false;
        Bind();
    }

    void dgCadastro_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgCadastro.EditItemIndex = e.Item.ItemIndex;
        Bind();
    }

	[Anthem.Method]
	public void Excluir(int id)
	{
        DiaMaximoLancamentoFamod d = DiaMaximoLancamentoFamod.Get(id);
        d.Delete();
        Bind();
	}

    void btnNovo_Click(object sender, EventArgs e)
    {
        dgCadastro.ShowFooter = true;
        Bind();
        dgCadastro.UpdateAfterCallBack = true;
    }
    #endregion
}
