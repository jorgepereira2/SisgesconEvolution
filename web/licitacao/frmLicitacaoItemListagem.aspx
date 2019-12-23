<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLicitacaoItemListagem.aspx.cs" Inherits="frmLicitacaoItemListagem" %>
<%@ Register TagPrefix="WebControls" Assembly="Shared.WebControls" Namespace="Shared.WebControls" %>
<%@ Register Src="~/UserControls/ucColumnManager.ascx" TagPrefix="uc" TagName="ColumnManager" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet"  />   
    
    <style>
        .red {
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div align="center" style="width:90%" class="ReportTitle">
    <br />
        Relatório de Licitações        
    </div>
    <uc:ColumnManager runat="server" ID="ucColumn" />  
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <WebControls:ReportGridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid" 
         AutoGenerateColumns="false" CellPadding="3" >
        <HeaderStyle CssClass="dgHeader" />                                            
        <AlternatingRowStyle CssClass="dgAlternatingItem" />
        <FooterStyle CssClass="dgFooter" />
        <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Número"  SortExpression="NumeroPregao" >
                <ItemTemplate>
                    <%# ((Licitacao)Container.DataItem).NumeroPregao %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Emissão" SortExpression="DataEmissao"  >
                <ItemTemplate>
                    <%# ((Licitacao)Container.DataItem).DataEmissao.ToShortDateString() %>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Status" Visible="true"  >
                <ItemTemplate>
                    <%# Shared.Common.Util.GetDescription(((Licitacao)Container.DataItem).Status) %>
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Sistema Licitatório" Visible="false"  >
                <ItemTemplate>
                    <%# Shared.Common.Util.GetDescription(((Licitacao)Container.DataItem).SistemaLicitatorio) %>
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Processo Licitatório" Visible="false"  >
                <ItemTemplate>
                    <%# Shared.Common.Util.GetDescription(((Licitacao)Container.DataItem).ProcessoLicitatorio) %>
                </ItemTemplate>
            </asp:TemplateField> 
             <asp:BoundField HeaderText="Objeto" ItemStyle-HorizontalAlign="left"  SortExpression="Objetivo" 
                DataField="Objetivo" Visible="True" />
                     
            <asp:BoundField HeaderText="Data Pregão" ItemStyle-HorizontalAlign="center"  SortExpression="DataPregao" 
                DataField="DataPregao" Visible="True" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false"  />
            <asp:BoundField HeaderText="Valor Estimado" ItemStyle-HorizontalAlign="right"  SortExpression="ValorTotalEstimado" 
                DataField="ValorTotalEstimado" Visible="True" DataFormatString="{0:c2}" HtmlEncode="false"  />
            <asp:BoundField HeaderText="ValorFinal" ItemStyle-HorizontalAlign="right"  SortExpression="ValorTotalFinal" 
                DataField="ValorTotalFinal" Visible="True" DataFormatString="{0:c2}" HtmlEncode="false"  />
             <asp:BoundField HeaderText="Número CI" ItemStyle-HorizontalAlign="left"  SortExpression="NumeroCI"  DataField="NumeroCI" Visible="false" />
             <asp:BoundField HeaderText="NUP" ItemStyle-HorizontalAlign="left"  SortExpression="NUP"  DataField="NUP" Visible="false" />
            <asp:BoundField HeaderText="Observação" ItemStyle-HorizontalAlign="left"  SortExpression="Observacao" 
                DataField="Observacao" Visible="false" />
            
           

            <asp:TemplateField HeaderText="Detalhes" ItemStyle-HorizontalAlign="right" Visible="true">
                <ItemTemplate>
                    <Anthem:LinkButton runat="server" Text="Detalhes" ID="lnkDetalhes" />

                
                
                       <anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
                             AutoGenerateColumns="false" CellPadding="3" BorderWidth="1px"  
                           OnItemDataBound="dgItem_ItemDataBound">
                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center"  />                                    
                            <ItemStyle CssClass="dgItem" />
                            <AlternatingItemStyle CssClass="dgAlternatingItem" />
                            <FooterStyle CssClass="dgFooter" />
                            <EditItemStyle HorizontalAlign="Center" />
                            <Columns>
                                 <asp:TemplateColumn HeaderText="Código" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                         <span style="color:<%# ((LicitacaoItem)Container.DataItem).QuantidadeUtilizada >= ((LicitacaoItem)Container.DataItem).Quantidade *0.8m ? "red" : "black" %>">
                                        <%# ((LicitacaoItem)Container.DataItem).Material.CodigoInterno %>
                                        </span>
                                        
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>  
                                <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <span style="color:<%# ((LicitacaoItem)Container.DataItem).QuantidadeUtilizada >= ((LicitacaoItem)Container.DataItem).Quantidade *0.8m ? "red" : "black" %>">
                                        <%# ((LicitacaoItem)Container.DataItem).Material.Descricao %>
                                        </span>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>      
                                 <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <span style="color:<%# ((LicitacaoItem)Container.DataItem).QuantidadeUtilizada >= ((LicitacaoItem)Container.DataItem).Quantidade *0.8m ? "red" : "black" %>">
                                        <%# (((LicitacaoItem)Container.DataItem).Quantidade).ToString("N0")%>
                                        </span>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Valor Final" ItemStyle-HorizontalAlign="right">
                                    <ItemTemplate>
                                        <span style="color:<%# ((LicitacaoItem)Container.DataItem).QuantidadeUtilizada >= ((LicitacaoItem)Container.DataItem).Quantidade *0.8m ? "red" : "black" %>">
                                        <%# (((LicitacaoItem)Container.DataItem).ValorFinalPregao).ToString("N2")%>
                                        </span>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Saldos" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        
                                        <table border="0" style="color:<%# ((LicitacaoItem)Container.DataItem).QuantidadeUtilizada >= ((LicitacaoItem)Container.DataItem).Quantidade *0.8m ? "red" : "black" %>">
                                            <tr  >
                                                <td>
                                                Total:
                                                </td>
                                                <td align="right" nowrap="nowrap">
                                                <%# (((LicitacaoItem)Container.DataItem).ValorFinalPregao * ((LicitacaoItem)Container.DataItem).Quantidade).ToString("C2")%>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                Utilizado:
                                                </td>
                                                <td align="right">
                                                <%# (((LicitacaoItem)Container.DataItem).ValorFinalPregao * ((LicitacaoItem)Container.DataItem).QuantidadeUtilizada).ToString("C2")%>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                Disponível:
                                                </td>
                                                <td align="right" >
                                                <%# (((LicitacaoItem)Container.DataItem).ValorFinalPregao * ((LicitacaoItem)Container.DataItem).QuantidadeDisponivel).ToString("C2")%>
                                                </td>
                                            </tr>
                                        </table>
                                    
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>    
                                  <asp:TemplateColumn HeaderText="Saldos Qtd." ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <table border="0" style="color:<%# ((LicitacaoItem)Container.DataItem).QuantidadeUtilizada >= ((LicitacaoItem)Container.DataItem).Quantidade *0.8m ? "red" : "black" %>">
                                            <tr  >
                                                <td>
                                                Total:
                                                </td>
                                                <td align="right" nowrap="nowrap">
                                                <%#  ((LicitacaoItem)Container.DataItem).Quantidade.ToString("N0")%>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                Utilizado:
                                                </td>
                                                <td align="right">
                                                <%# ((LicitacaoItem)Container.DataItem).QuantidadeUtilizada.ToString("N0")%>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    <b>
                                                    Disponível:        
                                                    </b>
                                                </td>
                                                <td align="right" >
                                                    <span style="font-weight:bold;color:<%# ((LicitacaoItem)Container.DataItem).QuantidadeUtilizada >= ((LicitacaoItem)Container.DataItem).Quantidade / 2 ? "red" : "black" %>">
                                                <%# ((LicitacaoItem)Container.DataItem).QuantidadeDisponivel.ToString("N0")%>
                                                        </span>
                                                </td>
                                            </tr>
                                        </table>
                                    
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>    
                                
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="center" headertext="POs" Visible="true"  >
                                <ItemTemplate>
                                    <asp:DataList runat="server" RepeatDirection="Vertical" id="dlPO" OnItemDataBound="dlPO_ItemDataBound">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" Text="<%# ((PedidoObtencao) Container.DataItem).Numero %>" ID="lnkPO" />
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ItemTemplate>
                            </asp:TemplateColumn>                                  
                            </Columns>
                        </anthem:DataGrid>
           


                </ItemTemplate>                                                                                  
            </asp:TemplateField>
        </Columns>
    </WebControls:ReportGridView>  
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
