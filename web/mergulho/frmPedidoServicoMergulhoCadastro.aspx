<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoMergulhoCadastro.aspx.cs" Inherits="frmPedidoServicoMergulhoCadastro" %>
<%@ Register Src="~/UserControls/BuscaCliente.ascx" TagName="BuscaCliente" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/BuscaEquipamento.ascx" TagName="BuscaEquipamento" TagPrefix="uc" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
      <script language="javascript" type="text/javascript">
        function PedidoSelecionado()
        {            
            Anthem_InvokePageMethod("CopiarPedidoServico", 
                [document.getElementById('id_pedidoServico').value], function(result){});
        }
      
      </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="text" style="display:none" id="id_pedidoServico" />
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Cadastro de Pedido de Serviço de Mergulho
    
    </div>
    
    <Anthem:Panel runat="server" ID="pnPS" >

    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Pedido
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="5%" ></td>
                        <td align="right" width="20%" >
                           Código Interno:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblCodigo" CssClass="legenda" />                           
                        </td>
                    </tr>  
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Situação:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblStatus" CssClass="legenda" />                           
                        </td>
                    </tr>                                               
                    <tr>
                        <td ></td>
                        <td align="right" >
                           Emissão:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblDataEmissao" CssClass="legenda" />                           
                        </td>
                    </tr>                 
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Divisão:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlDivisao" />    
                             &nbsp;                           
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDivisao" InitialValue="0" ErrorMessage="Campo obrigatório"
                             Display="Dynamic" />                         
                        </td>
                    </tr>       
                     <tr>
						<td class="msgErro" >*</td>
						<td align="right" >
						   Cliente:
						</td>
						<td align="left">
							<uc:BuscaCliente runat="server" ID="ucCliente" Required="false" AutoCallBack="false" />	
							   &nbsp;							
						</td>
					</tr> 
					<tr>
						<td class="msgErro" >*</td>
						<td align="right" >
						   Cliente Pagador:
						</td>
						<td align="left">
							<uc:BuscaCliente runat="server" ID="ucClientePagador" Required="false" SelectedValue="0"  />	
							   &nbsp;							
						</td>
					</tr> 
                    <tr>
		                <td  ></td>
		                <td align="right" >
		                   PS Cliente:
		                </td>
		                <td align="left">
		                    <div style="overflow:auto;" id="divTeste" onclick="document.getElementById('divTeste').style.display = 'block';" >
			                    <Anthem:TextBox runat="server" ID="txtCodigoPedidoCliente" MaxLength="15" Columns="50" />						    
			                </div>
		                </td>
	                </tr>
	               <%-- <tr>
						<td class="msgErro" >*</td>
						<td align="right" >
						   Equipamento:
						</td>
						<td align="left">
							<uc:BuscaEquipamento runat="server" ID="ucEquipamento" Required="false"  />	
							   &nbsp;							
						</td>
					</tr> --%>
					<%--<tr>
                        <td  class="msgErro" >*</td>
                        <td align="right" >
                           Quantidade:
                        </td>
                        <td align="left">
                          
                        </td>
                    </tr>	--%>               
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Categoria Serviço:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlCategoriaServico" />                           
                        </td>
                    </tr>     
                      <tr>
                        <td class="msgErro">*</td>
                        <td align="right" >
                           Embarcação:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlEmbarcacao" />     
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlEmbarcacao" InitialValue="0" ErrorMessage="Campo obrigatório"
                             Display="Dynamic" />                          
                        </td>
                    </tr>    
                     <tr>
                        <td class="msgErro">*</td>
                        <td align="right" >
                           Prioridade:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlPrioridade" />     
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPrioridade" InitialValue="0" ErrorMessage="Campo obrigatório"
                             Display="Dynamic" />                          
                        </td>
                    </tr>    
                    <%--<tr>
                        <td  ></td>
                        <td align="right" >
                           Defeito Reclamado:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtDefeitoReclamado" TextMode="MultiLine" Rows="4" Columns="60" />                           
                        </td>
                    </tr>--%>
                    <tr>
                        <td  ></td>
                        <td align="right" >
                           Localizacao:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtLocalizacao" MaxLength="100" Columns="50" />                           
                        </td>
                    </tr>
                    <tr>
                        <td  ></td>
                        <td align="right" >
                           Contatos:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtContatos" MaxLength="100" Columns="50" />                           
                        </td>
                    </tr>
                    <tr>
                        <td  ></td>
                        <td align="right" >
                           Telefones Contato:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtTelefoneContatos" MaxLength="100" Columns="50" />                           
                        </td>
                    </tr>       
                     <tr>
                        <td  ></td>
                        <td align="right" >
                           Mensagem Solicitação:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtMensagemSolicitacao" TextMode="MultiLine" Rows="3" Columns="50" />                           
                        </td>
                    </tr>  
                                 
                    <tr>
                        <td  ></td>
                        <td align="right" >
                           Observação:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtObservacao" TextMode="MultiLine" Rows="3" Columns="50" />                           
                        </td>
                    </tr>  
                </table>                     
            </td>
        </tr>	
        
       
        																		
    </table>

    </Anthem:Panel>
    <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td width="40%" align="left">
            
            </td>
            <td align="right">
                <Anthem:Button runat="server" ID="btnSalvar" TextDuringCallBack="Aguarde" Text="Salvar"
                     EnabledDuringCallBack="false" CssClass="Button" />
                 <Anthem:Button runat="server" ID="btnEnviar" TextDuringCallBack="Aguarde" Text="Enviar"
                     EnabledDuringCallBack="false" CssClass="Button" />
                <Anthem:Button runat="server" ID="btnNovo" TextDuringCallBack="Aguarde" Text="Novo"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />     
                <Anthem:Button runat="server" ID="btnBuscarPedidoServico"  TextDuringCallBack="Aguarde" Text="Copiar PS" Width="120px"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />  &nbsp;                
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
