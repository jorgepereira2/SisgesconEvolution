<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMovimentoContaCadastro.aspx.cs" Inherits="frmMovimentoContaCadastro" %>
<%@ Import Namespace="Marinha.Business" %>
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
        Lançamento de Movimento Financeiro
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Tipo Movimento
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                     <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" >
                           Tipo Movimento:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlTipoOperacao" AutoCallBack="true"  >
                           </Anthem:DropDownList>
                           
                        </td>
                    </tr>
                                                     
                </table> 
            
                <Anthem:Panel runat="server" ID="pnSaida">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Saída
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                     <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="30%" >
                            Conta:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlContaSaida" AutoCallBack="true" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="valConta" runat="server" ControlToValidate="ddlContaSaida" InitialValue="0" ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>  
                     <tr runat="server" id="tr1">
                       <td ></td>
                        <td align="right" >
                           Saldo Conta:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblSaldoContaSaida" />
                        </td>
                    </tr>                                                      
                </table>   
                </Anthem:Panel>
            
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    <Anthem:Label runat="server" text="Entrada" ID="lblEntradaSaida" />
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                     <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="30%" >
                           Conta:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlConta" AutoCallBack="true" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlConta" InitialValue="0" ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr runat="server" id="tr4">
                       <td ></td>
                        <td align="right" >
                           Saldo Conta:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblSaldoConta" />
                        </td>
                    </tr>                        
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" >
                           Valor:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtValor" Columns="14" MaxLength="12" DecimalPlaces="2"
                                    CssClass="numerico" /> &nbsp;                              
                                <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValor"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>   
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" >
                           Data:
                        </td>
                        <td align="left">
                           <Anthem:DateTextBox runat="server" ID="txtData" /> &nbsp;                              
                                <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtData"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>     
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" >
                           Número Documento:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNumeroDocumento" 
                                MaxLength="30" Columns="20" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNumeroDocumento"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>                                
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" >
                           Observação:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtObservacao" Columns="30" TextMode="MultiLine" Rows="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNumeroDocumento"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>    
                </table>                     
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
                <Anthem:Button runat="server" ID="btnExcluir" TextDuringCallBack="Aguarde" Text="Excluir"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                <Anthem:Button runat="server" ID="btnVoltar" Text="Voltar"
                     CssClass="Button" CausesValidation="false" />
            </td>
            <td width="10px">
                &nbsp;
            </td>
        </tr>
    </table>
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    </div>    
    </form>    
</body>
</html>
