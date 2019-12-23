<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoAtividadeSecundariaCadastro.aspx.cs" Inherits="frmPedidoServicoAtividadeSecundariaCadastro" %>
<%@ Register Src="~/UserControls/BuscaCliente.ascx" TagName="BuscaCliente" TagPrefix="uc" %>
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
        Cadastro de Pedido de Serviço Atividade Secundária
    
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
                           Célula:
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
                        <td  class="msgErro" >*</td>
                        <td align="right" >
                           Quantidade HH:
                        </td>
                        <td align="left">
                          <Anthem:NumericTextBox runat="server" ID="txtQuantidadeHH"  Columns="30" />     &nbsp;
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQuantidadeHH"  ErrorMessage="Campo obrigatório"
                             Display="Dynamic" />                         
                        </td>
                    </tr>	
                    <tr>
                        <td  class="msgErro" >*</td>
                        <td align="right" >
                           Data Vencimento:
                        </td>
                        <td align="left">
                          <Anthem:DateTextBox runat="server" ID="txtDataVencimento"   />                           
                          &nbsp;
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDataVencimento"  ErrorMessage="Campo obrigatório"
                             Display="Dynamic" />                         
                        </td>
                    </tr>	
                   <%--  <tr>
                        <td ></td>
                        <td align="right" >
                           Categoria Serviço:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlCategoriaServico" />                           
                        </td>
                    </tr>   --%>  
                   
                  
            
                                 
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
        


         <tr>
            <td style="border:solid 1px black" valign="top" align="center">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Itens
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <div align="right">
                    <Anthem:Button runat="server" ID="btnNovoItem"  TextDuringCallBack="Aguarde" Text="Novo Item" Width="120px"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />&nbsp;&nbsp;
                </div>
                <anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid" AutoGenerateColumns="false" CellPadding="3" >
                    <HeaderStyle CssClass="dgHeader" />                                    
                    <ItemStyle CssClass="dgItem" HorizontalAlign="Center" />
                    <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="Center" />
                    <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Serviço" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                 <%# ((PedidoServicoAtividadeSecundariaItem)Container.DataItem).DescricaoServico %> 
                            </ItemTemplate>
                             <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtDescricaoServico" Text='<%# ((PedidoServicoAtividadeSecundariaItem)Container.DataItem).DescricaoServico %>' />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDescricaoServico" ErrorMessage="Campo obrigatório"
                                     Display="Dynamic" ValidationGroup="Item" />
                            </EditItemTemplate> 
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtDescricaoServico" />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDescricaoServico" ErrorMessage="Campo obrigatório"
                                     Display="Dynamic" ValidationGroup="Item" />
                            </FooterTemplate>                                            
                        </asp:TemplateColumn>

                         <asp:TemplateColumn HeaderText="Tipo Atividade" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                 <%# ((PedidoServicoAtividadeSecundariaItem)Container.DataItem).TipoAtividadeSecundaria.Descricao %> 
                            </ItemTemplate>
                             <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="ddlTipoAtividadeSecundaria" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlTipoAtividadeSecundaria" InitialValue="0" ErrorMessage="Campo obrigatório"
                                     Display="Dynamic" ValidationGroup="Item" />
                            </EditItemTemplate> 
                            <FooterTemplate>
                                <asp:DropDownList runat="server" ID="ddlTipoAtividadeSecundaria" />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlTipoAtividadeSecundaria" InitialValue="0" ErrorMessage="Campo obrigatório"
                                     Display="Dynamic" ValidationGroup="Item" />
                            </FooterTemplate>                                            
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                 <%# ((PedidoServicoAtividadeSecundariaItem)Container.DataItem).Valor.ToString("N2") %> 
                            </ItemTemplate>      
                             <EditItemTemplate>
                                
                                 <Anthem:NumericTextBox runat="server" ID="txtValor" MaxLength="10" Columns="12" DecimalPlaces="0" Text='<%# ((PedidoServicoAtividadeSecundariaItem)Container.DataItem).Valor.ToString("N2") %>' />
                                   &nbsp;                           
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtValor" ErrorMessage="Campo obrigatório"
                                     Display="Dynamic" ValidationGroup="Item" />
                            </EditItemTemplate>                     
                            <FooterTemplate>                                
                                 <Anthem:NumericTextBox runat="server" ID="txtValor" MaxLength="10" Columns="12" DecimalPlaces="0" />
                                   &nbsp;                           
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtValor" ErrorMessage="Campo obrigatório"
                                     Display="Dynamic" ValidationGroup="Item" />

                            </FooterTemplate>
                        </asp:TemplateColumn>

                       

                       
                        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" CommandName="Edit" CausesValidation="false" />&nbsp;
                                <Anthem:LinkButton runat="server" ID="btnExcluir" Text="Excluir" CommandName="Delete" CausesValidation="false"/>    
                            </ItemTemplate>  
                            <EditItemTemplate>
                                <Anthem:LinkButton runat="server" ID="btnSalvarItem" Text="Salvar" CommandName="Update" 
                                    CausesValidation="true" ValidationGroup="Item"/>
                                &nbsp;
                                <Anthem:LinkButton runat="server" ID="btnCancelarNovo" Text="Cancelar" CommandName="Cancel" 
                                    CausesValidation="false"/>
                            </EditItemTemplate>
                           
                            <FooterTemplate>
                                <Anthem:LinkButton runat="server" ID="btnSalvarNovo" Text="Salvar" CommandName="Insert" 
                                    CausesValidation="true" ValidationGroup="Item"/>
                                &nbsp;
                                <Anthem:LinkButton runat="server" ID="btnCancelarNovo" Text="Cancelar" CommandName="Cancel" 
                                    CausesValidation="false"/>
                            </FooterTemplate>                                            
                        </asp:TemplateColumn>                        
                    </Columns>
                </anthem:datagrid>

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
                  <Anthem:Button runat="server" ID="btnFatura" TextDuringCallBack="Aguarde" Text="Fatura"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />     
                <Anthem:Button runat="server" ID="btnRecalcularFatura" TextDuringCallBack="Aguarde" Text="Recalcular Fatura"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />     
              <%--  <Anthem:Button runat="server" ID="btnBuscarPedidoServico"  TextDuringCallBack="Aguarde" Text="Copiar PS" Width="120px"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />  &nbsp; --%>               
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
