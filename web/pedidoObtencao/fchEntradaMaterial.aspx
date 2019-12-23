<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchEntradaMaterial.aspx.cs" Inherits="fchEntradaMaterial" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />  
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" Class="ReportTitle">
        Saída de  Material
    </div>
    <br /><br />
    <div class="PageTitle" style="width:98%;text-align:right;">
            Dados Básicos
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top"  >
                    <b>Origem:
                    <%# Shared.Common.Util.GetDescription(_entrada.OrigemMaterial) %>
                    </b> 
                </td>
                 <td align="left" valign="top" >
                    <b>Data:</b> 
                    <%# _entrada.Data.ToShortDateString() %>
                </td>
            </tr>            
            <tr>
                 <td align="left" valign="top" >
                    <b>PS:</b>
                    <%# _entrada.DelineamentoOrcamento != null ? _entrada.DelineamentoOrcamento.CodigoComAno  : "" %>                     
                </td>
                <td align="left" valign="top"  >
                    <b>AC:</b> 
                    <%# _entrada.PedidoObtencao != null ? _entrada.PedidoObtencao.CodigoComAno : "-"%>
                </td>               
            </tr>  
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Servidor:</b> 
                    <%# _entrada.Servidor.Identificacao %>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Célula:</b> 
                    <%# _entrada.PedidoObtencao != null ? _entrada.PedidoObtencao.Celula.Descricao : (_entrada.DelineamentoOrcamento != null ? _entrada.DelineamentoOrcamento.Celula.Descricao : "")%>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Aplicação:</b> 
                    <%# _entrada.PedidoObtencao != null ? _entrada.PedidoObtencao.Aplicacao : (_entrada.DelineamentoOrcamento != null ? _entrada.DelineamentoOrcamento.CodigoComAno : "")%>
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
                <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Código" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((EntradaMaterialItem)Container.DataItem).Material.CodigoInterno %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((EntradaMaterialItem)Container.DataItem).Material.Descricao %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((EntradaMaterialItem)Container.DataItem).Quantidade.ToString("N2") %>
                        </ItemTemplate>  
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="UF" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((EntradaMaterialItem)Container.DataItem).Material.Unidade.Descricao %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Preço Unit." ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                            <%# _entrada.PedidoObtencao != null ? ((EntradaMaterialItem)Container.DataItem).GetItemObtencao().Valor.ToString("N2") : "-" %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Total" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                            <%# _entrada.PedidoObtencao != null ? (((EntradaMaterialItem)Container.DataItem).GetItemObtencao().Valor * ((EntradaMaterialItem)Container.DataItem).Quantidade).ToString("N2") : "-" %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </anthem:DataGrid> 
       <br /><br /><br /><br /><br /><br /><br />   
       
       <div style="text-align:center">
       _____________________________________________<br />
       ASSINATURA<br /><br />
       NOME<br /><br />
       NIP<br />
       </div>
       
    </form>
</body>
</html>
