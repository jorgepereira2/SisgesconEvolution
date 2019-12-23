<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchPedidoObtencaoAssinatura.aspx.cs" Inherits="fchPedidoObtencaoAssinatura" %>
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
            <td width="20%">
                <asp:Image runat="server" ID="img" ImageUrl="~/images/imagem_ac.gif" Visible="false" />
            </td>
            <td align="center" >
                
                <uc:DadosOM runat="server" />
            </td>            
            <td width="20%">
            </td>
        </tr>
    </table>
    <br /><br />
    <div  style="width:98%;text-align:right;">
        <div style="text-align:center">
            <b>
            Autorização de Compra/Ordem de execução do serviço No <%# _po.CodigoComAno %>            
            </b>
        </div>
        <br />
        
        <br />
        <div style="text-align:left">
        <b>Data:</b> <%# _po.DataEmissao.ToShortDateString() %>
        </div>
        <br />
        
        <div style="text-align:justify">
        <%# Parametro.Get().TextoAC %>
        </div>
        <hr />
        <br />
        
       <table style="width:98%" cellpadding="2" cellspacing="3" border="0" >
            <tr>
                <td align="left" valign="top" >
                    <b>Licitação:</b>  <%# _po.Licitacao == null ? "" : _po.Licitacao.NumeroPregao %>                    
                </td>     
                <td align="left" valign="top" >
                    <b>Natureza Despesa:</b> <%# (_po.NaturezaDespesa != null ? _po.NaturezaDespesa.Codigo + " - " : "") +_po.NaturezaDespesa%>
                </td>              
                <td align="left" valign="top" >
                   <b> Comprador:</b> <%# _po.Servidor %>
                </td>                                 
            </tr>            
            <tr>
                <td align="left" valign="top" colspan="2" >
                    <b>Ação Interna</b> <%# _po.Projeto %>
                </td>                 
                <td align="left" valign="top" colspan="2" >
                   
                </td>                 
            </tr> 
        </table>
        </div>
        
        
        <hr />
        <br />
        <div style="text-align:left">
        <b>
        Fornecedor
        </b>
             <table style="width:98%" cellpadding="2" cellspacing="3" border="0" >
            <tr>
                <td align="left" valign="top" >
                    <b>Razão Social:</b>
                    <%# _po.Fornecedor != null ? _po.Fornecedor.RazaoSocial : null %>
                    
                </td>                 
                <td>
                    <b>Simples:</b>
                    <%# _po.Fornecedor != null ? (_po.Fornecedor.FlagOptanteSimples ? "Sim" : "Não") : null %>
                </td>                 
            </tr>            
            <tr>
                <td align="left" valign="top"  >
                    <b>CNPJ:</b>
                    <%# _po.Fornecedor == null ? null : _po.Fornecedor.CNPJ %>
                </td>
                <td align="left" valign="top" >
                    <b>Telefone:</b>
                    <%# _po.Fornecedor == null ? null : _po.Fornecedor.Telefone %>                     
                </td>
            </tr>  
            <tr>                
                <td align="left" valign="top" colspan="2" >
                    <b>Endereço:</b>
                    <%# _po.Fornecedor == null ? null : _po.Fornecedor.Endereco %>
                    -
                    <%# _po.Fornecedor == null ? null : _po.Fornecedor.Bairro %>
                    &nbsp;
                   
                    <%# _po.Fornecedor == null ? null : _po.Fornecedor.Municipio %>
                   
                    &nbsp;
                    <%# _po.Fornecedor == null ? null : _po.Fornecedor.Estado %>
                </td>                
            </tr>
            <tr>
                <td align="left" valign="top"  >
                    <b>CEP:</b> 
                    <%# _po.Fornecedor == null ? null : _po.Fornecedor.CEP %>
                </td>
                <td align="left" valign="top" >
                    <b>E-mail:</b>
                    <%# _po.Fornecedor == null ? null : _po.Fornecedor.Email %>                     
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Contatos:</b> 
                    <%# _po.Fornecedor == null ? null : _po.Fornecedor.DescricaoContatos %>
                </td>                
            </tr>            
        </table>
            
        </div>
        
           <br />
             
        <br />
        <div class="PageTitle" style="width:98%;text-align:left;">
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
                            <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.CodigoInterno %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.SubClasseServicoMaterial.Codigo%>
                            -
                            <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.NumeroReferencia%>
                            -
                            <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Descricao%>
                             -
                            <%# ((PedidoObtencaoItem)Container.DataItem).Especificacao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="UN" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Unidade.Descricao%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((PedidoObtencaoItem)Container.DataItem).Quantidade.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Valor Unit." ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((PedidoObtencaoItem)Container.DataItem).Valor.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                   
                    <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((PedidoObtencaoItem)Container.DataItem).ValorTotal.ToString("C2")%>
                        </ItemTemplate>
                        <FooterTemplate>                                                
                            <b><%# _po.ValorTotal.ToString("C2") %></b>
                        </FooterTemplate>                                            
                    </asp:TemplateColumn>                                                                                                                      
                </Columns>
            </anthem:DataGrid> 
            
             <br />
             
         <div style="text-align:left">
         Aplicação: <%# _po.Observacao %>
         </div>       
    
     <br /><br />
  

          <asp:Panel runat="server" ID="pnEmpenho">
          <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Empenhos
        </div>  
       <anthem:DataGrid runat="server" ID="dgEmpenho" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" ShowFooter="false" >
                <HeaderStyle CssClass="dgHeader" />                                    
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
                            <%# ((PedidoObtencaoEmpenho)Container.DataItem).PTRESS %>
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
                        </ItemTemplate>                                            
                    </asp:TemplateColumn> 
                </Columns>
            </anthem:DataGrid>     
          </asp:Panel>
          
        <br />
        <br />

        <table width="98%" border="0">
        <tbody><tr>
            <td style="width:50%; text-align:center" >
                ________________________________________<br />
                <%# _parametro.OrdenadorDespesaAC == null ? "" : _parametro.OrdenadorDespesaAC.NomeCompleto %> <br />                
                <%# _parametro.OrdenadorDespesaAC == null ? "" : _parametro.OrdenadorDespesaAC.Graduacao %> <br />
                <%# _parametro.OrdenadorDespesaAC == null ? "" : _parametro.OrdenadorDespesaAC.DiscriminacaoFuncao %> <br />
            </td>
            <td style="width:50%; text-align:center" >
            ________________________________________<br />
                <%# _parametro.OrdenadorDespesaAC == null ? "" : _parametro.Comandante.NomeCompleto %> <br />                
                <%# _parametro.OrdenadorDespesaAC == null ? "" : _parametro.Comandante.Graduacao %> <br />
                <%# _parametro.OrdenadorDespesaAC == null ? "" : _parametro.Comandante.DiscriminacaoFuncao %> <br />
            </td>
        </tr>
        </tbody>
        </table>


        <br />
          <table style="width:98%" cellpadding="2" cellspacing="3" border="0" >
             <tr>
                <td align="left" valign="top" colspan="2" >
                   Cód. Gestão: <%# _po.CodigoGestao %>
                </td>                 
            </tr>
             <tr>
                <td align="left" valign="top" colspan="2" >
                   Lista: <%# _po.Lista %>
                </td>                 
            </tr>
             <tr>
                <td align="left" valign="top" colspan="2" >
                   Numero Empenho: <%# _po.NumeroEmpenho %>
                </td>                 
            </tr>
             <tr>
                <td align="left" valign="top" colspan="2" >
                   Numero Lançamento: <%# _po.NumeroLancamento %>
                </td>                 
            </tr>
             <tr>
                <td align="left" valign="top" colspan="2" >
                   Ordem Bancária: <%# _po.OrdemBancaria %>
                </td>                 
            </tr>
             <tr>
                <td align="left" valign="top" colspan="2" >
                   Valor DARF: 
                </td>                 
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2" >
                   Valor Final: 
                </td>                 
            </tr>
          </table>
                 
          
    </form>
</body>
</html>
