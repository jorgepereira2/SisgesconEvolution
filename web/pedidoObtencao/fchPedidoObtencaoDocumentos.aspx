<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchPedidoObtencaoDocumentos.aspx.cs" Inherits="fchPedidoObtencaoDocumentos" %>
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
                 <b ><asp:Label runat="server" ID="lblTitulo" Text="Pedido Obtenção" /></b>
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
            Documentos
        </div>  
       <anthem:DataGrid runat="server" ID="dgDocumento" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Documento" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoDocumento)Container.DataItem).Nome %> 
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                             <a href='<%# "frmPedidoObtencaoDocumentoDownload.aspx?id=" + ((PedidoObtencaoDocumento)Container.DataItem).ID %>' target="_blank">[Download]</a>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                                       
                </Columns>
            </anthem:DataGrid>           
         </asp:Panel>
      

         

    </form>
</body>
</html>
