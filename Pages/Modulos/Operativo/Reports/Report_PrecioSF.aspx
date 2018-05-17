<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_PrecioSF.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_PrecioSF" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc3" %>
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
                REPORTE DE PRECIOS
            </div>
            <table style="width: 100%; height: auto;" align="center" class="art-Block-body">
                <tr>
                    <td class="style2">
                        Fecha :
                    </td>
                    <td class="style8">
                        Desde:<telerik:RadDateTimePicker ID="txt_fecha_inicio" runat="server" Culture="es-PE"
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
                        &nbsp;Supervisores :
                    </td>
                    <td class="style7">
                        <asp:DropDownList ID="cmbSupervisor" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbSupervisor_SelectedIndexChanged" Width="275px" CausesValidation="True"
                            Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Mercaderista :
                    </td>
                    <td class="style8">
                        <%--<asp:DropDownList ID="cmbperson" runat="server" Enabled="False" Height="25px" Width="275px">
                        </asp:DropDownList>--%>
                        <cc3:DropDownCheckBoxes ID="cmbperson" runat="server" AddJQueryReference="true" UseButtons="true"
                            UseSelectAllNode="true" Style="top: 0px; left: 315px; height: 16px; width: 241px"
                            OnSelectedIndexChanged="cmbperson_SelectedIndexChanged">
                            <%--  <Style SelectBoxWidth="160" DropDownBoxBoxWidth="160" DropDownBoxBoxHeight="115" />--%>
                            <Texts SelectBoxCaption="-----Gestores de informarción y exhibición -----" />
                        </cc3:DropDownCheckBoxes>
                        <div>
                            <asp:Panel ID="Panel_mercaderistas" runat="server">
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Oficina :
                    </td>
                    <td class="style8">
                        <asp:DropDownList ID="cmbOficina" runat="server" Enabled="False" Height="25px" Width="275px"
                            AutoPostBack="true" OnSelectedIndexChanged="cmbOficina_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        zonas :
                    </td>
                    <td class="style7">
                        <asp:DropDownList ID="cmbSector" runat="server" Enabled="False" Height="25px" Width="275px"
                            OnSelectedIndexChanged="cmbSector_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Mercados :
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
                        Producto :
                    </td>
                    <td class="style8">
                        <asp:DropDownList ID="cmbsku" runat="server" Enabled="False" Height="25px" Width="275px">
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
            <div class="class_div" >
                <div align="right">
                    <asp:Label ID="lbl_cb_all" runat="server" Text="Seleccionar todos" Visible="false"></asp:Label>
                    <asp:CheckBox ID="cb_all" Visible="false" runat="server" AutoPostBack="True" OnCheckedChanged="cb_all_CheckedChanged" />
                </div>
                <telerik:RadGrid ID="gv_precios" runat="server" AutoGenerateColumns="False" PageSize="100"
                    Skin="Vista" Font-Size="Small" AllowPaging="True" GridLines="None" OnCancelCommand="gv_precios_CancelCommand"
                    OnEditCommand="gv_precios_EditCommand" OnPageIndexChanged="gv_precios_PageIndexChanged"
                    ShowFooter="True" OnPageSizeChanged="gv_precios_PageSizeChanged" OnUpdateCommand="gv_precios_UpdateCommand"
                    OnDataBound="gv_precios_DataBound"  >
                    <MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E"
                        AlternatingItemStyle-BackColor="#F7F7F7" Font-Size="Smaller" AlternatingItemStyle-ForeColor="#333333">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Person_Name" HeaderText="Generador" UniqueName="Person_Name"
                                ReadOnly="true" Visible="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Punto de Venta" HeaderText="Nombre del Cliente"
                                UniqueName="Punto de Venta" ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Nivel_Cliente" HeaderText="Nivel" UniqueName="Nivel_Cliente"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="commercialNodeName" HeaderText="Nombre del Mercado"
                                UniqueName="commercialNodeName" ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Direccion" HeaderText="Direccion" UniqueName="Direccion"
                                ReadOnly="true" >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Distribuidora" HeaderText="Distribuidora" UniqueName="Distribuidora"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Frecuencia" HeaderText="Frecuencia" UniqueName="Frecuencia"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="malla" HeaderText="Zona" UniqueName="malla" ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Frecuencia" HeaderText="Frecuencia" UniqueName="Frecuencia"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Supervisor" HeaderText="Supervisor" UniqueName="Supervisor"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ClientPDV_Code" HeaderText="Cod Pdv" UniqueName="ClientPDV_Code"
                                ReadOnly="true" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Categoria" HeaderText="Categoria" UniqueName="Categoria"
                                ReadOnly="true" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Marca" HeaderText="Marca" UniqueName="Marca"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Producto" HeaderText="Producto" UniqueName="Producto"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SKU" HeaderText="SKU" UniqueName="Sku" ReadOnly="true"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn DataField="PRECIO_MIN" DataType="System.Double" EmptyDataText="0"
                                HeaderText="Precio Min" DataFormatString="{0:C}" UniqueName="PRECIO_MIN">
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn DataField="PRECIO_MAX" DataType="System.Double" EmptyDataText="0"
                                HeaderText="Precio Max" DataFormatString="{0:C}" UniqueName="PRECIO_MAX">
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn DataField="Obervacion" HeaderText="Obervaciones" UniqueName="Obervacion"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="Fec_Reg_Bd" UniqueName="Fec_Reg_Bd" HeaderText="Fecha de registro"
                                PickerType="DateTimePicker">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="Person_name" HeaderText="Registrado por" UniqueName="Person_name"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModiBy" HeaderText="Modificado por" UniqueName="ModiBy"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DateModiBy" HeaderText="Modificó en" UniqueName="DateModiBy"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn DataField="Validado" HeaderText="Validado" UniqueName="TemplateColumn">
                                <HeaderTemplate>
                                    <asp:Button ID="btn_validar" runat="server" OnClick="btn_validar_Click" OnClientClick="return confirm('¿Esta seguro de invalidar los registros?');"
                                        Text="Invalidar" />
                                    <br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("Validado")%>' />
                                    <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_Id_Detalle_Precio" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
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
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False">
                    <RowStyle ForeColor="#000066" />
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
         .class_div 
        {
        	 overflow-x: scroll; 
        	 background-color:white;  	 
        }
    </style>
</asp:Content>
