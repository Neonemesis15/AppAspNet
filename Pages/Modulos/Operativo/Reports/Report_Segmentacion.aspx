<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_Segmentacion.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Segmentacion"
    Culture="auto" UICulture="auto" %>

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
    <%--    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </cc1:ToolkitScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>
            <table style="width: 100%; height: auto;" align="center" class="style1">
                <tr>
                    <td class="style11" colspan="2">
                        &nbsp; REPORTE SEGMENTACION
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        Fecha :
                    </td>
                    <td class="style5">
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
                    <td class="style8">
                        Canal :
                    </td>
                    <td class="style5">
                        <asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        Campaña :
                    </td>
                    <td class="style5">
                        <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged" Width="275px" Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        Mercaderista :
                    </td>
                    <td class="style5">
                        <asp:DropDownList ID="cmbperson" runat="server" Height="25px" Width="275px" Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        Oficina :
                    </td>
                    <td class="style5">
                        <asp:DropDownList ID="cmbOficina" runat="server" Enabled="False" Height="25px" Width="275px"
                            AutoPostBack="true" OnSelectedIndexChanged="cmbOficina_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        Zona :
                    </td>
                    <td class="style5">
                        <asp:DropDownList ID="cmbNodeComercial" runat="server" Enabled="False" Height="25px"
                            Width="275px" OnSelectedIndexChanged="cmbNodeComercial_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        Punto de venta :
                    </td>
                    <td class="style5">
                        <asp:DropDownList ID="cmbPuntoDeVenta" runat="server" Enabled="False" Height="25px"
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        Categoria del producto :
                    </td>
                    <td class="style5">
                        <asp:DropDownList ID="cmbcategoria_producto" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbcategoria_producto_SelectedIndexChanged" Width="275px"
                            Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        Marca :
                    </td>
                    <td class="style5">
                        <asp:DropDownList ID="cmbmarca" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbmarca_SelectedIndexChanged" Width="275px" Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        SKU:
                    </td>
                    <td class="style5">
                        <asp:DropDownList ID="cmbsku" runat="server" Height="25px" Width="275px" Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        &nbsp;
                    </td>
                    <td class="style5">
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
            <div id="div_gvCompetencia" runat="server" style="width: 100%; height: auto;">
                <asp:GridView ID="gv_quibre" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderWidth="1px" CellPadding="3" Height="95px" Style="margin-top: 0px" Width="100%"
                    BorderStyle="None" OnPageIndexChanging="gv_quibre_PageIndexChanging" AllowPaging="True"
                    AutoGenerateColumns="False" PageSize="100" OnRowCancelingEdit="gv_quiebre_RowCancelingEdit"
                    OnRowUpdating="gv_quiebre_RowUpdating" OnRowEditing="gv_quiebre_RowEditing">
                    <HeaderStyle CssClass="GridHeader" />
                    <RowStyle CssClass="GridRow" />
                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                    <Columns>
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
                        <asp:BoundField DataField="Person_name" HeaderText="Promotor" ReadOnly="true" />
                        <asp:BoundField DataField="DateModiBy" HeaderText="Modifico en" ReadOnly="true" />
                        <asp:BoundField DataField="ModiBy" HeaderText="Modificado por" ReadOnly="true" />
                        <asp:BoundField DataField="commercialNodeName" HeaderText="Zona" ReadOnly="true" />
                        <asp:BoundField DataField="Punto de venta" HeaderText="Punto de venta" ReadOnly="true" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" ReadOnly="true" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" ReadOnly="true" />
                        <asp:BoundField DataField="Producto" HeaderText="Producto" ReadOnly="true" />
                        <asp:BoundField DataField="Quiebre" HeaderText="Item" ReadOnly="true" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" ReadOnly="true" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Button ID="btn_validar" runat="server" OnClick="btn_validar_Click" Text="Invalidar"
                                    OnClientClick="return confirm('¿Esta seguro de invalidar los registros?');" />
                                <br />
                                <asp:CheckBox ID="cb_all" runat="server" AutoPostBack="True" OnCheckedChanged="cb_all_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("Validado")%>'  /><%--Enabled='<%#!(Convert.ToBoolean(Eval("Validado")))%>'--%>
                                <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                <asp:Label ID="lbl_Id_Quiebre_Detall" runat="server" Visible="false" Text='<%# Eval("Id_reqd")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" EditImageUrl="~/Pages/images/edit_icon.gif"
                            ButtonType="Image" CancelImageUrl="~/Pages/images/cancel_edit_icon.png" UpdateImageUrl="~/Pages/images/save_icon.png" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
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
                <asp:GridView ID="gv_quiebreToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False" AutoGenerateColumns="false">
                    <RowStyle ForeColor="#000066" />
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Person_name" HeaderText="Promotor" />
                        <asp:BoundField DataField="Fec_Reg_Bd" HeaderText="Fecha de registro" />
                        <asp:BoundField DataField="ModiBy" HeaderText="Modificado por" />
                        <asp:BoundField DataField="DateModiBy" HeaderText="Modifico en" />
                        <asp:BoundField DataField="commercialNodeName" HeaderText="Zona" />
                        <asp:BoundField DataField="Punto de venta" HeaderText="Punto de venta" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" />
                        <asp:BoundField DataField="Producto" HeaderText="Producto" />
                        <asp:BoundField DataField="Quiebre" HeaderText="Item" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                        <asp:CheckBoxField DataField="Validado" HeaderText="Validado" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style5
        {
            text-align: left;
            width: 533px;
        }
        .style8
        {
            text-align: right;
            width: 154px;
        }
        .style10
        {
            height: 18px;
        }
        .style11
        {
            text-align: center;
            font-weight: bold;
            font-size: medium;
            vertical-align: center;
        }
    </style>
</asp:Content>
