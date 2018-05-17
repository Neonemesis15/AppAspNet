<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_Competencia_SF_M.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Competencia_SF_M" %>

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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Informe_de_Competencia/Reporte_v2_Competencia_ResumenEjecutivo.ascx"
    TagName="Reporte_v2_Competencia_ResumenEjecutivo" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc3" %>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte de Competencia
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <%-- Start body--%>

            <div id="Titulo_reporte_precio" style="text-align: left;">
                <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
                    color: #D01887;">
                    REPORTE DE COMPETENCIA - MODERNO
                    
                </div>
            </div>
                        <%--<div id="oculta" style="text-align: right">
                <asp:Button ID="btn_ocultar" runat="server" OnClick="btn_ocultar_Click" Text="Ocultar"
                    CssClass="buttonOcultar" Height="16px" Width="63px" />--%>
             <cc1:Accordion ID="MyAccordion" runat="server" SelectedIndex="1" HeaderCssClass="accordionHeader"
        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
        AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
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
                        <div style="height: 250px; width: 800px; margin: auto;">
                                <div style="float: left">
                                    <p>
                                        <span class="etiqueta">Oficina:</span>
                                        <telerik:RadComboBox ID="cmb_oficina" runat="server" Width="238px" Skin="Vista" AutoPostBack="True"
                                                            OnSelectedIndexChanged="cmb_oficina_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                        <p>
                                        </p>
                                        <p>
                                            <span class="etiqueta">Corporación:</span>
                                            <telerik:RadComboBox ID="cmb_corporacion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_corporacion_SelectedIndexChanged"
                                                            Skin="Vista" Width="238px">
                                                        </telerik:RadComboBox>
                                            <p>
                                            </p>
                                            <p>
                                                <span class="etiqueta">Cadena:</span>
                                              <telerik:RadComboBox ID="cmb_nodocomercial" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_nodocomercial_SelectedIndexChanged"
                                                            Skin="Vista" Width="238px">
                                                        </telerik:RadComboBox>
                                                <p>
                                                </p>
                                                <p>
                                                    <%--<span class="etiqueta">Punto de Venta:</span>
                                                   <telerik:RadComboBox ID="cmb_pventa" runat="server" Skin="Vista" Width="238px">
                                                        </telerik:RadComboBox>--%>
                                                    <p>
                                                    <p>
                                                    <span class="etiqueta">Punto de Venta:</span>
                                                     <cc3:DropDownCheckBoxes ID="ddl_PuntoDeVenta" runat="server" 
                                                            AddJQueryReference="True" RepeatDirection="Horizontal" Style="top: 0px; left: 315px; height: 16px;
                                                        width: 500px" UseButtons="True" UseSelectAllNode="True">
                                                           
                                                            <Texts CancelButton="Cancelar" SelectAllNode="--Seleccionar todo--" 
                                                                SelectBoxCaption="--Seleccione Punto de Venta--" />
                                                        </cc3:DropDownCheckBoxes>
                                                        <div>
                                                            <asp:Panel ID="Panel_PuntoDeVenta" runat="server">
                                                            </asp:Panel>
                                                        </div>

                                                    </p>
                                                    </p>
                                                    <p>
                                                       <%-- <span class="etiqueta">Punto de Venta:</span>--%>
                                                      
                                                      
                                                        <p>
                                                        </p>
                                                        <p>
                                                            <span class="etiqueta">Fuerza de Venta:</span>
                                                            <telerik:RadComboBox ID="cmb_fuerzav" runat="server" Skin="Vista" Width="238px">
                                                        </telerik:RadComboBox>
                                                            <p>
                                                            </p>
                                                        </p>
                                                    </p>
                                                </p>
                                            </p>
                                        </p>
                                    </p>
                                </div>
                                <!-- otra columna -->
                                <div style="float: left; margin-left: 15px">
                                    <p>
                                        <label class="etiqueta" for="cmb_supervisor">
                                            Supervisor:</label>
                                      <telerik:RadComboBox ID="cmb_supervisor" runat="server" Skin="Vista" Width="238px">
                                                        </telerik:RadComboBox>
                                        <p>
                                        </p>
                                        <p>
                                            <span class="etiqueta">Categoria:</span>
                                           <telerik:RadComboBox ID="cmb_categoria" runat="server" Width="238px" Skin="Vista"
                                                        AutoPostBack="True" OnSelectedIndexChanged="cmb_categoria_SelectedIndexChanged">
                                                    </telerik:RadComboBox>
                                            <p>
                                            <span class="etiqueta">Empresa Competidora</span>
                                            <telerik:RadComboBox ID="cmb_companyCompetidora" runat="server" Width="238px" Skin="Vista"
                                                        AutoPostBack="True" 
                        onselectedindexchanged="cmb_companyCompetidora_SelectedIndexChanged" >
                                                    </telerik:RadComboBox>
                                            </p>
                                            <p>
                                                <label class="etiqueta" for="cmb_marca">
                                                Marca:</label>
                                           <telerik:RadComboBox ID="cmb_marca" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_marca_SelectedIndexChanged"
                                                Skin="Vista" Width="238px">
                                            </telerik:RadComboBox>
                                                <p>
                                                </p>
                                                <p>
                                                    <p>
                                                      <span class="etiqueta">Actividad:</span>
                                                      <telerik:RadComboBox ID="cmb_tipoActividad" runat="server" AutoPostBack="True" Skin="Vista" Width="238px">
                                            </telerik:RadComboBox>
                                                    </p>
                                                  
                                                    <p>

                                                        <p>
                                                        <%--<span class="etiqueta">Familia:</span>
                                                      <telerik:RadComboBox ID="cmb_familia" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_familia_SelectedIndexChanged"
                                                Skin="Vista" Width="238px">
                                            </telerik:RadComboBox>--%>
                                                        </p>
                                                        <p>
                                                            <%--<span class="etiqueta">SubFamilia:</span>
                                                           <telerik:RadComboBox ID="cmb_subfamilia" runat="server" Skin="Vista" Width="238px">
                                            </telerik:RadComboBox>--%>
                                                            <p>
                                                            </p>
                                                            <p>
                                                          
                                                                <p>
                                                                </p>
                                                                <p>
                                                                   <%-- <span class="etiqueta">Producto</span>--%>
                                                                 
                                                                    <p>
                                                                    </p>
                                                                </p>
                                                            </p>
                                                        </p>
                                                    </p>
                                                </p>
                                            </p>
                                        </p>
                                    </p>
                                </div>
                                <!-- otra columna -->
                                <div style="float: left; margin-left: 15px">
                                    <p>
                                        <span class="etiqueta">Año:</span>
                                        <telerik:RadComboBox ID="cmb_año" runat="server" Skin="Vista" Width="238px">
                                                        </telerik:RadComboBox>
                                        <p>
                                        </p>
                                        <p>
                                            <span class="etiqueta">Mes:</span>
                                               <telerik:RadComboBox ID="cmb_mes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged"
                                                            Skin="Vista" Width="238px">
                                                        </telerik:RadComboBox>
                                            <p>
                                            </p>
                                            <p>
                                                <span class="etiqueta">Periodo:</span>
                                                <telerik:RadComboBox ID="cmb_periodo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_periodo_SelectedIndexChanged"
                                                            Skin="Vista" Width="238px">
                                                        </telerik:RadComboBox> 
                                                <p>
                                                </p>
                                                <p>
                                                    <%--<span class="etiqueta">Días:</span>
                                                   <telerik:RadComboBox ID="cmb_dia" runat="server" Skin="Vista" Width="238px">
                                                        </telerik:RadComboBox>--%>
                                                    <p>
                                                     <span class="etiqueta">Días:</span>
                                                      <cc3:DropDownCheckBoxes ID="ddl_Dia" runat="server" 
                                                                 AddJQueryReference= "true"
                                                                    UseButtons="true" UseSelectAllNode="true" Style="top: 0px; left: 315px; height: 16px;
                                                                    width: 241px">
                                                                    <Texts SelectBoxCaption="------Seleccione Día(s)------" />
                                                             </cc3:DropDownCheckBoxes>
                                                    </p>

                                                      <p>
                                                    <span class="etiqueta">Público Objetivo: </span>
                                                      <telerik:RadComboBox ID="cmb_targetGroup" runat="server" AutoPostBack="True" Skin="Vista" Width="238px">
                                            </telerik:RadComboBox>
                                                    </p>
                                                </p>
                                            </p>
                                        </p>
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


                            <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3"
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
                <table align="center">
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btngnerar" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                                Height="16px" Width="140px" Visible="true" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnActComp" runat="server" Text="Act. Comp." CssClass="buttonOcultar"
                                Height="16px" Width="140px" Visible="true" 
                                ToolTip="Actividades de la Competencia" onclick="btnActComp_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnActCompPtoVenta" runat="server" Text="ActComp_X_PtoVenta" CssClass="buttonOcultar"
                                Height="16px" Width="140px" Visible="true" 
                                ToolTip="Actividades de la Competencia por Punto de Venta" 
                                onclick="btnActCompPtoVenta_Click" />
                        </td>
                    </tr>
                </table>
            </div>
         </Content>
         </cc1:AccordionPane>
         </Panes>
         </cc1:Accordion>
            

                <asp:UpdatePanel ID="UpdatePanel_validacion" runat="server">
        <ContentTemplate>
            <%-- Div para validar por el Analista --%>
            <div id="div_Validar" runat="server" align="left" visible="true">
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
                <%-- PopupValidarAnalista --%>
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
        <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel_validacion"
        BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup">
                Procesando, por favor espere...
                <img alt="Procesando...." src="../../../images/loading5.gif" style="vertical-align: middle" />
            </div>
        </ProgressTemplate>
    </cc2:ModalUpdateProgress>
        <%-- Bloque - Editar Fotografía --%>
    <div>
        <asp:Button ID="btn_popup_ocultar" runat="server" CssClass="alertas" Text="ocultar"
            Width="95px" />
        <cc1:ModalPopupExtender ID="ModalPopup_Edit" runat="server" DropShadow="True" TargetControlID="btn_popup_ocultar"
            PopupControlID="panelEdit">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="panelEdit" runat="server" BackColor="White" BorderColor="#AEDEF9"
            BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="360px"
            Width="450px" Style="display: none;">
            <div>
                <asp:ImageButton ID="ImageButton1" runat="server" BackColor="Transparent" Height="22px"
                    ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="BtnclosePanel_Click" />
            </div>
            <%-- Div para Editar o cambiar la foto actual --%>
            <%-- <div align="center" style="font-family: verdana; font-size: medium; color: #005DA3;">
                <div>
                    Cambiar Foto</div>
                <br />
                <div align="center">
                    <input type="file" runat="server" id="inputFile" />
                    <asp:Button ID="buttonSubmit" runat="server" Text="Cargar" CssClass="RadUploadButton"
                        OnClick="buttonSubmit_Click" />
                    <telerik:RadProgressManager ID="RadProgressManager1" runat="server" ClientIDMode="AutoID" />
                    <telerik:RadProgressArea ID="RadProgressArea1" runat="server" Culture="es-PE" Localization-Uploaded="Cargado "
                        Localization-UploadedFiles="Archivos Cargados: " Localization-TotalFiles="Total de Archivos: "
                        Localization-EstimatedTime="Tiempo estimado: " Localization-CurrentFileName="Cargando archivo: "
                        Localization-TransferSpeed="Velocidad: " Localization-ElapsedTime="Tiempo transacurrido: "
                        Skin="Outlook" EnableAjaxSkinRendering="False" Language="">
                    </telerik:RadProgressArea>
                    <br />
                    <br />
                </div>--%>
            <div>
                <telerik:RadBinaryImage ID="RadBinaryImage_fotoBig" runat="server" Width="420px"
                    Height="250px" AutoAdjustImageControlSize="False" Visible="false" AlternateText="Subir foto"
                    GenerateEmptyAlternateText="true" />
            </div>
            <%-- Div para declarar el botón Guardar y Cancelar del PoppupEditarFoto --%>
            <%--                <div style="font-size: small">
                    Guardar<asp:ImageButton ID="imgbtn_save" runat="server" ImageUrl="~/Pages/images/save_icon.png"
                        OnClick="imgbtn_save_Click" />
                    &nbsp;Cancelar<asp:ImageButton ID="imgbtn_cancel" runat="server" ImageUrl="~/Pages/images/cancel_edit_icon.png"
                        OnClick="imgbtn_cancel_Click" />
                </div>--%>
        </asp:Panel>
    </div>

     <%-- Bloque Grilla para Exportar a Excel --%>
    <asp:GridView ID="gv_competenciaToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
        BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False" AutoGenerateColumns="false"
        ForeColor="#333333">
        <PagerStyle CssClass="pager-row" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle CssClass="row" BackColor="#F7F6F3" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerSettings PageButtonCount="7" FirstPageText="«" LastPageText="»" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="ROWID" HeaderText="N°" ReadOnly="true" />
            <asp:BoundField DataField="corporacion" HeaderText="Cadena" />
            <asp:BoundField DataField="oficina" HeaderText="Ciudad" />
            <asp:BoundField DataField="nodocomercial" HeaderText="Cliente" />
            <asp:BoundField DataField="codigoPDV" HeaderText="Código PDV" />
            <asp:BoundField DataField="pdv" HeaderText="Punto de Venta" />
            <asp:BoundField DataField="competidora" HeaderText="Empresa Competidora" />
            <asp:BoundField DataField="Marca" HeaderText="Marca Competidora" />
            <asp:BoundField DataField="categoria" HeaderText="Categoria" />
            <asp:BoundField DataField="actividad" HeaderText="Nombre de Actividad" />
            <asp:BoundField DataField="grupoobjetivo" HeaderText="Grupo Objetivo" />
            <asp:BoundField DataField="promocionini" HeaderText="Inicio de Actividad" />
            <asp:BoundField DataField="promocionfin" HeaderText="Fin de Actividad" />
            <asp:BoundField DataField="mecanica" HeaderText="mecanica" />
            <asp:BoundField DataField="precioregular" HeaderText="Precio Regular" />
            <asp:BoundField DataField="preciooferta" HeaderText="Precio Oferta" />
            <asp:BoundField DataField="supervisor" HeaderText="Supervisor" />
            <asp:BoundField DataField="mercaderista" HeaderText="Mercaderista" />
            <asp:BoundField DataField="fec_comunicacion" HeaderText="Fecha de comunicación" />
            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
            <%--<asp:BoundField DataField="mercaderista" HeaderText="Registrado por" />--%>
            <asp:BoundField DataField="Fec_Reg_Bd" HeaderText="Fecha de registro" />
            <asp:BoundField DataField="ModiBy" HeaderText="Modificado por" ReadOnly="true" />
            <asp:BoundField DataField="DateModiBy" HeaderText="Modifico en" />
            <asp:BoundField DataField="Validado" HeaderText="Validado" ReadOnly="true" />
            <%--<asp:BoundField DataField="DateModiBy" HeaderText="Modifico en" ReadOnly="true" />--%>
            <%--<asp:CheckBoxField DataField="Validado" HeaderText="Validado" />--%>
        </Columns>
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <%--                    <RowStyle ForeColor="#000066" />
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />--%>
    </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
    <%-- Div para Mostrar los Reportes --%>
    <div id="div_rportes" runat="server" align="center">
        <cc1:TabContainer ID="TabContainer_Reporte_Ventas_Distribuidora_AAVV" runat="server"
            Style="width: auto" CssClass="magenta" ScrollBars="Both">
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel2" Style="width: auto;">
                <HeaderTemplate>
                    Report Compet Fotografía</HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <telerik:RadGrid ID="rgv_competencia" runat="server" AutoGenerateColumns="False"
                                Skin="Vista" Font-Size="Small" AllowPaging="True" GridLines="None" ShowFooter="True"
                                OnDataBound="rgv_competencia_DataBound" OnItemCommand="rgv_competencia_ItemCommand"
                                OnCancelCommand="rgv_competencia_CancelCommand" OnEditCommand="rgv_competencia_EditCommand"
                                OnPageIndexChanged="rgv_competencia_PageIndexChanged" OnPageSizeChanged="rgv_competencia_PageSizeChanged"
                                OnUpdateCommand="rgv_competencia_UpdateCommand" PageSize="2000">
                                <%--
                                para permitir establecer el ancho de las columnas 
                                TableLayout="Fixed"
                                --%><MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E"
                                    AlternatingItemStyle-BackColor="#F7F7F7" Font-Size="Smaller" AlternatingItemStyle-ForeColor="#333333"
                                    EditMode="PopUp">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ROWID" HeaderText="N°" UniqueName="ROWID" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Foto" UniqueName="TemplateColumn2">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:LinkButton ID="lnkBtn_viewphoto" runat="server" CommandArgument='<%# Bind("Id_regft") %>'
                                                                CommandName="VERFOTO" Font-Underline="True">Ver</asp:LinkButton>
                                                        </td>
                                                        <%-- <td>
                                                            <asp:LinkButton ID="lnkBtn_Editphoto" runat="server" CommandArgument='<%# Bind("Id_regft") %>'
                                                                CommandName="EDITFOTO" Font-Underline="True">Cambiar_Foto</asp:LinkButton>
                                                        </td>--%></tr>
                                                </table>
                                                <asp:Label ID="lbl_id_reg_foto" runat="server" Text='<%# Bind("Id_regft") %>' Visible="False"></asp:Label><telerik:RadBinaryImage
                                                    ID="RadBinaryImage_foto" runat="server" Width="110px" Height="90px" DataValue='<%# Eval("Foto") %>'
                                                    AutoAdjustImageControlSize="False" /></ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="corporacion" HeaderText="Cadena" ReadOnly="true"
                                            UniqueName="corporacion">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="oficina" HeaderText="Ciudad" ReadOnly="true"
                                            UniqueName="oficina">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="nodocomercial" HeaderText="Cliente" ReadOnly="true"
                                            UniqueName="nodocomercial">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="codigoPDV" HeaderText="Código PDV" ReadOnly="true"
                                            UniqueName="codigoPDV">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="pdv" HeaderText="Punto de Venta" ReadOnly="true"
                                            UniqueName="pdv">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="competidora" HeaderText="Empresa" ReadOnly="true"
                                            UniqueName="competidora">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Marca" HeaderText="Marca" UniqueName="Marca"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="categoria" HeaderText="Categoria" ReadOnly="true"
                                            UniqueName="categoria">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="actividad" HeaderText="Nombre de Actividad" UniqueName="actividad"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="grupoobjetivo" HeaderText="Grupo Objetivo" UniqueName="grupoobjetivo"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="promocionini" UniqueName="promocionini" HeaderText="Inicio de Actividad"
                                            PickerType="DatePicker">
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridDateTimeColumn DataField="promocionfin" UniqueName="promocionfin" HeaderText="Fin de Actividad"
                                            PickerType="DatePicker">
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="mecanica" HeaderText="Mecanica" UniqueName="mecanica"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="precioregular" DataType="System.Double" EmptyDataText="NULO"
                                            HeaderText="Precio Regular" UniqueName="precioregular">
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="preciooferta" DataType="System.Double" EmptyDataText="NULO"
                                            HeaderText="Precio Oferta" UniqueName="preciooferta">
                                        </telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn DataField="supervisor" HeaderText="Supervisor" UniqueName="supervisor"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="mercaderista" HeaderText="Mercaderista" UniqueName="mercaderista"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="fec_comunicacion" HeaderText="Fecha de comunicación"
                                            UniqueName="fec_comunicacion" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones:" UniqueName="Observaciones"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="mercaderista" HeaderText="Registrado por:" ReadOnly="true"
                                            UniqueName="mercaderista">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Fec_Reg_Bd" HeaderText="Fecha de Relevo:" ReadOnly="true"
                                            UniqueName="Fec_Reg_Bd">
                                        </telerik:GridBoundColumn>
                                        <%--  <telerik:GridBoundColumn DataField="ModiBy" HeaderText="Modificado por" ReadOnly="true"
                                            UniqueName="ModiBy">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DateModiBy" HeaderText="Modifico en" ReadOnly="true"
                                            UniqueName="DateModiBy">
                                        </telerik:GridBoundColumn>--%><%-- <telerik:GridTemplateColumn HeaderText="Validar" UniqueName="Validado">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("Validado")%>' />
                                                <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                                <asp:Label ID="lbl_id_reg_competencia" Visible="false" runat="server" Text='<%# Bind("Id_rcompe") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                            EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png">
                                        </telerik:GridEditCommandColumn>--%></Columns>
                                    <%--<AlternatingItemStyle BackColor="#F7F7F7" ForeColor="#333333" />--%></MasterTableView><%--                                <ClientSettings AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                        <Resizing AllowRowResize="True" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                            AllowColumnResize="True"></Resizing>
                        <ClientEvents OnGridCreated="onGridCreated" />
                    </ClientSettings>--%><%--<HeaderStyle Width="100px" />--%><%-- Genera un Error porque falta el Script que define esta funcionalidad 
                         para este reporte no se usa
                    --%><%--        <ClientSettings>
                                    <ClientEvents OnPopUpShowing="PopUpShowing" />
                                </ClientSettings>--%></telerik:RadGrid>
                            <%-- Bloque - Ver Fotografía --%>
                            <div id="divFotografia">
                                <asp:Button ID="btn_view" runat="server" CssClass="alertas" Text="ocultar" Width="95px" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender_viewfoto" runat="server" DropShadow="True"
                                    TargetControlID="btn_view" PopupControlID="panel_viewfoto" CancelControlID="ImageButtonCancel_viewfoto">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="panel_viewfoto" runat="server" BackColor="White" BorderColor="#AEDEF9"
                                    BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="450px"
                                    Width="590px" Style="display: none">
                                    <div>
                                        <asp:ImageButton ID="ImageButtonCancel_viewfoto" runat="server" BackColor="Transparent"
                                            Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                                    </div>
                                    <div align="center" style="font-family: verdana; font-size: medium; color: #005DA3;">
                                        <div>
                                            Foto</div>
                                        <br />
                                        <div>
                                            <telerik:RadBinaryImage ID="RadBinaryImage_viewFoto" runat="server" Width="570px"
                                                Height="400px" AutoAdjustImageControlSize="False" AlternateText="Subir foto"
                                                GenerateEmptyAlternateText="true" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     <div>
                        <asp:ImageButton ID="btn_img_toExcel" runat="server" OnClick="btn_img_toExcel_Click"
                            Height="27px" ImageUrl="~/Pages/Modulos/Operativo/Reports/Image/icono_excel.jpg"
                            Width="28px" />Exportar a Excel
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <%--<cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" Style="width: auto;">
                        <HeaderTemplate>
                            Rep. Competencia</HeaderTemplate>
                        <ContentTemplate>
                            <rsweb:ReportViewer ID="rpt_competencia_SF_M" runat="server" Font-Names="Verdana"
                                Font-Size="8pt" ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False"
                                ToolTip="Comparativo" SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>--%>
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" Style="width: auto;">
                <HeaderTemplate>
                    Activ. Competencia
                </HeaderTemplate>
                <ContentTemplate>
                    <rsweb:ReportViewer ID="rpt_ActComp" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False" ToolTip="Comparativo"
                        SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                    </rsweb:ReportViewer>
                </ContentTemplate>
            </cc1:TabPanel>
             <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel3" Style="width: auto;">
                <HeaderTemplate>
                    Activ. Por Tienda
                </HeaderTemplate>
                <ContentTemplate>
                    <rsweb:ReportViewer ID="rpt_ActComp_X_Tda" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False" ToolTip="Comparativo"
                        SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                    </rsweb:ReportViewer>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>

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
    
   
    
   
</asp:Content>
