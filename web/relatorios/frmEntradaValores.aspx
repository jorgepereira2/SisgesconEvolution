<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEntradaValores.aspx.cs" Inherits="frmEntradaValores" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Movimento Financeiro" />
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
             <asp:BoundField HeaderText="Ação Interna" ItemStyle-HorizontalAlign="left"  SortExpression="Projeto" 
                DataField="Projeto" Visible="True" />
            <asp:BoundField HeaderText="Natureza Despesa" ItemStyle-HorizontalAlign="left"  SortExpression="NaturezaDespesa" 
                DataField="NaturezaDespesa" Visible="True" />                         
            <asp:BoundField HeaderText="Fonte Recurso" ItemStyle-HorizontalAlign="left"  SortExpression="Fonterecurso" 
                DataField="FonteRecurso" Visible="True" />
            <asp:BoundField HeaderText="PTRES" ItemStyle-HorizontalAlign="left"  SortExpression="PTRES" 
                DataField="PTRES" Visible="True" />
            <asp:BoundField HeaderText="Entrada" ItemStyle-HorizontalAlign="right"  SortExpression="ValorEntrada" 
                DataField="ValorEntrada" Visible="True" />
            <asp:BoundField HeaderText="Comprometido" ItemStyle-HorizontalAlign="right"  SortExpression="ValorComprometido" 
                DataField="ValorComprometido" Visible="True" />
            <asp:BoundField HeaderText="Empenhado" ItemStyle-HorizontalAlign="right"  SortExpression="ValorEmpenhado" 
                DataField="ValorEmpenhado" Visible="True" />
            <asp:TemplateField HeaderText="Saldo" ItemStyle-HorizontalAlign="right"  Visible="true">
                <ItemTemplate>
                    <%# Convert.ToDecimal(((DataRowView)Container.DataItem)["ValorEntrada"]) - Convert.ToDecimal(((DataRowView)Container.DataItem)["ValorComprometido"]) - Convert.ToDecimal(((DataRowView)Container.DataItem)["ValorEmpenhado"])%>
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
