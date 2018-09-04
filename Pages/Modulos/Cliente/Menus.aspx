<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menus.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Menus" %>
<!--<%@ PreviousPageType VirtualPath ="~/Pages/Modulos/Cliente/ini_clientef.aspx"%>-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Menu</title>
    <script type="text/javascript">
      function pageLoad() {
      }
    </script>

  <link href="../../css/stilo.css" rel="stylesheet" type="text/css" />    
    <link href="../../css/Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style4 {
            width: 226px;
        }      
        #tgraficas {
            width: 765px;
        }
        #menu {
            width: 249px;
            height: 674px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
                                <p class="tituloAzul" align="left" >
                                    Administraci&oacute;n</p>
                                <p class="altrow">
                                    &nbsp;</p>
                                <asp:Label ID="LblTip" runat="server" Text="Informes Tipo" 
                               CssClass="selectedrow"></asp:Label>
                                <asp:ImageButton ID="BtnAdd" runat="server" ImageUrl="../../../Pages/images/add.png" 
                                onclick="BtnAdd_Click" 
            AlternateText="~/Pages/images/add.png" />
                                <asp:ImageButton ID="BtnRest" runat="server" ImageUrl="~/Pages/images/delete.png" 
                                onclick="BtnRest_Click" Visible="False" />
                                <br />
                                <asp:Menu ID="MenuEmpresarial" runat="server" Orientation="Vertical" 
                                CssSelectorClass="menu" 
                                onmenuitemclick="MenuEmpresarial_MenuItemClick" Width="221px" Height="107px">
                                    
                                </asp:Menu>
                                <p class="altrow">
                                    &nbsp;</p>
                                <asp:Label ID="LblDinam" runat="server" Text="Informes Dinámicos" 
                                CssClass="selectedrow"></asp:Label>
                                <asp:ImageButton ID="BtnAddDina" runat="server" 
                                ImageUrl="~/Pages/images/add.png" onclick="BtnAddDina_Click" />
                                <asp:ImageButton ID="BtnRestDina" runat="server" ImageUrl="~/Pages/images/delete.png" 
                                Visible="False" onclick="BtnRestDina_Click" />
                                <br />
                                <asp:Menu ID="MenuDinamico" runat="server" Orientation="Vertical"                 
                                CssSelectorClass="menu" 
                                onmenuitemclick="MenuDinamico_MenuItemClick" Width="227px">
                                    
                                </asp:Menu>
                                <p class="altrow">
                                    &nbsp;</p>
                                <asp:Label ID="LblFavoritos" runat="server" Text="Mis Favoritos" 
                                CssClass="selectedrow"></asp:Label>
                                <asp:ImageButton ID="BtnAddFav" runat="server" 
                                ImageUrl="~/Pages/images/add.png" onclick="BtnAddFav_Click" />
                                <asp:ImageButton ID="BtnRestFav" runat="server" ImageUrl="~/Pages/images/delete.png" 
                                Visible="False" onclick="BtnRestFav_Click" />
                                <br />
                                <asp:Menu ID="MenuFavoritos" runat="server" Orientation="Vertical"                 
                                CssSelectorClass="menu" Visible="False" >
                                    <Items>
                                        <asp:MenuItem Text="Mi favorito 1"></asp:MenuItem>
                                        <asp:MenuItem Text="Mi favorito 2"></asp:MenuItem>
                                        <asp:MenuItem Text="Mi favorito 3"></asp:MenuItem>
                                        <asp:MenuItem Text="Mi favorito 4"></asp:MenuItem>
                                        <asp:MenuItem Text="Nuevo Favorito..."></asp:MenuItem>
                                    </Items>
                                </asp:Menu>
                                <p class="altrow">
                                    &nbsp;</p>
                                <asp:Label ID="LblSolicitudes" runat="server" Text="Solicitudes" 
                                 CssClass="selectedrow"></asp:Label>
                                <br />
                                <asp:ImageButton ID="ImageButton1" runat="server" 
                                ImageUrl="~/Pages/images/mailreminder.png" Height="58px" 
                                Width="83px"  />
                              </div>
                          
    </div>
    </form>
</body>
</html>
