using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmPedidoServicoOrcamentoFornecedor : MarinhaPageBase
{
    #region private variables
  
    protected DelineamentoOrcamento _delineamentoOrcamento
    {
        get { return (DelineamentoOrcamento)Session["frmPedidoServicoOrcamento._delineamentoOrcamento"]; }
        set{ Session["frmPedidoServicoOrcamento._delineamentoOrcamento"] = value;}
    }
    
    #endregion
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnSalvar.Click +=new EventHandler(btnSalvar_Click);
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {
            Bind();
        }
    }
    #endregion
    
    
    #region Bind
    private void Bind()
    {
		dgMaterial.DataSource = _delineamentoOrcamento.ItensOrcamento; 
        dgMaterial.DataKeyField = "ID";
        dgMaterial.DataBind();
        dgMaterial.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }
    #endregion

   
    void btnSalvar_Click(object sender, EventArgs e)
    {
        Fornecedor fornecedor = Fornecedor.Get(Convert.ToInt32(ucBuscaFornecedor.SelectedValue));
        if(fornecedor == null)
        {
            ShowMessage("Selecione o fornecedor.");
            return;
        }
        foreach (DataGridItem item in dgMaterial.Items)
        {
            if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                CheckBox chkMarcado = (CheckBox) item.FindControl("chkMarcado");
                if (chkMarcado.Checked)
                {
                    int id = Convert.ToInt32(dgMaterial.DataKeys[item.ItemIndex]);
                    PedidoServicoItemOrcamento itemOrcamento = _delineamentoOrcamento.ItensOrcamento.Find(id);
                    itemOrcamento.Fornecedor = fornecedor;
                    itemOrcamento.Save();
                }
            }
        }
        Anthem.Manager.AddScriptForClientSideEval("self.close();");
    }


    protected void chkTodos_CheckChanged(object sender, EventArgs e)
    {
        CheckBox chkTodos = (CheckBox) sender;
        foreach (DataGridItem item in dgMaterial.Items)
        {
            if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                CheckBox chkMarcado = (CheckBox)item.FindControl("chkMarcado");
                chkMarcado.Checked = chkTodos.Checked;
            }
        }
        dgMaterial.UpdateAfterCallBack = true;
    }
}
