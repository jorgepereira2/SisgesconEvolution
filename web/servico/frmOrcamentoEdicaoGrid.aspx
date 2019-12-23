<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOrcamentoEdicaoGrid.aspx.cs" Inherits="frmOrcamentoEdicaoGrid" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="Anthem" Assembly="Anthem" Namespace="Anthem" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" /> 
    
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Pesquisa de Pedidos de Serviço
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="94%" style="height:350px;" >																		    
        <tr>
            <td valign="top">                
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                    <tr>                            
                        <td colspan="2" align="center" valign="top" style="border:solid 1px black">
                        <br />
                            <div align="left" style="vertical-align:text-bottom">                                
                                <div class="PageTitle" >
                                &nbsp;&nbsp;
                                Filtros</div>
                                <hr size="1" class="divisor" style="" />
                                <table width="100%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td align="right">
                                            Data:
                                        </td>
                                        <td align="left">
                                            <Anthem:DateTextBox runat="server" ID="txtDataInicio" />&nbsp;a&nbsp;
                                            <Anthem:DateTextBox runat="server" ID="txtDataFim" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Status:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlStatus" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            PS Cliente:
                                        </td>
                                        <td align="left">
                                            <Anthem:TextBox runat="server" ID="txtNumeroPedidoCliente" Columns="40" /> &nbsp;&nbsp;                                          
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td align="right">
                                            Código Interno/Cliente:
                                        </td>
                                        <td align="left">
                                            <Anthem:TextBox runat="server" ID="txtTexto" Columns="40" /> &nbsp;&nbsp; 
                                            <Anthem:Button runat="server" ID="btnPesquisar"  TextDuringCallBack="Aguarde" Text="Pesquisar"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                            &nbsp;&nbsp; 
                                            <Anthem:Button runat="server" ID="btnNovo"  TextDuringCallBack="Aguarde" Text="Novo"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />                            
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center" >
                            
                           
                            <anthem:GridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <RowStyle CssClass="dgItem" />
                                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                <Columns>        
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Código Interno" SortExpression="CodigoComAno" >
                                        <ItemTemplate>
                                            <%# ((DelineamentoOrcamento)Container.DataItem).CodigoComAno %>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                     
                                    <asp:TemplateField ItemStyle-HorizontalAlign="left" headertext="Cliente" SortExpression="Cliente" ItemStyle-Width="120px"  >
                                        <ItemTemplate>
                                            <%# ((DelineamentoOrcamento)Container.DataItem).Cliente %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Status" SortExpression="Status" ItemStyle-Width="120px" >
                                        <ItemTemplate>
                                            <%# ((DelineamentoOrcamento)Container.DataItem).Status %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="left" headertext="Equipamento" SortExpression="Equipamento" >
                                        <ItemTemplate>
                                            <%# ((DelineamentoOrcamento)Container.DataItem).DescricaoEquipamentos %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Data" SortExpression="DataEmissao" >
                                        <ItemTemplate>
                                            <%# ((DelineamentoOrcamento)Container.DataItem).DataEmissao.ToShortDateString() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" headertext="Mensagens" ItemStyle-VerticalAlign="Top" >
                                        <ItemTemplate>
                                            
                                            <div style="margin-bottom: 5px">
                                                <b>Comprometimento:</b>
                                                <div style="min-height: 12px;">
                                                    <%# ((DelineamentoOrcamento)Container.DataItem).ComprometimentoCliente %>
                                                </div>
                                            </div>
                                            <div style="margin-bottom: 5px">
                                                <b>Numero NL:</b>
                                                <div style="min-height: 12px;">
                                                    <%# ((DelineamentoOrcamento)Container.DataItem).NumeroNL %>
                                                </div>
                                            </div>
                                            <div style="margin-bottom: 5px">
                                                <b>Satisfeito:</b>
                                                <div style="min-height: 12px;">
                                                    <%# ((DelineamentoOrcamento)Container.DataItem).MensagemProntificacao %>
                                                </div>
                                            </div>
                                            <div style="margin-bottom: 5px">
                                                <b>Msg. Orcamento:</b>
                                                <div style="min-height: 12px;">
                                                    <%# ((DelineamentoOrcamento)Container.DataItem).MensagemEnvioCliente %>
                                                </div>
                                            </div>
                                             <div style="margin-bottom: 5px">
                                                <b>Previsão Entrega:</b>
                                                <div style="min-height: 12px;">
                                                  <%# ((DelineamentoOrcamento)Container.DataItem).PedidoServico.PrevisaoEntrega.HasValue ? ((DelineamentoOrcamento)Container.DataItem).PedidoServico.PrevisaoEntrega.Value.ToShortDateString() : "" %>
                                                </div>
                                            </div>

                                            
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <table border="0">
                                                <tr>
                                                    <td align="right">
                                                    Comprometimento:        
                                                    </td>
                                                    <td>
                                                    <Anthem:TextBox runat="server" ID="txtComprometimento" Text="<%# ((DelineamentoOrcamento)Container.DataItem).ComprometimentoCliente %>" />            
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                    Numero NL:        
                                                    </td>
                                                    <td>
                                                    <Anthem:TextBox runat="server" ID="txtNumeroNL" Text="<%# ((DelineamentoOrcamento)Container.DataItem).NumeroNL %>" />
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td align="right">
                                                    Satisfeito:        
                                                    </td>
                                                    <td>
                                                    <Anthem:TextBox runat="server" ID="txtMensagemProntificacao" Text="<%# ((DelineamentoOrcamento)Container.DataItem).MensagemProntificacao %>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                    Msg. Orçamento:        
                                                    </td>
                                                    <td>
                                                    <Anthem:TextBox runat="server" ID="txtMensagemEnvioCliente" Text="<%# ((DelineamentoOrcamento)Container.DataItem).MensagemEnvioCliente %>" />
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td align="right">
                                                   Previsão Entrega:        
                                                    </td>
                                                    <td>
                                                    <Anthem:DateTextBox runat="server" ID="txtPrevisaoEntrega" Text='<%# ((DelineamentoOrcamento)Container.DataItem).PedidoServico.PrevisaoEntrega.HasValue ? ((DelineamentoOrcamento)Container.DataItem).PedidoServico.PrevisaoEntrega.Value.ToShortDateString() : "" %>' MaxLength="100" />
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkEditar" Text="Editar" CommandName="Edit" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkSalvar" Text="Salvar" CommandName="Update" />
                                            &nbsp;
                                            <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </anthem:gridview>
                          

                       
                            <Anthem:Panel runat="server" ID="pnMensagem" CssClass="msgErro" Visible="false">
                                <br />
                                Nenhum registro foi encontrado.
                            </Anthem:Panel>
                        </td>
                    </tr>
                </table>     
                
            </td>
        </tr>																			
    </table>
    <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td width="40%" align="left">                
            </td>
            <td align="right">              
            </td>
            <td width="10px">
                &nbsp;
            </td>
        </tr>
    </table>
    </div>    
    </form>    
</body>
</html>
