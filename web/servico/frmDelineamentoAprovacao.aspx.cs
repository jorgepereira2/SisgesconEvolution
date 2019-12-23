using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
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

public partial class frmDelineamentoAprovacao : MarinhaPageBase
{
    #region Private Member
    protected DelineamentoOrcamento _orcamento
    {
        get { return (DelineamentoOrcamento)Session["frmDelineamentoAprovacao._delineamentoOrcamento"]; }
        set { Session["frmDelineamentoAprovacao._delineamentoOrcamento"] = value; }
    }

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        
        this.btnEnviar.Click += BtnEnviar_OnClick;
        dlDelineamentos.ItemDataBound += new DataListItemEventHandler(dlDelineamentos_ItemDataBound);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {


            _orcamento = DelineamentoOrcamento.Get(Convert.ToInt32(Request["id_delineamentoOrcamento"]));
            PopulateFields();
            
            Anthem.AnthemClientMethods.Redirect("frmPedidoServicoPendente.aspx", btnVoltar);
           
        }
    }

	
  

    private void PopulateFields()
    {
        lblCodigo.Text = _orcamento.CodigoComAno;
        lblDataEmissao.Text = ObjectReader.ReadDate(_orcamento.DataEmissao);
        lblStatus.Text = _orcamento.Status.Descricao;
        lblGerente.Text = _orcamento.ServidorGerente.NomeCompleto;

        Anthem.AnthemClientMethods.Popup(lnkDetalhes, "fchPedidoServico.aspx?id_orcamento=" + _orcamento.ID.ToString(),
              false, false, false, true, true, true, true, 10, 40, 700, 520, false);


        dlDelineamentos.DataSource = _orcamento.Delineamentos;
        dlDelineamentos.DataKeyField = "ID";
        dlDelineamentos.DataBind();
    }


    void dlDelineamentos_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataGrid dgDelineamento = (DataGrid) e.Item.FindControl("dgDelineamento");
            DataGrid dgServicoMaterial = (DataGrid)e.Item.FindControl("dgServicoMaterial");

            DelineamentoOficina delineamentoOficina = (DelineamentoOficina) e.Item.DataItem;
            dgDelineamento.DataSource = _orcamento.ItensDelineamento.Where(i => i.ServidorDelineamento.ID == delineamentoOficina.Servidor.ID);
            dgDelineamento.DataBind();

            dgServicoMaterial.DataSource = _orcamento.ItensOrcamento.Where(i => i.ServidorDelineamento.ID == delineamentoOficina.Servidor.ID);
            dgServicoMaterial.DataBind();
        }
    }
    #endregion

    #region Events 
    
    private void BtnEnviar_OnClick(object sender, EventArgs e)
    {

        foreach (DataListItem item in dlDelineamentos.Items)
        {
            if(item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                int id = Convert.ToInt32(dlDelineamentos.DataKeys[item.ItemIndex]);

                DelineamentoOficina delineamentoOficina = _orcamento.Delineamentos.Where(d => d.ID == id).First();

                RadioButton rbAprovar = (RadioButton) item.FindControl("rbAprovar");
                RadioButton rbrecusar = (RadioButton)item.FindControl("rbRecusar");
                TextBox txtJustificativa = (TextBox) item.FindControl("txtJustificativa");

                if (!rbAprovar.Checked && !rbrecusar.Checked)
                {
                    ShowMessage("Todos os delineamentos devem ser aprovados ou recusados.");
                    return;
                }
                _orcamento.Delineamentos.Where(d => d.ID == id).First().Justificativa = txtJustificativa.Text;
                _orcamento.Delineamentos.Where(d => d.ID == id).First().FlagRecusado = rbrecusar.Checked;
            }
        }

        _orcamento.AprovarDelineamento(this.ID_Servidor);
        Anthem.AnthemClientMethods.Redirect("frmPedidoServicoPendente.aspx");
    }

   
    #endregion
}
