<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEntradarodizioPesquisa.aspx.cs" Inherits="frmEntradaRodizioPesquisa" %>
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
        Recibo de Saída de Material Rodízio
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="94%" style="height:350px;" >																		    
        <tr>
            <td valign="top">                
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >                   
                    <tr>
                        <td valign="top" align="center">
                             <anthem:GridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <RowStyle CssClass="dgItem" />
                                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                <Columns>                                                      
                                    <asp:TemplateField HeaderText="PM">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencao)Container.DataItem).CodigoComAno %>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                      
                                    <asp:TemplateField HeaderText="PS">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencao)Container.DataItem).DelineamentoOrcamento %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Servidor">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencao)Container.DataItem).Servidor %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Aplicação">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencao)Container.DataItem).Aplicacao %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Data" ItemStyle-HorizontalAlign="center"  SortExpression="DataEmissao" 
                                        DataField="DataEmissao" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                                    
                                    <asp:HyperLinkField NavigateUrl="" Text="Editar" itemstyle-horizontalalign="center"
                                         DataNavigateUrlFields="ID" DataNavigateUrlFormatString="frmEntradaRodizio.aspx?id_pedido={0}" />    
                                   
                                </Columns>
                            </anthem:GridView>
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
