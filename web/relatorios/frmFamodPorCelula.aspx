<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFamodPorCelula.aspx.cs" Inherits="frmFamodPorCelula" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Famod por Célula" />    
    <div>
    <br />
    <asp:LinkButton runat="server" ID="btnExportar" Text="Exportar para o Excel" cssclass="noprint" />    
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <asp:DataList runat="server" ID="dlDivisao" Width="100%">
        <ItemTemplate>
            <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                <tr>
                    <td align="left" style="width:38%">
                        <b style="font-size:12px"><%# ((DataRowView)Container.DataItem)["Divisao"].ToString() %></b>       
                    </td>
                    <td align="right">
                        Horas Disponíveis:<b style="font-size:12px">
                            <Anthem:Label runat="server" ID="lblHorasDisponiveis" />                            
                        </b>
                    </td>
                    <td align="right">
                        Horas Apropriadas:<b style="font-size:12px"><%# ((DataRowView)Container.DataItem)["HorasApropriadas"].ToString()%></b>
                    </td>
                    <td align="right">
                        Diferença:<b style="font-size:12px">
                            <Anthem:Label runat="server" ID="lblDiferenca" /> 
                        </b>
                    </td>
                </tr>
            </table>           
             
            </div>
            <ul>
                           
                <Anthem:DataGrid runat="server" ID="dgSecao" AutoGenerateColumns="false" Width="100%"
                    CssClass="datagrid" >
                    <HeaderStyle CssClass="dgHeader" /> 
                    <Columns>
                        <asp:BoundColumn HeaderText="Código" DataField="CodigoCelula" ItemStyle-HorizontalAlign="center"/>
                        <asp:BoundColumn HeaderText="Célula" DataField="Celula" ItemStyle-HorizontalAlign="left" />
                        <asp:BoundColumn HeaderText="Horas Disponíveis" DataField="HorasDisponiveis" ItemStyle-HorizontalAlign="center" />
                         <asp:BoundColumn HeaderText="Horas Apropriadas" DataField="HorasApropriadas" ItemStyle-HorizontalAlign="center" />
                        <asp:TemplateColumn HeaderText="Diferença" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Convert.ToInt32(((DataRowView)Container.DataItem)["HorasDisponiveis"]) - Convert.ToInt32(((DataRowView)Container.DataItem)["HorasApropriadas"])%>
                            </ItemTemplate>
                        </asp:TemplateColumn>                        
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
