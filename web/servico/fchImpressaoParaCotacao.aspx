<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchImpressaoParaCotacao.aspx.cs" Inherits="fchImpressaoParaCotacao" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/servico/DetalhamentoOrcamento.ascx" TagName="Orcamento" TagPrefix="uc" %>
<%@ Register TagPrefix="uc" TagName="DadosOM" Src="~/UserControls/DadosOM.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />  
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="2" style="border:solid 1px black; width:98%">
        <tr>
            <td>
            
            </td>
            <td align="center" width="50%">
                <uc:DadosOM runat="server" />
            </td>
            <td align="right" width="25%">
                 <b>
                 <asp:Label runat="server" ID="lblTitulo" Text="Pedido de Orçamento" /></b>
                <br />
               
                <br />
                <br />
                
            </td>
        </tr>
    </table>
    <br /><br />
    <div class="PageTitle" style="width:98%;text-align:right;">
            Dados Básicos
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top"  >
                    <b>Código Interno:
                    <%# _pedido.CodigoComAno %>
                    </b> 
                </td>
                 <td align="left" valign="top" >
                    <b>Data Emissão:</b> 
                    <%# _pedido.DataEmissao.ToShortDateString() %>
                </td>
            </tr>   
            <tr>
                <td align="left" valign="top"  >
                    <b>Codigo Pedido Cliente:
                    <%# _pedido.CodigoPedidoCliente %></b> 
                </td>
                <td align="left" valign="top" >
                    <b>Cliente:</b>
                    <%# _pedido.Cliente.Descricao %>                     
                </td>
            </tr>           
            <tr>
                <td align="left" valign="top"  >
                    <b>Divisão:</b> 
                    <%# _pedido.Celula == null ? "" : _pedido.Celula.Descricao %>
                </td>
               <td align="left" valign="top" >
                    <b>Situação:
                    <%# _pedido.Status.Descricao %>
                    </b>
                </td>
            </tr>              
          
        </table>
        <br />
         <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top"  >
                    <b>Fornecedor:</b> 
                </td>
                 <td align="left" valign="top" >
                    <b>CNPJ:</b>                     
                </td>
            </tr>   
            <tr>
                <td align="left" valign="top"  >
                    <b>Endereço:</b> 
                </td>
                <td align="left" valign="top" >
                    <b>Telefone:</b>                    
                </td>
            </tr>           
            <tr>
                <td align="left" valign="top"  >
                    <b>Cidade:</b>                     
                </td>
               <td align="left" valign="top" >
                    <b>Fax:
                    </b>
                </td>
            </tr>    
            <tr>
                <td align="left" valign="top"  >
                    <b>Contato:</b>                     
                </td>
               <td align="left" valign="top" >
                    
                </td>
            </tr>              
          
        </table>
      
                
        <br />
        <div class="PageTitle" style="width:100%;text-align:right;">
            Itens Orçamento
        </div>
        <anthem:DataGrid runat="server" ID="dgOrcamentoItem" Width="98%" CssClass="datagrid"
                AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
            <HeaderStyle CssClass="dgHeader" />                                    
            <ItemStyle CssClass="dgItem" />
            <AlternatingItemStyle CssClass="dgAlternatingItem" />
            <FooterStyle CssClass="dgFooter" HorizontalAlign="Right" />
            <EditItemStyle HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.Descricao %>
                    </ItemTemplate>                                                                                  
                </asp:TemplateColumn>                                      
                <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%# ((PedidoServicoItemOrcamento)Container.DataItem).Quantidade.ToString("N2") %>
                    </ItemTemplate>                                            
                </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Valor Unit." ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </ItemTemplate>
                    <FooterTemplate>
                        <b>Total</b>
                    </FooterTemplate>                                            
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </ItemTemplate>                                            
                </asp:TemplateColumn>
            </Columns>
        </anthem:DataGrid> 
        <br />

        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top"  >
                    <b>Obs:</b> 
                    <%# Parametro.Get().TextoImpressaoMensagemOrcamento %>
                </td>                 
            </tr> 
        </table>
      
        <br />
        <div style="width:100%;text-align:Left;">
            <b>Contatos do PS:</b> 
            <%# _pedido.Contatos %>
        </div>   
    
      
    </form>
</body>
</html>
