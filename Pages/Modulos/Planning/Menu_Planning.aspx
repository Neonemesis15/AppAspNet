<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu_Planning.aspx.cs" Inherits="SIGE.Pages.Modulos.Planning.Menu_Planning" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!--
-- Author       : Carlos Hernandez Rincon (CHR)
-- Create date  : 01/01/2010
-- Description  : Página principal para el Planning.
--
-- Change History:
-- 26/10/2018 Pablo Salas Alvarez (PSA): Refactoring.
-->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Planning. Menú Principal</title>

    <script type="text/javascript">

        function pageLoad() {
        }
        
    </script>

    <link href="../../css/layout.css" rel="stylesheet" type="text/css" />
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
    <link href="../../css/Webfotter.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style49
        {
            width: 238px;
            height: 119px;
        }
        .style50
        {
            height: 119px;
            width: 79px;
        }
    </style>
</head>
<body>
    
    <div class="Header" align="center" style="height: 150px;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Pages/ImgBooom/logotipo.png"></asp:Image>
    </div>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
     <ContentTemplate>
   
    </ContentTemplate>
    </asp:UpdatePanel>
    <div class="HeaderRight">
        <asp:Label ID="lblUsuario" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="#114092"></asp:Label>
        <asp:Label ID="usersession" runat="server" Visible="False" Font-Names="Verdana" Font-Size="9pt"
            ForeColor="#114092"></asp:Label>
        <br />
        
        <asp:ImageButton ID="ImgCloseSession" runat="server" Height="16px" ImageUrl="~/Pages/images/SesionClose.png"
            Width="84px" OnClick="ImgCloseSession_Click" />
            
    </div>
    <div class="menuplanning">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>             
                <table align="center">
                    <tr>
                        <td><asp:Label ID="LblUpProgressUP" runat="server" Text="." ForeColor="White"></asp:Label></td>
                        <td>
                            <asp:Panel ID="PProgresso" runat="server">
                                <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
                                    <ProgressTemplate>
                                        <div style="text-align: center;">
                                            <img src="../../images/loading1.gif" alt="Procesando" style="width: 90px; height: 13px" />
                                            <asp:Label ID="lblProgress" runat="server" Text=" Procesando, por favor espere ..."
                                                ForeColor="Black"></asp:Label>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                
                <div align="center" >
                    <asp:Label ID="SelModulo" runat="server" Text="Módulo Alternativo" ForeColor="ActiveCaption" 
                        Font-Names="Verdana" Font-Size="12px"></asp:Label>
                    <asp:DropDownList ID="cmbselModulos" runat="server"  Font-Names="Verdana" 
                        Font-Size="11px" AutoPostBack="True" onselectedindexchanged="cmbselModulos_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="SelCliente" runat="server" Text="Cliente" ForeColor="ActiveCaption" 
                        Font-Names="Verdana" Font-Size="12px"></asp:Label>
                    <asp:DropDownList ID="cmbcliente" runat="server"  Font-Names="Verdana" 
                        Font-Size="11px" Visible = "false"></asp:DropDownList>
                    <asp:Button ID="GO" runat="server" Text="Ir" Font-Size="10px" onclick="GO_Click"  Width="26px" />
                </div>
                
                <!-- PANEL - MENU PLANNING - INI -->
                <table align="center" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td><img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png" width="6"> </img></td>
                        <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                            <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1"></img></td>
                        <td><img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png" width="6"> </img></td>
                    </tr>
                    <tr>
                        <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                            <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6"></img></td>
                        <td bgcolor="#CCD4E1" valign="top">
                            

                            <asp:Panel ID="PnlMenuPlanning" CssClass="borde_panel" runat="server" BackColor="White" Width="600px">
                                <div align="center"><br /><br />
                                    <span class="labelsTitN">Menú Planning</span>
                                    <br /><br /><br />
                                    
                                    <table align="center">
                                        <tr>
                                            <td><asp:Button ID="BtnSolicitudes" runat="server" Text="Solicitudes" CssClass="buttonPlanSolicitud"
                                                    Width="164px" Height="25px" /></td>
                                            <td><asp:Button ID="BtnInforComun" runat="server" Text="Informes Comunes" 
                                                CssClass="buttonPlanInfocomun" Width="164px" Height="25px" OnClick="BtnInforComun_Click" /></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Button ID="BtnNuevasCamp" runat="server" Text="Nuevas Campañas" CssClass="buttonNewPlan"
                                                    Width="164px" Height="25px" OnClick="BtnNuevasCamp_Click"   /></td>
                                            <td><asp:Button ID="BtnConsultas" runat="server" Text="Consultar Campañas" CssClass="buttonPlanConsultaCam"
                                                    Width="164px" Height="25px" OnClick="BtnConsultas_Click" visible ="true" /></td>
                                        </tr>
                                        <tr><td colspan="2">
                                                <asp:Button ID="BtnBreaf" runat="server" Text="Breaf" CssClass="buttonPlanBreafCam"
                                                    Width="164px" Height="25px" visible ="false" /></td>
                                        </tr>
                                    </table>
                                    
                                    <asp:Button ID="btncontruplanning" runat="server" CssClass="buttonPlan" OnClick="btncontruplanning_Click"
                                        Text="Construccion de Campañas" Visible="False" />
                                    <asp:Button ID="btnasignaporcanal" runat="server" CssClass="buttonPlan" Text="Asignaciones Por Canal"
                                        OnClick="btnasignaporcanal_Click" Enabled="False" ToolTip="En Contrucción" Visible="False" />
                                    <br /><br /><br />
                                </div>
                            </asp:Panel>



                        </td>
                        
                        <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                            <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6"></img>
                        </td>

                    </tr>

                    <tr>
                        <td><img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png" width="6"> </img></td>
                        <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                            <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png" width="1"> </img>
                        </td>
                        <td><img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png" width="6"> </img></td>
                    </tr>
                </table>
                <!-- PANEL - MENU PLANNING - FIN -->


                <asp:Panel ID="PanelSolicitud" runat="server" Style="display: none;">
                    <table align="center" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td><img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png" 
                                width="6"> </img></td>
                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1"></img>
                            </td>
                            <td><img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png" width="6"> </img></td>
                        </tr>
                        <tr>
                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6"></img>
                            </td>
                            <td bgcolor="#28377d" valign="top">
                                <asp:Panel ID="PSolicitudes" runat="server" Width="593px" Height="397px">
                                    <div align="right">
                                        <asp:ImageButton ID="BtnCloseSolicitudes" runat="server" BackColor="Transparent"
                                            Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px"
                                            OnClick="BtnCloseSolicitudes_Click" />
                                        <asp:Button ID="DisparaCloseSolicit" runat="server" CssClass="alertas" Text="" Visible="true"
                                            Width="0px" />
                                    </div>
                                    
                                    <table align="center">
                                        <tr>
                                            <td><asp:Image ID="ImgMail" runat="server" Height="52px" 
                                                ImageUrl="~/Pages/images/mailreminder.png" Width="65px" />
                                            </td>
                                            <td align="center"><asp:Label ID="LblTitEnvioMail" runat="server" CssClass="labelsTit" 
                                                Text="Novedades / Solicitudes"></asp:Label>
                                            </td>
                                        </tr>
                                    </table><br />

                                    <table align="center">
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="LblMailSolicitante" runat="server" CssClass="labels" Text="De"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtSolicitante" runat="server" Enabled="False" Width="400px" 
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="LblMailPara" runat="server" CssClass="labels" Text="Para"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtEmail" runat="server" Enabled="False" Width="400px" ReadOnly="True">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="LblMotivo" runat="server" CssClass="labels" Text="Asunto"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtMotivo" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Reqmotivo" runat="server" 
                                                    ControlToValidate="TxtMotivo" Display="None" 
                                                    ErrorMessage="Sr. Usuario, por favor no ingrese caracteres especiales y recuerde que no debe inicial con número"
                                                    ValidationExpression="([a-zA-Z][a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]{0,49})">
                                                </asp:RegularExpressionValidator>
                                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" 
                                                    Enabled="True" TargetControlID="Reqmotivo">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="LblMensaje" runat="server" CssClass="labels" Text="Mensaje"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtMensaje" runat="server" Height="113px" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                                <asp:RegularExpressionValidator
                                                    ID="ReqMensaje" runat="server" ControlToValidate="TxtMensaje" Display="None"
                                                    ErrorMessage="Sr. Usuario, recuerde que no debe inicial con número. Máximo ingrese 255 caracteres"
                                                    ValidationExpression="([a-zA-Z][\W\wa-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]{0,255})">
                                                </asp:RegularExpressionValidator>
                                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" 
                                                    Enabled="True" TargetControlID="ReqMensaje">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                    </table><br />
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="ImgEnviarMail" runat="server" AlternateText="Enviar" BorderStyle="None"
                                                    ImageUrl="../../images/BtnEnviarMail.png" 
                                                    onmouseout="this.src = '../../images/BtnEnviarMail.png'" 
                                                    onmouseover="this.src = '../../images/BtnEnviarMailDown.png'" 
                                                    Style="margin-left: 0px" OnClick="ImgEnviarMail_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6"></img>
                            </td>
                        </tr>
                        <tr>
                            <td><img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png" width="6"></img></td>
                            <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png" width="1"> </img>
                            </td>
                            <td><img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png" width="6"> </img></td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPanelSolicitud" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" Enabled="True" OkControlID="DisparaCloseSolicit" PopupControlID="PanelSolicitud"
                    TargetControlID="BtnSolicitudes">
                </cc1:ModalPopupExtender>
                <asp:Button ID="Btndisparaalertas" runat="server" CssClass="alertas" Text="" Visible="true" Width="0px" />
                
                <asp:Panel ID="Alertas" runat="server" DefaultButton="BtnAceptarAlert" Height="169px"
                    Style="display: none;" Width="332px">
                    <table align="center">
                        <tr>
                            <td align="center" class="style50" valign="top">
                                <br />
                            </td>
                            <td class="style49" valign="top">
                                <br />
                                <asp:Label ID="LblAlert" runat="server" CssClass="labels" Font-Bold="True"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="LblFaltantes" runat="server" CssClass="labels"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td align="center">
                                <asp:Button ID="BtnAceptarAlert" runat="server" CssClass="buttonIndexF" Text="Aceptar" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupAlertas" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" Enabled="True" OkControlID="BtnAceptarAlert" PopupControlID="Alertas"
                    TargetControlID="Btndisparaalertas">
                </cc1:ModalPopupExtender>
            
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="footer" style="width:100%;">
        <br />
        <br />
        <br />
        <br />
        <asp:Image ID="Image1" runat="server" 
            ImageUrl="~/Pages/ImgBooom/logo_luckyn.png" ImageAlign="Right">
        </asp:Image>        
    </div>
    </form>
</body>
</html>
