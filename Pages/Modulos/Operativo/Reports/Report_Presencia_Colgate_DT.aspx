<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master" AutoEventWireup="true" CodeBehind="Report_Presencia_Colgate_DT.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Presencia_Colgate_DT" %>


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
<asp:Content ID="Scripts" ContentPlaceHolderID="ScriptIncludePlaceHolder" runat="server">
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script> 

<script src="../../../js/jquery.overscroll.min.js" type="text/javascript"></script>
    
<script type="text/javascript">
    $(function (o) {
        //div.overscroll
                o = $("#<%=gv_presencia.ClientID %>_GridData").overscroll({
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

                });

   </script>
</asp:Content>

<%--<asp:Content ID="SideBar1" ContentPlaceHolderID="Sidebar1ContentPlaceHolder" runat="Server">
    <uc1:DefaultSidebar2 ID="DefaultSidebar1Content" runat="server" />
</asp:Content>--%>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>
        <div align="center">
            <div align="center" style="font-family: Verdana; font-size: medium; color: #00579E;
                font-weight: bold">
                REPORTE DE PRESENCIA</div>
            <fieldset    style="width:850px"  >
            <legend  > Consultar Reporte Presencia</legend>
            <table style="width: 100%; height: auto;" align="center" class="art-Block-body">
                <tr>
                    <td class="style3" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Fecha :
                    </td>
                    <td class="style6">
                        Desde :<telerik:RadDatePicker ID="txt_fecha_inicio" runat="server" Culture="es-PE" Skin="Web20"
                            Visible="true">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Web20">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        &nbsp;Hasta :<telerik:RadDatePicker ID="txt_fecha_fin" runat="server" Culture="es-PE" Skin="Web20"
                            Visible="true">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Web20">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                    </td>
                </tr>          
                <tr>
                    <td class="style2">
                        Canal :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbcanal" runat="server" runat="server" AutoPostBack="True"
                            Enabled="true" Height="25px" Width="275px" OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Campaña :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbplanning" runat="server" AutoPostBack="True" Enabled="False"
                            Height="25px" Width="275px" OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Mercaderista :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbMercaderista" runat="server" Enabled="False" Height="25px" 
                             Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td class="style2">
                        Cadena :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbCadena" runat="server" Enabled="False" Height="25px" Width="275px"
                           >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Ciudad :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbOficina" runat="server" Enabled="False" Height="25px" Width="275px"
                           >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Zona :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="cmbMalla" runat="server" Enabled="False"
                            Height="25px" 
                            Width="275px" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Tipo de Reporte :
                    </td>
                    <td class="style6">
                       <asp:DropDownList ID="cmbTipoReporte" runat="server" Enabled="False"
                            Height="25px" 
                            Width="275px" >
                        </asp:DropDownList></td>
                </tr>                 
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                    <td class="style6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4">
                    </td>
                    <td class="style6">
                        <asp:Button ID="btn_buscar" runat="server" CssClass="buttonRed" Height="25px"
                            OnClick="btn_buscar_Click" Text="Buscar" Width="164px" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                    </td>
                    <td class="style1">
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                    </td>
                    <td class="style6">
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                    </td>
                    <td class="style6">
                        <asp:Label ID="lblmensaje" runat="server" Style="text-align: left"></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                    </td>
                    <td class="style6" align="right">
                    </td>
                </tr>
                <tr>
                    <td class="style4" colspan="2">
                    </td>
                </tr>               
            </table>
             </fieldset>
             </div>
            <div id="div_Presencia" runat="server" class="class_div" style="width: auto; height: auto;">
                <table>
                    <tr style="display:none">
                        <td align="left">
                        Seleccionar todos<asp:CheckBox ID="cb_all"  runat="server" 
                            Font-Size="Small" oncheckedchanged="cb_all_CheckedChanged" 
                            AutoPostBack="True" />
                            <asp:Button ID="btn_validar_Click" OnClientClick="return confirm('¿Esta seguro de invalidar los registros?');"
                                                    runat="server" Text="Validar" OnClick="btn_validar_Pres_Click" CssClass="button"/>
                        </td>
                    </tr>                    
                    <tr>
                        <td align="left">
                            <telerik:RadGrid ID="gv_Calculos" runat="server" 
                                oncolumncreated="gv_Calculos_ColumnCreated" Skin="Vista" GridLines="None" 
                                ondatabound="gv_Calculos_DataBound">
                                <MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E"
                                    AlternatingItemStyle-BackColor="#F7F7F7" Font-Size="Smaller" 
                                    AlternatingItemStyle-ForeColor="#333333">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                    
                                    <telerik:GridBoundColumn DataField="a" UniqueName="en_blanco0"
                                            ReadOnly="true" Visible="true" HeaderStyle-Width="800px" HeaderText="">
                                        </telerik:GridBoundColumn>
                                       <%--  <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco1"
                                            ReadOnly="true" Visible="true" HeaderStyle-Width="500px">
                                        </telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco2"
                                            ReadOnly="true" Visible="true" HeaderStyle-Width="500px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco3"
                                            ReadOnly="true" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco4"
                                            ReadOnly="true" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco5"
                                            ReadOnly="true" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco6"
                                            ReadOnly="true" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco7"
                                            ReadOnly="true" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco8"
                                            ReadOnly="true" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco9"
                                            ReadOnly="true" Visible="true">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco10"
                                            ReadOnly="true" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco11"
                                            ReadOnly="true" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco12"
                                            ReadOnly="true" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco13"
                                            ReadOnly="true" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco14"
                                            ReadOnly="true" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco15"
                                            ReadOnly="true" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="a" HeaderText="" UniqueName="en_blanco16"
                                            ReadOnly="true" Visible="false">
                                            </telerik:GridBoundColumn>--%>
                                    </Columns>
                                    <AlternatingItemStyle BackColor="#F7F7F7" ForeColor="#333333" />
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <div class="overscroll">
                            <telerik:RadGrid ID="gv_presencia" runat="server" GridLines="None" Skin="Vista" 
                                oncolumncreated="gv_presencia_ColumnCreated" OnDataBound="gv_presencia_DataBound"
                                OnCancelCommand="gv_presencia_CancelCommand" OnEditCommand="gv_presencia_EditCommand"
                                OnUpdateCommand="gv_presencia_UpdateCommand" 
                                onitemdatabound="gv_presencia_ItemDataBound" 
                                onneeddatasource="gv_presencia_NeedDataSource"  >
                                <MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E"
                                    AlternatingItemStyle-BackColor="#F7F7F7" Font-Size="Smaller" 
                                    AlternatingItemStyle-ForeColor="#333333">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridTemplateColumn Visible="false" UniqueName="TemplateColumn" ReadOnly="true" ItemStyle-Width="100"  >
                                            <ItemTemplate>
                                            
                                                <asp:CheckBox ID="cb_validar_presencia" Checked='<%# Eval("validado")%>' 
                                                    runat="server" />
                                                <asp:Label ID="lbl_validar_presencia" runat="server" Visible="true"></asp:Label>
                                          
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                            EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png"
                                            CancelText="Cancelar" UpdateText="Actualizar" ItemStyle-Width="100" >
                                        </telerik:GridEditCommandColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <EditColumn UniqueName="EditCommandColumnPre" ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                            EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png">
                                        </EditColumn>
                                    </EditFormSettings>
                                    <AlternatingItemStyle BackColor="#F7F7F7" ForeColor="#333333" />
                                </MasterTableView>
                            </telerik:RadGrid>
                </div>
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
    <div align="center">
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
                <asp:GridView ID="gv_PresenciaToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False" AutoGenerateColumns="False"
                    EnableModelValidation="True">
                    <RowStyle ForeColor="#000066" />
                   <%-- <Columns>
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
                        
                    </Columns>--%>
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
                 <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                <asp:GridView ID="GridView2" runat="server" EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                        <asp:BoundField ReadOnly="True" />
                    </Columns>
                 <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    </div>
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
        .style6
        {
            text-align: left;
            width: 836px;
        }
        .class_div
        {
            overflow-x: scroll;
            background-color: white;
        }
    </style>
</asp:Content>