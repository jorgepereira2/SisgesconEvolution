<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMotivoCancelamentoPSCadastro.aspx.cs" Inherits="frmMotivoCancelamentoPSCadastro" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div align="right" style="width:90%" Class="PageTitle">
    <br />
        Cadastro de Motivo Cancelamento
        
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr   >
            <td style="border:solid 1px black" valign="top">
                <Anthem:Panel runat="server" ID="pnCampos" BorderStyle="Solid" BorderColor="black"
                    BorderWidth="0px" Width="90%" HorizontalAlign="center" style="vertical-align:top;" >
                    <table border="0" cellpadding="2" cellspacing="2" width="100%" >

                        <tr>                            
                            <td colspan="2" align="center" valign="top">
                            <br />
                                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                    Motivos Cancelamento
                                    <hr size="1" class="divisor" style="" />
                                </div>
                               
                                <br />
                                 <Anthem:DataGrid runat="server" ID="dgCadastro" Width="98%" CssClass="datagrid"
                                     AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                    <HeaderStyle CssClass="dgHeader" />                                    
                                    <ItemStyle CssClass="dgItem" />
                                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                    <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                    <PagerStyle Visible="false" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" readonly="true" ItemStyle-HorizontalAlign="Center" HeaderText="ID" ItemStyle-Width="10%" />
                                        <asp:TemplateColumn HeaderText="Descrição" sortexpression="Descricao">
                                            <ItemTemplate>
                                                <%# ((MotivoCancelamento)Container.DataItem).Descricao %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:TextBox  runat="server" ID="txtDescricao" columns="40" Text=' <%# ((MotivoCancelamento)Container.DataItem).Descricao %>' />
                                                <Anthem:RequiredFieldValidator runat="server" ID="valDescricao" ControlToValidate="txtDescricao" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:TextBox runat="server" ID="txtDescricaoNovo" columns="40" />
                                                <Anthem:RequiredFieldValidator runat="server" ID="valDescricaoNovo" ControlToValidate="txtDescricaoNovo" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="PS" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <%# ((MotivoCancelamento)Container.DataItem).FlagPS ? "Sim" : "Nao" %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagPS" Text="PS" checked='<%# ((MotivoCancelamento)Container.DataItem).FlagPS %>' />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagPS" Text="PS" Checked="true"/>                                                
                                            </FooterTemplate>
                                        </asp:TemplateColumn>                                     
                                        <asp:TemplateColumn HeaderText="PO" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <%# ((MotivoCancelamento)Container.DataItem).FlagPO ? "Sim" : "Nao" %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagPO" Text="PO" checked='<%# ((MotivoCancelamento)Container.DataItem).FlagPO %>' />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagPO" Text="PO" Checked="true"/>                                                
                                            </FooterTemplate>
                                        </asp:TemplateColumn>                                     
                                        <asp:TemplateColumn HeaderText="AC" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <%# ((MotivoCancelamento)Container.DataItem).FlagAC ? "Sim" : "Nao" %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagAC" Text="AC" checked='<%# ((MotivoCancelamento)Container.DataItem).FlagAC %>' />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagAC" Text="AC" Checked="true"/>                                                
                                            </FooterTemplate>
                                        </asp:TemplateColumn> 
                                        <asp:TemplateColumn HeaderText="Item Comprador" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <%# ((MotivoCancelamento)Container.DataItem).FlagItemComprador ? "Sim" : "Nao" %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagItemComprador" Text="Item Comprador" checked='<%# ((MotivoCancelamento)Container.DataItem).FlagItemComprador %>' />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagItemComprador" Text="Item Comprador" Checked="true"/>                                                
                                            </FooterTemplate>
                                        </asp:TemplateColumn>  
                                        <asp:TemplateColumn HeaderText="Licitação" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <%# ((MotivoCancelamento)Container.DataItem).FlagLicitacao ? "Sim" : "Nao" %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagLicitacao" Text="Licitação" checked='<%# ((MotivoCancelamento)Container.DataItem).FlagLicitacao %>' />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagLicitacao" Text="Licitação" Checked="true"/>                                                
                                            </FooterTemplate>
                                        </asp:TemplateColumn>   
                                         <asp:TemplateColumn HeaderText="OMF" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <%# ((MotivoCancelamento)Container.DataItem).FlagOMF ? "Sim" : "Nao" %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagOMF" Text="OMF" checked='<%# ((MotivoCancelamento)Container.DataItem).FlagOMF %>' />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagOMF" Text="FlagOMF" Checked="true"/>                                                
                                            </FooterTemplate>
                                        </asp:TemplateColumn>  
                                        
                                         <asp:TemplateColumn HeaderText="PSM" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <%# ((MotivoCancelamento)Container.DataItem).FlagPedidoServicoMergulho ? "Sim" : "Nao" %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagPedidoServicoMergulho" Text="PSM" checked='<%# ((MotivoCancelamento)Container.DataItem).FlagPedidoServicoMergulho %>' />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagPedidoServicoMergulho" Text="PSM" Checked="true"/>                                                
                                            </FooterTemplate>
                                        </asp:TemplateColumn>  

                                         <asp:TemplateColumn HeaderText="PSAS" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <%# ((MotivoCancelamento)Container.DataItem).FlagPedidoServicoAtividadeSecundaria ? "Sim" : "Nao" %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagPedidoServicoAtividadeSecundaria" Text="PSAS" checked='<%# ((MotivoCancelamento)Container.DataItem).FlagPedidoServicoAtividadeSecundaria %>' />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkFlagPedidoServicoAtividadeSecundaria" Text="PSAS" Checked="true"/>                                                
                                            </FooterTemplate>
                                        </asp:TemplateColumn> 
                                                                         
                                         <asp:TemplateColumn HeaderText="Ativo" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <%# ((MotivoCancelamento)Container.DataItem).FlagAtivo ? "Sim" : "Nao" %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkAtivo" Text="Ativo" checked='<%# ((MotivoCancelamento)Container.DataItem).FlagAtivo %>' />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkAtivoNovo" Text="Ativo" Checked="true"/>                                                
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
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
                                            <FooterTemplate>
                                                <Anthem:LinkButton runat="server" ID="btnSalvarNovo" Text="Salvar" CommandName="Insert" 
                                                    CausesValidation="true" ValidationGroup="Descricao"/>
                                                &nbsp;
                                                <Anthem:LinkButton runat="server" ID="btnCancelarNovo" Text="Cancelar" CommandName="Cancel" 
                                                    CausesValidation="false"/>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <a href="#" onclick='javascript:Excluir(<%# ((MotivoCancelamento)Container.DataItem).ID %>)' >
                                                    <asp:Image runat="server" ID="imgExcluir" ImageUrl="~/images/ico_del.gif" ToolTip="Excluir" />
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </Anthem:datagrid>
                                 <br />
                                <div align="right">
                                    <Anthem:Button runat="server" ID="btnNovo"  TextDuringCallBack="Aguarde" Text="Novo"
                                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                </div>
                            </td>
                        </tr>
                    </table>     
                </Anthem:Panel>
            </td>
        </tr>																			
    </table>
    <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td width="40%" align="left">
                
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
