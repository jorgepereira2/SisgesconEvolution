<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmExtratoMovimentoConta.aspx.cs" Inherits="frmExtratoMovimentoConta" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Extrato de Conta" />
    <uc:ColumnManager runat="server" ID="ucColumn" />  
    <div>
    <br />
    <table width="100%" border="0">
        <tr>            
            <td style="text-align:left" colspan="2" >
                Data: <asp:Label runat="server" ID="lblDataInicio" /> a <asp:Label runat="server" ID="lblDataFim" />
            </td>
        </tr>      
        <tr>
            <td style="width:50%; text-align:left">
                Conta: <asp:Label runat="server" ID="lblConta" />
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
                    <%# ((MovimentoConta)Container.DataItem).NumeroDocumento %> 
                </ItemTemplate>
                <FooterTemplate>
                    <b>Saldo Final</b>
                </FooterTemplate>
            </asp:TemplateField>
             
            <asp:TemplateField HeaderText="Tipo" ItemStyle-HorizontalAlign="center"  Visible="true">
                <ItemTemplate>
                    <%# ((MovimentoConta)Container.DataItem).TipoMovimentoFinanceiro == TipoMovimentoFinanceiro.Entrada ? "Entrada" : "Saída"%> 
                    <%# ((MovimentoConta)Container.DataItem).TipoOperacaoFinanceira == TipoOperacaoFinanceira.Transferencia ? " Transf." : ""%> 
                </ItemTemplate>
            </asp:TemplateField>
                        
            <asp:TemplateField HeaderText="Data" ItemStyle-HorizontalAlign="center"  Visible="true" FooterStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <%# ((MovimentoConta)Container.DataItem).Data.ToShortDateString()%> 
                </ItemTemplate>               
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="right"  Visible="true" FooterStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <%# ((MovimentoConta)Container.DataItem).Valor.ToString("c")%> 
                </ItemTemplate>
                <FooterTemplate>
                    <b><%# GetSaldoFinal() %> </b>
                </FooterTemplate>
            </asp:TemplateField>
            
             <asp:TemplateField HeaderText="Obs." ItemStyle-HorizontalAlign="left"  Visible="false" FooterStyle-HorizontalAlign="left">
                <ItemTemplate>
                    <%# ((MovimentoConta)Container.DataItem).Observacao %> 
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
