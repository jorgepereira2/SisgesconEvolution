<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOMFCancelamento.aspx.cs" Inherits="frmOMFCancelamento" %>
<%@ Register Src="~/servico/MotivoCancelamento.ascx" TagName="MotivoCancelamento" TagPrefix="uc" %>
<%@ Register TagPrefix="Anthem" Assembly="Anthem" Namespace="Anthem" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" /> 
    
</head>
<body>
    <script type="text/javascript" src="../js/wz_tooltip.js" ></script>
    
    <form id="form1" runat="server">
     <uc:MotivoCancelamento runat="server" ID="ucMotivoCancelamento" TipoObjeto="AutorizacaoCompra"  />
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Cancelamento de OMF
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
                                           <Anthem:DateTextBox runat="server" ID="txtDataInicio" /> &nbsp;a&nbsp; 
                                            <Anthem:DateTextBox runat="server" ID="txtDataFim" />
                                        </td>
                                         <td align="right">
                                            Status:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlStatus" />
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td align="right">
                                            Recebedor:
                                        </td>
                                        <td align="left">
                                          <Anthem:DropDownList runat="server" ID="ddlRecebedor" />
                                        </td>
                                         <td align="right">
                                            Tipo Emprego:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlTipoEmprego" />
                                        </td>
                                    </tr>                                
                                    <tr>
                                        <td align="right">
                                            Número Nota/Fornecedor:
                                        </td>
                                        <td align="left" colspan="3">
                                           <Anthem:TextBox runat="server" ID="txtTexto" Columns="30" />
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
                        <td valign="top" align="center">
                             <anthem:GridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <RowStyle CssClass="dgItem" />
                                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                <Columns>
                                    <asp:BoundField HeaderText="Número" ItemStyle-HorizontalAlign="center"  SortExpression="Numero" 
                                        DataField="Numero" />
                                    <asp:BoundField HeaderText="Número Nota" ItemStyle-HorizontalAlign="center"  SortExpression="NumeroNota" 
                                        DataField="NumeroNota" />
                                    <asp:BoundField HeaderText="Status" ItemStyle-HorizontalAlign="center"  SortExpression="Status" 
                                        DataField="Status" />
                                     <asp:BoundField HeaderText="Fornecedor" ItemStyle-HorizontalAlign="left"  SortExpression="Fornecedor" 
                                        DataField="Fornecedor" />
                                    <asp:BoundField HeaderText="Recebedor" ItemStyle-HorizontalAlign="left"  SortExpression="Recebedor" 
                                        DataField="Recebedor" />                                    
                                    <asp:BoundField HeaderText="Data Entrega" ItemStyle-HorizontalAlign="center"  SortExpression="DataEntrega" 
                                        DataField="DataEntrega" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkCancelar" CommandName="Delete" Text="Cancelar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
