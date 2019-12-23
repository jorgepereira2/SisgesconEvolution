<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFaturamento.aspx.cs" Inherits="frmFaturamento" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register Src="InsereFaturamento.ascx" TagName="InsereFaturamento" TagPrefix="uc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />    
    <script language="javascript">
        function AtualizarPesquisa()
        {
            Anthem_InvokePageMethod("btnPesquisar_Click", [], function(result){});
        }
    
        function Mostrar(control, imagem)
	    {
		    var div = document.getElementById(control);
		    var img = document.getElementById(imagem);
		    if(div.style.display=='none')
		    {
			    div.style.display = '';
			    img.src = "../images/minus.gif"
		    }
		    else
		    {
			    div.style.display = 'none';
			    img.src = "../images/plus.gif"
		    }
		    window.parent.iframeresize();
	    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc:InsereFaturamento runat="server" ID="ucFaturamento" />
    <div align="center">
    
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Faturamento PS
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
                                            Número PS/Cliente:
                                        </td>
                                        <td align="left">
                                            <Anthem:TextBox runat="server" ID="txtTexto" />
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td align="right">
                                            Status:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlStatus" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Status Faturamento:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlJaFaturados" >
                                                <asp:ListItem Value="0">Todos</asp:ListItem>
                                                <asp:ListItem Value="True">Emitido</asp:ListItem>
                                                <asp:ListItem Value="False">Não Emitido</asp:ListItem>
                                            </Anthem:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Ano:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlAno" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Data:
                                        </td>
                                        <td align="left">
                                           <Anthem:DateTextBox runat="server" ID="txtDataInicio" /> &nbsp;a&nbsp; 
                                            <Anthem:DateTextBox runat="server" ID="txtDataFim" />
                                            <Anthem:Button runat="server" ID="btnPesquisar"  TextDuringCallBack="Aguarde" Text="Pesquisar"
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
                               <Anthem:DataList Runat="server" ID="dlOrcamento" RepeatColumns="1" RepeatLayout="Table"
										RepeatDirection="Vertical" Width="98%" OnItemDataBound="dlOrcamento_ItemDataBound" >
										<ItemTemplate>
											<table cellpadding="2" width="100%" style="border: solid 1px black;">
												<tr runat="server" id="trOrcamento" >
													<td align="center" width="20px">
														<asp:Image Runat="server" ImageUrl="../images/plus.gif" ID="imgDetalheOrcamento" />
													</td>													
													<td align="center" width="30%">
														PS:
														<b><%# ((DelineamentoOrcamento)Container.DataItem).CodigoComAno %></b>
													</td>
													<td align="center" width="30%">
														Cliente:
														<b><%# ((DelineamentoOrcamento)Container.DataItem).Cliente %></b>
													</td>
													<td align="center" width="15%">
														Valor Total:
														<b><%# ((DelineamentoOrcamento)Container.DataItem).ValorTotalOrcamento.ToString("C2") %></b>
													</td>	
													<td align="center" width="15%">
														<Anthem:LinkButton runat="server" ID="lnkFaturar" Text="Faturar" CommandName="Faturar" 
														    CommandArgument='<%# ((DelineamentoOrcamento)Container.DataItem).ID %>' CausesValidation="false" />
													</td>
												</tr>
											</table>
											<div runat="server" align="right" id="divFaturamento" style="display:none;">
												<table width="100%" border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td width="25px">
															&nbsp;
														</td>
														<td align="center" valign="top">
															 <anthem:GridView runat="server" ID="gvFaturamento" Width="100%" CssClass="datagrid" OnRowDataBound="gvFaturamento_RowDataBound"
                                                                 AutoGenerateColumns="false" CellPadding="3" AllowSorting="false" >
                                                                <HeaderStyle CssClass="dgHeader" />                                    
                                                                <RowStyle CssClass="dgItem" />
                                                                <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                                                <FooterStyle CssClass="dgFooter" />
                                                                <Columns>                                                                                            
                                                                    <asp:BoundField HeaderText="Data" ItemStyle-HorizontalAlign="center"  SortExpression="Data" 
                                                                        DataField="Data" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false"/>
                                                                    <asp:BoundField HeaderText="Validade" ItemStyle-HorizontalAlign="center"  SortExpression="Validade" 
                                                                        DataField="Validade" />
                                                                    <asp:BoundField HeaderText="Valor" ItemStyle-HorizontalAlign="right"  SortExpression="Valor" 
                                                                        DataField="Valor" DataFormatString="{0:C2}" HtmlEncode="false" />
                                                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center">
                                                                        <ItemTemplate>
                                                                            <Anthem:LinkButton runat="server" ID="lnkImprimir" Text="Imprimir" />
                                                                        </ItemTemplate>                                                                                            
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center">
                                                                        <ItemTemplate>
                                                                             <a href="#" onclick='javascript:Excluir(<%# ((DelineamentoOrcamentoFaturamento)Container.DataItem).ID %>)' >
                                                                                Excluir</a>
                                                                        </ItemTemplate>                                                                                            
                                                                    </asp:TemplateField>                                                                 
                                                                </Columns>
                                                            </anthem:gridview>
														</td>
													</tr>
												</table>																
											</div>															
										</ItemTemplate>
									</anthem:DataList>
   
                            <Anthem:Panel runat="server" ID="pnMensagem" >
                                <br />
                                <anthem:Label runat="server" ID="lblMensagem" CssClass="msgErro" />
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
    <br /><br /><br />  
    </form>    
</body>
</html>
