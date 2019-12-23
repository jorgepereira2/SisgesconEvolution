<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEntradaAC.aspx.cs" Inherits="frmEntradaAC" %>
<%@ Import namespace="System.Collections.Generic"%>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />      
</head>
<body>
    <form id="form1" runat="server">       
    <input id="id_orcamento" style="display:none;" />
    <div align="center">
    <div align="right" style="width:98%" class="PageTitle">
    <br />
        Entrada Material AC    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="98%" >																		    
        <tr   >
            <td style="border:solid 1px black" valign="top">                
                <table border="0" cellpadding="2" cellspacing="2" width="100%" >
                     <tr>
                        <td width="5%" ></td>
                        <td align="right" width="20%" >
                           AC:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblAC" CssClass="legenda" />                           
                        </td>
                     </tr>
                     <%--<tr>
                        <td width="5%" ></td>
                        <td align="right" width="20%" >
                            AC:
                        </td>
                        <td align="left">
                           <asp:Repeater runat="server" ID="repPO" OnItemDataBound="repPO_ItemDataBound">
                        <ItemTemplate>
                            <Anthem:LinkButton runat="server" ID="lnkPO" Text='<%# ((KeyValuePair<int, string>)Container.DataItem).Value %>' />
                        </ItemTemplate>
                        <SeparatorTemplate>
                        ,&nbsp;
                        </SeparatorTemplate>
                    </asp:Repeater>                        
                        </td>
                    </tr>  --%>
                    <tr>
                        <td width="5%" ></td>
                        <td align="right" width="20%" >
                           PS:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblPS" CssClass="legenda" />                           
                        </td>
                    </tr>
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Comprador:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblComprador" CssClass="legenda" />                           
                        </td>
                    </tr>  
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Emissão:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblDataEmissao" CssClass="legenda" />                           
                        </td>
                    </tr>                                    
                    <tr>                            
                        <td colspan="3" align="center" valign="top">
                        <br />
                            <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                Materiais                                  
                                <hr size="1" class="divisor" style="" />
                            </div>                            
                            <br />
                            <Anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>                                        
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>    

                                      <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItem)Container.DataItem).Quantidade %>
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>  
                                                                   
                                    <asp:TemplateColumn HeaderText="Qtd. Já Recebida" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItem)Container.DataItem).QuantidadeEntregue %>
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn> 
                                                                    
                                    <asp:TemplateColumn HeaderText="Qtd. Entrada" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:NumericTextBox runat="server" ID="txtQuantidade" Text="0" Enabled="<%# !((PedidoObtencaoItem)Container.DataItem).FlagRecebido %>" />
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn> 
                                                                                                           
                                    <%--<asp:TemplateColumn HeaderText="Entregue" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:CheckBox runat="server" ID="chkMarcado" Checked="true" />
                                            
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>--%>
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
                 <Anthem:Button runat="server" ID="btnEnviar" TextDuringCallBack="Aguarde" Text="Enviar"
                     EnabledDuringCallBack="false" CssClass="Button" />&nbsp;
                 <Anthem:Button runat="server" ID="btnVoltar" Text="Voltar"
                     EnabledDuringCallBack="false" CssClass="Button" />&nbsp;
                
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
