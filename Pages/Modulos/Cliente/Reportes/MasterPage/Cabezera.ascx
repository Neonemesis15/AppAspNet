<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cabezera.ascx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.MasterPage.Cabezera" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Image ID="Imgcliente" runat="server" Height="23px" Width="173px" />
<div id="Div1" class="Header" runat="server" style="height: auto;">
    <asp:Image ID="Image3" runat="server" ImageUrl="~/Pages/ImgBooom/logotipo_master.png">
    </asp:Image>
    <br />
    <br />
    <br />
    <div>
        
        <%--<telerik:RadMenu ID="RadMenu_reportes" runat="server" Skin="Office2007">
        </telerik:RadMenu>--%>
    </div>
</div>
<%--<div id="Div_menu_Horizontal" runat="server" style="height: auto;">
    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" CssSelectorClass="PrettyMenu"
        EnableViewState="false" MaximumDynamicDisplayLevels="15" StaticDisplayLevels="15"
        DynamicEnableDefaultPopOutImage="False" StaticEnableDefaultPopOutImage="False">
    </asp:Menu>
</div>--%>
