using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Drawing;
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

public partial class frmPedidoAquisicaoAprovacao : MarinhaPageBase
{
    #region Private Member
    [TransientPageState]
    protected PedidoAquisicao _pedido;
    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.rbAprovar.CheckedChanged += new EventHandler(rbAprovar_CheckedChanged);
        this.rbRecusar.CheckedChanged += new EventHandler(rbAprovar_CheckedChanged);
        this.rbRetornarFinanceiro.CheckedChanged += new EventHandler(rbAprovar_CheckedChanged);        
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillPage();
            if (Request["ID_Pedido"] != null)
            {
                _pedido = PedidoAquisicao.Get(Convert.ToInt32(Request["ID_Pedido"]));
                dgItem.DataSource = _pedido.Itens;

                ucHistorico.DataSource = _pedido.Historico;
                
                DataBind();
                
                //if(_pedido.Status.StatusPedidoAquisicaoEnum != StatusPedidoAquisicaoEnum.AguardandoAprovacoes)
                    rbRetornarFinanceiro.Visible = false;
                    
                trRequerLicitacao.Visible = _pedido.RequerLicitacao;
            }
            
            Anthem.AnthemClientMethods.Redirect("frmPedidoAquisicaoAprovacaoPesquisa.aspx", btnVoltar);
            Anthem.AnthemClientMethods.Popup(btnImprimir, "fchPedidoAquisicao.aspx?id_pedido=" + _pedido.ID.ToString(),
            false, false, false, true, true, true, true, 10, 30, 700, 550, false);
        }
    }

    private void FillPage()
    {
        
    }

    #endregion


    void dlHistorico_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            HistoricoPedidoAquisicao historico = (HistoricoPedidoAquisicao)e.Item.DataItem;
            if (historico.StatusPosterior.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.Reprovado
                //|| (historico.StatusPosterior.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.AguardandoParecerAgenteFinanceiro
                //    && historico.StatusAnterior.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.AguardandoAprovacoes)
                )
                e.Item.ForeColor = Color.Red;
        }
    }
    
    void rbAprovar_CheckedChanged(object sender, EventArgs e)
    {
        
        Anthem.AnthemClientMethods.ShowHide(trNumeroEmpenho, _pedido.Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.AguardandoEmpenho
        && rbAprovar.Checked);

    }

    private void btnSalvar_Click(object sender, EventArgs e)
    {
        if (rbAprovar.Checked)
        {
            if (_pedido.Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.AguardandoEmpenho)
                _pedido.Manager.EmitirParecerFinanceiro(
                    ID_Servidor,
                    PageReader.ReadString(txtJustificativa),                    
                    txtNumeroEmpenho.Text);
            else
                _pedido.Manager.IrParaProximoStatus(ID_Servidor, PageReader.ReadString(txtJustificativa));
        }
        else if (rbRecusar.Checked)
        {
            _pedido.Manager.Reprovar(ID_Servidor, txtJustificativa.Text);
        }
        else if (rbRetornarFinanceiro.Checked)
        {
            _pedido.Manager.RetornarFinanceiro(ID_Servidor, txtJustificativa.Text);
        }
        Anthem.AnthemClientMethods.Redirect("frmPedidoAquisicaoAprovacaoPesquisa.aspx");
    }

  
   
}
