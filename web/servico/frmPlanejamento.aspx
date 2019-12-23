<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPlanejamento.aspx.cs" Inherits="frmPlanejamento" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/BuscaServicoMaterial.ascx" TagPrefix="uc" TagName="ServicoMaterial" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />     
</head>
<body>
    <form id="form1" runat="server">       
    <input id="id_orcamento" style="display:none;" />
    <div align="center">
    <div align="right" style="width:98%" class="PageTitle">
    <br />
        Planejamento de Execução
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="98%" >																		    
        <tr   >
            <td style="border:solid 1px black" valign="top">                
                <table border="0" cellpadding="2" cellspacing="2" width="100%" >
                     <tr>
                        <td width="5%" ></td>
                        <td align="right" width="20%" >
                           Código Interno:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblCodigo" CssClass="legenda" />
                           &nbsp;
                           <Anthem:LinkButton runat="server" ID="lnkDetalhes" Text="(Ver Detalhes)" />                           
                        </td>
                    </tr>  
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Emissão:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblDataEmissao" CssClass="legenda" />                           
                        </td>
                    </tr>
                    <tr>
                        <td ></td>
                        <td align="right" >
                           Status:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblStatus" CssClass="legenda" />                           
                        </td>
                    </tr>
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Equipamento:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblEquipamento" CssClass="legenda" />                           
                        </td>
                    </tr>                                              
                    <tr>
                        <td ></td>
                        <td align="right" >
                           Categoria:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblCategoria" CssClass="legenda" />                             
                        </td>
                    </tr>                          
                     <tr>                            
                        <td colspan="3" align="center" valign="top">
                        <br />
                            <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                Delineamento                                    
                                <hr size="1" class="divisor" style="" />
                            </div>
                            <br />
                            <div align="center">
                                <table cellpadding="0" cellspacing="0" border="0" width="98%">
                                    <tr>
                                        <td align="left">
                                            <Anthem:Label runat="server" ID="lblHomemHoraTotal"  Font-Bold="true" />
                                        </td>
                                        <td align="right">                                            
                                        </td>
                                    </tr>
                                </table>                                
                            </div>
                            <br />
                            <Anthem:DataGrid runat="server" ID="dgDelineamento" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>                                    
                                    <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                             <%# ((PedidoServicoDelineamento)Container.DataItem).Celula.Descricao %>                                             
                                        </ItemTemplate>                                                                              
                                    </asp:TemplateColumn>                                    
                                    <asp:TemplateColumn HeaderText="HH" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoDelineamento)Container.DataItem).HomemHora %>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="Descrição Serviço" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoDelineamento)Container.DataItem).DescricaoServicoOficina %>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Previsão Início" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:DateTextBox runat="server" ID="txtDataPrevisaoInicio" Text='<%# DateTime.Today.AddMonths(1).ToShortDateString() %>'  />
                                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtDataPrevisaoInicio"
                                                 ErrorMessage="Campo obrigatório" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </Anthem:DataGrid>
                            <div align="right">
                                
                            </div>
                        </td>
                    </tr>                      
                    
                </table>
            </td>
        </tr>																			
    </table>
    <br /><br />
    <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">                
                 <Anthem:Button runat="server" ID="btnEnviar" TextDuringCallBack="Aguarde" Text="Enviar"
                     EnabledDuringCallBack="false" CssClass="Button" />&nbsp;
                <Anthem:Button runat="server" ID="btnVoltar" Text="Voltar"
                     CssClass="Button" CausesValidation="false" />
            </td>
            <td width="10px">
                &nbsp;
            </td>
        </tr>
    </table>
    </div>    
    <br /><br /><br /><br />
    </form>    
</body>
</html>
