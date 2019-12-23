<%@ Page Language="C#" AutoEventWireup="true" CodeFile="relProcessoPesquisaDetalhadoListagem.aspx.cs" Inherits="relPessoaLista" %>

<%@ Import Namespace="Marinha.Business" %>
<%@ Register TagPrefix="WebControls" Assembly="Shared.WebControls" Namespace="Shared.WebControls" %>
<%@ Register Src="~/UserControls/ucColumnManager.ascx" TagPrefix="uc" TagName="ColumnManager" %>
<%@ Register Src="~/UserControls/CabecalhoRelatorio.ascx" TagPrefix="uc" TagName="Cabecalho" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/basicStyle.css" type="text/css" rel="stylesheet"  />      
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div align="center" style="width:90%">
    <br />
    <uc:Cabecalho ID="Cabecalho1" runat="server" Titulo="Listagem Controle de Acesso" />   
    </div>

    <uc:ColumnManager runat=server ID="ucColumn" />

    <div>
    <br />
    <Anthem:Panel runat="server" ID="pnGrid" >

        <Anthem:DataList runat="server" ID="dlProcesso" style="width: 100%;"> 
            <ItemTemplate>            
                <div style="border: 1px solid #000;">
                    <div style="text-transform: uppercase; font-weight: bold; color: #FFF; background: #00538a; font-size: 15px; width: 100%; padding: 10px;">
                        <%# ((Processo)Container.DataItem).Root((Processo)Container.DataItem) %>
                    </div>
                    <Anthem:DataList runat="server" ID="dlServidor" style="width: 100%;"> 
                        <HeaderStyle BackColor="#CCCCCC"></HeaderStyle>
                        <HeaderTemplate>
                            <table style="width: 100%;" class="" cellpadding="0" cellspacing="0">
                                <tr>
                                    <th style="width: 40%; text-align: left; border-bottom: 2px solid #000;">Nome</th>
                                    <th style="width: 30%; text-align: left; border-bottom: 2px solid #000;">Login</th>
                                    <th style="width: 30%; text-align: left; border-bottom: 2px solid #000;">Email</th>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>  
                            <table style="width: 100%;" class="" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 40%; border-bottom: 1px solid #000;"><%# ((Servidor)Container.DataItem).NomeCompleto %></td>
                                    <td style="width: 30%; border-bottom: 1px solid #000;"><%# ((Servidor)Container.DataItem).Login %></td>
                                    <td style="width: 30%; border-bottom: 1px solid #000;"><%# ((Servidor)Container.DataItem).Email %></td>
                                </tr>
                            </table>                             
                        </ItemTemplate>
                    </Anthem:DataList>  
                </div>
                <br>
                <br>
            </ItemTemplate>
        </Anthem:DataList>

    </Anthem:Panel>
    </div>
    </div>    
    </form>    
</body>
</html>