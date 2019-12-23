<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BuscaFornecedor.ascx.cs" Inherits="BuscaFornecedor" %>
<%@ Import Namespace="Marinha.Business" %>
 <Anthem:AutoSuggestBox id="txtFornecedor" Width="340px" TextBoxDisplayField="RazaoSocial" SelectedMenuItemCSSClass="asbSelMenuItem" 
    DataKeyField="ID" runat="server" MaxSuggestChars="15" MenuItemCSSClass="asbMenuItem" ItemNotFoundMessage="Nenhum item encontrado" UseIFrame="false"  >
 <ItemTemplate>    
    <div align="left">
    <%# ((FornecedorUI)Container.DataItem).RazaoSocial %>
    </div>
 </ItemTemplate>
</Anthem:AutoSuggestBox>
&nbsp;<Anthem:LinkButton runat="server" ID="btnNovo" Text="Novo" CausesValidation="false"/>
&nbsp;<Anthem:LinkButton runat="server" ID="btnBuscaAvancada" Text="Avançado" CausesValidation="false"/>

