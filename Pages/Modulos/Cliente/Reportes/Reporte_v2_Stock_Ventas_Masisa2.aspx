<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"  AutoEventWireup="true" CodeBehind="Reporte_v2_Stock_Ventas_Masisa2.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Stock_Ventas_Masisa2" %>

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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc3" %>
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
    <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>
    <%-- Start body--%>

            <%--<asp:Panel ID="Panel_filtros" runat="server" BackColor="White" BorderColor="#E46322"
                BorderWidth="3px" Style="display: block; width: auto" BorderStyle="Solid">--%>
            <div id="Titulo_reporte_Efectividad" style="text-align: left;">
                <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
                    color: #D01887;">
                    REPORTE DE VENTAS
                    </div>
            </div>
           <%-- <div id="oculta" style="text-align: right">
                <asp:Button ID="btn_ocultar" runat="server" OnClick="btn_ocultar_Click" Text="Filtros"
                    CssClass="buttonOcultar" Height="16px" Width="63px" />
            </div>--%>
            <cc1:Accordion ID="MyAccordion" runat="server" SelectedIndex="1" HeaderCssClass="accordionHeader"
        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
        AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                 <Panes>
           <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                <Header>
                    <div id="Div1" style="text-align: right">
                        <button class="buttonOcultar" >
                            Ampliar vista</button>
                    </div>
                </Header>
                <Content>

            <div id="Div_filtros" runat="server" align="center" visible="true" style="width: 100%">
            <asp:UpdatePanel ID="UpReportStock" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                <cc1:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                    <cc1:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Personalizado</HeaderTemplate>
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
                                        <cc3:DropDownCheckBoxes Visible="false" ID="ddl_mes" runat="server" AddJQueryReference="True"
                                                            RepeatDirection="Horizontal" Style="top: 0px; left: 315px; height: 16px; width: 500px"
                                                            UseButtons="True" UseSelectAllNode="True" CssClass="alinearizquierda">
                                                            <Texts CancelButton="Cancelar" SelectAllNode="--Seleccionar todo--" SelectBoxCaption="----Seleccione Punto de Venta----"  />
                                                            
                                                            
                                                        </cc3:DropDownCheckBoxes>
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
                                                                                <td>
                                            Dia
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmb_Dia" runat="server" Width="238px" Skin="Vista">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                                                            <td>
                                            Pto Venta
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmb_PtoVenta" runat="server" Width="238px" Skin="Vista">
                                            </telerik:RadComboBox>
                                        </td>
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

                                    <tr>
                                    <td colspan="8" style="text-align:center">
                                    <asp:UpdatePanel ID="up_saveConsulta" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                                        <div style="margin:auto">
                                                            <asp:Label ID="lbl_saveconsulta" runat="server" Text="Guardar consulta">
                                                            </asp:Label><asp:ImageButton ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/qryguardar.png"
                                                                Height="32px" Width="33px" />
                                                            <br />
                                                            <asp:Label ID="lbl_updateconsulta" runat="server" Text="Actualizar consulta" Visible="False">
                                                            </asp:Label><asp:ImageButton ID="btn_img_actualizar" runat="server" ImageUrl="~/Pages/images/update-icon-tm.jpg"
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
                                                                        Imagestyle="float:right;" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
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
                                                                        Height="22px" Imagestyle="float:right;" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
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
                                                        </ContentTemplate>
                            </asp:UpdatePanel>
                                                    </td>
                                    </tr>
                                </table>
                            </div>
                       
                            
                        </ContentTemplate>
                    </cc1:TabPanel>


                    <cc1:TabPanel ID="TabMisFavoritos" runat="server" HeaderText="TabPanel1">
                                        <HeaderTemplate>
                                            Mis Favoritos</HeaderTemplate>
                                        <ContentTemplate>
                                         <asp:UpdatePanel ID="up_favoritos" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                            <div id="div_parametro" align="left">
                                                Agregar<asp:ImageButton ID="btn_imb_tab" runat="server" Height="16px" ImageUrl="~/Pages/images/add.png"
                                                    Width="16px" OnClick="btn_imb_tab_Click" />
                                                <telerik:RadGrid ID="RadGrid_parametros" runat="server" AutoGenerateColumns="False"
                                                    GridLines="None" OnItemCommand="RadGrid_parametros_ItemCommand" Skin="" AllowPaging="True"
                                                    PageSize="10">
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="column">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_id" runat="server" Visible="False" Text='<%# Bind("id") %>'></asp:Label><asp:Label
                                                                        ID="lbl_id_servicio" runat="server" Visible="False" Text='<%# Bind("id_servicio") %>'></asp:Label><asp:Label
                                                                        ID="lbl_id_canal" runat="server" Visible="False" Text='<%# Bind("id_canal") %>'></asp:Label><asp:Label
                                                                        ID="lbl_id_company" runat="server" Visible="False" Text='<%# Bind("id_compañia") %>'> </asp:Label><asp:Label
                                                                        ID="lbl_id_reporte" runat="server" Visible="False" Text='<%# Bind("id_reporte") %>'> </asp:Label><asp:Label
                                                                        ID="lbl_id_user" runat="server" Visible="False" Text='<%# Bind("id_user") %>'></asp:Label><asp:Label
                                                                        ID="lbl_id_pdv" runat="server" Visible="False" Text='<%# Bind("id_punto_venta") %>'></asp:Label><asp:Label
                                                                        ID="lbl_id_categoria" runat="server" Visible="False" Text='<%# Bind("id_producto_categoria") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_id_subcategoria" runat="server" Visible="False" Text='<%# Bind("Id_subCategoria") %>'> </asp:Label><asp:Label
                                                                        ID="lbl_id_producto" runat="server" Visible="False" Text='<%# Bind("SkuProducto") %>'></asp:Label><asp:Label
                                                                        ID="lbl_id_año" runat="server" Visible="False" Text='<%# Bind("id_año") %>'></asp:Label><asp:Label
                                                                        ID="lbl_id_mes" runat="server" Visible="False" Text='<%# Bind("id_mes") %>'></asp:Label><asp:Label
                                                                        ID="lbl_id_periodo" runat="server" Visible="False" Text='<%# Bind("id_periodo") %>'></asp:Label>
                                                                        <asp:Label
                                                                        ID="lbl_id_dia" runat="server" Visible="False" Text='<%# Bind("id_dia") %>'></asp:Label><asp:ImageButton
                                                                        ID="btn_img_buscar" runat="server" ImageUrl="~/Pages/img/Qrys_File.png" Height="28px"
                                                                                                                            Width="26px" CommandName="BUSCAR" />
                                                                    <asp:ImageButton ID="btn_img_edit" runat="server" ImageUrl="~/Pages/images/edit_icon.gif"
                                                                        CommandName="EDITAR" /><asp:ImageButton ID="btn_img_eliminar" runat="server" ImageUrl="~/Pages/images/delete.png"
                                                                            CommandName="ELIMINAR" OnClientClick="return confirm('¿Confirma que desea eliminar el registro indicado?');" /></ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView></telerik:RadGrid></div>
                                                     </ContentTemplate>
                                                   
                                                    </asp:UpdatePanel>
                                                    
                                        </ContentTemplate>
                                    </cc1:TabPanel>


                </cc1:TabContainer>
                 </ContentTemplate>
                        </asp:UpdatePanel>
                      <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3"
                            AssociatedUpdatePanelID="UpReportStock" BackgroundCssClass="modalProgressGreyBackground">
                            <ProgressTemplate>
                                <div>
                                    Cargando...
                                    <img alt="Procesando" src="../../../images/loading.gif" style="vertical-align: middle" />
                                </div>
                            </ProgressTemplate>
                        </cc2:ModalUpdateProgress>
                <asp:Button ID="btngnerar" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                    Height="16px" Width="140px" Visible="true" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />
            </div>
            <%--</asp:Panel>--%>
            </Content>
            </cc1:AccordionPane>
          </Panes>
            </cc1:Accordion>

                <asp:UpdatePanel ID="UpdatePanel_validacion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div_Validar" runat="server" align="left" visible="false">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_año_value" runat="server" ></asp:Label>
                            <asp:Label ID="lbl_mes_value" runat="server" ></asp:Label>
                            <asp:Label ID="lbl_periodo_value" runat="server" ></asp:Label>
                            <asp:Label ID="lbl_validacion" runat="server"></asp:Label>
                        </td>
                        <td>
                        <asp:CheckBoxList ID="chkb_validar" runat="server" Height="18px" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="chkb_validar_OnSelectedIndexChanged" >

                        <asp:ListItem >Validar</asp:ListItem>
                        <asp:ListItem>Invalidar</asp:ListItem>

                        </asp:CheckBoxList>
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

            <div id="div_rportes" runat="server"  style="width: 100%; margin:auto;">
                <cc1:TabContainer ID="TabContainer_Reporte_Precio" runat="server" CssClass="magenta"
                    ActiveTabIndex="0" ScrollBars="Horizontal">
                    <cc1:TabPanel ID="TabPanel12" runat="server" HeaderText="TabPanel12">
                        <HeaderTemplate>
                            Evo.Mensual
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="margin:auto">
                                <rsweb:ReportViewer ID="EvolucionMensualdeVentas" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Reporte Ventas"
                                    SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                                </rsweb:ReportViewer>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel12">
                        <HeaderTemplate>
                             Evo.Mesual por Línea
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div>
                                <rsweb:ReportViewer ID="EvoluciondeVentasMesualporLinea" runat="server" Font-Names="Verdana"
                                    Font-Size="8pt" ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False"
                                    ToolTip="Reporte Ventas" SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                                </rsweb:ReportViewer>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel12">
                        <HeaderTemplate>
                            Evo.por Semana
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div>
                                <rsweb:ReportViewer ID="EvoluciondeVentasporSemana" runat="server" Font-Names="Verdana"
                                    Font-Size="8pt" ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False"
                                    ToolTip="Reporte Ventas" SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                                </rsweb:ReportViewer>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel12">
                        <HeaderTemplate>
                             Evo.Semanal por Linea
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div>
                                <rsweb:ReportViewer ID="EvoluciondeVentasSemanalesporLinea" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Reporte Ventas"
                                    SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                                </rsweb:ReportViewer>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
                <%--<asp:Button ID="Button1" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                    Height="16px" Width="140px" Visible="false" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />--%>
            </div>
    
    <%-- End Body--%>
</asp:Content>
