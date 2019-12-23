<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFamodPesquisa.aspx.cs" Inherits="frmFamodPesquisa" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Import Namespace="Marinha.Business.UI" %>

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
    <div align="center">
    
    <div align="right" style="width:90%" class="PageTitle">
    <br />
        Pesquisa de FAMOD
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
                                            Servidor:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlServidor" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Oficina:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlOficina" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Status:
                                        </td>
                                        <td align="left">
                                            <Anthem:DropDownList runat="server" ID="ddlSituacao" />
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
                               <Anthem:DataList Runat="server" ID="dlOficina" RepeatColumns="1" RepeatLayout="Table"
										RepeatDirection="Vertical" Width="98%" OnItemDataBound="dlOficina_ItemDataBound" >
										<ItemTemplate>
											<table cellpadding="2" width="100%" style="border: solid 1px black;">
												<tr runat="server" id="trOficina" >
													<td align="center" width="20px">
														<asp:Image Runat="server" ImageUrl="../images/plus.gif" ID="imgDetalheOficina" />
													</td>													
													<td align="center" width="30%">
														Dia:
														<b><%# ((FamodOficina)Container.DataItem).Data.ToShortDateString() %></b>
													</td>
													<td align="center" width="30%">
														Oficina:
														<b><%# ((FamodOficina)Container.DataItem).Oficina.Descricao %></b>
													</td>
													<td align="center" width="15%">
														Total Horas:
														<b><%# ((FamodOficina)Container.DataItem).TotalHoras%></b>
													</td>
													<td align="center" >
														Quantidade:
														<b><%# ((FamodOficina)Container.DataItem).Servidores.Count %></b>
													</td>
												</tr>
											</table>
											<div runat="server" align="right" id="divServidor" style="display:none;">
												<table width="100%" border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td width="25px">
															&nbsp;
														</td>
														<td align="center" valign="top">
															<Anthem:DataList Runat="server" ID="dlServidor" RepeatColumns="1" RepeatLayout="Table" OnItemDataBound="dlServidor_ItemDataBound" 
																	RepeatDirection="Vertical" Width="100%" >
																<ItemTemplate>																		
																	<table cellpadding="2" width="100%" style="border: solid 1px black;" >
																		<tr runat="server" id="trServidor">
																			<td align="center" width="20px">
																				<asp:Image Runat="server" ImageUrl="../images/plus.gif" ID="imgDetalheServidor" />
																			</td>	
																			<td align="center" width="100px">
																				Aprovado:
																				<b><%# ((FamodServidor)Container.DataItem).Aprovado ? "Sim" : "Não" %></b>
																			</td>																		
																			<td align="center" width="50%">
																				Servidor:
																				<b><%# ((FamodServidor)Container.DataItem).Servidor.Identificacao %></b>
																			</td>
																			<td align="center" >
																				Total Horas:
																				<b><%# ((FamodServidor)Container.DataItem).TotalHoras %></b>
																			</td>																			
																		</tr>
																	</table>
																	
																	<div runat="server" id="divFamod" style="display:none;">
																		<table width="100%" border="0" cellpadding="0" cellspacing="0">
																			<tr>
																				<td width="25px">
																					&nbsp;
																				</td>
																				<td align="center" valign="top">
																				   <anthem:GridView runat="server" ID="gvFamod" Width="100%" CssClass="datagrid"
                                                                                         AutoGenerateColumns="false" CellPadding="3" AllowSorting="false" >
                                                                                        <HeaderStyle CssClass="dgHeader" />                                    
                                                                                        <RowStyle CssClass="dgItem" />
                                                                                        <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                                                                        <FooterStyle CssClass="dgFooter" />
                                                                                        <Columns>                                                                                            
                                                                                            <asp:BoundField HeaderText="PS" ItemStyle-HorizontalAlign="center"  SortExpression="PedidoServico" 
                                                                                                DataField="PedidoServico" />
                                                                                            <asp:BoundField HeaderText="Atividade" ItemStyle-HorizontalAlign="center"  SortExpression="Atividade" 
                                                                                                DataField="Atividade" />
                                                                                            <asp:BoundField HeaderText="Horas" ItemStyle-HorizontalAlign="center"  SortExpression="HorasApropriadas" 
                                                                                                DataField="HorasApropriadas" />
                                                                                            <asp:TemplateField HeaderText="Situação" ItemStyle-HorizontalAlign="center">
                                                                                                <ItemTemplate>
                                                                                                    <%# Shared.Common.Util.GetDescription(((Famod)Container.DataItem).SituacaoFAMOD) %>
                                                                                                </ItemTemplate>                                                                                            
                                                                                            </asp:TemplateField>
                                                                                            <asp:HyperLinkField NavigateUrl="" Text="Editar" itemstyle-horizontalalign="center"
                                                                                                 DataNavigateUrlFields="ID" DataNavigateUrlFormatString="frmFamodCadastro.aspx?id_famod={0}" /> 
                                                                                        </Columns>
                                                                                    </anthem:gridview>
																				</td>
																			</tr>
																		</table>																								
																	</div>															
																</ItemTemplate>
															</anthem:DataList>															
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
