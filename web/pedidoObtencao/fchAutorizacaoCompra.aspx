<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchAutorizacaoCompra.aspx.cs" Inherits="fchAutorizacaoCompra" %>
<%@ Import namespace="System.Collections.Generic"%>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/DadosOM.ascx" TagName="DadosOM" TagPrefix="uc" %>
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
                    AC <%# _ac.CodigoComAno %>
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
                    <%# _ac.CodigoComAno %>
                    </b> 
                </td>
                 <td align="left" valign="top" >
                    <b>Data Emissão:</b> 
                    <%# _ac.DataEmissao.ToShortDateString() %>
                </td>
            </tr>            
            <tr>
                <td align="left" valign="top"  >
                    <b>Comprador:</b> 
                    <%# _ac.Servidor.Identificacao %>
                </td>
                <td align="left" valign="top" >
                    <b>Status:
                    <%# _ac.Status.Descricao %>
                    </b>
                </td>
            </tr>              
            <tr>
                <td align="left" valign="top"  >
                    <b>AC:</b> 
                    <asp:Repeater runat="server" ID="repPO" OnItemDataBound="repPO_ItemDataBound">
                        <ItemTemplate>
                            <Anthem:LinkButton runat="server" ID="lnkPO" Text='<%# ((KeyValuePair<int, string>)Container.DataItem).Value %>' />
                        </ItemTemplate>
                        <SeparatorTemplate>
                        ,&nbsp;
                        </SeparatorTemplate>
                    </asp:Repeater> 
                    
                </td>
                <td align="left" valign="top" >
                    <b>PS:</b>
                    <asp:Repeater runat="server" ID="repPS" OnItemDataBound="repPS_ItemDataBound">
                        <ItemTemplate>
                            <Anthem:LinkButton runat="server" ID="lnkPS" Text='<%# ((KeyValuePair<int, string>)Container.DataItem).Value %>' />
                        </ItemTemplate>
                        <SeparatorTemplate>
                        ,&nbsp;
                        </SeparatorTemplate>
                    </asp:Repeater> 
                </td>
            </tr>
            <tr>
                <td align="left" valign="top"  >
                    <b>Tipo Compra:</b> 
                    <%# _ac.TipoCompra.Descricao %>
                </td>
                <td align="left" valign="top" >
                    <b>Número Empenho:</b> 
                    <%# _ac.NumeroEmpenho %>
                </td>
            </tr> 
             <tr>
                <td align="left" valign="top"  >
                    <b>Valor Total:</b> 
                    <%# _ac.ValorTotal.ToString("c2") %>
                </td>
                <td align="left" valign="top" >
                    <b>Valor Pago:</b>
                    <%# _ac.ValorPago.ToString("c2") %>                     
                </td>
            </tr>
            
            <tr>
                <td align="left" valign="top"  >
                    <b>Licitação:</b> 
                    <%# _ac.Licitacao == null ? "" : _ac.Licitacao.NumeroPregao %>
                </td>
                <td align="left" valign="top" >
                    <b>Requer Aprovação Conselho:</b>
                    <%# _ac.FlagRequerAprovacaoConselhoEconomico ? "Sim" : "Não" %>                     
                </td>
            </tr>
          
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Observação:</b> 
                    <%# _ac.Observacao %>
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
                <td align="left" valign="top" colspan="2" >
                    <b>Razão Social:
                    <%# _ac.Fornecedor.RazaoSocial %>
                    </b> 
                </td>                 
            </tr>            
            <tr>
                <td align="left" valign="top"  >
                    <b>CNPJ:</b> 
                    <%# _ac.Fornecedor.CNPJ %>
                </td>
                <td align="left" valign="top" >
                    <b>Telefone:</b>
                    <%# _ac.Fornecedor.Telefone %>                     
                </td>
            </tr>  
            <tr>                
                <td align="left" valign="top" >
                    <b>Endereço:
                    <%# _ac.Fornecedor.Endereco %>
                    </b>
                </td>
                <td align="left" valign="top" >
                    <b>Bairro:
                    <%# _ac.Fornecedor.Bairro %>
                    </b>
                </td>
            </tr> 
             <tr>                
                <td align="left" valign="top" >
                    <b>Cidade:
                    <%# _ac.Fornecedor.Municipio %>
                    </b>
                </td>
                <td align="left" valign="top" >
                    <b>Estado:
                    <%# _ac.Fornecedor.Estado %>
                    </b>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top"  >
                    <b>CEP:</b> 
                    <%# _ac.Fornecedor.CEP %>
                </td>
                <td align="left" valign="top" >
                    <b>E-mail:</b>
                    <%# _ac.Fornecedor.Email %>                     
                </td>
            </tr>
            <tr>
                <td align="left" valign="top"  >
                    <b>Contatos:</b> 
                    <%# _ac.Fornecedor.DescricaoContatos %>
                </td>
                <td align="left" valign="top" >
                    <b>HomePage:</b>
                    <%# _ac.Fornecedor.HomePage %>                     
                </td>
            </tr>
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Observação:</b> 
                    <%# _ac.Observacao %>
                </td>
            </tr>
        </table>
        
           <br />
             
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
                    <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                            <%# string.IsNullOrEmpty(((PedidoCotacaoItem)Container.DataItem).Especificacao) ? "" : " - " + ((PedidoCotacaoItem)Container.DataItem).Especificacao %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((PedidoCotacaoItem)Container.DataItem).Quantidade.ToString("N2") %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((PedidoCotacaoItem)Container.DataItem).Valor.ToString("N2") %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                   
                    <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((PedidoCotacaoItem)Container.DataItem).ValorTotal.ToString("C2") %>
                        </ItemTemplate>
                        <FooterTemplate>                                                
                            <b><%# _ac.ValorTotal.ToString("C2") %></b>
                        </FooterTemplate>                                            
                    </asp:TemplateColumn>                                                                                                                      
                </Columns>
            </anthem:DataGrid> 
            
             <br />
             
        <asp:Panel runat="server" ID="pnPagamento">
        <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Pagamento
        </div>  
       <anthem:DataGrid runat="server" ID="dgPagamento" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Data" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((AutorizacaoCompraPagamento)Container.DataItem).Data.ToShortDateString() %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="NF" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((AutorizacaoCompraPagamento)Container.DataItem).NumeroNotaFiscal %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((AutorizacaoCompraPagamento)Container.DataItem).Valor.ToString("N2")%>
                        </ItemTemplate>                                                                
                        <FooterTemplate>                                                
                            <b><%# _ac.ValorPago.ToString("C2") %></b>
                        </FooterTemplate>                                            
                    </asp:TemplateColumn>                                                                                                                      
                </Columns>
            </anthem:DataGrid>            
        </asp:Panel>         
         
            <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Histórico
        </div>  
        <Anthem:DataList runat="server" ID="dlHistorico">
            <ItemTemplate>
                <b><%# ((HistoricoAutorizacaoCompra)Container.DataItem).Servidor.Identificacao %></b> - 
                <%# ((HistoricoAutorizacaoCompra)Container.DataItem).Data.ToString("dd/MM/yyyy hh:mm")%><br />
                
                <b>Alteração:</b> <%# ((HistoricoAutorizacaoCompra)Container.DataItem).Descricao%>
                <br />
                <b>Comentário:</b>
                <%# ((HistoricoAutorizacaoCompra)Container.DataItem).Justificativa%>
                <hr />
            </ItemTemplate>                                
        </Anthem:DataList>
    </form>
</body>
</html>
