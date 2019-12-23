<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMetaCadastro.aspx.cs" Inherits="frmMetaCadastro" %>
<%@ Register Src="~/UserControls/BuscaServicoMaterial.ascx" TagPrefix="uc" TagName="BuscaMaterial" %>
<%@ Register TagPrefix="Anthem" Assembly="Anthem" Namespace="Anthem" %>
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
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Cadastro de Meta
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
                                            Conta:
                                        </td>
                                        <td align="left" width="40%" >
                                            <Anthem:DropDownList runat="server" ID="ddlConta" />                                         
                                        </td>  
                                         <td align="right">
                                            Ano:
                                        </td>
                                        <td align="left" >
                                            <Anthem:DropDownList runat="server" ID="ddlAno" />
                                        </td>                                      
                                    </tr>                                    
                                    <tr>
                                        <td align="right">
                                            Célula:
                                        </td>
                                        <td align="left" colspan="3">
                                            <Anthem:DropDownList runat="server" ID="ddlCelula" />       
                                            &nbsp;
                                            <Anthem:Button runat="server" ID="btnPesquisar"  TextDuringCallBack="Aguarde" Text="Pesquisar"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                                            &nbsp;&nbsp; 
                                            <Anthem:Button runat="server" ID="btnNovo"  TextDuringCallBack="Aguarde" Text="Novo"
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
                            <div>
                                <Anthem:Label runat="server" ID="lblTotal" Font-Bold="true" />
                            </div>
                             <anthem:DataGrid runat="server" ID="dgCadastro" Width="100%" CssClass="datagrid" PageSize="20" 
                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="true" AllowPaging="false" >
                                <HeaderStyle CssClass="dgHeader" />                                    
                                
                                <ItemStyle CssClass="dgItem" />
                                <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                <FooterStyle CssClass="dgFooter" />                                
                                <PagerStyle HorizontalAlign="Center" Mode="NextPrev" Position="Bottom" Font-Bold="true" Font-Size="12px"  />
                                <Columns>
                                     <asp:TemplateColumn HeaderText="Saldo" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <img src="../images/calc_16.gif" />
                                        </ItemTemplate>                                        
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Ano" sortexpression="Ano" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblAno" Text='<%# ((Meta)Container.DataItem).Ano %>' />
                                        </ItemTemplate>                                       
                                    </asp:TemplateColumn>
                                
                                    <asp:TemplateColumn HeaderText="Descrição" sortexpression="Descricao" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblDescricao" Text='<%# ((Meta)Container.DataItem).Descricao %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <Anthem:TextBox  runat="server" ID="txtDescricao" columns="40" MaxLength="100" 
                                                text='<%# ((Meta)Container.DataItem).Descricao %>' />
                                            <Anthem:RequiredFieldValidator runat="server" ID="valDescricao" ControlToValidate="txtDescricao"  
                                                 Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:TextBox runat="server" ID="txtDescricao" columns="40" MaxLength="100" />
                                            <Anthem:RequiredFieldValidator runat="server" ID="valDescricaoNovo" ControlToValidate="txtDescricao" 
                                                 Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn HeaderText="Celula" sortexpression="Celula" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblCelula" Text='<%# ((Meta)Container.DataItem).Celula.Descricao %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlCelula" />                                            
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlCelula" />                                            
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    
                                   <asp:TemplateColumn HeaderText="Tipo Moeda" sortexpression="TipoMoeda" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblTipoMoeda" Text='<%# ((Meta)Container.DataItem).TipoMoeda.Descricao %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlTipoMoeda" />                                            
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:DropDownList runat="server" ID="ddlTipoMoeda" />                                            
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn HeaderText="Valor" sortexpression="Valor" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblValor" Text='<%# ((Meta)Container.DataItem).Valor.ToString("N2") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <Anthem:NumericTextBox runat="server" ID="txtValor" text='<%# ((Meta)Container.DataItem).Valor.ToString("N2") %>' DecimalPlaces="2"/>
                                            <Anthem:RequiredFieldValidator runat="server" ID="valValor" ControlToValidate="txtValor"  
                                                 Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:NumericTextBox runat="server" ID="txtValor" DecimalPlaces="2" />
                                            <Anthem:RequiredFieldValidator runat="server" ID="valValor" ControlToValidate="txtValor"  
                                                 Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    
                                     <asp:TemplateColumn HeaderText="Quantidade" sortexpression="Quantidade" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblQuantidade" Text='<%# ((Meta)Container.DataItem).Quantidade %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <Anthem:NumericTextBox runat="server" ID="txtQuantidade" text='<%# ((Meta)Container.DataItem).Quantidade %>' DecimalPlaces="0"/>
                                            <Anthem:RequiredFieldValidator runat="server" ID="valQuantidade" ControlToValidate="txtQuantidade"  
                                                 Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:NumericTextBox runat="server" ID="txtQuantidade" DecimalPlaces="0" />
                                            <Anthem:RequiredFieldValidator runat="server" ID="valQuantidade" ControlToValidate="txtQuantidade"  
                                                 Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    
                                     <asp:TemplateColumn HeaderText="Total" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblSubTotal" Text='<%# (((Meta)Container.DataItem).Quantidade * ((Meta)Container.DataItem).Valor).ToString("N2") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <Anthem:Label runat="server" ID="lblSubTotal" Text='<%# (((Meta)Container.DataItem).Quantidade * ((Meta)Container.DataItem).Valor).ToString("N2") %>' />
                                        </EditItemTemplate>
                                        <FooterTemplate>                                           
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    
                                     <asp:TemplateColumn HeaderText="Unidade" sortexpression="UnidadeMedida" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <Anthem:Label runat="server" ID="lblUnidade" Text='<%# ((Meta)Container.DataItem).UnidadeMedida %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <Anthem:TextBox runat="server" ID="txtUnidade" text='<%# ((Meta)Container.DataItem).UnidadeMedida %>' Columns="8"/>
                                            <Anthem:RequiredFieldValidator runat="server" ID="valUnidade" ControlToValidate="txtUnidade"  
                                                 Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <Anthem:TextBox runat="server" ID="txtUnidade" Columns="8" />
                                            <Anthem:RequiredFieldValidator runat="server" ID="valUnidade" ControlToValidate="txtUnidade"  
                                                 Display="Dynamic" ValidationGroup="Descricao" ErrorMessage="Campo Obrigatório" /> 
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                   
                                   
                                   
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
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
                                            <FooterTemplate>
                                                <Anthem:LinkButton runat="server" ID="btnSalvarNovo" Text="Salvar" CommandName="Insert" 
                                                    CausesValidation="true" ValidationGroup="Descricao"/>
                                                &nbsp;
                                                <Anthem:LinkButton runat="server" ID="btnCancelarNovo" Text="Cancelar" CommandName="Cancel" 
                                                    CausesValidation="false"/>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                 
                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <Anthem:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" ImageUrl="~/images/ico_del.gif"
                                                ToolTip="Excluir" />                                               
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                      
                                </Columns>
                            </anthem:DataGrid>
                            <Anthem:Panel runat="server" ID="pnMensagem" CssClass="msgErro" Visible="false" >
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
            <td width="10px">
                &nbsp;
            </td>
        </tr>
    </table>
    </div>    
    </form>    
</body>
</html>
