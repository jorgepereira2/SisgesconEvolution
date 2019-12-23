<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLicitacaoCadastro.aspx.cs" Inherits="frmLicitacaoCadastro" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/UserControls/BuscaServicoMaterial.ascx" TagPrefix="uc" TagName="BuscaMaterial" %>
<%@ Register Src="~/UserControls/BuscaPedidoServico.ascx" TagPrefix="uc" TagName="BuscaPedidoServico" %>
<%@ Register TagPrefix="uc" TagName="BuscaFornecedor" Src="~/UserControls/BuscaFornecedor.ascx" %>
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
    }  
    
     function LicitacaoSelecionada(id_licitacao)
    {
        parent.iframeresize();
        Anthem_InvokePageMethod('LicitacaoSelecionada', [id_licitacao], function(result){});
    }   
   
    </script> 
</head>
<body >
    <form id="form1" runat="server">
    <Anthem:ValidationSummary runat="server" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="true" />
    <div align="center">
    <div align="right" style="width:90%" Class="PageTitle">
    <br />
        Cadastro de Licitações
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
                    <ComponentArt:TabStripTab Text="Contratos" runat="server" ID="tabContratos" ClientSideCommand="AbaAlterada();" PageViewId="pvContrato" />                    
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
                                    Licitacao
                                    <hr size="1" class="divisor" />
                                </div>
                                <br />
                                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                                    <tr>
                                        <td width="10%" class="msgErro" >*</td>
                                        <td align="right" width="20%" >
                                           Data Emissão:
                                        </td>
                                        <td align="left">
                                           <Anthem:DateTextBox runat="server" ID="txtDataEmissao"  />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtDataEmissao"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Número CI:
                                        </td>
                                        <td align="left">
                                           <Anthem:TextBox runat="server" ID="txtNumeroCI" MaxLength="70" Columns="50" />    
                                            &nbsp;
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtNumeroCI"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="DadosBasicos"/>                       
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  ></td>
                                        <td align="right" valign="top" >
                                           PS:
                                        </td>
                                        <td align="left">
                                          <uc:BuscaPedidoServico runat="server" ID="ucPedidoServico" AutoCallBack="true" />
                                          <br />
                                          <Anthem:Button runat="server" ID="btnAdicionarPS" Text="Adicionar PS" EnabledDuringCallBack="false" CssClass="Button" Width="120" />                                                  
                                            <br />
                                          <Anthem:ListBox runat="server" ID="lstPS" Width="160" />
                                          <br />
                                          <Anthem:Button runat="server" ID="btnRemoverPS" Text="Remover PS" EnabledDuringCallBack="false" CssClass="Button" Width="120" />                                                  
                                        </td>
                                    </tr>		

                                    <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Objeto:
                                        </td>
                                        <td align="left">
                                           <Anthem:TextBox runat="server" ID="txtObjetivo" TextMode="MultiLine" Rows="2" 
                                                 Columns="50" />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtObjetivo"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Tipo Licitação:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlTipoLicitacao" />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlTipoLicitacao" InitialValue="0"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Sistema Licitatório:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlSistemaLicitatorio" />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSistemaLicitatorio" InitialValue="0"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Processo Licitatório:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlProcessoLicitatorio" />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlProcessoLicitatorio" InitialValue="0"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Modalidade Licitação:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlModalidadePregao" />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlModalidadePregao" InitialValue="0"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>
					                 <tr>
                                        <td  ></td>
                                        <td align="right" >
                                           Número Processo:
                                        </td>
                                        <td align="left">
                                           <Anthem:TextBox runat="server" ID="txtNumeroPregao" MaxLength="20" Columns="50" />                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  ></td>
                                        <td align="right" >
                                           Data Prevista Pregão:
                                        </td>
                                        <td align="left">
                                           <Anthem:DateTextBox runat="server" ID="txtDataPregao" />                           
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="msgErro" ></td>
                                        <td align="right" >
                                           Servidor Fiscal Contrato:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlServidorFiscalContrato" />
                                           &nbsp;
                                          <%-- <Anthem:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlServidorFiscalContrato" InitialValue="0"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="DadosBasicos"/>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  ></td>
                                        <td align="right" >
                                           Valor Total Estimado:
                                        </td>
                                        <td align="left">
                                           <Anthem:Label runat="server" ID="lblValorTotalEstimado" />                           
                                        </td>
                                    </tr>
                                   <%-- <tr>
                                        <td  ></td>
                                        <td align="right" >
                                           Número Contrato Ata:
                                        </td>
                                        <td align="left">
                                           <Anthem:TextBox runat="server" ID="txtNumeroContratoAta" Columns="40" MaxLength="40" />                           
                                        </td>
                                    </tr>	--%>	
                                     <tr>
                                        <td  ></td>
                                        <td align="right" >
                                           NUP:
                                        </td>
                                        <td align="left">
                                           <Anthem:TextBox runat="server" ID="txtNUP" Columns="20" MaxLength="20" />                           
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
                                    <FooterStyle CssClass="dgFooter" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Siasg" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                 <%# ((LicitacaoItem)Container.DataItem).Material.CodigoSiasg %> 
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Material/Serviço" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                 <%# ((LicitacaoItem)Container.DataItem).Material.CodigoInterno %> - 
                                                <%# ((LicitacaoItem)Container.DataItem).Material.Descricao %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <%# ((LicitacaoItem)Container.DataItem).Quantidade %>
                                               &nbsp;
                                               <%# ((LicitacaoItem)Container.DataItem).Material.Unidade != null ? ((LicitacaoItem)Container.DataItem).Material.Unidade.Descricao : "" %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                         <asp:TemplateColumn HeaderText="Valor 1" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                               <%# ((LicitacaoItem)Container.DataItem).Valor1.ToString("C2") %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Valor 2" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                               <%# ((LicitacaoItem)Container.DataItem).Valor2.ToString("C2") %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Valor 3" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                               <%# ((LicitacaoItem)Container.DataItem).Valor3.ToString("C2") %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Valor 4" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                               <%# ((LicitacaoItem)Container.DataItem).Valor4.ToString("C2") %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Valor 5" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                               <%# ((LicitacaoItem)Container.DataItem).Valor5.ToString("C2") %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                         <asp:TemplateColumn HeaderText="Valor Médio" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                               <%# ((LicitacaoItem)Container.DataItem).ValorMedio.ToString("C2") %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Valor Médio Total" ItemStyle-HorizontalAlign="right" FooterStyle-Wrap="false">
                                            <ItemTemplate>
                                               <%# ((LicitacaoItem)Container.DataItem).ValorMedioTotal.ToString("C2") %>
                                            </ItemTemplate>                                            
                                            <FooterTemplate>
                                               <b><%# GetTotal().ToString("C2") %></b>
                                            </FooterTemplate>                                            
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
                                </anthem:datagrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>                      
                        <tr>
                            <td width="5%" class="msgErro" >*</td>
                            <td align="right" width="15%">
                               Material/Serviço:
                            </td>
                            <td align="left">
                               <uc:BuscaMaterial runat="server" ID="ucBuscaMaterial" ValidationGroup="Item" ErrorMessage="Campo Obrigatório"
                                Required="false" SearchURL="../comum/frmServicoMaterialBusca.aspx?controle=ucBuscaMaterial" AutoCallBack="true"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="msgErro" ></td>
                            <td align="right" >
                               Código Siasg:
                            </td>
                            <td align="left">
                               <Anthem:TextBox runat="server" ID="txtCodigoSiasg" MaxLength="20" />                                
                            </td>
                        </tr> 
                         <tr>
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Quantidade:
                            </td>
                            <td align="left">
                               <Anthem:NumericTextBox runat="server" ID="txtQuantidade" Columns="14" MaxLength="12" DecimalPlaces="0" /> &nbsp;                              
                                <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtQuantidade"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="Item"/>
                            </td>
                        </tr> 
                         <tr>
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Valor 1:
                            </td>
                            <td align="left">
                               <Anthem:NumericTextBox runat="server" ID="txtValor1" Columns="14" MaxLength="12" DecimalPlaces="2"
                                    CssClass="numerico" /> &nbsp;                              
                                <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValor1"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="Item"/>
                            </td>
                        </tr> 
                        <tr>
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Valor 2:
                            </td>
                            <td align="left">
                               <Anthem:NumericTextBox runat="server" ID="txtValor2" Columns="14" MaxLength="12" DecimalPlaces="2" 
                                    CssClass="numerico"/> &nbsp;                              
                                <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtValor2"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="Item"/>
                            </td>
                        </tr> 
                        <tr>
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Valor 3:
                            </td>
                            <td align="left">
                               <Anthem:NumericTextBox runat="server" ID="txtValor3" Columns="14" MaxLength="12" DecimalPlaces="2" 
                                    CssClass="numerico"/> &nbsp;                              
                                <Anthem:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtValor3"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="Item"/>
                            </td>
                        </tr>     
                         <tr>
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Valor 4:
                            </td>
                            <td align="left">
                               <Anthem:NumericTextBox runat="server" ID="txtValor4" Columns="14" MaxLength="12" DecimalPlaces="2" 
                                    CssClass="numerico"/> &nbsp;                              
                                <Anthem:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtValor4"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="Item"/>
                            </td>
                        </tr>     
                         <tr>
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Valor 5:
                            </td>
                            <td align="left">
                               <Anthem:NumericTextBox runat="server" ID="txtValor5" Columns="14" MaxLength="12" DecimalPlaces="2" 
                                    CssClass="numerico"/> &nbsp;                              
                                <Anthem:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtValor5"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="Item"/>
                            </td>
                        </tr>                       
                        <tr>
                           <td ></td>
                            <td align="right" >
                               Tipo Cálculo:
                            </td>
                            <td align="left">
                               <Anthem:DropDownList runat="server" ID="ddlTipoCalculo" />
                               &nbsp;
                              <Anthem:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTipoCalculo"
                                    ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" ValidationGroup="Item"/>
                            </td>
                        </tr>    
                        <tr>
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Observação:
                            </td>
                            <td align="left">
                               <Anthem:TextBox runat="server" ID="txtObservacaoItem" Columns="50" Rows="3" TextMode="MultiLine" />                                 
                            </td>
                        </tr> 
                    </table> 
                </ComponentArt:PageView>   


                   <ComponentArt:PageView CssClass="PageContent" runat="server" id="pvContrato" >
                    <table border="0" cellpadding="2" cellspacing="2" width="750px" >
                        <tr>                            
                            <td colspan="3" align="center" valign="top">
                            <br />
                                <div align="left" style="vertical-align:text-bottom" class="PageTitle" >
                                    Contratos
                                    <hr size="1" class="divisor" style="" />
                                </div>                                                                
                                <br />
                                <anthem:DataGrid runat="server" ID="dgContrato" Width="98%" CssClass="datagrid"
                                     AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
                                    <HeaderStyle CssClass="dgHeader" />                                    
                                    <ItemStyle CssClass="dgItem" />
                                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                    <FooterStyle CssClass="dgFooter" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Fornecedor" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                 <%# ((LicitacaoContrato)Container.DataItem).Fornecedor.RazaoSocial %> 
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>                                       
                                        <asp:TemplateColumn HeaderText="Número" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <%# ((LicitacaoContrato)Container.DataItem).NumeroContrato %>                                               
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                         <asp:TemplateColumn HeaderText="Vigência" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <%# ((LicitacaoContrato)Container.DataItem).DataVigencia.ToShortDateString() %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Data Assinatura" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                               <%# ((LicitacaoContrato)Container.DataItem).DataAssinatura.ToShortDateString() %>
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
                                </anthem:datagrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>                      
                        <tr>
                            <td width="5%" class="msgErro" >*</td>
                            <td align="right" width="15%">
                               Fornecedor:
                            </td>
                            <td align="left">
                               <uc:BuscaFornecedor runat="server" ID="ucFornecedorContrato" ValidationGroup="Contrato" ErrorMessage="Campo Obrigatório"
                                Required="true"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="msgErro" ></td>
                            <td align="right" >
                               Número Contrato:
                            </td>
                            <td align="left">
                               <Anthem:TextBox runat="server" ID="txtNumeroContrato" MaxLength="20" />                                
                            </td>
                        </tr> 
                         <tr>
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Data Vigência:
                            </td>
                            <td align="left">
                               <Anthem:DateTextBox runat="server" ID="txtDataVigencia"  /> &nbsp;                              
                                <Anthem:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDataVigencia"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="Contrato"/>
                            </td>
                        </tr> 
                        <tr>
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Data Assinatura:
                            </td>
                            <td align="left">
                               <Anthem:DateTextBox runat="server" ID="txtDataAssinatura"  /> &nbsp;                              
                                <Anthem:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtDataAssinatura"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="Contrato"/>
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
                    <Anthem:Button runat="server" ID="btnenviar" TextDuringCallBack="Aguarde" Text="Enviar"
                     EnabledDuringCallBack="false" CssClass="Button" />
                <Anthem:Button runat="server" ID="btnNovo" TextDuringCallBack="Aguarde" Text="Novo"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                <Anthem:Button runat="server" ID="btnImprimir" TextDuringCallBack="Aguarde" Text="Imprimir"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />       
                <Anthem:Button runat="server" ID="btnCopiar" Text="Copiar Licitação"
                     CssClass="Button" CausesValidation="false" Width="120px" />
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
