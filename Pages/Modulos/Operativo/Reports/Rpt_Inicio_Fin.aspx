<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Rpt_Inicio_Fin.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Rpt_Inicio_Fin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="Rpt_Segmen" Src="~/Pages/Modulos/Operativo/Reports/Rpt_Segmen.ascx" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
    <%--    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </cc1:ToolkitScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>
            <table style="width: 1111px; height: auto;" class="style1">
                <tr>
                    <td class="style11" colspan="2">
                        &nbsp; SEGUIMIENTO DE INICIO FIN DE DIA</td>
                </tr>
                <tr>
                    <td class="style8">
                        Fecha :
                    </td>
                    <td class="style5">
                        Desde:<telerik:RadDateTimePicker ID="txt_fecha_inicio" runat="server" Culture="es-PE"
                            Skin="Web20">
                            <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora">
                            </TimeView>
                            <TimePopupButton HoverImageUrl="" ImageUrl="" />
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Web20">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDateTimePicker>
                        &nbsp;Hasta :<telerik:RadDateTimePicker ID="txt_fecha_fin" runat="server" Culture="es-PE"
                            Skin="Web20">
                            <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora">
                            </TimeView>
                            <TimePopupButton HoverImageUrl="" ImageUrl="" />
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Web20">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDateTimePicker>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        Canal :
                    </td>
                    <td class="style5">
                        <asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        Campaña :
                    </td>
                    <td class="style5">
                        <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Height="25px"
                             Width="275px" Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <%--<td class="style8">
                        Promotor :
                    </td>--%>
                    <td class="style5">
                        <asp:DropDownList ID="cmbperson" runat="server" Height="25px" Width="275px" Enabled="False" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                   <%-- <td class="style8">
                        Motivos de No vista :
                    </td>--%>
                    <td class="style5">
                        <asp:DropDownList ID="cmbmotivos" runat="server" Height="25px" Width="275px" Enabled="False" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        &nbsp;
                    </td>
                    <td class="style5">
                        <div>
                            <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="boton2"
                                Height="25px" Width="164px" OnClick="btn_buscar_Click" />
                        </div>
                        <div>
                            <asp:Label ID="lblmensaje" runat="server" Style="text-align: left"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        

            <div id="dvrnv" runat="server" class="class_div" style="width: auto; height: auto;">
                <rsweb:ReportViewer ID="Rptsegnv" runat="server" Font-Names="Verdana" Font-Size="8pt"
                    ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="inicio_Fin_Dia"
                    SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                </rsweb:ReportViewer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style5
        {
            text-align: left;
            width: 533px;
        }
        .style8
        {
            text-align: right;
            width: 154px;
        }
        .style11
        {
            text-align: center;
            font-weight: bold;
            font-size: medium;
        }
        
        .class_div
        {
            overflow-x: scroll;
            background-color: white;
        }
    </style>
</asp:Content>
