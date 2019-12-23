<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLicitacaoFinalizacao.aspx.cs" Inherits="frmLicitacaoFinalizacao" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function FornecedorAdicionado(id, razaosocial)
        {
            Anthem_InvokePageMethod("FornecedorAdicionado", [id, razaosocial], function(result){});
        }    
    </script>      
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Fnalização de Licitação     
    
    </div>

    
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Licitação
                    <hr size="1" class="divisor" />
                </div>
                <br />

                <asp:Panel runat="server" ID="pnLicitacao">
                  <table style="width:98%" cellpadding="2" cellspacing="3" border="0" >
                    <tr>
                        <td align="left" valign="top"  >
                            <b>Número:
                            <%# _licitacao.NumeroPregao %>
                            </b> 
                        </td>
                         <td align="left" valign="top" >
                            <b>Data Pregão:</b> 
                            <%# _licitacao.DataPregao.HasValue ? _licitacao.DataPregao.Value.ToShortDateString() : "" %>
                        </td>               
                    </tr>
                    <tr>
                        <td align="left" valign="top"  >
                            <b>Data Emissão:</b> 
                            <%# _licitacao.DataEmissao.ToShortDateString() %>
                        </td>
                        <td align="left" valign="top" >
                            <b>Status:</b> 
                            <%# Shared.Common.Util.GetDescription(_licitacao.Status) %>
                        </td>
                    </tr>
                     <tr>
                        <td align="left" valign="top"  >
                            <b>Tipo Licitação:</b> 
                            <%# _licitacao.TipoLicitacao %>
                        </td>
                        <td align="left" valign="top" >
                            <b>Modalidade Pregão:</b> 
                            <%# _licitacao.ModalidadePregao %>
                        </td>
                    </tr>
                    <tr>                        
                        <td align="left" valign="top" colspan="2" >
                            <b>Servidor Fiscal:</b> 
                            <%# _licitacao.ServidorFiscalContrato %>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top"  colspan="2" >
                            <b>Objetivo:</b> 
                            <%# _licitacao.Objetivo %>
                        </td>                            
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <b>Observação:</b> 
                            <%# _licitacao.Observacao %>
                        </td>
                    </tr>
                     <tr>
                        <td align="left" valign="top"  >
                            <b>Valor Total Estimado:</b> 
                            <%# _licitacao.ValorTotalEstimado.ToString("C2") %>
                        </td>
                        <td align="left" valign="top" >
                            <b>Valor Total Final:</b> 
                            <%# _licitacao.ValorTotalFinal.ToString("C2") %>
                        </td>
                    </tr>                    
                </table>
                </asp:Panel>
                <br /><br />

                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td class="msgErro" width="5%" >*</td>
                        <td align="right" width="20%" class="label">
                           Número Processo:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtNumeroPregao" MaxLength="20" Columns="30" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtNumeroPregao"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Data Pregão:
                        </td>
                        <td align="left">
                           <Anthem:DateTextBox runat="server" ID="txtDataPregao" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtDataPregao"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>                           
                </table>   
                <br />
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Itens
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td>
                        
                             <anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# ((LicitacaoItem)Container.DataItem).Material.Descricao %>
                                        </ItemTemplate>                                            
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                           <%# ((LicitacaoItem)Container.DataItem).Quantidade %>
                                        </ItemTemplate>                                            
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Valor Médio" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                           <%# ((LicitacaoItem)Container.DataItem).ValorMedio.ToString("C2") %>
                                        </ItemTemplate>                                            
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Contrato" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlContrato" />  
                                             &nbsp;
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlContrato"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic" InitialValue="0" />                                         
                                        </ItemTemplate>                                            
                                    </asp:TemplateColumn>
                                   <%-- <asp:TemplateColumn HeaderText="Número Contrato" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:TextBox runat="server" ID="txtNumeroContratoAta" Columns="10"  />                                                                                    
                                        </ItemTemplate>                                            
                                    </asp:TemplateColumn>--%>
                                    <asp:TemplateColumn HeaderText="Valor Unit." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:NumericTextBox runat="server" ID="txtValorFinal" DecimalPlaces="2" Columns="9"
                                                CssClass="numerico" />  
                                            &nbsp;
                                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtValorFinal"
                                                 ErrorMessage="Campo obrigatório" Display="dynamic"  />                                           
                                        </ItemTemplate>                                            
                                    </asp:TemplateColumn>
                                </Columns>
                            </anthem:DataGrid>
                        
                        </td>                        
                    </tr>
                    
                </table>                    
            </td>
        </tr>																			
    </table>
    <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td width="40%" align="left">
            
            </td>
            <td align="right">
                <Anthem:Button runat="server" ID="btnSalvar" TextDuringCallBack="Aguarde" Text="Salvar"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                <Anthem:Button runat="server" ID="btnFinalizar" TextDuringCallBack="Aguarde" Text="Finalizar"
                     EnabledDuringCallBack="false" CssClass="Button" />
               <%-- <Anthem:Button runat="server" ID="btnNovoFornecedor" Text="Novo Fornecedor" Width="150px"
                     CssClass="Button" CausesValidation="false" />--%>
                <Anthem:Button runat="server" ID="btnVoltar" Text="Voltar"
                     CssClass="Button" CausesValidation="false" />
            </td>
            <td width="10px">
                &nbsp;
            </td>
        </tr>
    </table>
    <br /><br /><br /><br /><br /><br /><br /><br />
    </div>    
    </form>    
</body>
</html>
