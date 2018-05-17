<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
 AutoEventWireup="true" CodeBehind="Reporte_ElementoVisibilidad.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Reporte_ElementoVisibilidad.Reporte_ElementoVisibilidad" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%--Referencias al usrcontrol--%>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Src="../MasterPage/DefaultHeader.ascx" TagName="DefaultHeader" TagPrefix="uc1" %>
<%@ Register Src="../MasterPage/DefaultMenu.ascx" TagName="DefaultMenu" TagPrefix="uc1" %>
<%@ Register Src="../MasterPage/DefaultSidebar2.ascx" TagName="DefaultSidebar2" TagPrefix="uc1" %>
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
        <div style="text-align: center; font-weight: 700; font-size: large">
        ELEMENTOS DE VISIBILIDAD
        </div>
        <asp:UpdatePanel ID="up_filtros" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <table class="style1">
                    <tr>
                        <td>
                            Canal:
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbcanal" runat="server" OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged"
                                AutoPostBack="True" Height="25px" Width="275px" Style="text-align: left">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Campaña:
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbplanning" runat="server" OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged"
                                AutoPostBack="True" Enabled="False" Height="25px" Width="275px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Oficina:
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbOficina" runat="server" Enabled="False" Height="25px" Width="275px"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cadena:
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbNodeComercial" runat="server" Enabled="False" Height="25px"
                                Width="275px" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fecha:
                        </td>
                        <td>
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
                            Hasta :<telerik:RadDateTimePicker ID="txt_fecha_fin" runat="server" Culture="es-PE"
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
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div align="center">
            <asp:Button ID="btn_buscar" runat="server" CssClass="buttonRed" Height="25px" OnClick="btn_buscar_Click"
                Text="Buscar" Width="164px" />
            <asp:Button ID="btn_guardar" runat="server" CssClass="buttonRed" Height="25px" Text="Guardar"
                Width="164px" OnClick="btn_guardar_Click" OnClientClick="return confirm('¿Esta seguro de continuar?');" />
        </div>
        <div>
            <asp:Label ID="lblmensaje" runat="server" Style="text-align: left"></asp:Label>
        </div>
        <asp:UpdatePanel ID="up_dataList" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div align="center">
                    <div align="left">
                        <asp:CheckBox ID="cb_all" runat="server" AutoPostBack="true" OnCheckedChanged="cb_all_CheckedChanged"
                            TextAlign="Left" Text="Selecionar todo" Style="text-decoration: underline" />
                    </div>
                    <asp:DataList ID="DataList_ElementVis" runat="server" RepeatColumns="3" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <ItemTemplate>
                            <table class="style1">
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadBinaryImage ID="radBin_foto" runat="server" Height="320px" Width="400px"
                                            DataValue='<%# Eval("foto") %>' AutoAdjustImageControlSize="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="etiqueta">
                                        <%-- <asp:RadioButton ID="rb_validar" runat="server" Text="Validar" GroupName="unique"
                                    ValidationGroup="unique" Checked='<%# Eval("Validado")%>' Style="font-weight: 700;
                                    text-decoration: underline" />--%>
                                        <asp:CheckBox ID="cb_validar" runat="server" Text="Validar" Checked='<%# Eval("Valid_Foto")%>'
                                            Style="font-weight: 700; text-decoration: underline" />
                                        <asp:HiddenField ID="hf_idElemtVisib" runat="server" Value='<%# Eval("Id_rpvisibilidadCompDet") %>' />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        Elemento
                                    </td>
                                    <td class="style4">
                                        <asp:Label ID="lbl_popName" runat="server" Text='<%# Eval("POP_name") %>' 
                                            style="font-weight: 700"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        Comentario
                                    </td>
                                    <td class="style4">
                                        <asp:Label ID="lbl_cometario" runat="server" Text='<%# Eval("Comentario") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="etiqueta">
                                        <b style="text-align: left">Fecha </b>
                                    </td>
                                    <td class="etiqueta">
                                        
                                        <asp:Label ID="lbl_fecha" runat="server" Text='<%# Eval("Fec_Reg_Bd") %>'></asp:Label>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <b style="text-align: left">Empresa</b>
                                    </td>
                                    <td class="style4">
                                        <asp:Label ID="lbl_precProm" runat="server" Text='<%# Eval("Company_Name") %>'></asp:Label>
                                    </td>
                                </tr>
                                  
                                <tr>
                                    <td class="style4">
                                        <b style="text-align: left">Punto de venta</b>
                                    </td>
                                    <td class="style4">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Pdv_Name") %>'></asp:Label>
                                    </td>
                                </tr
                                <tr>
                                </tr>
                                    <tr>
                                        <td class="style4">
                                            <b style="text-align: left">Ciudad</b>
                                        </td>
                                        <td class="style4">
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Name_Oficina") %>'></asp:Label>
                                        </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    </asp:DataList>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style3
        {
            height: 20px;
            font-weight: bold;
            text-align: left;
        }
        .style4
        {
            text-align: left;
            height: 20px;
        }
    </style>
</asp:Content>
