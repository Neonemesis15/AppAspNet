<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    CodeBehind="Reporte_v2_Stock_Ventas_Masisa.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Stock_Ventas_Masisa" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%-- Referencias de master--%>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<%-- end de master--%>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%-- Informes--%>
<%--end Informes--%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte de Stock_Ventas
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <div id="Titulo_reporte_Efectividad" style="text-align: left;">
        <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
            color: #D01887;">
            REPORTE DE VENTAS</div>
    </div>
    <asp:Accordion ID="MyAccordion" runat="server" SelectedIndex="1" HeaderCssClass="accordionHeader"
        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
        AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
        <Panes>
            <asp:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                <Header>
                    <div id="Div1" style="text-align: right">
                        <button class="buttonOcultar">
                            Ampliar Vista</button>
                    </div>
                </Header>
                <Content>
                    <div id="Div_filtros" runat="server" align="center" visible="true" style="width: 100%">
                        <asp:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                            <asp:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                                <HeaderTemplate>
                                    Personalizado</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpFiltrosVentas" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div align="center" style="width: 100%">
                                                <table align="center">
                                                    <tr>
                                                        <td>
                                                            Año
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmb_año" runat="server" Width="238px" Skin="Vista">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Mes
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmb_mes" runat="server" Width="238px" Skin="Vista" OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged"
                                                                AutoPostBack="True">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Periodo
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmb_periodo" runat="server" Width="238px" Skin="Vista">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Dia
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmb_Dia" runat="server" Width="238px" Skin="Vista">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            Pto Venta
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmb_PtoVenta" runat="server" Width="238px" Skin="Vista">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Categoria
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmb_categoria" runat="server" Width="238px" Skin="Vista">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Sub Categoria
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmb_SubCategoria" runat="server" Width="238px" Skin="Vista">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Producto
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmbProducto" runat="server" Width="238px" Skin="Vista">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3"
                                                AssociatedUpdatePanelID="UpFiltrosVentas" BackgroundCssClass="modalProgressGreyBackground">
                                                <ProgressTemplate>
                                                    <div class="modalPopup">
                                                        <div>
                                                            Cargando Informes
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
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                        <asp:Button ID="btngnerar" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                            Height="16px" Width="140px" Visible="true" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />
                    </div>
                </Content>
            </asp:AccordionPane>
        </Panes>
    </asp:Accordion>
    <%--</asp:Panel>--%>
    <div id="div_rportes" runat="server" align="center" style="width: 100%;">
        <asp:TabContainer ID="TabContainer_Reporte_Precio" runat="server" CssClass="magenta"
            ActiveTabIndex="0" ScrollBars="Horizontal">
            <asp:TabPanel ID="TabPanel12" runat="server" HeaderText="TabPanel12">
                <HeaderTemplate>
                    Informe Ventas por Mes
                </HeaderTemplate>
                <ContentTemplate>
                    <div>
                        <rsweb:ReportViewer ID="ReporPrecio" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Reporte Ventas"
                            SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                        </rsweb:ReportViewer>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel12">
                <HeaderTemplate>
                    Informe Ventas por Semana
                </HeaderTemplate>
                <ContentTemplate>
                    <div>
                        <rsweb:ReportViewer ID="ReporStock_VentasXSemana" runat="server" Font-Names="Verdana"
                            Font-Size="8pt" ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False"
                            ToolTip="Reporte Ventas" SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                        </rsweb:ReportViewer>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel12">
                <HeaderTemplate>
                    Informe Ventas por Dia
                </HeaderTemplate>
                <ContentTemplate>
                    <div>
                        <rsweb:ReportViewer ID="ReporStock_VentasXDia" runat="server" Font-Names="Verdana"
                            Font-Size="8pt" ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False"
                            ToolTip="Reporte Ventas" SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                        </rsweb:ReportViewer>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel12">
                <HeaderTemplate>
                    Informe Split de Ventas
                </HeaderTemplate>
                <ContentTemplate>
                    <div>
                        <rsweb:ReportViewer ID="ReporSplitVentas" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Reporte Ventas"
                            SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                        </rsweb:ReportViewer>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
</asp:Content>
