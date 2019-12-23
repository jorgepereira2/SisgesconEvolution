<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCustoHHCadastro.aspx.cs" Inherits="frmCustoHHCadastro" %>
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
        Cadastro de Custo de HH
    
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
                                   Custo HH
                                    <hr size="1" class="divisor" style="" />
                                </div>
                                
                                <br />
                                 <Anthem:DataGrid runat="server" ID="dgCadastro" Width="98%" CssClass="datagrid"
                                     AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                    <HeaderStyle CssClass="dgHeader" />                                    
                                    <ItemStyle CssClass="dgItem" HorizontalAlign="Center" />
                                    <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                    <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                    <PagerStyle Visible="false" />
                                    <Columns>                                        
                                        <asp:TemplateColumn HeaderText="Mês/Ano" sortexpression="MesAno">
                                            <ItemTemplate>
                                                <%# ((CustoHH)Container.DataItem).MesAno.ToString("MMM/yyyy") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlMes" /> / <Anthem:DropDownList runat="server" ID="ddlAno" />
                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlMes" /> / <Anthem:DropDownList runat="server" ID="ddlAno" />
                                                
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Custo" sortexpression="ValorCusto">
                                            <ItemTemplate>
                                                <%# ((CustoHH)Container.DataItem).ValorCusto.ToString("N2") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <Anthem:NumericTextBox  runat="server" ID="txtValorCusto" DecimalPlaces="2" Text=' <%# ((CustoHH)Container.DataItem).ValorCusto.ToString("N2") %>' />
                                                <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValorCusto" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:NumericTextBox  runat="server" ID="txtValorCusto" DecimalPlaces="2"  />
                                                <Anthem:RequiredFieldValidator  runat="server" ControlToValidate="txtValorCusto" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
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
                                                <a href="#" onclick='javascript:Excluir(<%# ((CustoHH)Container.DataItem).ID %>)' >
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
