<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master" CodeBehind="Report_Spot_SF_M.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Spot_SF_M" %>


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
                        REPORTE SPOT
                    </td>
                </tr>
            </table>

            <fieldset    style="width:850px"  >
            <legend  > Consultar Reporte Spot</legend>

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
             <td  align="right">Mes :</td>
           <td><asp:DropDownList ID="cmbMes" runat="server" Height="25px" Width="275px"
                            Style="text-align: left" CausesValidation="True" 
                            >
                        </asp:DropDownList></td>
           <td  align="right"><asp:Label Visible="false" runat="server" ID="lblComentario" >Competencia :</asp:Label></td>
           <td>
               <asp:DropDownList ID="cmbCompetencia" runat="server" Visible="false"
                    Height="25px" 
                  Width="275px">
               </asp:DropDownList>
                             </td>
            </tr>
             <tr>
             <td  align="right">Canal :</td>
           <td><asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="275px" Style="text-align: left">
                        </asp:DropDownList></td>
           <td  align="right">Marca :</td>
           <td><asp:DropDownList ID="cmbMarca" runat="server" Enabled="False" Height="25px"
                            Width="275px" 
                            >
                        </asp:DropDownList></td>
            </tr>
             <tr>
           <td  align="right"> Campaña:</td>
           <td><asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged" Width="275px" CausesValidation="True"
                            Enabled="False">
                        </asp:DropDownList></td>
           <td  align="right">Actividad :</td>
           <td><asp:DropDownList ID="cmbActividad" runat="server" Enabled="False" Height="25px"
                            Width="275px">
                        </asp:DropDownList></td>
            </tr>
             <tr>
           <td  align="right">Cadena :</td>
           <td><asp:DropDownList ID="cmbCadena" runat="server" Height="25px" Width="275px" Style="text-align: left"
                            CausesValidation="True" Enabled="False">
                        </asp:DropDownList></td>
           <td  align="right">&nbsp;</td>
           <td>&nbsp;</td>
            </tr>
             <tr>
           <td  align="right">Categoria :</td>
           <td><asp:DropDownList ID="cmbCategoria" runat="server" Enabled="False" 
                   Height="25px" Width="275px" AutoPostBack="True" onselectedindexchanged="cmbCategoria_SelectedIndexChanged"
                            >
                        </asp:DropDownList></td>
           <td  align="right">&nbsp;</td>
           <td>&nbsp;</td>
            </tr>
            <tr>
           <td  align="right">Familia :</td>
           <td><asp:DropDownList ID="cmbFamilia" runat="server" Enabled="False" Height="25px" Width="275px"
                            >
                        </asp:DropDownList></td>
           <td  align="right">&nbsp;</td>
           <td>&nbsp;</td>
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


                                    <fieldset style="width:850px" >
            <legend  > Crear Reporte Spot</legend>
            <table align="center" style="height:25px">
            <tr>
            <td style="height:25px">
            <asp:Button ID="BtnCrear" runat="server" CssClass="buttonGreen" Height="25px" 
                            Text="Crear uno a uno" Width="164px" onclick="BtnCrear_Click" />
            </td>
                        <td style="height:25px">
            <asp:Button ID="btnMasiva" runat="server" CssClass="buttonGreen" Height="25px" 
                            Text="Carga Masiva" Width="164px" onclick="btnMasiva_Click"  />
            </td>
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
                    OnDataBound="gv_Foto_DataBound" OnItemCommand="gv_Foto_ItemCommand">
                    <MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E"
                        AlternatingItemStyle-BackColor="#F7F7F7" Font-Size="Smaller" AlternatingItemStyle-ForeColor="#333333">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Mes" HeaderText="Mes" UniqueName="Mes"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Cadena" HeaderText="Cadena" UniqueName="Cadena"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Tienda" HeaderText="Tienda" UniqueName="Tienda"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Categoria" HeaderText="Categoria" UniqueName="Categoria"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Familia" HeaderText="Familia" UniqueName="Familia"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Variedad" HeaderText="Variedad" UniqueName="Variedad"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="Competencia" HeaderText="Competencia" UniqueName="Competencia"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                              <telerik:GridBoundColumn DataField="Empresa" HeaderText="Empresa" UniqueName="Empresa"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Marca" HeaderText="Marca" UniqueName="Marca"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Actividad" HeaderText="Actividad" UniqueName="Actividad"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                              <telerik:GridBoundColumn DataField="Grupo_Objetivo" HeaderText="Grupo Objetivo" UniqueName="Grupo_Objetivo"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="Inicio" HeaderText="Inicio" UniqueName="Inicio"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="Termino" HeaderText="Termino" UniqueName="Termino"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Mecanica" HeaderText="Mecanica" UniqueName="Mecanica"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Normal" HeaderText="Normal" UniqueName="Normal"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="Oferta" HeaderText="Oferta" UniqueName="Oferta"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn HeaderText="Validado" UniqueName="TemplateColumn" ReadOnly="true">
                                <HeaderTemplate>
                                 
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("Validado")%>' />
                                    <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_Id_Rep" runat="server" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridEditCommandColumn Visible="false" ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
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

    <!---------------------------------------------------------------------------------------------------------------------------------------->
    <!---------------------------------------------------------------------------------------------------------------------------------------->
    <!---------------------------------------------------------------------------------------------------------------------------------------->
    <!-----------------------------******************** CREAR REPORTE ************************------------------------------------------------>
    <!---------------------------------------------------------------------------------------------------------------------------------------->
    <!---------------------------------------------------------------------------------------------------------------------------------------->
    <!---------------------------------------------------------------------------------------------------------------------------------------->

     <div>
               <asp:Panel ID="CrearReport"  style="display:none" runat="server" CssClass="busqueda" DefaultButton="btnGuardarReporteFotografico"  
                                Height="600px" Width="780px" >
    <table align="center">
        <caption>
            <br />
            <tr>
                <td>
                    <asp:Label ID="lblBGR" runat="server" CssClass="labelsTit2" 
                        Text="Crear Reporte SPOT" />
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
    <td align="right"><asp:Label ID="Label7" runat="server" CssClass="labels" Text="Cadena :" /></td><td>
    <asp:DropDownList runat="server" Width="205px" ID="ddlCadena" 
                
                AutoPostBack="True" 
                onselectedindexchanged="ddlCadena_SelectedIndexChanged"  ></asp:DropDownList></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label6" runat="server" CssClass="labels" Text="Tiendas:" /></td><td>
        <asp:DropDownList ID="ddlPDV" runat="server" Width="205px" 
           
             ></asp:DropDownList></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label13" runat="server" CssClass="labels" Text="Categoria:" /></td><td>
        <asp:DropDownList ID="ddlCategoria" runat="server" Width="205px" 
            AutoPostBack="True" 
            onselectedindexchanged="ddlCategoria_SelectedIndexChanged"     ></asp:DropDownList></td>
    </tr>
     <tr>
    <td align="right"><asp:Label ID="Label1" runat="server" CssClass="labels" Text="Familia:" /></td><td>
        <asp:DropDownList ID="ddlFamilia" runat="server" Width="205px" 
             AutoPostBack="True" 
             onselectedindexchanged="ddlFamilia_SelectedIndexChanged"     ></asp:DropDownList></td>
    </tr>
        <tr>
    <td align="right"><asp:Label ID="Label8" runat="server" CssClass="labels" Text="Variedad:" /></td><td>
        <asp:DropDownList ID="ddlSubfamilia" runat="server" Width="205px"     ></asp:DropDownList></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label9" runat="server" CssClass="labels" Text="Competencia:" /></td><td>
        <asp:DropDownList ID="ddlTipoEmpresa" runat="server" Width="205px" 
            AutoPostBack="True" 
            onselectedindexchanged="ddlTipoEmpresa_SelectedIndexChanged"     >
            <asp:ListItem Value="0">---Sleccionar---</asp:ListItem>
            <asp:ListItem Value="2">San Fernando</asp:ListItem>
            <asp:ListItem Value="3">Competencia</asp:ListItem>
        </asp:DropDownList></td>
    </tr>
            <tr>
    <td align="right"><asp:Label ID="Label10" runat="server" CssClass="labels" Text="Empresa:" /></td><td>
        <asp:DropDownList ID="ddlEmpresa" runat="server" Width="205px" AutoPostBack="True" 
                    onselectedindexchanged="ddlEmpresa_SelectedIndexChanged"     ></asp:DropDownList></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label2" runat="server" CssClass="labels" Text="Marca:" /></td><td>
        <asp:DropDownList ID="ddlMarca" runat="server" Width="205px" 
                ></asp:DropDownList></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label5" runat="server" CssClass="labels" Text="Actividad:" /></td><td>
        <asp:DropDownList ID="ddlTipoActividad" runat="server" Width="205px" 
                ></asp:DropDownList></td>
    </tr>
        <tr>
    <td align="right"><asp:Label ID="Label3" runat="server" CssClass="labels" Text="Grupo Objetivo:" /></td><td>
            <asp:DropDownList ID="ddlGrupoObjetivo" runat="server" Width="205px" 
                ></asp:DropDownList></td>
    </tr>
   <tr>
    <td align="right"><asp:Label ID="Label11" runat="server" CssClass="labels" Text="Mecanica:" /></td><td>
            <asp:TextBox ID="txtMecanica" runat="server" Width="205px" 
                ></asp:TextBox></td>
    </tr>
            <tr>
    <td align="right"><asp:Label ID="Label14" runat="server" CssClass="labels" Text="Precio Normal:" /></td><td>
            <asp:TextBox ID="txtPrecioNormal" runat="server" Width="205px" 
                ></asp:TextBox></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label15" runat="server" CssClass="labels" Text="Precio Oferta:" /></td><td>
            <asp:TextBox ID="txtPrecioOferta" runat="server" Width="205px" 
                ></asp:TextBox></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label16" runat="server" CssClass="labels" Text="Fecha Inicio:" /></td><td>
            <telerik:RadDateTimePicker ID="rdtFechaini" runat="server" Culture="es-PE"
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
    <td align="right"><asp:Label ID="Label4" runat="server" CssClass="labels" Text="Fecha Fin:" /></td><td>
            <telerik:RadDateTimePicker ID="rdtFechafin" runat="server" Culture="es-PE"
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
     <cc1:ModalPopupExtender ID="MopoReport" runat="server" 
                                DropShadow="True" Enabled="True"  PopupControlID="CrearReport"
                                TargetControlID="Button1" >
     </cc1:ModalPopupExtender>

     <asp:Button ID="Button1" runat="server" CssClass="alertas" Text="ocultar"
            Width="95px" />
    </div>
    
     <!-----------------------------******************** FIN CREAR REPORTE ************************------------------------------------------------>

     <div>
               <asp:Panel ID="pCarga" style="display:none"  runat="server" CssClass="busqueda" DefaultButton="btnGuardarReporteFotografico"  
                                Height="200px" Width="780px" >
    <table align="center">
        <caption>
            <br />
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" CssClass="labelsTit2" 
                        Text="Carga Masiva Reporte SPOT" />
                </td>
            </tr>
        </caption>
    </table>
    <table align="center">
    <tr>
    <td align="right"><asp:Label ID="Label19" runat="server" CssClass="labels" Text="Canal :" /></td>
    <td><asp:DropDownList ID="ddlCargaMasiva_Canal" runat="server" Width="232px" 
            AutoPostBack="True" onselectedindexchanged="ddlCargaMasiva_Canal_SelectedIndexChanged" 
             ></asp:DropDownList>
    </td>
    </tr>
        <tr>
    <td align="right"><asp:Label ID="Label20" runat="server" CssClass="labels" Text="Campaña :" /></td><td>
    <asp:DropDownList runat="server" Width="232px" ID="ddlCargaMasiva_Campaña" 
           ></asp:DropDownList></td>
                
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label18" runat="server" CssClass="labels" Text="Archivo :" /></td>
    <td><asp:FileUpload ID="FileUpReport" runat="server"  />
    </td>
    </tr>
        <tr>
        <td></td>
    <td  align="center" >

    <a  class="button" href="masivo/formato_reporte_spot.xls"><span>
        <div >
            Descargar Formato</div>
        </span></a>
        
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
                        <asp:Button ID="Button2" runat="server" CssClass="buttonPlan" 
                            Text="Cargar" Width="80px" onclick="Button2_Click" 
                             />
                        <asp:Button ID="Button3" runat="server" CssClass="buttonPlan" 
                            Text="Cancelar" Width="80px" />
                    </td>
                </tr>
            </caption>
     </table>
     </asp:Panel>
     <cc1:ModalPopupExtender ID="mopoCargaMasiva" runat="server" 
                                DropShadow="True" Enabled="True"  PopupControlID="pCarga"
                                TargetControlID="Button4" >
     </cc1:ModalPopupExtender>

     <asp:Button ID="Button4" runat="server" CssClass="alertas" Text="ocultar"
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

