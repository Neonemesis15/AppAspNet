<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MOD_Admin.aspx.cs" Inherits="SIGE.Pages.Modulos.Administrativo.MOD_Admin" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modulo Administrativo</title>
    <link href="../../css/StiloAdministrativo.css" rel="stylesheet" type="text/css" />
    <link href="../../css/Menu.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"></cc1:ToolkitScriptManager>
        <div class="Header" align="center" style="height:140px;">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Pages/ImgBooom/logotipo.png" ImageAlign="Left" Width="280px"  />
        </div>
        <div class="HeaderRight">
            <table width="100%" align="center">
                <tr>
                    <td align="right">
                        <asp:Label ID="lbltitulo" runat="server" Text="Administración" CssClass="stilo_titulo"></asp:Label><br />
                        <asp:Label ID="lblUsuario" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="#0033CC"></asp:Label>
                        <asp:Label ID="usersession" runat="server" Visible="False" Font-Names="Verdana" Font-Size="11px" ForeColor="#0033CC"></asp:Label><br />
                        <asp:ImageButton ID="ImgCloseSession" runat="server" Height="20px" ImageUrl="~/Pages/images/SesionClose.png" Width="84px" onclick="ImgCloseSession_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="MarcaAgua"></div>
        <asp:UpdatePanel ID="Uppanel" runat="server">
            <ContentTemplate>
                <%--   
                <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="LblUpProgressUP" runat="server" Text="." ForeColor="#C7CEDA"></asp:Label>
                        </td>
                        <td>
                            <asp:Panel ID="PProgresso" runat="server">
                                <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <div style="text-align: center;">
                                            <img alt="Procesando" src="../../images/loading1.gif" style="vertical-align: middle" />
                                            Por favor espere...
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                --%>
                <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server"
                    AssociatedUpdatePanelID="Uppanel" BackgroundCssClass="modalProgressGreyBackground">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            <div>
                                Cargando...
                            </div><br />
                            <div>
                                <img alt="Procesando" src="../../images/loading5.gif" style="vertical-align: middle" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </cc2:ModalUpdateProgress>

                <div class="anchotabla">
                    <table width="100%" align="center">
                        <tr >
                            <td style="width: 210px; min-width: 210px;" >                            
                                <asp:Button ID="ImgBtnPais" runat="server" CssClass="buttonAdmPais" onclick="ImgBtnPais_Click" />                             
                                <asp:Button ID="ImgBtnUsuario" runat="server" CssClass="buttonAdmUsuario" onclick="ImgBtnUsuario_Click" /> <br />
                                <asp:Button ID="ImgBtnProducto" runat="server" CssClass="buttonAdmProducto" onclick="ImgBtnProducto_Click" />                            
                                <asp:Button ID="ImgBtnPdv" runat="server" CssClass="buttonAdmPDV" onclick="ImgBtnPdv_Click" /> <br />
                                <asp:Button ID="ImgBtnInformes" runat="server" CssClass="buttonAdmInformes" onclick="ImgBtnInformes_Click" />
                                <asp:Button ID="ImgBtnGeneral" runat="server" CssClass="buttonAdmGeneral" onclick="ImgBtnGeneral_Click" /> <br /><br /><br /> 
                            </td>
                            <td valign="top" style="width: 900px; min-width: 900px;">
                                <iframe id="Iframe" runat ="server" src="IframeTransparente.aspx" height="530px" allowtransparency="true" scrolling="no"
                                    width="100%" frameborder="0"></iframe>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
          
        <div class="push">
        </div>

        <div class="footer" align="left">
            <br /><br /><br /><br />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Pages/ImgBooom/logo_lucky.png" />
        </div>
    </form>
</body>
</html>
