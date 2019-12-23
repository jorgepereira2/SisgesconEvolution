<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchFaturamentoPSM.aspx.cs" Inherits="fchFaturamentoPSM" %>

<%@ Register Src="../usercontrols/CertificadoOM.ascx" TagName="Certificado" TagPrefix="uc" %>
<%@ Register TagPrefix="uc" TagName="DadosOM" Src="~/UserControls/DadosOM.ascx" %>
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
                <uc:DadosOM runat="server" Titulo="FATURA DE SERVIÇOS DA ATIVIDADE DE MERGULHO" />
            </td>
            <td align="right" width="25%">
                                   
            </td>
        </tr>
    </table>
    <br /><br />
    <div id="divExportar" align="center" runat="server" class="noprint" >
        <a runat="server" ID="lnkExportar" >Exportar p/ Word</a>
    </div>    
    <div class="PageTitle" style="width:98%;text-align:right;">
            Faturamento
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Tipo de Atividade:
                    Principal
                    </b>
                </td>
                 
            </tr>
            <tr>
                <td align="left" valign="top"  >
                    <b>Número Fatura:
                    <%# _pedido.CodigoComAno %>
                    </b>
                </td>
                 <td align="left" valign="top" >
                    <b>Data Emissão:</b>
                    <%# _pedido.DataEmissao.ToShortDateString() %>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Cliente:
                    <%# _pedido.Cliente.Descricao %></b> - <%# _pedido.Cliente.Codigo %> - <%# _pedido.Cliente.IndicativoNaval %>
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
                <td align="left" valign="top" >
                    <b>Embarcação:</b> 
                    <%# _pedido.Embarcacao == null ? "" : _pedido.Embarcacao.Descricao %>
                </td> 
                <td align="left" valign="top" >
                    <b>Prioridade:</b> 
                    <%# _pedido.Prioridade == null ? "" : _pedido.Prioridade.Descricao %>
                </td>                
            </tr> 
             <tr>
                <td align="left" valign="top" >
                    <b>Categoria:</b> 
                    <%# _pedido.CategoriaServico == null ? "" : _pedido.CategoriaServico.Descricao %>
                </td> 
                <td align="left" valign="top" >
                    <b>Localização:</b> 
                    <%# _pedido.Localizacao %>
                </td>                
            </tr> 
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Serviços:</b> 
                    <%# _pedido.DescricaoServicos %>
                </td>
            </tr>           
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Observação:</b> 
                    <%# _pedido.Observacao %>
                </td>
            </tr>
        </table>
        
      <%--     <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Cliente
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
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
                <td align="left" valign="top" colspan="2"  >
                    <b>Contatos:</b> 
                    <%# _pedido.Contatos %>
                </td>               
            </tr> 
            <tr>
                 <td align="left" valign="top" >
                    <b>Telefones:</b>
                    <%# _pedido.TelefoneContatos %>                    
                </td>
                <td align="left" valign="top" >
                    <b>Código Pedido Cliente:</b> 
                    <%# _pedido.CodigoPedidoCliente %>
                </td>                
            </tr>
           
        </table>--%>
        
    

        
                
            <br />
           <%--     <div class="PageTitle" style="width:100%;text-align:right;">
                Delineamento
            </div>
            <anthem:DataGrid runat="server" ID="dgDelineamento" Width="100%" CssClass="datagrid"
                    AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" OnItemCreated="dgDelineamento_ItemCreated" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle CssClass="dgFooter" />
                <EditItemStyle HorizontalAlign="Center" />
                <Columns>                                         
                        <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoServicoMergulhoDelineamento)Container.DataItem).Celula.Descricao  %>
                        </ItemTemplate>                                                                        
                    </asp:TemplateColumn>
                    
                      <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoServicoMergulhoDelineamento)Container.DataItem).Celula.Descricao  %>
                        </ItemTemplate>                                                                        
                    </asp:TemplateColumn>

                      <asp:TemplateColumn HeaderText="Qtd. Mergulhadores" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((PedidoServicoMergulhoDelineamento)Container.DataItem).QuantidadeMergulhadores %>
                        </ItemTemplate>                                                                        
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Nº Dias" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((PedidoServicoMergulhoDelineamento)Container.DataItem).NumeroDias %>
                        </ItemTemplate>                                                                        
                    </asp:TemplateColumn>

                        
                    <asp:TemplateColumn HeaderText="Faina Diaria" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((PedidoServicoMergulhoDelineamento)Container.DataItem).TempoFainaDiaria %>
                        </ItemTemplate> 
                        <FooterTemplate>
                        Total: <asp:Label runat="server" ID="lblTotalHH" Font-Bold="true" />
                        </FooterTemplate>                                           
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Descrição" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((PedidoServicoMergulhoDelineamento)Container.DataItem).DescricaoServicoOficina%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                </Columns>
            </anthem:DataGrid>  --%>
                
              <%--  <br />
                <div class="PageTitle" style="width:100%;text-align:right;">
                    Itens 
                </div>
                <anthem:DataGrid runat="server" ID="dgOrcamentoItem" Width="100%" CssClass="datagrid"
                     AutoGenerateColumns="false" CellPadding="3" >
                    <HeaderStyle CssClass="dgHeader" />                                    
                    <ItemStyle CssClass="dgItem" />
                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                    <FooterStyle CssClass="dgFooter" />
                    <EditItemStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoServicoMergulhoItemOrcamento)Container.DataItem).ServicoMaterial.Descricao %>
                            </ItemTemplate>                                                                                  
                        </asp:TemplateColumn> 
                        <asp:TemplateColumn HeaderText="Dados Complementares" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoServicoMergulhoItemOrcamento)Container.DataItem).Observacao%>
                            </ItemTemplate>                              
                        </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="Fornecedor" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%# ((PedidoServicoMergulhoItemOrcamento)Container.DataItem).Fornecedor%>
                            </ItemTemplate>                              
                        </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="Origem" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                               <%# Shared.Common.Util.GetDescription(((PedidoServicoMergulhoItemOrcamento)Container.DataItem).OrigemMaterial)%>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                               <%# ((PedidoServicoMergulhoItemOrcamento)Container.DataItem).Quantidade.ToString("N2")%>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="Valor Unit." ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                               <%# ((PedidoServicoMergulhoItemOrcamento)Container.DataItem).Valor.ToString("N2")%>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                               <%# ((PedidoServicoMergulhoItemOrcamento)Container.DataItem).ValorTotal.ToString("N2")%>
                            </ItemTemplate>                                            
                        </asp:TemplateColumn>
                    </Columns>
                </anthem:DataGrid>     --%>            
                
  
        <div class="PageTitle" style="width:98%;text-align:right;">
    Detalhamento
</div>  
<table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio" style="border-collapse:collapse;" border="1" rules="cols">
    <tr style="font-weight:bold;border-bottom: solid 1px black;">
        <td align="center" width="50px">
            FRE
        </td>                 
        <td align="center" width="50px">
            Item
        </td>                 
        <td align="left"  >
            Discriminação
        </td>
        <td align="right" width="190px" >
            Valor
        </td>
    </tr>
    <tr >
        <td align="center" valign="middle" rowspan="4"  >
            170
        </td>
        <td align="center"  >
            01
        </td>                 
        <td align="left"  >
           MOD
        </td>
        <td align="right"  >
            <%# _pedido.MOD.ToString("C2") %>
        </td>
    </tr>    
    <tr >
        <td align="center"  >
            02
        </td>                 
        <td align="left"  >
            MOI
        </td>
        <td align="right"  >
            <%# _pedido.MOI.ToString("C2") %>
        </td>
    </tr>            
    <tr >
        <td align="center"  >
            03
        </td>                 
        <td align="left"  >
            TOMO
        </td>
        <td align="right"  >
            <%# _pedido.TOMO.ToString("C2") %>
        </td>
    </tr>  
    <tr >
        <td align="center"  >
            A
        </td>                 
        <td align="left"  >
            (01 + 02 + 03)
        </td>
        <td align="right"  >
            <%# _pedido.A123.ToString("C2") %>
        </td>
    </tr>
    <%--<tr style="border-bottom: solid 1px black;" >
         <td align="center" >
            05
        </td>                 
        <td align="left"  >
            Desconto Mão-de-Obra
        </td>
        <td align="right"  >
            <%# _pedido.DescontoFRE170Mergulho.ToString("C2") %>
        </td>
    </tr> 
     <tr style="border-bottom: solid 1px black;" >
         <td align="center" >
            06
        </td>                 
        <td align="left"  >
            Total Mão-de-Obra
        </td>
        <td align="right"  >
            <%# _orcamento.ValorTotalMaoObra.ToString("C2") %>
        </td>
    </tr>--%>  
    <tr style="border-top: solid 1px black;">
         <td align="center" valign="middle" rowspan="6"  >
            172/171
        </td>
         <td align="center"  >
            01
        </td>                 
        <td align="left"  >
            MD
        </td>
        <td align="right"  >
            <%# _pedido.MD.ToString("C2") %>
        </td>
    </tr>
     <tr>
         <td align="center"  >
            02
        </td>                 
        <td align="left"  >
            M. Extra: 
        </td>
        <td align="right"  >
            -
        </td>
    </tr>
    <tr>
         <td align="center"  >
            03
        </td>                 
        <td align="left"  >
           C. Embracação:
        </td>
        <td align="right"  >
            <%# _pedido.CEmbarcacao.ToString("C2") %>
        </td>
    </tr>
    <tr>
         <td align="center"  >
            04
        </td>                 
        <td align="left"  >
            Desloc. MG:
        </td>
        <td align="right"  >
            <%# _pedido.DeslocamentoMG.ToString("C2") %>
        </td>
    </tr>
    <tr>
         <td align="center"  >
            05
        </td>                 
        <td align="left"  >
            Custo Indireto:
        </td>
        <td align="right"  >
            <%# _pedido.CustoIndireto.ToString("C2") %>
        </td>
    </tr>
     <tr >
        <td align="center"  >
            B
        </td>                 
        <td align="left"  >
            Sub Total (01 + 02 + 03 + 04 + 05)
        </td>
        <td align="right"  >
            <%# _pedido.B12345.ToString("C2") %>
        </td>
    </tr>    
    <tr style="border-top: solid 1px black;">
     <td align="center" valign="middle" rowspan="3"  >
           -
        </td>
         <td align="center"  >
           C
        </td>                 
        <td align="left"  >
           TCO ( 5%):	
        </td>
        <td align="right"  >
            
            <%# _pedido.TCO.ToString("C2") %>
            
        </td>
    </tr>   
    <tr>
         <td align="center"  >
            A
        </td>                 
        <td align="left"  >
           Total FRE-170:	
        </td>
        <td align="right"  >
            <b>
            <%# _pedido.TotalFRE170.ToString("C2") %>
            </b>
        </td>
    </tr>
    <tr>
         <td align="center"  >
            B + C
        </td>                 
        <td align="left"  >
           Total FRE-171 / 172:		
        </td>
        <td align="right"  >
            <b>
            <%# _pedido.TotalFRE171172.ToString("C2") %>
            </b>
        </td>
    </tr>
     <tr style="border-top: solid 1px black;">
     <td align="center" valign="middle" rowspan="3"  >
           -
        </td>
         <td align="center"  >
           
        </td>                 
        <td align="left"  >
           Desconto FRE-170:	
        </td>
        <td align="right"  >
            
            <%# _pedido.DescontoConcedidoFRE170.ToString("C2") %>
            
        </td>
    </tr>   
    <tr>
         <td align="center"  >
           
        </td>                 
        <td align="left"  >
           Desconto FRE-171/172:	
        </td>
        <td align="right"  >
            
            <%# _pedido.DescontoConcedidoFRE171172.ToString("C2") %>
            
        </td>
    </tr>
    <tr>
         <td align="center"  >
           
        </td>                 
        <td align="left"  >
           Total a Pagar:		
        </td>
        <td align="right"  >
            <b>
            <%# _pedido.TotalAPagar.ToString("C2") %>
            </b>
        </td>
    </tr>

    
</table>   
<br />
<p>
 Obs: Ao pagamento posterior a data de vencimento poderá ser realizado um faturamento suplementar de atualização monetária acordo SGM301-Vol IV-16.11.						
</p>

<br/>
<uc:Certificado runat="server" />

<br />
        <br />
        <br />
        <div style="text-align:center">
        ______________________________________<br />
        <%# _pedido.Celula.GetDepartamento().Descricao %>
        </div>          
  
     
        
    </form>
</body>
</html>
