<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_Stock.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Stock" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--Referencias al usrcontrol--%>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Src="MasterPage/DefaultHeader.ascx" TagName="DefaultHeader" TagPrefix="uc1" %>
<%@ Register Src="MasterPage/DefaultMenu.ascx" TagName="DefaultMenu" TagPrefix="uc1" %>
<%@ Register Src="MasterPage/DefaultSidebar2.ascx" TagName="DefaultSidebar2" TagPrefix="uc1" %>
<%--end al usercontrol--%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Data Mercaderistas</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <uc1:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <uc1:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<%--<asp:Content ID="SideBar1" ContentPlaceHolderID="Sidebar1ContentPlaceHolder" runat="Server">
    <uc1:DefaultSidebar2 ID="DefaultSidebar1Content" runat="server" />
</asp:Content>--%>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <%--    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>
            <div style="width: 800px; margin: auto">
                <div class="style3" style="margin: auto">
                    REPORTE DE STOCK
                </div>
                <fieldset>
                    <legend>Búsqueda de Reporte de Stock</legend>
                    <table style="width: 100%; height: auto; margin: auto;" class="style1">
                        <tr>
                            <td class="style5">
                                Fecha Inicio:
                            </td>
                            <td>
                                <telerik:RadDateTimePicker ID="txt_fecha_inicio" runat="server" Culture="es-PE" Skin="Web20">
                                    <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora">
                                    </TimeView>
                                    <TimePopupButton HoverImageUrl="" ImageUrl="" />
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                        Skin="Web20">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDateTimePicker>
                            </td>
                            <td class="style5">
                                Fecha Fin:
                            </td>
                            <td>
                                <telerik:RadDateTimePicker ID="txt_fecha_fin" runat="server" Culture="es-PE" Skin="Web20">
                                    <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora">
                                    </TimeView>
                                    <TimePopupButton HoverImageUrl="" ImageUrl="" />
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                        Skin="Web20">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDateTimePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                Canal :
                            </td>
                            <td class="style7">
                                <asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Height="25px"
                                    OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="275px">
                                </asp:DropDownList>
                            </td>
                            <td class="style5">
                                Campaña :
                            </td>
                            <td class="style2">
                                <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Height="25px"
                                    OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged" Width="275px" Enabled="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                Mercaderista :
                            </td>
                            <td class="style7">
                                <asp:DropDownList ID="cmbperson" runat="server" Height="25px" Width="275px" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td class="style5">
                                Oficina :
                            </td>
                            <td class="style2">
                                <asp:DropDownList ID="cmbOficina" runat="server" Enabled="False" Height="25px" Width="275px"
                                    AutoPostBack="true" OnSelectedIndexChanged="cmbOficina_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                Zona :
                            </td>
                            <td class="style7">
                                <asp:DropDownList ID="cmbNodeComercial" runat="server" Enabled="False" Height="25px"
                                    Width="275px" OnSelectedIndexChanged="cmbNodeComercial_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td class="style5">
                                Punto de venta :
                            </td>
                            <td class="style2">
                                <asp:DropDownList ID="cmbPuntoDeVenta" runat="server" Enabled="False" Height="25px"
                                    Width="275px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                Categoria del producto :
                            </td>
                            <td class="style7">
                                <asp:DropDownList ID="cmbcategoria_producto" runat="server" AutoPostBack="True" Height="25px"
                                    OnSelectedIndexChanged="cmbcategoria_producto_SelectedIndexChanged" Width="275px"
                                    Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td class="style5">
                                Familia :
                            </td>
                            <td class="style2">
                                <asp:DropDownList ID="cmbfamilia" runat="server" Height="25px" Width="275px" Enabled="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                         
                    </table>
                    <div align="center">
                        <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="buttonBlue" Height="25px"
                            Width="164px" OnClick="btn_buscar_Click" />
                        <asp:Button ID="btn_exportarexcel" runat="server" CssClass="buttonGreen" Height="25px"
                            OnClick="btn_exportarexcel_Click" Text="Exportar a Excel" Width="164px" />
                        <asp:Button ID="BtnCrear" runat="server" CssClass="buttonGreen" Height="25px" Text="Crear"
                            Width="164px" />
                            <asp:Button ID="BtnCrearMasivo" runat="server" CssClass="buttonGreen" Height="25px" 
                                        Text="Carga Masiva" Width="164px" />
                    </div>
                    <div align="center">
                        <asp:Label ID="lblmensaje" runat="server" Style="text-align: left"></asp:Label></div>
                    <div id="controlesvalidez" runat="server">
                        <fieldset style="width: 550px; margin: auto">
                            <legend>Controles de validez de datos</legend>
                            <div class="centrarcontenido">
                                <asp:Button ID="btn_verificar" runat="server" CssClass="botonBlue" Height="25px"
                                    Text="Ver duplicados" Width="164px" />
                                <asp:Button ID="btn_verfecha" runat="server" CssClass="botonBlue" Height="25px" Text="Fuera de Período"
                                    Width="164px" OnClick="btn_verfecha_Click" />
                                <asp:Button ID="btn_verfaltante" runat="server" CssClass="botonBlue" Height="25px"
                                    Text="Faltantes" Width="164px" onclick="btn_verfaltante_Click" />
                            </div>
                            <div class="centrarcontenido">
                                <asp:Label ID="lbl_mensaje_verifica" runat="server" Style="text-align: left"></asp:Label>
                            </div>
                        </fieldset>
                    </div>
                    <div id="div_verDuplicados">
                        <cc1:ModalPopupExtender ID="ModalPopupExtender_verDuplicados" runat="server" TargetControlID="btn_verificar"
                            PopupControlID="panel_verDuplicados" BackgroundCssClass="modalBackground" CancelControlID="BtnclosePanel">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="panel_verDuplicados" runat="server" BackColor="#BFDBFF" BorderColor="#B5CBD5"
                            BorderStyle="Solid" Height="150px" Width="500px" BorderWidth="6px" Font-Names="Verdana" Style="display: none;">
                            <div>
                                <asp:ImageButton ID="BtnclosePanel" runat="server" BackColor="Transparent" Height="22px"
                                    ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" /></div>
                            <div style="font-size: 14px; color: #000000; font-weight: bold; margin: auto;" align="center">
                                Generar data duplicada
                            </div>
                            <br />
                            <div style="margin: auto;" align="center">
                                Año
                                <asp:DropDownList ID="cmb_año" runat="server">
                                </asp:DropDownList>
                                Mes
                                <asp:DropDownList ID="cmb_mes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged">
                                </asp:DropDownList>
                                Periodo
                                <asp:DropDownList ID="cmb_periodo" runat="server">
                                </asp:DropDownList>
                                <br />
                                <br />
                                <asp:Button ID="btn_verDuplicado" runat="server" Text="Ver" CssClass="botonBlue"
                                    Width="164px" Height="25px" OnClick="btn_verDuplicado_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                </fieldset>
            </div>
            <div id="div_gvStock" runat="server" style="width: 100%; height: auto;">
                <div>
                    <asp:ImageButton ID="btn_newRegister" runat="server" OnClick="btn_newRegister_Click"
                        Visible="false" />
                </div>
                <asp:GridView ID="gv_stock" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Style="margin-top: 0px"
                    Width="100%" AllowPaging="True" OnPageIndexChanging="gv_stock_PageIndexChanging"
                    AutoGenerateColumns="False" PageSize="100" OnRowEditing="gv_stock_RowEditing"
                    OnRowCancelingEdit="gv_stock_RowCancelingEdit" OnRowUpdating="gv_stock_RowUpdating"
                    EnableModelValidation="True">
                    <HeaderStyle CssClass="GridHeader" />
                    <RowStyle CssClass="GridRow" />
                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                    <Columns>
                        <asp:BoundField DataField="ROWID" HeaderText="N°" ReadOnly="true" />
                        <asp:BoundField DataField="Abreviatura" HeaderText="Oficina" ReadOnly="true" />
                        <asp:BoundField DataField="commercialNodeName" HeaderText="Zona" ReadOnly="true" />
                        <asp:BoundField DataField="ClientPDV_Code" HeaderText="Código PDV" ReadOnly="true" />
                        <asp:BoundField DataField="Punto de venta" HeaderText="Punto de venta" ReadOnly="true" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" ReadOnly="true" />
                        <asp:BoundField DataField="Familia" HeaderText="Familia" ReadOnly="true" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Cantidad
                            </HeaderTemplate>
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txt_gvs_cantidad" runat="server" Enabled="false" ShowSpinButtons="True"
                                    IncrementSettings-InterceptMouseWheel="False" NumberFormat-DecimalDigits="0"
                                    Height="20px" Skin="Telerik" Width="100px" Culture="es-PE" DbValue='<%#Eval("Cantidad")%>'
                                    MinValue="0" ToolTip="cantidad" EmptyMessage="vacio">
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Person_name" HeaderText="Registrado por" ReadOnly="true" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Fecha de registro
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_fec_Reg" runat="server" Text='<%# Eval("Fec_Reg_Bd")%>' Visible="true"></asp:Label>
                                <telerik:RadDateTimePicker ID="RadDateTimePicker_fec_reg" runat="server" Visible="False"
                                    DateInput-EmptyMessage="Fecha" TimePopupButton-ToolTip="Mostrar hora." DatePopupButton-ToolTip="Mostrar fecha."
                                    TimeView-Culture="es-PE" TimeView-Interval="00:20:00" Culture="es-PE" Skin="Outlook"
                                    EnableTyping="false">
                                    <TimeView CellSpacing="-1" Culture="es-PE" Interval="00:20:00" HeaderText="Hora">
                                    </TimeView>
                                    <TimePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Mostrar hora." />
                                    <Calendar Skin="Outlook" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput EmptyMessage="Fecha">
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Mostrar fecha." />
                                </telerik:RadDateTimePicker>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ModiBy" HeaderText="Modificado por" ReadOnly="true" />
                        <asp:BoundField DataField="DateModiBy" HeaderText="Modifico en" ReadOnly="true" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Button ID="btn_validar" runat="server" OnClick="btn_validar_Click" Text="Invalidar"
                                    OnClientClick="return confirm('¿Esta seguro de invalidar los registros?');" />
                                <br />
                                <asp:CheckBox ID="cb_all" runat="server" AutoPostBack="True" OnCheckedChanged="cb_all_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("Validado")%>' />
                                <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                <asp:Label ID="lbl_id_StockDetalle" runat="server" Text='<%# Eval("Id_rstkd") %>'
                                    Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" EditImageUrl="~/Pages/images/edit_icon.gif"
                            ButtonType="Image" CancelImageUrl="~/Pages/images/cancel_edit_icon.png" UpdateImageUrl="~/Pages/images/save_icon.png" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                </asp:GridView>

                <asp:GridView ID="gv_faltantes" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Style="margin-top: 0px"
                    Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" Visible="false">
                    <HeaderStyle CssClass="GridHeader" />
                    <RowStyle CssClass="GridRow" />
                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                    <Columns>
                        <asp:BoundField DataField="ROWID" HeaderText="N°" ReadOnly="true" />
                        <asp:BoundField DataField="Abreviatura" HeaderText="Oficina" ReadOnly="true" />
                        <asp:BoundField DataField="commercialNodeName" HeaderText="Zona" ReadOnly="true" />
                        <asp:BoundField DataField="ClientPDV_Code" HeaderText="Código PDV" ReadOnly="true" />
                        <asp:BoundField DataField="Punto de venta" HeaderText="Punto de venta" ReadOnly="true" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" ReadOnly="true" />
                        <asp:BoundField DataField="Familia" HeaderText="Familia" ReadOnly="true" />
                        <asp:BoundField DataField="Mercaderista" HeaderText="Mercaderista" ReadOnly="true" />
                        <asp:BoundField DataField="Periodo" HeaderText="Periodo" ReadOnly="true" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>
            <div id="div_popup_NewRegisterForm">
                <cc1:ModalPopupExtender ID="ModalPopupExtender_NewRegisterForm" runat="server" TargetControlID="btn_insert_stock"
                    PopupControlID="Panel_newRegister">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panel_newRegister" runat="server" BackImageUrl="~/Pages/images/FondoGris.jpg"
                    BorderColor="#7F99CC" BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana"
                    Font-Size="10pt" Height="490px" Style="display: none;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                PDV:
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmb_pdv" runat="server">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Categoria:
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmb_categoria" runat="server">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Familias
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmb_familia" runat="server">
                                </telerik:RadComboBox>
                                Cantidad:
                                <telerik:RadNumericTextBox ID="RadNumericTextBox1" runat="server">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rgv" runat="server">
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btn_insert_stock" runat="server" Text="Guardar" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3"
                AssociatedUpdatePanelID="UpdatePanel_contenido" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">
                        <div>
                            Cargando...
                        </div>
                        <br />
                        <div>
                            <img alt="Procesando" src="../../../images/loading5.gif" style="vertical-align: middle" />
                        </div>
                    </div>
                </ProgressTemplate>
            </cc2:ModalUpdateProgress>
            <asp:Panel ID="CrearReporStock" runat="server" CssClass="busqueda" DefaultButton="btnGuardarReportStock"
                Height="400px" Width="780px" Style="display: none">
                <table align="center">
                    <caption>
                        <br />
                        <tr>
                            <td>
                                <asp:Label ID="lblBGR" runat="server" CssClass="labelsTit2" Text="Crear Reporte de Stock" />
                            </td>
                        </tr>
                    </caption>
                </table>
                <table align="center">
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label12" runat="server" CssClass="labels" Text="Canal :" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCanal" runat="server" Width="205px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlCanal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="LblCanal" runat="server" CssClass="labels" Text="Campaña :" />
                        </td>
                        <td>
                            <asp:DropDownList runat="server" AutoPostBack="True" Width="205px" ID="ddlCampana"
                                OnSelectedIndexChanged="ddlCampana_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label19" runat="server" CssClass="labels" Text="Mercaderista :" />
                        </td>
                        <td>
                            <asp:DropDownList runat="server" Width="205px" ID="ddlMercaderista">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" CssClass="labels" Text="Oficina :" />
                        </td>
                        <td>
                            <asp:DropDownList runat="server" Width="205px" ID="ddlOficina" OnSelectedIndexChanged="ddlOficina_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label6" runat="server" CssClass="labels" Text="Zona:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNodeComercial" runat="server" Width="205px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlNodeComercial_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label13" runat="server" CssClass="labels" Text="Punto de venta:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPuntoVenta" runat="server" Width="205px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label1" runat="server" CssClass="labels" Text="Categoria:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCategoria" runat="server" Width="205px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label2" runat="server" CssClass="labels" Text="Familia:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFamilia" runat="server" Width="205px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" CssClass="labels" Text="Cantidad:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtCantidad" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label4" runat="server" CssClass="labels" Text="Observación:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtObservacion" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <caption>
                        <br />
                    </caption>
                </table>
                <table align="center">
                    <caption>
                        <br />
                        <tr>
                            <td>
                                <asp:Button ID="btnGuardarReportStock" runat="server" CssClass="buttonPlan" Text="Guardar"
                                    Width="80px" OnClick="btnGuardarReportStock_Click" />
                                <asp:Button ID="btnCancelar" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" />
                            </td>
                        </tr>
                    </caption>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="MopoReporStock" runat="server" BackgroundCssClass="modalBackground"
                DropShadow="True" Enabled="True" OkControlID="btnCancelar" PopupControlID="CrearReporStock"
                TargetControlID="BtnCrear" DynamicServicePath="">
            </cc1:ModalPopupExtender>


             <asp:Panel ID="CrearReporMasivo" runat="server" CssClass="busqueda" 
                Height="200px" Width="700px" >
                <table align="center">
                    <caption>
                        <br />
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" CssClass="labelsTit2" Text="Crear Reporte Stock" />
                            </td>
                        </tr>
                    </caption>
                </table>
                <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" CssClass="labels" Text="Canal :" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCanalCargaMasiva" runat="server" Width="205px" 
                                AutoPostBack="True" onselectedindexchanged="ddlCanalCargaMasiva_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="Label11" runat="server" CssClass="labels" Text="Archivo:" />
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpCMasivo" runat="server"  />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" CssClass="labels" Text="Campaña :" />
                        </td>
                        <td>
                            <asp:DropDownList runat="server"  Width="205px" ID="ddlCampañaCargaMasiva" onselectedindexchanged="ddlCampañaCargaMasiva_SelectedIndexChanged"
                              AutoPostBack="true" >
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            
                        </td>
                        <td>
                           <a class="button" href="../../../formatos/Formato_Masivo_Reporte_Stock.xls"><span>Descargar Formato</span></a>
                           <a runat="server" visible="false" id="Datos" class="button" href="masivo/DATOS_CARGA_REPORTE_STOCK.xls"><span>Descargar Datos</span></a>
                           </td>
                       
                    </tr>
                    <caption>
                        <br />
                    </caption>
                </table>
                <table align="center">
                    <caption>
                        <br />
                        <tr>
                            <td>
                                <asp:Button ID="btnCargaMasiva" runat="server" CssClass="buttonPlan" Text="Cargar"
                                    Width="80px" onclick="btnCargaMasiva_Click"  />
                                <asp:Button ID="btnCancelarCargaMasiva" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" />
                            </td>
                        </tr>
                    </caption>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="MopoReporMasivo" runat="server" BackgroundCssClass="modalBackground"
                DropShadow="True" Enabled="True" OkControlID="btnCancelarCargaMasiva" PopupControlID="CrearReporMasivo"
                TargetControlID="BtnCrearMasivo" DynamicServicePath="">
            </cc1:ModalPopupExtender>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_exportarexcel" />
            <asp:PostBackTrigger ControlID="btnCargaMasiva" />
        </Triggers>
    </asp:UpdatePanel>
    <%--<div style="text-align: center;">
        <asp:ImageButton ID="btn_img_exporttoexcel" runat="server" Height="42px" ImageUrl="~/Pages/Modulos/Operativo/Reports/Image/icono_excel.jpg"
            OnClick="btn_img_exporttoexcel_Click" Width="39px" />
    </div>
    <div style="text-align: center;">
        Exportar a excel
    </div>--%>
    <asp:GridView ID="gv_stockToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
        BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False">
        <RowStyle ForeColor="#000066" />
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style2
        {
            text-align: left;
        }
        .style3
        {
            text-align: center;
            font-weight: bold;
            font-size: medium;
            vertical-align: middle;
        }
        .style4
        {
            font-size: medium;
            font-weight: bold;
            vertical-align: middle;
        }
        .style5
        {
            text-align: right;
            width: 156px;
        }
        .style6
        {
            text-align: left;
            width: 156px;
        }
        .style7
        {
            text-align: left;
        }
    </style>
</asp:Content>
