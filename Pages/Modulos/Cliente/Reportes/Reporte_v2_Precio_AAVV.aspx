<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_Precio_AAVV.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Precio_AAVV" %>

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
<%--<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_SF_AAVV_CB" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio_AAVV/Reporte_v2_Precio_SF_AAVV_CB.ascx" %>--%>
<%--<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_SF_AAVV_DET" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio_AAVV/Reporte_v2_Precio_SF_AAVV_DET.ascx" %>--%>
<%--<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_InformePrecio" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio/Reporte_v2_Precio_InformePrecio.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_MargenesYBrechas" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio/Reporte_v2_Precio_MargenesYBrechas.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_VariacionQuincenal" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio/Reporte_v2_Precio_VariacionQuincenal.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_ComparativoDePrecios" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio/Reporte_v2_Precio_ComparativoDePrecios.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_ComparativoPrecioEnCiudades"
    Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio/Reporte_v2_Precio_ComparativoPrecioEnCiudades.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_Data" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio/Reporte_v2_Precio_Data.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_IndiceMayoristas" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio/Reporte_v2_Precio_IndiceMayoristas.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_PanelDeCliente" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio/Reporte_v2_Precio_PanelDeCliente.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_ResumenEjecutivo" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio/Reporte_v2_Precio_ResumenEjecutivo.ascx" %>--%>
<%-- end referecias Informes--%>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
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
    <div id="Titulo_reporte_precio" style="text-align: left;">
        <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
            color: #D01887;">
            REPORTE DE PRECIOS - AVES VIVAS
        </div>
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
                    <div id="Div_filtros" runat="server" align="center" style="width: auto;" visible="true">
                        <asp:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                            <asp:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                                <HeaderTemplate>
                                    Personalizado
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpFiltrosPrecios" runat="server" UpdateMode="Conditional">
                                        <contenttemplate>
                            <table style="width: auto; height: auto;" align="center" class="art-Block-body">
                                <tr>
                                    <td align="right">
                                        A�o
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_a�o" runat="server" Width="238px" Skin="Vista">
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
                                            Skin="Vista" onselectedindexchanged="cmb_periodo_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        D�as
                                    </td>
                                    <td align="left">
                                        <%--<telerik:RadComboBox ID="cmb_dia" runat="server" Width="238px" Skin="Vista">
                                        </telerik:RadComboBox>--%>
                                        <telerik:RadDatePicker ID="calendar_day" runat="server" Width="238px">
                                        </telerik:RadDatePicker>
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
                                                Height="32px" Width="33px" onclick="btn_img_add_Click" />
                                            <br />
                                            <asp:Label ID="lbl_updateconsulta" runat="server" Text="Actualizar consulta" Visible="False"></asp:Label><asp:ImageButton ID="btn_img_actualizar" runat="server" ImageUrl="~/Pages/images/update-icon-tm.jpg"
                                                Height="25px" Width="29px" Visible="False" />
                                            <asp:ModalPopupExtender ID="ModalPopupExtender_agregar" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="True" Enabled="True" PopupControlID="Panel_parametros" TargetControlID="btn_img_add"
                                                CancelControlID="BtnclosePanel" DynamicServicePath="">
                                            </asp:ModalPopupExtender>
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
                                                    Descripci�n :<asp:TextBox ID="txt_descripcion_parametros" runat="server" Width="300px"></asp:TextBox>
                                                    <br></br>
                                                    <asp:Button ID="btn_guardar_parametros" runat="server" Text="Guardar" CssClass="buttonGuardar"
                                                        OnClick="buttonGuardar_Click" Height="25px" Width="164px" />
                                                </div>
                                            </asp:Panel>
                                            <asp:ModalPopupExtender ID="ModalPopupExtender_edit" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="True" Enabled="True" PopupControlID="Panel_edit" TargetControlID="btn_img_actualizar"
                                                CancelControlID="btn_close_planel_Edit" DynamicServicePath="">
                                            </asp:ModalPopupExtender>
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
                                                    Descripci�n :<asp:TextBox ID="txt_pop_actualiza" runat="server" Width="300px"></asp:TextBox>
                                                    <br></br>
                                                    <asp:Button ID="btn_actualizar" runat="server" Text="Actualizar" CssClass="buttonGuardar"
                                                        OnClick="btn_actualizar_Click" Height="25px" Width="164px" />
                                                </div>
                                            </asp:Panel>
                                        </div>

                                    </td>
                                </tr>
                            </table>
                            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" AssociatedUpdatePanelID="UpFiltrosPrecios"
                BackgroundCssClass="modalProgressGreyBackground">
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
                         </contenttemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="TabMisFavoritos" runat="server" HeaderText="TabPanel1">
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
                                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripci�n" UniqueName="column">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_id" runat="server" Visible="False" Text='<%# Bind("id") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_servicio" runat="server" Visible="False" Text='<%# Bind("id_servicio") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_canal" runat="server" Visible="False" Text='<%# Bind("id_canal") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_company" runat="server" Visible="False" Text='<%# Bind("id_compa�ia") %>'> </asp:Label>
                                                            <asp:Label ID="lbl_id_reporte" runat="server" Visible="False" Text='<%# Bind("id_reporte") %>'> </asp:Label>
                                                            <asp:Label ID="lbl_id_user" runat="server" Visible="False" Text='<%# Bind("id_user") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_oficina" runat="server" Visible="False" Text='<%# Bind("id_oficina") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_pdv" runat="server" Visible="False" Text='<%# Bind("id_punto_venta") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_categoria" runat="server" Visible="False" Text='<%# Bind("id_producto_categoria") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_marca" runat="server" Visible="False" Text='<%# Bind("id_producto_marca") %>'> </asp:Label>
                                                            <asp:Label ID="lbl_id_subCategoria" runat="server" Visible="False" Text='<%# Bind("id_subCategoria") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_subMarca" runat="server" Visible="False" Text='<%# Bind("id_subMarca") %>'></asp:Label>
                                                            <asp:Label ID="lbl_skuProducto" runat="server" Visible="False" Text='<%# Bind("skuProducto") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_a�o" runat="server" Visible="False" Text='<%# Bind("id_a�o") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_mes" runat="server" Visible="False" Text='<%# Bind("id_mes") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_periodo" runat="server" Visible="False" Text='<%# Bind("id_periodo") %>'></asp:Label>
                                                            <asp:ImageButton ID="btn_img_buscar" runat="server" ImageUrl="~/Pages/img/Qrys_File.png"
                                                                Height="28px" Width="26px" CommandName="BUSCAR" />
                                                            <asp:ImageButton ID="btn_img_edit" runat="server" ImageUrl="~/Pages/images/edit_icon.gif"
                                                                CommandName="EDITAR" />
                                                            <asp:ImageButton ID="btn_img_eliminar" runat="server" ImageUrl="~/Pages/images/delete.png"
                                                                CommandName="ELIMINAR" OnClientClick="confirm('�Esta seguro de eliminar el registro?')" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                        <asp:Button ID="btngnerar" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                            Height="16px" Width="140px" Visible="true" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />
                    </div>
                </Content>
            </asp:AccordionPane>
        </Panes>
        <%--</asp:Panel>--%>
    </asp:Accordion>
    <asp:UpdatePanel ID="UpdatePanel_validacion" runat="server" UpdateMode="Conditional">
        <contenttemplate>
            <div id="div_Validar" runat="server" align="left" visible="false">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_a�o_value" runat="server" Visible="false"></asp:Label>
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
                                Mensaje de confirmaci�n</div>
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
            </contenttemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender_validacion" runat="server"
        TargetControlID="UpdatePanel_validacion">
        <Animations> 
            <OnUpdated>
                <Sequence>
                  <FadeOut Duration="0.2" Fps="30" />
                  <FadeIn Duration="0.2" Fps="30" />
                </Sequence>
          </OnUpdated>
        </Animations>
    </asp:UpdatePanelAnimationExtender>
    <div id="div_rportes" runat="server" align="center">
        <asp:TabContainer ID="TabContainer_Reporte_Precio_AAVV" runat="server" Style="width: auto"
            CssClass="magenta" ScrollBars="Both">
            <%--ActiveTabIndex="0"--%>
            <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel3" Style="width: auto;">
                <HeaderTemplate>
                    Por Zona
                </HeaderTemplate>
                <ContentTemplate>
                    <%--<art:Reporte_v2_Precio_SF_AAVV_DET ID="Reporte_v2_Precio_SF_AAVV_DET" runat="server" />--%>
                    <rsweb:ReportViewer ID="rpt_zona_dia" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Comparativo"
                        SizeToReportContent="True" ZoomMode="FullPage" Visible="true">
                    </rsweb:ReportViewer>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel2" Style="width: auto;">
                <HeaderTemplate>
                    Por Mercado
                </HeaderTemplate>
                <ContentTemplate>
                    <%--<art:Reporte_v2_Precio_SF_AAVV_DET ID="Reporte_v2_Precio_SF_AAVV_DET" runat="server" />--%>
                    <rsweb:ReportViewer ID="rpt_mercado_dia" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Comparativo"
                        SizeToReportContent="True" ZoomMode="FullPage" Visible="true">
                    </rsweb:ReportViewer>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" Style="width: auto;">
                <HeaderTemplate>
                    Precios Pavita
                </HeaderTemplate>
                <ContentTemplate>
                    <%--<art:Reporte_v2_Precio_SF_AAVV_DET ID="Reporte_v2_Precio_SF_AAVV_DET" runat="server" />--%>
                    <rsweb:ReportViewer ID="rpt_precios_pavita" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        ProcessingMode="Remote" Width="100%" Height="100%" ShowParameterPrompts="False" ToolTip="Comparativo"
                        SizeToReportContent="False" ZoomPercent="100" ZoomMode="Percent" Visible="true">
                    </rsweb:ReportViewer>
                </ContentTemplate>
            </asp:TabPanel>
            <%-- <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel9">
                        <HeaderTemplate>
                            Por Mercado/Promedio
                        </HeaderTemplate>
                        <ContentTemplate>--%>
            <%--<art:Reporte_v2_Precio_SF_AAVV_CB ID="Reporte_v2_Precio_SF_AAVV_CB" runat="server" />--%>
            <%-- <rsweb:ReportViewer ID="rptmercado"  runat="server" Font-Names="Verdana" 
            Font-Size="8pt"  ProcessingMode="Remote"  style="width:auto"
            ShowParameterPrompts="False" ToolTip="Comparativo"  
           SizeToReportContent="True" 
            ZoomMode="FullPage" Visible="true">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </asp:TabPanel>--%>
            <%--<asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" Style="width: auto;">
                        <HeaderTemplate>--%>
            <%--Por Punto de Venta
                        </HeaderTemplate>
                        <ContentTemplate>--%>
            <%--<art:Reporte_v2_Precio_SF_AAVV_DET ID="Reporte_v2_Precio_SF_AAVV_DET" runat="server" />--%>
            <%-- <rsweb:ReportViewer ID="rptpventa"  runat="server" Font-Names="Verdana" 
            Font-Size="8pt"  ProcessingMode="Remote"  style="width:auto"
            ShowParameterPrompts="False" ToolTip="Comparativo"  
           SizeToReportContent="True" 
            ZoomMode="FullPage" Visible="true">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </asp:TabPanel>--%>
        </asp:TabContainer>
    </div>
    <%-- End Body--%>
</asp:Content>
