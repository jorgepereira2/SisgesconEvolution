<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmServicoMaterialBusca.aspx.cs" Inherits="frmServicoMaterialBusca" %>
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
            setTimeout('self.close();', 1000);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <Anthem:TextBox runat="server" id="id_controle" style="display:none" />
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Busca de Serviços/Materiais
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
                                            <Anthem:DropDownList runat="server" ID="ddlTipoServicoMaterial" />
                                        </td>
                                         <td align="right">
                                            Origem:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlOrigem" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" >
                                            Classe:
                                        </td>
                                        <td align="left" colspan="3">
                                            <Anthem:DropDownList runat="server" ID="ddlClasseServicoMaterial" AutoCallBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Sub-Classe:
                                        </td>
                                        <td align="left" colspan="3">
                                            <Anthem:DropDownList runat="server" ID="ddlSubClasseServicoMaterial" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Unidade:
                                        </td>
                                        <td align="left" colspan="3">
                                            <Anthem:DropDownList runat="server" ID="ddlUnidade" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Descrição Singra:
                                        </td>
                                        <td align="left" colspan="3">
                                            <Anthem:TextBox runat="server" ID="txtDescricaoSingra" />
                                        </td>
                                    </tr> 
                                    <tr>
                                        <td align="right">
                                            Código Siasg:
                                        </td>
                                        <td align="left" colspan="3">
                                            <Anthem:TextBox runat="server" ID="txtCodigoSiasg" />
                                        </td>
                                    </tr>                                  
                                    <tr>
                                        <td align="right">
                                            Descrição/Código:
                                        </td>
                                        <td align="left" colspan="3">
                                            <Anthem:TextBox runat="server" ID="txtTexto" Columns="40" /> &nbsp;&nbsp; 
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
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="true" PageSize="50" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <RowStyle CssClass="dgItem" />
                                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                  <PagerSettings Mode="NextPrevious" Position="Bottom" />
                                <PagerStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="12px"  />
                                <Columns>
                                    <asp:BoundField HeaderText="Código" ItemStyle-HorizontalAlign="center"  SortExpression="CodigoInterno" 
                                        DataField="CodigoInterno" />                       
                                    <asp:BoundField HeaderText="Descrição" ItemStyle-HorizontalAlign="left"  SortExpression="Descricao" 
                                        DataField="Descricao" />
                                    <asp:BoundField HeaderText="Tipo" ItemStyle-HorizontalAlign="center"  SortExpression="TipoServicoMaterial" 
                                        DataField="TipoServicoMaterial" />
                                    <asp:BoundField HeaderText="Classe" ItemStyle-HorizontalAlign="center"  SortExpression="ClasseServicoMaterial" 
                                        DataField="ClasseServicoMaterial" />
                                    <asp:BoundField HeaderText="Sub-Classe" ItemStyle-HorizontalAlign="center"  SortExpression="SubClasseServicoMaterial" 
                                        DataField="SubClasseServicoMaterial" /> 
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Unidade">
                                        <ItemTemplate>
                                            <%# ((ServicoMaterial)Container.DataItem).Unidade.Descricao %>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                        
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="#" onclick='Selecionar(<%# ((ServicoMaterial)Container.DataItem).ID %>);' >Selecionar</a>
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
            <td width="10px" align="right">
                <Anthem:Button runat="server" ID="btnFechar"  TextDuringCallBack="Aguarde" Text="Fechar"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" /> 
                &nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>    
    </form>    
</body>
</html>
