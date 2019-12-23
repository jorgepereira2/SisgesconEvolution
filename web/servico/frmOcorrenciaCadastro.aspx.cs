using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
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

public partial class frmOcorrenciaCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected DelineamentoOrcamentoOcorrencia _ocorrencia;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        lnkDetalhes.Click += new EventHandler(lnkDetalhes_Click);
        chkFlagParteEquipamento.CheckedChanged += delegate { FlagParteEquipamentoChanged(); };
        btnImprimir.Click += BtnImprimir_OnClick;   
    }

    private void BtnImprimir_OnClick(object sender, EventArgs e)
    {
        Anthem.AnthemClientMethods.Popup(this, "fchOcorrencia.aspx?id_ocorrencia=" + _ocorrencia.ID.ToString(),
            false, false, false, true, true, true, true, 20, 40, 700, 500);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        	FillPage();
            if (Request["ID_Ocorrencia"] != null)
            {
                _ocorrencia = DelineamentoOrcamentoOcorrencia.Get(Convert.ToInt32(Request["ID_Ocorrencia"]));
                PopulateFields();
            }
            else
            {
                _ocorrencia = new DelineamentoOrcamentoOcorrencia();
            }

           Anthem.AnthemClientMethods.Redirect("frmOcorrenciaPesquisa.aspx", btnVoltar);
            
        }
    }

	private void FillPage()
	{
		Util.FillDropDownList(ddlCelula, Celula.List(null, true));
	}
    #endregion
    
    #region Object
    private void PopulateFields()
    {
        chkFlagServicoTerceiro.Checked = _ocorrencia.FlagServicoTerceiro;
        txtDataInicio.Text = ObjectReader.ReadDate(_ocorrencia.DataInicio);
        txtDescricaoServico.Text = _ocorrencia.DescricaoServico;
        chkFlagParteEquipamento.Checked = _ocorrencia.FlagParteEquipamento;
        txtParteEquipamento.Text = _ocorrencia.DescricaoParteEquipamento;
        txtDataPrevisaoFim.Text = ObjectReader.ReadDate(_ocorrencia.DataPrevisaoFim);
        txtDataFim.Text = ObjectReader.ReadDate(_ocorrencia.DataFim);
        txtDescricaoConclusao.Text = _ocorrencia.DescricaoConclusao;
        ucOrcamento.SelectedValue = _ocorrencia.DelineamentoOrcamento.ID.ToString();
        ucOrcamento.Text = _ocorrencia.DelineamentoOrcamento.CodigoComAno;
        ddlCelula.SelectedValue = ObjectReader.ReadID(_ocorrencia.Celula);
    }

    private void ClearFields()
    {
        chkFlagServicoTerceiro.Checked = true;
        txtDataInicio.Text = "";
        txtDescricaoServico.Text = "";
        chkFlagParteEquipamento.Checked = true;
        txtParteEquipamento.Text = "";
        txtDataPrevisaoFim.Text = "";
        txtDataFim.Text = "";
        txtDescricaoConclusao.Text = "";
        ddlCelula.SelectedIndex = -1;
        //ucOrcamento.Reset();
       
        chkFlagServicoTerceiro.UpdateAfterCallBack = true;
        txtDataInicio.UpdateAfterCallBack = true;
        txtDescricaoServico.UpdateAfterCallBack = true;
        chkFlagParteEquipamento.UpdateAfterCallBack = true;
        txtParteEquipamento.UpdateAfterCallBack = true;
        txtDataPrevisaoFim.UpdateAfterCallBack = true;
        txtDataFim.UpdateAfterCallBack = true;
        txtDescricaoConclusao.UpdateAfterCallBack = true;
        ddlCelula.UpdateAfterCallBack = true;

    }

    private void FillObject()
    {
        _ocorrencia.DelineamentoOrcamento = DelineamentoOrcamento.Get(Convert.ToInt32(ucOrcamento.SelectedValue));
        _ocorrencia.Servidor = Servidor.Get(ID_Servidor);
        _ocorrencia.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
        _ocorrencia.FlagServicoTerceiro = chkFlagServicoTerceiro.Checked;
        _ocorrencia.DataInicio = PageReader.ReadDate(txtDataInicio);
        _ocorrencia.DescricaoServico = PageReader.ReadString(txtDescricaoServico);
        _ocorrencia.FlagParteEquipamento = chkFlagParteEquipamento.Checked;
        _ocorrencia.DescricaoParteEquipamento = PageReader.ReadString(txtParteEquipamento);
        _ocorrencia.DataPrevisaoFim = PageReader.ReadDate(txtDataPrevisaoFim);
        _ocorrencia.DataFim = PageReader.ReadNullableDate(txtDataFim);
        _ocorrencia.DescricaoConclusao = PageReader.ReadString(txtDescricaoConclusao);
        FlagParteEquipamentoChanged();
    }
  
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _ocorrencia.Save();
        ShowSuccessMessage();
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _ocorrencia = new DelineamentoOrcamentoOcorrencia();
    }
    
    private void FlagParteEquipamentoChanged()
    {
        if(chkFlagParteEquipamento.Checked)
            txtParteEquipamento.Visible = true;
        else
        {
            txtParteEquipamento.Visible = false;
            txtParteEquipamento.Text = "";
        }
        txtParteEquipamento.UpdateAfterCallBack = true;
    }

    void lnkDetalhes_Click(object sender, EventArgs e)
    {
        Anthem.AnthemClientMethods.Popup(this, "fchPedidoServico.aspx?id_orcamento=" + ucOrcamento.SelectedValue,
            false, false, false, true, true, true, true, 10, 40, 700, 520);
    }
    #endregion
   
}
