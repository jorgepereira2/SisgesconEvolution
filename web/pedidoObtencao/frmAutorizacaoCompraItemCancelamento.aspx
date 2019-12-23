<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAutorizacaoCompraItemCancelamento.aspx.cs" Inherits="frmAutorizacaoCompraItemCancelamento" %>
<%@ Register Src="ucJustificativa.ascx" TagName="Justificativa" TagPrefix="uc" %>
<%@ Register TagPrefix="Anthem" Assembly="Anthem" Namespace="Anthem" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" /> 
    
</head>
<body>    
    <form id="form1" runat="server" defaultbutton="btnPesquisar">
    <script type="text/javascript" src="../js/wz_tooltip.js" ></script>
     <uc:Justificativa runat="server" ID="ucJustificativa" />
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Cancelamento de Item de Autorização de Compras
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
                                        <td align="right">
                                            Número AC:
                                        </td>
                                        <td align="left">
                                           <Anthem:TextBox runat="server" ID="txtTexto" Columns="30" />
                                            <Anthem:Button runat="server" ID="btnPesquisar"  TextDuringCallBack="Aguarde" Text="Pesquisar"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                            &nbsp;&nbsp;                                           
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />                            
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                              <anthem:DataGrid runat="server" ID="dgItem" Width="100%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" ShowFooter="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                                <Columns>                     
                                    <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# ((PedidoCotacaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                                        </ItemTemplate>                                            
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                           <%# ((PedidoCotacaoItem)Container.DataItem).Quantidade.ToString("N2") %>
                                        </ItemTemplate>                                            
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                           <%# ((PedidoCotacaoItem)Container.DataItem).Valor.ToString("N2") %>
                                        </ItemTemplate>                                            
                                    </asp:TemplateColumn>                   
                                    <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                           <%# ((PedidoCotacaoItem)Container.DataItem).ValorTotal.ToString("C2") %>
                                        </ItemTemplate>                         
                                    </asp:TemplateColumn>  
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                           <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" />
                                        </ItemTemplate>                                            
                                    </asp:TemplateColumn>                                                                                                                     
                                </Columns>
                            </anthem:DataGrid> 
                            <Anthem:Panel runat="server" ID="pnMensagem" CssClass="msgErro" Visible="false">
                                <br />
                                <Anthem:Label runat="server" Text="" ID="lblMensagem" />
                                
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
