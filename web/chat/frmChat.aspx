<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmChat.aspx.cs" Inherits="frmChat" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="Anthem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Marinha - Chat</title>
    <script language="javascript" type="text/javascript">
        function OnClose()
        {
            Anthem_InvokePageMethod(
						'EliminaJanelaAtiva',
						[],
						''
					);
        }       
              
        function KeyDownHandler(e)
		{
		   if (!e) var e = window.event
		    
			var btn = document.getElementById('btnEnviar');
			
			// process only the Enter key
			if (e.keyCode == 13)
			{
				// cancel the default submit
				e.returnValue=false;
				e.cancel = true;
				// submit the form by programmatically clicking the specified button
				btn.click();
			}
		}
		
		function ScrollDown()
		{
		   document.getElementById('aAncora').scrollIntoView();
		}	
		
    </script>
    <style type="text/css">
        <!--
        body {
	        margin-left: 0px;
	        margin-top: 0px;
	        margin-right: 0px;
	        margin-bottom: 0px;
	        background-color: #19546B;
        }
        .style1 {
	        color: #FFFFFF;
	        font-weight: bold;
        }
        .div {
	        font-family: Verdana, Arial, Helvetica, sans-serif;
	        font-size: 11px;
	        color: #666666;
	        background-color: #F3F5FA;
	        border: 1px solid #A3B6DB;
	        letter-spacing: normal;
	        text-align: left;
	        vertical-align: top;
	        word-spacing: normal;
	        margin: 0px;
	        padding: 8px;
        }
        -->
    </style>
</head>
<body onunload="OnClose();">
    
    <form id="form1" runat="server">
    <table width="550"  height="474" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td height="50" align="center" valign="top" >
        <table width="100%"  border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td valign="top"><table width="100%" height="25"  border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="5"><img src="../images/blank.gif" width="5" height="25"></td>
                <td align="left" valign="middle" class="style1">                
                        Conversando com
                    <asp:Label runat="server" ID="lblServidor" />
                </td>
              </tr>
            </table></td>
            <td width="170" align="left" valign="top"><img src="../images/logo_chat_conversa.png" ></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td height="419" align="center" valign="top">
		    <!--- AQUI ENTRA A AREA ONDE A CONVERSA É EXIBIDA com as dimensões: width:512px; height:275px --->
		    <DIV align="left" style="overflow: auto; WIDTH: 512px; HEIGHT: 275px;" class="div" id="divHistorico">
                <Anthem:Label ID="lblHistorico" runat="server" Width="100%" />
	            <Anthem:Label ID="lblChat" runat="server" Width="100%" />
            <a id="aAncora"></a>
		    </DIV>	
		    <%--<div class="div" style="width:512px; height:275px;">blablabla</div><br>--%>
		    <table border="0" cellspacing="0" cellpadding="0">
 			    <tr>
    			    <td width="434">
					    <!--- AQUI ENTRA A AREA ONDE SE ESCREVE A MENSAGEM com as dimensões: width:234px; height:80px --->
					    <Anthem:TextBox CssClass="div" ID="txtMensagem" style="width:434px; height:80px;" TextMode="MultiLine" runat="server" />					
			      </td>
    			    <td width="80" align="center" valign="top">
    			    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td height="50" align="center" valign="middle">
						    <!--- AQUI ENTRA O BOTÃO PARA ENVIAR A MENSAGEM --->
						    <Anthem:Button ID="btnEnviar" Width="59" Height="37" runat="server" Text="Enviar" 
						        OnClick="btnEnviar_Click" EnabledDuringCallBack="false" TextDuringCallBack="Enviando"  />						
					    </td>
                      </tr>
                      <tr>
                        <td height="50" align="center" valign="middle">
						    <!--- AQUI ENTRA O BOTÃO PARA FECHAR A JANELA DE CHAT --->				
						    
					    </td>
                      </tr>
                    </table></td>
  			    </tr>
	      </table>		
	    </td>
      </tr>
    </table>
        <Anthem:Timer ID="timer" runat="server" Interval="6000">
        </Anthem:Timer>
  
    </form>
</body>
</html>
