<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecebedorEmpenhoPO.ascx.cs" Inherits="RecebedorEmpenhoPO" %>
<%@ Register Src="~/UserControls/BuscaFornecedor.ascx" TagName="BuscaFornecedor" TagPrefix="uc" %>
<Anthem:Window runat="server" ID="winDados" style="left:120px;" Caption="Dados Complementares" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="220px" Modal="true">

<br />

<br />
<br />
Nome Recebedor:<br />
<Anthem:TextBox runat="server" ID="txtNomeRecebedorEmpenho" Columns="50" MaxLength="50" ValidationGroup="Recebedor" /> 
<br />
<br />
Telefone Recebedor:<br />
<Anthem:TextBox runat="server" ID="txtTelefoneRecebedorEmpenho" Columns="50" MaxLength="50" ValidationGroup="Recebedor" /> 
<br />
<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" ValidationGroup="Recebedor" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>