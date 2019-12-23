<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFamodAprovacaoPesquisa.aspx.cs" Inherits="frmFamodAprovacaoPesquisa" %>
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
        Aprovação de FAMOD
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="94%" style="height:350px;" >																		    
        <tr>
            <td valign="top">                
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >                   
                    <tr>
                        <td valign="top" align="center">                          
                        <div>
                            Oficina: &nbsp;
                            <Anthem:DropDownList runat="server" ID="ddlOficina" />
                            &nbsp;
                          
                             <Anthem:Button runat="server" ID="btnPesquisar"  TextDuringCallBack="Aguarde" Text="Filtrar"
                                                EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />
                            </div>
                               <Anthem:DataList Runat="server" ID="dlOficina" RepeatColumns="1" RepeatLayout="Table"
										RepeatDirection="Vertical" Width="98%" OnItemDataBound="dlOficina_ItemDataBound" >
										<ItemTemplate>
											<table cellpadding="2" width="100%" style="border: solid 1px black;">
												<tr runat="server" id="trOficina" >
													<td align="center" width="20px">
														<asp:Image Runat="server" ImageUrl="../images/minus.gif" ID="imgDetalheOficina" />
													</td>													
													<td align="center" width="30%">
														Dia:
														<Anthem:Label runat="server" ID="lblData" Text='<%# ((FamodOficina)Container.DataItem).Data.ToShortDateString() %>' Font-Bold="true" />
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
											<div runat="server" align="right" id="divServidor" >
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
																				<asp:Image Runat="server" ImageUrl="../images/minus.gif" ID="imgDetalheServidor" />
																			</td>																																					
																			<td align="center" width="50%">
																				Servidor:
																				<b><%# ((FamodServidor)Container.DataItem).Servidor.Identificacao %></b>
																			</td>
																			<td align="center" >
																				Total Horas:
																				<Anthem:Label runat="server" ID="lblHoras" Font-Bold="true" 
																				    Text='<%# ((FamodServidor)Container.DataItem).TotalHoras %>' />
																			</td>
																			<td align="center" width="30px">																				
																				<Anthem:CheckBox runat="server" ID="chkAprovar" />
																			</td>																				
																			<%--<td align="center" width="50px">
																			    <a href='<%# "frmFamodAprovacao.aspx?data=" + ((FamodServidor)Container.DataItem).Data.ToShortDateString() + "&id_servidor=" + ((FamodServidor)Container.DataItem).Servidor.ID.ToString() %>'>Aprovar</a>																																								
																			</td>--%>																				
																		</tr>
																	</table>
																	
																	<div runat="server" id="divFamod" >
																		<table width="100%" border="0" cellpadding="0" cellspacing="0">
																			<tr>
																				<td width="25px">
																					&nbsp;
																				</td>
																				<td align="center" valign="top">
																				   <anthem:GridView runat="server" ID="gvFamod" Width="100%" CssClass="datagrid"
                                                                                         AutoGenerateColumns="false" CellPadding="3" AllowSorting="false" OnRowDataBound="gvFamod_RowDataBound" >
                                                                                        <HeaderStyle CssClass="dgHeader" />                                    
                                                                                        <RowStyle CssClass="dgItem" />
                                                                                        <AlternatingRowStyle CssClass="dgAlternatingItem" />
                                                                                        <FooterStyle CssClass="dgFooter" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="PS" ItemStyle-HorizontalAlign="center" SortExpression="PedidoServico" >
                                                                                                <ItemTemplate>
                                                                                                    <Anthem:LinkButton runat="server" ID="lnkPS" Text="<%# ((Famod)Container.DataItem).PedidoServico %>" />
                                                                                                </ItemTemplate>                                                                                            
                                                                                            </asp:TemplateField>                                                                                             
                                                                                           
                                                                                            <asp:BoundField HeaderText="Atividade" ItemStyle-HorizontalAlign="center"  SortExpression="Atividade" 
                                                                                                DataField="Atividade" />
                                                                                            <asp:BoundField HeaderText="Horas" ItemStyle-HorizontalAlign="center"  SortExpression="HorasApropriadas" 
                                                                                                DataField="HorasApropriadas" />
                                                                                            <asp:TemplateField HeaderText="Situação" ItemStyle-HorizontalAlign="center">
                                                                                                <ItemTemplate>
                                                                                                    <%# Shared.Common.Util.GetDescription(((Famod)Container.DataItem).SituacaoFAMOD) %>
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
               <Anthem:Button runat="server" ID="btnAprovar" TextDuringCallBack="Aguarde" Text="Aprovar Itens Selecionados"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" Width="250px"  /> 
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
