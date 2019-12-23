using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.SessionState;

public partial class frmStatusPedidoServicoResponsavel : MarinhaPageBase
{
    #region Private Mambers

    [TransientPageState]
    protected StatusPedidoServicoMergulho _status;

    #endregion
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        this.dgCadastro.CancelCommand += new DataGridCommandEventHandler(dgCadastro_CancelCommand);
        this.dgCadastro.ItemCommand += new DataGridCommandEventHandler(dgCadastro_ItemCommand);

        this.dgCadastro.ItemDataBound += new DataGridItemEventHandler(dgCadastro_ItemDataBound);
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
            
            Servidor servidor = _status.Responsaveis.Find(Convert.ToInt32(e.Data));
            _status.Responsaveis.Remove(servidor);
            _status.Save();
            Bind();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            _status = StatusPedidoServicoMergulho.Get(Convert.ToInt32(Request["id_status"]));
            lblEtapa.Text = _status.Descricao;
            Bind();
        }
        Anthem.AnthemClientMethods.Redirect("frmResponsavelEtapaPesquisa.aspx", btnVoltar);
    }

    #endregion

    #region Bind
    private void Bind()
    {
		dgCadastro.DataSource = _status.Responsaveis; 
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
                Anthem.DropDownList ddlServidor = (Anthem.DropDownList)e.Item.FindControl("ddlServidor");

                Servidor servidor = Servidor.Get(Convert.ToInt32(ddlServidor.SelectedValue));
                _status.Responsaveis.Add(servidor);
				_status.Save();

                Bind();
                dgCadastro.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    void dgCadastro_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.Footer)
        {
            Anthem.DropDownList ddlServidor = (Anthem.DropDownList)e.Item.FindControl("ddlServidor");
            Util.FillDropDownList(ddlServidor, Servidor.List(null));
        }
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
    #endregion
}
