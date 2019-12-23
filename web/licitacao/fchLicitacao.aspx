<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchLicitacao.aspx.cs" Inherits="fchLicitacao" %>
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
        Detalhes do Licitação        
    </div>
    <br /><br />
    <div class="PageTitle" style="width:98%;text-align:right;">
            Dados Básicos
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top"  >
                    <b>Número Processo:
                    <%# _licitacao.NumeroPregao %>
                    </b> 
                </td>
                 <td align="left" valign="top" >
                    <b>Data Pregão:</b> 
                    <%# _licitacao.DataPregao.HasValue ? _licitacao.DataPregao.Value.ToShortDateString() : "" %>
                </td>               
            </tr>
            <tr>
                <td align="left" valign="top"  >
                    <b>Data Emissão:</b> 
                    <%# _licitacao.DataEmissao.ToShortDateString() %>
                </td>
                <td align="left" valign="top" >
                    <b>Status:</b> 
                    <%# Shared.Common.Util.GetDescription(_licitacao.Status) %>
                </td>
            </tr>
             <tr>
                <td align="left" valign="top"  >
                    <b>Tipo Licitação:</b> 
                    <%# _licitacao.TipoLicitacao %>
                </td>
                <td align="left" valign="top" >
                    <b>Modalidade Licitação:</b> 
                    <%# _licitacao.ModalidadePregao %>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top"  >
                    <b>Sistema Licitatório:</b> 
                    <%# Shared.Common.Util.GetDescription(_licitacao.SistemaLicitatorio) %>
                </td>
                <td align="left" valign="top" >
                    <b>Processo Licitatório:</b> 
                    <%# Shared.Common.Util.GetDescription(_licitacao.ProcessoLicitatorio) %>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top"  >
                    <b>Número CI:</b> 
                    <%# _licitacao.NumeroCI %>
                </td>
                <td align="left" valign="top" >
                    <b>NUP:</b> 
                    <%# _licitacao.NUP %>
                </td>
            </tr>
            <tr>                
                <td align="left" valign="top" colspan="2" >
                    <b>Servidor Fiscal:</b> 
                    <%# _licitacao.ServidorFiscalContrato %>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Objeto:</b> 
                    <%# _licitacao.Objetivo %>
                </td>                            
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <b>Observação:</b> 
                    <%# _licitacao.Observacao %>
                </td>
            </tr>
             <tr>
                <td align="left" valign="top"  >
                    <b>Valor Total Estimado:</b> 
                    <%# _licitacao.ValorTotalEstimado.ToString("C2") %>
                </td>
                <td align="left" valign="top" >
                    <b>Valor Total Final:</b> 
                    <%# _licitacao.ValorTotalFinal.ToString("C2") %>
                </td>
            </tr>                    
        </table>
        
           
              <br />
            <div class="PageTitle" style="width:98%;text-align:right;">
                Itens
            </div>  
           <anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle CssClass="dgFooter" />
                <EditItemStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((LicitacaoItem)Container.DataItem).Material.DescricaoCompleta %>
                           
                           
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn HeaderText="NEB" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((LicitacaoItem)Container.DataItem).Material.CodigoInterno %>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>                    
                     <asp:TemplateColumn HeaderText="Cotações" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((LicitacaoItem)Container.DataItem).Valor1.ToString("N2")%><br />
                           <%# ((LicitacaoItem)Container.DataItem).Valor2.ToString("N2")%><br />
                           <%# ((LicitacaoItem)Container.DataItem).Valor3.ToString("N2")%><br />
                           <%# ((LicitacaoItem)Container.DataItem).Valor4.ToString("N2")%><br />
                           <%# ((LicitacaoItem)Container.DataItem).Valor5.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Val. Estimado" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                            <%# ((LicitacaoItem)Container.DataItem).ValorMedio.ToString("N2") %>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((LicitacaoItem)Container.DataItem).Quantidade.ToString("N2") %>
                            &nbsp;
                             <%# ((LicitacaoItem)Container.DataItem).Material.Unidade.Descricao %>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Valor Final" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                            <%# ((LicitacaoItem)Container.DataItem).ValorFinalPregao.ToString("N2") %>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Fornecedor" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# ((LicitacaoItem)Container.DataItem).Contrato == null ? "" : ((LicitacaoItem)Container.DataItem).Contrato.Fornecedor.RazaoSocial %>
                        </ItemTemplate>                                                                                  
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Saldo Disponível" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((LicitacaoItem)Container.DataItem).QuantidadeDisponivel.ToString("N2") %>
                        </ItemTemplate>                                 
                    </asp:TemplateColumn>
                </Columns>
            </anthem:DataGrid>      
          
            <br />
            <div class="PageTitle" style="width:98%;text-align:right;">
                Histórico
            </div> 
          <Anthem:DataList runat="server" ID="dlHistorico">
    <ItemTemplate>
        <b><%# ((HistoricoLicitacao)Container.DataItem).Servidor.Identificacao %></b> - 
        <%# ((HistoricoLicitacao)Container.DataItem).Data.ToString("dd/MM/yyyy HH:mm")%><br />
        
        <b>Alteração:</b> <%# ((HistoricoLicitacao)Container.DataItem).Descricao%>
        <br />
        <b>Comentário:</b>
        <%# ((HistoricoLicitacao)Container.DataItem).Justificativa%>
        <hr />
    </ItemTemplate>                                
</Anthem:DataList>
    </form>
</body>
</html>
