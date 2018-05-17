<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_Pedido_SF_AAVV.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Pedido_SF" %>

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
<%-- referencias a Informes--%>
<%-- end referecias Informes--%>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte de Precios
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<%--<asp:Content ID="SideBar1" ContentPlaceHolderID="Sidebar1ContentPlaceHolder" runat="Server">
    <art:DefaultSidebar1 ID="DefaultSidebar1Content" runat="server" />
</asp:Content>--%>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <%-- <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>--%>
    <%-- Start body--%>
    <asp:UpdatePanel ID="UpFiltrosPrecios" runat="server">
        <ContentTemplate>
            <%--<asp:Panel ID="Panel_filtros" runat="server" BackColor="White" BorderColor="#E46322"
                BorderWidth="3px" Style="display: block; width: 100;" BorderStyle="Solid">--%>
            <div id="Titulo_reporte_precio" style="text-align: left;">
                <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
                    color: #D01887;">
                    REPORTE DE PEDIDOS VS INGRESOS
                </div>
            </div>
            <div id="oculta" style="text-align: right">
                <asp:Button ID="btn_ocultar" runat="server" OnClick="btn_ocultar_Click" Text="Filtros"
                    CssClass="buttonOcultar" Height="16px" Width="63px" />
            </div>
            <div id="Div_filtros" runat="server" align="center" style="width: auto;" visible="false">
                <cc1:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                    <cc1:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Personalizado
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table style="width: auto; height: auto;" align="center" class="art-Block-body">
                                <tr>
                                    <td align="right">
                                        Año
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_año" runat="server" Width="238px" Skin="Vista">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Mes
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_mes" runat="server" Width="238px" OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged"
                                            Skin="Vista" AutoPostBack="True">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Semana
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_periodo" runat="server" Width="238px" AutoPostBack="True"
                                            Skin="Vista" OnSelectedIndexChanged="cmb_periodo_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Días
                                    </td>
                                    <td align="left">
                                        <%-- <telerik:RadComboBox ID="cmb_dia" runat="server" Width="238px" Skin="Vista">
                                        </telerik:RadComboBox>--%>
                                        <telerik:RadDatePicker ID="calendar_day" runat="server" Width="238px">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Categoria
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_categoria" runat="server" Width="238px" Skin="Vista"
                                            AutoPostBack="false">
                                           
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <div>
                                            <asp:Label ID="lbl_saveconsulta" runat="server" Text="Guardar consulta"></asp:Label>
                                            <asp:ImageButton ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/qryguardar.png"
                                                Height="32px" Width="33px" OnClick="btn_img_add_Click" />
                                            <br />
                                            <asp:Label ID="lbl_updateconsulta" runat="server" Text="Actualizar consulta" Visible="False"></asp:Label><asp:ImageButton
                                                ID="btn_img_actualizar" runat="server" ImageUrl="~/Pages/images/update-icon-tm.jpg"
                                                Height="25px" Width="29px" Visible="False" />
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
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender_edit" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="True" Enabled="True" PopupControlID="Panel_edit" TargetControlID="btn_img_actualizar"
                                                CancelControlID="btn_close_planel_Edit" DynamicServicePath="">
                                            </cc1:ModalPopupExtender>
                                            <asp:Panel ID="Panel_edit" runat="server" BackColor="White" BorderColor="#0099CB"
                                                BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="150px"
                                                Width="400px" Style="display: none;">
                                                <div>
                                                    <asp:ImageButton ID="btn_close_planel_Edit" runat="server" BackColor="Transparent"
                                                        Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                                                </div>
                                                <div align="center">
                                                    <div style="font-family: verdana; font-size: medium; color: #D01887;">
                                                        Actualizar consulta</div>
                                                    <br />
                                                    Descripción :<asp:TextBox ID="txt_pop_actualiza" runat="server" Width="300px"></asp:TextBox>
                                                    <br></br>
                                                    <asp:Button ID="btn_actualizar" runat="server" Text="Actualizar" CssClass="buttonGuardar"
                                                        OnClick="btn_actualizar_Click" Height="25px" Width="164px" />
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabMisFavoritos" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Mis Favoritos
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div id="div_parametro" align="left">
                                Agregar<asp:ImageButton ID="btn_imb_tab" runat="server" Height="16px" ImageUrl="~/Pages/images/add.png"
                                    Width="16px" OnClick="btn_imb_tab_Click" />
                                <telerik:RadGrid ID="RadGrid_parametros" runat="server" AutoGenerateColumns="False"
                                    GridLines="None" OnItemCommand="RadGrid_parametros_ItemCommand" Skin="">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="column">
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
                                                    <asp:Label ID="lbl_id_pdv" runat="server" Visible="False" Text='<%# Bind("id_punto_venta") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_categoria" runat="server" Visible="False" Text='<%# Bind("id_producto_categoria") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_marca" runat="server" Visible="False" Text='<%# Bind("id_producto_marca") %>'> </asp:Label>
                                                    <asp:Label ID="lbl_id_subCategoria" runat="server" Visible="False" Text='<%# Bind("id_subCategoria") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_subMarca" runat="server" Visible="False" Text='<%# Bind("id_subMarca") %>'></asp:Label>
                                                    <asp:Label ID="lbl_skuProducto" runat="server" Visible="False" Text='<%# Bind("skuProducto") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_año" runat="server" Visible="False" Text='<%# Bind("id_año") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_mes" runat="server" Visible="False" Text='<%# Bind("id_mes") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_periodo" runat="server" Visible="False" Text='<%# Bind("id_periodo") %>'></asp:Label>
                                                    <asp:ImageButton ID="btn_img_buscar" runat="server" ImageUrl="~/Pages/img/Qrys_File.png"
                                                        Height="28px" Width="26px" CommandName="BUSCAR" />
                                                    <asp:ImageButton ID="btn_img_edit" runat="server" ImageUrl="~/Pages/images/edit_icon.gif"
                                                        CommandName="EDITAR" />
                                                    <asp:ImageButton ID="btn_img_eliminar" runat="server" ImageUrl="~/Pages/images/delete.png"
                                                        CommandName="ELIMINAR" OnClientClick="confirm('¿Esta seguro de eliminar el registro?')" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
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
                    <cc1:ModalPopupExtender ID="ModalPopupExtender_ValidationAnalyst" runat="server"
                        TargetControlID="btn_dispara_popupvalidar" PopupControlID="Panel_validAnalyst"
                        BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
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
            <div id="div_rportes" runat="server" align="center">
                <cc1:TabContainer ID="TabContainer_Reporte_Pedido_AAVV" runat="server" Style="width: auto"
                    CssClass="magenta" ScrollBars="Both">
                    <%--ActiveTabIndex="0"--%>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel2" Style="width: auto;">
                        <HeaderTemplate>
                            Por Distribuidora
                        </HeaderTemplate>
                        <ContentTemplate>
                            <%--<art:Reporte_v2_Precio_SF_AAVV_DET ID="Reporte_v2_Precio_SF_AAVV_DET" runat="server" />--%>
                            <rsweb:ReportViewer ID="rpt_pvi_dist" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Comparativo"
                                SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel9">
                        <HeaderTemplate>
                            Por Nivel
                        </HeaderTemplate>
                        <ContentTemplate>
                            <%--<art:Reporte_v2_Precio_SF_AAVV_CB ID="Reporte_v2_Precio_SF_AAVV_CB" runat="server" />--%>
                            <rsweb:ReportViewer ID="rpt_pvi_nivel" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Comparativo"
                                SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel3">
                        <HeaderTemplate>
                            Por Marca
                        </HeaderTemplate>
                        <ContentTemplate>
                            <%--<art:Reporte_v2_Precio_SF_AAVV_CB ID="Reporte_v2_Precio_SF_AAVV_CB" runat="server" />--%>
                            <rsweb:ReportViewer ID="rpt_pvi_marca" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Comparativo"
                                SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" Style="width: auto;">
                        <HeaderTemplate>
                            P. Blancos/P. Azules – Distrib.
                        </HeaderTemplate>
                        <ContentTemplate>
                            <%--<art:Reporte_v2_Precio_SF_AAVV_DET ID="Reporte_v2_Precio_SF_AAVV_DET" runat="server" />--%>
                            <rsweb:ReportViewer ID="rpt_bva_dist" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Comparativo"
                                SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel> 
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel4">
                        <HeaderTemplate>
                            P. Blancos/P. Azules – Marca
                        </HeaderTemplate>
                        <ContentTemplate>
                            <%--<art:Reporte_v2_Precio_SF_AAVV_CB ID="Reporte_v2_Precio_SF_AAVV_CB" runat="server" />--%>
                            <rsweb:ReportViewer ID="rpt_bva_marca" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Comparativo"
                                SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </div>
            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" AssociatedUpdatePanelID="UpFiltrosPrecios"
                BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div>
                        <%--   <telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />
                            <telerik:RadProgressArea Width="250" Height="10" ID="RadProgressArea1" runat="server" Culture="es-PE"  Localization-CurrentFileName="Cargando Informes"
                                DisplayCancelButton="false" ProgressIndicators="FilesCountBar,
                          FilesCountPercent" Skin="Outlook" EnableAjaxSkinRendering="False" />--%>
                        <telerik:RadProgressManager ID="RadProgressManager1" runat="server" ClientIDMode="AutoID" />
                        <telerik:RadProgressArea ID="RadProgressArea1" runat="server" Culture="es-PE" Localization-ElapsedTime="Tiempo transacurrido: "
                            Skin="WebBlue" EnableAjaxSkinRendering="False" Language="es-PE" Localization-TransferSpeed="Velocidad: "
                            Localization-CurrentFileName="Procesando Informes:" Localization-Total="" Localization-TotalFiles=""
                            Localization-Uploaded="" Localization-UploadedFiles="" Height="160px" ProgressIndicators="FilesCountBar, FilesCountPercent, CurrentFileName, TimeElapsed">
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
