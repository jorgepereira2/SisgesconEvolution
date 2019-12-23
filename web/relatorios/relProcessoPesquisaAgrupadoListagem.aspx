<%@ Page Language="C#" AutoEventWireup="true" CodeFile="relProcessoPesquisaAgrupadoListagem.aspx.cs" Inherits="relPessoaLista" %>

<%@ Import Namespace="Marinha.Business" %>
<%@ Register TagPrefix="WebControls" Assembly="Shared.WebControls" Namespace="Shared.WebControls" %>
<%@ Register Src="~/UserControls/ucColumnManager.ascx" TagPrefix="uc" TagName="ColumnManager" %>
<%@ Register Src="~/UserControls/CabecalhoRelatorio.ascx" TagPrefix="uc" TagName="Cabecalho" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet"  />      
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div align="center" style="width:90%">
    <br />
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Listagem Controle de Acesso" />   
    </div>

    <uc:ColumnManager runat=server ID="ucColumn" />

    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >

        <div style="color: #FFF; background: #00538a; width: 100%; padding: 10px; text-align: left; border-bottom: 1px solid #000;">
            <div style="font-weight: bold;">MENUS:</div>
            <Anthem:Repeater runat="server" ID="dlProcesso"> 
                <ItemTemplate>            
                     <%# ((Processo)Container.DataItem).Root((Processo)Container.DataItem) %><br>
                </ItemTemplate>
            </Anthem:Repeater>
        </div>

        <WebControls:ReportGridView runat="server" ID="gvServidor" Width="100%" 
          CssClass="datagrid" AutoGenerateColumns="false" CellPadding="3" UseAccessibleHeader="True">
            <HeaderStyle CssClass="dgHeader" />                                    
            <RowStyle CssClass="dgItem" />
            <AlternatingRowStyle CssClass="dgAlternatingItem" />
            <FooterStyle CssClass="dgFooter" />
            <Columns>
                <asp:BoundField HeaderText="Nome" ItemStyle-HorizontalAlign="left"  SortExpression="Nome" DataField="NomeCompleto" />                                 
                <asp:BoundField HeaderText="Login" ItemStyle-HorizontalAlign="left"  SortExpression="Login" DataField="Login" />                                 
                <asp:BoundField HeaderText="Email" ItemStyle-HorizontalAlign="left"  SortExpression="Email" DataField="Email" />                                 
            </Columns>
        </WebControls:ReportGridView>

    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>