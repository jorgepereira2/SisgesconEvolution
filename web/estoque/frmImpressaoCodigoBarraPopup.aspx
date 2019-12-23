<%@ Page AutoEventWireup="true" CodeFile="frmImpressaoCodigoBarraPopup.aspx.cs" EnableViewState="false"
    Inherits="frmImpressaoCodigoBarraPopup" Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">     
       
        .divs 
        { 
			BORDER-RIGHT: black 0px solid; 
			BORDER-TOP: black 0px solid; 
			BORDER-LEFT: black 0px solid; 
			BORDER-BOTTOM: black 0px solid; 
			BACKGROUND-COLOR: transparent 
		} 
					
		.break
		{ 
			page-break-after: always;
		}
    </style>
</head>
<body leftmargin="0" rightmargin="0" bottommargin="0" topmargin="0">
    <form id="form1" runat="server">
        <asp:Literal ID="litPrintArea" runat="server"></asp:Literal>
    </form>
</body>
</html>
