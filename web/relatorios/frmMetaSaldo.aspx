<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMetaSaldo.aspx.cs" Inherits="frmMetaSaldo" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Listagem de Saldos por Meta" />
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
             <asp:BoundField HeaderText="Meta" ItemStyle-HorizontalAlign="left"  SortExpression="Meta" 
                DataField="Meta" Visible="True" />
            <asp:BoundField HeaderText="Total Planejado" ItemStyle-HorizontalAlign="Right"  SortExpression="TotalPlanejado" 
                DataField="TotalPlanejado" Visible="True" DataFormatString="{0:N2}" HtmlEncode="true"/>
            <asp:BoundField HeaderText="Total Em Execução" ItemStyle-HorizontalAlign="Right"  SortExpression="TotalEmExecucao" 
                DataField="TotalEmExecucao" Visible="True" DataFormatString="{0:N2}" HtmlEncode="true"/>
            <asp:BoundField HeaderText="Total Realizado" ItemStyle-HorizontalAlign="Right"  SortExpression="TotalRealizado" 
                DataField="TotalRealizado" Visible="True" DataFormatString="{0:N2}" HtmlEncode="true"/>
            <asp:BoundField HeaderText="Saldo Disponível" ItemStyle-HorizontalAlign="right"  SortExpression="SaldoDisponivel" 
                DataField="SaldoDisponivel" Visible="True" DataFormatString="{0:N2}" HtmlEncode="true"/>
        </Columns>
    </WebControls:ReportGridView>  
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
