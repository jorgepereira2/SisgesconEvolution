<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MotivoCancelamento.ascx.cs" Inherits="ucMotivoCancelamento" %>
<Anthem:Window runat="server" ID="winRecusa" style="left:120px;" Caption="Confirma Cancelamento" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="230px" Modal="true">

Esta ação cancelará este registro assim como todos os processos ligados a ele. Deseja prosseguir?
<br />
<br />
Motivo:<br />
<Anthem:DropDownList runat="server" ID="ddlMotivoCancelamento" /><br />
<asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMotivoCancelamento" Display="Dynamic" InitialValue="0"
    ErrorMessage="Campo obrigatório" ValidationGroup="MotivoCancelamento" />

<br /><br />
Comentário:<br />
<Anthem:TextBox runat="server" ID="txtComentario" TextMode="MultiLine" Rows="2" Columns="40" />
<br /><br />


<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" EnabledDuringCallBack="false" ValidationGroup="MotivoCancelamento"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>