<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cambio_pswd.aspx.cs" Inherits="SIGE.Pages.Cambio_pswd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI.Compatibility" Namespace="eWorld.UI.Compatibility"
    TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Pages/css/backstilo.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function mpeSeleccionOnOk() {
            var ddlCiudades = document.getElementById("ddlCiudades");
            var ddlMeses = document.getElementById("ddlMeses");
            var ddlAnualidades = document.getElementById("ddlAnualidades");
            var txtSituacion = document.getElementById("txtSituacion");
            txtSituacion.value = ddlCiudades.options[ddlCiudades.selectedIndex].value + ", " +
                ddlMeses.options[ddlMeses.selectedIndex].value + " de " +
                ddlAnualidades.options[ddlAnualidades.selectedIndex].value;
        }
        function mpeSeleccionOnCancel() {
            var txtSituacion = document.getElementById("txtSituacion");
            txtSituacion.value = "";
            txtSituacion.style.backgroundColor = "#FFFF99";
        }   
    </script>

    <style type="text/css">
        .style1
        {
            width: 320px;
        }
    </style>
</head>
<body >
    <form id="form1" runat="server"  style="background-image: url('../ImgBooom/cuadro_gris.png'); background-repeat: no-repeat">
    <div align="center">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:UpdatePanel ID="upcontine" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PSolicitudes" runat="server" Width="481px"  Height="363px" 
                    BackColor="#A8A8FF">
                    <br />
                    <table align="center" style="width: 403px">
                        <tr>
                            <td align="center">
                                <asp:Image ID="ImgCambioPass" runat="server" ImageUrl="~/Pages/images/password.gif"
                                    Height="53px" Width="50px" />
                            </td>
                            <td align="center" class="style1">
                                <asp:Label ID="LblTitCambioPass" runat="server" Text="Cambio de contraseña" CssClass="labelsTit"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table frame="border" align="center" style="margin-right: 0px" class="ScrollFotos">
                        <tr>
                            <td>
                                <asp:Label ID="LblNameUsu" runat="server" Text="Usuario" CssClass="labels"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtSolicitante" runat="server" Width="132px"></asp:TextBox>
                            </td>
                             <td align="center" >
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                ControlToValidate="TxtSolicitante" ErrorMessage="El nombre de Usuario es requerido" 
                                                ToolTip="El nombre de Usuario es requerido." ValidationGroup="Login">*</asp:RequiredFieldValidator>
                                        </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblPassActual" runat="server" Text="Nueva Clave" CssClass="labels"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtPassNuevo" runat="server" Width="133px" TextMode="Password"
                                    MaxLength="12"></asp:TextBox>
                                <cc1:PasswordStrength ID="PasswordStrength1" runat="server" Enabled="True" TargetControlID="TxtPassNuevo"
                                    MinimumNumericCharacters="6" MinimumSymbolCharacters="1" PreferredPasswordLength="13"
                                    PrefixText="Calidad del Password: " RequiresUpperAndLowerCaseCharacters="true"
                                    StrengthIndicatorType="Text" 
                                    TextStrengthDescriptions="muy débil; débil; mejorable; buena; perfecta">
                                </cc1:PasswordStrength>
                                 
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtPassNuevo"
                                    ErrorMessage="La nueva clave es obligatoria" ToolTip="La nueva clave es obligatoria."
                                    ValidationGroup="Login">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="PReqex1" runat="server" 
                                    ControlToValidate="TxtPassNuevo" Display="None" 
                                    ErrorMessage="Señor Usuario:&lt;br&gt;El Password debe contener mínimo 8 caracteres y por lo menos una letra ,&lt;br&gt; No puede contener caracteres especiales&lt;br/&gt;" 
                                    ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,12})$"></asp:RegularExpressionValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                                    Enabled="True" TargetControlID="PReqex1">
                                </cc1:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblPassNew" runat="server" Text="Confimar clave" CssClass="labels"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtPassNew" runat="server" Width="133px" MaxLength="12" TextMode="Password"></asp:TextBox>
                                <cc1:PasswordStrength ID="TxtPassNew_PasswordStrength" runat="server" Enabled="True"
                                    TargetControlID="TxtPassNew" MinimumNumericCharacters="6" MinimumSymbolCharacters="1"
                                    PreferredPasswordLength="13" PrefixText="Calidad del Password: " RequiresUpperAndLowerCaseCharacters="true"
                                    StrengthIndicatorType="Text" TextStrengthDescriptions="muy débil; débil; mejorable; buena; perfecta">
                                </cc1:PasswordStrength> 
                                </td>
                                <td>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ControlToValidate="TxtPassNew" ErrorMessage="La nueva clave es obligatoria" 
                                                ToolTip="La nueva clave es obligatoria." ValidationGroup="Login">*</asp:RequiredFieldValidator>
                                    
                                    <asp:RegularExpressionValidator ID="PReqex" runat="server" 
                                        ControlToValidate="TxtPassNew" Display="None" 
                                        ErrorMessage="Señor Usuario:&lt;br&gt;El Password debe contener mínimo 8 caracteres y por lo menos una letra ,&lt;br&gt; No puede contener caracteres especiales&lt;br/&gt;" 
                                        ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,12})$"></asp:RegularExpressionValidator>
                                    <cc1:ValidatorCalloutExtender ID="RegularExpressionValidator1_ValidatorCalloutExtender" 
                                        runat="server" Enabled="True" TargetControlID="PReqex">
                                    </cc1:ValidatorCalloutExtender>
                                    
                           </td>
                        </tr>
                    </table>
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="btnEnviarNewPass" runat="server" CssClass="buttonPlan" 
                                    Text="Cambiar Clave" OnClick="btnEnviarNewPass_Click" ValidationGroup="Login" />
                                <asp:Button ID="btndipararalerta" runat="server" Text="" Height="0px" CssClass="alertas"
                                    Width="0px" Enabled="False" />
                                <br />
                            </td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" DynamicServicePath=""
                    Enabled="True" TargetControlID="btndipararalerta" PopupControlID="PMensajeClave"
                    BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="PMensajeClave" runat="server" Width="343px" CssClass="busqueda" Height="202px"
                    Style="display: none;">
                    <br />
                    <div align="center">
                        <asp:Label ID="LblBcanal" runat="server" Text="Cambio Clave" CssClass="labelsTit" />
                    </div>
                    <br />
                    <br />
                  
                    <table width="80%">
                        <tr>
                            <td>
                                <asp:Label ID="lblpasw" runat="server" CssClass="labels" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                   
                    <asp:Button ID="btningresonew" runat="server" CssClass="buttonPlan" Height="26px"
                        OnClick="btningresonew_Click" Text="Finalizar" Width="135px" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
