using System;
using System.Data;
using System.Configuration;
using System.Collections;
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

public partial class frmPedidoObtencaoBusca : SortingPageBase
{

    private OrigemPO _origemPO
    {
        get { return (OrigemPO)Convert.ToInt32(Request["OrigemPO"]); }
    }

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
        this.RegisterSortingControl(this.gvPesquisa);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);

          

            Util.FillDropDownList(ddlStatus, StatusPedidoObtencao.List(), "Todos");

            Servidor servidor = Servidor.Get(this.ID_Servidor);
            if (servidor.GetFlagPodeFazerPOOutraCelula())
                Util.FillDropDownList(ddlCelula, Celula.List(), "Todas");
            else
                Util.FillDropDownList(ddlCelula, Celula.ListCelulasSubordinadas(this.ID_Servidor, null));

            SetTitulo();
        }
    }

    private void SetTitulo()
    {
        if (Request["pm"] == null && _origemPO == OrigemPO.Direto)
            lblTitulo.Text = "Busca de Pedido de Obtenção Direto";
        else if (Request["pm"] == null && _origemPO == OrigemPO.GastoExtraPS)
            lblTitulo.Text = "Busca de Pedido de Obtenção de Gasto Extra PS";
        else if (Request["pm"] != null && _origemPO == OrigemPO.Direto)
            lblTitulo.Text = "Busca de Pedido de Material Direto";
        else if (Request["pm"] != null && _origemPO == OrigemPO.GastoExtraPS)
            lblTitulo.Text = "Busca de Pedido de Material de Gasto Extra PS";
    }
    #endregion

    protected override void Bind()
    {
        TipoPedido? tipoPedido = null;
        if (Request["pm"] == null)
            tipoPedido = TipoPedido.PedidoObtencao;
        else
            tipoPedido = TipoPedido.PedidoMaterial;

        List<PedidoObtencao> list = PedidoObtencao.Select(
            txtTexto.Text,
            IsNull(txtDataInicio.Text, DateTime.MinValue),
            IsNull(txtDataFim.Text, DateTime.MinValue),
            Convert.ToInt32(ddlStatus.SelectedValue),
            Convert.ToInt32(ddlCelula.SelectedValue),
            txtAplicacao.Text,
            tipoPedido,
            _origemPO, 0);
        this.Sort(list);
        gvPesquisa.DataSource = list;
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }
       

    void btnPesquisar_Click(object sender, EventArgs e)
    {
        Bind();
    }
}
