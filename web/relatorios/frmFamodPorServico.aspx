<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFamodPorServico.aspx.cs" Inherits="frmFamodPorServico" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Famod por Serviço" />    
    <div>
    <br />
    <asp:LinkButton runat="server" ID="btnExportar" Text="Exportar para o Excel" cssclass="noprint" />  
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <asp:DataList runat="server" ID="dlCategoria" Width="100%">
        <ItemTemplate>
            <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                <tr>
                    <td align="left" style="width:30%">
                        <b style="font-size:12px"><%# ((DataRowView)Container.DataItem)["CategoriaServico"].ToString() %></b>       
                    </td>
                    <td align="right">
                        Horas Apropriadas:<b style="font-size:12px"><%# ((DataRowView)Container.DataItem)["TotalHorasApropriadas"].ToString()%></b>
                    </td>
                    <td align="right">
                        Horas Delineadas:<b style="font-size:12px">
                            <asp:Label runat="server" ID="lblTotalHorasDelineadas" />
                        </b>
                    </td>
                    <td align="right">
                        Diferença:<b style="font-size:12px">
                         <asp:Label runat="server" ID="lblDiferenca" />                        
                    </td>
                </tr>
            </table>           
             
            </div>
            <ul>
                           
                <Anthem:DataGrid runat="server" ID="dgPedidoServico" AutoGenerateColumns="false" Width="100%"
                    CssClass="datagrid" >
                    <HeaderStyle CssClass="dgHeader" /> 
                    <Columns>                                        
                        <asp:BoundColumn HeaderText="Código Cliente" DataField="CodigoCliente" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundColumn HeaderText="Cliente" DataField="Cliente" ItemStyle-HorizontalAlign="left" />
                        <asp:BoundColumn HeaderText="PS" DataField="CodigoPS" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundColumn HeaderText="Status PS" DataField="StatusPS" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundColumn HeaderText="Equipamento" DataField="Equipamento" ItemStyle-HorizontalAlign="left" />
                        
                        <asp:BoundColumn HeaderText="Horas Apropriadas" DataField="TotalHorasApropriadas" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundColumn HeaderText="Horas Delineadas" DataField="TotalHorasDelineadas" ItemStyle-HorizontalAlign="center" />                         
                        <asp:BoundColumn HeaderText="Diferença" DataField="Diferenca" ItemStyle-HorizontalAlign="center" />                         
                    </Columns>
                </Anthem:DataGrid>
                    
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
