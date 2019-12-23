<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmListaUsuarios.aspx.cs" Inherits="frmListaUsuarios" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="Anthem" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">    
    <title>Marinha - Chat</title>    
    <link type="text/css" rel="stylesheet" href="../css/menuStyle.css" />
    <script language="javascript">
        function OpenChat(id_funcionario)
        {
            window.open('frmChat.aspx?id_servidor=' + id_funcionario, id_funcionario, 
             'toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=0,width=550,height=474,top=20,left=50');
        }
        
        function Mostrar(control, imagem, id_grupo)
	    {
		    var div = document.getElementById(control);
		    var img = document.getElementById(imagem);
		    var aberto;
		    
		    if(div.style.display=='none')
		    {
			    div.style.display = '';
			    img.src = "../images/minus.gif";			    
			    aberto = true;
		    }
		    else
		    {
			    div.style.display = 'none';
			    img.src = "../images/plus.gif";			    
			    aberto = false;
		    }
		    
		        Anthem_InvokePageMethod(
		            'AbreFechaGrupo',
		            [aberto, id_grupo],
		            ''
		        );
	    }	
	    
	    function Agrupar(id)
	    {
	        var item = Menu1.Items(1).Items(id);	        
	                
	        Anthem_InvokePageMethod(
		            'Agrupar',
		            [item.Value],
		            function (result){
		            }
		        );
	    }
    </script>
    <style type="text/css">
        <!--        
        body {
            
	        margin-left: 0px;
	        margin-top: 0px;
	        margin-right: 0px;
	        margin-bottom: 0px;
	        PADDING-TOP: 0px; 
        }
     a:link {

        font-family: Arial, Helvetica, sans-serif; 
        font-size: 12px; 
        font-weight: normal; 
        color: #003366; 
        text-decoration: none;
    }

    a:visited { 

        font-family: Arial, Helvetica, sans-serif; 
        font-size: 12px; 
        font-weight: normal; 
        color: #003366; 
        text-decoration: none; 
    }

    a:hover { 

        font-family: Arial, Helvetica, sans-serif; 
        font-size: 12px; 
        font-weight: normal; 
        color: #0000ff; 
        text-decoration: underline 
    }
        -->
</style>
    
</head>
<body topmargin="0" leftmargin="0" >
    <form id="form1" runat="server">
      <div style="background-color:#EEEEEE; border-bottom: solid 1px #17485C;">
       <ComponentArt:Menu id="Menu1" 
				Orientation="Horizontal"
				CssClass="TopGroup"
				DefaultGroupCssClass="MenuGroup"				
				DefaultItemLookId="DefaultItemLook"
				DefaultDisabledItemLookId="DisabledItemLook"
				DefaultGroupItemSpacing="1"
				ExpandDelay="100"
				ImagesBaseUrl="../images/menu/"
				EnableViewState="true"
				ExpandTransition="Fade"
				CollapseTransition="Fade"				
				AutoPostBackOnSelect="false"				
				Width="100%"
				runat="server">
			<ItemLooks>
				<ComponentArt:ItemLook LookID="DefaultItemLook" CssClass="MenuItem" HoverCssClass="MenuItemHover" LabelPaddingLeft="10" LabelPaddingRight="10" LabelPaddingTop="3" LabelPaddingBottom="3" />
				<ComponentArt:ItemLook LookID="DisabledItemLook" CssClass="DisabledMenuItem" HoverCssClass="DisabledMenuItemHover" LabelPaddingLeft="10" LabelPaddingRight="10" LabelPaddingTop="3" LabelPaddingBottom="3" />
				<ComponentArt:ItemLook LookID="BreakItem" ImageUrl="break.gif" CssClass="MenuBreak" ImageHeight="1" ImageWidth="100%" />						
			</ItemLooks>
			<Items>
				<ComponentArt:MenuItem ID="mnuArquivo" Text="Arquivo">
					<ComponentArt:MenuItem ID="mnuFechar" Text="Fechar" ClientSideCommand="window.close()" />							
				</ComponentArt:MenuItem>
				<ComponentArt:MenuItem ID="mnuAgrupar" Text="Agrupamento" TextAlign="left" DefaultSubItemTextAlign="left">
				    <ComponentArt:MenuItem ID="mnuIgreja" Text="Célula" Value="0" TextAlign="Left"  ClientSideCommand="Agrupar('mnuIgreja');" />
				    <ComponentArt:MenuItem ID="mnuOnline" Text="Online/Offline" Value="1" ClientSideCommand="Agrupar('mnuOnline');" />				    
				</ComponentArt:MenuItem>
				<ComponentArt:MenuItem ID="MenuItem1" Text="" Enabled="false" Width="100%" />
			</Items>
		</ComponentArt:Menu>							
       </div>  
        <table width="350" height="474" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/fundo_chat.jpg);" >
          <tr>
            <td align="center" height="474" valign="top">
                <table width="330" height="470" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td align="right" valign="bottom" colspan="2">
                        
                      <img src="../images/logo_chat_conversa.png" />
                     </td>                    
                  </tr>
                  <tr align="center" valign="top">
                    <td colspan="2">
			            <table width="100%" height="335"   border="1" cellpadding="0" cellspacing="0" bordercolor="#5B7EC0" bgcolor="#DCE5EA">
  				            <tr>
				                <td height="335">
						            <!--- AQUI ENTRA O COMPONENTE MOSTRANDO OS USUÁRIOS DO CHAT. 
						            LARGURA A 100% E ALTURA A 380 PIXELS E SCROLL BAR VERTICAL --->
						            <div style="overflow-y:scroll; overflow-x:hidden;width:100%;height:335px; margin: 0 0 0 0;">
    						        
						            <Anthem:DataList runat="server" ID="dlGrupo" BorderWidth="0" Width="96%" OnItemDataBound="dlGrupo_ItemDataBound">
                                        <ItemTemplate>
                                            <table style="width:100%; border-width:0px;" class="formtexto" cellspacing="0" >
                                                <tr runat="server" id="trGrupo">
                                                    <td align="center" width="10px">
													    <asp:Image Runat="server" ImageUrl="../images/minus.gif" ID="imgGrupo" />
													    <Anthem:HiddenField runat="server" ID="hiddenGrupo" Value="0" />
												    </td>
                                                    <td align="left" style="color:#003399; font-size:14px" >
                                                        <b>
                                                        <%# ((DataRowView)Container.DataItem)["Grupo"].ToString() + 
                                                            " (" + ((DataRowView)Container.DataItem)["Quantidade"].ToString() + ")" %>
                                                            </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" width="10px">
												    </td>
                                                    <td>
                                                    <Anthem:DataList runat="server" ID="dlServidor" BorderWidth="0" Width="96%">
                                                        <ItemTemplate>
                                                            <table style="width:100%; border-width:0px;" class="formtexto" >
                                                                <tr>
                                                                    <td align="center" width="5%">
                                                                        <img border="0"  src='
                                                                        <%# Convert.ToBoolean(((DataRowView)Container.DataItem)["online"]) ? "../images/online.png" : "../images/offline.png" %>
                                                                        ' />
                                                                    </td>
                                                                    <td align="left">
                                                                        <a href='#' onclick='OpenChat(<%# ((DataRowView)Container.DataItem)["ID_Servidor"].ToString()  %>);' >
                                                                            <%# ((DataRowView)Container.DataItem)["NomeGuerra"].ToString() %>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </Anthem:DataList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </Anthem:DataList>
        						    </div>
					            </td>
				            </tr>
			            </table>
		            </td>
                  </tr>
                </table>
              </td>
          </tr>
        </table>
         <Anthem:Timer ID="timer" runat="server" Interval="10000">
         </Anthem:Timer>
    </form>
</body>
</html>
