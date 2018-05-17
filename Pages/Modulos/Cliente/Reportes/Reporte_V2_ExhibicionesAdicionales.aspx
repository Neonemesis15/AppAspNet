<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_V2_ExhibicionesAdicionales.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_V2_ExhibicionesAdicionales" %>

<%@ Register Src="Informe_de_Exibicion/Reporte_v2_DetalleDeExhibicion.ascx" TagName="Reporte_v2_DetalleDeExhibicion"
    TagPrefix="uc1" %>
<%@ Register Src="Informe_de_Exibicion/Reporte_V2_GetionDeExibicion.ascx" TagName="Reporte_V2_GetionDeExibicion"
    TagPrefix="uc2" %>
<%@ Register Src="Informe_de_Exibicion/Revporte_v2_CumplimientoLayout.ascx" TagName="Revporte_v2_CumplimientoLayout"
    TagPrefix="uc3" %>
<%@ Register Src="~/Pages/Modulos/Cliente/Reportes/Informe_de_Exibicion/Reporte_V2_GetionDeExibicionD.ascx"
    TagName="Revporte_v2_GetionDeExibicionD" TagPrefix="uc4" %>
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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%-- end referecias ajax--%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte Exhibiciones
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
<asp:Content ID="ScriptReferences" ContentPlaceHolderID="ScriptIncludePlaceHolder"
    runat="server">
    <script src="Galeria_fotografica/Silverlight.js" type="text/javascript"></script>
    <script src="Galeria_fotografica/SlideShow.js" type="text/javascript"></script>
</asp:Content>
<%--end  referecias ajax y script--%>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <div id="Titulo_reporte_fotografico" style="text-align: left;">
        <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
            color: #D01887;">
            REPORTE DE EXHIBICIONES ADICIONALES
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
                                        Personalizado</HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpFiltrosExhibicion" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table style="width: auto; height: auto;" align="center" class="art-Block-body">
                                                    <tr>
                                                        <td align="right">
                                                            Año
                                                        </td>
                                                        <td align="left">
                                                            <telerik:RadComboBox ID="cmb_año" runat="server">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Mes
                                                        </td>
                                                        <td align="left">
                                                            <telerik:RadComboBox ID="cmb_mes" runat="server">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Cadena
                                                        </td>
                                                        <td align="left">
                                                            <telerik:RadComboBox ID="cmb_cadena" runat="server">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Categoria
                                                        </td>
                                                        <td align="left">
                                                            <telerik:RadComboBox ID="cmb_categoria" runat="server">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Tipo de Exhibición
                                                        </td>
                                                        <td align="left">
                                                            <telerik:RadComboBox ID="cmb_tipoExhibicion" runat="server">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div align="left">
                                                    Guardar consulta<asp:ImageButton ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/qryguardar.png"
                                                        Height="32px" Width="33px" />
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
                                                            Descripción :<asp:TextBox ID="txt_descripcion_parametros" runat="server" Width="300px"></asp:TextBox>
                                                            <br></br>
                                                            <asp:Button ID="btn_guardar_parametros" runat="server" Text="Guardar" CssClass="buttonGuardar"
                                                                OnClick="buttonGuardar_Click" Height="25px" Width="164px" />
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3"
                                                    AssociatedUpdatePanelID="UpFiltrosExhibicion" BackgroundCssClass="modalProgressGreyBackground">
                                                    <ProgressTemplate>
                                                        <div>
                                                           Obteniendo Datos...
                                                            <img alt="Procesando" src="../../../images/loading.gif" style="vertical-align: middle" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </cc2:ModalUpdateProgress>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabMisFavoritos" runat="server" HeaderText="TabPanel1">
                                    <HeaderTemplate>
                                        Mis Favoritos</HeaderTemplate>
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
                                                                <asp:Label ID="lbl_id_año" runat="server" Visible="False" Text='<%# Bind("id_año") %>'></asp:Label>
                                                                <asp:Label ID="lbl_id_mes" runat="server" Visible="False" Text='<%# Bind("id_mes") %>'></asp:Label>
                                                                <asp:Label ID="lbl_id_periodo" runat="server" Visible="False" Text='<%# Bind("id_periodo") %>'></asp:Label>
                                                                <asp:Label ID="lbl_id_tipoReporte" runat="server" Visible="False" Text='<%# Bind("id_tipoReporte") %>'></asp:Label>
                                                                <asp:ImageButton ID="btn_img_buscar" runat="server" ImageUrl="~/Pages/img/Qrys_File.png"
                                                                    Height="28px" Width="26px" CommandName="BUSCAR" />
                                                                <asp:ImageButton ID="btn_img_eliminar" runat="server" ImageUrl="~/Pages/images/delete.png"
                                                                    CommandName="ELIMINAR" />
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
            <%-- <HeaderTemplate>
            close
        </HeaderTemplate>
        <ContentTemplate>
        </ContentTemplate>--%>
        </asp:Accordion>
        <asp:UpdatePanel ID="UpdatePanel_validacion" runat="server" UpdateMode="Conditional">
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
            </ContentTemplate>
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
        <div id="div_rportes" runat="server" align="center" style="width: 100%;">
            <asp:TabContainer ID="TabContainer_Reporte_Fotografico" runat="server" CssClass="magenta"
                ActiveTabIndex="1" ScrollBars="Horizontal">
                <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel9" Enabled="false">
                    <HeaderTemplate>
                        Resumen Ejecutivo</HeaderTemplate>
                    <ContentTemplate>
                        RESUMEN EJECUTIVO</ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel2">
                    <HeaderTemplate>
                        Gestión de Exhibición</HeaderTemplate>
                    <ContentTemplate>
                        <uc2:Reporte_V2_GetionDeExibicion ID="Reporte_V2_GetionDeExibicion1" runat="server" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel5">
                    <HeaderTemplate>
                        Detalle Tip.Exh</HeaderTemplate>
                    <ContentTemplate>
                        <uc4:Revporte_v2_GetionDeExibicionD ID="Revporte_v2_GetionDeExibicionD1" runat="server" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel3">
                    <HeaderTemplate>
                        Detalle de Exhibición</HeaderTemplate>
                    <ContentTemplate>
                        <uc1:Reporte_v2_DetalleDeExhibicion ID="Reporte_v2_DetalleDeExhibicion1" runat="server" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                    <HeaderTemplate>
                        Muestra Fotografica
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div>
                            <asp:Button ID="btnbuscar" runat="server" Height="24px" Text="Buscar" Width="160px"
                                OnClick="btn_buscar_Click" />
                            <script type="text/javascript">
                                new SlideShow.Control(new SlideShow.XmlConfigProvider({ url: "Galeria_fotografica/ConfigurationRExhibicion.xml" }));
                            </script>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel4">
                    <HeaderTemplate>
                        Cumplimiento de Lay Out</HeaderTemplate>
                    <ContentTemplate>
                        <uc3:Revporte_v2_CumplimientoLayout ID="Reporte_v2_CumplimientoLayout1" runat="server" />
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </div>
       
</asp:Content>
