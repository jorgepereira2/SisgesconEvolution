<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoCotacao.aspx.cs" Inherits="frmPedidoServicoCotacao" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/UserControls/BuscaServicoMaterial.ascx" TagPrefix="uc" TagName="ServicoMaterial" %>
<%@ Register Src="~/servico/DadosComplementaresItemServico.ascx" TagPrefix="uc" TagName="DadosComplementares" %>
<%@ Register Src="~/servico/Comentario.ascx" TagPrefix="uc" TagName="Comentario" %>
<%@ Register Src="~/UserControls/BuscaFornecedor.ascx" TagName="BuscaFornecedor" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />      
</head>
<body>
    <form id="form1" runat="server">       
    <input id="id_orcamento" style="display:none;" />
    <%--<uc:DadosComplementares runat="server" ID="ucDadosComplementares" Visible="false" />--%>
    <uc:Comentario runat="server" ID="ucComentario" />
    <Anthem:NumericTextBox  runat="server" ID="txtHidden" style="display:none"  DecimalPlaces="2"/> 
    <div align="center">
    <div align="right" style="width:98%" class="PageTitle">
    <br />
       Cotação de Pedido de Serviço    
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
                     <tr runat="server" id="trJustificativaRecusa">
                        <td ></td>
                        <td align="right" >
                           Justificativa recusa:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblJustificativaRecusa" CssClass="legenda" ForeColor="Red" Font-Bold="true" />                           
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
                           
                            <Anthem:DataGrid runat="server" ID="dgDelineamento" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>    
                                     <asp:TemplateColumn HeaderText="Delineador" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                             <%# ((PedidoServicoDelineamento)Container.DataItem).ServidorDelineamento.NomeCompleto %>                                             
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>                                
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
                                </Columns>
                            </Anthem:DataGrid>
                             <div align="center">
                                <table cellpadding="2" cellspacing="2" border="0" width="98%">
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
                        </td>
                    </tr>
                      
                    <tr>                            
                        <td colspan="3" align="center" valign="top">
                        <br />
                            <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                Serviços  / Materiais
                                <hr size="1" class="divisor" style="" />
                            </div>
                            <br />                            
                            <Anthem:DataGrid runat="server" ID="dgMaterial" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>         
                                     <asp:TemplateColumn HeaderText="Delineador" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                             <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServidorDelineamento.NomeCompleto%>                                             
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Código" ItemStyle-HorizontalAlign="left" >
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.CodigoInterno %>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                               
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.Descricao %>
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Oficina" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                             <%# ((PedidoServicoItemOrcamento)Container.DataItem).Celula.Descricao %>                                          
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Origem" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlOrigemMaterial" />                                             
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                   <%-- <asp:TemplateColumn HeaderText="RMC" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).RMC %>
                                        </ItemTemplate>                                                                               
                                    </asp:TemplateColumn>--%>
                                     <asp:TemplateColumn HeaderText="Disponível" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblQuantidadeEstoque" />
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn> 
                                      <asp:TemplateColumn HeaderText="Saldo Licitação" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblSaldoLicitacao" />
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).Quantidade + " " + ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.Unidade.Descricao %>
                                        </ItemTemplate>                                      
                                    </asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <Anthem:NumericTextBox  runat="server" ID="txtValor" columns="8" MaxLength="14" 
                                                CssClass="numerico" DecimalPlaces="2" Text='<%# ((PedidoServicoItemOrcamento)Container.DataItem).Valor.ToString("N2") %>'/>                                                
                                            <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValor" 
                                                 Display="Dynamic" ValidationGroup="ItemEdit" ErrorMessage="Campo Obrigatório" /> 
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Fornecedor" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <uc:BuscaFornecedor runat="server" ID="ucFornecedor" Enabled="true" />
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>
                                    

                                   <%-- <asp:TemplateColumn HeaderText="Total" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <%# ((PedidoServicoItemOrcamento)Container.DataItem).ValorTotal.ToString("N2") %>
                                        </ItemTemplate>                                                                               
                                    </asp:TemplateColumn>  
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnDadosComplementares" Text="Dados Complementares" 
                                                    CausesValidation="false" CommandName="DadosComplementares"/>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnDadosComplementares" Text="Dados Complementares" 
                                                    CausesValidation="false" CommandName="DadosComplementares"/>
                                        </FooterTemplate>
                                         <EditItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnDadosComplementares" Text="Dados Complementares" 
                                                    CausesValidation="false" CommandName="DadosComplementares" />
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>   --%>                                                                                                       
                                    
                                </Columns>
                            </Anthem:DataGrid>
                            <div align="center">
                                <table cellpadding="2" cellspacing="2" border="0" width="98%">
                                    <tr>
                                        <td align="left">
                                            <%--<Anthem:Label runat="server" ID="lblValorTotal"  Font-Bold="true" />--%>
                                        </td>
                                        <td align="right">
                                            
                                        </td>
                                    </tr>
                                </table>                                
                            </div>
                            <br />
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
                <Anthem:Button runat="server" ID="btnSalvar" TextDuringCallBack="Aguarde" Text="Salvar"
                     EnabledDuringCallBack="false" CssClass="Button" />&nbsp;
                 <Anthem:Button runat="server" ID="btnEnviar" TextDuringCallBack="Aguarde" Text="Enviar"
                     EnabledDuringCallBack="false" CssClass="Button" />&nbsp;                     

                <Anthem:Button runat="server" ID="btnRecalcularTaxas" TextDuringCallBack="Aguarde" Text="Recalcular Taxas" Visible="true"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="true" />&nbsp;                             
                <%--  <Anthem:Button runat="server" ID="btnDefinirFornecedor"  TextDuringCallBack="Aguarde" Text="Definir Fornecedor" Width="180px" Visible="false"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />  &nbsp;--%>
                <Anthem:Button runat="server" ID="btnImprimirServico" Text="Imprimir Serviço"
                     CssClass="Button" CausesValidation="false" Width="160px" />
                <Anthem:Button runat="server" ID="btnImprimirMaterial" Text="Imprimir Material"
                     CssClass="Button" CausesValidation="false" Width="160px" />
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
