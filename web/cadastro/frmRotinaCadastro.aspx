<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmRotinaCadastro.aspx.cs" Inherits="frmRotinaCadastro" %>
<%@ Register TagPrefix="uc" TagName="ServicoMaterial_1" Src="~/UserControls/BuscaServicoMaterial.ascx" %>
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
        Cadastro de Rotinas       
    
    </div>

    <div style="display:none">
     <uc:ServicoMaterial_1 runat="server" ID="uctest"  AutoCallBack="true" TipoServicoMaterial="Material"  />
    </div>                  
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Rotina
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    
                    <tr>
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Descrição:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtDescricao" 
                                MaxLength="100" Columns="50" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtDescricao"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
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
                        <td width="10%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Categoria Serviço:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlCategoriaServico" AutoCallBack="true" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCategoriaServico" InitialValue="0"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>  
                    
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Total:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblTotal" Font-Bold="True"  />
                        </td>
                    </tr>                                    
                </table>     
                

                 <table border="0" cellpadding="2" cellspacing="4" width="100%" >

                  <tr>                            
                        <td colspan="3" align="center" valign="top">
                        <br />
                            <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                Delineamento                                    
                                <hr size="1" class="divisor" style="" />
                            </div>
                            <br />
                           
                            <Anthem:DataGrid runat="server" ID="dgDelineamento" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>    
                                     <%--<asp:TemplateColumn HeaderText="Delineador" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                             <%# ((RotinaCategoriaServicoDelineamento)Container.DataItem).ServidorDelineamento.NomeCompleto %>                                             
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn> --%>                               
                                    <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                             <%# ((RotinaCategoriaServicoDelineamento)Container.DataItem).Celula.Descricao%>                                             
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlCelulaNovo" AutoCallBack="true"  />&nbsp;
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2x" runat="server" ControlToValidate="ddlCelulaNovo" ErrorMessage="Campo obrigatório" InitialValue="0"
                                                 Display="dynamic" ValidationGroup="Delineamento" />                                            
                                        </FooterTemplate>
                                         <EditItemTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlCelula" AutoCallBack="true" />  &nbsp;
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCelula" ErrorMessage="Campo obrigatório" InitialValue="0"
                                                 Display="dynamic" ValidationGroup="Delineamento" />
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>                                    
                                    <asp:TemplateColumn HeaderText="HH" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((RotinaCategoriaServicoDelineamento)Container.DataItem).HomemHora%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtHomemHoraNovo" columns="8" MaxLength="8" DecimalPlaces="0"/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2v" runat="server" ControlToValidate="txtHomemHoraNovo" 
                                                 Display="Dynamic" ValidationGroup="Delineamento" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtHomemHora" columns="8" MaxLength="8" DecimalPlaces="0"
                                             Text='<%# ((RotinaCategoriaServicoDelineamento)Container.DataItem).HomemHora %>'
                                            />                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2b" runat="server" ControlToValidate="txtHomemHora" 
                                                 Display="Dynamic" ValidationGroup="DelineamentoEdit" ErrorMessage="Campo Obrigatório"/> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="Descrição Serviço" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# ((RotinaCategoriaServicoDelineamento)Container.DataItem).DescricaoServicoOficina%>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:TextBox  runat="server" ID="txtDescricaoServicoNovo" columns="20" MaxLength="14" TextMode="MultiLine"
                                                Rows="2" />                                            
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:TextBox  runat="server" ID="txtDescricaoServico" columns="20" MaxLength="14" TextMode="MultiLine"
                                                Rows="2"  Text='<%# ((RotinaCategoriaServicoDelineamento)Container.DataItem).DescricaoServicoOficina %>'/>                                            
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>                                                                                                           
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>                                            
                                             <a href="#" onclick='javascript:ExcluirDelineamento(<%# ((RotinaCategoriaServicoDelineamento)Container.DataItem).ID %>)' >
                                                    Excluir
                                                </a>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:LinkButton ID="btnSalvarDelineamento" runat="server" CommandName="Insert" 
                                                Text="Adicionar" ValidationGroup="Delineamento" TextDuringCallBack="Aguarde"/>
                                                &nbsp;
                                            <Anthem:LinkButton ID="btnCancelarDelineamento" runat="server" CommandName="Cancel" 
                                                Text="Cancelar" CausesValidation="false" />
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                                <Anthem:LinkButton runat="server" ID="btnSalvar" Text="Salvar" CommandName="Update" 
                                                    CausesValidation="true" ValidationGroup="Delineamento" EnabledDuringCallBack="false" TextDuringCallBack="Aguarde" />
                                                &nbsp;
                                                <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" 
                                                    CausesValidation="false"/>
                                            </EditItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </Anthem:DataGrid>
                             <div align="center">
                                <table cellpadding="2" cellspacing="2" border="0" width="98%">
                                    <tr>
                                        <td align="left">
                                            <Anthem:Label runat="server" ID="lblHomemHoraTotal"  Font-Bold="true" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Button runat="server" ID="btnNovoDelineamento"  TextDuringCallBack="Aguarde" Text="Novo"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>                                
                            </div>
                            <br />
                        </td>
                    </tr>
                      
                    <tr>                            
                        <td colspan="3" align="center" valign="top">
                        <br />
                            <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                Materiais                                    
                                <hr size="1" class="divisor" style="" />
                            </div>
                            <br />                            
                            <Anthem:DataGrid runat="server" ID="dgMaterial" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>         
                                     <%--<asp:TemplateColumn HeaderText="Delineador" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                             <%# ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).ServidorDelineamento.NomeCompleto%>                                             
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>--%>
                                     <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                             <%# ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).Celula.Descricao%>                                             
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlCelula" AutoCallBack="true"  />&nbsp;
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2cx" runat="server" ControlToValidate="ddlCelula" ErrorMessage="Campo obrigatório" InitialValue="0"
                                                 Display="dynamic" ValidationGroup="Servico" />                                            
                                        </FooterTemplate>
                                        
                                     </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Código Material" ItemStyle-HorizontalAlign="left" >
                                        <ItemTemplate>
                                            <%# ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).ServicoMaterial.CodigoInterno%>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <div style="text-align:left">
                                            Material:<br />
                                            <Anthem:TextBox runat="server" ID="txtCodigoMaterial" AutoCallBack="true"  OnTextChanged="txtCodigoMaterial_TextChanged"  />
                                           <br />
                                           <uc:ServicoMaterial_1 runat="server" ID="ucServicoMaterialNovo"  AutoCallBack="true" TipoServicoMaterial="Material" 
                                                OnSelectedValueChanged="ucServicoMaterial_SelectedValueChanged"  />                                            
                                                                              
                                           
                                        </FooterTemplate>
                                       
                                    </asp:TemplateColumn>                               
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).ServicoMaterial.Descricao%>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            
                                        </FooterTemplate>
                                       
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn HeaderText="Origem Material" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                             <%# Shared.Common.Util.GetDescription(((RotinaCategoriaServicoItemOrcamento)Container.DataItem).OrigemMaterial)%>                                             
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlOrigemMaterial" AutoCallBack="true" />  
                                        </FooterTemplate>
                                       
                                    </asp:TemplateColumn>
                                                                   
                                    <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).Quantidade + " " + ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).ServicoMaterial.Unidade.Descricao%>
                                        </ItemTemplate>                                        
                                        <FooterTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtQuantidadeNovo" columns="5" MaxLength="8" DecimalPlaces="2"/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2u" runat="server" ControlToValidate="txtQuantidadeNovo" 
                                                 Display="Dynamic" ValidationGroup="Item" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtQuantidade" columns="5" MaxLength="8" DecimalPlaces="2"
                                                text='<%# ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).Quantidade %>'/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2i" runat="server" ControlToValidate="txtQuantidade" 
                                                 Display="Dynamic" ValidationGroup="ItemEdit" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn> 
                                    
                                    <asp:TemplateColumn HeaderText="Preço Estimado" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%#  (((RotinaCategoriaServicoItemOrcamento)Container.DataItem).Quantidade * ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).ServicoMaterial.PrecoEstimadoVenda).ToString("C2") %>
                                        </ItemTemplate>                                        
                                        <FooterTemplate>
                                            
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            
                                        </EditItemTemplate>
                                    </asp:TemplateColumn> 
                                    
                                   <%-- <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnDadosComplementares" Text="Dados Complementares" 
                                                    CausesValidation="false" CommandName="DadosComplementares"/>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnDadosComplementares" Text="Dados Complementares" 
                                                    CausesValidation="false" CommandName="DadosComplementares"/>
                                        </FooterTemplate>
                                         <EditItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnDadosComplementares" Text="Dados Complementares" 
                                                    CausesValidation="false" CommandName="DadosComplementares" />
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>       --%>                                                                                                 
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                           
                                             <a href="#" onclick='javascript:ExcluirItemMaterial(<%# ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).ID %>)' >
                                                    Excluir
                                                </a>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:LinkButton ID="btnSalvarNovo" runat="server" CommandName="Insert" 
                                                Text="Adicionar" ValidationGroup="Item" EnabledDuringCallBack="false" TextDuringCallBack="Aguarde" />
                                            &nbsp;
                                            <Anthem:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" 
                                                Text="Cancelar" CausesValidation="false" />
                                        </FooterTemplate>
                                         <EditItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnSalvar" Text="Salvar" CommandName="Update" 
                                                CausesValidation="true" ValidationGroup="Item" EnabledDuringCallBack="false" TextDuringCallBack="Aguarde" />
                                            &nbsp;
                                            <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" 
                                                CausesValidation="false"/>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </Anthem:DataGrid>
                            <div align="center">
                                <table cellpadding="2" cellspacing="2" border="0" width="98%">
                                    <tr>
                                        <td align="left">
                                            <Anthem:Label runat="server" ID="lblValorTotalMaterial"  Font-Bold="true" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Button runat="server" ID="btnNovoMaterial"  TextDuringCallBack="Aguarde" Text="Novo"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>                                
                            </div>
                            <br />
                        </td>
                    </tr>
                    
                     <tr>                            
                        <td colspan="3" align="center" valign="top">
                        <br />
                            <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                Serviços
                                <hr size="1" class="divisor" style="" />
                            </div>
                            <br />
                           
                            <Anthem:DataGrid runat="server" ID="dgServicos" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>         
                                     <%--<asp:TemplateColumn HeaderText="Delineador" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                             <%# ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).ServidorDelineamento.NomeCompleto%>                                             
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>--%>
                                     <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                             <%# ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).Celula.Descricao%>                                             
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlCelula" AutoCallBack="true"  />&nbsp;
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2cx" runat="server" ControlToValidate="ddlCelula" ErrorMessage="Campo obrigatório" InitialValue="0"
                                                 Display="dynamic" ValidationGroup="Servico" />                                            
                                        </FooterTemplate>
                                        
                                     </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Código Serviço" ItemStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <%# ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).ServicoMaterial.CodigoInterno%>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <div style="text-align:left">
                                            Serviço:<br />
                                            <Anthem:TextBox runat="server" ID="txtCodigoServico" AutoCallBack="true" OnTextChanged="txtCodigoMaterial_TextChanged" />
                                            <br />
                                            <uc:ServicoMaterial_1 runat="server" ID="ucServicoMaterialNovo"  AutoCallBack="true" TipoServicoMaterial="Servico" 
                                                OnSelectedValueChanged="ucServicoMaterial_SelectedValueChanged"  />       
                                            <br />                                            
                                            </div>
                                        </FooterTemplate>                                       
                                    </asp:TemplateColumn>                               
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).ServicoMaterial.Descricao%>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                                                                     
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                                                                       
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>                                                           
                                                                   
                                    <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).Quantidade + " " + ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).ServicoMaterial.Unidade.Descricao%>
                                        </ItemTemplate>                                        
                                        <FooterTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtQuantidadeNovo" columns="5" MaxLength="8" DecimalPlaces="0"/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2p" runat="server" ControlToValidate="txtQuantidadeNovo" 
                                                 Display="Dynamic" ValidationGroup="Servico" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>                                       
                                    </asp:TemplateColumn>     
                                    
                                     <asp:TemplateColumn HeaderText="Preço Estimado" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%#  (((RotinaCategoriaServicoItemOrcamento)Container.DataItem).Quantidade * ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).ServicoMaterial.PrecoEstimadoVenda).ToString("C2") %>
                                        </ItemTemplate>                                        
                                        <FooterTemplate>
                                            
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            
                                        </EditItemTemplate>
                                    </asp:TemplateColumn> 
                                                                 
                                    <%-- <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnDadosComplementares" Text="Dados Complementares" 
                                                    CausesValidation="false" CommandName="DadosComplementares"/>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnDadosComplementares" Text="Dados Complementares" 
                                                    CausesValidation="false" CommandName="DadosComplementares"/>
                                        </FooterTemplate>                                         
                                    </asp:TemplateColumn>     --%>      
                                                                                                                               
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>                                          
                                             <a href="#" onclick='javascript:ExcluirItemServico(<%# ((RotinaCategoriaServicoItemOrcamento)Container.DataItem).ID %>)' >
                                                    Excluir
                                                </a>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:LinkButton ID="btnSalvarNovo" runat="server" CommandName="Insert" 
                                                Text="Adicionar" ValidationGroup="Servico" EnabledDuringCallBack="false" TextDuringCallBack="Aguarde" />
                                            &nbsp;
                                            <Anthem:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" 
                                                Text="Cancelar" CausesValidation="false" />
                                        </FooterTemplate>
                                         <EditItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnSalvar" Text="Salvar" CommandName="Update" 
                                                CausesValidation="true" ValidationGroup="Servico" EnabledDuringCallBack="false" TextDuringCallBack="Aguarde" />
                                            &nbsp;
                                            <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" 
                                                CausesValidation="false"/>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </Anthem:DataGrid>
                             <div align="center">
                                <table cellpadding="2" cellspacing="2" border="0" width="98%">
                                    <tr>
                                        <td align="left">
                                            <Anthem:Label runat="server" ID="lblValorTotalServico"  Font-Bold="true" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Button runat="server" ID="btnNovoServico"  TextDuringCallBack="Aguarde" Text="Novo"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>                                
                            </div>
                            <br />                            
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
