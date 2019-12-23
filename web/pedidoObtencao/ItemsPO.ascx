<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsPO.cs" Inherits="ItemsPO" %>
<%@ Import Namespace="Marinha.Business" %>

 <anthem:DataGrid runat="server" ID="dgItems" Width="100%" CssClass="datagrid" AutoGenerateColumns="false" CellPadding="3" AllowSorting="false" >
    <HeaderStyle CssClass="dgHeader" />                                    
    <ItemStyle CssClass="dgItem" />
    <AlternatingItemStyle CssClass="dgAlternatingItem" />
    <FooterStyle HorizontalAlign="Right" BackColor="#F4F4F4" />
    <Columns> 
        <asp:TemplateColumn HeaderText="Material/Serviço" ItemStyle-HorizontalAlign="left">
            <ItemTemplate>
                <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.CodigoInterno %> - 
                <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Descricao %>
            </ItemTemplate>                                            
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Especificação" ItemStyle-HorizontalAlign="left">
            <ItemTemplate>
                <%# ((PedidoObtencaoItem)Container.DataItem).Especificacao %>
            </ItemTemplate>                                            
        </asp:TemplateColumn>   
        <asp:TemplateColumn HeaderText="Natureza Despesa" ItemStyle-HorizontalAlign="center">
            <ItemTemplate>
                <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.NaturezaDespesa != null ? ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.NaturezaDespesa.Codigo : "" %>
            </ItemTemplate>                                            
        </asp:TemplateColumn>                            
        <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
            <ItemTemplate>
                <%# ((PedidoObtencaoItem)Container.DataItem).Quantidade.ToString("N2") %>
            </ItemTemplate>                                            
        </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Valor" ItemStyle-HorizontalAlign="right">
            <ItemTemplate>
                <table border="0">
                    <tr>
                        <td nowrap="nowrap">
                            Valor 1: <%# ((PedidoObtencaoItem)Container.DataItem).Valor.ToString("N2") %>         
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap">
                            Valor 2: <%# ((PedidoObtencaoItem)Container.DataItem).ValorCotacao2.ToString("N2") %>         
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap">
                            Valor 3: <%# ((PedidoObtencaoItem)Container.DataItem).ValorCotacao3.ToString("N2") %>         
                        </td>
                    </tr>
                </table>
            </ItemTemplate>                                            
        </asp:TemplateColumn>                   
        <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
            <ItemTemplate>
                <%# ((PedidoObtencaoItem)Container.DataItem).ValorTotal.ToString("C2") %>
            </ItemTemplate>
        </asp:TemplateColumn>                                                                                                                      
    </Columns>
</anthem:DataGrid>

