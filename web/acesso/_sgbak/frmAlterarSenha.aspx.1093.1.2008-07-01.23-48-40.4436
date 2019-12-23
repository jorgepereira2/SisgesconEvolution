<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAlterarSenha.aspx.cs" Inherits="frmAlterarSenha" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div align="right" style="width:90%">
    <br />
        <asp:label runat="server" labelid="AlteracaoSenha" CssClass="PageTitle" Text="Alteração de Senha">
            </asp:label>
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">               
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="10%" >&nbsp;</td>
                        <td align="right" width="20%">
                           <asp:Label ID="Label2" runat="server" labelid="Nome" Text="Nome" CssClass="label" />:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblNome" CssClass="legenda"/>                           
                        </td>
                    </tr>
                     <tr>
                        <td width="10%" >&nbsp;</td>
                        <td align="right" width="20%">
                           <asp:Label runat="server" labelid="Login" Text="Login" CssClass="label" />:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblLogin" CssClass="legenda"/>                           
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" >&nbsp;</td>
                        <td align="right" width="20%">
                           <asp:Label ID="Label4" runat="server" labelid="NovaSenha" Text="Nova Senha" CssClass="label" />:
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
                           <asp:Label runat="server" labelid="ConfirmeSenha" Text="Confirme a Senha" CssClass="label" />:
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
