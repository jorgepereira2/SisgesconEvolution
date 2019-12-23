<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DescartarItem.ascx.cs" Inherits="DescartarItem" %>
<Anthem:Window runat="server" ID="winRecusa" style="left:120px;" Caption="Cancelar Item" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="220px" Modal="true">


<br />
<br />
Informe a justificativa:<br />
<Anthem:TextBox runat="server" ID="txtJustificativa" TextMode="MultiLine" Width="96%" Rows="3" />
<br />
<Anthem:RequiredFieldValidator runat="server" ValidationGroup="Descartar" ErrorMessage="Campo obrigatório"
    ControlToValidate="txtJustificativa" Display="Dynamic" />
<br />
<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Confirmar" ValidationGroup="Descartar" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Width="160px" Text="Sair sem descartar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>