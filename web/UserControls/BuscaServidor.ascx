<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BuscaServidor.ascx.cs" Inherits="BuscaServidor" %>
<%@ Import Namespace="Marinha.Business" %>
 <Anthem:AutoSuggestBox id="txtServidor" Width="340px" TextBoxDisplayField="Nome" SelectedMenuItemCSSClass="asbSelMenuItem" 
    DataKeyField="ID" runat="server" MaxSuggestChars="15" MenuItemCSSClass="asbMenuItem" ItemNotFoundMessage="Nenhum item encontrado" >
 <ItemTemplate>    
    <div align="left">
    <%# ((ServidorUI)Container.DataItem).Nome %>
    
    </div>
 </ItemTemplate>
</Anthem:AutoSuggestBox>
&nbsp;
<%--<Anthem:LinkButton runat="server" ID="lnkBusca" Text="Avançado" />--%>

