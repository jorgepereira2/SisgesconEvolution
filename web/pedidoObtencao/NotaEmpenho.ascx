<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NotaEmpenho.ascx.cs" Inherits="NotaEmpenho" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register TagPrefix="uc" TagName="SaldoServicoMaterial" Src="~/pedidoObtencao/SaldoServicoMaterial.ascx" %>
<Anthem:Window runat="server" ID="winNotaEmpenho" style="left:120px;" Caption="Nota de Empenho" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="580px"  Height="420px" Modal="true">

<br />
<br />
Número do Empenho:<br />
<Anthem:TextBox runat="server" ID="txtNotaEmpenho" Width="90%" MaxLength="30" />
<Anthem:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="NotaEmpenho" ErrorMessage="Campo obrigatório"
    ControlToValidate="txtNotaEmpenho" Display="Dynamic" />
<br />
Ação Interna:<br />
<Anthem:DropDownList runat="server" ID="ddlProjeto" AutoCallBack="true" OnSelectedIndexChanged="AtualizaSaldo"/>
<br />
<%--Natureza Despesa:<br />
<Anthem:DropDownList runat="server" ID="ddlNaturezaDespesa" AutoCallBack="true" OnSelectedIndexChanged="AtualizaSaldo" />
<br />--%>
PTRES:<br />
<%--<Anthem:DropDownList runat="server" ID="ddlPTRES" AutoCallBack="true" OnSelectedIndexChanged="AtualizaSaldo" />--%>
<Anthem:TextBox runat="server" ID="txtPTRES" Width="90%" MaxLength="100" />
<br />
Código Gestão:<br />
<Anthem:TextBox runat="server" ID="txtCodigoGestao" Width="90%" MaxLength="10" />
<br />
Lista:<br />
<Anthem:TextBox runat="server" ID="txtLista" Width="90%" MaxLength="10" />
<%--<br />
Número Lançamento:<br />
<Anthem:TextBox runat="server" ID="txtNumeroLancamento" Width="90%" MaxLength="10" />--%>
<br />
Comentário:<br />
<Anthem:TextBox runat="server" ID="txtComentario" Width="90%" MaxLength="100" />


<div align="center" style="vertical-align:text-bottom" class="PageTitle">
    
</div>
<Anthem:Button runat="server" ID="btnAdicionar" CssClass="Button" Text="Adicionar" ValidationGroup="NotaEmpenho" EnabledDuringCallBack="false"
    TextDuringCallBack="Aguarde" />&nbsp;
<br />

   <anthem:DataGrid runat="server" ID="dgEmpenho" Width="98%" CssClass="datagrid" AutoGenerateColumns="false" CellPadding="3" ShowFooter="false" >
        <HeaderStyle CssClass="dgHeader" />                                    
        <ItemStyle CssClass="dgItem" />
        <AlternatingItemStyle CssClass="dgAlternatingItem" />
        <FooterStyle HorizontalAlign="Right" />
        <Columns>
            <asp:TemplateColumn HeaderText="Empenho" ItemStyle-HorizontalAlign="left">
                <ItemTemplate>
                        <%# ((PedidoObtencaoEmpenho)Container.DataItem).NumeroEmpenho %> 
                </ItemTemplate>
            </asp:TemplateColumn>                                      
            <asp:TemplateColumn HeaderText="Ação Interna" ItemStyle-HorizontalAlign="left">
                <ItemTemplate>
                        <%# ((PedidoObtencaoEmpenho)Container.DataItem).Projeto.Descricao %> 
                </ItemTemplate>
            </asp:TemplateColumn>                           
            <asp:TemplateColumn HeaderText="PTRES" ItemStyle-HorizontalAlign="left">
                <ItemTemplate>
                        <%--<%# ((PedidoObtencaoEmpenho)Container.DataItem).PTRES.Descricao %>--%> 
                        <%# ((PedidoObtencaoEmpenho)Container.DataItem).PTRESS %> 
                </ItemTemplate>
            </asp:TemplateColumn>                           
             <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                <ItemTemplate>                   
                    <Anthem:LinkButton runat="server" ID="btnExcluir" Text="Excluir" CommandName="Delete" CausesValidation="false"/>    
                </ItemTemplate>                  
            </asp:TemplateColumn> 
                                                                              
        </Columns>
    </anthem:DataGrid>

<br />


<%--<br />
<div align="left" style="vertical-align:text-bottom" class="PageTitle">
    Saldo
    <hr size="1" class="divisor" />
</div>

Saldo: <Anthem:Label runat="server" ID="lblSaldo" />
<br />
Total Comprometido: <Anthem:Label runat="server" ID="lblComprometido" />
<br />
Custo Simulado: <Anthem:Label runat="server" ID="lblCusto" />
<br />
Saldo Total: <Anthem:Label runat="server" ID="lblSaldoTotal" />
<br /><br />

<div align="left" style="vertical-align:text-bottom" class="PageTitle">
    Saldo por Item
    <hr size="1" class="divisor" />
</div>
--%>
<br />

   <anthem:DataGrid runat="server" ID="dgItem" Width="98%" CssClass="datagrid" AutoGenerateColumns="false" CellPadding="3" ShowFooter="true" >
        <HeaderStyle CssClass="dgHeader" />                                    
        <ItemStyle CssClass="dgItem" />
        <AlternatingItemStyle CssClass="dgAlternatingItem" />
        <FooterStyle HorizontalAlign="Right" />
        <Columns>
            <asp:TemplateColumn HeaderText="Material" ItemStyle-HorizontalAlign="left">
                <ItemTemplate>
                        <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.CodigoInterno %> - 
                    <%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Descricao %>
                </ItemTemplate>
            </asp:TemplateColumn>                                      
                <asp:TemplateColumn HeaderText="Valor 1" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                        <%# ((PedidoObtencaoItem)Container.DataItem).Valor.ToString("N2")  %>      
                </ItemTemplate>                                            
            </asp:TemplateColumn>   
            <asp:TemplateColumn HeaderText="Quantidade" ItemStyle-HorizontalAlign="center">
                <ItemTemplate>
                    <%# ((PedidoObtencaoItem)Container.DataItem).Quantidade.ToString("N2") %>
                    (<%# ((PedidoObtencaoItem)Container.DataItem).ServicoMaterial.Unidade.Descricao %>)
                </ItemTemplate>                                            
            </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Valor Total" ItemStyle-HorizontalAlign="right">
                <ItemTemplate>
                    <%# ((PedidoObtencaoItem)Container.DataItem).ValorTotal.ToString("N2") %>                                               
                </ItemTemplate>                                            
            </asp:TemplateColumn>                                    

                                                                             

                <asp:TemplateColumn HeaderText="Saldo" ItemStyle-HorizontalAlign="center">
                <ItemTemplate>
                    <uc:SaldoServicoMaterial runat="server" ID="ucSaldo" />
                </ItemTemplate>                                            
            </asp:TemplateColumn>                                                                         
        </Columns>
    </anthem:DataGrid>

<br />


<div style="text-align:right">
<Anthem:Button runat="server" ID="btnFinalizar" CssClass="Button" Text="Finalizar" CausesValidation="false" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Fechar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>