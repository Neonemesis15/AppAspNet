<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="SIGE.Pages.index" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="Pages/css/backstilo.css" rel="stylesheet" type="text/css" />
    <script src="Pages/js/carousel_2008.js" type="text/javascript"></script>
    <script src="Pages/js/carousel_2008_conf.js" type="text/javascript"></script>
    
    <title>:::Lucky SAC - XPlora</title>
    <script type="text/javascript">        
        function T_onclick() 
        {
            document.getElementById('T').value = "";
        }
        function IAnfitrion_onclick() 
        {
            document.getElementById('T').value = "Es una herramienta estrat�gica de posicionamiento que consiste en recurrir a la colocaci�n de anfitrionas o anfitriones en determinados eventos con el fin de que contribuyan a reforzar su marca, su funci�n es la encarnaci�n de la marca o el rostro y el f�sico de una organizaci�n.";
        }
        function IBt_onclick() 
        {
            document.getElementById('T').value = "Es toda acci�n que acerque al cliente con la marca y logre que ambos interact�en. Es una forma de comunicaci�n  no masiva orientada a target espec�ficos. Estas actividades son creadas y dise�adas por la agencia Booom BTL del grupo Lucky e implementadas por el �rea de Producci�n (Meylin Cubas).";
        }
        function ISampling_onclick() 
        {
            document.getElementById('T').value = "La mejor manera de generar prueba de producto directamente a las manos del potencial consumidor. Sembramos consumo, preferencias y recordaci�n de la marca. Es la acci�n de distribuci�n o envi� de demostraciones o pruebas de nuestro producto de forma gratuita y promocional.";
        }
        function IVisibilidad_onclick() 
        {
            document.getElementById('T').value = "Consiste en realizar la presencia de marca en el Punto de venta a trav�s de una comunicaci�n visual eficiente (afichaje, olas de visibilidad, pegado de stickers, banners, etc.)";
        }
        function IMercaderismo_onclick() 
        {
          document.getElementById('T').value = "Son un conjunto de acciones en el punto de venta destinadas a la rentabilidad colocando producto en el lugar, durante el tiempo, en forma, al precio, y en la cantidad m�s conveniente.";
        }
        function IImpulso_onclick() 
        {
          document.getElementById('T').value = "M�s del 70% de las decisiones se definen en el punto de venta. Nosotros ayudamos a tomar decisiones, se trata de vender beneficios, emociones, y sentimientos, los productos y servicios no solo son medios para alcanzarlos. Para destacarse es clave cautivar al cliente y orientarse al servicio tratando de crear empatia.";
        }
        function IDegustacion_onclick() 
        {
          document.getElementById('T').value = "No es solo probar el producto, es entender sus beneficios. Es dejar huella sensorial e indeleble en la mente del consumidor, es potenciar la venta de un producto nuevo o existente permitiendo a los clientes que lo prueben antes de comprarlo.";
        }
        function ISellSampling_onclick() 
        {
            document.getElementById('T').value = "Venta de muestras de productos a precio promocional, esto esta orientado al casa por casa, al consumidor final.";
        }
        function Otros_onclick() {
            document.getElementById('T').value = "Brindamos informaci�n de la evaluaci�n de sus ventas y otros datos necesarios para una mejor decisi�n sobre sus productos. Conocer las marcas nos hace tambi�n conocer el mercado de sus consumidores";
        }
    </script>
    
    

    <style type="text/css">      
       .Tama�oServ
       {
            width:100px;       	
       }
       	 #carousel1,
                    #carousel2{
	                    width: 90%;
	                    margin: auto;
	                    border: 1px solid #ccc;
	                    overflow: auto;
	                    height: 180px;
	                    position: relative;	                    
                    }
    </style>
    
    

</head>
    <body class="IndexF">      
        <form id="form1" runat="server" >
        <table style="width: 300px;" >
            <tr align="right" >
                <td>
                    <img alt="ImgXplora" src="Pages/images/Xplora.png"    width= "268px"/>
                </td>
            </tr>
        </table>
        <br />
                
        <br />
        <table align="center">
            <tr>
                <td style="width:120px;" align="center" >                
                    <asp:Button ID="BtnLogin" runat="server" Text="Login" CssClass="buttonIndexF" 
                        onclick="BtnLogin_Click"/>                    
                </td>
                <td style="width:120px;" align="center">
                    <asp:Button ID="BtnMisi�n" runat="server" Text="Misi�n" CssClass="buttonIndexF"/>                                    
                </td>
                <td style="width:120px;" align="center">
                    <asp:Button ID="BtnVisi�n" runat="server" Text="Visi�n" CssClass="buttonIndexF"/>                    
                </td>           
            </tr>
        </table>
        <br />
        
        <table align="center">
            <tr>
                <td>
                    <img alt="ImgActividades"src="Pages/images/ActividadesXplora.png" />                
                </td>
            </tr>            
        </table>
        <table align="center">
            <tr>
                <td>
                    <input ID="IAnfitrion" runat="server" alt="Anfitrionaje" name="IEAnfiotrion" class="Tama�oServ" 
                    onmouseout="this.src = 'Pages/img/Anfitrionaje.png'; return T_onclick() " 
                    onmouseover="this.src = 'Pages/img/Anfitrionaje_g.png'; return IAnfitrion_onclick()" 
                    src="Pages/img/Anfitrionaje.png" type="image" />
                </td>
                <td>
                    <input ID="IBt" runat="server" alt="Activaci�n BTL" name="IEBt" class="Tama�oServ" 
                    onmouseout="this.src = 'Pages/img/btl.png';return T_onclick()" 
                    onmouseover="this.src = 'Pages/img/btl_g.png'; return IBt_onclick()" 
                    src="Pages/img/btl.png" type="image" />
                </td>
                <td>
                    <input ID="ISampling" 
                    runat="server" alt="Sampling" name="IESampling" class="Tama�oServ" 
                    onmouseout="this.src = 'Pages/img/Sampling.png';return T_onclick()" 
                    onmouseover="this.src = 'Pages/img/Sampling_g.png'; return ISampling_onclick()" 
                    src="Pages/img/Sampling.png" type="image" />
                </td>
                <td>
                    <input ID="IVisib" runat="server" alt="Visibilidad" name="IVisib" class="Tama�oServ" 
                    onmouseout="this.src = 'Pages/img/Visibilidad.png';return T_onclick()" 
                    onmouseover="this.src = 'Pages/img/Visibilidad_g.png'; return IVisibilidad_onclick()" 
                    src="Pages/img/Visibilidad.png" type="image" /> 
                </td>
                <td>
                    <input ID="IMercad" runat="server" alt="Mercaderismo" name="IMercad" class="Tama�oServ" 
                    onmouseout="this.src = 'Pages/img/Mercaderismo.png';return T_onclick()" 
                    onmouseover="this.src = 'Pages/img/Mercaderismo_g.png';return IMercaderismo_onclick()" 
                    src="Pages/img/Mercaderismo.png" type="image" />
                </td>
                <td>
                    <input ID="IDegus" runat="server" alt="Degustacion" name="IDegus" class="Tama�oServ" 
                    onmouseout="this.src = 'Pages/img/Degustaci�n.png';return T_onclick()" 
                    onmouseover="this.src = 'Pages/img/Degustaci�n_g.png';return IDegustacion_onclick()" 
                    src="Pages/img/Degustaci�n.png" type="image" />
                </td>
                <td>
                    <input ID="ISellSam" runat="server" alt="Sell Sampling" name="ISellSam" class="Tama�oServ" 
                    onmouseout="this.src = 'Pages/img/Sell Sampling.png';return T_onclick()" 
                    onmouseover="this.src = 'Pages/img/Sell Sampling_g.png';return ISellSampling_onclick()" 
                    src="Pages/img/Sell Sampling.png" type="image" />
                </td>
                <td>
                    <input ID="IImpul" runat="server" alt="Impulso" name="IImpul" class="Tama�oServ" 
                    onmouseout="this.src = 'Pages/img/Impulso.png';return T_onclick()" 
                    onmouseover="this.src = 'Pages/img/Impulso_g.png';return IImpulso_onclick()" 
                    src="Pages/img/Impulso.png" type="image" />
                </td>                
            </tr>
        </table>
        <table align="center">
            <tr>
                <td>
                    <textarea cols="1" rows="1" id="T" runat="server" class="TextArea" name="txt" readonly="readonly"                                                
                        style="overflow:hidden;  font-family:Arial, Helvetica, sans-serif; font-size: 13px; font-style: normal; color:#3366FF; ">
                    </textarea>
                </td>
            </tr>
        </table>
        <div id="carousel1" >       
		<img alt="imagen 2" src="Pages/images/imgXplora%20(1).jpg" />
		<img alt="imagen 3" src="Pages/images/imgXplora%20(2).jpg" />
		<img alt="imagen 4" src="Pages/images/imgXplora%20(3).jpg" />
		<img alt="imagen 5" src="Pages/images/imgXplora%20(4).jpg" /> 
		<img alt="imagen 6" src="Pages/images/imgXplora%20(5).jpg" /> 
		<img alt="imagen 7" src="Pages/images/imgXplora%20(6).jpg" /> 
		<img alt="imagen 8" src="Pages/images/imgXplora%20(7).jpg" /> 		 
		<img alt="imagen 8" src="Pages/images/evento1lucky.jpg" />
		<img alt="imagen 10" src="Pages/images/evento2lucky.jpg" />
		<img alt="imagen 11" src="Pages/images/evento3lucky.jpg" />
		<img alt="imagen 12" src="Pages/images/evento4lucky.jpg" />
		<img alt="imagen 13" src="Pages/images/evento5lucky.jpg" /> 
		<img alt="imagen 14" src="Pages/images/evento6lucky.jpg" /> 
	</div>         
        </form>
        
        
           <%--antiguo index--%>
           <%--<style type="text/css">
      
        .Tama�oServ
        {
            height: 41px;
            width: 55px;
        }

        #T
        {
            height: 99px;
            width: 394px;
            margin-left: 0px;
        }
     
         .header
        {
           width: 1024px;
            height: 600px;
            margin:auto;
         }
 
        #SIGEV2
        {
            height: 71px;
            width: 385px;
            top: 20px;
            left: 201px;
        }
        
    </style>--%>
           <%--  <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <object id="SIGEV2" align="middle" 
            classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" 
            codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0">
            <param name="allowScriptAccess" value="sameDomain" />
            <param name="movie" value="Pages/images/SIGEV2.swf" />
            <param name="loop" value="false" />
            <param name="quality" value="high" />
            <param name="wmode" value="transparent" />
            <param name="bgcolor" value="#ffffff" />
            <embed align="middle" allowscriptaccess="sameDomain" bgcolor="#ffffff" 
                height="100" loop="false" name="Pages/images/SIGEV2.swf" 
                pluginspage="http://www.macromedia.com/go/getflashplayer" quality="high" 
                src="Pages/images/SIGEV2.swf" type="application/x-shockwave-flash" width="559" 
                wmode="transparent" />
        </object>--%>
           <%--<div>           
                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                <asp:UpdatePanel ID="btnvision" runat="server" >
                    <ContentTemplate>                        
                        <asp:UpdatePanel ID="estrategie" runat="server" >
                            <ContentTemplate>
                            <table style="height: 536px; width: 1178px;">
                                <tr>
                                    <td>
                                        <object id="Pages/images/SIGEV3" align="middle" 
                                            classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" 
                                            codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" 
                                            style="height: 469px; width: 526px">
                                            <param name="allowScriptAccess" value="sameDomain" />
                                            <param name="movie" value="Pages/images/SIGEV3.swf" />
                                            <param name="wmode" value="transparent" />
                                            <param name="quality" value="high" />
                                            <param name="bgcolor" value="#ffffff" />
                                            <embed align="middle" allowscriptaccess="sameDomain" bgcolor="#ffffff" 
                                                height="500" name="Pages/images/SIGEV3" 
                                                pluginspage="http://www.macromedia.com/go/getflashplayer" quality="high" 
                                                src="Pages/images/SIGEV3.swf" type="application/x-shockwave-flash" width="550" 
                                                wmode="transparent" />
                                        </object>
                                    </td>
                              
                                 
                                     <td align="center" >
                                         <asp:ImageButton ID="IbtnLogin" runat="server" 
                                             AlternateText="Solo usuarios registrados" BorderStyle="None" 
                                             ImageUrl="~/Pages/images/Login.gif" onclick="IbtnLogin_Click" 
                                             onmouseout="this.src = 'Pages/images/Login.gif'" 
                                             onmouseover="this.src = 'Pages/images/LoginDown.gif'" 
                                             style="margin-left: 0px" />
                                         <asp:ImageButton ID="IbtnMision" runat="server" AlternateText="Misi�n Lucky" 
                                             BorderStyle="None" ImageUrl="~/Pages/images/Mision.gif" 
                                             onmouseout="this.src = 'Pages/images/Mision.gif' " 
                                             onmouseover="this.src = 'Pages/images/MisionDown.gif'" />
                                         <asp:ImageButton ID="IbtnVis�on" runat="server" AlternateText="Visi�n Lucky" 
                                             BorderStyle="None" ImageUrl="~/Pages/images/Vision.gif" 
                                             onmouseout="this.src = 'Pages/images/Vision.gif'" 
                                             onmouseover="this.src = 'Pages/images/VisionDown.gif'" />
                                         <br />
                                         <br />
                                         <br />
                                         <br />
                                         <br />
                                         <asp:Label ID="Actividades" runat="server"  
                                             Font-Bold="True" Font-Names="Arial" Font-Size="20px" ForeColor="#003399" 
                                             Text="Actividades"></asp:Label>
                                         <br />
                                         <br />
                                         <input ID="IAnfitrion" runat="server" alt="Anfitrionaje" name="IEAnfiotrion" class="Tama�oServ" 
                                             onmouseout="this.src = 'Pages/img/Anfitrionaje.png'; return T_onclick() " 
                                             onmouseover="this.src = 'Pages/img/Anfitrionaje_g.png'; return IAnfitrion_onclick()" 
                                             src="Pages/img/Anfitrionaje.png" type="image" /> 
                                             &nbsp;<input ID="IBt" runat="server" alt="Activaci�n BTL" name="IEBt" class="Tama�oServ" 
                                             onmouseout="this.src = 'Pages/img/btl.png';return T_onclick()" 
                                             onmouseover="this.src = 'Pages/img/btl_g.png'; return IBt_onclick()" 
                                             src="Pages/img/btl.png" type="image" /> &nbsp;<input ID="ISampling" 
                                             runat="server" alt="Sampling" name="IESampling" class="Tama�oServ" 
                                             onmouseout="this.src = 'Pages/img/Sampling.png';return T_onclick()" 
                                             onmouseover="this.src = 'Pages/img/Sampling_g.png'; return ISampling_onclick()" 
                                             src="Pages/img/Sampling.png" type="image" />&nbsp;&nbsp; <input ID="IVisib" runat="server" alt="Visibilidad" name="IVisib" class="Tama�oServ" 
                                             onmouseout="this.src = 'Pages/img/Visibilidad.png';return T_onclick()" 
                                             onmouseover="this.src = 'Pages/img/Visibilidad_g.png'; return IVisibilidad_onclick()" 
                                             src="Pages/img/Visibilidad.png" type="image" /> &nbsp;<input 
                                             ID="IMercad" runat="server" alt="Mercaderismo" name="IMercad" class="Tama�oServ" 
                                             onmouseout="this.src = 'Pages/img/Mercaderismo.png';return T_onclick()" 
                                             onmouseover="this.src = 'Pages/img/Mercaderismo_g.png';return IMercaderismo_onclick()" 
                                             src="Pages/img/Mercaderismo.png" type="image" />&nbsp; <input ID="IDegus" 
                                             runat="server" alt="Degustacion" name="IDegus" class="Tama�oServ" 
                                             onmouseout="this.src = 'Pages/img/Degustaci�n.png';return T_onclick()" 
                                             onmouseover="this.src = 'Pages/img/Degustaci�n_g.png';return IDegustacion_onclick()" 
                                             src="Pages/img/Degustaci�n.png" type="image" /> &nbsp;<input ID="ISellSam" runat="server" alt="Sell Sampling" name="ISellSam" class="Tama�oServ" 
                                             onmouseout="this.src = 'Pages/img/Sell Sampling.png';return T_onclick()" 
                                             onmouseover="this.src = 'Pages/img/Sell Sampling_g.png';return ISellSampling_onclick()" 
                                             src="Pages/img/Sell Sampling.png" type="image" />&nbsp; <input ID="IImpul" 
                                             runat="server" alt="Impulso" name="IImpul" class="Tama�oServ" 
                                             onmouseout="this.src = 'Pages/img/Impulso.png';return T_onclick()" 
                                             onmouseover="this.src = 'Pages/img/Impulso_g.png';return IImpulso_onclick()" 
                                             src="Pages/img/Impulso.png" type="image" /><br />
                                         <br />
                                        
                                         <textarea id="T" runat="server" class="TextArea" name="txt" readonly="readonly" cols="1" 
                                             
                                             style="overflow:hidden; margin-top: 0px; top: 357px; left: 626px; font-family:Arial ; font-size: 13px; font-style: normal; color:White; margin-right: 0px;">
                                </textarea><br />
                                         <br />
                                             
                                             <asp:Label ID="Details" runat="server" BackColor="Transparent" 
                                             Font-Bold="True" Font-Names="Arial" Font-Size="13px" 
                                             ForeColor="Black" CssClass="mensajes" 
                                             
                                             Text="Lo invitamos a acceder al sistema de consultas en l�nea para que est� al tanto de toda la informaci�n de la evaluaci�n de ventas y otros datos para una mejor desici�n sobre sus productos"></asp:Label>                                        
                                     </td>
                                </tr>                                
                            </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:UpdatePanel>              
             </div>--%>        
    </body>
</html>
