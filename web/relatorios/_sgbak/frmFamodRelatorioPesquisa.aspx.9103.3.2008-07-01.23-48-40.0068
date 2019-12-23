<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFamodRelatorioPesquisa.aspx.cs" Inherits="frmFamodRelatorioPesquisa" %>
<%@ Register TagPrefix="Anthem" Assembly="Anthem" Namespace="Anthem" %>
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
        Relatório de FAMOD
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="94%" style="height:350px;" >																		    
        <tr>
            <td valign="top">                
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                    <tr>                            
                        <td colspan="2" align="center" valign="top" style="border:solid 1px black">
                        <br />
                            <div align="left" style="vertical-align:text-bottom">                                
                                <div class="PageTitle" >
                                &nbsp;&nbsp;
                                Filtros</div>
                                <hr size="1" class="divisor" style="" />
                                <table width="100%" cellpadding="2" cellspacing="2">    
                                    <tr>
                                        <td align="right">
                                            Atividade:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlAtividade" /> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Tipo Atividade:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlTipoAtividade" >
                                                <asp:ListItem Value="0">Todos</asp:ListItem>
                                                <asp:ListItem Value="True">Direta</asp:ListItem>
                                                <asp:ListItem Value="False">Indireta</asp:ListItem>                                                
                                            </Anthem:DropDownList> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Oficina:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlOficina" /> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Servidor:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlServidor" /> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Situação:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlSituacao" /> 
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right">
                                            Data:
                                        </td>
                                        <td align="left">
                                            <Anthem:DateTextBox runat="server" ID="txtDataInicio" />&nbsp;a&nbsp;
                                            <Anthem:DateTextBox runat="server" ID="txtDataFim" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />                            
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
                 <Anthem:Button runat="server" ID="btnImprimir"  TextDuringCallBack="Aguarde" Text="Imprimir / Visualizar"
                               EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" Width="170px" />
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
