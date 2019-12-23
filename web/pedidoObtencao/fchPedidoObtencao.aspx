<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchPedidoObtencao.aspx.cs" Inherits="fchPedidoObtencao" %>
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
            <td align="center" width="50%">
                <uc:DadosOM runat="server" />
            </td>
            <td align="right" width="25%">
                 <b ><asp:Label runat="server" ID="lblTitulo" Text="Autorização de Compra" /></b>
                <br />
                <br />
                <b>
                    AC <%# _pedido.CodigoComAno %>
                </b>
                <br />
                <br />
                Emissão: <%# DateTime.Today.ToShortDateString() %>                         
            </td>
        </tr>
    </table>
    <br /><br />
        <div  style="width:98%;text-align:right;">
        <div style="text-align:center">
            <b>
            Autorização de Compra/Ordem de execução do serviço No <%# _pedido.CodigoComAno %>            
            </b>
        </div>
        <br />
        
        <br />
        <div style="text-align:left">
        <b>Data:</b> <%# _pedido.DataEmissao.ToShortDateString() %>
        </div>
        <br />
        
        <div style="text-align:justify">
        <%# Parametro.Get().TextoAC %>
        </div>
        <hr />
        </div>
        <br />
    <div class="PageTitle" style="width:98%;text-align:right;">
            Dados Básicos
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>               
                 <td align="left" valign="top" >
                    <b>Data Emissão:</b> 
                    <%# _pedido.DataEmissao.ToShortDateString() %>
                </td>
                 <td align="left" valign="top"  >
                    <b>Divisão:</b> 
                    <%# _pedido.Celula.Descricao %>
                </td>
            </tr>            
            <tr>               
                <td align="left" valign="top" >
                    <b>Servidor:</b>
                    <%# _pedido.Servidor.Identificacao %>                     
                </td>
                <td align="left" valign="top"  >
                    <b>Tipo AC:</b>
                    <%# _pedido.TipoCompra.Descricao %>&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lnkLicitacao" />                     
                </td>
            </tr>  
            <tr>                
                <td align="left" valign="top"  >
                    <b>Status:
                    <%# _pedido.Status.Descricao %>
                    </b>
                </td>
                 <td align="left" valign="top" >
                    <b>Finalidade:
                    <%# Shared.Common.Util.GetDescription(_pedido.TipoPedidoObtencao ) %>
                    </b>
                </td>
            </tr> 
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Aplicação:</b> 
                    <%# _pedido.Aplicacao %>
                    <asp:LinkButton runat="server" ID="lnkPS" />
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
             
        <asp:Panel runat="server" ID="pnItem">     
        <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Itens
        </div>  
       <anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((IItemServicoMaterial)Container.DataItem).ServicoMaterial.CodigoInterno%> - 
                            <%# ((IItemServicoMaterial)Container.DataItem).ServicoMaterial.Descricao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Especificação" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((IItemServicoMaterial)Container.DataItem).Especificacao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                                       
                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <span id="Color" style="<%# ((IItemServicoMaterial)Container.DataItem).Quantidade > 0 ? "color: red; font-weight: bold; font-size: 15px" : "" %>"><%# ((IItemServicoMaterial)Container.DataItem).Quantidade.ToString("N2")%></span>
                        </ItemTemplate>                                             
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((IItemServicoMaterial)Container.DataItem).Valor.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                   
                    <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                          <%# (((IItemServicoMaterial)Container.DataItem).Quantidade * ((IItemServicoMaterial)Container.DataItem).Valor).ToString("C2")%>
                        </ItemTemplate>
                        <FooterTemplate>                                                
                            <b><%# _pedido.ValorTotal.ToString("C2") %></b>
                        </FooterTemplate>                                            
                    </asp:TemplateColumn>                                                                                                                      
                </Columns>
            </anthem:DataGrid>           
         </asp:Panel>
         
         <asp:Panel runat="server" ID="pnItemPEP">
         <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Itens PEP
        </div> 
         <anthem:DataGrid runat="server" ID="dgItemPEP" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((IItemServicoMaterial)Container.DataItem).ServicoMaterial.CodigoInterno%> - 
                            <%# ((IItemServicoMaterial)Container.DataItem).ServicoMaterial.Descricao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Especificação" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((IItemServicoMaterial)Container.DataItem).Especificacao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                                        
                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((IItemServicoMaterial)Container.DataItem).Quantidade.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((IItemServicoMaterial)Container.DataItem).Valor.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                   
                    <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# (((IItemServicoMaterial)Container.DataItem).Quantidade * ((IItemServicoMaterial)Container.DataItem).Valor).ToString("C2")%>
                        </ItemTemplate>
                        <FooterTemplate>                                                
                            <b><%# _pedido.ValorTotal.ToString("C2") %></b>
                        </FooterTemplate>                                            
                    </asp:TemplateColumn>                                                                                                                      
                </Columns>
            </anthem:DataGrid>   
         </asp:Panel>
         
             <asp:Panel runat="server" ID="pnItemRodizio">
         <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Itens Rodízio
        </div> 
         <anthem:DataGrid runat="server" ID="dgItemRodizio" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((IItemServicoMaterial)Container.DataItem).ServicoMaterial.CodigoInterno%> - 
                            <%# ((IItemServicoMaterial)Container.DataItem).ServicoMaterial.Descricao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Especificação" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((IItemServicoMaterial)Container.DataItem).Especificacao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                                   
                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((IItemServicoMaterial)Container.DataItem).Quantidade.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((IItemServicoMaterial)Container.DataItem).Valor.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                   
                    <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# (((IItemServicoMaterial)Container.DataItem).Quantidade * ((IItemServicoMaterial)Container.DataItem).Valor).ToString("C2")%>
                        </ItemTemplate>
                        <FooterTemplate>                                                
                            <b><%# _pedido.ValorTotal.ToString("C2") %></b>
                        </FooterTemplate>                                            
                    </asp:TemplateColumn>                                                                                                                      
                </Columns>
            </anthem:DataGrid>   
         </asp:Panel>
          
           <br /><br />
       <table width="98%" border="0">
        <%--<tr>
            <td style="width:2%; text-align:center" >            
            
            </td>
            <td style="width:98%; text-align:left" >
               Numero NE: __________________ <br /><br />
               Numero Lista: __________________
               
            </td>            
        </tr>--%>
         <tr>
            <td style="width:2%; text-align:center" >
            
            
            </td>
            <td style="width:98%; text-align:left" >
                <br /><br />
                _______________________________<br />
                Encarregado Obtenção
               
            </td>
            
        </tr>

       
     </table>

         

    </form>
</body>
</html>
