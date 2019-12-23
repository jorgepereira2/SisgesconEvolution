<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoOrcamento.aspx.cs" Inherits="frmPedidoServicoOrcamento" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/BuscaServicoMaterial.ascx" TagPrefix="uc" TagName="ServicoMaterial" %>
<%@ Register Src="~/servico/DadosComplementaresItemServico.ascx" TagPrefix="uc" TagName="DadosComplementares" %>
<%@ Register Src="~/servico/Comentario.ascx" TagPrefix="uc" TagName="Comentario" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
      <script language="javascript" type="text/javascript">
        function PedidoSelecionado()
        {          
            alert(document.getElementById('id_orcamento').value); 
            Anthem_InvokePageMethod("CopiarOrcamento", [document.getElementById('id_orcamento').value], function(result){});
        }
      
      </script>
</head>
<body>
    <form id="form1" runat="server">       
    <input id="id_orcamento" style="display:none;" />
    <uc:DadosComplementares runat="server" ID="ucDadosComplementares" />
    <uc:Comentario runat="server" ID="ucComentario" />
    <Anthem:NumericTextBox  runat="server" ID="txtHidden" style="display:none"  DecimalPlaces="2"/> 
    <div align="center">
    <div align="right" style="width:98%" class="PageTitle">
    <br />
        Orçamento de Pedido de Serviço    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="98%" >																		    
        <tr   >
            <td style="border:solid 1px black" valign="top">                
                <table border="0" cellpadding="2" cellspacing="2" width="100%" >
                     <tr>
                        <td width="5%" ></td>
                        <td align="right" width="20%" >
                           Código Interno:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblCodigo" CssClass="legenda" />
                           &nbsp;
                           <Anthem:LinkButton runat="server" ID="lnkDetalhes" Text="(Ver Detalhes)" />                           
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
                           Status:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblStatus" CssClass="legenda" />                           
                        </td>
                    </tr>
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Equipamento:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblEquipamento" CssClass="legenda" />                           
                        </td>
                    </tr>       
                     <tr runat="server" id="trJustificativaRecusa">
                        <td ></td>
                        <td align="right" >
                           Justificativa recusa:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblJustificativaRecusa" CssClass="legenda" ForeColor="Red" Font-Bold="true" />                           
                        </td>
                    </tr> 
                   <tr>
                        <td ></td>
                        <td align="right" >
                           Rotina:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlRotina" /> &nbsp;&nbsp;
                             <a href="#" onclick='javascript:AdicionarRotina(0)' >
                                                    Adicionar Rotina
                                                </a>                       
                        </td>
                    </tr>  
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Rotinas:
                        </td>
                        <td align="left">
                           <Anthem:DataList runat="server" ID="dlRotinas">
                               <ItemTemplate>
                                   <%# ((Rotina)Container.DataItem).Descricao %>
                                   &nbsp;&nbsp;
                                   <Anthem:LinkButton runat="server" CommandName="Delete" CommandArgument="<%# ((Rotina)Container.DataItem).ID %>" >Remover</Anthem:LinkButton>
                               </ItemTemplate>
                           </Anthem:DataList>

                        </td>
                    </tr>  
                    
                    <tr>
                        <td align="center" colspan="3" >
                        <Anthem:Button runat="server" ID="btnSalvar" TextDuringCallBack="Aguarde" Text="Salvar" EnabledDuringCallBack="false" CssClass="Button" />&nbsp;
                        </td>                        
                    </tr>  
                           
                                                           
                    <%--<tr>
                        <td ></td>
                        <td align="right" >
                           Categoria:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlCategoriaServico" />                           
                        </td>
                    </tr>
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Validade:
                        </td>
                        <td align="left">
                           <Anthem:DateTextBox runat="server" ID="txtDataValidade" />                           
                        </td>
                    </tr>         --%>                  
                   <%-- <tr>
                        <td ></td>
                        <td align="right" >
                           Observacao:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtObservacao" TextMode="MultiLine" Rows="2" Columns="40" />                           
                        </td>
                    </tr>--%>
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
                                     <asp:TemplateColumn HeaderText="Delineador" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                             <%# ((PedidoServicoDelineamento)Container.DataItem).ServidorDelineamento.NomeCompleto %>                                             
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>                                
                                    <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                             <%# ((PedidoServicoDelineamento)Container.DataItem).Celula.Descricao %>                                             
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlCelulaNovo" AutoCallBack="true"  />&nbsp;
                                            <Anthem:RequiredFieldValidator runat="server" ControlToValidate="ddlCelulaNovo" ErrorMessage="Campo obrigatório" InitialValue="0"
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
                                            <%# ((PedidoServicoDelineamento)Container.DataItem).HomemHora %>
                                        </ItemTemplate>                                        
                                        <FooterTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtHomemHoraNovo" columns="8" MaxLength="8" DecimalPlaces="0"/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHomemHoraNovo" 
                                                 Display="Dynamic" ValidationGroup="Delineamento" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtHomemHora" columns="8" MaxLength="8" DecimalPlaces="0"
                                             Text='<%# ((PedidoServicoDelineamento)Container.DataItem).HomemHora %>'
                                            />                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHomemHora" 
                                                 Display="Dynamic" ValidationGroup="DelineamentoEdit" ErrorMessage="Campo Obrigatório"/> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="Descrição Serviço" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoDelineamento)Container.DataItem).DescricaoServicoOficina %>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:TextBox  runat="server" ID="txtDescricaoServicoNovo" columns="20" MaxLength="14" TextMode="MultiLine"
                                                Rows="2" />                                            
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:TextBox  runat="server" ID="txtDescricaoServico" columns="20" MaxLength="14" TextMode="MultiLine"
                                                Rows="2"  Text='<%# ((PedidoServicoDelineamento)Container.DataItem).DescricaoServicoOficina %>'/>                                            
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>                                                                                                           
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" 
                                                    CommandName="Edit" CausesValidation="false" />
                                            &nbsp;
                                             <a href="#" onclick='javascript:ExcluirDelineamento(<%# ((PedidoServicoDelineamento)Container.DataItem).ID %>)' >
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
                                     <asp:TemplateColumn HeaderText="Delineador" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                             <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServidorDelineamento.NomeCompleto%>                                             
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Código Material" ItemStyle-HorizontalAlign="left" >
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.CodigoInterno %>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <div style="text-align:left">
                                            Material:<br />
                                            <Anthem:TextBox runat="server" ID="txtCodigoMaterial" AutoCallBack="true" OnTextChanged="txtCodigoMaterial_TextChanged" />
                                           <br />
                                           <uc:ServicoMaterial runat="server" ID="ucServicoMaterialNovo"  AutoCallBack="true" TipoServicoMaterial="Material" 
                                                OnSelectedValueChanged="ucServicoMaterial_SelectedValueChanged"  />                                            
                                            <br />                                            
                                            Origem:<br />
                                            <Anthem:DropDownList runat="server" ID="ddlOrigemMaterialNovo" AutoCallBack="true" OnSelectedIndexChanged="ddlOrigemMaterial_SelectedIndexChanged" />                                            
                                            <br />
                                            <div>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <div style="text-align:left">
                                            Material:<br />
                                            <Anthem:TextBox runat="server" ID="txtCodigoMaterial" AutoCallBack="true" OnTextChanged="txtCodigoMaterial_TextChanged" 
                                                Text='<%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.CodigoInterno %>'  />                                            
                                            <br />
                                            <uc:ServicoMaterial runat="server" ID="ucServicoMaterial" AutoCallBack="true" OnSelectedValueChanged="ucServicoMaterial_SelectedValueChanged"  
                                                SelectedValue='<%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.ID %>'
                                                TipoServicoMaterial="Material" 
                                                Text='<%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.Descricao %>'  />                                                
                                            <br />                                           
                                            Origem:<br />
                                            <Anthem:DropDownList runat="server" ID="ddlOrigemMaterial" AutoCallBack="true" OnSelectedIndexChanged="ddlOrigemMaterial_SelectedIndexChanged" />                                            
                                                
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>                               
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.Descricao %>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                             <%# ((PedidoServicoItemOrcamento)Container.DataItem).Celula.Descricao %>                                          
                                        </ItemTemplate>
                                        <FooterTemplate>
                                                                                     
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Origem Material" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                             <%# Shared.Common.Util.GetDescription(((PedidoServicoItemOrcamento)Container.DataItem).OrigemMaterial) %>                                             
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                
                                     <asp:TemplateColumn HeaderText="Disponível" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblQuantidadeEstoque" />
                                        </ItemTemplate>
                                         <EditItemTemplate>
                                            <Anthem:Label runat="server" ID="lblQuantidadeEstoque" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:Label runat="server" ID="lblQuantidadeEstoque" />
                                        </FooterTemplate>
                                    </asp:TemplateColumn> 
                                     <asp:TemplateColumn HeaderText="Saldo Licitação" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblSaldoLicitacao" />
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).Quantidade + " " + ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.Unidade.Descricao %>
                                        </ItemTemplate>                                        
                                        <FooterTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtQuantidadeNovo" columns="5" MaxLength="8" DecimalPlaces="2"/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuantidadeNovo" 
                                                 Display="Dynamic" ValidationGroup="Item" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtQuantidade" columns="5" MaxLength="8" DecimalPlaces="2"
                                                text='<%# ((PedidoServicoItemOrcamento)Container.DataItem).Quantidade %>'/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuantidade" 
                                                 Display="Dynamic" ValidationGroup="ItemEdit" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn> 
                                    <%--<asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).Valor.ToString("N2") %>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtValorNovo" columns="8" MaxLength="14" 
                                                CssClass="numerico" DecimalPlaces="2"/>                                                
                                            <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtValorNovo" 
                                                 Display="Dynamic" ValidationGroup="Item" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtValor" columns="8" MaxLength="14" 
                                                CssClass="numerico" DecimalPlaces="2" Text='<%# ((PedidoServicoItemOrcamento)Container.DataItem).Valor.ToString("N2") %>'/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValor" 
                                                 Display="Dynamic" ValidationGroup="ItemEdit" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>--%>
                                    <asp:TemplateColumn HeaderText="Total" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).ValorTotal.ToString("N2") %>
                                        </ItemTemplate>                                                                               
                                    </asp:TemplateColumn>  
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
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
                                    </asp:TemplateColumn>                                                                                                        
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" 
                                                    CommandName="Edit" CausesValidation="false" />
                                            &nbsp;
                                             <a href="#" onclick='javascript:Excluir(<%# ((PedidoServicoItemOrcamento)Container.DataItem).ID %>)' >
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
                                            <%--<Anthem:Label runat="server" ID="lblValorTotal"  Font-Bold="true" />--%>
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
                                     <asp:TemplateColumn HeaderText="Delineador" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                             <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServidorDelineamento.NomeCompleto%>                                             
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Código Serviço" ItemStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.CodigoInterno %>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <div style="text-align:left">
                                            Serviço:<br />
                                            <Anthem:TextBox runat="server" ID="txtCodigoServico" AutoCallBack="true" OnTextChanged="txtCodigoMaterial_TextChanged" />
                                            <br />
                                            <uc:ServicoMaterial runat="server" ID="ucServicoMaterialNovo"  AutoCallBack="true" TipoServicoMaterial="Servico" 
                                                OnSelectedValueChanged="ucServicoMaterial_SelectedValueChanged"  />       
                                            <br />                                            
                                            </div>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                             <div style="text-align:left">
                                            Serviço:<br />
                                            <Anthem:TextBox runat="server" ID="txtCodigoServico" AutoCallBack="true" OnTextChanged="txtCodigoMaterial_TextChanged" 
                                                Text='<%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.CodigoInterno %>'  />                                            
                                            <br />
                                            <uc:ServicoMaterial runat="server" ID="ucServicoMaterial" AutoCallBack="true" OnSelectedValueChanged="ucServicoMaterial_SelectedValueChanged"  
                                                SelectedValue='<%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.ID %>'
                                                TipoServicoMaterial="Servico"
                                                Text='<%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.Descricao %>'  /> 
                                            <br />                                           
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>                               
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.Descricao %>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                                                                     
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                                                                       
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                             <%# ((PedidoServicoItemOrcamento)Container.DataItem).Celula.Descricao %>                                             
                                        </ItemTemplate>
                                        <FooterTemplate>
                                                                                        
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Origem Material" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                             <%# Shared.Common.Util.GetDescription(OrigemMaterial.Obtencao) %>                                             
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                             
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                             
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>    
                                     <asp:TemplateColumn HeaderText="Saldo Licitação" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblSaldoLicitacao" />
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>                                 
                                    <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).Quantidade + " " + ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.Unidade.Descricao %>
                                        </ItemTemplate>                                        
                                        <FooterTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtQuantidadeNovo" columns="5" MaxLength="8" DecimalPlaces="0"/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuantidadeNovo" 
                                                 Display="Dynamic" ValidationGroup="Servico" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtQuantidade" columns="5" MaxLength="8" DecimalPlaces="0"
                                                text='<%# ((PedidoServicoItemOrcamento)Container.DataItem).Quantidade %>'/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuantidade" 
                                                 Display="Dynamic" ValidationGroup="Servico" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn> 
                                   <%-- <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).Valor.ToString("N2") %>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtValorNovo" columns="8" MaxLength="14" 
                                                CssClass="numerico" DecimalPlaces="2"/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtValorNovo" 
                                                 Display="Dynamic" ValidationGroup="Servico" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtValor" columns="8" MaxLength="14" 
                                                CssClass="numerico" DecimalPlaces="2" Text='<%# ((PedidoServicoItemOrcamento)Container.DataItem).Valor.ToString("N2") %>'/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValor" 
                                                 Display="Dynamic" ValidationGroup="Servico" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Total" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).ValorTotal.ToString("N2") %>
                                        </ItemTemplate>                                                                               
                                    </asp:TemplateColumn>  --%>   
                                     <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
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
                                    </asp:TemplateColumn>           
                                                                                                                               
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" 
                                                    CommandName="Edit" CausesValidation="false" />
                                            &nbsp;
                                             <a href="#" onclick='javascript:Excluir(<%# ((PedidoServicoItemOrcamento)Container.DataItem).ID %>)' >
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
    <br /><br />
    <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">                
                 <Anthem:Button runat="server" ID="btnEnviar" TextDuringCallBack="Aguarde" Text="Enviar"
                     EnabledDuringCallBack="false" CssClass="Button" />&nbsp;
                <Anthem:Button runat="server" ID="btnRecalcularTaxas" TextDuringCallBack="Aguarde" Text="Recalcular Taxas" Visible="false"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="true" />&nbsp;
                <Anthem:Button runat="server" ID="btnBuscarOrcamento"  TextDuringCallBack="Aguarde" Text="Buscar Orçamento" Width="180px" Visible="false"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />  &nbsp;                
                  <Anthem:Button runat="server" ID="btnDefinirFornecedor"  TextDuringCallBack="Aguarde" Text="Definir Fornecedor" Width="180px" Visible="false"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />  &nbsp;
                <Anthem:Button runat="server" ID="btnImprimir" Text="Imprimir/Visualizar"
                     CssClass="Button" CausesValidation="false" Width="160px" />
                <Anthem:Button runat="server" ID="btnVoltar" Text="Voltar"
                     CssClass="Button" CausesValidation="false" />
            </td>
            <td width="10px">
                &nbsp;
            </td>
        </tr>
    </table>
    </div>    
    <br /><br /><br /><br />
    </form>    
</body>
</html>
