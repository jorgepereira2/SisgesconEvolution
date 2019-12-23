<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchPedidoCotacao.aspx.cs" Inherits="fchPedidoCotacao" %>
<%@ Import namespace="Shared.NHibernateDAL"%>
<%@ Import Namespace="Marinha.Business" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />  
</head>
<body>
    <form id="form1" runat="server">
    
    <div align="center" Class="ReportTitle">
        Detalhes do Pedido de Cotação
    </div>
    <br /><br />
    <div class="PageTitle" style="width:98%;text-align:right;">
            Dados Básicos
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top"  >
                    <b>Número:
                    <%# _pedido.Numero %>
                    </b> 
                </td>
                 <td align="left" valign="top" >
                    <b>Data Emissão:</b> 
                    <%# _pedido.DataEmissao.ToShortDateString() %>
                </td>
            </tr>            
            <tr>
                <td align="left" valign="top"  >
                    <b>Finalizado:</b> 
                    <%# _pedido.FlagFinalizado ? "Sim" : "Não" %>
                </td>
                <td align="left" valign="top" >
                    <b>Comprador:</b>
                    <%# _pedido.Servidor.Identificacao %>                     
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
            Itens
        </div>  
       <anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
             AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
            <HeaderStyle CssClass="dgHeader" />                                    
            <ItemStyle CssClass="dgItem" />
            <AlternatingItemStyle CssClass="dgAlternatingItem" />
            <FooterStyle HorizontalAlign="Right" />
            <Columns>
                <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                            <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.SubClasseServicoMaterial.Codigo %> 
                            -
                            <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.NumeroReferencia %>
                            -
                            <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                             -
                            <%# ((PedidoCotacaoItem)Container.DataItem).Especificacao %>
                    </ItemTemplate>
                </asp:TemplateColumn>                                        
                <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                       <%# ((PedidoCotacaoItem)Container.DataItem).Quantidade.ToString("N2") %>
                    </ItemTemplate>                                            
                </asp:TemplateColumn>                                        
                 <asp:TemplateColumn HeaderText="Cotação" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                       <table cellpadding="3" border="1" rules="all" >
                            <tr style="background-color:#EEEEEE; color:#17485C;">
                                <td>
                                    
                                </td>
                                <td align="center">
                                    <b>Fornecedor</b>
                                </td>
                                <td align="center">
                                    <b>Valor</b>
                                </td>                              
                            </tr>
                            <tr>
                                <td align="center">
                                    1
                                </td>
                                <td align="center">
                                    <%# _pedido.FornecedorCotacao1 %>
                                </td>
                                <td align="center">
                                    <%# ObjectReader.ReadDecimal(((PedidoCotacaoItem)Container.DataItem).ValorCotacao1) %>
                                </td>                                
                            </tr>
                            <tr>
                                <td align="center">
                                  2
                                </td>
                                <td align="center">
                                    <%# _pedido.FornecedorCotacao2 %>
                                </td>
                                <td align="center">
                                    <%# ObjectReader.ReadDecimal(((PedidoCotacaoItem)Container.DataItem).ValorCotacao2) %>
                                </td> 
                            </tr>
                            <tr>
                                <td align="center">
                                   3
                                </td>
                                <td align="center">
                                    <%# _pedido.FornecedorCotacao3 %>
                                </td>
                                <td align="center">
                                    <%# ObjectReader.ReadDecimal(((PedidoCotacaoItem)Container.DataItem).ValorCotacao3) %>
                                </td> 
                            </tr>
                            <tr>
                                <td align="center">
                                    4
                                </td>
                                <td align="center">
                                    <%# _pedido.FornecedorCotacao4 %>
                                </td>
                                <td align="center">
                                    <%# ObjectReader.ReadDecimal(((PedidoCotacaoItem)Container.DataItem).ValorCotacao4) %>
                                </td> 
                            </tr>
                        </table>
                    </ItemTemplate>                                            
                </asp:TemplateColumn>                                                       
            </Columns>
        </anthem:DataGrid>         
         
         
          
    </form>
</body>
</html>
