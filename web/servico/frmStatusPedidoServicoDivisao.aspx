<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmStatusPedidoServicoDivisao.aspx.cs" Inherits="frmStatusPedidoServicoDivisao" %>
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
    <div align="right" style="width:90%">
    <br />
        <asp:label ID="Label1" runat="server" CssClass="PageTitle" Text="Responsáveis por Divisão">
            </asp:label>
    
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
                                    Etapa: <asp:Label runat="server" ID="lblEtapa" ForeColor="red" />                               
                                    <hr size="1" class="divisor" style="" />
                                </div>
                                <br />
                                <div align="right">
                                    <Anthem:Button runat="server" ID="btnNovo"  TextDuringCallBack="Aguarde" Text="Novo"
                                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                    &nbsp;&nbsp;
                                    <Anthem:Button runat="server" ID="btnVoltar" Text="Voltar"
                                        CssClass="Button" CausesValidation="false" />
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
                                        <asp:TemplateColumn HeaderText="Divisão" sortexpression="Celula">
                                            <ItemTemplate>
                                                <%# ((StatusPedidoServicoDivisao)Container.DataItem).Celula.Descricao %>
                                            </ItemTemplate>                                            
                                            <FooterTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlCelula"  />
                                                <Anthem:RequiredFieldValidator runat="server" ControlToValidate="ddlCelula" InitialValue="0" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </FooterTemplate>
                                        </asp:TemplateColumn>                                         
                                        <asp:TemplateColumn HeaderText="Servidor" sortexpression="Servidor">
                                            <ItemTemplate>
                                                <%# ((StatusPedidoServicoDivisao)Container.DataItem).Servidor%>
                                            </ItemTemplate>                                            
                                            <FooterTemplate>
                                                <Anthem:DropDownList runat="server" ID="ddlServidor"  />
                                                <Anthem:RequiredFieldValidator runat="server" ControlToValidate="ddlServidor" InitialValue="0" 
                                                     Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                            </FooterTemplate>
                                        </asp:TemplateColumn>                                                                                                                 
                                        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <Anthem:ImageButton runat="server" CommandName="Delete" ImageUrl="~/images/ico_del.gif"
                                                    ToolTip="Excluir" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <Anthem:LinkButton runat="server" ID="btnSalvar" Text="Salvar" CommandName="Insert" 
                                                    CausesValidation="true" ValidationGroup="Descricao"/>
                                                &nbsp;
                                                <Anthem:LinkButton runat="server" ID="btnCancelarNovo" Text="Cancelar" CommandName="Cancel" 
                                                    CausesValidation="false"/>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </Anthem:DataGrid>
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
