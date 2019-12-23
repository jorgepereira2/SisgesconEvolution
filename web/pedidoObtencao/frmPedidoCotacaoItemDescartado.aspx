<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoCotacaoItemDescartado.aspx.cs" Inherits="frmPedidoCotacaoItemDescartado" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="~/pedidoObtencao/ucJustificativa.ascx" TagName="Justificativa" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />      
</head>
<body>
    <form id="form1" runat="server">       
    <uc:Justificativa runat="server" ID="ucJustificativaCancelamento" />
    <uc:Justificativa runat="server" ID="ucJustificativaReativacao" />
    
    <div align="center">
    <div align="right" style="width:98%" class="PageTitle">
    <br />
        Itens Descartados
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="98%" >																		    
        <tr   >
            <td style="border:solid 1px black" valign="top">                
                <table border="0" cellpadding="2" cellspacing="2" width="100%" >                                                  
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
                                            Comprador:
                                        </td>
                                        <td align="left">
                                           <Anthem:DropDownList runat="server" ID="ddlComprador" />
                                        </td>
                                    </tr>                                 
                                    <tr>
                                        <td align="right">
                                            AC:
                                        </td>
                                        <td align="left">
                                           <Anthem:TextBox runat="server" ID="txtPO" Columns="30" />
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
                        <td colspan="3" align="center" valign="top">                         
                            <Anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="AC" ItemStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkPO" style="text-decoration:underline;" 
                                                Text='<%# ((PedidoCotacaoItemDescartado)Container.DataItem).PedidoObtencao.CodigoComAno %>' />
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>                                          
                                    <asp:TemplateColumn HeaderText="Comprador" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((PedidoCotacaoItemDescartado)Container.DataItem).Servidor %>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>                                          
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((PedidoCotacaoItemDescartado)Container.DataItem).ServicoMaterial.Descricao%>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>  
                                     <asp:TemplateColumn HeaderText="Justificativa" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((PedidoCotacaoItemDescartado)Container.DataItem).Justificativa%>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn> 
                                     <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkDescartar" CommandName="Delete" Text="Descartar" />
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkReativar" CommandName="Edit" Text="Reativar" />
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                   
                                </Columns>
                            </Anthem:DataGrid>                            
                        </td>
                    </tr>
                </table>                     
            </td>
        </tr>																			
    </table>
    <br /><br />
    <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">                
                 
                 
            </td>
            <td width="10px">
                &nbsp;
            </td>
        </tr>
    </table>
    </div>    
    <br /><br /><br /><br />
    </form>    
</body>
</html>
