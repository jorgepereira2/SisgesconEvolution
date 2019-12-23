<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoCustoRealizadoPesquisa.aspx.cs" Inherits="frmPedidoServicoCustoRealizadoPesquisa" %>
<%@ Register Src="~/UserControls/BuscaEquipamento.ascx" TagName="BuscaEquipamento" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/BuscaCliente.ascx" TagName="BuscaCliente" TagPrefix="uc" %>
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
        Relatório de Custo X Realizado por PS
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
                                            Gerente:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlGerente" />
                                        </td>
                                    </tr>              
                                    <tr>
                                        <td align="right">
                                            Divisão:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlDivisao" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Status:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlStatus" /> 
                                        </td>
                                    </tr>                                                        
                                    <tr>
                                        <td align="right">
                                            Equipamento:
                                        </td>
                                        <td align="left">
                                            <uc:BuscaEquipamento runat="server" ID="ucEquipamento" ShowNew="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Cliente:
                                        </td>
                                        <td align="left">
                                            <uc:BuscaCliente runat="server" ID="ucCliente" ShowNew="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            PROGEM:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlProgem" >
                                                <asp:ListItem Value="0">Todos</asp:ListItem>
                                                <asp:ListItem Value="True">Sim</asp:ListItem>
                                                <asp:ListItem Value="False">Não</asp:ListItem>
                                            </Anthem:DropDownList> 
                                        </td>
                                    </tr> 
                                    <tr>
                                        <td align="right">
                                            Número Registro:
                                        </td>
                                        <td align="left">
                                            <Anthem:TextBox runat="server" ID="txtNumeroRegistro" />                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Observação:
                                        </td>
                                        <td align="left">
                                            <Anthem:TextBox runat="server" ID="txtObservacao" />                                            
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
                                    <tr>
                                        <td align="right">
                                           Período:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlMes" />&nbsp;/&nbsp;
                                            <Anthem:DropDownList runat="server" ID="ddlAno" />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right">
                                            Código Interno:
                                        </td>
                                        <td align="left">
                                            <Anthem:TextBox runat="server" ID="txtNumeroPS" />                                            
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
