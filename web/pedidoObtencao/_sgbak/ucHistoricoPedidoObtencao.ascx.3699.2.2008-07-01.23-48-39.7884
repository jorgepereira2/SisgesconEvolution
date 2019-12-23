<%@ Import namespace="Marinha.Business"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHistoricoPedidoObtencao.ascx.cs" Inherits="pedidoObtencao_ucHistoricoPedidoObtencao" %>
<Anthem:DataList runat="server" ID="dlHistorico">
    <ItemTemplate>
        <b><%# ((HistoricoPedidoObtencao)Container.DataItem).Servidor.Identificacao %></b> - 
        <%# ((HistoricoPedidoObtencao)Container.DataItem).Data.ToString("dd/MM/yyyy hh:mm") %><br />
        
        <b>Alteração:</b> <%# ((HistoricoPedidoObtencao)Container.DataItem).Descricao %>
        <br />
        <b>Comentário:</b>
        <%# ((HistoricoPedidoObtencao)Container.DataItem).Justificativa %>
        <hr />
    </ItemTemplate>                                
</Anthem:DataList>