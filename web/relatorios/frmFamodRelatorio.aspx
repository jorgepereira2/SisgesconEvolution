<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFamodRelatorio.aspx.cs" Inherits="frmFamodRelatorio" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="FAMOD" />   
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <asp:DataList runat="server" ID="dlOficina" Width="100%">
        <ItemTemplate>
            <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                <tr>
                    <td align="left">
                        <b style="font-size:12px"><%# ((DataRowView)Container.DataItem)["Oficina"].ToString() %></b>       
                    </td>
                    <td align="right">
                        Horas:<b style="font-size:12px"><%# ((DataRowView)Container.DataItem)["TotalHoras"].ToString() %></b>
                    </td>
                </tr>
            </table>           
             
            </div>
            <ul>
            <asp:DataList runat="server" ID="dlServidor" Width="100%" OnItemDataBound="dlServidor_ItemDataBound">
                <ItemTemplate>
                    <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                        <tr>
                            <td align="left">
                                <b><%# ((DataRowView)Container.DataItem)["Servidor"].ToString() %></b>
                            </td>
                            <td align="right">
                                Horas:<b style="font-size:12px"><%# ((DataRowView)Container.DataItem)["TotalHoras"].ToString() %></b>
                            </td>
                        </tr>
                    </table>     
                    <br />
                    <ul>
                      <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                        <tr>
                            <td align="left">
                                <b>Atividades Diretas</b>
                            </td>
                            <td align="right">
                                Horas: <asp:Label runat="server" ID="lblTotalHorasAtividadeDireta" />
                            </td>
                        </tr>
                    </table>                    
                    <asp:DataGrid runat="server" ID="dgAtividadeDireta" AutoGenerateColumns="false" Width="100%"
                        CssClass="datagrid">
                        <HeaderStyle CssClass="dgHeader" /> 
                        <Columns>
                            <asp:BoundColumn HeaderText="Atividade" DataField="Atividade" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundColumn HeaderText="Data" DataField="Data" ItemStyle-HorizontalAlign="center"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundColumn HeaderText="Horas" DataField="HorasApropriadas" ItemStyle-HorizontalAlign="center" />
                        </Columns>
                    </asp:DataGrid>
                    
                    <br />
                    
                    <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                        <tr>
                            <td align="left">
                                <b>Atividades Indiretas</b>
                            </td>
                            <td align="right">
                                Horas: <asp:Label runat="server" ID="lblTotalHorasAtividadeIndireta" />
                            </td>
                        </tr>
                    </table>                    
                    <asp:DataGrid runat="server" ID="dgAtividadeIndireta" AutoGenerateColumns="false" Width="100%"
                        CssClass="datagrid">
                        <HeaderStyle CssClass="dgHeader" /> 
                        <Columns>
                            <asp:BoundColumn HeaderText="Atividade" DataField="Atividade" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundColumn HeaderText="Data" DataField="Data" ItemStyle-HorizontalAlign="center" 
                                DataFormatString="{0:dd/MM/yyyy}"/>
                            <asp:BoundColumn HeaderText="Horas" DataField="HorasApropriadas" ItemStyle-HorizontalAlign="center" />
                        </Columns>
                    </asp:DataGrid>
                    <br />
                    </ul>    
                </ItemTemplate>
            </asp:DataList>
            </ul>
        </ItemTemplate>      
      </asp:DataList>
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
