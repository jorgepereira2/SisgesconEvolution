<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOMFListagem.aspx.cs" Inherits="frmOMFListagem" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="OMF" />
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
            <asp:BoundField HeaderText="Número Nota" ItemStyle-HorizontalAlign="center"  SortExpression="NumeroNota" 
                DataField="NumeroNota" Visible="True" />
            <asp:BoundField HeaderText="Data Entrega" ItemStyle-HorizontalAlign="center"  SortExpression="DataEntrega" 
                DataField="DataEntrega" Visible="True" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" />
             <asp:BoundField HeaderText="Número Empenho" ItemStyle-HorizontalAlign="center"  SortExpression="NumeroEmpenho" 
                DataField="NumeroEmpenho" Visible="True" />
            <asp:BoundField HeaderText="Fornecedor" ItemStyle-HorizontalAlign="left"  SortExpression="Fornecedor" 
                DataField="Fornecedor" Visible="True" />
            <asp:BoundField HeaderText="Recebedor" ItemStyle-HorizontalAlign="left"  SortExpression="Recebedor" 
                DataField="Recebedor" Visible="True" />
            <asp:BoundField HeaderText="Status" ItemStyle-HorizontalAlign="center"  SortExpression="Status" 
                DataField="Status" Visible="True" />             
            <asp:BoundField HeaderText="Material" ItemStyle-HorizontalAlign="left"  SortExpression="DescriminacaoMaterial" 
                DataField="DescriminacaoMaterial" Visible="True"  />
            <asp:BoundField HeaderText="Tipo Emprego" ItemStyle-HorizontalAlign="center"  SortExpression="TipoEmprego" 
                DataField="TipoEmprego" Visible="True"  />      
            <asp:TemplateField HeaderText="Detalhes" ItemStyle-HorizontalAlign="Center"  Visible="true">
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
