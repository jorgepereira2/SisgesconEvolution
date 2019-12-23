<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchPedidoCotacaoParaFornecedor.aspx.cs" Inherits="fchPedidoCotacaoParaFornecedor" %>
<%@ Import namespace="Shared.NHibernateDAL"%>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/DadosOM.ascx" TagName="DadosOM" TagPrefix="uc" %>
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
            <td align="center" width="40%">
                <uc:DadosOM ID="DadosOM1" runat="server" />
            </td>
            <td align="right" width="30%">
                 <b>Pedido Cotação</b>
                <br />
                <br />
                <b>
                    PC <%# _pedido.Numero %>
                </b>
                <br />
                <br />
                Emissão: <%# DateTime.Today.ToShortDateString() %>                         
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
                    <b>Número:
                    <%# _pedido.Numero %>
                    </b> 
                </td>
                 <td align="left" valign="top" >
                    <b>Data Emissão:</b> 
                    <%# _pedido.DataEmissao.ToShortDateString() %>
                </td>
            </tr>            
            <tr>
                <td align="left" valign="top"  >
                    <b>Finalizado:</b> 
                    <%# _pedido.FlagFinalizado ? "Sim" : "Não" %>
                </td>
                <td align="left" valign="top" >
                    <b>Comprador:</b>
                    <%# _pedido.Servidor.Identificacao %>                     
                </td>
            </tr>                     
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Observação:</b> 
                    <%# _pedido.Observacao %>
                </td>
            </tr>
        </table>
        
           <br />
             
        <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Itens
        </div>  
       <anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
             AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
            <HeaderStyle CssClass="dgHeader" />                                    
            <ItemStyle CssClass="dgItem" />
            <AlternatingItemStyle CssClass="dgAlternatingItem" />
            <FooterStyle HorizontalAlign="Right" />
            <Columns>
                <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                            <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.SubClasseServicoMaterial.Codigo %> 
                            -
                            <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.NumeroReferencia %>
                            -
                            <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                             -
                            <%# ((PedidoCotacaoItem)Container.DataItem).Especificacao %>
                    </ItemTemplate>
                </asp:TemplateColumn>                                        
                <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                       <%# ((PedidoCotacaoItem)Container.DataItem).Quantidade.ToString("N2") %>
                       (<%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.Unidade.Descricao %>)
                    </ItemTemplate>
                    <FooterTemplate>
                        <b>Total:</b>
                    </FooterTemplate>
                </asp:TemplateColumn>                                        
                 <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                      &nbsp;
                    </ItemTemplate>                                            
                </asp:TemplateColumn>                                                       
            </Columns>
        </anthem:DataGrid>         
         
         
          
    </form>
</body>
</html>
