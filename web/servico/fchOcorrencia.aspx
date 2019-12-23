<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchOcorrencia.aspx.cs" Inherits="fchOcorrencia" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/servico/DetalhamentoOrcamento.ascx" TagName="Orcamento" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />  
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" Class="ReportTitle">
        Detalhes da Ocorrência
    </div>
    <br /><br />
    <div class="PageTitle" style="width:98%;text-align:right;">
            Dados
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top"  >
                    <b>Código Interno:
                    <%# _ocorrencia.DelineamentoOrcamento.CodigoComAno %>
                    </b> 
                </td>
                <td align="left" valign="top"  >
                    <b>Oficina:</b> 
                    <%# _ocorrencia.Celula.Descricao %>
                </td> 
            </tr>            
            <tr>
                <td align="left" valign="top" >
                    <b>Data Início:</b> 
                    <%# _ocorrencia.DataInicio.ToShortDateString() %>
                </td>                
                <td align="left" valign="top" >
                    <b>Previsão Fim:</b>
                    <%# _ocorrencia.DataPrevisaoFim.ToShortDateString() %>                     
                </td>
            </tr>  
             <tr>
                <td align="left" valign="top" >
                    <b>Serviço de Terceiro:</b> 
                    <%# _ocorrencia.FlagServicoTerceiro ? "Sim" : "Não" %>
                </td>                
                <td align="left" valign="top" >
                    <b>Parte Equipamento:</b> 
                    <%# _ocorrencia.FlagParteEquipamento ? "Sim - " : "Não" %>&nbsp;
                    <%# _ocorrencia.DescricaoParteEquipamento %>
                </td>                
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2" >
                    <b>Descrição:</b> 
                    <%# _ocorrencia.DescricaoServico %>
                </td>                
            </tr>              
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Data Fim:</b> 
                    <%# _ocorrencia.DataFim.HasValue ? _ocorrencia.DataFim.Value.ToShortDateString() : "" %>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2" >
                    <b>Descrição Conclusão:</b> 
                    <%# _ocorrencia.DescricaoConclusao %>
                </td>                
            </tr> 
        </table>
    </form>
</body>
</html>
