<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SIGE.login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>:::XPlora-Inicio</title>
    <link href="Pages/css/SLogin.css" rel="stylesheet" type="text/css" />
    <link href="Pages/css/backstilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ValidateEvent(oSrc, args){A
        args.IsValid = ((args.Value % 2) == 0);
        }
    </script>
    <style type="text/css">
        .AnchoMensaje
        {
            width: 347px;
        }
        .style1
        {
            height: 138px;
        }
    </style>
</head>
<body>
    <div >
        <img src="Pages/ImgBooom/logotipo.png" alt="" style="float:left" />
        <img src="Pages/ImgBooom/logo_luckyn.png" alt="" 
            style="float:right; height: 56px;" />
    </div>
    <div>        
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <asp:UpdatePanel ID="UpanelLogin" runat="server" UpdateMode="Conditional">
                <contenttemplate>
                    <div>
                        <asp:Panel CssClass="LuckyIluminado" style=" margin:auto; width:500px; height:400px" 
                            ID="PanelLogin" runat="server" DefaultButton="btningreso" ForeColor="Black">
                            <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3"
                                AssociatedUpdatePanelID="UpanelLogin" BackgroundCssClass="modalProgressGreyBackground" >
                                <ProgressTemplate>
                                    <div class="modalPopup">
                                        <div>Procesando sus Credenciales...</div>
                                        <div><img alt="Procesando Credenciales" src="Pages/images/loading5.gif"/></div>
                                        <!--style="vertical-align: middle"-->
                                    </div>
                                </ProgressTemplate>
                            </cc2:ModalUpdateProgress>
                            <div style="padding-top:150px">
                                <table style="margin:auto">
                                    <tr>
                                        <td><asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtuser" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="8pt" ForeColor="White" Text="USUARIO">
                                            </asp:Label>
                                        </td>
                                        <td class="fondotextbox">
                                            <asp:TextBox ID="txtuser" runat="server" BorderStyle="None"
                                                CssClass="textbox" Width="120px" oncopy="return false" onpaste="return false" 
                                                oncut="return false" TextMode="SingleLine">
                                            </asp:TextBox>
                                        </td>     
                                        <td><asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtuser"
                                                ErrorMessage="El nombre de Usuario es requerido" ToolTip="El nombre de Usuario es requerido."
                                                ValidationGroup="Login1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style=" float:right">
                                            <asp:Label ID="Label1" runat="server" AssociatedControlID="txtpassw" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="8pt" ForeColor="White" Text="CLAVE">
                                            </asp:Label>
                                        </td>
                                        <td class="fondotextbox">
                                            <asp:TextBox ID="txtpassw" runat="server" BorderStyle="None" CssClass="textbox" 
                                                TextMode="Password" Width="120px" oncopy="return false" onpaste="return false" 
                                                oncut="return false">
                                            </asp:TextBox>
                                        </td>
                                        <td><asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtpassw"
                                            ErrorMessage="La Contraseña es requerida." ToolTip="La Contraseña es requerida."
                                            ValidationGroup="Login1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style=" margin:auto; width:135px">
                                <asp:Label ID="LblOlvido" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="6pt"
                                     ForeColor="#999999" Text="OLVIDO SU CONTRASEÑA">
                                </asp:Label>
                                <asp:ImageButton ID="BtnOlvido" runat="server" AlternateText="Aquí le indicamos que hacer..."
                                     ImageUrl="~/Pages/ImgBooom/boton_olvido_contrasen¦âa.png" />
                            </div>
                            <div style=" margin:auto; width:100px">        
                                <asp:Button ID="btningreso" runat="server" CommandName="Login" CssClass="btIngresar" 
                                    Height="55px" OnClick="btningreso_Click" Width="100px" ValidationGroup="Login1"/>
                            </div>
                        </asp:Panel>
                    </div>
                    <asp:Panel ID="PRecordatorio"  runat="server" BackImageUrl="~/Pages/ImgBooom/Fondo_OlvidoContraseña.png"
                         Style="height:355px; width:525px; display: none;" >
                        <div style=" float:right;">
                            <asp:ImageButton ID="BtnCOlv" runat="server" AlternateText="Oprima aquí para cancelar solicitud"
                                BackColor="Transparent" Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png"
                                OnClick="BtnCOlv_Click" Width="23px" />
                            <table>
                                <tr>
                                    <td style=" float:right"></td>
                                    <td style=" margin:auto"></td>
                                </tr>
                            </table>
                        </div>
                        <br /><br /><br /><br />
                        <table style=" margin:auto" class="fondoOlvContrs">
                            <tr>
                                <td>
                                    <table style=" margin:auto">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="11px"
                                                    Text="Usuario:" ForeColor="#666666"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtusaolvi" runat="server" AutoCompleteType="Disabled" Width="155px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="11px"
                                                Text="Palabra recordatoria:" ForeColor="#666666"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtrecall" runat="server" Width="155px"></asp:TextBox>
                                        </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="11px"
                                                    Text="Email:" ForeColor="#666666"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtfrom" runat="server" Width="155px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="UReqEmail" runat="server" ControlToValidate="txtfrom"
                                                    Display="none" ErrorMessage="Formato de email invalido: Este debe ser user@mail.xyz"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                </asp:RegularExpressionValidator>
                                                <asp:ValidatorCalloutExtender ID="UReqEmail_ValidatorCalloutExtender" runat="server"
                                                    Enabled="True" TargetControlID="UReqEmail">
                                                </asp:ValidatorCalloutExtender>
                                            </td>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblcoutry" runat="server" Text="Pais" ForeColor="#666666">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbpaisolv" runat="server" Width="150px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </tr>
                                    </table>
                                    <br />
                                    <div style=" text-align:center">
                                        <asp:Button ID="btnenvio" runat="server" BorderWidth="1px" CssClass="buttonPlan"
                                            Height="21px" OnClick="btnenvio_Click" Style="margin: 0px" Text="Enviar Solicitud" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" Enabled="True" TargetControlID="BtnOlvido"
                        PopupControlID="PRecordatorio" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                </contenttemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="uplogin" runat="server">
                <contenttemplate>
                    <asp:Button ID="btndipararalerta" runat="server" Text="" Height="0px" CssClass="alertas"
                        Width="0px" Enabled="False" />
                    <asp:ModalPopupExtender ID="ModalPopupCanal" runat="server" Enabled="True" TargetControlID="btndipararalerta"
                        PopupControlID="PMensajes" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="PMensajes" runat="server" Width="355px" Height="197px" Style="display: none;">
                        <table id="tmensaje" runat="server" align="center" width="95%">
                            <tr>
                                <td style=" margin:auto; text-align:center" class="AnchoMensaje">
                                    <br /><br /><br />
                                    <asp:Label ID="lblencabezado" runat="server" Font-Bold="True" Font-Names="Verdana"
                                        Font-Size="10pt" ForeColor="White"></asp:Label>
                                    <br /><br />
                                    <asp:Label ID="lblmensajegeneral" runat="server" Font-Names="Verdana" Font-Size="9pt"
                                        ForeColor="White">
                                    </asp:Label>
                                    <br /><br /><br />
                                    <asp:Button ID="btnaceptar" runat="server" CssClass="buttonPlan" Text="Aceptar" Font-Bold="True"
                                        Font-Names="Verdana" Font-Overline="False" Style="cursor: hand; margin:auto;" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="White" Width="100px" BackColor="#D5DBE4" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="1px" Height="25px" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="BtnAlerta" runat="server" Text="" Height="0px" CssClass="alertas"
                        Width="0px" Enabled="False" />
                    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" Enabled="True" TargetControlID="BtnAlerta"
                        PopupControlID="Pmensaje2" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="Pmensaje2" runat="server" Width="355px" Height="197px" Style="display: none;">
                        <table id="Table1" runat="server" align="center" width="95%">
                            <tr>
                                <td style=" margin:auto" class="AnchoMensaje">
                                    <br /><br /><br />
                                    <asp:Label ID="LblEncabezado2" runat="server" Font-Bold="True" Font-Names="Verdana"
                                        Font-Size="10pt" ForeColor="White"></asp:Label>
                                    <br /><br />
                                    <asp:Label ID="LblMesanje2" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="White"></asp:Label>
                                    <br /><br />
                                    <asp:Button ID="btnacepmensaje2" runat="server" CssClass="buttonPlan" Text="Aceptar"
                                        Font-Bold="True" Font-Names="Verdana" Font-Overline="False" Style="cursor: hand;"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="White" Width="100px"
                                        BackColor="#D5DBE4" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                        Height="25px" OnClick="btnacepmensaje2_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <%--panel de mensaje de usuario paneles   --%>
                            <asp:Panel ID="MensajeSeguimiento" runat="server" Height="169px" Style="display: none;" Width="332px">
                                <table align="center">
                                    <tr>
                                        <td align="center" style="height: 119px; width: 79px;" valign="top"><br /></td>
                                        <td style="width: 220px; height: 119px;" valign="top"><br />
                                            <asp:Label ID="lblencabezadoSeguimiento" runat="server" CssClass="labelsTit"></asp:Label><br /><br />
                                            <asp:Label ID="lblmensajegeneralSeguimiento" runat="server" CssClass="labels"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr><td align="center"><asp:Button ID="BtnaceptaSeguimiento" runat="server" 
                                            BorderStyle="Solid" CssClass="buttonPlan" Text="Aceptar" /></td></tr>
                                </table>
                            </asp:Panel>
                            <asp:ModalPopupExtender ID="MPMensajeSeguimiento" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="MensajeSeguimiento" TargetControlID="btndipararseguimiento">
                            </asp:ModalPopupExtender>
                            <asp:Button ID="btndipararseguimiento" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>	
                </contenttemplate>
            </asp:UpdatePanel>
        </form>
    </div>
</body>
</html>
