<%@ Page Language="C#" AutoEventWireup="true" Debug="true" EnableSessionState="True"
    CodeBehind="ini_clientef.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.ini_clientef" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Import Namespace="System.Web.Services" %>
<%@ Import Namespace="SIGE.Facade_Procesos_Administrativos" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>:::Cliente</title>

    <script type="text/javascript" language="javascript" src="../../js/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" language="javascript" src="../../js/carousel.js"></script>

    <link href="../../css/stilo.css" rel="stylesheet" type="text/css" />
    <link href="../../css/Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
    <link href="../../css/stilo.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />

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
        *   Función para mostar los canales de cada servicio selecionado
        *   @param: objetivo String
        */

        function mostrar(objetivo) {
            htmlObjetivo = "#" + objetivo;
            var style = document.getElementById('' + objetivo + '').style.display;
            if (style == "block") {
                $(htmlObjetivo).slideToggle("slow");
            }
            else {

                var valor = $('#cmbnivel2 :selected').val();
                $('#divOculto').load('../Cliente/Informes/cargarSesion.aspx', 'servicio=' + objetivo + '&idcanal=' + 0 + '&nivel=' + valor,
                    function() {
                        //alert('Load was performed.');
                        $(htmlObjetivo).slideDown("slow");
                    }
                );
                //AjaxConsulta('../Cliente/Informes/cargarSesion.aspx', 'divOculto', 'servicio=' + objetivo+ '&idcanal=' + 0 +'&nivel='+ valor, '1');
            }
        }

        function lanzaCanal(service, canal, usuario) {
            var valor = $('#cmbnivel2 :selected').val();
            AjaxConsulta('../Cliente/Informes/cargarSesion.aspx', 'divOculto', 'servicio=' + service + '&idcanal=' + canal + '&nivel=' + valor, '1');

            $('#divOculto').load('../Cliente/Informes/cargarSesion.aspx', 'servicio=' + service + '&idcanal=' + canal + '&nivel=' + valor,
                function() {
                    //alert('Load was performed.');
                    //  AjaxConsulta('../Cliente/Contenido.aspx','divOculto', 'servicio=' + service + '&idcanal=' + canal + '&nivel=' + valor, '1');
                    htmlObjetivo = "#" + service;

                    $(htmlObjetivo).slideToggle("slow");
                    $('#icontenido').html('<span>Cargando...<br /><img src="../../img/ajax-loader.gif" alt="Cargando..." /></span>');
                    $('#icontenido').attr('src', '../Cliente/Contenido.aspx');
                }
            );
            //AjaxConsulta('../Cliente/Informes/cargarSesion.aspx', 'divOculto', 'servicio=' + service + '&idcanal=' + canal + '&nivel=' + valor, '1');

        }

        /*
        *   Función para realziar los llamados http a las páginas, y recibe por parametro:
        *   @param: url            String Dirección de la página que retorna el contenido a cargar.
        *   @param: objetivoHtml   String El elemento html que contendrá el resultado de la carga.
        *   @param: parametros     JSON {par1: 'a',par2: 'b'}
        *   @param: mensaje        1,0 Si es 1 entonces mostrará una animación de cargando.
        */
        function AjaxConsulta(url, objetivoHtml, parametros, mensaje) {
            var objHtml = "#" + objetivoHtml;
            if (mensaje != 1) {
                $(objHtml).html('<span>Cargando...<br /><img src="../../img/ajax-loader.gif" alt="Cargando..." /></span>');
            }
            $(objHtml).load(url, parametros);
        }

        function mostrarTabla() {
            var valor = $('#cmbnivel2 :selected').val();
            var visibilidad = $('#CarrosTabla').css('visibility');
            if (visibilidad == 'hidden' && valor != 0) {
                $('#CarrosTabla').css('display', 'block');
                $('#CarrosTabla').css('visibility', 'visible');
                AjaxConsulta('../Cliente/Informes/cargarSesion.aspx', 'divOculto', 'servicio=' + 0 + '&idcanal=' + 0 + '&nivel=' + valor, '1');
            }
            else {
                if (valor == 0) {
                    $('#CarrosTabla').css('visibility', 'hidden');
                    AjaxConsulta('../Cliente/Informes/cargarSesion.aspx', 'divOculto', 'nivel=' + valor, '1');
                }
            }
        }



        function Cerrar_Session() {

            $('#icontenido').attr('src', '../Cliente/Informes/Cerrar_Session.aspx');
            //           
        }


    </script>

    <style type="text/css">
        #tgraficas
        {
            width: 765px;
        }
        #menu
        {
            width: 249px;
            height: 674px;
        }
        #imenus
        {
            width: 241px;
            height: 703px;
        }
        #icontenido
        {
            height: 704px;
            width: 994px;
            margin-bottom: 36px;
        }
        #btncerrar
        {
            width: 130px;
        }
    </style>
    
</head>
<body id="body">
    <form id="form1" runat="server">
    <div id="divOculto" style="display: none">
    </div>
    <table width="90%" align="center">
        <tr>
            <td>
                <asp:Image ID="Imglogo" runat="server" Height="62px" ImageUrl="~/Pages/img/logo explora.png"
                    Width="291px" />
            </td>
            <td align="right" enableviewstate="true">
                <asp:Label ID="lblUsuario" runat="server" CssClass="labelsN"></asp:Label>
                <br />
                <img src="../../images/SesionClose.png" onclick="Cerrar_Session()" style="cursor: pointer;
                    height: 18px; width: 119px" />
                <%-- &nbsp;<asp:ImageButton ID="btncloseseccion" runat="server" BorderStyle="None" Height="16px" 
                        ImageUrl="~/Pages/images/SesionClose.png" Width="113px" 
                        onclick="btncloseseccion_Click" />--%>
            </td>
        </tr>
    </table>
    <table align="center">
        <tr align="center">
            <td>
                <asp:Label ID="LblNivelClie" runat="server" Text="Nivel" CssClass="labelsN"></asp:Label>
                <!--
                    <asp:DropDownList ID="cmbnivel" runat="server" 
                        onselectedindexchanged="cmbnivel_SelectedIndexChanged1">
                    </asp:DropDownList>
                    -->
                <select name="cmbnivel2" id="cmbnivel2" runat="server" class="select" onchange="mostrarTabla();">
                    <option value="0">-Seleccione-</option>
                    <option value="1">General</option>
                    <option value="2">Detallado</option>
                </select>
            </td>
        </tr>
    </table>
    <div id="CarrosTabla" class="tamañocarrusel" style="visibility: hidden; margin-left: auto;
        margin-right: auto; width: 1000px; border: 0px solid #000;">
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
        <div id="carousel" class="stepcarousel">
            <div class="belt">
                <asp:ListView ID="lvCarrusel" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="panel">
                            <a id="A1" href="#" onclick='<%#Eval("Codigo","mostrar({0})") %>' runat="server">
                                <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("Imagen") %>' AlternateText='<%#Eval("Nombre")%>'
                                    ToolTip='<%#Eval("Nombre")%>' Width="130px" Height="93px" />
                                <br />
                                <%#Eval("Nombre")%>
                            </a>
                            <div id='<%#Eval("Codigo") %>' style="display: none;">
                                <ul class="canal">
                                    <asp:Label ID="lbl" runat="server" Text='<%#Eval("Canales") %>'></asp:Label>
                                </ul>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
    <table align="center" style="margin-bottom: 108px">
        <tr>
            <td>
                <div id="mainContent">
                    <!-- Contenedor principal -->
                    <table align="center" width="99%">
                        <tr>
                            <td>
                                <iframe id="icontenido" runat="server" src="" scrolling="auto" frameborder="0" style="border: 1px solid #ccc;
                                    min-height: 900px; width: 100%;"></iframe>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <!-- Opción para el placeHolder -->
    <div>
    </div>
    </form>
</body>
</html>
