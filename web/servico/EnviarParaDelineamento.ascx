<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EnviarParaDelineamento.ascx.cs" Inherits="EnviarParaDelineamento" %>
<Anthem:Window runat="server" ID="winRecusa" style="left:120px;" Caption="Enviar para Delineamento" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="200px" Modal="true">


<br />
<br />
Categoria:<br />
<Anthem:DropDownList runat="server" ID="ddlCategoriaServico" /><br />
<asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCategoriaServico" Display="Dynamic" InitialValue="0"
    ErrorMessage="Campo obrigatório" ValidationGroup="Categoria" />

<br /><br />
<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" EnabledDuringCallBack="false" ValidationGroup="Categoria"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>