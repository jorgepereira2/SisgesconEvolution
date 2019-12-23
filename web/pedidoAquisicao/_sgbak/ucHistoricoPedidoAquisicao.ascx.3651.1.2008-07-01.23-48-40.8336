<%@ Import namespace="Marinha.Business"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHistoricoPedidoAquisicao.ascx.cs" Inherits="pedidoAquisicao_ucHistoricoPedidoAquisicao" %>
<Anthem:DataList runat="server" ID="dlHistorico">
    <ItemTemplate>
        <b><%# ((HistoricoPedidoAquisicao)Container.DataItem).Servidor.Identificacao %></b> - 
        <%# ((HistoricoPedidoAquisicao)Container.DataItem).Data.ToString("dd/MM/yyyy hh:mm") %><br />
        
        <b>Alteração:</b> <%# 
                    ((HistoricoPedidoAquisicao)Container.DataItem).StatusAnterior == null ? "" : 
                    ((HistoricoPedidoAquisicao)Container.DataItem).StatusAnterior.Descricao + " -> "%>  
                    <%# ((HistoricoPedidoAquisicao)Container.DataItem).StatusPosterior %>
        <br />
        <b>Comentário:</b>
        <%# ((HistoricoPedidoAquisicao)Container.DataItem).Justificativa %>
        <hr />
    </ItemTemplate>                                
</Anthem:DataList>