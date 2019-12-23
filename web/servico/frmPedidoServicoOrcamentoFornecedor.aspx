<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoOrcamentoFornecedor.aspx.cs" Inherits="frmPedidoServicoOrcamentoFornecedor" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/BuscaFornecedor.ascx" TagPrefix="uc" TagName="BuscaFornecedor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />    
</head>
<body>
    <form id="form1" runat="server">               
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
                        <td ></td>
                        <td align="right" >
                           Fornecedor:
                        </td>
                        <td align="left">
                           <uc:BuscaFornecedor runat="server" ShowNovo="false" ID="ucBuscaFornecedor" />
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
                                     <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <Anthem:CheckBox runat="server" ID="chkMarcado" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <Anthem:CheckBox runat="server" ID="chkTodos" AutoCallBack="true" OnCheckedChanged="chkTodos_CheckChanged" />
                                        </HeaderTemplate>                                       
                                    </asp:TemplateColumn>            
                                     <asp:TemplateColumn HeaderText="Código Material" ItemStyle-HorizontalAlign="left" >
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.CodigoInterno %>
                                        </ItemTemplate>                                                                              
                                    </asp:TemplateColumn>                               
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.Descricao %>
                                        </ItemTemplate>                                                                              
                                    </asp:TemplateColumn>                                   
                                    <asp:TemplateColumn HeaderText="Origem Material" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                             <%# Shared.Common.Util.GetDescription(((PedidoServicoItemOrcamento)Container.DataItem).OrigemMaterial) %>                                             
                                        </ItemTemplate>                                                                              
                                    </asp:TemplateColumn>                                                                                                   
                                </Columns>
                            </Anthem:DataGrid>                           
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
                 <Anthem:Button runat="server" ID="btnSalvar" TextDuringCallBack="Aguarde" Text="Salvar"
                     EnabledDuringCallBack="false" CssClass="Button" />&nbsp;              
                <Anthem:Button runat="server" ID="btnFechar" Text="Fechar"
                     CssClass="Button" CausesValidation="false" OnClientClick="self.close();" />
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
