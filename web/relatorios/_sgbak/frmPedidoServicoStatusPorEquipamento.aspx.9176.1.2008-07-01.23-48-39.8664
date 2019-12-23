<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoStatusPorEquipamento.aspx.cs" Inherits="frmPedidoServicoStatusPorEquipamento" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Status de Pedidos de Serviço por Equipamento" />    
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <asp:DataList runat="server" ID="dlEquipamento" Width="100%">
        <ItemTemplate>
            <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                <tr>
                    <td align="left" style="width:50%">
                        <b style="font-size:12px">                            
                            <%# ((DataRowView)Container.DataItem)["Equipamento"].ToString() %>    
                        </b>       
                    </td>
                    <td align="right">
                        Total:<b style="font-size:12px"><%# ((DataRowView)Container.DataItem)["Quantidade"].ToString() %></b>
                    </td>
                </tr>
            </table>           
             
            </div>
            <ul>
                           
                <Anthem:DataGrid runat="server" ID="dgStatus" AutoGenerateColumns="True" Width="100%"
                    CssClass="datagrid" OnItemDataBound="dgStatus_ItemDataBound" >
                    <HeaderStyle CssClass="dgHeader" /> 
                    <ItemStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                 <%# ((DataRowView)Container.DataItem)["Status"].ToString().Substring(((DataRowView)Container.DataItem)["Status"].ToString().LastIndexOf("|") + 1)%>    
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
