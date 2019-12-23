<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmRequisicaoEstoqueCadastro.aspx.cs" Inherits="frmRequisicaoEstoqueCadastro" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/UserControls/BuscaServicoMaterial.ascx" TagPrefix="uc" TagName="BuscaMaterial" %>
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
   
    </script> 
</head>
<body >
    <form id="form1" runat="server">
    <Anthem:ValidationSummary runat="server" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="true" />
    <div align="center">
    <div align="right" style="width:90%" Class="PageTitle">
    <br />
        Cadastro de Entrada/Saída de Estoque
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
                                    Entrada/Saída Estoque
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
                                           <Anthem:DateTextBox runat="server" ID="txtDataEmissao"  />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtDataEmissao"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Responsável:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlResponsavel" />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="ddlResponsavel" InitialValue="0"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="DadosBasicos"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Tipo Requisição:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlTipoRequisicao" AutoCallBack="true" OnSelectedIndexChanged="ddlTipoRequisicao_SelectedIndexChanged" />
                                           &nbsp;
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlTipoRequisicao" 
                                                InitialValue="0" ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="DadosBasicos"/>
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
                                     AutoGenerateColumns="false" CellPadding="3" >
                                    <HeaderStyle CssClass="dgHeader" />                                    
                                    <ItemStyle CssClass="dgItem" />
                                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                    <FooterStyle CssClass="dgFooter" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Codigo" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <%# ((RequisicaoEstoqueItem)Container.DataItem).Material.CodigoInterno %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <%# ((RequisicaoEstoqueItem)Container.DataItem).Material.Descricao %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <%# ((RequisicaoEstoqueItem)Container.DataItem).Quantidade.ToString("N2") %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>                                         
                                        <asp:TemplateColumn HeaderText="Origem" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <%# Shared.Common.Util.GetDescription(((RequisicaoEstoqueItem)Container.DataItem).OrigemMaterial) %>
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
                            <td width="5%" class="msgErro" ></td>
                            <td align="right" width="15%">
                              Código Material:
                            </td>
                            <td align="left">
                               <Anthem:TextBox runat="server" AutoCallBack="true" ID="txtCodigoMaterial" OnTextChanged="txtCodigoMaterial_TextChanged" />
                            </td>
                        </tr>                      
                        <tr>
                            <td width="5%" class="msgErro" >*</td>
                            <td align="right" width="15%">
                               Material:
                            </td>
                            <td align="left">
                               <uc:BuscaMaterial runat="server" ID="ucBuscaMaterial" TipoServicoMaterial="Material" ValidationGroup="Item" ErrorMessage="Campo Obrigatório"
                                Required="true" />
                            </td>
                        </tr>
                         <tr>
                            <td class="msgErro" >*</td>
                            <td align="right" >
                               Quantidade:
                            </td>
                            <td align="left">
                               <Anthem:NumericTextBox runat="server" ID="txtQuantidade" Columns="14" MaxLength="12" DecimalPlaces="2" /> &nbsp;                              
                                <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtQuantidade"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" ValidationGroup="Item"/>
                            </td>
                        </tr>                                        
                        <tr>
                           <td class="msgErro" >*</td>
                            <td align="right" >
                               Origem Material:
                            </td>
                            <td align="left">
                               <Anthem:DropDownList runat="server" ID="ddlOrigemMaterial" />
                               &nbsp;
                              <Anthem:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlOrigemMaterial"
                                    ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" ValidationGroup="Item"/>
                            </td>
                        </tr>  
                        <tr runat="server" id="trDestinoMaterial" style="display:none;">
                           <td class="msgErro"  >*</td>
                            <td align="right" >
                               Transferir Para:
                            </td>
                            <td align="left">
                               <Anthem:DropDownList runat="server" ID="ddlOrigemMaterialDestino" />
                               
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
                <Anthem:Button runat="server" ID="btnEnviar" TextDuringCallBack="Aguarde" Text="Enviar"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                <Anthem:Button runat="server" ID="btnNovo" TextDuringCallBack="Aguarde" Text="Novo"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                <Anthem:Button runat="server" ID="btnImprimir" TextDuringCallBack="Aguarde" Text="Imprimir"
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
