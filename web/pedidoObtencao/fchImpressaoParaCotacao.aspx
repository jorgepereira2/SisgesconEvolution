<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchImpressaoParaCotacao.aspx.cs" Inherits="fchImpressaoParaCotacaoPO" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/servico/DetalhamentoOrcamento.ascx" TagName="Orcamento" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />  
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" Class="ReportTitle">
        Autorização de Compra        
    </div>
    <br /><br />
    <div class="PageTitle" style="width:98%;text-align:right;">
            Dados Básicos
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top"  >
                    <b>Número:
                    <%# _pedido.CodigoComAno %>
                    </b> 
                </td>
                 <td align="left" valign="top" >
                    <b>Data Emissão:</b> 
                    <%# _pedido.DataEmissao.ToShortDateString() %>
                </td>
            </tr>   
            
        </table>
        
     
        
      
                
        <br />
        <div class="PageTitle" style="width:100%;text-align:right;">
            Itens
        </div>
        <anthem:DataGrid runat="server" ID="dgItens" Width="100%" CssClass="datagrid"
                AutoGenerateColumns="false" CellPadding="3" >
            <HeaderStyle CssClass="dgHeader" />                                    
            <ItemStyle CssClass="dgItem" />
            <AlternatingItemStyle CssClass="dgAlternatingItem" />
            <FooterStyle CssClass="dgFooter" />
            <EditItemStyle HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                    </ItemTemplate>                                                                                  
                </asp:TemplateColumn>                    
                    <asp:TemplateColumn HeaderText="Origem" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%# Shared.Common.Util.GetDescription(((PedidoObtencaoItem)Container.DataItem).OrigemMaterial)%>
                    </ItemTemplate>                                            
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%# ((PedidoObtencaoItem)Container.DataItem).Quantidade.ToString("N2")%>
                    </ItemTemplate>                                            
                </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Valor Unit." ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </ItemTemplate>                                            
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </ItemTemplate>                                            
                </asp:TemplateColumn>
            </Columns>
        </anthem:DataGrid> 
               
                
    
      
    </form>
</body>
</html>
