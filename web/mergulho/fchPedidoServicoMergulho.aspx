<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchPedidoServicoMergulho.aspx.cs" Inherits="fchPedidoServicoMergulho" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/servico/DetalhamentoOrcamento.ascx" TagName="Orcamento" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />  
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" Class="ReportTitle">
        Detalhes do PS de Mergulho
    </div>
    <br /><br />
    <div class="PageTitle" style="width:98%;text-align:right;">
            Dados Básicos
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top"  >
                    <b>Código Interno:
                    <%# _pedido.CodigoComAno %>
                    </b>
                </td>
                 <td align="left" valign="top" >
                    <b>Data Emissão:</b>
                    <%# _pedido.DataEmissao.ToShortDateString() %>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top"  >
                    <b>PS Cliente:
                    <%# _pedido.CodigoPedidoCliente %></b> 
                </td>
                <td align="left" valign="top" >
                    <b>PO Associado:</b>
                    <asp:Repeater runat="server" ID="dlPO" >
                        <SeparatorTemplate>,</SeparatorTemplate>
                        <ItemTemplate>
                           <a href='<%# "../pedidoObtencao/fchPedidoObtencao.aspx?id_pedido=" + ((PedidoObtencao)Container.DataItem).ID  %>' target="_blank" ><%# ((PedidoObtencao)Container.DataItem).CodigoComAno %></a>
                        </ItemTemplate>
                    </asp:Repeater>
                                
                </td>
            </tr>           
            <tr>
                <td align="left" valign="top"  >
                    <b>Divisão:</b> 
                    <%# _pedido.Celula == null ? "" : _pedido.Celula.Descricao %>
                </td>
               <td align="left" valign="top" >
                    <b>Situação:
                    <%# _pedido.Status.Descricao %>
                    </b>
                </td>
            </tr>              
               <tr>
                <td align="left" valign="top" >
                    <b>Embarcação:</b> 
                    <%# _pedido.Embarcacao == null ? "" : _pedido.Embarcacao.Descricao %>
                </td> 
                <td align="left" valign="top" >
                    <b>Prioridade:</b> 
                    <%# _pedido.Prioridade == null ? "" : _pedido.Prioridade.Descricao %>
                </td>                
            </tr> 
             <tr>
                <td align="left" valign="top" >
                    <b>Categoria:</b> 
                    <%# _pedido.CategoriaServico == null ? "" : _pedido.CategoriaServico.Descricao %>
                </td> 
                <td align="left" valign="top" >
                    <b>Localização:</b> 
                    <%# _pedido.Localizacao %>
                </td>                
            </tr>    
              <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>NL Pagamento:</b> 
                    <%# _pedido.NLPagamento%>
                </td>
            </tr>        
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Msg. Indicaèc\ao Recurso:</b> 
                    <%# _pedido.MensagemIndicacaoRecurso %>
                </td>
            </tr>        
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Observação:</b> 
                    <%# _pedido.Observacao %>
                </td>
            </tr>
        </table>
        
           <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Cliente
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Cliente:</b> 
                    <%# _pedido.Cliente.DescricaoCompleta %>
                </td>               
            </tr>
             <tr>                
                <td align="left" valign="top" colspan="2" >
                    <b>Pagador:</b> 
                    <%# _pedido.ClientePagador.DescricaoCompleta %>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Contatos:</b> 
                    <%# _pedido.Contatos %>
                </td>               
            </tr> 
            <tr>
                 <td align="left" valign="top" >
                    <b>Telefones:</b>
                    <%# _pedido.TelefoneContatos %>                    
                </td>
                <td align="left" valign="top" >
                    <b>Código Pedido Cliente:</b> 
                    <%# _pedido.CodigoPedidoCliente %>
                </td>                
            </tr>
           
        </table>
        
    

        
                
            <br />
                <div class="PageTitle" style="width:100%;text-align:right;">
                Delineamento
            </div>
            <anthem:DataGrid runat="server" ID="dgDelineamento" Width="100%" CssClass="datagrid"
                    AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" OnItemCreated="dgDelineamento_ItemCreated" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle CssClass="dgFooter" />
                <EditItemStyle HorizontalAlign="Center" />
                <Columns>                                         
                        <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoServicoMergulhoDelineamento)Container.DataItem).Celula.Descricao  %>
                        </ItemTemplate>                                                                        
                    </asp:TemplateColumn>
                    
                      <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoServicoMergulhoDelineamento)Container.DataItem).Celula.Descricao  %>
                        </ItemTemplate>                                                                        
                    </asp:TemplateColumn>

                      <asp:TemplateColumn HeaderText="Qtd. Mergulhadores" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((PedidoServicoMergulhoDelineamento)Container.DataItem).QuantidadeMergulhadores %>
                        </ItemTemplate>                                                                        
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Nº Dias" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((PedidoServicoMergulhoDelineamento)Container.DataItem).NumeroDias %>
                        </ItemTemplate>                                                                        
                    </asp:TemplateColumn>

                        
                    <asp:TemplateColumn HeaderText="Faina Diaria" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((PedidoServicoMergulhoDelineamento)Container.DataItem).TempoFainaDiaria %>
                        </ItemTemplate> 
                        <FooterTemplate>
                        Total: <asp:Label runat="server" ID="lblTotalHH" Font-Bold="true" />
                        </FooterTemplate>                                           
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Descrição" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoServicoMergulhoDelineamento)Container.DataItem).DescricaoServicoOficina%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                </Columns>
            </anthem:DataGrid>  
                
                <br />
                <div class="PageTitle" style="width:100%;text-align:right;">
                    Itens 
                </div>
                <anthem:DataGrid runat="server" ID="dgOrcamentoItem" Width="100%" CssClass="datagrid"
                     AutoGenerateColumns="false" CellPadding="3" >
                    <HeaderStyle CssClass="dgHeader" />                                    
                    <ItemStyle CssClass="dgItem" />
                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                    <FooterStyle CssClass="dgFooter" />
                    <EditItemStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoServicoMergulhoItemOrcamento)Container.DataItem).ServicoMaterial.Descricao %>
                            </ItemTemplate>                                                                                  
                        </asp:TemplateColumn> 
                        <asp:TemplateColumn HeaderText="Dados Complementares" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoServicoMergulhoItemOrcamento)Container.DataItem).Observacao%>
                            </ItemTemplate>                              
                        </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="Fornecedor" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoServicoMergulhoItemOrcamento)Container.DataItem).Fornecedor%>
                            </ItemTemplate>                              
                        </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="Origem" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                               <%# Shared.Common.Util.GetDescription(((PedidoServicoMergulhoItemOrcamento)Container.DataItem).OrigemMaterial)%>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                               <%# ((PedidoServicoMergulhoItemOrcamento)Container.DataItem).Quantidade.ToString("N2")%>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="Valor Unit." ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                               <%# ((PedidoServicoMergulhoItemOrcamento)Container.DataItem).Valor.ToString("N2")%>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                               <%# ((PedidoServicoMergulhoItemOrcamento)Container.DataItem).ValorTotal.ToString("N2")%>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                    </Columns>
                </anthem:DataGrid>                 
                
     <%--   <asp:Panel ID="pnOcorrencia" runat="server" Visible="false">
        <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
                Ocorrências
            </div>  
        <anthem:DataGrid runat="server" ID="dgOcorrencia" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle CssClass="dgFooter" />
                <EditItemStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Início" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((DelineamentoOrcamentoOcorrencia)Container.DataItem).DataInicio.ToString("dd/MM/yyyy") %>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn HeaderText="Fim" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((DelineamentoOrcamentoOcorrencia)Container.DataItem).DataFim.HasValue ?  
                                ((DelineamentoOrcamentoOcorrencia)Container.DataItem).DataFim.Value.ToString("dd/MM/yyyy") : "" %>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>                    
                     <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                           <%# ((DelineamentoOrcamentoOcorrencia)Container.DataItem).Celula.Descricao %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Descrição" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# 
                                ((DelineamentoOrcamentoOcorrencia)Container.DataItem).DescricaoServico %>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>
                </Columns>
            </anthem:DataGrid>    
             </asp:Panel>    --%>
          
          
          
        <br />
       
        <asp:Panel runat="server" Visible="true">        
        <div class="PageTitle" style="width:98%;text-align:right;">
                Histórico
            </div>  
        <anthem:DataGrid runat="server" ID="dgHistorico" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle CssClass="dgFooter" />
                <EditItemStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Data" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((HistoricoPedidoServicoMergulho)Container.DataItem).Data.ToString("dd/MM/yyyy hh:mm") %>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>                    
                     <asp:TemplateColumn HeaderText="Servidor" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                           <%# ((HistoricoPedidoServicoMergulho)Container.DataItem).Servidor.Identificacao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Descrição" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((HistoricoPedidoServicoMergulho)Container.DataItem).Texto%>
                            <font style="color:red">
                            <%# String.IsNullOrEmpty(((HistoricoPedidoServicoMergulho)Container.DataItem).JustificativaRecusa) ? "" : "<br>Comentário:&nbsp;" + ((HistoricoPedidoServicoMergulho)Container.DataItem).JustificativaRecusa%>
                            </font>
                            <%# ((HistoricoPedidoServicoMergulho)Container.DataItem).StatusPosterior.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.Cancelado ? "<br>Motivo: " + _pedido.MotivoCancelamento.Descricao : ""%>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>
                </Columns>
            </anthem:DataGrid>      
          </asp:Panel>             
        
    </form>
</body>
</html>
