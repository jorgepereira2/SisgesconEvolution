<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SaldoAC.cs" Inherits="SaldoAC" %>

 <anthem:GridView runat="server" ID="dgSaldo" Width="100%" CssClass="datagrid" AutoGenerateColumns="false" CellPadding="3" AllowSorting="false" >
    <HeaderStyle CssClass="dgHeader" />                                    
    <RowStyle CssClass="dgItem" Wrap="False" />
    <AlternatingRowStyle CssClass="dgAlternatingItem" />
    <FooterStyle CssClass="dgFooter" />
    <Columns> 
                
        <asp:BoundField HeaderText="Sub-Item Natureza" ItemStyle-HorizontalAlign="left"  SortExpression="Descricao" DataField="Descricao" ReadOnly="true" />             
        <asp:BoundField HeaderText="Total AC" ItemStyle-HorizontalAlign="right"  DataFormatString="{0:C2}"  SortExpression="ValorAC" DataField="ValorAC" ReadOnly="true" />             
        <asp:BoundField HeaderText="Saldo Anual Utilizado" ItemStyle-HorizontalAlign="right" DataFormatString="{0:C2}"  SortExpression="SaldoUtilizado" DataField="SaldoUtilizado" ReadOnly="true" />             
        <asp:BoundField HeaderText="Saldo Comprometido" ItemStyle-HorizontalAlign="right" DataFormatString="{0:C2}"  SortExpression="SaldoComprometido" DataField="SaldoComprometido" ReadOnly="true" />             
        <asp:BoundField HeaderText="Saldo Disponível" ItemStyle-HorizontalAlign="right" DataFormatString="{0:C2}"  SortExpression="SaldoDisponivel" DataField="SaldoDisponivel" ReadOnly="true" />             
                
    </Columns>
</anthem:GridView>