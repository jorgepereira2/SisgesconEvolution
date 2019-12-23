<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoServicoPesquisa.aspx.cs" Inherits="frmPedidoServicoPesquisa" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="Anthem" Assembly="Anthem" Namespace="Anthem" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" /> 
    
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Pesquisa de Pedidos de Serviço
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
                                            Data:
                                        </td>
                                        <td align="left">
                                            <Anthem:DateTextBox runat="server" ID="txtDataInicio" />&nbsp;a&nbsp;
                                            <Anthem:DateTextBox runat="server" ID="txtDataFim" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Código Interno/Cliente:
                                        </td>
                                        <td align="left">
                                            <Anthem:TextBox runat="server" ID="txtTexto" Columns="40" /> &nbsp;&nbsp; 
                                            <Anthem:Button runat="server" ID="btnPesquisar"  TextDuringCallBack="Aguarde" Text="Pesquisar"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                            &nbsp;&nbsp; 
                                            <Anthem:Button runat="server" ID="btnNovo"  TextDuringCallBack="Aguarde" Text="Novo"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            
                                        </td>
                                        <td align="left" class="legenda">
                                            <asp:Label runat="server" ID="lblAviso" Text="Apenas Pedidos com o status 'Não Enviado' aparecem na lista" />
                                            
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
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <Anthem:CheckBox runat="server" ID="chkEnviar" /> 
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                     
                                    <asp:BoundField HeaderText="Código Interno" ItemStyle-HorizontalAlign="center"  SortExpression="CodigoComAno" 
                                        DataField="CodigoComAno" />             
                                    <asp:BoundField HeaderText="Cliente" ItemStyle-HorizontalAlign="left"  SortExpression="Cliente" 
                                        DataField="Cliente" />
                                    <asp:BoundField HeaderText="Equipamento" ItemStyle-HorizontalAlign="left"  
                                        DataField="DescricaoEquipamentos" />    
                                    <asp:BoundField HeaderText="Data" ItemStyle-HorizontalAlign="center"  SortExpression="DataEmissao" 
                                        DataField="DataEmissao" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" /> 
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkDetalhes" Text="Detalhes" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkEditar" Text="Editar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                  
                                </Columns>
                            </anthem:gridview>
                            <Anthem:Panel runat="server" ID="pnMensagem" CssClass="msgErro" Visible=false>
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
                 <Anthem:Button runat="server" ID="btnEnviar"  TextDuringCallBack="Aguarde" Text="Enviar Selecionados"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" Width="160px" />
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
