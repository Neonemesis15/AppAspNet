<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_ValidarPeriodos.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_validacion.UC_ValidarPeriodos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="up_validacion" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div>
            <asp:RadioButton ID="rb_valido" runat="server" Text="Valido" GroupName="validacion"
                OnCheckedChanged="rb_valido_CheckedChanged" AutoPostBack="true" />
            <asp:RadioButton ID="rb_invalido" runat="server" Text="Invalido" GroupName="validacion"
                OnCheckedChanged="rb_invalido_CheckedChanged" AutoPostBack="true" />
            <div>
                <asp:Label ID="lbl_periodo" runat="server"></asp:Label>
            </div>
        </div>
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
                    <div style="font-family: verdana; font-size: medium; color: #537DBA;">
                        Mensaje de confirmación</div>
                    <br />
                    <asp:Label ID="lbl_msj_validar" runat="server"></asp:Label>
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
    </ContentTemplate>
</asp:UpdatePanel>
<cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender_validacion" runat="server"
    TargetControlID="up_validacion">
    <Animations> 
            <OnUpdated>
                <Sequence>
                  <FadeOut Duration="0.2" Fps="30" />
                  <FadeIn Duration="0.2" Fps="30" />
                </Sequence>
          </OnUpdated>
    </Animations>
</cc1:UpdatePanelAnimationExtender>
