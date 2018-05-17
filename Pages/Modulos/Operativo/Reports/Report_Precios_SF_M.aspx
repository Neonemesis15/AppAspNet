<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_Precios_SF_M.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Precios_SF_M" %>

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
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>
            <div align="center" style="font-family: Verdana; font-size: medium; color: #00579E;
                font-weight: bold">
                REPORTE DE PRECIOS
            </div>
            <table style="width: 100%; height: auto;" align="center" class="art-Block-body">
                <tr>
                    <td class="style2">
                        Fecha :
                    </td>
                    <td class="style6">
                        Desde :<telerik:RadDateTimePicker ID="txt_fecha_inicio" runat="server" Culture="es-PE"
                            Skin="Web20">
                            <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora">
                            </TimeView>
                            <TimePopupButton HoverImageUrl="" ImageUrl="" />
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Web20">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDateTimePicker>
                        &nbsp;Hasta :<telerik:RadDateTimePicker ID="txt_fecha_fin" runat="server" Culture="es-PE"
                            Skin="Web20">
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
                    <td class="style8">
                        <asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="275px" Font-Bold="False"
                            Font-Italic="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Campaña :
                    </td>
                    <td class="style8">
                        <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Enabled="False"
                            Height="25px" OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Cadena :
                    </td>
                    <td class="style8">
                        <asp:DropDownList ID="cmbcorporacion" runat="server" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Ciudad :
                    </td>
                    <td class="style8">
                        <asp:DropDownList ID="cmbOficina" runat="server" AutoPostBack="true" Enabled="False"
                            Height="25px" OnSelectedIndexChanged="cmbOficina_SelectedIndexChanged" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Cliente :
                    </td>
                    <td class="style8">
                        <asp:DropDownList ID="cmbNodeComercial" runat="server" Enabled="False" Height="25px"
                            Width="275px" OnSelectedIndexChanged="cmbNodeComercial_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Punto de venta :
                    </td>
                    <td class="style8">
                        <asp:DropDownList ID="cmbPuntoDeVenta" runat="server" Enabled="False" Height="25px"
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Categoria del producto :
                    </td>
                    <td class="style8">
                        <asp:DropDownList ID="cmbcategoria_producto" runat="server" AutoPostBack="True" CssClass="RadComboBoxDropDown"
                            Enabled="False" Height="25px" OnSelectedIndexChanged="cmbcategoria_producto_SelectedIndexChanged"
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Label ID="lbl_marca" runat="server" Text="Marca : "></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:DropDownList ID="cmbmarca" runat="server" AutoPostBack="True" Enabled="False"
                            Height="25px" OnSelectedIndexChanged="cmbmarca_SelectedIndexChanged" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Supervisor :
                    </td>
                    <td class="style8">
                        <asp:DropDownList ID="cmbperson" runat="server" Enabled="False" Height="25px" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Label ID="Label1" runat="server" Text="Producto :" Visible="False"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:DropDownList ID="cmbsku" runat="server" Enabled="False" Height="25px" Width="275px"
                            Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td class="style8">
                        <div>
                            <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="buttonSearch"
                                Height="25px" Width="164px" OnClick="btn_buscar_Click" />
                        </div>
                        <div>
                            <asp:Label ID="lblmensaje" runat="server" Style="text-align: left"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
            <div align="right">
                <asp:Label ID="lbl_cb_all" runat="server" Text="Seleccionar todos" Visible="false"></asp:Label>
                <asp:CheckBox ID="cb_all" Visible="false" runat="server" AutoPostBack="True" OnCheckedChanged="cb_all_CheckedChanged" />
            </div>
            <div id="div_gvCompetencia" runat="server" class="class_div" style="width: auto;
                height: auto;">
                <table>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gv_precios" runat="server" AutoGenerateColumns="False" PageSize="100"
                                Skin="Vista" Font-Size="Medium" AllowPaging="True" GridLines="None" OnCancelCommand="gv_precios_CancelCommand"
                                OnEditCommand="gv_precios_EditCommand" OnPageIndexChanged="gv_precios_PageIndexChanged"
                                ShowFooter="True" OnPageSizeChanged="gv_precios_PageSizeChanged" OnUpdateCommand="gv_precios_UpdateCommand"
                                OnDataBound="gv_precios_DataBound" Font-Names="Calibri" 
                                Font-Strikeout="False" >

                                <MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E"
                                    AlternatingItemStyle-BackColor="#F7F7F7" Font-Size="Smaller" AlternatingItemStyle-ForeColor="#333333"
                                    TableLayout="Fixed">

                                    <Columns>
                                        <telerik:GridBoundColumn DataField="corporacion" HeaderText="Cadena" UniqueName="corporacion"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="100px"  
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="oficina" HeaderText="Ciudad" UniqueName="oficina"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="nodocomercial" HeaderText="Cliente" UniqueName="nodocomercial"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="codigopdv" HeaderText="Codigo PDV" UniqueName="codigopdv"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="pdv" HeaderText="Punto de Venta" UniqueName="pdv"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="marca" HeaderText="Marca" UniqueName="marca"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="categoria" HeaderText="Categoria" UniqueName="categoria"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="familia" HeaderText="Familia" UniqueName="familia"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="subfamilia" HeaderText="Subfamilia" UniqueName="subfamilia"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="sku" HeaderText="SKU" UniqueName="sku" ReadOnly="true"
                                            Visible="true" Resizable="true" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Producto" HeaderText="Producto" UniqueName="Producto"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="370px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="unidadmedida" HeaderText="Unidad de medida" UniqueName="unidadmedida"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="supervisor" HeaderText="Supervisor" UniqueName="supervisor"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="precioregular" DataType="System.Double" EmptyDataText="NULO"
                                            HeaderText="Precio Regular" UniqueName="precioregular" Resizable="true" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="preciooferta" DataType="System.Double" EmptyDataText="NULO"
                                            HeaderText="Precio Oferta" UniqueName="preciooferta" Resizable="true" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn DataField="variacion" HeaderText="Variación" UniqueName="variacion"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="mercaderista" HeaderText="Mercaderista" UniqueName="mercaderista"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="170px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="fecharegistro" UniqueName="fecharegistro"
                                            HeaderText="Fecha de Registro" PickerType="DateTimePicker" Resizable="true" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="modificadopor" HeaderText="Modificado por:" UniqueName="modificadopor"
                                            ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DateModiBy" HeaderText="Fecha de modificación:"
                                            UniqueName="DateModiBy" ReadOnly="true" Visible="true" Resizable="true" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Button ID="btn_validar" runat="server"  OnClick="btn_validar_Click"
                                                    OnClientClick="return confirm('¿Esta seguro de invalidar los registros?');" Text="Invalidar" />
                                                <br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("Validado")%>' />
                                                <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                                <asp:Label ID="lbl_Id_Detalle_Precio" runat="server" Text='<%# Bind("idregpreciodet") %>'
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                            EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png"
                                            CancelText="Cancelar" UpdateText="Actualizar" HeaderStyle-Width="50px">
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
                            <%--<clientsettings>
                            <scrolling allowscroll="True"
                            usestaticheaders="true"></ClientSettings>--%>
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
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="corporacion" HeaderText="Corporacion" />
                        <asp:BoundField DataField="oficina" HeaderText="Ciudad" />
                        <asp:BoundField DataField="nodocomercial" HeaderText="Cliente" />
                        <asp:BoundField DataField="codigopdv" HeaderText="Codigo de PDV" />
                        <asp:BoundField DataField="pdv" HeaderText="Punto de Venta" />
                        <asp:BoundField DataField="marca" HeaderText="Marca" />
                        <asp:BoundField DataField="categoria" HeaderText="Categoria" />
                        <asp:BoundField DataField="familia" HeaderText="Familia" />
                        <asp:BoundField DataField="subfamilia" HeaderText="Subfamilia" />
                        <asp:BoundField DataField="sku" HeaderText="SKU" />
                        <asp:BoundField DataField="Producto" HeaderText="Producto" />
                        <asp:BoundField DataField="unidadmedida" HeaderText="Unidad de Medida" />
                        <asp:BoundField DataField="supervisor" HeaderText="Supervisor" />
                        <asp:BoundField DataField="precioregular" HeaderText="Precio Regular" />
                        <asp:BoundField DataField="preciooferta" HeaderText="Precio Oferta" />
                        <asp:BoundField DataField="variacion" HeaderText="Variación" />
                        <asp:BoundField DataField="mercaderista" HeaderText="Mercaderista" />
                        <asp:BoundField DataField="fecharegistro" HeaderText="Fecha de Registro:" />
                        <asp:BoundField DataField="modificadopor" HeaderText="Modificado por:" />
                        <asp:BoundField DataField="DateModiBy" HeaderText="Fecha de Modificación:" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<%--<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
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
    </style>
</asp:Content>--%>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style2
        {
            
            text-align: right;
            width: 153px;
        }
        .style5
        {
            text-align: right;
            height: 18px;
            width: 153px;
        }
        .style6
        {
            text-align: left;
            width: 836px;
        }
        .style7
        {
            text-align: left;
            height: 18px;
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
