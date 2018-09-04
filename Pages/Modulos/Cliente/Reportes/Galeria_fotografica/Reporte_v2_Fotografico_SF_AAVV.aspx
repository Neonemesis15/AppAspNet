<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_Fotografico_SF_AAVV.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Galeria_fotografica.Reporte_v2_Fotografico_SF_AAVV" %>

<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Reporte Fotografico
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptIncludePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<%--<asp:Content ID="SideBar1" ContentPlaceHolderID="Sidebar1ContentPlaceHolder" runat="server">
</asp:Content>--%>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="server">
    <script src="Silverlight.js" type="text/javascript"></script>
    <script src="SlideShow.js" type="text/javascript"></script>
<%--    <asp:UpdatePanel ID="UpFiltrosFotografico" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
            <div id="Titulo_reporte_fotografico" style="text-align: left;">
                <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
                    color: #D01887;">
                    REPORTE FOTOGRAFICO
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
                            Personalizado</HeaderTemplate>
                        <ContentTemplate>
                         <asp:UpdatePanel ID="UpFiltrosFotografico" runat="server" UpdateMode="Conditional">
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
                            </table>
                             <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" AssociatedUpdatePanelID="UpFiltrosFotografico"
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
                         </ContentTemplate>
                                    </asp:UpdatePanel>

                            <%--<div align="left">
                                Guardar consulta<asp:ImageButton ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/qryguardar.png"
                                    Height="32px" Width="33px" /><cc1:ModalPopupExtender ID="ModalPopupExtender_agregar"
                                        runat="server" BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True"
                                        PopupControlID="Panel_parametros" TargetControlID="btn_img_add" CancelControlID="BtnclosePanel"
                                        DynamicServicePath="">
                                    </cc1:ModalPopupExtender>
                                <asp:Panel ID="Panel_parametros" runat="server" BackColor="White" BorderColor="#0099CB"
                                    BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="150px"
                                    Width="400px" Style="display: none;">
                                    <div>
                                        <asp:ImageButton ID="BtnclosePanel" runat="server" BackColor="Transparent" Height="22px"
                                            ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" /></div>
                                    <div align="center">
                                        <div style="font-family: verdana; font-size: medium; color: #D01887;">
                                            Nueva consulta</div>
                                        <br />
                                        Descripción :<asp:TextBox ID="txt_descripcion_parametros" runat="server" Width="300px"></asp:TextBox><br>
                                        </br>
                                        <asp:Button ID="btn_guardar_parametros" runat="server" Text="Guardar" CssClass="buttonGuardar"
                                            OnClick="buttonGuardar_Click" Height="25px" Width="164px" /></div>
                                </asp:Panel>
                            </div>--%>
                        </ContentTemplate>
                    </asp:TabPanel>

                    <asp:TabPanel ID="TabMisFavoritos" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Mis Favoritos
                            </HeaderTemplate>
                       <%-- <ContentTemplate>
                            <div id="div_parametro" align="left">
                                Agregar<asp:ImageButton ID="btn_imb_tab" runat="server" Height="16px" ImageUrl="~/Pages/images/add.png"
                                    Width="16px" OnClick="btn_imb_tab_Click" /><telerik:RadGrid ID="RadGrid_parametros"
                                        runat="server" AutoGenerateColumns="False" GridLines="None" OnItemCommand="RadGrid_parametros_ItemCommand"
                                        Skin="">
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
                                                                                ID="lbl_id_oficina" runat="server" Visible="False" Text='<%# Bind("id_oficina") %>'></asp:Label><asp:Label
                                                                                    ID="lbl_id_año" runat="server" Visible="False" Text='<%# Bind("id_año") %>'></asp:Label><asp:Label
                                                                                        ID="lbl_id_mes" runat="server" Visible="False" Text='<%# Bind("id_mes") %>'></asp:Label><asp:Label
                                                                                            ID="lbl_id_periodo" runat="server" Visible="False" Text='<%# Bind("id_periodo") %>'></asp:Label><asp:Label
                                                                                                ID="lbl_id_tipoReporte" runat="server" Visible="False" Text='<%# Bind("id_tipoReporte") %>'></asp:Label><asp:ImageButton
                                                                                                    ID="btn_img_buscar" runat="server" ImageUrl="~/Pages/img/Qrys_File.png" Height="28px"
                                                                                                    Width="26px" CommandName="BUSCAR" /><asp:ImageButton ID="btn_img_eliminar" runat="server"
                                                                                                        ImageUrl="~/Pages/images/delete.png" CommandName="ELIMINAR" /></ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView></telerik:RadGrid></div>
                        </ContentTemplate>--%>
                    </asp:TabPanel>
                </asp:TabContainer>
            </div>
        
            </Content>
            </asp:AccordionPane>
        </Panes>
            <%--</asp:Panel>--%>
            </asp:Accordion>

        <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnbuscar" EventName="Click" />
            </Triggers>--%>

<%--
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
    </asp:UpdatePanelAnimationExtender>--%>
    <div id="div_rportes" runat="server" align="center" style="width: 100%;">
        <asp:TabContainer ID="TabContainer_Reporte_Fotografico" runat="server" CssClass="magenta">
            <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                <HeaderTemplate>
                    Galeria Fotografica
                </HeaderTemplate>
                <ContentTemplate>
                    <div>
                        <asp:Button ID="btnbuscar" runat="server" Height="24px" Text="Buscar" Width="160px"
                            OnClick="btn_buscar_Click" />
                        <script type="text/javascript">
                            new SlideShow.Control(new SlideShow.XmlConfigProvider({ url: "Configuration.xml" }));
                        </script>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
