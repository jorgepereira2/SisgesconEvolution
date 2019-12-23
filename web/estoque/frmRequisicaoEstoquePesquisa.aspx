<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmRequisicaoEstoquePesquisa.aspx.cs" Inherits="frmRequisicaoEstoquePesquisa" %>
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
        Pesquisa de Entrada/Saída de Estoque
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
                                            Tipo Requisição:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlTipoRequisicao" /> 
                                        </td>
                                        <td align="right">
                                            Tipo Movimento:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlTipoMovimento" /> 
                                        </td>
                                    </tr>                                    
                                    <tr>
                                         <td align="right">
                                            Número:
                                        </td>
                                        <td align="left">
                                            <Anthem:TextBox runat="server" ID="txtTexto" Columns="16" MaxLength="10" /> 
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
                                            Data:
                                        </td>
                                        <td align="left" colspan="3">
                                            <Anthem:DateTextBox runat="server" ID="txtDataInicio" /> &nbsp;a&nbsp; 
                                            <Anthem:DateTextBox runat="server" ID="txtDataFim" />
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
                                    <asp:BoundField HeaderText="Tipo" ItemStyle-HorizontalAlign="left"  SortExpression="TipoRequisicaoEstoque" 
                                        DataField="TipoRequisicaoEstoque" />
                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# Shared.Common.Util.GetDescription(((RequisicaoEstoque)Container.DataItem).Status) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Data" ItemStyle-HorizontalAlign="center"  SortExpression="DataCadastro" 
                                        DataField="DataCadastro" dataformatstring="{0:dd/MM/yyyy}" htmlencode="false" />
                                    <asp:HyperLinkField NavigateUrl="" Text="Editar" itemstyle-horizontalalign="center"
                                         DataNavigateUrlFields="ID" DataNavigateUrlFormatString="frmRequisicaoEstoqueCadastro.aspx?id_requisicao={0}" /> 
                                    
                                    
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
