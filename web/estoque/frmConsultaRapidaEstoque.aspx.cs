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

public partial class frmConsultaRapidaEstoque : PageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
    }

    void btnPesquisar_Click(object sender, EventArgs e)
    {
        ServicoMaterial material = ServicoMaterial.GetByCodigo(txtCodigoBarras.Text, TipoServicoMaterial.Material);
        if (material != null)
        {
            lblDescricao.Text = material.Descricao;
            lblCodigoInterno.Text = material.CodigoInterno;
            lblAtivo.Text = material.FlagAtivo ? "Sim" : "Não";
            lblClasse.Text = material.ClasseServicoMaterial.Descricao;
            lblSubClasse.Text = material.SubClasseServicoMaterial.Descricao;
            lblCodigoSiasg.Text = material.CodigoSiasg;
            lblDescricaoSingra.Text = material.DescricaoSingra;
            lblFabricante.Text = material.Fabricante != null ? material.Fabricante.Descricao : "";
            lblNumeroReferencia.Text = material.NumeroReferencia;
            lblOrigem.Text = Util.GetDescription(material.OrigemMaterial);
            lblQuantidadeMinima.Text = material.GetQuantidadeMinima((OrigemMaterial) Convert.ToInt32(ddlOrigemMaterial.SelectedValue)).ToString();
            lblQuantidadeMaxima.Text = material.GetQuantidadeMaxima((OrigemMaterial)Convert.ToInt32(ddlOrigemMaterial.SelectedValue)).ToString();
            lblSJB.Text = material.SJB.ToString();
            lblUnidade.Text = material.Unidade.Descricao;

            QuantidadeEstoque quantidade =
                MovimentoEstoque.GetQuantidadeEstoque(material.ID, Convert.ToInt32(ddlOrigemMaterial.SelectedValue));

            lblQuantidadeEstoque.Text = quantidade.QuantidadeAtual.ToString();


            dgLocalizacao.DataSource = material.Localizacoes;
            dgLocalizacao.DataBind();
        }

        pnMaterial.Visible = material != null;
        pnMensagem.Visible = material == null;

        pnMensagem.UpdateAfterCallBack = true;
        pnMaterial.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.SetFocus(txtCodigoBarras);
        Anthem.Manager.AddScriptForClientSideEval(string.Format("document.getElementById('{0}').select();", txtCodigoBarras.ClientID));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);                        
			Anthem.AnthemClientMethods.SetFocus(txtCodigoBarras);

            Util.FillDropDownList(ddlOrigemMaterial, typeof(OrigemMaterial));
            ddlOrigemMaterial.Items.Remove(ddlOrigemMaterial.Items.FindByValue(OrigemMaterial.Obtencao.GetHashCode().ToString()));
            ddlOrigemMaterial.SelectedValue = OrigemMaterial.PEP.GetHashCode().ToString();
        }
    }
    #endregion  
    
    
}
