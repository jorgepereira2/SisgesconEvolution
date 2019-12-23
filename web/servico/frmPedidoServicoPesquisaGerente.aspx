<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoPesquisaGerente.aspx.cs" Inherits="frmPedidoServicoPesquisaGerente" %>
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
        Pesquisa de Pedidos de Serviço
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
                                            <Anthem:DateTextBox runat="server" ID="txtDataInicio" />&nbsp;a&nbsp;
                                            <Anthem:DateTextBox runat="server" ID="txtDataFim" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Status:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlStatus" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            PS Cliente:
                                        </td>
                                        <td align="left">
                                            <Anthem:TextBox runat="server" ID="txtNumeroPedidoCliente" Columns="40" /> &nbsp;&nbsp; 
                                         
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Código Interno/Cliente:
                                        </td>
                                        <td align="left">
                                            <Anthem:TextBox runat="server" ID="txtTexto" Columns="40" /> &nbsp;&nbsp; 
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
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Código Interno" SortExpression="CodigoComAno" >
                                        <ItemTemplate>
                                            <%# ((IPedido)Container.DataItem).CodigoComAno %>
                                        </ItemTemplate>
                                    </asp:TemplateField>     
                                     <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Registro" SortExpression="NumeroRegistro" >
                                        <ItemTemplate>
                                            <%# ((IPedido)Container.DataItem).NumeroRegistro %>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                               
                                    <asp:TemplateField ItemStyle-HorizontalAlign="left" headertext="Cliente" SortExpression="Cliente" >
                                        <ItemTemplate>
                                            <%# ((IPedido)Container.DataItem).Cliente %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Status" SortExpression="Status" >
                                        <ItemTemplate>
                                            <%# ((IPedido)Container.DataItem).Status %>
                                            <br runat="server" id="br" />
                                            <Anthem:LinkButton runat="server" ID="lnkGarantia" Text="Colocar em Garantia" CommandName="Garantia"
                                                CommandArgument="<%# ((IPedido)Container.DataItem).ID %>" />
                                            <Anthem:LinkButton runat="server" ID="lnkFinalizar" Text="Finalizar" CommandName="Finalizar"
                                                CommandArgument="<%# ((IPedido)Container.DataItem).ID %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="left" headertext="Equipamento" SortExpression="Equipamento" >
                                        <ItemTemplate>
                                            <%# ((IPedido)Container.DataItem).DescricaoEquipamentos %>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Data" SortExpression="DataEmissao" >
                                        <ItemTemplate>
                                            <%# ((IPedido)Container.DataItem).DataEmissao.ToShortDateString() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkDetalhes" Text="Detalhes" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                     <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkOrcamento" Text="Orçamento" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkNovoOrcamento" Text="Novo Orçamento" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkEditar" Text="Editar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </anthem:gridview>
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
