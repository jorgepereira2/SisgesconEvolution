<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmValorIndiretoDepartamentoCadastro.aspx.cs" Inherits="frmValorIndiretoDepartamentoCadastro" %>
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
        <asp:label ID="Label1" runat="server" CssClass="PageTitle" Text="Cadastro de Valor Indireto por Departamento">
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
                                    Valor Indireto                                   
                                    <hr size="1" class="divisor" style="" />
                                </div>
                                <br />
                                <div align="right">
                                    <Anthem:Button runat="server" ID="btnNovo"  TextDuringCallBack="Aguarde" Text="Novo"
                                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                </div>
                                <br />
                                <Anthem:NumericTextBox  runat="server" ID="txtHidden"  DecimalPlaces="2" style="display:none" />
                                <Anthem:DataGrid runat="server" ID="dgCadastro" Width="98%" CssClass="datagrid"
                                     AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                    <HeaderStyle CssClass="dgHeader" />                                    
                                    <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                    <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                    <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                    <PagerStyle Visible="false" />
                                    <Columns>      
                                        <asp:TemplateColumn HeaderText="Departamento" sortexpression="Departamento" >
                                            <ItemTemplate>
                                                <%# ((ValorIndiretoDepartamento)Container.DataItem).Departamento.Descricao %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlDepartamento"  />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlDepartamento" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Tipo" sortexpression="TipoValor" >
                                            <ItemTemplate>
                                                <%# Shared.Common.Util.GetDescription(((ValorIndiretoDepartamento)Container.DataItem).TipoValor) %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlTipoValor"  />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlTipoValor" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                                                          
                                        <asp:TemplateColumn HeaderText="Valor" sortexpression="Valor" >
                                            <ItemTemplate>
                                                <%# ((ValorIndiretoDepartamento)Container.DataItem).Valor.ToString("N2") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:NumericTextBox  runat="server" ID="txtValor"  DecimalPlaces="2" Columns="10"
                                                    text='<%# ((ValorIndiretoDepartamento)Container.DataItem).Valor.ToString("N2") %>' />
                                                <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtValor"  
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:NumericTextBox  runat="server" ID="txtValor"  DecimalPlaces="2" Columns="10" />
                                                <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValor"  
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </FooterTemplate>                                            
                                        </asp:TemplateColumn>
                                        
                                         
                                        
                                        
                                        <asp:TemplateColumn HeaderText="Ano" sortexpression="Ano" >
                                            <ItemTemplate>
                                                <%# ((ValorIndiretoDepartamento)Container.DataItem).Ano%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlAno"  />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlAno" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Mês" sortexpression="Mes" >
                                            <ItemTemplate>
                                                <%# System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[((ValorIndiretoDepartamento)Container.DataItem).Mes - 1]%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlMes"  />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlMes" />
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
    <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  
    </form>    
</body>
</html>
