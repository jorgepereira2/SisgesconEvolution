<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConfirmarAprovacao.ascx.cs" Inherits="ConfirmarAprovacao" %>
<Anthem:Window runat="server" ID="winRecusa" style="left:120px;" Caption="Aprovação Pedido de Serviço" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="400px"  Height="80px" Modal="true">
<b>
Confirma a operação?
</b>
<br />

<br />
<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" CausesValidation="false" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>