<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoHistoricoStatus.aspx.cs" Inherits="frmPedidoServicoHistoricoStatus" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Relatório de Controle de OS por Célula" />    
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <asp:DataList runat="server" ID="dlCelula" Width="100%">
        <ItemTemplate>
            <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                <tr>
                    <td align="left" style="width:50%">
                        <b style="font-size:12px">
                            <%# ((DataRowView)Container.DataItem)["Codigo"].ToString() %> - 
                            <%# ((DataRowView)Container.DataItem)["Celula"].ToString() %>    
                        </b>       
                    </td>
                    <td align="right">
                        Total:<b style="font-size:12px"><%# ((DataRowView)Container.DataItem)["Quantidade"].ToString() %></b>
                    </td>
                </tr>
            </table>           
             
            </div>
            
            <ul>
            <asp:DataList runat="server" ID="dlProgem" Width="100%" OnItemDataBound="dlProgem_ItemDataBound">
                <ItemTemplate>
                    <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                        <tr>
                            <td align="left" style="width:50%">
                                <b style="font-size:12px">
                                    <%# ((DataRowView)Container.DataItem)["DescricaoProgem"].ToString() %> 
                                </b>       
                            </td>
                            <td align="right">
                                Total:<b style="font-size:12px"><%# ((DataRowView)Container.DataItem)["Quantidade"].ToString() %></b>
                            </td>
                        </tr>
                    </table>           
                     
                    </div>
            
            
                    <ul>
                    <asp:DataList runat="server" ID="dlTipoCliente" Width="100%" OnItemDataBound="dlTipoCliente_ItemDataBound">
                        <ItemTemplate>
                            <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                                <tr>
                                    <td align="left" style="width:50%">
                                        <b style="font-size:12px">
                                            <%# ((DataRowView)Container.DataItem)["TipoCliente"].ToString() %> 
                                        </b>       
                                    </td>
                                    <td align="right">
                                        Total:<b style="font-size:12px"><%# ((DataRowView)Container.DataItem)["Quantidade"].ToString() %></b>
                                    </td>
                                </tr>
                            </table>           
                             
                            </div>
                    
                    
                            <ul>
                                   
                            <Anthem:DataGrid runat="server" ID="dgStatus" AutoGenerateColumns="False" Width="100%"
                                CssClass="datagrid"  >
                                <HeaderStyle CssClass="dgHeader" /> 
                                <ItemStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                             <%# ((DataRowView)Container.DataItem)["Status"].ToString().Substring(((DataRowView)Container.DataItem)["Status"].ToString().LastIndexOf("|") + 1)%>    
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                
                                    <asp:BoundColumn HeaderText="Jan" ItemStyle-HorizontalAlign="center"  SortExpression="1" DataField="1" />
                                    <asp:BoundColumn HeaderText="Fev" ItemStyle-HorizontalAlign="center"  SortExpression="2" DataField="2" />
                                    <asp:BoundColumn HeaderText="Mar" ItemStyle-HorizontalAlign="center"  SortExpression="3" DataField="3" />
                                    <asp:BoundColumn HeaderText="Abr" ItemStyle-HorizontalAlign="center"  SortExpression="4" DataField="4" />
                                    <asp:BoundColumn HeaderText="Mai" ItemStyle-HorizontalAlign="center"  SortExpression="5" DataField="5" />
                                    <asp:BoundColumn HeaderText="Jun" ItemStyle-HorizontalAlign="center"  SortExpression="6" DataField="6" />
                                    <asp:BoundColumn HeaderText="Jul" ItemStyle-HorizontalAlign="center"  SortExpression="7" DataField="7" />
                                    <asp:BoundColumn HeaderText="Ago" ItemStyle-HorizontalAlign="center"  SortExpression="8" DataField="8" />
                                    <asp:BoundColumn HeaderText="Set" ItemStyle-HorizontalAlign="center"  SortExpression="9" DataField="9" />
                                    <asp:BoundColumn HeaderText="Out" ItemStyle-HorizontalAlign="center"  SortExpression="10" DataField="10" />
                                    <asp:BoundColumn HeaderText="Nov" ItemStyle-HorizontalAlign="center"  SortExpression="11" DataField="11" />
                                    <asp:BoundColumn HeaderText="Dec" ItemStyle-HorizontalAlign="center"  SortExpression="12" DataField="12" />
                                    <asp:BoundColumn HeaderText="Total" ItemStyle-HorizontalAlign="center"  SortExpression="Total" DataField="Total" />
                                        
                                </Columns>
                            </Anthem:DataGrid>
                        
                        </ul>
                    </ItemTemplate>          
                    </asp:DataList>
                        
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
