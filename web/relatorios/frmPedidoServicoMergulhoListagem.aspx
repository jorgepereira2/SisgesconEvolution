<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoMergulhoListagem.aspx.cs" Inherits="frmPedidoServicoMergulhoListagem" ValidateRequest="false"  enableEventValidation="false" %>
<%@ Register TagPrefix="WebControls" Assembly="Shared.WebControls" Namespace="Shared.WebControls" %>
<%@ Register Src="~/UserControls/ucColumnManager.ascx" TagPrefix="uc" TagName="ColumnManager" %>
<%@ Register Src="~/UserControls/CabecalhoRelatorio.ascx" TagPrefix="uc" TagName="Cabecalho" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet"  />   
   <%-- <style>
        .text { mso-number-format:\@; } 
    </style>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <uc:Cabecalho runat="server" Titulo="Pedidos de Serviço de Mergulho" />
    <uc:ColumnManager runat="server" ID="ucColumn" />  
    <div>
    <br />
        <Anthem:Panel runat="server" id="pnGrid">
        
        <div style="text-align:left">
           
           <%-- <b>Quantidade:</b>&nbsp;
            <Anthem:Label runat="server" ID="lblQuantidade" />
            
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
             <b>Valor Total:</b>&nbsp;
            <Anthem:Label runat="server" ID="lblValorTotal" />
        </div>
        
      <WebControls:ReportGridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid" 
         AutoGenerateColumns="false" CellPadding="3" AllowSorting="false" >
        <HeaderStyle CssClass="dgHeader" />                                            
        <AlternatingRowStyle CssClass="dgAlternatingItem" />
        <FooterStyle CssClass="dgFooter" />
        <Columns>
            <asp:BoundField DataField="CodigoComAno" ItemStyle-CssClass="text" HeaderText="Código Interno" ItemStyle-HorizontalAlign="center" SortExpression="CodigoComAno" />
            <%--<asp:TemplateField HeaderText="Código Interno" ItemStyle-HorizontalAlign="center" Visible="True" SortExpression="CodigoComAno">
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).CodigoInterno + "-" + ((DelineamentoOrcamento)Container.DataItem).DataEmissao.Year %>
                </ItemTemplate>
            </asp:TemplateField>--%>
             <asp:TemplateField HeaderText="PS Cliente" ItemStyle-HorizontalAlign="center"  Visible="True" >
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).CodigoPedidoCliente %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Data" ItemStyle-HorizontalAlign="center" Visible="True" SortExpression="DataEmissao">
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).DataEmissao.ToShortDateString()%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="PO" ItemStyle-HorizontalAlign="center"  Visible="false" >
                <ItemTemplate>
                    <Anthem:LinkButton runat="server" ID="lnkPO" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Cliente" ItemStyle-HorizontalAlign="left"  Visible="True" SortExpression="Cliente" >
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).Cliente.Descricao%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ind. Naval" ItemStyle-HorizontalAlign="center"  Visible="false"  >
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).Cliente.IndicativoNaval%>
                </ItemTemplate>
            </asp:TemplateField>     
            <asp:TemplateField HeaderText="Pagador" ItemStyle-HorizontalAlign="left"  Visible="False" SortExpression="ClientePagador">
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).ClientePagador.Descricao%>
                </ItemTemplate>
            </asp:TemplateField>          
            <asp:TemplateField HeaderText="Divisão" ItemStyle-HorizontalAlign="left"  Visible="True" SortExpression="Celula">
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).Celula%>
                </ItemTemplate>
            </asp:TemplateField>
           
         <%--  <asp:TemplateField HeaderText="NL Pagamento" ItemStyle-HorizontalAlign="center"  Visible="False" SortExpression="NLPagamento">
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).NumeroNL%>
                </ItemTemplate>
            </asp:TemplateField> --%>
           <%-- <asp:TemplateField HeaderText="Qtd." ItemStyle-HorizontalAlign="center"  Visible="True" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).PedidoServico.Quantidade %>
                </ItemTemplate>
            </asp:TemplateField>--%> 
               
             <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center"  Visible="True" SortExpression="Status">
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).Status ?? ((PedidoServicoMergulho)Container.DataItem).Status%>
                </ItemTemplate>
            </asp:TemplateField>           
            <asp:TemplateField HeaderText="Categoria" ItemStyle-HorizontalAlign="center"  Visible="false" SortExpression="CategoriaServico">
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).CategoriaServico ?? ((PedidoServicoMergulho)Container.DataItem).CategoriaServico%>
                </ItemTemplate>
            </asp:TemplateField>                      
            <asp:TemplateField HeaderText="Msg. Indicação Recurso" ItemStyle-HorizontalAlign="center"  Visible="false" >
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).MensagemIndicacaoRecurso %>
                </ItemTemplate>
            </asp:TemplateField>                  
           <%-- <asp:TemplateField HeaderText="Msg. Aprov. Cliente" ItemStyle-HorizontalAlign="center"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).MensagemAprovacaoCliente %>
                </ItemTemplate>
            </asp:TemplateField>  --%>
           <%-- <asp:TemplateField HeaderText="Msg. Orçamento" ItemStyle-HorizontalAlign="center"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).MensagemEnvioCliente %>
                </ItemTemplate>
            </asp:TemplateField>  --%>
           <%-- <asp:TemplateField HeaderText="Msg. Chamada Meio" ItemStyle-HorizontalAlign="center"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).MensagemChamadaMeio %>
                </ItemTemplate>
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Msg. Prontificação" ItemStyle-HorizontalAlign="center"  Visible="False" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).MensagemProntificacao %>
                </ItemTemplate>
            </asp:TemplateField> --%> 
           <%-- <asp:TemplateField HeaderText="Número NL" ItemStyle-HorizontalAlign="center"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).NumeroNL %>
                </ItemTemplate>
            </asp:TemplateField>  --%>
            <asp:TemplateField HeaderText="Detalhes" ItemStyle-HorizontalAlign="Center"  Visible="true">
                <ItemTemplate>
                    <Anthem:LinkButton runat="server" ID="lnkDetalhes" Text="Detalhes" />
                </ItemTemplate>
            </asp:TemplateField>


         <%--   <asp:TemplateField HeaderText="MOD" ItemStyle-HorizontalAlign="right"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).ValorMaoObraDireta.ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MOI" ItemStyle-HorizontalAlign="right"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).ValorMaoObraIndireta.ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TOMO" ItemStyle-HorizontalAlign="right"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).TaxaContribuicaoOperacionalMaoObra.ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Desconto MO" ItemStyle-HorizontalAlign="right"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).ValorDescontoMaoObra.ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total MO" ItemStyle-HorizontalAlign="right"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).ValorTotalMaoObra.ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MD" ItemStyle-HorizontalAlign="right"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).ValorMateriaPrima.ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="STD" ItemStyle-HorizontalAlign="right"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).ValorServicoTerceiros.ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MI" ItemStyle-HorizontalAlign="right"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).ValorMaterialIndireto.ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="STI" ItemStyle-HorizontalAlign="right"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).ValorServicoTerceirosIndireto.ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TOMS" ItemStyle-HorizontalAlign="right"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).ValorTaxaOperacionalMaterialServicos.ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TCOMO" ItemStyle-HorizontalAlign="right"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).ValorTaxaContribuicaoOperacionalMaoObra.ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TCOMS" ItemStyle-HorizontalAlign="right"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).ValorTaxaContribuicaoOperacionalMaterial.ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Desconto Material" ItemStyle-HorizontalAlign="right"  Visible="false" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).ValorDescontoMaterial.ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>     --%>       
             
            <asp:TemplateField HeaderText="Valor Total" ItemStyle-HorizontalAlign="right"  Visible="false" SortExpression="ValorTotalOrcamento">
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).ValorTotalItens.ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="Defeito" ItemStyle-HorizontalAlign="left"  Visible="false" SortExpression="DefeitoReclamado">
                <ItemTemplate>
                    <%# ((DelineamentoOrcamento)Container.DataItem).PedidoServico.DefeitoReclamado %>
                </ItemTemplate>
            </asp:TemplateField>--%>
             <asp:TemplateField HeaderText="Observação" ItemStyle-HorizontalAlign="left"  Visible="false" SortExpression="Observacao">
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).Observacao%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Motivo Cancelamento" ItemStyle-HorizontalAlign="center"  Visible="false" >
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).MotivoCancelamento%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Diversos" ItemStyle-HorizontalAlign="left"  Visible="false" SortExpression="Diversos">
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).Diversos%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Data Pronto" ItemStyle-HorizontalAlign="center"  Visible="false" SortExpression="DataPronto">
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).DataPronto.HasValue ? ((PedidoServicoMergulho)Container.DataItem).DataPronto.Value.ToShortDateString() : ""%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Data Planejamento" ItemStyle-HorizontalAlign="center"  Visible="false" SortExpression="DataPlanejamentoPS">
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).DataPlanejamentoPS.HasValue ? ((PedidoServicoMergulho)Container.DataItem).DataPlanejamentoPS.Value.ToShortDateString() : ""%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Previsão Entrega" ItemStyle-HorizontalAlign="center"  Visible="false" SortExpression="PrevisaoEntrega">
                <ItemTemplate>
                    <%# ((PedidoServicoMergulho)Container.DataItem).PrevisaoEntrega.HasValue ? ((PedidoServicoMergulho)Container.DataItem).PrevisaoEntrega.Value.ToShortDateString() : ""%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </WebControls:ReportGridView>  
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
