<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSaldoAnual.aspx.cs" Inherits="frmSaldoAnual" %>
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
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Saldo Anual" />
    <uc:ColumnManager runat="server" ID="ucColumn" />  
    <div>
    <br />
    
      <table Width="100%" CellPadding="3" style="border-collapse:collapse; border: 1px inset black;" rules="all" >
        <tr class="dgHeader">
            <td>
                Descrição
            </td>
            <td>
                Receita
            </td>
            <td>
                Descrição
            </td>
            <td>
                Pagamento
            </td>
        </tr>
        <tr>
            <td colspan="4">
            <br/>
             <b>Atividade Principal</b>
            </td>
        </tr>
        <tr >
            <td >
                PS Industrial
                <%= Convert.ToInt32(Request["ano"]) - 1 %>
            </td>
            <td style="text-align:right;">
                <%# PSAnoAnterior.ToString("N2")%>
            </td>
            <td>
                PO Industrial
            </td>
            <td style="color: red; text-align:right;">
                <%# POIndustrial.ToString("N2")  %>
            </td>
        </tr>
        <tr >
            <td>
                PS Industrial
                <%= Request["ano"] %>
            </td>
            <td style="text-align:right;">
                <%# PSAnoAtual.ToString("N2") %>
            </td>
            <td>
                
            </td>
            <td >
                
            </td>
        </tr>

         <tr >
            <td >
                PS Mergulho
                <%= Convert.ToInt32(Request["ano"]) - 1 %>
            </td>
            <td style="text-align:right;">
                <%# PSMAnoAnterior.ToString("N2")%>
            </td>
            <td>
                
            </td>
            <td style="color: red; text-align:right;">
                
            </td>
        </tr>
        <tr >
            <td>
                PS Mergulho
                <%= Request["ano"] %>
            </td>
            <td style="text-align:right;">
                <%# PSMAnoAtual.ToString("N2") %>
            </td>
            <td>
                
            </td>
            <td >
                
            </td>
        </tr>
        <tr style="background-color: whitesmoke;" >
            <td>
              Total
            </td>
            <td style="text-align:right;">
                <%# TotalAtividadePrincipalRecebimento.ToString("N2") %>
            </td>
            <td>
                
            </td >
            <td style="text-align:right; color:Red;" >
                <%# POIndustrial.ToString("N2") %>
            </td>
        </tr>
        <tr style="background-color: LightGray;" >
            <td>
                Saldo             
            </td>
            <td style="text-align:right;border-top: solid 1px black;">
               <b> <%# TotalAtividadePrincipal.ToString("N2") %> </b>
            </td>
            <td>
                
            </td >
            <td >
                
            </td>
        </tr>
         <tr>
            <td colspan="4">
            <br/>
             <b>Atividade Secundária</b>
            </td>
        </tr>
        <tr >
            <td >
                Ativ. Sec.
                <%= Convert.ToInt32(Request["ano"]) - 1 %>
            </td>
            <td style="text-align:right;">
                <%# AtividadeSecundariaAnoAnterior.ToString("N2")%>
            </td>
            <td>
                PO Direto
            </td>
            <td style="color: red; text-align:right;">
                <%# PODireto.ToString("N2")%>
            </td>
        </tr>
        <tr >
            <td>
                Ativ. Sec.
                <%= Request["ano"] %>
            </td>
            <td style="text-align:right;">
                <%# AtividadeSecundariaAnoAtual.ToString("N2")%>
            </td>
            <td>
                Gastos Fixos
            </td>
            <td style="color: red; text-align:right;">
                <%# PagamentoMensal.ToString("N2")%>
            </td>
        </tr>

        <tr style="background-color: whitesmoke;" >
            <td>
              Total
            </td>
            <td style="text-align:right;">
                <%# TotalAtividadeSecundariaRecebimento.ToString("N2") %>
            </td>
            <td>
                
            </td >
            <td style="text-align:right; color:Red;" >
                <%# TotalAtividadeSecundariaPagamento.ToString("N2") %>
            </td>
        </tr>
        <tr style="background-color: LightGray;" >
            <td>
                Saldo             
            </td>
            <td style="text-align:right;">
               <b> <%# TotalAtividadeSecundaria.ToString("N2") %> </b>
            </td>
            <td>
                
            </td >
            <td >
                
            </td>
        </tr>
         <tr style="background-color: LightBlue;" >
            <td>
                Saldo Atual (PRIN. + SEC.)
            </td>
            <td style="text-align:right;">
               <b> <%# TotalGeral.ToString("N2") %> </b>
            </td>
            <td>
                
            </td >
            <td >
                
            </td>
        </tr>
         
    </table>    
    
    </div>
    </div>    
    </form>    
</body>
</html>
