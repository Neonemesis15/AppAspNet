<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_Competencia_SF_M.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Competencia_SF" %>

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
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Data Mercaderistas</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <uc1:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <uc1:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <%--<telerik:GridBoundColumn DataField="mercaderista" HeaderText="Generador" UniqueName="mercaderista"
                                    ReadOnly="true">
                                </telerik:GridBoundColumn>--%>
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>
            <table style="width: 100%; height: auto;" align="center" class="style1">
                <tr>
                    <center>
                    </center>
                </tr>
                <caption class="style8">
                    </center> <b>REPORTE DE COMPETENCIA</center></b> </tr> <b></tr> </b>
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
                        <td class="style6">
                            <asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Height="25px"
                                OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="275px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Campaña :
                        </td>
                        <td class="style6">
                            <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Height="25px"
                                OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged" Width="275px" Enabled="False">
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
                        <td class="style6">
                            <asp:DropDownList ID="cmbPuntoDeVenta" runat="server" Enabled="False" Height="25px"
                                Width="275px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Categoria del producto :
                        </td>
                        <td class="style6">
                            <asp:DropDownList ID="cmbcategoria_producto" runat="server" AutoPostBack="True" Height="25px"
                                OnSelectedIndexChanged="cmbcategoria_producto_SelectedIndexChanged" Width="275px"
                                Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            Marca :
                        </td>
                        <td class="style7">
                            <asp:DropDownList ID="cmbmarca" runat="server" Height="25px" Width="275px" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td class="style2">
                            Supervisor :
                        </td>
                        <td class="style6">
                            <asp:DropDownList ID="cmbperson" runat="server" Height="25px" Width="275px" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td class="style6">
                            <div>
                                <asp:Button ID="btn_buscar" runat="server" Text="Buscar" Height="25px" Width="164px"
                                    OnClick="btn_buscar_Click" />
                            </div>
                            <div>
                                <asp:Label ID="lblmensaje" runat="server" Style="text-align: left" Visible="False"></asp:Label>
                            </div>
                        </td>
                    </tr>
                </caption>
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
            </table>
            <div id="div_gvCompetencia" runat="server" class="class_div" style="width: auto;
                height: auto;">
                <table>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gv_competencia" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                Font-Size="Small" GridLines="None" OnCancelCommand="gv_competencia_CancelCommand"
                                OnDataBound="gv_competencia_DataBound" OnEditCommand="gv_competencia_EditCommand"
                                OnPageIndexChanged="gv_competencia_PageIndexChanged" OnPageSizeChanged="gv_competencia_PageSizeChanged"
                                OnPdfExporting="gv_competencia_PdfExporting" OnUpdateCommand="gv_competencia_UpdateCommand"
                                PageSize="30" ShowFooter="True" Skin="Vista">
                                <MasterTableView AlternatingItemStyle-BackColor="#F7F7F7" AlternatingItemStyle-ForeColor="#333333"
                                    Font-Size="Smaller" ForeColor="#00579E" NoMasterRecordsText="Sin Datos para mostrar.">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="corporacion" HeaderText="Cadena" ReadOnly="true"
                                            UniqueName="corporacion">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="oficina" HeaderText="Ciudad" ReadOnly="true"
                                            UniqueName="oficina">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="nodocomercial" HeaderText="Cliente" ReadOnly="true"
                                            UniqueName="nodocomercial">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="codigoPDV" HeaderText="Codigo PDV" ReadOnly="true"
                                            UniqueName="codigoPDV">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="pdv" HeaderText="PDV" ReadOnly="true" UniqueName="pdv">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="competidora" HeaderText="Empresa" ReadOnly="true"
                                            UniqueName="competidora">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="categoria" HeaderText="Categoria" ReadOnly="true"
                                            UniqueName="categoria">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="marca" HeaderText="Marca" ReadOnly="true" UniqueName="marca">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="actividad" HeaderText="Actividad" ReadOnly="true"
                                            UniqueName="actividad">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="grupoobjetivo" HeaderText="Grupo Objetivo" ReadOnly="true"
                                            UniqueName="grupoobjetivo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="promocionini" UniqueName="promocionini" HeaderText="Inicio de la promocion"
                                            PickerType="DateTimePicker">
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridDateTimeColumn DataField="promocionfin" UniqueName="promocionfin" HeaderText="Fin de la promocion"
                                            PickerType="DateTimePicker">
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="mecanica" HeaderText="Mecanica" ReadOnly="true"
                                            UniqueName="mecanica">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="precioregular" DataType="System.Double" EmptyDataText="NULO"
                                            HeaderText="Precio Regular" UniqueName="precioregular">
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="preciooferta" DataType="System.Double" EmptyDataText="NULO"
                                            HeaderText="Precio Oferta" UniqueName="preciooferta">
                                        </telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn DataField="supervisor" HeaderText="Supervisor" ReadOnly="true"
                                            UniqueName="supervisor">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="mercaderista" HeaderText="Mercaderista" ReadOnly="true"
                                            UniqueName="mercaderista">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FEC_COMUNICACION" HeaderText="Fecha de Comunicación"
                                            ReadOnly="true" UniqueName="FEC_COMUNICACION">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="observaciones" HeaderText="Observaciones" ReadOnly="true"
                                            UniqueName="Observaciones">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Fec_Reg_Bd" HeaderText="Fecha de Registro" ReadOnly="true"
                                            UniqueName="Fec_Reg_Bd">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ModiBy" HeaderText="Modificado por:" ReadOnly="true"
                                            UniqueName="modiby">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DateModiBy" HeaderText="Fecha de Modificación:"
                                            ReadOnly="true" UniqueName="datemodiby">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                            <HeaderTemplate>
                                                &nbsp;<asp:Button ID="btn_validar_Click" runat="server" OnClick="btn_validar_Click_Click"
                                                    OnClientClick="return confirm('¿Esta seguro de invalidar los registros?');" Text="Invalidar" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("CabezaValidado").ToString().Equals("True")?false:true%>'
                                                    Enabled="true" />
                                                <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                                <asp:Label ID="lblregcompetencia" runat="server" Text='<%#Eval("Id_rcompe") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridEditCommandColumn HeaderText="Modificar" ButtonType="ImageButton" CancelText="Cancelar"
                                            UpdateText="Actualizar">
                                        </telerik:GridEditCommandColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <EditColumn ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                            EditImageUrl="~/Pages/images/edit_icon.gif" UniqueName="EditCommandColumn1" UpdateImageUrl="~/Pages/images/save_icon.png">
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
                <asp:GridView ID="gv_competenciaToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="corporacion" HeaderText="Cadena" ReadOnly="true" />
                        <asp:BoundField DataField="oficina" HeaderText="Ciudad" />
                        <asp:BoundField DataField="nodocomercial" HeaderText="Cliente" />
                        <asp:BoundField DataField="codigoPDV" HeaderText="CodigoPDV" />
                        <asp:BoundField DataField="pdv" HeaderText="PDV" />
                        <asp:BoundField DataField="competidora" HeaderText="Empresa" />
                        <asp:BoundField DataField="categoria" HeaderText="Categoria" />
                        <asp:BoundField DataField="marca" HeaderText="Marca" />
                        <asp:BoundField DataField="actividad" HeaderText="Nombre Actividad" />
                        <asp:BoundField DataField="grupoobjetivo" HeaderText="Grupo Objetivo" />
                        <asp:BoundField DataField="promocionini" HeaderText="Inicio de Promocion" />
                        <asp:BoundField DataField="promocionfin" HeaderText="Fin de Promocion" />
                        <asp:BoundField DataField="mecanica" HeaderText="Mecanica" />
                        <asp:BoundField DataField="precioregular" HeaderText="Precio Regular" />
                        <asp:BoundField DataField="supervisor" HeaderText="Supervisor" />
                        <asp:BoundField DataField="mercaderista" HeaderText="Mercaderista" />
                        <asp:BoundField DataField="FEC_COMUNICACION" HeaderText="Fecha de Comunicación" />
                        <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
                        <asp:BoundField DataField="Fec_Reg_Bd" HeaderText="Fecha de Registro:" />
                        <asp:BoundField DataField="Modiby" HeaderText="Modificado por:" />
                        <asp:BoundField DataField="DateModiBy" HeaderText="Fecha de Modificacion:" />
                       
                    </Columns>
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
