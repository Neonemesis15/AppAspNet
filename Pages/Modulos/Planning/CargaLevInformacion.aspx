<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargaLevInformacion.aspx.cs" Inherits="SIGE.Pages.Modulos.Planning.CargaLevInformacion" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
     <link href="../../css/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <cc2:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc2:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <asp:Button ID="btndipararalerta" runat="server" Text="" Height="0px" CssClass="alertas"
                    Width="0" Enabled="False" />
                <table bgcolor="#7F99CC" style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="LblTitCargarArchivo" runat="server" CssClass="labels" Text="Cargar Masiva"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <div align="center">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="LblSelPresupuestoPDV" runat="server" CssClass="labelsN" Text="Presupuesto : "></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LblPlanning" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="LblPresupuestoPDV" runat="server" CssClass="labelsN"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <table frame="box" align="center">
                    <tr>
                        <td>
                            <asp:Label ID="LblCargarArchivo" runat="server" Text="Nombre de Archivo:" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" CssClass="labelsN"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpLevInformacion" runat="server" Width="337px" />
                        </td>
                    </tr>
                    <tr>
                    <td colspan="2" style="margin:auto">
                    <a visible="false" runat="server" id="formato_Producto" class="button" href="../../formatos/Formato_Lev_Info_Productos.xls"><span>Descargar Formato</span></a>
                    <a visible="false" runat="server" id="formato_Marca" class="button" href="../../formatos/Formato_Lev_Info_Marca.xls"><span>Descargar Formato</span></a>
                    <a visible="false" runat="server" id="formato_Familia" class="button" href="../../formatos/Formato_Lev_Info_Familia.xls"><span>Descargar Formato</span></a>
                    <a visible="false" runat="server" id="formato_Categoria" class="button" href="../../formatos/Formato_Lev_Info_Categoria.xls"><span>Descargar Formato</span></a>
                    </td>
                    </tr>
                </table>
                <br />
                <br />
                <br />
                <table align="center" style="display: none;">
                    <tr>
                        <td>
                            <asp:DropDownList ID="CbmPDV" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="CmbOpePlanning" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <table align="center">
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="BtnCargaArchivo" runat="server" CssClass="buttonNewPlan" Text="Cargar Archivo"
                                    Height="25px" Width="164px" onclick="BtnCargaArchivo_Click"  />
                            </td>
                        </tr>
                    </table>
                    <div align="center">
                        <asp:GridView ID="GvProductosPlanning" runat="server" Visible="false">
                        </asp:GridView>
                    </div>
            </div>
            <%--panel de mensaje de usuario   --%>
            <asp:Panel ID="Pmensaje" runat="server" Height="169px" Width="332px" Style="display: none;">
                <table align="center">
                    <tr>
                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                            <br />
                        </td>
                        <td style="width: 238px; height: 119px;" valign="top">
                            <br />
                            <asp:Label ID="lblencabezado" runat="server" CssClass="labels"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="lblmensajegeneral" runat="server" CssClass="labels"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnaceptar" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                Text="Aceptar" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc2:ModalPopupExtender ID="ModalPopupMensaje" runat="server" Enabled="True" TargetControlID="btndipararalerta"
                PopupControlID="Pmensaje" BackgroundCssClass="modalBackground">
            </cc2:ModalPopupExtender>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnCargaArchivo" />
        </Triggers>
    </asp:UpdatePanel>
    <cc1:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server"   
       DisplayAfter="3"
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
    </cc1:ModalUpdateProgress>
    </form>
</body>
</html>
