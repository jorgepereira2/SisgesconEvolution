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

using Marinha.Business;
using Shared.SessionState;
using Shared.Common;

public partial class frmMotivoCancelamentoPSCadastro : CadastroSimples<MotivoCancelamento>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        this.BeforeSave += new CadastroSimples<MotivoCancelamento>.BeforeSaveEventHandler(frmMotivoCancelamentoPSCadastro_BeforeSave);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #endregion


    void frmMotivoCancelamentoPSCadastro_BeforeSave(object sender, BeforeSaveEventArgs<MotivoCancelamento> e)
    {
        CheckBox chkFlagPS = (CheckBox) e.DataGridItem.FindControl("chkFlagPS");
        CheckBox chkFlagPO = (CheckBox)e.DataGridItem.FindControl("chkFlagPO");
        CheckBox chkFlagAC = (CheckBox)e.DataGridItem.FindControl("chkFlagAC");
        CheckBox chkFlagItemComprador = (CheckBox)e.DataGridItem.FindControl("chkFlagItemComprador");
        CheckBox chkFlagLicitacao = (CheckBox)e.DataGridItem.FindControl("chkFlagLicitacao");
        CheckBox chkFlagOMF = (CheckBox)e.DataGridItem.FindControl("chkFlagOMF");
        CheckBox chkFlagPedidoServicoMergulho = (CheckBox)e.DataGridItem.FindControl("chkFlagPedidoServicoMergulho");
        CheckBox chkFlagPedidoServicoAtividadeSecundaria = (CheckBox)e.DataGridItem.FindControl("chkFlagPedidoServicoAtividadeSecundaria");
        
        e.Object.FlagPS = chkFlagPS.Checked;
        e.Object.FlagPO = chkFlagPO.Checked;
        e.Object.FlagAC = chkFlagAC.Checked;
        e.Object.FlagItemComprador = chkFlagItemComprador.Checked;
        e.Object.FlagLicitacao = chkFlagLicitacao.Checked;
        e.Object.FlagOMF = chkFlagOMF.Checked;
        e.Object.FlagPedidoServicoMergulho = chkFlagPedidoServicoMergulho.Checked;
        e.Object.FlagPedidoServicoAtividadeSecundaria = chkFlagPedidoServicoAtividadeSecundaria.Checked;
    }

    #region Bind
    protected override void Bind()
    {
        BindToGrid(MotivoCancelamento.Select());
    }
    #endregion

 
}
