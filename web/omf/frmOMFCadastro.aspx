<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOMFCadastro.aspx.cs" Inherits="frmOMFCadastro" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/Buscafornecedor.ascx" TagName="Buscafornecedor" TagPrefix="uc" %>
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
        Cadastro de OMF       
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Dados
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                           Número:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblNumero" CssClass="legenda" />
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                           Status:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblStatus" CssClass="legenda" />
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Número Nota:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNumeroNota" 
                                MaxLength="30" Columns="30" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtNumeroNota"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Data Entrega:
                        </td>
                        <td align="left">
                           <Anthem:DateTextBox runat="server" ID="txtDataEntrega" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtDataEntrega"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Número Empenho:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNumeroEmpenho" MaxLength="30" Columns="30" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtNumeroEmpenho"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Fornecedor:
                        </td>
                        <td align="left">
                           <uc:BuscaFornecedor runat="server" ID="ucFornecedor" ShowNovo="true" />
                           &nbsp;
                           
                        </td>
                    </tr>    
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Recebedor:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlRecebedor" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRecebedor"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />
                        </td>
                    </tr>                
                     <tr>
		                <td ></td>
		                <td align="right">
		                   Descriminação Material:
		                </td>
		                <td align="left">
			                <Anthem:TextBox runat="server" ID="txtDescriminacaoMaterial" Columns="50" TextMode="MultiLine" Rows="2"  />
		                </td>
	                </tr>
                    
	                 <tr>
                        <td  ></td>
                        <td align="right" >
                           Tipo Emprego:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlTipoEmprego" />                           
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
                 <Anthem:Button runat="server" ID="btnEnviar" TextDuringCallBack="Aguarde" Text="Enviar"
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
