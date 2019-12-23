<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOMFItem.aspx.cs" Inherits="frmOMFItem" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/BuscaServicoMaterial.ascx" TagPrefix="uc" TagName="ServicoMaterial" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
      
      </script>
</head>
<body>
    <form id="form1" runat="server">       
    
    <Anthem:NumericTextBox  runat="server" ID="txtHidden" style="display:none"  DecimalPlaces="2"/> 
    <div align="center">
    <div align="right" style="width:98%" class="PageTitle">
    <br />
        Itens da OMF  
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="98%" >																		    
        <tr   >
            <td style="border:solid 1px black" valign="top">                
                <table border="0" cellpadding="2" cellspacing="2" width="100%" >
                     <tr>
                        <td width="5%" ></td>
                        <td align="right" width="20%" >
                           Numero Nota:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblNumeroNota" CssClass="legenda" />
                           &nbsp;
                           
                        </td>
                    </tr>  
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Fornecedor:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblFornecedor" CssClass="legenda" />                           
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
                                     <asp:TemplateColumn HeaderText="Código Material" ItemStyle-HorizontalAlign="left" >
                                        <ItemTemplate>
                                            <%# ((NotaEntregaMaterialOMFItem)Container.DataItem).Material.CodigoInterno %>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            
                                            Material:
                                            <Anthem:TextBox runat="server" ID="txtCodigoMaterial" AutoCallBack="true" Columns="10" OnTextChanged="txtCodigoMaterial_TextChanged" />
                                           <uc:ServicoMaterial runat="server" ID="ucServicoMaterial"  AutoCallBack="true" TipoServicoMaterial="Material" 
                                                OnSelectedValueChanged="ucServicoMaterial_SelectedValueChanged"  />
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            Material:
                                            <Anthem:TextBox runat="server" ID="txtCodigoMaterial" AutoCallBack="true" Columns="10" OnTextChanged="txtCodigoMaterial_TextChanged" />
                                           <uc:ServicoMaterial runat="server" ID="ucServicoMaterial"  AutoCallBack="true" TipoServicoMaterial="Material" 
                                                OnSelectedValueChanged="ucServicoMaterial_SelectedValueChanged"  />
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>                               
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((NotaEntregaMaterialOMFItem)Container.DataItem).Material.Descricao%>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn HeaderText="LOC" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((NotaEntregaMaterialOMFItem)Container.DataItem).LOC %>
                                        </ItemTemplate>                                        
                                        <FooterTemplate>
                                            <Anthem:TextBox  runat="server" ID="txtLOC" columns="18" MaxLength="20" />                                                
                                            <Anthem:RequiredFieldValidator  runat="server" ControlToValidate="txtLOC" 
                                                 Display="Dynamic" ValidationGroup="Item" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:TextBox  runat="server" ID="txtLOC" columns="18" MaxLength="20"  
                                                text='<%# ((NotaEntregaMaterialOMFItem)Container.DataItem).Quantidade %>'/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLOC" 
                                                 Display="Dynamic" ValidationGroup="ItemEdit" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn> 
                                    
                                    <asp:TemplateColumn HeaderText="Tipo" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# Shared.Common.Util.GetDescription(((NotaEntregaMaterialOMFItem)Container.DataItem).TipoTAV) %>
                                        </ItemTemplate>                                        
                                        <FooterTemplate>
                                            <Anthem:DropDownList  runat="server" ID="ddlTipoTAV" />  
                                            <Anthem:RequiredFieldValidator runat="server" ControlToValidate="ddlTipoTAV" InitialValue="0" 
                                                 Display="Dynamic" ValidationGroup="Item" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:DropDownList  runat="server" ID="ddlTipoTAV" />  
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTipoTAV" InitialValue="0" 
                                                 Display="Dynamic" ValidationGroup="Item" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn> 
                                    
                                    <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((NotaEntregaMaterialOMFItem)Container.DataItem).Quantidade %>
                                            &nbsp;
                                            (<%# ((NotaEntregaMaterialOMFItem)Container.DataItem).Material.Unidade.Descricao %>)
                                        </ItemTemplate>                                        
                                        <FooterTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtQuantidade" columns="5" MaxLength="8" DecimalPlaces="2"/>                                                
                                            <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtQuantidade" 
                                                 Display="Dynamic" ValidationGroup="Item" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtQuantidade" columns="5" MaxLength="8" DecimalPlaces="2"
                                                text='<%# ((NotaEntregaMaterialOMFItem)Container.DataItem).Quantidade %>'/>                                                
                                            <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtQuantidade" 
                                                 Display="Dynamic" ValidationGroup="ItemEdit" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <%# ((NotaEntregaMaterialOMFItem)Container.DataItem).Valor.ToString("N2")%>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtValor" columns="8" MaxLength="14" 
                                                CssClass="numerico" DecimalPlaces="2"/>                                                
                                            <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtValor" 
                                                 Display="Dynamic" ValidationGroup="Item" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtValor" columns="8" MaxLength="14" 
                                                CssClass="numerico" DecimalPlaces="2" Text='<%# ((NotaEntregaMaterialOMFItem)Container.DataItem).Valor.ToString("N2") %>'/>                                                
                                            <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtValor"  
                                                 Display="Dynamic" ValidationGroup="ItemEdit" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Total" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <%# ((NotaEntregaMaterialOMFItem)Container.DataItem).ValorTotal.ToString("N2")%>
                                        </ItemTemplate>                                                                               
                                    </asp:TemplateColumn>  
                                                                                                                                       
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" 
                                                    CommandName="Edit" CausesValidation="false" />
                                            &nbsp;
                                             <a href="#" onclick='javascript:Excluir(<%# ((NotaEntregaMaterialOMFItem)Container.DataItem).ID %>)' >
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
                                            <Anthem:Label runat="server" ID="lblValorTotal"  Font-Bold="true" />
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
