<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchPedidoServico.aspx.cs" Inherits="fchPedidoServico" %>
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
        Detalhes do Pedido de Serviço        
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
                    <b>AC Associado:</b>
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
                    <b>Licitação:</b> 
                    <%# _pedido.Licitacao == null ? "" : _pedido.Licitacao.NumeroPregao %>
                </td>
            </tr>              
               <tr>
                <td align="left" valign="top" >
                    <b>Gerente:</b> 
                    <%# _pedido.ServidorGerente == null ? "" : _pedido.ServidorGerente.Identificacao %>
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
                    <b>Diversos:</b> 
                    <%# _pedido.Diversos %>
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
        <div class="PageTitle" style="width:98%;text-align:right;">
            Equipamento
        </div>  

        <anthem:DataGrid runat="server" ID="dgEquipamento" Width="100%" CssClass="datagrid"
                     AutoGenerateColumns="false" CellPadding="3" ShowFooter="true"  >
                    <HeaderStyle CssClass="dgHeader" />                                    
                    <ItemStyle CssClass="dgItem" />
                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                    <FooterStyle CssClass="dgFooter" />
                    <EditItemStyle HorizontalAlign="Center" />
                    <Columns>                                         
                         <asp:TemplateColumn HeaderText="Equipamento" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                               <%# ((PedidoServicoEquipamento)Container.DataItem).Equipamento.Descricao  %>
                            </ItemTemplate>                                                                        
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Codeq" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                               <%# ((PedidoServicoEquipamento)Container.DataItem).Equipamento.Codeq  %>
                            </ItemTemplate>                                                                        
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                               <%# ((PedidoServicoEquipamento)Container.DataItem).Quantidade  %>
                            </ItemTemplate>                                                                        
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Defeito Reclamado" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                               <%# ((PedidoServicoEquipamento)Container.DataItem).DefeitoReclamado  %>
                            </ItemTemplate>                                                                        
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Número Série" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                               <%# ((PedidoServicoEquipamento)Container.DataItem).NumeroSerie  %>
                            </ItemTemplate>                                                                        
                        </asp:TemplateColumn>
                    </Columns>
                </anthem:DataGrid>  

      <%--  <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top"  >
                    <b>Equipamento:</b> 
                    <%# _pedido.Equipamento.DescricaoCompleta %>
                    
                </td>
                 <td align="left" valign="top" >
                    <b>N. Registro:</b> 
                    <%# _pedido.NumeroRegistro %>
                </td>
            </tr>
            <tr>                
                <td align="left" valign="top" >
                    <b>Codeq:</b> 
                    <%# _pedido.Equipamento.Codeq %>
                </td>
                <td align="left" valign="top"  >
                    <b>Quantidade:</b> 
                    <%# _pedido.Quantidade %>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Defeito Reclamado:</b> 
                    <%# _pedido.DefeitoReclamado %>
                </td>
            </tr>            
        </table>--%>
        
        <br />
        
        <asp:DataList runat="server" ID="dlOrcamento" Width="98%">
            <ItemTemplate>
                <div class="PageTitle" style="width:100%;text-align:right;">
                    Orçamento <%# ((DelineamentoOrcamento)Container.DataItem).Numero %>   
                </div>  
                <table style="width:100%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
                    <tr>
                        <td align="left" valign="top"  >
                            <b>Número:</b> 
                            <%# ((DelineamentoOrcamento)Container.DataItem).Numero %>                            
                        </td>
                         <td align="left" valign="top" >
                            <b>Status:</b> 
                            <%# ((DelineamentoOrcamento)Container.DataItem).Status.Descricao %>
                        </td>
                    </tr>
                    <tr>                
                        <td align="left" valign="top" >
                            <b>Status PEP:</b> 
                            <%# Shared.Common.Util.GetDescription(((DelineamentoOrcamento)Container.DataItem).StatusEntregaMaterialPEP) %>                            
                        </td>
                        <td align="left" valign="top"  >
                            <b>Status Rodízio:</b> 
                            <%# Shared.Common.Util.GetDescription(((DelineamentoOrcamento)Container.DataItem).StatusEntregaMaterialRodizio) %>                            
                        </td>
                    </tr>
                    <tr>                
                        <td align="left" valign="top" >
                            <b>Status Singra:</b> 
                            <%# Shared.Common.Util.GetDescription(((DelineamentoOrcamento)Container.DataItem).StatusEntregaMaterialSingra) %>                            
                        </td>   
                        <td align="left" valign="top" >
                            <b>Categoria:</b> 
                            <%# ((DelineamentoOrcamento)Container.DataItem).CategoriaServico.Descricao %>                            
                        </td>                     
                    </tr>
                    <tr>
                        <td align="left" valign="top"  >
                            <b>AC:</b> 
                            <%# ((DelineamentoOrcamento)Container.DataItem).PedidoObtencao == null ? "N/A" : ((DelineamentoOrcamento)Container.DataItem).PedidoObtencao.CodigoComAno %>                            
                        </td>
                         <td align="left" valign="top" >
                            <b>Status AC:</b> 
                            <%# ((DelineamentoOrcamento)Container.DataItem).PedidoObtencao == null ? "N/A" : ((DelineamentoOrcamento)Container.DataItem).PedidoObtencao.Status.Descricao %>                            
                        </td>
                    </tr>   
                    
                    <tr>
                        <td align="left" valign="top"  >
                            <b>Cotador:</b> 
                            <%# ((DelineamentoOrcamento)Container.DataItem).ServidorCotador %>                            
                        </td>    
                        <td align="left" valign="top" >
                             <b>Delineador:</b> 
                            <%# ((DelineamentoOrcamento)Container.DataItem).Delineamentos.Count > 0 ? ((DelineamentoOrcamento)Container.DataItem).Delineamentos[0].Servidor.NomeCompleto : ""%>                                              
                        </td>                         
                    </tr>   
                                   
                    <tr>
                        <td align="left" valign="top"  >
                            <b>Msg. de Orçamento:</b> 
                            <%# ((DelineamentoOrcamento)Container.DataItem).MensagemEnvioCliente %>                            
                        </td>
                        <td align="left" valign="top"  >
                            <b>Comprometimento Cliente:</b> 
                            <%# ((DelineamentoOrcamento)Container.DataItem).ComprometimentoCliente %>                            
                        </td>
                    </tr>   
                     <tr>
                        <td align="left" valign="top" colspan="2" >
                            <b>Msg. Satisfeito:</b>
                            <%# ((DelineamentoOrcamento)Container.DataItem).MensagemProntificacao %>
                        </td>
                    </tr>                             
                    <tr>
                        <td align="left" valign="top" colspan="2" >
                            <b>Comentário:</b>
                            <%# ((DelineamentoOrcamento)Container.DataItem).Comentario %>
                        </td>
                    </tr>
                    <tr runat="server" id="trDevolucao">
                        <td align="left" valign="top" colspan="2" >
                            <b>Comentário Devolução:</b>
                            <%# ((DelineamentoOrcamento)Container.DataItem).ComentarioDevolucao %>
                        </td>
                    </tr>
                </table>
                
                <br />
                 <div class="PageTitle" style="width:100%;text-align:right;">
                    Delineamento&nbsp;<%# ((DelineamentoOrcamento)Container.DataItem).Numero %> 
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
                               <%# ((PedidoServicoDelineamento)Container.DataItem).Celula.Descricao  %>
                            </ItemTemplate>                                                                        
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="HH" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                               <%# ((PedidoServicoDelineamento)Container.DataItem).HomemHora %>
                            </ItemTemplate> 
                            <FooterTemplate>
                            Total: <asp:Label runat="server" ID="lblTotalHH" Font-Bold="true" />
                            </FooterTemplate>                                           
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Descrição" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                               <%# ((PedidoServicoDelineamento)Container.DataItem).DescricaoServicoOficina %>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                    </Columns>
                </anthem:DataGrid>  
                
                <br />
                <div class="PageTitle" style="width:100%;text-align:right;">
                    Itens Orçamento&nbsp;<%# ((DelineamentoOrcamento)Container.DataItem).Numero %> 
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
                                <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.Descricao %>
                            </ItemTemplate>                                                                                  
                        </asp:TemplateColumn> 
                        <asp:TemplateColumn HeaderText="Dados Complementares" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoServicoItemOrcamento)Container.DataItem).Observacao %>
                            </ItemTemplate>                              
                        </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="Fornecedor" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoServicoItemOrcamento)Container.DataItem).Fornecedor %>
                            </ItemTemplate>                              
                        </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="Origem" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                               <%# Shared.Common.Util.GetDescription(((PedidoServicoItemOrcamento)Container.DataItem).OrigemMaterial)  %>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                               <%# ((PedidoServicoItemOrcamento)Container.DataItem).Quantidade.ToString("N2") %>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="Valor Unit." ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                               <%# ((PedidoServicoItemOrcamento)Container.DataItem).Valor.ToString("N2") %>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                               <%# ((PedidoServicoItemOrcamento)Container.DataItem).ValorTotal.ToString("N2") %>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                    </Columns>
                </anthem:DataGrid> 
                <br />
                <uc:Orcamento runat="server" id="ucOrcamento" Orcamento='<%# Container.DataItem %>' />
                <br />
                
        <asp:Panel ID="pnOcorrencia" runat="server" Visible="false">
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
          </asp:Panel>   
            </ItemTemplate>        
        </asp:DataList>
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
                            <%# ((HistoricoPedidoServico)Container.DataItem).Data.ToString("dd/MM/yyyy hh:mm") %>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>                    
                     <asp:TemplateColumn HeaderText="Servidor" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                           <%# ((HistoricoPedidoServico)Container.DataItem).Servidor.Identificacao %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Descrição" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((HistoricoPedidoServico)Container.DataItem).Texto %>
                            <font style="color:red">
                            <%# String.IsNullOrEmpty(((HistoricoPedidoServico)Container.DataItem).JustificativaRecusa) ? "" : "<br>Comentário:&nbsp;" + ((HistoricoPedidoServico)Container.DataItem).JustificativaRecusa %>
                            </font>
                            <%# ((HistoricoPedidoServico)Container.DataItem).StatusPosterior.StatusPedidoServicoEnum == StatusPedidoServicoEnum.Cancelado ? "<br>Motivo: " + _pedido.MotivoCancelamento.Descricao : "" %>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>
                </Columns>
            </anthem:DataGrid>      
          </asp:Panel>             
        
    </form>
</body>
</html>
