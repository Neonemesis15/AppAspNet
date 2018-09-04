<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mod_Cliente_CanalesNw.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Mod_Cliente_CanalesNw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    

    <title>Xplora V1</title>
    <link href="../../css/Webfotter.css" rel="stylesheet" type="text/css" />
    <link href="../../css/StiloPOP.css" rel="stylesheet" type="text/css" />
     <link href="../../css/MenuBiblioteca.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="../../js/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" language="javascript" src="../../js/carousel.js"></script>

    <script type="text/javascript" language="javascript">


        /*
        * Función para inicializar el carusel
        * se parametrizan todas las opciones del carusel.
        */




        stepcarousel.setup(
            {
                galleryid: 'carousel',
                beltclass: 'belt',
                panelclass: 'panel',
                autostep: { enable: false, moveby: 1, pause: 3000 },
                panelbehavior: { speed: 500, wraparound: true, persist: true },
                statusvars: ['statusA', 'statusB', 'statusC'],
                contenttype: ['external']
            }
        );
        /*
        *   Función para realziar los llamados http a las páginas, y recibe por parametro:
        *   @param: url            String Dirección de la página que retorna el contenido a cargar.
        *   @param: objetivoHtml   String El elemento html que contendrá el resultado de la carga.
        *   @param: parametros     JSON {par1: 'a',par2: 'b'}
        *   @param: mensaje        1,0 Si es 1 entonces mostrará una animación de cargando.
        */


    </script>
    
    
</head>
<body>
<form id="form1" runat="server" >
   
      <cc1:ToolkitScriptManager ID="ToolKitScriptManager" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true">
        </cc1:ToolkitScriptManager>

         
    <div align="center">
       <asp:Label ID="SelModulo" runat="server" Text="Módulo Alternativo" ForeColor="ActiveCaption"
                        Font-Names="Verdana" Font-Size="12px"></asp:Label>
                    <asp:DropDownList ID="cmbselModulos" runat="server" Font-Names="Verdana" Font-Size="11px"
                        AutoPostBack="True">
                        <asp:ListItem>A</asp:ListItem>
                        <asp:ListItem>B</asp:ListItem>
                        <asp:ListItem>C</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="SelCliente" runat="server" Text="Cliente" ForeColor="ActiveCaption"
                        Font-Names="Verdana" Font-Size="12px"></asp:Label>
                    <asp:DropDownList ID="cmbcliente" runat="server" Font-Names="Verdana" Font-Size="11px">
                        <asp:ListItem>C</asp:ListItem>
                        <asp:ListItem>D</asp:ListItem>
                        <asp:ListItem>E</asp:ListItem>
                        <asp:ListItem>F</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="GO" runat="server" Text="Ir" Font-Size="10px" 
                         />
                
   
    
    </div>
    </form>
</body>
</html>
