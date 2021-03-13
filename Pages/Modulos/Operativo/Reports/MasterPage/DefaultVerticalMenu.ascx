<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefaultVerticalMenu.ascx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.MasterPage.DefaultVerticalMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%--<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>--%>
<div class="art-vmenublock">
    <div class="art-vmenublock-body">
        <div class="art-vmenublockheader">
            <div class="l"></div>
            <div class="r"></div>
            <div class="t">Navegación</div>
        </div>
        <div class="art-vmenublockcontent">
            <div class="art-vmenublockcontent-body">
                <!-- block-content -->
                <telerik:RadPanelBar ID="RadPanelBar_menu" runat="server" Height="380px" 
                    ExpandMode="FullExpandedItem" Skin="Vista">
                </telerik:RadPanelBar>
                <div class="cleared"></div>
            </div>
        </div>
        <div class="cleared"></div>
    </div>
</div>
