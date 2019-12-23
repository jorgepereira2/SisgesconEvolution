<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InsereFaturamento.ascx.cs" Inherits="InsereFaturamento" %>
<Anthem:Window runat="server" ID="winDados" style="left:120px;" Caption="Faturamento" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="390px" Modal="true">



<br />
Valor Orçamento:
<Anthem:Label runat="server" ID="lblValorOrcamento" Font-Bold="true" />
<br />
<br />
Valor Faturado:
<Anthem:Label runat="server" ID="lblValorFaturado" Font-Bold="true" />
<br />
<br />
Data:<br />
<Anthem:DateTextBox runat="server" ID="txtData" />
<br />
<Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtData" ErrorMessage="Campo obrigatório" Display="Dynamic" ValidationGroup="Faturamento" />
<br />
Valor:<br />
<Anthem:NumericTextBox runat="server" ID="txtValor" DecimalPlaces="2" CssClass="numerico" /> 
<br />
<Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValor" ErrorMessage="Campo obrigatório" Display="Dynamic" 
    ValidationGroup="Faturamento"/>
<br />
Validade:<br />
<Anthem:TextBox runat="server" ID="txtValidade" Columns="35" /> 
<br />
<br />
Numero da NL:<br />
<Anthem:TextBox runat="server" ID="txtNumeroNL" Columns="35" /> 
<br />
<br />
Garantia:<br />
<Anthem:TextBox runat="server" ID="txtGarantia" Columns="35" /> 
<br />
<br />
Observação:<br />
<Anthem:TextBox runat="server" ID="txtObservacao" TextMode="MultiLine" Rows="2" Columns="35" /> 
<br />
<br />

<div style="text-align:right">
<Anthem:Button runat="server" ID="btnFaturar" CssClass="Button" Text="Faturar" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" ValidationGroup="Faturamento"/>&nbsp;
<Anthem:Button runat="server" ID="btnEnviar" CssClass="Button" Text="Enviar" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" CausesValidation="false" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>