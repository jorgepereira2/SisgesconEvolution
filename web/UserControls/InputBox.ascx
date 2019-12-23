<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InputBox.ascx.cs" Inherits="InputBox" %>
<Anthem:Window runat="server" ID="winInputBox" style="left:120px;" Caption="" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="160px" Modal="true">


<br />
<br />
<Anthem:Label runat="server" ID="lblTexto" /> <br />
<Anthem:TextBox runat="server" ID="txtTexto" Width="96%"  TextMode="MultiLine" Rows="2" />
<br />
<Anthem:RequiredFieldValidator runat="server" ValidationGroup="InputBox" ErrorMessage="Campo obrigatório" 
    ControlToValidate="txtTexto" Display="Dynamic" ID="valTexto" />
<br />
<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" ValidationGroup="InputBox" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>