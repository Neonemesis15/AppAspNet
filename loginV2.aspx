<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="loginV2.aspx.cs" Inherits="SIGE.loginV2" %>

<%-- Referencias de master--%>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%--<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %> --%>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%-- end de master--%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Xplora v2.0
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="UpanelLogin" runat="server">
        <ContentTemplate>
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
                                        <img alt="Procesando" src="Pages/images/loading1.gif" style="vertical-align: middle" />
                                        Por favor espere...
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="PRecordatorio" runat="server" BackImageUrl="~/Pages/ImgBooom/Fondo_OlvidoContraseña.png"
                Height="355px" Width="525px" Style="display: none;">
                <div align="right" style="width: 98%;">
                    <asp:ImageButton ID="BtnCOlv" runat="server" AlternateText="Oprima aquí para cancelar solicitud"
                        BackColor="Transparent" Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png"
                        Width="23px" />
                </div>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <table align="center" class="fondoOlvContrs">
                    <tr>
                        <td>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="11px"
                                            Text="Usuario:" ForeColor="White"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtusaolvi" runat="server" AutoCompleteType="Disabled" Width="155px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="11px"
                                            Text="Email:" ForeColor="White"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfrom" runat="server" Width="155px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="UReqEmail" runat="server" ControlToValidate="txtfrom"
                                            Display="none" ErrorMessage="Formato de email invalido: Este debe ser user@mail.xyz"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender ID="UReqEmail_ValidatorCalloutExtender" runat="server"
                                            Enabled="True" TargetControlID="UReqEmail">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblcoutry" runat="server" Text="Pais" ForeColor="White"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbpaisolv" runat="server" Width="150px" Font-Names="Arial"
                                                Font-Size="11pt" Font-Bold="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tr>
                            </table>
                            <br />
                            <div align="center">
                                <asp:Button ID="btnenvio" runat="server" BorderWidth="1px" CssClass="buttonPlan"
                                    Height="21px" OnClick="btnenvio_Click" Style="margin-left: 0px" Text="Enviar Solicitud" />
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" Enabled="True" TargetControlID="BtnOlvido"
                PopupControlID="PRecordatorio" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Button ID="btndipararalerta" runat="server" Text="" Height="0px" CssClass="alertas"
                Width="0px" Enabled="False" />
            <cc1:ModalPopupExtender ID="ModalPopupCanal" runat="server" Enabled="True" TargetControlID="btndipararalerta"
                PopupControlID="PMensajes" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PMensajes" runat="server" Width="355px" Height="197px" Style="display: none;">
                <table id="tmensaje" runat="server" align="center" width="95%">
                    <tr>
                        <td align="center" class="AnchoMensaje">
                            <br />
                            <br />
                            <br />
                            <asp:Label ID="lblencabezado" runat="server" Font-Bold="True" Font-Names="Verdana"
                                Font-Size="10pt" ForeColor="White"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="lblmensajegeneral" runat="server" Font-Names="Verdana" Font-Size="9pt"
                                ForeColor="White"></asp:Label>
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btnaceptar" runat="server" CssClass="buttonPlan" Text="Aceptar" Font-Bold="True"
                                Font-Names="Verdana" Font-Overline="False" Style="cursor: hand;" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="White" Width="100px" BackColor="#D5DBE4" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="1px" Height="25px" />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="BtnAlerta" runat="server" Text="" Height="0px" CssClass="alertas"
                Width="0px" Enabled="False" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" Enabled="True" TargetControlID="BtnAlerta"
                PopupControlID="Pmensaje2" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Pmensaje2" runat="server" Width="355px" Height="197px" Style="display: none;">
                <table id="Table1" runat="server" align="center" width="95%">
                    <tr>
                        <td align="center" class="AnchoMensaje">
                            <br />
                            <br />
                            <br />
                            <br />
                            <asp:Label ID="LblEncabezado2" runat="server" Font-Bold="True" Font-Names="Verdana"
                                Font-Size="10pt" ForeColor="White"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="LblMesanje2" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="White"></asp:Label>
                            <br />
                            <br />
                            <asp:Button ID="btnacepmensaje2" runat="server" CssClass="buttonPlan" Text="Aceptar"
                                Font-Bold="True" Font-Names="Verdana" Font-Overline="False" Style="cursor: hand;"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="White" Width="100px"
                                BackColor="#D5DBE4" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                Height="25px" OnClick="btnacepmensaje2_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="margin-left: 30px; margin-top: 3px; margin-right: 30px;">
                <asp:Panel ID="panelBienvenida" runat="server">
                    
                    <table width="100%">
                        <tr>
                            <td style="width: 200px;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtuser" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="8pt" Text="USUARIO"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtuser" runat="server" Width="120px" oncopy="return false" onpaste="return false"  oncut="return false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtuser"
                                                ErrorMessage="El nombre de Usuario es requerido" ToolTip="El nombre de Usuario es requerido."
                                                ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" AssociatedControlID="txtpassw" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="8pt" Text="CLAVE"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpassw" runat="server" TextMode="Password" Width="120px" oncopy="return false" onpaste="return false"  oncut="return false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtpassw"
                                                ErrorMessage="La Contraseña es requerida." ToolTip="La Contraseña es requerida."
                                                ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Button ID="btningreso" runat="server" CommandName="Login" Text="Ingresar" CssClass="art-button"
                                                OnClick="btningreso_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Label ID="LblOlvido" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="6pt"
                                                Font-Underline="true" ForeColor="#999999" Text="OLVIDO SU CLAVE"></asp:Label>
                                            <asp:ImageButton ID="BtnOlvido" runat="server" AlternateText="Aquí le indicamos que hacer..."
                                                ImageUrl="~/Pages/ImgBooom/boton_olvido_contrasen¦âa.png" />
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                            <td>
                                <p class="art-postheader" align="right">
                                    Bienvenido        
                                </p>
                                    
                                <asp:Label ID="Acceso" runat="server" Text="Le invitamos a utilizar Xplora, nuestro sistema de consulta en l&#237nea, donde podr&#225
                            tener acceso a los datos correspondientes a las ventas,  precios, disponibilidad
                            y otra serie de datos que le podr&#225 ayudar a tomar nuevas y mejores decisiones sobre
                            sus productos."></asp:Label>
                            </td>
                        </tr>
                    </table>
                    
                </asp:Panel>
              
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table align="center" width="95%">
        <tr>
            <td colspan="2">
                <br />
                <script src="Pages/js/carousel_2008.js" type="text/javascript"></script>
                <script src="Pages/js/carousel_2008_conf.js" type="text/javascript"></script>
                <div id="carousel1" style="width: 100%; margin: auto; border: 1px solid #ccc; overflow: auto;
                    height: 180px; position: relative;">
                    <img alt="imagen 1" src="Pages/images/evento1lucky.jpg" />
                    <img alt="imagen 2" src="Pages/images/evento3lucky.jpg" />
                    <img alt="imagen 3" src="Pages/images/evento4lucky.jpg" />
                    <img alt="imagen 4" src="Pages/images/evento5lucky.jpg" />
                    <img alt="imagen 5" src="Pages/images/evento6lucky.jpg" />
                    <img alt="imagen 6" src="Pages/images/evento2lucky.jpg" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="contenedormail" runat="server" ContentPlaceHolderID="ContentPlaceHolder1" >
    <asp:Panel ID="PanelContactenos" runat="server" Style="display: none; " >
                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="90%">
                        <tr>
                            <td bgcolor="#f2f3f5" valign="top">
                                <asp:Panel ID="PSolicitudes" runat="server" Width="593px" Height="397px">
                                    <div>
                                        <asp:ImageButton ID="BtnclosePanel1" runat="server" BackColor="Transparent" Height="22px"
                                            ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                                    </div>
                                    <br />
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <asp:Image ID="ImgMail" runat="server" Height="62px" ImageUrl="~/Pages/images/mailreminder.png"
                                                    Width="65px" />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="LblTitEnvioMail" runat="server" CssClass="labelsTit" Text="Novedades / Solicitudes"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table align="center">
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:Label ID="LblMailSolicitante" runat="server" Text="Email"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtSolicitante" runat="server" Width="400px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Reqmail" runat="server" ControlToValidate="TxtSolicitante"
                                                    Display="None" ErrorMessage="Formato de email invalido: Este debe ser user@mail.xyz"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                        ID="VCReqMail" runat="server" Enabled="True" TargetControlID="Reqmail">
                                                    </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="LblMotivo" runat="server" Text="Asunto"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtMotivo" runat="server" MaxLength="50" Width="400px"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="Reqmotivo" runat="server" ControlToValidate="TxtMotivo" Display="None" ErrorMessage="Sr. Usuario, por favor no ingrese caracteres especiales y recuerde que no debe inicial con número"
                                                    ValidationExpression="([a-zA-Z][a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]{0,49})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                        ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="Reqmotivo">
                                                    </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="LblMensaje" runat="server" Text="Mensaje"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtMensaje" runat="server" Height="113px" TextMode="MultiLine" Width="400px"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="ReqMensaje" runat="server" ControlToValidate="TxtMensaje" Display="None"
                                                    ErrorMessage="Sr. Usuario, recuerde que no debe inicial con número. Máximo ingrese 255 caracteres"
                                                    ValidationExpression="([a-zA-Z][\W\wa-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]{0,255})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                        ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="ReqMensaje">
                                                    </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <asp:Button ID="ImgEnviarMail" runat="server" AlternateText="Enviar" BorderStyle="None"
                                                    CssClass="mailSend" Width="71px" Height="26px" OnClick="ImgEnviarMail_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" Enabled="True" TargetControlID="linkContactenos"
                    PopupControlID="PanelContactenos" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
            
</asp:Content>

<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="ScriptIncludePlaceHolder">
    <script type="text/javascript" src="<%= ResolveUrl("~/script.js") %>"></script>
</asp:Content>
