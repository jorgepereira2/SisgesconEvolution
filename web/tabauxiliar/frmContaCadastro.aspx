<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmContaCadastro.aspx.cs" Inherits="frmContaCadastro" %>
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
        Cadastro de Conta
        
    
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
                                    Contas
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
                                                <%# ((Conta)Container.DataItem).Descricao %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:TextBox  runat="server" ID="txtDescricao" columns="40" Text=' <%# ((Conta)Container.DataItem).Descricao %>' />
                                                <Anthem:RequiredFieldValidator runat="server" ID="valDescricao" ControlToValidate="txtDescricao" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:TextBox runat="server" ID="txtDescricaoNovo" columns="40" />
                                                <Anthem:RequiredFieldValidator runat="server" ID="valDescricaoNovo" ControlToValidate="txtDescricaoNovo" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </FooterTemplate>
                                        </asp:TemplateColumn> 
                                        
                                        <asp:TemplateColumn HeaderText="Ano" sortexpression="Ano">
                                            <ItemTemplate>
                                                <%# ((Conta)Container.DataItem).Ano %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DropDownList  runat="server" ID="ddlAno" />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DropDownList  runat="server" ID="ddlAno" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Ação Interna" sortexpression="Projeto">
                                            <ItemTemplate>
                                                <%# ((Conta)Container.DataItem).Projeto.Descricao %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DropDownList  runat="server" ID="ddlProjeto" />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DropDownList  runat="server" ID="ddlProjeto" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>                                    
                                        
                                        <asp:TemplateColumn HeaderText="Fase" sortexpression="fase">
                                            <ItemTemplate>
                                                <%# ((Conta)Container.DataItem).Fase.Descricao %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DropDownList  runat="server" ID="ddlFase" />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DropDownList  runat="server" ID="ddlFase" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>                                    
                                        
                                        <asp:TemplateColumn HeaderText="Natureza Despesa" sortexpression="NaturezaDespesa">
                                            <ItemTemplate>
                                                <%# ((Conta)Container.DataItem).NaturezaDespesa.Descricao%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DropDownList  runat="server" ID="ddlNaturezaDespesa" />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DropDownList  runat="server" ID="ddlNaturezaDespesa" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn> 
                                        
                                        <asp:TemplateColumn HeaderText="PTRES" sortexpression="PTRES">
                                            <ItemTemplate>
                                                <%# ((Conta)Container.DataItem).PTRES%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DropDownList  runat="server" ID="ddlPTRES" />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DropDownList  runat="server" ID="ddlPTRES" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>  
                                        
                                         <asp:TemplateColumn HeaderText="UGE" sortexpression="UGE">
                                            <ItemTemplate>
                                                <%# ((Conta)Container.DataItem).UGE%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DropDownList  runat="server" ID="ddlUGE" />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DropDownList  runat="server" ID="ddlUGE" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>                                  
                                        
                                         <asp:TemplateColumn HeaderText="Ativo" ItemStyle-HorizontalAlign="Center" Visible="false" >
                                            <ItemTemplate>
                                                <%# ((Conta)Container.DataItem).FlagAtivo ? "Sim" : "Nao" %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkAtivo" Text="Ativo" checked="True" />                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:CheckBox runat="server" ID="chkAtivoNovo" Text="Ativo" Checked="True"/>                                                
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
                                                <a href="#" onclick='javascript:Excluir(<%# ((Conta)Container.DataItem).ID %>)' >
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
