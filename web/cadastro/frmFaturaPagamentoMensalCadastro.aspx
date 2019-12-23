<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFaturaPagamentoMensalCadastro.aspx.cs" Inherits="frmFaturaPagamentoMensalCadastro" %>
<%@ Register TagPrefix="uc" TagName="BuscaFornecedor" Src="~/UserControls/BuscaFornecedor.ascx" %>
<%@ Register TagPrefix="uc" TagName="BuscaServidor" Src="~/UserControls/BuscaServidor.ascx" %>

<%@ Register Src="~/UserControls/BuscaServicoMaterial.ascx" TagPrefix="uc" TagName="BuscaMaterial" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Cadastro de Fatura de Pagamento Mensal
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Fatura de Pagamento
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" class="label">
                           Tipo:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlTipoFatura" AutoCallBack="true" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTipoFatura"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />
                        </td>
                    </tr>
                     <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Número Fatura:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNumeroFatura" Columns="30" MaxLength="40"  />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNumeroFatura"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" class="label">
                           Mês Referência:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlMes" AutoCallBack="true" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMes"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Data Emissão:
                        </td>
                        <td align="left">
                           <Anthem:DateTextBox runat="server" ID="txtDataEmissao" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDataEmissao" 
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                     <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Data Vencimento:
                        </td>
                        <td align="left">
                           <Anthem:DateTextBox runat="server" ID="txtDataVencimento" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDataVencimento" 
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Valor Total:
                        </td>
                        <td align="left">
                            <Anthem:NumericTextBox runat="server" ID="txtValorTotal" Columns="14" MaxLength="12" DecimalPlaces="2"
                                    CssClass="numerico" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtValorTotal" 
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr runat="server" id="trFornecedor">
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Fornecedor:
                        </td>
                        <td align="left">
                            <uc:BuscaFornecedor runat="server" ID="ucFornecedor" />                                                      
                        </td>
                    </tr>
                     <tr runat="server" id="trServidor">
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Servidor:
                        </td>
                        <td align="left">
                            <uc:BuscaServidor runat="server" ID="ucServidor" />                                                      
                        </td>
                    </tr>
                   
                   
                   
                </table>              
                <br />       
            </td>
        </tr>																			
    </table>
    <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td width="40%" align="left">
            
            </td>
            <td align="right">
                <Anthem:Button runat="server" ID="btnSalvar" TextDuringCallBack="Aguarde" Text="Salvar"
                     EnabledDuringCallBack="false" CssClass="Button" />
                <Anthem:Button runat="server" ID="btnNovo" TextDuringCallBack="Aguarde" Text="Novo"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />     
                <Anthem:Button runat="server" ID="btnVoltar" Text="Voltar"
                     CssClass="Button" CausesValidation="false" />
            </td>
            <td width="10px">
                &nbsp;
            </td>
        </tr>
    </table>
    <br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br />
    </div>    
    
    </form>    
</body>
</html>
