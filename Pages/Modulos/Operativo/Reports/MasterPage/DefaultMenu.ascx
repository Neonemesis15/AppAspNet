<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefaultMenu.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Operativo.Reports.MasterPage.DefaultMenu" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div >
    <telerik:RadMenu ID="RadMenuDataMercaderista" runat="server" Skin="Office2007">
        <Items>
            <telerik:RadMenuItem runat="server" NavigateUrl="~/Pages/Modulos/Operativo/Reports/MasterPage/Default2.aspx"
                Text="Inicio">
            </telerik:RadMenuItem>

            <%--<telerik:RadMenuItem runat="server" 
                NavigateUrl="~/Pages/Modulos/Planning/Menu_Planning.aspx" Text="Menu Planning">
            </telerik:RadMenuItem>--%>

        </Items>
    </telerik:RadMenu>
</div>


<%--<ul class="art-menu">
    
    <%-- <li><a href="#" class=" active"><span class="l"></span><span class="r"></span><span class="t"> 
        <asp:LinkButton ID="lnkbtnInicio" runat="server" onclick="lnkbtnInicio_Click">Inicio</asp:LinkButton></span></a></li>
    <li><a href="#"><span class="l"></span><span class="r"></span><span class="t">Menu Planning</span></a>
        <!--ul>
		    <li><a href="#">Menu Subitem 1</a></li>
		    <li><a href="#">Menu Subitem 2</a></li>
		    <li><a href="#">Menu Subitem 3</a></li>
	    </ul-->
    </li>--
    <%--<li><a href="#"><span class="l"></span><span class="r"></span><span class="t">About</span></a></li>--
</ul>--%>
