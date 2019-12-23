<%@ Page Language="C#" AutoEventWireup="true" CodeFile="relProcessoPesquisa.aspx.cs" Inherits="Acesso_frmProcessoCadastro" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Import Namespace="Marinha.Business" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../css/treeStyle.css" type="text/css" rel="stylesheet" />
      <link href="../css/contextMenu.css" type="text/css" rel="stylesheet" />
      <link href="../css/basicStyle.css" type="text/css" rel="stylesheet" />
       <script type="text/javascript" language="javascript">
           function Expandir(ancora) {
               document.body.disabled = 'disabled';
               document.body.style.cursor = 'wait';

               if (document.all) {
                   if (ancora.innerText == 'Expandir Todos') {
                       setTimeout('ExpandirTudo()', 100)
                       ancora.innerText = 'Recolher Todos';
                   }
                   else {
                       setTimeout('RecolherTudo()', 100)
                       ancora.innerText = 'Expandir Todos';
                   }
               }
               else {
                   if (ancora.textContent == 'Expandir Todos') {
                       setTimeout('ExpandirTudo()', 100)
                       ancora.textContent = 'Recolher Todos';
                   }
                   else {
                       setTimeout('RecolherTudo()', 100)
                       ancora.textContent = 'Expandir Todos';
                   }
               }
           }

           function ExpandirTudo() {
               tvProcesso.ExpandAll();
               document.body.disabled = '';
               document.body.style.cursor = '';

           }

           function RecolherTudo() {
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
                       
            if (menuItem.Value == 'Marcar')
                contextDataNode.CheckAll();
            else if (menuItem.Value == 'Desmarcar')
                contextDataNode.UnCheckAll();
                
           contextDataNode.SaveState();    
           tvProcesso.Render();
          return true; 
        }        
        
        function NovoProcesso(treeNode)
        {
            Anthem_InvokePageMethod("NovoProcesso", 
                [treeNode == null ? null : treeNode.ID], 
                function(result){document.getElementById("txtNome").focus();});
            
        }
        
        function ExcluirProcesso(treeNode)
        {
            Anthem_InvokePageMethod("ExcluirProcesso", [treeNode.ID], function(result){});
        }
        
        function AddNode(nodeID, nodeName, parentID)
        {   
            try
            {
               
                var newNode = new ComponentArt_TreeViewNode();                
                newNode.Text = nodeName;                
                newNode.Id = nodeID;                                
                
                if(parentID == null)
                {
                    tvProcesso.AddNode(newNode);
                }
                else
                {
                    tvProcesso.FindNodeById(parentID).AddNode(newNode);
                }
                                
                tvProcesso.Render();
            }
            catch(e)
            {
                alert(e.description);
            }
        }
        
        function UpdateNode(nodeID, nodeName)
        {
             // alert(f.f);
             tvProcesso.FindNodeById(nodeID).Text = nodeName;       
             tvProcesso.FindNodeById(nodeID).SaveState(); 
             tvProcesso.Render();
        }
        
        function RemoveNode(nodeID)
        {
            tvProcesso.FindNodeById(nodeID).Remove();
            tvProcesso.Render();
        }
        
        function NodeSelected(node)
        {
            Anthem_InvokePageMethod("NodeSelected", [node.ID], function(result){});
            
        }


        var marcado = false;
        function Marcar(obj) {
            document.getElementById('imgIndicador').style.display = '';

            if (marcado == false) {
                setTimeout('MarcarTudo(false)', 1000);
            }
            else {
                setTimeout('MarcarTudo(true)', 1000);
            }
        }

        function MarcarTudo(obj) {
            var novoTexto;
            if (obj == false) {
                novoTexto = 'Desmarcar Todos';
                tvProcesso.CheckAll();
                marcado = true;
            }
            else {
                novoTexto = 'Marcar Todos'
                tvProcesso.UnCheckAll();
                marcado = false;
            }

            if (document.all)
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
    <div align="center">
    <div align="right" style="width:90%">
    <br />
        <asp:label runat="server" labelid="CadastroProcesso" CssClass="PageTitle" Text="Relatório Controle de Acesso">
            </asp:label>
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr   >
            <td valign="middle" align="left" class="TabelaCadastro" width="40%">		              
               
                <div class="PageFooter" >
                    &nbsp;
                    <img id="img1" src="../images/treeview/newnode.gif" border="0" onclick="NovoProcesso(null);"
                        title="Novo Processo na Raiz" style="cursor:hand" />
                     &nbsp;
                    <img id="img2" src="../images/treeview/expand.gif" onclick="Expandir(this);" title="Expandir Todos" 
                        style="cursor:hand" border="0" />
                    <br />
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
                 </div>
            </td>
            
            
            <td class="TabelaCadastro" valign="top">               
                <div align="left" style="vertical-align:text-bottom">
                    <asp:Label runat="server" labelid="Processo" CssClass="PageTitle" style="vertical-align:text-bottom;" Text="Filtros" />
                            
                    <hr size="1" class="divisor" style="" />
                        
                </div>
                <br />
                <table border="0" cellpadding="2" cellspacing="2" width="100%" >
                    <tr>                            
                        <td align="right" width="15%">
                            <asp:Label ID="Label2" runat="server" labelid="Tipo" Text="Tipo de Relatório" />:
                        </td>
                        <td align="left">
                            <Anthem:DropDownList runat="server" ID="ddlTipo" >
                                <asp:ListItem Value="2">Agrupado</asp:ListItem>
                                <asp:ListItem Value="1" Selected>Detalhado</asp:ListItem>
                            </Anthem:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="15%">
                            <asp:Label ID="Label1" runat="server" labelid="Nome" Text="Nome" />: </td>
                        <td align="left">
                            <Anthem:TextBox ID="txtNome" runat="server" Columns="30" />
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
                <Anthem:Button runat="server" ID="btnImprimir"  TextDuringCallBack="Aguarde" Text="Imprimir"
                        EnabledDuringCallBack="false" CssClass="Button" CausesValidation="false" labelid="Imprimir" />                                      
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
