<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLicitacaoContratoListagemExcel.aspx.cs" Inherits="frmLicitacaoContratoListagemExcel" %>
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
                    <%# ((LicitacaoContrato)Container.DataItem).Licitacao.NumeroPregao %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Emissão" SortExpression="DataEmissao"  >
                <ItemTemplate>
                    <%# ((LicitacaoContrato)Container.DataItem).Licitacao.DataEmissao.ToShortDateString() %>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Status" Visible="true" SortExpression="Status"  >
                <ItemTemplate>
                    <%# Shared.Common.Util.GetDescription(((LicitacaoContrato)Container.DataItem).Licitacao.Status) %>
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Sistema Licitatório" Visible="false"  >
                <ItemTemplate>
                    <%# Shared.Common.Util.GetDescription(((LicitacaoContrato)Container.DataItem).Licitacao.SistemaLicitatorio) %>
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Processo Licitatório" Visible="false"  >
                <ItemTemplate>
                    <%# Shared.Common.Util.GetDescription(((LicitacaoContrato)Container.DataItem).Licitacao.ProcessoLicitatorio) %>
                </ItemTemplate>
            </asp:TemplateField> 
            
             <asp:TemplateField ItemStyle-HorizontalAlign="left" headertext="Objeto" Visible="True"  >
                <ItemTemplate>
                    <%# ((LicitacaoContrato)Container.DataItem).Licitacao.Objetivo %>
                </ItemTemplate>
            </asp:TemplateField> 
            
             <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Data Pregão" Visible="True"  >
                <ItemTemplate>
                    <%# ((LicitacaoContrato)Container.DataItem).Licitacao.DataPregao.HasValue ? ((LicitacaoContrato)Container.DataItem).Licitacao.DataPregao.Value.ToShortDateString() : "" %>
                </ItemTemplate>
            </asp:TemplateField> 
            
             <asp:TemplateField ItemStyle-HorizontalAlign="right" headertext="Valor Estimado" Visible="True"  >
                <ItemTemplate>
                    <%# ((LicitacaoContrato)Container.DataItem).Licitacao.ValorTotalEstimado.ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField> 
            
             <asp:TemplateField ItemStyle-HorizontalAlign="right" headertext="Valor Final" Visible="True"  >
                <ItemTemplate>
                    <%# ((LicitacaoContrato)Container.DataItem).Licitacao.ValorTotalFinal.ToString("N2") %>
                </ItemTemplate>
            </asp:TemplateField> 
            
            <asp:TemplateField ItemStyle-HorizontalAlign="left" headertext="Número CI" Visible="False"  >
                <ItemTemplate>
                    <%# ((LicitacaoContrato)Container.DataItem).Licitacao.NumeroCI %>
                </ItemTemplate>
            </asp:TemplateField> 
            
            <asp:TemplateField ItemStyle-HorizontalAlign="left" headertext="Observação" Visible="True"  >
                <ItemTemplate>
                    <%# ((LicitacaoContrato)Container.DataItem).Licitacao.Observacao %>
                </ItemTemplate>
            </asp:TemplateField> 
            
            <%-- <asp:BoundField HeaderText="NUP" ItemStyle-HorizontalAlign="left"  SortExpression="NUP"  DataField="NUP" Visible="false" />--%>
            
            <asp:TemplateField HeaderText="Contrato" ItemStyle-HorizontalAlign="center">
                <ItemTemplate>
                    <%# ((LicitacaoContrato)Container.DataItem).NumeroContrato %>
                </ItemTemplate>                                                                                  
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Vigência" ItemStyle-HorizontalAlign="left">
                <ItemTemplate>
                    <%# ((LicitacaoContrato)Container.DataItem).DataAssinatura.ToShortDateString() %> a
                    <%# ((LicitacaoContrato)Container.DataItem).DataVigencia.ToShortDateString() %>
                </ItemTemplate>                                                                                  
            </asp:TemplateField>      
                <asp:TemplateField HeaderText="Fiscal" ItemStyle-HorizontalAlign="center">
                <ItemTemplate>
                    <%# ((LicitacaoContrato)Container.DataItem).Licitacao.ServidorFiscalContrato  %>
                </ItemTemplate>                                                                                  
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CNPJ" ItemStyle-HorizontalAlign="left">
                <ItemTemplate>
                    <%# ((LicitacaoContrato)Container.DataItem).Fornecedor.CNPJ %>
                </ItemTemplate>                                                                                  
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contratado" ItemStyle-HorizontalAlign="left">
                <ItemTemplate>
                    <%# ((LicitacaoContrato)Container.DataItem).Fornecedor.RazaoSocial %>
                </ItemTemplate>                                                                                  
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Valor Contrato" ItemStyle-HorizontalAlign="right" ItemStyle-Wrap="False">
                <ItemTemplate>
                    <%# ((LicitacaoContrato)Container.DataItem).ValorContrato.ToString("C2") %>
                </ItemTemplate>                                                                                  
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Detalhes" ItemStyle-HorizontalAlign="right" Visible="true">
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
