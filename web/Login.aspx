<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<HEAD runat="server">
	<title>SIC</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="css/basicStyle.css" type="text/css" rel="stylesheet">
</HEAD>
<body topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" style="background:url(images/fundo_login.jpg)" >
    <form id="form1" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
	        <tr>
		        <td align="center" >
			        &nbsp;
		        </td>		       
	        </tr>				
        </table>
        <br><br><br><br><br><br>
        <table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
            <tr>
                <td align="center" valign="middle">
                
                    <asp:ValidationSummary runat="server" ID="valMensagem" CssClass="msgErro" ShowMessageBox="false" DisplayMode="BulletList" />
                    <asp:Label runat="server" ID="lblMensagem" CssClass="msgErro" />
                    <table cellpadding="3"  style="background: url(images/logo_login.png); border: solid 1px black; color:#0C4765;font-family:Verdana;font-weight:bold;height:300px;width:529px;">
			            <tr>
			                <td colspan="3" style="height:100px">
			                
			                </td>
			            </tr>
			            <tr>
			                <td align="right" style="width:70px; height:30px">
			                    Usuário:
			                </td>
			                <td style="width:210px;">
			                    <asp:TextBox runat="server" ID="txtLogin" style="width:180px;border: solid 1px black;letter-spacing:1px;" ></asp:TextBox>
			                   &nbsp;
			                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLogin" Display="None" ErrorMessage="Usuário obrigatório" />
			                </td>
			                 <td>
				                &nbsp;
				            </td>
			            </tr>
			            <tr>
				            <td align="right" style="height:30px">
				                Senha:
				            </td>
				            <td>
                                <asp:TextBox runat="server" ID="txtSenha" style="width:180px;border: solid 1px black; letter-spacing:1px;" TextMode="password" ></asp:TextBox>
			                    &nbsp;
			                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSenha" Display="None" ErrorMessage="Senha obrigatória" />
                            </td>
                            <td>
				            
				            </td>				                
			            </tr>			           
			            <tr>
				            <td align="center" colspan="2" style="height:30px">
				                <br />
				                <asp:Button runat="server" Text="Fazer Logon" id="btnEntrar" Font-Bold="true" Font-Size="12px" 
				                style="color:#0C4765;background-color:#FFFBFF;border-color:black;border-width:2px;border-style:Solid;font-family:Verdana;width:100px;" />
				            </td>
				            <td>
				            
				            </td>
			            </tr>
			            <tr>
				            <td colspan="3" align="center">
				            
				            </td>
			            </tr>
		            </table>                    
                    
                
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
