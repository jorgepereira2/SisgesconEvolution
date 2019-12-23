<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NaturezaDespesa.ascx.cs" Inherits="ucNaturezaDespesa" %>
<Anthem:Window runat="server" ID="winNaturezaDespesa" style="left:120px;" Caption="Natureza de Despesa" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="520px"  Height="330px" Modal="true">

<br />


Natureza de Despesa:<br />
<Anthem:DropDownList runat="server" ID="ddlNaturezaDespesa" AutoCallBack="true" OnSelectedIndexChanged="AtualizaSaldo"/>
<br />
PTRES:<br />
<Anthem:DropDownList runat="server" ID="ddlPTRES" AutoCallBack="true" OnSelectedIndexChanged="AtualizaSaldo" />
<br />
Comentário:<br />
<Anthem:TextBox runat="server" ID="txtComentario" TextMode="MultiLine" Rows="3" Columns="40" />

<br />
<br />

<div align="left" style="vertical-align:text-bottom" class="PageTitle">
    Saldo
    <hr size="1" class="divisor" />
</div>

Saldo: <Anthem:Label runat="server" ID="lblSaldo" />
<br />
Total Comprometido: <Anthem:Label runat="server" ID="lblComprometido" />
<br />
Custo Simulado: <Anthem:Label runat="server" ID="lblCusto" />
<br />
Saldo Total: <Anthem:Label runat="server" ID="lblSaldoTotal" />
<br />

<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" EnabledDuringCallBack="false" CausesValidation="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>

