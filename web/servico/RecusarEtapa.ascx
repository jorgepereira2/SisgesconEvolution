<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecusarEtapa.ascx.cs" Inherits="servico_ucRecusarEtapa" %>
<Anthem:Window runat="server" ID="winRecusa" style="left:120px;" Caption="Recusar Etapa" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="220px" Modal="true">

<br />
Ao recusar esta etapa o PS retornará à situação anterior.
<br />
<br />
Informe a justificativa:<br />
<Anthem:TextBox runat="server" ID="txtJustificativa" TextMode="MultiLine" Width="96%" Rows="3" />
<br />
<Anthem:RequiredFieldValidator runat="server" ValidationGroup="Recusa" ErrorMessage="Campo obrigatório"
    ControlToValidate="txtJustificativa" Display="Dynamic" />
<br />
<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" ValidationGroup="Recusa" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>