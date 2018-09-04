<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_Fotografico.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Fotografico" %>

<%-- Referencias de master--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<%-- end de master--%>
<%--  referecias ajax--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%-- end referecias ajax--%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte Fotografico
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
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <script src="Silverlight.js" type="text/javascript"></script>
    <script src="SlideShow.js" type="text/javascript"></script>
    <%--  referecias ajax y script--%>
    <asp:UpdatePanel ID="UpFiltrosFotografico" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Titulo_reporte_fotografico" style="text-align: left;">
                <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
                    color: #D01887;">
                    REPORTE DE FOTOGRAFICO
                </div>
            </div>
            <div id="oculta" style="text-align: right">
                <asp:Button ID="btn_ocultar" runat="server" OnClick="btn_ocultar_Click" Text="Ocultar"
                    CssClass="buttonOcultar" Height="16px" Width="63px" />
            </div>
            <div id="Div_filtros" runat="server" align="center" style="width: auto;" visible="false">
                <cc1:TabContainer ID="TabContainer_filtros" runat="server" ActiveTabIndex="0" CssClass="cyan">
                    <cc1:TabPanel ID="TabFiltros" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Personalizado</HeaderTemplate>
                        <ContentTemplate>
                            <table style="width: auto; height: auto;" align="center" class="art-Block-body">
                                <tr>
                                    <td align="right">
                                        Tipo de Reporte
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cmb_tipoReporte" runat="server" Height="19px" Width="200px">
                                            <asp:ListItem Value="0">--Todos--</asp:ListItem>
                                            <asp:ListItem Value="01">Exhibicion</asp:ListItem>
                                            <asp:ListItem Value="02">Visibilidad</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Año
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cmb_año" runat="server" Height="22px" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lbl_Mes" Text="Mes" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cmb_mes" runat="server" Height="19px" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lbl_Periodo" Text="Periodo" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cmb_periodo" runat="server" Height="19px" Width="150px" Visible="False">
                                            <asp:ListItem Value="01">Periodo I</asp:ListItem>
                                            <asp:ListItem Value="02">Periodo II</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Cobertura
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cmb_ciudad" runat="server" Height="19px" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <div align="left">
                                Guardar consulta<asp:ImageButton ID="btn_img_add" runat="server" ImageUrl="~/Pages/img/qryguardar.png"
                                    Height="32px" Width="33px" />
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
                                                    <asp:Label ID="lbl_id_año" runat="server" Visible="False" Text='<%# Bind("id_año") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_mes" runat="server" Visible="False" Text='<%# Bind("id_mes") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_periodo" runat="server" Visible="False" Text='<%# Bind("id_periodo") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id_tipoReporte" runat="server" Visible="False" Text='<%# Bind("id_tipoReporte") %>'></asp:Label>
                                                    <asp:ImageButton ID="btn_img_buscar" runat="server" ImageUrl="~/Pages/img/Qrys_File.png"
                                                        Height="28px" Width="26px" CommandName="BUSCAR" />
                                                    <asp:ImageButton ID="btn_img_eliminar" runat="server" ImageUrl="~/Pages/images/delete.png"
                                                        CommandName="ELIMINAR" />
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
        </ContentTemplate>
        <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnbuscar" EventName="Click" />
            </Triggers>--%>
    </asp:UpdatePanel>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div id="div_rportes" runat="server" align="center" style="width: 100%;">
        <cc1:TabContainer ID="TabContainer_Reporte_Fotografico" runat="server" CssClass="magenta"
            ActiveTabIndex="1">
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel9">
                <HeaderTemplate>
                    Resumen Ejecutivo</HeaderTemplate>
                <ContentTemplate>
                    RESUMEN EJECUTIVO</ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                <HeaderTemplate>
                    Galeria de Fotografica
                </HeaderTemplate>
                <ContentTemplate>
                    <div>
                        <asp:Button ID="btnbuscar" runat="server" Height="24px" Text="Buscar" Width="160px"
                            OnClick="btn_buscar_Click" />
                        <script type="text/javascript">
                            new SlideShow.Control(new SlideShow.XmlConfigProvider({ url: "Configuration.xml" }));
                        </script>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
    <%-- </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnbuscar" />
          
        </Triggers>
    </asp:UpdatePanel>--%>
    <%-- end referecias ajax--%>
</asp:Content>
