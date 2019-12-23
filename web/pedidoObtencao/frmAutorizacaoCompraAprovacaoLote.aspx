<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAutorizacaoCompraAprovacaoLote.aspx.cs" Inherits="frmAutorizacaoCompraAprovacaoLote" %>
<%@ Import Namespace="System.Data"%>
<%@ Register TagPrefix="Anthem" Assembly="Anthem" Namespace="Anthem" %>
<%@ Register Src="../servico/ConfirmarAprovacao.ascx" TagName="ConfirmarAprovacao" TagPrefix="uc" %>
<%@ Register Src="../servico/Comentario.ascx" TagName="Comentario" TagPrefix="uc" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />         
</head>
<body>
    <script type="text/javascript" src="../js/wz_tooltip.js" ></script>
    <form id="form1" runat="server">    
     <uc:ConfirmarAprovacao runat="server" ID="ucConfirmarAprovacao"  />
    <uc:Comentario runat="server" ID="ucComentario"  />    
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Aprovação de Autorização de Compra
    </div>
    
    <Anthem:Window runat="server" ID="winDetalhes" style="left:120px;" Caption="Dados Complementares" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="220px" Modal="true">
        
        <br />
        <br />
        Ação Interna:<br />
        <Anthem:DropDownList runat="server" ID="ddlProjeto" />
        <br />
        <br />
        Natureza Despesa:<br />
        <Anthem:DropDownList runat="server" ID="ddlNaturezaDespesa" />
        <br /><br />
        PTRES:<br />
        <Anthem:DropDownList runat="server" ID="ddlPTRES" />
        <br /><br />
        Fonte Recurso:<br />
        <Anthem:DropDownList runat="server" ID="ddlFonteRecurso" />
        
        <br />
        <div style="text-align:right">
        <Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" ValidationGroup="Recusa" EnabledDuringCallBack="false"
            TextDuringCallBack="Aguarde" />&nbsp;
        <Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
        </div>


   </Anthem:Window>
      
       <Anthem:Window runat="server" ID="winPedidoServico" style="left:20px;" Caption="Pedidos de Servico" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="720px"  Height="350px" Modal="true">
        <div align="right" style="margin-bottom:4px;">
            <Anthem:Button runat="server" ID="btnFecharPS" CssClass="Button" Text="Fechar" CausesValidation="false" />&nbsp;    
            
        </div>
        <div>
            <b>AC</b> <Anthem:Label runat="server" ID="lblACPS" />
        </div>
        <anthem:GridView runat="server" ID="dgPedidoServico" Width="100%" CssClass="datagrid"
             AutoGenerateColumns="false" CellPadding="3" AllowSorting="false" >
            <HeaderStyle CssClass="dgHeader" />                                    
            <RowStyle CssClass="dgItem" />
            <AlternatingRowStyle CssClass="dgAlternatingItem" />
            <FooterStyle CssClass="dgFooter" />
            <Columns> 
                 <asp:TemplateField HeaderText="Nº PS" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <Anthem:LinkButton runat="server" ID="btnVisualizarPS" Text='<%# ((DataRowView)Container.DataItem)["Codigo"] %>' CausesValidation="false"  /> 
                    </ItemTemplate>  
                </asp:TemplateField>                    
                <asp:BoundField HeaderText="Material Orçado" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" DataField="TotalMaterialOrcado"  ReadOnly="true" />             
                <asp:BoundField HeaderText="Material Comprado" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" DataField="TotalMaterialComprado"  ReadOnly="true" />             
                <asp:BoundField HeaderText="Material AC" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" DataField="MaterialAC"  ReadOnly="true" />             
                <asp:BoundField HeaderText="Serviço Orçado" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" DataField="TotalServicoOrcado"  ReadOnly="true" />             
                <asp:BoundField HeaderText="Serviço Comprado" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" DataField="TotalServicoComprado"  ReadOnly="true" />                             
                <asp:BoundField HeaderText="Serviço AC" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" DataField="ServicoAC"  ReadOnly="true" />             
                <asp:TemplateField HeaderText="Saldo Material" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#  (Convert.ToDecimal(((DataRowView)Container.DataItem)["TotalMaterialOrcado"]) - (Convert.ToDecimal(((DataRowView)Container.DataItem)["TotalMaterialComprado"]) - Convert.ToDecimal(((DataRowView)Container.DataItem)["MaterialAC"]))).ToString("n2") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Saldo Serviço" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#  (Convert.ToDecimal(((DataRowView)Container.DataItem)["TotalServicoOrcado"]) - (Convert.ToDecimal(((DataRowView)Container.DataItem)["TotalServicoComprado"]) - Convert.ToDecimal(((DataRowView)Container.DataItem)["ServicoAC"]))).ToString("n2") %>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </anthem:GridView>
        </Anthem:Window>
   
    <Anthem:Window runat="server" ID="winSaldo" style="left:20px;" Caption="Saldo" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="720px"  Height="350px" Modal="true">
        <div align="right" style="margin-bottom:4px;">
            <Anthem:Button runat="server" ID="btnFecharSaldo" CssClass="Button" Text="Fechar" CausesValidation="false" />&nbsp;    
            
        </div>
        <anthem:GridView runat="server" ID="dgSaldo" Width="100%" CssClass="datagrid"
             AutoGenerateColumns="false" CellPadding="3" AllowSorting="false" >
            <HeaderStyle CssClass="dgHeader" />                                    
            <RowStyle CssClass="dgItem" />
            <AlternatingRowStyle CssClass="dgAlternatingItem" />
            <FooterStyle CssClass="dgFooter" />
            <Columns> 
                <asp:BoundField HeaderText="Ação Interna" ItemStyle-HorizontalAlign="left"  SortExpression="Projeto" DataField="Projeto" ReadOnly="true" />             
                <asp:BoundField HeaderText="Natureza Despesa" ItemStyle-HorizontalAlign="left"  SortExpression="NaturezaDespesa" DataField="NaturezaDespesa" ReadOnly="true" />             
                <asp:BoundField HeaderText="Fonte Recurso" ItemStyle-HorizontalAlign="left"  SortExpression="FonteRecurso" DataField="FonteRecurso" ReadOnly="true" />             
                <asp:BoundField HeaderText="PTRES" ItemStyle-HorizontalAlign="left"  SortExpression="PTRES" DataField="PTRES" ReadOnly="true" />             
                
                 <asp:TemplateField HeaderText="Saldo" ItemStyle-HorizontalAlign="Right"  >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSaldo" />
                    </ItemTemplate>                                        
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comprometido" ItemStyle-HorizontalAlign="Right"  >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblComprometido" />
                    </ItemTemplate>                                        
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Custo Simulado" ItemStyle-HorizontalAlign="Right"  >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCustoSimulado" />
                    </ItemTemplate>                                        
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Saldo Total" ItemStyle-HorizontalAlign="Right"  >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSaldoTotal" />
                    </ItemTemplate>                                        
                </asp:TemplateField>
                
                
            </Columns>
        </anthem:GridView>
        </Anthem:Window>
    
    <table cellSpacing="4" cellPadding="3" border="0" Width="98%" style="height:350px;" >																		    
        <tr>
            <td valign="top">                
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >                   
                    <tr>
                        <td valign="top" align="center" style="margin-bottom:3px">
                             <div>
                            Status: &nbsp;
                            <Anthem:DropDownList runat="server" ID="ddlStatus" />
                            &nbsp;
                            
                             <Anthem:Button runat="server" ID="btnPesquisar"  TextDuringCallBack="Aguarde" Text="Filtrar"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                            </div>
                             <anthem:GridView runat="server" ID="gvPesquisa" Width="100%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <RowStyle CssClass="dgItem" />
                                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <Anthem:ImageButton runat="server" ID="btnLimpar" ToolTip="Limpar Seleção" ImageUrl="../images/cancel.gif" OnClick="btnLimpar_Click" CausesValidation="false" />
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="A" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="LightGreen" >
                                        <ItemTemplate>
                                            <Anthem:RadioButton runat="server" ID="rbAprovar" GroupName="Aprovacao"  /> 
                                            
                                            <Anthem:TextBox runat="server" ID="txtComentario" style="display:none" />                                             
                                        </ItemTemplate>                                          
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="R" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="Red" >
                                        <ItemTemplate>
                                            <Anthem:RadioButton runat="server" ID="rbRecusar" GroupName="Aprovacao" />                                         
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                       
                                    <asp:BoundField HeaderText="Número" ItemStyle-HorizontalAlign="center"  SortExpression="Numero" 
                                        DataField="Numero" ReadOnly="true" />             
                                    <asp:TemplateField HeaderText="Fornecedor" ItemStyle-HorizontalAlign="Left"  >
                                        <ItemTemplate>
                                            <%# ((AutorizacaoCompra)Container.DataItem).Fornecedor + " (" +
                                                    AutorizacaoCompra.GetSaldoComprasUtilizado(((AutorizacaoCompra)Container.DataItem).Fornecedor.ID,
                                                    ((AutorizacaoCompra)Container.DataItem).TipoCompra.ID,
                                                    DateTime.Today.Year).SaldoReal.ToString("N2") + ")" %>
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Aplicação" ItemStyle-HorizontalAlign="center"  SortExpression="Observacao" 
                                        DataField="Observacao" ReadOnly="true"/>
                                    <asp:BoundField HeaderText="Situação" ItemStyle-HorizontalAlign="center"  SortExpression="Status" 
                                        DataField="Status" ReadOnly="true"/>
                                    <asp:BoundField HeaderText="Data" ItemStyle-HorizontalAlign="center"  SortExpression="DataEmissao" 
                                        DataField="DataEmissao" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" ReadOnly="true"/>
                                    <asp:BoundField HeaderText="Valor" ItemStyle-HorizontalAlign="right"  SortExpression="ValorTotal" 
                                        DataField="ValorTotal" DataFormatString="{0:n2}" HtmlEncode="false" ReadOnly="true"/>                              
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnVisualizar" Text="Visualizar" CausesValidation="false"  /> 
                                        </ItemTemplate>                                          
                                    </asp:TemplateField> 
                                  <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnComentar" Text="Comentar" CausesValidation="false" CommandName="Comentar"
                                                CommandArgument='<%# ((AutorizacaoCompra)Container.DataItem).ID %>'  /> <br />
                                            
                                            <Anthem:LinkButton runat="server" ID="btnEditarDetalhes" Text="Financeiro" CommandArgument="<%# ((AutorizacaoCompra)Container.DataItem).ID %>" 
                                                CommandName="EditarDetalhes" />    
                                                <br />
                                            
                                            <Anthem:LinkButton runat="server" ID="btnAtualizarSaldo" Text="Saldo" CommandName="AtualizarSaldo" />    
                                            <Anthem:LinkButton runat="server" ID="btnPS" Text="PS" CommandArgument="<%# ((AutorizacaoCompra)Container.DataItem).ID %>" 
                                                CommandName="PS" />    
                                                <br />
                                        </ItemTemplate>  
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false" >
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" 
                                                CommandName="Edit" CausesValidation="false" /> 
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnSalvar" Text="Salvar" CommandName="Update" 
                                                CausesValidation="true" ValidationGroup="Descricao" />
                                            &nbsp;
                                            <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" 
                                                CausesValidation="false"/>
                                        </EditItemTemplate>                                            
                                    </asp:TemplateField>   
                                   
                                </Columns>
                            </anthem:GridView>
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
            <td align="right">
                <Anthem:Button runat="server" ID="btnAprovar" TextDuringCallBack="Aguarde" Text="Salvar"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" Width="120px"  />                                       
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
