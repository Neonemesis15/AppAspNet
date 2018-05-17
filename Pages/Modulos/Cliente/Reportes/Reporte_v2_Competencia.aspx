<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_Competencia.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Competencia"
    ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte de Competencia
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="script1" ContentPlaceHolderID="ScriptIncludePlaceHolder" runat="server">

</asp:Content>
<%--<asp:Content ID="SideBar1" ContentPlaceHolderID="Sidebar1ContentPlaceHolder" runat="Server">
    <art:DefaultSidebar1 ID="DefaultSidebar1Content" runat="server" />
</asp:Content>--%>
<%@ Register Src="Informe_de_Competencia/Reporte_v2_Competencia_ResumenEjecutivo.ascx"
    TagName="Reporte_v2_Competencia_ResumenEjecutivo" TagPrefix="uc3" %>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <%-- Start body--%>
    <%-- Divide en dos partes -  1.-Definido como el updatePanel --%>
    <div id="Titulo_reporte_competencia" style="text-align: left;">
        <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
            color: #D01887;">
            REPORTE DE COMPETENCIA
        </div>
    </div>
    <asp:Accordion ID="MyAccordion" runat="server" SelectedIndex="1" HeaderCssClass="accordionHeader"
        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
        AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
        <Panes>
            <asp:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                <Header>
                    <div id="Div1" style="text-align: right">
                        <button class="buttonOcultar">
                            Ampliar Vista</button>
                    </div>
                </Header>
                <Content>
                    <div id="oculta" style="text-align: right">
                        <%--<asp:Button ID="btn_ocultar" runat="server" OnClick="btn_ocultar_Click" Text="Filtros"
                    CssClass="buttonOcultar" Height="16px" Width="63px" />--%>
                    </div>
                    <div id="Div_filtros" runat="server" align="center" style="width: 100%;" visible="true">
                        <asp:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                                <HeaderTemplate>
                                    Personalizado
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpFiltrosCompetencia" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table style="width: auto; height: auto;" align="center" class="art-Block-body">
                                                <tr>
                                                    <td align="right">
                                                        Año
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="cmb_año" runat="server" Height="22px" Width="200px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="cmb_año_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lbl_Mes" Text="Mes" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="cmb_mes" runat="server" Height="19px" Width="200px" Visible="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        Cobertura
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="cmb_oficina" CssClass="AjaxToolkitStyle" runat="server" Height="19px"
                                                            Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        Actividad
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="cmb_actividad" runat="server" Height="19px" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        Categoria
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="cmb_categoria" runat="server" Height="19px" Width="200px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="cmb_categoria_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">--Todos--</asp:ListItem>
                                                            <asp:ListItem Value="01">Exhibicion</asp:ListItem>
                                                            <asp:ListItem Value="02">Visibilidad</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <div align="center">
                                                <asp:Button ID="btn_buscar" runat="server" Text="Buscar" OnClick="btn_buscar_Click" />
                                            </div>
                                            <div align="left">
                                                Guardar consulta<asp:ImageButton ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/qryguardar.png"
                                                    Height="32px" Width="33px" />
                                                <asp:ModalPopupExtender ID="ModalPopupExtender_agregar" runat="server" BackgroundCssClass="modalBackground"
                                                    DropShadow="True" Enabled="True" PopupControlID="Panel_parametros" TargetControlID="btn_img_add"
                                                    CancelControlID="BtnclosePanel" DynamicServicePath="">
                                                </asp:ModalPopupExtender>
                                                <asp:Panel ID="Panel_parametros" runat="server" BackColor="White" BorderColor="#0099CB"
                                                    BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="150px"
                                                    Width="400px" Style="display: none;">
                                                    <div>
                                                        <asp:ImageButton ID="BtnclosePanel" runat="server" BackColor="Transparent" Height="22px"
                                                            ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                                                    </div>
                                                    <div align="center">
                                                        <div style="font-family: verdana; font-size: medium; color: #D01887;">
                                                            Nueva consulta</div>
                                                        <br />
                                                        Descripción :<asp:TextBox ID="txt_descripcion_parametros" runat="server" Width="300px"></asp:TextBox>
                                                        <br></br>
                                                        <asp:Button ID="btn_guardar_parametros" runat="server" Text="Guardar" CssClass="buttonGuardar"
                                                            OnClick="buttonGuardar_Click" Height="25px" Width="164px" />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <div aling="center">
                                                <p style="font-weight: bold; font-family: Tahoma;">
                                                    Categoria :<asp:Label ID="lbl_categoria" runat="server" Visible="False"></asp:Label>
                                            </div>
                                            </p>
                                            <p style="font-weight: bold; font-family: Tahoma;">
                                                <asp:Label ID="lbl_msj" runat="server" Visible="False"></asp:Label>
                                            </p>
                                            <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3"
                                                AssociatedUpdatePanelID="UpFiltrosCompetencia" BackgroundCssClass="modalProgressGreyBackground">
                                                <ProgressTemplate>
                                                    <div>
                                                        Cargando...
                                                        <img alt="Procesando" src="../../../images/loading.gif" style="vertical-align: middle" />
                                                    </div>
                                                </ProgressTemplate>
                                            </cc2:ModalUpdateProgress>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                                <HeaderTemplate>
                                    Mis Favoritos
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div id="div_parametro" align="left">
                                        Agregar<asp:ImageButton ID="btn_imb_tab" runat="server" Height="16px" ImageUrl="~/Pages/images/add.png"
                                            Width="16px" OnClick="btn_imb_tab_Click" />
                                        <telerik:RadGrid ID="RadGrid_parametros" runat="server" AutoGenerateColumns="False"
                                            GridLines="None" OnItemCommand="RadGrid_parametros_ItemCommand" Skin="">
                                            <MasterTableView>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="column">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_id" runat="server" Visible="False" Text='<%# Bind("id") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_servicio" runat="server" Visible="False" Text='<%# Bind("id_servicio") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_canal" runat="server" Visible="False" Text='<%# Bind("id_canal") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_company" runat="server" Visible="False" Text='<%# Bind("id_compañia") %>'> </asp:Label>
                                                            <asp:Label ID="lbl_id_reporte" runat="server" Visible="False" Text='<%# Bind("id_reporte") %>'> </asp:Label>
                                                            <asp:Label ID="lbl_id_user" runat="server" Visible="False" Text='<%# Bind("id_user") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_oficina" runat="server" Visible="False" Text='<%# Bind("id_oficina") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_categoria" runat="server" Visible="False" Text='<%# Bind("id_producto_categoria") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_año" runat="server" Visible="False" Text='<%# Bind("id_año") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_mes" runat="server" Visible="False" Text='<%# Bind("id_mes") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_tipoactividad" runat="server" Visible="False" Text='<%# Bind("id_tipoactividad") %>'></asp:Label>
                                                            <asp:Label ID="lbl_fecha_inicio" runat="server" Visible="False" Text='<%# Bind("fecha_inicio") %>'></asp:Label>
                                                            <asp:Label ID="lbl_fecha_fin" runat="server" Visible="False" Text='<%# Bind("fecha_fin") %>'></asp:Label>
                                                            <asp:ImageButton ID="btn_img_buscar" runat="server" ImageUrl="~/Pages/img/Qrys_File.png"
                                                                OnClick="btn_img_buscar_Click" Height="28px" Width="26px" CommandName="BUSCAR" />
                                                            <asp:ImageButton ID="btn_img_eliminar" runat="server" ImageUrl="~/Pages/images/delete.png"
                                                                CommandName="ELIMINAR" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </div>
                </Content>
            </asp:AccordionPane>
        </Panes>
        <%-- <HeaderTemplate>
            close
        </HeaderTemplate>
        <ContentTemplate>
        </ContentTemplate>--%>
    </asp:Accordion>
    &nbsp;<%-- 2.-Definido fuera del UpdatePanel   --%><div id="div_rportes" runat="server"
        align="center" style="width: 100%;">
        &nbsp;<asp:TabContainer ID="TabContainer_Reporte_Competencia" runat="server" CssClass="magenta"
            ActiveTabIndex="0" ScrollBars="Horizontal">
            <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel3">
                <HeaderTemplate>
                    Resume Ejecutivo</HeaderTemplate>
                <ContentTemplate>
                    <uc3:Reporte_v2_Competencia_ResumenEjecutivo ID="Reporte_v2_Competencia_ResumenEjecutivo1"
                        runat="server" />
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel4">
                <HeaderTemplate>
                    Actividades de la Competencia</HeaderTemplate>
                <ContentTemplate>
                    <div>
                        <div class="grid" style="width: 100%;">
                            <div class="rounded">
                                <div class="top-outer">
                                    <div class="top-inner">
                                        <div class="top">
                                            <h2>
                                                Informe de Competencia</h2>
                                        </div>
                                    </div>
                                </div>
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gv_competenciaV2" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" EnableModelValidation="True" OnPageIndexChanging="gv_competenciaV2_PageIndexChanging"
                                                        CssClass="datatable" OnSorting="gv_competenciaV2_Sorting" GridLines="Vertical">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Distrito" DataField="Name_Local" Visible="false" SortExpression="Name_Local">
                                                                <HeaderStyle CssClass="first" />
                                                                <ItemStyle CssClass="first" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Empresa" DataField="Company_Name" SortExpression="Company_Name" />
                                                            <asp:BoundField HeaderText="Marca" DataField="Name_Brand" SortExpression="Name_Brand" />
                                                            <asp:BoundField HeaderText="Agr Comercial" DataField="agrupacionComercial" SortExpression="agrupacionComercial" />
                                                            <asp:BoundField HeaderText="Prec PVP" DataField="Precio_PVP" SortExpression="Precio_PVP"
                                                                Visible="false" DataFormatString="{0:c}">
                                                                <ItemStyle CssClass="money" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Precio Costo" DataField="Precio_Costo" SortExpression="Precio_Costo"
                                                                Visible="false" DataFormatString="{0:c}">
                                                                <ItemStyle CssClass="money" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Grupo Objetivo" DataField="TargetGroup" SortExpression="TargetGroup"
                                                                Visible="false" />
                                                            <asp:BoundField HeaderText="Actividad" DataField="Actividad" SortExpression="Actividad" />
                                                            <asp:BoundField HeaderText="Mecanica" DataField="Mecanica" SortExpression="Mecanica" />
                                                            <asp:BoundField HeaderText="Tipo Prom" DataField="tipo_promocion" SortExpression="tipo_promocion"
                                                                Visible="false" />
                                                            <asp:BoundField HeaderText="Inicio Act" DataField="Fec_Ini_Act" SortExpression="Fec_Ini_Act" />
                                                            <asp:BoundField HeaderText="Fin Act" DataField="Fec_Fin_Act" SortExpression="Fec_Fin_Act" />
                                                            <asp:BoundField HeaderText="Cantidad Personal" DataField="Cant_Personal" SortExpression="Cant_Personal"
                                                                Visible="false" />
                                                            <asp:BoundField HeaderText="Premio" DataField="Premio" SortExpression="Premio" Visible="false" />
                                                            <asp:BoundField HeaderText="Material de Apoyo" DataField="Mat_Apoyo" SortExpression="Mat_Apoyo" />
                                                            <asp:BoundField HeaderText="Observaciones" DataField="Observaciones" SortExpression="Observaciones" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btn_img_buscar_detalle" runat="server" CommandArgument='<%# Bind("Id_rcompe") %>'
                                                                        ImageUrl="~/Pages/images/add.png" OnClick="btn_img_buscar_detalle_Click" /></ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pager-row" />
                                                        <RowStyle CssClass="row" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="bottom-outer">
                                    <div class="bottom-inner">
                                        <div class="bottom">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:ImageButton ID="btn_img_toExcel" runat="server" OnClick="btn_img_toExcel_Click"
                            Height="27px" ImageUrl="~/Pages/Modulos/Operativo/Reports/Image/icono_excel.jpg"
                            Width="28px" />Exportar a Excel</div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
        &nbsp;</div>
    <asp:Button ID="btn_popup_ocultar" runat="server" CssClass="alertas" Text="ocultar"
        Width="95px" />
    &nbsp;<asp:ModalPopupExtender ID="ModalPopupExtender_detalle" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="True" Enabled="True" PopupControlID="Panel_DetalleCompetencia" TargetControlID="btn_popup_ocultar">
    </asp:ModalPopupExtender>
    &nbsp;<asp:Panel ID="Panel_DetalleCompetencia" runat="server" ScrollBars="Auto" BackColor="#D8D8DA"
        Style="display: none">
        <asp:UpdatePanel ID="UpdatePanel_foto_detalle" runat="server" ChildrenAsTriggers="true"
            RenderMode="Inline">
            <ContentTemplate>
                <div id="foto" class="grid" style="width: auto;">
                    <div class="rounded">
                        <div class="top-outer">
                            <div class="top-inner">
                                <div class="top">
                                    <h2>
                                        Foto
                                    </h2>
                                </div>
                            </div>
                        </div>
                        <div class="mid-outer">
                            <table style="width: 100%; background-color: #FFFFFF;">
                                <tr>
                                    <td colspan="2" align="right" style="width: 100%;">
                                        <asp:ImageButton ID="btn_cerrar_popup" runat="server" OnClick="btn_cerrar_popup_Click"
                                            BackColor="Transparent" ImageAlign="Right" Height="22px" ImageUrl="~/Pages/ImgBooom/salir.png"
                                            Width="23px" />
                                    </td>
                                </tr>
                            </table>
                            <div class="mid-inner">
                                <div class="mid">
                                    <!-- El contenido va aqui! -->
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Image ID="foto_url" runat="server" Height="300px" Width="500px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="lbl_foto_comentario" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="lbl_foto_fecha" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="bottom-outer">
                            <div class="bottom-inner">
                                <div class="bottom">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="detalle_gridview" class="grid" style="width: auto;">
                    <div class="rounded">
                        <div class="top-outer">
                            <div class="top-inner">
                                <div class="top">
                                    <h2>
                                        Detalle
                                    </h2>
                                </div>
                            </div>
                        </div>
                        <div class="mid-outer">
                            <div class="mid-inner">
                                <div class="mid">
                                    <!-- El contenido va aqui! -->
                                    <asp:GridView ID="gv_detalle_competencia" runat="server" AutoGenerateColumns="false"
                                        AllowPaging="true" AllowSorting="true" CssClass="datatable" CellPadding="0" CellSpacing="0"
                                        BorderWidth="0" GridLines="None" PageSize="5">
                                        <PagerStyle CssClass="pager-row" />
                                        <RowStyle CssClass="row" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="3" FirstPageText="«" LastPageText="»" />
                                        <Columns>
                                            <asp:BoundField HeaderText="POP" DataField="POP_name" SortExpression="Name_Local"
                                                HeaderStyle-CssClass="first" ItemStyle-CssClass="first" />
                                            <asp:BoundField HeaderText="Descripcion" DataField="POP_description" SortExpression="POP_description" />
                                            <asp:BoundField HeaderText="Tipo" DataField="TipoMaDescripcion" SortExpression="TipoMaDescripcion"
                                                DataFormatString="{0:c}" ItemStyle-CssClass="money" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="bottom-outer">
                            <div class="bottom-inner">
                                <div class="bottom">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    &nbsp;<div id="Div_gv_competencia_to_excel" runat="server" visible="false" class="grid"
        style="width: auto;">
        <asp:GridView ID="gv_competencia_to_excel" runat="server" AutoGenerateColumns="False"
            CssClass="datatable" CellPadding="4" GridLines="None" EnableModelValidation="True"
            ForeColor="#333333">
            <PagerStyle CssClass="pager-row" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle CssClass="row" BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerSettings PageButtonCount="7" FirstPageText="«" LastPageText="»" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField HeaderText="Distrito" DataField="Name_Local" SortExpression="Name_Local">
                    <HeaderStyle CssClass="first" />
                    <ItemStyle CssClass="first" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Agr Comercial" DataField="agrupacionComercial" SortExpression="agrupacionComercial" />
                <asp:BoundField HeaderText="Empresa" DataField="Company_Name" SortExpression="Company_Name" />
                <asp:BoundField HeaderText="Marca" DataField="Name_Brand" SortExpression="Name_Brand" />
                <asp:BoundField HeaderText="Prec PVP" DataField="Precio_PVP" SortExpression="Precio_PVP"
                    DataFormatString="{0:c}">
                    <ItemStyle CssClass="money" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Precio Costo" DataField="Precio_Costo" SortExpression="Precio_Costo"
                    DataFormatString="{0:c}">
                    <ItemStyle CssClass="money" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Grupo Objetivo" DataField="TargetGroup" SortExpression="TargetGroup" />
                <asp:BoundField HeaderText="Actividad" DataField="Actividad" SortExpression="Actividad" />
                <asp:BoundField HeaderText="Tipo Prom" DataField="tipo_promocion" SortExpression="tipo_promocion" />
                <asp:BoundField HeaderText="Inicio Act" DataField="Fec_Ini_Act" SortExpression="Fec_Ini_Act" />
                <asp:BoundField HeaderText="Fin Act" DataField="Fec_Fin_Act" SortExpression="Fec_Fin_Act" />
                <asp:BoundField HeaderText="Cantidad Personal" DataField="Cant_Personal" SortExpression="Cant_Personal" />
                <asp:BoundField HeaderText="Premio" DataField="Premio" SortExpression="Premio" />
                <asp:BoundField HeaderText="Mecanica" DataField="Mecanica" SortExpression="Mecanica" />
                <asp:BoundField HeaderText="Observaciones" DataField="Observaciones" SortExpression="Observaciones" />
                <asp:BoundField HeaderText="Mat Apoyo" DataField="Mat_Apoyo" SortExpression="Mat_Apoyo" />
            </Columns>
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
        &nbsp;</div>
    <%-- </ContentTemplate>
 
                            </asp:UpdatePanel>--%>
    &nbsp;</cc1:accordion>
</asp:Content>
