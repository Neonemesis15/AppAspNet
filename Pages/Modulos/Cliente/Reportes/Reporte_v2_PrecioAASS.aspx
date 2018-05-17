<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master" AutoEventWireup="true" CodeBehind="Reporte_v2_PrecioAASS.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_PrecioAASS" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%-- Referencias de master--%>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<%-- end de master--%>
<%-- referencias a Informes--%>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_InformePrecioAASS"  Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio/Reporte_v2_Precio_InformePrecioAASS.ascx"%>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_InformePrecioAASSCad"  Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio/Reporte_v2_Precio_InformePrecioAASSCad.ascx"%>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Precio_InformePrecioAASSG"  Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Precio/Reporte_v2_Precio_InformePrecioAASSG.ascx" %>
 
<%-- end referecias Informes--%>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte de Precios
</asp:Content>



<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<%--<asp:Content ID="SideBar1" ContentPlaceHolderID="Sidebar1ContentPlaceHolder" runat="Server">
    <art:DefaultSidebar1 ID="DefaultSidebar1Content" runat="server" />
</asp:Content>--%>
<asp:Content ID="Scripts" ContentPlaceHolderID="ScriptIncludePlaceHolder" runat="server">
<script src="../../../js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#P23487a1f09544c628cfd77f79ceedadb_1_oReportCell table").css('margin', 'auto');
        });  
    </script>
<style type="text/css" >
.combo-item-template input,
.combo-item-template label
{
    vertical-align:middle;
}

.combo-item-template img
{
    vertical-align:top;
}

.example-panel .rent-button
{
    float:right;
    width: 73px;
    height:21px;
    background: transparent url('Img/button.png') no-repeat 0 0;
    text-decoration:none;
    color: #000;
    text-align:center;
    line-height:21px;
    margin: 18px 30px 0 auto;
}</style>
<script type="text/javascript">
        //<![CDATA[
    var cancelDropDownClosing = false;

    function pageLoad() {
        $telerik.$('#cmb_skuProducto .rcbList li').click(function (e) {
            e.cancelBubble = true;
            if (e.stopPropagation) {
                e.stopPropagation();
            }
        });

        var combo = $find("<%= cmb_skuProducto.ClientID %>");
        $get(combo.get_id() + "_i0_lbl1").innerHTML = "Seleccionar todos";
        check_checkeds(combo);
    }

    function StopPropagation(e) {
        e.cancelBubble = true;
        if (e.stopPropagation)
            e.stopPropagation();
    }

    function onBlur(sender) {
        cancelFirstCombo = false;
    }

    function onDropDownClosing(sender, args) {
        cancelDropDownClosing = false;
    }

    function onchkallClick(estado) {
        var combo = $find("<%= cmb_skuProducto.ClientID %>");
        var items = combo.get_items();
        for (var i = 1; i < items.get_count(); i++) {
            var chk1 = $get(combo.get_id() + "_i" + i + "_chk1");
            chk1.checked = estado;
        }
    }

    function onCheckBoxClick(chk) {
        var combo = $find("<%= cmb_skuProducto.ClientID %>");

        if (chk.id == combo.get_id() + "_i0_chk1") {
            onchkallClick(chk.checked);
        }

        var text = "";
        check_checkeds(combo);
    }

    function check_checkeds(combo) {
        var count = 0;
        var items = combo.get_items();
        for (var i = 0; i < items.get_count(); i++) {
            var chk1 = $get(combo.get_id() + "_i" + i + "_chk1");
            if (chk1.checked) {
                count++;
            }
        }

        if (count !== 1) {
            text = count + " elementos seleccionados";
        }
        else {
            text = count + " elemento seleccionado";
        }

        combo.set_text(text);
    }

    function OnClientNodeClickingHandler(sender, e) {
        var node = e.get_node();
        if (node.get_category() == "Make") {
            node.toggle();
        }
        else {
            var combo = $find("<%= cmb_skuProducto.ClientID %>");
            combo.set_text(node.get_text());
            cancelDropDownClosing = false;
            combo.hideDropDown();
        }
    }

    function OnClientDropDownClosingHandler(sender, e) {
        e.set_cancel(cancelDropDownClosing);
    }
            //]]>
    </script>
</asp:Content>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>
    <%-- Start body--%>
   
            <%--<asp:Panel ID="Panel_filtros" runat="server" BackColor="White" BorderColor="#E46322"
                BorderWidth="3px" Style="display: block; width: 100;" BorderStyle="Solid">--%>
           
              <asp:accordion id="MyAccordion" runat="server" selectedindex="1" headercssclass="accordionHeader"
        headerselectedcssclass="accordionHeaderSelected" contentcssclass="accordionContent"
        autosize="None" fadetransitions="true" transitionduration="250" framespersecond="40"
        requireopenedpane="false" suppressheaderpostbacks="true">
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
            <div id="Div_filtros" runat="server" align="center" style="width: auto;" visible="true">
                <asp:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                    <asp:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Personalizado
                        </HeaderTemplate>
                        <ContentTemplate>
                         <asp:UpdatePanel ID="UpFiltrosPrecios1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: auto; height: auto; margin:auto" class="art-Block-body">
                                <tr>
                                    <td style="text-align:right;">
                                        Año
                                    </td>
                                    <td style="text-align:left;">
                                        <telerik:RadComboBox ID="cmb_año" runat="server" Width="238px"
                                             Skin="Vista">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="text-align:right;">
                                       Negocio
                                    </td>
                                    <td style="text-align:left;">
                                        <telerik:RadComboBox ID="cmb_negocio" runat="server" Width="300px" OnSelectedIndexChanged="cmb_negocio_SelectedIndexChanged" 
                                            AutoPostBack="True" Skin="Vista">
                                        </telerik:RadComboBox>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td style="text-align:right;">
                                        Mes
                                    </td>
                                    <td style="text-align:left;">
                                        <telerik:RadComboBox ID="cmb_mes" runat="server" Width="238px" OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged"
                                            Skin="Vista" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="text-align:right;">
                                      Categoria
                                    </td>
                                    <td style="text-align:left;">
                                        <telerik:RadComboBox ID="cmb_categoria" runat="server" Width="300px" OnSelectedIndexChanged="cmb_categoria_SelectedIndexChanged"
                                            AutoPostBack="True" Skin="Vista">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:right;">
                                        Periodo
                                    </td>
                                    <td style="text-align:left;">
                                        <telerik:RadComboBox ID="cmb_periodo" runat="server" Width="238px" AutoPostBack="False" Skin="Vista">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="text-align:right;">
                                       Cadenas
                                    </td>
                                    <td style="text-align:left;">
                                        <telerik:RadComboBox ID="cmb_cadena" runat="server" Width="300px" AutoPostBack="False" Skin="Vista">
                                        </telerik:RadComboBox>
                                    </td>                                   
                                </tr>
                                <tr>
                                    <td style="text-align:right;">
                                    </td>
                                    <td style="text-align:left;">
                                    </td>
                                    <td style="text-align:right;">
                                    Productos
                                    </td>
                                    <td style="text-align:left;">        
                                        <telerik:RadComboBox ID="cmb_skuProducto" runat="server" Width="300px" Skin="Vista" 
                                                       DataValueField="cod_Product" DataTextField="Name_Product" EmptyMessage="--Seleccione--" HighlightTemplatedItems="true"
                                                       AllowCustomText="true" OnClientDropDownClosing="onDropDownClosing" OnClientBlur="onBlur" EnableLoadOnDemand="true" >                                                       
                                                            <ItemTemplate>                                                                
                                                                <div onclick="StopPropagation(event)" class="combo-item-template" style="text-align:left">
                                                                    <asp:CheckBox runat="server" ID="chk1" onclick="onCheckBoxClick(this)" />
                                                                    <asp:Label runat="server" ID="lbl1" AssociatedControlID="chk1">
                                                                            <%# Eval("Product_Name")%>
                                                                    </asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                          </telerik:RadComboBox>                                
                                    </td>                                   
                                </tr>
                                <tr>
                                <td colspan="5">
                                        <div class="centrarcontenido">
                                            <asp:Label ID="lbl_saveconsulta" runat="server" Text="Guardar consulta">
                                            </asp:Label><asp:ImageButton ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/qryguardar.png"
                                                Height="32px" Width="33px" />
                                            <br />
                                            <asp:Label ID="lbl_updateconsulta" runat="server" Text="Actualizar consulta" Visible="False">
                                            </asp:Label><asp:ImageButton ID="btn_img_actualizar" runat="server" ImageUrl="~/Pages/images/update-icon-tm.jpg"
                                                Height="25px" Width="29px" Visible="False" />
                                            <asp:ModalPopupExtender ID="ModalPopupExtender_agregar" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="True" Enabled="True" PopupControlID="Panel_parametros" TargetControlID="btn_img_add"
                                                CancelControlID="BtnclosePanel" DynamicServicePath="">
                                            </asp:ModalPopupExtender>
                                            <asp:Panel ID="Panel_parametros" runat="server" BackColor="White" BorderColor="#0099CB"
                                                BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="150px"
                                                Width="400px" Style="display: none;">
                                                <div>
                                                    <asp:ImageButton ID="BtnclosePanel" runat="server" BackColor="Transparent" Height="22px"
                                                        Imagestyle="text-align:right;" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
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
                                            <asp:ModalPopupExtender ID="ModalPopupExtender_edit" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="True" Enabled="True" PopupControlID="Panel_edit" TargetControlID="btn_img_actualizar"
                                                CancelControlID="btn_close_planel_Edit" DynamicServicePath="">
                                            </asp:ModalPopupExtender>
                                            <asp:Panel ID="Panel_edit" runat="server" BackColor="White" BorderColor="#0099CB"
                                                BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="150px"
                                                Width="400px" Style="display: none;">
                                                <div>
                                                    <asp:ImageButton ID="btn_close_planel_Edit" runat="server" BackColor="Transparent"
                                                        Height="22px" Imagestyle="text-align:right;" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                                                </div>
                                                <div align="center">
                                                    <div style="font-family: verdana; font-size: medium; color: #D01887;">
                                                        Actualizar consulta</div>
                                                    <br />
                                                    Descripción :<asp:TextBox ID="txt_pop_actualiza" runat="server" Width="300px"></asp:TextBox>
                                                    <br></br>
                                                    <asp:Button ID="btn_actualizar" runat="server" Text="Actualizar" CssClass="buttonGuardar"
                                                        OnClick="btn_actualizar_Click" Height="25px" Width="164px" />
                                                </div>
                                            </asp:Panel>
                                        </div></td>
                                </tr>
                            </table>
                       
                         <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3"
                                                AssociatedUpdatePanelID="UpFiltrosPrecios1" BackgroundCssClass="modalProgressGreyBackground">
                                                <ProgressTemplate>
                                                    <div>
                                                         Cargando...
                                                        <img alt="Procesando" src="../../../images/loading.gif"  style="vertical-align: middle" />
                                                    </div>
                                                </ProgressTemplate>
                                            </cc2:ModalUpdateProgress>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                        </ContentTemplate>
                     
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabMisFavoritos" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Mis Favoritos
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div id="div_parametro" style="text-align:left;">
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
                                                    <asp:Label ID="lbl_cadena" runat="server" Visible="False" Text='<%# Bind("icadena") %>'></asp:Label>
                                                    <asp:Label ID="lbl_negocio" runat="server" Visible="False" Text='<%# Bind("inegocio") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_categoria" runat="server" Visible="False" Text='<%# Bind("id_producto_categoria") %>'></asp:Label>
                                                    
                                                    <asp:Label ID="lbl_id_año" runat="server" Visible="False" Text='<%# Bind("id_año") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_mes" runat="server" Visible="False" Text='<%# Bind("id_mes") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_periodo" runat="server" Visible="False" Text='<%# Bind("id_periodo") %>'></asp:Label>
                                                    <asp:ImageButton ID="btn_img_buscar" runat="server" ImageUrl="~/Pages/img/Qrys_File.png"
                                                        Height="28px" Width="26px" CommandName="BUSCAR" />
                                                    <asp:ImageButton ID="btn_img_edit" runat="server" ImageUrl="~/Pages/images/edit_icon.gif"
                                                        CommandName="EDITAR" />
                                                    <asp:ImageButton ID="btn_img_eliminar" runat="server" ImageUrl="~/Pages/images/delete.png"
                                                        CommandName="ELIMINAR" OnClientClick="confirm('¿Esta seguro de eliminar el registro?')" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
                <asp:Button ID="btngnerar" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                    Height="16px" Width="140px" Visible="true" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />
            </div>
               </Content>
            </asp:AccordionPane>
        </Panes>
        <%-- <HeaderTemplate>
            close
        </HeaderTemplate>
        <ContentTemplate>
        </ContentTemplate>--%>
    </asp:accordion>
         <asp:UpdatePanel ID="UpdatePanel_validacion" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <div id="div_Validar" runat="server" style="text-align:left;" visible="false">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_año_value" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbl_mes_value" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbl_periodo_value" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbl_validacion" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkb_validar" runat="server" ValidationGroup="1" OnCheckedChanged="chkb_validar_CheckedChanged"
                                Text="Validar" AutoPostBack="true" />
                            <asp:CheckBox ID="chkb_invalidar" runat="server" ValidationGroup="1" Text="Invalidar"
                                OnCheckedChanged="chkb_invalidar_CheckedChanged" AutoPostBack="true" />
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:Button ID="btn_dispara_popupvalidar" runat="server" CssClass="alertas" Text="ocultar"
                        Width="95px" />
                    <asp:ModalPopupExtender ID="ModalPopupExtender_ValidationAnalyst" runat="server"
                        TargetControlID="btn_dispara_popupvalidar" PopupControlID="Panel_validAnalyst"
                        BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="Panel_validAnalyst" runat="server" BackColor="White" BorderColor="#0099CB"
                        BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="120px"
                        Width="400px" Style="display: none;">
                        <div align="center">
                            <div style="font-family: verdana; font-size: medium; color: #D01887;">
                                Mensaje de confirmación</div>
                            <br />
                            <asp:Label ID="lbl_msj_validar" runat="server"></asp:Label>
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btn_aceptar" runat="server" Text="Aceptar" CssClass="buttonNew" Height="25px"
                                Width="164px" OnClick="btn_aceptar_Click" />
                            <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="buttonNew"
                                Height="25px" Width="164px" OnClick="btn_cancelar_Click" />
                            <asp:Button ID="btn_aceptar2" runat="server" Text="Aceptar" CssClass="buttonNew"
                                Height="25px" Width="164px" Visible="false" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
    <asp:updatepanelanimationextender id="UpdatePanelAnimationExtender_validacion" runat="server"
        targetcontrolid="UpdatePanel_validacion">
        <Animations> 
            <OnUpdated>
                <Sequence>
                  <FadeOut Duration="0.2" Fps="30" />
                  <FadeIn Duration="0.2" Fps="30" />
                </Sequence>
          </OnUpdated>
        </Animations>
    </asp:updatepanelanimationextender>
            <div id="div_rportes" runat="server" align="center">
                <asp:TabContainer ID="TabContainer_Reporte_Precio" runat="server" Style="width: auto"
                    CssClass="magenta" ActiveTabIndex="0" ScrollBars="Both">
                   <%-- <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel9">
                        <HeaderTemplate>
                            Info.Ejecutivo
                        </HeaderTemplate>
                        <ContentTemplate>
                              <art:Reporte_v2_Precio_ResumenEjecutivo ID="Reporte_v2_Precio_ResumenEjecutivo" runat="server" />
                        </ContentTemplate>
                    </asp:TabPanel>--%>
                    <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" Style="width: auto;">
                        <HeaderTemplate>
                           Detalle Precios
                        </HeaderTemplate>
                        <ContentTemplate>
                          
                            <art:Reporte_v2_Precio_InformePrecioAASS ID="Reporte_v2_Precio_InformePrecioAASS" runat="server" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                        <HeaderTemplate>
                            Precios Cadena
                        </HeaderTemplate>
                        <ContentTemplate>
                            <art:Reporte_v2_Precio_InformePrecioAASSCad ID="Reporte_v2_Precio_InformePrecioAASSCad" runat="server" />
                        </ContentTemplate>
                    </asp:TabPanel>
                   
                    <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel3" Enabled="false">
                        <HeaderTemplate>
                           Comparativo Cadenas
                        </HeaderTemplate>
                        <ContentTemplate>
                            <art:Reporte_v2_Precio_InformePrecioAASSG ID="Reporte_v2_Precio_InformePrecioAASSG" runat="server"/>
                        </ContentTemplate>
                    </asp:TabPanel>
                    
                    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel3">
                        <HeaderTemplate>
                            Evolución Precios por Producto
                        </HeaderTemplate>
                        <ContentTemplate>
                              <rsweb:ReportViewer ID="evolucionPreciosAASS" runat="server" Font-Names="Verdana"
                                    Font-Size="8pt" ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False"
                                    ToolTip="Evolución de precios" SizeToReportContent="True" ZoomMode="FullPage">
                              </rsweb:ReportViewer>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </div>

                 
    <%-- End Body--%>
</asp:Content>
