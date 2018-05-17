<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_Impulso_SF_M.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Impulso_SF_M" %>

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
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>
            <div align="center" style="font-family: Verdana; font-size: medium; color: #00579E;
                font-weight: bold">
                REPORTE DE IMPULSO - SAN FERNANDO</div>
            <table style="width: 100%; height: auto;" align="center" class="art-Block-body">
                <tr>
                    <td class="style3" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Fecha :
                    </td>
                    <td>
                        Desde :<telerik:RadDateTimePicker ID="txt_fecha_inicio" runat="server" Culture="es-PE" Skin="Web20"
                            Visible="true">
                            <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora">
                            </TimeView>
                            <TimePopupButton HoverImageUrl="" ImageUrl="" />
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Web20">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDateTimePicker>
                        <%--<telerik:RadDatePicker ID="txt_fecha_inicio" Width="275px" runat="server" Culture="es-PE"
                                Skin="Web20">
                                <Calendar UseColumnHeadersAsSelectors="false" UseRowHeadersAsSelectors="false" ViewSelectorText="x">
                                </Calendar>
                            </telerik:RadDatePicker>--%>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Hasta :
                        <telerik:RadDateTimePicker ID="txt_fecha_fin" runat="server" Culture="es-PE" Skin="Web20"
                            Visible="true">
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
                    <td class="style2">
                        Canal :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbcanal" runat="server" runat="server" AutoPostBack="True"
                            Enabled="true" Height="25px" Width="275px" OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Campaña :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Enabled="False"
                            Height="25px" Width="275px" OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Cadena :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbcorporacion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbcorporacion_SelectedIndexChanged"
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Ciudad :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbOficina" runat="server" Enabled="False" Height="25px" Width="275px"
                            AutoPostBack="true" OnSelectedIndexChanged="cmbOficina_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Cliente :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbNodeComercial" runat="server" AutoPostBack="true" Enabled="False"
                            Height="25px" OnSelectedIndexChanged="cmbNodeComercial_SelectedIndexChanged"
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Punto de Venta :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbPuntoDeVenta" runat="server" Enabled="False" Height="25px"
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Categoria de Producto :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbcategoria_producto" runat="server" AutoPostBack="True" Enabled="False"
                            Height="25px" Width="275px" OnSelectedIndexChanged="cmbcategoria_producto_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Marca :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbmarca" runat="server" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Supervisor :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbperson" runat="server" Enabled="False" Height="25px" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                    </td>
                    <td class="style6">
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                    </td>
                    <td class="style6">
                        <asp:Button ID="btn_buscar" runat="server" CssClass="buttonSearch" Height="25px"
                            OnClick="btn_buscar_Click" Text="Buscar" Width="164px" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                    </td>
                    <td class="style1">
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                    </td>
                    <td class="style6">
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                    </td>
                    <td class="style6">
                        <asp:Label ID="lblmensaje" runat="server" Style="text-align: left"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                    </td>
                    <td class="style6" align="right">
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;
                    </td>
                    <td class="style8" align="right">
                        <asp:Label ID="lbl_cb_all" runat="server" Text="Seleccionar todos" Font-Size="Small"></asp:Label>
                        <asp:CheckBox ID="cb_all" runat="server" OnCheckedChanged="cb_all_CheckedChanged"
                            Font-Size="Small" AutoPostBack="True" />
                    </td>
                </tr>
                <%-- <tr>
                            <td colspan=2 class="style8">
                             <div align="right">
                        <asp:Label ID="lbl_cb_all" runat="server" Text="Seleccionar todos" Visible="false"></asp:Label>
                        <asp:CheckBox ID="cb_all" Visible="false" runat="server" AutoPostBack="True" OnCheckedChanged="cb_all_CheckedChanged" />
                    </div>
                            </td>
                        </tr>--%>
            </table>
            <div id="div_Impulso" runat="server" class="class_div" style="width: auto; height: auto;">
                <table>
                    <tr>
                        <td colspan="2">
                            <telerik:RadGrid ID="gv_impulso" runat="server" AutoGenerateColumns="False" PageSize="30"
                                Skin="Vista" Font-Size="Small" AllowPaging="True" GridLines="None" ShowFooter="True"
                                OnCancelCommand="gv_precios_CancelCommand" OnDataBound="gv_precios_DataBound"
                                OnEditCommand="gv_precios_EditCommand" OnPageIndexChanged="gv_precios_PageIndexChanged"
                                OnPageSizeChanged="gv_precios_PageSizeChanged" OnPdfExporting="gv_precios_PdfExporting"
                                OnUpdateCommand="gv_precios_UpdateCommand">
                                <MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E"
                                    AlternatingItemStyle-BackColor="#F7F7F7" Font-Size="Smaller" AlternatingItemStyle-ForeColor="#333333">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="corporacion" HeaderText="Cadena" UniqueName="corporacion"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="oficina" HeaderText="Ciudad" UniqueName="oficina"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="nodocomercial" HeaderText="Cliente" UniqueName="nodocomercial"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="codigoPDV" HeaderText="Codigo PDV" UniqueName="codigoPDV"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="pdv" HeaderText="PDV" UniqueName="pdv" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="marca" HeaderText="Marca" UniqueName="marca"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="categoria" HeaderText="Categoria" UniqueName="categoria"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="familia" HeaderText="Familia" UniqueName="familia"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="subfamilia" HeaderText="Subfamilia" UniqueName="subfamilia"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="sku" HeaderText="SKU" UniqueName="sku" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="producto" HeaderText="Producto" UniqueName="producto"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="UnidadDeMedida" HeaderText="Unidad de Medida" UniqueName="UnidadDeMedida"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="supervisor" HeaderText="Supervisor" UniqueName="supervisor"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="stockinicial" HeaderText="Stock Inicial" UniqueName="stockinicial"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridNumericColumn DataField="ingreso" DataType="System.Double" EmptyDataText="no registró"
                                            HeaderText="Ingresos" UniqueName="ingreso">
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="stockfinal" DataType="System.Double" EmptyDataText="no registró"
                                            HeaderText="Stock Final" UniqueName="stockfinal">
                                        </telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn DataField="Ventas" HeaderText="Ventas" UniqueName="Ventas"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="mercaderista" HeaderText="Mercaderista:" UniqueName="mercaderista"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="fecharegistro" HeaderText="Fecha Reg:" UniqueName="fecharegistro"
                                            PickerType="DateTimePicker">
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="ModiBy" HeaderText="Modificado por:" UniqueName="ModiBy"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DateModiBy" HeaderText="Fecha de Modificación:"
                                            UniqueName="DateModiBy" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                            <HeaderTemplate>
                                                &nbsp;<asp:Button ID="btn_validar_Click" OnClientClick="return confirm('¿Esta seguro de invalidar los registros?');"
                                                    runat="server" Text="Invalidar" OnClick="btn_validar_Click_Click" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_validar" Checked='<%# Eval("validado")%>' runat="server" />
                                                <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                                <asp:Label ID="lblrimpulsodet" Text='<%#Eval("id_rimpulsodet") %>' runat="server"
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                            EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png"
                                            CancelText="Cancelar" UpdateText="Actualizar">
                                        </telerik:GridEditCommandColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <EditColumn UniqueName="EditCommandColumn1" ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                            EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png">
                                        </EditColumn>
                                    </EditFormSettings>
                                    <AlternatingItemStyle BackColor="#F7F7F7" ForeColor="#333333" />
                                </MasterTableView>
                                <PagerStyle PageSizeLabelText="Tamaño de pagina:" />
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:Button ID="btn_popup_ocultar" runat="server" CssClass="alertas" Text="ocultar"
                    Width="95px" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender_detalle" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" Enabled="True" PopupControlID="Panel_DetalleCompetencia" TargetControlID="btn_popup_ocultar"
                    CancelControlID="BtnclosePanel">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panel_DetalleCompetencia" runat="server" ScrollBars="Auto" BackColor="#D8D8DA"
                    Style="display: none">
                    <div>
                        <asp:ImageButton ID="BtnclosePanel" runat="server" BackColor="Transparent" Height="22px"
                            ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" /></div>
                    <div align="center">
                        <br />
                        <asp:Image ID="foto_url" runat="server" Height="320px" Width="500px" />
                    </div>
                </asp:Panel>
            </div>
            &nbsp;
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <table style="width: 100%;">
        <tr>
            <td class="style10">
                &nbsp;
            </td>
            <td class="style10">
                &nbsp;
                <asp:ImageButton ID="btn_img_exporttoexcel" runat="server" Height="42px" ImageUrl="~/Pages/Modulos/Operativo/Reports/Image/icono_excel.jpg"
                    OnClick="btn_img_exporttoexcel_Click" Width="39px" />
                Exportar a excel
            </td>
            <td class="style10">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp; &nbsp; &nbsp;
                <asp:GridView ID="gv_impulsoToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False" AutoGenerateColumns="False"
                    EnableModelValidation="True" ForeColor="#333333">
                    <PagerStyle CssClass="pager-row" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle CssClass="row" BackColor="#F7F6F3" ForeColor="#333333" />
                    <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerSettings PageButtonCount="7" FirstPageText="«" LastPageText="»" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="corporacion" HeaderText="Corporacion" />
                        
                        <asp:BoundField DataField="oficina" HeaderText="Ciudad" />
                        <asp:BoundField DataField="nodocomercial" HeaderText="Cliente" />
                        <asp:BoundField DataField="codigoPDV" HeaderText="Codigo del PDV" />
                        <asp:BoundField DataField="pdv" HeaderText="PDV" />
                        <asp:BoundField DataField="marca" HeaderText="Marca" />
                        <asp:BoundField DataField="categoria" HeaderText="Categoría" />
                        <asp:BoundField DataField="familia" HeaderText="Familia" />
                        <asp:BoundField DataField="subfamilia" HeaderText="SubFamilia" />
                        <asp:BoundField DataField="sku" HeaderText="SKU" />
                        <asp:BoundField DataField="producto" HeaderText="Producto" />
                        <asp:BoundField DataField="UnidadDeMedida" HeaderText="Unidad de Medida" />
                        <asp:BoundField DataField="supervisor" HeaderText="Supervisor" />
                        
                        <asp:BoundField DataField="ingreso" HeaderText="Ingresos" />
                        <asp:BoundField DataField="stockfinal" HeaderText="Stock Final" />
                        <asp:BoundField DataField="Ventas" HeaderText="Ventas" />
                        <asp:BoundField DataField="mercaderista" HeaderText="Mercaderista" />
                        <asp:BoundField DataField="fecharegistro" HeaderText="Fecha de Registro" />
                        <asp:BoundField DataField="ModiBy" HeaderText="Modificado por" />
                        <asp:BoundField DataField="DateModiBy" HeaderText="Fecha de modificacion" />
                        <asp:BoundField DataField="Validado" HeaderText="Validado" ReadOnly="true" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp; &nbsp;
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txt_fecha_inicio">
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
</asp:Content>
<%--<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style1
        {
            width: 195px;
        }
        </style>
</asp:Content>--%>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style2
        {
            text-align: right;
            width: 153px;
        }
        .style6
        {
            text-align: left;
            width: 836px;
        }
        .style8
        {
            font-size: medium;
            vertical-align: center;
        }
        .class_div
        {
            overflow-x: scroll;
            background-color: white;
        }
    </style>
</asp:Content>
