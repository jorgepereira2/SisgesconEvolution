using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Text;
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
using System.Linq;

public partial class frmPedidoServicoAtividadeSecundariaPendente : SortingPageBase
{
    
    protected PedidoServicoAtividadeSecundaria _pedido
    {
        get { return Session["frmPedidoServicoAtividadeSecundariaPendente.IPedido"] == null ? null : (PedidoServicoAtividadeSecundaria)Session["frmPedidoServicoAtividadeSecundariaPendente.IPedido"]; }
        set { Session["frmPedidoServicoAtividadeSecundariaPendente.IPedido"] = value; }
    }
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
		this.RegisterSortingControl(this.gvPesquisa);
        this.gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
        gvPesquisa.RowEditing += new GridViewEditEventHandler(gvPesquisa_RowEditing);
        gvPesquisa.RowUpdating += new GridViewUpdateEventHandler(gvPesquisa_RowUpdating);
        ucMessageBox.MessageBoxClose += new UserControls_MessageBox.MessageBoxEventHandler(ucMessageBox_MessageBoxClose);
        ucRecusarEtapa.PedidoRecusado += new EventHandler(ucRecusarEtapa_PedidoRecusado);
        btnFiltrar.Click += new EventHandler(btnFiltrar_Click);
        //gvPesquisa.RowCommand += new GridViewCommandEventHandler(gvPesquisa_RowCommand);
        //ucAguardandoPagamento.MensagemInformada += new EventHandler(ucAguardandoPagamento_MensagemInformada);
    }

    

    void btnFiltrar_Click(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);
            this.SortField = "ID";
            this.SortDirection = "ASC";
            Util.FillDropDownList(ddlStatus, StatusPedidoServicoAtividadeSecundaria.List(), "Todos");
            //Bind();
        }
    }
    #endregion  
    
   
    
    #region Recusar
    void ucRecusarEtapa_PedidoRecusado(object sender, EventArgs e)
    {
        _pedido.Recusar(this.ID_Servidor, ucRecusarEtapa.Justificativa);
        ucRecusarEtapa.Close();
        Bind();
    }

    void gvPesquisa_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.RowIndex].Value);
       
       _pedido = PedidoServicoAtividadeSecundaria.Get(id);
       
        ucRecusarEtapa.Show();
    }
    #endregion

   

    void gvPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.NewEditIndex].Value);
        StatusPedidoServicoAtividadeSecundariaEnum status = (StatusPedidoServicoAtividadeSecundariaEnum)
            Convert.ToInt32(((Label) gvPesquisa.Rows[e.NewEditIndex].FindControl("lblID_Status")).Text);

        _pedido = PedidoServicoAtividadeSecundaria.Get(id);


        if (_pedido.Status.StatusPedidoServicoAtividadeSecundariaEnum == StatusPedidoServicoAtividadeSecundariaEnum.AguardandoPagamento)
        {
            ucMessageBox.Show("Confirma Pagamento?", null);
            //ucAguardandoPagamento.Show();
        }
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnEditar = (LinkButton)e.Row.FindControl("btnEditar");
            PedidoServicoAtividadeSecundaria pedido = (PedidoServicoAtividadeSecundaria)e.Row.DataItem;
            string address = "";
            if (pedido.Status.StatusPedidoServicoAtividadeSecundariaEnum == StatusPedidoServicoAtividadeSecundariaEnum.AguardandoPagamento)
            {
                btnEditar.Text = "Finalizar";

            }

            if (address != "")
                Anthem.AnthemClientMethods.Redirect(address, btnEditar);
           

            e.Row.Cells[4].Attributes.Add("onmouseover", string.Format("Tip('{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                GetTextoComentarios(pedido)));

            
            //HtmlAnchor lnkFaturamento = (HtmlAnchor)e.Row.FindControl("lnkFaturamento");
            //LinkButton btnRecalcular = (LinkButton)e.Row.FindControl("btnRecalcular");

            //if (pedido.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.EmitirFaturamento)
            //{

            //    Anthem.AnthemClientMethods.Popup(lnkFaturamento, "fchFaturamentoPSM.aspx?id_pedido=" + pedido.ID, false, false, false, true, true, true, true, 10, 40, 700, 520, false);
            //}
            //else
            //{
            //    lnkFaturamento.Visible = btnRecalcular.Visible = false;
            //}
            HtmlAnchor lnkDetalhes = (HtmlAnchor)e.Row.FindControl("lnkDetalhes");
            Anthem.AnthemClientMethods.Popup(lnkDetalhes, "fchPedidoServicoAtividadeSecundaria.aspx?id_pedido=" + pedido.ID, false, false, false, true, true, true, true, 10, 40, 700, 520, false);
        }
    }

    public static string GetTextoComentarios(PedidoServicoAtividadeSecundaria ps)
    {
        StringBuilder str = new StringBuilder();
        
        str.Append("<b>Comentários:</b><br>");
        for (int i = ps.Historico.Count - 1; i >= 0; i--)
        {
            HistoricoPedidoServicoAtividadeSecundaria historico = ps.Historico[i];
            if (!string.IsNullOrEmpty(historico.JustificativaRecusa))
                str.AppendFormat("- {0} ({1}):<br> {2}<br><br>", historico.Servidor.NomeGuerra, historico.Data.ToShortDateString(),
                                 historico.JustificativaRecusa);
        }
        return str.ToString();
    }

    void ucMessageBox_MessageBoxClose(object sender, MessageBoxEventArgs e)
    {
        if(e.Result == MessageBoxResult.Sim)
        {
           if (_pedido.Status.StatusPedidoServicoAtividadeSecundariaEnum == StatusPedidoServicoAtividadeSecundariaEnum.AguardandoPagamento)
           {
               
               _pedido.Finalizar(this.ID_Servidor, "");
           }

           ////else if (_pedido.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.AguardandoAprovaçãoDivProgControle)
           ////{
           ////    ((DelineamentoOrcamento)_pedido).Aprovar(this.ID_Servidor, "");
           ////}
           // else if (_pedido.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.AguardandoMeio)
           // {
           //     ((DelineamentoOrcamento)_pedido).RegistrarChegadaMeio(this.ID_Servidor);
           // }
         
            
            Bind();
            _pedido = null;
        }
    }

    //void ucAguardandoPagamento_MensagemInformada(object sender, EventArgs e)
    //{
    //    _pedido.EfetuarPagamento(this.ID_Servidor, ucAguardandoPagamento.NLPagamento, ucAguardandoPagamento.MensagemIndicacaoRecurso);
    //    ucAguardandoPagamento.Close();
    //    Bind();
    //    _pedido = null;
        
    //}

    private static object GetPropertyValue(object obj, string property)
    {
        System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
        return propertyInfo.GetValue(obj, null);
    }


    protected override void Bind()
    {
        List<PedidoServicoAtividadeSecundaria> list = PedidoServicoAtividadeSecundaria.Select(this.ID_Servidor, Convert.ToInt32(ddlStatus.SelectedValue), txtCodigo.Text);


        if (this.SortDirection == "ASC")
        {
            var query = from enumerable in list
                        orderby GetPropertyValue(enumerable, this.SortField) ascending
                        select enumerable;
            gvPesquisa.DataSource = query;
        }
        else
        {
            var query = from enumerable in list
                        orderby GetPropertyValue(enumerable, this.SortField) descending 
                        select enumerable;
            gvPesquisa.DataSource = query;
        }
        gvPesquisa.DataKeyNames = new string[]{"ID"};
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }  
    
    private string GetDetalhes(IPedido pedido)
    {
        
        StringBuilder s = new StringBuilder();
        s.Append(@"
         Detalhes
            <span >");
        s.Append("<b>Detalhes</b><br><br>");    
        if(pedido is DelineamentoOrcamento)
        {
            s.Append(string.Format("<b>Categoria:</b>{0}", ((DelineamentoOrcamento) pedido).CategoriaServico.Descricao));
        }
       
        
        s.Append("<br></span>");

        return s.ToString();
    }
}
