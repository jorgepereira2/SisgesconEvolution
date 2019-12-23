<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSenhaCadastro.aspx.cs" Inherits="frmSenhaCadastro" %>
<%@ Register TagPrefix="uc" TagName="BuscaServidor" Src="~/UserControls/BuscaServidor.ascx" %>
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
        Cadastro de Senhas de Acesso    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">               
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="10%" >&nbsp;</td>
                        <td align="right" width="20%">
                           Servidor:
                        </td>
                        <td align="left">
                           <uc:BuscaServidor runat="server" ID="ddlServidor" Required="true" AutoCallBack="true"   />	
                        </td>
                    </tr>
                     <tr>
                        <td width="10%" >&nbsp;</td>
                        <td align="right" width="20%">
                           Login:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtLogin" Columns="20" />  
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" >&nbsp;</td>
                        <td align="right" width="20%">
                           Senha:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtSenha" Columns="30" TextMode="Password" AutoComplete="off" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtSenha"
                                 ErrorMessage="Campo obrigatório" labelid="CampoObrigatorio" Display="dynamic"
                                />
                           
                        </td>
                    </tr>
                   <tr>
                        <td width="10%" >&nbsp;</td>
                        <td align="right" width="20%">
                           Confirme a Senha:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtConfirmacaoSenha" Columns="30" TextMode="Password" AutoComplete="off" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmacaoSenha"
                                 ErrorMessage="Campo obrigatório" labelid="CampoObrigatorio" Display="dynamic"
                                />                           
                           <Anthem:CompareValidator runat="server" ControlToValidate="txtConfirmacaoSenha" ControlToCompare="txtSenha"
                                  ErrorMessage="As senhas informadas são diferentes" labelid="SenhaDiferente" Display="dynamic" /> 
                        </td>
                    </tr> 
                     <tr>
                        <td width="10%" >&nbsp;</td>
                        <td align="right" width="20%">
                           
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkFlagPrimeiroAcesso" Text="O usuário deve trocar a senha no primeiro acesso?" Checked="true" />  
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
                     EnabledDuringCallBack="false" CssClass="Button" labelid="Salvar" />                
            </td>
            <td width="10px">
                &nbsp;
            </td>
        </tr>
    </table>
    </div>    
    <br /><br /><br /><br />
    </form>    
</body>
</html>
