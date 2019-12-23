<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEquipamentoBusca.aspx.cs" Inherits="frmEquipamentoBusca" %>
<%@ Register Src="~/UserControls/BuscaEquipamento.ascx" TagName="BuscaEquipamento" TagPrefix="uc" %>
<%@ Register TagPrefix="Anthem" Assembly="Anthem" Namespace="Anthem" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" /> 
    <script language="javascript" type="text/javascript">
        function Selecionar(id)
        {   
            opener.UpdateUserControl(document.getElementById('id_controle').value, id);
            setTimeout('self.close();', 100);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">    
    <Anthem:TextBox runat="server" id="id_controle" style="display:none" />
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Busca de Equipamentos
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="94%" style="height:350px;" >																		    
        <tr>
            <td valign="top">                
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                    <tr>                            
                        <td colspan="2" align="center" valign="top" style="border:solid 1px black">
                        <br />
                            <div align="left" style="vertical-align:text-bottom">                                
                                <div class="PageTitle" >
                                &nbsp;&nbsp;
                                Filtros</div>
                                <hr size="1" class="divisor" style="" />
                                 <table width="100%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td align="right">
                                            Tipo:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlTipoEquipamento" AutoCallBack="true"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Sub-Tipo:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlSubTipoEquipamento" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Tipo Operativo:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlTipoOperativo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Descrição:
                                        </td>
                                        <td align="left">
                                            <Anthem:TextBox runat="server" ID="txtDescricao" Columns="40" /> &nbsp;&nbsp; 
                                            <Anthem:Button runat="server" ID="btnPesquisar"  TextDuringCallBack="Aguarde" Text="Pesquisar"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                                                                      
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />                            
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                             <anthem:GridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <RowStyle CssClass="dgItem" />
                                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                <Columns>                       
                                  <asp:BoundField HeaderText="Descrição" ItemStyle-HorizontalAlign="left"  SortExpression="Descricao" 
                                        DataField="Descricao" />                                    
                                    <asp:BoundField HeaderText="Tipo" ItemStyle-HorizontalAlign="center"  SortExpression="TipoEquipamento" 
                                        DataField="TipoEquipamento" />
                                    <asp:BoundField HeaderText="SubTipo" ItemStyle-HorizontalAlign="center"  SortExpression="SubTipoEquipamento" 
                                        DataField="SubTipoEquipamento" />
                                    <asp:BoundField HeaderText="Codeq" ItemStyle-HorizontalAlign="center"  SortExpression="Codeq" 
                                        DataField="Codeq" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="#" onclick='Selecionar(<%# ((Equipamento)Container.DataItem).ID %>);' >Selecionar</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                   
                                </Columns>
                            </anthem:GridView>
                            <Anthem:Panel runat="server" ID="pnMensagem" CssClass="msgErro" Visible="False">
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
