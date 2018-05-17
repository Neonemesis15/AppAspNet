<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master" AutoEventWireup="true" CodeBehind="Report_V2_SF_Spot.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Report_V2_SF_Spot" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc3" %>
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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Reporte de Spot Publicitario
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MenuContentPlaceHolder" runat="server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="SheetContentPlaceHolder" runat="server">
    <div id="Titulo_reporte_precio" style="text-align: left;">
        <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
            color: #D01887;">
            REPORTE DE SPOT PUBLICITARIO
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
                        <button class="buttonOcultar">
                            Ampliar Vista</button>
                    </div>
                </Header>
                <Content>
                    <div id="Div_filtros" runat="server" align="center" style="width: auto; overflow:hidden" visible="true">
                        <cc1:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                            <cc1:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                                <HeaderTemplate>
                                    Personalizado
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpFiltrosPrecios" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table style="width: auto; height: auto;" align="center" class="art-Block-body">
                                               
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
             <td  align="right">Año :</td>
           <td><asp:DropDownList ID="cmbAnio" runat="server" Height="25px" Width="275px"
                            Style="text-align: left" CausesValidation="True" 
                            >
                        </asp:DropDownList></td>
           
           
           <td  align="right">Familia :</td>
           <td><asp:DropDownList ID="cmbFamilia" runat="server" Enabled="True" Height="25px" Width="275px"
                            >
                        </asp:DropDownList></td>
            </tr>

                         <tr>
             <td  align="right">Mes :</td>
           <td><asp:DropDownList ID="cmbMes" runat="server" Height="25px" Width="275px"
                            Style="text-align: left" CausesValidation="True" 
                            >
                        </asp:DropDownList></td>
           <td  align="right">Competencia :</td>
           <td>
               <asp:DropDownList ID="cmbCompetencia" runat="server" Visible="True"
                    Height="25px" 
                  Width="275px">
               </asp:DropDownList>
                             </td>
            </tr>


             <tr>
             <td  align="right">Periodo :</td>
           <td><asp:DropDownList ID="cmbPeriodo" runat="server" Height="25px" Width="275px"
                            Style="text-align: left" CausesValidation="True" 
                            >
                        </asp:DropDownList></td>
           <td  align="right">Categoria :</td>
           <td><asp:DropDownList ID="cmbCategoria" runat="server" Enabled="False" 
                   Height="25px" Width="275px" AutoPostBack="True" onselectedindexchanged="cmbCategoria_SelectedIndexChanged"
                            >
                        </asp:DropDownList></td>
            </tr>


             <tr>
             <td  align="right">Canal :</td>
           <td><asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="275px" Style="text-align: left">
                        </asp:DropDownList></td>
           <td  align="right">Marca :</td>
           <td><asp:DropDownList ID="cmbMarca" runat="server" Enabled="True" Height="25px"
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
           <td  align="right"></td>
           <td></td>
           <td  align="right">&nbsp;</td>
           <td>&nbsp;</td>
            </tr>
            <tr>
           <td  align="right"></td>
           <td></td>
           <td  align="right">&nbsp;</td>
           <td>&nbsp;</td>
            </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <div>
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
                                                                       Height="25px" Width="164px" />
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
                                                                        Height="25px" Width="164px" />
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3"
                                                AssociatedUpdatePanelID="UpFiltrosPrecios" BackgroundCssClass="modalProgressGreyBackground">
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
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabMisFavoritos" runat="server" HeaderText="TabPanel1">
                                <HeaderTemplate>
                                    Mis Favoritos
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div id="div_parametro" align="left">
                                        Agregar<asp:ImageButton ID="btn_imb_tab" runat="server" Height="16px" ImageUrl="~/Pages/images/add.png"
                                            Width="16px" />
                                        <telerik:RadGrid ID="RadGrid_parametros" runat="server" AutoGenerateColumns="False">
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
                                                            <asp:Label ID="lbl_id_pdv" runat="server" Visible="False" Text='<%# Bind("id_punto_venta") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_categoria" runat="server" Visible="False" Text='<%# Bind("id_producto_categoria") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_marca" runat="server" Visible="False" Text='<%# Bind("id_producto_marca") %>'> </asp:Label>
                                                            <asp:Label ID="lbl_id_subCategoria" runat="server" Visible="False" Text='<%# Bind("id_subCategoria") %>'></asp:Label>
                                                            <asp:Label ID="lbl_id_subMarca" runat="server" Visible="False" Text='<%# Bind("id_subMarca") %>'></asp:Label>
                                                            <asp:Label ID="lbl_skuProducto" runat="server" Visible="False" Text='<%# Bind("skuProducto") %>'></asp:Label>
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
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                        <asp:Button ID="btngnerar" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                            Height="16px" Width="140px" Visible="true" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />
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
    <div id="div_rportes" runat="server" align="center">
        <cc1:TabContainer ID="TabContainer_Reporte_Precio" runat="server" Style="width: auto"
            CssClass="magenta" ActiveTabIndex="1" ScrollBars="Both">
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel9" Enabled="false">
                <HeaderTemplate>
                    Spot Publicitario
                </HeaderTemplate>
                <ContentTemplate>
                    <rsweb:ReportViewer ID="rpt_spot_publicitario" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False" ToolTip="Comparativo"
                                SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" Style="width: auto;">
                <HeaderTemplate>
                    Marcas vs Meses
                </HeaderTemplate>
                <ContentTemplate>
                    <rsweb:ReportViewer ID="rpt_spot_marca_meses" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False" ToolTip="Comparativo"
                                SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                <HeaderTemplate>
                    Distribución Spot
                </HeaderTemplate>
                <ContentTemplate>
                   <rsweb:ReportViewer ID="rpt_spot_distri" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False" ToolTip="Comparativo"
                                SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel3">
                <HeaderTemplate>
                    Spots Por Familias
                </HeaderTemplate>
                <ContentTemplate>
                    <rsweb:ReportViewer ID="rpt_spot_familia" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False" ToolTip="Comparativo"
                                SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel3">
                <HeaderTemplate>
                    Promociones Adicionales
                </HeaderTemplate>
                <ContentTemplate>
                    <rsweb:ReportViewer ID="rpt_spot_promo" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False" ToolTip="Comparativo"
                                SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
    <%-- End Body--%>
</asp:Content>