<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_Exhibicion.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Exhibicion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <table style="width: 100%; height: auto;" align="center">
        <tr>
            <td class="style4" colspan="2">
                <span class="style11">REPORTE DE EXHIBICION</span>
            </td>
        </tr>
        <tr>
            <td class="style13">
                Fecha :
            </td>
            <td class="style9">
                Desde:<telerik:RadDateTimePicker ID="txt_fecha_inicio" runat="server" Culture="es-PE"
                    Skin="Web20" DatePopupButton-ToolTip="Abir el calendario.">
                    <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora">
                    </TimeView>
                    <TimePopupButton HoverImageUrl="" ImageUrl="" />
                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                        Skin="Web20">
                    </Calendar>
                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                </telerik:RadDateTimePicker>
                &nbsp;Hasta :<telerik:RadDateTimePicker ID="txt_fecha_fin" runat="server" Culture="es-PE"
                    Skin="Web20" DatePopupButton-ToolTip="Abir el calendario.">
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
            <td class="style13">
                Canal :
            </td>
            <td class="style9">
                <asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Height="25px"
                    OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="275px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style13">
                Campaña :
            </td>
            <td class="style8">
                <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Height="25px"
                    OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged" Width="275px" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style13">
                Mercaderista :
            </td>
            <td class="style8">
                <asp:DropDownList ID="cmbperson" runat="server" Height="25px" Width="275px" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
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
            <td class="style2">
                <asp:DropDownList ID="cmbNodeComercial" runat="server" Enabled="False" Height="25px"
                    Width="275px" OnSelectedIndexChanged="cmbNodeComercial_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
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
            <td class="style13">
                Categoria del producto :
            </td>
            <td class="style8">
                <asp:DropDownList ID="cmbcategoria_producto" runat="server" AutoPostBack="True" Height="25px"
                    OnSelectedIndexChanged="cmbcategoria_producto_SelectedIndexChanged" Width="275px"
                    Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style13">
                Marca :
            </td>
            <td class="style8">
                <asp:DropDownList ID="cmbmarca" runat="server" Height="25px" Width="275px" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style13">
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
    <div id="div_gvExhibicion" runat="server" style="width: 100%; height: auto;">
        <asp:GridView ID="gvExhib" runat="server" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="True" Width="100%"
            AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvExhib_PageIndexChanging1"
            CssClass="Grid" PageSize="100" OnRowEditing="gv_exhibicion_RowEditing" OnRowCancelingEdit="gv_exhibicion_RowCancelingEdit"
            OnRowUpdating="gv_exhibicion_RowUpdating">
            <%--<RowStyle ForeColor="#000066" />--%>
            <HeaderStyle CssClass="GridHeader" />
            <RowStyle CssClass="GridRow" />
            <AlternatingRowStyle CssClass="GridAlternateRow" />
            <Columns>
                <asp:BoundField DataField="ROWID" HeaderText="N°" ReadOnly="true" />
                <asp:BoundField DataField="Punto de venta" HeaderText="Punto de venta" ReadOnly="true" />
                <asp:BoundField DataField="Categoria" HeaderText="Categoria" ReadOnly="true" />
                <asp:BoundField DataField="Marca" HeaderText="Marca" ReadOnly="true" />
                <asp:BoundField DataField="Fecha_inicio" HeaderText="Fecha inicio" ReadOnly="true" />
                <asp:BoundField DataField="Fecha_Fin" HeaderText="Fecha fin" ReadOnly="true" />
                <asp:BoundField DataField="descripcion" HeaderText="descripcion" ReadOnly="true" />
                <asp:TemplateField>
                    <HeaderTemplate>
                        Cantidad
                    </HeaderTemplate>
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txt_gve_cantidad" runat="server" Enabled="false" ShowSpinButtons="True"
                            IncrementSettings-InterceptMouseWheel="False" NumberFormat-DecimalDigits="0"
                            Height="20px" Skin="Telerik" Width="112px" Culture="es-PE" DbValue='<%#Eval("Cantidad")%>'
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
                <%--<asp:TemplateField HeaderText="Foto">
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_img_foto" runat="server" CommandArgument='<%# Bind("Id_regft") %>'
                            Height="29px" ImageUrl="~/Pages/ImgBooom/Fotografia1.5.png" OnClick="btn_img_foto_Click"
                            Width="67px" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
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
                        <asp:Label ID="lbl_id_ExbDetall" Visible="false" Text='<%# Eval("Id_rexhd")%>' runat="server"></asp:Label>
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
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
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
                <asp:GridView ID="gv_exhibicionToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False" AutoGenerateColumns="False">
                    <RowStyle ForeColor="#000066" />
                    <Columns>
                        <asp:BoundField DataField="ROWID" HeaderText="N°" ReadOnly="true" />
                        <asp:BoundField DataField="commercialNodeName" HeaderText="Zona" ReadOnly="true" />
                        <asp:BoundField DataField="ClientPDV_Code" HeaderText="Código PDV" />
                        <asp:BoundField DataField="Punto de venta" HeaderText="Punto de venta" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" />
                        <asp:BoundField DataField="Fecha_inicio" HeaderText="Fecha inicio" />
                        <asp:BoundField DataField="Fecha_Fin" HeaderText="Fecha fin" />
                       <%-- <asp:BoundField DataField="Comentario" HeaderText="Comentario" /> comentario de la foto --%>
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="descripcion" HeaderText="descripcion" />
                        <asp:BoundField DataField="Person_name" HeaderText="Registrado por" />
                        <asp:BoundField DataField="Fec_Reg_Bd" HeaderText="Fecha de registro" />
                        <asp:BoundField DataField="ModiBy" HeaderText="Modificado por" ReadOnly="true" />
                        <asp:BoundField DataField="DateModiBy" HeaderText="Modifico en" ReadOnly="true" />
                        <asp:CheckBoxField DataField="Validado" HeaderText="Validado" />
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
        .style4
        {
            text-align: center;
            height: 17px;
            font-weight: bold;
            vertical-align: center;
        }
        .style5
        {
            width: 162px;
            text-align: right;
        }
        .style8
        {
            text-align: left;
            width: 598px;
        }
        .style9
        {
            width: 598px;
        }
        .style10
        {
            vertical-align: center;
        }
        .style11
        {
            font-size: medium;
        }
        .style12
        {
            text-align: left;
            width: 170px;
        }
        .style13
        {
            width: 170px;
            text-align: right;
        }   
    </style>
</asp:Content>
