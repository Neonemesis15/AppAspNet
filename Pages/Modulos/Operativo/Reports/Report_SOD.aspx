<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_SOD.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_SOD" %>

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
            <table style="width: 100%; height: auto;" align="center" class="style1">
                <tr>
                    <td class="style5" colspan="2">
                        &nbsp; <span class="style6">REPORTE DE SOD </span>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Fecha :
                    </td>
                    <td class="style2">
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
                    <td class="style4">
                        Canal :
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Campaña :
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged" Width="275px" Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Mercaderista :
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="cmbperson" runat="server" Height="25px" Width="275px" Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Oficina :
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="cmbOficina" runat="server" Enabled="False" Height="25px" Width="275px"
                            AutoPostBack="true" OnSelectedIndexChanged="cmbOficina_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
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
                    <td class="style4">
                        Punto de venta :
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="cmbPuntoDeVenta" runat="server" Enabled="False" Height="25px"
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Categoria del producto :
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="cmbcategoria_producto" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbcategoria_producto_SelectedIndexChanged" Width="275px"
                            Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Marca :
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="cmbmarca" runat="server" Height="25px" Width="275px" Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td class="style2">
                        <div>
                            <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="buttonGreen"
                                Height="25px" Width="164px" OnClick="btn_buscar_Click" />
                                <asp:Button ID="BtnCrear" runat="server" CssClass="buttonGreen" Height="25px" Text="Crear"
                            Width="164px" />
                          <asp:Button ID="BtnCrearMasivo" runat="server" CssClass="buttonGreen" Height="25px" Text="Carga Masiva"
                            Width="164px" />


                        
                        </div>
                            
                        <div>
                            <asp:Label ID="lblmensaje" runat="server" Style="text-align: left" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="div_gvSOD" runat="server" style="width: 100%; height: auto;">
                <asp:Button ID="btnocultar" runat="server" CssClass="alertas" Text="Consultar" Width="100%" />
                <asp:GridView ID="gv_SOD" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderWidth="1px" CellPadding="3" BorderStyle="None" AllowPaging="True" OnPageIndexChanging="gv_SOD_PageIndexChanging"
                    AutoGenerateColumns="False" PageSize="100" OnRowEditing="gv_sod_RowEditing" OnRowCancelingEdit="gv_sod_RowCancelingEdit"
                    OnRowUpdating="gv_sod_RowUpdating">
                    <HeaderStyle CssClass="GridHeader" />
                    <RowStyle CssClass="GridRow" />
                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                    <Columns>
                        <asp:BoundField DataField="ROWID" HeaderText="N°" ReadOnly="true" />
                         <asp:BoundField DataField="Oficina" HeaderText="Oficina" ReadOnly="true" />
                        <asp:BoundField DataField="commercialNodeName" HeaderText="Zona" ReadOnly="true" Visible="false" />
                        <asp:BoundField DataField="ClientPDV_Code" HeaderText="Código PDV" ReadOnly="true" />
                        <asp:BoundField DataField="Punto de venta" HeaderText="Punto de venta" ReadOnly="true" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" ReadOnly="true" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" ReadOnly="true" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Exhib Primaria
                            </HeaderTemplate>
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txt_gvsod_Exhib_Primaria" runat="server" Enabled="false"
                                    ShowSpinButtons="True" IncrementSettings-InterceptMouseWheel="False" NumberFormat-DecimalDigits="2"
                                    Height="20px" Skin="Telerik" Width="112px" Culture="es-PE" DbValue='<%#Eval("Exhib_Primaria")%>'
                                    MinValue="0" ToolTip="Exhib Primaria" EmptyMessage="vacio">
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Exhib Secundaria
                            </HeaderTemplate>
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txt_gvsod_Exhib_Secundaria" runat="server" Enabled="false"
                                    ShowSpinButtons="True" IncrementSettings-InterceptMouseWheel="False" NumberFormat-DecimalDigits="2"
                                    Height="20px" Skin="Telerik" Width="112px" Culture="es-PE" DbValue='<%#Eval("Exhib_Secundaria")%>'
                                    MinValue="0" ToolTip=" Exhib Secundaria" EmptyMessage="vacio">
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
                                <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("Validado")%>'  />
                                <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                <asp:Label ID="lbl_id_rpsd" runat="server" Visible="false" Text='<%# Eval("id_rpsd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" EditImageUrl="~/Pages/images/edit_icon.gif"
                            ButtonType="Image" CancelImageUrl="~/Pages/images/cancel_edit_icon.png" UpdateImageUrl="~/Pages/images/save_icon.png" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" ForeColor="White" Font-Bold="True" />
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
             <asp:Panel ID="CrearReport" runat="server" CssClass="busqueda" DefaultButton="btnGuardarReport"
                Height="400px" Width="780px" Style="display: none">
                <table align="center">
                    <caption>
                        <br />
                        <tr>
                            <td>
                                <asp:Label ID="lblBGR" runat="server" CssClass="labelsTit2" Text="Crear Reporte de SOD" />
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
                            <asp:DropDownList ID="ddlCanal" runat="server" Width="205px" AutoPostBack="true"
                              OnSelectedIndexChanged="ddlCanal_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="LblCanal" runat="server" CssClass="labels" Text="Campaña :" />
                        </td>
                        <td>
                            <asp:DropDownList runat="server" AutoPostBack="True" Width="205px" ID="ddlCampana"
                             OnSelectedIndexChanged="ddlCampana_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>
                    </tr>
              
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" CssClass="labels" Text="Oficina :" />
                        </td>
                        <td>
                            <asp:DropDownList runat="server" Width="205px" ID="ddlOficina" AutoPostBack="true" OnSelectedIndexChanged="ddlOficina_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label6" runat="server" CssClass="labels" Text="Zona:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNodeComercial" runat="server" Width="205px" AutoPostBack="true" 
                              OnSelectedIndexChanged="ddlNodeComercial_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label13" runat="server" CssClass="labels" Text="Punto de venta:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPuntoVenta" runat="server" Width="205px" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label1" runat="server" CssClass="labels" Text="Categoria:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCategoria" runat="server" Width="205px" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label2" runat="server" CssClass="labels" Text="Marca:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMarca" runat="server" Width="205px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td align="right">
                            <asp:Label ID="Label5" runat="server" CssClass="labels" Text="Exhib. Primaria:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrimaria" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td align="right">
                            <asp:Label ID="Label8" runat="server" CssClass="labels" Text="Exhib. Secundaria:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtSecundaria" runat="server" Width="205px"></asp:TextBox>
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
                                <asp:Button ID="btnGuardarReport" runat="server" CssClass="buttonPlan" Text="Guardar"
                                    Width="80px" OnClick="btnGuardarReport_Click" />
                                <asp:Button ID="btnCancelar" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" />
                            </td>
                        </tr>
                    </caption>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="MopoReport" runat="server" BackgroundCssClass="modalBackground"
                DropShadow="True" Enabled="True" OkControlID="btnCancelar" PopupControlID="CrearReport"
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
                           <a class="button" href="../../../formatos/Formato_Masivo_Reporte_SOD.xls"><span>Descargar Formato</span></a>
                           <a runat="server" visible="false" id="Datos" class="button" href="masivo/DATOS_CARGA_REPORTE_SOD.xls"><span>Descargar Datos</span></a>
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
           
            <asp:PostBackTrigger ControlID="btnCargaMasiva" />
        </Triggers>
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
                &nbsp; &nbsp; &nbsp;<asp:GridView ID="gv_sodToExcel" runat="server" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False">
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

