<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDelineamentoAprovacao.aspx.cs" Inherits="frmDelineamentoAprovacao" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />     
</head>
<body>
    <form id="form1" runat="server">
    <input type="text" style="display:none" id="id_pedidoServico" />
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Aprovação de Delineamentos
    
    </div>      
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr>
            <td style="border:solid 1px black" valign="top">
                <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                    Pedido
                    <hr size="1" class="divisor" />
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td width="5%" ></td>
                        <td align="right" width="20%" >
                           Código Interno:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblCodigo" CssClass="legenda" />    
                           &nbsp;
                           <Anthem:LinkButton runat="server" ID="lnkDetalhes" Text="(Ver Detalhes)" />                           
                        </td>
                    </tr>  
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Situação:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblStatus" CssClass="legenda" />                           
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
                        <td width="5%" ></td>
                        <td align="right" width="20%" >
                           Gerente:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblGerente" CssClass="legenda" />                                                         
                        </td>
                    </tr>                    				 
                   
                   <tr>
                        <td></td>
                        <td colspan="2">

                        <br /><div align="center" style="width:90%" class="PageTitle">
                            <br />
                                Delineamentos
    
                            </div>  
                        
                            <asp:DataList runat="server" ID="dlDelineamentos" >
                                <ItemTemplate>
                                    <div align="left" style="width:90%" class="PageTitle">
                                        <%# ((DelineamentoOficina)Container.DataItem).Oficina.Descricao %>: <%# ((DelineamentoOficina)Container.DataItem).Servidor.NomeCompleto%>  
                                    </div>
                                    <hr size="1" class="divisor" />

                                    
                                     <anthem:DataGrid runat="server" ID="dgDelineamento" Width="98%" CssClass="datagrid"
                                         AutoGenerateColumns="false" CellPadding="3" ShowFooter="false" >
                                        <HeaderStyle CssClass="dgHeader" />                                    
                                        <ItemStyle CssClass="dgItem" />
                                        <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                        <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Descrição Serviço" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <%# ((PedidoServicoDelineamento)Container.DataItem).DescricaoServicoOficina%> 
                                                </ItemTemplate>                                            
                                            </asp:TemplateColumn>
                                           <asp:TemplateColumn HeaderText="HH" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <%# ((PedidoServicoDelineamento)Container.DataItem).HomemHora%> 
                                                </ItemTemplate>                                            
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </anthem:DataGrid>      

                                    <br />

                                     <anthem:DataGrid runat="server" ID="dgServicoMaterial" Width="98%" CssClass="datagrid"
                                         AutoGenerateColumns="false" CellPadding="3" ShowFooter="false" >
                                        <HeaderStyle CssClass="dgHeader" />                                    
                                        <ItemStyle CssClass="dgItem" />
                                        <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                        <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Serviço/Material" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <%# ((PedidoServicoItemOrcamento)Container.DataItem).ServicoMaterial.DescricaoCompleta %> 
                                                </ItemTemplate>                                            
                                            </asp:TemplateColumn>
                                           <asp:TemplateColumn HeaderText="Qtd." ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <%# ((PedidoServicoItemOrcamento)Container.DataItem).Quantidade %> 
                                                </ItemTemplate>                                            
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </anthem:DataGrid>   
                                    
                                    
                                     <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                                       <tr>
                                        <td width="10%" class="msgErro" >*</td>
                                            <td align="right" width="20%" >
                                                Aprovação:
                                            </td>
                                            <td align="left" >
                                                <Anthem:RadioButton runat="server" ID="rbAprovar" Text="Aprovar" GroupName="Aprovacao"  />
                                                <br />
                                                <Anthem:RadioButton runat="server" ID="rbRecusar" Text="Recusar" GroupName="Aprovacao" />                                           
                                           
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="10%" class="msgErro" ></td>
                                            <td align="right" width="20%" >
                                                Justificativa:
                                            </td>
                                            <td align="left" class="legenda">
                                                <Anthem:TextBox runat="server" ID="txtJustificativa" TextMode="MultiLine" Rows="3" Columns="40" />
                                            </td>
                                        </tr>       
                                    </table>   

                                </ItemTemplate>                        
                            </asp:DataList>

                           

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
                 <Anthem:Button runat="server" ID="btnEnviar" TextDuringCallBack="Aguarde" Text="Enviar"
                     EnabledDuringCallBack="false" CssClass="Button" />
                <Anthem:Button runat="server" ID="btnNovo" TextDuringCallBack="Aguarde" Text="Novo"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />                     
                <Anthem:Button runat="server" ID="btnVoltar" Text="Voltar"
                     CssClass="Button" CausesValidation="false" />
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
