<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EtapaAguardandoPagamento.ascx.cs" Inherits="EtapaAguardandoPagamento" %>
<Anthem:Window runat="server" ID="winMensagem" style="left:120px;" Caption="" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="480px"  Height="160px" Modal="true">

<br />
<br />
<asp:Label runat="server" ID="lblLabel" Text="NL Pagamento" />
:<br />
<Anthem:TextBox runat="server" ID="txtNLPagamento" Width="96%" MaxLength="30" />
<br />
<Anthem:RequiredFieldValidator runat="server" ValidationGroup="MensagemCliente" ErrorMessage="Campo obrigatório"
    ControlToValidate="txtNLPagamento" Display="Dynamic" />
<br />


<asp:Label runat="server" ID="Label1" Text="Mensagem Indicação Recurso" />
:<br />
<Anthem:TextBox runat="server" ID="txtMensagemIndicacaoRecurso" Width="96%" MaxLength="200" />
<br />
<Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="MensagemCliente" ErrorMessage="Campo obrigatório"
    ControlToValidate="txtMensagemIndicacaoRecurso" Display="Dynamic" />
<br />

<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" ValidationGroup="MensagemCliente" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>