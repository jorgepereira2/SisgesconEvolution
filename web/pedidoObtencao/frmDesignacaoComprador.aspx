<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDesignacaoComprador.aspx.cs" Inherits="frmDesignacaoComprador" %>
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
        Designação de Comprador    
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
                           <Anthem:LinkButton runat="server" ID="lnkPO" CssClass="legenda" />                           
                        </td>
                     </tr>
                     <tr>
                        <td width="5%" ></td>
                        <td align="right" width="20%" >
                           PS:
                        </td>
                        <td align="left">
                           <Anthem:LinkButton runat="server" ID="lnkPS" CssClass="legenda" />                           
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
                        <td ></td>
                        <td></td>
                        <td >
                            <br />
                            Selecione o comprador e seus itens
                        </td>
                    </tr>
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Comprador:
                        </td>
                        <td align="left">
                           <Anthem:DropDownList runat="server" ID="ddlComprador" /> &nbsp;
                           <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlComprador" InitialValue="0"
                                ErrorMessage="Campo obrigatório" Display="dynamic" />
                        </td>
                    </tr>                                       
                    <tr>                            
                        <td colspan="3" align="center" valign="top">
                        <br />                            
                            <Anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:CheckBox runat="server" ID="chkMarcado" Checked="true" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <Anthem:CheckBox runat="server" ID="chkTodos" AutoCallBack="true" OnCheckedChanged="chkTodos_CheckedChanged" />
                                        </HeaderTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.DescricaoCompleta %>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>  
                                     <asp:TemplateColumn HeaderText="Exec." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItem)Container.DataItem).FlagExec ? "Sim" : "Não" %>                                             
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>                                                                   
                                    <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItem)Container.DataItem).Quantidade %>
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn> 
                                      <asp:TemplateColumn HeaderText="Saldo Licitação" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="lnkLicitacao" Text='
                                                <%# ((PedidoObtencaoItem)Container.DataItem).ItemLicitacaoDisponivel != null ? 
                                                ((PedidoObtencaoItem)Container.DataItem).ItemLicitacaoDisponivel.QuantidadeDisponivel + 
                                                    string.Format(" (Licitação {0})", ((PedidoObtencaoItem)Container.DataItem).ItemLicitacaoDisponivel.Licitacao.NumeroPregao) : "" %>' /> 
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>                                  
                                </Columns>
                            </Anthem:DataGrid>
                            <div align="right" style="width:98%">
                                <br />
                               <Anthem:Button runat="server" ID="btnSalvar" TextDuringCallBack="Aguarde" Text="Salvar"
                                    EnabledDuringCallBack="false" CssClass="Button" />&nbsp;
                            </div>
                        </td>
                    </tr>
                     <tr>                            
                        <td colspan="3" align="center" valign="top">
                        <br />  
                        <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                Itens Designados
                                <hr size="1" class="divisor" style="" />
                            </div>                            
                            <br />                          
                            <Anthem:DataGrid runat="server" ID="dgItemDesignado" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>                                    
                                    <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.DescricaoCompleta %>
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>                                                                   
                                                                     
                                    <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <%# ((PedidoObtencaoItem)Container.DataItem).Quantidade %>
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnRemover" Text="Remover" CommandName="Delete" CausesValidation="false" />
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
                 <Anthem:Button runat="server" ID="btnEnviar" TextDuringCallBack="Aguarde" Text="Finalizar"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />&nbsp;
                 <Anthem:Button runat="server" ID="btnVoltar"  Text="Voltar" CausesValidation="false"
                     CssClass="Button" />&nbsp;
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
