<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_PresenciaColgate_Menor_ES.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informe_de_Presencia.Informe_Minoristas.Reporte_v2_PresenciaColgate_Menor_ES" %>

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
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Reporte de Presencia
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="ScriptReferences" ContentPlaceHolderID="ScriptIncludePlaceHolder"
    runat="server">
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content_body" ContentPlaceHolderID="SheetContentPlaceHolder" runat="server">
    <telerik:RadSplitter ID="RadSplinter_presencia" runat="server" Orientation="Vertical"
        Width="100%" Height="560px">
        <telerik:RadPane ID="RadPane_presencia" runat="server" Width="300px" MaxWidth="300"
            Collapsed="true">
            <telerik:RadPanelBar ID="RadPanelBar_menu" runat="server" ExpandMode="FullExpandedItem"
                Height="525px" Width="300px" Skin="Outlook">
                <CollapseAnimation Type="InCubic" />
                <Items>
                    <telerik:RadPanelItem runat="server" Text="Filtros" Selected="false" Expanded="false">
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
                                                        <telerik:RadComboBox ID="cmb_mes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Periodo
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmb_periodo" runat="server">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Cobertura
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmb_cobertura" runat="server" OnSelectedIndexChanged="cmb_cobertura_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_ciudad" runat="server" Text="Ciudad" Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmb_ciudad" runat="server" Visible="false" OnSelectedIndexChanged="cmb_ciudad_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_cadena" runat="server" Text="Mercado" Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmb_cadena" runat="server" Visible="false">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div style="text-align: center">
                                        <asp:Button ID="btn_generar" OnClick="btn_generar_Click" runat="server" Text="Generar" />
                                    </div>
                                    <asp:UpdatePanel ID="up_saveConsulta" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div align="left">
                                                <br />
                                                <br />
                                                Guardar consulta<asp:ImageButton ID="btn_img_add" runat="server" ImageUrl="~/Pages/images/save_icon.png"
                                                    Height="22px" Width="23px" />
                                                Mis consulta<asp:ImageButton ID="btn_img_ver" runat="server" ImageUrl="~/Pages/images/ViewDetails.png"
                                                    Height="22px" Width="23px" OnClick="Click_btn_img_ver" />
                                                <cc1:ModalPopupExtender ID="ModalPopupExtender_agregar" runat="server" BackgroundCssClass="modalBackground"
                                                    DropShadow="True" Enabled="True" PopupControlID="Panel_parametros" TargetControlID="btn_img_add"
                                                    CancelControlID="BtnclosePanel" DynamicServicePath="">
                                                </cc1:ModalPopupExtender>
                                                <asp:Panel ID="Panel_parametros" runat="server" BackColor="White" BorderColor="#0099CB"
                                                    BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="150px"
                                                    Width="400px" Style="display: none;">
                                                    <div>
                                                        <asp:ImageButton ID="BtnclosePanel" runat="server" BackColor="Transparent" Height="22px"
                                                            ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                                                    </div>
                                                    <div align="center">
                                                        <div style="font-family: verdana; font-size: medium; color: #D01887;">
                                                            Nueva consulta</div>
                                                        <br />
                                                        Descripción :<asp:TextBox ID="txt_descripcion_parametros" runat="server" Width="300px"></asp:TextBox>
                                                        <br></br>
                                                        <asp:Button ID="btn_guardar_parametros" runat="server" Text="Guardar" CssClass="buttonGuardar"
                                                            OnClick="buttonGuardar_Click" Height="25px" Width="164px" />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <asp:Button ID="btn_disparrar" runat="server" CssClass="alertas" Text="ocultar" Width="95px" />
                                            <cc1:ModalPopupExtender ID="mpe_ver" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="True" Enabled="True" PopupControlID="panel_consultas" CancelControlID="btn_close"
                                                DynamicServicePath="" TargetControlID="btn_disparrar">
                                            </cc1:ModalPopupExtender>
                                            <asp:Panel ID="panel_consultas" runat="server" BackColor="White" BorderColor="#0099CB"
                                                BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="400px"
                                                Width="500px" Style="display: none;">
                                                <div>
                                                    <asp:ImageButton ID="btn_close" runat="server" BackColor="Transparent" Height="22px"
                                                        ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                                                </div>
                                                <div id="div_parametro" align="left">
                                                    <div style="font-family: verdana; font-size: medium; color: #1A2B7D;" align="center">
                                                        Mis Consultas</div>
                                                    <br />
                                                    <telerik:RadGrid ID="RadGrid_parametros" runat="server" AutoGenerateColumns="False"
                                                        GridLines="None" OnItemCommand="RadGrid_parametros_ItemCommand" Skin="">
                                                        <MasterTableView NoMasterRecordsText="Sin resultados">
                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="descripcion" UniqueName="column">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_id" runat="server" Visible="False" Text='<%# Bind("id") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_id_servicio" runat="server" Visible="False" Text='<%# Bind("id_servicio") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_id_canal" runat="server" Visible="False" Text='<%# Bind("id_canal") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_id_company" runat="server" Visible="False" Text='<%# Bind("id_compañia") %>'> </asp:Label>
                                                                        <asp:Label ID="lbl_id_reporte" runat="server" Visible="False" Text='<%# Bind("id_reporte") %>'> </asp:Label>
                                                                        <asp:Label ID="lbl_id_user" runat="server" Visible="False" Text='<%# Bind("id_user") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_id_oficina" runat="server" Visible="False" Text='<%# Bind("id_oficina") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_id_año" runat="server" Visible="False" Text='<%# Bind("id_año") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_id_mes" runat="server" Visible="False" Text='<%# Bind("id_mes") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_id_periodo" runat="server" Visible="False" Text='<%# Bind("id_periodo") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_id_cadena" runat="server" Visible="False" Text='<%# Bind("Icadena") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_id_tipoCiudad" runat="server" Visible="False" Text='<%# Bind("Id_TipoCiudad") %>'></asp:Label>
                                                                        <asp:ImageButton ID="btn_img_buscar" runat="server" ImageUrl="~/Pages/img/Qrys_File.png"
                                                                            Height="28px" Width="26px" CommandName="BUSCAR" />
                                                                        <asp:ImageButton ID="btn_img_eliminar" runat="server" ImageUrl="~/Pages/images/delete.png"
                                                                            CommandName="ELIMINAR" OnClientClick="return confirm('¿Realmente desea eliminar?');" />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem runat="server" Text="Reportes">
                        <Items>
                            <telerik:RadPanelItem runat="server" Value="menu" Height="500px">
                                <ItemTemplate>
                                    <telerik:RadMenu ID="rad_menu" runat="server" Flow="Vertical" EnableRoundedCorners="true"
                                        EnableShadows="true" Style="padding-top: 10px; padding-bottom: 100px; z-index: inherit;"
                                        Skin="Outlook">
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
                    <%-- <telerik:RadPanelItem runat="server" Text="Favoritos">
                        <Items>
                            <telerik:RadPanelItem runat="server" Value="favorito" Height="500px">
                                <ItemTemplate>
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>--%>
                    <telerik:RadPanelItem runat="server" NavigateUrl="~/Pages/Modulos/Cliente/Reportes/Informe_de_Presencia/Reporte_v2_PresenciaConfiguracion.aspx"
                        Text="Configuración" Value="configuracion" Visible="false">
                    </telerik:RadPanelItem>
                </Items>
                <ExpandAnimation Type="InCubic" />
            </telerik:RadPanelBar>
        </telerik:RadPane>
        <telerik:RadSplitBar ID="RadSplitBar_presencia" runat="server" CollapseMode="Forward"
            ToolTip="Filtros/Menu" CollapseExpandPaneText="Filtros/Menu" />
        <telerik:RadPane ID="RadPane_contenidoReportes" runat="server" Scrolling="Both">
            <div>
                <rsweb:ReportViewer ID="Reporte" runat="server" Font-Names="Verdana" Font-Size="8pt"
                    ProcessingMode="Remote" Width="100%" Height="545px" ShowParameterPrompts="False"
                    ToolTip="Wholesalers Reports" ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="false"
                    ZoomMode="Percent" Visible="False">
                </rsweb:ReportViewer>
                <br />
                <br />
            </div>
        </telerik:RadPane>
    </telerik:RadSplitter>
</asp:Content>
