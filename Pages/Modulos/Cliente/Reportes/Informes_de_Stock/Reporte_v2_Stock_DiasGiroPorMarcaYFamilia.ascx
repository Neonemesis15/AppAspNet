<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_Stock_DiasGiroPorMarcaYFamilia.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_Stock.Reporte_v2_Stock_DiasGiroPorMarcaYFamilia" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<p>
    <link href="../../../../css/Cliente_V2.css" rel="stylesheet" type="text/css" />
    <link href="../MasterPageV2/style.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="center">
                <rsweb:ReportViewer ID="ReportstockdiagiroMarcaFamilia" runat="server" Font-Names="Verdana"
                    Font-Size="8pt" ProcessingMode="Remote" Height="1800px" Width="1450px" ShowParameterPrompts="False"
                    ToolTip="Comparativo" ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="True"
                    ZoomMode="PageWidth" Visible="False">
                </rsweb:ReportViewer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div_filtroGraf" runat="server" align="center">
                <asp:Button ID="btn_mostrarpopup" runat="server" CssClass="alertas" Text="ocultar"
                    Width="95px" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender_mostrarFiltros" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" Enabled="True" PopupControlID="Panel_combos" TargetControlID="btn_img_add"
                    CancelControlID="BtnclosePanel" DynamicServicePath="">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panel_combos" runat="server" BackColor="White" BorderColor="#0099CB"
                    BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="245px"
                    Width="600px" Style="display: none;">
                    <div>
                        <asp:ImageButton ID="BtnclosePanel" runat="server" BackColor="Transparent" Height="22px"
                            ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" /></div>
                    <div align="center">
                        <div style="font-family: verdana; font-size: medium; color: #D01887;">
                            Busqueda Personalizada</div>

                        <br />
                        <table style="width: 55%; height: auto;" align="center" class="art-Block-body">
                            <tr>
                                <td align="right">
                                    Año
                                </td>
                                <td align="left">
                                    <telerik:RadComboBox ID="cmb_año" runat="server" Width="238px" Skin="Vista">
                                    </telerik:RadComboBox>
                                </td>
                                <td class="style1">
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td class="style1">
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Mes
                                </td>
                                <td align="left">
                                    <telerik:RadComboBox ID="cmb_mes" runat="server" Skin="Vista" Width="238px" OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged"
                                        Text="Mes" ToolTip="Mes" AutoPostBack="True">
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right">
                                    Periodo
                                </td>
                                <td align="left">
                                    <telerik:RadComboBox ID="cmb_periodo" runat="server" Skin="Vista" Enabled="False">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Cobertura
                                </td>
                                <td align="left">
                                    <telerik:RadComboBox ID="cmb_oficina" runat="server" Width="238px" Skin="Vista">
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="cmb_punto_de_venta" runat="server" Height="19px" Width="238px"
                                        Visible="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Categoria
                                </td>
                                <td align="left">
                                    <telerik:RadComboBox ID="cmb_categoria" runat="server" Width="238px" OnSelectedIndexChanged="cmb_categoria_SelectedIndexChanged"
                                        Skin="Vista" AutoPostBack="True">
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="cmb_subCategoria" runat="server" Height="19px" Width="238px"
                                        Enabled="False" Visible="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Marca
                                </td>
                                <td align="left">
                                    <telerik:RadComboBox ID="cmb_marca" runat="server" Width="238px" Skin="Vista" Enabled="False">
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="cmb_subMarca" runat="server" Height="19px" Width="238px" OnSelectedIndexChanged="cmb_subMarca_SelectedIndexChanged"
                                        Enabled="False" Visible="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Familia
                                </td>
                                <td align="left">
                                    <telerik:RadComboBox ID="cmb_familia" runat="server" Width="238px" Skin="Vista" Enabled="False">
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right">
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="cmb_skuProducto" runat="server" Height="19px" Width="238px"
                                        Visible="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btngnerar" runat="server" Text="Generar" CssClass="buttonOcultar"
                            OnClick="btngnerar_Click" Height="25px" Width="164px" /></div>
                </asp:Panel>
                Buscar
                <asp:ImageButton ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/Qrys_File.png"
                    Height="32px" Width="33px" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</p>
