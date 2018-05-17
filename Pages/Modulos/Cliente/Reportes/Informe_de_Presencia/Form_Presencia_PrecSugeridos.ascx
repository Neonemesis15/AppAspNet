<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Form_Presencia_PrecSugeridos.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informe_de_Presencia.Form_Presencia_PrecSugeridos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%--<asp:UpdatePanel ID="up_PSugerido" runat="server" RenderMode="Inline" >
    <ContentTemplate>--%>
        <div align="left">
            Año:<telerik:RadComboBox ID="rcmb_añoPS" runat="server">
            </telerik:RadComboBox>
            Mes:<telerik:RadComboBox ID="rcmb_mesPS" runat="server">
            </telerik:RadComboBox>
            <asp:Button ID="btn_buscarPS" runat="server" Text="Buscar" CssClass="buttonSearch"
                Height="25px" Width="164px" OnClick="btn_buscarPS_Click" />
        </div>
        <br />
        <asp:Label ID="lbl_info" runat="server"></asp:Label>
        <telerik:RadGrid ID="RadGrid_PSugeridosPS" runat="server" AutoGenerateColumns="False">
            <MasterTableView NoMasterRecordsText="Sin resultados.">
                <Columns>
                    <telerik:GridBoundColumn DataField="cod_Product" HeaderText="Código" UniqueName="cod_Product">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Product_Name" HeaderText="Nombre" UniqueName="Product_Name">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="English_Alias" HeaderText="Nombre en ingles"
                        UniqueName="English_Alias">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="TemplateColumn1">
                        <HeaderTemplate>
                            Precio
                        </HeaderTemplate>
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="rtxt_precio" runat="server" Culture="es-PE" DbValue='<%# Eval("value_sku") %>'
                                NumberFormat-DecimalDigits="2" MinValue="0" Type="Number" EmptyMessage="sin valor">
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                        <ItemTemplate>
                            <asp:Label ID="lbl_id_año" runat="server" Visible="false" Text='<%# Bind("id_year") %>'></asp:Label>
                            <asp:Label ID="lbl_id_mes" runat="server" Visible="false" Text='<%# Bind("id_month") %>'></asp:Label>
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
            <asp:Button ID="btn_nuevoPS" runat="server" Text="Nuevo" CssClass="buttonNew" Height="25px"
                Width="164px" />
            <asp:Button ID="btn_guardarPS" runat="server" Text="Guardar" CssClass="buttonGuardar"
                Height="25px" Width="164px" OnClick="btn_guardarPS_Click" />
        </div>
        <div id="mesajeConfirmacionPS">
            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_confirmMessagePS" runat="server"
                TargetControlID="btn_guardarPS" DisplayModalPopupID="ModalPopupExtender_confirmMessagePS">
            </cc1:ConfirmButtonExtender>
            <cc1:ModalPopupExtender ID="ModalPopupExtender_confirmMessagePS" runat="server" CancelControlID="btn_cancelarPS"
                BackgroundCssClass="modalBackground" TargetControlID="btn_guardarPS" OkControlID="btn_aceptarPS"
                PopupControlID="Panel_confirmMessagePS">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel_confirmMessagePS" runat="server" BackColor="White" BorderColor="#0099CB"
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
                    <asp:Button ID="btn_aceptarPS" runat="server" Text="Aceptar" CssClass="buttonNew"
                        Height="25px" Width="164px" />
                    <asp:Button ID="btn_cancelarPS" runat="server" Text="Cancelar" CssClass="buttonNew"
                        Height="25px" Width="164px" />
                </div>
            </asp:Panel>
        </div>
        <div id="div_addPSugeridoPS">
            <cc1:ModalPopupExtender ID="ModalPopupExtender_addPS" runat="server" BackgroundCssClass="modalBackground"
                DropShadow="True" Enabled="True" PopupControlID="Panel_addObjPS" TargetControlID="btn_nuevoPS"
                CancelControlID="btn_closePS" DynamicServicePath="">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel_addObjPS" runat="server" BackColor="White" BorderColor="#0099CB"
                BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="650px"
                Width="1000px" Style="display: none;" ScrollBars="Vertical">
                <div>
                    <asp:ImageButton ID="btn_closePS" runat="server" BackColor="Transparent" Height="22px"
                        ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" /></div>
                <div align="center">
                    <div style="font-family: verdana; font-size: medium; color: #D01887;">
                        Agregar Precios Sugeridos</div>
                    <br />
                    <br />
                    <div>
                        Crear para año:
                        <telerik:RadComboBox ID="rcmb_añoNewPS" runat="server">
                        </telerik:RadComboBox>
                        Mes
                        <telerik:RadComboBox ID="rcmb_mesNewDesdePS" runat="server">
                        </telerik:RadComboBox>
                        <telerik:RadComboBox ID="rcmb_mesNewHastaPS" runat="server">
                        </telerik:RadComboBox>
                    </div>
                    <br />
                    <telerik:RadGrid ID="RadGrid_newPSugeridoPS" runat="server" AutoGenerateColumns="false"
                        OnNeedDataSource="RadGrid_newPSugeridoPS_NeedDataSource">
                        <MasterTableView NoMasterRecordsText="Sin resultados.">
                            <Columns>
                                <telerik:GridBoundColumn DataField="cod_Product" HeaderText="Código" UniqueName="cod_Product">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Product_Name" HeaderText="Nombre" UniqueName="Product_Name">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="English_Alias" HeaderText="Nombre en ingles"
                                    UniqueName="English_Alias">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="TemplateColumn1">
                                    <HeaderTemplate>
                                        Precio
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="rtxt_precio" runat="server" Culture="es-PE" NumberFormat-DecimalDigits="2"
                                            MinValue="0" Type="Number" EmptyMessage="sin valor">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <br />
                    <asp:Button ID="btn_guardarNewPS" runat="server" Text="Guardar" CssClass="buttonGuardar"
                        Height="25px" Width="164px" OnClick="btn_guardarPSNew_Click" />
                    <br />
                </div>
            </asp:Panel>
        </div>
        <div id="NewMesajeConfirmacionPS">
            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_NewConfirmMessagePS" runat="server"
                TargetControlID="btn_guardarNewPS" DisplayModalPopupID="ModalPopupExtender_NewConfirmMessagePS">
            </cc1:ConfirmButtonExtender>
            <cc1:ModalPopupExtender ID="ModalPopupExtender_NewConfirmMessagePS" runat="server"
                CancelControlID="btn_newCancelarPS" BackgroundCssClass="modalBackground" TargetControlID="btn_guardarNewPS"
                OkControlID="btn_newAceptarPS" PopupControlID="Panel_NewConfirmMessagePS">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel_NewConfirmMessagePS" runat="server" BackColor="White" BorderColor="#0099CB"
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
                    <asp:Button ID="btn_newAceptarPS" runat="server" Text="Aceptar" CssClass="buttonNew"
                        Height="25px" Width="164px" />
                    <asp:Button ID="btn_newCancelarPS" runat="server" Text="Cancelar" CssClass="buttonNew"
                        Height="25px" Width="164px" />
                </div>
            </asp:Panel>
        </div>
        <div id="validationMessage">
            <asp:Button ID="btn_dispara_popupvalidarPS" runat="server" CssClass="alertas" Text="ocultar"
                Width="95px" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender_validationMessagePS" runat="server"
                TargetControlID="btn_dispara_popupvalidarPS" PopupControlID="Panel_validationMessagePS"
                BackgroundCssClass="modalBackground" CancelControlID="btn_CancelValidationMessagePS">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel_validationMessagePS" runat="server" BackColor="White" BorderColor="#0099CB"
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
                    <asp:Button ID="btn_CancelValidationMessagePS" runat="server" Text="Aceptar" CssClass="buttonNew"
                        Height="25px" Width="164px" />
                </div>
            </asp:Panel>
        </div>
<%--    </ContentTemplate>
</asp:UpdatePanel>
<cc2:ModalUpdateProgress ID="ModalUpdateProgress_PSugerido" runat="server" DisplayAfter="3"
    AssociatedUpdatePanelID="up_PSugerido" BackgroundCssClass="modalProgressGreyBackground"
    DynamicLayout="true">
    <ProgressTemplate>
        <div>
            <img alt="Procesando" src="../../../images/loading5.gif" style="vertical-align: middle" />
        </div>
    </ProgressTemplate>
</cc2:ModalUpdateProgress>
<cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender_PSugerido" runat="server"
    TargetControlID="up_PSugerido">
    <Animations> 
                    <OnUpdated>
                        <Sequence>
                          <FadeOut Duration="0.2" Fps="30" />
                          <FadeIn  Duration="0.2" Fps="30" />
                        </Sequence>
                  </OnUpdated>
    </Animations>
</cc1:UpdatePanelAnimationExtender>
--%>