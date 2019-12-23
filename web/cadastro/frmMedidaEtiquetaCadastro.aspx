<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMedidaEtiquetaCadastro.aspx.cs" Inherits="frmMedidaEtiquetaCadastro" %>
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
        Cadastro de Medidas de Etiquetas  
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Medidas de Etiqueta
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Nome:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNome" 
                                MaxLength="50" Columns="50" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="valNome" ControlToValidate="txtNome"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Linhas:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtLinhas" Columns="50" DecimalPlaces="0" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtLinhas"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Colunas:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtColunas" Columns="50" DecimalPlaces="0" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtColunas"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                   <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Altura Papel:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtAlturaPapel" Columns="50" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtAlturaPapel"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Largura Papel:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtLarguraPapel" Columns="50" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtLarguraPapel"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                   <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Margem Direita:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtMargemDireita" Columns="50" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtMargemDireita"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Margem Esquerda:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtMargemEsquerda" Columns="50" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtMargemEsquerda"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                   <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Margem Superior:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtMargemSuperior" Columns="50" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtMargemSuperior"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Margem Inferior:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtMargemInferior" Columns="50" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtMargemInferior"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Separação Horizontal:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtSeparacaoHorizontal" Columns="50" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtSeparacaoHorizontal"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Separação Vertial:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtSeparacaoVertical" Columns="50" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtSeparacaoVertical"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Altura Conteúdo:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtAlturaConteudo" Columns="50" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtAlturaConteudo"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
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
