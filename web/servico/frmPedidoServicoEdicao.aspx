<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoEdicao.aspx.cs" Inherits="frmPedidoServicoEdicao" %>
<%@ Register Src="~/UserControls/BuscaCliente.ascx" TagName="BuscaCliente" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/BuscaEquipamento.ascx" TagName="BuscaEquipamento" TagPrefix="uc" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
      
</head>
<body>
    <form id="form1" runat="server">
    <input type="text" style="display:none" id="id_pedidoServico" />
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Edição de Pedido de Serviço
    
    </div>
      
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
						<td class="msgErro" >*</td>
						<td align="right" >
						   Divisão:
						</td>
						<td align="left">
							<Anthem:DropDownList runat="server" ID="ddlDivisao" />	
							   &nbsp;							
						</td>
					</tr> 
					<tr>
                        <td class="msgErro">*</td>
                        <td align="right" >
                           Gerente:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlGerente" />      
                            &nbsp;                           
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlGerente" InitialValue="0" ErrorMessage="Campo obrigatório"
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
                           <Anthem:NumericTextBox runat="server" ID="txtQuantidade" MaxLength="10" Columns="12" DecimalPlaces="0" />
                           &nbsp;                           
                           <asp:RequiredFieldValidator runat="server" ControlToValidate="txtQuantidade" ErrorMessage="Campo obrigatório"
                             Display="Dynamic" />
                        </td>
                    </tr>--%>
	                <%-- <tr>
                        <td  ></td>
                        <td align="right" >
                           Número Registro:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNumeroRegistro" MaxLength="200" Columns="50" />                           
                        </td>
                    </tr>--%>
                  <%--  <tr>
                        <td  ></td>
                        <td align="right" >
                           Defeito Reclamado:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtDefeitoReclamado" MaxLength="100" Columns="50" />                           
                        </td>
                    </tr>--%>
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
                        <td ></td>
                        <td align="right" >
                           Progem:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkFlagProgem"/>
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
                    <tr>
                        <td  ></td>
                        <td align="right" >
                           Categoria:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlCategoria" />   
                        </td>
                    </tr> 
                    <tr>
                        <td  ></td>
                        <td align="right" >
                           Localização:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtLocalizacao" Columns="50"/>   
                        </td>
                    </tr> 
                     <%--<tr>
                        <td  ></td>
                        <td align="right" >
                           Prioridade:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlPrioridade" />   
                        </td>
                    </tr> --%>
                     <tr>
                        <td  ></td>
                        <td align="right" >
                           Data Pronto:
                        </td>
                        <td align="left">
                           <Anthem:DateTextBox runat="server" ID="txtDataPronto" />                           
                        </td>
                    </tr> 
                    <tr>
                        <td  ></td>
                        <td align="right" >
                           Data Planejamento:
                        </td>
                        <td align="left">
                           <Anthem:DateTextBox runat="server" ID="txtDataPlanejamento" />                           
                        </td>
                    </tr> 
                     <tr>
                        <td  ></td>
                        <td align="right" >
                           Diversos:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtDiversos" TextMode="MultiLine" Rows="3" Columns="50" />                           
                        </td>
                    </tr>   
                    <tr>
                        <td colspan="3" align="center">
                            <Anthem:DataGrid runat="server" ID="dgOrcamento" Width="80%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="false" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>                                        
                                    <asp:TemplateColumn HeaderText="Orçamento" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <%# ((DelineamentoOrcamento)Container.DataItem).Numero %>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Categoria" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlCategoriaOrcamento" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Cotador" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlCotador" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </Anthem:Datagrid>
                    
                        </td>
                    </tr> 
                    
                    <tr>
                        <td colspan="3" align="center">


                           <br />
                <div align="right">
                    <Anthem:Button runat="server" ID="btnNovoEquipamento"  TextDuringCallBack="Aguarde" Text="Novo Equipamento" Width="150"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />&nbsp;&nbsp;
                </div>
                <anthem:DataGrid runat="server" ID="dgEquipamento" Width="98%" CssClass="datagrid" AutoGenerateColumns="false" CellPadding="3" >
                    <HeaderStyle CssClass="dgHeader" />                                    
                    <ItemStyle CssClass="dgItem" HorizontalAlign="Center" />
                    <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="Center" />
                    <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Equipamento" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                 <%# ((PedidoServicoEquipamento)Container.DataItem).Equipamento.Descricao %> 
                            </ItemTemplate>
                             <EditItemTemplate>
                                <uc:BuscaEquipamento runat="server" ID="ucEquipamento" Required="false"  />	
                            </EditItemTemplate> 
                            <FooterTemplate>
                                <uc:BuscaEquipamento runat="server" ID="ucEquipamento" Required="false"  />	
                            </FooterTemplate>                                            
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                 <%# ((PedidoServicoEquipamento)Container.DataItem).Quantidade %> 
                            </ItemTemplate>      
                             <EditItemTemplate>
                                
                                 <Anthem:NumericTextBox runat="server" ID="txtQuantidade" MaxLength="10" Columns="12" DecimalPlaces="0" Text=" <%# ((PedidoServicoEquipamento)Container.DataItem).Quantidade %> " />
                                   &nbsp;                           
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQuantidade" ErrorMessage="Campo obrigatório"
                                     Display="Dynamic" ValidationGroup="Equipamento" />

                            </EditItemTemplate>                     
                            <FooterTemplate>
                                
                                 <Anthem:NumericTextBox runat="server" ID="txtQuantidade" MaxLength="10" Columns="12" DecimalPlaces="0" />
                                   &nbsp;                           
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQuantidade" ErrorMessage="Campo obrigatório"
                                     Display="Dynamic" ValidationGroup="Equipamento" />

                            </FooterTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Defeito Reclamado" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                 <%# ((PedidoServicoEquipamento)Container.DataItem).DefeitoReclamado %> 
                            </ItemTemplate>   
                            <EditItemTemplate>                                
                                 <Anthem:TextBox runat="server" ID="txtDefeitoReclamado" Text="<%# ((PedidoServicoEquipamento)Container.DataItem).DefeitoReclamado %>" />                                   
                            </EditItemTemplate>                        
                            <FooterTemplate>                                
                                 <Anthem:TextBox runat="server" ID="txtDefeitoReclamado" />                                   
                            </FooterTemplate>
                        </asp:TemplateColumn>

                       
                        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" CommandName="Edit" CausesValidation="false" />&nbsp;
                                <Anthem:LinkButton runat="server" ID="btnExcluir" Text="Excluir" CommandName="Delete" CausesValidation="false"/>    
                            </ItemTemplate>  
                            <EditItemTemplate>
                                <Anthem:LinkButton runat="server" ID="btnSalvarEquipamento" Text="Salvar" CommandName="Update" 
                                    CausesValidation="true" ValidationGroup="Equipamento"/>
                                &nbsp;
                                <Anthem:LinkButton runat="server" ID="btnCancelarNovo" Text="Cancelar" CommandName="Cancel" 
                                    CausesValidation="false"/>
                            </EditItemTemplate>
                           
                            <FooterTemplate>
                                <Anthem:LinkButton runat="server" ID="btnSalvarNovo" Text="Salvar" CommandName="Insert" 
                                    CausesValidation="true" ValidationGroup="Equipamento"/>
                                &nbsp;
                                <Anthem:LinkButton runat="server" ID="btnCancelarNovo" Text="Cancelar" CommandName="Cancel" 
                                    CausesValidation="false"/>
                            </FooterTemplate>                                            
                        </asp:TemplateColumn>                        
                    </Columns>
                </anthem:datagrid>
                           


                       </td>
                    </tr> 

                    <tr>
                        <td colspan="3" align="center">


                             <br />
                                 <div align="right" style="vertical-align:text-bottom" class="PageTitle" >
                                   <Anthem:Button runat="server" ID="btnNovoDelineador"  TextDuringCallBack="Aguarde" Text="Novo Delineador" Width="150"
                                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                </div>  
                                <anthem:DataGrid runat="server" ID="dgDelineadores" Width="98%" CssClass="datagrid"
                                     AutoGenerateColumns="false" CellPadding="3" ShowFooter="false" >
                                    <HeaderStyle CssClass="dgHeader" />                                    
                                    <ItemStyle CssClass="dgItem" />
                                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                    <FooterStyle HorizontalAlign="left" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Delineador" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                    <%# ((DelineamentoOficina)Container.DataItem).Servidor.NomeCompleto %>
                                            </ItemTemplate>     
                                            <FooterTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlDelineador" />
                                            </FooterTemplate>                                       
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                               <%# ((DelineamentoOficina)Container.DataItem).Oficina.Codigo + " " + ((DelineamentoOficina)Container.DataItem).Oficina.Descricao%>
                                            </ItemTemplate>  
                                            <FooterTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlOficinaDelineador" />
                                            </FooterTemplate>                                          
                                        </asp:TemplateColumn>
                                                               
                                         <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">                                           
                                            <FooterTemplate>
                                                <Anthem:LinkButton runat="server" ID="btnSalvarNovo" Text="Salvar" CommandName="Insert" 
                                                    CausesValidation="true" ValidationGroup="Descricao"/>
                                                &nbsp;
                                                <Anthem:LinkButton runat="server" ID="btnCancelarNovo" Text="Cancelar" CommandName="Cancel" 
                                                    CausesValidation="false"/>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                                                                                                
                                        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <a href="#" onclick='javascript:ExcluirDelineamento(<%# ((DelineamentoOficina)Container.DataItem).ID %>)' >
                                                    Excluir
                                                </a>                                              
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </anthem:DataGrid>


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
