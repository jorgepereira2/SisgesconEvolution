<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEncarregadoCelulaCadastro.aspx.cs" Inherits="frmEncarregadoCelulaCadastro" %>
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
    <div align="right" style="width:98%">
    <br />
        <asp:label ID="Label1" runat="server" CssClass="PageTitle" Text="Cadastro de Encarregado por Células">
            </asp:label>
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="98%" >																		    
        <tr   >
            <td style="border:solid 1px black" valign="top">
                <Anthem:Panel runat="server" ID="pnCampos" BorderStyle="Solid" BorderColor="black"
                    BorderWidth="0px" Width="98%" HorizontalAlign="center" style="vertical-align:top;" >
                    <table border="0" cellpadding="2" cellspacing="2" width="100%" >

                        <tr>                            
                            <td colspan="2" align="center" valign="top">
                            <br />
                                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                    Encarregados por Célula                                    
                                    <hr size="1" class="divisor" style="" />
                                </div>
                                <br />
                                <div align="right">
                                    <Anthem:Button runat="server" ID="btnNovo"  TextDuringCallBack="Aguarde" Text="Novo"
                                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                </div>
                                <br />
                                <Anthem:DataGrid runat="server" ID="dgCadastro" Width="98%" CssClass="datagrid"
                                     AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                    <HeaderStyle CssClass="dgHeader" />                                    
                                    <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                    <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                    <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                    <PagerStyle Visible="false" />
                                    <Columns>                                        
                                        <asp:TemplateColumn HeaderText="Código" sortexpression="Codigo" >
                                            <ItemTemplate>
                                                <%# ((EncarregadoCelula)Container.DataItem).Celula.Codigo %>
                                            </ItemTemplate>                                           
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Célula" sortexpression="Celula" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <Anthem:Label runat="server" ID="lblCelula" Text='<%# ((EncarregadoCelula)Container.DataItem).Celula %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlCelula" />
                                                <Anthem:RequiredFieldValidator runat="server" ID="valCelula" ControlToValidate="ddlCelula"  
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" InitialValue="0" /> 
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlCelula"  />
                                                <Anthem:RequiredFieldValidator runat="server" ID="valCelula" ControlToValidate="ddlCelula" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" InitialValue="0" /> 
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Servidor" sortexpression="Servidor" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# ((EncarregadoCelula)Container.DataItem).Servidor.Identificacao %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlServidor" />
                                                <Anthem:RequiredFieldValidator runat="server" ID="valServidor" ControlToValidate="ddlServidor"  
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" InitialValue="0" /> 
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlServidor"  />
                                                <Anthem:RequiredFieldValidator runat="server" ID="valServidor" ControlToValidate="ddlServidor" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" InitialValue="0" /> 
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                         <asp:TemplateColumn HeaderText="Data Início" sortexpression="DataInicio" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <%# ((EncarregadoCelula)Container.DataItem).DataInicio.ToShortDateString() %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DateTextBox runat="server" ID="txtDataInicio"  />
                                                <Anthem:RequiredFieldValidator runat="server" ID="valDataInicio" ControlToValidate="txtDataInicio" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DateTextBox runat="server" ID="txtDataInicio"  />
                                                <Anthem:RequiredFieldValidator runat="server" ID="valDataInicio" ControlToValidate="txtDataInicio" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Data Fim" sortexpression="DataFim" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# ((EncarregadoCelula)Container.DataItem).DataFim.HasValue ? ((EncarregadoCelula)Container.DataItem).DataFim.Value.ToShortDateString() : "" %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DateTextBox runat="server" ID="txtDataFim"  />                                            
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DateTextBox runat="server" ID="txtDataFim"  />
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
                                                <Anthem:ImageButton runat="server" CommandName="Delete" ImageUrl="~/images/ico_del.gif"
                                                    ToolTip="Excluir" />
                                                <%--<a href="#" onclick='javascript:Excluir(<%# ((Celula)Container.DataItem).ID %>)' >
                                                    <asp:Image runat="server" ID="imgExcluir" ImageUrl="~/images/ico_del.gif" ToolTip="Excluir" />
                                                </a>--%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </Anthem:datagrid>
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
