<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_Incidencias.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Incidencias" %>

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
    <%--   <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>
            <div align="center" style="font-family: Verdana; font-size: medium; color: #00579E;
                font-weight: bold">
                REPORTE DE INCIDENCIAS
            </div>
            <table style="width: 125%; height: auto;" align="center" class="art-Block-body">
                <tr>
                    <td class="style2">
                        Fecha :
                    </td>
                    <td class="style7">
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
                        &nbsp;Tipo de Reporte :
                    </td>
                    <td class="style7">
                        <asp:DropDownList ID="cmbtiporeporte" runat="server" Height="25px" Width="275px"
                            Style="text-align: left" CausesValidation="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;Canal :
                    </td>
                    <td class="style7">
                        <asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="275px" Style="text-align: left">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;Campaña :
                    </td>
                    <td class="style7">
                        <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged" Width="275px" CausesValidation="True"
                            Enabled="False">
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
                    <td class="style7">
                        <%-- <asp:DropDownList ID="cmbperson" runat="server" Height="25px" Width="275px" Style="text-align: left"
                            CausesValidation="True" Enabled="False">
                        </asp:DropDownList>--%>
                        <cc3:DropDownCheckBoxes ID="cmbperson" runat="server" AddJQueryReference="true" UseButtons="true"
                            UseSelectAllNode="true" Style="top: 0px; left: 315px; height: 16px; width: 241px"
                            OnSelectedIndexChanged="cmbperson_SelectedIndexChanged">
                            <%--<Style   SelectBoxWidth="160" DropDownBoxBoxWidth="160" DropDownBoxBoxHeight="115" />--%>
                            <Texts SelectBoxCaption= "-----Gestores de informarción y exhibición -----" />
                        </cc3:DropDownCheckBoxes>
                        <div>
                            <asp:Panel ID="Panel_mercaderistas" runat="server" ScrollBars="Vertical">
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Oficina :
                    </td>
                    <td class="style7">
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
                    <td class="style7">
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
                    <td class="style7">
                        <asp:DropDownList ID="cmbPuntoDeVenta" runat="server" Enabled="False" Height="25px"
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Categoria del producto :
                    </td>
                    <td class="style7">
                        <asp:DropDownList ID="cmbcategoria_producto" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbcategoria_producto_SelectedIndexChanged" Width="275px"
                            CausesValidation="True" Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Label ID="lbl_marca" runat="server" Text="Marca : "></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:DropDownList ID="cmbmarca" runat="server" Height="25px" Width="275px" Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td class="style7">
                        <div>
                            <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="buttonRed"
                                Height="25px" Width="164px" OnClick="btn_buscar_Click" />
                                               <script type="text/javascript">
                                                   function txt1(e, txt) {

                                                       var txtNombre = document.getElementById(txt);
                                                       txtNombre.style.borderColor = "black";


                                                   }
</script>
                        </div>
                        <div>
                            <asp:Label ID="lblmensaje" runat="server"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
            <div>
                <div align="right">
                    <asp:Label ID="lbl_cb_all" runat="server" Text="Seleccionar todos" Visible="false"></asp:Label>
                    <asp:CheckBox ID="cb_all" Visible="false" runat="server" AutoPostBack="True" OnCheckedChanged="cb_all_CheckedChanged" />
                </div>
                <telerik:RadGrid ID="gv_incidencias" runat="server" AutoGenerateColumns="False" PageSize="2000"
                    Skin="Vista" Font-Size="Small" AllowPaging="True" GridLines="None" OnCancelCommand="gv_incidencias_CancelCommand"
                    OnEditCommand="gv_incidencias_EditCommand" OnPageIndexChanged="gv_incidencias_PageIndexChanged"
                    ShowFooter="True" OnPageSizeChanged="gv_incidencias_PageSizeChanged" OnUpdateCommand="gv_incidencias_UpdateCommand"
                    OnDataBound="gv_incidencias_DataBound" 
                    onitemcommand="gv_incidencias_ItemCommand"  style="overflow:scroll" >
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
                            <telerik:GridBoundColumn DataField="Abreviatura" HeaderText="Oficina" UniqueName="Abreviatura"
                                ReadOnly="true" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ClientPDV_Code" HeaderText="Cod Pdv" UniqueName="ClientPDV_Code"
                                ReadOnly="true" Visible="false">
                            </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Foto" UniqueName="TemplateColumn" ReadOnly="true">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkBtn_viewphoto" runat="server" CommandArgument='<%# Bind("Id_regft") %>'
                                                    CommandName="VERFOTO" Font-Underline="True">Ver</asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkBtn_Editphoto" runat="server" CommandArgument='<%# Bind("Id_regft") %>'
                                                    CommandName="EDITFOTO" Font-Underline="True">Cambiar_Foto</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="lbl_id_reg_foto" runat="server" Text='<%# Bind("Id_regft") %>' Visible="False"></asp:Label>
                                    <telerik:RadBinaryImage ID="RadBinaryImage_foto" runat="server" Width="110px" Height="90px"
                                        DataValue='<%# Eval("Foto") %>' AutoAdjustImageControlSize="False" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Punto de Venta" HeaderText="Punto de venta" UniqueName="Punto de Venta"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Nivel_Cliente " HeaderText="Nivel" UniqueName="Nivel_Cliente"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="commercialNodeName" HeaderText="Nombre del Mercado"
                                UniqueName="commercialNodeName" ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Direccion" HeaderText="Direccion" UniqueName="Direccion"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Distribuidora" HeaderText="Distribuidora" UniqueName="Distribuidora"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Frecuencia" HeaderText="Frecuencia" UniqueName="Frecuencia"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Zona" HeaderText="Zona" UniqueName="Zona" ReadOnly="true">
                            
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Supervisor" HeaderText="Supervisor" UniqueName="Supervisor"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Categoria" HeaderText="Categoria" UniqueName="Categoria"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Marca" HeaderText="Marca" UniqueName="Marca"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SKU" HeaderText="SKU" UniqueName="Sku" ReadOnly="true"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Producto" HeaderText="Producto" UniqueName="Producto"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="id_servicio" HeaderText="Cod Servicio" UniqueName="id_servicio"
                                ReadOnly="true" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Servicio_Nombre" HeaderText="Servicio" UniqueName="Servicio_Nombre"
                                ReadOnly="true" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="pedido" HeaderText="Marcar(x)" UniqueName="pedido"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripcion" UniqueName="descripcion"
                                ReadOnly="true" Visible="false">
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
                                    <asp:Label ID="Id_incidencia" runat="server" Text='<%# Bind("Id_incidencia") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="Id_incidenciaDetalle" runat="server" Text='<%# Bind("Id_incidenciaDetalle") %>'
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
            </div>
            <div>
                <asp:Button ID="btn_view" runat="server" CssClass="alertas" Text="ocultar" Width="95px" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender_viewfoto" runat="server" DropShadow="True"
                    TargetControlID="btn_view" PopupControlID="panel_viewfoto" CancelControlID="ImageButtonCancel_viewfoto">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="panel_viewfoto" runat="server"  style="display:none"   BackColor="White"
                    BorderColor="#AEDEF9" BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana"
                    Font-Size="10pt" Height="520px" Width="590px">
                    <div>
                        <asp:ImageButton ID="ImageButtonCancel_viewfoto" runat="server" BackColor="Transparent"
                            Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                    </div>
                    <div align="center" style="font-family: verdana; font-size: medium; color: #005DA3;">
                        <div>
                            Foto</div>
                        <br />
                        <div>
                            <telerik:RadBinaryImage ID="RadBinaryImage_viewFoto" runat="server" Width="570px"
                                Height="400px" AutoAdjustImageControlSize="False"  AlternateText="Subir foto"
                                GenerateEmptyAlternateText="true" />
                        </div>
                        <div align="center"  >
                        <asp:CheckBox runat="server" ID="chkValidar" Text="Validar" />
                                    <asp:imagebutton id="ImageButton3"
				runat="server" Width="50px" Height="50px" 
                onclick="ImageButton3_Click" ImageUrl="~/Pages/images/sub_black_rotate_ccw.ico"
                ></asp:imagebutton>
                            <asp:imagebutton id="ImageButton4" 
				runat="server" Width="50px" Height="50px" ImageUrl="~/Pages/images/sub_black_rotate_cw.ico" onclick="ImageButton4_Click" 
                ></asp:imagebutton>
                                     Guardar<asp:ImageButton 
                                        ID="ibtnGuardarImagen" runat="server" 
                                ImageUrl="~/Pages/images/save_icon.png" onclick="ibtnGuardarImagen_Click" style="height: 24px"
                       />
                        </div>
                    </div>
                </asp:Panel>
            </div>
                        <asp:Panel ID="Pmensaje" style="display:none"  runat="server" Height="169px"  Width="332px" >
            <div runat="server" id="divMensaje"   >
                <table align="center" width="332px" >
                    <tr >
                        <td align="center"  valign="top">
                            <br />
                           <asp:Image runat="server" ID="ImgMensaje" />
                        </td>
                        <td style="width: 238px; height: 119px;" valign="top">
                            <br />
                            <asp:Label ID="lblencabezado" runat="server" CssClass="labels"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="lblmensajegeneral" runat="server" CssClass="labels"></asp:Label>
                            <br />
                            <br />
                            <div align="center">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button  runat="server" ID="btnMensaje" Text="Aceptar" CssClass="buttonBlue" Width="100px"  />
                            </div>
                        </td>

                    </tr>
                </table>
                </div>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupMensaje" runat="server" Enabled="True" TargetControlID="btndipararalerta"
                PopupControlID="Pmensaje" OkControlID="btnMensaje"   BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Button ID="btndipararalerta" runat="server" Text="" Height="0px" CssClass="alertas"
                    Width="0" Enabled="False" />
            



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

    <div >
        <asp:Button ID="btn_popup_ocultar" runat="server"  CssClass="alertas" Text="ocultar"
            Width="95px" />
        <cc1:ModalPopupExtender ID="ModalPopup_Edit" runat="server" DropShadow="True" TargetControlID="btn_popup_ocultar"
            PopupControlID="panelEdit">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="panelEdit" runat="server" style="display:none"  BackColor="White"
            BorderColor="#AEDEF9" BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana"
            Font-Size="10pt" Height="360px" Width="450px">
            <div>
                <asp:ImageButton ID="BtnclosePanel" runat="server" BackColor="Transparent" Height="22px"
                    ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="BtnclosePanel_Click" />
            </div>
            <div align="center" style="font-family: verdana; font-size: medium; color: #005DA3;">
                <div>
                    Cambiar Foto</div>
                <br />
                <div align="center">
                    <input type="file" runat="server" id="inputFile" />
                    <asp:Button ID="buttonSubmit" runat="server" Text="Cargar" OnClick="buttonSubmit_Click"
                        CssClass="RadUploadButton" />
                  <telerik:RadProgressManager ID="RadProgressManager1" runat="server" ClientIDMode="AutoID"  />
                    <telerik:RadProgressArea ID="RadProgressArea1" runat="server" Culture="es-PE" Localization-Uploaded="Cargado "
                        Localization-UploadedFiles="Archivos Cargados: " Localization-TotalFiles="Total de Archivos: "
                        Localization-EstimatedTime="Tiempo estimado: " Localization-CurrentFileName="Cargando archivo: "
                        Localization-TransferSpeed="Velocidad: " Localization-ElapsedTime="Tiempo transacurrido: "
                        Skin="Outlook" EnableAjaxSkinRendering="False" Language="">
                    </telerik:RadProgressArea>
                    <br />
                    <br />
                </div>
                <div>
                    <telerik:RadBinaryImage ID="RadBinaryImage_fotoBig" runat="server" Width="420px"
                        Height="250px" AutoAdjustImageControlSize="False" Visible="false" AlternateText="Subir foto"
                        GenerateEmptyAlternateText="true" />
                </div>
                <div style="font-size: small">
                    Guardar<asp:ImageButton ID="imgbtn_save" runat="server" ImageUrl="~/Pages/images/save_icon.png"
                        OnClick="imgbtn_save_Click" />
                    &nbsp;Cancelar<asp:ImageButton ID="imgbtn_cancel" runat="server" ImageUrl="~/Pages/images/cancel_edit_icon.png"
                        OnClick="imgbtn_cancel_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>

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
                <asp:GridView ID="gv_incidenciasToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
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
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style2
        {
            text-align: right;
            width: 120px;
        }
        .style7
        {
            width: 555px;
        }
        </style>
</asp:Content>
