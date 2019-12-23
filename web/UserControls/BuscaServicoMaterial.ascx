<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BuscaServicoMaterial.ascx.cs" Inherits="BuscaServicoMaterial" %>
<%@ Import Namespace="Marinha.Business" %>
 <Anthem:AutoSuggestBox id="txtServicoMaterial" Width="340px" TextBoxDisplayField="Descricao" SelectedMenuItemCSSClass="asbSelMenuItem" 
    DataKeyField="ID" runat="server" MaxSuggestChars="15" MenuItemCSSClass="asbMenuItem" ItemNotFoundMessage="Nenhum item encontrado" >
 <ItemTemplate>    
    <div align="left">
    <%# ((ServicoMaterialUI)Container.DataItem).Descricao %>
    (<%# ((ServicoMaterialUI)Container.DataItem).Unidade %>)
    </div>
 </ItemTemplate>
</Anthem:AutoSuggestBox>
&nbsp;
<Anthem:LinkButton runat="server" ID="lnkBusca" Text="Avançado" />

