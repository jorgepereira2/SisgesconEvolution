<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOrcamentoEdicao.aspx.cs" Inherits="frmOrcamentoEdicao" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/BuscaOrcamento.ascx" TagName="Orcamento" TagPrefix="uc" %>
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
        Edição de Orçamento     
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Orçamento
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >                                                         
                     <tr runat="server" id="trPedidoServico">
                        <td class="msgErro" ></td>
                        <td align="right" width="40%"  class="label">
                           Código Interno:
                        </td>
                        <td align="left">
                           <%# _orcamento.CodigoComAno %>
                        </td>
                    </tr>
                     <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                           Mensagem de Orçamento:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtMensagemEnvioCliente" />
                        </td>
                    </tr>     
                  <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                           Mensagem Aprovação Cliente:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtMensagemAprovacaoCliente" />
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
                     EnabledDuringCallBack="false" CssClass="Button" />
                <Anthem:Button runat="server" ID="btnFechar" Text="Fechar" OnClientClick="self.close();"
                     CssClass="Button" CausesValidation="false" />
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
