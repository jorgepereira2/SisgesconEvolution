<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCotaMaterialCadastro.aspx.cs" Inherits="frmCotaMaterialCadastro" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/BuscaServicoMaterial.ascx" TagPrefix="uc" TagName="BuscaMaterial" %>
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
        Cadastro de Cota de Material      
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Cota de Material
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Célula:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlCelula" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="ddlCelula" InitialValue="0"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Material:
                        </td>
                        <td align="left">
                           <uc:BuscaMaterial runat="server" TipoServicoMaterial="Material" ID="ucMaterial" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" class="label">
                           Ano:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlAno" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="ddlAno"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" class="label">
                           Mês:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlMes" />
                           
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Quantidade:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtQuantidade" DecimalPlaces="0" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtQuantidade"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>          
                </table>              
                <br />       
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
                <Anthem:Button runat="server" ID="btnVoltar" Text="Voltar"
                     CssClass="Button" CausesValidation="false" />
            </td>
            <td width="10px">
                &nbsp;
            </td>
        </tr>
    </table>
    <br /><br /><br /><br /><br />
    </div>    
    
    </form>    
</body>
</html>
