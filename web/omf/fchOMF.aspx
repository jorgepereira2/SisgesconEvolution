<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchOMF.aspx.cs" Inherits="fchOMF" %>
<%@ Import namespace="System.Collections.Generic"%>
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
                 <b >OMF</b>
                <br />
                <br />
                <b>
                    
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
                <td align="left" valign="top"  >
                    <b>Número Nota:</b> 
                    <%# _omf.NumeroNota %>
                </td>
                 <td align="left" valign="top" >
                    <b>Data Entrega:</b> 
                    <%# _omf.DataEntrega.ToShortDateString() %>
                </td>
            </tr>            
            <tr>
                <td align="left" valign="top"  >
                    <b>Recebedor:</b> 
                    <%# _omf.Recebedor.Identificacao %>
                </td>
                <td align="left" valign="top" >
                    <b>Status:</b>
                    <%# _omf.Status.Descricao %>
                    
                </td>
            </tr>              
            <tr>
                <td align="left" valign="top"  >
                    <b>Fornecedor:</b>
                    <%# _omf.Fornecedor %>
                </td>
                <td align="left" valign="top" >
                    <b>Nota Empenho:</b> 
                    <%# _omf.NumeroEmpenho %>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top"  >
                    <b>Tipo Emprego:</b>
                    <%# _omf.TipoEmprego %>
                </td>
                <td align="left" valign="top" >
                    <b>Mensagem Solicitação Perícia:</b> 
                    <%# _omf.MensagemSolicitacaoPericia %>
                </td>
            </tr>
        </table>
        <br />
        <br />
         <div class="PageTitle" style="width:98%;text-align:right;">
            Fornecedor
        </div>  
     
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
                            <%# ((NotaEntregaMaterialOMFItem)Container.DataItem).Material.Descricao %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((NotaEntregaMaterialOMFItem)Container.DataItem).Quantidade.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="LOC" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((NotaEntregaMaterialOMFItem)Container.DataItem).LOC%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tipo" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# Shared.Common.Util.GetDescription(((NotaEntregaMaterialOMFItem)Container.DataItem).TipoTAV)%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((NotaEntregaMaterialOMFItem)Container.DataItem).Valor.ToString("N2")%>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>                   
                    <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                           <%# ((NotaEntregaMaterialOMFItem)Container.DataItem).ValorTotal.ToString("C2")%>
                        </ItemTemplate>
                        <FooterTemplate>                                                
                            <b><%# _omf.ValorTotal.ToString("C2") %></b>
                        </FooterTemplate>                                            
                    </asp:TemplateColumn>                                                                                                                      
                </Columns>
            </anthem:DataGrid> 
            
             <br />
             
        <asp:Panel runat="server" ID="pnResponsavel">
        <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Responsáveis Perícia
        </div>  
       <anthem:DataGrid runat="server" ID="dgResponsavel" Width="98%" CssClass="datagrid"
                 AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
                <HeaderStyle CssClass="dgHeader" />                                    
                <ItemStyle CssClass="dgItem" />
                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Nome" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).Nome %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="NIP" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).NIP %>
                        </ItemTemplate>                                            
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Graduação" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).Graduacao %>
                        </ItemTemplate>
                    </asp:TemplateColumn>                                                                                                                      
                    <asp:TemplateColumn HeaderText="Tipo Notificação" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                           <%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).TipoNotificacao.Descricao %>
                        </ItemTemplate>
                    </asp:TemplateColumn>                                                                                                                      
                </Columns>
            </anthem:DataGrid>            
        </asp:Panel>         
         
            <br />
        <div class="PageTitle" style="width:98%;text-align:right;">
            Histórico
        </div>  
        <Anthem:DataList runat="server" ID="dlHistorico">
            <ItemTemplate>
                <b><%# ((HistoricoOMF)Container.DataItem).Servidor.Identificacao%></b> - 
                <%# ((HistoricoOMF)Container.DataItem).Data.ToString("dd/MM/yyyy hh:mm")%><br />
                
                <b>Alteração:</b> <%# ((HistoricoOMF)Container.DataItem).Descricao%>
                <br />
                <b>Comentário:</b>
                <%# ((HistoricoOMF)Container.DataItem).Justificativa%>
                <hr />
            </ItemTemplate>                                
        </Anthem:DataList>
    </form>
</body>
</html>
