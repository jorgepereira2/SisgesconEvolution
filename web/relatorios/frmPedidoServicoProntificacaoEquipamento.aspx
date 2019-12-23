<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoProntificacaoEquipamento.aspx.cs" Inherits="frmPedidoServicoProntificacaoEquipamento" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Relatório Prontificação por Tipo de Equipamento" />    
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
                            <asp:DataList runat="server" ID="dlTipoEquipamento" Width="100%" OnItemDataBound="dlTipoEquipamento_ItemDataBound">
                                <ItemTemplate>
                                    <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                                        <tr>
                                            <td align="left" style="width:50%">
                                                <b style="font-size:12px">
                                                    <%# ((DataRowView)Container.DataItem)["SubTipoEquipamento"].ToString() %> 
                                                </b>       
                                            </td>
                                            <td align="right">
                                                Total:<b style="font-size:12px"><%# ((DataRowView)Container.DataItem)["Quantidade"].ToString() %></b>
                                            </td>
                                        </tr>
                                    </table>           
                                     
                                    </div>
                
                                    <ul>
                                                   
                                        <Anthem:DataGrid runat="server" ID="dgEquipamento" AutoGenerateColumns="False" Width="100%"
                                            CssClass="datagrid" >
                                            <HeaderStyle CssClass="dgHeader" /> 
                                            <ItemStyle HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Equipamento" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                         <%# ((DataRowView)Container.DataItem)["Equipamento"]%>    
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                         <%# ((DataRowView)Container.DataItem)["Quantidade"]%>    
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </Anthem:DataGrid>
                                            
                                        <br />
                                    </ul>
                                    
                                    </ItemTemplate>
                                </asp:DataList>
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
