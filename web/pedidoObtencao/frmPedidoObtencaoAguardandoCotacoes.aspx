<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoObtencaoAguardandoCotacoes.cs" Inherits="frmPedidoObtencaoAguardandoCotacoes" %>
<%@ Register Src="ucHistoricoPedidoObtencao.ascx" TagPrefix="uc" TagName="Historico" %>
<%@ Register Src="~/UserControls/BuscaFornecedor.ascx" TagName="BuscaFornecedor" TagPrefix="uc" %>
<%@ Register Src="CancelarItem.ascx" TagName="CancelarItem" TagPrefix="uc" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />          
</head>
<body >
    <form id="form1" runat="server">
    <uc:CancelarItem runat="server" ID="ucCancelarItem" />       
    <div align="center">
    <div align="right" style="width:90%" Class="PageTitle">
    <br />
        Aprovação de AC/PM
    </div>
      <table cellSpacing="4" cellPadding="3" border="0" Width="92%" >																		    
        <tr>
            <td style="border:solid 0px black" valign="top" align="left">               
                <br />
                
            
                    <table cellspacing="4" cellpadding="3" border="0" width="900px" >																		    
                        <tr>
                            <td style="border:solid 0px black" valign="top">
                                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                    Pedido
                                    <hr size="1" class="divisor" />
                                </div>
                                <br />
                                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                                     <tr>
                                        <td width="10%" class="msgErro" ></td>
                                        <td align="right" width="20%" >
                                           Número:
                                        </td>
                                        <td align="left" class="legenda">
                                           <%# _pedido.CodigoComAno %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%" class="msgErro" ></td>
                                        <td align="right" width="20%" >
                                           Tipo:
                                        </td>
                                        <td align="left" class="legenda">
                                           <%# Shared.Common.Util.GetDescription(_pedido.TipoPedido) %>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td width="10%" class="msgErro" ></td>
                                        <td align="right" width="20%" >
                                           Tipo PO:
                                        </td>
                                        <td align="left" class="legenda">
                                           <%# Shared.Common.Util.GetDescription(_pedido.TipoPedidoObtencao) %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%" class="msgErro" ></td>
                                        <td align="right"  >
                                           Status:
                                        </td>
                                         <td align="left" class="legenda">
                                            <%# _pedido.Status.Descricao %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%" class="msgErro" ></td>
                                        <td align="right" >
                                           Data Emissão:
                                        </td>
                                         <td align="left" class="legenda">
                                            <%# _pedido.DataEmissao.ToShortDateString() %>
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
                                        <td class="msgErro" ></td>
                                        <td align="right" >
                                           Divisão:
                                        </td>
                                       <td align="left" class="legenda">
                                            <%# _pedido.Celula.Descricao %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="msgErro" ></td>
                                        <td align="right" >
                                           Aplicação:
                                        </td>
                                      <td align="left" class="legenda">
                                            <%# _pedido.Aplicacao %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td ></td>
                                        <td align="right" >
                                           Observação:
                                        </td>
                                        <td align="left" class="legenda">
                                            <%# _pedido.Observacao %>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>																			
                    </table>
                  
                
                      <table border="0" cellpadding="2" cellspacing="2" width="900px" >
                        <tr>                            
                            <td colspan="3" align="center" valign="top">
                            <br />
                                <div align="left" style="vertical-align:text-bottom" class="PageTitle" >
                                    Fornecedores
                                    <hr size="1" class="divisor" style="" />
                                </div>                                                                
                                <br />
                                 <br />
                                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                                      <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Fornecedor:
                                        </td>
                                        <td align="left">
                                           <uc:BuscaFornecedor runat="server" ID="ucBuscaFornecedor" ShowNovo="False" Required="True" />    
                                           
                                        </td>
                                    </tr>	
                                     <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Fornecedor 2:
                                        </td>
                                        <td align="left">
                                           <uc:BuscaFornecedor runat="server" ID="ucBuscaFornecedor2" ShowNovo="False" Required="True" />    
                                           
                                        </td>
                                    </tr>	
                                    <tr>
                                        <td class="msgErro" >*</td>
                                        <td align="right" >
                                           Fornecedor 3:
                                        </td>
                                        <td align="left">
                                           <uc:BuscaFornecedor runat="server" ID="ucBuscaFornecedor3" ShowNovo="False" Required="True" />    
                                           
                                        </td>
                                    </tr>	   
                                    <tr>
                                        <td class="msgErro" ></td>
                                        <td align="right" >
                                           Fornecedor 4:
                                        </td>
                                        <td align="left">
                                           <uc:BuscaFornecedor runat="server" ID="ucBuscaFornecedor4" ShowNovo="False" />    <%-- Required="True"--%>
                                           
                                        </td>
                                    </tr>	   
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>                      
                    </table>      

                  
                    <table border="0" cellpadding="2" cellspacing="2" width="900px" >
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
                                        <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <%# ((PedidoObtencaoItem)Container.DataItem).Quantidade %> 
                                               <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Unidade.Descricao %>
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Qtd. Estoque" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <asp:Label runat="server" ID="lblQuantidadeEstoque" />
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                         <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <Anthem:NumericTextBox Columns="10" ID="txtValor"  runat="server" Text='<%# ((PedidoObtencaoItem)Container.DataItem).Valor.ToString("N2") %>' />
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Valor 2" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <Anthem:NumericTextBox ID="txtValor2" Columns="10"  runat="server" Text='<%# ((PedidoObtencaoItem)Container.DataItem).ValorCotacao2.ToString("N2") %>' />
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Valor 3" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <Anthem:NumericTextBox ID="txtValor3" Columns="10"  runat="server" Text='<%# ((PedidoObtencaoItem)Container.DataItem).ValorCotacao3.ToString("N2") %>' />
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Valor 4" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <Anthem:NumericTextBox ID="txtValor4" Columns="10"  runat="server" Text='<%# ((PedidoObtencaoItem)Container.DataItem).ValorCotacao4.ToString("N2") %>' />
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                       <%-- <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                               <%# ((PedidoObtencaoItem)Container.DataItem).ValorTotal.ToString("C2") %>
                                            </ItemTemplate>
                                            <FooterTemplate>                                                
                                               <b><%# _pedido.ValorTotal.ToString("C2") %></b>
                                            </FooterTemplate>                                            
                                        </asp:TemplateColumn>--%>
                                        
                                        <asp:TemplateColumn HeaderText="Cota" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <%# ((PedidoObtencaoItem)Container.DataItem).GetCota() %> 
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Qtd. Aprovada" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <%# ((PedidoObtencaoItem)Container.DataItem).GetQuantidadeAprovada() %> 
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Cota Disponível" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                               <%# ((PedidoObtencaoItem)Container.DataItem).GetCota() - ((PedidoObtencaoItem)Container.DataItem).GetQuantidadeAprovada()%> 
                                            </ItemTemplate>                                            
                                        </asp:TemplateColumn>
                                        
                                         <asp:TemplateColumn HeaderText="Cancelar" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Delete" CausesValidation="false" />
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>                                                                                                                      
                                    </Columns>
                                </anthem:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br/>
                            </td>
                        </tr>                      
                         <tr>
                                        <td class="msgErro" ></td>
                                        <td align="right" >
                                           Comentário:
                                        </td>
                                        <td align="left">
                                            <Anthem:TextBox runat="server" ID="txtComentario" TextMode="MultiLine" Rows="3"  
                                            Columns="50" />    
                                           
                                        </td>
                                    </tr>	    
                        <tr>
                            <td colspan="3">
                                &nbsp;
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
                <Anthem:Button runat="server" ID="btnEnviar" TextDuringCallBack="Aguarde" Text="Enviar"
                     EnabledDuringCallBack="false" CssClass="Button" /> 
                <Anthem:Button runat="server" ID="btnImprimir" Text="Imprimir"
                     CssClass="Button" CausesValidation="false" />                             
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
