<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchOrcamento.aspx.cs" Inherits="fchOrcamento" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/servico/DetalhamentoOrcamento.ascx" TagName="Orcamento" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/DadosOM.ascx" TagName="DadosOM" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />  
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
                 <b><asp:Label runat="server" ID="lblTitulo" Text="Orçamento" /></b>
                <br />
                <br />
                <b>
                    PS <%# _orcamento.PedidoServico.CodigoComAno %>
                </b>
                <br />
                <br />
                Emissão: <%# _orcamento.DataEmissao %>                         
            </td>
        </tr>
    </table>
    <br /><br />
    <div class="PageTitle" style="width:98%;text-align:right;">
            Dados Básicos
        </div>  
        <table style="width:98%" cellpadding="2" cellspacing="3" class="TabelaRelatorio">
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Cliente:
                    <%# _orcamento.Cliente.DescricaoParaOrcamento %>
                    </b> 
                </td>                 
            </tr>  
            <tr>
                <td align="left" valign="top"  >
                    <b>Pagador:
                    <%# _orcamento.PedidoServico.ClientePagador.DescricaoParaOrcamento %>
                    </b> 
                </td> 
                <td align="left" valign="top"   >
                    <b>Delineador: </b> 
                    <%# _orcamento.Delineamentos.Count > 0 ? _orcamento.Delineamentos[0].Servidor.NomeCompleto : "" %>
                   
                </td>                
            </tr>           
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Equipamento:</b> 
                    <%# _orcamento.DescricaoEquipamentos %>
                </td>                
            </tr>             
            <tr>
                <td align="left" valign="top" colspan="2"  >
                    <b>Validade:</b> 
                    <%# _orcamento.DataValidade %>
                </td>                
            </tr>             
            <tr>
                <td align="left" valign="top"  >
                    <b>Delineamento:</b> 
                    <%# _orcamento.HomemHoraTotal %> HH
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
        </table>
        
        <br />
       <uc:Orcamento runat="server" id="ucOrcamento" />
       <br />
       <br />
       <br />
       <div style="text-align:center">
        ______________________________________<br />
        <%# _orcamento.ServidorGerente.NomeCompleto %>
        </div>
        
    </form>
</body>
</html>
