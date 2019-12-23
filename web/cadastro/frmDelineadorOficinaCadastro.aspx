<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDelineadorOficinaCadastro.aspx.cs" Inherits="frmDelineadorOficinaCadastro" %>
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
        <asp:label ID="Label1" runat="server" CssClass="PageTitle" Text="Cadastro de Delineador por Oficina">
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
                                    Delineador x Oficina                                    
                                    <hr size="1" class="divisor" style="" />
                                </div>
                                <br />
                                
                                <table width="100%" border="0">
                                    <tr>
                                        <td width="30%" align="left" valign="top">

                                             <Anthem:DataList runat="server" Width="100%" id="dlOficinas" CellPadding="3" >
                                                <HeaderTemplate>
                                                    <b>1 - Selecione uma Oficina</b>
                                                   <br /> <br />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <Anthem:LinkButton runat="server" ID="lnkOficina" CommandName="Select">
                                                       <%# ((Celula)Container.DataItem).Codigo + " " + ((Celula)Container.DataItem).Descricao %> 
                                                    </Anthem:LinkButton>
                                                </ItemTemplate>
                                             </Anthem:DataList> 

                                        </td>
                                        <td>

                                            <table border="0" width="100%"> 
                                                <tr>
                                                    <td>
                                                         <b>2 - Selecione um Servidor e cique em 'Adicionar'</b>
                                                         <br /> <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Servidor: &nbsp;<Anthem:DropDownList runat="server" ID="ddlServidor" /> &nbsp;&nbsp; 
                                                        <Anthem:Button runat="server" ID="btnAdicionar" Text="Adicionar"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        

                                                        <anthem:DataGrid runat="server" ID="dgServidores" CssClass="datagrid" Width="350px" AutoGenerateColumns="false" CellPadding="3" ShowFooter="false" >
                                                            <HeaderStyle CssClass="dgHeader" />                                    
                                                            <ItemStyle CssClass="dgItem" HorizontalAlign="Center" />
                                                            <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="Center" />
                                                            <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                                            <Columns>
                                                                <asp:TemplateColumn HeaderText="Descrição" ItemStyle-HorizontalAlign="center">
                                                                    <ItemTemplate>
                                                                         <%# ((Servidor)Container.DataItem).NomeCompleto %> 
                                                                    </ItemTemplate>                                                                                                               
                                                                </asp:TemplateColumn>                                                         
                                                                <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                                                                    <ItemTemplate>
                                                                        <Anthem:LinkButton runat="server" ID="btnExcluir" Text="Excluir" CommandName="Delete" />                                                
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </anthem:datagrid>


                                                    </td>
                                                </tr>
                                            </table>
                                        
                                        </td>
                                    </tr>                                    
                                </table>

                               
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
