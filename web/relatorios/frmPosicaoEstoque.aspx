<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPosicaoEstoque.aspx.cs" Inherits="frmPosicaoEstoque" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Posição Estoque" />
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
            <asp:BoundField HeaderText="Código" ItemStyle-HorizontalAlign="center"  SortExpression="CodigoMaterial" 
                DataField="CodigoMaterial" Visible="True" />
             <asp:BoundField HeaderText="Material" ItemStyle-HorizontalAlign="left"  SortExpression="Material" 
                DataField="Material" Visible="True" />
             <asp:BoundField HeaderText="Localizações" ItemStyle-HorizontalAlign="left"  SortExpression="Localizacoes" 
                DataField="Localizacoes" Visible="True" />
            <asp:BoundField HeaderText="Entrada" ItemStyle-HorizontalAlign="center"  SortExpression="QuantidadeEntrada" 
                DataField="QuantidadeEntrada" Visible="True" DataFormatString="{0:N2}"/>
            <asp:BoundField HeaderText="Saida" ItemStyle-HorizontalAlign="center"  SortExpression="QuantidadeSaida" 
                DataField="QuantidadeSaida" Visible="True" DataFormatString="{0:N2}" />
            <asp:BoundField HeaderText="Saldo" ItemStyle-HorizontalAlign="center"  SortExpression="Saldo" 
                DataField="Saldo" Visible="True" DataFormatString="{0:N2}"/>    
            <asp:BoundField HeaderText="Reservado" ItemStyle-HorizontalAlign="center"  SortExpression="QuantidadeReservada" 
                DataField="QuantidadeReservada" Visible="True" DataFormatString="{0:N2}"/>    
             <asp:BoundField HeaderText="Disponível" ItemStyle-HorizontalAlign="center"  SortExpression="QuantidadeDisponivel" 
                DataField="QuantidadeDisponivel" Visible="True" DataFormatString="{0:N2}"/>
        </Columns>
    </WebControls:ReportGridView>  
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
