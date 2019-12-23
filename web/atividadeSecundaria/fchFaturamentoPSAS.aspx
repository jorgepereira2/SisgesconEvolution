<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fchFaturamentoPSAS.aspx.cs" Inherits="fchFaturamentoPSAS" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="../usercontrols/CertificadoOM.ascx" TagName="Certificado" TagPrefix="uc" %>
<%@ Register TagPrefix="uc" TagName="DadosOM" Src="~/UserControls/DadosOM.ascx" %>
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
                <uc:DadosOM runat="server" Titulo="FATURA DE SERVIÇOS DE FORNECIMENTO DE BENS" />
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
                <td align="left" valign="top" colspan="2"  >
                    <b>Tipo de Atividade: Secundária
                    </b>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top"  >
                    <b>Número Fatura:
                    <%# _pedido.CodigoComAno %>
                    </b>
                </td>
                 <td align="left" valign="top" >
                    <b>Data Emissão:</b>
                    <%# _pedido.DataEmissao.ToShortDateString() %>
                </td>
            </tr>
            <td align="left" valign="top" colspan="2" >
                    <b>Data Vencimento:</b>
                    <%# _pedido.DataVencimento.ToShortDateString() %>
                </td>
            <tr>
                <td align="left" valign="top" colspan="=2"  >
                    <b>Cliente:
                     <%# _pedido.Cliente.Descricao %></b> - <%# _pedido.Cliente.Codigo %> - <%# _pedido.Cliente.IndicativoNaval %>
                </td>                
            </tr>           
          <%--  <tr>
                <td align="left" valign="top"  >
                    <b>Divisão:</b> 
                    <%# _pedido.Celula == null ? "" : _pedido.Celula.Descricao %>
                </td>
               <td align="left" valign="top" >
                    <b>Situação:
                    <%# _pedido.Status.Descricao %>
                    </b>
                </td>
            </tr>                                           
            <tr>
                <td align="left" valign="top"  colspan="2" >
                    <b>Observação:</b> 
                    <%# _pedido.Observacao %>
                </td>
            </tr>--%>
        </table>
        
        
                
            <br />
       
                
              <br />
                <div class="PageTitle" style="width:100%;text-align:right;">
                    Detalhamento 
                </div>
                <anthem:DataGrid runat="server" ID="dgItem" Width="100%" CssClass="datagrid"
                     AutoGenerateColumns="false" CellPadding="3" >
                    <HeaderStyle CssClass="dgHeader" />                                    
                    <ItemStyle CssClass="dgItem" />
                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                    <FooterStyle CssClass="dgFooter" />
                    <EditItemStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundColumn DataField="DescricaoServico" HeaderText="Discriminação" ItemStyle-HorizontalAlign="left" />                        
                        <asp:TemplateColumn HeaderText="FRE" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFRE" Text="172" />
                            </ItemTemplate>                              
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="Valor" HeaderText="Valor" ItemStyle-HorizontalAlign="right" DataFormatString="{0:C2}" />                                                      
                    </Columns>
                </anthem:DataGrid>           
                
  
<br />
<p>
 Obs: Ao pagamento posterior a data de vencimento poderá ser realizado um faturamento suplementar de atualização monetária acordo SGM301-Vol IV-16.11.						
</p>


<br/>
<uc:Certificado runat="server" />

<br />
        <br />
        <br />
        <div style="text-align:center">
        ______________________________________<br />
        <%# _pedido.Celula.GetDepartamento().Descricao %>
        </div>          
  
     
        
    </form>
</body>
</html>
