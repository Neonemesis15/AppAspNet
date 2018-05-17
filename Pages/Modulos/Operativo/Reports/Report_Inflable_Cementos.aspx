<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master" AutoEventWireup="true" CodeBehind="Report_Inflable_Cementos.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Inflable_Cementos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
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
    <asp:UpdatePanel ID="UpReportFotografico" runat="server">
        <ContentTemplate>
         <div align="center">
            <table style="width: 100%; height: auto;" align="center" class="art-Block-body">
                <tr>
                    <td class="style3" >
                        REPORTE INFLABLE
                    </td>
                </tr>
            </table>

            <fieldset    style="width:850px"  >
            <legend  > Consultar Reporte Inflable</legend>

            <table>
            <tr>
           <td  align="right"> Fecha De Inicio:</td>
           <td  align="left"><telerik:RadDateTimePicker ID="txt_fecha_inicio" runat="server" Culture="es-PE"
                            Skin="Web20">
                            <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora">
                            </TimeView>
                            <TimePopupButton HoverImageUrl="" ImageUrl="" />
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Web20">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDateTimePicker></td>
           <td align="right">Fecha De Fin:</td>
           <td align="left"><telerik:RadDateTimePicker ID="txt_fecha_fin" runat="server" Culture="es-PE"
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
             <td  align="right">Canal :</td>
           <td><asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="275px" Style="text-align: left">
                        </asp:DropDownList></td>
           <td  align="right">Zona :</td>
           <td><asp:DropDownList ID="cmbNodeComercial" runat="server" Enabled="False" Height="25px"
                            Width="275px" OnSelectedIndexChanged="cmbNodeComercial_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList></td>
            </tr>
             <tr>
           <td  align="right"> Campaña:</td>
           <td><asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged" Width="275px" CausesValidation="True"
                            Enabled="False">
                        </asp:DropDownList></td>
           <td  align="right">Punto de venta :</td>
           <td><asp:DropDownList ID="cmbPuntoDeVenta" runat="server" Enabled="False" Height="25px"
                            Width="275px">
                        </asp:DropDownList></td>
            </tr>
             <tr>
           <td  align="right">Mercaderista :</td>
           <td><asp:DropDownList ID="cmbperson" runat="server" Height="25px" Width="275px" Style="text-align: left"
                            CausesValidation="True" Enabled="False">
                        </asp:DropDownList></td>
           <td  align="right"> 
               <asp:Label ID="lbl_marca" runat="server" Text="Status : "></asp:Label>
                 </td>
           <td>
               <asp:DropDownList ID="cmbstatus" runat="server" Enabled="False" Height="25px" 
                   Width="275px">
               </asp:DropDownList>
                 </td>
            </tr>
             <tr>
           <td  align="right">Oficina :</td>
           <td><asp:DropDownList ID="cmbOficina" runat="server" Enabled="False" Height="25px" Width="275px"
                            AutoPostBack="true" OnSelectedIndexChanged="cmbOficina_SelectedIndexChanged">
                        </asp:DropDownList></td>
           <td  align="right">Ciudad :</td>
           <td>
               <asp:DropDownList ID="cmbciudad" runat="server" AutoPostBack="True" 
                   Height="25px" onselectedindexchanged="cmbciudad_SelectedIndexChanged" 
                   Width="275px">
               </asp:DropDownList>
                 </td>
            </tr>
 
                                     <tr>
           <td  align="right"></td>
           <td>&nbsp;</td>
           <td  align="right">
               <asp:Label ID="Label9" runat="server" Text="Distrito : "></asp:Label>
                                         </td>
           <td>
               <asp:DropDownList ID="cmbDistrito" runat="server" Enabled="False" Height="25px" 
                   Width="275px">
               </asp:DropDownList>
                                         </td>
            </tr>
             <tr>
           <td colspan="4" align="center">

               <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="buttonRed"
                                Height="25px" Width="164px" OnClick="btn_buscar_Click" /> </td>
            </tr>
             <tr>
           <td>&nbsp;</td>
           <td> <asp:Label ID="lblmensaje" runat="server"></asp:Label></td>
           <td>&nbsp;</td>
           <td>&nbsp;</td>
            </tr>

            </table>

            </fieldset>



            </div>






            <div id="div_gvFoto" runat="server" aling="center" style="width: 100%; height: auto;">
                <div align="right">
                    <asp:Label ID="lbl_cb_all" runat="server" Text="Seleccionar todos" Visible="false"></asp:Label>
                    <asp:CheckBox ID="cb_all" Visible="false" runat="server" AutoPostBack="True" OnCheckedChanged="cb_all_CheckedChanged" />
                       <asp:Button ID="btn_validar" runat="server" OnClick="btn_validar_Click" OnClientClick="return confirm('¿Esta seguro de Validar los registros?');"
                                        Text="Validar" CssClass="button" />
                                    <br />
                </div>
                <telerik:RadGrid ID="gv_Foto" runat="server" AutoGenerateColumns="False" PageSize="2000"
                    Skin="Vista" Font-Size="Small" AllowPaging="True" GridLines="None" OnCancelCommand="gv_Foto_CancelCommand"
                    OnEditCommand="gv_Foto_EditCommand" OnPageIndexChanged="gv_Foto_PageIndexChanged"
                    ShowFooter="True" OnPageSizeChanged="gv_Foto_PageSizeChanged" OnUpdateCommand="gv_Foto_UpdateCommand"
                    OnDataBound="gv_Foto_DataBound" OnItemCommand="gv_Foto_ItemCommand" 
                    onitemdatabound="gv_Foto_ItemDataBound">
                    <MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E"
                        AlternatingItemStyle-BackColor="#F7F7F7" Font-Size="Smaller" AlternatingItemStyle-ForeColor="#333333">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ROWID" HeaderText="N°" UniqueName="ROWID" ReadOnly="true">
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
                            <telerik:GridBoundColumn DataField="malla" HeaderText="Zona" UniqueName="malla"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Ciudad" HeaderText="Ciudad" UniqueName="Ciudad"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Distrito" HeaderText="Distrito" UniqueName="Distrito"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="pdv_Name" HeaderText="Punto de Venta" UniqueName="Punto de Venta"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Direccion" HeaderText="Dirección" UniqueName="Direccion"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="FECHA_INCIDENTE" HeaderText="Fecha de Incidente" UniqueName="FECHA_INCIDENTE"
                               PickerType="DateTimePicker" >
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="Fecha_Vencimiento" HeaderText="Fecha Final" UniqueName="Fecha_Vencimiento"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                               <telerik:GridDropDownColumn Visible="false"  HeaderText="Status" UniqueName="ddlStatus">
                            </telerik:GridDropDownColumn>
                            <telerik:GridBoundColumn DataField="Comentario" HeaderText="Observaciones" UniqueName="Comentario"
                               >
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="Gestor" HeaderText="Gestor de Inf. y Exh." UniqueName="Gestor"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModiBy" HeaderText="Modificado por" UniqueName="ModiBy"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DateModiBy" HeaderText="Fecha de Modificación" UniqueName="DateModiBy"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Validado" UniqueName="TemplateColumn" ReadOnly="true">
                                <HeaderTemplate>
                                 
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("Validado")%>' />
                                    <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_Id_Reg_Fachada" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_Reg_Foto" runat="server" Text='<%# Bind("Id_regft") %>' Visible="false"></asp:Label>
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
                <br />
            </div>
            <div>
                <asp:Button ID="btn_view" runat="server" CssClass="alertas" Text="ocultar" Width="95px" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender_viewfoto" runat="server" DropShadow="True"
                    TargetControlID="btn_view" PopupControlID="panel_viewfoto" CancelControlID="ImageButtonCancel_viewfoto">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="panel_viewfoto" runat="server" style="display:none"   BackColor="White"
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
                                    <asp:imagebutton id="ImageButton3"
				runat="server" Width="50px" Height="50px" 
                onclick="ImageButton3_Click" ImageUrl="~/Pages/images/sub_black_rotate_ccw.ico"
                ></asp:imagebutton>
                            <asp:imagebutton id="ImageButton4" 
				runat="server" Width="50px" Height="50px" ImageUrl="~/Pages/images/sub_black_rotate_cw.ico" onclick="ImageButton4_Click" 
                ></asp:imagebutton>
                                    Guardar<asp:ImageButton 
                                        ID="ibtnGuardarImagen" runat="server" ImageUrl="~/Pages/images/save_icon.png" onclick="ibtnGuardarImagen_Click"
                       />
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3"
                AssociatedUpdatePanelID="UpReportFotografico" BackgroundCssClass="modalProgressGreyBackground">
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

     <div>
               <asp:Panel ID="CrearReporFotografico" style="display:none" runat="server" CssClass="busqueda" DefaultButton="btnGuardarReporteFotografico"  
                                Height="400px" Width="780px" >
    <table align="center">
        <caption>
            <br />
            <tr>
                <td>
                    <asp:Label ID="lblBGR" runat="server" CssClass="labelsTit2" 
                        Text="Crear Reporte Fotografico" />
                </td>
            </tr>
        </caption>
    </table>
    <table align="center">
    <tr>
    <td align="right"><asp:Label ID="Label12" runat="server" CssClass="labels" Text="Canal :" /></td>
    <td><asp:DropDownList ID="ddlCanal" runat="server" Width="205px" 
            AutoPostBack="True" 
            onselectedindexchanged="ddlCanal_SelectedIndexChanged"   ></asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="LblCanal" runat="server" CssClass="labels" Text="Campaña :" /></td><td>
    <asp:DropDownList runat="server" AutoPostBack="True" Width="205px" ID="ddlCampana" onselectedindexchanged="ddlCampana_SelectedIndexChanged" 
           ></asp:DropDownList></td>
                
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label19" runat="server" CssClass="labels" Text="Mercaderista :" /></td><td>
    <asp:DropDownList runat="server" Width="205px" ID="ddlMercaderista"  ></asp:DropDownList></td>
    </tr>
        <tr>
    <td align="right"><asp:Label ID="Label7" runat="server" CssClass="labels" Text="Oficina :" /></td><td>
    <asp:DropDownList runat="server" Width="205px" ID="ddlOficina" 
                onselectedindexchanged="ddlOficina_SelectedIndexChanged" 
                AutoPostBack="True"  ></asp:DropDownList></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label6" runat="server" CssClass="labels" Text="Zona:" /></td><td>
        <asp:DropDownList ID="ddlNodeComercial" runat="server" Width="205px" 
            AutoPostBack="True" onselectedindexchanged="ddlNodeComercial_SelectedIndexChanged" 
             ></asp:DropDownList></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label13" runat="server" CssClass="labels" Text="Punto de venta:" /></td><td>
        <asp:DropDownList ID="ddlPuntoVenta" runat="server" Width="205px"     ></asp:DropDownList></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label1" runat="server" CssClass="labels" Text="Categoria:" /></td><td>
        <asp:DropDownList ID="ddlCategoria" runat="server" Width="205px" 
            
            AutoPostBack="True" 
            onselectedindexchanged="ddlCategoria_SelectedIndexChanged"     ></asp:DropDownList></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label2" runat="server" CssClass="labels" Text="Marca:" /></td><td>
        <asp:DropDownList ID="ddlMarca" runat="server" Width="205px" 
                ></asp:DropDownList></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label5" runat="server" CssClass="labels" Text="Tipo Reporte:" /></td><td>
        <asp:DropDownList ID="ddlTipoReporte" runat="server" Width="205px" 
                ></asp:DropDownList></td>
    </tr>
        <tr>
    <td align="right"><asp:Label ID="Label3" runat="server" CssClass="labels" Text="Comentario:" /></td><td>
                                       <asp:TextBox ID="txtComentario" runat="server" Width="205px"  ></asp:TextBox></td>
    </tr>
          <tr>
    <td align="right"><asp:Label ID="Label4" runat="server" CssClass="labels" Text="Imagen:" /></td><td>
    <asp:FileUpload runat="server" ID="fileImagen" />
    
        </td>
    </tr>


        <caption>
            <br />
        </caption>
        </table><table align="center">
            <caption>
                <br />
                <tr>
                    <td>
                        <asp:Button ID="btnGuardarReporteFotografico" runat="server" CssClass="buttonPlan" 
                            Text="Guardar" Width="80px" 
                            onclick="btnGuardarReporteFotografico_Click"  />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="buttonPlan" 
                            Text="Cancelar" Width="80px" />
                    </td>
                </tr>
            </caption>
     </table>
     </asp:Panel>
     <cc1:ModalPopupExtender ID="MopoReporFotografico" runat="server" 
                                DropShadow="True" Enabled="True"  PopupControlID="CrearReporFotografico"
                                TargetControlID="Button1" >
     </cc1:ModalPopupExtender>

     <asp:Button ID="Button1" runat="server" CssClass="alertas" Text="ocultar"
            Width="95px" />
    </div>
    



    <div>
        <asp:Button ID="btn_popup_ocultar" runat="server" CssClass="alertas" Text="ocultar"
            Width="95px" />
        <cc1:ModalPopupExtender ID="ModalPopup_Edit" runat="server" DropShadow="True" TargetControlID="btn_popup_ocultar"
            PopupControlID="panelEdit">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="panelEdit" runat="server" style="display:none"   BackColor="White"
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
    <%--<asp:Button ID="btnocultar" runat="server" CssClass="alertas" Text="Consultar" />
    <cc1:ModalPopupExtender ID="modalpopudetalle" runat="server" BackgroundCssClass="modalBackground" 
        DropShadow="True" Enabled="True" PopupControlID="Panel_para_iframe" TargetControlID="btnocultar">
    </cc1:ModalPopupExtender>
    <asp:Panel ID='Panel_para_iframe' runat='server'>
        <iframe id="Iframe" runat="server" src="verFoto.aspx" height="500px" allowtransparency="true"
            scrolling="no" width="100%"></iframe>
    </asp:Panel>--%>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style3
        {
            text-align: center;
            width: 350px;
            font-weight: bold;
        }
        </style>
</asp:Content>

