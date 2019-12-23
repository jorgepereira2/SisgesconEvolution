<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFornecedorSaldo.aspx.cs" Inherits="frmFornecedorSaldo" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Listagem de Saldos por Fornecedores" />
    <uc:ColumnManager runat="server" ID="ucColumn" />  
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
    <%--<div>
        <b>Total:&nbsp; <Anthem:Label runat="server" ID="lblTotal" />
        </b>
    </div>--%>
      <WebControls:ReportGridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid" 
         AutoGenerateColumns="false" CellPadding="3" >
        <HeaderStyle CssClass="dgHeader" />                                            
        <AlternatingRowStyle CssClass="dgAlternatingItem" />
        <FooterStyle CssClass="dgFooter" />
        <Columns>
             <asp:BoundField HeaderText="Razão Social" ItemStyle-HorizontalAlign="left"  SortExpression="RazaoSocial" 
                DataField="Razaosocial" Visible="True" />
            <asp:BoundField HeaderText="Limite Anual" ItemStyle-HorizontalAlign="Right"  SortExpression="LimiteAnual" 
                DataField="LimiteAnual" Visible="True" DataFormatString="{0:N2}" HtmlEncode="true"/>
            <asp:BoundField HeaderText="Valor Utilizado" ItemStyle-HorizontalAlign="Right"  SortExpression="ValorUtilizado" 
                DataField="ValorUtilizado" Visible="True" DataFormatString="{0:N2}" HtmlEncode="true"/>
            <asp:BoundField HeaderText="Saldo Disponível" ItemStyle-HorizontalAlign="Right"  SortExpression="SaldoDisponivel" 
                DataField="SaldoDisponivel" Visible="True" DataFormatString="{0:N2}" HtmlEncode="true"/>
            <asp:BoundField HeaderText="Valor Comprometido" ItemStyle-HorizontalAlign="right"  SortExpression="ValorARealizar" 
                DataField="ValorARealizar" Visible="True" DataFormatString="{0:N2}" HtmlEncode="true"/>
        </Columns>
    </WebControls:ReportGridView>  
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
