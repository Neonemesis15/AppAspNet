<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_Competencia.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Competencia" %>

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
    <%--    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>
       <div align="center">
            <table style="width: 100%; height: auto;" align="center" >
                   
                    <tr>
                        <td class="style3" >
                            REPORTE DE COMPETENCIA
                        </td>
                    </tr>  
            </table>
             
           <fieldset    style="width:850px"  >
            <legend  > Consultar Reporte de Competencia</legend>

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
                            </telerik:RadDateTimePicker></td>
            </tr>
             <tr>
             <td  align="right">Canal :</td>
           <td><asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Height="25px"
                                OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="275px">
                            </asp:DropDownList></td>
           <td  align="right">Zona :</td>
           <td><asp:DropDownList ID="cmbNodeComercial" runat="server" Enabled="False" Height="25px"
                                Width="275px" OnSelectedIndexChanged="cmbNodeComercial_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList></td>
            </tr>
             <tr>
           <td  align="right"> Campaña:</td>
           <td> <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Height="25px"
                                OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged" Width="275px" Enabled="False">
                            </asp:DropDownList></td>
           <td  align="right">Punto de venta :</td>
           <td><asp:DropDownList ID="cmbPuntoDeVenta" runat="server" Enabled="False" Height="25px"
                                Width="275px">
                            </asp:DropDownList></td>
            </tr>
             <tr>
           <td  align="right">Mercaderista :</td>
           <td><asp:DropDownList ID="cmbperson" runat="server" Height="25px" Width="275px" Enabled="False">
                            </asp:DropDownList></td>
           <td  align="right">Categoria del producto:</td>
           <td><asp:DropDownList ID="cmbcategoria_producto" runat="server" AutoPostBack="True" Height="25px"
                                OnSelectedIndexChanged="cmbcategoria_producto_SelectedIndexChanged" Width="275px"
                                Enabled="False">
                            </asp:DropDownList></td>
            </tr>
             <tr>
           <td  align="right">Oficina :</td>
           <td><asp:DropDownList ID="cmbOficina" runat="server" Enabled="False" Height="25px" Width="275px"
                                AutoPostBack="true" OnSelectedIndexChanged="cmbOficina_SelectedIndexChanged">
                            </asp:DropDownList></td>
           <td  align="right">Marca :</td>
           <td><asp:DropDownList ID="cmbmarca" runat="server" Height="25px" Width="275px" Enabled="False">
                            </asp:DropDownList></td>
            </tr>
             <tr>
           <td colspan="4" align="center">
               <asp:Button ID="btn_buscar" runat="server" CssClass="buttonRed" Height="25px" 
                   OnClick="btn_buscar_Click" Text="Buscar" Width="164px" />
                 </td>
            </tr>
             <tr>
           <td>&nbsp;</td>
           <td><asp:Label ID="lblmensaje" runat="server" Style="text-align: left" Visible="False"></asp:Label></td>
           <td>&nbsp;</td>
           <td>&nbsp;</td>
            </tr>

            </table>

            </fieldset>

            <fieldset style="width:850px" >
            <legend  > Crear Reporte de Competencia</legend>
            <table align="center" style="height:25px">
            <tr>
            <td style="height:25px">
            <asp:Button ID="btnCrear" runat="server" Text="Crear" Height="25px" Width="164px" CssClass="buttonBlue" />
            </td>
            </tr>
            </table>
            
            </fieldset>



            </div>
            <div id="div_gvCompetencia" runat="server" class="class_div" style="width: auto; height: auto;"> <!-- inicio del grid_div -->
                            <!-- boton para validar todo -->
                            <div align="right">
                            <asp:Label ID="lbl_cb_all" runat="server" Text="Seleccionar todos" Visible="false"></asp:Label>
                            <asp:CheckBox ID="cb_all" Visible="false" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="cb_all_CheckedChanged"/>
                            <asp:Button ID="btn_validar" runat="server" OnClientClick="return confirm('¿Esta seguro de Validar los registros?');"
                                            Text="Validar" CssClass="button" onclick="btn_validar_Click"/>
                                        <br />
                            </div>
                            <!-- fin boton validar todo -->

                            <telerik:RadCodeBlock ID="rcdTenderDocuments" runat="server">
                                <script type="text/javascript">
                                    var popUp;
                                    function PopUpShowing(sender, eventArgs) {
                                        popUp = eventArgs.get_popUp();
                                        var gridWidth = sender.get_element().offsetWidth;
                                        var gridHeight = sender.get_element().offsetHeight;
                                        var popUpWidth = popUp.style.width.substr(0, popUp.style.width.indexOf("px"));
                                        var popUpHeight = popUp.style.height.substr(0, popUp.style.height.indexOf("px"));
                                        popUp.style.left = ((gridWidth - popUpWidth) / 2 + sender.get_element().offsetLeft).toString() + "px";
                                        popUp.style.top = (-250 + sender.get_element().offsetTop).toString() + "px";
                                        //popUp.style.top = ((gridHeight - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
                                    }
                                </script>
                            </telerik:RadCodeBlock>

                            <!-- inicia el RadGrid para competencia -->

                            <telerik:RadGrid ID="rgv_competencia" runat="server" 
                                AutoGenerateColumns="False" Skin="Vista" Font-Size="Small" AllowPaging="True" 
                                    GridLines="None" ShowFooter="True" ondatabound="rgv_competencia_DataBound" 
                                    onitemcommand="rgv_competencia_ItemCommand" AllowSorting="True" 
                                    oncancelcommand="rgv_competencia_CancelCommand" 
                                    oneditcommand="rgv_competencia_EditCommand" 
                                    onpageindexchanged="rgv_competencia_PageIndexChanged" 
                                    onpagesizechanged="rgv_competencia_PageSizeChanged" 
                                    onupdatecommand="rgv_competencia_UpdateCommand" 
                    PageSize="2000" >
                                <MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E"
                                    AlternatingItemStyle-BackColor="#F7F7F7" Font-Size="Smaller" AlternatingItemStyle-ForeColor="#333333" EditMode="PopUp">



                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ROWID" HeaderText="N°" UniqueName="ROWID" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Foto" UniqueName="TemplateColumn2">
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
                                        <telerik:GridBoundColumn DataField="Name_Local" HeaderText="Dist" ReadOnly="true"  UniqueName="Name_Local">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AgrComercial" HeaderText="Agr Comerc" ReadOnly="true" UniqueName="AgrComercial">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Company_Name" HeaderText="Emp.Compet" ReadOnly="true" UniqueName="Company_Name">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ClientPDV_Code" HeaderText="Código PDV" ReadOnly="true" UniqueName="ClientPDV_Code">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Punto de venta" HeaderText="PDV" ReadOnly="true" UniqueName="Punto de venta">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Categoria del producto" HeaderText="Categoria" ReadOnly="true" UniqueName="Categoria del producto">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Marca" HeaderText="Marca" ReadOnly="true" UniqueName="Marca">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Precio de costo" HeaderText="Prc.costo" UniqueName="Precio de costo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Precio de punto de venta" HeaderText="Prc.PDV" UniqueName="Precio de punto de venta">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="Fecha inicio de actividad" UniqueName="Fecha_inicio_de_actividad" HeaderText="Ini Act"
                                            PickerType="DateTimePicker">
                                        </telerik:GridDateTimeColumn>    
                                        <telerik:GridDateTimeColumn DataField="Fecha fin de actividad" UniqueName="Fecha_fin_de_actividad" HeaderText="Fin Act"
                                            PickerType="DateTimePicker">
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="Cant_Personal" HeaderText="Cant personal" UniqueName="Cant_Personal">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Premio" HeaderText="Premio" UniqueName="Premio">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Mecanica" HeaderText="Mecanica" UniqueName="Mecanica">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Obs." UniqueName="Obs">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Tipo promocion" HeaderText="Tipo Promocion" ReadOnly="true" UniqueName="Tipo promocion">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Grupo objetivo" HeaderText="Grp Obj" ReadOnly="true" UniqueName="Grupo objetivo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="GrObjAdc" HeaderText="Grp Obj Comentario" UniqueName="GrObjAdc">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Material de apoyo" HeaderText="POP" ReadOnly="true" UniqueName="Material de apoyo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Descripcion del material de apoyo" HeaderText="Descp POP" ReadOnly="true" UniqueName="Descripcion del material de apoyo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PopAdc" HeaderText="POP Comentario" UniqueName="PopAdc">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Person_name" HeaderText="Registrado por" ReadOnly="true" UniqueName="Person_name">
                                        </telerik:GridBoundColumn>                                        
                                        <telerik:GridDateTimeColumn DataField="Fec_Reg_Bd" UniqueName="Fec_Reg_Bd" HeaderText="Fecha de registro"
                                            PickerType="DateTimePicker">
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="ModiBy" HeaderText="Modificado por" ReadOnly="true" UniqueName="ModiBy">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DateModiBy" HeaderText="Modifico en" ReadOnly="true" UniqueName="DateModiBy">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Validar" UniqueName="Validado">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("Validado")%>'/> 
                                                <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                                <asp:Label ID="lbl_id_reg_competencia" Visible="false" runat="server" Text='<%# Bind("Id_rcompe") %>'></asp:Label>
                                                <asp:Label ID="lbl_id_reg_comp_detalle" Visible="false" runat="server" Text='<%# Bind("Id_rpde") %>'></asp:Label>
                                                <asp:Label ID="lbl_tipoReg" Visible="false" runat="server" Text='<%# Bind("TipoReg") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" 
                                            CancelImageUrl="~/Pages/images/cancel_edit_icon.png" 
                                            EditImageUrl="~/Pages/images/edit_icon.gif" 
                                            UpdateImageUrl="~/Pages/images/save_icon.png">
                                        </telerik:GridEditCommandColumn>
                                    </Columns>
                                    <EditFormSettings >
                                        <EditColumn UniqueName="EditCommandColumn1" ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png" >
                                        </EditColumn>
                                    </EditFormSettings>
                                    <AlternatingItemStyle BackColor="#F7F7F7" ForeColor="#333333" />
                                </MasterTableView>
                                <ClientSettings>
                                    <ClientEvents OnPopUpShowing="PopUpShowing" />
                                </ClientSettings>
                            </telerik:RadGrid>

                            <!-- finaliza el RadGrid para competencia -->
                            <%-- 
                            <asp:GridView ID="gv_competencia" runat="server" BackColor="White" BorderColor="#999999"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Style="margin-top: 0px;
                                text-align: left;" AllowPaging="True" OnPageIndexChanging="gv_competencia_PageIndexChanging"
                                AutoGenerateColumns="False" ShowFooter="True" PageSize="10" OnRowCancelingEdit="gv_competencia_RowCancelingEdit"
                                OnRowUpdating="gv_competencia_RowUpdating" OnRowEditing="gv_competencia_RowEditing"
                                EnableModelValidation="True" GridLines="Vertical">
                                <Columns>
                                    <asp:BoundField DataField="ROWID" HeaderText="N°" ReadOnly="true" />
                                    <asp:BoundField DataField="Name_Local" HeaderText="Dist" ReadOnly="true" />
                                    <asp:BoundField DataField="AgrComercial" HeaderText="Agr Comerc" ReadOnly="true" />
                                    <asp:BoundField DataField="Company_Name" HeaderText="Emp.Compet" ReadOnly="true" />
                                    <asp:BoundField DataField="ClientPDV_Code" HeaderText="Código PDV" ReadOnly="true" />
                                    <asp:BoundField DataField="Punto de venta" HeaderText="PDV" ReadOnly="true" />
                                    <asp:BoundField DataField="Categoria del producto" HeaderText="Categoria" ReadOnly="true" />
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" ReadOnly="true" />
                                    <asp:BoundField DataField="Precio de costo" HeaderText="Prc.costo" />
                                    <asp:BoundField DataField="Precio de punto de venta" HeaderText="Prc.PDV" />
                                    <asp:BoundField DataField="Fecha inicio de actividad" HeaderText="Ini Act" />
                                    <asp:BoundField DataField="Fecha fin de actividad" HeaderText="Fin Act" />
                                    <asp:BoundField DataField="Cant_Personal" HeaderText="Cant personal" />
                                    <asp:BoundField DataField="Premio" HeaderText="Premio" />
                                    <asp:BoundField DataField="Mecanica" HeaderText="Mecanica" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Obs.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblobs" runat="server" Text='<%# Bind("Observaciones") %>' Visible="true"></asp:Label>
                                            <asp:TextBox ID="txtobs" runat="server" Visible="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
                                    <asp:BoundField DataField="Tipo promocion" HeaderText="Tipo Promocion" ReadOnly="true" />
                                    <asp:BoundField DataField="Grupo objetivo" HeaderText="Grp Obj" ReadOnly="true" />
                                    <asp:BoundField DataField="GrObjAdc" HeaderText="Grp Obj Comentario" />
                                    <asp:BoundField DataField="Material de apoyo" HeaderText="POP" ReadOnly="true" />
                                    <asp:BoundField DataField="Descripcion del material de apoyo" HeaderText="Descp POP"
                                        ReadOnly="true" />
                                    <asp:BoundField DataField="PopAdc" HeaderText="POP Comentario" />
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
                                            <asp:Button ID="btn_validar1" runat="server" OnClick="btn_validar_Click" Text="Validar"
                                                OnClientClick="return confirm('¿Esta seguro de validar los registros?');" />
                                            <br />
                                            <asp:CheckBox ID="cb_all" runat="server" AutoPostBack="True" OnCheckedChanged="cb_all_CheckedChanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("Validado")%>'/> 
                                            <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                            <asp:Label ID="lbl_id_reg_competencia" Visible="false" runat="server" Text='<%# Bind("Id_rcompe") %>'></asp:Label>
                                            <asp:Label ID="lbl_id_reg_comp_detalle" Visible="false" runat="server" Text='<%# Bind("Id_rpde") %>'></asp:Label>
                                            <asp:Label ID="lbl_tipoReg" Visible="false" runat="server" Text='<%# Bind("TipoReg") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" EditImageUrl="~/Pages/images/edit_icon.gif"
                                        ButtonType="Image" CancelImageUrl="~/Pages/images/cancel_edit_icon.png" UpdateImageUrl="~/Pages/images/save_icon.png" />
                                    <asp:TemplateField HeaderText="Foto">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgBtn_foto" CommandArgument='<%# Bind("Id_rcompe") %>' runat="server"
                                                Height="32px" Width="53px" ImageUrl="~/Pages/ImgBooom/Fotografia1.5.png" OnClick="btn_img_buscar_detalle_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="GridHeader" BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="GridRow" BackColor="#EEEEEE" ForeColor="Black" />
                                <AlternatingRowStyle CssClass="GridAlternateRow" BackColor="#DCDCDC" />
                            </asp:GridView>
                        --%>
            </div>
            <%--<div>
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
            </div> --%>

          

    <div>
                <asp:Button ID="btn_view" runat="server" CssClass="alertas" Text="ocultar" Width="95px" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender_viewfoto" runat="server" DropShadow="True"
                    TargetControlID="btn_view" PopupControlID="panel_viewfoto" CancelControlID="ImageButtonCancel_viewfoto">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="panel_viewfoto" runat="server" style="display:none" BackColor="White"
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
                    </div>
                          <div align="center"  >
                                    <asp:imagebutton id="ImageButton3"
				runat="server" Width="50px" Height="50px" 
                onclick="ImageButton3_Click" ImageUrl="~/Pages/images/sub_black_rotate_ccw.ico"
                ></asp:imagebutton>
                            <asp:imagebutton id="ImageButton4" 
				runat="server" Width="50px" Height="50px" ImageUrl="~/Pages/images/sub_black_rotate_cw.ico" onclick="ImageButton4_Click" 
                ></asp:imagebutton>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Guardar<asp:ImageButton 
                                        ID="ibtnGuardarImagen" runat="server" ImageUrl="~/Pages/images/save_icon.png" onclick="ibtnGuardarImagen_Click"
                       />
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


     
    <asp:Panel ID="CrearReporCompetencia" runat="server" CssClass="busqueda" DefaultButton="btnGuardarReportCompetencia"  
                                Height="400px" Width="780px" style="display:none" >
    <table align="center">
        <caption>
            <br />
            <tr>
                <td>
                    <asp:Label ID="lblBGR" runat="server" CssClass="labelsTit2" 
                        Text="Crear Reporte de Competencia" />
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
    <td style="width:40px;"></td>
    <td><asp:Label ID="Label21" runat="server" CssClass="labels" Text="Grupo Objetivo:" /></td>
    <td><asp:DropDownList ID="ddlGrupoObj" runat="server" Width="205px"     ></asp:DropDownList></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="LblCanal" runat="server" CssClass="labels" Text="Campaña :" /></td><td>
    <asp:DropDownList runat="server" AutoPostBack="True" Width="205px" ID="ddlCampana" onselectedindexchanged="ddlCampana_SelectedIndexChanged" 
           ></asp:DropDownList></td>
                <td style="width:40px;"></td>
    <td><asp:Label ID="Label8" runat="server" CssClass="labels" Text="Precio Costo:" /></td>
    <td><asp:TextBox ID="txtPrecioCosto" runat="server" Width="205px"     ></asp:TextBox></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label19" runat="server" CssClass="labels" Text="Mercaderista :" /></td><td>
    <asp:DropDownList runat="server" Width="205px" ID="ddlMercaderista"  ></asp:DropDownList></td>
        <td style="width:40px;"></td>
    <td><asp:Label ID="Label9" runat="server" CssClass="labels" Text="Precio PVP:" /></td>
    <td><asp:TextBox ID="txtPrecioPVP" runat="server" Width="205px"     ></asp:TextBox></td>
    </tr>
        <tr>
    <td align="right"><asp:Label ID="Label7" runat="server" CssClass="labels" Text="Oficina :" /></td><td>
    <asp:DropDownList runat="server" Width="205px" ID="ddlOficina" 
                onselectedindexchanged="ddlOficina_SelectedIndexChanged"  ></asp:DropDownList></td>
        <td style="width:40px;"></td>
    <td><asp:Label ID="Label20" runat="server" CssClass="labels" Text="Fec.Ini Act:" /></td>
    <td><telerik:RadDateTimePicker ID="txtFecIniActividad" runat="server" Culture="es-PE"
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
    <td align="right"><asp:Label ID="Label6" runat="server" CssClass="labels" Text="Zona:" /></td><td>
        <asp:DropDownList ID="ddlNodeComercial" runat="server" Width="205px" 
            AutoPostBack="True" onselectedindexchanged="ddlNodeComercial_SelectedIndexChanged" 
             ></asp:DropDownList></td>
                <td style="width:40px;"></td>
    <td ><asp:Label ID="Label10" runat="server" CssClass="labels" Text="Fec.Fin Act:" /></td>
    <td><telerik:RadDateTimePicker ID="txtFecFinActividad" runat="server" Culture="es-PE"
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
    <td align="right"><asp:Label ID="Label13" runat="server" CssClass="labels" Text="Punto de venta:" /></td><td>
        <asp:DropDownList ID="ddlPuntoVenta" runat="server" Width="205px"     ></asp:DropDownList></td>
            <td style="width:40px;"></td>
    <td><asp:Label ID="Label11" runat="server" CssClass="labels" Text="Grp. Obj Comentario:" /></td>
    <td><asp:TextBox ID="txtGrupObjComen" runat="server" Width="205px"     ></asp:TextBox></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label1" runat="server" CssClass="labels" Text="Categoria:" /></td><td>
        <asp:DropDownList ID="ddlCategoria" runat="server" Width="205px" 
            
            AutoPostBack="True" 
            onselectedindexchanged="ddlCategoria_SelectedIndexChanged"     ></asp:DropDownList></td>
                <td style="width:40px;"></td>
    <td><asp:Label ID="Label14" runat="server" CssClass="labels" Text="Cant. de Personal:" /></td>
    <td><asp:TextBox ID="txtCantPersonal" runat="server" Width="205px"     ></asp:TextBox></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label2" runat="server" CssClass="labels" Text="Marca:" /></td><td>
        <asp:DropDownList ID="ddlMarca" runat="server" Width="205px" 
                ></asp:DropDownList></td>
                <td style="width:40px;"></td>
    <td ><asp:Label ID="Label15" runat="server" CssClass="labels" Text="Premio:" /></td>
    <td ><asp:TextBox ID="txtPremio" runat="server" Width="205px"     ></asp:TextBox></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label3" runat="server" CssClass="labels" Text="Tipo Promoción:" /></td>
    <td>
        <asp:DropDownList ID="ddlTipoProm" runat="server" Width="205px" 
              ></asp:DropDownList>
    </td>
              <td style="width:40px;"></td>
    <td ><asp:Label ID="Label16" runat="server" CssClass="labels" Text="Mecanica:" /></td>
    <td ><asp:TextBox ID="txtMecanica" runat="server" Width="205px"     ></asp:TextBox></td>
    </tr>
    <tr >
    <td align="right"><asp:Label ID="Label4" runat="server" CssClass="labels" Text="Tipo Actividad:" /></td><td><asp:DropDownList ID="ddlTipoAct" runat="server" Width="205px"     ></asp:DropDownList></td>
    <td style="width:40px;"></td>
    <td><asp:Label ID="Label17" runat="server" CssClass="labels" Text="Material de Apoyo:" /></td>
    <td><asp:TextBox ID="txtMatApoyo" runat="server" Width="205px"     ></asp:TextBox></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label5" runat="server" CssClass="labels" Text="POP:" /></td><td><asp:DropDownList ID="ddlPop" runat="server" Width="205px"     ></asp:DropDownList></td>
    <td style="width:40px;"></td>
    <td ><asp:Label ID="Label18" runat="server" CssClass="labels" Text="Observación:" /></td>
    <td ><asp:TextBox ID="txtObservacion" runat="server" Width="205px"  TextMode="MultiLine"    ></asp:TextBox></td>
    </tr>
        <tr>
    <td align="right"><asp:Label ID="Label22" runat="server" CssClass="labels" Text="Fec. Comunicación:" /></td><td><telerik:RadDateTimePicker ID="txtFecComunicacion" runat="server" Culture="es-PE"
                                Skin="Web20">
                                <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora">
                                </TimeView>
                                <TimePopupButton HoverImageUrl="" ImageUrl="" />
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                    Skin="Web20">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDateTimePicker></td>
    <td style="width:40px;"></td>
    <td ><asp:Label ID="Label23" runat="server" CssClass="labels" Text="Empresa:" /></td>
    <td ><asp:DropDownList ID="ddlEmpresa" runat="server" Width="205px"     ></asp:DropDownList></td>
    </tr>





        <caption>
            <br />
        </caption>
        </table><table align="center">
            <caption>
                <br />
                <tr>
                    <td>
                        <asp:Button ID="btnGuardarReportCompetencia" runat="server" CssClass="buttonPlan" 
                            Text="Guardar" Width="80px" onclick="btnGuardarReportCompetencia_Click"  />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="buttonPlan" 
                            Text="Cancelar" Width="80px" />
                    </td>
                </tr>
            </caption>
     </table>
     </asp:Panel>
     <cc1:ModalPopupExtender ID="MopoReporCompetencia" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="btnCancelar" PopupControlID="CrearReporCompetencia"
                                TargetControlID="BtnCrear" DynamicServicePath="">
     </cc1:ModalPopupExtender>




        </ContentTemplate>
    </asp:UpdatePanel>

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
                    ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" 
                    onclick="BtnclosePanel_Click"  /> 
            </div>
            <div align="center" style="font-family: verdana; font-size: medium; color: #005DA3;">
                <div>
                    Cambiar Foto</div>
                <br />
                <div align="center">
                    <input type="file" runat="server" id="inputFile" />
                    <asp:Button ID="buttonSubmit" runat="server" Text="Cargar" 
                        CssClass="RadUploadButton" onclick="buttonSubmit_Click" />
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
                    Guardar<asp:ImageButton ID="imgbtn_save" runat="server" 
                        ImageUrl="~/Pages/images/save_icon.png" onclick="imgbtn_save_Click"
                        />
                    &nbsp;Cancelar<asp:ImageButton ID="imgbtn_cancel" runat="server" 
                        ImageUrl="~/Pages/images/cancel_edit_icon.png" onclick="imgbtn_cancel_Click"
                         />
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
                <asp:GridView ID="gv_competenciaToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="ROWID" HeaderText="N°" ReadOnly="true" />
                        <asp:BoundField DataField="Name_Local" HeaderText="Dist" />
                        <asp:BoundField DataField="AgrComercial" HeaderText="Agr Comerc" />
                        <asp:BoundField DataField="Company_Name" HeaderText="Emp.Compet" />
                        <asp:BoundField DataField="ClientPDV_Code" HeaderText="Código PDV" />
                        <asp:BoundField DataField="Punto de venta" HeaderText="PDV" />
                        <asp:BoundField DataField="Categoria del producto" HeaderText="Categoria" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" />
                        <asp:BoundField DataField="Precio de costo" HeaderText="Prc.costo" />
                        <asp:BoundField DataField="Precio de punto de venta" HeaderText="Prc.PDV" />
                        <asp:BoundField DataField="Fecha inicio de actividad" HeaderText="Ini Act" />
                        <asp:BoundField DataField="Fecha fin de actividad" HeaderText="Fin Act" />
                        <asp:BoundField DataField="Cant_Personal" HeaderText="Cant personal" />
                        <asp:BoundField DataField="Premio" HeaderText="Premio" />
                        <asp:BoundField DataField="Mecanica" HeaderText="Mecanica" />
                        <asp:BoundField DataField="Observaciones" HeaderText="Obs" />
                        <asp:BoundField DataField="Tipo promocion" HeaderText="Tipo Promocion" />
                        <asp:BoundField DataField="Grupo objetivo" HeaderText="Grp Obj" />
                        <asp:BoundField DataField="GrObjAdc" HeaderText="Grp Obj Adicional" />
                        <asp:BoundField DataField="Material de apoyo" HeaderText="POP" />
                        <asp:BoundField DataField="Descripcion del material de apoyo" HeaderText="Descp POP" />
                        <asp:BoundField DataField="PopAdc" HeaderText="POP Adicional" />
                        <asp:BoundField DataField="Person_name" HeaderText="Registrado por" ReadOnly="true" />
                        <asp:BoundField DataField="Fec_Reg_Bd" HeaderText="Fec Reg" />
                        <asp:BoundField DataField="ModiBy" HeaderText="Modificado por" ReadOnly="true" />
                        <asp:BoundField DataField="DateModiBy" HeaderText="Modifico en" ReadOnly="true" />
                        <asp:CheckBoxField DataField="Validado" HeaderText="Validado" />
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
        .style3
        {
            text-align: center;
            width: 350px;
            font-weight: bold;
        }
        .style6
        {
            text-align: left;
            }
        .class_div 
        {
        	 overflow-x: scroll; 
        	 background-color:white;  	 
        }
        .style9
        {
            text-align: center;
            width: 531px;
            font-weight: bold;
        }
        .style10
        {
            text-align: left;
            width: 835px;
        }
        .style13
        {
            text-align: right;
            width: 401px;
        }
        .style14
        {
            text-align: left;
            width: 531px;
        }
        .style15
        {
            text-align: center;
            width: 134px;
            font-weight: bold;
        }
        .style17
        {
            text-align: left;
            width: 134px;
        }
    </style>
</asp:Content>
