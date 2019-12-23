<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmProcessoCadastro.aspx.cs" Inherits="Acesso_frmProcessoCadastro" %>
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
        function Expandir(img)
        {    
            document.body.disabled = 'disabled';
            document.body.style.cursor = 'wait';
            
            if(document.all)
            {
                if(img.title == 'Expandir Todos')
                {
                    setTimeout('ExpandirTudo()',100)
                    img.title = 'Recolher Todos';    
                }
                else
                {
                    setTimeout('RecolherTudo()',100)
                    img.title = 'Expandir Todos';
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
            
            if(menuItem.Value == 'Novo')
                NovoProcesso(contextDataNode);
             else if(menuItem.Value == 'Excluir')
                ExcluirProcesso(contextDataNode);
                
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div align="right" style="width:90%">
    <br />
        <asp:label runat="server" labelid="CadastroProcesso" CssClass="PageTitle" Text="Cadastro de Processos">
            </asp:label>
    
    </div>
    <table cellSpacing="4" cellPadding="3" border="0" Width="90%" >																		    
        <tr   >
            <td valign="middle" align="left" style="border:solid 1px black" width="40%">		              
               
                <div class="PageFooter" >
                    &nbsp;
                    <img id="img1" src="../images/treeview/newnode.gif" border="0" onclick="NovoProcesso(null);"
                        title="Novo Processo na Raiz" style="cursor:hand" />
                     &nbsp;
                    <img id="img2" src="../images/treeview/expand.gif" onclick="Expandir(this);" title="Expandir Todos" 
                        style="cursor:hand" border="0" />
                 </div>
                 <Anthem:Panel runat="server" ID="pnTreeView" BorderWidth="0px" >
                    <ComponentArt:TreeView id="tvProcesso" Width="100%" Height="380" 
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
                        OnContextMenu="treeContextMenu" ClientSideOnNodeSelect="NodeSelected"                         
                        BorderStyle="Solid" BorderColor="black" BorderWidth="0px" >
                            <%--<ClientEvents>
                                <NodeSelect EventHandler="NodeSelected" />
                            </ClientEvents>--%>
                        </ComponentArt:TreeView>
                    </Anthem:Panel>
                    
                    
                    
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
                    <ComponentArt:MenuItem Text="Novo Processo" id="mnuNovoProcesso" Value="Novo" runat="server" LookId="LookNovo" />
                    <ComponentArt:MenuItem ID="mnuExcluir" Text="Excluir"  Value="Excluir"  runat="server" LookId="LookExcluir" />
                </Items>
               </ComponentArt:Menu>
            </td>
            
            
            <td style="border:solid 1px black" valign="top">
                <Anthem:Panel runat="server" ID="pnCampos" BorderStyle="Solid" BorderColor="black"
                    BorderWidth="0px" Width="90%" HorizontalAlign="center" style="vertical-align:top;" >
                    <div align="left" style="vertical-align:text-bottom">
                        <asp:Label runat="server" labelid="Processo" CssClass="PageTitle" style="vertical-align:text-bottom;" Text="Processo" />
                            
                        <hr size="1" class="divisor" style="" />
                        
                    </div>
                    <br />
                    <table border="0" cellpadding="2" cellspacing="2" width="100%" >
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label3" runat="server" labelid="Pai" Text="Pai" />:
                            </td>
                            <td align="left">
                               <Anthem:Label runat="server" ID="lblProcessoPai" CssClass="Legenda" />                               
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                               <asp:Label ID="Label2" runat="server" labelid="Nome" Text="Nome" />:
                            </td>
                            <td align="left">
                               <Anthem:TextBox runat="server" ID="txtNome" 
                                    MaxLength="30" Columns="30" />
                               &nbsp;
                               <Anthem:RequiredFieldValidator runat="server" ID="valNome" ControlToValidate="txtNome"
                                     Text="*" ErrorMessage="Campo obrigatório" labelid="CampoObrigatorio" Display="dynamic"
                                     ValidationGroup="Processo" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label1" runat="server" labelid="Link" Text="Link" />:
                            </td>
                            <td align="left">
                               <Anthem:TextBox runat="server" ID="txtLink" MaxLength="100" 
                                    columns="50"/>                               
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label runat="server" labelid="Ordem" Text="Ordem" />:
                            </td>
                            <td align="left">
                               <Anthem:NumericTextBox runat="server" ID="txtOrdem" 
                                    MaxLength="30" Columns="30" />
                               &nbsp;
                               <Anthem:RequiredFieldValidator runat="server" ID="valOrdem" ControlToValidate="txtOrdem"
                                     Text="*" ErrorMessage="Campo obrigatório" labelid="CampoObrigatorio" Display="Dynamic" 
                                     ValidationGroup="Processo"/>
                                     
                                     &nbsp;
                                     <Anthem:Button runat="server" ID="btnSalvar" TextDuringCallBack="Aguarde" Text="Salvar"
                     EnabledDuringCallBack="false" CssClass="Button" ValidationGroup="Processo" labelid="Salvar" />
                            </td>
                        </tr>                       
                    </table>     
                </Anthem:Panel>
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
    </form>    
</body>
</html>
