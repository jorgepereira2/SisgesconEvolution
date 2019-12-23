<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmServidorPermissao.aspx.cs" Inherits="frmServidorPermissao" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Serviço/Material por Natureza Despesa" />
    <uc:ColumnManager runat="server" ID="ucColumn" />  
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >

      <asp:DataList runat="server" ID="dlProcesso" Width="100%">
        <ItemTemplate>
            <div>
               <b style="font-size:12px">
               <%#  ((Processo)Container.DataItem).TextoCaminho %>
               </b>      
             
            </div>
            <ul>

              <WebControls:ReportGridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid" 
                 AutoGenerateColumns="false" CellPadding="3" col >
                <HeaderStyle CssClass="dgHeader" />                                            
                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                <FooterStyle CssClass="dgFooter" />
                <Columns>
                       <asp:BoundField HeaderText="NIP" ItemStyle-HorizontalAlign="center"  SortExpression="NIP" 
                            DataField="NIP" Visible="True" />
                        <asp:BoundField HeaderText="Nome Guerra" ItemStyle-HorizontalAlign="center"  SortExpression="NomeGuerra" 
                            DataField="NomeGuerra" Visible="True" />
                        <asp:BoundField HeaderText="Graduação" ItemStyle-HorizontalAlign="center"  SortExpression="Graduacao" 
                            DataField="Graduacao" Visible="True" />    
                        <asp:BoundField HeaderText="Nome" ItemStyle-HorizontalAlign="left"  SortExpression="NomeCompleto" 
                            DataField="NomeCompleto" Visible="True" />
                         <asp:TemplateField HeaderText="Código Célula" ItemStyle-HorizontalAlign="center"  Visible="true">
                            <ItemTemplate>
                                <%# ((Servidor)Container.DataItem).Celula.Codigo %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Célula" ItemStyle-HorizontalAlign="center"  SortExpression="Celula" 
                            DataField="Celula" Visible="True" />           
                            </Columns>
            </WebControls:ReportGridView>  

       <br />
            </ul>
        </ItemTemplate>      
      </asp:DataList>
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
