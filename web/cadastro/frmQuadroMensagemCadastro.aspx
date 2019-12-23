<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmQuadroMensagemCadastro.aspx.cs" Inherits="frmQuadroMensagemCadastro" %>
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
        Cadastro de Quadro Mensagens       
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Mensagem
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Título:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtTitulo" 
                                MaxLength="40" Columns="50" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtTitulo"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Mensagem:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtMensagem" TextMode="MultiLine" Rows="5" 
                                Columns="50" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtMensagem"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" class="label">
                           Assinatura:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtAssinatura" 
                                MaxLength="50" Columns="50" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtAssinatura"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Data Início:
                        </td>
                        <td align="left">
                           <Anthem:DateTextBox runat="server" ID="txtDataInicio" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtDataInicio"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>                    
                      <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Data Fim:
                        </td>
                        <td align="left">
                           <Anthem:DateTextBox runat="server" ID="txtDataFim" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDataFim"
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
                <Anthem:Button runat="server" ID="btnVoltar" Text="Voltar"
                     CssClass="Button" CausesValidation="false" />
            </td>
            <td width="10px">
                &nbsp;
            </td>
        </tr>
    </table>
    </div>    
    </form>    
</body>
</html>
