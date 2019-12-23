<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoObtencaoDocumento.aspx.cs" Inherits="frmPedidoObtencaoDocumento" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/BuscaCliente.ascx" TagName="BuscaCliente" TagPrefix="uc" %>
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
        Documentos de AC
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    PO
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Número:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblNumero" />                           
                        </td>
                    </tr>
                     <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Status:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblStatus" />                           
                        </td>
                    </tr>
                     <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Aplicação:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblAplicacao" />                           
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Documento:
                        </td>
                        <td align="left">
                           <Anthem:FileUpload ID="FileUpload1" runat="server"  />
                           &nbsp;&nbsp;
                           <Anthem:Button runat="server" ID="btnUpload" TextDuringCallBack="Aguarde" Text="Enviar"
                                 EnabledDuringCallBack="false" CssClass="Button" />
                        </td>
                    </tr>
                </table>                     
            </td>
        </tr>		
        <tr>
            <td>
            <anthem:GridView runat="server" ID="gvDocumento" Width="100%" CssClass="datagrid"
                    AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <RowStyle CssClass="dgItem" />
                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                <FooterStyle CssClass="dgFooter" />
                <Columns>
                    <asp:BoundField HeaderText="Documento" ItemStyle-HorizontalAlign="left"  SortExpression="Nome" 
                        DataField="Nome" />

                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <a href='<%# "frmPedidoObtencaoDocumentoDownload.aspx?id=" + ((PedidoObtencaoDocumento)Container.DataItem).ID %>' target="_blank">[Download]</a>
                        </ItemTemplate>                                          
                    </asp:TemplateField>                     
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <Anthem:LinkButton runat="server" ID="btnExcluir" Text="[Excluir]" CommandName="Delete" 
                                CausesValidation="false"  /> 
                        </ItemTemplate>                                          
                    </asp:TemplateField>                                
                </Columns>
            </anthem:GridView>
        </td>
        </tr>																	
    </table>
    <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td width="40%" align="left">
            
            </td>
            <td align="right">
               
               <%-- <Anthem:Button runat="server" ID="btnVoltar" Text="Voltar"
                     CssClass="Button" CausesValidation="false" />--%>
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
