<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Report_V2_Quiebres.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Report_V2_Quiebres" %>

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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%-- Informes--%>
<%--end Informes--%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte de Quiebres AASS
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
            REPORTE QUIEBRES AASS
        </div>
    </div>
    <asp:accordion id="MyAccordion" runat="server" selectedindex="1" headercssclass="accordionHeader"
        headerselectedcssclass="accordionHeaderSelected" contentcssclass="accordionContent"
        autosize="None" fadetransitions="true" transitionduration="250" framespersecond="40"
        requireopenedpane="false" suppressheaderpostbacks="true">
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
                         <asp:UpdatePanel ID="UpFiltrosQuiebres" runat="server" UpdateMode="Conditional">
                           <ContentTemplate>
                            <table style="width: auto; height: auto;" class="art-Block-body">
                                <tr>
                                    <td align="right">
                                        Año
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_año" runat="server" Width="238px" Skin="Vista">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td align="right">
                                        Mes
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_mes" runat="server" Width="238px" Skin="Vista" OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td align="right">
                                        Periodo
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_periodo" runat="server" Width="238px" Skin="Vista" OnSelectedIndexChanged="cmb_periodo_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td align="right">
                                        Dias
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_Dias" runat="server" Width="238px" Skin="Vista">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <table id="tfil" runat="server" align="center">
                                    <tr align="center">
                                        <td align="right">
                                            Categorias
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmb_categoria" runat="server" Width="238px" Skin="Vista">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td align="right">
                                            Cadenas
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmb_cadena" runat="server" Width="238px" Skin="Vista">
                                            </telerik:RadComboBox>
                                        </td>
                                         <td align="right">
                                            Tipo de Quiebre
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbquiebrre" runat="server" Width="238px" Skin="Vista">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                                <tr>
                                    <td align="left">
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:Label ID="lbl_saveconsulta" runat="server" Text="Guardar consulta"></asp:Label><asp:ImageButton
                                                ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/qryguardar.png" Height="32px"
                                                Width="33px" /><br />
                                            <asp:Label ID="lbl_updateconsulta" runat="server" Text="Actualizar consulta" Visible="False"></asp:Label><asp:ImageButton
                                                ID="btn_img_actualizar" runat="server" ImageUrl="~/Pages/images/update-icon-tm.jpg"
                                                Height="25px" Width="29px" Visible="False" /><asp:ModalPopupExtender ID="ModalPopupExtender_agregar"
                                                    runat="server" BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True"
                                                    PopupControlID="Panel_parametros" TargetControlID="btn_img_add" CancelControlID="BtnclosePanel"
                                                    DynamicServicePath="">
                                                </asp:ModalPopupExtender>
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
                                                        Height="25px" Width="164px" /></div>
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
                                                        Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" /></div>
                                                <div align="center">
                                                    <div style="font-family: verdana; font-size: medium; color: #D01887;">
                                                        Actualizar consulta</div>
                                                    <br />
                                                    Descripción :<asp:TextBox ID="txt_pop_actualiza" runat="server" Width="300px"></asp:TextBox><br>
                                                    </br>
                                                    <asp:Button ID="btn_actualizar" runat="server" Text="Actualizar" CssClass="buttonGuardar"
                                                        Height="25px" Width="164px" /></div>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                               <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3"
                                                AssociatedUpdatePanelID="UpFiltrosQuiebres" BackgroundCssClass="modalProgressGreyBackground">
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
                                                   <asp:Label ID="lbl_id_año" runat="server" Visible="False" Text='<%# Bind("id_año") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_mes" runat="server" Visible="False" Text='<%# Bind("id_mes") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_periodo" runat="server" Visible="False" Text='<%# Bind("id_periodo") %>'></asp:Label>
                                                    <asp:Label ID="Label1" runat="server" Visible="False" Text='<%# Bind("id_periodo") %>'></asp:Label>
                                                    <asp:Label ID="lbl_quiebre" runat="server" Visible="False" Text='<%# Bind("quiebre") %>'></asp:Label>
                                                     <asp:Label ID="lbl_dias" runat="server" Visible="False" Text='<%# Bind("sdias") %>'></asp:Label>
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
    </asp:accordion>
    <%--</asp:Panel>--%>
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
            <div align="left">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_msj_allMonth" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:updatepanelanimationextender id="UpdatePanelAnimationExtender_validacion" runat="server"
        targetcontrolid="UpdatePanel_validacion">
        <Animations> 
            <OnUpdated>
                <Sequence>
                  <FadeOut Duration="0.2" Fps="30" />
                  <FadeIn Duration="0.2" Fps="30" />
                </Sequence>
          </OnUpdated>
        </Animations>
    </asp:updatepanelanimationextender>
    <div id="div_rportes" runat="server" align="center" style="width: auto;">
        <asp:TabContainer ID="TabContainer_Reporte_Precio" runat="server" CssClass="magenta"
            ActiveTabIndex="1" ScrollBars="Horizontal">
            <asp:TabPanel ID="TabPanel14" runat="server" HeaderText="TabPanel4" Enabled="false">
                <HeaderTemplate>
                    Informe Ejecutivo</HeaderTemplate>
                <ContentTemplate>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel12" runat="server" HeaderText="TabPanel12">
                <HeaderTemplate>
                    Informe Quiebres
                </HeaderTemplate>
                <ContentTemplate>
                    <div>
                        <asp:UpdatePanel ID="upquiebre" runat="server">
                            <ContentTemplate>
                                <rsweb:ReportViewer ID="ReporQuiebre" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False" ToolTip="Quiebres"
                                    SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                                </rsweb:ReportViewer>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
        <%--<asp:Button ID="Button1" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                    Height="16px" Width="140px" Visible="false" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />--%>
    </div>
   
    <%-- End Body--%>
</asp:Content>
