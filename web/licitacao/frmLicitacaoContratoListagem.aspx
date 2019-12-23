<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLicitacaoContratoListagem.aspx.cs" Inherits="frmLicitacaoContratoListagem" %>
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
        Relatório de Licitações (Contratos)        
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
            <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Status" Visible="true" SortExpression="Status"  >
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
                DataField="Observacao" Visible="True" />
            <asp:TemplateField HeaderText="Detalhes" ItemStyle-HorizontalAlign="right" Visible="true">
                <ItemTemplate>
                    <Anthem:LinkButton runat="server" Text="Detalhes" ID="lnkDetalhes" />

                    <div style="margin: 0 0 15px 10px; float: right; width: 98%">
                        
                  
                
                       <anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid" 
                             AutoGenerateColumns="false" CellPadding="3" BorderWidth="1px" HorizontalAlign="Right"  >
                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center"  />                                    
                            <ItemStyle CssClass="dgItem" />
                            <AlternatingItemStyle CssClass="dgAlternatingItem" />
                            <FooterStyle CssClass="dgFooter" />
                            <EditItemStyle HorizontalAlign="Center" />
                            <Columns>
                                 <asp:TemplateColumn HeaderText="Contrato" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# ((LicitacaoContrato)Container.DataItem).NumeroContrato %>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>  
                                <asp:TemplateColumn HeaderText="Vigência" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <%# ((LicitacaoContrato)Container.DataItem).DataAssinatura.ToShortDateString() %> a
                                        <%# ((LicitacaoContrato)Container.DataItem).DataVigencia.ToShortDateString() %>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>      
                                 <asp:TemplateColumn HeaderText="Fiscal" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# ((LicitacaoContrato)Container.DataItem).Licitacao.ServidorFiscalContrato  %>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CNPJ" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <%# ((LicitacaoContrato)Container.DataItem).Fornecedor.CNPJ %>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Contratado" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <%# ((LicitacaoContrato)Container.DataItem).Fornecedor.RazaoSocial %>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Valor Contrato" ItemStyle-HorizontalAlign="right" ItemStyle-Wrap="False">
                                    <ItemTemplate>
                                        <%# ((LicitacaoContrato)Container.DataItem).ValorContrato.ToString("C2") %>
                                    </ItemTemplate>                                                                                  
                                </asp:TemplateColumn>
                            </Columns>
                        </anthem:DataGrid>
           
                      </div>

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
