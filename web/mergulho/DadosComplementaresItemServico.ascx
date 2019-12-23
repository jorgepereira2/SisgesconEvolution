<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DadosComplementaresItemServico.ascx.cs" Inherits="DadosComplementaresItemServicoMergulho" %>
<%@ Register Src="~/UserControls/BuscaFornecedor.ascx" TagName="BuscaFornecedor" TagPrefix="uc" %>
<Anthem:Window runat="server" ID="winDados" style="left:120px;" Caption="Dados Complementares" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="220px" Modal="true">

<br />

<br />
<br />
<%--Fornecedor:<br />
<uc:BuscaFornecedor runat="server" ID="ucFornecedor" Enabled="false" />
<br />
<br />--%>
Observação:<br />
<Anthem:TextBox runat="server" ID="txtObservacao" TextMode="MultiLine" Rows="3" Columns="50" /> 
<br />
<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" ValidationGroup="Recusa" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>