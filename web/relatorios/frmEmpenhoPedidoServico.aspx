<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEmpenhoPedidoServico.aspx.cs" Inherits="frmEmpenhoPedidoServico" %>
<%@ Import namespace="System.Data"%>
<%@ Register TagPrefix="WebControls" Assembly="Shared.WebControls" Namespace="Shared.WebControls" %>
<%@ Register Src="~/UserControls/ucColumnManager.ascx" TagPrefix="uc" TagName="ColumnManager" %>
<%@ Register Src="~/UserControls/CabecalhoRelatorio.ascx" TagPrefix="uc" TagName="Cabecalho" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet"  />   
    
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Empenhos por PS" />    
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <asp:DataList runat="server" ID="dlPS" Width="100%">
        <ItemTemplate>
            <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                <tr>
                    <td align="left" >
                        <b>Código Interno: </b> <asp:LinkButton runat="server" ID="lnkDetalhes" Font-Bold="false" />                        
                    </td>
                    <td align="left" >
                        <b>Pedido Cliente: </b> <%# ((DelineamentoOrcamento)Container.DataItem).CodigoPedidoCliente %>
                    </td>
                    <td align="left">
                       <b> Divisão:</b> <%# ((DelineamentoOrcamento)Container.DataItem).Celula.Descricao %>
                    </td>
                    <td align="left">
                       <b> Status:</b> <%# ((DelineamentoOrcamento)Container.DataItem).DescricaoStatus%>
                    </td>                    
                </tr>
                  <tr>
                    <td align="left" style="width:14%">
                        <b>PO: </b> <asp:LinkButton runat="server" ID="lnkDetalhesPO" Font-Bold="false" />                        
                    </td>
                    <td align="left" colspan="1">
                       <b> Divisão:</b> <%# ((DelineamentoOrcamento)Container.DataItem).PedidoObtencao.Celula.Descricao%>
                    </td>
                    <td align="left">
                       <b> Status:</b> <%# ((DelineamentoOrcamento)Container.DataItem).PedidoObtencao.DescricaoStatus%>
                    </td>                    
                </tr>

            </table>           
             
            </div>
            <ul>
                           
                 <asp:DataGrid runat="server" ID="dgEmpenho" Width="98%" CssClass="datagrid" AutoGenerateColumns="false" CellPadding="3" ShowFooter="false" OnItemDataBound="dgEmpenhoItemDataBound" >
                    <HeaderStyle CssClass="dgHeader"  />                                    
                    <ItemStyle CssClass="dgItem" />
                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                    <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Número" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoObtencaoEmpenho)Container.DataItem).NumeroEmpenho %>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Ação Interna" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoObtencaoEmpenho)Container.DataItem).Projeto %>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PTRES" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoObtencaoEmpenho)Container.DataItem).PTRES %>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>                           
                        <asp:TemplateColumn HeaderText="Lançamento" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoObtencaoEmpenho)Container.DataItem).NumeroLancamento %>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Lista" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoObtencaoEmpenho)Container.DataItem).Lista %>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Código Gestão" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoObtencaoEmpenho)Container.DataItem).CodigoGestao %>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>  
                            <asp:TemplateColumn HeaderText="Comentário" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoObtencaoEmpenho)Container.DataItem).Comentario %>

                                <asp:Literal runat="server" ID="litBreak"></asp:Literal>

                                &nbsp;&nbsp;
                                 <asp:DataGrid runat="server" ID="dgPagamento" Width="98%" CssClass="datagrid" AutoGenerateColumns="false" CellPadding="3" ShowFooter="false"  >
                                        <HeaderStyle CssClass="dgHeader"  />                                    
                                        <ItemStyle CssClass="dgItem" />
                                        <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                        <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Número" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <%# ((PedidoObtencaoPagamento)Container.DataItem).Numero %>
                                                </ItemTemplate>                                            
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="NF" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                   <%# ((PedidoObtencaoPagamento)Container.DataItem).NumeroNF %>
                                                </ItemTemplate>                                            
                                            </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Ordem Bancaria" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <%# ((PedidoObtencaoPagamento)Container.DataItem).NumeroOrdemBancaria %>
                                                </ItemTemplate>                                            
                                            </asp:TemplateColumn>                           
                                            <asp:TemplateColumn HeaderText="Status" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <%# ((PedidoObtencaoPagamento)Container.DataItem).Status.Descricao %>
                                                </ItemTemplate>                                            
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <%# ((PedidoObtencaoPagamento)Container.DataItem).Valor %>
                                                </ItemTemplate>                                            
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>    

                            </ItemTemplate>                                            
                        </asp:TemplateColumn> 
                    </Columns>
                </asp:DataGrid>    
                    
                <br />
            </ul>
        </ItemTemplate>      
      </asp:DataList>
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
