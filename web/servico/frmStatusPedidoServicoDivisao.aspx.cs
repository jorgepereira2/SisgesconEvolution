using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.SessionState;

public partial class frmStatusPedidoServicoDivisao : MarinhaPageBase
{
    #region Private Mambers

    [TransientPageState]
    protected StatusPedidoServico _status;

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
            
            StatusPedidoServicoDivisao divisao = _status.ResponsaveisDivisao.Find(Convert.ToInt32(e.Data));
            _status.ResponsaveisDivisao.Remove(divisao);
            divisao.Delete();
            
            Bind();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            _status = StatusPedidoServico.Get(Convert.ToInt32(Request["id_status"]));
            lblEtapa.Text = _status.Descricao;
            Bind();
        }
        Anthem.AnthemClientMethods.Redirect("frmResponsavelEtapaPesquisa.aspx", btnVoltar);
    }

    #endregion

    #region Bind
    private void Bind()
    {
		dgCadastro.DataSource = _status.ResponsaveisDivisao; 
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
                Anthem.DropDownList ddlCelula = (Anthem.DropDownList)e.Item.FindControl("ddlCelula");

                StatusPedidoServicoDivisao responsavelDivisao = new StatusPedidoServicoDivisao();
                responsavelDivisao.Servidor = Servidor.Get(Convert.ToInt32(ddlServidor.SelectedValue));
                responsavelDivisao.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
                responsavelDivisao.StatusPedidoServico = _status;
                responsavelDivisao.Save();
                _status.ResponsaveisDivisao.Add(responsavelDivisao);
				
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
            Anthem.DropDownList ddlCelula = (Anthem.DropDownList)e.Item.FindControl("ddlCelula");
            Util.FillDropDownList(ddlServidor, Servidor.List(null));
            Util.FillDropDownList(ddlCelula, Celula.List(TipoCelula.Divisao, true));
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
