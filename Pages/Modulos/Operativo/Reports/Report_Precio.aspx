<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_Precio.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Precio" %>

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
    <%--   <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>
            <div style="width: 800px; margin: auto">
                <div class="style3" style="margin: auto">
                    REPORTE DE PRECIOS</div>
                <fieldset style="width: 800px">
                    <legend>Búsqueda de Reporte de Precios</legend>
                    <div class="centrarcontenido" style="margin: auto; align-content: center; border: 1px; width: 630px;">
                    <fieldset style="width: 600px">
                    <legend>Fecha</legend>
                    <table style="width: 580px; height: auto; margin:auto;">
                    <tr>
                    <td class="style17">Fecha de Inicio:</td>
                    <td><telerik:RadDateTimePicker ID="txt_fecha_inicio" runat="server" Culture="es-PE" Skin="Web20">
                            <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora"></TimeView> <TimePopupButton HoverImageUrl="" ImageUrl="" />
                            <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"></Calendar><DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDateTimePicker>
                    </td>
                    <td class="style17">Fecha de Fin:</td>
                    <td><telerik:RadDateTimePicker ID="txt_fecha_fin" runat="server" Culture="es-PE" Skin="Web20">
                            <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora"></TimeView><TimePopupButton HoverImageUrl="" ImageUrl="" />
                            <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"></Calendar><DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDateTimePicker>
                    </td>
                    </tr>
                    </table>
                    </fieldset>
                    
                    <fieldset style="width: 600px">
                    <legend>Cliente</legend>
                    <table style="width: 580px; height: auto; margin:auto;">
                    <tr>
                    <td class="style17">Canal :</td>
                        <td class="style12"><asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Font-Bold="False" Font-Italic="False" Height="25px" OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="150px"></asp:DropDownList></td>
                    <td class="style17">Campaña :</td>
                        <td class="style12"><asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Enabled="False" Height="25px" OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged" Width="150px"></asp:DropDownList></td>
                    </tr>
                    </table>
                    </fieldset>

                    <fieldset style="width: 600px">
                    <legend>Mercaderista</legend>
                    <table style="width: 580px; height: auto; margin:auto;">
                    <tr>
                    <td class="style17">Mercaderista :</td>
                        <td class="style12"><asp:DropDownList ID="cmbperson" runat="server" Enabled="False" Height="25px" Width="150px"></asp:DropDownList></td>
                    </tr>
                    </table>
                    </fieldset>

                    <fieldset style="width: 600px">
                    <legend>Ubigeo</legend>
                    <table style="width: 580px; height: auto; margin:auto;">
                    <tr>
                    <td class="style17">Oficina :</td>
                        <td class="style12"><asp:DropDownList ID="cmbOficina" runat="server" AutoPostBack="true" Enabled="False" Height="25px" OnSelectedIndexChanged="cmbOficina_SelectedIndexChanged" Width="150px"></asp:DropDownList></td>
                    <td class="style17">Zona :</td>
                        <td class="style12"><asp:DropDownList ID="cmbNodeComercial" runat="server" AutoPostBack="true" Enabled="False" Height="25px" OnSelectedIndexChanged="cmbNodeComercial_SelectedIndexChanged" Width="150px"></asp:DropDownList></td>
                    </tr>
                    <tr>
                    <td class="style17">Punto de venta :</td>
                        <td class="style12"><asp:DropDownList ID="cmbPuntoDeVenta" runat="server" Enabled="False" Height="25px" Width="150px"></asp:DropDownList></td>
                    </tr>
                    </table>
                    </fieldset>

                    <fieldset style="width: 600px">
                    <legend>Item</legend>
                    <table style="width: 580px; height: auto; margin:auto;">
                    <tr>
                        <td class="style17">Categoria :</td>
                        <td class="style12"><asp:DropDownList ID="cmbcategoria_producto" runat="server" AutoPostBack="True" CssClass="RadComboBoxDropDown" Enabled="False" Height="25px" OnSelectedIndexChanged="cmbcategoria_producto_SelectedIndexChanged" Width="150px"></asp:DropDownList></td>
                        <td class="style17">Subcategoria :</td>
                        <td class="style12"><asp:DropDownList ID="cmbsubcategoria" runat="server" AutoPostBack="True" Enabled="False" Height="25px" Width="150px"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="style17"><asp:Label ID="lbl_marca" runat="server" Text="Marca : "></asp:Label></td>
                            <td class="style12"><asp:DropDownList ID="cmbmarca" runat="server" AutoPostBack="True" Enabled="False" Height="25px" OnSelectedIndexChanged="cmbmarca_SelectedIndexChanged" Width="150px"></asp:DropDownList></td>
                        <td class="style17">SKU :</td>
                            <td class="style12"><asp:DropDownList ID="cmbsku" runat="server" Enabled="False" Height="25px" Width="150px"></asp:DropDownList></td>
                    </tr>
                    </table>
                    </fieldset>
                    </div>
                    <%--<table style="width: 580px; height: auto; margin: auto;">--%>
                    <div class="centrarcontenido" style="margin-bottom:10px; margin-top:10px">
                        
                            <p>
                                <asp:Button ID="BtnCrear" runat="server" CssClass="buttonGreen" Height="25px" OnClick="BtnCrear_Click" Text="Crear" Width="100px" />
                                <asp:Button ID="BtnCrearMasivo" runat="server" CssClass="buttonGreen" Height="25px" Text="Carga Masiva" Width="100px" />
                                <asp:Button ID="btn_buscar" runat="server" CssClass="buttonBlue" Height="25px" OnClick="btn_buscar_Click" Text="Buscar" Width="100px" />
                                <asp:Button ID="btn_exportarexcel" runat="server" CssClass="buttonGreen" Height="25px" OnClick="btn_exportarexcel_Click" Text="Exportar a Excel" Width="100px" />
                            </p>
                            <p>
                                <asp:Label ID="lblmensaje" runat="server" Style="text-align: left"></asp:Label>
                            </p>
                        
                    </div>
                    <%--</table>--%>
                    <div id="controlesvalidez" runat="server" class="centrarcontenido" style="margin-bottom:10px; margin-top:10px">
                        <fieldset style="width: 580px; margin: auto">
                            <legend>Controles de validez de datos</legend>
                            <p>
                                <asp:Button ID="btn_verificar" runat="server" CssClass="botonBlue" Height="25px" OnClick="btn_verificar_Click" Text="Precios Cero" Width="164px" Enabled="false" />
                                <asp:Button ID="btn_verfecha" runat="server" CssClass="botonBlue" Height="25px" Text="Fuera de Período" Width="164px" OnClick="btn_verfecha_Click" />
                                <asp:Button ID="btn_verfaltante" runat="server" CssClass="botonBlue" Height="25px" Text="Faltantes" Width="164px" onclick="btn_verfaltante_Click" />
                            </p>
                            <p>
                                <asp:Label ID="lbl_mensaje_verifica" runat="server" Style="text-align: left"></asp:Label>
                            </p>
                        </fieldset>
                    </div>
                </fieldset>
            </div>
                <div id="div_gvPrecios" runat="server" style="margin:auto; width: 100%; height: auto;" >
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="3" BorderStyle="None" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" AutoGenerateColumns="False" AllowSorting="True" OnSorting="GridView1_Sorting" Width="100%" PageSize="100" OnRowEditing="gv_precios_RowEditing" OnRowCancelingEdit="gv_precios_RowCancelingEdit" OnRowUpdating="gv_precios_RowUpdating" EnableModelValidation="True">
                        <HeaderStyle CssClass="GridHeader" />
                        <RowStyle CssClass="GridRow" />
                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                        <Columns>
                            <asp:BoundField DataField="ROWID" HeaderText="N°" ReadOnly="true" />
                            <asp:BoundField DataField="Oficina" HeaderText="Oficina" ReadOnly="true" />
                            <asp:BoundField DataField="commercialNodeName" HeaderText="Zona" ReadOnly="true" Visible="false" />
                            <asp:BoundField DataField="ClientPDV_Code" HeaderText="Código PDV" ReadOnly="true" />
                            <asp:BoundField DataField="Punto de Venta" HeaderText="Punto de Venta" ReadOnly="true" />
                            <asp:BoundField DataField="Categoria" HeaderText="Categoria" ReadOnly="true" />
                            <asp:BoundField DataField="SubCategoria" HeaderText="SubCategoria" ReadOnly="true" />
                            <asp:BoundField DataField="Marca" HeaderText="Marca" ReadOnly="true" />
                            <asp:BoundField DataField="Producto" HeaderText="Producto" ReadOnly="true" />
                            <asp:BoundField DataField="SKU" HeaderText="SKU" ReadOnly="true" />
                            <%--Canal Autoservicio--%>
                            <asp:TemplateField Visible="false">
                                <HeaderTemplate> Precio PDV </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_gvp_precio_pdv" runat="server" Text='<%# Eval("Precio punto de venta")%>' Visible="true"></asp:Label>
                                    <telerik:RadNumericTextBox ID="txt_gvp_precio_pdv" runat="server" Visible="false" ShowSpinButtons="True" NumberFormat-DecimalDigits="2" Height="20px" Skin="Telerik" Width="112px" Culture="es-PE" MinValue="0" ToolTip="Precio de PDV" EmptyMessage="vacio"> </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <HeaderTemplate>Precio de oferta</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_gvp_precio_oferta" runat="server" Text='<%# Eval("Precio de oferta")%>' Visible="true"></asp:Label>
                                    <telerik:RadNumericTextBox ID="txt_gvp_precio_oferta" runat="server" Visible="false" ShowSpinButtons="True" NumberFormat-DecimalDigits="2" Height="20px" Skin="Telerik" Width="112px" Culture="es-PE" MinValue="0" ToolTip="Precio Oferta" EmptyMessage="vacio"> </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%---Canal Mayorista---%>
                            <asp:TemplateField Visible="false">
                                <HeaderTemplate>Precio lista</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_gvp_precio_lista" runat="server" Text='<%# Eval("Precio lista")%>' Visible="true"></asp:Label>
                                    <telerik:RadNumericTextBox ID="txt_gvp_precio_lista" runat="server" Visible="false" NumberFormat-DecimalDigits="2" Height="20px" Skin="Telerik" Width="112px" Culture="es-PE" ShowSpinButtons="true" MinValue="0" ToolTip="Precio de lista" EmptyMessage="vacio"> </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <HeaderTemplate> Precio reventa </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_gvp_precio_reventa" runat="server" Text='<%# Eval("Precio reventa")%>' Visible="true"></asp:Label>
                                    <telerik:RadNumericTextBox ID="txt_gvp_precio_reventa" runat="server" Visible="false" NumberFormat-DecimalDigits="2" Height="20px" Skin="Telerik" Width="112px" Culture="es-PE" ShowSpinButtons="true" MinValue="0" ToolTip="Precio reventa" EmptyMessage="vacio"></telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--Canal Minorista----%>
                            <asp:TemplateField Visible="false">
                                <HeaderTemplate> Precio costo </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_gvp_precio_costo" runat="server" Text='<%# Eval("Precio costo")%>' Visible="true"></asp:Label>
                                    <telerik:RadNumericTextBox ID="txt_gvp_precio_costo" runat="server" Visible="false" ShowSpinButtons="True" NumberFormat-DecimalDigits="2" Height="20px" Skin="Telerik" Width="112px" Culture="es-PE" MinValue="0" ToolTip="Precio costo" EmptyMessage="vacio"></telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%---------------------%>
                            <asp:BoundField DataField="Obervacion" HeaderText="Observación" ReadOnly="true" />
                            <asp:BoundField DataField="Person_name" HeaderText="Registrado por" ReadOnly="true" />
                            <asp:TemplateField>
                                <HeaderTemplate> Fecha de registro </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_fec_Reg" runat="server" Text='<%# Eval("Fec_Reg_Bd")%>' Visible="true"></asp:Label>
                                    <telerik:RadDateTimePicker ID="RadDateTimePicker_fec_reg" runat="server" Visible="False" DateInput-EmptyMessage="Fecha" TimePopupButton-ToolTip="Mostrar hora." DatePopupButton-ToolTip="Mostrar fecha." TimeView-Culture="es-PE" TimeView-Interval="00:20:00" Culture="es-PE" Skin="Outlook" EnableTyping="false">
                                        <TimeView CellSpacing="-1" Culture="es-PE" Interval="00:20:00" HeaderText="Hora"> </TimeView>
                                        <TimePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Mostrar hora." />
                                        <Calendar Skin="Outlook" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
                                        <DateInput EmptyMessage="Fecha"> </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Mostrar fecha." />
                                    </telerik:RadDateTimePicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ModiBy" HeaderText="Modificado por" ReadOnly="true" />
                            <asp:BoundField DataField="DateModiBy" HeaderText="Modifico en" ReadOnly="true" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Button ID="btn_validar" runat="server" OnClick="btn_validar_Click" Text="Invalidar" OnClientClick="return confirm('¿Esta seguro de invalidar los registros?');" />
                                    <br />
                                    <asp:CheckBox ID="cb_all" runat="server" AutoPostBack="True" OnCheckedChanged="cb_all_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("Validado")%>' />
                                    <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_Id_Detalle_Precio" runat="server" Visible="false" Text='<%# Bind("Id") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" EditImageUrl="~/Pages/images/edit_icon.gif" ButtonType="Image" CancelImageUrl="~/Pages/images/cancel_edit_icon.png" UpdateImageUrl="~/Pages/images/save_icon.png" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" ForeColor="White" Font-Bold="True" />
                    </asp:GridView>
                    
                    <asp:GridView ID="dgv_faltantes" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="3" BorderStyle="None" AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" Visible="false" CssClass="centrar">
                        <HeaderStyle CssClass="GridHeader" />
                        <RowStyle CssClass="GridRow" />
                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                        <Columns>
                            <asp:BoundField DataField="ROWID" HeaderText="N°" ReadOnly="true" />
                            <asp:BoundField DataField="Name_Oficina" HeaderText="Oficina" ReadOnly="true" />
                            <asp:BoundField DataField="ClientPDV_Code" HeaderText="Código PDV" ReadOnly="true" />
                            <asp:BoundField DataField="pdv_Name" HeaderText="Punto de Venta" ReadOnly="true" />
                            <asp:BoundField DataField="Product_Category" HeaderText="Categoria" ReadOnly="true" />
                            <asp:BoundField DataField="Name_Subcategory" HeaderText="SubCategoria" ReadOnly="true" />
                            <asp:BoundField DataField="Name_Brand" HeaderText="Marca" ReadOnly="true" />
                            <asp:BoundField DataField="Product_Name" HeaderText="Producto" ReadOnly="true" />
                            <asp:BoundField DataField="cod_Product" HeaderText="SKU" ReadOnly="true" />
                            <asp:BoundField DataField="Person_Name" HeaderText="Mercaderista" ReadOnly="true" />
                            <asp:BoundField DataField="Periodo" HeaderText="Periodo" ReadOnly="true" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" ForeColor="White" Font-Bold="True" />
                    </asp:GridView>                    
                </div>
            
            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3" AssociatedUpdatePanelID="UpdatePanel_contenido" BackgroundCssClass="modalProgressGreyBackground">
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

            <asp:Panel ID="CrearReporPrecio" runat="server" CssClass="busqueda" DefaultButton="btnGuardarReportPrecio"
                Height="400px" Width="700px" >
                <table align="center">
                    <caption>
                        <br />
                        <tr>
                            <td>
                                <asp:Label ID="lblBGR" runat="server" CssClass="labelsTit2" Text="Crear Reporte Precio" />
                            </td>
                        </tr>
                    </caption>
                </table>
                <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" CssClass="labels" Text="Canal :" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCanal" runat="server" Width="205px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlCanal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" CssClass="labels" Text="Producto:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProducto" runat="server" Width="205px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblCanal" runat="server" CssClass="labels" Text="Campaña :" />
                        </td>
                        <td>
                            <asp:DropDownList runat="server" AutoPostBack="True" Width="205px" ID="ddlCampana"
                                OnSelectedIndexChanged="ddlCampana_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="lblPrecioLista" Visible="false" runat="server" CssClass="labels" Text="Precio Lista:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrecioListar" Visible="false" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label19" runat="server" CssClass="labels" Text="Mercaderista :" />
                        </td>
                        <td>
                            <asp:DropDownList runat="server" Width="205px" ID="ddlMercaderista">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="lblPrecioReventa" Visible="false" runat="server" CssClass="labels"
                                Text="Precio Reventa:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrecioReventa" Visible="false" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" CssClass="labels" Text="Zona:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNodeComercial" runat="server" Width="205px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlNodeComercial_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="lblPrecioOferta" Visible="false" runat="server" CssClass="labels"
                                Text="Precio Oferta:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrecioOferta" Visible="false" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" CssClass="labels" Text="Punto de venta:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPuntoVenta" runat="server" Width="205px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="lblPVP" runat="server" Visible="false" CssClass="labels" Text="PVP:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPVP" runat="server" Visible="false" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="labels" Text="Categoria:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCategoria" runat="server" Width="205px" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="lblPrecioCosto" runat="server" Visible="false" CssClass="labels" Text="Precio Costo:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrecioCosto" Visible="false" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="labels" Text="Marca:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMarca" runat="server" Width="205px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td style="display: none;">
                            <asp:Label ID="Label15" runat="server" CssClass="labels" Text="Precio Min:" />
                        </td>
                        <td style="display: none;">
                            <asp:TextBox ID="txtPrecioMin" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            <asp:Label ID="Label3" runat="server" CssClass="labels" Text="Familia:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFamilia" runat="server" Width="205px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlFamilia_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td style="display: none;">
                            <asp:Label ID="Label16" runat="server" CssClass="labels" Text="Precio Max:" />
                        </td>
                        <td style="display: none;">
                            <asp:TextBox ID="txtPrecioMax" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            <asp:Label ID="Label4" runat="server" CssClass="labels" Text="SubFamilia:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubfamilia" runat="server" Width="205px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td style="display: none;">
                            <asp:Label ID="Label17" runat="server" CssClass="labels" Text="Precio Regular:" />
                        </td>
                        <td style="display: none;">
                            <asp:TextBox ID="txtPrecioRegular" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="labels" Text="Observacion:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtObservacion" runat="server" Width="205px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td style="display: none;">
                            <asp:Label ID="Label18" runat="server" CssClass="labels" Text="Motivo:" />
                        </td>
                        <td style="display: none;">
                            <asp:TextBox ID="txtMotivo" runat="server" Width="205px" MaxLength="1"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" CssClass="labels" Text="Fecha de registro:" />
                        </td>
                        <td>
                            <telerik:RadDateTimePicker ID="rdtpFechaRegistro" runat="server" Culture="es-PE" Skin="Web20">
                                    <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora">
                                    </TimeView>
                                    <TimePopupButton HoverImageUrl="" ImageUrl="" />
                                    <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDateTimePicker>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td style="display: none;">
                            <asp:Label ID="Label8" runat="server" CssClass="labels" Text="Motivo:" />
                        </td>
                        <td style="display: none;">
                            <asp:TextBox ID="TextBox2" runat="server" Width="205px" MaxLength="1"></asp:TextBox>
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
                                <asp:Button ID="btnGuardarReportPrecio" runat="server" CssClass="buttonPlan" Text="Guardar"
                                    Width="80px" OnClick="btnGuardarReportPrecio_Click" />
                                <asp:Button ID="btnCancelar" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" />
                            </td>
                        </tr>
                    </caption>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="MopoReporPrecio" runat="server" BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" OkControlID="btnCancelar" PopupControlID="CrearReporPrecio" TargetControlID="BtnCrear" DynamicServicePath=""> </cc1:ModalPopupExtender>
                <asp:Panel ID="CrearReporPrecioMasiva" runat="server" CssClass="busqueda" DefaultButton="btnGuardarReportPrecio" Height="200px" Width="700px" >
                <table align="center">
                    <caption>
                        <br />
                        <tr>
                            <td><asp:Label ID="Label9" runat="server" CssClass="labelsTit2" Text="Crear Reporte Precio" /></td>
                        </tr>
                    </caption>
                </table>
                <table align="center">
                    <tr>
                        <td><asp:Label ID="Label10" runat="server" CssClass="labels" Text="Canal :" /></td>
                        <td>
                            <asp:DropDownList ID="ddlCanalCargaMasiva" runat="server" Width="205px" AutoPostBack="True" onselectedindexchanged="ddlCanalCargaMasiva_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td style="width: 40px;"></td>
                        <td><asp:Label ID="Label11" runat="server" CssClass="labels" Text="Archivo:" /></td>
                        <td><asp:FileUpload ID="FileUpCMasivaPrecio" runat="server"  /></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label14" runat="server" CssClass="labels" Text="Campaña :" /></td>
                        <td><asp:DropDownList runat="server"  Width="205px" ID="ddlCampañaCargaMasiva" AutoPostBack="true" onselectedindexchanged="ddlCampañaCargaMasiva_SelectedIndexChanged"></asp:DropDownList></td>
                        <td style="width: 40px;"></td>
                        <td></td>
                        <td>
                           <a class="button" href="../../../formatos/Formato_Masivo_Reporte_Precio.xls"><span>Descargar Formato</span></a>
                           <a runat="server" visible="false" id="Datos" class="button" href="~/Pages/Modulos/Operativo/Reports/masivo/DATOS_CARGA_REPORTE_PRECIO.xls"><span>Descargar Datos</span></a>
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
                                <asp:Button ID="btnCargaMasiva" runat="server" CssClass="buttonPlan" Text="Cargar" Width="80px" onclick="btnCargaMasiva_Click"  />
                                <asp:Button ID="btnCancelarCargaMasiva" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="80px" />
                            </td>
                        </tr>
                    </caption>
                </table>
            </asp:Panel>

            <cc1:ModalPopupExtender ID="MopoReporPrecioMasiva" runat="server" BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" OkControlID="btnCancelarCargaMasiva" PopupControlID="CrearReporPrecioMasiva" TargetControlID="BtnCrearMasivo" DynamicServicePath=""> </cc1:ModalPopupExtender>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_exportarexcel" />
            <asp:PostBackTrigger ControlID="btnCargaMasiva" />            
        </Triggers>
    </asp:UpdatePanel>
    <%--  <div style=" text-align: center;">
    <asp:ImageButton ID="btn_img_exporttoexcel" runat="server" Height="42px" ImageUrl="~/Pages/Modulos/Operativo/Reports/Image/icono_excel.jpg"
                    onclick="btn_img_exporttoexcel_Click" Width="39px"/>

    </div>
    <div style=" text-align: center;">
                Exportar a excel
    </div>--%>
    <asp:GridView ID="gv_precioToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False">
        <RowStyle ForeColor="#000066" />
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style3
        {
            text-align: center;
            font-weight: bold;
            font-size: medium;
            vertical-align: middle;
        }
        .style6
        {
            width: 207px;
        }
        .style8
        {
        }
        .class_div
        {
            overflow: scroll;
            background-color: white;
        }
        .style12
        {
            width: 160px;
        }
        .style14
        {
            width: 116px;
        }
        .style16
        {
            width: 249px;
        }
        .style17
        {
            text-align: right;
            width: 200px;
        }
        fieldset, legend  
        {
            margin:0;
            padding-top: 0;
            padding-bottom:0;
        }
    </style>
</asp:Content>
