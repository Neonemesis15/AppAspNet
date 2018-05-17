<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_ControlActividadesAASS.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_ControlActividadesAASS" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
    Reporte Control de Actividades
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
                    REPORTE DE CONTROL DE ACTIVIDADES
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
                            Ampliar Vista</button>
                    </div>
                </Header>
                <Content>



            <div id="Div_filtros" runat="server" align="center" style="width: auto;" visible="true">
                <cc1:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                    <cc1:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Personalizado</HeaderTemplate>
                        <ContentTemplate>





                        <asp:UpdatePanel ID="UpFiltrosActividades" runat="server" UpdateMode="Conditional">
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
                                        Promoción
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_promocion" runat="server">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
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
                              <cc2:ModalUpdateProgress ID="ModalUpdateProgress3" runat="server" DisplayAfter="3"
                                                AssociatedUpdatePanelID="UpFiltrosActividades" BackgroundCssClass="modalProgressGreyBackground">
                                                <ProgressTemplate>
                                                    <div>
                                                         Cargando...
                                                        <img alt="Procesando" src="../../../images/loading.gif"  style="vertical-align: middle" />
                                                    </div>
                                                </ProgressTemplate>
                                            </cc2:ModalUpdateProgress>
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
                    </cc1:TabPanel>
                </cc1:TabContainer>
                <asp:Button ID="btngnerar" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                    Height="16px" Width="140px" Visible="true" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />
          </div>
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
    <div id="div_rportes" runat="server" align="center" style="width: 100%;">
        <cc1:TabContainer ID="TabContainer_Reporte_Fotografico" runat="server" CssClass="magenta"
            ActiveTabIndex="1" ScrollBars="Horizontal">
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel9">
                <HeaderTemplate>
                    Resumen Ejecutivo</HeaderTemplate>
                <ContentTemplate>
                    RESUMEN EJECUTIVO</ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel2">
                <HeaderTemplate>
                    Material POP</HeaderTemplate>
                <ContentTemplate>
                    <div align="center">
                        <rsweb:ReportViewer ID="Report_MatPOP" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Material POP"
                            SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                        </rsweb:ReportViewer>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel3">
                <HeaderTemplate>
                    Publicaciones</HeaderTemplate>
                <ContentTemplate>
                    <div>
                        <rsweb:ReportViewer ID="Report_Public" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Publicaciones"
                            SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                        </rsweb:ReportViewer>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel4">
                <HeaderTemplate>
                    Exhibición Programada e Impulso</HeaderTemplate>
                <ContentTemplate>
                    <div>
                        <rsweb:ReportViewer ID="Report_ExhiImp" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Exhibiciones"
                            SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                        </rsweb:ReportViewer>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>


    </asp:Content>
  
  