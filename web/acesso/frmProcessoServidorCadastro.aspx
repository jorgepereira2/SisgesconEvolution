<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmProcessoServidorCadastro.aspx.cs" Inherits="Acesso_frmProcessoServidorCadastro" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../css/treeStyle.css" type="text/css" rel="stylesheet" />
      <link href="../css/contextMenu.css" type="text/css" rel="stylesheet" />
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
      <link href="../css/tabStyle.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
       function Expandir(ancora)
        {    
            document.body.disabled = 'disabled';
            document.body.style.cursor = 'wait';
            
            if(document.all)
            {
                if(ancora.innerText == 'Expandir Todos')
                {
                    setTimeout('ExpandirTudo()',100)
                    ancora.innerText = 'Recolher Todos';    
                }
                else
                {
                    setTimeout('RecolherTudo()',100)
                    ancora.innerText = 'Expandir Todos';
                }
            }
            else
            {
                if(ancora.textContent == 'Expandir Todos')
                {
                    setTimeout('ExpandirTudo()',100)
                    ancora.textContent = 'Recolher Todos';    
                }
                else
                {
                    setTimeout('RecolherTudo()',100)
                    ancora.textContent = 'Expandir Todos';
                }
            }
        }
        
        function ExpandirTudo()
        {
            tvProcesso.ExpandAll();
            document.body.disabled = '';
            document.body.style.cursor = '';
            
        }
        
        function RecolherTudo()
        {
            tvProcesso.CollapseAll();
            document.body.disabled = '';
            document.body.style.cursor = '';
            
        }
        
        function treeContextMenu(treeNode, e)
        {
            mnuContexto.ShowContextMenu(e, treeNode);
        }
    
        function contextMenuClickHandler(menuItem)
        {
            var contextDataNode = menuItem.ParentMenu.ContextData;
            
            if(menuItem.Value == 'Marcar')
                contextDataNode.CheckAll();
             else if(menuItem.Value == 'Desmarcar')
                contextDataNode.UnCheckAll();
                
           contextDataNode.SaveState();    
           tvProcesso.Render();
          return true; 
        }
        
        var marcado = false;
        function Marcar(obj)
        {
            document.getElementById('imgIndicador').style.display = '';
            
            if(marcado == false)
            {                
                setTimeout('MarcarTudo(false)',1000);
            }
            else
            {
                setTimeout('MarcarTudo(true)',1000);               
            }           
        }
        
        function MarcarTudo(obj)
        {       
            var novoTexto;
            if(obj == false)
            {
                novoTexto = 'Desmarcar Todos';
                tvProcesso.CheckAll();
                marcado = true;
            }
            else
            {                
                novoTexto = 'Marcar Todos'
                tvProcesso.UnCheckAll();
                marcado = false;
            }            
            
            if(document.all)
                document.getElementById('aMarcar').innerText = novoTexto;
            else
                document.getElementById('aMarcar').textContent = novoTexto;


            tvProcesso.Render();
            document.getElementById('imgIndicador').style.display = 'none';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="width:100%" >
    <div align="right" style="width:90%">
    <br />
        <asp:label runat="server" labelid="ControleAcesso" CssClass="PageTitle" Text="Controle de Acesso">
            </asp:label>:
        
        <asp:Label runat="server" ID="lblServidor" CssClass="PageTitle" />    
        &nbsp;
    
    </div>
    <div align="left" style="width:90%">
     
      <ComponentArt:TabStrip id="TabStrip1" 
          CssClass="TopGroup"                  
          DefaultItemLookId="DefaultTabLook"
          DefaultSelectedItemLookId="SelectedTabLook"
          DefaultDisabledItemLookId="DisabledTabLook" 
          DefaultGroupTabSpacing="0" TopGroupAlign="Left" DefaultGroupAlign="Left" DefaultItemTextAlign="Left"
          ImagesBaseUrl="../images/tabstrip/"
          runat="server" MultiPageId="MultiPage1">
        <Tabs>
            <ComponentArt:TabStripTab Text="Processos" PageViewId="pvProcessos" LookId="DefaultTabLook" runat="server" ID="tabProcessos" ClientSideCommand="parent.iframeresize();" />
            <ComponentArt:TabStripTab Text="Regras de Acesso" PageViewId="pvRegrasAcesso" LookId="DefaultTabLook"  runat="server" ID="tabRegrasAcesso" ClientSideCommand="parent.iframeresize();" />            
        </Tabs>  
        <ItemLooks>
          <ComponentArt:ItemLook LookId="DefaultTabLook" CssClass="DefaultTab" HoverCssClass="DefaultTabHover" LabelPaddingLeft="10" LabelPaddingRight="10" LabelPaddingTop="5" LabelPaddingBottom="4" LeftIconUrl="tab_left_icon.gif" RightIconUrl="tab_right_icon.gif" HoverLeftIconUrl="hover_tab_left_icon.gif" HoverRightIconUrl="hover_tab_right_icon.gif" LeftIconWidth="3" LeftIconHeight="21" RightIconWidth="3" RightIconHeight="21" />
          <ComponentArt:ItemLook LookId="SelectedTabLook" CssClass="SelectedTab" LabelPaddingLeft="10" LabelPaddingRight="10" LabelPaddingTop="4" LabelPaddingBottom="4" LeftIconUrl="selected_tab_left_icon.gif" RightIconUrl="selected_tab_right_icon.gif" LeftIconWidth="3" LeftIconHeight="21" RightIconWidth="3" RightIconHeight="21" />
        </ItemLooks>
        </ComponentArt:TabStrip>
        <ComponentArt:MultiPage id="MultiPage1" CssClass="MultiPage" runat="server" Width="100%">
            <ComponentArt:PageView CssClass="PageContent" id="pvProcessos" runat="server">
                <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
                    <tr   >
                        <td valign="middle" align="left" style="border:solid 0px black" >		              
                            <br />                                          
                             <a class="linkpadrao" id="aMarcar" href="#" onclick="Marcar(this);" runat="server">Marcar Todos</a>
                            &nbsp;
                            <img id="imgIndicador" src="../images/indicator_arrows.gif" style="display:none;" />
                            <br />
                        <ComponentArt:TreeView id="tvProcesso" Width="500px" Height="350px"  
                            DragAndDropEnabled="false" KeyboardEnabled="true"
                            CssClass="TreeView"  
                            NodeCssClass="TreeNode" 
                            HoverNodeCssClass="HoverTreeNode" 
                            SelectedNodeCssClass="SelectedTreeNode"
                            NodeEditCssClass="NodeEdit" 
                            DefaultImageWidth="16"
                            DefaultImageHeight="16"  														                                    
                            ExpandCollapseImageWidth="17"
                            ExpandCollapseImageHeight="15" 
                            NodeLabelPadding="3" 
                            ItemSpacing="2" 
                            NodeIndent="17" 
                            LineImagesFolderUrl="../images/treeview"
                            showlines="true" 
                            ParentNodeImageUrl="../images/treeview/folder.gif" 
                            LeafNodeImageUrl="../images/treeview/tasks.gif"
                            ExpandImageUrl="../images/treeview/col.gif" 
                            CollapseImageUrl="../images/treeview/exp.gif" 
                            EnableViewState="true"
                            runat="server" AutoPostBackOnSelect="false" 
                            AutoScroll="true" 														                                   													                                
                            CausesValidation="False" 
                            ExpandNodeOnSelect="true"
                            ExpandSelectedPath="true"
                             
                             OnContextMenu="treeContextMenu"
                            BorderStyle="Solid" BorderColor="black" BorderWidth="1px" />
                      
                        <a class="linkpadrao" id="aExpandir" href="#" onclick="Expandir(this);">Expandir Tudo</a>
                        
                        
                            <ComponentArt:Menu id="mnuContexto" 
                                  Orientation="Vertical"
                                  DefaultGroupCssClass="MenuGroup"                                      
                                  DefaultItemLookID="DefaultItemLook"
                                  DefaultGroupItemSpacing="1"
                                  ImagesBaseUrl="../images/contextMenu/"
                                  EnableViewState="false"
                                  ContextMenu="Custom"
                                  ShadowEnabled="true"
                                  ClientSideOnItemSelect="contextMenuClickHandler"
                                  runat="server">
                                <ItemLooks>
                                  <ComponentArt:ItemLook LookID="LookNovo" CssClass="MenuItem" LeftIconUrl="checked.gif" HoverCssClass="MenuItemHover" ExpandedCssClass="MenuItemHover" LeftIconWidth="20" LeftIconHeight="18" LabelPaddingLeft="10" LabelPaddingRight="10" LabelPaddingTop="3" LabelPaddingBottom="4" />
                                  <ComponentArt:ItemLook LookID="LookExcluir" CssClass="MenuItem" LeftIconUrl="X.gif" HoverCssClass="MenuItemHover" ExpandedCssClass="MenuItemHover" LeftIconWidth="20" LeftIconHeight="18" LabelPaddingLeft="10" LabelPaddingRight="10" LabelPaddingTop="3" LabelPaddingBottom="4" />
                                </ItemLooks>
                                <Items>
                                    <ComponentArt:MenuItem Text="Marcar Filhos" id="mnuMarcarFilhos" Value="Marcar" runat="server" LookId="LookNovo" />
                                    <ComponentArt:MenuItem ID="mnuDesmarcarFilhos" Text="Desmarcar Filhos"  Value="Desmarcar"  runat="server" LookId="LookExcluir" />
                                </Items>
                               </ComponentArt:Menu>

                        </td>            
                    </tr>																
                </table>
            </ComponentArt:PageView>
            
            
            <ComponentArt:PageView CssClass="PageContent" id="pvRegrasAcesso" runat="server" width="100%">
                <br />
                 <table border="0" cellpadding="2" cellspacing="4" width="100%" >
                    <tr>
                        <td colspan="3">
                            <div align="left" style="vertical-align:text-bottom" Class="PageTitle">
                                 Controle de Horário
                                <hr size="1" class="divisor" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" >&nbsp;</td>
                        <td align="right" width="25%">
                           Controle:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkControlarHorario" Text="Controlar o horário de acesso desta pessoa" AutoCallBack="true"  />
                        </td>
                    </tr>
                     <tr runat="server" id="trIntervalo">
                        <td >&nbsp;</td>
                        <td align="right" >
                           Período:
                        </td>
                        <td align="left">
                           <Anthem:TimeTextBox runat="server" ID="txtHorarioInicio" />&nbsp;a&nbsp;
                           <Anthem:TimeTextBox runat="server" ID="txtHorarioFim" />                           
                        </td>
                    </tr>
                    <tr runat="server" id="trDias">
                        <td >&nbsp;</td>
                        <td align="right" >
                           Dias da Semana:
                        </td>
                        <td align="left">
                           <table border="0" width="600px" cellpadding="4px">
		                        <tr>
		                            <td align="left">
		                                <anthem:checkbox runat="server" id="chkDomingo" text="Domingo" />        
		                            </td>
		                            <td align="left">
		                                <anthem:checkbox runat="server" id="chkSegunda" text="Segunda" />        
		                            </td>
		                            <td align="left">
		                                <anthem:checkbox runat="server" id="chkTerca" text="Terça" />        
		                            </td>
		                            <td align="left">
		                                <anthem:checkbox runat="server" id="chkQuarta" text="Quarta" />        
		                            </td>
		                        </tr>
		                        <tr>
		                            <td align="left">
		                                <anthem:checkbox runat="server" id="chkQuinta" text="Quinta" />        
		                            </td>
		                            <td align="left">
		                                <anthem:checkbox runat="server" id="chkSexta" text="Sexta" />        
		                            </td>
		                            <td align="left">
		                                <anthem:checkbox runat="server" id="chkSabado" text="Sábado" />        
		                            </td>
		                            <td>
		                            </td>
		                        </tr>
		                    </table>
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="3">
                            <div align="left" style="vertical-align:text-bottom" Class="PageTitle">
                                 Controle de Local
                                <hr size="1" class="divisor" />
                            </div>
                        </td>
                    </tr>
                     <tr>
                        <td width="10%" >&nbsp;</td>
                        <td align="right" >
                           Controle:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkControlarLocal" Text="Controlar os locais de acesso desta pessoa" 
                            AutoCallBack="true"  />
                        </td>
                    </tr> 
                     <tr runat="server" id="trLocalAcesso">
                        <td >&nbsp;</td>
                        <td align="right" >
                           Locais de Acesso:
                        </td>
                        <td align="left">
                           <Anthem:CheckBoxList CellSpacing="20" RepeatColumns="3" RepeatDirection="Horizontal" runat="server" ID="cblLocalAcesso" />
                        </td>
                    </tr>  
                     <tr>
                        <td colspan="3">
                            <div align="left" style="vertical-align:text-bottom" Class="PageTitle">                                
                                    Perfis de Acesso
                                <hr size="1" class="divisor" />
                            </div>
                        </td>
                    </tr>
                     <tr runat="server" id="tr1">
                        <td width="10%" >&nbsp;</td>
                        <td align="right" >
                           Perfis:
                        </td>
                        <td align="left">
                           <Anthem:CheckBoxList CellSpacing="20" RepeatColumns="3" RepeatDirection="Horizontal" runat="server" ID="cblPerfil" />
                        </td>
                    </tr>
                    
                     <tr>
                        <td colspan="3">
                            <div align="left" style="vertical-align:text-bottom" Class="PageTitle">
                                   Restrição Material
                                <hr size="1" class="divisor" />
                            </div>
                        </td>
                    </tr>
                     <tr runat="server" id="tr3">
                        <td  >&nbsp;</td>
                        <td align="right" >
                           Restringir Acesso:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkRestringirAcessoMaterial" />
                        </td>
                    </tr> 
                     <tr runat="server" id="tr4">
                        <td width="10%" >&nbsp;</td>
                        <td align="right" valign="top" >
                           SJBs Liberados:
                        </td>
                        <td align="left">
                           <Anthem:CheckBoxList CellSpacing="20" RepeatColumns="3" RepeatDirection="Horizontal" runat="server" ID="cblSJBLiberados" />
                        </td>
                    </tr>
                    
                    
                     <tr id="trAvancado" runat="server">
                        <td colspan="3">
                            <div align="left" style="vertical-align:text-bottom" Class="PageTitle">                                
                                    Avançado
                                <hr size="1" class="divisor" />
                            </div>
                        </td>
                    </tr>                     
                     <tr runat="server" id="trFlagPodeVerPAOutraCelula">
                        <td  >&nbsp;</td>
                        <td align="right" >
                           Pode ver PA's de outros departamentos:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkFlagPodeVerPAOutraCelula" />
                        </td>
                    </tr> 
                    <tr runat="server" id="tr2">
                        <td width="10%" >&nbsp;</td>
                        <td align="right" >
                           Pode fazer PO's de outros departamentos:
                        </td>
                        <td align="left">
                           <Anthem:CheckBox runat="server" ID="chkFlagPodeFazerPOOutraCelula" />
                        </td>
                    </tr>                                          
                 </table>  
            </ComponentArt:PageView>
            
            </ComponentArt:MultiPage>
    </div>
    <table class="PageFooter" cellpadding="0" cellspacing="0">
        <tr>
            <td width="40%" align="left">                
            </td>
            <td align="right">
                <Anthem:Button runat="server" ID="btnSalvar" TextDuringCallBack="Aguarde" Text="Salvar"
                     EnabledDuringCallBack="false" EnableCallBack="false" CssClass="Button" CausesValidation="false" />
                &nbsp;
                <Anthem:Button runat="server" ID="btnEnviarSenha" TextDuringCallBack="Aguarde" Text="Enviar Senha Email" Width="165px"
                     EnabledDuringCallBack="false" EnableCallBack="true" CssClass="Button" CausesValidation="false"  />
                &nbsp;
                <Anthem:Button runat="server" ID="btnVoltar" Text="Voltar"
                     EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" />     
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
