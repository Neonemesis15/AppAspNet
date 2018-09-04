<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mod_Cliente_CanalesV2.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Mod_Cliente_CanalesV2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/Webfotter.css" rel="stylesheet" type="text/css" />
    <link href="../../css/StiloPOP.css" rel="stylesheet" type="text/css" />

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
    <div class="Header" align="center">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Pages/ImgBooom/logotipo.png"></asp:Image>
        <br />
        <br />
        <br />
    </div>
    <div class="wrapper">
        <asp:Image ID="Imgcliente" runat="server" 
            Height="49px" 
            Width="218px" />
        
        <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolKitScriptManager" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true">
        </cc1:ToolkitScriptManager>
        <div class="HeaderRight">
            <asp:Label ID="lblUsuario" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="#114092"></asp:Label>
            <asp:Label ID="usersession" runat="server" Visible="False" Font-Names="Verdana" Font-Size="9pt"
                ForeColor="#114092"></asp:Label>
            <br />
              <asp:Label ID="lblcompany" runat="server"  Font-Names="Verdana" Font-Size="9pt"
                ForeColor="#114092"></asp:Label>
            <br />
            <asp:ImageButton ID="ImgCloseSession" runat="server" Height="16px" ImageUrl="~/Pages/images/SesionClose.png"
                Width="84px" OnClick="ImgCloseSession_Click" />
        </div>
        <div align="center" style="width: 100%; height: 100%; vertical-align: middle;">
            <asp:Panel ID="PanelClienteCanales" runat="server">
                <table align="center" style="height: 306px; position: absolute; top: 10%; width: 720px;
                    left: 10%; width: 80%; height: 80%;">
                    <tr>
                        <td align="center">
                            <div id="CarrosTabla" class="tamañocarrusel" style="margin-left: auto; margin-right: auto;
                                width: 720px; border: 0px solid #000;">
                                <div class="button-next">
                                    <a href="javascript:stepcarousel.stepBy('carousel', 1)">
                                        <img src="../../img/derecha.png" alt="Siguiente" style="height: 50px;" />
                                    </a>
                                </div>
                                <div class="button-prev">
                                    <a href="javascript:stepcarousel.stepBy('carousel', -1)">
                                        <img src="../../img/izquierda.png" alt="Atras" style="height: 50px;" />
                                    </a>
                                </div>
                                <div id="carousel" class="stepcarousel" align="center">
                                    <div class="belt" align="center">
                                        <asp:ListView runat="server" ID="ListViewCanales" OnSelectedIndexChanged="ListViewCanales_SelectedIndexChanged"
                                            Style="vertical-align: middle" DataKeyNames="codigo_canal" OnPagePropertiesChanging="ListViewCanales_PagePropertiesChanging"
                                            OnSelectedIndexChanging="ListViewCanales_SelectedIndexChanging">
                                            <LayoutTemplate>
                                                <table runat="server" id="table1" align="center">
                                                    <tr runat="server" id="itemPlaceholder">
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="Tr2" runat="server" class="panel">
                                                    <td id="Td3" runat="server" align="center">
                                                        <asp:Label ID="CodCanal" runat="server" Text='<%#Eval("codigo_canal") %>' Visible="false" />
                                                        <asp:Label ID="NameCanal" runat="server" Text='<%#Eval("nombre_canal") %>' Visible="false" />
                                                        <asp:LinkButton  runat="server" ID="SelectCategoryButton" Text="" CssClass='<%#Eval("nombre_canal") %>'
                                                            CommandName="Select" Width="184px" Height="238px" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div class="POP">
            <asp:ListView runat="server" ID="Biblioteca" OnSelectedIndexChanged="Biblioteca_SelectedIndexChanged"
                Style="vertical-align: middle" DataKeyNames="Report_Id" 
                OnPagePropertiesChanging="Biblioteca_PagePropertiesChanging" onselectedindexchanging="Biblioteca_SelectedIndexChanging"
                >
                <LayoutTemplate>
                    <table runat="server" id="table1" align="center">
                        <tr runat="server" id="itemPlaceholder">
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr id="Tr1" runat="server">
                        <td id="Td1" runat="server" align="center">
                            <asp:Label ID="CodReporte" runat="server" Text='<%#Eval("Report_Id") %>' Visible="false" />
                            <asp:Label ID="NameReporte" runat="server" Text='<%#Eval("Report_NameReport") %>' Visible="false" />
                            <asp:LinkButton runat="server" ID="SelectBUTTONPOP" Text=""  CssClass="listviewpop"
                                CommandName="Select" Width="96px" Height="116px" Font-Overline="false" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
        </form>
        <div class="push">
        </div>
    </div>
    
    <div class="footer" align="right">
        <br />
        <br />
        
        <br />
        <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Pages/ImgBooom/logo_lucky.png">
        </asp:Image>
        <div class="Franjafinal">
        </div>
    </div>
</body>
</html>
