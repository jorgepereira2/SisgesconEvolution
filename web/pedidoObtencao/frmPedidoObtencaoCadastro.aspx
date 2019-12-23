<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoObtencaoCadastro.aspx.cs" Inherits="frmPedidoObtencaoCadastro" ValidateRequest="false"   %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/UserControls/BuscaServicoMaterial.ascx" TagPrefix="uc" TagName="BuscaMaterial" %>
<%@ Register Src="~/UserControls/BuscaOrcamento.ascx" TagPrefix="uc" TagName="BuscaOrcamento" %>
<%@ Register Src="ucHistoricoPedidoObtencao.ascx" TagPrefix="uc" TagName="Historico" %>
<%@ Register TagPrefix="uc" TagName="BuscaFornecedor" Src="~/UserControls/BuscaFornecedor.ascx" %>
<%@ Register TagPrefix="uc" TagName="SaldoServicoMaterial" Src="SaldoServicoMaterial.ascx" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
      <link href="../css/tabStyle.css" type="text/css" rel="stylesheet" />  
     <script type="text/javascript">
    
    function AbaAlterada()
    {
        parent.iframeresize();
        Anthem_InvokePageMethod('AbaAlterada', [], function(result){});
    }    
   
    function POSelecionado(id_po)
    {       
        
        Anthem_InvokePageMethod('CopiarPO', [id_po]);
    }   
    </script> 
</head>
<body >
    <form id="form1" runat="server">
    <Anthem:ValidationSummary runat="server" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="true" />
    <div align="center">
    <div align="right" style="width:90%" Class="PageTitle">
    <br />
        <asp:Label runat="server" ID="lblTitulo" />        
    </div>
      <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 0px black" valign="top" align="left">               
                <br />
                
                <ComponentArt:TabStrip id="TabStrip1" 
                  CssClass="TopGroup"                  
                  DefaultItemLookId="DefaultTabLook"
                  DefaultSelectedItemLookId="SelectedTabLook"
                  DefaultDisabledItemLookId="DisabledTabLook" 
                  DefaultGroupTabSpacing="0" TopGroupAlign="Left" DefaultGroupAlign="Left" DefaultItemTextAlign="Left"
                  ImagesBaseUrl="../images/tabstrip/"
                  runat="server" MultiPageId="MultiPage1">
                <Tabs>
                    <ComponentArt:TabStripTab Text="Dados Básicos" PageViewId="pvDadosBasicos" LookId="DefaultTabLook" runat="server" ID="tabDadosBasicos" ClientSideCommand="AbaAlterada();" />
                    <ComponentArt:TabStripTab Text="Itens" runat="server" ID="tabItem" ClientSideCommand="AbaAlterada();" PageViewId="pvItem" />                    
                    <ComponentArt:TabStripTab Text="Histórico" runat="server" ID="tabHistorico" ClientSideCommand="AbaAlterada();" PageViewId="pvHistorico" />                    
                </Tabs>  
                <ItemLooks>
                  <ComponentArt:ItemLook LookId="DefaultTabLook" CssClass="DefaultTab" HoverCssClass="DefaultTabHover" LabelPaddingLeft="10" LabelPaddingRight="10" LabelPaddingTop="5" LabelPaddingBottom="4" LeftIconUrl="tab_left_icon.gif" RightIconUrl="tab_right_icon.gif" HoverLeftIconUrl="hover_tab_left_icon.gif" HoverRightIconUrl="hover_tab_right_icon.gif" LeftIconWidth="3" LeftIconHeight="21" RightIconWidth="3" RightIconHeight="21" />
                  <ComponentArt:ItemLook LookId="SelectedTabLook" CssClass="SelectedTab" LabelPaddingLeft="10" LabelPaddingRight="10" LabelPaddingTop="4" LabelPaddingBottom="4" LeftIconUrl="selected_tab_left_icon.gif" RightIconUrl="selected_tab_right_icon.gif" LeftIconWidth="3" LeftIconHeight="21" RightIconWidth="3" RightIconHeight="21" />
                </ItemLooks>
                </ComponentArt:TabStrip>
                <ComponentArt:MultiPage id="MultiPage1" CssClass="MultiPage" runat="server">
                    
                  <ComponentArt:PageView CssClass="PageContent" id="pvDadosBasicos" runat="server">
                    <table cellspacing="4" cellpadding="3" border="0" width="700px" >																		    
                        <tr>
                            <td style="border:solid 0px black" valign="top">
                                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                    Pedido
                                    <hr size="1" class="divisor" />
                                </div>
                                <br />
                                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                                     <tr>
                                        <td width="10%" class="msgErro" >*</td>
                                        <td align="right" width="20%" >
                                           Número:
                                        </td>
                                        <td align="left">
                                           <Anthem:Label runat="server" ID="lblNumero" CssClass="legenda"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%" class="msgErro" >*</td>
                                        <td align="right" width="20%" >
                                           Status:
                                        </td>
                                        <td align="left">
                                           <Anthem:Label runat="server" ID="lblStatus" CssClass="legenda"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%" class="msgErro" >*</td>
                                        <td align="right" width="20%" >
                                           Data Emissão:
                                        </td>
                                        <td align="left">
                                           <Anthem:Label runat="server" ID="lblData" CssClass="legenda" />
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Tipo Pedido:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlTipoPedido" Enabled="false" />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTipoPedido"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Origem Pedido:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlOrigemPO" Enabled="false" />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlOrigemPO"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr> 
                                    
                                    <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Tipo Compra:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlTipoCompra" AutoCallback="true"  />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlTipoCompra"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Modalidade:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlModalidade"  />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator95" runat="server" ControlToValidate="ddlModalidade"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>

                                    <tr runat="server" id="trTipoPedidoObtencao">
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Finalidade:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlTipoPedidoObtencao" />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator  runat="server" ControlToValidate="ddlTipoPedidoObtencao"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>

                                     <tr runat="server" id="trLicitacao">
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Licitação:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlLicitacao" />
                                           &nbsp;                                           
                                        </td>
                                    </tr>
                                    
                                    <tr runat="server" id="trPedidoServico">
                                        <td class="msgErro" >*</td>
                                        <td align="right"  class="label">
                                           Cod. Serv.:
                                        </td>
                                        <td align="left">
                                           <uc:BuscaOrcamento runat="server" ID="ucOrcamento" AutoCallBack="true" />                           
                                        </td>
                                    </tr>   
                                    <tr runat="server" id="trEquipamentoPS">
                                        <td class="msgErro" ></td>
                                        <td align="right"  class="label">
                                           Equipamento:
                                        </td>
                                        <td align="left">
                                           <Anthem:Label runat="server" ID="lblEquipamento" />                           
                                        </td>
                                    </tr>                                   
                                    <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Divisão/Departamento:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlCelula" />                                           
                                            &nbsp;
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCelula"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>                                                                        
                                    <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Aplicação:
                                        </td>
                                        <td align="left">
                                           <Anthem:TextBox runat="server" ID="txtAplicacao" TextMode="MultiLine" Rows="2" 
                                                 Columns="50" />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtAplicacao"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>	
                                      <tr id="tr_forn_001" runat="server">
                                        <td class="msgErro" >*</td>
                                        <td align="right" valign="top" >
                                           Fornecedor 1:
                                        </td>
                                        <td align="left">
                                           <uc:BuscaFornecedor runat="server" ID="ucFornecedor1" />                           
                                        </td>
                                    </tr>
                                    <tr id="tr_forn_002" runat="server">
                                        <td  ></td>
                                        <td align="right" valign="top">
                                           Fornecedor 2:
                                        </td>
                                        <td align="left">
                                          <uc:BuscaFornecedor runat="server" ID="ucFornecedor2" />                           
                                        </td>
                                    </tr>
                                    <tr id="tr_forn_003" runat="server">
                                        <td  ></td>
                                        <td align="right" valign="top">
                                           Fornecedor 3:
                                        </td>
                                        <td align="left">
                                          <uc:BuscaFornecedor runat="server" ID="ucFornecedor3" />                           
                                        </td>
                                    </tr>         
                                    <tr id="tr_forn_004" runat="server">
                                        <td  ></td>
                                        <td align="right" valign="top">
                                           Fornecedor 4:
                                        </td>
                                        <td align="left">
                                          <uc:BuscaFornecedor runat="server" ID="ucFornecedor4" />                           
                                        </td>
                                    </tr>         
                                    <tr>
                                        <td  ></td>
                                        <td align="right" >
                                           Observação:
                                        </td>
                                        <td align="left">
                                           <Anthem:TextBox runat="server" ID="txtObservacao" TextMode="MultiLine" Rows="3"  
                                            Columns="50" />                           
                                        </td>
                                    </tr>					                 
                                </table>
                            </td>
                        </tr>																			
                    </table>
                  </ComponentArt:PageView>
                  
                  
                   <ComponentArt:PageView CssClass="PageContent" runat="server" id="pvItem" >
                    <table border="0" cellpadding="2" cellspacing="2" width="750px" >
                        <tr>                            
                            <td colspan="3" align="center" valign="top">
                            <br />
                                <div align="left" style="vertical-align:text-bottom" class="PageTitle" >
                                    Itens
                                    <hr size="1" class="divisor" style="" />
                                </div>                                                                
                                <br />
                                <anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
                                     AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
                                    <HeaderStyle CssClass="dgHeader" />                                    
                                    <ItemStyle CssClass="dgItem" />
                                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Material/Serviço" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                 <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.CodigoInterno %> - 
                                                <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Especificação" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                 <%# ((PedidoObtencaoItem)Container.DataItem).Especificacao %> 
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <%--<asp:TemplateColumn HeaderText="Natureza Despesa" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                 <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.NaturezaDespesa != null ? ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.NaturezaDespesa.Codigo + " - " + ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.NaturezaDespesa.Descricao : ""%> 
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                                        <asp:TemplateColumn HeaderText="Sub-Natureza Despesa" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                 <%# ((PedidoObtencaoItem)Container.DataItem).SubNaturezaDespesa %> 
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <%# ((PedidoObtencaoItem)Container.DataItem).Quantidade.ToString("N2") %>
                                               (<%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Unidade.Descricao %>)
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                         <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                               <%# ((PedidoObtencaoItem)Container.DataItem).ValorTotal.ToString("N2") %>                                               
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>                                    

                                         <asp:TemplateColumn HeaderText="Valor 1" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                    <Anthem:NumericTextBox runat="server" ID="txtValorGrid1" Columns="14" MaxLength="12" DecimalPlaces="2" Text='<%# ((PedidoObtencaoItem)Container.DataItem).Valor.ToString("N2")  %>'
                                                        CssClass="numerico" />                                               
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Valor 2" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <Anthem:NumericTextBox runat="server" ID="txtValorGrid2" Columns="14" MaxLength="12" DecimalPlaces="2" Text='<%# ((PedidoObtencaoItem)Container.DataItem).ValorCotacao2.ToString("N2")  %>'
                                                        CssClass="numerico" />                                                                                              
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Valor 3" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                               <Anthem:NumericTextBox runat="server" ID="txtValorGrid3" Columns="14" MaxLength="12" DecimalPlaces="2" Text='<%# ((PedidoObtencaoItem)Container.DataItem).ValorCotacao3.ToString("N2")  %>'
                                                        CssClass="numerico" />  
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Valor 4" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                               <Anthem:NumericTextBox runat="server" ID="txtValorGrid4" Columns="14" MaxLength="12" DecimalPlaces="2" Text='<%# ((PedidoObtencaoItem)Container.DataItem).ValorCotacao4.ToString("N2")  %>'
                                                        CssClass="numerico" />  
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                       <%-- <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                               <%# ((PedidoObtencaoItem)Container.DataItem).ValorTotal.ToString("C2") %>
                                            </ItemTemplate>
                                            <FooterTemplate>                                                
                                               <b>Total: <%# _pedido.ValorTotal.ToString("C2") %></b>
                                            </FooterTemplate>                                            
                                        </asp:TemplateColumn>  --%>    
                                         <asp:TemplateColumn HeaderText="Saldo" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <uc:SaldoServicoMaterial runat="server" ID="ucSaldo" />
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>  
                                          <asp:TemplateColumn HeaderText="Saldo Licitação" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <asp:Label runat="server" ID="lblSaldoLicitacao" />
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>                                 
                                        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" 
                                                    CommandName="Edit" CausesValidation="false" /> 
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <Anthem:LinkButton runat="server" ID="btnExcluir" Text="Excluir" CommandName="Delete" />                                                
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </anthem:DataGrid>
                                <div style="text-align:right">
                                    <Anthem:Button runat="server" ID="btnAtualizarCotacao" TextDuringCallBack="Aguarde" Text="Salvar Valores" Width="140" EnabledDuringCallBack="false" CssClass="Button" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>                                          
                        <tr runat="server" id="trMaterial">
                            <td width="5%" class="msgErro" >*</td>
                            <td align="right" width="15%" valign="top">
                               Material/Serviço:
                            </td>
                            <td align="left">
                               <uc:BuscaMaterial runat="server" ID="ucBuscaMaterial" ValidationGroup="Item" ErrorMessage="Campo Obrigatório"
                                Required="true" AutoCallBack="true" />
                                <uc:SaldoServicoMaterial runat="server" ID="ucSaldoServicoMaterial" />
                            </td>
                        </tr>    
                         <tr>
                            <td class="msgErro" ></td>
                            <td align="right" >
                               Natureza Despesa:
                            </td>
                            <td align="left">
                               <Anthem:Label runat="server" ID="lblNaturezaDespesa" /> 
                            </td>
                        </tr>
                         <tr>
                            <td class="msgErro" ></td>
                            <td align="right" >
                               Sub-Natureza Despesa:
                            </td>
                            <td align="left">
                               <Anthem:Label runat="server" ID="lblSubNaturezaDespesa" /> 
                               <Anthem:DropDownList runat="server" ID="ddlSubNaturezaDespesa" />
                            </td>
                        </tr>
                         <tr runat="server" id="trDadosLicitacao">
                            <td class="msgErro" ></td>
                            <td align="right" >
                               Dados da Licitação:
                            </td>
                            <td align="left">
                               <div><Anthem:Label runat="server" ID="lblLicitacaoNumeroProcesso" Columns="40" MaxLength="500" /></div>
                               <div><Anthem:Label runat="server" ID="lblLicitacaoDataEmissao" Columns="40" MaxLength="500" /></div>
                               <div><Anthem:Label runat="server" ID="lblLicitacaoTipoLicitacao" Columns="40" MaxLength="500" /></div>
                               <div><Anthem:Label runat="server" ID="lblLicitacaoSistemaLicitatorio" Columns="40" MaxLength="500" /></div>
                               <div><Anthem:Label runat="server" ID="lblLicitacaoDataPregao" Columns="40" MaxLength="500" /></div>
                               <div><Anthem:Label runat="server" ID="lblLicitacaoStatus" Columns="40" MaxLength="500" /></div>
                            </td>
                        </tr>
                         <tr>
                            <td class="msgErro" ></td>
                            <td align="right" >
                               Especificação:
                            </td>
                            <td align="left">
                               <Anthem:TextBox runat="server" ID="txtEspecificacao" Columns="40" MaxLength="500" /> 
                            </td>
                        </tr>
                        <tr>
                            <td class="msgErro" ></td>
                            <td align="right" >
                               Estoque PEP:
                            </td>
                            <td align="left">
                               <Anthem:Label runat="server" ID="lblEstoquePEP" /> 
                            </td>
                        </tr>
                        <tr id="trOrigemMaterial" runat="server">
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Origem:
                            </td>
                            <td align="left">
                               <Anthem:DropDownList runat="server" ID="ddlOrigemMaterial" AutoCallBack="true" /> &nbsp;
                            </td>
                        </tr>
                        <%--<tr id="trRMC" runat="server">
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               RMC:
                            </td>
                            <td align="left">
                               <Anthem:TextBox runat="server" ID="txtRMC" Columns="40" /> 
                            </td>
                        </tr>--%>
                         <tr>
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Quantidade:
                            </td>
                            <td align="left">
                               <Anthem:NumericTextBox DecimalPlaces="2" runat="server" ID="txtQuantidade" Columns="14" MaxLength="12" /> &nbsp;                              
                                <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtQuantidade"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="Item"/>
                            </td>
                        </tr>                      
                         <tr>
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Ganhador:
                            </td>
                            <td align="left">
                               <Anthem:NumericTextBox runat="server" ID="txtValor1" Columns="14" MaxLength="12" DecimalPlaces="2"
                                    CssClass="numerico" /> &nbsp;                              
                                <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValor1"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="Item"/>
                            </td>
                        </tr> 
                        <tr id="tr_item_001" runat="server">
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Valor 2:
                            </td>
                            <td align="left">
                               <Anthem:NumericTextBox runat="server" ID="txtValor2" Columns="14" MaxLength="12" DecimalPlaces="2"
                                    CssClass="numerico" /> &nbsp;                              
                               
                            </td>
                        </tr> 
                        <tr id="tr_item_002" runat="server">
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Valor 3:
                            </td>
                            <td align="left">
                               <Anthem:NumericTextBox runat="server" ID="txtValor3" Columns="14" MaxLength="12" DecimalPlaces="2"
                                    CssClass="numerico" /> &nbsp;                                                              
                            </td>
                        </tr> 
                        <tr id="tr_item_003" runat="server">
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Valor 4:
                            </td>
                            <td align="left">
                               <Anthem:NumericTextBox runat="server" ID="txtValor4" Columns="14" MaxLength="12" DecimalPlaces="2"
                                    CssClass="numerico" /> &nbsp;                                                              
                            </td>
                        </tr> 

                        <tr id="tr1" runat="server">
                            <td class="msgErro" ></td>
                            <td align="right" >
                               Exec:
                            </td>
                            <td align="left">
                               <Anthem:CheckBox runat="server" ID="chkFlagExec" /> 
                            </td>
                        </tr>
                    </table> 
                </ComponentArt:PageView>  
                
                <ComponentArt:PageView CssClass="PageContent" runat="server" id="pvHistorico" >
                    <table border="0" cellpadding="2" cellspacing="2" width="750px" >
                        <tr>                            
                            <td colspan="3" align="center" valign="top">
                            <br />
                                <div align="left" style="vertical-align:text-bottom" class="PageTitle" >
                                    Histórico
                                    <hr size="1" class="divisor" style="" />
                                </div>                                                                
                                <br />
                                <uc:Historico runat="server" id="ucHistorico" />
                                  
                            </td>
                        </tr>            
                    </table> 
                </ComponentArt:PageView>  
                </ComponentArt:MultiPage>
            </td>
        </tr>
    </table>
    <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td width="40%" align="left">
            
            </td>
            <td align="right">
                <Anthem:Button runat="server" ID="btnSalvar" TextDuringCallBack="Aguarde" Text="Salvar"
                     EnabledDuringCallBack="false" CssClass="Button" ValidationGroup="DadosBasicos" />
                <Anthem:Button runat="server" ID="btnEnviar" TextDuringCallBack="Aguarde" Text="Enviar"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                 <Anthem:Button runat="server" ID="btnCopiar" Text="Copiar PO"
                     CssClass="Button" CausesValidation="false" Width="100px" />
                <Anthem:Button runat="server" ID="btnNovo" TextDuringCallBack="Aguarde" Text="Novo"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                <Anthem:Button runat="server" ID="btnImprimir" TextDuringCallBack="Aguarde" Text="Imprimir"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />                       
                <Anthem:Button runat="server" ID="btnImprimirItens" TextDuringCallBack="Aguarde" Width="140px" Text="Imprimir Itens"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                <Anthem:Button runat="server" ID="btnDocumentos" TextDuringCallBack="Aguarde" Text="Documentos" Width="115px"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                <Anthem:Button runat="server" ID="btnAtualizarValoresLicitacao" TextDuringCallBack="Aguarde" Width="190px" Text="Atualizar Valores Licitação"
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
