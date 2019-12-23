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

public partial class frmPedidoObtencaoDocumentoPesquisa : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
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

            Util.FillDropDownList(ddlStatus, StatusPedidoObtencao.List(), "Todos");
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(DateTime.Today.AddYears(-6).Year, DateTime.Today.Year), "Todos");
            ddlAno.SelectedValue = DateTime.Today.Year.ToString();

            Servidor servidor = Servidor.Get(this.ID_Servidor);
            if (servidor.GetFlagPodeFazerPOOutraCelula())
                Util.FillDropDownList(ddlCelula, Celula.List(), "Todas");
            else
                Util.FillDropDownList(ddlCelula, Celula.ListCelulasSubordinadas(this.ID_Servidor, null));
        }
    }
    #endregion  
    
    protected override void Bind()
    {
        List<PedidoObtencao> list = PedidoObtencao.Select(
          txtTexto.Text,
          IsNull(txtDataInicio.Text, DateTime.MinValue),
          IsNull(txtDataFim.Text, DateTime.MinValue),
          Convert.ToInt32(ddlStatus.SelectedValue),
          Convert.ToInt32(ddlCelula.SelectedValue),
          txtAplicacao.Text,
          null,
          null,
          Convert.ToInt32(ddlAno.SelectedValue));

		this.Sort(list);
		gvPesquisa.DataSource = list;
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }  
}
