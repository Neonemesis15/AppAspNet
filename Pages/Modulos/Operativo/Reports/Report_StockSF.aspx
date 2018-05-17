<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_StockSF.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_StockSF" %>

<%--Referencias al ProgressBar MB--%>
<%@ Register Assembly="MattBerseth.WebControls.AJAX" Namespace="MattBerseth.WebControls.AJAX.Progress"
    TagPrefix="mb" %>
<%--Fin de la referencia al ProgressBar MB--%>
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
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <div>
        <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
            <ContentTemplate>
                <div align="center" style="font-family: Verdana; font-size: medium; color: #00579E;
                    font-weight: bold">
                    REPORTE DE STOCK SAN FERNANDO</div>
                <table style="width: 100%; height: auto;" align="center" class="art-Block-body">
                    <tr>
                        <td class="style3" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            Fecha :
                        </td>
                        <td>
                            <%-- <asp:TextBox runat="server" ID="txt_fecha_inicio1" />
        <asp:ImageButton runat="Server" ID="Image1" ImageUrl="Image/Calendar_scheduleHS.png" AlternateText="Click to show calendar" /><br />
          <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" 
                            TargetControlID="txt_fecha_inicio1" format="dd/MM/yyyy"
            PopupButtonID="Image1" />
                            --%>
                            <telerik:RadDatePicker ID="txt_fecha_inicio" Width="275px" runat="server" Culture="es-PE"
                                Skin="Web20">
                                <Calendar UseColumnHeadersAsSelectors="false" UseRowHeadersAsSelectors="false" ViewSelectorText="x">
                                </Calendar>
                            </telerik:RadDatePicker>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <telerik:RadDateTimePicker ID="txt_fecha_fin" runat="server" Culture="es-PE" Skin="Web20"
                                Visible="False">
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
                        <td class="style4">
                            Canal:
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="cmbcanal" runat="server" runat="server" AutoPostBack="True"
                                Enabled="true" Height="25px" Width="275px" OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            Campaña :
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Enabled="False"
                                Height="25px" Width="275px" OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            Mercaderista :
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="cmbperson" runat="server" Enabled="False" Height="25px" Width="275px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            Oficina:
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="cmbOficina" runat="server" Enabled="False" Height="25px" Width="275px"
                                AutoPostBack="true" OnSelectedIndexChanged="cmbOficina_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            Zona :
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="cmbNodeComercial" runat="server" Enabled="False" Height="25px"
                                Width="275px" AutoPostBack="true" OnSelectedIndexChanged="cmbNodeComercial_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            Punto de Venta :
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="cmbPuntoDeVenta" runat="server" Enabled="False" Height="25px"
                                Width="275px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            Categoria de Producto :
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="cmbcategoria_producto" runat="server" AutoPostBack="True" Enabled="False"
                                Height="25px" Width="275px" OnSelectedIndexChanged="cmbcategoria_producto_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            Marca
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="cmbmarca" runat="server" AutoPostBack="True" Enabled="False"
                                Height="25px" Width="275px" OnSelectedIndexChanged="cmbmarca_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            Producto
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="cmbsku" runat="server" AutoPostBack="True" Enabled="False"
                                Height="25px" Width="275px" OnSelectedIndexChanged="cmbsku_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            &nbsp;
                        </td>
                        <td class="style8">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td class="style1">
                            <asp:Button ID="btn_buscar" runat="server" CssClass="buttonSearch" Height="25px"
                                OnClick="btn_buscar_Click" Text="Buscar" Width="164px" />
                               
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            &nbsp;
                        </td>
                        <td class="style8">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            &nbsp;
                        </td>
                        <td class="style8">
                            <asp:Label ID="lblmensaje" runat="server" Style="text-align: left"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            &nbsp;
                        </td>
                        <td class="style8" align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            <div>
                            </div>
                            <div>
                            </div>
                        </td>
                    </tr>
                </table>
                <div align="right">
                    <asp:Label ID="lbl_cb_all" runat="server" Text="Seleccionar todos" Visible="false"></asp:Label>
                    <asp:CheckBox ID="cb_all" Visible="false" runat="server" AutoPostBack="True" OnCheckedChanged="cb_all_CheckedChanged" />
                </div>
                <telerik:RadGrid ID="gv_precios" runat="server" AutoGenerateColumns="False" PageSize="100"
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
                            <telerik:GridDateTimeColumn DataField="Fecha_Fin" UniqueName="Fecha_Fin" HeaderText="Fecha de registro"
                                PickerType="DateTimePicker">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="categoria" HeaderText="Categoria" UniqueName="categoria"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="cadena" HeaderText="Cadena" UniqueName="cadena"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Pdv_name" HeaderText="Punto de Venta" UniqueName="Pdv_name"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="marca" HeaderText="Marca" UniqueName="marca"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UNI/KG" HeaderText="UNI/KG" UniqueName="UNI/KG"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Producto" HeaderText="Producto" UniqueName="Producto"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="id_Product" HeaderText="SKU" UniqueName="id_Product"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="stock_incial" HeaderText="Stock Inicial" UniqueName="stock_incial"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn DataField="Ingresos" DataType="System.Double" EmptyDataText="0"
                                HeaderText="Ingresos" UniqueName="Ingresos">
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn DataField="stock_Final" DataType="System.Double" EmptyDataText="0"
                                HeaderText="Stock Final" UniqueName="stock_Final">
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn DataField="Ventas" HeaderText="Ventas" UniqueName="Ventas"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Person_name" HeaderText="Generador" UniqueName="Person_name"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                <HeaderTemplate>
                                    &nbsp;<asp:Button ID="btn_validar_Click" OnClientClick="return confirm('¿Esta seguro de invalidar los registros?');"
                                        runat="server" Text="Invalidar" OnClick="btn_validar_Click_Click" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb_validar" runat="server" Enabled='<%# Eval("Habilitado")%>' Checked='<%# Eval("validado_fin")%>' />
                                    <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_id_StockDetalle" runat="server" Text='<%# Eval("id_rstkd_fin") %>'
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
                    <asp:GridView ID="gv_precioToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False" AutoGenerateColumns="False"
                        EnableModelValidation="True">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="fecha_fin" HeaderText="fecha de registro" />
                            <asp:BoundField DataField="categoria" HeaderText="categoria" />
                            <asp:BoundField DataField="cadena" HeaderText="cadena" />
                            <asp:BoundField DataField="pdv_name" HeaderText="punto de venta" />
                            <asp:BoundField DataField="marca" HeaderText="marca" />
                            <asp:BoundField DataField="uni/kg" HeaderText="uni/kg" />
                            <asp:BoundField DataField="producto" HeaderText="producto" />
                            <asp:BoundField DataField="id_Product" HeaderText="sku" />
                            <asp:BoundField DataField="stock_incial" HeaderText="Stock inicial" />
                            <asp:BoundField DataField="Ingresos" HeaderText="Ingresos" />
                            <asp:BoundField DataField="stock_Final" HeaderText="Stock final" />
                            <asp:BoundField DataField="ventas" HeaderText="ventas" />
                            <asp:BoundField DataField="person_name" HeaderText="generador" />
                            <asp:CheckBoxField DataField="validado_fin" HeaderText="Validado" />
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
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadAjaxManager runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txt_fecha_inicio">
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style1
        {
            width: 195px;
        }
        .style2
        {
            width: 195px;
            text-align: right;
        }
        .style4
        {
            text-align: right;
        }
    </style>
</asp:Content>
