<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLicitacaoPendente.aspx.cs" Inherits="frmLicitacaoPendente" %>
<%@ Register Src="~/UserControls/InputBox.ascx" TagName="InputBox" TagPrefix="uc" %>
<%@ Register Src="~/pedidoObtencao/ucJustificativa.ascx" TagName="Justificativa" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Import Namespace="Shared.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />         
</head>
<body>    
    <script type="text/javascript" src="../js/wz_tooltip.js" ></script>
    <form id="form1" runat="server">
      <uc:MessageBox runat="server" ID="ucMessageBox"  />
      <uc:InputBox runat="server" ID="ucInputBox" ValidationEnabled="false"  />
      <uc:Justificativa runat="server" ID="ucJustificativa"  />
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Licitação Pendentes
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="94%" style="height:350px;" >																		    
        <tr>
            <td valign="top">                
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >                   
                    <tr>
                        <td valign="top" align="center">
                           
                            <div style="text-align: left">
                                Status: 
                                <Anthem:DropDownList runat="server" ID="ddlStatus" />
                                &nbsp;
                                <Anthem:Button runat="server" ID="btnFiltrar"  TextDuringCallBack="Aguarde" Text="Filtrar"
                                    EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" /> 
                                
                            </div>

                             <anthem:GridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <RowStyle CssClass="dgItem" />
                                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblID_Status" Text='<%# ((Licitacao)Container.DataItem).Status.GetHashCode().ToString() %>'  />                                            
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                                       
                                    <asp:TemplateField HeaderText="Número" ItemStyle-HorizontalAlign="Center" SortExpression="NumeroPregao">
                                        <ItemTemplate>
                                            <%# ((Licitacao)Container.DataItem).NumeroPregao%>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                     
                                    <asp:TemplateField HeaderText="Objeto" ItemStyle-HorizontalAlign="left" SortExpression="Objetivo">
                                        <ItemTemplate>
                                            <%# ((Licitacao)Container.DataItem).Objetivo %>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                                   
                                                                      
                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="center" SortExpression="Status">
                                        <ItemTemplate>
                                            <%# Util.GetDescription(((Licitacao)Container.DataItem).Status) %>
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emissão" ItemStyle-HorizontalAlign="center" SortExpression="DataEmissao">
                                        <ItemTemplate>
                                            <%# ((Licitacao)Container.DataItem).DataEmissao.ToShortDateString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnEditar" Text="Encaminhar" CommandName="Edit" 
                                               CausesValidation="false"  /> 
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnRecusar" Text="Recusar" CommandName="Delete" 
                                               CausesValidation="false"  /> 
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkDetalhes" Text="Detalhes" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </anthem:gridview>
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
            <td width="40%" align="left">                
            </td>
            <td align="right">              
                
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
