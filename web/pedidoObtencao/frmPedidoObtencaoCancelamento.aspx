<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoObtencaoCancelamento.aspx.cs" Inherits="frmPedidoObtencaoCancelamento" %>
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
    <form id="form1" runat="server">
    <script type="text/javascript" src="../js/wz_tooltip.js" ></script>
     <uc:MotivoCancelamento runat="server" ID="ucMotivoCancelamento" TipoObjeto="PedidoObtencao"  />
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Cancelamento de Autorização de Compra
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
                                            Tipo:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlTipoPO" />
                                        </td>
                                        <td align="right">
                                            Célula:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlCelula" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Aplicação:
                                        </td>
                                        <td align="left" colspan="3">
                                           <Anthem:TextBox runat="server" ID="txtAplicacao" />
                                        </td>                                        
                                    </tr>                                
                                    <tr>
                                        <td align="right">
                                            Número:
                                        </td>
                                        <td align="left" colspan="3">
                                           <Anthem:TextBox runat="server" ID="txtTexto" Columns="30" />
                                            <Anthem:Button runat="server" ID="btnPesquisar"  TextDuringCallBack="Aguarde" Text="Pesquisar"
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
                                    <asp:TemplateField HeaderText="Tipo">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencao)Container.DataItem).TipoPedido == TipoPedido.PedidoMaterial ? "PM" : "PO" %>
                                        </ItemTemplate>                                    
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Status" ItemStyle-HorizontalAlign="center"  SortExpression="Status" 
                                        DataField="Status" />
                                     <asp:BoundField HeaderText="Célula" ItemStyle-HorizontalAlign="center"  SortExpression="Celula" 
                                        DataField="Celula" />
                                    <asp:BoundField HeaderText="Aplicação" ItemStyle-HorizontalAlign="left"  SortExpression="Aplicacao" 
                                        DataField="Aplicacao" />                                    
                                    <asp:BoundField HeaderText="Data" ItemStyle-HorizontalAlign="center"  SortExpression="DataEmissao" 
                                        DataField="DataEmissao" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkDetalhes" Text="Detalhes" />
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkCancelar" Text="Cancelar" CommandName="Delete" />
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
