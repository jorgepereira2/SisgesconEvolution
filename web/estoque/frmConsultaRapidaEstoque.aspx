<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmConsultaRapidaEstoque.aspx.cs" Inherits="frmConsultaRapidaEstoque" %>
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
    <div align="center">
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Consulta Rápida de Estoque
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
                                <table width="100%" cellpadding="2" cellspacing="2">
                                     <tr>
                                        <td align="right">
                                            Tipo Estoque:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlOrigemMaterial" /> &nbsp;
                                        </td>
                                    </tr>      
                                    <tr>
                                        <td align="right">
                                            Código de Barras:
                                        </td>
                                        <td align="left">
                                            <Anthem:TextBox runat="server" ID="txtCodigoBarras" /> &nbsp;
                                            <Anthem:Button runat="server" ID="btnPesquisar"  TextDuringCallBack="Aguarde" Text="Consultar"
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
                            <Anthem:Panel runat="server" ID="pnMaterial" Visible="false">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="right" >
                                        Material:
                                    </td>
                                    <td align="left" colspan="3">
                                        <Anthem:Label runat="server" ID="lblCodigoInterno" /> - 
                                        <Anthem:Label runat="server" ID="lblDescricao" /> 
                                    </td>
                                    
                                </tr>  
                                 <tr>
                                    <td align="right" >
                                        Quantidade Estoque:
                                    </td>
                                    <td align="left" colspan="3" >
                                        <Anthem:Label runat="server" ID="lblQuantidadeEstoque" /> 
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" >
                                        Quantidade Mínima:
                                    </td>
                                    <td align="left" >
                                        <Anthem:Label runat="server" ID="lblQuantidadeMinima" /> 
                                    </td>
                                    <td align="right" >
                                        Quantidade Máxima:
                                    </td>
                                    <td align="left" >
                                        <Anthem:Label runat="server" ID="lblQuantidadeMaxima" /> 
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" >
                                        Classe:
                                    </td>
                                    <td align="left" >
                                        <Anthem:Label runat="server" ID="lblClasse" /> 
                                    </td>
                                    <td align="right" >
                                        Sub-Classe:
                                    </td>
                                    <td align="left" >
                                        <Anthem:Label runat="server" ID="lblSubClasse" /> 
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" >
                                        Origem:
                                    </td>
                                    <td align="left" >
                                        <Anthem:Label runat="server" ID="lblOrigem" /> 
                                    </td>
                                    <td align="right" >
                                        Unidade:
                                    </td>
                                    <td align="left" >
                                        <Anthem:Label runat="server" ID="lblUnidade" /> 
                                    </td>
                                </tr>
                                    <tr>
                                    <td align="right" >
                                        SJB:
                                    </td>
                                    <td align="left" >
                                        <Anthem:Label runat="server" ID="lblSJB" /> 
                                    </td>
                                    <td align="right" >
                                        Fabricante:
                                    </td>
                                    <td align="left" >
                                        <Anthem:Label runat="server" ID="lblFabricante" /> 
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" >
                                        Descrição SINGRA:
                                    </td>
                                    <td align="left" >
                                        <Anthem:Label runat="server" ID="lblDescricaoSingra" /> 
                                    </td>
                                    <td align="right" >
                                        Número Referência:
                                    </td>
                                    <td align="left" >
                                        <Anthem:Label runat="server" ID="lblNumeroReferencia" /> 
                                    </td>
                                </tr>
					              <tr>
                                    <td align="right" >
                                        Código Siasg:
                                    </td>
                                    <td align="left" >
                                        <Anthem:Label runat="server" ID="lblCodigoSiasg" /> 
                                    </td>
                                    <td align="right" >
                                        Ativo:
                                    </td>
                                    <td align="left" >
                                        <Anthem:Label runat="server" ID="lblAtivo" /> 
                                    </td>
                                </tr>  
					            <tr>
					                <td colspan="4">
					                    <div align="left" style="vertical-align:text-bottom" class="PageTitle" >
                                            Localizações
                                            <hr size="1" class="divisor" style="" />
                                        </div>                                                                
                                        <br />
                                        <anthem:DataGrid runat="server" ID="dgLocalizacao" Width="80%" CssClass="datagrid"
                                             AutoGenerateColumns="false" CellPadding="3" >
                                            <HeaderStyle CssClass="dgHeader" />                                    
                                            <ItemStyle CssClass="dgItem" />
                                            <AlternatingItemStyle CssClass="dgAlternatingItem" />
                                            <FooterStyle CssClass="dgFooter" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Descrição" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <%# ((Localizacao)Container.DataItem).Descricao %>
                                                    </ItemTemplate>                                            
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </anthem:datagrid>
					                </td>
					            </tr>
                                                                                  
					                
                                                       
                            </table>
                            </Anthem:Panel>
                            
                            <Anthem:Panel runat="server" ID="pnMensagem" CssClass="msgErro" Visible="false">
                                <br />
                                Material não encontrado.
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
