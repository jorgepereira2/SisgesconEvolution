<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchRequisicaoEstoque.aspx.cs" Inherits="fchRequisicaoEstoque" %>
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
        Detalhes da Requisição de Estoque
    </div>
    <br /><br />
    <div class="PageTitle" style="width:98%;text-align:right;">
            Dados Básicos
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top"  >
                    <b>Número:
                    <%# _requisicao.Numero %>
                    </b> 
                </td>
                 <td align="left" valign="top" >
                    <b>Data Finalização:</b> 
                    <%# _requisicao.DataFinalizacao.ToShortDateString() %>
                </td>
            </tr>            
            <tr>
                <td align="left" valign="top"  >
                    <b>Responsável:</b> 
                    <%# _requisicao.ServidorResponsavel.Identificacao %>
                </td>
                <td align="left" valign="top" >
                    <b>Servidor Cadastro:</b>
                    <%# _requisicao.ServidorCadastro.Identificacao %>                     
                </td>
            </tr>  
            <tr>                
                <td align="left" valign="top" >
                    <b>Status:
                    <%# Shared.Common.Util.GetDescription(_requisicao.Status) %>
                    </b>
                </td>
                <td align="left" valign="top" >
                    <b>Tipo:</b>
                    <%# _requisicao.TipoRequisicaoEstoque.Descricao %>
                    
                </td>
            </tr>            
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Observação:</b> 
                    <%# _requisicao.Observacao %>
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
                    <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((RequisicaoEstoqueItem)Container.DataItem).Material.DescricaoCompleta %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((RequisicaoEstoqueItem)Container.DataItem).Quantidade %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Origem" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# Shared.Common.Util.GetDescription(((RequisicaoEstoqueItem)Container.DataItem).OrigemMaterial) %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                                                                                                                                                           
                </Columns>
            </anthem:DataGrid>           
         
         
          
    </form>
</body>
</html>
