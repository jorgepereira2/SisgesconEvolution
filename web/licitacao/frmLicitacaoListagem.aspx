<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLicitacaoListagem.aspx.cs" Inherits="frmLicitacaoListagem" %>
<%@ Register TagPrefix="WebControls" Assembly="Shared.WebControls" Namespace="Shared.WebControls" %>
<%@ Register Src="~/UserControls/ucColumnManager.ascx" TagPrefix="uc" TagName="ColumnManager" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet"  />   
    
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
            <asp:TemplateField ItemStyle-HorizontalAlign="left" headertext="Itens" Visible="false"  >
                <ItemTemplate>
                
                       <anthem:DataGrid runat="server" ID="dgItem" Width="100%" CssClass="datagrid"
                             AutoGenerateColumns="false" CellPadding="3" BorderWidth="1px"  >
                            <HeaderStyle Font-Bold="true"  />                                    
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
                                 <asp:TemplateColumn HeaderText="Cotações" ItemStyle-HorizontalAlign="right">
                                    <ItemTemplate>
                                       <%# ((LicitacaoItem)Container.DataItem).Valor1.ToString("N2")%><br />
                                       <%# ((LicitacaoItem)Container.DataItem).Valor2.ToString("N2")%><br />
                                       <%# ((LicitacaoItem)Container.DataItem).Valor3.ToString("N2")%>
                                    </ItemTemplate>                                            
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Val. Estimado" ItemStyle-HorizontalAlign="right">
                                    <ItemTemplate>
                                        <%# ((LicitacaoItem)Container.DataItem).ValorMedio.ToString("N2") %>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# ((LicitacaoItem)Container.DataItem).Quantidade %>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Valor Final" ItemStyle-HorizontalAlign="right">
                                    <ItemTemplate>
                                        <%# ((LicitacaoItem)Container.DataItem).ValorFinalPregao %>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Fornecedor" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <%# ((LicitacaoItem)Container.DataItem).Fornecedor == null ? "" : ((LicitacaoItem)Container.DataItem).Fornecedor.RazaoSocial %>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>   
                                <asp:TemplateColumn HeaderText="Contrato" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# ((LicitacaoItem)Container.DataItem).Contrato == null ? "" : ((LicitacaoItem)Container.DataItem).Contrato.NumeroContrato %>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>                                
                            </Columns>
                        </anthem:DataGrid>
                    
                </ItemTemplate>
            </asp:TemplateField>             
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
            <asp:TemplateField HeaderText="Detalhes" ItemStyle-HorizontalAlign="center" Visible="true">
                <ItemTemplate>
                    <Anthem:LinkButton runat="server" Text="Detalhes" ID="lnkDetalhes" />
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
