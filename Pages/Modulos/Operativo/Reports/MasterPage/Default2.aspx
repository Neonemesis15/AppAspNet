<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    ValidateRequest="false" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="_Default2"
    Title="Data Mercaderistas" %>

<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="DefaultHeader.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultSidebar1" Src="DefaultSidebar2.ascx" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Data Mercaderistas
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
    <artisteer:Article ID="Article1" Caption="Bienvenidos a Data Mercaderistas" runat="server">
        <ContentTemplate>
            <img class="art-article" src="../../../../images/imgXplora (6).JPG" alt="an image"
                style="float: left; border: 0; margin: 1em 1em 0 0;" />
            <p>
                Mercaderismo: Son un conjunto de acciones en el Punto de Venta destinadas a aumentar
                la rentabilidad. Colocando el producto en el lugar, durante el tiempo, en la forma,
                al precio y en la cantidad m�s conveniente.
            </p>
            <p>
                <span class="art-button-wrapper"><span class="l"></span><span class="r"></span><a
                    class="art-button" href="javascript:void(0)">Leer...</a> </span>
            </p>
            <div class="cleared">
            </div>
            <%--<div class="art-content-layout overview-table">
                <div class="art-content-layout-row">
                    <div class="art-layout-cell">
                        <div class="overview-table-inner">
                            <h4>
                                Support</h4>
                            <img src="images/01.png" width="55px" height="55px" alt="an image" class="image" />
                            <p>
                                Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Quisque sed felis. Aliquam
                                sit amet felis. Mauris semper, velit semper laoreet dictum, quam diam dictum urna.
                            </p>
                        </div>
                    </div>
                    <!-- end cell -->
                    <div class="art-layout-cell">
                        <div class="overview-table-inner">
                            <h4>
                                Development</h4>
                            <img src="images/02.png" width="55px" height="55px" alt="an image" class="image" />
                            <p>
                                Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Quisque sed felis. Aliquam
                                sit amet felis. Mauris semper, velit semper laoreet dictum, quam diam dictum urna.
                            </p>
                        </div>
                    </div>
                    <!-- end cell -->
                    <div class="art-layout-cell">
                        <div class="overview-table-inner">
                            <h4>
                                Strategy</h4>
                            <img src="images/03.png" width="55px" height="55px" alt="an image" class="image" />
                            <p>
                                Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Quisque sed felis. Aliquam
                                sit amet felis. Mauris semper, velit semper laoreet dictum, quam diam dictum urna.
                            </p>
                        </div>
                    </div>
                    <!-- end cell -->
                </div>
                <!-- end row -->
            </div>--%>
            <!-- end table -->
        </ContentTemplate>
    </artisteer:Article>
    <%--<artisteer:Article ID="Article2" Caption="Text, &lt;a href=&quot;#&quot; rel=&quot;bookmark&quot; title=&quot;Permanent Link to this Post&quot;&gt;Link&lt;/a&gt;, &lt;a class=&quot;visited&quot; href=&quot;#&quot; rel=&quot;bookmark&quot; title=&quot;Visited Hyperlink&quot;&gt;Visited&lt;/a&gt;, &lt;a class=&quot;hovered&quot; href=&quot;#&quot; rel=&quot;bookmark&quot; title=&quot;Hovered Hyperlink&quot;&gt;Hovered&lt;/a&gt;"
        runat="server">
        <ContentTemplate>
            <p>
                Lorem <sup>superscript</sup> dolor <sub>subscript</sub> amet, consectetuer adipiscing
                elit, <a href="#" title="test link">test link</a>. Nullam dignissim convallis est.
                Quisque aliquam. <cite>cite</cite>. Nunc iaculis suscipit dui. Nam sit amet sem.
                Aliquam libero nisi, imperdiet at, tincidunt nec, gravida vehicula, nisl. Praesent
                mattis, massa quis luctus fermentum, turpis mi volutpat justo, eu volutpat enim
                diam eget metus. Maecenas ornare tortor. Donec sed tellus eget sapien fringilla
                nonummy. <acronym title="National Basketball Association">NBA</acronym> Mauris a
                ante. Suspendisse quam sem, consequat at, commodo vitae, feugiat in, nunc. Morbi
                imperdiet augue quis tellus.
                <abbr title="Avenue">
                    AVE</abbr></p>
            <h1>
                Heading 1</h1>
            <h2>
                Heading 2</h2>
            <h3>
                Heading 3</h3>
            <h4>
                Heading 4</h4>
            <h5>
                Heading 5</h5>
            <h6>
                Heading 6</h6>
            <blockquote>
                <p>
                    &#8220;This stylesheet is going to help so freaking much.&#8221;
                    <br />
                    -Blockquote
                </p>
            </blockquote>
            <br />
            <table class="art-article" border="0" cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <th>
                            Header
                        </th>
                        <th>
                            Header
                        </th>
                        <th>
                            Header
                        </th>
                    </tr>
                    <tr>
                        <td>
                            Data
                        </td>
                        <td>
                            Data
                        </td>
                        <td>
                            Data
                        </td>
                    </tr>
                    <tr class="even">
                        <td>
                            Data
                        </td>
                        <td>
                            Data
                        </td>
                        <td>
                            Data
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data
                        </td>
                        <td>
                            Data
                        </td>
                        <td>
                            Data
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                <span class="art-button-wrapper"><span class="l"></span><span class="r"></span><a
                    class="art-button" href="javascript:void(0)">Join&nbsp;Now!</a> </span>
            </p>
        </ContentTemplate>
    </artisteer:Article>--%>
</asp:Content>
