<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchPedidoObtencaoCompleto2.aspx.cs" Inherits="fchPedidoObtencaoCompleto" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/DadosOM.ascx" TagName="DadosOM" TagPrefix="uc" %>
<%@ Register Src="ucHistoricoPedidoObtencao.ascx" TagPrefix="uc" TagName="Historico" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />  
</head>
<body>
    <form id="form1" runat="server">   
   <table cellpadding="2" style="border:solid 1px black; width:98%">
        <tr>
            <td>
            
            </td>
            <td align="center" width="50%">
                <uc:DadosOM runat="server" />
            </td>
            <td align="right" width="25%">
                 <b >Autorização de Compra</b>
                <br />
                <br />
                <b>
                    AC <%# _pedido.CodigoComAno %>
                </b>
                <br />
                <br />
                Emissão: <%# DateTime.Today.ToShortDateString() %>                         
            </td>
        </tr>
    </table>
    <br /><br />
    <div class="PageTitle" style="width:98%;text-align:right;">
            Dados Básicos
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top"  >
                    <b>Número:
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
                    <b>Comprador:</b> 
                    <%# _pedido.Servidor.Identificacao %>
                </td>
                <td align="left" valign="top" >
                    <b>Status:
                    <%# _pedido.Status.Descricao %>
                    </b>
                </td>
            </tr>              
            <tr>
                <td align="left" valign="top" >
                    <%--<b>AC:</b> 
                    <asp:Repeater runat="server" ID="repPO" OnItemDataBound="repPO_ItemDataBound">
                        <ItemTemplate>
                            <Anthem:LinkButton runat="server" ID="lnkPO" Text='<%# ((KeyValuePair<int, string>)Container.DataItem).Value %>' />
                        </ItemTemplate>
                        <SeparatorTemplate>,&nbsp;</SeparatorTemplate>
                    </asp:Repeater> --%>
                    
                <%--</td>
                <td align="left" valign="top" >--%>
                    <b>PS:</b>
                    <asp:Repeater runat="server" ID="repPS" OnItemDataBound="repPS_ItemDataBound">
                        <ItemTemplate>
                            <Anthem:LinkButton runat="server" ID="lnkPS" Text='<%# ((KeyValuePair<int, string>)Container.DataItem).Value %>' />
                        </ItemTemplate>
                        <SeparatorTemplate>,&nbsp;</SeparatorTemplate>
                    </asp:Repeater> 
                </td>
                <td>
                    <b>Modalidade:</b> 
                    <%--<%# _pedido.TipoCompra.Descricao %>--%>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top"  >
                    <b>Tipo Compra:</b> 
                    <%# _pedido.TipoCompra.Descricao %>
                </td>
                <td align="left" valign="top" >
                    <b>Número Empenho:</b> 
                    <%# _pedido.NumeroEmpenho %>
                </td>
            </tr>
           
             
             <tr>
                <td align="left" valign="top"  >
                    <b>Valor Total:</b> 
                    <%# _pedido.ValorTotal.ToString("c2") %>
                </td>
                <td align="left" valign="top" >
                    <b>Valor Pago:</b>
                    <%# _pedido.ValorPago.ToString("c2") %>                     
                </td>
            </tr>
            
            <tr>
                <td align="left" valign="top"  >
                    <b>Licitação:</b> 
                    <%# _pedido.Licitacao == null ? "" : _pedido.Licitacao.NumeroPregao %>
                </td>
                <td align="left" valign="top" >
                    <%--<b>Requer Aprovação Conselho:</b>
                    <%# _pedido.FlagRequerAprovacaoConselhoEconomico ? "Sim" : "Não" %>          --%>           
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
        <br />
         <div class="PageTitle" style="width:98%;text-align:right;">
            Fornecedor
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>              
                 <td align="left" valign="top" >
                    <b>Data Emissão:</b> 
                    <%# _pedido.DataEmissao.ToShortDateString() %>
                </td>
                 <td align="left" valign="top"  >
                    <b>Divisão:</b> 
                    <%# _pedido.Celula.Descricao %>
                </td>
            </tr>            
              <tr>               
                <td align="left" valign="top" >
                    <b>Servidor:</b>
                    <%# _pedido.Servidor.Identificacao %>                     
                </td>
                <td align="left" valign="top"  >
                    <b>Tipo AC:</b>
                    <%# _pedido.TipoCompra.Descricao %>&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lnkLicitacao" />                     
                </td>
            </tr>  
            <tr>
                <td align="left" valign="top" colspan="2" >
                    <b>Fornecedor:
                    <%# _pedido.Fornecedor != null ? _pedido.Fornecedor.RazaoSocial + " (" + Shared.Common.Util.FormataCNPJ(_pedido.Fornecedor.CNPJ) + ")" : ""  %>
                    </b> 
                </td>                
            </tr> 
            <tr>                
                <td align="left" valign="top" >
                    <b>Status:</b>
                    <%# _pedido.Status.Descricao %>                    
                </td>
                <td align="left" valign="top" >
                    <b>Finalidade:
                    <%# Shared.Common.Util.GetDescription(_pedido.TipoPedidoObtencao ) %>
                    </b>
                </td>
            </tr> 
             <tr>                
                <td align="left" valign="top" >
                    <b>Recebedor Empenho:</b>
                    <%# _pedido.NomeRecebedorEmpenho %>                    
                </td>
                <td align="left" valign="top" >
                    <b>Telefone Recebedor Empenho:
                    <%# _pedido.TelefoneRecebedorEmpenho %>
                    </b>
                </td>
            </tr> 
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Aplicação:</b> 
                    <%# _pedido.Aplicacao %>
                     <asp:LinkButton runat="server" ID="lnkPS" />
                </td>
            </tr>
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Observação:</b> 
                    <%# _pedido.Observacao %>
                </td>
            </tr>
            <%--<tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Outros Fornecedores Cotados:</b> 
                    <%# _pedido.FornecedorCotacao2 + (_pedido.FornecedorCotacao2 != null ? " (" + Shared.Common.Util.FormataCNPJ(_pedido.FornecedorCotacao2.CNPJ) + ")" : "")%>, 
                    <%# _pedido.FornecedorCotacao3 + (_pedido.FornecedorCotacao3 != null ? " (" + Shared.Common.Util.FormataCNPJ(_pedido.FornecedorCotacao3.CNPJ) + ")" : "")%>
                </td>
            </tr>--%>
        </table>

        
        
           <br />
             
              <asp:Panel runat="server" ID="pnItem">     
        <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Itens
        </div>  
       <anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Material/Serviço" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.CodigoInterno %> - 
                            <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Especificação" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoItem)Container.DataItem).Especificacao %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>   
                    <asp:TemplateColumn HeaderText="Natureza Despesa" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.NaturezaDespesa != null ? ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.NaturezaDespesa.Codigo : "" %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                            
                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((PedidoObtencaoItem)Container.DataItem).Quantidade.ToString("N2") %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                            <table border="0">
                                <tr>
                                    <td nowrap="nowrap">
                                        Valor 1: <%# ((PedidoObtencaoItem)Container.DataItem).Valor.ToString("N2") %>         
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">
                                        Valor 2: <%# ((PedidoObtencaoItem)Container.DataItem).ValorCotacao2.ToString("N2") %>         
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">
                                        Valor 3: <%# ((PedidoObtencaoItem)Container.DataItem).ValorCotacao3.ToString("N2") %>         
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">
                                        Valor 4: <%# ((PedidoObtencaoItem)Container.DataItem).ValorCotacao4.ToString("N2") %>         
                                    </td>
                                </tr>
                            </table>
                           
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                   
                    <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((PedidoObtencaoItem)Container.DataItem).ValorTotal.ToString("C2") %>
                        </ItemTemplate>
                        <FooterTemplate>                                                
                            <b><%# _pedido.ValorTotal.ToString("C2") %></b>
                        </FooterTemplate>                                            
                    </asp:TemplateColumn>                                                                                                                      
                </Columns>
            </anthem:DataGrid>     
            </asp:Panel>
            
             <asp:Panel runat="server" ID="pnItemPEP">
         <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Itens PEP
        </div> 
         <anthem:DataGrid runat="server" ID="dgItemPEP" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((IItemServicoMaterial)Container.DataItem).ServicoMaterial.CodigoInterno%> - 
                            <%# ((IItemServicoMaterial)Container.DataItem).ServicoMaterial.Descricao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Especificação" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((IItemServicoMaterial)Container.DataItem).Especificacao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                                     
                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((IItemServicoMaterial)Container.DataItem).Quantidade.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((IItemServicoMaterial)Container.DataItem).Valor.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                   
                    <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# (((IItemServicoMaterial)Container.DataItem).Quantidade * ((IItemServicoMaterial)Container.DataItem).Valor).ToString("C2")%>
                        </ItemTemplate>
                        <FooterTemplate>                                                
                            <b><%# _pedido.ValorTotal.ToString("C2") %></b>
                        </FooterTemplate>                                            
                    </asp:TemplateColumn>                                                                                                                      
                </Columns>
            </anthem:DataGrid>   
         </asp:Panel>
         
             <asp:Panel runat="server" ID="pnItemRodizio">
         <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Itens Rodízio
        </div> 
         <anthem:DataGrid runat="server" ID="dgItemRodizio" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((IItemServicoMaterial)Container.DataItem).ServicoMaterial.CodigoInterno%> - 
                            <%# ((IItemServicoMaterial)Container.DataItem).ServicoMaterial.Descricao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Especificação" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((IItemServicoMaterial)Container.DataItem).Especificacao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                                    
                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((IItemServicoMaterial)Container.DataItem).Quantidade.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((IItemServicoMaterial)Container.DataItem).Valor.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                   
                    <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# (((IItemServicoMaterial)Container.DataItem).Quantidade * ((IItemServicoMaterial)Container.DataItem).Valor).ToString("C2")%>
                        </ItemTemplate>
                        <FooterTemplate>                                                
                            <b><%# _pedido.ValorTotal.ToString("C2") %></b>
                        </FooterTemplate>                                            
                    </asp:TemplateColumn>                                                                                                                      
                </Columns>
            </anthem:DataGrid>   
         </asp:Panel>    

          <asp:Panel runat="server" ID="pnEmpenho">
          <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Empenhos            
        </div>  
       <anthem:DataGrid runat="server" ID="dgEmpenho" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" ShowFooter="false" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Número" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoEmpenho)Container.DataItem).NumeroEmpenho %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Ação Interna" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoEmpenho)Container.DataItem).Projeto %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="PTRES" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
<%--                            <%# ((PedidoObtencaoEmpenho)Container.DataItem).PTRES %>--%>
                            <%# ((PedidoObtencaoEmpenho)Container.DataItem).PTRESS %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                           
                    <asp:TemplateColumn HeaderText="Lançamento" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoEmpenho)Container.DataItem).NumeroLancamento %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Lista" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoEmpenho)Container.DataItem).Lista %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Código Gestão" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoEmpenho)Container.DataItem).CodigoGestao %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>  
                     <asp:TemplateColumn HeaderText="Comentário" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoEmpenho)Container.DataItem).Comentario %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn> 
                </Columns>
            </anthem:DataGrid>     
              </asp:Panel>

             <asp:Panel runat="server" ID="pnPagamentos">
          <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Pagamentos            
        </div>  
       <anthem:DataGrid runat="server" ID="dgPagamento" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" ShowFooter="false" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Número" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoPagamento)Container.DataItem).Numero %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="NF" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoPagamento)Container.DataItem).NumeroNF %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Empenho" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoPagamento)Container.DataItem).Empenho == null ? "" : ((PedidoObtencaoPagamento)Container.DataItem).Empenho.NumeroEmpenho%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Ordem Bancaria" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoPagamento)Container.DataItem).NumeroOrdemBancaria %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                           
                    <asp:TemplateColumn HeaderText="Status" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoPagamento)Container.DataItem).Status.Descricao %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoPagamento)Container.DataItem).Valor %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                </Columns>
            </anthem:DataGrid>     


          </asp:Panel>
            
            
             <br />
            <div class="PageTitle" style="width:98%;text-align:left;">
                Histórico
            </div>       
            <uc:Historico runat="server" ID="ucHistorico" />                          
                          
                    
          <%--<br /><br />
       <table width="98%" border="0">
        <tr>
            <td style="width:50%; text-align:center;padding: 10px;" >
                ___________________________________________<br />
                Chefe do Departamento
            </td>
            <td style="width:0%; text-align:center" >
            
            
            </td>
        
            <td style="width:50%; text-align:center;padding: 10px;" >
                ___________________________________________<br />
                Agente Fiscal
            </td>
            <td style="width:0%; text-align:center" >
            
            
            </td>
        </tr>
           <tr>
            <td colspan="2" style="width:100%; text-align:center;padding: 10px;" >
                ___________________________________________<br />
                Ordenador de Despesas
            </td>
            <td style="width:0%; text-align:center" >
            
            
            </td>
        </tr>
     </table>--%>
    </form>
</body>
</html>
