<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_SOD_EvolucionSOD.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_SOD.Reporte_v2_SOD_EvolucionSOD" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/Pages/Modulos/Cliente/Reportes/Informes_validacion/UC_ValidarPeriodos.ascx"
    TagName="UC_ValidarPeriodos" TagPrefix="uc1" %>
<%-- Referencias de master--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<%-- end de master--%>
<%--  referecias ajax--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%-- end referecias ajax--%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte de SOD
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="ScriptReferences" ContentPlaceHolderID="ScriptIncludePlaceHolder"
    runat="server">
    <script type="text/javascript">

    </script>
</asp:Content>
<%--end  referecias ajax y script--%>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <telerik:RadSplitter ID="RadSplinter_report" runat="server" Orientation="Vertical"
        Width="100%" Height="540px" Skin="Simple">
        <telerik:RadPane ID="RadPane_report" runat="server" Width="300px" MaxWidth="300"
            Collapsed="true">
            <telerik:RadPanelBar ID="RadPanelBar_menu" runat="server" ExpandMode="FullExpandedItem"
                Height="525px" Width="300px" Skin="Outlook">
                <CollapseAnimation Type="InCubic" />
                <Items>
                    <telerik:RadPanelItem runat="server" Text="Filtros" Selected="true" Expanded="true">
                        <Items>
                            <telerik:RadPanelItem runat="server" Value="filtro" Height="500px">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="up_filtros" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        Año
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmb_año" runat="server">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Mes
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmb_mes" runat="server">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Categoria
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmb_categoria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmb_categoria_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Marca
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmb_marca" runat="server">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Región
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmb_region" runat="server">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div style="text-align: center">
                                        <asp:Button ID="btn_generar" OnClick="btn_generar_Click" runat="server" Text="Generar" />
                                    </div>
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem runat="server" Text="Menu">
                        <Items>
                            <telerik:RadPanelItem runat="server" Value="menu" Height="500px">
                                <ItemTemplate>
                                    <telerik:RadMenu ID="rad_menu" runat="server" Flow="Vertical" EnableRoundedCorners="true"
                                        EnableShadows="true" Style="padding-top: 10px; padding-bottom: 100px; z-index: inherit;"
                                        Skin="Outlook" Width="100%">
                                    </telerik:RadMenu>
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem runat="server" Text="Validación" Value="itemValidacion" Visible="false">
                        <Items>
                            <telerik:RadPanelItem runat="server" Value="validar" Height="500px">
                                <ItemTemplate>
                                    <uc1:UC_ValidarPeriodos ID="UC_ValidarPeriodos1" runat="server" />
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>
                </Items>
                <ExpandAnimation Type="InCubic" />
            </telerik:RadPanelBar>
        </telerik:RadPane>
        <telerik:RadSplitBar ID="RadSplitBar_report" runat="server" CollapseMode="Forward"
            CollapseExpandPaneText="Filtros/Menu" />
        <telerik:RadPane ID="RadPane_contenidoReportes" runat="server">
            <div>
                <asp:UpdatePanel ID="up_report" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <rsweb:ReportViewer ID="Reporte_contenido" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            ProcessingMode="Remote" Height="540px" Width="100%" ShowParameterPrompts="False"
                            ToolTip="Wholesalers Reports" ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="false"
                            ZoomMode="Percent" Visible="False">
                        </rsweb:ReportViewer>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <br />
            </div>
        </telerik:RadPane>
    </telerik:RadSplitter>
</asp:Content>
