<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMedidaEtiquetaPesquisa.aspx.cs" Inherits="frmMedidaEtiquetaPesquisa" %>
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
        Medidas de Etiqueta
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="94%" style="height:350px;" >																		    
        <tr>
            <td valign="top">                
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                    <tr>                            
                        <td colspan="2" align="right" valign="top" style="border:solid 1px black">
                        <Anthem:Button runat="server" ID="btnNovo"  TextDuringCallBack="Aguarde" Text="Novo"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />                  
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                             <anthem:GridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <RowStyle CssClass="dgItem" />
                                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                <Columns>
                                    <asp:BoundField HeaderText="Nome" ItemStyle-HorizontalAlign="left"  SortExpression="Nome" 
                                        DataField="Nome" />
                                    <asp:BoundField HeaderText="Linhas" ItemStyle-HorizontalAlign="center"  SortExpression="Linhas" 
                                        DataField="Linhas" />
                                    <asp:BoundField HeaderText="Colunas" ItemStyle-HorizontalAlign="center"  SortExpression="Colunas" 
                                        DataField="Colunas" />
                                    <asp:HyperLinkField NavigateUrl="" Text="Editar" itemstyle-horizontalalign="center"
                                         DataNavigateUrlFields="ID" DataNavigateUrlFormatString="frmMedidaEtiquetaCadastro.aspx?id_medida={0}" />    
                                   
                                </Columns>
                            </anthem:gridview>
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
