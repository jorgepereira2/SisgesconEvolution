<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BuscaCliente.ascx.cs" Inherits="BuscaCliente" %>
<%@ Import Namespace="Marinha.Business" %>
 <Anthem:AutoSuggestBox id="txtCliente" Width="340px" TextBoxDisplayField="DescricaoCompleta" SelectedMenuItemCSSClass="asbSelMenuItem" 
    DataKeyField="ID" runat="server" MaxSuggestChars="15" MenuItemCSSClass="asbMenuItem" ItemNotFoundMessage="Nenhum item encontrado" UseIFrame="false"  >
 <ItemTemplate>    
    <div align="left">
    <%# ((ClienteUI)Container.DataItem).DescricaoCompleta %>
    </div>
 </ItemTemplate>
</Anthem:AutoSuggestBox>
&nbsp;<Anthem:LinkButton runat="server" ID="btnNovo" Text="Novo" CausesValidation="false" />
&nbsp;<Anthem:LinkButton runat="server" ID="btnBuscaAvancada" Text="Avançado" CausesValidation="false"/>

