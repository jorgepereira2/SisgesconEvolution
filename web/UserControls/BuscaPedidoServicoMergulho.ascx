<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BuscaPedidoServicoMergulho.ascx.cs" Inherits="BuscaPedidoServicoMergulho" %>
<%@ Import Namespace="Marinha.Business" %>
 <Anthem:AutoSuggestBox id="txtPedidoServico" Width="380px" TextBoxDisplayField="Descricao" SelectedMenuItemCSSClass="asbSelMenuItem" 
    DataKeyField="ID" runat="server" MaxSuggestChars="15" MenuItemCSSClass="asbMenuItem" ItemNotFoundMessage="Nenhum item encontrado" >
 <ItemTemplate>    
    <div align="left">
    <%# ((PedidoServicoUI)Container.DataItem).Descricao %>
    </div>
 </ItemTemplate>
</Anthem:AutoSuggestBox>
&nbsp;<Anthem:LinkButton runat="server" ID="btnBuscaAvancada" Text="Avançado" CausesValidation="false" />
