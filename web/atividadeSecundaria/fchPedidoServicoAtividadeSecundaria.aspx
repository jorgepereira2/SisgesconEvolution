<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchPedidoServicoAtividadeSecundaria.aspx.cs" Inherits="fchPedidoServicoAtividadeSecundaria" %>
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
        Detalhes do PS Atividade Secundária
    </div>
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
                    <b>Divisão:</b> 
                    <%# _pedido.Celula == null ? "" : _pedido.Celula.Descricao %>
                </td>
               <td align="left" valign="top" >
                    <b>Situação:
                    <%# _pedido.Status.Descricao %>
                    </b>
                </td>
            </tr>        
           <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Cliente:</b> 
                    <%# _pedido.Cliente.DescricaoCompleta %>
                </td>               
            </tr>
             <tr>                
                <td align="left" valign="top" colspan="2" >
                    <b>Pagador:</b> 
                    <%# _pedido.ClientePagador.DescricaoCompleta %>
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
                <div class="PageTitle" style="width:100%;text-align:right;">
                    Itens 
                </div>
                <anthem:DataGrid runat="server" ID="dgItem" Width="100%" CssClass="datagrid"
                     AutoGenerateColumns="false" CellPadding="3" >
                    <HeaderStyle CssClass="dgHeader" />                                    
                    <ItemStyle CssClass="dgItem" />
                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                    <FooterStyle CssClass="dgFooter" />
                    <EditItemStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoServicoAtividadeSecundariaItem)Container.DataItem).DescricaoServico %>
                            </ItemTemplate>                                                                                  
                        </asp:TemplateColumn>                        
                         <asp:TemplateColumn HeaderText="Tipo" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                  <%# ((PedidoServicoAtividadeSecundariaItem)Container.DataItem).TipoAtividadeSecundaria.Descricao %>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                               <%# ((PedidoServicoAtividadeSecundariaItem)Container.DataItem).Valor.ToString("C2")%>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>                        
                    </Columns>
                </anthem:DataGrid>                 
   
          
        <br />
       
        <asp:Panel runat="server" Visible="true">        
        <div class="PageTitle" style="width:98%;text-align:right;">
                Histórico
            </div>  
        <anthem:DataGrid runat="server" ID="dgHistorico" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle CssClass="dgFooter" />
                <EditItemStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Data" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((HistoricoPedidoServicoAtividadeSecundaria)Container.DataItem).Data.ToString("dd/MM/yyyy hh:mm")%>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>                    
                     <asp:TemplateColumn HeaderText="Servidor" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                           <%# ((HistoricoPedidoServicoAtividadeSecundaria)Container.DataItem).Servidor.Identificacao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Descrição" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((HistoricoPedidoServicoAtividadeSecundaria)Container.DataItem).Texto%>
                            <font style="color:red">
                            <%# String.IsNullOrEmpty(((HistoricoPedidoServicoAtividadeSecundaria)Container.DataItem).JustificativaRecusa) ? "" : "<br>Comentário:&nbsp;" + ((HistoricoPedidoServicoAtividadeSecundaria)Container.DataItem).JustificativaRecusa%>
                            </font>
                            <%--<%# ((HistoricoPedidoServicoAtividadeSecundaria)Container.DataItem).StatusPosterior.StatusPedidoServicoAtividadeSecundariaEnum == StatusPedidoServicoAtividadeSecundariaEnum.Cancelado ? "<br>Motivo: " + _pedido.MotivoCancelamento.Descricao : ""%>--%>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>
                </Columns>
            </anthem:DataGrid>      
          </asp:Panel>             
        
    </form>
</body>
</html>
