<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoCotacaoUltimasCotacoes.aspx.cs" Inherits="frmPedidoCotacaoUltimasCotacoes" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />      
</head>
<body>
    <form id="form1" runat="server">       


    
    <div align="center">
    <div align="right" style="width:98%" class="PageTitle">
    <br />
        Últimas Compras
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="98%" >																		    
        <tr   >
            <td style="border:solid 1px black" valign="top">                
                <table border="0" cellpadding="2" cellspacing="2" width="100%" >                                                  
                    <tr>                            
                        <td colspan="3" align="center" valign="top">
                        <br />
                             <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                <asp:Label runat="server" ID="lblServicoMaterial" />                                 
                                <hr size="1" class="divisor" style="" />
                            </div>                            
                            <br />
                            <Anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>                                                                            
                                    <asp:TemplateColumn HeaderText="Fornecedor" ItemStyle-HorizontalAlign="left" >
                                        <ItemTemplate>
                                            <%# ((PedidoCotacaoItem)Container.DataItem).AutorizacaoCompra.Fornecedor %>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>                                          
                                    <asp:TemplateColumn HeaderText="Telefone" ItemStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <%# ((PedidoCotacaoItem)Container.DataItem).AutorizacaoCompra.Fornecedor.Telefone %>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>                                          
                                    <asp:TemplateColumn HeaderText="Data" ItemStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <%# ((PedidoCotacaoItem)Container.DataItem).AutorizacaoCompra.DataEmissao %>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Valor Unit." ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <%# ((PedidoCotacaoItem)Container.DataItem).Valor.ToString("C2") %> 
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>                                   
                                </Columns>
                            </Anthem:DataGrid>                            
                        </td>
                    </tr>
                </table>                     
            </td>
        </tr>																			
    </table>
    <br /><br />
    <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">                
                 <Asp:Button runat="server" ID="btnFechar" Text="Fechar" OnClientClick="self.close();return;"
                    CssClass="Button"  />&nbsp;
                 
            </td>
            <td width="10px">
                &nbsp;
            </td>
        </tr>
    </table>
    </div>    
    <br /><br /><br /><br />
    </form>    
</body>
</html>
