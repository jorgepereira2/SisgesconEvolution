<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoEtapa.aspx.cs" Inherits="frmPedidoServicoEtapa" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Pedidos de Serviço" />    
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <asp:DataList runat="server" ID="dlPedido" Width="100%">
        <ItemTemplate>
            <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                <tr>
                    <td align="left" style="width:85%">
                        <b style="font-size:12px"><%# ((IPedido)Container.DataItem).CodigoComAno %></b>       
                        &nbsp;-&nbsp;
                        <%# ((DelineamentoOrcamento)Container.DataItem).DescricaoEquipamentos %> 
                    </td>                    
                    <td align="right">
                        Data:<b><%# ((DelineamentoOrcamento)Container.DataItem).DataEmissao.ToShortDateString()%></b>
                    </td>                    
                </tr>
                 <tr>
                    <td align="left" colspan="2">
                        PS Cliente:<b>
                        <%# ((IPedido)Container.DataItem).CodigoPedidoCliente %></b>       
                        &nbsp;-&nbsp;
                        <%# ((DelineamentoOrcamento)Container.DataItem).Cliente.DescricaoCompleta %> 
                    </td>                                        
                </tr>
            </table>           
             
            </div>
            <ul>
                           
                <Anthem:DataGrid runat="server" ID="dgEtapa" AutoGenerateColumns="false" Width="100%"
                    CssClass="datagrid" >
                    <HeaderStyle CssClass="dgHeader" /> 
                    <Columns>
                        <asp:BoundColumn HeaderText="Data" DataField="Data" ItemStyle-HorizontalAlign="center"
                            DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundColumn HeaderText="Tipo" DataField="Tipo" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundColumn HeaderText="Descrição" DataField="Texto" ItemStyle-HorizontalAlign="left" />
                        <asp:TemplateColumn HeaderText="Dias" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# ((Etapa)Container.DataItem).DiasDemora == -1 ? "N/A" : ((Etapa)Container.DataItem).DiasDemora.ToString() %>
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
