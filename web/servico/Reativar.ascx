<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Reativar.ascx.cs" Inherits="ucReativar" %>
<Anthem:Window runat="server" ID="winReativar" style="left:120px;" Caption="Reativar PS" OnClientShow="window.parent.expandiframe()" ScrollBars="Auto" 
    Width="500px"  Height="230px" Modal="true">

Deseja excluir os orçamentos existentes?
<br />
<br />
<Anthem:RadioButton ID="rbExcluir" runat="server" Text="Sim, excluir os orçamentos deste PS." Checked="True" GroupName="Reativar" /><br/>
<Anthem:RadioButton ID="rbNaoExcluir" runat="server" Text="Não, mantenha os orçamentos." GroupName="Reativar" /><br/>


<div style="text-align:right">
<Anthem:Button runat="server" ID="btnOk" CssClass="Button" Text="Ok" EnabledDuringCallBack="false" 
    TextDuringCallBack="Aguarde" CausesValidation="False" />&nbsp;
<Anthem:Button runat="server" ID="btnCancelar" CssClass="Button" Text="Cancelar" CausesValidation="false" />&nbsp;
</div>


</Anthem:Window>