<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmProvisionadoSiafiCadastro.aspx.cs" Inherits="frmProvisionadoSiafiCadastro" %>
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
        Cadastro de Provisionado Siafi    
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Provisionado Siafi    
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" class="label">
                           Ano:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlAno" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAno"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />
                        </td>
                    </tr>
                     <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Código Siafi:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtCodigoSiafi"  />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCodigoSiafi"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Ação Interna:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlProjeto" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="ddlProjeto" InitialValue="0"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Natureza Despesa:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlNaturezaDespesa" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlNaturezaDespesa" InitialValue="0"
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
