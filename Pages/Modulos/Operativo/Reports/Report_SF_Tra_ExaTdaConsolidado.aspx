<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_SF_Tra_ExaTdaConsolidado.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_SF_Tra_ExaTdaConsolidado" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--Referencias al usrcontrol--%>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Src="MasterPage/DefaultHeader.ascx" TagName="DefaultHeader" TagPrefix="uc1" %>
<%@ Register Src="MasterPage/DefaultMenu.ascx" TagName="DefaultMenu" TagPrefix="uc1" %>
<%@ Register Src="MasterPage/DefaultSidebar2.ascx" TagName="DefaultSidebar2" TagPrefix="uc1" %>
<%--end al usercontrol--%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Data Mercaderistas</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <uc1:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <uc1:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<%--<asp:Content ID="SideBar1" ContentPlaceHolderID="Sidebar1ContentPlaceHolder" runat="Server">
    <uc1:DefaultSidebar2 ID="DefaultSidebar1Content" runat="server" />
</asp:Content>--%>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>
            <div align="center">
                <table style="width: 100%; height: auto;" align="center" class="art-Block-body">
                    <tr>
                        <td class="style3">
                            REPORTE EXAMEN DE TIENDA - SAN FERNANDO - TRADICIONAL
                        </td>
                    </tr>
                </table>
                <fieldset style="width: 850px">
                    <legend>Consultar Reporte Examen de Tienda </legend>
                    <table>
                        <tr>
                            <td align="right">
                                Fecha de Inicio:
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="txt_fecha_inicio" runat="server" Culture="es-PE" Skin="Web20"
                                    Visible="true">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                        Skin="Web20">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </td>
                            <td align="right">
                                Fecha de Fin :
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="txt_fecha_fin" runat="server" Culture="es-PE" Skin="Web20"
                                    Visible="true">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                        Skin="Web20">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Canal :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Enabled="true"
                                    Height="25px" Width="275px" OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Campaña :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Enabled="False"
                                    Height="25px" Width="275px" 
                                    onselectedindexchanged="cmbplanning_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                       
                         <tr>
                            <td align="right">
                                Distribuidor :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cmbNodeComercial" runat="server" Enabled="False" Height="25px"
                                    Width="275px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Distrito :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cmbDistrito" runat="server" Enabled="False" Height="25px"
                                    Width="275px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Supervisor :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cmbSupervisor" runat="server" Enabled="False" Height="25px"
                                    Width="275px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Generador :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cmbGenerador" runat="server" Enabled="False" Height="25px"
                                    Width="275px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="btn_buscar" runat="server" CssClass="buttonRed" Height="25px" OnClick="btn_buscar_Click"
                                    Text="Buscar" Width="164px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" colspan="4">
                                <asp:Label ID="lblmensaje" runat="server" Style="text-align: left"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div id="div_rportes" runat="server" align="center">
                <rsweb:ReportViewer ID="rpt_SF_Tra_ExaTda" runat="server" Font-Names="Verdana" Font-Size="8pt"
                    ProcessingMode="Remote" Height="620px" Width="100%" ShowParameterPrompts="False"
                    ToolTip="Comparativo" SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                </rsweb:ReportViewer>
            </div>
            </div>
            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3"
                AssociatedUpdatePanelID="UpdatePanel_contenido" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">
                        <div>
                            Cargando...
                        </div>
                        <br />
                        <div>
                            <img alt="Procesando" src="../../../images/loading5.gif" style="vertical-align: middle" />
                        </div>
                    </div>
                </ProgressTemplate>
            </cc2:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txt_fecha_inicio">
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style2
        {
            text-align: right;
            width: 153px;
        }
        .style3
        {
            text-align: center;
            width: 350px;
            font-weight: bold;
        }
        .style6
        {
            text-align: left;
            width: 836px;
        }
        .style8
        {
            font-size: medium;
            vertical-align: center;
        }
        .class_div
        {
            overflow-x: scroll;
            background-color: white;
        }
    </style>
</asp:Content>
