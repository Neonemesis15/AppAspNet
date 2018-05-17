<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_Competencia_SF_M_V_1_1.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Reporte_Competencia_SF_M_V_1_1" %>

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
            <table style="width: 100%; height: auto;" align="center" class="style1">
                <tr>
                    <td colspan="4" align="center">
                         </center> <b>REPORTE DE COMPETENCIA</center></b> </tr> <b></tr> </b>
                    </td>
                </tr>
                <%-- filtros de Fecha --%>
                 <tr>
                     <td align="right" >
                         Fecha :
                     </td>
                     <td  colspan="3" align="left">
                         Desde:<telerik:RadDateTimePicker ID="txt_fecha_inicio" runat="server" 
                             Culture="es-PE" Skin="Web20">
                             <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora">
                             </TimeView>
                             <TimePopupButton HoverImageUrl="" ImageUrl="" />
                             <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False" 
                                 UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                             </Calendar>
                             <DatePopupButton HoverImageUrl="" ImageUrl="" />
                         </telerik:RadDateTimePicker>
                         &nbsp;Hasta :<telerik:RadDateTimePicker ID="txt_fecha_fin" runat="server" 
                             Culture="es-PE" Skin="Web20">
                             <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora">
                             </TimeView>
                             <TimePopupButton HoverImageUrl="" ImageUrl="" />
                             <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False" 
                                 UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                             </Calendar>
                             <DatePopupButton HoverImageUrl="" ImageUrl="" />
                         </telerik:RadDateTimePicker>
                     </td>
                </tr>
                
                <tr>
                    <%-- filtro de Canal --%>
                    <td align="right" >
                        Canal :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" 
                            Height="25px" OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" 
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                    <%-- filtro de Campaña --%>
                    <td align="right" >
                        Campaña:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" 
                            Enabled="False" Height="25px" 
                            OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>


                <tr>
                    <%-- filtro de Cadena --%>
                    <td align="right">
                        Cadena :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbcorporacion" runat="server" AutoPostBack="True" 
                            OnSelectedIndexChanged="cmbcorporacion_SelectedIndexChanged" Width="275px">
                        </asp:DropDownList>
                    </td>
                    <%-- filtro de Oficina --%>
                    <td align="right">
                        Oficina :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbOficina" runat="server" AutoPostBack="true" 
                            Enabled="False" Height="25px" 
                            OnSelectedIndexChanged="cmbOficina_SelectedIndexChanged" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <%-- filtro de Cliente --%>
                    <td align="right" >
                        Cliente :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbNodeComercial" runat="server" AutoPostBack="true" 
                            Enabled="False" Height="25px" 
                            OnSelectedIndexChanged="cmbNodeComercial_SelectedIndexChanged" Width="275px">
                        </asp:DropDownList>
                    </td>
                    <%-- filtro de Punto de Venta --%>
                    <td align="right" >
                        Punto de venta :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbPuntoDeVenta" runat="server" Enabled="False" 
                            Height="25px" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <%-- filtro de Categoria --%>
                    <td align="right">
                        Categoria del producto:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbcategoria_producto" runat="server" AutoPostBack="True" 
                            Enabled="False" Height="25px" 
                            OnSelectedIndexChanged="cmbcategoria_producto_SelectedIndexChanged" 
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                    <%-- filtro de Empresa Competidora --%>
                    <td align="right" >
                        Empresa:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbempresa_competidora" runat="server" 
                            AutoPostBack="True" Enabled="False" Height="25px" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <%-- filtro de Marca de la Competencia --%>
                    <td align="right" >
                        Marca :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbmarca" runat="server" Enabled="False" Height="25px" 
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                    <%-- filtro de Tipo de Actividad --%>
                    <td align="right" >
                        Actividad :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbTipoActividad" runat="server" AutoPostBack="True" 
                            Enabled="False" Height="25px" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <%-- Grupo Objetivo --%>
                    <td align="right" >
                        Grupo Objetivo :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbGrupoObjetivo" runat="server" Enabled="False" 
                            Height="25px" Width="275px">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>

                <tr>
                    <%-- Inicio de Actividad --%>
                    <td align="right" >
                        <asp:Label ID="lblInicioActividad" runat="server" Text="Inicio de Actividad:" 
                            Visible="False"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                            Enabled="False" Height="25px" Visible="False" Width="275px">
                        </asp:DropDownList>
                    </td>
                    <%-- Fin de Actividad --%>
                    <td align="right" >
                        <asp:Label ID="lblFinActividad" runat="server" Text="Fin de Actividad :" 
                            Visible="False"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                            Enabled="False" Height="25px" Visible="False" Width="275px">
                        </asp:DropDownList>
                    </td>
                    <tr>
                        <td >
                        </td>
                        <td >
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                    <%-- Mercaderista --%>
                        <td align="right">
                            <asp:Label ID="lblPerson" runat="server" Text="Mercaderista :" Visible="false"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="cmbperson" runat="server" Enabled="False" Height="25px" 
                                Visible="False" Width="275px">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;
                        </td>
                        <td >
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;
                        </td>
                        <td >
                            <div>
                                <asp:Button ID="btn_buscar" runat="server" CssClass="button" Height="25px" 
                                    OnClick="btn_buscar_Click" Text="Buscar" Width="164px" />
                            </div>
                            
                        </td>
                        <td >
                            &nbsp;
                        </td>
                        <td >
                            <asp:Button ID="btnCrear" runat="server" CssClass="buttonBlue" Height="25px" 
                                Text="Crear" Width="164px" />
                        </td>
                    </tr>
                    <tr>
                    <td colspan="4">
                    <div>
                                <asp:Label ID="lblmensaje" runat="server" Style="text-align: left" 
                                    Visible="False"></asp:Label>
                            </div>
                    </td>
                    </tr>
                </tr>
                </tr>

            </table>
            </div>
            <%-- Inicio del GridView de Competencia --%>

            <div id="div_gvCompetencia" runat="server" class="class_div" style="width: auto; height: auto;">
                <!-- inicio del grid_div -->
                
                <!-- boton para validar todo -->
                <div id="div_Validar" align="right" >
                    <asp:Label ID="lbl_cb_all" runat="server" Text="Seleccionar todos" Visible="false"></asp:Label>
                    <asp:CheckBox ID="cb_all" Visible="false" runat="server" AutoPostBack="True" OnCheckedChanged="cb_all_CheckedChanged" />
                    <asp:Button ID="btn_validar" runat="server" OnClientClick="return confirm('¿Esta seguro de Validar los registros?');"
                        Text="Validar" CssClass="button" OnClick="btn_validar_Click" />
                    <caption>
                        <br />
                    </caption>
                </div>
                <!-- fin boton validar todo -->

                
                <%-- Inicio del RadCodeBlock JavaScript
                     bloque de código JavaScript para el Documento de JavaScript --%>
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
                            popUp.style.top = ((gridHeight - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
                        }
                    </script>
                </telerik:RadCodeBlock>
                <%-- Fin del RadCodeBlock JavaScript --%>

               
                <!-- inicia el RadGrid para competencia -->
                <telerik:RadGrid ID="rgv_competencia" 
                runat="server" 
                AutoGenerateColumns="False"
                Skin="Vista" 
                Font-Size="Small" 
                AllowPaging="True" 
                GridLines="Both" 
                ShowFooter="True" 
                    OnDataBound="rgv_competencia_DataBound" 
                    OnItemCommand="rgv_competencia_ItemCommand"
                    OnCancelCommand="rgv_competencia_CancelCommand" 
                    OnEditCommand="rgv_competencia_EditCommand"
                    OnPageIndexChanged="rgv_competencia_PageIndexChanged" 
                    OnPageSizeChanged="rgv_competencia_PageSizeChanged"
                    OnUpdateCommand="rgv_competencia_UpdateCommand" 
                    PageSize="2000" >
                    <MasterTableView 
                        NoMasterRecordsText="Sin Datos para mostrar."
                        AlternatingItemStyle-BackColor="#F7F7F7" 
                        Font-Size="Smaller" AlternatingItemStyle-ForeColor="#333333"
                        EditMode="PopUp" 
                        Width="100%">
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
                            <telerik:GridBoundColumn DataField="corporacion" HeaderText="Cadena" ReadOnly="true"
                                UniqueName="corporacion">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="oficina" HeaderText="Ciudad" ReadOnly="true"
                                UniqueName="oficina">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="nodocomercial" HeaderText="Cliente" ReadOnly="true"
                                UniqueName="nodocomercial">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="codigoPDV" HeaderText="Código PDV" ReadOnly="true"
                                UniqueName="codigoPDV">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="pdv" HeaderText="Punto de Venta" ReadOnly="true"
                                UniqueName="pdv">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="competidora" HeaderText="Empresa" ReadOnly="true"
                                UniqueName="competidora">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Marca" HeaderText="Marca" UniqueName="Marca"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="categoria" HeaderText="Categoria" ReadOnly="true"
                                UniqueName="categoria">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="actividad" HeaderText="Nombre de Actividad" UniqueName="actividad"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="grupoobjetivo" HeaderText="Grupo Objetivo" UniqueName="grupoobjetivo"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="promocionini" UniqueName="promocionini" HeaderText="Inicio de Actividad"
                                PickerType="DatePicker">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn DataField="promocionfin" UniqueName="promocionfin" HeaderText="Fin de Actividad"
                                PickerType="DatePicker">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="mecanica" HeaderText="Mecanica" UniqueName="mecanica"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn DataField="precioregular" DataType="System.Double" EmptyDataText="NULO"
                                HeaderText="Precio Regular" UniqueName="precioregular">
                            </telerik:GridNumericColumn>

                            <telerik:GridNumericColumn DataField="preciooferta" DataType="System.Double" EmptyDataText="NULO"
                                HeaderText="Precio Oferta" UniqueName="preciooferta">
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn DataField="supervisor" HeaderText="Supervisor" UniqueName="supervisor"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="mercaderista" HeaderText="Mercaderista" UniqueName="mercaderista"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="fec_comunicacion" HeaderText="Fecha de comunicación"
                                UniqueName="fec_comunicacion" ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" UniqueName="Observaciones"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="mercaderista" HeaderText="Registrado por" ReadOnly="true"
                                UniqueName="mercaderista">
                            </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="Fec_Reg_Bd" HeaderText="Fecha de registro" ReadOnly="false"
                                UniqueName="Fec_Reg_Bd">
                             </telerik:GridBoundColumn>
                        --%>
                        
                            <telerik:GridDateTimeColumn DataField="Fec_Reg_Bd" UniqueName="Fec_Reg_Bd" HeaderText="Fecha de registro"
                                PickerType="DateTimePicker" ReadOnly="false">
                            </telerik:GridDateTimeColumn>
                            
                            <telerik:GridBoundColumn DataField="ModiBy" HeaderText="Modificado por" ReadOnly="true"
                                UniqueName="ModiBy">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DateModiBy" HeaderText="Modifico en" ReadOnly="true"
                                UniqueName="DateModiBy">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Validar" UniqueName="Validado">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("Validado")%>' />
                                    <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_id_reg_competencia" Visible="false" runat="server" Text='<%# Bind("Id_rcompe") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png">
                            </telerik:GridEditCommandColumn>

                        </Columns>
                       
                        <EditFormSettings>
                            <EditColumn UniqueName="EditCommandColumn1" ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png">
                            </EditColumn>
                        </EditFormSettings>
                        <AlternatingItemStyle BackColor="#F7F7F7" ForeColor="#333333" />
                    </MasterTableView>
                    <HeaderStyle Width="120px" BorderStyle="Solid" />
                    <ClientSettings>
                        <ClientEvents OnPopUpShowing="PopUpShowing" />
                    </ClientSettings>
                </telerik:RadGrid>
                <!-- finaliza el RadGrid para competencia -->
      
            </div>
       
            
            <%-- Bloque para el Panel de la Fotografía 
            se crea dentro de un div 
                utilizando un ModalPopupExtender
            --%>
            <%-- Declaración del Div --%>
            <div id="divFotografia">
                <%-- Declaración del Boton--%>
                <asp:Button ID="btn_view" runat="server" CssClass="alertas" Text="ocultar" Width="95px" />
                <%-- Declaración del ModalPopPup para mostrar la Foto--%>
                <cc1:ModalPopupExtender ID="ModalPopupExtender_viewfoto" runat="server" DropShadow="True"
                    TargetControlID="btn_view" PopupControlID="panel_viewfoto" CancelControlID="ImageButtonCancel_viewfoto">
                </cc1:ModalPopupExtender>
                <%-- comienzo del Panel--%>
                <asp:Panel ID="panel_viewfoto" runat="server" BackColor="White" BorderColor="#AEDEF9"
                    BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="450px"
                    Width="590px" style="display:none">
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
                                Height="400px" AutoAdjustImageControlSize="False" AlternateText="Subir foto"
                                GenerateEmptyAlternateText="true" />
                        </div>
                    </div>
                </asp:Panel>
                <%-- Fin del Panel --%>
            </div>
            

            <%-- Bloque del UpdatePanel del ProgressBar --%>
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
            

            <%-- Panel para crear un Nuevo Reporte de Competencia --%>
            <asp:Panel ID="CrearReporCompetencia" runat="server" CssClass="busqueda" DefaultButton="btnGuardarReportCompetencia"
                Height="400px" Width="780px" style="display:none">
                <table align="center">
                    <caption>
                        <br />
                        <tr>
                            <td>
                                <asp:Label ID="lblBGR" runat="server" CssClass="labelsTit2" Text="Crear Reporte de Competencia" />
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
                            <asp:DropDownList ID="ddlCanal" runat="server" Width="205px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlCanal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" CssClass="labels" Text="Grupo Objetivo:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGrupoObj" runat="server" Width="205px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
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
                            <asp:Label ID="Label8" runat="server" CssClass="labels" Text="Precio Costo:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrecioCosto" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label19" runat="server" CssClass="labels" Text="Mercaderista :" />
                        </td>
                        <td>
                            <asp:DropDownList runat="server" Width="205px" ID="ddlMercaderista">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" CssClass="labels" Text="Precio PVP:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrecioPVP" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" CssClass="labels" Text="Oficina :" />
                        </td>
                        <td>
                            <asp:DropDownList runat="server" Width="205px" ID="ddlOficina" OnSelectedIndexChanged="ddlOficina_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="Label20" runat="server" CssClass="labels" Text="Fec.Ini Act:" />
                        </td>
                        <td>
                            <telerik:RadDateTimePicker ID="txtFecIniActividad" runat="server" Culture="es-PE"
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
                        <td align="right">
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
                            <asp:Label ID="Label10" runat="server" CssClass="labels" Text="Fec.Fin Act:" />
                        </td>
                        <td>
                            <telerik:RadDateTimePicker ID="txtFecFinActividad" runat="server" Culture="es-PE"
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
                        <td align="right">
                            <asp:Label ID="Label13" runat="server" CssClass="labels" Text="Punto de venta:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPuntoVenta" runat="server" Width="205px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="Label11" runat="server" CssClass="labels" Text="Grp. Obj Comentario:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtGrupObjComen" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label1" runat="server" CssClass="labels" Text="Categoria:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCategoria" runat="server" Width="205px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" CssClass="labels" Text="Cant. de Personal:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtCantPersonal" runat="server" Width="205px"></asp:TextBox>
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
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="Label15" runat="server" CssClass="labels" Text="Premio:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPremio" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" CssClass="labels" Text="Tipo Promoción:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipoProm" runat="server" Width="205px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="Label16" runat="server" CssClass="labels" Text="Mecanica:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtMecanica" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label4" runat="server" CssClass="labels" Text="Tipo Actividad:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipoAct" runat="server" Width="205px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="Label17" runat="server" CssClass="labels" Text="Material de Apoyo:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtMatApoyo" runat="server" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label5" runat="server" CssClass="labels" Text="POP:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPop" runat="server" Width="205px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="Label18" runat="server" CssClass="labels" Text="Observación:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtObservacion" runat="server" Width="205px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label22" runat="server" CssClass="labels" Text="Fec. Comunicación:" />
                        </td>
                        <td>
                            <telerik:RadDateTimePicker ID="txtFecComunicacion" runat="server" Culture="es-PE"
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
                        <td style="width: 40px;">
                        </td>
                        <td>
                            <asp:Label ID="Label23" runat="server" CssClass="labels" Text="Empresa:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmpresa" runat="server" Width="205px">
                            </asp:DropDownList>
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
                                <asp:Button ID="btnGuardarReportCompetencia" runat="server" CssClass="buttonPlan"
                                    Text="Guardar" Width="80px" OnClick="btnGuardarReportCompetencia_Click" />
                                <asp:Button ID="btnCancelar" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" />
                            </td>
                        </tr>
                    </caption>
                </table>
            </asp:Panel>
            
            <%-- ModalPopPup para el Reporte de Competencia --%>
            <cc1:ModalPopupExtender ID="MopoReporCompetencia" runat="server" BackgroundCssClass="modalBackground"
                DropShadow="True" Enabled="True" OkControlID="btnCancelar" PopupControlID="CrearReporCompetencia"
                TargetControlID="BtnCrear" DynamicServicePath="">
            </cc1:ModalPopupExtender>


        </ContentTemplate>
    </asp:UpdatePanel>
    
    <%-- Panel para mostrar la fotografía empieza aquí con un DIV--%>
    <div>
        <asp:Button ID="btn_popup_ocultar" runat="server" CssClass="alertas" Text="ocultar"
            Width="95px" />
        
        <%-- popPup para Ver las Fotos
        1.-Tenemos que crear un ModalPopPupExtender
        Entre sus propiedades esta:
        TargerControlID - para indicar cual es el botón que le dará acción
        PopupConrolID   - indica cual es el panel que se activará al activar el evento con el botón
         --%>
        <cc1:ModalPopupExtender 
        ID="ModalPopup_Edit" 
        runat="server" 
        DropShadow="True" 
        TargetControlID="btn_popup_ocultar"
        PopupControlID="panelEdit">
        </cc1:ModalPopupExtender>
        
        
        <%-- Panel para Editar Fotografía --%>
        <asp:Panel ID="panelEdit" runat="server" Style="display: none;" BackColor="White"
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
                    <asp:Button ID="buttonSubmit" runat="server" Text="Cargar" CssClass="RadUploadButton"
                        OnClick="buttonSubmit_Click" />
                    <telerik:RadProgressManager ID="RadProgressManager1" runat="server" ClientIDMode="AutoID" />
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


    <table id="tblExportarExcel" style="width: 100%;">
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
                
                <%-- Define un GridView para Exportar la Data a Excel --%>
                <asp:GridView ID="gv_competenciaToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False" AutoGenerateColumns="false" ForeColor="#333333">
                     <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="ROWID" HeaderText="N°" ReadOnly="true" />
                        <asp:BoundField DataField="corporacion" HeaderText="Cadena" />
                        <asp:BoundField DataField="oficina" HeaderText="Ciudad" />
                        <asp:BoundField DataField="nodocomercial" HeaderText="Cliente" />
                        <asp:BoundField DataField="codigoPDV" HeaderText="Código PDV" />
                        <asp:BoundField DataField="pdv" HeaderText="Punto de Venta" />
                        <asp:BoundField DataField="competidora" HeaderText="Empresa Competidora" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca Competidora" />
                        <asp:BoundField DataField="categoria" HeaderText="Categoria" />
                        <asp:BoundField DataField="actividad" HeaderText="Nombre de Actividad" />
                        <asp:BoundField DataField="grupoobjetivo" HeaderText="Grupo Objetivo" />
                        <asp:BoundField DataField="promocionini" HeaderText="Inicio de Actividad" />
                        <asp:BoundField DataField="promocionfin" HeaderText="Fin de Actividad" />
                        <asp:BoundField DataField="mecanica" HeaderText="mecanica" />
                        <asp:BoundField DataField="precioregular" HeaderText="Precio Regular" />
                        <asp:BoundField DataField="preciooferta" HeaderText="Precio Oferta" />
                        <asp:BoundField DataField="supervisor" HeaderText="Supervisor" />
                        <asp:BoundField DataField="mercaderista" HeaderText="Mercaderista" />
                        <asp:BoundField DataField="fec_comunicacion" HeaderText="Fecha de comunicación" />
                        <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
                        <%--<asp:BoundField DataField="mercaderista" HeaderText="Registrado por" />--%>
                        <asp:BoundField DataField="Fec_Reg_Bd" HeaderText="Fecha de registro" />
                        <asp:BoundField DataField="ModiBy" HeaderText="Modificado por" ReadOnly="true" />
                        <asp:BoundField DataField="DateModiBy" HeaderText="Modifico en" />
                        <asp:BoundField DataField="Validado" HeaderText="Validado" ReadOnly="true" />
                        <%--<asp:BoundField DataField="DateModiBy" HeaderText="Modifico en" ReadOnly="true" />--%>
                        <%--<asp:CheckBoxField DataField="Validado" HeaderText="Validado" />--%>
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
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
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

<%-- Estilos Utilizaodos en la página --%>
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
            background-color: white;
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
            width: 229px;
            font-weight: bold;
        }
        .style16
        {
            text-align: left;
            width: 229px;
        }
    </style>
</asp:Content>
