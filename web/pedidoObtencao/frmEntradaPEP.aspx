<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEntradaPEP.aspx.cs" Inherits="frmEntradaPEP" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />      
</head>
<body>
    <form id="form1" runat="server">       
    <uc:MessageBox runat="server" ID="ucMessageBox" />
    
    <div align="center">
    <div align="right" style="width:98%" class="PageTitle">
    <br />
        Recibo de Saída de Material PEP    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="98%" >																		    
        <tr   >
            <td style="border:solid 1px black" valign="top">                
                <table border="0" cellpadding="2" cellspacing="2" width="100%" >
                     <tr>
                        <td width="5%" ></td>
                        <td align="right" width="20%" >
                          PM:
                        </td>
                        <td align="left">
                           <Anthem:LinkButton runat="server" ID="lnkPO"  />                           
                        </td>
                     </tr>
                     <tr>
                        <td width="5%" ></td>
                        <td align="right" width="20%" >
                           PS:
                        </td>
                        <td align="left">
                          <Anthem:LinkButton runat="server" ID="lnkPS"  />                        
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
                        <td colspan="3" align="center" valign="top">
                        <br />
                            <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                Materiais                                  
                                <hr size="1" class="divisor" style="" />
                            </div>                            
                            <br />
                              <div align="left" style="vertical-align:text-bottom; width:98%; margin-bottom:5px;" >
                                Oficina:&nbsp;
                                <Anthem:DropDownList runat="server" ID="ddlOficina" />&nbsp;
                                 <Anthem:Button runat="server" ID="btnFiltrar" TextDuringCallBack="Aguarde" Text="Filtrar" CausesValidation="false"
                                    EnabledDuringCallBack="false" CssClass="Button" />
                            </div>
                            <Anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>                                        
                                    <asp:TemplateColumn HeaderText="Código" ItemStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItemPEPRodizio)Container.DataItem).ServicoMaterial.CodigoInterno %>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>                               
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItemPEPRodizio)Container.DataItem).ServicoMaterial.Descricao %>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>                                    
                                     <asp:TemplateColumn HeaderText="Especificação" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItemPEPRodizio)Container.DataItem).Especificacao%>
                                        </ItemTemplate>                                            
                                    </asp:TemplateColumn> 
                                     <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItemPEPRodizio)Container.DataItem).Celula %>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>                                  
                                    <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItemPEPRodizio)Container.DataItem).Quantidade.ToString("N2") %>
                                            (<%# ((PedidoObtencaoItemPEPRodizio)Container.DataItem).ServicoMaterial.Unidade.Descricao %>)
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Estoque" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblQuantidadeEstoque" />
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Qtd. Entregue" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItemPEPRodizio)Container.DataItem).QuantidadeEntregue.ToString("N2") %>
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Qtd. Entrada" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:NumericTextBox runat="server" ID="txtQuantidade" DecimalPlaces="2" Columns="8" />
                                            <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtQuantidade" ErrorMessage="Campo Obrigatório"
                                                Display="dynamic" /> 
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkCancelar" Text="Cancelar" CommandName="Cancel" />
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                </Columns>
                            </Anthem:DataGrid>
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
                     EnabledDuringCallBack="false" CssClass="Button" />&nbsp;
                 <Anthem:Button runat="server" ID="btnImprimir" TextDuringCallBack="Aguarde" Text="Imprimir Recibo" CausesValidation="false"
                     EnabledDuringCallBack="false" CssClass="Button" Width="120px" Visible="false" />&nbsp;
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
