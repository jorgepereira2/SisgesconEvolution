<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucJustificativa.ascx.cs" Inherits="ucJustificativa" %>
<Anthem:Window runat="server" ID="winJustificativa" style="left:120px;" Caption="Justificativa" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="220px" Modal="true">


<br />
<br />
Informe a justificativa/comentário:<br />
<Anthem:TextBox runat="server" ID="txtJustificativa" TextMode="MultiLine" Width="96%" Rows="3" />
<br />
<Anthem:RequiredFieldValidator runat="server" ValidationGroup="Recusa" ErrorMessage="Campo obrigatório"
    ControlToValidate="txtJustificativa" Display="Dynamic" />
<br />
<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Confirmar" ValidationGroup="Justificativa" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Width="160px" Text="Sair" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>