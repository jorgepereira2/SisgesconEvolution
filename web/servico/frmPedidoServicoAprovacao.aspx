<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoAprovacao.aspx.cs" Inherits="frmPedidoServicoAprovacao" %>
<%@ Register TagPrefix="Anthem" Assembly="Anthem" Namespace="Anthem" %>
<%@ Register Src="RecusarAprovacao.ascx" TagName="RecusarAprovacao" TagPrefix="uc" %>
<%@ Register Src="ConfirmarAprovacao.ascx" TagName="ConfirmarAprovacao" TagPrefix="uc" %>
<%@ Register Src="Comentario.ascx" TagName="Comentario" TagPrefix="uc" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />         
</head>
<body>
    <script type="text/javascript" src="../js/wz_tooltip.js" ></script>
    <form id="form1" runat="server">        
    <uc:RecusarAprovacao runat="server" ID="ucRecusarAprovacao"  />
    <uc:ConfirmarAprovacao runat="server" ID="ucConfirmarAprovacao"  />
    <uc:Comentario runat="server" ID="ucComentario"  />
    <Anthem:NumericTextBox runat="server" Columns="7" DecimalPlaces="2" style="display:none;"  />
    <div align="center">
    <div align="right" style="width:96%" class="PageTitle">
    <br />
        Aprovação de Cotação
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="98%" style="height:350px;" >																		    
        <tr>
            <td valign="top">                
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >                   
                    <tr>
                        <td valign="top" align="center">
                             <anthem:GridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <RowStyle CssClass="dgItem" />
                                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                <Columns>
                                     <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <Anthem:ImageButton runat="server" ID="btnLimpar" ToolTip="Limpar Seleção" ImageUrl="../images/cancel.gif" OnClick="btnLimpar_Click" CausesValidation="false" />
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="A" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="LightGreen" >
                                        <ItemTemplate>
                                            <Anthem:RadioButton runat="server" ID="rbAprovar" GroupName="Aprovacao" /> 
                                            
                                            <Anthem:TextBox runat="server" ID="txtComentario" style="display:none" />                                             
                                        </ItemTemplate>                                          
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="R" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="Red" >
                                        <ItemTemplate>
                                            <Anthem:RadioButton runat="server" ID="rbRecusar" GroupName="Aprovacao" />                                         
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                       
                                    <asp:BoundField HeaderText="Código Interno" ItemStyle-HorizontalAlign="center"  SortExpression="CodigoComAno" 
                                        DataField="CodigoComAno" ReadOnly="true" />             
                                    <asp:BoundField HeaderText="Cliente" ItemStyle-HorizontalAlign="left"  SortExpression="Cliente" 
                                        DataField="Cliente" ReadOnly="true"/>
                                    <asp:TemplateField HeaderText="Equipamento" ItemStyle-HorizontalAlign="left" SortExpression="Equipamento">
                                        <ItemTemplate>
                                            <%# ((IPedido)Container.DataItem).DescricaoEquipamentos %> 
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Situação" ItemStyle-HorizontalAlign="center"  SortExpression="Status" 
                                        DataField="Status" ReadOnly="true"/>
                                    <asp:BoundField HeaderText="Data" ItemStyle-HorizontalAlign="center"  SortExpression="DataEmissao" 
                                        DataField="DataEmissao" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" ReadOnly="true"/>
                                    <asp:TemplateField HeaderText="Desc. MO" ItemStyle-HorizontalAlign="right" >
                                        <ItemTemplate>
                                            <%# ((DelineamentoOrcamento)Container.DataItem).PercentualDescontoSubTotalMaoObra.ToString("N2") %>%
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <Anthem:Label runat="server" ID="lblValorMaoObra" style="display:none;" Text='<%# ((DelineamentoOrcamento)Container.DataItem).SubTotalMaoObra.ToString()  %>'  />
                                            <Anthem:NumericTextBox runat="server" ID="txtDescontoMO" Columns="7" DecimalPlaces="2" CssClass="numerico" AutoCallBack="true" OnTextChanged="DescontoMOChanged"
                                              Text='<%# ((DelineamentoOrcamento)Container.DataItem).PercentualDescontoSubTotalMaoObra.ToString("N2") %>'  />(%)&nbsp;
                                            <Anthem:NumericTextBox runat="server" ID="txtValorDescontoMO" Columns="7" DecimalPlaces="2" CssClass="numerico" AutoCallBack="true" OnTextChanged="ValorDescontoMOChanged"
                                              Text='<%# ((DelineamentoOrcamento)Container.DataItem).ValorDescontoMaoObra.ToString("N2") %>'  />(R$)
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Desc. Material" ItemStyle-HorizontalAlign="right" >
                                        <ItemTemplate>
                                            <%# ((DelineamentoOrcamento)Container.DataItem).PercentualDescontoSubTotalMaterialServicoTerceiro.ToString("N2") %>%
                                        </ItemTemplate>
                                        <EditItemTemplate>                                        
                                            <Anthem:Label runat="server" ID="lblValorMaterial" style="display:none;" Text='<%# ((DelineamentoOrcamento)Container.DataItem).SubTotalMaterial.ToString()  %>'  />
                                            <Anthem:NumericTextBox runat="server" ID="txtDescontoMaterial" Columns="7" DecimalPlaces="2" CssClass="numerico" AutoCallBack="true"  OnTextChanged="DescontoMaterialChanged"
                                              Text='<%# ((DelineamentoOrcamento)Container.DataItem).PercentualDescontoSubTotalMaterialServicoTerceiro.ToString("N2")  %>'  />(%)&nbsp;
                                           <Anthem:NumericTextBox runat="server" ID="txtValorDescontoMaterial" Columns="7" DecimalPlaces="2" CssClass="numerico"  OnTextChanged="ValorDescontoMaterialChanged"
                                              Text='<%# ((DelineamentoOrcamento)Container.DataItem).ValorDescontoMaterial.ToString("N2") %>' AutoCallBack="true"  />(R$)                                                 
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Orçamento" ItemStyle-HorizontalAlign="right"  SortExpression="ValorTotalOrcamento" 
                                        DataField="ValorTotalOrcamento" DataFormatString="{0:n2}" HtmlEncode="false" ReadOnly="true"/>             
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnComentar" Text="Comentar" CausesValidation="false" CommandName="Comentar"
                                                CommandArgument='<%# ((DelineamentoOrcamento)Container.DataItem).ID %>'  /> 
                                        </ItemTemplate>  
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnVisualizar" Text="Visualizar" CausesValidation="false"  /> 
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                                                                       
                                     <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" 
                                                    CommandName="Edit" CausesValidation="false" /> 
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:LinkButton runat="server" ID="btnSalvar" Text="Salvar" CommandName="Update" 
                                                    CausesValidation="true" ValidationGroup="Descricao" />
                                                &nbsp;
                                                <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" 
                                                    CausesValidation="false"/>
                                            </EditItemTemplate>                                            
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnRecalcular" Text="Recalcular Orcamento" CausesValidation="false" CommandName="Recalcular"
                                                 CommandArgument='<%# ((DelineamentoOrcamento)Container.DataItem).ID %>' /> 
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>  
                                                                                  
                                                                      
                                </Columns>
                            </anthem:GridView>
                            <Anthem:Panel runat="server" ID="pnMensagem" CssClass="msgErro" Visible="false">
                                <br />
                                Nenhum registro foi encontrado.
                            </Anthem:Panel>
                        </td>
                    </tr>
                </table>     
                
            </td>
        </tr>																			
    </table>
     <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">
                 <Anthem:Button runat="server" ID="btnAprovar" TextDuringCallBack="Aguarde" Text="Salvar"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" Width="120px"  />                 
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
