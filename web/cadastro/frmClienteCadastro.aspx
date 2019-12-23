<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmClienteCadastro.aspx.cs" Inherits="frmClienteCadastro" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/BuscaCliente.ascx" TagName="BuscaCliente" TagPrefix="uc" %>
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
        Cadastro de Clientes       
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Cliente
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Código:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtCodigo" 
                                MaxLength="5" Columns="20" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="valCodigo" ControlToValidate="txtCodigo"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Indicativo Naval:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtIndicativoNaval" 
                                MaxLength="6" Columns="20" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtIndicativoNaval"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Descrição:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtDescricao" 
                                MaxLength="70" Columns="50" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtDescricao"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           CNPJ/CPF:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtCNPJCPF" 
                                MaxLength="15" Columns="50" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtCNPJCPF"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>                    
                     <tr>
						<td class="msgErro" >*</td>
						<td align="right" >
						   Tipo
						</td>
						<td align="left">
							<Anthem:DropDownList runat="server" ID="ddlTipoCliente" />	
							   &nbsp;
							   <Anthem:RequiredFieldValidator runat="server" ID="valTipoCliente" ControlToValidate="ddlTipoCliente"
									 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />
						</td>
					</tr> 
                    <tr>
		                <td  class="msgErro">*</td>
		                <td align="right" >
		                   Telefone:
		                </td>
		                <td align="left">
			                <Anthem:TextBox runat="server" ID="txtTelefone" 
						                MaxLength="50" Columns="40" />
						     &nbsp;
			                   <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTelefone"
					                 ErrorMessage="Campo obrigatório" Display="dynamic" />
		                </td>
	                </tr>
	                 <tr>
                        <td  ></td>
                        <td align="right" >
                           Fax:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtFax" 
                                MaxLength="25" Columns="50" />                           
                        </td>
                    </tr>
	                 <tr>
                       <td ></td>
                        <td align="right" >
                           E-mail:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtEmail" MaxLength="70" Columns="50" />
                           &nbsp;
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator" 
                                ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                        </td>
                    </tr>                                    
                   
                    <tr>
                        <td ></td>
                        <td align="right" >
                           Home Page:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtHomePage" 
                                MaxLength="100" Columns="50" />
                        </td>
                    </tr>
                    <tr>
		                <td ></td>
		                <td align="right" >
		                   Cliente Pagador:
		                </td>
		                <td align="left">
			                <uc:BuscaCliente runat="server" ID="ucClientePagador" />
		                </td>
	                </tr>
                    <tr>
                        <td ></td>
                        <td align="right" >
                           Ativo:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkFlagAtivo" Checked="true" />
                        </td>
                    </tr>
                    <tr>
		                <td ></td>
		                <td align="right">
		                   Observação:
		                </td>
		                <td align="left">
			                <Anthem:TextBox runat="server" ID="txtObservacao" 
						                Columns="50" TextMode="MultiLine" Rows="2"  />
		                </td>
	                </tr>
                    <tr>
                        <td colspan="3" class="PageTitle" align="left">
                            <br />
                            Endereço
                            <hr size="1" class="divisor" />
                
                        </td>
                    </tr>
                  	<tr>
		                <td >&nbsp;</td>
		                <td align="right" >
		                   Endereço:
		                </td>
		                <td align="left">
			                <Anthem:TextBox runat="server" ID="txtEndereco" 
						                MaxLength="120" Columns="50" />	
		                </td>
	                </tr>
	                <tr>
		                <td ></td>
		                <td align="right" >
		                   Bairro:
		                </td>
		                <td align="left">
			                <Anthem:TextBox runat="server" ID="txtBairro" 
						                MaxLength="50" Columns="50" />
		                </td>
	                </tr>
	                <tr>
		                <td >&nbsp;</td>
		                <td align="right" >
		                   CEP:
		                </td>
		                <td align="left">
			                <Anthem:TextBox runat="server" ID="txtCEP" MaxLength="9" Columns="12" />
		                </td>
	                </tr>
	                <tr>
		                <td ></td>
		                <td align="right" >
		                   Estado:
		                </td>
		                <td align="left">
			                <Anthem:DropDownList runat="server" ID="ddlEstado" AutoCallBack="true" />								                   
		                </td>
	                </tr>
	                <tr>
		                <td ></td>
		                <td align="right" >
		                   Município:
		                </td>
		                <td align="left">
			                <Anthem:DropDownList runat="server" ID="ddlMunicipio" />								                   
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
                <Anthem:Button runat="server" ID="btnExcluir" TextDuringCallBack="Aguarde" Text="Excluir"
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
