<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoCotacaoCadastro.aspx.cs" Inherits="frmPedidoCotacaoCadastro" %>
<%@ Import namespace="Shared.NHibernateDAL"%>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/UserControls/BuscaServicoMaterial.ascx" TagPrefix="uc" TagName="BuscaMaterial" %>
<%@ Register Src="~/UserControls/BuscaFornecedorCompra.ascx" TagPrefix="uc" TagName="BuscaFornecedor" %>
<%@ Register Src="ucHistoricoPedidoObtencao.ascx" TagPrefix="uc" TagName="Historico" %>
<%@ Register Src="~/pedidoObtencao/DadosComplementaresItemServicoCotacao.ascx" TagPrefix="uc" TagName="DadosComplementares" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/pedidoObtencao/ucJustificativa.ascx" TagName="Justificativa" TagPrefix="uc" %>
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
   
    </script> 
</head>
<body >
    <form id="form1" runat="server">
    <uc:Justificativa runat="server" ID="ucComentario" />
    <Anthem:ValidationSummary runat="server" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="true" />
    <uc:DadosComplementares runat="server" ID="ucDadosComplementares" />
    <div align="center">
    <div align="right" style="width:90%" Class="PageTitle">
    <br />
        Cadastro de Pedido de Cotação
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
                                        <td width="5%" class="msgErro" ></td>
                                        <td align="right" width="25%" >
                                           Número:
                                        </td>
                                        <td align="left">
                                           <Anthem:Label runat="server" ID="lblNumero" CssClass="legenda"  />
                                        </td>
                                    </tr>                                   
                                    <tr>
                                        <td class="msgErro" ></td>
                                        <td align="right" >
                                           Data Emissão:
                                        </td>
                                        <td align="left">
                                           <Anthem:Label runat="server" ID="lblData" CssClass="legenda" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="msgErro" ></td>
                                        <td align="right" >
                                           Comprador:
                                        </td>
                                        <td align="left">
                                           <Anthem:Label runat="server" ID="lblComprador" />                                           
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trRecusado" visible="false">
                                        <td class="msgErro" ></td>
                                        <td align="right" >
                                           Status:
                                        </td>
                                        <td align="left" style="color:Red;font-weight:bold;">                                            
                                           RECUSADO
                                        </td>
                                    </tr>
                                     <tr runat="server" id="trJustificativa" visible="false">
                                        <td class="msgErro" ></td>
                                        <td align="right" >
                                           Justificativa:
                                        </td>
                                        <td align="left" >
                                           <Anthem:Label ForeColor="red" runat="server" ID="lblJustificativa" />                                           
                                        </td>
                                    </tr>
                                    <tr >
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Tipo Compra:
                                        </td>
                                        <td align="left" >
                                           <Anthem:DropDownList runat="server" ID="ddlTipoCompra" AutoCallBack="true" OnSelectedIndexChanged="ddlTipoCompra_SelectedIndexChanged" />                                           
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="ddlTipoCompra" ErrorMessage="Campo obrigatório" Display="Dynamic"
                                                InitialValue="0" />                                             
                                        </td>
                                    </tr>
                                    <tr >
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Natureza Despesa:
                                        </td>
                                        <td align="left" >
                                           <Anthem:DropDownList runat="server" ID="ddlNaturezaDespesa" />                                           
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="ddlNaturezaDespesa" ErrorMessage="Campo obrigatório" Display="Dynamic"
                                                InitialValue="0" />                                             
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  ></td>
                                        <td align="right" valign="top" >
                                           Fornecedor 1:
                                        </td>
                                        <td align="left">
                                           <uc:BuscaFornecedor runat="server" ID="ucFornecedor1" />                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  ></td>
                                        <td align="right" valign="top">
                                           Fornecedor 2:
                                        </td>
                                        <td align="left">
                                          <uc:BuscaFornecedor runat="server" ID="ucFornecedor2" />                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  ></td>
                                        <td align="right" valign="top">
                                           Fornecedor 3:
                                        </td>
                                        <td align="left">
                                          <uc:BuscaFornecedor runat="server" ID="ucFornecedor3" />                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  ></td>
                                        <td align="right" valign="top">
                                           Fornecedor 4:
                                        </td>
                                        <td align="left">
                                          <uc:BuscaFornecedor runat="server" ID="ucFornecedor4" />                           
                                        </td>
                                    </tr>
                                                                        					             
                                    <tr>
                                        <td  class="msgErro">*</td>
                                        <td align="right" valign="top">
                                           Observação:
                                        </td>
                                        <td align="left">
                                           <Anthem:TextBox runat="server" ID="txtObservacao" TextMode="MultiLine" Rows="3"  
                                            Columns="50" /> &nbsp;
                                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtObservacao" ErrorMessage="Campo obrigatório" Display="Dynamic" />                                
                                        </td>
                                    </tr>					                 
                                </table>
                            </td>
                        </tr>																			
                    </table>
                  </ComponentArt:PageView>
                  
                  
                   <ComponentArt:PageView CssClass="PageContent" runat="server" id="pvItem" >
                    <table border="0" cellpadding="2" cellspacing="2" width="780px" >
                        <tr>                            
                            <td colspan="3" align="center" valign="top">
                            <br />
                                <div align="left" style="vertical-align:text-bottom" class="PageTitle" >
                                    Itens
                                    <hr size="1" class="divisor" style="" />
                                </div>                                                                
                                <br />                               
                                <b>Limites</b>  
                                <Anthem:Panel runat="server" ID="pnLimites">
                                <table cellpadding="3" border="1" rules="all"  width="400px">
                                    <tr style="background-color:#EEEEEE; color:#17485C;">
                                        <td>
                                
                                        </td>
                                        <td align="center">
                                            <b>Fornecedor</b>
                                        </td>
                                        <td align="center">
                                            <b>Saldo Real</b>
                                        </td>                                        
                                        <td align="center">
                                            <b>A Realizar</b>
                                        </td>                                        
                                        <td align="center">
                                            <b>Saldo Total</b>
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            1
                                        </td>
                                        <td align="center">
                                            <Anthem:Label runat="server" ID="lblFornecedorLimite1" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Label runat="server" ID="lblSaldoReal1" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Label runat="server" ID="lblARealizar1" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Label runat="server" ID="lblSaldoTotal1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            2
                                        </td>
                                        <td align="center">
                                            <Anthem:Label runat="server" ID="lblFornecedorLimite2" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Label runat="server" ID="lblSaldoReal2" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Label runat="server" ID="lblARealizar2" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Label runat="server" ID="lblSaldoTotal2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            3
                                        </td>
                                        <td align="center">
                                            <Anthem:Label runat="server" ID="lblFornecedorLimite3" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Label runat="server" ID="lblSaldoReal3" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Label runat="server" ID="lblARealizar3" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Label runat="server" ID="lblSaldoTotal3" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            4
                                        </td>
                                        <td align="center">
                                            <Anthem:Label runat="server" ID="lblFornecedorLimite4" />
                                        </td>
                                         <td align="right">
                                            <Anthem:Label runat="server" ID="lblSaldoReal4" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Label runat="server" ID="lblARealizar4" />
                                        </td>
                                        <td align="right">
                                            <Anthem:Label runat="server" ID="lblSaldoTotal4" />
                                        </td>
                                    </tr>
                                </table>
                                <Anthem:Button runat="server" ID="btnAtualizarSaldo" TextDuringCallBack="Aguarde" Text="Atualizar Saldos"
                                    EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" Width="150" />   
                            
                                </Anthem:Panel>
                                <br />
                                <anthem:DataGrid runat="server" ID="dgItem" Width="720px" CssClass="datagrid"
                                     AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
                                    <HeaderStyle CssClass="dgHeader" />                                    
                                    <ItemStyle CssClass="dgItem" />
                                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                                                &nbsp;-&nbsp;
                                                <Anthem:LinkButton runat="server" ID="btnEspecificacao" Text='<%# ((PedidoCotacaoItem)Container.DataItem).Especificacao ?? "Inserir Especificação" %>' 
                                                    OnClick="btnEspecificacao_Click" CausesValidation="false"/>
                                                <Anthem:Panel runat="server" ID="pnEspecificacao" style="display:none" >
                                                    <Anthem:TextBox runat="server" ID="txtEspecificacao" Text="<%# ((PedidoCotacaoItem)Container.DataItem).Especificacao %>" />
                                                    &nbsp;<Anthem:LinkButton runat="server" ID="btnSalvarEspecificacao" Text="Salvar" OnClick="btnSalvarEspecificacao_Click" CausesValidation="false"  />
                                                </Anthem:Panel>
                                                <br />
                                                (<Anthem:LinkButton runat="server" ID="lnkUltimasCotacoes" Text="Ver Últimas Compras" />)<br />
                                                <Anthem:LinkButton runat="server" ID="lnkDadosComplementares" Text="Dados Complementares" 
                                                    CausesValidation="false" CommandName="DadosComplementares"/><br />
                                                    <Anthem:LinkButton runat="server" ID="lnkDetalhesPO" Text="Detalhes PO" 
                                                    CausesValidation="false" CommandName="AbrirPO" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <%# ((PedidoCotacaoItem)Container.DataItem).Quantidade.ToString("N2") %>
                                               (<%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.Unidade.Descricao %>)
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                         <asp:TemplateColumn HeaderText="Cotação" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <table cellpadding="3" border="1" rules="all"  width="400px">
                                                    <tr style="background-color:#EEEEEE; color:#17485C;">
                                                        <td>
                                                            
                                                        </td>
                                                        <td align="center">
                                                            <b>Fornecedor</b>
                                                        </td>
                                                        <td align="center">
                                                            <b>Valor</b>
                                                        </td>
                                                        <td align="center">
                                                            <b>Vencedor</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            1
                                                        </td>
                                                        <td align="center">
                                                            <Anthem:Label runat="server" ID="lblFornecedor1" Text='<%# _cotacao.FornecedorCotacao1 %>' />
                                                        </td>
                                                        <td align="center">
                                                            <Anthem:NumericTextBox runat="server" ID="txtValor1" DecimalPlaces="2" Columns="10" CssClass="numerico" 
                                                                Text='<%# ObjectReader.ReadDecimal(((PedidoCotacaoItem)Container.DataItem).ValorCotacao1) %>' />
                                                        </td>
                                                        <td align="center">
                                                            <Anthem:RadioButton runat="server" ID="rbValor1" GroupName="rbVencedor" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                          2
                                                        </td>
                                                        <td align="center">
                                                            <Anthem:Label runat="server" ID="lblFornecedor2" Text='<%# _cotacao.FornecedorCotacao2 %>'/>
                                                        </td>
                                                        <td align="center">
                                                            <Anthem:NumericTextBox runat="server" ID="txtValor2" DecimalPlaces="2" Columns="10" CssClass="numerico" 
                                                                Text='<%# ObjectReader.ReadDecimal(((PedidoCotacaoItem)Container.DataItem).ValorCotacao2) %>'/>
                                                        </td>
                                                         <td align="center">
                                                            <Anthem:RadioButton runat="server" ID="rbValor2" GroupName="rbVencedor" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                           3
                                                        </td>
                                                        <td align="center">
                                                            <Anthem:Label runat="server" ID="lblFornecedor3" Text='<%# _cotacao.FornecedorCotacao3 %>'/>
                                                        </td>
                                                        <td align="center">
                                                            <Anthem:NumericTextBox runat="server" ID="txtValor3" DecimalPlaces="2" Columns="10" CssClass="numerico" 
                                                                Text='<%# ObjectReader.ReadDecimal(((PedidoCotacaoItem)Container.DataItem).ValorCotacao3) %>' />
                                                        </td>
                                                         <td align="center">
                                                            <Anthem:RadioButton runat="server" ID="rbValor3" GroupName="rbVencedor" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            4
                                                        </td>
                                                        <td align="center">
                                                            <Anthem:Label runat="server" ID="lblFornecedor4" Text='<%# _cotacao.FornecedorCotacao4 %>'/>
                                                        </td>
                                                        <td align="center">
                                                            <Anthem:NumericTextBox runat="server" ID="txtValor4" DecimalPlaces="2" Columns="10" CssClass="numerico" 
                                                                Text='<%# ObjectReader.ReadDecimal(((PedidoCotacaoItem)Container.DataItem).ValorCotacao4) %>' />
                                                        </td>
                                                         <td align="center">
                                                            <Anthem:RadioButton runat="server" ID="rbValor4" GroupName="rbVencedor" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>                                        
                                        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <Anthem:LinkButton runat="server" ID="btnExcluir" Text="Excluir" CommandName="Delete" CausesValidation="false" />                                                
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </anthem:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="right">
                                <Anthem:Button runat="server" ID="btnMarcarMenorPreco" TextDuringCallBack="Aguarde" Text="Selecionar Menores Preços"
                                    EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" Width="270" />   
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
                     EnabledDuringCallBack="false" CssClass="Button" />
                <Anthem:Button runat="server" ID="btnEnviar" TextDuringCallBack="Aguarde" Text="Criar AC"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />               
                <Anthem:Button runat="server" ID="btnImprimir" TextDuringCallBack="Aguarde" Text="Ficha"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />       
                <Anthem:Button runat="server" ID="btnImprimirParaFornecedor" TextDuringCallBack="Aguarde" Text="Imprimir"
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
