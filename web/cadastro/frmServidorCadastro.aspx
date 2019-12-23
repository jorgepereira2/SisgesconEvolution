<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmServidorCadastro.aspx.cs" Inherits="frmServidorCadastro" %>
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
                    Servidor
                    <hr size="1" class="divisor" />
                </div>
                <br />
               <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" >
                           Nome Completo:
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
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" >
                           Nome Guerra:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNomeGuerra" 
                                MaxLength="20" Columns="50" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ID="valNomeGuerra" ControlToValidate="txtNomeGuerra"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" >
                           NIP:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNIP" 
                                MaxLength="8" Columns="50" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtNIP"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" width="20%" >
                           Posto/Graduação:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtGraduacao" 
                                MaxLength="40" Columns="50" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtGraduacao"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr> 
                     <tr>
                        <td class="msgErro" ></td>
                        <td align="right" width="20%" >
                           Discriminação Função:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtDiscriminacaoFuncao" 
                                MaxLength="70" Columns="50" />
                           
                        </td>
                    </tr> 
                     <tr>
						<td class="msgErro" >*</td>
						<td align="right" >
						   Tipo
						</td>
						<td align="left">
							<Anthem:DropDownList runat="server" ID="ddlTipoServidor" />	
							   &nbsp;
							   <Anthem:RequiredFieldValidator runat="server" ID="valTipoServidor" ControlToValidate="ddlTipoServidor"
									 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />
						</td>
					</tr>
					<tr>
						<td class="msgErro" >*</td>
						<td align="right" >
						   Função
						</td>
						<td align="left">
							<Anthem:DropDownList runat="server" ID="ddlFuncaoServidor" />	
							   &nbsp;
							   <Anthem:RequiredFieldValidator runat="server" ControlToValidate="ddlFuncaoServidor"
									 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />
						</td>
					</tr>  
                    <tr>
						<td class="msgErro" >*</td>
						<td align="right" >
						   Célula
						</td>
						<td align="left">
							<Anthem:DropDownList runat="server" ID="ddlCelula" />	
							   &nbsp;
							   <Anthem:RequiredFieldValidator runat="server" ID="valCelula" ControlToValidate="ddlCelula"
									 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />
						</td>
					</tr> 
                   <tr>
                       <td ></td>
                        <td align="right" >
                           E-mail:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtEmail" MaxLength="70" Columns="40" />
                           &nbsp;
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator" 
                                ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                        </td>
                    </tr>
                    <tr>
                        <td ></td>
                        <td align="right" width="20%" >
                           Telefone:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtTelefone" 
                                MaxLength="35" Columns="50" />
                          
                           
                        </td>
                    </tr> 
                    <tr>
                        <td  ></td>
                        <td align="right" width="20%" >
                           Tel. Celular:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtCelular" 
                                MaxLength="25" Columns="50" />                           
                        </td>
                    </tr> 
                    <tr>
                        <td  ></td>
                        <td align="right" width="20%" >
                           Faz Atividade Direta:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkFlagFazAtividadeDireta" />
                        </td>
                    </tr> 
                    <tr>
						<td >&nbsp;</td>
						<td align="right" >
						   Data Saída:
						</td>
						<td align="left">
							<Anthem:DateTextBox runat="server" ID="txtDataSaida"  />	
						</td>
					</tr>
					
					
                </table>                     
            </td>
        </tr>	
        <tr>
            <td style="border:solid 1px black" valign="top" align="center">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Histórico Alterações
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <div align="right">
                    <Anthem:Button runat="server" ID="btnNovoHistorico"  TextDuringCallBack="Aguarde" Text="Novo"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />&nbsp;&nbsp;
                </div>
                <anthem:DataGrid runat="server" ID="dgHistorico" Width="98%" CssClass="datagrid" AutoGenerateColumns="false" CellPadding="3" >
                    <HeaderStyle CssClass="dgHeader" />                                    
                    <ItemStyle CssClass="dgItem" HorizontalAlign="Center" />
                    <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="Center" />
                    <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Data" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                 <%# ((ServidorHistorico)Container.DataItem).Data.ToShortDateString() %> 
                            </ItemTemplate>
                            <EditItemTemplate>
                                <Anthem:DateTextBox runat="server" ID="txtDataHistorico" Text="<%# ((ServidorHistorico)Container.DataItem).Data.ToShortDateString() %>"  />
                            </EditItemTemplate> 
                            <FooterTemplate>
                                <Anthem:DateTextBox runat="server" ID="txtDataHistorico"  />
                            </FooterTemplate>                                            
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Célula" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                 <%# ((ServidorHistorico)Container.DataItem).Celula.Descricao %> 
                            </ItemTemplate>
                            <EditItemTemplate>
                                <Anthem:DropDownList runat="server" ID="ddlCelulaHistorico"  />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <Anthem:DropDownList runat="server" ID="ddlCelulaHistorico"  />
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Faz Atividade Direta" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                 <%# ((ServidorHistorico)Container.DataItem).FlagFazAtividadeDireta ? "Sim" : "Não" %> 
                            </ItemTemplate>
                            <EditItemTemplate>
                                <Anthem:CheckBox runat="server" ID="chkFazAtividadeDiretaHistorico" Checked="<%# ((ServidorHistorico)Container.DataItem).FlagFazAtividadeDireta %> "  />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <Anthem:CheckBox runat="server" ID="chkFazAtividadeDiretaHistorico"  />
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
