<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOcorrenciaCadastro.aspx.cs" Inherits="frmOcorrenciaCadastro" %>
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
        Lançamento de Ocorrências     
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Ocorrência
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >                                                         
                     <tr runat="server" id="trPedidoServico">
                        <td class="msgErro" >*</td>
                        <td align="right"  class="label">
                           PS:
                        </td>
                        <td align="left">
                           <uc:Orcamento runat="server" ID="ucOrcamento" Required="true" />                           
                           &nbsp;
                           <Anthem:LinkButton runat="server" ID="lnkDetalhes" Text="(Ver Detalhes)" CausesValidation="false" />
                        </td>
                    </tr>
                     <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Oficina:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlCelula" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCelula"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />
                        </td>
                    </tr>     
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Data Início:
                        </td>
                        <td align="left">
                           <Anthem:DateTextBox runat="server" ID="txtDataInicio" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtDataInicio"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Previsão Fim:
                        </td>
                        <td align="left">
                           <Anthem:DateTextBox runat="server" ID="txtDataPrevisaoFim" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDataPrevisaoFim"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>                             
					<tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                           Descrição:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtDescricaoServico" Columns="40" TextMode="MultiLine" Rows="2" />
                            &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescricaoServico"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                           Serviço Terceiro:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkFlagServicoTerceiro" />                           
                        </td>
                    </tr>  
                    
                     <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                           Parte Equipamento:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkFlagParteEquipamento" AutoCallBack="true" />
                           &nbsp;
                           <Anthem:TextBox runat="server" ID="txtParteEquipamento" Columns="30" />
                        </td>
                    </tr>
                     <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Data Fim:
                        </td>
                        <td align="left">
                           <Anthem:DateTextBox runat="server" ID="txtDataFim" />                           
                        </td>
                    </tr>                             
                    <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                           Descrição Conclusão:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtDescricaoConclusao" Columns="40" TextMode="MultiLine" Rows="2" />
                           
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
                <Anthem:Button runat="server" ID="btnNovo" TextDuringCallBack="Aguarde" Text="Novo"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />     
                <Anthem:Button runat="server" ID="btnImprimir" TextDuringCallBack="Aguarde" Text="Imprimir"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />     
                <Anthem:Button runat="server" ID="btnVoltar" Text="Voltar"
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
