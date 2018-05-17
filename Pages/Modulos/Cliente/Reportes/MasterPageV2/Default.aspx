<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    ValidateRequest="false" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    Title="Untitled Page" %>

<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultSidebar1" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultSidebar1.ascx" %>
<%@ Register Assembly="obout_Show_Net" Namespace="OboutInc.Show" TagPrefix="obshow" %>

<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    .:::.Xplora V 2.0
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    
            <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
            
</asp:Content>
<asp:Content ID="SideBar1" ContentPlaceHolderID="Sidebar1ContentPlaceHolder" runat="Server">
    <div class="art-layout-cell art-sidebar1">
        <art:DefaultSidebar1 ID="DefaultSidebar1Content" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <artisteer:Article ID="Article1" Caption="BIENVENIDOS A XPLORA " runat="server">
        <ContentTemplate>
            
            <%--slidebox--%>
            <link rel="stylesheet" href="./jQuery/slidebox/jquery.mSimpleSlidebox.css">
	        <div id="slidebox" class="slidebox">
            <% int icompany = Convert.ToInt32(this.Session["companyid"]);
                   if (icompany == 1572)
                   {
                       Response.Write("<ul>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv2.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv3.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv4.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv5.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv6.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv7.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv8.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv9.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv10.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv11.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv12.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv13.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv14.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv15.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv16.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv17.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv18.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv19.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv20.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv21.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv22.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv23.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv24.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv25.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv26.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv27.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv28.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv29.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/SF/aavv30.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       
                       
                       Response.Write("</ul>");
                       
                       //Response.Write("<ul>");
                       //Response.Write("<li style=\"background:url(./Foto/SF/aavv1.jpg) no-repeat;> <a href='#slide_1_link' title='Slide 1 title'><span>This is the first slide</span></a> </li>");
                       //Response.Write("<img src='./Foto/SF/aavv2.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv3.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv4.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv5.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv6.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv7.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv8.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv9.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv10.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv11.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv12.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv13.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv14.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv15.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv16.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv17.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv18.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv19.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv20.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv21.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv22.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv23.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv24.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv25.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv26.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv27.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv28.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv29.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv30.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv31.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv32.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv33.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv34.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv35.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv36.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv37.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv38.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv39.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv40.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv41.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv42.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv43.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv44.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv45.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv46.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv47.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv48.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv49.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv50.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv51.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv52.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv53.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv54.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv55.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv56.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv57.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv58.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv59.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv60.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv61.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv62.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv63.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv64.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv65.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/SF/aavv66.jpg' alt='' />");
                       //Response.Write("</ul>");
                   }
                   else if (icompany == 1561)
                   {
                       Response.Write("<ul>");
                       Response.Write("<li style=\"background:url(./Foto/Colgate/baner1.png) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/Colgate/bodegasExecution.png) no-repeat;\"> <a href='#slide_2_link' title='Slide 2 title'><span>Bodegas Execution</span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/Colgate/CarritosMarketStall.png) no-repeat;\"> <a href='#slide_3_link' title='Slide 3 title'><span>Carritos Market Stall</span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/Colgate/Exhibidores.png) no-repeat;\"> <a href='#slide_4_link' title='Slide 4 title'><span></span>Exhibidores</a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/Colgate/IslasPharma.jpg) no-repeat;\"> <a href='#slide_5_link' title='Slide 5 title'><span>Islas Pharma</span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/Colgate/MarketStallExcecution.png) no-repeat;\"> <a href='#slide_6_link' title='Slide 6 title'><span>Market Stall Excecution</span></a> </li>");

                       Response.Write("<li style=\"background:url(./Foto/Colgate/MuebleCorporativo.png) no-repeat;\"> <a href='#slide_7_link' title='Slide 7 title'><span>Mueble Corporativo</span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/Colgate/Pharma1.png) no-repeat;\"> <a href='#slide_8_link' title='Slide 8 title'><span>Pharma</span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/Colgate/Pharma2.png) no-repeat;\"> <a href='#slide_9_link' title='Slide 9 title'><span>Pharma</span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/Colgate/Pharmait.jpg) no-repeat;\"> <a href='#slide_10_link' title='Slide 10 title'><span>Pharma IT</span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/Colgate/WholesalerExecution.jpg) no-repeat;\"> <a href='#slide_11_link' title='Slide 11 title'><span>Whole sales Execution</span></a> </li>");
                       Response.Write("</ul>");
                   }
                   else if (icompany == 1562)
                   {
                       Response.Write("<ul>");
                       Response.Write("<li style=\"background:url(./Foto/ALICORP/alicorp1.png) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span>Días Giro y Sell Out</span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/ALICORP/alicorp2.png) no-repeat;\"> <a href='#slide_2_link' title='Slide 2 title'><span>Fotográfico</span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/ALICORP/alicorp3.png) no-repeat;\"> <a href='#slide_3_link' title='Slide 3 title'><span>Efectividad</span></a> </li>");
                       Response.Write("</ul>");
                       
                       //Response.Write("<img src='./Foto/ALICORP/alicorp1.png' alt='Días Giro y Sell Out' />");
                       //Response.Write("<img src='./Foto/ALICORP/alicorp2.png' alt='Fotográfico' />");
                       //Response.Write("<img src='./Foto/ALICORP/alicorp3.png' alt='Efectividad' />");
                   }
                   //else if (icompany == 1609)
                   //{
                   //    Response.Write("<img src='./Foto/Masisa/masisa1.jpg' alt='' />");
                   //    Response.Write("<img src='./Foto/Masisa/masisa2.jpg' alt='' />");
                   //    Response.Write("<img src='./Foto/Masisa/masisa3.jpg' alt='' />");
                   //    Response.Write("<img src='./Foto/Masisa/masisa4.jpg' alt='' />");
                   //}
                   else
                   {
                       Response.Write("<ul>");
                       Response.Write("<li style=\"background:url(./Foto/01.jpg) no-repeat;\"> <a href='#slide_1_link' title='Slide 1 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/02.jpg) no-repeat;\"> <a href='#slide_2_link' title='Slide 2 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/03.jpg) no-repeat;\"> <a href='#slide_3_link' title='Slide 3 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/04.jpg) no-repeat;\"> <a href='#slide_4_link' title='Slide 4 title'><span></span>Exhibidores</a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/05.jpg) no-repeat;\"> <a href='#slide_5_link' title='Slide 5 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/06.jpg) no-repeat;\"> <a href='#slide_6_link' title='Slide 6 title'><span></span></a> </li>");

                       Response.Write("<li style=\"background:url(./Foto/07.jpg) no-repeat;\"> <a href='#slide_7_link' title='Slide 7 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/08.jpg) no-repeat;\"> <a href='#slide_8_link' title='Slide 8 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/09.jpg) no-repeat;\"> <a href='#slide_9_link' title='Slide 9 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/10.jpg) no-repeat;\"> <a href='#slide_10_link' title='Slide 10 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/11.jpg) no-repeat;\"> <a href='#slide_11_link' title='Slide 11 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/12.jpg) no-repeat;\"> <a href='#slide_12_link' title='Slide 12 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/13.jpg) no-repeat;\"> <a href='#slide_13_link' title='Slide 13 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/14.jpg) no-repeat;\"> <a href='#slide_14_link' title='Slide 14 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/15.jpg) no-repeat;\"> <a href='#slide_15_link' title='Slide 15 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/16.jpg) no-repeat;\"> <a href='#slide_16_link' title='Slide 16 title'><span></span></a> </li>");
                       Response.Write("<li style=\"background:url(./Foto/17.jpg) no-repeat;\"> <a href='#slide_17_link' title='Slide 17 title'><span></span></a> </li>");
                       Response.Write("</ul>");
                       
                       //Response.Write("<img src='./Foto/01.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/02.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/03.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/04.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/05.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/06.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/07.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/08.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/09.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/10.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/11.jpg' alt='' />");
                       
                       //Response.Write("<img src='./Foto/12.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/13.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/14.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/15.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/16.jpg' alt='' />");
                       //Response.Write("<img src='./Foto/17.jpg' alt='' />");
                   }                    
                %>
		        <%--<ul>
    		        <li style="background:url(./Foto/SF/aavv1.jpg) no-repeat;">
        		        <a href="#slide_1_link" title="Slide 1 title"><span>This is the first slide</span></a>
        	        </li>
        	        <li style="background:url(./Foto/SF/aavv2.jpg) no-repeat;">
        		        <a href="#slide_2_link" title="Slide 2 title"><span>Slide no 2</span></a>
        	        </li>
        	        <li style="background:url(./Foto/SF/aavv3.jpg) no-repeat;">
        		        <a href="#slide_3_link" title="Slide 3 title"><span>Another slide. This is the third slide.</span></a>
        	        </li>
        	        <li style="background:url(./Foto/SF/aavv4.jpg) no-repeat;">
        		        <a href="#slide_4_link" title="Slide 4 title"><span>This is slide no 4</span></a>
        	        </li>
			        <li style="background:url(./Foto/SF/aavv5.jpg) no-repeat;">
        		        <a href="#slide_5_link" title="Slide 5 title"><span>Final fifth slide</span></a>
        	        </li>
		        </ul>--%>
	        </div>
<%--            <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js"></script>
            <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>--%>
            <script src="./jQuery/slidebox/jquery.mSimpleSlidebox.js"></script>
            <!-- slidebox function call -->
            <script type="text/javascript">
                $(document).ready(function () {
                    $(".slidebox").mSlidebox({
                        autoPlayTime: 3000,
                        animSpeed: 500,
                        easeType: "easeInOutQuint",
                        controlsPosition: {
                            buttonsPosition: "inside",
                            thumbsPosition: "inside"
                        },
                        pauseOnHover: true,
                        numberedThumbnails: false
                    }); 
                });
//                $(document).ready(function () {
//                    $("#slidebox_1").mSlidebox({
//                        controlsPosition: {
//                            buttonsPosition: "outside",
//                            thumbsPosition: "outside"
//                        }
//                    });
//                });    
            </script>
            <%--slidebox--%>



            <%--<div id="slider1" class="slider3d">
                <% int icompany = Convert.ToInt32(this.Session["companyid"]);
                   if (icompany == 1572)
                   {
                       Response.Write("<img src='./Foto/SF/aavv1.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv2.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv3.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv4.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv5.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv6.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv7.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv8.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv9.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv10.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv11.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv12.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv13.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv14.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv15.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv16.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv17.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv18.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv19.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv20.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv21.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv22.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv23.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv24.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv25.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv26.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv27.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv28.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv29.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv30.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv31.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv32.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv33.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv34.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv35.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv36.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv37.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv38.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv39.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv40.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv41.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv42.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv43.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv44.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv45.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv46.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv47.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv48.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv49.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv50.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv51.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv52.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv53.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv54.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv55.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv56.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv57.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv58.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv59.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv60.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv61.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv62.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv63.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv64.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv65.jpg' alt='' />");
                       Response.Write("<img src='./Foto/SF/aavv66.jpg' alt='' />");
                   }
                   else if (icompany == 1561)
                   {
                       Response.Write("<img src='./Foto/Colgate/baner1.png' alt='' />");
                       Response.Write("<img src='./Foto/Colgate/bodegasExecution.png' alt='Bodegas Execution' />");
                       Response.Write("<img src='./Foto/Colgate/CarritosMarketStall.png' alt='Carritos Market Stall' />");
                       Response.Write("<img src='./Foto/Colgate/Exhibidores.png' alt='Exhibidores' />");
                       Response.Write("<img src='./Foto/Colgate/IslasPharma.jpg' alt='Islas Pharma' />");
                       Response.Write("<img src='./Foto/Colgate/MarketStallExcecution.png' alt='Market Stall Excecution' />");
                       Response.Write("<img src='./Foto/Colgate/MuebleCorporativo.png' alt='Mueble Corporativo' />");
                       Response.Write("<img src='./Foto/Colgate/Pharma1.png' alt='Pharma' />");
                       Response.Write("<img src='./Foto/Colgate/Pharma2.png' alt='Pharma' />");
                       Response.Write("<img src='./Foto/Colgate/Pharmait.jpg' alt='Pharma IT' />");
                       Response.Write("<img src='./Foto/Colgate/WholesalerExecution.jpg' alt='Whole sales Execution' />");
                       //Response.Write("<img src='./Foto/Colgate/colgate1.png' alt='1. Coverage & Visibility Initiatives' />");
                       //Response.Write("<img src='./Foto/Colgate/colgate2.png' alt='' />");
                       //Response.Write("<img src='./Foto/Colgate/colgate3.png' alt='2. Mandatory Assortment Distribución By Sku' />");
                       //Response.Write("<img src='./Foto/Colgate/colgate4.png' alt='2.1 (Toothpaste / Last 8 Weeks)' />");
                       //Response.Write("<img src='./Foto/Colgate/colgate5.png' alt='2.2 (Toothbruses & MW / Last 8 Weeks)' />");
                       //Response.Write("<img src='./Foto/Colgate/colgate6.png' alt='2.3 (Toilet Soaps & UAP / Last 8 Weeks)' />");
                       //Response.Write("<img src='./Foto/Colgate/colgate7.png' alt='2.4 (Softeners/ Last 8 Weeks)' />");
                       //Response.Write("<img src='./Foto/Colgate/colgate8.png' alt='3. Mandatory Assorment Distribution by Clusters' />");
                   }
                   else if (icompany == 1562)
                   {
                       Response.Write("<img src='./Foto/ALICORP/alicorp1.png' alt='Días Giro y Sell Out' />");
                       Response.Write("<img src='./Foto/ALICORP/alicorp2.png' alt='Fotográfico' />");
                       Response.Write("<img src='./Foto/ALICORP/alicorp3.png' alt='Efectividad' />");
                   }
                   else if (icompany == 1609)
                   {
                       Response.Write("<img src='./Foto/Masisa/masisa1.jpg' alt='' />");
                       Response.Write("<img src='./Foto/Masisa/masisa2.jpg' alt='' />");
                       Response.Write("<img src='./Foto/Masisa/masisa3.jpg' alt='' />");
                       Response.Write("<img src='./Foto/Masisa/masisa4.jpg' alt='' />");
                   }
                   else
                   {
                       Response.Write("<img src='./Foto/01.jpg' alt='' />");
                       Response.Write("<img src='./Foto/02.jpg' alt='' />");
                       Response.Write("<img src='./Foto/03.jpg' alt='' />");
                       Response.Write("<img src='./Foto/04.jpg' alt='' />");
                       Response.Write("<img src='./Foto/05.jpg' alt='' />");
                       Response.Write("<img src='./Foto/06.jpg' alt='' />");
                       Response.Write("<img src='./Foto/07.jpg' alt='' />");
                       Response.Write("<img src='./Foto/08.jpg' alt='' />");
                       Response.Write("<img src='./Foto/09.jpg' alt='' />");
                       Response.Write("<img src='./Foto/10.jpg' alt='' />");
                       Response.Write("<img src='./Foto/11.jpg' alt='' />");
                       Response.Write("<img src='./Foto/12.jpg' alt='' />");
                       Response.Write("<img src='./Foto/13.jpg' alt='' />");
                       Response.Write("<img src='./Foto/14.jpg' alt='' />");
                       Response.Write("<img src='./Foto/15.jpg' alt='' />");
                       Response.Write("<img src='./Foto/16.jpg' alt='' />");
                       Response.Write("<img src='./Foto/17.jpg' alt='' />");
                   }                    
                %>
            </div>


            <link rel="stylesheet" href="./jQuery/style/Xplora.css" />
            <br />            
            <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js"></script>            
            <script src="./jQuery/js/jquery.easing.1.3.min.js"></script>
            <script src="./jQuery/js/jquery.ccslider.pack.js"></script>
            <script src="./jQuery/js/demo.js"></script>
--%>        
        </ContentTemplate>
    </artisteer:Article>
    <%--Reemplazando control Silverligth por JQuery
            Joseph Gonzales
            21-10-2011--%>
    <%--<script src="js_of_carrusel_silverlight.js" type="text/javascript"></script>
            <link href="../../../../css/Style_Corusel.css" rel="stylesheet" type="text/css" />
            <div id="silverlightControlHost" style="height: 400px; width: 875px;" align="center">
                <object data="data:application/x-silverlight-2," type="application/x-silverlight-2"
                    width="100%" height="100%">
                    <param name="source" value="ControlTest.xap" />
                    <param name="onError" value="onSilverlightError" />
                    <param name="background" value="white" />
                    <param name="minRuntimeVersion" value="3.0.40818.0" />
                    <param name="autoUpgrade" value="true" />
                    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none">
                        <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                            style="border-style: none" />
                    </a>
                </object>
                <iframe id="_sl_historyFrame" style="visibility: hidden; height: 0px; width: 0px;
                    border: 0px"></iframe>
            </div>--%>
    <%--<img class="art-article" src="images/VS.png" alt="an image" style="float:left;border:0;margin: 1em 1em 0 0;" />--%>
    <%--<div class="art-article" style="float: left; border: 0; margin: 1em 1em 0 0;">
                <script src="../Galeria_fotografica/Silverlight.js" type="text/javascript"></script>
                <script src="../Galeria_fotografica/SlideShow.js" type="text/javascript"></script>
                <script type="text/javascript">
                    new SlideShow.Control(new SlideShow.XmlConfigProvider());
                </script>
            </div>--%>
    <%--<p>
                En construcción, <a href="javascript:void(0)" title="link">en construcción</a>,
                <a class="visited" href="javascript:void(0)" title="visited link">visitenos</a>,
                <a class="hover" href="javascript:void(0)" title="hovered link">en construcción</a>,
                en construcción , en construcción, en construcción, en construcción, en construcción,
                en construcción, en construcción , en construcción, en construcción, en construcción,
                en construcción, en construcción, en construcción , en construcción, en construcción,
                en construcción, en construcción, en construcción, en construcción , en construcción,
                en construcción, en construcción, en construcción.</p>--%>
    <%--<p>
                <span class="art-button-wrapper"><span class="l"></span><span class="r"></span><a
                    class="art-button" href="javascript:void(0)">Leer más...</a> </span>
            </p>--%>
    <%--           <div class="cleared">
            </div>
            <div class="art-content-layout overview-table">
                <div class="art-content-layout-row">
                    <div class="art-layout-cell">
                        <div class="overview-table-inner">
                            <h4>
                                Suporte</h4>
                            <img src="images/01.png" width="55px" height="55px" alt="an image" class="image" />
                            <p>
                                En construción, en construcción, en construcción, en construcción, en construcción
                                , en construcción, en construcción, en construcción, en construcción, en construcción
                                , en construcción, en construcción, en construcción, en construcción, en construcción
                                , en construcción, en construcción, en construcción, en construcción.
                            </p>
                        </div>
                    </div>
                    <!-- end cell -->
                    <div class="art-layout-cell">
                        <div class="overview-table-inner">
                            <h4>
                                En construcción</h4>
                            <img src="images/02.png" width="55px" height="55px" alt="an image" class="image" />
                            <p>
                                en construcción , en construcción, en construcción, en construcción en construcción
                                , en construcción, en construcción, en construcción en construcción , en construcción,
                                en construcción, en construcción en construcción , en construcción, en construcción,
                                en construcción en construcción , en construcción, en construcción, en construcción
                                .
                            </p>
                        </div>
                    </div>
                    <!-- end cell -->
                    <div class="art-layout-cell">
                        <div class="overview-table-inner">
                            <h4>
                                Servicios</h4>
                            <img src="images/03.png" width="55px" height="55px" alt="an image" class="image" />
                            <p>
                                En construcción, en construcción, en construcción, en construcción, en construcción
                                , en construcción, en construcción, en construcción, en construcción , en construcción,
                                en construcción, en construcción, en construcción , en construcción, en construcción,
                                en construcción, en construcción , en construcción, en construcción, en construcción.
                            </p>
                        </div>
                    </div>
                    <!-- end cell -->
                </div>
                <!-- end row -->
            </div>--%>
    <!-- end table -->
    <%--<artisteer:Article ID="Article2" Caption="En construcción,, &lt;a href=&quot;#&quot; rel=&quot;bookmark&quot; title=&quot;Permanent En construcción to this Post&quot;&gt;En construcción,&lt;/a&gt;, &lt;a class=&quot;En construcción&quot; href=&quot;#&quot; rel=&quot;bookmark&quot; title=&quot;En construcción Hyperlink&quot;&gt;En construcción,&lt;/a&gt;, &lt;a class=&quot;En construcción&quot; href=&quot;#&quot; rel=&quot;bookmark&quot; title=&quot;En construcción Hyperlink&quot;&gt;Hovered&lt;/a&gt;"
        runat="server">
        <ContentTemplate>
            <p>
                En construcción <sup>En construcción</sup> En construcción <sub>En construcción</sub>
                En construcción <a href="#" title="test link">tEn construcción</a>. En construcción,En
                construcción, <cite>cite</cite>. En construcción,En construcción,En construcción,En
                construcción, En construcción,En construcción,En construcción,En construcción,En
                construcción, En construcción,En construcción,En construcción,En construcción,En
                construcción,En construcción,En construcción, En construcción,En construcción,En
                construcción,En construcción,En construcción,En construcción,. <acronym title="Lucky Sac">
                    Lucky SAC</acronym>En construcción,En construcción,En construcción,En construcción,
                En construcción,En construcción,En construcción,En construcción,.
                <abbr title="Avenue">
                    En construcción,</abbr></p>
            <h1>
                En construcción</h1>
            <h2>
                En construcción</h2>
            <h3>
                En construcción</h3>
            <h4>
                En construcción</h4>
            <h5>
                En construcción</h5>
            <h6>
                En construcción</h6>
            <blockquote>
                <p>
                    “En construcción,.”
                    <br />
                    -En construcción,
                </p>
            </blockquote>
            <br />
             <table class="art-article" border="0" cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <th>
                            En construcción,
                        </th>
                        <th>
                            En construcción,
                        </th>
                        <th>
                            En construcción,
                        </th>
                    </tr>
                    <tr>
                        <td>
                            En construcción,
                        </td>
                        <td>
                            En construcción,
                        </td>
                        <td>
                            En construcción,
                        </td>
                    </tr>
                    <tr class="even">
                        <td>
                            En construcción,
                        </td>
                        <td>
                            En construcción,
                        </td>
                        <td>
                            En construcción,
                        </td>
                    </tr>
                    <tr>
                        <td>
                            En construcción,
                        </td>
                        <td>
                            En construcción,
                        </td>
                        <td>
                            En construcción,
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                <span class="art-button-wrapper"><span class="l"></span><span class="r"></span><a
                    class="art-button" href="javascript:void(0)">Lucky&nbsp;SAC!</a> </span>
            </p>
        </ContentTemplate>
    </artisteer:Article>--%>
</asp:Content>
