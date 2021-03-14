<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormTest1.aspx.cs" Inherits="SIGE.Pages.Modulos.Administrativo.WebFormTest1" %>

<%@ Register src="ProductUserControls/CategoryUserControl.ascx" tagname="CategoryUserControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:CategoryUserControl ID="CategoryUserControl1" runat="server" />
    
    </div>
    </form>
</body>
</html>
