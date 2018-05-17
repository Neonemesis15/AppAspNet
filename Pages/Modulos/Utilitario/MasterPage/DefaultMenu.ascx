<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefaultMenu.ascx.cs" Inherits="SIGE.Pages.Modulos.Utilitario.MasterPage.DefaultMenu" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%--<ul class="art-menu">
    <li><a href="#" class=" active"><span class="l"></span><span class="r"></span><span class="t">Home</span></a></li>
    <li><a href="#"><span class="l"></span><span class="r"></span><span class="t">Menu Item</span></a>
        <ul>
		    <li><a href="#">Menu Subitem 1</a></li>
		    <li><a href="#">Menu Subitem 2</a></li>
		    <li><a href="#">Menu Subitem 3</a></li>
	    </ul>
    </li>
    <li><a href="#"><span class="l"></span><span class="r"></span><span class="t">About</span></a></li>
</ul>--%>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
           
                        <telerik:RadMenu ID="RadMenu_reportes" Runat="server" Skin="Web20" 
                            style="top: 0px; left: 0px;" >
                         <Items>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Pages/Modulos/Utilitario/RegistrarFirmas.aspx"
                                    Text="INICIO">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenu>
           
                    </ContentTemplate>
                </asp:UpdatePanel>
