<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegistrarDevolucaoMeio.ascx.cs" Inherits="RegistrarDevolucaoMeio" %>
<Anthem:Window runat="server" ID="winRecusa" style="left:120px;" Caption="Devolução Meio" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="200px" Modal="true">


<br />
<br />
Comentários:<br />
<Anthem:TextBox runat="server" ID="txtComentario" TextMode="MultiLine" Width="96%" Rows="3" />

<br /><br />
<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>