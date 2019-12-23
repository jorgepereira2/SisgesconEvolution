<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DefinicaoFinanceiraRelator.ascx.cs" Inherits="ucDefinicaoFinanceiraRelator" %>
<%@ Register Src="../pedidoObtencao/SaldoAC.ascx" TagName="SaldoAC" TagPrefix="uc" %>
<%@ Register Src="../pedidoObtencao/ItemsPO.ascx" TagName="ItemsPO" TagPrefix="uc" %>
<Anthem:Window runat="server" ID="winNaturezaDespesa" style="left:120px;" Caption="Definição Financeira" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="520px"  Height="330px" Modal="true">

<%--<br />
Comprador:<br />
<Anthem:DropDownList runat="server" ID="ddlComprador"   />--%>

<%--<br />
Natureza de Despesa:<br />
<Anthem:DropDownList runat="server" ID="ddlNaturezaDespesa" AutoCallBack="true" OnSelectedIndexChanged="ddlNaturezaDespesa_Changed" />

<br />--%>
<%--PTRES:<br />
<Anthem:DropDownList runat="server" ID="ddlPTRES"/>--%>
<br />
Fonte Recurso:<br />
<Anthem:DropDownList runat="server" ID="ddlFonteRecurso" />
<br />
Comentário:<br />
<Anthem:TextBox runat="server" ID="txtComentario" TextMode="MultiLine" Rows="3" Columns="40" />

<br />
<div align="left" style="vertical-align:text-bottom" class="PageTitle">
    Saldo
    <hr size="1" class="divisor" />
</div>
<uc:SaldoAC runat="server" id="ucSaldo" />

<br />
<div align="left" style="vertical-align:text-bottom" class="PageTitle">
    Itens
    <hr size="1" class="divisor" />
</div>
<uc:ItemsPO runat="server" id="ucItemsPO" />


<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" EnabledDuringCallBack="false" CausesValidation="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>

