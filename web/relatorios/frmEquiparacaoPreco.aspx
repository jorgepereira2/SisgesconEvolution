<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEquiparacaoPreco.aspx.cs" Inherits="frmEquiparacaoPreco" %>
<%@ Import Namespace="System.Data"%>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Equiparação de Preços" />
    <uc:ColumnManager runat="server" ID="ucColumn" />  
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <WebControls:ReportGridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid" 
         AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
        <HeaderStyle CssClass="dgHeader" />                                            
        <AlternatingRowStyle CssClass="dgAlternatingItem" />
        <FooterStyle CssClass="dgFooter" HorizontalAlign="Right" />
        <Columns>
            <asp:BoundField HeaderText="Item" ItemStyle-HorizontalAlign="left"  SortExpression="ServicoMaterial" 
                DataField="ServicoMaterial" Visible="True" />           
            <asp:BoundField HeaderText="Qtd." ItemStyle-HorizontalAlign="center"  SortExpression="Quantidade" 
                DataField="Quantidade" Visible="True" DataFormatString="{0:N0}" />
            <asp:TemplateField HeaderText="PO" ItemStyle-HorizontalAlign="Center" SortExpression="NumeroPO"  Visible="True">
                <ItemTemplate>
                    <Anthem:LinkButton runat="server" ID="lnkDetalhesPO" Text='<%# ((DataRowView)Container.DataItem)["NumeroPO"] %>' />
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Valor PO" ItemStyle-HorizontalAlign="right" SortExpression="ValorPO" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["ValorPO"]).ToString("N2")%>                    
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label runat="server" ID="lblValorTotalPO" Font-Bold="true" />
                </FooterTemplate>
            </asp:TemplateField>  
             
            <asp:TemplateField HeaderText="SubTotal PO" ItemStyle-HorizontalAlign="right" SortExpression="SubTotalPO" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["SubTotalPO"]).ToString("N2")%>                    
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label runat="server" ID="lblSubTotalPO" Font-Bold="true" />
                </FooterTemplate>
            </asp:TemplateField>             
            <asp:TemplateField HeaderText="AC" ItemStyle-HorizontalAlign="Center" SortExpression="NumeroAC"  Visible="True">
                <ItemTemplate>
                    <Anthem:LinkButton runat="server" ID="lnkDetalhesAC" Text='<%# ((DataRowView)Container.DataItem)["NumeroAC"] %>' />
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Valor AC" ItemStyle-HorizontalAlign="right" SortExpression="ValorAC" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["ValorAC"]).ToString("N2")%>                    
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label runat="server" ID="lblValorTotalAC" Font-Bold="true" />
                </FooterTemplate>
            </asp:TemplateField> 
             <asp:TemplateField HeaderText="SubTotal AC" ItemStyle-HorizontalAlign="right" SortExpression="SubTotalAC" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["SubTotalAC"]).ToString("N2")%>                    
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label runat="server" ID="lblSubTotalAC" Font-Bold="true" />
                </FooterTemplate>
            </asp:TemplateField> 
            
            <asp:TemplateField HeaderText="Diferença Unitária" ItemStyle-HorizontalAlign="right" SortExpression="DiferencaUnitaria" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["DiferencaUnitaria"]).ToString("N2")%>                    
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label runat="server" ID="lblDiferencaUnitaria" Font-Bold="true" />
                </FooterTemplate>
            </asp:TemplateField> 
            
            <asp:TemplateField HeaderText="Diferença Total" ItemStyle-HorizontalAlign="right" SortExpression="DiferencaTotal" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["DiferencaTotal"]).ToString("N2")%>                    
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label runat="server" ID="lblDiferencaTotal" Font-Bold="true" />
                </FooterTemplate>
            </asp:TemplateField> 
        </Columns>
    </WebControls:ReportGridView>  
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
