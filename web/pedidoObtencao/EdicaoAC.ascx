<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EdicaoAC.ascx.cs" Inherits="EdicaoAC" %>
<Anthem:Window runat="server" ID="winEdicaoAC" style="left:120px;" Caption="AC" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="450px" Modal="true">

<br />
<br />
Número Empenho:<br />
<Anthem:TextBox runat="server" ID="txtNumeroEmpenho" Width="90%" MaxLength="20" />
<br /><br />
Código Gestão:<br />
<Anthem:TextBox runat="server" ID="txtCodigoGestao" Width="90%" MaxLength="10" />
<br /><br />
Lista:<br />
<Anthem:TextBox runat="server" ID="txtLista" Width="90%" MaxLength="40" />
<br /><br />
Número Lançamento:<br />
<Anthem:TextBox runat="server" ID="txtNumeroLancamento" Width="90%" MaxLength="10" />
<br />
<br />
Natureza Despesa:<br />
<Anthem:DropDownList runat="server" ID="ddlNaturezaDespesa" />
<br />
Observação:<br />
<Anthem:TextBox runat="server" ID="txtObservacao" Width="90%" Rows="3"  TextMode="MultiLine"  />

<br />
<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok"  EnabledDuringCallBack="false" CausesValidation="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>