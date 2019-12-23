<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmServicoMaterialListagem.aspx.cs" Inherits="frmServicoMaterialListagem" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Listagem de Serviço/Material" />
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
             <asp:BoundField HeaderText="Código" ItemStyle-HorizontalAlign="center"  SortExpression="CodigoInterno" 
                DataField="CodigoInterno" Visible="True" />
            <asp:BoundField HeaderText="Descrição" ItemStyle-HorizontalAlign="left"  SortExpression="Descricao" 
                DataField="Descricao" Visible="True" />                         
            <asp:BoundField HeaderText="Classe" ItemStyle-HorizontalAlign="center"  SortExpression="ClasseServicoMaterial" 
                DataField="ClasseServicoMaterial" Visible="True" />
            <asp:BoundField HeaderText="Sub-Classe" ItemStyle-HorizontalAlign="center"  SortExpression="SubClasseServicoMaterial" 
                DataField="SubClasseServicoMaterial" Visible="True" />
            <asp:BoundField HeaderText="SJB" ItemStyle-HorizontalAlign="center"  SortExpression="SJB" 
                DataField="SJB" Visible="True" />
            <asp:BoundField HeaderText="Natureza Despesa" ItemStyle-HorizontalAlign="center"  SortExpression="NaturezaDespesa" 
                DataField="NaturezaDespesa" Visible="True" />
            <asp:BoundField HeaderText="Origem" ItemStyle-HorizontalAlign="center"  SortExpression="OrigemMaterial" 
                DataField="OrigemMaterial" Visible="False" />
            <asp:BoundField HeaderText="Unidade" ItemStyle-HorizontalAlign="center"  SortExpression="Unidade" 
                DataField="Unidade" Visible="False" />
            <asp:BoundField HeaderText="Descrição Singra" ItemStyle-HorizontalAlign="left"  SortExpression="DescricaoSingra" 
                DataField="DescricaoSingra" Visible="False" />            
        </Columns>
    </WebControls:ReportGridView>  
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
