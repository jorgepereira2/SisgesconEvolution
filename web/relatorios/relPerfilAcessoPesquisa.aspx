<%@ Page Language="C#" AutoEventWireup="true" CodeFile="relPerfilAcessoPesquisa.aspx.cs" Inherits="Acesso_frmProcessoCadastro" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Import Namespace="Marinha.Business" %>
<%@ Register TagPrefix="Anthem" Assembly="Anthem" Namespace="Anthem" %>
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
            <td valign="top" align="left" class="TabelaCadastro" width="40%">		              
               
                <div class="PageFooter" >
                    <div align="left" style="vertical-align:text-bottom">
                    <asp:Label ID="Label5" runat="server" labelid="Processo" CssClass="PageTitle" style="vertical-align:text-bottom;" Text="Perfis de Acesso" />
                            
                    <hr size="1" class="divisor" style="" />
                        
                </div> 
                    <br>
                    <Anthem:CheckBox runat="server" id="CheckBox1"  Text="Marcar/Desmarcar todos" AutoCallBack="true" OnCheckedChanged="chkMarcarTodos2_CheckedChanged" style="margin-bottom: 5px;" />
                    <br><br>
                    <anthem:GridView runat="server" ID="gvPesquisa" Width="100%" class="tabela5"
                        AutoGenerateColumns="false" CellPadding="3" ShowHeader="False">
                        <Columns>  
                             <asp:TemplateField ItemStyle-HorizontalAlign="center" headertext="Imprimir">
                                <ItemTemplate>
                                <label  style="text-align: left;">
                                    <%--<input type="checkbox" id="chkPerfilAcesso" value="<%# ((PerfilAcesso)Container.DataItem).ID %>" runat="server" />--%>
                                    <Anthem:CheckBox runat="server" id="chkPerfilAcesso" />  
                                    <%# ((PerfilAcesso)Container.DataItem).Nome %>                                
                                </label>
                            </ItemTemplate>
                            </asp:TemplateField>                                      
                        </Columns>
                    </anthem:GridView>                                          
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
