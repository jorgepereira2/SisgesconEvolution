using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmPlanejamento : MarinhaPageBase
{
    #region private variables

    [TransientPageState] 
    protected DelineamentoOrcamento _delineamentoOrcamento;
    
    #endregion
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnEnviar.Click += new EventHandler(btnEnviar_Click);
    }
   

    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {   
            _delineamentoOrcamento = DelineamentoOrcamento.Get(Convert.ToInt32(Request["id_delineamentoOrcamento"]));
            
            BindDelineamento();
            Anthem.AnthemClientMethods.Redirect("frmPedidoServicoPendente.aspx", btnVoltar);
            Populate();
        }
    }
    
    private  void FillPage()
    {
        
    }

    private void Populate()
    {
        lblCodigo.Text = _delineamentoOrcamento.CodigoComAno;
        lblEquipamento.Text = _delineamentoOrcamento.DescricaoEquipamentos;
        lblDataEmissao.Text = _delineamentoOrcamento.DataEmissao.ToShortDateString();
        lblStatus.Text = _delineamentoOrcamento.Status.Descricao;
        lblCategoria.Text = _delineamentoOrcamento.CategoriaServico.Descricao;
        

        Anthem.AnthemClientMethods.Popup(lnkDetalhes, "fchPedidoServico.aspx?id_pedido=" + _delineamentoOrcamento.PedidoServico.ID.ToString(),
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);
    }
    #endregion
    
    #region Delineamento
    private void BindDelineamento()
    {
        dgDelineamento.DataSource = _delineamentoOrcamento.ItensDelineamento;
        dgDelineamento.DataKeyField = "ID";
        dgDelineamento.DataBind();
        dgDelineamento.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        lblHomemHoraTotal.Text = string.Format("HH Total: {0}", _delineamentoOrcamento.HomemHoraTotal);
        lblHomemHoraTotal.UpdateAfterCallBack = true;
    }
    #endregion

 
    
    void btnEnviar_Click(object sender, EventArgs e)
    {
        Dictionary<int, DateTime> list = new Dictionary<int, DateTime>(_delineamentoOrcamento.ItensDelineamento.Count);
        foreach (DataGridItem gridItem in dgDelineamento.Items)
        {
            if(gridItem.ItemType == ListItemType.AlternatingItem || gridItem.ItemType == ListItemType.Item)
            {
                TextBox txtDataInicio = (TextBox) gridItem.FindControl("txtDataPrevisaoInicio");
                int id = Convert.ToInt32(dgDelineamento.DataKeys[gridItem.ItemIndex]);
                list.Add(id, Convert.ToDateTime(txtDataInicio.Text));
            }
        }
        _delineamentoOrcamento.EfetuarPlanejamento(ID_Servidor, list);
        Anthem.AnthemClientMethods.Redirect("frmPedidoServicoPendente.aspx");
    }
}
