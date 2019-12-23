<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEquipamentoCadastro.aspx.cs" Inherits="frmEquipamentoCadastro" %>
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
        Cadastro de Equipamentoes       
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Equipamento
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" >
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
						<td align="right" >
						   Tipo:
						</td>
						<td align="left">
							<Anthem:DropDownList runat="server" ID="ddlTipoEquipamento" AutoCallBack="true" />	
							   &nbsp;
							   <Anthem:RequiredFieldValidator runat="server" ID="valTipoEquipamento" ControlToValidate="ddlTipoEquipamento"
									 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />
						</td>
					</tr> 
					 <tr>
						<td class="msgErro" >*</td>
						<td align="right" >
						   Sub-Tipo:
						</td>
						<td align="left">
							<Anthem:DropDownList runat="server" ID="ddlSubTipoEquipamento" />	
							   &nbsp;
							   <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlSubTipoEquipamento"
									 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />
						</td>
					</tr>
					<tr>
						<td class="msgErro" >*</td>
						<td align="right" >
						   Tipo Operativo:
						</td>
						<td align="left">
							<Anthem:DropDownList runat="server" ID="ddlTipoOperativo" />	
							   &nbsp;
							   <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlTipoOperativo"
									 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />
						</td>
					</tr>  
                    <tr>
		                <td  ></td>
		                <td align="right" >
		                   Codeq:
		                </td>
		                <td align="left">
			                <Anthem:TextBox runat="server" ID="txtCodeq" 
						                MaxLength="15" Columns="50" />						    
		                </td>
	                </tr>
	                 <tr>
                        <td  ></td>
                        <td align="right" >
                           Descrição Codeq:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtDescricaoCodeq" 
                                MaxLength="70" Columns="50" />                           
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
