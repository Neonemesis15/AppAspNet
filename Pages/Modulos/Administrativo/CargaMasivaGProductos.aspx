<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargaMasivaGProductos.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Administrativo.CargaMasivaGProductos" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Carga Masiva Gestion Productos</title>
    <style type="text/css">
        .style49
        {
            width: 296px;
            height: 145px;
        }
        .style50
        {
            height: 145px;
            width: 62px;
        }
    </style>
    <link href="../../css/StiloAdministrativo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <br /><br />
                    <table  align="center">
                        <tr>
                            <td>
                                <asp:Label ID="LblCargamasivaMarca" runat="server" CssClass="labelsTit2" Text="CARGA MASIVA " />
                            </td>
                        </tr>
                    </table>
                    <br /><br />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="LblCargarArchivo" runat="server" Text="Nombre de Archivo:" Font-Names="Arial"
                                    Font-Size="9pt"  CssClass="labelsN"></asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpCMasivaMArca" runat="server" Width="320px" />                           
                                <asp:TextBox ID="TxtCodBrand" runat="server" Visible="false"></asp:TextBox>
                                 <asp:TextBox ID="TxtCodProductType" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br /><br />
                    <div align="center">
                        <asp:Button ID="btnCargaArchivo" runat="server" CssClass="buttonPlan" Text="Cargar Archivo"
                        Width="98px" OnClick="btnCargaArchivo_Click" />
                    </div>
                    <div align="center">
                        <asp:GridView ID="GvCargaArchivo" runat="server" Visible="false">
                        </asp:GridView>
                    </div>
                </div>
                <asp:Panel ID="Alertas" runat="server" Style="display: none;" DefaultButton="BtnAceptarAlert"
                    Height="215px" Width="375px">
                    <table align="center">
                        <tr>
                            <td align="center" class="style50" valign="top">
                                <br />
                                &nbsp;
                            </td>
                            <td class="style49" valign="top">
                                <br />
                                <asp:Label ID="LblAlert" runat="server" Text="Señor Usuario" CssClass="labelsMensaje"></asp:Label>
                                <br /><br />
                                <asp:Label ID="LblFaltantes" runat="server" CssClass="labelsMensaje"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="BtnAceptarAlert" runat="server" CssClass="buttonPlan" Text="Aceptar" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupAlertas" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" Enabled="True" OkControlID="BtnAceptarAlert" PopupControlID="Alertas"
                    TargetControlID="Btndisparaalertas">
                </cc1:ModalPopupExtender>
                <asp:Button ID="Btndisparaalertas" runat="server" CssClass="alertas" Text="" Visible="true"
                    Width="0px" />
            </ContentTemplate>
            <Triggers>
                <%-- <asp:PostBackTrigger ControlID="btnloadPdv" />--%>
                <asp:PostBackTrigger ControlID="btnCargaArchivo" />
            </Triggers>
        </asp:UpdatePanel>
        <%--<cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server"
                    AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">
                        <div>
                            Cargando...
                        </div>
                        <br />
                        <div>
                            <img alt="Procesando" src="../../images/loading5.gif" style="vertical-align: middle" />
                        </div>
                    </div>
                </ProgressTemplate>
    </cc2:ModalUpdateProgress>--%>
    </form>
</body>
</html>
