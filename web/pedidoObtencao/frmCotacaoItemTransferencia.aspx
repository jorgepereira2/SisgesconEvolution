<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCotacaoItemTransferencia.aspx.cs" Inherits="frmCotacaoItemTransferencia" %>
<%@ Register TagPrefix="uc" Src="~/UserControls/BuscaServicoMaterial.ascx" TagName="ServicoMaterial" %>
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
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Transferência de Item para Cotação
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
                                            Serviço/Material:
                                        </td>
                                        <td align="left">
                                           <uc:ServicoMaterial runat="server" ID="ucServicoMaterial" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Comprador:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlComprador" />
                                           &nbsp;
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
                             <anthem:DataGrid runat="server" ID="dgPesquisa" Width="100%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                <Columns>
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="center" headertext="AC" >
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItem)Container.DataItem).PedidoObtencao.CodigoComAno %>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="left" headertext="Serviço/Material" >
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                                                  
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="center" headertext="Quantidade"  >
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItem)Container.DataItem).Quantidade %>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                                                  
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="center" headertext="Comprador"  >
                                        <ItemTemplate>
                                            <%--<%# ((PedidoObtencaoItem)Container.DataItem).Comprador.NomeGuerra %>--%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlNovoComprador" />
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>                                                                  
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="center" headertext="" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkEditar" Text="Editar" CommandName="Edit" />
                                        </ItemTemplate>
                                         <EditItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkSalvar" Text="Salvar" CommandName="Update" />
                                            &nbsp;
                                            <Anthem:LinkButton runat="server" ID="lnkCancel" Text="Cancel" CommandName="Cancel" />
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>   
                                   
                                </Columns>
                            </anthem:DataGrid>
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
