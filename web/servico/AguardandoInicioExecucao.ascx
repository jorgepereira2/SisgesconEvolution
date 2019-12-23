<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AguardandoInicioExecucao.ascx.cs" Inherits="ucAguardandoInicioExecucao" %>
<Anthem:Window runat="server" ID="winRecusa" style="left:120px;" Caption="Comentário" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="385px"  Height="220px" Modal="true">

Data Previsão Entrega:<br />
<Anthem:DateTextBox runat="server" ID="txtDataPrevisaoEntrega"  />
<br />
<asp:RequiredFieldValidator runat="server" ErrorMessage="Data obrigatória" Display="Dynamic" ControlToValidate="txtDataPrevisaoEntrega" ValidationGroup="DataEntrega"></asp:RequiredFieldValidator>
<br />
<br />    
Comentário:<br />
<Anthem:TextBox runat="server" ID="txtComentario" TextMode="MultiLine" Rows="3" Width="90%" />
<br />

<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ir para próxima etapa" EnabledDuringCallBack="false" ValidationGroup="DataEntrega" Width="220"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>