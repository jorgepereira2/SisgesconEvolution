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

using Marinha.Business;
using Shared.SessionState;
using Shared.Common;

public partial class frmCustoHHCadastro : SortingPageBase
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.RegisterSortingControl(dgCadastro);
        dgCadastro.EditCommand += new DataGridCommandEventHandler(dgCadastro_EditCommand);
        dgCadastro.ItemCommand += new DataGridCommandEventHandler(dgCadastro_ItemCommand);
        dgCadastro.UpdateCommand += new DataGridCommandEventHandler(dgCadastro_UpdateCommand);
        dgCadastro.CancelCommand += new DataGridCommandEventHandler(dgCadastro_CancelCommand);
        dgCadastro.ItemDataBound += new DataGridItemEventHandler(dgCadastro_ItemDataBound);
        btnNovo.Click += new EventHandler(btnNovo_Click);
        this.Load += new EventHandler(frmCustoHHCadastro_Load);
    }

    void dgCadastro_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlMes = (DropDownList) e.Item.FindControl("ddlMes");
            DropDownList ddlAno = (DropDownList)e.Item.FindControl("ddlAno");

            Util.FillDropDownList(ddlMes, DateTimeManager.Meses());
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(DateTime.Today.Year - 5, DateTime.Today.Year + 1));

            if (e.Item.ItemType == ListItemType.Footer)
                ddlAno.SelectedValue = DateTime.Today.Year.ToString();
            else
            {
                CustoHH custo = (CustoHH) e.Item.DataItem;
                ddlAno.SelectedValue = custo.MesAno.Year.ToString();
                ddlMes.SelectedValue = custo.MesAno.Month.ToString();
            }
        }
    }

    void frmCustoHHCadastro_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
            Bind();
    }

    

    void btnNovo_Click(object sender, EventArgs e)
    {
        dgCadastro.ShowFooter = true;
        dgCadastro.UpdateAfterCallBack = true;
    }

    void dgCadastro_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgCadastro.ShowFooter = false;
        dgCadastro.EditItemIndex = -1;
        Bind();
    }


    void dgCadastro_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            Save(e, true);
        }
    }

    private void dgCadastro_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Save(e, false);
    }

    void Save(DataGridCommandEventArgs e, bool isNew)
    {
        try
        {
            TextBox txtValorCusto = (TextBox)e.Item.FindControl("txtValorCusto");
            DropDownList ddlMes = (DropDownList)e.Item.FindControl("ddlMes");
            DropDownList ddlAno = (DropDownList)e.Item.FindControl("ddlAno");


            CustoHH custo;
            if (isNew)
                custo = new CustoHH();
            else
                custo = CustoHH.Get(Convert.ToInt32(dgCadastro.DataKeys[e.Item.ItemIndex]));

            custo.ValorCusto = Convert.ToDecimal(txtValorCusto.Text);
            custo.MesAno = new DateTime(Convert.ToInt32(ddlAno.SelectedValue), Convert.ToInt32(ddlMes.SelectedValue), 1);

            custo.Save();

            
            dgCadastro.ShowFooter = false;
            dgCadastro.EditItemIndex = -1;
            Bind(); 
        }
        catch (Exception ex)
        {
            Anthem.AnthemClientMethods.Alert(ex.Message, this);
        }
    }

    void dgCadastro_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgCadastro.EditItemIndex = e.Item.ItemIndex;
        Bind();
    }

  
    #endregion

    protected override void Bind()
    {
        List<CustoHH> list = CustoHH.Select();
        this.Sort(list);

        dgCadastro.DataSource = list;
        dgCadastro.DataKeyField = "ID";
        dgCadastro.DataBind();
        dgCadastro.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }
    
}
