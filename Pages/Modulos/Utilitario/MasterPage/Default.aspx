<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Utilitario/MasterPage/design/MasterPage.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SIGE.Pages.Modulos.Utilitario.MasterPage.Default" %>

<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Utilitario/MasterPage/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Utilitario/MasterPage/DefaultHeader.ascx" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultSidebar1" Src="~/Pages/Modulos/Utilitario/MasterPage/DefaultSidebar1.ascx" %>
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
            <script src="js_of_carrusel_silverlight.js" type="text/javascript"></script>
            <link href="../../../css/Style_Corusel.css" rel="stylesheet" type="text/css" />
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
            </div>
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
            <p>
            </p>
            <p>
            </p>
            </p>
        </ContentTemplate>
    </artisteer:Article>
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
