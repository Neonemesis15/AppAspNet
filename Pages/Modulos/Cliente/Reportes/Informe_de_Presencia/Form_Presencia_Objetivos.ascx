<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Form_Presencia_Objetivos.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informe_de_Presencia.Form_Presencia_Objetivos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%--<asp:UpdatePanel ID="up_objetivo" runat="server" UpdateMode="Conditional">
    <ContentTemplate>--%>
<div align="left">
    Objetivos por:<telerik:RadComboBox ID="rcmb_tipoObjetivos" runat="server">
        <Items>
            <telerik:RadComboBoxItem Value="0" Text="--Seleccione--" runat="server" />
            <telerik:RadComboBoxItem Value="1" Text="Lima y Provincias" runat="server" />
            <telerik:RadComboBoxItem Value="2" Text="Oficinas" runat="server" />
        </Items>
    </telerik:RadComboBox>
    Año:<telerik:RadComboBox ID="rcmb_año" runat="server">
    </telerik:RadComboBox>
    Mes:<telerik:RadComboBox ID="rcmb_mes" runat="server">
    </telerik:RadComboBox>
    <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="buttonSearch"
        Height="25px" Width="164px" OnClick="btn_buscar_Click" />
</div>
<br />
<asp:Label ID="lbl_info" runat="server"></asp:Label>
<telerik:RadGrid ID="RadGrid_objetivos" runat="server" AutoGenerateColumns="False">
    <MasterTableView NoMasterRecordsText="Sin resultados.">
        <Columns>
            <telerik:GridBoundColumn DataField="name_cobertura" HeaderText="Cobertura" UniqueName="cod_cobertura">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="nameElemento" HeaderText="Elementos de Visibilidad"
                UniqueName="idElemento">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateColumn1">
                <HeaderTemplate>
                    Objetivo
                </HeaderTemplate>
                <ItemTemplate>
                    <telerik:RadNumericTextBox ID="rtxt_objetive" runat="server" Culture="es-PE" DbValue='<%# Eval("objetivo") %>'
                        NumberFormat-DecimalDigits="0" MinValue="0" Type="Number" EmptyMessage="sin valor">
                    </telerik:RadNumericTextBox>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                <ItemTemplate>
                    <asp:Label ID="lbl_id_cobertura" runat="server" Visible="false" Text='<%# Bind("cod_cobertura") %>'></asp:Label>
                    <asp:Label ID="lbl_id_elemento" runat="server" Visible="false" Text='<%# Bind("idElemento") %>'></asp:Label>
                    <asp:Label ID="lbl_id_month" runat="server" Visible="false" Text='<%# Bind("id_Month") %>'></asp:Label>
                    <asp:Label ID="lbl_id_year" runat="server" Visible="false" Text='<%# Bind("id_Year") %>'></asp:Label>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
            Font-Underline="False" Wrap="True" />
        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
            Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
    </MasterTableView>
</telerik:RadGrid>
<div>
    <asp:Button ID="btn_nuevo" runat="server" Text="Nuevo" CssClass="buttonNew" Height="25px"
        Width="164px" />
    <asp:Button ID="btn_guardar" runat="server" Text="Guardar" CssClass="buttonGuardar"
        Height="25px" Width="164px" OnClick="btn_guardar_Click" />
</div>
<div id="mesajeConfirmacion">
    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_confirmMessage" runat="server"
        TargetControlID="btn_guardar" DisplayModalPopupID="ModalPopupExtender_confirmMessage">
    </cc1:ConfirmButtonExtender>
    <cc1:ModalPopupExtender ID="ModalPopupExtender_confirmMessage" runat="server" CancelControlID="btn_cancelar"
        BackgroundCssClass="modalBackground" TargetControlID="btn_guardar" OkControlID="btn_aceptar"
        PopupControlID="Panel_confirmMessage">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel_confirmMessage" runat="server" BackColor="White" BorderColor="#0099CB"
        BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="120px"
        Width="400px" Style="display: none;">
        <div align="center">
            <div style="font-family: verdana; font-size: medium; color: #D01887;">
                Mensaje de confirmación</div>
            <br />
            ¿Esta seguro de confirmar la actualización.?
            <br />
            <br />
            <br />
            <asp:Button ID="btn_aceptar" runat="server" Text="Aceptar" CssClass="buttonNew" Height="25px"
                Width="164px" />
            <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="buttonNew"
                Height="25px" Width="164px" />
        </div>
    </asp:Panel>
</div>
<div id="div_addObjetivo">
    <cc1:ModalPopupExtender ID="ModalPopupExtender_add" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="True" Enabled="True" PopupControlID="Panel_addObj" TargetControlID="btn_nuevo"
        CancelControlID="btn_close" DynamicServicePath="">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel_addObj" runat="server" BackColor="White" BorderColor="#0099CB"
        BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="650px"
        Width="1000px" Style="display: none;" ScrollBars="Vertical">
        <div>
            <asp:ImageButton ID="btn_close" runat="server" BackColor="Transparent" Height="22px"
                ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" /></div>
        <div align="center">
            <div style="font-family: verdana; font-size: medium; color: #D01887;">
                Agregar Objetivos</div>
            <br />
            <br />
            <div>
                Objetivos por:<telerik:RadComboBox ID="rcmb_newObj" runat="server" OnSelectedIndexChanged="rcmb_newObj_SelectedIndexChanged"
                    AutoPostBack="true">
                    <Items>
                        <telerik:RadComboBoxItem Value="0" Text="--Seleccione--" runat="server"   />
                        <telerik:RadComboBoxItem Value="1" Text="Lima y Provincias" runat="server"   />
                        <telerik:RadComboBoxItem Value="2" Text="Oficinas" runat="server"   />
                    </Items>
                </telerik:RadComboBox>
                Crear para:
                <telerik:RadComboBox ID="rcmb_añoNew" runat="server">
                </telerik:RadComboBox>
                desde
                <telerik:RadComboBox ID="rcmb_mesNewDesde" runat="server">
                </telerik:RadComboBox>
                hasta
                <telerik:RadComboBox ID="rcmb_mesNewHasta" runat="server">
                </telerik:RadComboBox>
            </div>
            <br />
            <telerik:RadGrid ID="RadGrid_newObjtives" runat="server" AutoGenerateColumns="false">
                <MasterTableView NoMasterRecordsText="Sin resultados.">
                    <Columns>
                        <telerik:GridBoundColumn DataField="name_cobertura" HeaderText="Cobertura" UniqueName="cod_cobertura">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="nameElemento" HeaderText="Elementos de Visibilidad"
                            UniqueName="idElemento">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn1">
                            <HeaderTemplate>
                                Objetivo
                            </HeaderTemplate>
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="rtxt_objetive" runat="server" Culture="es-PE" NumberFormat-DecimalDigits="0"
                                    MinValue="0" Type="Number" EmptyMessage="sin valor">
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <asp:Label ID="lbl_id_cobertura" runat="server" Visible="false" Text='<%# Bind("cod_cobertura") %>'></asp:Label>
                                <asp:Label ID="lbl_id_elemento" runat="server" Visible="false" Text='<%# Bind("idElemento") %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <br />
            <asp:Button ID="btn_guardarNew" runat="server" Text="Guardar" CssClass="buttonGuardar"
                Height="25px" Width="164px" OnClick="btn_guardarNew_Click" />
            <br />
        </div>
    </asp:Panel>
</div>
<div id="NewMesajeConfirmacion">
    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_NewConfirmMessage" runat="server"
        TargetControlID="btn_guardarNew" DisplayModalPopupID="ModalPopupExtender_NewConfirmMessage">
    </cc1:ConfirmButtonExtender>
    <cc1:ModalPopupExtender ID="ModalPopupExtender_NewConfirmMessage" runat="server"
        CancelControlID="btn_newCancelar" BackgroundCssClass="modalBackground" TargetControlID="btn_guardarNew"
        OkControlID="btn_newAceptar" PopupControlID="Panel_NewConfirmMessage">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel_NewConfirmMessage" runat="server" BackColor="White" BorderColor="#0099CB"
        BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="120px"
        Width="400px" Style="display: none;">
        <div align="center">
            <div style="font-family: verdana; font-size: medium; color: #D01887;">
                Mensaje de confirmación</div>
            <br />
            ¿Esta seguro de confirmar la actualización.?
            <br />
            <br />
            <br />
            <asp:Button ID="btn_newAceptar" runat="server" Text="Aceptar" CssClass="buttonNew"
                Height="25px" Width="164px" />
            <asp:Button ID="btn_newCancelar" runat="server" Text="Cancelar" CssClass="buttonNew"
                Height="25px" Width="164px" />
        </div>
    </asp:Panel>
</div>
<div id="validationMessage">
    <asp:Button ID="btn_dispara_popupvalidar" runat="server" CssClass="alertas" Text="ocultar"
        Width="95px" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender_validationMessage" runat="server"
        TargetControlID="btn_dispara_popupvalidar" PopupControlID="Panel_validationMessage"
        BackgroundCssClass="modalBackground" CancelControlID="btn_CancelValidationMessage">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel_validationMessage" runat="server" BackColor="White" BorderColor="#0099CB"
        BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="120px"
        Width="400px" Style="display: none;">
        <div align="center">
            <div style="font-family: verdana; font-size: medium; color: #D01887;">
                Mensaje</div>
            <br />
            <asp:Label ID="lbl_msj_validation" runat="server"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Button ID="btn_CancelValidationMessage" runat="server" Text="Aceptar" CssClass="buttonNew"
                Height="25px" Width="164px" />
        </div>
    </asp:Panel>
</div>
<%--  </ContentTemplate>
</asp:UpdatePanel>
<cc2:ModalUpdateProgress ID="ModalUpdateProgress_objetivo" runat="server" DisplayAfter="3"
    AssociatedUpdatePanelID="up_objetivo" BackgroundCssClass="modalProgressGreyBackground"
    DynamicLayout="true">
    <ProgressTemplate>
        <div>
            <img alt="Procesando" src="../../../images/loading5.gif" style="vertical-align: middle" />
        </div>
    </ProgressTemplate>
</cc2:ModalUpdateProgress>
<cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender_objetivos" runat="server"
    TargetControlID="up_objetivo">
    <Animations> 
                    <OnUpdated>
                        <Sequence>
                          <FadeOut Duration="0.2" Fps="30" />
                          <FadeIn  Duration="0.2" Fps="30" />
                        </Sequence>
                  </OnUpdated>
    </Animations>
</cc1:UpdatePanelAnimationExtender>--%>
