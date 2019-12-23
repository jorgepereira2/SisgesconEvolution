<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMovimentoPorMaterial.aspx.cs" Inherits="frmMovimentoPorMaterial" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Movimento por Material" />    
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <asp:DataList runat="server" ID="dlMaterial" Width="100%">
        <ItemTemplate>
            <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                <tr>
                    <td align="left" style="width:50%">
                        <b style="font-size:12px"><%# ((DataRowView)Container.DataItem)["Material"].ToString() %></b>       
                    </td>
                    <td align="right">
                        Entrada:<b style="font-size:12px"><%# Convert.ToDecimal(((DataRowView)Container.DataItem)["QuantidadeEntrada"]).ToString("N2") %></b>
                    </td>
                    <td align="right">
                        Saída:<b style="font-size:12px"><%# Convert.ToDecimal(((DataRowView)Container.DataItem)["QuantidadeSaida"]).ToString("N2") %></b>
                    </td>
                    <td align="right">
                        Saldo:<b style="font-size:12px"><%# Convert.ToDecimal(((DataRowView)Container.DataItem)["Saldo"]).ToString("N2") %></b>
                    </td>
                </tr>
            </table>           
             
            </div>
            <ul>
                           
                <Anthem:DataGrid runat="server" ID="dgMovimento" AutoGenerateColumns="false" Width="100%"
                    CssClass="datagrid" OnItemDataBound="dgMovimento_ItemDataBound">
                    <HeaderStyle CssClass="dgHeader" /> 
                    <Columns>
                        <asp:BoundColumn HeaderText="Data" DataField="Data" ItemStyle-HorizontalAlign="center"
                            DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundColumn HeaderText="Quantidade" DataField="Quantidade" ItemStyle-HorizontalAlign="center" DataFormatString="{0:N2}" />
                        <asp:BoundColumn HeaderText="Origem" DataField="OrigemMaterial" ItemStyle-HorizontalAlign="center" />
                         <asp:BoundColumn HeaderText="Tipo" DataField="TipoMovimento" ItemStyle-HorizontalAlign="center" />
                        <asp:TemplateColumn HeaderText="Código" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <Anthem:LinkButton runat="server" id="lnkCodigo" Text='<%# ((DataRowView)Container.DataItem)["Codigo"].ToString() %>' />
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
