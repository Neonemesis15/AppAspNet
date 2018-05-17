<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="err_mensaje.aspx.cs" Inherits="SIGE.err_mensaje" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Combobox" Assembly="obout_Combobox_Net" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register assembly="obout_Combobox_NET" namespace="OboutInc.Combobox" tagprefix="cc2" %>

<%@ Register assembly="obout_Grid_NET" namespace="Obout.Grid" tagprefix="cc3" %>

<%@ Register assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" namespace="eWorld.UI.Compatibility" tagprefix="cc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" >
    <title></title>
    <link href="Pages/css/backstilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function pageLoad() 
        {
        }
        void btnagregar_Click()
       {
       }
    
    </script>
    
    	<style type="text/css">
			
			
			a {
				font:11px Verdana;
				color:#315686;
				text-decoration:underline;
			}
			
		    .style1
            {
                width: 437px;
                height: 135px;
            }
			
		  
			
		  
			
		    .style2
            {
                width: 509px;
            }
			
		  
			
		  
			
		    .style3
            {
                width: 68px;
            }
			
		  
			
		  
			
		</style>
</head>
<body>
    <form id="form1" runat="server" >
    <div >
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
       
        <table align="center" style="border-style:solid; border-width:2px; border-color:gray; border-right-color:Black; border-right-width:3px;" 
        class="busqueda">
            <tr>
                <td valign="top" class="style3"  >
                    <img alt="" src="Pages/Modulos/Cliente/imgs/Error.gif" 
                    style="width: 50px; height: 50px;" />
                </td>
                <td class="style1" valign="top">
                    <br />
                    <asp:Label ID="Error" runat="server" 
                        Text="Sr. Usuario se produjo un error durante la ejecución" Font-Bold="True" 
                        Font-Names="Arial" Font-Overline="False" Font-Size="14pt" 
                        ForeColor="White"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Error0" runat="server" Text="Mensaje:" Font-Bold="True" 
                        Font-Names="Arial" Font-Overline="False" Font-Size="12pt" 
                        ForeColor="White" Font-Underline="False"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblerror" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Overline="False" Font-Size="12pt" ForeColor="White"></asp:Label>    
                </td>
            </tr>
            
        </table>
        
        <table align="center" style="border-style:solid; border-width:2px; border-color:gray; border-right-color:Black; border-right-width:3px;
         border-bottom-color:Black;  border-bottom-width:3px" class="busqueda">
            <tr>
                <td align="center" class="style2">
                    <asp:Button ID="search" runat="server" onclick="Button1_Click" 
                    Text="Finalizar" CssClass="button" />
                </td>
            </tr>
        </table>
        
        
    </div>
    </form>
</body>
</html>
