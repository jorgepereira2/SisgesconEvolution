<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BuscaEquipamento.ascx.cs" Inherits="BuscaEquipamento" %>
<%@ Import Namespace="Marinha.Business" %>
 <Anthem:AutoSuggestBox id="txtEquipamento" Width="340px" TextBoxDisplayField="Descricao" SelectedMenuItemCSSClass="asbSelMenuItem" 
    DataKeyField="ID" runat="server" MaxSuggestChars="15" MenuItemCSSClass="asbMenuItem" ItemNotFoundMessage="Nenhum item encontrado" >
 <ItemTemplate>    
    <div align="left">
    <%# ((EquipamentoUI)Container.DataItem).Descricao %>
    </div>
 </ItemTemplate>
</Anthem:AutoSuggestBox>
&nbsp;<Anthem:LinkButton runat="server" ID="btnNovo" Text="Novo" />
&nbsp;<Anthem:LinkButton runat="server" ID="btnBuscaAvancada" Text="Avançado"  />
