<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefaultMenu.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.MasterPageV2.DefaultMenu" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
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
<asp:UpdatePanel ID="UpdatePanel_Menu" runat="server">
    <ContentTemplate>
        <telerik:RadMenu ID="RadMenu_reportes" runat="server" Skin="Web20" OnItemClick="RadMenu_reportes_ItemClick"
            Style="top: 0px; left: 0px">
            <Items>
                <telerik:RadMenuItem runat="server" NavigateUrl="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/Default.aspx"
                    Text="INICIO">
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenu>
        <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3"
            AssociatedUpdatePanelID="UpdatePanel_Menu" BackgroundCssClass="modalProgressGreyBackground">
            <ProgressTemplate>
                <div class="modalPopup">
                    <div>
                        Cargando...
                    </div>
                    <br />
                    <div>
                        <asp:Image ID="img_loading" ImageUrl="~/Pages/images/loading5.gif" Style="vertical-align: middle"
                            runat="server" />
                    </div>
                </div>
            </ProgressTemplate>
        </cc2:ModalUpdateProgress>
    </ContentTemplate>
</asp:UpdatePanel>
