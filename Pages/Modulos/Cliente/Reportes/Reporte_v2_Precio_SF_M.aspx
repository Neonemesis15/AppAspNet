<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_Precio_SF_M.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Precio_SF_M" %>

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

            <%--<asp:Panel ID="Panel_filtros" runat="server" BackColor="White" BorderColor="#E46322"
                BorderWidth="3px" Style="display: block; width: 100;" BorderStyle="Solid">--%>
            <div id="Titulo_reporte_precio" style="text-align: left;">
                <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
                    color: #D01887;">
                    REPORTE DE PRECIO - MODERNO
                </div>
            </div>
            <div id="oculta" style="text-align: right">
                <asp:Button ID="btn_ocultar" runat="server" OnClick="btn_ocultar_Click" Text="Filtros"
                    CssClass="buttonOcultar" Height="16px" Width="63px" Visible="false" />
            </div>
             <cc1:Accordion ID="MyAccordion" runat="server" SelectedIndex="1" HeaderCssClass="accordionHeader"
        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
        AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
        RequireOpenedPane="false" SuppressHeaderPostbacks="true" >
        <Panes>
        <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
        <Header>
                    <div id="Div1" style="text-align: right">
                        <button class="buttonOcultar">
                            Ampliar vista</button>
                    </div>
                </Header>
                <Content>
                 
                

        
            <div id="Div_filtros" runat="server" align="center" style="width: auto;" visible="true">
                <cc1:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                    <cc1:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Personalizado
                        </HeaderTemplate>
                        <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel_filtros" runat="server" UpdateMode="Conditional">
                             <ContentTemplate>
                            <div style="height: 250px; width: 800px; margin: auto">
                                
                                <div style="float: left">
                                    <p>
                                        <span class="etiqueta">Ciudad:</span>
                                        <telerik:RadComboBox ID="cmb_oficina" runat="server" Width="238px" Skin="Vista" AutoPostBack="True"
                                            OnSelectedIndexChanged="cmb_oficina_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </p>
                                    <p>
                                        <span class="etiqueta">Corporación:</span>
                                        <telerik:RadComboBox ID="cmb_corporacion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_corporacion_SelectedIndexChanged"
                                            Skin="Vista" Width="238px">
                                        </telerik:RadComboBox>
                                    </p>
                                    <p>
                                        <span class="etiqueta">Cadena:</span>
                                        <telerik:RadComboBox ID="cmb_nodocomercial" runat="server" AutoPostBack="True" Skin="Vista"
                                            Width="238px" OnSelectedIndexChanged="cmb_nodocomercial_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </p>
                                    <p>
                                        <span class="etiqueta">Punto de Venta:</span>
                                        <telerik:RadComboBox ID="cmb_pventa" runat="server" Skin="Vista" Width="238px">
                                        </telerik:RadComboBox>
                                    </p>
                                    <p>
                                        <span class="etiqueta">Fuerza de Venta:</span>
                                        <telerik:RadComboBox ID="cmb_fuerzav" runat="server" Skin="Vista" Width="238px">
                                        </telerik:RadComboBox>
                                    </p>
                                    </div>
                                     <div style="float:left; margin-left:15px">
                                     <p>
                                     <span class="etiqueta">Supervisor:</span>
                                     <telerik:RadComboBox ID="cmb_supervisor" runat="server" Skin="Vista" Width="238px">
                                                </telerik:RadComboBox>
                                     </p>
                                     <p>
                                      <span class="etiqueta">Categoria:</span>
                                       <telerik:RadComboBox ID="cmb_categoria" runat="server" Width="238px" Skin="Vista"
                                                    AutoPostBack="True" OnSelectedIndexChanged="cmb_categoria_SelectedIndexChanged">
                                                </telerik:RadComboBox>
                                     </p>
                                     <p>
                                     <span class="etiqueta">Marca:</span>
                                     <telerik:RadComboBox ID="cmb_marca" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_marca_SelectedIndexChanged"
                                                    Skin="Vista" Width="238px">
                                                </telerik:RadComboBox>
                                     </p>
                                     <p>
                                     <span class="etiqueta">Familia:</span>
                                     <telerik:RadComboBox ID="cmb_familia" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_familia_SelectedIndexChanged"
                                                    Skin="Vista" Width="238px">
                                                </telerik:RadComboBox>
                                     </p>
                                     <p>
                                      <span class="etiqueta">SubFamilia:</span>
                                       <telerik:RadComboBox ID="cmb_subfamilia" runat="server" Skin="Vista" Width="238px">
                                                </telerik:RadComboBox>
                                     </p>
                                     </div>
                                   <div style="float:left; margin-left:15px">
                                   <p>
                                    <span class="etiqueta">Año:</span>
                                     <telerik:RadComboBox ID="cmb_año" runat="server" Skin="Vista" Width="238px">
                                                    </telerik:RadComboBox>
                                   </p>
                                   <p>
                                   <span class="etiqueta">Mes:</span>
                                    <telerik:RadComboBox ID="cmb_mes" runat="server" AutoPostBack="True" Skin="Vista"
                                                        Width="238px" OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged">
                                                    </telerik:RadComboBox>
                                   </p>
                                   <p>
                                    <span class="etiqueta">Periodo:</span>
                                     <telerik:RadComboBox ID="cmb_periodo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_periodo_SelectedIndexChanged"
                                                        Skin="Vista" Width="238px">
                                                    </telerik:RadComboBox>
                                   </p>
                                   </div>
                                
                            </div>
                            <table style="width: auto; height: auto;" align="center" class="art-Block-body">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <div>
                                            <asp:Label ID="lbl_saveconsulta" runat="server" Text="Guardar consulta"></asp:Label>
                                            <asp:ImageButton ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/qryguardar.png"
                                                Height="32px" Width="33px" />
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
                                <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3"
                                                AssociatedUpdatePanelID="UpdatePanel_filtros" BackgroundCssClass="modalProgressGreyBackground"
                                                EnableViewState="true">
                                                <ProgressTemplate>
                                                    <div>
                                                    Procesando...
                                                       <img alt="Procesando" src="../../../images/loading5.gif" style="vertical-align: middle" />
                                                    </div>
                                                </ProgressTemplate>
                                            </cc2:ModalUpdateProgress>
                                        </ContentTemplate>
            </asp:UpdatePanel>
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
                    Height="16px" Width="140px" Visible="true" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />
            </div>


        

                </Content>
        </cc1:AccordionPane>
            
            </Panes>
            </cc1:Accordion>
            <%--</asp:Panel>--%>
             <asp:UpdatePanel ID="UpdatePanel_validacion" runat="server">
        <ContentTemplate>
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
            </ContentTemplate>
    </asp:UpdatePanel>
 <cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender_validacion" runat="server"
        TargetControlID="UpdatePanel_validacion">
        <Animations> 
            <OnUpdated>
                <Sequence>
                  <FadeOut Duration="0.2" Fps="30" />
                  <FadeIn Duration="0.2" Fps="30" />
                </Sequence>
          </OnUpdated>
        </Animations>
    </cc1:UpdatePanelAnimationExtender>

            <div id="div_rportes" runat="server" align="center">
                <cc1:TabContainer ID="TabContainer_Reporte_Ventas_Distribuidora_AAVV" runat="server"
                    Style="width: auto" CssClass="magenta" ScrollBars="Both">
                    <%--ActiveTabIndex="0"--%>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel2" Style="width: auto;">
                        <HeaderTemplate>
                            Reporte Precios
                        </HeaderTemplate>
                        <ContentTemplate>
                            <%--<art:Reporte_v2_Precio_SF_AAVV_DET ID="Reporte_v2_Precio_SF_AAVV_DET" runat="server" />--%>
                            <rsweb:ReportViewer ID="reportinPreciosSF_M" runat="server" Font-Names="Verdana"
                                Font-Size="8pt" ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False"
                                ToolTip="Comparativo" SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </div>
           <%-- <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" AssociatedUpdatePanelID="UpFiltrosPrecios"
                BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>--%>
                    <%--<div>--%>
                        <%--   <telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />
                            <telerik:RadProgressArea Width="250" Height="10" ID="RadProgressArea1" runat="server" Culture="es-PE"  Localization-CurrentFileName="Cargando Informes"
                                DisplayCancelButton="false" ProgressIndicators="FilesCountBar,
                          FilesCountPercent" Skin="Outlook" EnableAjaxSkinRendering="False" />--%>
                       <%-- <telerik:RadProgressManager ID="RadProgressManager1" runat="server" ClientIDMode="AutoID" />
                        <telerik:RadProgressArea ID="RadProgressArea1" runat="server" Culture="es-PE" Localization-ElapsedTime="Tiempo transacurrido: "
                            Skin="WebBlue" EnableAjaxSkinRendering="False" Language="es-PE" Localization-TransferSpeed="Velocidad: "
                            Localization-CurrentFileName="Procesando Informes:" Localization-Total="" Localization-TotalFiles=""
                            Localization-Uploaded="" Localization-UploadedFiles="" Height="160px" ProgressIndicators="FilesCountBar, FilesCountPercent, CurrentFileName, TimeElapsed">
                        </telerik:RadProgressArea>--%>
                   <%-- </div>--%>
                       <%-- <div class="modalPopup">
                                Procesando, por favor espere...
                                <img alt="Procesando" src="../../../images/progress_bar.gif" style="vertical-align: middle" />
                                </div>
                </ProgressTemplate>
            </cc2:ModalUpdateProgress>--%>
        
    <%-- End Body--%>
</asp:Content>
