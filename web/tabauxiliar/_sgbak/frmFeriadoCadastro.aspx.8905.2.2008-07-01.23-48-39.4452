<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFeriadoCadastro.aspx.cs" Inherits="frmFeriadoCadastro" %>
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
        Cadastro de Feriado       
    
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
                                   Feriados
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
                                        <asp:TemplateColumn HeaderText="Descrição" sortexpression="Descricao">
                                            <ItemTemplate>
                                                <%# ((Feriado)Container.DataItem).Descricao %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:TextBox  runat="server" ID="txtDescricao" columns="40" Text=' <%# ((Feriado)Container.DataItem).Descricao %>' />
                                                <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtDescricao" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:TextBox runat="server" ID="txtDescricaoNovo" columns="40" />
                                                <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtDescricaoNovo" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Data" sortexpression="Data">
                                            <ItemTemplate>
                                                <%# ((Feriado)Container.DataItem).Data.ToShortDateString() %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DateTextBox  runat="server" ID="txtData" Text=' <%# ((Feriado)Container.DataItem).Data.ToShortDateString() %>' />
                                                <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtData" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DateTextBox  runat="server" ID="txtDataNovo" />
                                                <Anthem:RequiredFieldValidator runat="server"  ControlToValidate="txtDataNovo" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Meio Expediente" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <%# ((Feriado)Container.DataItem).FlagMeioExpediente ? "Sim" : "Nao" %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkMeioExpediente" Text="Meio Expediente" checked='<%# ((Feriado)Container.DataItem).FlagMeioExpediente %>' />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkMeioExpedienteNovo" Text="Meio Expediente" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Ativo" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <%# ((Feriado)Container.DataItem).FlagAtivo ? "Sim" : "Nao" %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkAtivo" Text="Ativo" checked='<%# ((Feriado)Container.DataItem).FlagAtivo %>' />                                                
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
                                                <a href="#" onclick='javascript:Excluir(<%# ((Feriado)Container.DataItem).ID %>)' >
                                                    <asp:Image runat="server" ID="imgExcluir" ImageUrl="~/images/ico_del.gif" ToolTip="Excluir" />
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </Anthem:Datagrid>
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
    <br /><br />
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
    <br /><br /><br /><br /><br /><br /><br /><br />
    </form>    
</body>
</html>
