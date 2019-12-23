<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AguardandoIndicacaoRecurso.ascx.cs" Inherits="AguardandoIndicacaoRecurso" %>
<Anthem:Window runat="server" ID="winMensagem" style="left:120px;" Caption="Número Mensagem Cliente" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="480px"  Height="150px" Modal="true">

<br />
<br />
<asp:Label runat="server" ID="lblLabel" Text="Informe o número da mensagem" />
:<br />
<Anthem:TextBox runat="server" ID="txtNumeroMensagem" Width="96%" MaxLength="30" />
<br />
<Anthem:RequiredFieldValidator runat="server" ValidationGroup="AguardandoIndicacaoRecurso" ErrorMessage="Campo obrigatório"
    ControlToValidate="txtNumeroMensagem" Display="Dynamic" />

<br />

<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" ValidationGroup="AguardandoIndicacaoRecurso" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>