<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFamodPorApropriacao.aspx.cs" Inherits="frmFamodPorApropriacao" %>
<%@ Import namespace="System.Data"%>
<%@ Register TagPrefix="WebControls" Assembly="Shared.WebControls" Namespace="Shared.WebControls" %>
<%@ Register Src="~/UserControls/ucColumnManager.ascx" TagPrefix="uc" TagName="ColumnManager" %>
<%@ Register Src="~/UserControls/CabecalhoRelatorio.ascx" TagPrefix="uc" TagName="Cabecalho" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet"  />   
    
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="FAMOD por Apropriação" />    
    <div>
    <br />
    <asp:LinkButton runat="server" ID="btnExportar" Text="Exportar para o Excel" cssclass="noprint" />    
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >
      <asp:DataList runat="server" ID="dlApropriacao" Width="100%">
        <ItemTemplate>
            <table border="0" cellpadding="2" width="100%" style="border-bottom-width:1px;">
                <tr>
                    <td align="left" style="width:50%">
                        <b style="font-size:12px">
                            <%# ((DataRowView)Container.DataItem)["Descricao"].ToString() %> 
                        </b>       
                    </td>
                    <td align="right">
                        
                    </td>
                </tr>
            </table>           
             
            </div>
            <ul>
                           
                <Anthem:DataGrid runat="server" ID="dgAtividade" AutoGenerateColumns="True" Width="100%"
                    CssClass="datagrid" OnItemDataBound="dgAtividade_ItemDataBound" OnItemCreated="dgAtividade_ItemCreated" ShowFooter="true" >
                    <HeaderStyle CssClass="dgHeader" /> 
                    <ItemStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Atividade" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                 <%# ((DataRowView)Container.DataItem)["Atividade"].ToString().Substring(((DataRowView)Container.DataItem)["Atividade"].ToString().LastIndexOf("|") + 1)%>    
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </Anthem:DataGrid>
                    
                <br />
            </ul>
        </ItemTemplate>      
      </asp:DataList>
    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>
