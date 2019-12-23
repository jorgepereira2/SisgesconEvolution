<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoObtencaoPendente.aspx.cs" Inherits="frmPedidoObtencaoPendente" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/InputBox.ascx" TagName="InputBox" TagPrefix="uc" %>
<%@ Register Src="NotaEmpenho.ascx" TagName="NotaEmpenho" TagPrefix="uc" %>
<%@ Register Src="BaixaPO.ascx" TagName="BaixaPO" TagPrefix="uc" %>
<%@ Register Src="RecebedorEmpenhoPO.ascx" TagName="RecebedorEmpenho" TagPrefix="uc" %>
<%@ Register Src="RecusarEtapa.ascx" TagName="RecusarEtapa" TagPrefix="uc" %>
<%@ Register Src="DefinicaoFinanceiraRelator.ascx" TagName="DefinicaoFinanceiraRelator" TagPrefix="uc" %>
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
    <uc:NotaEmpenho runat="server" ID="ucNotaEmpenho"  />   
    <uc:BaixaPO runat="server" ID="ucBaixaPO"  />   
    <uc:RecebedorEmpenho runat="server" ID="ucRecebedorEmpenho"  />   
    <uc:InputBox runat="server" ID="ucInputBox" ValidationEnabled="false"  />
    <uc:RecusarEtapa runat="server" ID="ucRecusarEtapa"  />
    <uc:DefinicaoFinanceiraRelator runat="server" ID="ucDefinicaoFinanceiraRelator" />

    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        PO/PM Pendentes
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="94%" style="height:350px;" >																		    
        <tr>
            <td valign="top">                
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >                   
                    <tr>
                        <td align="left">
                            Status:&nbsp;<Anthem:DropDownList runat="server" ID="ddlStatus" />&nbsp;
                            <Anthem:Button runat="server" ID="btnFiltrar"  TextDuringCallBack="Aguarde" Text="Filtrar"
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
                                    <asp:TemplateField HeaderText="" Visible="false" >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblID_Status" Text='<%# ((IPedidoObtencao)Container.DataItem).Status.ID %>'  />                                            
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                                       
                                    <asp:TemplateField HeaderText="Número" ItemStyle-HorizontalAlign="Center" SortExpression="Numero">
                                        <ItemTemplate>
                                            <%# ((IPedidoObtencao)Container.DataItem).CodigoComAno %>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Tipo" ItemStyle-HorizontalAlign="Center" SortExpression="TipoPedidoSigla">
                                        <ItemTemplate>
                                            <%# ((IPedidoObtencao)Container.DataItem).TipoPedidoSigla%>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                   
                                    <asp:TemplateField HeaderText="Aplicação" ItemStyle-HorizontalAlign="left" SortExpression="Aplicacao">
                                        <ItemTemplate>
                                            <%# ((IPedidoObtencao)Container.DataItem).Aplicacao%>
                                            &nbsp;<%# ((IPedidoObtencao)Container.DataItem).DelineamentoOrcamento != null ?
                                                                     " - " + ((IPedidoObtencao)Container.DataItem).DelineamentoOrcamento.DescricaoEquipamentos : ""%>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                                   
                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="center" SortExpression="DescricaoStatus">
                                        <ItemTemplate>
                                            <%# ((IPedidoObtencao)Container.DataItem).Status.Descricao%>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data" ItemStyle-HorizontalAlign="center" SortExpression="DataEmissao">
                                        <ItemTemplate>
                                            <%# ((IPedidoObtencao)Container.DataItem).DataEmissao.ToShortDateString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="right" SortExpression="ValorTotal">
                                        <ItemTemplate>
                                            <%# ((IPedidoObtencao)Container.DataItem).ValorTotal.ToString("N2")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnVisualizar" Text="Visualizar" CommandName="Edit" 
                                               CausesValidation="false"  /> 
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnImpressao" Text="Impressão" CausesValidation="false"  /> 
                                            <asp:Literal runat="server" ID="litBr" Text="<br>" Visible="false" />
                                            <Anthem:LinkButton runat="server" ID="btnDocumentos" Text="Anexar Documentos" Visible="false" CausesValidation="false"  /> 
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
                                            <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" CommandName="Edit" 
                                               CausesValidation="false"  /> 
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
