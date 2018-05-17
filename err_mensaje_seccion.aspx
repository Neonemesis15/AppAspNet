<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="err_mensaje_seccion.aspx.cs" Inherits="SIGE.err_mensaje_seccion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="Pages/css/backstilo.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 116px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="backcolor1">
    <div>    
        <br />
    &nbsp;<table class="style1">
            <tr>
                <td class="style2" align="center">
    <img alt="" src="Pages/Modulos/Cliente/imgs/Error.gif" 
        style="width: 60px; height: 56px;" /></td>
                <td>
    <asp:Label ID="Error" runat="server" Font-Bold="True" Font-Names="Arial" 
        Font-Overline="False" Font-Size="14pt" ForeColor="White" 
        Text="Sr. Usuario " 
        CssClass="labelsTit"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td>
    <asp:Label ID="lblerrorseccion" runat="server" CssClass="labels"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
    <br />
    &nbsp;&nbsp;
    <table align="center" class="backcolor" 
        style="border-left: 2px solid gray; border-right: 3px solid Black; border-top: 2px solid gray; border-bottom: 3px solid Black; height: 9px;">
        <tr>
            <td align="center">
                <asp:Button ID="search" runat="server" CssClass="button" 
                    onclick="Button1_Click" Text="Ir a Login" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    
    </div>
    </form>
</body>
</html>
