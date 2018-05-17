<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_Stock_M_SF.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Stock_M_SF" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc3" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%-- Referencias de master--%>
<%@ Import Namespace="Artisteer" %> 
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<%-- end de master--%>
<%-- referencias a Informes--%>
<%--<%@ Register Src="Informes_SF_M_Stock/uc_SF_M_Stock_Ventas_X_Familia.ascx" TagName="Reporte_V2_SF_M_Stock_Ventas_X_Familia"
    TagPrefix="uc2" %>--%>
<%--<%@ Register Src="Informes_SF_M_Stock/uc_SF_M_Stock_Quiebre_X_PtoVenta.ascx" TagName="Reporte_V2_SF_M_Stock_Ventas_X_Familia"
    TagPrefix="uc2" %>--%>
<%-- end referecias Informes--%>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte de Ingresos - Stock </asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<%--<asp:Content ID="SideBar1" ContentPlaceHolderID="Sidebar1ContentPlaceHolder" runat="Server">
    <art:DefaultSidebar1 ID="DefaultSidebar1Content" runat="server" />
</asp:Content>--%>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <%-- Start body--%>

            <div id="Titulo_reporte_precio" style="text-align: left;">
                <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
                    color: #D01887;">
                    REPORTE DE STOCK - MODERNO
                </div>
            </div>
            <%--<div id="oculta" style="text-align: right">
                                <asp:Button ID="btn_ocultar" runat="server" OnClick="btn_ocultar_Click" Text="Ocultar"
                                    CssClass="buttonOcultar" Height="16px" Width="63px" />
                            </div>--%>
                             <cc1:Accordion ID="MyAccordion" runat="server" SelectedIndex="1" HeaderCssClass="accordionHeader"
        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
        AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
         <Panes>
           <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                <Header>
                    <div id="Div1" style="text-align: right">
                        <button class="buttonOcultar" >
                            Ampliar vista</button>
                    </div>
                </Header>
                <Content>
            <div id="Div_filtros" runat="server"  style="width: auto;" visible="true">
                <cc1:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                    <cc1:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Personalizado</HeaderTemplate>
                        <ContentTemplate>
                         <asp:UpdatePanel ID="UpdatePanel_filtros" runat="server" UpdateMode="Conditional">
                             <ContentTemplate>
                            <div style="height: 215px; width: 850px; margin: auto;">
                                <div style="float: left">
                                    <p>
                                        <span class="etiqueta">Oficina:</span>
                                        <telerik:RadComboBox ID="cmb_oficina" runat="server" Width="200px" Skin="Vista" AutoPostBack="True"
                                            OnSelectedIndexChanged="cmb_oficina_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                      </p>  
                                       <p>
                                            <span class="etiqueta">Corporación:</span>
                                            <telerik:RadComboBox ID="cmb_corporacion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_corporacion_SelectedIndexChanged"
                                                Skin="Vista" Width="200px">
                                            </telerik:RadComboBox>
                                            
                                          </p>
                                          <p>   <span class="etiqueta">Cadena:</span>
                                                <telerik:RadComboBox ID="cmb_nodocomercial" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_nodocomercial_SelectedIndexChanged"
                                                    Skin="Vista" Width="200px">
                                                </telerik:RadComboBox>
                                             </p> 
  <p>                                               
                                                        <span class="etiqueta">Punto de Venta:</span>
                                                            <cc3:DropDownCheckBoxes ID="ddl_PuntoDeVenta" runat="server" AddJQueryReference="True"
                                                            RepeatDirection="Horizontal" Style="top: 0px; left: 315px; height: 16px; width: 500px"
                                                            UseButtons="True" UseSelectAllNode="True" CssClass="alinearizquierda">
                                                            <Texts CancelButton="Cancelar" SelectAllNode="--Seleccionar todo--" SelectBoxCaption="--Seleccione Tienda(s)--"  />
                                                            
                                                            
                                                        </cc3:DropDownCheckBoxes>
                                                        <div>
                                                            <asp:Panel ID="Panel_PuntoDeVenta" runat="server">
                                                            </asp:Panel>
                                                        </div>
                                                </p>
                                                 

                                </div>
                                
                                <div style="float: left; margin-left: 15px">
                                 
                                               <p>                                                        
                                                            <span class="etiqueta">Fuerza de Venta:</span>
                                                            <telerik:RadComboBox ID="cmb_fuerzav" runat="server" Skin="Vista" Width="200px">
                                                            </telerik:RadComboBox>
                                                </p>  
                                                                                    <p>
                                        
                                            <span class="etiqueta">Supervisor:</span>
                                        <telerik:RadComboBox ID="cmb_supervisor" runat="server" Skin="Vista" Width="200px">
                                        </telerik:RadComboBox>
                                        </p>
                                      <p>  
                                            <span class="etiqueta">Categoria:</span>
                                            <telerik:RadComboBox ID="cmb_categoria" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_categoria_SelectedIndexChanged"
                                                Skin="Vista" Width="200px">
                                            </telerik:RadComboBox>
                                         </p>
                                         <p>
                                                    <span class="etiqueta">Marca:</span>
                                                <telerik:RadComboBox ID="cmb_marca" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_marca_SelectedIndexChanged"
                                                    Skin="Vista" Width="200px">
                                                </telerik:RadComboBox>
                                            </p>   
                                </div>

                                <div style="float: left; margin-left: 15px">

   
                                                <p>
                                                   
                                                    <span class="etiqueta">Familia:</span>
                                                    <cc3:DropDownCheckBoxes ID="ddl_Familia" runat="server" AddJQueryReference="True"
                                                        OnSelectedIndexChanged="ddl_Familia_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                        Style="top: 0px; left: 315px; height: 16px; width: 500px" UseButtons="True" UseSelectAllNode="True">
                                                        <Texts CancelButton="Cancelar" SelectAllNode="--Seleccionar todo--" SelectBoxCaption="-Seleccione Familia(s)-" />
                                                    </cc3:DropDownCheckBoxes>
                                                    <div>
                                                        <asp:Panel ID="Panel_Familia" runat="server">
                                                        </asp:Panel>
                                                    </div>
                                                  </p>
                                                  <p>  
                                                            <span class="etiqueta">SubFamilia:</span>
                                                            <cc3:DropDownCheckBoxes ID="ddl_Subfamilia" runat="server" AddJQueryReference="True"
                                                                OnSelectedIndexChanged="ddl_Subfamilia_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                                Style="top: 0px; left: 315px; height: 16px; width: 500px" UseButtons="True" UseSelectAllNode="True">
                                                                <Texts CancelButton="Cancelar" SelectAllNode="--Seleccionar todo--" SelectBoxCaption="Seleccione Subfamilias(s)" />
                                                            </cc3:DropDownCheckBoxes>
                                                     </p>      
                                                          <p>
                                                                <span class="etiqueta">Producto</span>
                                                                <cc3:DropDownCheckBoxes ID="ddl_Producto" runat="server" AddJQueryReference="True"
                                                                    CssClass="" OnSelectedIndexChanged="ddl_Producto_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                                    Style="top: 0px; left: 315px; height: 16px; width: 241px" UseButtons="True" UseSelectAllNode="True">
                                                                    <Texts CancelButton="Cancelar" SelectAllNode="--Seleccionar todo--" SelectBoxCaption="-Seleccione Producto(s)-" />
                                                                </cc3:DropDownCheckBoxes>
                                                                </p>
                                                           
                                </div>
                                 

                                <div style="float: left; margin-left: 15px">
                                    <p>
                                        <span class="etiqueta">Año:</span>
                                        <telerik:RadComboBox ID="cmb_año" runat="server" Skin="Vista" Width="200px">
                                        </telerik:RadComboBox>
                                     </p>
                                     <p>  

                                            <span class="etiqueta">Mes</span> &nbsp;<telerik:RadComboBox ID="cmb_mes" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged" Skin="Vista"
                                                Width="200px">
                                            </telerik:RadComboBox>
                                       </p>
                                       <p>    
                                                <span class="etiqueta">Periodo:</span>
                                                <telerik:RadComboBox ID="cmb_periodo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmb_periodo_SelectedIndexChanged"
                                                    Skin="Vista" Width="200px">
                                                </telerik:RadComboBox>
                                          </p>
                                          <p> 
                                                <span class="etiqueta">Días:</span>
                                                <cc3:DropDownCheckBoxes ID="ddl_Dia" runat="server" AddJQueryReference="true" UseButtons="true"
                                                    UseSelectAllNode="true" Style="top: 0px; left: 315px; height: 16px; width: 241px">
                                                    <Texts SelectBoxCaption="--- Seleccione Día(s) ---" />
                                                </cc3:DropDownCheckBoxes>
                                          </p>
                                </div>
                               
                            </div>
                            
                            <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3"
                                                AssociatedUpdatePanelID="UpdatePanel_filtros" BackgroundCssClass="modalProgressGreyBackground"
                                                EnableViewState="true">
                                                <ProgressTemplate>
                                                    <div>
                                                    Procesando...
                                                    <img alt="Procesando" src="../../../images/loading5.gif" style="vertical-align: middle" />
                                                    </div>
                                                </ProgressTemplate>
                                            </cc2:ModalUpdateProgress>
                            
                            
                            
                            </ContentTemplate>
                            
                            </asp:UpdatePanel>
                            <table style="width: auto; height: auto;" align="center" class="art-Block-body">
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <div>
                                            <asp:Label ID="lbl_saveconsulta" runat="server" Text="Guardar consulta"></asp:Label>
                                            <asp:ImageButton
                                                ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/qryguardar.png" Height="32px"
                                                Width="33px" /><br />
                                            <asp:Label ID="lbl_updateconsulta" runat="server" Text="Actualizar consulta" Visible="False"></asp:Label>
                                            <asp:ImageButton
                                                ID="btn_img_actualizar" runat="server" ImageUrl="~/Pages/images/update-icon-tm.jpg"
                                                Height="25px" Width="29px" Visible="False" />
                                                <cc1:ModalPopupExtender ID="ModalPopupExtender_agregar"
                                                    runat="server" BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True"
                                                    PopupControlID="Panel_parametros" TargetControlID="btn_img_add" CancelControlID="BtnclosePanel"
                                                    DynamicServicePath="">
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
                                                    Nueva consulta
                                                    </div>
                                                    <br />
                                                    Descripción :
                                                    <asp:TextBox ID="txt_descripcion_parametros" runat="server" Width="300px">
                                                    </asp:TextBox>
                                                    <br>
                                                    </br>
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
                                                    Descripción :
                                                    <asp:TextBox ID="txt_pop_actualiza" runat="server" Width="300px">
                                                    </asp:TextBox>
                                                    <br>
                                                    </br>
                                                    <asp:Button ID="btn_actualizar" runat="server" Text="Actualizar" CssClass="buttonGuardar"
                                                        OnClick="btn_actualizar_Click" Height="25px" Width="164px" /></div>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabMisFavoritos" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Mis Favoritos</HeaderTemplate>
                        <ContentTemplate>
                            <div id="div_parametro" align="left">
                                Agregar
                                <asp:ImageButton ID="btn_imb_tab" runat="server" Height="16px" ImageUrl="~/Pages/images/add.png"
                                    Width="16px" OnClick="btn_imb_tab_Click" />
                                    <telerik:RadGrid ID="RadGrid_parametros"
                                        runat="server" AutoGenerateColumns="False" GridLines="None" OnItemCommand="RadGrid_parametros_ItemCommand"
                                        Skin="">
                                        <MasterTableView>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="column">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                                     <ItemTemplate>
                                                        <asp:Label ID="lbl_id" runat="server" Visible="False" Text='<%# Bind("id") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_servicio" runat="server" Visible="False" Text='<%# Bind("id_servicio") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_canal" runat="server" Visible="False" Text='<%# Bind("id_canal") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_company" runat="server" Visible="False" Text='<%# Bind("id_compania") %>'> </asp:Label>
                                                        <asp:Label ID="lbl_id_reporte" runat="server" Visible="False" Text='<%# Bind("id_reporte") %>'> </asp:Label>
                                                        <asp:Label ID="lbl_id_user" runat="server" Visible="False" Text='<%# Bind("id_user") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_oficina" runat="server" Visible="False" Text='<%# Bind("id_oficina") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_corporacion" runat="server" Visible="False" Text='<%# Bind("id_corporacion") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_cadena" runat="server" Visible="False" Text='<%# Bind("id_cadena") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_pdv" runat="server" Visible="False" Text='<%# Bind("id_puntoDeVenta") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_fuerzaVenta" runat="server" Visible="False" Text='<%# Bind("id_fuerzaVenta") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_supervisor" runat="server" Visible="False" Text='<%# Bind("id_supervisor") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_categoria" runat="server" Visible="False" Text='<%# Bind("id_categoria") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_marca" runat="server" Visible="False" Text='<%# Bind("id_marca") %>'> </asp:Label>
                                                        <asp:Label ID="lbl_id_familias" runat="server" Visible="False" Text='<%# Bind("id_familias") %>'> </asp:Label>
                                                        <asp:Label ID="lbl_id_subfamilias" runat="server" Visible="False" Text='<%# Bind("id_subfamilias") %>'> </asp:Label>
                                                        <asp:Label ID="lbl_skuProducto" runat="server" Visible="False" Text='<%# Bind("id_productos") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_año" runat="server" Visible="False" Text='<%# Bind("id_year") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_mes" runat="server" Visible="False" Text='<%# Bind("id_month") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_periodo" runat="server" Visible="False" Text='<%# Bind("id_periodo") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id_dias" runat="server" Visible="False" Text='<%# Bind("id_dias") %>'></asp:Label>
                                                        <asp:ImageButton ID="btn_img_buscar" runat="server" CommandName="BUSCAR"
                                                        ImageUrl="~/Pages/img/Qrys_File.png" Height="28px"  Width="26px"  />
                                                        
                                                        <asp:ImageButton ID="btn_img_edit" runat="server" CommandName="EDITAR" 
                                                        ImageUrl="~/Pages/images/edit_icon.gif" />
                                                        
                                                        <asp:ImageButton ID="btn_img_eliminar" runat="server" CommandName="ELIMINAR"
                                                        ImageUrl="~/Pages/images/delete.png"  OnClientClick="confirm('¿Esta seguro de eliminar el registro?')" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                   </telerik:RadGrid>
                           </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
                
            </div>
            <div style="width: auto;" align="center">
            <table align="center">
                    <tr>
                        <td colspan="7">
                            <asp:Button ID="btngnerar" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                                Height="16px" Width="140px" Visible="true" OnClick="btngnerar_Click" ToolTip="Permite Generar Todos los Informes" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnVentaXFamilia" runat="server" Text="Vta Familia" CssClass="buttonOcultar"
                                Height="16px" Width="140px" Visible="true" ToolTip="Ventas Por Familias" OnClick="btnVentaXFamilia_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnQuiebreXTienda" runat="server" Text="Quieb Tda" CssClass="buttonOcultar"
                                Height="16px" Width="140px" Visible="true" ToolTip="Quiebres Por Tienda" OnClick="btnQuiebreXTienda_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnVentaXTienda" runat="server" Text="Graf Vta Tda" CssClass="buttonOcultar"
                                Height="16px" Width="140px" Visible="true" ToolTip="Venta Por Tienda" OnClick="btnVentaXTienda_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnVentaTotalXMes" runat="server" Text="Graf Vta Mes" CssClass="buttonOcultar"
                                Height="16px" Width="140px" Visible="true" ToolTip="Ventas Totales por Mes" OnClick="btnVentaTotalXMes_Click" />
                        </td>
                        <%--<td>--%>
                        <%-- pSalas 20/12/2011 Se combinará en una sola pantalla --%>
                            <%--<asp:Button ID="btnVentaMensualXFamilia" runat="server" Text="VtaMen X Familia" CssClass="buttonOcultar"
                                Height="16px" Width="140px" Visible="true" ToolTip="Ventas Mensuales X Familia"
                                OnClick="btnVentaMensualXFamilia_Click" />
                        </td>--%>
                        <td>
                            <asp:Button ID="btnVentaAcumXProducto" runat="server" Text="Graf Vta Acum" CssClass="buttonOcultar"
                                Height="16px" Width="140px" Visible="true" ToolTip="Ventas Acumuladas X Producto"
                                OnClick="btnVentaAcumXProducto_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnEvoProdXSemana" runat="server" Text="Evo Prod Sem" CssClass="buttonOcultar"
                                Height="16px" Width="140px" Visible="true" ToolTip="Evolución de Producto X Semana"
                                OnClick="btnEvoProdXSemana_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            </Content>
            </cc1:AccordionPane>
          </Panes>
            </cc1:Accordion>

            <asp:UpdatePanel ID="UpdatePanelAnimationExtender_validacion" runat="server">
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
         <%--   <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" AssociatedUpdatePanelID="UpFiltrosPrecios"
                BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">
                        Procesando, por favor espere...
                        <img alt="Procesando" src="../../../images/progress_bar.gif" style="vertical-align: middle" />
                    </div>
                </ProgressTemplate>
            </cc2:ModalUpdateProgress>--%>
            </ContentTemplate>
    </asp:UpdatePanel>
                <cc1:UpdatePanelAnimationExtender ID="UpdatePanelValidacion" runat="server"
        TargetControlID="UpdatePanelAnimationExtender_validacion">
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
                <cc1:TabContainer ID="TabContainer_Reporte_Stock_SF_M" runat="server"
                    Style="width: auto" CssClass="magenta" ScrollBars="Both" ActiveTabIndex="4">
                    <%--ActiveTabIndex="0"--%>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel2" Style="width: auto;">
                        <HeaderTemplate>
                            Ingr-Stock</HeaderTemplate> <%--Reporte de Ingresos - Stock--%>
                        <ContentTemplate>
                        <%-- <rsweb:ReportViewer ID="rpt_stock_m" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                ProcessingMode="Remote"  Style="width: auto" ShowParameterPrompts="False" ToolTip="Comparativo"
                                SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                            </rsweb:ReportViewer>--%>
                             <asp:Button ID="Btndisparaalertas" runat="server" CssClass="alertas" Text="" Visible="true"
                Width="0px" />
                            
                            <rsweb:ReportViewer ID="rpt_stock_m" runat="server" Font-Names="Verdana" 
                                Font-Size="8pt" ProcessingMode="Remote" Height="569px" Width="100%"  ShowParameterPrompts="False" 
                                ToolTip="Reporte de Ingresos Stock" SizeToReportContent="False"  
                                ZoomPercent="100"  Visible="False" ZoomMode="Percent">
                                
                            </rsweb:ReportViewer>
                          
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" Style="width: auto;">
                        <HeaderTemplate>
                            Quieb Tda</HeaderTemplate> <%--Quiebre por Tienda--%>
                        <ContentTemplate>
<%--                            <rsweb:ReportViewer ID="rpt_Quiebre_PtoVenta" runat="server" Font-Names="Verdana"
                                Font-Size="8pt" ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False"
                                ToolTip="Comparativo" SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                            </rsweb:ReportViewer>--%>
                             <rsweb:ReportViewer ID="rpt_Quiebre_PtoVenta" runat="server" Font-Names="Verdana"
                                Font-Size="8pt" ProcessingMode="Remote" Height="569px" Width="100%"  ShowParameterPrompts="False"
                                ToolTip="Quiebre Por Tienda" SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>

                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel3" Style="width: auto;">
                        <HeaderTemplate>
                            Quieb Prod</HeaderTemplate> <%--Quiebre por Producto--%>
                        <ContentTemplate>
                           <%-- <rsweb:ReportViewer ID="rpt_Quiebre_Producto" runat="server" Font-Names="Verdana"
                                Font-Size="8pt" ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False"
                                ToolTip="Comparativo" SizeToReportContent="True" ZoomMode="FullPage" Visible="False">
                            </rsweb:ReportViewer>--%>
                             <rsweb:ReportViewer ID="rpt_Quiebre_Producto" runat="server" Font-Names="Verdana"
                                Font-Size="8pt" ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False"
                                ToolTip="Quiebre Por Producto" SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel4" Style="width: auto;">
                        <HeaderTemplate>
                            Vta Familia</HeaderTemplate> <%--Ventas por Familia--%>
                        <ContentTemplate>
                            <rsweb:ReportViewer ID="rpt_Ventas_X_Familia" runat="server" Font-Names="Verdana"
                                Font-Size="8pt" ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False"
                                ToolTip="Venta por Familia" SizeToReportContent="False" ZoomMode="Percent" Visible="false">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel5" Style="width: auto;">
                        <HeaderTemplate>
                            Graf Quieb Tda</HeaderTemplate> <%--Gráfico de Ventas por Familia--%>
                        <ContentTemplate>
                            <rsweb:ReportViewer ID="rpt_Quiebre_X_Tienda" runat="server" Font-Names="Verdana"
                                Font-Size="8pt" ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False"
                                ToolTip="Gráfico Quiebres por Tienda" SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel6" Style="width: auto;">
                        <HeaderTemplate>
                            Graf Vta Tda</HeaderTemplate> <%--Gráfico de Ventas por Tienda--%>
                        <ContentTemplate>
                            <rsweb:ReportViewer ID="rpt_Venta_X_Tienda" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False" ToolTip="Gráfico Venta por Tienda"
                                SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel7" Style="width: auto;">
                        <HeaderTemplate>
                            Graf Vta Mes</HeaderTemplate> <%--Grafico Ventas Mensuales - VtaTot_X_Mes --%>
                        <ContentTemplate>
                            <rsweb:ReportViewer ID="rpt_VentasTotales_X_Mes" runat="server" Font-Names="Verdana"
                                Font-Size="8pt" ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False"
                                ToolTip="Comparativo" SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <%--<cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel8" Style="width: auto;">
                        <HeaderTemplate>
                            VtaMen_X_Fam</HeaderTemplate> 
                        <ContentTemplate>
                            <rsweb:ReportViewer ID="rpt_VentasMensuales_X_Familia" runat="server" Font-Names="Verdana"
                                Font-Size="8pt" ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False"
                                ToolTip="Comparativo" SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>--%>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel9" Style="width: auto;">
                        <HeaderTemplate>
                            Graf Vta Acum</HeaderTemplate> <%-- Grafico Ventas Acumuladas por Producto - VtaAcu_X_Prod --%>
                        <ContentTemplate>
                            <rsweb:ReportViewer ID="rpt_VentasAcumuladas_X_Prod" runat="server" Font-Names="Verdana"
                                Font-Size="8pt" ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False"
                                ToolTip="Comparativo" SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel10" Style="width: auto;">
                        <HeaderTemplate>
                            Evo Prod Sem</HeaderTemplate> <%-- Grafico Evolución Productos Semanales - Evo Prod Sem --%>
                        <ContentTemplate>
                            <rsweb:ReportViewer ID="rpt_Evo_Prod_Sem" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                ProcessingMode="Remote" Height="569px" Width="100%" ShowParameterPrompts="False" ToolTip="Comparativo"
                                SizeToReportContent="False" ZoomMode="Percent" Visible="False">
                            </rsweb:ReportViewer>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </div>
 </asp:Content>