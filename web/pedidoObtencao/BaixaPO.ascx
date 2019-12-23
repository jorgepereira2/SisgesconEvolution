<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BaixaPO.ascx.cs" Inherits="BaixaPO" %>
<Anthem:Window runat="server" ID="winBaixaPO" style="left:100px;" Caption="Baixa de PO" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="490px"  Height="420px" Modal="true">



<table border="0" cellpadding="2" cellspacing="4" width="100%" >
    <tr>
        <td width="5%" ></td>
        <td align="right" width="20%" >
           PO:
        </td>
        <td align="left">
           <Anthem:LinkButton runat="server" ID="lnkNumeroPO" />           
        </td>
    </tr> 
    <tr>
        <td  ></td>
        <td align="right"  >
           Fornecedor:
        </td>
        <td align="left">
           <Anthem:Label runat="server" ID="lblFornecedor" />           
        </td>
    </tr> 
    <tr>
        <td  ></td>
        <td align="right"  >
           CNPJ:
        </td>
        <td align="left">
           <Anthem:Label runat="server" ID="lblCNPJ" />           
        </td>
    </tr> 
   
    <tr>
        <td  ></td>
        <td align="right"  >
           Valor Total:
        </td>
        <td align="left">
           <Anthem:Label runat="server" ID="lblValorTotal" />           
        </td>
    </tr> 
    <tr>
        <td  ></td>
        <td align="right"  >
           Valor Restante:
        </td>
        <td align="left">
           <Anthem:Label runat="server" ID="lblValorRestante" />           
        </td>
    </tr> 
    <%-- <tr>
        <td  class="msgErro" >*</td>
        <td align="right"  >
           Data:
        </td>
        <td align="left">
           <Anthem:DateTextBox runat="server" ID="txtData" />
           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Baixa" ErrorMessage="Campo obrigatório"
                ControlToValidate="txtData" Display="Dynamic" />
        </td>
    </tr> --%>
      <tr>
        <td class="msgErro" >*</td>
        <td align="right"  >
           Empenho:
        </td>
        <td align="left">
           <Anthem:DropDownList runat="server" ID="ddlEmpenho" />
         
        </td>
        </tr>
    <tr>
        <td class="msgErro" >*</td>
        <td align="right" >
           Valor:
        </td>
        <td align="left">
           <Anthem:NumericTextBox runat="server" ID="txtValor" DecimalPlaces="2" Columns="16"  />
           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Baixa" ErrorMessage="Campo obrigatório"
                ControlToValidate="txtValor" Display="Dynamic" />
        </td>
    </tr> 
  <%--  <tr>
        <td class="msgErro" >*</td>
        <td align="right" >
           Valor Imposto:
        </td>
        <td align="left">
           <Anthem:NumericTextBox runat="server" ID="txtValorImposto" DecimalPlaces="2" Columns="16"  />
           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Baixa" ErrorMessage="Campo obrigatório"
                ControlToValidate="txtValorImposto" Display="Dynamic" Text="0,00" />
        </td>
    </tr> 
    <tr>
        <td class="msgErro" >*</td>
        <td align="right" >
           Valor Desconto:
        </td>
        <td align="left">
           <Anthem:NumericTextBox runat="server" ID="txtValorDesconto" DecimalPlaces="2" Columns="16"  />
           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Baixa" ErrorMessage="Campo obrigatório"
                ControlToValidate="txtValorDesconto" Display="Dynamic" Text="0,00" />
        </td>
    </tr> --%>
     <tr>
        <td class="msgErro" >*</td>
        <td align="right"  >
           NF:
        </td>
        <td align="left">
           <Anthem:TextBox runat="server" ID="txtNotaFiscal" Columns="16"  />
           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Baixa" ErrorMessage="Campo obrigatório"
                ControlToValidate="txtNotaFiscal" Display="Dynamic" />
        </td>
        </tr>
        <%--<tr>
        <td class="msgErro" >*</td>
        <td align="right"  >
           Ordem Bancária:
        </td>
        <td align="left">
           <Anthem:TextBox runat="server" ID="txtNumeroOrdemBancaria" Columns="16" MaxLength="30"  />
           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Baixa" ErrorMessage="Campo obrigatório"
                ControlToValidate="txtNumeroOrdemBancaria" Display="Dynamic" />
        </td>
    </tr> --%>
     <%--<tr>
        <td class="msgErro" ></td>
        <td align="right"  >
           Observação:
        </td>
        <td align="left">
           <Anthem:TextBox runat="server" ID="txtObservacao" Rows="2" TextMode="MultiLine"   />
        </td>
        <tr>--%>
</table>

<br />
<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" ValidationGroup="Baixa" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>