<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmVerificacaoPaiol.aspx.cs" Inherits="frmVerificacaoPaiol" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="CancelarItem.ascx" TagName="CancelarItem" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/BuscaFornecedor.ascx" TagName="BuscaFornecedor" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />      
</head>
<body>
    <form id="form1" runat="server">   
    <div style="display:none">
    <uc:BuscaFornecedor runat="server" ID="ucAux" ShowNovo="false"  />    
    </div>
    <uc:CancelarItem runat="server" ID="ucCancelarItem" />   
    <div align="center">
    <div align="right" style="width:98%" class="PageTitle">
    <br />
        Pedido de Material   
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="98%" >																		    
        <tr   >
            <td style="border:solid 1px black" valign="top">                
                <table border="0" cellpadding="2" cellspacing="2" width="100%" >                       
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
                           PS:
                        </td>
                        <td align="left">
                           <Anthem:LinkButton runat="server" ID="lnkPS" />                           
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
                            <Anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="ID" Visible="false" >                                        
                                        <FooterTemplate>
                                            <Anthem:Label runat="server" ID="lblID_Material" />
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblID_Material" Text="<%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.ID %>" />
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="ID" Visible="false" >                                        
                                        <FooterTemplate>
                                            <Anthem:Label runat="server" ID="lblID" />
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />                       
                                    </asp:TemplateColumn>                                        
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:Label runat="server" ID="lblServicoMaterial" />
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />                       
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Especificação" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <Anthem:TextBox runat="server" ID="txtEspecificacao" Text='<%# ((PedidoObtencaoItem)Container.DataItem).Especificacao %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                           
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />                       
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItem)Container.DataItem).Quantidade %> 
                                            <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Unidade.Descricao %>
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>  
                                    <asp:TemplateColumn HeaderText="Fornecedor" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkFornecedor" ToolTip="Alterar" CommandName="DefinirFornecedor" CausesValidation="false"
                                                Text='<%# ((PedidoObtencaoItem)Container.DataItem).Fornecedor == null ? "Definir Fornecedor" : ((PedidoObtencaoItem)Container.DataItem).Fornecedor.ToString() %>' />
                                            <div runat="server" id="divFornecedor" style="display:none;">
                                            <uc:BuscaFornecedor runat="server" ID="ucBuscaFornecedor" ShowNovo="false"  />&nbsp;
                                            <Anthem:LinkButton runat="server" ID="btnSalvarFornecedor" Text="Salvar" CausesValidation="false" CommandName="SalvarFornecedor" />
                                            <Anthem:LinkButton runat="server" ID="btnCancelarFornecedor" Text="Cancelar" CausesValidation="false" CommandName="CancelarFornecedor" />
                                            
                                            </div>    
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>                                 
                                    <asp:TemplateColumn HeaderText="Origem" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlOrigemMaterial" AutoCallBack="true" OnSelectedIndexChanged="ddlOrigemMaterial_SelectedIndexChanged" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlOrigemMaterialNovo" AutoCallBack="true" />
                                        </FooterTemplate>                                       
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Qtd. Estoque" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblQuantidadeEstoque" />
                                        </ItemTemplate> 
                                        <FooterTemplate>
                                            <Anthem:Label runat="server" ID="lblQuantidadeEstoque" />
                                        </FooterTemplate>                                       
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Qtd. Atendida" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:NumericTextBox runat="server" ID="txtQuantidade" DecimalPlaces="0" Columns="8" />
                                            <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtQuantidade" ErrorMessage="Campo Obrigatório"
                                                Display="dynamic" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:NumericTextBox runat="server" ID="txtQuantidadeNovo" DecimalPlaces="0" Columns="8" />
                                            <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtQuantidadeNovo" ErrorMessage="Campo Obrigatório"
                                                Display="dynamic" ValidationGroup="Novo" />
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Separar" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnSeparar" Text="Separar" CommandName="Edit" CausesValidation="false" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnSalvar" Text="Salvar" CommandName="Update" ValidationGroup="Novo" />
                                            &nbsp;
                                            <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" CausesValidation="false"  />
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Cancelar" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Delete" CausesValidation="false" />
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>
                                </Columns>
                            </Anthem:DataGrid>
                            <div align="right">
                                
                            </div>
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
                 <Anthem:Button runat="server" ID="btnVoltar" TextDuringCallBack="Aguarde" Text="Voltar"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
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
