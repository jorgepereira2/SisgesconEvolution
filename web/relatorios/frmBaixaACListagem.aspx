<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmBaixaACListagem.aspx.cs" Inherits="frmBaixaACListagem" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Demostrativo de Execução Financeira (DEF)" />
    <uc:ColumnManager runat="server" ID="ucColumn" />  
    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
    <%-- <div style="text-align:left">
            
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <b>Valor Total:</b>&nbsp;
            <Anthem:Label runat="server" ID="lblValorTotal" />
        </div>--%>
        
      <WebControls:ReportGridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid" 
         AutoGenerateColumns="false" CellPadding="3" >
        <HeaderStyle CssClass="dgHeader" />                                            
        <AlternatingRowStyle CssClass="dgAlternatingItem" />
        <FooterStyle CssClass="dgFooter" />
        <Columns>

             <asp:TemplateField HeaderText="NE" ItemStyle-HorizontalAlign="Center"  >
                <ItemTemplate>
                    <%# ((AutorizacaoCompraPagamento)Container.DataItem).AutorizacaoCompra.NumeroEmpenho %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Valor" ItemStyle-HorizontalAlign="right"  SortExpression="Valor" 
                DataField="Valor" Visible="True" HtmlEncode="false" DataFormatString="{0:C2}" />
            <asp:BoundField HeaderText="Ordem Bancária" ItemStyle-HorizontalAlign="center"  SortExpression="OrdemBancaria" 
                DataField="OrdemBancaria" Visible="True" />
            <asp:BoundField HeaderText="Valor Total" ItemStyle-HorizontalAlign="right"  SortExpression="ValorTotal" 
                DataField="ValorTotal" HtmlEncode="false" DataFormatString="{0:C2}" />
                        
            <asp:BoundField HeaderText="Valor Total" ItemStyle-HorizontalAlign="right"  SortExpression="ValorTotal" 
                DataField="ValorTotal" Visible="True" DataFormatString="{0:N2}" HtmlEncode="true" />
            <asp:BoundField HeaderText="CNPJ" ItemStyle-HorizontalAlign="center"  SortExpression="CNPJ" 
                DataField="CNPJ"  />    
            <asp:BoundField HeaderText="Fornecedor" ItemStyle-HorizontalAlign="left"  SortExpression="RazaoSocial" 
                DataField="RazaoSocial"  />                
            <asp:BoundField HeaderText="Data" ItemStyle-HorizontalAlign="center"  SortExpression="Data" 
                DataField="Data" Visible="False" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="true" />   
            <asp:BoundField HeaderText="Valor Imposto" ItemStyle-HorizontalAlign="right"  SortExpression="ValorImposto" 
                DataField="ValorImposto" Visible="False" DataFormatString="{0:N2}" HtmlEncode="true" />      
            <asp:BoundField HeaderText="Valor Desconto" ItemStyle-HorizontalAlign="right"  SortExpression="ValorDesconto" 
                DataField="ValorDesconto" Visible="False" DataFormatString="{0:N2}" HtmlEncode="true" />      
             <asp:BoundField HeaderText="Nota Fiscal" ItemStyle-HorizontalAlign="center"  SortExpression="NumeroNotaFiscal" 
                DataField="NumeroNotaFiscal" Visible="false" />
            <asp:TemplateField HeaderText="AC" ItemStyle-HorizontalAlign="Center"  Visible="false">
                <ItemTemplate>
                    <Anthem:LinkButton runat="server" ID="lnkAC" Text="<%# ((AutorizacaoCompraPagamento)Container.DataItem).AutorizacaoCompra.CodigoComAno %>" />
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

