<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoAtividadeSecundariaPendente.aspx.cs" Inherits="frmPedidoServicoAtividadeSecundariaPendente" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc" %>
<%--<%@ Register Src="EtapaAguardandoPagamento.ascx" TagName="AguardandoPagamento" TagPrefix="uc" %>--%>
<%@ Register Src="RecusarEtapa.ascx" TagName="RecusarEtapa" TagPrefix="uc" %>

<%--<%@ Register Src="RegistrarDevolucaoMeio.ascx" TagName="RegistrarDevolucaoMeio" TagPrefix="uc" %>--%>
<%--<%@ Register Src="InsereFaturamento.ascx" TagName="InsereFaturamento" TagPrefix="uc" %>--%>
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
    <uc:MessageBox runat="server" ID="ucMessageBox"  />
    <%--<uc:AguardandoPagamento runat="server" ID="ucAguardandoPagamento"  />--%>
    <uc:RecusarEtapa runat="server" ID="ucRecusarEtapa"  />
    
    
    <%--<uc:RegistrarDevolucaoMeio runat="server" ID="ucDevolucaoMeio"  />--%>    
    <%--<uc:InsereFaturamento runat="server" ID="ucInsereFaturamento"  />--%>
    
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Pedidos de Serviço Pendentes
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="94%" style="height:350px;" >																		    
        <tr>
            <td valign="top">                
                <table border="0" cellpadding="2" cellspacing="0" width="100%" >
                    <tr>
                        <td align="left">
                            Código Interno:&nbsp;<Anthem:TextBox runat="server" ID="txtCodigo" Columns="12" />
                            &nbsp;&nbsp;&nbsp;
                            Status:&nbsp;<Anthem:DropDownList runat="server" ID="ddlStatus" />&nbsp;
                            <Anthem:Button runat="server" ID="btnFiltrar"  TextDuringCallBack="Aguarde" Text="Filtrar"
                                    EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />  
                            
                            
                        </td>
                    </tr>   
                   <%-- <tr>
                        <td align="left">   
                            Oficina Delineamento:&nbsp;<Anthem:DropDownList runat="server" ID="ddlOficinaDelineamento" />
                          </td>
                    </tr>   --%>             
                    <tr>
                        <td valign="top" align="center">
                             <anthem:GridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <RowStyle CssClass="dgItem" />
                                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                <Columns>                                            
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false" >
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblID_Status" Text='<%# ((PedidoServicoAtividadeSecundaria)Container.DataItem).Status.ID %>' />
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>    
                                    <asp:TemplateField HeaderText="Código Interno" ItemStyle-HorizontalAlign="Center" SortExpression="CodigoInterno">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoAtividadeSecundaria)Container.DataItem).CodigoComAno%>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                                                        
                                    <asp:TemplateField HeaderText="Cliente" ItemStyle-HorizontalAlign="left" SortExpression="DescricaoCliente" >
                                        <ItemTemplate>
                                            <%# ((PedidoServicoAtividadeSecundaria)Container.DataItem).DescricaoCliente%>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                                   
                                    <asp:TemplateField HeaderText="Situação" ItemStyle-HorizontalAlign="center" SortExpression="DescricaoStatus" >
                                        <ItemTemplate>
                                            <%# ((PedidoServicoAtividadeSecundaria)Container.DataItem).DescricaoStatus%>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data" ItemStyle-HorizontalAlign="center" SortExpression="Data">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoAtividadeSecundaria)Container.DataItem).DataEmissao.ToShortDateString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" CommandName="Edit" 
                                               CausesValidation="false"  /> 
                                        </ItemTemplate>                                          
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnRecusar" Text="Recusar" CommandName="Update" 
                                               CausesValidation="false"  /> 
                                        </ItemTemplate>                                          
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a runat="server" ID="lnkDetalhes" class="detalhe" >Detalhes</a>
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
