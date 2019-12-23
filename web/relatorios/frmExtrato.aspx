<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmExtrato.aspx.cs" Inherits="frmExtrato" %>
<%@ Import Namespace="System.Data"%>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Extrato de Movimento Financeiro" />
    <uc:ColumnManager runat="server" ID="ucColumn" />  
    <div>
    <br />
    <table width="100%" border="0">
        <tr>
            <td style="width:50%; text-align:left">
                Ação Interna: <asp:Label runat="server" ID="lblProjeto" />
            </td>
            <td style="text-align:right">
                Natureza Despesa: <asp:Label runat="server" ID="lblNaturezaDespesa" />
            </td>
        </tr>
        <tr>
            <td style="text-align:left">
                Fonte Recurso: <asp:Label runat="server" ID="lblFonteRecurso" />
            </td>
            <td style="text-align:right">
                PTRES: <asp:Label runat="server" ID="lblPTRES" />
            </td>
        </tr>
        <tr>
            <td style="text-align:left" >
                Data: <asp:Label runat="server" ID="lblDataInicio" /> a <asp:Label runat="server" ID="lblDataFim" />
            </td>
            <td style="text-align:right">
                <b>Saldo Inicial</b>: <asp:Label runat="server" ID="lblSaldoInicial" />
            </td>
        </tr>
    </table>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <WebControls:ReportGridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid" 
         AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
        <HeaderStyle CssClass="dgHeader" />                                            
        <AlternatingRowStyle CssClass="dgAlternatingItem" />
        <FooterStyle CssClass="dgFooter" />
        <Columns>
             
              <asp:TemplateField HeaderText="Documento" ItemStyle-HorizontalAlign="left"  Visible="true" FooterStyle-HorizontalAlign="left">
                <ItemTemplate>
                    <%# ((EntradaValores)Container.DataItem).NumeroDocumento %> 
                </ItemTemplate>
                <FooterTemplate>
                    <b>Saldo Final</b>
                </FooterTemplate>
            </asp:TemplateField>
             
            <asp:TemplateField HeaderText="Tipo" ItemStyle-HorizontalAlign="center"  Visible="true">
                <ItemTemplate>
                    <%# ((EntradaValores)Container.DataItem).TipoMovimentoFinanceiro == TipoMovimentoFinanceiro.Entrada ? "Entrada" : "Saída" %> 
                    <%# ((EntradaValores)Container.DataItem).TipoOperacaoFinanceira == TipoOperacaoFinanceira.Transferencia ? " Transf." : "" %> 
                </ItemTemplate>
            </asp:TemplateField>
                        
            <asp:TemplateField HeaderText="Data" ItemStyle-HorizontalAlign="center"  Visible="true" FooterStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <%# ((EntradaValores)Container.DataItem).Data.ToShortDateString() %> 
                </ItemTemplate>               
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="right"  Visible="true" FooterStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <%# ((EntradaValores)Container.DataItem).Valor.ToString("c") %> 
                </ItemTemplate>
                <FooterTemplate>
                    <b><%# GetSaldoFinal() %> </b>
                </FooterTemplate>
            </asp:TemplateField>
            
        </Columns>
    </WebControls:ReportGridView>  
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
