<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAutorizacaoCompraPendente.aspx.cs" Inherits="frmAutorizacaoCompraPendente" %>
<%@ Register Src="NotaEmpenho.ascx" TagName="NotaEmpenho" TagPrefix="uc" %>
<%@ Register Src="NaturezaDespesa.ascx" TagName="NaturezaDespesa" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc" %>

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
    <uc:NotaEmpenho runat="server" ID="ucNotaEmpenho"  />   
    <uc:NaturezaDespesa runat="server" ID="ucNaturezaDespesa"  />   
      <uc:MessageBox runat="server" ID="ucMessageBox"  />
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Autorizacões de Compra Pendentes
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="94%" style="height:350px;" >																		    
        <tr>
            <td valign="top">                
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >                   
                    <tr>
                        <td valign="top" align="center">
                            <div>
                            Comprador: &nbsp;
                            <Anthem:DropDownList runat="server" ID="ddlComprador" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            
                            Fonte Recurso: &nbsp;
                            <Anthem:DropDownList runat="server" ID="ddlFonteRecurso" />
                            &nbsp;
                            
                             <Anthem:Button runat="server" ID="btnPesquisar"  TextDuringCallBack="Aguarde" Text="Filtrar"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                            </div>
                             <anthem:GridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <RowStyle CssClass="dgItem" />
                                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblID_Status" Text='<%# ((AutorizacaoCompra)Container.DataItem).Status.ID %>'  />                                            
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                                       
                                    <asp:TemplateField HeaderText="Número" ItemStyle-HorizontalAlign="Center" SortExpression="Numero">
                                        <ItemTemplate>
                                            <%# ((AutorizacaoCompra)Container.DataItem).CodigoComAno %>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                     
                                    <asp:TemplateField HeaderText="Fornecedor" ItemStyle-HorizontalAlign="left" SortExpression="Fornecedor">
                                        <ItemTemplate>
                                            <%# ((AutorizacaoCompra)Container.DataItem).Fornecedor %>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                                   
                                    <asp:TemplateField HeaderText="Fonte Recurso" ItemStyle-HorizontalAlign="center" SortExpression="FonteRecurso">
                                        <ItemTemplate>
                                            <%# ((AutorizacaoCompra)Container.DataItem).FonteRecurso %>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                                   
                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="center" SortExpression="Status">
                                        <ItemTemplate>
                                            <%# ((AutorizacaoCompra)Container.DataItem).Status.Descricao %>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data" ItemStyle-HorizontalAlign="center" SortExpression="DataEmissao">
                                        <ItemTemplate>
                                            <%# ((AutorizacaoCompra)Container.DataItem).DataEmissao.ToShortDateString() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="right" SortExpression="ValorTotal">
                                        <ItemTemplate>
                                            <%# ((AutorizacaoCompra)Container.DataItem).ValorTotal.ToString("N2") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" CommandName="Edit" 
                                               CausesValidation="false"  /> 
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkDetalhes" Text="Detalhes" CausesValidation="false" />
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
