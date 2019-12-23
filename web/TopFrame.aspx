<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TopFrame.aspx.cs" Inherits="TopFrame" %>
<%@ Register TagPrefix="Frame" TagName="Menu" src="usercontrols/menu.ascx"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Shared|Lojas</title>
    <LINK href="css/baseStyle.css" type="text/css" rel="stylesheet">
    <LINK href="css/menuStyle.css" type="text/css" rel="stylesheet">
</head>
<body style="margin: 0 0 0 0;" >
        <form id="Form1" method="post" runat="server">
        <div>
        <table width="100%" height="100%" cellspacing="0" cellpadding="0">
            <tr>
	            <td height="68"  background="/netsaphyr/images/topo_fundo.jpg" style=" background-position: right; background-repeat: no-repeat;">
		            <table cellspacing="0" cellpadding="0" border="0">
	                    <tr>
		                    <td height="67" rowspan="2" width="100%">
		                        <img src="images/logo_saphyr.jpg" border="0" style="margin-left:30px"></td>
		                    <td>&nbsp;</td>
	                    </tr>
	                    <tr>
		                    <td style="width:395;color:white" valign=bottom align="right" nowrap>
		                        Bem-vinda, <b>Aline Almeida</b> &nbsp;&nbsp;&nbsp;
		                        <font size="-2">
		                        [<a href="images/logoff.asp" class="sair">Sair</a>]</font>&nbsp;&nbsp;&nbsp;</td>
	                    </tr>
	                   
	                   
	                </table>
	            </td>
            </tr> 
            <tr>
                <td style="background-color:#18556C;" >
					<Frame:Menu runat="server" ID="frmMenu" />
				</td>					
            </tr>           
        </table> 
        </div>                
       
		</form>
	</body>	
</html>
