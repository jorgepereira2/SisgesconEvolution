using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Threading;
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

public partial class frmPedidoServicoPendente : SortingPageBase
{
    #region protected

    protected IPedido _pedidoServico
    {
        get{ return Session["frmPedidoServicoPendente.IPedido"] == null ? null : (IPedido) Session["frmPedidoServicoPendente.IPedido"]; }
        set{ Session["frmPedidoServicoPendente.IPedido"] = value;}
    }

    #endregion

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
        ucMensagemCliente.MensagemInformada += new EventHandler(ucMensagemCliente_MensagemInformada);
        //ucDevolucaoMeio.OkClick += UcDevolucaoMeio_OnOkClick;
        ucInsereFaturamento.FaturamentoInserido += delegate{Bind();};
        ucDesignarCotador.OkClick += new EventHandler(ucDesignarCotador_OkClick);
        btnFiltrar.Click += new EventHandler(btnFiltrar_Click);
        ucAguardandoIndicacaoRecurso.MensagemInformada += new EventHandler(ucAguardandoIndicacaoRecurso_MensagemInformada);
        ucAguardandoInicioExecucao.OkClicked += ucAguardandoInicioExecucao_OkClicked;
        ucEmExecucao.OkClicked += ucEmExecucao_OkClicked;
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
            Util.FillDropDownList(ddlStatus, StatusPedidoServico.List(), "Todos");
            Util.FillDropDownList(ddlOficinaDelineamento, Celula.List(null, true), "Todos");
            Util.FillDropDownList(ddlGerente, Servidor.List(FuncaoServidor.GerenteDPCP), "Todos");
            //Bind();
        }
    }

    #endregion  
    
    #region Mensagem Cliente

    void ucMensagemCliente_MensagemInformada(object sender, EventArgs e)
    {
        ((DelineamentoOrcamento)_pedidoServico).RegistrarMensagemCliente(this.ID_Servidor, ucMensagemCliente.NumeroMensagem);
        ucMensagemCliente.Close();
        Bind();
        _pedidoServico = null;
    }

    void ucAguardandoIndicacaoRecurso_MensagemInformada(object sender, EventArgs e)
    {
        ((DelineamentoOrcamento)_pedidoServico).RegistrarIndicacaoRecurso(this.ID_Servidor, ucAguardandoIndicacaoRecurso.NumeroMensagem);
        
        ucAguardandoIndicacaoRecurso.Close();
        Bind();
        _pedidoServico = null;
    }

    void ucAguardandoInicioExecucao_OkClicked(object sender, EventArgs e)
    {
        ((DelineamentoOrcamento)_pedidoServico).RegistrarInicioExecucao(this.ID_Servidor, ucAguardandoInicioExecucao.DataPrevisaoEntrega, ucAguardandoInicioExecucao.Comentario);

        ucAguardandoInicioExecucao.Close();
        Bind();
        _pedidoServico = null;
    }

    void ucEmExecucao_OkClicked(object sender, EventArgs e)
    {
        ((DelineamentoOrcamento)_pedidoServico).RegistrarFimExecucao(this.ID_Servidor, ucEmExecucao.NumeroNL);

        ucEmExecucao.Close();
        Bind();
        _pedidoServico = null;
    }
    #endregion
    
    #region Recusar

    void ucRecusarEtapa_PedidoRecusado(object sender, EventArgs e)
    {
        _pedidoServico.Recusar(this.ID_Servidor, ucRecusarEtapa.Justificativa);
        ucRecusarEtapa.Close();
        Bind();
        _pedidoServico = null;
    }

    void gvPesquisa_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.RowIndex].Value);
        StatusPedidoServicoEnum status =(StatusPedidoServicoEnum)Convert.ToInt32(((Label)gvPesquisa.Rows[e.RowIndex].FindControl("lblID_Status")).Text);
            
        if (status < StatusPedidoServicoEnum.EmDelineamento)
            _pedidoServico = PedidoServico.Get(id);
        else
            _pedidoServico = DelineamentoOrcamento.Get(id);

        ucRecusarEtapa.Show();
    }
    #endregion

    #region Designar Cotador
    void ucDesignarCotador_OkClick(object sender, EventArgs e)
    {
        ((DelineamentoOrcamento)_pedidoServico).DesignarCotador(this.ID_Servidor, ucDesignarCotador.ID_Servidor, ucDesignarCotador.Comentario);
        ucDesignarCotador.Close();
        Bind();
        _pedidoServico = null;
    }
    #endregion

    #region Databind()

    void gvPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {        
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.NewEditIndex].Value);
        StatusPedidoServicoEnum status =
            (StatusPedidoServicoEnum)
            Convert.ToInt32(((Label) gvPesquisa.Rows[e.NewEditIndex].FindControl("lblID_Status")).Text);
        if(status < StatusPedidoServicoEnum.EmDelineamento)
            _pedidoServico = PedidoServico.Get(id);
        else
            _pedidoServico = DelineamentoOrcamento.Get(id);

        //Etapas OK para o BACS
        if(_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.Nenhum) //AguardandoDesignacaoCotador
        {
            ucDesignarCotador.Show();
        }
        else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.OrcamentoFinalizado)
        {
            ucMessageBox.Show("Deseja confirmar a Cotação?", null);
        }
        //else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.EmExecucao)
        //{
        //    ucEmExecucao.Show();
        //}
        else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.EmExecucao)
        {
            ucMessageBox.Show("Confirma fim da execução?", null);
        }
        else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoSatisfeito)
        {
            ucMensagemCliente.Show(true);
        }
        else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEmissaoFaturamentoFinal)
        {
            ucMessageBox.Show("Confirma faturamento?", null);
        }
        else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEnvioParaDelineamento)
        {
            ucMessageBox.Show("Aprovar Delineamento?", null);
        }
        else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEnvioParaDelineamentoAprovar)
        {
            ucMessageBox.Show("Enviar para Delineamento?", null);
        }
        //else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovaçãoDivProgControle)
        //{
        //    ucMessageBox.Show("Deseja aprovar este PS?", null);
        //}
        //END Etapas OK para o BACS


        else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.Nenhum)//AguardandoAprovacaoDelineamento
        {
            //ucMessageBox.Show("Deseja confirmar o delineamento deste PS?", null);
        }
        else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEnvioMensagemCliente
            || _pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovacaoCliente
            || _pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoMensagemProntificacao
            || _pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoChamadaMeio)
        {
            ucMensagemCliente.Show(true);
        }
        else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoComprometimentoCliente)
        {
            ucAguardandoIndicacaoRecurso.Show();
        }
        else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoMeio)
        {
            ucMessageBox.Show("Confirma a chegada do meio?", null);
        }
        else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoInicioExecucao)
        {
            ucAguardandoInicioExecucao.Show();
            //ucMessageBox.Show("Confirma início da execução?", null);
        }
   
        else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEmissaoFaturamentoFinal)
        {
            ucInsereFaturamento.Show(_pedidoServico.ID);
        }
        //else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoDevolucaoMeio)
        //{
        //    ucDevolucaoMeio.Show();
        //}
        else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoSatisfeito)
        {
            ucMessageBox.Show("Confirma satisfeito?", null);
        }
        else if (//_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovacaoComandanteDCPC || 
            _pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovacaoComandanteGeral)
        {
            ucMessageBox.Show("Deseja aprovar este PS?", null);
        }
        //else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoLiquidacao)
        //{
        //    ucMensagemCliente.Show(false);
        //}
        //else if (//_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovacaoComandanteDCPC || 
        //    _pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoVerificacaoEstoque)
        //{
        //    ucMessageBox.Show("Aguardando Verificação do Estoque", null);
        //}
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnEditar = (LinkButton)e.Row.FindControl("btnEditar");
            LinkButton btnRecusar = (LinkButton)e.Row.FindControl("btnRecusar");

            IPedido pedido = (IPedido)e.Row.DataItem;

            PedidoServico pedidoServico = PedidoServico.Get(pedido.ID);

            string address = "";           

            if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.NaoEnviado)
            {
                address = string.Format("frmPedidoServicoCadastro.aspx?id_pedido={0}", pedido.ID);
                btnRecusar.Visible = false;
            }
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoDAC)
            {
                btnEditar.Text = "Encaminhar";
                address = string.Format("frmPedidoServicoCadastro.aspx?id_pedido={0}&encaminhar=true", pedido.ID);
            }
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.EmDelineamento)
            {
                int id_deli = (pedidoServico != null) ? pedidoServico.Orcamentos[0].ID : pedido.ID;
                btnEditar.Text = "Fazer Delineamento";
                address = string.Format("frmPedidoServicoOrcamento.aspx?id_delineamentoOrcamento={0}", id_deli);//pedido.ID pedidoServico.Orcamentos[0].ID
            }
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEnvioParaDelineamento)
            {
                btnEditar.Text = "Aprovar Delineamento";
            }
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEnvioParaDelineamentoAprovar)
            {
                btnEditar.Text = "Enviar para Delineamento";
            }
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.Nenhum)//AguardandoAprovacaoDelineamento
            {
                btnEditar.Text = "Aprovar Delineamento";
                btnRecusar.Visible = false;
                address = string.Format("frmDelineamentoAprovacao.aspx?id_delineamentoOrcamento={0}", pedido.ID);
            }
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.Nenhum)//AguardandoDesignacaoCotador
            {
                btnEditar.Text = "Designar Cotador";
            }
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoOrcamento)
            {
                btnEditar.Text = "Fazer Cotação";
                //btnRecusar.Visible = false;
                address = string.Format("frmPedidoServicoCotacao.aspx?id_delineamentoOrcamento={0}", pedido.ID);
            }
            else if(pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.OrcamentoFinalizado)
            {
                //var pnRecalcular = (Panel)e.Row.FindControl("pnRecalcular");
                //pnRecalcular.Visible = true;
                btnEditar.Text = "Deseja confirmar a Cotação?";
            }
            //else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.EmExecucao)
            //{
            //    btnEditar.Text = "Enviar";
            //}
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.EmExecucao)
            {
                btnEditar.Text = "Registrar Fim Execução";
            }
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoSatisfeito)
            {
                btnEditar.Text = "Informar Mensagem";
            }
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEmissaoFaturamentoFinal)
            {
                btnEditar.Text = "Registrar Faturamento";
            }
            else if (//pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovacaoComandanteDCPC || 
                pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovacaoComandanteGeral)
            {
                btnEditar.Text = "Aprovar";
            }
            //bacs OK ateh aqui

            
            //else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoOrcamento)
            //{
            //    btnEditar.Text = "Preencher Delineamento/Orçamento";
            //    address = string.Format("frmPedidoServicoOrcamento.aspx?id_pedido={0}", pedido.ID);
            //}
            //else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovacaoCotacao || pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovaçãoDivProgControle)
            //{
            //    btnEditar.Text = "Aprovar";
            //}
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEnvioMensagemCliente
                || pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovacaoCliente
                //|| pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoComprometimentoCliente
                || pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoMensagemProntificacao
                || pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoChamadaMeio)
            {
                btnEditar.Text = "Informar Mensagem";
            }
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoComprometimentoCliente)
            {
                btnEditar.Text = "Informar Mensagem";
            }
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoPlanejamento)
            {
                btnRecusar.Visible = true;
                btnEditar.Text = "Planejar";
                address = string.Format("frmPlanejamento.aspx?id_delineamentoOrcamento={0}", pedido.ID);
            }
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoMeio)
            {
                btnEditar.Text = "Confirmar Chegada";
            }
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoInicioExecucao)
            {
                btnEditar.Text = "Registrar Início";
            }
           
            
            //else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoDevolucaoMeio)
            //{
            //    btnEditar.Text = "Registrar Devolução Meio";
            //}
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoSatisfeito)
            {
                btnEditar.Text = "Registrar Satisfeito";
            }
            //else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoLiquidacao)
            //{
            //    btnEditar.Text = "Registrar Liquidação";
            //}
            else if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoVerificacaoEstoque)
            {
                btnEditar.Text = "Aguardando Verificação do Estoque";
                address = string.Format("frmPedidoServicoOrcamentoConfirma.aspx?id_delineamentoOrcamento={0}", pedido.ID);
            }

            if (address != "")
                Anthem.AnthemClientMethods.Redirect(address, btnEditar);

            if (pedido.FlagRecusado)
            {
                e.Row.ForeColor = Color.Red;
                e.Row.ToolTip = pedido.UltimoHistorico.JustificativaRecusa;

                e.Row.Attributes.Add("onmouseover", string.Format("Tip('<b>Justificativa:</b><br><br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",

                pedido.UltimoHistorico.JustificativaRecusa));
            }

            //if(!string.IsNullOrWhiteSpace(pedido.UltimoHistorico.JustificativaRecusa))
            //{
            //    e.Row.ToolTip = pedido.UltimoHistorico.JustificativaRecusa;

            //    e.Row.Attributes.Add("onmouseover", string.Format("Tip('<b>Comentário:</b><br><br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",

            //    pedido.UltimoHistorico.JustificativaRecusa));
            //}

            if(pedido.FlagProgem)
            {
                e.Row.ForeColor = Color.DarkRed;
                e.Row.BackColor = Color.LightYellow;
                e.Row.Font.Bold = true;
            }

            PedidoServico ps;
            if (pedido is PedidoServico)
                ps = (PedidoServico)pedido;
            else
                ps = ((DelineamentoOrcamento) pedido).PedidoServico;

            e.Row.Cells[4].Attributes.Add("onmouseover", string.Format("Tip('{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                GetTextoComentarios(ps)));

            HtmlAnchor lnkDetalhes = (HtmlAnchor)e.Row.FindControl("lnkDetalhes");
            //lnkDetalhes.InnerHtml = GetDetalhes(pedido);
            
            string queryString = pedido.ID.ToString();

            if (
                (
                    pedido.Status.StatusPedidoServicoEnum >= StatusPedidoServicoEnum.AguardandoEnvioParaDelineamento || 
                    pedido.Status.StatusPedidoServicoEnum >= StatusPedidoServicoEnum.AguardandoEnvioParaDelineamentoAprovar 
                ) &&
                pedido is DelineamentoOrcamento
            )
                queryString = ((DelineamentoOrcamento)pedido).PedidoServico.ID.ToString();

            if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.Finalizado)
            {
                address = "";
                btnRecusar.Visible = false;
                btnEditar.Visible = false;
            }
            
            Anthem.AnthemClientMethods.Popup(lnkDetalhes, "fchPedidoServico.aspx?id_pedido=" + queryString, false, false, false, true, true, true, true, 10, 40, 700, 520, false);
        }
    }

    public static string GetTextoComentarios(PedidoServico ps)
    {
        StringBuilder str = new StringBuilder();
        str.AppendFormat("<b>Categoria:</b>{0}<br>", ps.CategoriaServico);

        str.Append("<b>Comentários:</b><br>");
        for (int i = ps.Historico.Count - 1; i >= 0; i--)
        {
            HistoricoPedidoServico historico = ps.Historico[i];
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
           if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.OrcamentoFinalizado)
            {
                ((DelineamentoOrcamento)_pedidoServico).Aprovar(this.ID_Servidor, "");
            }
            //else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovaçãoDivProgControle)
            //{
            //    ((DelineamentoOrcamento)_pedidoServico).Aprovar(this.ID_Servidor, "");
            //}
            else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoMeio)
            {
                ((DelineamentoOrcamento)_pedidoServico).RegistrarChegadaMeio(this.ID_Servidor);
            }
            //else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoInicioExecucao)
            //{
            //    ((DelineamentoOrcamento)_pedidoServico).RegistrarInicioExecucao(this.ID_Servidor);
            //}
            //else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.EmExecucao)
            //{
            //    ((DelineamentoOrcamento)_pedidoServico).RegistrarFimExecucao(this.ID_Servidor);
            //}
           else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.EmExecucao)
           {
               ((DelineamentoOrcamento)_pedidoServico).RegistrarFimExecucao(this.ID_Servidor, "");
           }
            else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEmissaoFaturamentoFinal)
            {
                ((DelineamentoOrcamento)_pedidoServico).RegistrarMensagemCliente(this.ID_Servidor, null);
            }
           else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEnvioParaDelineamento)
            {
               //((OrcamentoFinalizado((DelineamentoOrcamento)_pedidoServico).FinalizarDelineamento(this.ID_Servidor)));
                ((PedidoServico)_pedidoServico).Registrar(this.ID_Servidor);
            }
           else if (_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEnvioParaDelineamentoAprovar)
            {
               //((OrcamentoFinalizado((DelineamentoOrcamento)_pedidoServico).FinalizarDelineamento(this.ID_Servidor)));
                //((PedidoServico)_pedidoServico).Registrar(this.ID_Servidor);
                ((PedidoServico)_pedidoServico).Orcamentos[0].EnviarParaDelineamento(this.ID_Servidor);
            }
           else if (//_pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovacaoComandanteDCPC || 
               _pedidoServico.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovacaoComandanteGeral)
            {
               ((DelineamentoOrcamento)_pedidoServico).AprovarDelineamento(this.ID_Servidor);
            }
            
            Bind();
            _pedidoServico = null;
        }
    }

    //private void UcDevolucaoMeio_OnOkClick(object sender, EventArgs e)
    //{
    //     ((DelineamentoOrcamento)_pedidoServico).RegistrarDevolucaoMeio(this.ID_Servidor, ucDevolucaoMeio.Comentario);
    //     Bind();
    //    _pedidoServico = null;
    //    ucDevolucaoMeio.Close();
    //}

    private static object GetPropertyValue(object obj, string property)
    {
        System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
        if (propertyInfo == null) return DateTime.MinValue;
        return propertyInfo.GetValue(obj, null);
    }


    protected override void Bind()
    {
        List<IPedido> list = PedidoServico.Select(this.ID_Servidor, Convert.ToInt32(ddlStatus.SelectedValue), txtCodigo.Text, Convert.ToInt32(ddlOficinaDelineamento.SelectedValue), Convert.ToInt32(ddlGerente.SelectedValue));


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

    #endregion
}