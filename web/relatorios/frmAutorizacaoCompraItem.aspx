<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAutorizacaoCompraItem.aspx.cs" Inherits="frmAutorizacaoCompraItem" %>
<%@ Register TagPrefix="WebControls" Assembly="Shared.WebControls" Namespace="Shared.WebControls" %>
<%@ Register Src="~/UserControls/ucColumnManager.ascx" TagPrefix="uc" TagName="ColumnManager" %>
<%@ Register Src="~/UserControls/CabecalhoRelatorio.ascx" TagPrefix="uc" TagName="Cabecalho" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet"  />   
    
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Itens de Autorização de Compra" />
    <uc:ColumnManager runat="server" ID="ucColumn" />  
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <WebControls:ReportGridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid" 
         AutoGenerateColumns="false" CellPadding="3" >
        <HeaderStyle CssClass="dgHeader" />                                            
        <AlternatingRowStyle CssClass="dgAlternatingItem" />
        <FooterStyle CssClass="dgFooter" />
        <Columns>
            <asp:TemplateField HeaderText="Número" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <%# ((PedidoCotacaoItem)Container.DataItem).AutorizacaoCompra.CodigoComAno %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Data" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <%# ((PedidoCotacaoItem)Container.DataItem).AutorizacaoCompra.DataEmissao.ToShortDateString() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fornecedor" ItemStyle-HorizontalAlign="Left" Visible="false" >
                <ItemTemplate>
                    <%# ((PedidoCotacaoItem)Container.DataItem).AutorizacaoCompra.Fornecedor %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Comprador" ItemStyle-HorizontalAlign="Left" Visible="false" >
                <ItemTemplate>
                    <%# ((PedidoCotacaoItem)Container.DataItem).AutorizacaoCompra.Servidor %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Classe" ItemStyle-HorizontalAlign="Left" >
                <ItemTemplate>
                    <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.ClasseServicoMaterial %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SubClasse" ItemStyle-HorizontalAlign="Left" >
                <ItemTemplate>
                    <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.SubClasseServicoMaterial %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.CodigoInterno %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" SortExpression="ServicoMaterial" >
                <ItemTemplate>
                    <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Especificação" ItemStyle-HorizontalAlign="Left" Visible="false" >
                <ItemTemplate>
                    <%# ((PedidoCotacaoItem)Container.DataItem).Especificacao %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <%# ((PedidoCotacaoItem)Container.DataItem).Valor.ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantidade" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <%# ((PedidoCotacaoItem)Container.DataItem).Quantidade.ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Valor Total" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <%# ((PedidoCotacaoItem)Container.DataItem).ValorTotal.ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField>
            
            
            <%--<asp:TemplateField HeaderText="Detalhes" ItemStyle-HorizontalAlign="Center"  Visible="true">
                <ItemTemplate>
                    <Anthem:LinkButton runat="server" ID="lnkDetalhes" Text="Detalhes" />
                </ItemTemplate>
            </asp:TemplateField> --%>
        </Columns>
    </WebControls:ReportGridView>  
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
