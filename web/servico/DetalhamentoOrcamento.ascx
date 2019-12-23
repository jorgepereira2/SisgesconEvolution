<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DetalhamentoOrcamento.ascx.cs" Inherits="servico_DetalhamentoOrcamento" %>
<%@ Import Namespace="Marinha.Business" %>
<div class="PageTitle" style="width:98%;text-align:right;">
    Detalhamento
</div>  
<table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio" style="border-collapse:collapse;" border="1" rules="cols">
    <tr style="font-weight:bold;border-bottom: solid 1px black;">
        <td align="center" width="50px">
            FRE
        </td>                 
        <td align="center" width="50px">
            Item
        </td>                 
        <td align="left"  >
            Discriminação
        </td>
        <td align="right" width="190px" >
            Valor
        </td>
    </tr>
    <tr >
        <td align="center" valign="middle" rowspan="6"  >
            170
        </td>
        <td align="center"  >
            01
        </td>                 
        <td align="left"  >
            Mão-de-Obra Direta (MOD)
        </td>
        <td align="right"  >
            <%# _orcamento.ValorMaoObraDireta.ToString("C2") %>
        </td>
    </tr>    
    <tr >
        <td align="center"  >
            02
        </td>                 
        <td align="left"  >
            Mão-de-Obra Indireta (MOI)
        </td>
        <td align="right"  >
            <%# _orcamento.ValorMaoObraIndireta.ToString("C2") %>
        </td>
    </tr>            
    <tr >
        <td align="center"  >
            03
        </td>                 
        <td align="left"  >
            Taxa Operacional Mão-de-Obra (TOMO)
        </td>
        <td align="right"  >
            <%# _orcamento.ValorTaxaOperacionalMaoObra.ToString("C2") %>
        </td>
    </tr>  
    <tr >
        <td align="center"  >
            04
        </td>                 
        <td align="left"  >
            Sub Total (01 + 02 + 03)
        </td>
        <td align="right"  >
            <%# _orcamento.SubTotalMaoObra.ToString("C2") %>
        </td>
    </tr>
    <tr style="border-bottom: solid 1px black;" >
         <td align="center" >
            05
        </td>                 
        <td align="left"  >
            Desconto Mão-de-Obra
        </td>
        <td align="right"  >
            <%# _orcamento.ValorDescontoMaoObra.ToString("C2") %>
        </td>
    </tr> 
     <tr style="border-bottom: solid 1px black;" >
         <td align="center" >
            06
        </td>                 
        <td align="left"  >
            Total Mão-de-Obra
        </td>
        <td align="right"  >
            <%# _orcamento.ValorTotalMaoObra.ToString("C2") %>
        </td>
    </tr>  
    <tr>
         <td align="center" valign="middle" rowspan="11"  >
            172/171
        </td>
         <td align="center"  >
            07
        </td>                 
        <td align="left"  >
            Matéria-Prima (MD)
        </td>
        <td align="right"  >
            <%# _orcamento.ValorMateriaPrima.ToString("C2") %>
        </td>
    </tr>
     <tr>
         <td align="center"  >
            08
        </td>                 
        <td align="left"  >
            Serviço de Terceiros (STD)
        </td>
        <td align="right"  >
            <%# _orcamento.ValorServicoTerceiros.ToString("C2") %>
        </td>
    </tr>
    <tr>
         <td align="center"  >
            09
        </td>                 
        <td align="left"  >
            Material Indireto (MI)
        </td>
        <td align="right"  >
            <%# _orcamento.ValorMaterialIndireto.ToString("C2") %>
        </td>
    </tr>
    <tr>
         <td align="center"  >
            10
        </td>                 
        <td align="left"  >
            Serviço de Terceiros Indireto(STI)
        </td>
        <td align="right"  >
            <%# _orcamento.ValorServicoTerceirosIndireto.ToString("C2") %>
        </td>
    </tr>
    <tr>
         <td align="center"  >
            11
        </td>                 
        <td align="left"  >
            Taxa Operacional de Material e Serviços (TOMS)
        </td>
        <td align="right"  >
            <%# _orcamento.ValorTaxaOperacionalMaterialServicos.ToString("C2") %>
        </td>
    </tr>
     <tr >
        <td align="center"  >
            12
        </td>                 
        <td align="left"  >
            Sub Total (07 + 08 + 09 + 10 + 11)
        </td>
        <td align="right"  >
            <%# _orcamento.SubTotalMaterial.ToString("C2") %>
        </td>
    </tr>
    <tr>
         <td align="center"  >
            13
        </td>                 
        <td align="left"  >
            Taxa Contribuição Operacional Mão-de-Obra (TCOMO)
        </td>
        <td align="right"  >
            <%# _orcamento.ValorTaxaContribuicaoOperacionalMaoObra.ToString("C2") %>
        </td>
    </tr> 
    <tr>
         <td align="center"  >
            14
        </td>                 
        <td align="left"  >
            Taxa Contribuição Operacional Material (TCOMS)
        </td>
        <td align="right"  >
            <%# _orcamento.ValorTaxaContribuicaoOperacionalMaterial.ToString("C2") %>
        </td>
    </tr> 
     <tr >
         <td align="center"  >
            15
        </td>                 
        <td align="left"  >
            Desconto Concedido
        </td>
        <td align="right"  >
            <%# _orcamento.ValorDescontoMaterial.ToString("C2") %>
        </td>
    </tr>
    <tr style="border-bottom: solid 1px black;">
         <td align="center"  >
            16
        </td>                 
        <td align="left"  >
            Sub-Total (12 + 13 + 14 - 15)
        </td>
        <td align="right"  >
            
            <%# Convert.ToDecimal(_orcamento.ValorTotalOrcamento - _orcamento.ValorTotalMaoObra).ToString("C2") %>
            
        </td>
    </tr>   
    <tr style="border-bottom: solid 1px black;">
         <td align="center"  >
            17
        </td>                 
        <td align="left"  >
            <b>Total (06 + 16)</b>
        </td>
        <td align="right"  >
            <b>
            <%# _orcamento.ValorTotalOrcamento.ToString("C2") %>
            </b>
        </td>
    </tr>

    <tr>
         <td align="left" colspan="4"  >
         <br/>
            Caso o serviço não seja autorizado, serão cobrados os valores relativos ao delineamento no total de <%# Parametro.Get().ValorDelineamento.ToString("C2") %>
        </td>                         
    </tr>
    
</table>   