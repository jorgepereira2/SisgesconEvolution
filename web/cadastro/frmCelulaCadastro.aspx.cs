using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;

public partial class frmCelulaCadastro : SortingPageBase
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

    private void DgCadastro_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Celula celula = (Celula) e.Item.DataItem;
            Label lblDescricao = (Label) e.Item.FindControl("lblDescricao");
            if(celula.TipoCelula == TipoCelula.Departamento)
            {
                e.Item.Font.Bold = true;
            }
            else if (celula.TipoCelula == TipoCelula.Divisao)
            {
                lblDescricao.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblDescricao.Text;
            }
            else if (celula.TipoCelula == TipoCelula.Secao)
            {
                lblDescricao.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblDescricao.Text;
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
            Celula celula = Celula.Get(Convert.ToInt32(e.Data));
            celula.Delete();
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
		List<Celula> list = Celula.Select();
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
                Anthem.TextBox txtDescricao = (Anthem.TextBox)e.Item.FindControl("txtDescricaoNovo");
                Anthem.TextBox txtCodigo = (Anthem.TextBox)e.Item.FindControl("txtCodigoNovo");
                Anthem.TextBox txtFinalidade = (Anthem.TextBox)e.Item.FindControl("txtFinalidadeNovo");
				Anthem.CheckBox chkFlagAtivo = (Anthem.CheckBox)e.Item.FindControl("chkFlagAtivoNovo");
                Anthem.CheckBox chkFlagCentroCusto = (Anthem.CheckBox)e.Item.FindControl("chkFlagCentroCustoNovo");
                Anthem.CheckBox chkFlagOficina = (Anthem.CheckBox)e.Item.FindControl("chkFlagOficinaNovo");
                Anthem.CheckBox chkFlagMergulho = (Anthem.CheckBox)e.Item.FindControl("chkFlagMergulhoNovo");
				
                Celula celula = new Celula(); 
                celula.Descricao = txtDescricao.Text;
            	celula.FlagAtivo = chkFlagAtivo.Checked;
                celula.FlagCentroCusto = chkFlagCentroCusto.Checked;
                celula.FlagOficina = chkFlagOficina.Checked;
                celula.FlagMergulho = chkFlagMergulho.Checked;
                celula.Finalidade = PageReader.ReadString(txtFinalidade);
                celula.Codigo = txtCodigo.Text;
				celula.Save();

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
            Anthem.TextBox txtDescricao = (Anthem.TextBox)e.Item.FindControl("txtDescricao");
            Anthem.TextBox txtCodigo = (Anthem.TextBox)e.Item.FindControl("txtCodigo");
            Anthem.TextBox txtFinalidade = (Anthem.TextBox)e.Item.FindControl("txtFinalidade");
            Anthem.CheckBox chkFlagAtivo = (Anthem.CheckBox)e.Item.FindControl("chkFlagAtivo");
            Anthem.CheckBox chkFlagCentroCusto = (Anthem.CheckBox)e.Item.FindControl("chkFlagCentroCusto");
            Anthem.CheckBox chkFlagOficina = (Anthem.CheckBox)e.Item.FindControl("chkFlagOficina");
            Anthem.CheckBox chkFlagMergulho = (Anthem.CheckBox)e.Item.FindControl("chkFlagMergulho");
			
            int id = Convert.ToInt32(dgCadastro.DataKeys[e.Item.ItemIndex]);

			Celula celula = Celula.Get(id);
            celula.Descricao = txtDescricao.Text;
        	celula.FlagAtivo = chkFlagAtivo.Checked;
            celula.FlagCentroCusto = chkFlagCentroCusto.Checked;
            celula.FlagOficina = chkFlagOficina.Checked;
            celula.FlagMergulho = chkFlagMergulho.Checked;
            celula.Finalidade = PageReader.ReadString(txtFinalidade);
            celula.Codigo = txtCodigo.Text;
			celula.Save();
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
		Celula celula = Celula.Get(id);
		celula.Delete();
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
