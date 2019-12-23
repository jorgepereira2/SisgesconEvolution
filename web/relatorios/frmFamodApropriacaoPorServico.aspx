<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFamodApropriacaoPorServico.aspx.cs" Inherits="frmFamodApropriacaoPorServico" %>
<%@ Import namespace="System.Collections.Generic"%>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Apropriação da Famod por Serviço" />    
    <div>
    <br />
    <asp:LinkButton runat="server" ID="btnExportar" Text="Exportar para o Excel" cssclass="noprint" />    
    <br />
    
   <div align="left" style="width:98%;" id="divFiltros" runat="server">
        <b style="color:#000080">Filtros</b>
        <hr noshade="noshade" />
        <asp:DataList runat="server" ID="dlFiltros" Width="98%" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table">
            <ItemStyle HorizontalAlign="left" CssClass="formtexto" />
            <ItemTemplate>
                <b><%# ((KeyValuePair<string, string>)Container.DataItem).Key %></b>:
                <%# ((KeyValuePair<string, string>)Container.DataItem).Value %>
            </ItemTemplate>
        </asp:DataList>           
    </div>  
     <hr noshade="noshade" />
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
                   
                </tr>
            </table>           
             
            </div>
            <ul>
                           
                <Anthem:DataGrid runat="server" ID="dgPedidoServico" AutoGenerateColumns="false" Width="100%"
                    CssClass="datagrid" >
                    <HeaderStyle CssClass="dgHeader" /> 
                    <Columns>
                        <asp:TemplateColumn HeaderText="Cliente" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# ((DataRowView)Container.DataItem)["CodigoCliente"].ToString() + " - " + ((DataRowView)Container.DataItem)["Cliente"].ToString() %>
                            </ItemTemplate>
                        </asp:TemplateColumn>                        
                        <asp:BoundColumn HeaderText="PS" DataField="CodigoPS" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundColumn HeaderText="Status PS" DataField="StatusPS" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundColumn HeaderText="Equipamento" DataField="Equipamento" ItemStyle-HorizontalAlign="left" />
                        
                        <asp:BoundColumn HeaderText="Horas Apropriadas" DataField="TotalHorasApropriadas" ItemStyle-HorizontalAlign="center" />
                                             
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
