<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_Stock.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Stock" %>

<%@ Register Assembly="Lucky.CFG" Namespace="Lucky.CFG.Tools" TagPrefix="cc3" %>
<%@ Register Src="Informes_de_Stock/Reporte_V2_Stock_EvolucionTotalDiasGiroPorPeriodo.ascx"
    TagName="Reporte_V2_Stock_EvolucionTotalDiasGiroPorPeriodo" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="Informes_de_Stock/Reporte_v2_Stock_DetalleOficina.ascx" TagName="Reporte_v2_Stock_DetalleOficina"
    TagPrefix="uc3" %>
<%-- Referencias de master--%>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<%-- end de master--%>
<%-- referencias a Informes--%>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Stock_ClientesEliminadosBBDD" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Stock/Reporte_v2_Stock_ClientesEliminadosBBDD.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Stock_ClientesSinInformacion" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Stock/Reporte_v2_Stock_ClientesSinInformacion.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Stock_DiasGiroPorMarcaYFamilia"
    Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Stock/Reporte_v2_Stock_DiasGiroPorMarcaYFamilia.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Stock_DiasGiroPorPDV" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Stock/Reporte_v2_Stock_DiasGiroPorPDV.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Stock_ResumenEjecutivo" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Stock/Reporte_v2_Stock_ResumenEjecutivo.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Stock_TotalDiasGiroCategoria" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Stock/Reporte_v2_Stock_TotalDiasGiroCategoria.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Stock_TotalDiasGiroOficina" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Stock/Reporte_v2_Stock_TotalDiasGiroOficina.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Stock_RSellin" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Stock/Reporte_v2_Stock_RSellin.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Stock_RStock" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Stock/Reporte_v2_Stock_RStock.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Stock_RSOficina" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Stock/Reporte_v2_Stock_RSOficina.ascx" %>
<%@ Register TagPrefix="art" TagName="Reporte_v2_Stock_RptPdvRelevados" Src="~/Pages/Modulos/Cliente/Reportes/Informes_de_Stock/Reporte_v2_Stock_RptPdvRelevados.ascx" %>
<%@ Register Src="Informes_de_Stock/Form_Stock_RangoDiasGiro.ascx" TagName="Form_Stock_RangoDiasGiro"
    TagPrefix="uc2" %>
<%-- end referecias Informes--%>
<%--  referecias ajax--%>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%-- end referecias ajax--%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte de Stock
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
	width:auto;
	float:left;
}

#floatbutton div
{
    float:left;
    margin-top: 10px;
    }
    
#floatbutton img
{
    float:left;
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
        //$("#<%=UpdatePanel_validacion.ClientID %>").css('padding-bottom', '25px');

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
            //]]>
    </script>
</asp:Content>

<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <%--  referecias ajax--%>
    <%-- end referecias ajax--%>
    <%-- <asp:Panel ID="Panel_filtros" runat="server" BackColor="White" BorderColor="#E46322"
                BorderWidth="3px" Style="display: block; width: 100%;" BorderStyle="Solid">--%>
    <div id="Titulo_reporte_stock" style="text-align: left;">
        <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
            color: #D01887;">
            REPORTE DE STOCK
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
                    <div id="Div_filtros" runat="server" align="center" style="width: 100%;" visible="true">
                        <asp:UpdatePanel ID="UpReportStock" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <cc1:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                                    <cc1:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                                        <HeaderTemplate>
                                            Personalizado</HeaderTemplate>
                                        <ContentTemplate>
                                            <table style="width: 700px; margin: auto;">
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Año</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox class="control" ID="cmb_año" runat="server" Width="230px" Skin="Vista">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Oficina</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox class="control" ID="cmb_oficina" runat="server" Width="230px"
                                                            Skin="Vista">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Mes</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox class="control" ID="cmb_mes" runat="server" Skin="Vista" Width="230px"
                                                            LoadingMessage="Cargando..." OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged"
                                                            ToolTip="Mes" AutoPostBack="True">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Categoria</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox class="control" ID="cmb_categoria" runat="server" Width="230px"
                                                            Skin="Vista">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Periodo</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox class="control" ID="cmb_periodo" runat="server" Width="230px"
                                                            Skin="Vista" Enabled="False">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="display: none">
                                                <asp:DropDownList ID="cmb_punto_de_venta" runat="server" Height="19px" Width="238px"
                                                    Visible="False">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="cmb_subCategoria" runat="server" Height="19px" Width="238px"
                                                    Enabled="False" Visible="False">
                                                </asp:DropDownList>
                                                <telerik:RadComboBox ID="cmb_marca" runat="server" Width="238px" Skin="Vista" Enabled="False"
                                                    Visible="False">
                                                </telerik:RadComboBox>
                                                <asp:DropDownList ID="cmb_subMarca" runat="server" Height="19px" Width="238px" OnSelectedIndexChanged="cmb_subMarca_SelectedIndexChanged"
                                                    Enabled="False" Visible="False">
                                                </asp:DropDownList>
                                                <telerik:RadComboBox ID="cmb_familia" runat="server" Width="238px" Skin="Vista" Enabled="False"
                                                    Visible="False">
                                                </telerik:RadComboBox>
                                                <asp:DropDownList ID="cmb_skuProducto" runat="server" Height="19px" Width="238px"
                                                    Visible="False">
                                                </asp:DropDownList>
                                            </div>
                                            <div>
                                                <asp:UpdatePanel ID="up_saveConsulta" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lbl_saveconsulta" runat="server" Text="Guardar consulta"></asp:Label>
                                                        <asp:ImageButton ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/qryguardar.png"
                                                            Height="32px" Width="33px" /><br />
                                                        <asp:Label ID="lbl_updateconsulta" runat="server" Text="Actualizar consulta" Visible="False"></asp:Label>
                                                        <asp:ImageButton ID="btn_img_actualizar" runat="server" ImageUrl="~/Pages/images/update-icon-tm.jpg"
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
                                                                    ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" /></div>
                                                            <div align="center">
                                                                <div style="font-family: verdana; font-size: medium; color: #D01887;">
                                                                    Nueva consulta</div>
                                                                <br></br>
                                                                Descripción :<asp:TextBox ID="txt_descripcion_parametros" runat="server" Width="300px"></asp:TextBox>
                                                                <br></br>
                                                                <asp:Button ID="btn_guardar_parametros" runat="server" Text="Guardar" CssClass="buttonGuardar"
                                                                    OnClick="buttonGuardar_Click" Height="25px" Width="164px" /></div>
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
                                                                Descripción :<asp:TextBox ID="txt_pop_actualiza" runat="server" Width="300px"></asp:TextBox><br>
                                                                </br>
                                                                <asp:Button ID="btn_actualizar" runat="server" Text="Actualizar" CssClass="buttonGuardar"
                                                                    OnClick="btn_actualizar_Click" Height="25px" Width="164px" />
                                                            </div>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabMisFavoritos" runat="server" HeaderText="TabPanel1">
                                        <HeaderTemplate>
                                            Mis Favoritos</HeaderTemplate>
                                        <ContentTemplate>
                                            <div id="div_parametro" align="left">
                                                Agregar<asp:ImageButton ID="btn_imb_tab" runat="server" Height="16px" ImageUrl="~/Pages/images/add.png"
                                                    Width="16px" OnClick="btn_imb_tab_Click" />
                                                <telerik:RadGrid ID="RadGrid_parametros" runat="server" AutoGenerateColumns="False"
                                                    GridLines="None" OnItemCommand="RadGrid_parametros_ItemCommand" Skin="" AllowPaging="True"
                                                    PageSize="10">
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="column">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_id" runat="server" Visible="False" Text='<%# Bind("id") %>'></asp:Label><asp:Label
                                                                        ID="lbl_id_servicio" runat="server" Visible="False" Text='<%# Bind("id_servicio") %>'></asp:Label><asp:Label
                                                                            ID="lbl_id_canal" runat="server" Visible="False" Text='<%# Bind("id_canal") %>'></asp:Label><asp:Label
                                                                                ID="lbl_id_company" runat="server" Visible="False" Text='<%# Bind("id_compañia") %>'> </asp:Label><asp:Label
                                                                                    ID="lbl_id_reporte" runat="server" Visible="False" Text='<%# Bind("id_reporte") %>'> </asp:Label><asp:Label
                                                                                        ID="lbl_id_user" runat="server" Visible="False" Text='<%# Bind("id_user") %>'></asp:Label><asp:Label
                                                                                            ID="lbl_id_oficina" runat="server" Visible="False" Text='<%# Bind("id_oficina") %>'></asp:Label><asp:Label
                                                                                                ID="lbl_id_pdv" runat="server" Visible="False" Text='<%# Bind("id_punto_venta") %>'></asp:Label><asp:Label
                                                                                                    ID="lbl_id_categoria" runat="server" Visible="False" Text='<%# Bind("id_producto_categoria") %>'></asp:Label><asp:Label
                                                                                                        ID="lbl_id_marca" runat="server" Visible="False" Text='<%# Bind("id_producto_marca") %>'> </asp:Label><asp:Label
                                                                                                            ID="lbl_id_familia" runat="server" Visible="False" Text='<%# Bind("id_producto_familia") %>'></asp:Label><asp:Label
                                                                                                                ID="lbl_id_año" runat="server" Visible="False" Text='<%# Bind("id_año") %>'></asp:Label><asp:Label
                                                                                                                    ID="lbl_id_mes" runat="server" Visible="False" Text='<%# Bind("id_mes") %>'></asp:Label><asp:Label
                                                                                                                        ID="lbl_id_periodo" runat="server" Visible="False" Text='<%# Bind("id_periodo") %>'></asp:Label><asp:ImageButton
                                                                                                                            ID="btn_img_buscar" runat="server" ImageUrl="~/Pages/img/Qrys_File.png" Height="28px"
                                                                                                                            Width="26px" CommandName="BUSCAR" />
                                                                    <asp:ImageButton ID="btn_img_edit" runat="server" ImageUrl="~/Pages/images/edit_icon.gif"
                                                                        CommandName="EDITAR" /><asp:ImageButton ID="btn_img_eliminar" runat="server" ImageUrl="~/Pages/images/delete.png"
                                                                            CommandName="ELIMINAR" OnClientClick="return confirm('¿Confirma que desea eliminar el registro indicado?');" /></ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView></telerik:RadGrid></div>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3"
                            AssociatedUpdatePanelID="UpReportStock" BackgroundCssClass="modalProgressGreyBackground">
                            <ProgressTemplate>
                                <div>
                                    Cargando...
                                    <img alt="Procesando" src="../../../images/loading.gif" style="vertical-align: middle" />
                                </div>
                            </ProgressTemplate>
                        </cc2:ModalUpdateProgress>
                        <asp:Button ID="btngnerar" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                            Height="16px" Width="140px" ToolTip="Permite Generar el Informe" OnClick="btngnerar_Click" />
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
    <%--  </asp:Panel>--%>
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
    <%-- <asp:UpdatePanel ID="upreporte" runat="server">
        <ContentTemplate>--%>
    <div id="div_rportes" runat="server" align="center" style="width: 100%;">
    <div id="contenedor">
        <div id="floatbutton"><img src="../../../images/fullscreen.png" alt="Fullscreen" /></div>
        <cc1:TabContainer ID="TabContainer_Reporte_Stock" runat="server" CssClass="magenta"
            ActiveTabIndex="0">
            <cc1:TabPanel runat="server" ID="TabPanel_Configuracion">
                <ContentTemplate>
                    <div id="div_configuration" align="left" runat="server" visible="False">
                        <table>
                            <tr>
                                <td align="left">
                                    <telerik:RadTabStrip ID="RadTabStrip_Configuracion" runat="server" MultiPageID="RadMultiPage_Configuracion"
                                        Orientation="VerticalLeft" SelectedIndex="0">
                                        <Tabs>
                                            <telerik:RadTab runat="server" Text="Resumen Ejecutivo" ImageUrl="~/Pages/images/resumen.gif"
                                                Owner="" Selected="True">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="Rango de Dias Giro" ImageUrl="~/Pages/images/calendar.gif"
                                                Owner="">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="Rangos de Stock" ImageUrl="~/Pages/images/calendar.gif"
                                                Owner="">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="Log Errores" ImageUrl="~/Pages/images/calendar.gif"
                                                Owner="">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="Cierre Periodo" ImageUrl="~/Pages/images/calendar.gif"
                                                Owner="">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="Recalcular" PageViewID="RadPageView6" ImageUrl="~/Pages/images/resumen.gif"
                                                Owner="">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                    &nbsp;
                                </td>
                                <td style="border: 1px solid #C0C0C0; width: 100%;">
                                    <telerik:RadMultiPage ID="RadMultiPage_Configuracion" runat="server" SelectedIndex="0">
                                        <telerik:RadPageView ID="RadPageView1" runat="server" Selected="True">
                                            <art:Reporte_v2_Stock_ResumenEjecutivo ID="Reporte_v2_Stock_ResumenEjecutivo" runat="server"
                                                Visible="False" />
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView2" runat="server">
                                            <uc2:Form_Stock_RangoDiasGiro ID="Form_Stock_RangoDiasGiro1" runat="server" Visible="False" />
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView3" runat="server">
                                            <div>
                                                Año:<telerik:RadComboBox ID="rcmb_año" runat="server">
                                                </telerik:RadComboBox>
                                                Mes:<telerik:RadComboBox ID="rcmb_mes" runat="server" OnSelectedIndexChanged="rcmb_mes_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </telerik:RadComboBox>
                                                Periodo:<telerik:RadComboBox ID="rcmb_periodo" runat="server">
                                                </telerik:RadComboBox>
                                                <div>
                                                    <asp:Button ID="btn_exportRangos" runat="server" Text="Exportar rangos" CssClass="buttonNew"
                                                        Height="25px" Width="164px" OnClick="btn_exportRangos_Click" />
                                                </div>
                                            </div>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView4" runat="server">
                                            <div>
                                                <asp:Button ID="btn_generarErrores" runat="server" Text="Generar Log errores" CssClass="buttonNew"
                                                    Height="25px" Width="164px" OnClick="btn_generarErrores_Click" />
                                                <telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
                                                    <script type="text/javascript">

                                                        var DGInput = null;
                                                        var vstockIni = 0.0;
                                                        var vstockFin = 0.0;
                                                        var vsellIn = 0.0;
                                                        var vrangoDias = 0.0;

                                                        function Load(sender, args) {
                                                            DGInput = sender;
                                                        }
                                                        function Blur(sender, args) {
                                                            var vsellOut = vsellIn + vstockIni - vstockFin;
                                                            var DG = vstockFin / ((vsellOut) / vrangoDias);
                                                            DGInput.set_value(DG);
                                                        }
                                                        function stockIni(sender, args) {
                                                            vstockIni = sender.get_value();
                                                        }
                                                        function stockFin(sender, args) {
                                                            vstockFin = sender.get_value();
                                                        }
                                                        function sellIn(sender, args) {
                                                            vsellIn = sender.get_value();
                                                        }
                                                        function rangoDias(sender, args) {
                                                            vrangoDias = sender.get_value();
                                                        }
                                                               
                                                    </script>
                                                </telerik:RadScriptBlock>
                                                <telerik:RadGrid ID="gv_LogErrores" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                    Skin="Outlook" OnItemCommand="gv_LogErrores_ItemCommand" OnItemDataBound="gv_LogErrores_ItemDataBound">
                                                    <MasterTableView NoMasterRecordsText="Sin resultados.">
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="Oficina" HeaderText="Oficina" UniqueName="Oficina">
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="PDV" HeaderText="PDV" UniqueName="PDV">
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Categoria" HeaderText="Categoria" UniqueName="Categoria">
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Rango Días" UniqueName="TemplateColumn1">
                                                                <ItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txt_RangoDias" runat="server" Culture="es-PE" DbValue='<%# Eval("Rango_Dias") %>'
                                                                        Width="60px" MinValue="0" InvalidStyleDuration="1000" NumberFormat-DecimalDigits="0"
                                                                        DisabledStyle-BorderColor="Blue" Enabled="false" DisabledStyle-Font-Bold="true"
                                                                        DisabledStyle-ForeColor="#3366cc" DisabledStyle-BackColor="#E1EDFF">
                                                                        <%-- <ClientEvents OnLoad="rangoDias" />--%>
                                                                    </telerik:RadNumericTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Sell In" UniqueName="TemplateColumn0">
                                                                <ItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txt_sellIn" runat="server" Culture="es-PE" DbValue='<%# Eval("Sell_In") %>'
                                                                        Width="60px" MinValue="0" InvalidStyleDuration="1000" NumberFormat-DecimalDigits="0"
                                                                        DisabledStyle-BorderColor="Blue" Enabled="false" DisabledStyle-Font-Bold="true"
                                                                        DisabledStyle-ForeColor="#3366cc" DisabledStyle-BackColor="#E1EDFF">
                                                                        <%-- <ClientEvents OnLoad="sellIn" />--%>
                                                                    </telerik:RadNumericTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Stock inicial" UniqueName="TemplateColumn2">
                                                                <ItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txt_stockIni" runat="server" Culture="es-PE" DbValue='<%# Eval("Stock_Inicial") %>'
                                                                        Width="60px" MinValue="0" InvalidStyleDuration="1000" NumberFormat-DecimalDigits="0"
                                                                        DisabledStyle-BorderColor="Blue" Enabled="false" DisabledStyle-Font-Bold="true"
                                                                        DisabledStyle-ForeColor="#3366cc" DisabledStyle-BackColor="#E1EDFF">
                                                                        <%--<ClientEvents OnLoad="stockIni" />--%>
                                                                    </telerik:RadNumericTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Stock Final" UniqueName="TemplateColumn3">
                                                                <ItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txt_stockFin" runat="server" Culture="es-PE" DbValue='<%# Eval("Stock_Final") %>'
                                                                        Width="60px" MinValue="0" InvalidStyleDuration="1000" NumberFormat-DecimalDigits="0">
                                                                        <%--<ClientEvents OnBlur="Blur" OnValueChanging="stockFin" />--%>
                                                                    </telerik:RadNumericTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Días Giro" UniqueName="TemplateColumn4">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnImg_calc" ToolTip="Calcular Días Giro" runat="server" ImageUrl="~/Pages/images/calc-icon.png"
                                                                        CommandName="CALCULAR" Height="24px" Width="20px" />
                                                                    <telerik:RadNumericTextBox ID="txt_DG" runat="server" Culture="es-PE" DbValue='<%# Eval("Dias_Giro") %>'
                                                                        Width="100px" MinValue="0" InvalidStyleDuration="1000" NumberFormat-DecimalDigits="2"
                                                                        ToolTip="Días Giro" Enabled="false" EnabledStyle-BackColor="Yellow" EnabledStyle-BorderColor="Blue">
                                                                        <%-- <ClientEvents OnLoad="Load" />--%>
                                                                    </telerik:RadNumericTextBox>
                                                                    <asp:Label ID="lbl_cod_PDV" runat="server" Visible="false" Text='<%# Bind("Cod_PDV") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_cod_Categoria" runat="server" Visible="false" Text='<%# Bind("Cod_Categoria") %>'></asp:Label>
                                                                    <%--  <asp:Label ID="lbl_cod_Familia" runat="server" Visible="false" Text='<%# Bind("Cod_Familia") %>'></asp:Label>--%>
                                                                    <telerik:RadDateTimePicker ID="rad_fecha" Visible="false" runat="server" Culture="es-PE"
                                                                        DbSelectedDate='<%# Eval("Fecha_Final") %>'>
                                                                    </telerik:RadDateTimePicker>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="MIN_DG" HeaderText="Min DG" UniqueName="MIN_DG">
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="MAX_DG" HeaderText="Max DG" UniqueName="MAX_DG">
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="True" />
                                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                <div>
                                                    <div>
                                                        Guardar:
                                                        <asp:ImageButton ID="btnImg_save" ToolTip="Calcular Días Giro" runat="server" ImageUrl="~/Pages/images/save_icon.png"
                                                            Height="24px" Width="20px" OnClick="btnImg_save_Click" />
                                                    </div>
                                                    <br />
                                                    <asp:Label ID="lbl_msjRadView" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView5" runat="server">
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView6" runat="server">
                                            <div>
                                                Año:<telerik:RadComboBox ID="rcmb_año2" runat="server">
                                                </telerik:RadComboBox>
                                                Mes:<telerik:RadComboBox ID="rcmb_mes2" runat="server" OnSelectedIndexChanged="rcmb_mes2_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </telerik:RadComboBox>
                                                Periodo:<telerik:RadComboBox ID="rcmb_periodo2" runat="server">
                                                </telerik:RadComboBox>
                                                <div>
                                                    <asp:Button ID="btn_recalcularDg" runat="server" Text="Recalcular" CssClass="buttonNew"
                                                        Height="25px" Width="164px" OnClick="btn_recalcularDg_Click" />
                                                    <div>
                                                        <asp:Label runat="server" ID="lbl_msj_recalcular"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="Div_ResumenEjecutivo" runat="server" visible="False">
                        Display: Resumen Ejecutivo
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel_DG_Evolucion" runat="server" HeaderText="TabPanel3">
                <HeaderTemplate>
                    Evolución Dias Giro</HeaderTemplate>
                <ContentTemplate>
                    <div class="overscroll"><uc1:Reporte_V2_Stock_EvolucionTotalDiasGiroPorPeriodo ID="Reporte_V2_Stock_EvolucionTotalDiasGiroPorPeriodo"
                        runat="server" /></div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel_DG_Oficina" runat="server" HeaderText="TabPanel3">
                <HeaderTemplate>
                    Total Dias Giro Oficina</HeaderTemplate>
                <ContentTemplate>
                    <div class="overscroll"><art:Reporte_v2_Stock_TotalDiasGiroOficina ID="Reporte_v2_Stock_TotalDiasGiroOficina"
                        runat="server" /></div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel_Rangos_Sellin" runat="server" HeaderText="TabPanel3">
                <HeaderTemplate>
                    Rangos Sell In</HeaderTemplate>
                <ContentTemplate>
                    <div class="overscroll"><art:Reporte_v2_Stock_RSellin ID="Reporte_v2_Stock_RSellin" runat="server" /></div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel_Rangos_Stock" runat="server" HeaderText="TabPanel3">
                <HeaderTemplate>
                    Rangos Stock Final</HeaderTemplate>
                <ContentTemplate>
                    <div class="overscroll"><art:Reporte_v2_Stock_RStock ID="Reporte_v2_Stock_RStock" runat="server" /></div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel_RangosSOficina" runat="server" HeaderText="TabPanel3">
                <HeaderTemplate>
                    Rangos Sell In Of.</HeaderTemplate>
                <ContentTemplate>
                    <div class="overscroll"><art:Reporte_v2_Stock_RSOficina ID="Reporte_v2_Stock_RSOficina" runat="server" /></div>
                </ContentTemplate>
            </cc1:TabPanel>
            <%-- Se comentó 'Temporalmete' --%>
            <%-- <cc1:TabPanel ID="TabPanel_DG_Catego" runat="server" HeaderText="TabPanel3" Enabled="false">
                        <HeaderTemplate>
                            Total Días Giro Categoria</HeaderTemplate>
                        <ContentTemplate>
                            <art:Reporte_v2_Stock_TotalDiasGiroCategoria ID="Reporte_v2_Stock_TotalDiasGiroCategoria"
                                runat="server" />
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel_DG_MarcaFami" runat="server" HeaderText="TabPanel3" Enabled="false">
                        <HeaderTemplate>
                            Días Giro Marca y Familia</HeaderTemplate>
                        <ContentTemplate>
                            <art:Reporte_v2_Stock_DiasGiroPorMarcaYFamilia ID="Reporte_v2_Stock_DiasGiroPorMarcaYFamilia"
                                runat="server" />
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel3" Enabled="false">
                        <HeaderTemplate>
                            DG por Marca Detalle por Oficinas</HeaderTemplate>
                        <ContentTemplate>
                            <uc3:Reporte_v2_Stock_DetalleOficina ID="Reporte_v2_Stock_DetalleOficina1" runat="server" />
                        </ContentTemplate>
                    </cc1:TabPanel>--%>
            <cc1:TabPanel ID="TabPanel_DG_Pdv" runat="server" HeaderText="TabPanel3">
                <HeaderTemplate>
                    Días Giro PDV</HeaderTemplate>
                <ContentTemplate>
                    <div class="overscroll"><art:Reporte_v2_Stock_DiasGiroPorPDV ID="Reporte_v2_Stock_DiasGiroPorPDV" runat="server" /></div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel3">
                <HeaderTemplate>
                    PDV Relevados</HeaderTemplate>
                <ContentTemplate>
                    <div class="overscroll"><art:Reporte_v2_Stock_RptPdvRelevados ID="Reporte_v2_Stock_RptPdvRelevados" runat="server" /></div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
    </div>
    <%-- <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3"
                AssociatedUpdatePanelID="upreporte" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div dir="rtl">
                        Cargando Informes
                        <img alt="Procesando" src="../../../images/loading5.gif" style="vertical-align: middle" />
                    </div>
                </ProgressTemplate>
            </cc2:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
    <%-- end referecias ajax--%>
    <!-- POPUPS ALERTA -->
    <div>
        <asp:Button ID="btn_popup_ocultar" runat="server" CssClass="alertas" Text="ocultar"
            Width="95px" />
        <cc1:ModalPopupExtender ID="ModalPopupExtender_mensaje" runat="server" BackgroundCssClass="modalBackground"
            DropShadow="True" Enabled="True" PopupControlID="Panel_popupmensaje" TargetControlID="btn_popup_ocultar"
            CancelControlID="btn_aceptar_popup" DynamicServicePath="">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel_popupmensaje" runat="server" BackColor="White" BorderColor="#0099CB"
            BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="150px"
            Width="400px" Style="display: none;">
            <div align="center">
                <div style="font-family: verdana; font-size: medium; color: #D01887;">
                    Mensaje de usuario</div>
                <br />
                <asp:Label ID="lbl_msj_popup" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Button ID="btn_aceptar_popup" runat="server" Text="Continuar" CssClass="buttonNew"
                    Height="25px" Width="164px" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
<%--      
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ScriptIncludePlaceHolder">
    <script type="text/javascript" src="<%= ResolveUrl("~/script.js") %>"></script>
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</asp:Content>
--%>