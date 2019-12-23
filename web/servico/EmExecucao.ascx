<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmExecucao.ascx.cs" Inherits="EmExecucao" %>
<Anthem:Window runat="server" ID="winMensagem" style="left:120px;" Caption="Número Mensagem Cliente" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="480px"  Height="150px" Modal="true">

<br />
<br />
<asp:Label runat="server" ID="Label1" Text="Informe o número NL" />
:<br />
<Anthem:TextBox runat="server" ID="txtNL" Width="96%" MaxLength="30" />
<br />
<Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="EmExecucao" ErrorMessage="Campo obrigatório"
    ControlToValidate="txtNL" Display="Dynamic" />
<br />
<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" ValidationGroup="EmExecucao" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>