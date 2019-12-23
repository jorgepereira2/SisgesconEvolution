<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOMFResponsavelPericia.aspx.cs" Inherits="frmOMFResponsavelPericia" %>
<%@ Import Namespace="Marinha.Business" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
      
      </script>
</head>
<body>
    <form id="form1" runat="server">       
    
    
    <div align="center">
    <div align="right" style="width:98%" class="PageTitle">
    <br />
        Responsáveis pela Perícia
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="98%" >																		    
        <tr   >
            <td style="border:solid 1px black" valign="top">                
                <table border="0" cellpadding="2" cellspacing="2" width="100%" >
                     <tr>
                        <td width="5%" ></td>
                        <td align="right" width="20%" >
                           Numero Nota:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblNumeroNota" CssClass="legenda" />
                           &nbsp;
                           
                        </td>
                    </tr>  
                     <tr>
                        <td ></td>
                        <td align="right" >
                           Fornecedor:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblFornecedor" CssClass="legenda" />                           
                        </td>
                    </tr>
                    <tr>
                        <td ></td>
                        <td align="right" >
                           Status:
                        </td>
                        <td align="left">
                           <Anthem:Label runat="server" ID="lblStatus" CssClass="legenda" />                           
                        </td>
                    </tr>
                   
                   
                    <tr>                            
                        <td colspan="3" align="center" valign="top">
                        <br />
                            <div align="left" style="vertical-align:text-bottom" class="PageTitle">
                                Responsáveis                                    
                                <hr size="1" class="divisor" style="" />
                            </div>
                            <br />                            
                            <Anthem:DataGrid runat="server" ID="dgResponsavel" Width="98%" CssClass="datagrid"
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                <ItemStyle CssClass="dgItem" HorizontalAlign="center"  />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" HorizontalAlign="center" />
                                <FooterStyle CssClass="dgFooter" HorizontalAlign="Center" />
                                <PagerStyle Visible="false" />
                                <Columns>         
                                    <asp:TemplateColumn HeaderText="Graduação" ItemStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).Graduacao %>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:TextBox runat="server" ID="txtGraduacao" Columns="15"  />
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGraduacao" 
                                                 Display="Dynamic" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                             <Anthem:TextBox runat="server" ID="txtGraduacao" Columns="15" Text="<%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).Graduacao %>"  />
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtGraduacao" 
                                                 Display="Dynamic" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>  
                                     <asp:TemplateColumn HeaderText="Nome" ItemStyle-HorizontalAlign="left" >
                                        <ItemTemplate>
                                            <%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).Nome %>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:TextBox runat="server" ID="txtNome" Columns="25"  />
                                           <Anthem:RequiredFieldValidator  runat="server" ControlToValidate="txtNome" 
                                                 Display="Dynamic" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                             <Anthem:TextBox runat="server" ID="txtNome" Columns="25" Text=" <%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).Nome %>"  />
                                           <Anthem:RequiredFieldValidator   runat="server" ControlToValidate="txtNome" 
                                                 Display="Dynamic" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>                               
                                    <asp:TemplateColumn HeaderText="NIP" ItemStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).NIP %>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:TextBox runat="server" ID="txtNIP" Columns="15"  />
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator3"  runat="server" ControlToValidate="txtNIP" 
                                                 Display="Dynamic" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                             <Anthem:TextBox runat="server" ID="txtNIP" Columns="15" Text="<%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).NIP %>"  />
                                           <Anthem:RequiredFieldValidator runat="server" ControlToValidate="txtNIP" 
                                                 Display="Dynamic" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>  
                                    
                                    
                                    <asp:TemplateColumn HeaderText="Notificação" ItemStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).TipoNotificacao.Descricao %>
                                        </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlTipoNotificacao" />
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTipoNotificacao" 
                                                 Display="Dynamic" ErrorMessage="Campo Obrigatório" InitialValue="0" /> 
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlTipoNotificacao" />
                                           <Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTipoNotificacao" 
                                                 Display="Dynamic" ErrorMessage="Campo Obrigatório" InitialValue="0" /> 
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>  
                                    
                                    
                                    <asp:TemplateColumn HeaderText="Observação" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).Observacao%>
                                        </ItemTemplate>                                        
                                        <FooterTemplate>
                                            <Anthem:TextBox  runat="server" ID="txtObservacao" columns="25" />                                                
                                            
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <Anthem:TextBox  runat="server" ID="txtObservacao" columns="25" 
                                                text='<%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).Observacao %>'/>                                                
                                        </EditItemTemplate>
                                    </asp:TemplateColumn> 
                                    
                                    
                                                                                                                                       
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnEditar" Text="Editar" 
                                                    CommandName="Edit" CausesValidation="false" />
                                            &nbsp;
                                             <a href="#" onclick='javascript:Excluir(<%# ((NotaEntregaMaterialOMFResponsavelPericia)Container.DataItem).ID %>)' >
                                                    Excluir
                                                </a>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:LinkButton ID="btnSalvarNovo" runat="server" CommandName="Insert" 
                                                Text="Adicionar" EnabledDuringCallBack="false" TextDuringCallBack="Aguarde" />
                                            &nbsp;
                                            <Anthem:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" 
                                                Text="Cancelar" CausesValidation="false" />
                                        </FooterTemplate>
                                         <EditItemTemplate>
                                            <Anthem:LinkButton runat="server" ID="btnSalvar" Text="Salvar" CommandName="Update" 
                                                CausesValidation="true" EnabledDuringCallBack="false" TextDuringCallBack="Aguarde" />
                                            &nbsp;
                                            <Anthem:LinkButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" 
                                                CausesValidation="false"/>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </Anthem:DataGrid>
                            <div align="center">
                                <table cellpadding="2" cellspacing="2" border="0" width="98%">
                                    <tr>
                                        <td align="left">
                                            </td>
                                        <td align="right">
                                            <Anthem:Button runat="server" ID="btnNovo"  TextDuringCallBack="Aguarde" Text="Novo"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>                                
                            </div>
                            <br />
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
                     CssClass="Button" CausesValidation="false" />
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
