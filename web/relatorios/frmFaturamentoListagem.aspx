<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFaturamentoListagem.aspx.cs" Inherits="frmFaturamentoListagem" %>
<%@ Register TagPrefix="WebControls" Assembly="Shared.WebControls" Namespace="Shared.WebControls" %>
<%@ Register Src="~/UserControls/ucColumnManager.ascx" TagPrefix="uc" TagName="ColumnManager" %>
<%@ Register Src="~/UserControls/CabecalhoRelatorio.ascx" TagPrefix="uc" TagName="Cabecalho" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Listagem de Faturamento" />
    <uc:ColumnManager runat="server" ID="ucColumn" />  
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <WebControls:ReportGridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid" 
         AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
        <HeaderStyle CssClass="dgHeader" />                                            
        <AlternatingRowStyle CssClass="dgAlternatingItem" />
        <FooterStyle CssClass="dgFooter" />
        <Columns>
            <asp:TemplateField HeaderText="PS" ItemStyle-HorizontalAlign="center"  SortExpression="DelineamentoOrcamento" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamentoFaturamento)Container.DataItem).DelineamentoOrcamento.CodigoComAno %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Equipamento" ItemStyle-HorizontalAlign="left" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamentoFaturamento)Container.DataItem).DelineamentoOrcamento.DescricaoEquipamentos %> 
                </ItemTemplate>
            </asp:TemplateField>            
             <asp:BoundField HeaderText="Número" ItemStyle-HorizontalAlign="center"  SortExpression="CodigoComAno" 
                DataField="CodigoComAno" Visible="True" />
            <asp:BoundField HeaderText="Data" ItemStyle-HorizontalAlign="center"  SortExpression="Data" 
                DataField="Data" Visible="True" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />            
             <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="right" SortExpression="Valor">
                <ItemTemplate>
                    <%# ((DelineamentoOrcamentoFaturamento)Container.DataItem).Valor.ToString("N2") %>                    
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label runat="server" ID="lblValorTotal" Font-Bold="true" />
                </FooterTemplate>
            </asp:TemplateField>    
            <asp:BoundField HeaderText="Validade" ItemStyle-HorizontalAlign="left"  SortExpression="Validade" 
                DataField="Validade" Visible="True" />
            <asp:BoundField HeaderText="Garantia" ItemStyle-HorizontalAlign="left"  SortExpression="Garantia" 
                DataField="Garantia" Visible="True" />
            <asp:TemplateField HeaderText="Célula" ItemStyle-HorizontalAlign="left" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamentoFaturamento)Container.DataItem).DelineamentoOrcamento.PedidoServico.Celula %>                    
                </ItemTemplate>
            </asp:TemplateField>    
             <asp:TemplateField HeaderText="Comprometimento" ItemStyle-HorizontalAlign="left" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamentoFaturamento)Container.DataItem).DelineamentoOrcamento.ComprometimentoCliente %>                    
                </ItemTemplate>
            </asp:TemplateField>    
            <asp:TemplateField HeaderText="SubTotal MO" ItemStyle-HorizontalAlign="right" >
                <ItemTemplate>
                    <%# ((DelineamentoOrcamentoFaturamento)Container.DataItem).DelineamentoOrcamento.SubTotalMaoObra.ToString("N2") %>                    
                </ItemTemplate>
            </asp:TemplateField>                
            <asp:BoundField HeaderText="Observação" ItemStyle-HorizontalAlign="left"  SortExpression="Observacao" 
                DataField="Observacao" Visible="False" />
        </Columns>
    </WebControls:ReportGridView>  
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
