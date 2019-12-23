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

public partial class frmPedidoServicoMergulhoPendente : SortingPageBase
{
    
    protected PedidoServicoMergulho _pedido
    {
        get { return Session["frmPedidoServicoMergulhoPendente.IPedido"] == null ? null : (PedidoServicoMergulho)Session["frmPedidoServicoMergulhoPendente.IPedido"]; }
        set{ Session["frmPedidoServicoMergulhoPendente.IPedido"] = value;}
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
        gvPesquisa.RowCommand += new GridViewCommandEventHandler(gvPesquisa_RowCommand);
        ucAguardandoPagamento.MensagemInformada += new EventHandler(ucAguardandoPagamento_MensagemInformada);
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
            Util.FillDropDownList(ddlStatus, StatusPedidoServicoMergulho.List(), "Todos");
            Util.FillDropDownList(ddlOficinaDelineamento, Celula.List(null, true), "Todos");
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
       
       _pedido = PedidoServicoMergulho.Get(id);
       
        ucRecusarEtapa.Show();
    }
    #endregion

   

    void gvPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.NewEditIndex].Value);
        StatusPedidoServicoMergulhoEnum status = (StatusPedidoServicoMergulhoEnum)
            Convert.ToInt32(((Label) gvPesquisa.Rows[e.NewEditIndex].FindControl("lblID_Status")).Text);
        
        _pedido = PedidoServicoMergulho.Get(id);
        

        if (_pedido.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.EmitirFaturamento)
        {
            ucMessageBox.Show("Confirma faturamento?", null);
        }
        else if (_pedido.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.AguardandoPagamento)
        {
            ucAguardandoPagamento.Show();
        }

    }

    void gvPesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Recalcular")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            _pedido = PedidoServicoMergulho.Get(id);

            ucMessageBox.Show("Deseja recalcular as taxas do faturamento?", "recalcular");
        }
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnEditar = (LinkButton)e.Row.FindControl("btnEditar");
            PedidoServicoMergulho pedido = (PedidoServicoMergulho)e.Row.DataItem;
            string address = "";
            if (pedido.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.AguardandoDetalhamentoParaSupervidor ||
                pedido.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.AguardandoExecuçãoServiço)
            {
                address = string.Format("frmPedidoServicoMergulhoDetalhamento.aspx?id_pedido={0}", pedido.ID);
            }
            else if (pedido.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.EmitirFaturamento)
            {
                btnEditar.Text = "Registrar Faturamento";
                
            }
            else if (pedido.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.AguardandoPagamento)
            {
                btnEditar.Text = "Finalizar";

            }

            if (address != "")
                Anthem.AnthemClientMethods.Redirect(address, btnEditar);
           

            e.Row.Cells[4].Attributes.Add("onmouseover", string.Format("Tip('{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                GetTextoComentarios(pedido)));

            
            HtmlAnchor lnkFaturamento = (HtmlAnchor)e.Row.FindControl("lnkFaturamento");
            LinkButton btnRecalcular = (LinkButton)e.Row.FindControl("btnRecalcular");

            if (pedido.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.EmitirFaturamento)
            {

                Anthem.AnthemClientMethods.Popup(lnkFaturamento, "fchFaturamentoPSM.aspx?id_pedido=" + pedido.ID, false, false, false, true, true, true, true, 10, 40, 700, 520, false);
            }
            else
            {
                lnkFaturamento.Visible = btnRecalcular.Visible = false;
            }
            HtmlAnchor lnkDetalhes = (HtmlAnchor)e.Row.FindControl("lnkDetalhes");
            Anthem.AnthemClientMethods.Popup(lnkDetalhes, "fchPedidoServicoMergulho.aspx?id_pedido=" + pedido.ID, false, false, false, true, true, true, true, 10, 40, 700, 520, false);
        }
    }

    public static string GetTextoComentarios(PedidoServicoMergulho ps)
    {
        StringBuilder str = new StringBuilder();
        str.AppendFormat("<b>Categoria:</b>{0}<br>", ps.CategoriaServico);

        str.Append("<b>Comentários:</b><br>");
        for (int i = ps.Historico.Count - 1; i >= 0; i--)
        {
            HistoricoPedidoServicoMergulho historico = ps.Historico[i];
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
           if (e.Data != null && e.Data.ToString() == "recalcular")
           {
               _pedido.RecalcularTaxas();
           }
           else if (_pedido.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.EmitirFaturamento)
           {
               
               _pedido.EmitirFaturamento(this.ID_Servidor, "");
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

    void ucAguardandoPagamento_MensagemInformada(object sender, EventArgs e)
    {
        _pedido.EfetuarPagamento(this.ID_Servidor, ucAguardandoPagamento.NLPagamento, ucAguardandoPagamento.MensagemIndicacaoRecurso);
        ucAguardandoPagamento.Close();
        Bind();
        _pedido = null;
        
    }

    private static object GetPropertyValue(object obj, string property)
    {
        System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
        return propertyInfo.GetValue(obj, null);
    }


    protected override void Bind()
    {
        List<PedidoServicoMergulho> list = PedidoServicoMergulho.Select(this.ID_Servidor, Convert.ToInt32(ddlStatus.SelectedValue), txtCodigo.Text, Convert.ToInt32(ddlOficinaDelineamento.SelectedValue));


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
