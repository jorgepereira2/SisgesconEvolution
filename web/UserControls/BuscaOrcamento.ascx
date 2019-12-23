<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BuscaOrcamento.ascx.cs" Inherits="BuscaOrcamento" %>
<%@ Import Namespace="Marinha.Business" %>
 <Anthem:AutoSuggestBox id="txtOrcamento" Width="340px" TextBoxDisplayField="Descricao" SelectedMenuItemCSSClass="asbSelMenuItem" 
    DataKeyField="ID" runat="server" MaxSuggestChars="15" MenuItemCSSClass="asbMenuItem" ItemNotFoundMessage="Nenhum item encontrado" >
 <ItemTemplate>    
    <div align="left">
    <%# ((DelineamentoOrcamentoUI)Container.DataItem).Descricao %>
    </div>
 </ItemTemplate>
</Anthem:AutoSuggestBox>

&nbsp;<Anthem:LinkButton runat="server" ID="btnBuscaAvancada" Text="Avançado" CausesValidation="false" />