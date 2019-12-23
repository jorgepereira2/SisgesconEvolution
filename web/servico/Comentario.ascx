<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Comentario.ascx.cs" Inherits="ucComentario" %>
<Anthem:Window runat="server" ID="winRecusa" style="left:120px;" Caption="Comentário" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="180px" Modal="true">

Comentário:<br />
<Anthem:TextBox runat="server" ID="txtComentario" TextMode="MultiLine" Width="96%" Rows="3" />
<br />

<br />
<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" CausesValidation="false" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>