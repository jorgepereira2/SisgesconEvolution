<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchFaturamento.aspx.cs" Inherits="fchFaturamento" EnableViewState="false" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/servico/DetalhamentoOrcamento.ascx" TagName="Orcamento" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/DadosOM.ascx" TagName="DadosOM" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <style>
body
{
  background-color:White; 
  font-family: verdana; 
  font-size:11px;
  color:#3F3F3F; 
}

.PageTitle
{
	font-weight: bold;
	font-size: 11px;
	color: #0c4765;
	font-family: Verdana;	
	vertical-align:bottom;
	font-style:italic;
	letter-spacing:1px;
}

.ReportTitle
{
	font-weight: bold;
	font-size: 14px;
	color: #0c4765;
	font-family: Verdana;	
	vertical-align:bottom;	
	letter-spacing:2px;
}




.TabelaRelatorio
{
    border: solid 1px black;
}

@media print{
     .noprint { display: none; }
}



     </style>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="2" style="border:solid 1px black; width:98%">
        <tr>
            <td>
            
            </td>
            <td align="center" width="50%">
                <uc:DadosOM runat="server" />
            </td>
            <td align="right" width="25%">
                                   
            </td>
        </tr>
    </table>
    <br /><br />
    <div id="divExportar" align="center" runat="server" class="noprint" >
        <a runat="server" ID="lnkExportar" >Exportar p/ Word</a>
    </div>
    <div class="PageTitle" style="width:98%;text-align:right;">
            Faturamento
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
             <tr>
                <td align="left" valign="top" >
                    <b>Número Fatura:</b> 
                    <%# _faturamento.CodigoComAno %>
                    
                </td>                 
                <td align="left" valign="top" >
                    <b>Código Interno: </b>
                    <%# _faturamento.DelineamentoOrcamento.PedidoServico.CodigoComAno%>
                    
                </td>                 
            </tr> 
            <tr>
                <td align="left" valign="top" >
                    <b>Data:</b> 
                    <%# _faturamento.Data.ToShortDateString() %>
                    
                </td>                 
                <td align="left" valign="top" >
                    <b>Valor: 
                    <%# _faturamento.Valor.ToString("C2") %>
                    </b>
                </td>                 
            </tr>  
            <tr>
                <td align="left" valign="top" >
                    <b>Validade:</b> 
                    <%# _faturamento.Validade %>                    
                </td>                 
                <td align="left" valign="top" >
                    <b>Garantia:</b> 
                    <%# _faturamento.Garantia %>
                </td>
            </tr>           
            <tr>
                <td align="left" valign="top"   >
                    <b>Cliente:
                    <%# _faturamento.DelineamentoOrcamento.Cliente.DescricaoParaOrcamento %>
                    </b> 
                </td>   
                  <td align="left" valign="top" >
                    <b>Numero NL:</b> 
                    <%# _faturamento.NumeroNL %>
                </td>              
            </tr>  
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Pagador:
                    <%# _faturamento.DelineamentoOrcamento.PedidoServico.ClientePagador.DescricaoParaOrcamento%>
                    </b> 
                </td>                 
            </tr>           
            <tr>
                <td align="left" valign="top" colspan="2"   >
                    <b>Equipamento:</b> 
                    <%# _faturamento.DelineamentoOrcamento.DescricaoEquipamentos %> 
                </td>                
               <%-- <td align="left" valign="top"   >
                    <b>N. Registro:</b> 
                    <%# _faturamento.DelineamentoOrcamento.NumeroRegistro%> 
                </td> --%>               
            </tr>             
            <tr>                              
                <td align="left" valign="top" >
                    <b>Pedido Cliente:</b> 
                    <%# _faturamento.DelineamentoOrcamento.PedidoServico.CodigoPedidoCliente %>
                </td>                
                <td align="left" valign="top" >
                    <b>Comprometimento:</b> 
                    <%# _faturamento.DelineamentoOrcamento.ComprometimentoCliente %>
                </td>                
            </tr>             
            <tr>
                <td align="left" valign="top"  >
                    <b>Delineamento:</b> 
                    <%# _faturamento.DelineamentoOrcamento.HomemHoraTotal%> HH
                </td>
                <td align="left" valign="top" >
                    <b>Mão-de-Obra:</b> 
                    <%# _parametro.ValorMaoObraHora.ToString("C2") %> 
                </td>                  
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Serviços:</b> <br />
                    <asp:Repeater runat="server" ID="repServico">
                        <ItemTemplate>
                            - <%# ((PedidoServicoDelineamento)Container.DataItem).DescricaoServicoOficina %>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>     
            
             <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Observação:</b>
                    <%# _faturamento.Observacao %>
                </td>
            </tr>              
        </table>
        
        <br />
        
       <uc:Orcamento runat="server" id="ucOrcamento" />
        
        <br />
        <br />
        <br />
        <div style="text-align:center">
        ______________________________________<br />
        <%# Servidor.Get(this.ID_Servidor).NomeCompleto %>
        </div>
        
    </form>
</body>
</html>
