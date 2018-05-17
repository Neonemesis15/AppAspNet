<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_PresenciaColgate.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_PresenciaColgate"
    EnableEventValidation="false" %>

<%@ Register Src="Informe_de_Presencia/Reporte_v2_HistoricalTables.ascx" TagName="Reporte_v2_HistoricalTables"
    TagPrefix="uc1" %>
<%@ Register Src="Informe_de_Presencia/Reporte_v2_Wholesalers.ascx" TagName="Reporte_v2_Wholesalers"
    TagPrefix="uc2" %>
<%@ Register Src="Informe_de_Presencia/Reporte_v2_IndexPrice.ascx" TagName="Reporte_v2_IndexPrice"
    TagPrefix="uc3" %>
<%@ Register Src="Informe_de_Presencia/Reporte_v2_IndexPriceDetail.ascx" TagName="Reporte_v2_IndexPriceDetail"
    TagPrefix="uc4" %>
<%@ Register Src="Informe_de_Presencia/Form_Presencia_Objetivos.ascx" TagName="Form_Presencia_Objetivos"
    TagPrefix="uc5" %>
<%@ Register Src="Informe_de_Presencia/Form_Presencia_PrecSugeridos.ascx" TagName="Form_Presencia_PrecSugeridos"
    TagPrefix="uc6" %>
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
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte de Presencia
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
</asp:Content>
<%--end  referecias ajax y script--%>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <div id="Titulo_reporte_presencia" style="text-align: left;">
        <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
            color: #D01887;">
            REPORTE DE PRESENCIA
        </div>
    </div>
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
                        <asp:UpdatePanel ID="UpdatePanel_filtros" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <cc1:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="1" CssClass="cyan">
                                    <cc1:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                                        <HeaderTemplate>
                                            Personalizado</HeaderTemplate>
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
                                                        <telerik:RadComboBox ID="cmb_mes" runat="server" Width="238px" Skin="Vista" AutoPostBack="true"
                                                            OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lbl_periodo" runat="server" Text="Periodo" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_periodo" runat="server" Width="238px" Skin="Vista" Visible="false">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        Cobertura
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_cobertura" runat="server" Width="238px" Skin="Vista"
                                                            AutoPostBack="true" OnSelectedIndexChanged="cmb_cobertura_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lbl_ciudad" runat="server" Text="Ciudad" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_oficina" runat="server" Width="238px" Skin="Vista" Visible="false">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lbl_cadena" runat="server" Text="Mercado" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_cadena" runat="server" Width="238px" Skin="Vista" Visible="false">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:UpdatePanel ID="up_saveConsulta" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div align="left">
                                                        Guardar consulta<asp:ImageButton ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/qryguardar.png"
                                                            Height="32px" Width="33px" />
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabMisFavoritos" runat="server" HeaderText="TabPanel1">
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
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                        <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3"
                            AssociatedUpdatePanelID="UpdatePanel_filtros" BackgroundCssClass="modalProgressGreyBackground"
                            DynamicLayout="true">
                            <ProgressTemplate>
                                <div>
                                    <img alt="Procesando" src="../../../images/loading5.gif" style="vertical-align: middle" />
                                </div>
                            </ProgressTemplate>
                        </cc2:ModalUpdateProgress>
                    </div>
                    <div align="center">
                        <asp:Button ID="btngnerar" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                            Height="16px" Width="140px" Visible="true" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />
                    </div>
                </Content>
            </cc1:AccordionPane>
        </Panes>
        <%-- <HeaderTemplate>
            close
        </HeaderTemplate>
        <ContentTemplate>
        </ContentTemplate>--%>
    </cc1:Accordion>
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
    <%--<asp:Button ID="Btndisparaalertas" runat="server" CssClass="alertas" Text="" Visible="true"
        Width="0px" />
    <asp:UpdatePanel ID="Up_Presencia" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <div id="div_rportes" runat="server" align="center" style="width: 100%;">
        <cc1:TabContainer ID="TabContainer_Reporte_Presencia" runat="server" CssClass="magenta"
            ActiveTabIndex="1">
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel_config">
                <ContentTemplate>
                    <div id="div_RE_cliente" runat="server">
                    </div>
                    <div id="div_config" runat="server">
                        <asp:UpdatePanel ID="up_objetivo" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td align="left">
                                            <telerik:RadTabStrip ID="RadTabStrip_Config" runat="server" Orientation="VerticalLeft"
                                                MultiPageID="RadMultiPage_config">
                                                <Tabs>
                                                    <telerik:RadTab Text="Resumen Ejecutivo" PageViewID="RadPageView_RE" runat="server"
                                                        Owner="" Selected="true">
                                                    </telerik:RadTab>
                                                    <telerik:RadTab Text="Objetivos de presencia" PageViewID="RadPageView_objetivos"
                                                        runat="server" Owner="">
                                                    </telerik:RadTab>
                                                    <telerik:RadTab Text="Precios sugeridos" PageViewID="RadPageView_precSugerido" runat="server"
                                                        Owner="">
                                                    </telerik:RadTab>
                                                </Tabs>
                                            </telerik:RadTabStrip>
                                        </td>
                                        <td style="border: 1px solid #C0C0C0; width: 100%;">
                                            <telerik:RadMultiPage ID="RadMultiPage_config" runat="server">
                                                <telerik:RadPageView ID="RadPageView_RE" runat="server" Selected="true">
                                                    <div id="div_viewHtmlFormat" runat="server">
                                                    </div>
                                                    <div>
                                                        <asp:Button ID="btn_editorPopUp" runat="server" Visible="false" Text="Editar" OnClick="btn_editorPopUp_Click" />
                                                    </div>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPageView_objetivos" runat="server">
                                                    <uc5:Form_Presencia_Objetivos ID="Form_Presencia_Objetivos1" runat="server" />
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPageView_precSugerido" runat="server" Width="100%">
                                                    <uc6:Form_Presencia_PrecSugeridos ID="Form_Presencia_PrecSugeridos1" runat="server" />
                                                </telerik:RadPageView>
                                            </telerik:RadMultiPage>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <cc2:ModalUpdateProgress ID="ModalUpdateProgress_objetivo" runat="server" DisplayAfter="3"
                            AssociatedUpdatePanelID="up_objetivo" BackgroundCssClass="modalProgressGreyBackground"
                            DynamicLayout="true">
                            <ProgressTemplate>
                                <div>
                                    <img alt="Procesando" src="../../../images/loading5.gif" style="vertical-align: middle" />
                                </div>
                            </ProgressTemplate>
                        </cc2:ModalUpdateProgress>
                        <cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender_objetivos" runat="server"
                            TargetControlID="up_objetivo">
                            <Animations> 
                                <OnUpdated>
                                    <Sequence>
                                      <FadeOut Duration="0.2" Fps="30" />
                                      <FadeIn  Duration="0.2" Fps="30" />
                                    </Sequence>
                              </OnUpdated>
                            </Animations>
                        </cc1:UpdatePanelAnimationExtender>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel2">
                <HeaderTemplate>
                    Executive Summary
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="up_ExecutiveSummary" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <uc2:Reporte_v2_Wholesalers ID="Reporte_v2_Wholesalers1" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel9" Width="700px">
                <HeaderTemplate>
                    Historical</HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="up_historical" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <uc1:Reporte_v2_HistoricalTables ID="Reporte_v2_HistoricalTables1" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel4">
                <HeaderTemplate>
                    Index Price</HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="up_indexPrice" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <uc3:Reporte_v2_IndexPrice ID="Reporte_v2_IndexPrice1" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                <HeaderTemplate>
                    Index Price Detail</HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="up_IndexPriceDetail" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <uc4:Reporte_v2_IndexPriceDetail ID="Reporte_v2_IndexPriceDetail1" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
    <%-- <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3"
                AssociatedUpdatePanelID="Up_Presencia" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    Hola mundo
                </ProgressTemplate>
            </cc2:ModalUpdateProgress>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btngnerar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
