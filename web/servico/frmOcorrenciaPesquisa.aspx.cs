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

public partial class frmOcorrenciaPesquisa : SortingPageBase
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
			Anthem.AnthemClientMethods.Redirect("frmOcorrenciaCadastro.aspx", btnNovo);
        }
    }
    #endregion  
    
    protected override void Bind()
    {
        Servidor servidor = Servidor.Get(ID_Servidor);

        bool? flagConcluido = null;
        if(ddlStatus.SelectedValue != "0")
            flagConcluido = Convert.ToBoolean(ddlStatus.SelectedValue);
        
        List<DelineamentoOrcamentoOcorrencia> list = DelineamentoOrcamentoOcorrencia.Select(
            servidor.Celula.ID,
            txtTexto.Text,
            IsNull(txtDataInicio.Text, DateTime.MinValue),
            IsNull(txtDataFim.Text, DateTime.MinValue),
            flagConcluido);
		
		gvPesquisa.DataSource = list;
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }  
}
