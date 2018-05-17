<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mod_Planning.aspx.cs" Inherits="SIGE.Pages.Modulos.Planning.Mod_Planning" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/StyleModCliente.css" rel="stylesheet" type="text/css" />
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        </style>
</head>
<body>
    <div >
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Pages/ImgBooom/logotipo.png"></asp:Image>
       
    </div>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UPCLiente" runat="server">
        <ContentTemplate>
            <div class="HeaderRight">
                <table width="100%" align="center">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblUsuario" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="#0033CC"></asp:Label>
                            <asp:Label ID="usersession" runat="server" Visible="False" Font-Names="Verdana" Font-Size="11px"
                                ForeColor="#0033CC"></asp:Label>
                            <br />
                            <asp:ImageButton ID="ImgCloseSession" runat="server" Height="16px" ImageUrl="~/Pages/images/SesionClose.png"
                                Width="84px" OnClick="ImgCloseSession_Click" />
                        </td>
                    </tr>
                </table>
            </div>
               
            <div>   
                <%--  <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="LblUpProgressUP" runat="server" Text="." ForeColor="#919FFF"></asp:Label>
                        </td>
                        <td>
                            <asp:Panel ID="PProgresso" runat="server">
                                <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
                                    <ProgressTemplate>
                                        <div style="text-align: center;">
                                            <img src="../../images/loading1.gif" alt="Procesando" style="width: 90px; height: 13px" />
                                            <asp:Label ID="lblProgress" runat="server" Text=" Procesando, por favor espere ..."
                                                ForeColor="#0033CC"></asp:Label>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>--%>                
                <table align="center" width="100%">
                    <tr>
                        <td>
                            <iframe id="iframeExcel" runat="server" src="" width="100%" height="440px" allowtransparency="true"
                                scrolling="no"></iframe>
                        </td>
                    </tr>
                </table>
            </div>
            <%--panel de mensaje de usuario   
            <asp:Button ID="btndipararalerta" runat="server" Text="" Height="0px" CssClass="alertas"
                Width="0" Enabled="False" />
            <asp:Panel ID="PCanal" runat="server" Height="169px" Width="332px" Style="display: none;">
                <table align="center">
                    <tr>
                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                            <br />
                        </td>
                        <td style="width: 238px; height: 119px;" valign="top">
                            <br />
                            <asp:Label ID="lblencabezado" runat="server" CssClass="labelsTit"></asp:Label>
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
            <cc1:ModalPopupExtender ID="ModalPopupCanal" runat="server" Enabled="True" TargetControlID="btndipararalerta"
                PopupControlID="PCanal" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
 <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3" 
 AssociatedUpdatePanelID="UPCLiente" BackgroundCssClass="modalProgressGreyBackground">
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
    </cc2:ModalUpdateProgress>
    <div class="Regresa" align="left">
        <br />
        <br />
        
        <br />
        <br />
        <asp:Button ID="BtnRegresar" runat="server" CssClass="Regresar" 
            onclick="BtnRegresar_Click"  />
    </div>
    </form>
</body>
</html>
