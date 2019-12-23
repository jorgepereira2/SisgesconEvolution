using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
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

public partial class frmAutorizacaoCompraItemCancelamento : MarinhaPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
        dgItem.CancelCommand += new DataGridCommandEventHandler(dgItem_CancelCommand);
        ucJustificativa.JustificativaInformada += new EventHandler(ucJustificativa_JustificativaInformada);
        
    }

    void ucJustificativa_JustificativaInformada(object sender, EventArgs e)
    {
        PedidoCotacaoItem item = PedidoCotacaoItem.Get(ucJustificativa.ID_Item);
        item.CancelarItemAC(this.ID_Servidor, ucJustificativa.Justificativa);
        ucJustificativa.Close();
        Bind();
    }

    void dgItem_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        ucJustificativa.Show( Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]));
    }


    void btnPesquisar_Click(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);

        }
    }
    #endregion  
    
    protected void Bind()
    {
        if (txtTexto.Text.Trim() == "") return;
        
        AutorizacaoCompra ac = AutorizacaoCompra.Get(Convert.ToInt32(txtTexto.Text));

        
        if (ac != null)
        {
            if(ac.Status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.Finalizado)
            {
                lblMensagem.Text = "Esta AC já foi finalizada.";
                lblMensagem.UpdateAfterCallBack = true;
                pnMensagem.Visible = true;
                dgItem.Visible = false;
            }
            else
            {
                dgItem.DataSource = ac.Itens;
                dgItem.DataKeyField = "ID";
                dgItem.Visible = true;
                pnMensagem.Visible = false;
            }
        }
        else
        {
            dgItem.DataSource = null;
            lblMensagem.Text = "Nenhum registro foi encontrado.";
            pnMensagem.Visible = true;
        }

        dgItem.DataBind();
        dgItem.UpdateAfterCallBack = true;

        Anthem.AnthemClientMethods.ResizeIFrame();
        pnMensagem.UpdateAfterCallBack = true;
    }

  
}
