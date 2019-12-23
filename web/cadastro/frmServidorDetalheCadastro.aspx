<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmServidorDetalheCadastro.aspx.cs" Inherits="frmServidorDetalheCadastro" %>
<%@ Register Src="~/UserControls/BuscaCliente.ascx" TagPrefix="uc" TagName="BuscaCliente" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
      <link href="../css/tabStyle.css" type="text/css" rel="stylesheet" />  
      
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Cadastro de Servidores       
    
    </div>
   
                
           <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Detalhes Servidor
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                     <tr>
                        <td ></td>
                        <td align="right" width="30%" >
                           Servidor:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblNomeServidor"  />  
                        </td>
                    </tr> 
                     <tr>
                        <td ></td>
                        <td align="right" width="30%" >
                           Identidade:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtIdentidade"  MaxLength="20" Columns="50" />                           
                        </td>
                    </tr> 
                    <tr>
                        <td ></td>
                        <td align="right" >
                           Orgão Emissor:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtOrgaoEmissor"  MaxLength="20" Columns="50" />                           
                        </td>
                    </tr> 
                     <tr>
                        <td ></td>
                        <td align="right" >
                          CPF:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtCPF"  MaxLength="20" Columns="50" />                           
                        </td>
                    </tr> 
                    <tr>
                        <td ></td>
                        <td align="right" >
                          PASEP:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtPASEP"  MaxLength="20" Columns="50" />                           
                        </td>
                    </tr> 
                    <tr>
                        <td ></td>
                        <td align="right" >
                          Número Título Eleitoral:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNumeroTituloEleitoral"  MaxLength="20" Columns="50" />                           
                        </td>
                    </tr> 
                    <tr>
                        <td ></td>
                        <td align="right" >
                          Seção Título Eleitoral:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtSecaoTituloEleitoral"  MaxLength="20" Columns="50" />                           
                        </td>
                    </tr> 
                    <tr>
                        <td ></td>
                        <td align="right" >
                          Zona Título Eleitoral:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtZonaTituloEleitoral"  MaxLength="20" Columns="50" />                           
                        </td>
                    </tr> 
                    <tr>
                        <td ></td>
                        <td align="right" >
                          Número Carteira Motorista:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNumeroCarteiraMotorista"  MaxLength="20" Columns="50" />                           
                        </td>
                    </tr> 
                    <tr>
                        <td ></td>
                        <td align="right" >
                          Doador Orgãos:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkDoadorOrgaos"  />                           
                        </td>
                    </tr> 
                    <tr>
						<td  ></td>
						<td align="right" >
						   Tipo Sanguíneo:
						</td>
						<td align="left">
							<Anthem:DropDownList runat="server" ID="ddlTipoSanguineo" />								  
						</td>
					</tr>
                    <tr>
                        <td ></td>
                        <td align="right" >
                          Fator RH:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtFatorRH"  MaxLength="20" Columns="50" />                           
                        </td>
                    </tr> 
                   <tr>
						<td >&nbsp;</td>
						<td align="right" >
						   Data Nascimento:
						</td>
						<td align="left">
							<Anthem:DateTextBox runat="server" ID="txtDataNascimento"  />	
						</td>
					</tr>
                    <tr>
                        <td ></td>
                        <td align="right" >
                          Naturalidade:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNaturalidade"  MaxLength="30" Columns="50" />                           
                        </td>
                    </tr> 
                    <tr>
                        <td ></td>
                        <td align="right" >
                          Nome Pai:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNomePai"  MaxLength="70" Columns="50" />                           
                        </td>
                    </tr> 
                    <tr>
                        <td ></td>
                        <td align="right" >
                          Nome Mãe:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNomeMae"  MaxLength="70" Columns="50" />                           
                        </td>
                    </tr> 
                     <tr>
                        <td ></td>
                        <td align="right" >
                          Endereço:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtEndereco"  MaxLength="70" Columns="50" />                           
                        </td>
                    </tr> 
                     <tr>
                        <td ></td>
                        <td align="right" >
                          Bairro:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtBairro"  MaxLength="70" Columns="50" />                           
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
                     <tr>
                        <td ></td>
                        <td align="right" >
                         Telefone Residencial:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtTelefoneResidencial"  MaxLength="20" Columns="50" />                           
                        </td>
                    </tr> 

                     <tr>
                        <td ></td>
                        <td align="right" >
                          Nome Pessoa Contato:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNomePessoaContato"  MaxLength="70" Columns="50" />                           
                        </td>
                    </tr> 
                     <tr>
                        <td ></td>
                        <td align="right" >
                          Endereço Contato:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtEnderecoContato"  MaxLength="70" Columns="50" />                           
                        </td>
                    </tr> 
                     <tr>
                        <td ></td>
                        <td align="right" >
                          Bairro Contato:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtBairroContato"  MaxLength="70" Columns="50" />                           
                        </td>
                    </tr> 
                      <tr>
						<td >&nbsp;</td>
						<td align="right" >
						    CEP Contato:
						</td>
						<td align="left">
							<Anthem:TextBox runat="server" ID="txtCEPContato" MaxLength="9" Columns="12" />
						</td>
					</tr>
					<tr>
						<td ></td>
						<td align="right" >
						    Estado Contato:
						</td>
						<td align="left">
							<Anthem:DropDownList runat="server" ID="ddlEstadoContato" AutoCallBack="true" />								                   
						</td>
					</tr>
					<tr>
						<td ></td>
						<td align="right" >
						    Município Contato:
						</td>
						<td align="left">
							<Anthem:DropDownList runat="server" ID="ddlMunicipioContato" />								                   
						</td>
					</tr>                   
                     <tr>
                        <td ></td>
                        <td align="right" >
                         Telefone Contato:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtTelefoneContato"  MaxLength="20" Columns="50" />                           
                        </td>
                    </tr> 
                    <tr>
                        <td ></td>
                        <td align="right" >
                         Celular Contato:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtCelularContato"  MaxLength="20" Columns="50" />                           
                        </td>
                    </tr> 
                     <tr>
						<td  ></td>
						<td align="right" >
						   Estado Civil:
						</td>
						<td align="left">
							<Anthem:DropDownList runat="server" ID="ddlEstadoCivil" />								  
						</td>
					</tr>
                    <tr>
                        <td ></td>
                        <td align="right" >
                         Nome conjuge:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNomeConjuge"  MaxLength="70" Columns="50" />                           
                        </td>
                    </tr> 
                    <tr>
						<td >&nbsp;</td>
						<td align="right" >
						   Data Incorporação MB:
						</td>
						<td align="left">
							<Anthem:DateTextBox runat="server" ID="txtDataIncorporacaoMB"  />	
						</td>
					</tr>
                    <tr>
						<td >&nbsp;</td>
						<td align="right" >
						   OM Incorporação:
						</td>
						<td align="left">
							 <uc:BuscaCliente runat="server" ID="ucOMIncorporacao" /> 
						</td>
					</tr>
                    <tr>
						<td >&nbsp;</td>
						<td align="right" >
						   Data Última Promoção:
						</td>
						<td align="left">
							<Anthem:DateTextBox runat="server" ID="txtDataultimaPromocao"  />	
						</td>
					</tr>
                    <tr>
						<td >&nbsp;</td>
						<td align="right" >
						   Data Apresentação:
						</td>
						<td align="left">
							<Anthem:DateTextBox runat="server" ID="txtDataApresentacao"  />	
						</td>
					</tr>
                    <tr>
						<td >&nbsp;</td>
						<td align="right" >
						   OM Origem:
						</td>
						<td align="left">
							 <uc:BuscaCliente runat="server" ID="ucOMOrigem" /> 
						</td>
					</tr>


                   
                     <tr>
                        <td ></td>
                        <td align="right" >
                          Língua Estrangeira:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkLinguaEstrangeira"  />                           
                        </td>
                    </tr> 
                     <tr>
                        <td ></td>
                        <td align="right" >
                         Especificação Língua Estrangeira:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtDescricaoLinguaEstrangeira"  MaxLength="100" Columns="50" />                           
                        </td>
                    </tr> 
                      <tr>
                        <td ></td>
                        <td align="right" >
                          Pratica Esportes:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkPraticaEsporte"  />                           
                        </td>
                    </tr> 
                     <tr>
                        <td ></td>
                        <td align="right" >
                         Especificação Esportes:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtEspecificacaoEsporte"  MaxLength="100" Columns="50" />                           
                        </td>
                    </tr> 
                     <tr>
                        <td ></td>
                        <td align="right" >
                          Deseja Horas Fúnebres:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkDesejaHorasFunebres"  />                           
                        </td>
                    </tr> 
					<tr>
                        <td ></td>
                        <td align="right" >
                         Religião:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtReligiao"  MaxLength="30" Columns="50" />                           
                        </td>
                    </tr> 
                    <tr>
                        <td ></td>
                        <td align="right" >
                         Outras Habilidades:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtOutrasHabilidades"  MaxLength="100" Columns="50" />                           
                        </td>
                    </tr> 
                </table>                     
            </td>
        </tr>
        
         <tr>
            <td style="border:solid 1px black" valign="top" align="center">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Dependentes
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <div align="right">
                    <Anthem:Button runat="server" ID="btnNovoDependente"  TextDuringCallBack="Aguarde" Text="Novo"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />&nbsp;&nbsp;
                </div>
               <anthem:DataGrid runat="server" ID="dgDependentes" Width="98%" CssClass="datagrid" AutoGenerateColumns="false" CellPadding="3" >
                    <HeaderStyle CssClass="dgHeader" />                                    
                    <ItemStyle CssClass="dgItem" HorizontalAlign="Center" />
                    <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="Center" />
                    <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Nome" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                 <%# ((ServidorDependente)Container.DataItem).Nome %> 
                            </ItemTemplate>
                            <EditItemTemplate>
                                <Anthem:TextBox runat="server" ID="txtNomeDependente" Text="<%# ((ServidorDependente)Container.DataItem).Nome %>"  />
                            </EditItemTemplate> 
                            <FooterTemplate>
                                <Anthem:TextBox runat="server" ID="txtNomeDependente"  />
                            </FooterTemplate>                                            
                        </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="Grau Parentesco" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                 <%# ((ServidorDependente)Container.DataItem).GrauParentesco %> 
                            </ItemTemplate>
                            <EditItemTemplate>
                                <Anthem:TextBox runat="server" ID="txtGrauParentesco" Text="<%# ((ServidorDependente)Container.DataItem).GrauParentesco %>"  />
                            </EditItemTemplate> 
                            <FooterTemplate>
                                <Anthem:TextBox runat="server" ID="txtGrauParentesco"  />
                            </FooterTemplate>                                            
                        </asp:TemplateColumn>
                     
                        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" CommandName="Edit" CausesValidation="false" /> 
                            </ItemTemplate>
                            <EditItemTemplate>
                                <Anthem:LinkButton runat="server" ID="btnSalvar" Text="Salvar" CommandName="Update" 
                                    CausesValidation="true" ValidationGroup="Descricao" />
                                &nbsp;
                                <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" 
                                    CausesValidation="false"/>
                            </EditItemTemplate>
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
                                <Anthem:LinkButton runat="server" ID="btnExcluir" Text="Excluir" CommandName="Delete" />                                                
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </anthem:datagrid>

            </td>
        </tr>		
        
           <tr>
            <td style="border:solid 1px black" valign="top" align="center">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Condecorações
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <div align="right">
                    <Anthem:Button runat="server" ID="btnNovaCondecoracao"  TextDuringCallBack="Aguarde" Text="Novo"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />&nbsp;&nbsp;
                </div>
               <anthem:DataGrid runat="server" ID="dgCondecoracoes" Width="98%" CssClass="datagrid" AutoGenerateColumns="false" CellPadding="3" >
                    <HeaderStyle CssClass="dgHeader" />                                    
                    <ItemStyle CssClass="dgItem" HorizontalAlign="Center" />
                    <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="Center" />
                    <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Descrição" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                 <%# ((ServidorCondecoracao)Container.DataItem).Descricao %> 
                            </ItemTemplate>
                            <EditItemTemplate>
                                <Anthem:TextBox runat="server" ID="txtDescricaoCondecoracao" Text="<%# ((ServidorCondecoracao)Container.DataItem).Descricao %>"  />
                            </EditItemTemplate> 
                            <FooterTemplate>
                                <Anthem:TextBox runat="server" ID="txtDescricaoCondecoracao"  />
                            </FooterTemplate>                                            
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" CommandName="Edit" CausesValidation="false" /> 
                            </ItemTemplate>
                            <EditItemTemplate>
                                <Anthem:LinkButton runat="server" ID="btnSalvar" Text="Salvar" CommandName="Update" 
                                    CausesValidation="true" ValidationGroup="Descricao" />
                                &nbsp;
                                <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" 
                                    CausesValidation="false"/>
                            </EditItemTemplate>
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
                                <Anthem:LinkButton runat="server" ID="btnExcluir" Text="Excluir" CommandName="Delete" />                                                
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </anthem:datagrid>

            </td>
        </tr>			
        
          <tr>
            <td style="border:solid 1px black" valign="top" align="center">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Cursos Militares
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <div align="right">
                    <Anthem:Button runat="server" ID="btnNovoCursoMilitar"  TextDuringCallBack="Aguarde" Text="Novo"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />&nbsp;&nbsp;
                </div>
               <anthem:DataGrid runat="server" ID="dgCursosMilitares" Width="98%" CssClass="datagrid" AutoGenerateColumns="false" CellPadding="3" >
                    <HeaderStyle CssClass="dgHeader" />                                    
                    <ItemStyle CssClass="dgItem" HorizontalAlign="Center" />
                    <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="Center" />
                    <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Descrição" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                 <%# ((ServidorCursoMilitar)Container.DataItem).Descricao %> 
                            </ItemTemplate>
                            <EditItemTemplate>
                                <Anthem:TextBox runat="server" ID="txtDescricaoCursoMilitar" Text="<%# ((ServidorCursoMilitar)Container.DataItem).Descricao %>"  />
                            </EditItemTemplate> 
                            <FooterTemplate>
                                <Anthem:TextBox runat="server" ID="txtDescricaoCursoMilitar"  />
                            </FooterTemplate>                                            
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" CommandName="Edit" CausesValidation="false" /> 
                            </ItemTemplate>
                            <EditItemTemplate>
                                <Anthem:LinkButton runat="server" ID="btnSalvar" Text="Salvar" CommandName="Update" 
                                    CausesValidation="true" ValidationGroup="Descricao" />
                                &nbsp;
                                <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" 
                                    CausesValidation="false"/>
                            </EditItemTemplate>
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
                                <Anthem:LinkButton runat="server" ID="btnExcluir" Text="Excluir" CommandName="Delete" />                                                
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </anthem:datagrid>

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
