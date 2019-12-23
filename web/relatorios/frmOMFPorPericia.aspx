<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOMFPorPericia.aspx.cs" Inherits="frmOMFPorPericia" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Perícia Por OMF" />    
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <asp:DataList runat="server" ID="dlOMF" Width="100%">
        <ItemTemplate>
            <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                <tr>
                    <td align="Center" >
                        <b style="font-size:12px">Nota</b>       
                    </td>
                    <td align="Center">
                        <b style="font-size:12px">Empenho</b>
                    </td>
                    <td align="center">
                        <b style="font-size:12px">Data Entrega</b>
                    </td>
                    <td align="center">
                        <b style="font-size:12px">Fornecedor</b>
                    </td>
                    <td align="center">
                        <b style="font-size:12px">Status</b>
                    </td>
                </tr>
                <tr>
                    <td align="center" >
                        <%# ((NotaEntregaMaterialOMF)Container.DataItem).NumeroNota %>
                    </td>
                    <td align="center" >
                        <%# ((NotaEntregaMaterialOMF)Container.DataItem).NumeroEmpenho %>
                    </td>
                    <td align="center" >
                        <%# ((NotaEntregaMaterialOMF)Container.DataItem).DataEntrega.ToShortDateString() %>
                    </td>
                    <td align="left" >
                        <%# ((NotaEntregaMaterialOMF)Container.DataItem).Fornecedor.RazaoSocial %>
                    </td>
                    <td align="center" >
                        <%# ((NotaEntregaMaterialOMF)Container.DataItem).Status.Descricao %>
                    </td>
                </tr>
            </table>           
             
            </div>
            <ul>
                           
                <Anthem:DataGrid runat="server" ID="dgPericia" AutoGenerateColumns="false" Width="100%"
                    CssClass="datagrid" >
                    <HeaderStyle CssClass="dgHeader" /> 
                    <Columns>
                        <asp:BoundColumn HeaderText="Nome" DataField="Nome" ItemStyle-HorizontalAlign="left"/>
                        <asp:BoundColumn HeaderText="NIP" DataField="NIP" ItemStyle-HorizontalAlign="center"  />
                        <asp:BoundColumn HeaderText="Graduação" DataField="Graduacao" ItemStyle-HorizontalAlign="center" />
                         <asp:BoundColumn HeaderText="Tipo Notificação" DataField="TipoNotificacao" ItemStyle-HorizontalAlign="center" />
                         <asp:BoundColumn HeaderText="Observação" DataField="Observacao" ItemStyle-HorizontalAlign="left" />
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
