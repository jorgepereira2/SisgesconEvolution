<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmExtratoPesquisa.aspx.cs" Inherits="frmExtratoPesquisa" %>
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
        Extrato de Movimento Financeiro
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
                                            Ação Interna:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlProjeto" /> 
                                            &nbsp;
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlProjeto" InitialValue="0" ErrorMessage="Campo obrigatório" Display="Dynamic" />                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Natureza Despesa:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlNaturezaDespesa" />
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlNaturezaDespesa" InitialValue="0" ErrorMessage="Campo obrigatório" Display="Dynamic" />
                                        </td>
                                    </tr>                                   
                                    <tr>
                                        <td align="right">
                                            Fonte Recurso:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlFonteRecurso" /> 
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlFonteRecurso" InitialValue="0" ErrorMessage="Campo obrigatório" Display="Dynamic" />
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td align="right">
                                            PTRES:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlPTRES" /> 
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPTRES" InitialValue="0" ErrorMessage="Campo obrigatório" Display="Dynamic" />
                                        </td>
                                    </tr>   
                                     <tr>
                                        <td align="right">
                                            Data:
                                        </td>
                                        <td align="left">
                                            <Anthem:DateTextBox runat="server" ID="txtDataInicio" />&nbsp;a&nbsp;
                                            <Anthem:DateTextBox runat="server" ID="txtDataFim" />
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDataInicio" ErrorMessage="Campo obrigatório" Display="Dynamic" />
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDataFim" ErrorMessage="Campo obrigatório" Display="Dynamic" />
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
                               EnabledDuringCallBack="false" CssClass="Button" CausesValidation="true" Width="170px" />
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
