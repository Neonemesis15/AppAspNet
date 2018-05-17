<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master" 
AutoEventWireup="true" CodeBehind="Reporte_v2_Sugerido_Producto_3M.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Sugerido_Producto_3M" %>

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
    Reporte de Quiebres AASS
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">

    <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>
    <%-- Start body--%>
    <asp:UpdatePanel ID="UpFiltrosEfectividad" runat="server">
        <ContentTemplate>
            <%--<asp:Panel ID="Panel_filtros" runat="server" BackColor="White" BorderColor="#E46322"
                BorderWidth="3px" Style="display: block; width: auto" BorderStyle="Solid">--%>
            <div id="Titulo_reporte_Efectividad" style="text-align: left;">
                <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
                    color: #D01887;">
                    REPORTE SUGERIDO DE PRODUCTOS
                </div>
            </div>
            <div id="oculta" style="text-align: right">
                <asp:Button ID="btn_ocultar" runat="server" OnClick="btn_ocultar_Click" Text="Filtros"
                    CssClass="buttonOcultar" Height="16px" Width="63px" />
            </div>
            <div id="Div_filtros" runat="server" align="center" style="width: auto;" visible="false">
                <asp:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                    <asp:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Personalizado</HeaderTemplate>
                        <ContentTemplate>
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
                        Perido
                        </td>
                        <td>
                        <telerik:RadComboBox ID="cmb_periodo" runat="server" Width="238px" Skin="Vista" 
                                            OnSelectedIndexChanged="cmb_periodo_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                        </td>
                        </tr>
                        <tr>
                        <td>
                        Cadenas
                        </td>
                        <td>
                        <telerik:RadComboBox ID="cmb_cadena" runat="server" Width="238px" Skin="Vista">
                                            </telerik:RadComboBox>
                        </td>
                        <td>
                        Familia
                        </td>
                        <td>
                        <telerik:RadComboBox ID="cmbFamilia" runat="server" Width="238px" Skin="Vista" 
                                                AutoPostBack="True" onselectedindexchanged="cmbFamilia_SelectedIndexChanged">
                                            </telerik:RadComboBox>
                        </td>
                        <td>
                        Categorias
                        </td>
                        <td>
                        <telerik:RadComboBox ID="cmb_categoria" runat="server" Width="238px" Skin="Vista">
                                            </telerik:RadComboBox>
                        </td>
                        </tr>
                        </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
                <asp:Button ID="btngnerar" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                    Height="16px" Width="140px" Visible="false" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />
            </div>
            <%--</asp:Panel>--%>
            <div id="div_Validar" runat="server" align="left" visible="false">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_año_value" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbl_mes_value" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbl_periodo_value" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbl_validacion" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkb_validar" runat="server" ValidationGroup="1" OnCheckedChanged="chkb_validar_CheckedChanged"
                                Text="Validar" AutoPostBack="true" />
                            <asp:CheckBox ID="chkb_invalidar" runat="server" ValidationGroup="1" Text="Invalidar"
                                OnCheckedChanged="chkb_invalidar_CheckedChanged" AutoPostBack="true" />
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:Button ID="btn_dispara_popupvalidar" runat="server" CssClass="alertas" Text="ocultar"
                        Width="95px" />
                    <asp:ModalPopupExtender ID="ModalPopupExtender_ValidationAnalyst" runat="server"
                        TargetControlID="btn_dispara_popupvalidar" PopupControlID="Panel_validAnalyst"
                        BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="Panel_validAnalyst" runat="server" BackColor="White" BorderColor="#0099CB"
                        BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="120px"
                        Width="400px" Style="display: none;">
                        <div align="center">
                            <div style="font-family: verdana; font-size: medium; color: #D01887;">
                                Mensaje de confirmación</div>
                            <br />
                            <asp:Label ID="lbl_msj_validar" runat="server"></asp:Label>
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btn_aceptar" runat="server" Text="Aceptar" CssClass="buttonNew" Height="25px"
                                Width="164px" OnClick="btn_aceptar_Click" />
                            <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="buttonNew"
                                Height="25px" Width="164px" OnClick="btn_cancelar_Click" />
                            <asp:Button ID="btn_aceptar2" runat="server" Text="Aceptar" CssClass="buttonNew"
                                Height="25px" Width="164px" Visible="false" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div align="left">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_msj_allMonth" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="div_rportes" runat="server" align="center" style="width: auto;">
                <asp:TabContainer ID="TabContainer_Reporte_Precio" runat="server" CssClass="magenta"
                    ActiveTabIndex="0" ScrollBars="Horizontal">
                   
                    <asp:TabPanel ID="TabPanel12" runat="server" HeaderText="TabPanel12">
                        <HeaderTemplate>
                            Informe Sugerido de producto
                            </HeaderTemplate>
                        <ContentTemplate>
                            <div>
                                <asp:UpdatePanel ID="upquiebre" runat="server">
                                    <ContentTemplate>
                                        <rsweb:ReportViewer ID="ReporSugProducto" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                            ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Sugerido de Producto"
                                            SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                                        </rsweb:ReportViewer>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
                <%--<asp:Button ID="Button1" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                    Height="16px" Width="140px" Visible="false" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />--%>
            </div>
            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" 
                AssociatedUpdatePanelID="UpFiltrosEfectividad"  BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div>
                         <%--   <telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />
                            <telerik:RadProgressArea Width="250" Height="10" ID="RadProgressArea1" runat="server" Culture="es-PE"  Localization-CurrentFileName="Cargando Informes"
                                DisplayCancelButton="false" ProgressIndicators="FilesCountBar,
                          FilesCountPercent" Skin="Outlook" EnableAjaxSkinRendering="False" />--%>

                          <telerik:RadProgressManager ID="RadProgressManager1" runat="server" ClientIDMode="AutoID"  />
                    <telerik:RadProgressArea ID="RadProgressArea1" runat="server" Culture="es-PE" 
                        Localization-ElapsedTime="Tiempo transacurrido: "
                        Skin="WebBlue" EnableAjaxSkinRendering="False" Language="es-PE" 
                             Localization-TransferSpeed="Velocidad: " 
                             Localization-CurrentFileName="Procesando Informes:"  Localization-Total="" 
                             Localization-TotalFiles="" Localization-Uploaded="" 
                             Localization-UploadedFiles="" Height="160px" 
                             ProgressIndicators="FilesCountBar, FilesCountPercent, CurrentFileName, TimeElapsed">
                       
                    </telerik:RadProgressArea>
                    </div>
                   <%--  <div class="modalPopup">
                            Procesando, por favor espere...
                            <img alt="Procesando" src="../../../images/progress_bar.gif" style="vertical-align: middle" />
                        </div>--%>
                </ProgressTemplate>
            </cc2:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- End Body--%>
</asp:Content>

