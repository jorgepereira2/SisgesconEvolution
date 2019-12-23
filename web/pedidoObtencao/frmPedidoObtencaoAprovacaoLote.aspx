<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPedidoObtencaoAprovacaoLote.aspx.cs" Inherits="frmPedidoObtencaoAprovacaoLote" %>
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
        Aprovação de Autorizações de Compras
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="94%" style="height:350px;" >																		    
        <tr>
            <td valign="top">                
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >                   
                    <tr>
                        <td align="left">
                            Departamento:&nbsp;<Anthem:DropDownList runat="server" ID="ddlDepartamento" />&nbsp;
                            <Anthem:Button runat="server" ID="btnFiltrar"  TextDuringCallBack="Aguarde" Text="Filtrar"
                                    EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />   
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
                                            <Anthem:ImageButton runat="server" ID="btnLimpar" ToolTip="Limpar Seleção" ImageUrl="../images/cancel.gif" OnClick="btnLimpar_Click" CausesValidation="false" />
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="A" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="LightGreen" >
                                        <ItemTemplate>
                                            <Anthem:RadioButton runat="server" ID="rbAprovar" GroupName="Aprovacao" ToolTip="Aprovar"  /> 
                                            <br runat="server" id="brPar" Visible="false"/>
                                            <Anthem:RadioButton runat="server" ID="rbPar" GroupName="Aprovacao" ToolTip="Enviar para o PAR" Visible="false" /> 
                                            
                                            <Anthem:TextBox runat="server" ID="txtComentario" style="display:none" />                                             
                                        </ItemTemplate>                                          
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="R" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="Red" >
                                        <ItemTemplate>
                                            <Anthem:RadioButton runat="server" ID="rbRecusar" GroupName="Aprovacao" ToolTip="Recusar" />                                         
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Número" ItemStyle-HorizontalAlign="center"  SortExpression="Numero" 
                                        DataField="Numero" />             
                                    <asp:BoundField HeaderText="Tipo" ItemStyle-HorizontalAlign="center"  SortExpression="TipoPedidoSigla" 
                                        DataField="TipoPedidoSigla" />  
                                    <asp:BoundField HeaderText="Aplicação" ItemStyle-HorizontalAlign="left"  SortExpression="Aplicacao" 
                                        DataField="Aplicacao" />
                                    <asp:BoundField HeaderText="Situação" ItemStyle-HorizontalAlign="center"  SortExpression="Status" 
                                        DataField="Status" />
                                    <asp:BoundField HeaderText="Data" ItemStyle-HorizontalAlign="center"  SortExpression="DataEmissao" 
                                        DataField="DataEmissao" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                                    <asp:BoundField HeaderText="Valor" ItemStyle-HorizontalAlign="right"  SortExpression="ValorTotal" 
                                        DataField="ValorTotal" DataFormatString="{0:n2}" HtmlEncode="false" />  
                                     <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnComentar" Text="Comentar" CausesValidation="false" CommandName="Comentar"
                                                CommandArgument='<%# ((PedidoObtencao)Container.DataItem).ID %>'  /> 
                                        </ItemTemplate>  
                                    </asp:TemplateField>                                      
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnDocumentos" Text="Documentos" CausesValidation="false"  /> 
                                        </ItemTemplate>                                          
                                    </asp:TemplateField>                                        
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnVisualizar" Text="Visualizar" CausesValidation="false"  /> 
                                        </ItemTemplate>                                          
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
