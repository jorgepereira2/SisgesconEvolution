<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCotaMaterialPesquisa.aspx.cs" Inherits="frmCotaMaterialPesquisa" %>
<%@ Register Src="~/UserControls/BuscaServicoMaterial.ascx" TagPrefix="uc" TagName="BuscaMaterial" %>
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
        Pesquisa de Cota de Material
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
                                            Ano:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlAno" />
                                        </td>
                                         <td align="right">
                                            Mês:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlMes" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Célula:
                                        </td>
                                        <td align="left" colspan="3">
                                            <Anthem:DropDownList runat="server" ID="ddlCelula" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Material:
                                        </td>
                                        <td align="left" colspan="3">
                                            <uc:BuscaMaterial runat="server" TipoServicoMaterial="Material" ID="ucMaterial" />
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
                             <anthem:GridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid" PageSize="20" 
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="true" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <RowStyle CssClass="dgItem" />
                                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                <PagerSettings Mode="NextPrevious" Position="Bottom" />
                                <PagerStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="12px"  />
                                <Columns>
                                    <asp:TemplateField HeaderText="Célula" SortExpression="Celula">
                                        <ItemTemplate>
                                            <%# ((CotaMaterial)Container.DataItem).Celula.Codigo + " - " + ((CotaMaterial)Container.DataItem).Celula.Descricao %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material" SortExpression="Material">
                                        <ItemTemplate>
                                            <%# ((CotaMaterial)Container.DataItem).Material.CodigoInterno + " - " + ((CotaMaterial)Container.DataItem).Material.Descricao %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mês" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# ((CotaMaterial)Container.DataItem).Mes + "/" + ((CotaMaterial)Container.DataItem).Ano %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Quantidade" ItemStyle-HorizontalAlign="center"  SortExpression="Quantidade" 
                                        DataField="Quantidade" DataFormatString="{0:N0}" />
                                    <asp:HyperLinkField NavigateUrl="" Text="Editar" itemstyle-horizontalalign="center"
                                         DataNavigateUrlFields="ID" DataNavigateUrlFormatString="frmCotaMaterialCadastro.aspx?id_cota={0}" />                                       
                                </Columns>
                            </anthem:gridview>
                            <Anthem:Panel runat="server" ID="pnMensagem" CssClass="msgErro" Visible="false" >
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
