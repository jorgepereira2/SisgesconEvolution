<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DesignarCotador.ascx.cs" Inherits="DesignarCotador" %>
<Anthem:Window runat="server" ID="winRecusa" style="left:120px;" Caption="Designar Comprador" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="200px" Modal="true">


<br />
<br />
Cotador:<br />
<Anthem:DropDownList runat="server" ID="ddlServidor" /><br />
<asp:RequiredFieldValidator runat="server" ControlToValidate="ddlServidor" Display="Dynamic" InitialValue="0"
    ErrorMessage="Campo obrigatório" ValidationGroup="ServidorCotador" />

<br /><br />
<Anthem:TextBox runat="server" ID="txtCometario" TextMode="MultiLine" Rows="3" Columns="40" />
<br /><br />

<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" EnabledDuringCallBack="false" ValidationGroup="ServidorCotador"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>