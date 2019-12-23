<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmParametroPACadastro.aspx.cs" Inherits="frmParametroPACadastro" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Cadastro de Parâmetros do Sistema
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">               
                
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Pedido de Aquisição
                <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="5%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                          Número Mínimo de Cotações:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtNumeroMinimoCotacoes" DecimalPlaces="0" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtNumeroMinimoCotacoes"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                          Entrada Item Manual:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkEntradaItemManual" />
                          
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                          Valor Máximo Sem Orçamento:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtValorMaximoSemOrcamento" DecimalPlaces="2" Columns="14" CssClass="numerico"  /> &nbsp;
                          <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtValorMaximoSemOrcamento"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                </table> 
                
              
            
                
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Dados da OM
                <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="5%" class="msgErro" ></td>
                        <td align="right" width="20%" class="label">
                          Força:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtForca" Columns="40" />                           
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                          Organização Militar:
                        </td>
                        <td align="left">
                             <Anthem:TextBox runat="server" ID="txtOrganizacaoMilitar" Columns="40"  />
                        </td>
                    </tr>
                     <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                          CNPJ:
                        </td>
                        <td align="left">
                             <Anthem:TextBox runat="server" ID="txtCNPJ" Columns="40" MaxLength="20" />
                        </td>
                    </tr>
                     <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                          Endereço:
                        </td>
                        <td align="left">
                             <Anthem:TextBox runat="server" ID="txtEndereco" Columns="40" MaxLength="120"  />
                        </td>
                    </tr>
                     <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                          Telefone:
                        </td>
                        <td align="left">
                             <Anthem:TextBox runat="server" ID="txtTelefone" Columns="40" MaxLength="40" />
                        </td>
                    </tr>
                </table>  
                
                 <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Limites PA
                <hr size="1" class="divisor" />
                </div>
                <br />
                <Anthem:DataGrid runat="server" ID="dgTipoPA" Width="60%" CssClass="datagrid"
                     AutoGenerateColumns="false" CellPadding="3" AllowSorting="false" AllowPaging="false" >
                    <HeaderStyle CssClass="dgHeader" />                                    
                    <ItemStyle CssClass="dgItem" />
                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                    <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                    <PagerStyle Visible="false" />
                    <Columns>
                        <asp:BoundColumn DataField="Descricao" readonly="true" ItemStyle-HorizontalAlign="left" HeaderText="Tipo Compra" />
                        <asp:TemplateColumn HeaderText="Limite Anual" ItemStyle-HorizontalAlign="center" >
                            <ItemTemplate>
                                <Anthem:NumericTextBox runat="server" ID="txtLimiteAnual" Text='<%# ((TipoPedidoAquisicao)Container.DataItem).ValorLimiteAno.HasValue ? ((TipoPedidoAquisicao)Container.DataItem).ValorLimiteAno.Value.ToString("N2") : "0,00" %>' 
                                    DecimalPlaces="2" Columns="14" CssClass="numerico"  /> &nbsp;
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLimiteAnual" ErrorMessage="Campo Obrigatório"
                                     Display="Dynamic" />                             
                            </ItemTemplate>                            
                        </asp:TemplateColumn>                        
                        <asp:TemplateColumn HeaderText="Limite PA" ItemStyle-HorizontalAlign="center" >
                            <ItemTemplate>
                                <Anthem:NumericTextBox runat="server" ID="txtLimitePA" Text='<%# ((TipoPedidoAquisicao)Container.DataItem).ValorLimitePO.HasValue ? ((TipoPedidoAquisicao)Container.DataItem).ValorLimitePO.Value.ToString("N2") : "0,00" %>' 
                                    DecimalPlaces="2" Columns="14" CssClass="numerico"  /> &nbsp;
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLimitePA" ErrorMessage="Campo Obrigatório"
                                     Display="Dynamic" />                             
                            </ItemTemplate>                            
                        </asp:TemplateColumn>                        
                    </Columns>
                </Anthem:DataGrid>
                                           
            </td>
        </tr>																			
    </table>
    <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td width="40%" align="left">
            
            </td>
            <td align="right">
                <Anthem:Button runat="server" ID="btnSalvar" TextDuringCallBack="Aguarde" Text="Salvar"
                     EnabledDuringCallBack="false" CssClass="Button" />                
            </td>
            <td width="10px">
                &nbsp;
            </td>
        </tr>
    </table>
    </div>    
    </form>    
</body>
</html>
