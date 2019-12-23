<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoCustoRealizado.aspx.cs" Inherits="frmPedidoServicoCustoRealizado" %>
<%@ Import namespace="System.Data"%>
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
    <uc:Cabecalho runat="server" Titulo="Custo X Realizado por PS" />
    <uc:ColumnManager runat="server" ID="ucColumn" />  
    <div>
    <br />
        <Anthem:Panel runat="server" id="pnGrid">
      <WebControls:ReportGridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid" 
         AutoGenerateColumns="false" CellPadding="3" AllowSorting="false" >
        <HeaderStyle CssClass="dgHeader" />                                            
        <AlternatingRowStyle CssClass="dgAlternatingItem" />
        <FooterStyle CssClass="dgFooter" />
        <Columns>
            <asp:TemplateField HeaderText="Código Interno" ItemStyle-HorizontalAlign="center" Visible="True" SortExpression="Codigo">
                <ItemTemplate>
                    <%# ((DataRowView)Container.DataItem)["Codigo"].ToString()%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Data" ItemStyle-HorizontalAlign="center" Visible="True" SortExpression="DataEmissao">
                <ItemTemplate>
                    <%# Convert.ToDateTime(((DataRowView)Container.DataItem)["DataEmissao"]).ToShortDateString()%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cliente" ItemStyle-HorizontalAlign="left"  Visible="True" SortExpression="Cliente" >
                <ItemTemplate>
                    <%# ((DataRowView)Container.DataItem)["Cliente"] %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="HH Delineado(O)" ItemStyle-HorizontalAlign="center"  Visible="true" SortExpression="TotalHHDelineamento"  >
                <ItemTemplate>
                    <%# ((DataRowView)Container.DataItem)["TotalHHDelineamento"] %>
                </ItemTemplate>
            </asp:TemplateField>     
            <asp:TemplateField HeaderText="HH Famod(R)" ItemStyle-HorizontalAlign="center"  Visible="true" SortExpression="TotalHHFamod" >
                <ItemTemplate>
                    <%# ((DataRowView)Container.DataItem)["TotalHHFamod"]%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Diferença Horas" ItemStyle-HorizontalAlign="center"  Visible="true"  >
                <ItemTemplate>
                    <%#  Convert.ToInt32(((DataRowView)Container.DataItem)["TotalHHDelineamento"]) - Convert.ToInt32(((DataRowView)Container.DataItem)["TotalHHFamod"])%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="MOD Famod(R)" ItemStyle-HorizontalAlign="center"  Visible="true" SortExpression="MODFamod" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["MODFamod"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Material Orçado(O)" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="TotalMaterialOrcado" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["TotalMaterialOrcado"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Material Comprado(R)" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="TotalMaterialComprado" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["TotalMaterialComprado"]).ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Material PEP(R)" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="TotalMaterialPEP" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["TotalMaterialPEP"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total de Material Direto(R)" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="TotalMaterialDireto" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["TotalMaterialDireto"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Sub Total Realizado(R)" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="SubTotalRealizado" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["SubTotalRealizado"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Material Rodízio(R)" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="TotalMaterialRodizio" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["TotalMaterialRodizio"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Custo Material(R)" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="TotalCustoMaterial" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["TotalCustoMaterial"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Diferença Material" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="DiferencaMaterialOrcadoRealizado" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["DiferencaMaterialOrcadoRealizado"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Serviço Orçado(O)" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="TotalServicoOrcado" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["TotalServicoOrcado"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Custo Serviços(R)" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="TotalServicoComprado" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["TotalServicoComprado"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Diferença Serviço" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="DiferencaServico" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["DiferencaServico"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
             <%--<asp:TemplateField HeaderText="MOI" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="MOI" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["MOI"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MI" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="MI" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["MI"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SI" ItemStyle-HorizontalAlign="right"  Visible="true" SortExpression="SI" >
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["SI"]).ToString("N2")%>
                </ItemTemplate>
            </asp:TemplateField>--%>
             <asp:TemplateField HeaderText="Detalhes" ItemStyle-HorizontalAlign="center"  Visible="true" >
                <ItemTemplate>
                    <Anthem:LinkButton runat="server" ID="lnkDetalhes" Text="Detalhes" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </WebControls:ReportGridView>  
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
