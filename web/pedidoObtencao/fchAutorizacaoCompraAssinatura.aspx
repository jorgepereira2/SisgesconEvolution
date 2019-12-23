<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchAutorizacaoCompraAssinatura.aspx.cs" Inherits="fchAutorizacaoCompraAssinatura" %>
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
            Autorização De Compra/Ordem de execução do serviço No <%# _ac.CodigoComAno %>            
            </b>
        </div>
        <br />
        
        <br />
        <div style="text-align:left">
        <b>Data:</b> <%# _ac.DataEmissao.ToShortDateString() %>
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
                    <b>Licitação:</b>  <%# _ac.Licitacao == null ? "" : _ac.Licitacao.NumeroPregao %>                    
                </td>     
                <td align="left" valign="top" >
                    <b>Natureza Despesa:</b> <%# (_ac.NaturezaDespesa != null ? _ac.NaturezaDespesa.Codigo + " - " : "") +_ac.NaturezaDespesa%>
                </td>              
                <td align="left" valign="top" >
                   <b> Comprador:</b> <%# _ac.Servidor %>
                </td>                                 
            </tr>            
            <tr>
                <td align="left" valign="top" colspan="2" >
                    <b>Ação Interna</b> <%# _ac.Projeto %>
                </td>                 
                <td align="left" valign="top" colspan="2" >
                    <b>AC's associados:</b> <%# _ac.TextoPO %>
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
                    <%# _ac.Fornecedor.RazaoSocial %>
                    
                </td>                 
                <td>
                    <b>Simples:</b>
                    <%# _ac.Fornecedor.FlagOptanteSimples ? "Sim" : "Não" %>
                </td>                 
            </tr>            
            <tr>
                <td align="left" valign="top"  >
                    <b>CNPJ:</b>
                    <%# _ac.Fornecedor.CNPJ %>
                </td>
                <td align="left" valign="top" >
                    <b>Telefone:</b>
                    <%# _ac.Fornecedor.Telefone %>                     
                </td>
            </tr>  
            <tr>                
                <td align="left" valign="top" colspan="2" >
                    <b>Endereço:</b>
                    <%# _ac.Fornecedor.Endereco %>
                    -
                    <%# _ac.Fornecedor.Bairro %>
                    &nbsp;
                   
                    <%# _ac.Fornecedor.Municipio %>
                   
                    &nbsp;
                    <%# _ac.Fornecedor.Estado %>
                </td>                
            </tr>
            <tr>
                <td align="left" valign="top"  >
                    <b>CEP:</b> 
                    <%# _ac.Fornecedor.CEP %>
                </td>
                <td align="left" valign="top" >
                    <b>E-mail:</b>
                    <%# _ac.Fornecedor.Email %>                     
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Contatos:</b> 
                    <%# _ac.Fornecedor.DescricaoContatos %>
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
                            <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.CodigoInterno %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
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
                    <asp:TemplateColumn HeaderText="UN" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.Unidade.Descricao %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((PedidoCotacaoItem)Container.DataItem).Quantidade.ToString("N2") %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Valor Unit." ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((PedidoCotacaoItem)Container.DataItem).Valor.ToString("N2") %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                   
                    <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((PedidoCotacaoItem)Container.DataItem).ValorTotal.ToString("C2") %>
                        </ItemTemplate>
                        <FooterTemplate>                                                
                            <b><%# _ac.ValorTotal.ToString("C2") %></b>
                        </FooterTemplate>                                            
                    </asp:TemplateColumn>                                                                                                                      
                </Columns>
            </anthem:DataGrid> 
            
             <br />
             
         <div style="text-align:left">
         Aplicação: <%# _ac.Observacao %>
         </div>       
    
     <br /><br />
     <table width="98%" border="0">
        <tr>
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
     </table>
         <br />
          <table style="width:98%" cellpadding="2" cellspacing="3" border="0" >
            <tr>
                <td align="left" valign="top" colspan="2" >
                   Cód. Gestão: <%# _ac.CodigoGestao %>
                </td>                 
            </tr>
             <tr>
                <td align="left" valign="top" colspan="2" >
                   Lista: <%# _ac.Lista %>
                </td>                 
            </tr>
             <tr>
                <td align="left" valign="top" colspan="2" >
                   Numero Empenho: <%# _ac.NumeroEmpenho %>
                </td>                 
            </tr>
             <tr>
                <td align="left" valign="top" colspan="2" >
                   Numero Lançamento: <%# _ac.NumeroLancamento %>
                </td>                 
            </tr>
             <tr>
                <td align="left" valign="top" colspan="2" >
                   Ordem Bancária: <%# _ac.OrdemBancaria %>
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
