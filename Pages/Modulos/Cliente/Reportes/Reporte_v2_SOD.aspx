<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_SOD.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_SOD" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%-- Referencias de master--%>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<%-- end de master--%>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%-- Informes--%>
<%@ Register Src="Informes_de_SOD/Reporte_v2_SOD_CumplimientoDeObjetivosMayorista.ascx"
    TagName="Reporte_v2_SOD_CumplimientoDeObjetivosMayorista" TagPrefix="uc1" %>
<%@ Register Src="Informes_de_SOD/Reporte_v2_SOD_EvolucionSODMayorista.ascx" TagName="Reporte_v2_SOD_EvolucionSODMayorista"
    TagPrefix="uc2" %>
<%@ Register Src="Informes_de_SOD/Reporte_v2_SOD_ResumenEjecutivo.ascx" TagName="Reporte_v2_SOD_ResumenEjecutivo"
    TagPrefix="uc3" %>
<%@ Register Src="Informes_de_SOD/Reporte_v2_SOD_SODporPDVMayorista.ascx" TagName="Reporte_v2_SOD_SODporPDVMayorista"
    TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--end Informes--%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte de SOD
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
    
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="../../../js/jquery.overscroll.min.js" type="text/javascript"></script>
<style type="text/css" >

#floatbutton {
	position: relative;	
	text-align:left;
	vertical-align:middle;
	margin:0;
	margin-top: 8px;
	padding:0;
	font-size:11px;
	height:34px;
	width:170px;
	float:left;
}

#floatbutton div
{
    float:left;
    margin-top: 10px;
    }
    
#floatbutton img
{
    float:right;
    }

    
.overlay {  
    position:absolute;
    top:0;
    left:0;
    width:100%;
    height:100%;
    z-index:1000;
    background-color: #FFFFFF;
  }
  
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

    //overscroll like mobile OS
    $(function (o) {
        o = $("div.overscroll").overscroll({
            cancelOn: '.no-drag',
            hoverThumbs: true,
            persistThumbs: true,
            showThumbs: false
        }).on('overscroll:dragstart overscroll:dragend overscroll:driftstart overscroll:driftend', function (event) {
            console.log(event.type);
        });

        var status = false;
        var h = $(window).height();
        $("div.overscroll").css('height', h - 144);


        var show = false;
        $('#Div1').click(function () {
            show = !show;
            if (show)
                $('#btn_ocultar').val('Ocultar Filtros');
            else
                $('#btn_ocultar').val('Mostrar Filtros');
        });

        $('#floatbutton').click(function () {

            var h = $(window).height();
            status = !status;
            if (status) {
                $('#contenedor').addClass('overlay');
                $('#contenedor').css('height', h);
                $("div.overscroll").css('height', h - 82);
            }
            else {
                $('#contenedor').removeClass('overlay');
                $('#contenedor').css('height', h);
                $("div.overscroll").css('height', h - 82);
            }
        });
    });

    $("#<%=UpdatePanel_validacion.ClientID %>").css('padding-bottom', '25px');

            //]]>
    </script>
</asp:Content>

<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>
    <%-- Start body--%>
    <%--<asp:Panel ID="Panel_filtros" runat="server" BackColor="White" BorderColor="#E46322"
                BorderWidth="3px" Style="display: block; width: auto" BorderStyle="Solid">--%>
    <div id="Titulo_reporte_SOD" style="text-align: left;">
        <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
            color: #D01887;">
            REPORTE SOD
        </div>
    </div>
    <cc1:Accordion ID="MyAccordion" runat="server" SelectedIndex="1" HeaderCssClass="accordionHeader"
        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
        AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
        <Panes>
            <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                <Header>
                    <div id="Div1" style="text-align: right">
                        <input type="button" class="buttonOcultar" value="Mostrar Filtros" id="btn_ocultar"/>
                    </div>
                </Header>
                <Content>
                    <div id="Div_filtros" runat="server" align="center" style="width: auto;" visible="true">
                        <cc1:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                            <cc1:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                                <HeaderTemplate>
                                    Personalizado
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpFiltrosSOD" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table style="width: auto; height: auto; margin:auto"  class="art-Block-body">
                                                <tr>
                                                    <td align="right">
                                                        Año
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_año" runat="server" Width="238px" Skin="Vista">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td><asp:Label id="lbl_requerido_01" runat="server" Text="*" ForeColor="Red"></asp:Label></label> </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        Mes
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_mes" runat="server" Width="238px" Visible="true" Skin="Vista">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td><asp:Label id="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label></label> </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        Categoria
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_categoria" runat="server" Width="238px" AutoPostBack="true"
                                                            OnSelectedIndexChanged="cmb_categoria_SelectedIndexChanged" Skin="Vista">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td><asp:Label id="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label></label> </td>
                                                    <%--<td align="right">
                                        Sub categoria
                                    </td>--%>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_subCategoria" runat="server" Width="238px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="cmb_subCategoria_SelectedIndexChanged" Enabled="False"
                                                            Skin="Vista" Visible="false">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        Marca
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_marca" runat="server" Width="238px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="cmb_marca_SelectedIndexChanged" Enabled="False" Skin="Vista">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <%-- <td align="right">
                                        Sub marca
                                    </td>--%>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_subMarca" runat="server" Width="238px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="cmb_subMarca_SelectedIndexChanged" Enabled="False" Skin="Vista"
                                                            Visible="false">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        Región
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_region" runat="server" Width="238px" AutoPostBack="true"
                                                            Skin="Vista" OnSelectedIndexChanged="cmb_region_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <%-- Producto--%>
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_skuProducto" runat="server" Width="238px" Visible="false"
                                                            Skin="Vista">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        Oficina
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_ciudad" runat="server" Width="238px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="cmb_ciudad_SelectedIndexChanged" Skin="Vista">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cmb_punto_de_venta" runat="server" Width="238px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="cmb_punto_de_venta_SelectedIndexChanged" Visible="false"
                                                            Skin="Vista">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_msj" runat="server" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="text-align:center">
                                                        <div style="margin:auto">
                                                            <asp:Label ID="lbl_saveconsulta" runat="server" Text="Guardar consulta">
                                                            </asp:Label><asp:ImageButton ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/qryguardar.png"
                                                                Height="32px" Width="33px" />
                                                            <br />
                                                            <asp:Label ID="lbl_updateconsulta" runat="server" Text="Actualizar consulta" Visible="False">
                                                            </asp:Label><asp:ImageButton ID="btn_img_actualizar" runat="server" ImageUrl="~/Pages/images/update-icon-tm.jpg"
                                                                Height="25px" Width="29px" Visible="False" />
                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender_agregar" runat="server" BackgroundCssClass="modalBackground"
                                                                DropShadow="True" Enabled="True" PopupControlID="Panel_parametros" TargetControlID="btn_img_add"
                                                                CancelControlID="BtnclosePanel" DynamicServicePath="">
                                                            </cc1:ModalPopupExtender>
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
                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender_edit" runat="server" BackgroundCssClass="modalBackground"
                                                                DropShadow="True" Enabled="True" PopupControlID="Panel_edit" TargetControlID="btn_img_actualizar"
                                                                CancelControlID="btn_close_planel_Edit" DynamicServicePath="">
                                                            </cc1:ModalPopupExtender>
                                                            <asp:Panel ID="Panel_edit" runat="server" BackColor="White" BorderColor="#0099CB"
                                                                BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="150px"
                                                                Width="400px" Style="display: none;">
                                                                <div>
                                                                    <asp:ImageButton ID="btn_close_planel_Edit" runat="server" BackColor="Transparent"
                                                                        Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
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


                                                        </div>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3"
                                                AssociatedUpdatePanelID="UpFiltrosSOD" BackgroundCssClass="modalProgressGreyBackground">
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
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabMisFavoritos" runat="server" HeaderText="TabPanel1">
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
                                                            <asp:Label ID="lbl_id_malla" runat="server" Visible="False" Text='<%# Bind("id_malla") %>'>'></asp:Label>
                                                            <asp:Label ID="lbl_id_pdv" runat="server" Visible="False" Text='<%# Bind("id_punto_venta") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_categoria" runat="server" Visible="False" Text='<%# Bind("id_producto_categoria") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_marca" runat="server" Visible="False" Text='<%# Bind("id_producto_marca") %>'> </asp:Label>
                                                            <asp:Label ID="lbl_id_subCategoria" runat="server" Visible="False" Text='<%# Bind("id_subCategoria") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_subMarca" runat="server" Visible="False" Text='<%# Bind("id_subMarca") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_año" runat="server" Visible="False" Text='<%# Bind("id_año") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_mes" runat="server" Visible="False" Text='<%# Bind("id_mes") %>'></asp:Label>
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
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                        <asp:Button ID="btngnerar" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                            Height="16px" Width="140px" Visible="true"  OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />
                    </div>
                </Content>
            </cc1:AccordionPane>
        </Panes>
        <%-- <HeaderTemplate>
            close
        </HeaderTemplate>
        <ContentTemplate>
        </ContentTemplate>--%>
    </cc1:Accordion>
    <asp:UpdatePanel ID="UpdatePanel_validacion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div_Validar" runat="server" align="left" visible="false">
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
                    <cc1:ModalPopupExtender ID="ModalPopupExtender_ValidationAnalyst" runat="server"
                        TargetControlID="btn_dispara_popupvalidar" PopupControlID="Panel_validAnalyst"
                        BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
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
            <div align="left">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_msj_allMonth" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender_validacion" runat="server"
        TargetControlID="UpdatePanel_validacion">
        <Animations> 
            <OnUpdated>
                <Sequence>
                  <FadeOut Duration="0.2" Fps="30" />
                  <FadeIn Duration="0.2" Fps="30" />
                </Sequence>
          </OnUpdated>
        </Animations>
    </cc1:UpdatePanelAnimationExtender>
    <asp:UpdatePanel ID="upreporte" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div_rportes" runat="server" align="center" style="width: auto;">
                <div id="contenedor">
                    <div id="floatbutton"><div>Pantalla Completa</div><img src="../../../images/fullscreen.png" alt="Fullscreen" /></div>                                           
                    <asp:TabContainer ID="TabContainer_Reporte_Precio" runat="server" CssClass="magenta"
                    ActiveTabIndex="1" >
                    <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel11">
                        <HeaderTemplate>
                            <%-- Resumen Ejcutivo--%>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <%--
                    <uc3:Reporte_v2_SOD_ResumenEjecutivo ID="Reporte_v2_SOD_ResumenEjecutivo" runat="server" />--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel14" runat="server" HeaderText="TabPanel4">
                        <HeaderTemplate>
                            Cumplimiento de Objetivos
                        </HeaderTemplate>
                        <ContentTemplate>
                        <div class="overscroll">
                            <uc1:Reporte_v2_SOD_CumplimientoDeObjetivosMayorista ID="Reporte_v2_SOD_CumplimientoDeObjetivosMayorista"
                                runat="server" /></div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel12" runat="server" HeaderText="TabPanel12">
                        <HeaderTemplate>
                            Evolución SOD
                        </HeaderTemplate>
                        <ContentTemplate>
                        <div class="overscroll">
                            <uc2:Reporte_v2_SOD_EvolucionSODMayorista ID="Reporte_v2_SOD_EvolucionSODMayorista"
                                runat="server" /></div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel13" runat="server" HeaderText="TabPanel3">
                        <HeaderTemplate>
                            SOD por PDV
                        </HeaderTemplate>
                        <ContentTemplate>
                        <div class="overscroll">
                            <uc4:Reporte_v2_SOD_SODporPDVMayorista ID="Reporte_v2_SOD_SODporPDVMayorista" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
                    <asp:Button ID="Button1" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                        Height="16px" Width="140px" Visible="false" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />
                </div>
            </div>
            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3"
                AssociatedUpdatePanelID="upreporte" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div dir="rtl">
                        Cargando Informes
                        <img alt="Procesando" src="../../../images/loading5.gif" style="vertical-align: middle" />
                    </div>
                </ProgressTemplate>
            </cc2:ModalUpdateProgress>

      
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- End Body--%>
    
       

</asp:Content>
