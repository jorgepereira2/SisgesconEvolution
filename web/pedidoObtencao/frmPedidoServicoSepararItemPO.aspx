<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoSepararItemPO.aspx.cs" Inherits="frmPedidoServicoSepararItemPO" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/pedidoObtencao/DescartarItem.ascx" TagName="DescartarItem" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />      
</head>
<body>
    <form id="form1" runat="server">       
    <uc:DescartarItem runat="server" ID="ucDescartarItem" />
    <Anthem:Window runat="server" ID="winInserirItemPC" style="left:120px;" 
    Caption="Inserir Itens em PO" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="520px"  Height="220px" Modal="true" >
    <br />

    <div style="text-align:left">
    <br />
    <Anthem:RadioButton runat="server" ID="rbCriarNovoPO" Text="Criar nova AC" AutoCallBack="true" Checked="true" GroupName="PC" /><br />
    <Anthem:RadioButton runat="server" ID="rbPCExistente" Text="Inserir itens em AC existente" AutoCallBack="true" GroupName="PC" /><br />    
    <br />
    <Anthem:Panel runat="server" ID="pnEscolherPC" Visible="false">
        Inserir em:&nbsp;
        <Anthem:DropDownList runat="server" ID="ddlPO" />
    </Anthem:Panel>
    <br />
    <Anthem:CheckBox runat="server" ID="chkUtilizarSaldoLicitacao" Text="Utilizar saldo da licitação" Visible="false" />
    <br /><br />
    <div style="text-align:right">
        <Anthem:Button runat="server" ID="btnInserirItem" TextDuringCallBack="Aguarde" Text="Salvar"
                     EnabledDuringCallBack="false" CssClass="Button" />&nbsp;
        <Anthem:Button runat="server" ID="btnCancelarInserirItem" TextDuringCallBack="Aguarde" Text="Cancelar"
                     EnabledDuringCallBack="false" CssClass="Button" />&nbsp;
    </div>
    </div>

    </Anthem:Window>
    
    <div align="center">
    <div align="right" style="width:98%" class="PageTitle">
    <br />
        Separar Itens para Cotação
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="98%" >																		    
        <tr   >
            <td style="border:solid 1px black" valign="top">                
                <table border="0" cellpadding="2" cellspacing="2" width="100%" >                                                  
                    <tr>                            
                        <td colspan="3" align="center" valign="top">
                        <br />
                            <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                Materiais Pendentes                                  
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
                                     <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkDescartar" CommandName="Delete" Text="Descartar" />
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="AC" ItemStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkPS" style="text-decoration:underline;" 
                                                Text='<%# ((PedidoServicoItemOrcamento)Container.DataItem).DelineamentoOrcamento.CodigoComAno %>' />
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>                                    
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.Descricao%>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>                                                                      
                                    <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).Quantidade%> 
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.Unidade.Descricao%>
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Saldo Licitação" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkLicitacao" Text='
                                                <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.ItemLicitacaoDisponivel != null ? 
                                                ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.ItemLicitacaoDisponivel.QuantidadeDisponivel + 
                                                    string.Format(" (Licitação {0})", ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.ItemLicitacaoDisponivel.Licitacao.NumeroPregao) : "" %>' /> 
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>                                    
                                    <asp:TemplateColumn HeaderText="Selecionar" ItemStyle-HorizontalAlign="center" >
                                        <HeaderTemplate>
                                            <Anthem:CheckBox runat="server" ID="chkTodos" AutoCallBack="true" OnCheckedChanged="chkTodos_CheckedChanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <Anthem:CheckBox runat="server" ID="chkMarcado" />
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn> 
                                   
                                </Columns>
                            </Anthem:DataGrid>                            
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
                 <Anthem:Button runat="server" ID="btnInserir" TextDuringCallBack="Aguarde" Text="Criar Autorização de Compra dos Itens Selecionados"
                     EnabledDuringCallBack="false" CssClass="Button" Width="380px" />&nbsp;
                 
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
