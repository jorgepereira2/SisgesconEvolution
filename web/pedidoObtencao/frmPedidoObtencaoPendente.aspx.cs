using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Linq;
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

public partial class frmPedidoObtencaoPendente : SortingPageBase
{
    protected PedidoObtencao _po
    {
        get { return Session["frmPedidoObtencaoPendente._po"] == null ? null : (PedidoObtencao)Session["frmPedidoObtencaoPendente._po"]; }
        set { Session["frmPedidoObtencaoPendente._po"] = value; }
    }

    protected PedidoObtencaoPagamento _pagamento
    {
        get { return Session["frmPedidoObtencaoPendente._pagamento"] == null ? null : (PedidoObtencaoPagamento)Session["frmPedidoObtencaoPendente._pagamento"]; }
        set { Session["frmPedidoObtencaoPendente._pagamento"] = value; }
    }

    #region Initialization

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.RegisterSortingControl(this.gvPesquisa);
        this.gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
        this.gvPesquisa.RowEditing += new GridViewEditEventHandler(gvPesquisa_RowEditing);
        ucMessageBox.MessageBoxClose += new UserControls_MessageBox.MessageBoxEventHandler(ucMessageBox_MessageBoxClose);
        btnFiltrar.Click += new EventHandler(btnFiltrar_Click);

        this.ucNotaEmpenho.NotaInformada += new EventHandler(ucNotaEmpenho_NotaInformada);
        this.ucNotaEmpenho.OperacaoCancelada += new EventHandler(ucNotaEmpenho_OperacaoCancelada);

        this.ucBaixaPO.BaixaInformada += new EventHandler(ucBaixaPO_BaixaInformada);
        this.ucInputBox.TextoInformado += new EventHandler(ucInputBox_TextoInformado);
        ucRecebedorEmpenho.OkClicked += new EventHandler(ucRecebedorEmpenho_OkClicked);
        this.gvPesquisa.RowUpdating += new GridViewUpdateEventHandler(gvPesquisa_RowUpdating);
        ucRecusarEtapa.PedidoRecusado += new EventHandler(ucRecusarEtapa_PedidoRecusado);
        ucDefinicaoFinanceiraRelator.DefinicaoInformada += new EventHandler(ucDefinicaoFinanceiraRelator_DefinicaoInformada);
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            IPedidoObtencao pedido = (IPedidoObtencao)e.Row.DataItem;

            if (pedido.Status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.Reprovado)
            {
                e.Row.ForeColor = Color.Red;
                e.Row.ToolTip = pedido.UltimoHistorico.Justificativa;

                e.Row.Attributes.Add("onmouseover", string.Format("Tip('<b>Justificativa:</b><br><br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",

                pedido.UltimoHistorico.Justificativa));
            }

            if (pedido.UltimoHistorico != null && pedido.UltimoHistorico.FlagReprovado)
            {
                e.Row.ForeColor = Color.Red;
                e.Row.ToolTip = pedido.UltimoHistorico.Justificativa;

                e.Row.Attributes.Add("onmouseover", string.Format("Tip('<b>Justificativa:</b><br><br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                    pedido.UltimoHistorico.Justificativa));
            }

            LinkButton btnImpressao = (LinkButton)e.Row.FindControl("btnImpressao");
            LinkButton btnVisualizar = (LinkButton)e.Row.FindControl("btnVisualizar");

            string address = (pedido.Status.ID < 190) ? "fchPedidoObtencaoAssinatura.aspx?id_pedido=" + pedido.ID_PedidoObtencao : "fchPedidoObtencaoCompleto2.aspx?id_pedido=" + pedido.ID_PedidoObtencao;

            //if (pedido.Status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.ImprimirEnviarIntendência)
            //{
            //    address = "fchPedidoObtencaoAssinatura.aspx?id_pedido=" + pedido.ID_PedidoObtencao;
            //    btnVisualizar.Text = "Imprimir";
            //    LinkButton btnDocumentos = (LinkButton)e.Row.FindControl("btnDocumentos");
            //    Literal litBr = (Literal)e.Row.FindControl("litBr");
            //    btnDocumentos.Visible = true;
            //    litBr.Visible = true;
            //    Anthem.AnthemClientMethods.Popup(btnDocumentos, "frmPedidoObtencaoDocumento.aspx?id_pedido=" + pedido.ID_PedidoObtencao, false, false, false, true, true, true, true, 10, 40, 700, 520, false);
            //    LinkButton btnEditar = (LinkButton)e.Row.FindControl("btnEditar");
            //    btnEditar.Text = "Enviar";
            //}

            Anthem.AnthemClientMethods.Popup(btnVisualizar, address, false, false, false, true, true, true, true, 10, 40, 700, 520, false);
            Anthem.AnthemClientMethods.Popup(btnImpressao, "fchPedidoObtencao.aspx?id_pedido=" + pedido.ID_PedidoObtencao, false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            e.Row.Cells[4].Attributes.Add("onmouseover", string.Format("Tip('{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);", GetTextoComentarios(pedido)));
        }
    }

    private static string GetTextoComentarios(IPedidoObtencao po)
    {
        StringBuilder str = new StringBuilder();

        str.Append("<b>Comentários:</b><br>");
        for (int i = po.Historico.Count - 1; i >= 0; i--)
        {
            HistoricoPedidoObtencao historico = po.Historico[i];
            if (!string.IsNullOrEmpty(historico.Justificativa))
                str.AppendFormat("- {0} ({1}):<br> {2}<br><br>", historico.Servidor.NomeGuerra, historico.Data.ToShortDateString(),
                                 historico.Justificativa);
        }
        return str.ToString();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);

            Util.FillDropDownList(ddlStatus, StatusPedidoObtencao.List(), "Todos");

            this.SortField = "ID";
            this.SortDirection = "ASC";
            Bind();
        }
    }
    #endregion

    private static object GetPropertyValue(object obj, string property)
    {
        System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
        return propertyInfo.GetValue(obj, null);
    }

    protected override void Bind()
    {
        List<IPedidoObtencao> list = PedidoObtencao.Select(this.ID_Servidor, Convert.ToInt32(ddlStatus.SelectedValue));

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

        //this.Sort(list);
        //gvPesquisa.DataSource = list;
        gvPesquisa.DataKeyNames = new string[] { "ID", "ID_PedidoObtencao" };
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;

        _pagamento = null;
        _po = null;
    }

    void btnFiltrar_Click(object sender, EventArgs e)
    {
        Bind();
    }

    void gvPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.NewEditIndex]["ID_PedidoObtencao"]);

        StatusPedidoObtencaoEnum status = (StatusPedidoObtencaoEnum)Convert.ToInt32(((Label)gvPesquisa.Rows[e.NewEditIndex].FindControl("lblID_Status")).Text);

        // NaoEnviado = 10
        if (status == StatusPedidoObtencaoEnum.NaoEnviado)
            Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoCadastro.aspx?id_pedido=" + id);

        // AguardandoVerificacaoPaiol = 20 || AguardandoAprovacaoEncarregadoDepartamentoMaterial = 30,
        //else if (status == StatusPedidoObtencaoEnum.AguardandoVerificacaoPaiol || status == StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamentoMaterial)
        //    Anthem.AnthemClientMethods.Redirect("frmVerificacaoPaiol.aspx?id_pedido=" + id.ToString());

        // AguardandoAprovacaoChefeDepartamento_Servidor = 40
        else if (status == StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Servidor)
            Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoAprovacao.aspx?id_pedido=" + id.ToString());

        // AprovacaoEncDivisão = 50
        else if (status == StatusPedidoObtencaoEnum.AprovacaoEncDivisão)
            Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoAprovacao.aspx?id_pedido=" + id.ToString());

        // AguardandoAprovacaoChefeDepartamento_Departamento = 55
        else if (status == StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Departamento)
            Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoAprovacao.aspx?id_pedido=" + id.ToString());

        // AguardandoDesignacaoComprador = 58
        else if (status == StatusPedidoObtencaoEnum.AguardandoDesignacaoComprador)
            Anthem.AnthemClientMethods.Redirect("frmDesignacaoComprador.aspx?id_pedido=" + id.ToString());
        
        // AguardandoAvaliacaoEncarregadoObtencao = 64
        else if (status == StatusPedidoObtencaoEnum.AguardandoAvaliacaoEncarregadoObtencao)
            Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoAguardandoCotacoes.aspx?id_pedido=" + id.ToString());

        // DefiniçãoFinanceiraRelator = 150,
        else if (status == StatusPedidoObtencaoEnum.DefiniçãoFinanceiraRelator)
        {
            _po = PedidoObtencao.Get(id);
            ucDefinicaoFinanceiraRelator.Show(_po);
        }

        // AguardandoAprovacaoAgenteFiscal = 155,
        else if (status == StatusPedidoObtencaoEnum.AguardandoAprovacaoAgenteFiscal)
            Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoAprovacao.aspx?id_pedido=" + id.ToString());

        // AguardandoAprovacaoOrdenadorDespesa = 160,
        else if (status == StatusPedidoObtencaoEnum.AguardandoAprovacaoOrdenadorDespesa)
            Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoAprovacao.aspx?id_pedido=" + id.ToString());

        // AguardandoEnvioEmpenho = 185
        else if (status == StatusPedidoObtencaoEnum.AguardandoEnvioEmpenho)
        {
            _po = PedidoObtencao.Get(id);
            ucNotaEmpenho.Show(_po);
        }

        // AguardandoEntregaExecucao = 190
        else if (status == StatusPedidoObtencaoEnum.AguardandoEntregaExecucao)
            Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoAprovacao.aspx?id_pedido=" + id.ToString());

        // AguardandoEntregaMercadoria = 200
        else if (status == StatusPedidoObtencaoEnum.AguardandoEntregaMercadoria)
        {
            _po = PedidoObtencao.Get(id);
            //ucMessageBox.Show("Enviar para a próxima etapa?", null);
            ucBaixaPO.Show(_po);


            //Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoAprovacao.aspx?id_pedido=" + id.ToString());
        }


        // ANTIGO - POSEIDON/BACS
        ////if (status == StatusPedidoObtencaoEnum.AguardandoVerificacaoPaiol || status == StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamentoMaterial)
        ////{
        ////    Anthem.AnthemClientMethods.Redirect("frmVerificacaoPaiol.aspx?id_pedido=" + id.ToString());
        ////}
        ////else
        //if (status == StatusPedidoObtencaoEnum.NaoEnviado)
        //{
        //    Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoCadastro.aspx?id_pedido=" + id);
        //}
        //else if (status == StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDivisao ||
        //    status == StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamento ||
        //    status == StatusPedidoObtencaoEnum.AguardandoAprovacaoSupervisaoCotacao ||
        //    status == StatusPedidoObtencaoEnum.AguardandoAprovacaoAgenteFiscal ||
        //    status == StatusPedidoObtencaoEnum.AguardandoAprovacaoPAR ||
        //    status == StatusPedidoObtencaoEnum.AguardandoAprovacaoOrdenadorDespesa)
        //{
        //    Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoAprovacao.aspx?id_pedido=" + id.ToString());
        //}
        ////else if (status == StatusPedidoObtencaoEnum.ImprimirEnviarIntendência)
        ////{
        ////    _po = PedidoObtencao.Get(id);
        ////    ucMessageBox.Show("O PO foi impresso com sucesso?", null);
        ////}
        //else if (status == StatusPedidoObtencaoEnum.AguardandoCreditoEmpenho)
        //{
        //    _po = PedidoObtencao.Get(id);
        //    ucNotaEmpenho.Show(_po);
        //}
        //else if (status == StatusPedidoObtencaoEnum.AguardandoEntregaExecucao)
        //{
        //    _po = PedidoObtencao.Get(id);
        //    //ucMessageBox.Show("Enviar para a próxima etapa?", null);
        //    ucBaixaPO.Show(_po);
        //}
        //else if (status == StatusPedidoObtencaoEnum.AguardandoLiquidacao)
        //{
        //    int id_pagamento = Convert.ToInt32(gvPesquisa.DataKeys[e.NewEditIndex]["ID"]);
        //    _po = PedidoObtencao.Get(id);
        //    _pagamento = _po.Pagamentos.Find(id_pagamento);
        //    ucMessageBox.Show("Deseja confirmar liquidação?", null);
        //}
        //else if (status == StatusPedidoObtencaoEnum.AguardandoPagamentoPO)
        //{
        //    int id_pagamento = Convert.ToInt32(gvPesquisa.DataKeys[e.NewEditIndex]["ID"]);
        //    _po = PedidoObtencao.Get(id);
        //    _pagamento = _po.Pagamentos.Find(id_pagamento);
        //    ucInputBox.Show(id_pagamento, "Confirmar Pagamento. Número Ordem Bancária:");
        //}
        //else if (status == StatusPedidoObtencaoEnum.AguardandoEnvioEmpenho)
        //{
        //    int id_pagamento = Convert.ToInt32(gvPesquisa.DataKeys[e.NewEditIndex]["ID"]);
        //    _po = PedidoObtencao.Get(id);
        //    _pagamento = _po.Pagamentos.Find(id_pagamento);
        //    ucRecebedorEmpenho.Show();
        //}
    }

    #region Nota Empenho
    void ucNotaEmpenho_NotaInformada(object sender, EventArgs e)
    {
        //_po.Lista = ucNotaEmpenho.Lista;
        //_po.NumeroLancamento = ucNotaEmpenho.NumeroLancamento;
        //_po.CodigoGestao = ucNotaEmpenho.CodigoGestao;
        //_po.Projeto = Projeto.Get(Convert.ToInt32(ucNotaEmpenho.ID_Projeto));
        //_po.PTRES = PTRES.Get(Convert.ToInt32(ucNotaEmpenho.ID_PTRES));
        
        _po.GerarNotaEmpenho(ID_Servidor);

        ucNotaEmpenho.Close();
        _po = null;
        _pagamento = null;
        Bind();
    }

    void ucNotaEmpenho_OperacaoCancelada(object sender, EventArgs e)
    {
        _po = null;
        _pagamento = null;
    }
    #endregion

    void ucBaixaPO_BaixaInformada(object sender, EventArgs e)
    {
        _po.FazerBaixa(this.ID_Servidor, Convert.ToDecimal(ucBaixaPO.Valor), ucBaixaPO.NotaFiscal, ucBaixaPO.ID_Empenho);
        ucBaixaPO.Close();
        _po = null;
        _pagamento = null;
        Bind();
    }

    void ucMessageBox_MessageBoxClose(object sender, MessageBoxEventArgs e)
    {
        if (e.Result == MessageBoxResult.Sim)
        {
            if (_pagamento == null)
            {
                _po.IrParaProximoStatus(ID_Servidor, null);
            }
            else
            {
                if (_pagamento.Status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoLiquidacao)
                    _pagamento.ConfirmarLiquidacao(this.ID_Servidor);
            }

            _po = null;
            _pagamento = null;
            Bind();
        }
    }

    void ucInputBox_TextoInformado(object sender, EventArgs e)
    {
        if (_pagamento != null)
        {
            if (_pagamento.Status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoPagamentoPO)
                _pagamento.ConfirmarPagamento(this.ID_Servidor, ucInputBox.Texto);
        }

        ucInputBox.Close();
        _po = null;
        _pagamento = null;
        Bind();
    }

    void ucRecebedorEmpenho_OkClicked(object sender, EventArgs e)
    {
        _po.RegistrarRecebedorEmpenho(this.ID_Servidor, ucRecebedorEmpenho.NomeRecebedorEmpenho, ucRecebedorEmpenho.TelefoneRecebedorEmpenho);
        ucRecebedorEmpenho.Close();
        _po = null;
        _pagamento = null;
        Bind();
        ucRecebedorEmpenho.Close();
    }

    void gvPesquisa_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.RowIndex].Values["ID_PedidoObtencao"]);
        _po = PedidoObtencao.Get(id);

        if(_po.Status.StatusPedidoObtencaoEnum >= StatusPedidoObtencaoEnum.AguardandoEntregaExecucao && _po.Status.StatusPedidoObtencaoEnum <= StatusPedidoObtencaoEnum.Finalizado)
        {
            _pagamento = PedidoObtencaoPagamento.Get(Convert.ToInt32(gvPesquisa.DataKeys[e.RowIndex].Values["ID"]));

        }
        ucRecusarEtapa.Show();
    }

    void ucRecusarEtapa_PedidoRecusado(object sender, EventArgs e)
    {
        if(_pagamento == null)
        {
            _po.Recusar(this.ID_Servidor, ucRecusarEtapa.Justificativa);    
        }
        else
        {
            _pagamento.Recusar(this.ID_Servidor, ucRecusarEtapa.Justificativa);
        }

        _pagamento = null;
        
        ucRecusarEtapa.Close();
        Bind();
    }

    void ucDefinicaoFinanceiraRelator_DefinicaoInformada(object sender, EventArgs e)
    {
        //_po.NaturezaDespesa = NaturezaDespesa.Get(Convert.ToInt32(ucDefinicaoFinanceiraRelator.ID_NaturezaDespesa));
        //_po.Comprador = Servidor.Get(Convert.ToInt32(ucDefinicaoFinanceiraRelator.ID_Comprador));
        _po.FonteRecurso = FonteRecurso.Get(Convert.ToInt32(ucDefinicaoFinanceiraRelator.ID_FonteRecurso));
        //_po.PTRES = PTRES.Get(Convert.ToInt32(ucDefinicaoFinanceiraRelator.ID_PTRES));
        _po.IrParaProximoStatus(ID_Servidor, ucDefinicaoFinanceiraRelator.Comentario);
        ucDefinicaoFinanceiraRelator.Close();
        _po = null;
        Bind();
    }
}
