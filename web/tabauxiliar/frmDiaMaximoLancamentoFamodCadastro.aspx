<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDiaMaximoLancamentoFamodCadastro.aspx.cs" Inherits="frmDiaMaximoLancamentoFamodCadastro" %>
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
        <asp:label ID="Label1" runat="server" CssClass="PageTitle" Text="Cadastro de Dia Máximopara Lançamento de FAMOD">
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
                                    Dia Máximo por Mês                                    
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
                                        <asp:TemplateColumn HeaderText="Data" sortexpression="Data" >
                                            <ItemTemplate>
                                                <%# ((DiaMaximoLancamentoFamod)Container.DataItem).Data.ToShortDateString() %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DateTextBox  runat="server" ID="txtData" 
                                                    text='<%# ((DiaMaximoLancamentoFamod)Container.DataItem).Data.ToShortDateString() %>' />
                                                <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtData"  
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DateTextBox  runat="server" ID="txtData" />
                                                <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtData"  
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </FooterTemplate>                                            
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Ano" sortexpression="Ano" >
                                            <ItemTemplate>
                                                <%# ((DiaMaximoLancamentoFamod)Container.DataItem).Ano %>
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
                                                <%# System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[((DiaMaximoLancamentoFamod)Container.DataItem).Mes - 1] %>
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
    <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  
    </form>    
</body>
</html>
