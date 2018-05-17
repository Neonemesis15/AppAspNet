<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_Ingresos-Stock_SF_M.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Ingresos_Stock_SF_M" %>

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
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>


            <div align="center">


                <table style="width: 100%; height: auto;" align="center" class="art-Block-body">
                <tr>
                    <td class="style3" >
                        REPORTE DE INGRESOS - STOCK SAN FERNANDO
                    </td>
                </tr>
            </table>
                
                <fieldset    style="width:850px"  >
            <legend  > Consultar Reporte Ingresos - Stock </legend>
                

                 <table>
                <tr>
                    <td  align="right">
                        Fecha de Inicio:
                    </td>
                    <td align="left">
                        <telerik:RadDateTimePicker ID="txt_fecha_inicio" runat="server" Culture="es-PE" Skin="Web20"
                            Visible="true">
                            <TimeView CellSpacing="-1" Culture="es-PE" HeaderText="Hora">
                            </TimeView>
                            <TimePopupButton HoverImageUrl="" ImageUrl="" />
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Web20">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDateTimePicker></td>
                        <td  align="right">
                            Fecha de Fin :</td>
                        <td align="left"><telerik:RadDateTimePicker ID="txt_fecha_fin" runat="server" Culture="es-PE" Skin="Web20"
                            Visible="true">
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
                        Canal :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbcanal" runat="server" runat="server" AutoPostBack="True"
                            Enabled="true" Height="25px" Width="275px" OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>

                    <td align="right">
                        Categoria de Producto :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbcategoria_producto" runat="server" AutoPostBack="True" Enabled="False"
                            Height="25px" Width="275px" OnSelectedIndexChanged="cmbcategoria_producto_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Campaña :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Enabled="False"
                            Height="25px" Width="275px" OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Marca :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbmarca" runat="server" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Cadena :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbcorporacion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbcorporacion_SelectedIndexChanged"
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Supervisor :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbperson" runat="server" Enabled="False" Height="25px" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Ciudad :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbOficina" runat="server" Enabled="False" Height="25px" Width="275px"
                            AutoPostBack="true" OnSelectedIndexChanged="cmbOficina_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbfamilia" runat="server" Visible="false" AutoPostBack="True"
                            OnSelectedIndexChanged="cmbfamilia_SelectedIndexChanged" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Cliente :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbNodeComercial" runat="server" AutoPostBack="true" Enabled="False"
                            Height="25px" OnSelectedIndexChanged="cmbNodeComercial_SelectedIndexChanged"
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                     <td align="right">
                        
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbsubfamilia" runat="server" Width="275px" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Punto de Venta :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmbPuntoDeVenta" runat="server" Enabled="False" Height="25px"
                            Width="275px">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan=4>
                        <asp:Button ID="btn_buscar" runat="server"  CssClass="buttonRed" Height="25px"
                            OnClick="btn_buscar_Click" Text="Buscar" Width="164px" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                    </td>
                    <td ><asp:Label ID="lblmensaje" runat="server" Style="text-align: left"></asp:Label>
                    </td>
                    <td></td>
                    <td></td>
                </tr>                
            </table>
                             </fieldset>
            
            <fieldset style="width:850px" >
            <legend  > Crear Reporte Ingresos - Stock</legend>
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





           




            <div id="div_IngresosStock" runat="server" class="class_div" style="width: auto;
                height: auto;">
                <div align="right">
                <asp:Label ID="lbl_cb_all" runat="server" Text="Seleccionar todos" Font-Size="Small"></asp:Label>
                        <asp:CheckBox ID="cb_all"  runat="server" 
                            Font-Size="Small" oncheckedchanged="cb_all_CheckedChanged" 
                            AutoPostBack="True" />
                </div>
                <table>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gv_stock" runat="server" AutoGenerateColumns="False" PageSize="30"
                                Skin="Vista" Font-Size="Small" AllowPaging="True" GridLines="None" ShowFooter="True"
                                OnCancelCommand="gv_precios_CancelCommand" OnDataBound="gv_precios_DataBound"
                                OnEditCommand="gv_precios_EditCommand" OnPageIndexChanged="gv_precios_PageIndexChanged"
                                OnPageSizeChanged="gv_precios_PageSizeChanged" OnPdfExporting="gv_precios_PdfExporting"
                                OnUpdateCommand="gv_precios_UpdateCommand">
                                <MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E"
                                    AlternatingItemStyle-BackColor="#F7F7F7" Font-Size="Smaller" AlternatingItemStyle-ForeColor="#333333">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="corporacion" HeaderText="Cadena" UniqueName="corporacion"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ciudad" HeaderText="Ciudad" UniqueName="ciudad"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente" UniqueName="cliente"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="codigoPDV" HeaderText="Codigo PDV" UniqueName="codigoPDV"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="puntoventa" HeaderText="PDV" UniqueName="puntoventa"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="marca" HeaderText="Marca" UniqueName="marca"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="categoria" HeaderText="Categoria" UniqueName="categoria"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="familia" HeaderText="Familia" UniqueName="familia"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="subfamilia" HeaderText="Subfamilia" UniqueName="subfamilia"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="sku" HeaderText="SKU" UniqueName="sku" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="producto" HeaderText="Producto" UniqueName="producto"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="unidadmedida" HeaderText="Unidad de Medida" UniqueName="unidadmedida"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ffvv" HeaderText="FFVV" UniqueName="ffvv" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="supervisor" HeaderText="Supervisor" UniqueName="supervisor"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Opcion" HeaderText="Opcion" UniqueName="Opcion" 
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Exhibicion" HeaderText="Exhibicion" UniqueName="Exhibicion" 
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Camara" HeaderText="Camara" UniqueName="Camara" 
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="stock_inicial" HeaderText="Stock Inicial" UniqueName="stock_inicial" EmptyDataText="no registró"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="ingreso" DataType="System.Double" EmptyDataText="No registró"
                                            HeaderText="Ingresos" UniqueName="ingreso">
                                        </telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn DataField="Total" HeaderText="Total" UniqueName="Total" 
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="stock_final" DataType="System.Double" EmptyDataText="No registró"
                                            HeaderText="Stock Final" UniqueName="stock_final">
                                        </telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn DataField="ventas" HeaderText="Ventas" UniqueName="ventas"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="mercaderista" HeaderText="Mercaderista:" UniqueName="mercaderista"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="fecharegistro_ini" HeaderText="Fecha Reg. Stock Inicial:" UniqueName="fecharegistro_ini"
                                            ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="fecharegistro_fin" HeaderText="Fecha Reg. Stock Fin:"
                                            UniqueName="fecharegistro_fin" PickerType="DateTimePicker">
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="modiby" HeaderText="Modificado por:"
                                            UniqueName="modiby" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="datemodiby" HeaderText="Fecha de Modificación:"
                                            UniqueName="datemodiby" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridTemplateColumn HeaderText="Hola">
                                <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtingresos" Text='<%#Bind("ingresos") %>'></asp:TextBox>
                                </EditItemTemplate>
                                </telerik:GridTemplateColumn>--%>
                                        <%--<telerik:GridBoundColumn DataField="ventas" HeaderText="Ventas" UniqueName="ventas"
                                    ReadOnly="true">
                                </telerik:GridBoundColumn>--%>
                                        <%--<telerik:GridBoundColumn DataField="mercaderista" HeaderText="Generador" UniqueName="mercaderista"
                                    ReadOnly="true">
                                </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                            <HeaderTemplate>
                                                &nbsp;<asp:Button ID="btn_validar_Click" OnClientClick="return confirm('¿Esta seguro de invalidar los registros?');"
                                                    runat="server" Text="Invalidar" OnClick="btn_validar_Click_Click" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            
                                                <asp:CheckBox ID="cb_validar" Checked='<%# Eval("validado")%>' Enabled='<%# Eval("habilitado") %>'
                                                    runat="server" />
                                                <asp:Label ID="lbl_validar" runat="server" Visible="true"></asp:Label>
                                                <asp:Label ID="lblregstockfinaldet" Text='<%#Eval("id_rstkd") %>' runat="server"
                                                    Visible="false"></asp:Label>
                                                <%--<asp:Label ID="lblregingresodet" Text='<%#Eval("idregingresodet") %>' runat="server"
                                                    Visible="false"></asp:Label>--%>
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
            &nbsp;
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
                <asp:GridView ID="gv_stockToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False" AutoGenerateColumns="false" ForeColor="#333333">
                    <PagerStyle CssClass="pager-row" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle CssClass="row" BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerSettings PageButtonCount="7" FirstPageText="«" LastPageText="»" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="corporacion" HeaderText="Corporacion" />
                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                        <asp:BoundField DataField="cliente" HeaderText="Cliente" />
                        <asp:BoundField DataField="codigoPDV" HeaderText="Codigo del PDV" />
                        <asp:BoundField DataField="puntoventa" HeaderText="PDV" />
                        <asp:BoundField DataField="marca" HeaderText="Marca" />
                        <asp:BoundField DataField="categoria" HeaderText="Categoría" />
                        <asp:BoundField DataField="familia" HeaderText="Familia" />
                        <asp:BoundField DataField="subfamilia" HeaderText="SubFamilia" />
                        <asp:BoundField DataField="sku" HeaderText="SKU" />
                        <asp:BoundField DataField="producto" HeaderText="Producto" />
                        <asp:BoundField DataField="unidadMedida" HeaderText="Unidad de Medida" />
                        <asp:BoundField DataField="ffvv" HeaderText="FFVV" />
                        <asp:BoundField DataField="supervisor" HeaderText="Supervisor" />
                        <asp:BoundField DataField="stockinicial" HeaderText="Stock Inicial" />
                        <asp:BoundField DataField="ingresos" HeaderText="Ingresos" />
                        <asp:BoundField DataField="stockfinal" HeaderText="Stock Final" />
                        <asp:BoundField DataField="ventas" HeaderText="Ventas" />
                        <asp:BoundField DataField="mercaderista" HeaderText="Mercaderista" />
                        <asp:BoundField DataField="fecharegistrostockfinal" HeaderText="Fecha de Registro Stock" />
                        <asp:BoundField DataField="modificadopor" HeaderText="Modificado por" />
                        <asp:BoundField DataField="fechamodificacion" HeaderText="Fecha de modificacion" />
                        
                    </Columns>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <%--<FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />--%>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" ForeColor="#333333">
                    <PagerStyle CssClass="pager-row" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle CssClass="row" BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerSettings PageButtonCount="7" FirstPageText="«" LastPageText="»" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                 <%--<FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />--%>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txt_fecha_inicio">
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>


    <!---------------------------------------------------------------------------------------------------------------------------------------->
    <!---------------------------------------------------------------------------------------------------------------------------------------->
    <!---------------------------------------------------------------------------------------------------------------------------------------->
    <!-----------------------------******************** CREAR REPORTE ************************------------------------------------------------>
    <!---------------------------------------------------------------------------------------------------------------------------------------->
    <!---------------------------------------------------------------------------------------------------------------------------------------->
    <!---------------------------------------------------------------------------------------------------------------------------------------->

     <div>
 <asp:Panel ID="CrearReporStock" runat="server" CssClass="busqueda" DefaultButton="btnGuardarReportStock"  
                                Height="400px" Width="780px" style="display:none" >
    <table align="center">
        <caption>
            <br />
            <tr>
                <td>
                    <asp:Label ID="lblBGR" runat="server" CssClass="labelsTit2" 
                        Text="Crear Reporte de Stock" />
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
                onselectedindexchanged="ddlOficina_SelectedIndexChanged"  ></asp:DropDownList></td>
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
    <td align="right"><asp:Label ID="Label2" runat="server" CssClass="labels" Text="Familia:" /></td><td>
        <asp:DropDownList ID="ddlFamilia" runat="server" Width="205px" 
                ></asp:DropDownList></td>
    </tr>
        <tr>
    <td align="right"><asp:Label ID="Label3" runat="server" CssClass="labels" Text="Cantidad:" /></td><td>
        <asp:TextBox ID="txtCantidad" runat="server"  Width="205px"></asp:TextBox></td>
    </tr>
          <tr>
    <td align="right"><asp:Label ID="Label4" runat="server" CssClass="labels" Text="Observación:" /></td><td>
        <asp:TextBox ID="txtObservacion" runat="server" Width="205px"></asp:TextBox></td>
    </tr>


        <caption>
            <br />
        </caption>
        </table><table align="center">
            <caption>
                <br />
                <tr>
                    <td>
                        <asp:Button ID="btnGuardarReportStock" runat="server" CssClass="buttonPlan" 
                            Text="Guardar" Width="80px" onclick="btnGuardarReportStock_Click"  />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="buttonPlan" 
                            Text="Cancelar" Width="80px" />
                    </td>
                </tr>
            </caption>
     </table>
     </asp:Panel>
     <cc1:ModalPopupExtender ID="MopoReporStock" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="btnCancelar" PopupControlID="CrearReporStock"
                                TargetControlID="Button1" DynamicServicePath="">
     </cc1:ModalPopupExtender>
     <asp:Button ID="Button1" runat="server" CssClass="alertas" Text="ocultar"
            Width="95px" />
    </div>
    
     <!-----------------------------******************** FIN CREAR REPORTE ************************------------------------------------------------>



</asp:Content>
<%--<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style1
        {
            width: 195px;
        }
    </style>
</asp:Content>--%>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style2
        {
            text-align: right;
            width: 153px;
        }
        .style3
        {
            text-align: center;
            width: 350px;
            font-weight: bold;
        }        
        .style6
        {
            text-align: left;
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
