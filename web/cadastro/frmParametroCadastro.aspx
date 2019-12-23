<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmParametroCadastro.aspx.cs" Inherits="frmParametroCadastro" %>
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
                    Taxas Orçamento
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="5%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Valor Mão de Obra por Hora:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtValorMaoObraHora" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtValorMaoObraHora"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Taxa Contribuição Operacional Material(TOCMS):
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtTaxaContribuicaoOperacionalMaterial" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTaxaContribuicaoOperacionalMaterial"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Taxa Contribuição Operacional Mao de Obra(TOCMO):
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtTaxaContribuicaoOperacionalMaoObra" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTaxaContribuicaoOperacionalMaoObra"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Taxa Operacional Material Serviço(TOMS):
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtTaxaOperacionalMaterialServico" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTaxaOperacionalMaterialServico"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Percentual Serviço Terceiro Indireto(STI):
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtPercentualServicoTerceiroIndireto" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPercentualServicoTerceiroIndireto"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Percentual Material Indireto(MI):
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtPercentualMaterialIndireto" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPercentualMaterialIndireto"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Taxa Operacional Mão de Obra:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtTaxaOperacionalMaoObra" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTaxaOperacionalMaoObra"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                            Percentual Mão de Obra Indireta:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtPercentualMaoObraIndireta" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPercentualMaoObraIndireta"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                            Percentual Desconto Mão de Obra:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtPercentualDescontoSubTotalMaoObra" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtPercentualDescontoSubTotalMaoObra"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                            Percentual Desconto Material/Serviço Terceiros:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtPercentualDescontoSubTotalMaterialServicoTerceiro" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtPercentualDescontoSubTotalMaterialServicoTerceiro"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                </table> 


                   <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Pedido de Serviço
                <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="5%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                          Texto Mensagem Orçamento:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtTextoImpressaoMensagemOrcamento" TextMode="MultiLine" Rows="3" Columns="50" />
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                          Entrada Item Manual:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="CheckBox1" />
                          
                        </td>
                    </tr>
                    <tr>
                        <td width="5%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Valor Delineamento:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtValorDelineamento" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtValorDelineamento" ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                </table> 

                <div align="left" style="vertical-align:text-bottom" class="PageTitle" runat="server" id="dvMergulhoTitulo">
                    Taxas Mergulho
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" runat="server" id="dvMergulhoConteudo">
                    <tr>
                        <td width="5%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Valor Homem Hora:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtValorHomemHoraMergulho" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtValorHomemHoraMergulho"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Valor Deslocamento:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtValorDeslocamentoMergulho" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtValorDeslocamentoMergulho"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                     <tr>
                        <td width="5%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Valor Taxa Material:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtValorTaxaMaterialMergulho" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtValorTaxaMaterialMergulho"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Valor Taxa Bote:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtValorTaxaBoteMergulho" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtValorTaxaBoteMergulho"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Valor Taxa Catamarã:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtValorTaxaCatamaraMergulho" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtValorTaxaCatamaraMergulho"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Taxa Mão de Obra Direta:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtTaxaMaoObraDiretaMergulho" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtTaxaMaoObraDiretaMergulho"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Taxa Mão de Obra Indireta:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtTaxaMaoObraIndiretaMergulho" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtTaxaMaoObraIndiretaMergulho"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                          Taxa Operacional Mão de Obra:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtTaxaOperacionalMaoObraMergulho" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtTaxaOperacionalMaoObraMergulho"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                            Taxa Contribuição Operacional:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtTaxaContribuicaoOperacionalMergulho" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtTaxaContribuicaoOperacionalMergulho"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                            Desconto FRE 170:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtDescontoFRE170Mergulho" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtDescontoFRE170Mergulho"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                            Desconto FRE 171-172:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtDescontoFRE171172Mergulho" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtDescontoFRE171172Mergulho"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                </table> 



                      <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Taxas Atividade Secundária
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="5%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                           Valor Homem Hora:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtValorHomemHoraAtividadeSecundaria" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtValorHomemHoraAtividadeSecundaria"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>                    
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                            Desconto FRE 170:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtDescontoFRE170AtividadeSecundaria" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtDescontoFRE170AtividadeSecundaria"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                            Desconto FRE 171-172:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtDescontoFRE171172AtividadeSecundaria" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtDescontoFRE171172AtividadeSecundaria"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                           Taxa Operacional de Material e Serviço(TOMS):
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtTaxaOperacionalMaterialServicoAtividadeSecundaria" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtTaxaOperacionalMaterialServicoAtividadeSecundaria"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                     <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                            Taxa Operacional Mão Obra (TOMO):
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtTaxaOperacionalMaoObraAtividadeSecundaria" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtTaxaOperacionalMaoObraAtividadeSecundaria"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                      <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                            Taxa Contribuição Operacional(TCO):
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtTaxaContribuicaoOperacionalAtividadeSecundaria" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtTaxaContribuicaoOperacionalAtividadeSecundaria"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>


                </table> 
                
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
                </table> 
                
                 <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Compras
                <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="5%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                          Permite AC Além do Limite:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkFlagPermiteACComLimiteEstourado" />                           
                        </td>
                    </tr>
                     <tr>
                        <td width="5%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                          Permite Entrega Além do Estoque:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkFlagPermiteEntregaAlemEstoque" />                           
                        </td>
                    </tr>
                     <tr>
                        <td width="5%" class="msgErro" ></td>
                        <td align="right" width="20%" class="label">
                          Texto AC:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtTextoAC" TextMode="MultiLine" Rows="3" Columns="50" />                           
                        </td>
                    </tr>
                     <tr>
                        <td width="5%" class="msgErro" ></td>
                        <td align="right" width="20%" class="label">
                          Comandante/Ordenador Despesa:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlComandante" />                           
                        </td>
                    </tr>
                     <tr>
                        <td width="5%" class="msgErro" ></td>
                        <td align="right" width="20%" class="label">
                            Encarregado da Divisão Obtenção:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlOrdenadorDespesaAC" />  
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                            Percentual Limite Acima Licitação:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtPercentualLimiteAcimaLicitacao" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator  runat="server" ControlToValidate="txtPercentualLimiteAcimaLicitacao"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    
                     <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                            Percentual Bloqueio Licitação:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtPercentualBloqueioLicitacao" DecimalPlaces="2" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator29"  runat="server" ControlToValidate="txtPercentualBloqueioLicitacao"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>

                     <tr>
                        <td width="5%" class="msgErro" ></td>
                        <td align="right" width="20%" class="label">
                          Imagem AC:
                        </td>
                        <td align="left">
                           <Anthem:FileUpload runat="server" ID="txtImagemAC" />
                        </td>
                    </tr>
                </table> 
                
                 <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    FAMOD
                <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="5%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                          Máximo Horas:
                        </td>
                        <td align="left">
                           <Anthem:NumericTextBox runat="server" ID="txtMaximoHorasFAMOD" DecimalPlaces="0" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMaximoHorasFAMOD"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" >*</td>
                        <td align="right" class="label">
                          Máximo Horas Meio Expediente:
                        </td>
                        <td align="left">
                             <Anthem:NumericTextBox runat="server" ID="txtMaximoHorasMeioExpedienteFAMOD" DecimalPlaces="0" />
                           &nbsp;
                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtMaximoHorasMeioExpedienteFAMOD"
                                 ErrorMessage="Campo obrigatório" Display="dynamic" />
                          
                        </td>
                    </tr>
                </table>    
                
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Faturamento
                <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="5%" class="msgErro" >*</td>
                        <td align="right" width="20%" class="label">
                          Validade:
                        </td>
                        <td align="left">
                           <Anthem:TextBox runat="server" ID="txtValidadeFaturamento" Columns="40" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                          Garantia:
                        </td>
                        <td align="left">
                             <Anthem:TextBox runat="server" ID="txtGarantiaFaturamento" Columns="40"  />
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
                             <Anthem:TextBox runat="server" ID="txtTelefone" Columns="40" MaxLength="100" />
                        </td>
                    </tr>
                     <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                          Telefone Contabilidade:
                        </td>
                        <td align="left">
                             <Anthem:TextBox runat="server" ID="txtTelefoneContabilidade" Columns="40" MaxLength="100" />
                        </td>
                    </tr>
                    <tr>
                        <td class="msgErro" ></td>
                        <td align="right" class="label">
                          Email:
                        </td>
                        <td align="left">
                             <Anthem:TextBox runat="server" ID="txtEmail" Columns="40" MaxLength="50" />
                        </td>
                    </tr>
                </table>  
                
                 <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Limites de Compra
                <hr size="1" class="divisor" />
                </div>
                <br />
                <Anthem:DataGrid runat="server" ID="dgTipoCompra" Width="60%" CssClass="datagrid"
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
                                <Anthem:NumericTextBox runat="server" ID="txtLimiteAnual" Text='<%# ((TipoCompra)Container.DataItem).LimiteAnual.ToString("N2") %>' 
                                    DecimalPlaces="2" Columns="14" CssClass="numerico"  /> &nbsp;
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLimiteAnual" ErrorMessage="Campo Obrigatório"
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
