<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>
<%@ Import namespace="Marinha.Business"%>
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SIC</title>
    <LINK href="css/quadro.css" type="text/css" rel="stylesheet">
</head>
<body >
    <form id="form1" runat="server">
    <div style="text-align:center">
        <br />
        <table cellpadding="2" cellspacing="2" border="0" width="98%">
            <tr>
                <td align="center" style="width:50%;">
                
                    <%--######################
                    QUADRO  DE MENSAGENS
                    ########################--%>
                
                    <table cellpadding="0" class="quadro" cellspacing="0" border="0">
                        <tr>
                            <td class="top" colspan="3">
                                Quadro de Mensagens
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                            
                            </td>
                            <td class="middle">
                                <wc:SlideShow runat="server" ID="ssTeste" ShowEffectDuration="2000" DisplayInterval="6000" HorizontalAlign="Left" 
                                    NextButtonID="btnNext" BackButtonID="btnBack" PauseButtonID="btnPause"
                                    Width="346px" >
                                    <ItemTemplate>                                
                                        <b><%# ((QuadroMensagem)Container.DataItem).Titulo %>
                                        </b>
                                        
                                        <p style="text-align:justify;">
                                        <%# ((QuadroMensagem)Container.DataItem).Mensagem %>
                                        </p>
                                        <br />
                                        <br />
                                        <div style="text-align:right">
                                        <i>
                                        -&nbsp;<%# ((QuadroMensagem)Container.DataItem).Assinatura %>&nbsp;&nbsp;
                                        </i>
                                        </div>                                        
                                    </ItemTemplate>
                                </wc:SlideShow>
                            </td>
                            <td class="right">
                            
                            </td>
                        </tr>
                        <tr>
                            <td class="bottom" colspan="3">
                                <Anthem:ImageButton runat="server" ImageUrl="images/left_arrow.png" ID="btnBack" CssClass="button"  />&nbsp;
                                <Anthem:ImageButton runat="server" ImageUrl="images/pause.png" ID="btnPause" CssClass="button" />&nbsp;
                                <Anthem:ImageButton runat="server" ImageUrl="images/right_arrow.png" ID="btnNext" CssClass="button" />
                                    
                            </td>
                        </tr>
                    </table>                
                </td>
                <td>
                
                
                
                
                </td>
            </tr>        
        </table>
                
     
    </div>
    </form>
</body>
</html>
