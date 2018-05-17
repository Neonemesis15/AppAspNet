<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master" AutoEventWireup="true" CodeBehind="Reporte_v2_Pedido_3M.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Pedido_3M" %>
<%@ Register Src="Informe_de_Exibicion/Reporte_v2_DetalleDeExhibicion_3M.ascx" TagName="Reporte_v2_DetalleDeExhibicion_3M" TagPrefix="uc1" %>
<%@ Register Src="Informes_de_Precio/Reporte_v2_Precio_InformePrecio_3M.ascx" TagName="Reporte_v2_Precio_InformePrecio_3M" TagPrefix="uc2" %>
<%@ Register Src="Informes_de_Pedido/Reporte_v2_Pedido_3M.ascx" TagName="Reporte_v2_Pedido_3M" TagPrefix="uc3" %>
<%@ Register Src="Informe_de_Disponibilidad/Reporte_v2_Disponibilidad_3M.ascx" TagName="Reporte_v2_Disponibilidad_3M" TagPrefix="uc4" %>
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

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Reporte Exhibiciones
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptIncludePlaceHolder" runat="server">
    <script src="Galeria_fotografica/Silverlight.js" type="text/javascript"></script>
    <script src="Galeria_fotografica/SlideShow.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MenuContentPlaceHolder" runat="server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Sidebar1ContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="SheetContentPlaceHolder" runat="server">
    <asp:UpdatePanel ID="UpFiltrosExhibicion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             <div id="Titulo_reporte_fotografico" style="text-align: left;">
                <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
                    color: #D01887;">
                    REPORTE DE PEDIDO
                </div>
            </div>
            <div id="oculta" style="text-align: right">
                <asp:Button ID="btn_ocultar" runat="server" Text="Ocultar"
                    CssClass="buttonOcultar" Height="16px" Width="63px" />
            </div>
            <div id="Div_filtros" runat="server" align="center" style="width: auto;" visible="true">
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
                                        Semana
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_Semana" runat="server">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Cadena
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_cadena" runat="server" >
                                        </telerik:RadComboBox>
                                    </td>
                                </tr> 
                                <tr>
                                    <td align="right">
                                        Categoria
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_categoria" runat="server" AutoPostBack="true" onselectedindexchanged="cmb_categoria_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Familia
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="cmb_Familia" runat="server">
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
                                        <asp:Button ID="btn_guardar_parametros" runat="server" Text="Guardar" CssClass="buttonGuardar" OnClick="buttonGuardar_Click" 
                                            Height="25px" Width="164px" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabMisFavoritos" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Mis Favoritos</HeaderTemplate>
                        <ContentTemplate>
                            <div id="div_parametro" align="left">
                                Agregar<asp:ImageButton ID="btn_imb_tab" runat="server" Height="16px" ImageUrl="~/Pages/images/add.png"
                                    Width="16px" />
                                <telerik:RadGrid ID="RadGrid_parametros" runat="server" AutoGenerateColumns="False" OnItemCommand="RadGrid_parametros_ItemCommand" 
                                    GridLines="None"  Skin="">
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
                    Height="16px" Width="140px" Visible="true" ToolTip="Permite Generar el Informe" />
            </div>
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
                            <asp:CheckBox ID="chkb_validar" runat="server" ValidationGroup="1"
                                Text="Validar" AutoPostBack="true" />
                            <asp:CheckBox ID="chkb_invalidar" runat="server" ValidationGroup="1" Text="Invalidar"
                                 AutoPostBack="true" />
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
                                Width="164px" />
                            <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="buttonNew"
                                Height="25px" Width="164px" />
                            <asp:Button ID="btn_aceptar2" runat="server" Text="Aceptar" CssClass="buttonNew"
                                Height="25px" Width="164px" Visible="false" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div id="div_rportes" runat="server" align="center" style="width: 100%;">
               <cc1:TabContainer ID="TabContainer_Reporte_3M" runat="server" CssClass="magenta"
                    ActiveTabIndex="0" ScrollBars="Horizontal">                                                          
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel3">
                        <HeaderTemplate>
                            Gestión de Pedidos</HeaderTemplate>
                        <ContentTemplate>
                            <uc3:Reporte_v2_Pedido_3M ID="Reporte_v2_Pedido_3M1" runat="server" />
                        </ContentTemplate>
                    </cc1:TabPanel>
               </cc1:TabContainer>
            </div>
            <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3"
                AssociatedUpdatePanelID="UpFiltrosExhibicion" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>

                  <div>
                         <%--   <telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />
                            <telerik:RadProgressArea Width="250" Height="10" ID="RadProgressArea1" runat="server" Culture="es-PE"  Localization-CurrentFileName="Cargando Informes"
                                DisplayCancelButton="false" ProgressIndicators="FilesCountBar,
                          FilesCountPercent" Skin="Outlook" EnableAjaxSkinRendering="False" />--%>

                          <telerik:RadProgressManager ID="RadProgressManager2" runat="server" ClientIDMode="AutoID"  />
                    <telerik:RadProgressArea ID="RadProgressArea2" runat="server" Culture="es-PE" 
                        Localization-ElapsedTime="Tiempo transacurrido: "
                        Skin="WebBlue" EnableAjaxSkinRendering="False" Language="es-PE" 
                             Localization-TransferSpeed="Velocidad: " 
                             Localization-CurrentFileName="Procesando Informes:"  Localization-Total="" 
                             Localization-TotalFiles="" Localization-Uploaded="" 
                             Localization-UploadedFiles="" Height="160px" 
                             ProgressIndicators="FilesCountBar, FilesCountPercent, CurrentFileName, TimeElapsed">
                       
                    </telerik:RadProgressArea>
                    </div>
                    <%--<div class="modalPopup">
                        <div>
                            Procesando, por favor espere...
                            <img alt="Procesando" src="../../../images/progress_bar.gif" style="vertical-align: middle" />
                        </div>
                    </div>--%>
                </ProgressTemplate>
            </cc2:ModalUpdateProgress>            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
