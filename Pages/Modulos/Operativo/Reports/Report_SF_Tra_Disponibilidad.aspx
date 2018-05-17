<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
AutoEventWireup="true" CodeBehind="Report_SF_Tra_Disponibilidad.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_SF_Tra_Disponibilidad" %>


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

<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>


            <div align="center">
                <table style="width: 100%; height: auto;" align="center" class="art-Block-body">
                <tr>
                    <td class="style3" >
                        REPORTE DISPONIBILIDAD - SAN FERNANDO - TRADICIONAL
                    </td>
                </tr>
            </table>
                <fieldset    style="width:850px"  >
                        <legend  > Consultar Reporte de Disponibilidad </legend>
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
                                    <asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True"
                                        Enabled="true" Height="25px" Width="275px" OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                 <td align="right">
                                    Campaña :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Enabled="False"
                                        Height="25px" Width="275px" OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>

                            </tr>
                        <tr>
                            <td align="right">
                                Distribuidor :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cmbNodeComercial" runat="server" Enabled="False" Height="25px"
                                    Width="275px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Distrito :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cmbDistrito" runat="server" Enabled="False" Height="25px"
                                    Width="275px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Supervisor :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cmbSupervisor" runat="server" Enabled="False" Height="25px"
                                    Width="275px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Generador :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cmbGenerador" runat="server" Enabled="False" Height="25px"
                                    Width="275px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Button ID="btn_buscar" runat="server"  CssClass="buttonRed" Height="25px"
                                        OnClick="btn_buscar_Click" Text="Buscar" Width="164px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1" colspan="4">
                                <asp:Label ID="lblmensaje" runat="server" Style="text-align: left"></asp:Label>
                                </td>
                                <td >
                                </td>
                                <td></td>
                                <td></td>
                            </tr>                
                        </table>
                </fieldset>
            </div>

            <div id="div_ExamenTda" runat="server" class="class_div" style="width: auto;
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
                            <telerik:RadGrid ID="gv_Disponibilidad" 
                                runat="server" 
                                AutoGenerateColumns="False" 
                                PageSize="30"
                                Skin="Vista" 
                                Font-Size="Small" 
                                AllowPaging="True" 
                                GridLines="None" 
                                ShowFooter="True"
                                OnPageIndexChanged="gv_precios_PageIndexChanged"
                                OnPageSizeChanged="gv_precios_PageSizeChanged" 
                                Height="610px">

                                <MasterTableView 
                                NoMasterRecordsText="Sin Datos para mostrar." 
                                ForeColor="#00579E"
                                AlternatingItemStyle-BackColor="#F7F7F7" 
                                Font-Size="Smaller" 
                                AlternatingItemStyle-ForeColor="#333333" Width="100%">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Distribuidor" HeaderText="Distribuidor" UniqueName="Distribuidor" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RUC" HeaderText="RUC" UniqueName="RUC" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente" UniqueName="Cliente" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Dirección" HeaderText="Dirección" UniqueName="Dirección" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Distrito" HeaderText="Distrito" UniqueName="Distrito" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Departamento" HeaderText="Departamento" UniqueName="Departamento" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Ciudad" HeaderText="Ciudad" UniqueName="Ciudad" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ProductoDisponible" HeaderText="Producto Disponible" UniqueName="ProductoDisponible" ReadOnly="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="mercaderista" HeaderText="Mercaderista:" UniqueName="mercaderista" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="Fec_Reg_Bd" HeaderText="Fecha_de_Registro" UniqueName="Fec_Reg_Bd" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn"  HeaderStyle-Width="90px">
                                            <HeaderTemplate>
                                               <asp:Button ID="btn_validar_Click" OnClientClick="return confirm('¿Esta seguro de invalidar los registros?');"
                                                    runat="server" Text="Invalidar" OnClick="btn_validar_Click_Click" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_validar" Checked='<%# Eval("validado")%>' runat="server" />
                                                <asp:Label ID="lbl_validar" runat="server" Visible="true"></asp:Label>
                                                <asp:Label ID="lblregEstLayoutDetalle" Text='<%#Eval("id_regEstLayoutDetalle") %>' runat="server"
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                       <%-- <telerik:GridEditCommandColumn 
                                            ButtonType="ImageButton" 
                                            CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                            EditImageUrl="~/Pages/images/edit_icon.gif" 
                                            UpdateImageUrl="~/Pages/images/save_icon.png"
                                            CancelText="Cancelar" UpdateText="Actualizar">
                                        </telerik:GridEditCommandColumn>--%>
                                    </Columns>
                                   <%-- <EditFormSettings>
                                        <EditColumn UniqueName="EditCommandColumn1" ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                            EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png">
                                        </EditColumn>
                                    </EditFormSettings>--%>
                                    <AlternatingItemStyle BackColor="#F7F7F7" ForeColor="#333333" />
                                </MasterTableView>
                                <PagerStyle PageSizeLabelText="Tamaño de pagina:" />
                                  <ClientSettings>
                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True">
                </Scrolling>
            </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
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
                <asp:GridView ID="gv_DisponibilidadToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False" AutoGenerateColumns="false" ForeColor="#333333">
                    <PagerStyle CssClass="pager-row" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle CssClass="row" BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerSettings PageButtonCount="7" FirstPageText="«" LastPageText="»" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="Distribuidor" HeaderText="Distribuidor" />
                        <asp:BoundField DataField="RUC" HeaderText="RUC" />
                        <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                        <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
                        <asp:BoundField DataField="Provincia" HeaderText="Provincia" />
                        <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                        <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                        <asp:BoundField DataField="HorarioAtencion1" HeaderText="Horario de Atención 01" />
                        <asp:BoundField DataField="HorarioAtencion2" HeaderText="Horario de Atención 02" />
                        <asp:BoundField DataField="DiaDescanso" HeaderText="Dia de Descanso" />
                        <asp:BoundField DataField="descripcionGiro" HeaderText="Giro del Punto de Venta" />
                        <asp:BoundField DataField="descripcionLocation" HeaderText="Ubicación" />
                        <asp:BoundField DataField="ptoConsumo" HeaderText="¿Es Punto de Consumo?" />
                        <asp:BoundField DataField="descripcionSize" HeaderText="Tamaño del Punto de Venta" />
                        <asp:BoundField DataField="HorneaPavo" HeaderText="Hornea Pavo" />
                        <asp:BoundField DataField="descripcion_Equipment" HeaderText="Equipo de Frio" />
                        <asp:BoundField DataField="Mercaderista" HeaderText="Mercaderista" />
                        <asp:BoundField DataField="Fec_Reg_Bd" HeaderText="Fecha Registro de Base Datos" />
                        <asp:BoundField DataField="Validado" HeaderText="Validado" />
                    </Columns>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

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




</asp:Content>

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
