using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;

public partial class frmEncarregadoCelulaCadastro : SortingPageBase
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
		this.RegisterSortingControl(dgCadastro);
		this.dgCadastro.ItemDataBound += DgCadastro_OnItemDataBound;
        dgCadastro.DeleteCommand += new DataGridCommandEventHandler(dgCadastro_DeleteCommand);
        ucMessageBox.MessageBoxClose += new UserControls_MessageBox.MessageBoxEventHandler(ucMessageBox_MessageBoxClose);
    }

    void dgCadastro_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        ucMessageBox.Show("Deseja excluir este registro?", dgCadastro.DataKeys[e.Item.ItemIndex]);
    }

    void ucMessageBox_MessageBoxClose(object sender, MessageBoxEventArgs e)
    {
        if(e.Result == MessageBoxResult.Sim)
        {
            EncarregadoCelula encarregado = EncarregadoCelula.Get(Convert.ToInt32(e.Data));
            encarregado.Delete();
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
        List<EncarregadoCelula> list = EncarregadoCelula.Select();
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
                Save(e, new EncarregadoCelula());

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
            int id = Convert.ToInt32(dgCadastro.DataKeys[e.Item.ItemIndex]);

			EncarregadoCelula encarregado = EncarregadoCelula.Get(id);
            Save(e, encarregado);
            
            dgCadastro.EditItemIndex = -1;
            Bind();
        }
        catch (Exception ex)
        {
            Anthem.AnthemClientMethods.Alert(ex.Message, this);
        }
    }

    private void Save(DataGridCommandEventArgs e, EncarregadoCelula encarregado)
    {
        TextBox txtDataInicio = (TextBox)e.Item.FindControl("txtDataInicio");
        TextBox txtDataFim = (TextBox)e.Item.FindControl("txtDataFim");
        DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");
        DropDownList ddlServidor = (DropDownList)e.Item.FindControl("ddlServidor");

        encarregado.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
        encarregado.Servidor = Servidor.Get(Convert.ToInt32(ddlServidor.SelectedValue));
        encarregado.DataInicio = Convert.ToDateTime(txtDataInicio.Text);
        encarregado.DataFim = PageReader.ReadNullableDate(txtDataFim);
        encarregado.Save();
    }

    private void DgCadastro_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
        {
            DropDownList ddlCelula = (DropDownList) e.Item.FindControl("ddlCelula");
            DropDownList ddlServidor = (DropDownList)e.Item.FindControl("ddlServidor");
            TextBox txtDataInicio = (TextBox)e.Item.FindControl("txtDataInicio");
            TextBox txtDataFim = (TextBox)e.Item.FindControl("txtDataFim");
            
            Util.FillDropDownList(ddlCelula, Celula.List(), ESCOLHA_OPCAO);
            Util.FillDropDownList(ddlServidor, Servidor.List(null), ESCOLHA_OPCAO);
            
            if(e.Item.ItemType == ListItemType.EditItem)
            {
                EncarregadoCelula encarregado = (EncarregadoCelula) e.Item.DataItem;
                ddlCelula.SelectedValue = encarregado.Celula.ID.ToString();
                ddlServidor.SelectedValue = encarregado.Servidor.ID.ToString();
                txtDataInicio.Text = encarregado.DataInicio.ToShortDateString();
                txtDataFim.Text = ObjectReader.ReadDate(encarregado.DataFim);
            }
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

    void btnNovo_Click(object sender, EventArgs e)
    {
        dgCadastro.ShowFooter = true;
        Bind();
        dgCadastro.UpdateAfterCallBack = true;
    }
    #endregion
}
