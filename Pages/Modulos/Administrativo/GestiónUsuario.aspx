<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestiónUsuario.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Administrativo.GestiónUsuario" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión Usuario</title>
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
    <style type="text/css">
        
    </style>
</head>
<body style="background: transparent;">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="ToolkitScriptManager1" runat="server" EnableTheming="True"
        ScriptMode="Release">
    </telerik:RadScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--  <table align="center">
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
                    </table>--%>
            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                BackgroundCssClass="modalProgressGreyBackground">
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
            <div>
                <cc1:TabContainer ID="TabAdministradorUsuario" runat="server" ActiveTabIndex="3"
                    Width="100%" Height="460px" Font-Names="Verdana" style="Overflow:auto">
                    <%-- <cc1:TabPanel runat="server" HeaderText="Gestión Asignación de Ejecutivo" ID="Panel_Asignación">
                        <HeaderTemplate>
                            Asignación</HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td align="center" valign="middle">
                                        <asp:Label ID="LblTitAsignaDcuenta" runat="server" CssClass="labelsTit2" Text="Administración de Asignación de Personal Ejecutivo a Director de Cuenta"
                                            Width="450px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <br />
                                        <fieldset id="fieldset3" runat="server">
                                            <legend style="">Asignación</legend>
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSelPaisDirCu" runat="server" CssClass="labels" Text="País *"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="CmbSelPaisDirCu" runat="server" Width="150px" AutoPostBack="True"
                                                            Enabled="False" 
                                                            onselectedindexchanged="CmbSelPaisDirCu_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSelDirCu" runat="server" CssClass="labels" Text="Director de Cuenta *"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="CmbSelDirCu" runat="server" Width="300px" Enabled="False" 
                                                            >
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblPersonalSinAsign" runat="server" CssClass="labels" Text="Personal Ejecutivo sin Asignación"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblPersonalAsign" runat="server" CssClass="labels" Text="Personal Ejecutivo Asignado *"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:ListBox ID="LstBoxPersonalSinAsing" runat="server" Width="300px" Height="140px"
                                                            SelectionMode="Multiple" Enabled="False"></asp:ListBox>
                                                    </td>
                                                    <td width="100px" align="center" valign="middle">
                                                        <asp:Button ID="BtnMasAsing" runat="server" Text="Más >>" Width="85px" 
                                                            onclick="BtnMasAsing_Click" /><br />
                                                        <br />
                                                        <asp:Button ID="BtnMenosAsing" runat="server" Text="<< Menos" Width="85px" 
                                                            onclick="BtnMenosAsing_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ListBox ID="LstBoxPersonalAsing" runat="server" Width="300px" Height="140px"
                                                            SelectionMode="Multiple" Enabled="False"></asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnCrearAsignEje" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" onclick="BtnCrearAsignEje_Click" />
                                        <asp:Button ID="BtnSaveAsignEje" runat="server" CssClass="buttonPlan"
                                                Text="Guardar" Visible="False" Width="95px" 
                                            onclick="BtnSaveAsignEje_Click" /><asp:Button ID="BtnSearchAsignEje"
                                                    runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px" /><asp:Button
                                                        ID="BtnEditAsignEje" runat="server" CssClass="buttonPlan" 
                                            Text="Editar" Visible="False"
                                                        Width="95px" onclick="BtnEditAsignEje_Click" /><asp:Button ID="BtnUpdateAsignEje" runat="server" CssClass="buttonPlan"
                                                            Text="Actualizar" Visible="False" Width="95px" /><asp:Button ID="BtnCancelAsignEje"
                                                                runat="server" CssClass="buttonPlan" Text="Cancelar" Width="95px" />
                                    </td>
                                </tr>
                            </table>
                             <asp:Panel ID="BuscarAsignacion" runat="server" CssClass="busqueda" 
                            DefaultButton="BtnSearchAsign" Height="208px"  Width="375px">
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" CssClass="labelsTit2"
                                            Text="Buscar Asignación" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblBPaisDirect" runat="server" CssClass="labels" Text="País:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbBSelPaisDirect" runat="server" Width="150px" 
                                            AutoPostBack="True" >
                                        </asp:DropDownList>                                                            
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblBSelNameDirect" runat="server" CssClass="labels" Text="Director de Cuenta" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbBSelNameDirect" runat="server" Width="230px" 
                                            OnSelectedIndexChanged="cmbBSelNameDirect_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnSearchAsign" runat="server" CssClass="buttonPlan"
                                        Text="Buscar" Width="80px" onclick="BtnSearchAsign_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnCancelSearchAsign" runat="server" CssClass="buttonPlan" Text="Cancelar" 
                                        Width="80px" />
                                    </td>
                                </tr>
                            </table>
                            
                            
                            
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                            BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" 
                            OkControlID="BtnCancelSearchAsign" PopupControlID="BuscarAsignacion" 
                            TargetControlID="BtnSearchAsignEje" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>--%>
                    <cc1:TabPanel runat="server" HeaderText="Gestión Roles" ID="Panel_Roles">
                        <HeaderTemplate>
                            Roles</HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTitRoles" runat="server" CssClass="labelsTit2" Text="Administración de Roles"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <fieldset id="ContenRol" runat="server">
                                            <legend style="">Rol</legend>
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblCodRol" runat="server" CssClass="labels" Text="Código *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtCodRol" runat="server" Enabled="False" MaxLength="4" Width="50px"></asp:TextBox><asp:RegularExpressionValidator
                                                            ID="ReqIdRol" runat="server" ControlToValidate="TxtCodRol" Display="None" ErrorMessage="Requiere que el código contenga mínimo 4 números"
                                                            ValidationExpression="([0-9]{4,4})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="ReqIdRol">
                                                            </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblNomRol" runat="server" CssClass="labels" Text="Nombre * "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtNomRol" runat="server" MaxLength="40" Width="332px" Enabled="False"></asp:TextBox>
                                                    </td>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:Label ID="LblDescRol" runat="server" CssClass="labels" Text="Descripción * "></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtDescRol" runat="server" Height="68px" MaxLength="255" TextMode="MultiLine"
                                                                Width="334px" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                            <br />
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="TitEstadoRol" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RBtnListStatusRol" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" Enabled="False">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCrearRol" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px"
                                            OnClick="btnCrearRol_Click" /><asp:Button ID="btnsaveus" runat="server" CssClass="buttonPlan"
                                                Text="Guardar" Visible="False" Width="95px" OnClick="btnsaveus_Click" /><asp:Button
                                                    ID="btnConsultarRol" runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px" /><asp:Button
                                                        ID="btnEditRol" runat="server" CssClass="buttonPlan" Text="Editar" Visible="False"
                                                        Width="95px" OnClick="btnEditRol_Click" /><asp:Button ID="btnActuRol" runat="server"
                                                            CssClass="buttonPlan" Text="Actualizar" Visible="False" Width="95px" OnClick="btnActuRol_Click" /><asp:Button
                                                                ID="btnCancelRol" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="95px"
                                                                OnClick="btnCancelRol_Click" />
                                        <asp:Button ID="btnPreg0" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;" Visible="False"
                                            OnClick="btnPreg0_Click" /><asp:Button ID="btnAreg0" runat="server" CssClass="buttonPlan"
                                                Text="&lt;&lt;" Visible="False" OnClick="btnAreg0_Click" /><asp:Button ID="btnSreg0"
                                                    runat="server" CssClass="buttonPlan" Text="&gt;&gt;" Visible="False" OnClick="btnSreg0_Click" /><asp:Button
                                                        ID="btnUreg0" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|" Visible="False"
                                                        OnClick="btnUreg0_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarRoles" runat="server" CssClass="busqueda" DefaultButton="BtnBRol"
                                Style="display: none" Height="202px" Width="343px">
                                <br />
                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160;
                                &#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                <asp:Label ID="LblbuscarRol" runat="server" CssClass="labelsTit2" Text="Buscar Rol" /><br />
                                <br />
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBNomRol" runat="server" CssClass="labels" Text="Nombre Rol:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtBNomRol" runat="server" MaxLength="40" Width="200px"></asp:TextBox><asp:RegularExpressionValidator
                                                ID="ReqBNomRol" runat="server" ControlToValidate="TxtBNomRol" Display="None"
                                                ErrorMessage="El nombre del Rol no debe empezar por espacio en blanco, &lt;br /&gt; no debe ser numérico ni poseer caracteres especiales"
                                                ValidationExpression="([a-z-A-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,40})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                    ID="VCEBNomRol" runat="server" Enabled="True" TargetControlID="ReqBNomRol">
                                                </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160;
                                &#160;&#160;&#160;&#160;
                                <asp:Button ID="BtnBRol" runat="server" CssClass="buttonPlan" Text="Buscar" Width="80px"
                                    OnClick="BtnBRol_Click" /><asp:Button ID="btnCancelarRol" runat="server" CssClass="buttonPlan"
                                        Text="Cancelar" Width="80px" /></asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnRol_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="btnCancelarRol" PopupControlID="BuscarRoles"
                                TargetControlID="btnConsultarRol" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Gestión Nivel de Usuario" ID="Panel_Nivel">
                        <HeaderTemplate>
                            Nivel Usuario</HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblNivel" runat="server" CssClass="labelsTit2" Text="Gestión de Niveles de Usuario"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <fieldset id="Fieldset14" runat="server">
                                            <legend style="">Niveles de Usuario</legend>
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblcodnive" runat="server" CssClass="labels" Text="Código * "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtcodnive" runat="server" Enabled="False" MaxLength="4" Width="50px"></asp:TextBox><asp:RegularExpressionValidator
                                                            ID="ReqCodNivel" runat="server" ControlToValidate="TxtCodnive" Display="None"
                                                            ErrorMessage="Requiere que el código contenga mínimo 4 números" ValidationExpression="([0-9]{4,4})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="ReqCodNivel">
                                                            </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Lblnomnive" runat="server" CssClass="labels" Text="Nombre * "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtnomNivel" runat="server" MaxLength="40" Width="332px" Enabled="False"></asp:TextBox><asp:RegularExpressionValidator
                                                            ID="ReqNomNivel" runat="server" ControlToValidate="TxtnomNivel" Display="None"
                                                            ErrorMessage="El Nombre del Nivel no debe empezar por espacio en blanco, &lt;br /&gt; no debe ser numérico ni poseer caracteres especiales"
                                                            ValidationExpression="([a-z-A-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,40})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                ID="ValidatorCalloutExtender25" runat="server" Enabled="True" TargetControlID="ReqNomNivel">
                                                            </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table align="center" width="50%" border="1">
                                                <tr valign="top">
                                                    <td>
                                                        <asp:Label ID="Modulos" runat="server" Text="Modulos"></asp:Label>
                                                        <div class="ScrollInforme">
                                                            <asp:CheckBoxList ID="CheckModulo" runat="server" Enabled="False">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEstNivel" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RblistEstnivel" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" Enabled="False">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCrearNive" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px"
                                            OnClick="btnCrearNive_Click" /><asp:Button ID="btnSaveNive" runat="server" CssClass="buttonPlan"
                                                Text="Guardar" Visible="False" Width="95px" OnClick="btnSaveNive_Click" /><asp:Button
                                                    ID="btnSearchNive" runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px" /><asp:Button
                                                        ID="btneditNive" runat="server" CssClass="buttonPlan" Text="Editar" Visible="False"
                                                        Width="95px" OnClick="btneditNive_Click" /><asp:Button ID="btnupdNive" runat="server"
                                                            CssClass="buttonPlan" Text="Actualizar" Visible="False" Width="95px" OnClick="btnupdNive_Click" /><asp:Button
                                                                ID="btnCancelNive" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="95px"
                                                                OnClick="btnCancelNive_Click" />
                                        <asp:Button ID="btnPregNive" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" OnClick="btnPregNive_Click" /><asp:Button ID="btnAregNive" runat="server"
                                                CssClass="buttonPlan" Text="&lt;&lt;" Visible="False" OnClick="btnAregNive_Click" /><asp:Button
                                                    ID="btnSregNive" runat="server" CssClass="buttonPlan" Text="&gt;&gt;" Visible="False"
                                                    OnClick="btnSregNive_Click" /><asp:Button ID="btnUregNive" runat="server" CssClass="buttonPlan"
                                                        Text="&gt;&gt;|" Visible="False" OnClick="btnUregNive_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarNivel" runat="server" CssClass="busqueda" Style="display: none"
                                DefaultButton="BtnBNivel" Height="202px" Width="343px">
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblTitBNivel" runat="server" CssClass="labelsTit2" Text="Buscar Nivel de Usuario" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBCodNivel" runat="server" CssClass="labels" Text="Código:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtBCodNivel" runat="server" MaxLength="4" Width="50px"></asp:TextBox><asp:RegularExpressionValidator
                                                ID="ReqBCodNivel" runat="server" ControlToValidate="TxtBCodNivel" Display="None"
                                                ErrorMessage="Requiere que el código contenga mínimo 4 números" ValidationExpression="([0-9]{4,4})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                    ID="VCEReqBCodNivel" runat="server" Enabled="True" TargetControlID="ReqBCodNivel">
                                                </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBNomNivel" runat="server" CssClass="labels" Text="Nombre:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbBuscarNivel" runat="server" Width="180px" Enabled="true">
                                            </asp:DropDownList>
                                            <%--  <asp:TextBox ID="TxtBNomNivel" runat="server" MaxLength="40" Width="200px"></asp:TextBox><asp:RegularExpressionValidator
                                                ID="ReqBNomNivel" runat="server" ControlToValidate="TxtBNomNivel" Display="None"
                                                ErrorMessage="El nombre del Nivel de usuario no debe empezar por espacio en blanco, &lt;br /&gt; no debe ser numérico ni poseer caracteres especiales"
                                                ValidationExpression="([a-z-A-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,40})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                    ID="VCEReqBNomNivel" runat="server" Enabled="True" TargetControlID="ReqBNomNivel">
                                                </cc1:ValidatorCalloutExtender>--%>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnBNivel" runat="server" CssClass="buttonPlan" OnClick="BtnBNivel_Click"
                                                Text="Buscar" Width="80px" /><asp:Button ID="BtnCancelBNivel" runat="server" CssClass="buttonPlan"
                                                    Text="Cancelar" Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnNivel_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="BtnCancelBNivel" PopupControlID="BuscarNivel"
                                TargetControlID="btnSearchNive" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Gestión Perfil" ID="Panel_Perfil">
                        <HeaderTemplate>
                            Perfil
                        </HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblTitAdmin" runat="server" CssClass="labelsTit2" Text="Administración de Perfiles"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <fieldset id="contenPerfil" runat="server">
                                            <legend style="">Perfil</legend>
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblCodPerfil" runat="server" CssClass="labels" Text="Código * "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtCodPerfil" runat="server" Enabled="False" MaxLength="4" Width="50px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblNomPerfil" runat="server" CssClass="labels" Text="Nombre * "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtNomPerfil" runat="server" MaxLength="40" Width="332px" Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Label ID="LblDescPerfil" runat="server" CssClass="labels" Text="Descripción  * "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtDescPerfil" runat="server" Height="68px" MaxLength="255" TextMode="MultiLine"
                                                            Width="334px" Enabled="False"></asp:TextBox><asp:RegularExpressionValidator ID="ReqdesPerfil"
                                                                runat="server" ControlToValidate="TxtDescPerfil" Display="None" ErrorMessage="No debe comenzar por número ni espacio en blanco y &lt;br /&gt; No ingrese caracteres especiales y no exceda 255 caracteres"
                                                                ValidationExpression="([a-zA-Z][\w\sñÑáéíóúÁÉÍÓÚ0-9.,;]{0,254})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                    ID="VEdesPerfil" runat="server" Enabled="True" TargetControlID="ReqdesPerfil">
                                                                </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSelRol" runat="server" CssClass="labels" Text="Rol * "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DdlSelRol" runat="server" Width="200px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" CssClass="labels" Text="Canal *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlchannel" runat="server" Width="200px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSeleMod" runat="server" CssClass="labels" Text="Módulo * "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DdlSelMod" runat="server" Width="200px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblNivelUser" runat="server" CssClass="labels" Text="Nivel de Usuario* "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbniveluser" runat="server" Width="200px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblregister2" runat="server"></asp:Label><br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="TitEstadoPerfil" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RBtnListStatusPerfil" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" Enabled="False">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCrearPer" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px"
                                            OnClick="btnCrearPer_Click" /><asp:Button ID="BtnGuardarPer" runat="server" CssClass="buttonPlan"
                                                Text="Guardar" Visible="False" Width="95px" OnClick="BtnGuardarPer_Click" /><asp:Button
                                                    ID="btnConsultarPer" runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px" /><asp:Button
                                                        ID="btnEditPerfil" runat="server" CssClass="buttonPlan" Text="Editar" Visible="False"
                                                        Width="95px" OnClick="btnEditPerfil_Click" /><asp:Button ID="btnActualizarPer" runat="server"
                                                            CssClass="buttonPlan" Text="Actualizar" Visible="False" Width="95px" OnClick="btnActualizarPer_Click" /><asp:Button
                                                                ID="btnCancelPer" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="95px"
                                                                OnClick="btnCancelPer_Click" />
                                        <asp:Button ID="btnPreg1" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;" Visible="False"
                                            OnClick="btnPreg1_Click" /><asp:Button ID="btnAreg1" runat="server" CssClass="buttonPlan"
                                                Text="&lt;&lt;" Visible="False" OnClick="btnAreg1_Click" /><asp:Button ID="btnSreg1"
                                                    runat="server" CssClass="buttonPlan" Text="&gt;&gt;" Visible="False" OnClick="btnSreg1_Click" /><asp:Button
                                                        ID="btnUreg1" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|" Visible="False"
                                                        OnClick="btnUreg1_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarPerfil" runat="server" CssClass="busqueda" DefaultButton="BtnBPerfil"
                                Style="display: none" Height="202px" Width="343px">
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblbuscarPerfil" runat="server" CssClass="labelsTit2" Text="Buscar Perfil" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBNomPerfil" runat="server" CssClass="labels" Text="Perfil:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtBNomPerfil" runat="server" MaxLength="40" Width="200px"></asp:TextBox><asp:RegularExpressionValidator
                                                ID="ReqBNomPerfil" runat="server" ControlToValidate="TxtBNomPerfil" Display="None"
                                                ErrorMessage="El nombre del Perfil no debe ser numérico ni poseer caracteres especiales"
                                                ValidationExpression="([a-zA-Z][{a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,40})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                    ID="VCEBNomPerfil" runat="server" Enabled="True" TargetControlID="ReqBNomPerfil">
                                                </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBNomRolAs" runat="server" CssClass="labels" Text="Rol:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbBRolAs" runat="server" Width="205px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblcanal" runat="server" CssClass="labels" Text="Canal:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlchannelb" runat="server" Width="205px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBNomNivelUsus" runat="server" CssClass="labels" Text="Nivel:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbSniveluser" runat="server" Width="205px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160;
                                &#160;&#160;&#160;&#160;
                                <asp:Button ID="BtnBPerfil" runat="server" CssClass="buttonPlan" Text="Buscar" Width="80px"
                                    OnClick="BtnBPerfil_Click" /><asp:Button ID="BtnCancelarPerfil" runat="server" CssClass="buttonPlan"
                                        Text="Cancelar" Width="80px" /></asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnPerfil_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="BtnCancelarPerfil" PopupControlID="BuscarPerfil"
                                TargetControlID="btnConsultarPer" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Gestión usuario" ID="Panel_Usuario">
                        <HeaderTemplate>
                            Usuario
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LabTitUsu" runat="server" CssClass="labelsTit2" Text="Administración de Usuarios"></asp:Label>
                                    </td>
                                </tr>
                                <caption>
                                </caption>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <fieldset id="Fieldset1" runat="server">
                                            <legend style="">Información Personal</legend>
                                            <table align="center">
                                                <tr>
                                                    <td class="style27">
                                                        <asp:Label ID="LblCodUsu" runat="server" Text="Código de Usuario * " CssClass="labels"></asp:Label>
                                                    </td>
                                                    <td class="style27">
                                                        <asp:TextBox ID="txtCodUsu" runat="server" BackColor="Silver" ReadOnly="True" Width="180px"
                                                            Style="margin-left: 1px" Enabled="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style27">
                                                        <asp:Label ID="LblTiDoc" runat="server" Text="Tipo de documento * " CssClass="labels"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbdoc" runat="server" Width="185px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style27">
                                                        <asp:Label ID="LblNumDoc" runat="server" CssClass="labels" Text="Número Documento * "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtNumDoc" runat="server" MaxLength="11" Width="150px" Enabled="False"></asp:TextBox><asp:RegularExpressionValidator
                                                            ID="ReqNumDoc" runat="server" ControlToValidate="TxtNumDoc" Display="None" ErrorMessage="Se requiere que sea numérico y puede contener un [-]"
                                                            ValidationExpression="(^\d{1,11}[-]{0,1}\d{0,1})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                ID="VCReqNumDoc" runat="server" Enabled="True" TargetControlID="ReqNumDoc">
                                                            </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblPNom" runat="server" CssClass="labels" Text="Primer Nombre * "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtPNom" runat="server" MaxLength="50" Width="180px" Enabled="False"></asp:TextBox><asp:RegularExpressionValidator
                                                            ID="ReqPNom" runat="server" ControlToValidate="TxtPNom" Display="None" ErrorMessage="No debe empezar por espacio en blanco, &lt;br /&gt; no debe ser numérico ni poseer caracteres especiales"
                                                            ValidationExpression="([a-z-A-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,49})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                ID="VCReqPNom" runat="server" Enabled="True" TargetControlID="ReqPNom">
                                                            </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblSNom" runat="server" CssClass="labels" Text="Segundo Nombre "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtSNom" runat="server" MaxLength="50" Width="150px" Enabled="False"></asp:TextBox><asp:RegularExpressionValidator
                                                            ID="ReqSNom" runat="server" ControlToValidate="TxtSNom" Display="None" ErrorMessage="No debe empezar por espacio en blanco, &lt;br /&gt; no debe ser numérico ni poseer caracteres especiales"
                                                            ValidationExpression="([a-z-A-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,49})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                ID="VCReqSnom" runat="server" Enabled="True" TargetControlID="ReqSNom">
                                                            </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblPApe" runat="server" CssClass="labels" Text="Primer Apellido * "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtPApe" runat="server" MaxLength="50" Width="180px" Enabled="False"></asp:TextBox><asp:RegularExpressionValidator
                                                            ID="ReqPApe" runat="server" ControlToValidate="TxtPApe" Display="None" ErrorMessage="No debe empezar por espacio en blanco, &lt;br /&gt; no debe ser numérico ni poseer caracteres especiales"
                                                            ValidationExpression="([a-z-A-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,49})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                ID="VCReqPApe" runat="server" Enabled="True" TargetControlID="ReqPApe">
                                                            </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblSApe" runat="server" CssClass="labels" Text="Segundo Apellido "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtSApe" runat="server" MaxLength="50" Width="150px" Enabled="False"></asp:TextBox><asp:RegularExpressionValidator
                                                            ID="ReqSApe" runat="server" ControlToValidate="TxtSApe" Display="None" ErrorMessage="No debe empezar por espacio en blanco, &lt;br /&gt; no debe ser numérico ni poseer caracteres especiales"
                                                            ValidationExpression="([a-z-A-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,49})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                ID="VCSAPe" runat="server" Enabled="True" TargetControlID="ReqSApe">
                                                            </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblPalabra" runat="server" CssClass="labels" Text="Frase Recordatoria  "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtPalabra" runat="server" MaxLength="50" Width="180px" Enabled="False"></asp:TextBox><asp:RegularExpressionValidator
                                                            ID="ReqPalabra" runat="server" ControlToValidate="TxtPalabra" Display="None"
                                                            ErrorMessage="Ingrese mínimo 5 caracteres , no ingrese caracteres especiales y &lt;br /&gt; no debe iniciar por número"
                                                            ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s]{4,50})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                ID="VSPalabra" runat="server" Enabled="True" TargetControlID="ReqPalabra">
                                                            </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblSelMod0" runat="server" CssClass="labels" Text="Módulo *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbmodul" runat="server" AutoPostBack="True" Width="155px"
                                                            Enabled="False" OnSelectedIndexChanged="cmbmodul_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSelPer0" runat="server" CssClass="labels" Text="Perfil * "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbperfil" runat="server" Width="185px" Enabled="False" AutoPostBack="True"
                                                            OnSelectedIndexChanged="cmbperfil_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblSelClie" runat="server" CssClass="labels" Text="Cliente  *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbcompany" runat="server" Width="155px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <br />
                                        <fieldset id="Fieldset2" runat="server">
                                            <legend>Información de la Cuenta</legend>
                                            <table align="center">
                                                <tr>
                                                    <td class="style27">
                                                        <asp:Label ID="LblUsu" runat="server" Text="Nombre Usuario * " CssClass="labels"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtUsu" runat="server" MaxLength="12" ReadOnly="True" Width="180px"
                                                            Enabled="False"></asp:TextBox>
                                                    </td>
                                                    <td class="style27">
                                                        <asp:Label ID="LblPsw" runat="server" Text="Clave * " CssClass="labels"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtPsw" runat="server" MaxLength="12" ReadOnly="True" Width="150px"
                                                            Enabled="False"></asp:TextBox>
                                                        <asp:Button ID="btndeco" runat="server" CssClass="buttonPlan" 
                                                            onclick="btndeco_Click" Text="Dc" ToolTip="DesEncriptar Clave" 
                                                            Enabled="False" />
                                                    </td>
                                                     <td>
                                                     <asp:Panel ID="Pmensaclave" runat="server" Visible="False" >
                                                     
                                                    
                                                     <asp:Label ID="lblclave" runat="server"></asp:Label>
                                                      </asp:Panel>
                                                     
                                                     
                                                     </td>
                                                       
                                                </tr>
                                                     
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblEmail" runat="server" Text="Correo Electrónico*" CssClass="labels"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtMail" runat="server" Width="180px" Enabled="False"></asp:TextBox><asp:RegularExpressionValidator
                                                            ID="Reqmail" runat="server" ControlToValidate="TxtMail" Display="None" ErrorMessage="Formato de email invalido: Este debe ser user@mail.xyz"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                ID="VCReqMail" runat="server" Enabled="True" TargetControlID="Reqmail">
                                                            </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblTel" runat="server" Text="Teléfono" CssClass="labels"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtTel" runat="server" MaxLength="12" Width="150px" Enabled="False"></asp:TextBox><asp:RegularExpressionValidator
                                                            ID="ReqTel" runat="server" ControlToValidate="TxtTel" Display="None" ErrorMessage="Ingrese solo carateres numéricos &lt;br /&gt; Debe tener mínimo 6 dígitos"
                                                            ValidationExpression="([0-9]{6,12})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                ID="VCReqTel" runat="server" Enabled="True" TargetControlID="ReqTel">
                                                            </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblDir" runat="server" Text="Dirección" CssClass="labels"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtDir" runat="server" MaxLength="150" Width="180px" Enabled="False"></asp:TextBox><asp:RegularExpressionValidator
                                                            ID="ReqDirUsu" runat="server" ControlToValidate="TxtDir" Display="None" ErrorMessage="No ingrese caracteres especiales, no inicie con espacio en blanco ni número"
                                                            ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s#.-]{0,149})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                ID="VCEReqDirUsu" runat="server" Enabled="True" TargetControlID="ReqDirUsu">
                                                            </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblPais" runat="server" Text="País *" CssClass="labels"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbcontry" runat="server" Width="155px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <br />
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="TitEstadoUsu" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="RBtnListStatusUsu" runat="server" RepeatDirection="Horizontal"
                                                        CssClass="labels" Enabled="False">
                                                        <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                                        <asp:ListItem>Deshabilitado</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnCrearUsu" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px"
                                                        OnClick="btnCrearUsu_Click" /><asp:Button ID="BtnGuardarUsu" runat="server" CssClass="buttonPlan"
                                                            Text="Guardar" Visible="False" Width="95px" OnClick="BtnGuardarUsu_Click" /><asp:Button
                                                                ID="btnConsultarUsu" runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px" /><asp:Button
                                                                    ID="btnEditAct" runat="server" CssClass="buttonPlan" Text="Editar" Visible="False"
                                                                    Width="95px" OnClick="btnEditAct_Click" /><asp:Button ID="btnActua" runat="server"
                                                                        CssClass="buttonPlan" Text="Actualizar" Visible="False" Width="95px" OnClick="btnActua_Click" /><asp:Button
                                                                            ID="btnCancelarUsu" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="95px"
                                                                            OnClick="btnCancelarUsu_Click" />
                                                    <asp:Button ID="btnPreg2" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;" Visible="False"
                                                        OnClick="btnPreg2_Click" /><asp:Button ID="btnAreg2" runat="server" CssClass="buttonPlan"
                                                            Text="&lt;&lt;" Visible="False" OnClick="btnAreg2_Click" /><asp:Button ID="btnSreg2"
                                                                runat="server" CssClass="buttonPlan" Text="&gt;&gt;" Visible="False" OnClick="btnSreg2_Click" /><asp:Button
                                                                    ID="btnUreg2" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|" Visible="False"
                                                                    OnClick="btnUreg2_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="loginUsuar" runat="server" CssClass="busqueda" DefaultButton="btnBuscar"
                                Style="display: none" Height="202px" Width="343px">
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBuscarUsuario" runat="server" CssClass="labelsTit2" Text="Buscar Usuario" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblDoc" runat="server" CssClass="labels" Text="Documento:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtdoc" runat="server" MaxLength="11" Width="180px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Lbluser" runat="server" CssClass="labels" Text="Usuario" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txtuser" runat="server" Width="240px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:Button ID="btnBuscar" runat="server" CssClass="buttonPlan" OnClick="btnBuscar_Click"
                                    Text="Buscar" Width="80px" />
                                <asp:Button ID="btnCancelar" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" />
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="Ibtnlogin_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="btnCancelar" PopupControlID="loginUsuar"
                                TargetControlID="btnConsultarUsu" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="Panel_Cliente_Usuario" runat="server" HeaderText="Asignar Clientes a Usuario.">
                        <HeaderTemplate>
                            Asignar Clientes a Usuario
                        </HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <div class="centrarcontenido">
                                <span class="labelsTit2">Asignar Clientes a Usuario</span>
                            </div>
                            <br />
                            <div class="centrar">
                                <div class="tabla centrar">
                                    <div class="fila">
                                        <div class="celda">
                                            <span>Usuario*:</span></div>
                                        <div class="celda">
                                            <telerik:RadComboBox ID="ddl_cxu_usuario" runat="server" Enabled="False" Width="225px"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddl_cxu_usuario_SelectedIndexChanged">                                                
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda">
                                            <span>Cliente*:</span>
                                        </div>
                                        <div class="celda">
                                            <asp:CheckBoxList ID="cbxl_cxu_cliente" runat="server" Enabled="False">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda">
                                        </div>
                                        <div class="celda">
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda">
                                        </div>
                                        <div class="celda">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="centrarcontenido">
                                <asp:Button ID="btn_cxu_crear" runat="server" CssClass="buttonPlan" Text="Crear"
                                    Width="80px" OnClick="btn_cxu_crear_Click" />
                                <asp:Button ID="btn_cxu_guardar" runat="server" CssClass="buttonPlan" Text="Guardar"
                                    Width="80px" Visible="False" OnClick="btn_cxu_guardar_Click" />
                                <asp:Button ID="btn_cxu_consultar" runat="server" CssClass="buttonPlan" Text="Consultar"
                                    Width="80px" />
                                
                                <asp:Button ID="btn_cxucancelar" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" onclick="btn_cxucancelar_Click"/>
                            </div>

                            <telerik:RadCodeBlock ID="rcdTenderDocuments" runat="server">
                                <script type="text/javascript">
                                    var popUp;
                                    function PopUpShowing(sender, eventArgs) {
                                        popUp = eventArgs.get_popUp();
                                        var gridWidth = sender.get_element().offsetWidth;
                                        var gridHeight = sender.get_element().offsetHeight;
                                        var popUpWidth = popUp.style.width.substr(0, popUp.style.width.indexOf("px"));
                                        var popUpHeight = popUp.style.height.substr(0, popUp.style.height.indexOf("px"));
                                        popUp.style.left = ((gridWidth - popUpWidth) / 2 + sender.get_element().offsetLeft).toString() + "px";
                                        popUp.style.top = (-250 + sender.get_element().offsetTop).toString() + "px";
                                        //popUp.style.top = ((gridHeight - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
                                    }
                                </script>
                            </telerik:RadCodeBlock>



                            <asp:Panel ID="consulta_rgv_clientxuser" runat="server" Style="display: block">
                                <div class="centrarcontenido">
                                    <div class="p" style="width:780px; height: 315px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
	                                                font-family : arial, Helvetica, sans-serif;"> 
                                        <telerik:RadGrid ID="rgv_cxu_users" runat="server" AutoGenerateColumns="False" 
                                            GridLines="None" oncancelcommand="rgv_cxu_users_CancelCommand" 
                                            oneditcommand="rgv_cxu_users_EditCommand" 
                                            onupdatecommand="rgv_cxu_users_UpdateCommand">
                                            <MasterTableView EditMode="PopUp">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="company_id" Display="False" 
                                                        HeaderText="CodCliente" UniqueName="company_id" ReadOnly="True">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Cliente" UniqueName="Company_Name" 
                                                        DataField="Company_Name" ReadOnly="True">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Person_id" Display="False" 
                                                        HeaderText="CodUsuario" UniqueName="Person_id" ReadOnly="True">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Person_Name" HeaderText="Usuario" 
                                                        ReadOnly="True" UniqueName="Person_Name">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridCheckBoxColumn DataField="Status" HeaderText="Estado" 
                                                        UniqueName="Status">
                                                    </telerik:GridCheckBoxColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" 
                                                        EditText="Editar" UpdateText="Actualizar">
                                                    </telerik:GridEditCommandColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <EditColumn UniqueName="EditCommandColumn1" UpdateText="Actualizar" CancelText="Cancelar">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <ClientEvents OnPopUpShowing="PopUpShowing" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                        <br />
                                        <div class="centrarcontenido">
                                            <asp:Button ID="btn_rgv_cancelar" runat="server" Text="Cancelar" CssClass="buttonPlan" Width="80px" OnClick="btn_cxu_cancelar_Click"/>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="mp_consulta_rgv_clientxuser" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True"  PopupControlID="consulta_rgv_clientxuser"
                                TargetControlID="btnpopuprgv" DynamicServicePath="">
                            </cc1:ModalPopupExtender>

                            <asp:Button ID="btnpopuprgv" runat="server" CssClass="alertas" Width="0px" />

                            <asp:Panel ID="ClientxUsuario_Buscar" runat="server" CssClass="busqueda" DefaultButton="btn_cxu_buscar"
                                Style="display: none" Height="170px" Width="350px">
                                <br />
                                <div class="centrarcontenido">
                                    <span class="labelsTit2">
                                        buscar usuarios por cliente.
                                    </span>
                                </div>
                                <br />
                                <div class="centrar">
                                    <div class="tabla centrar">
                                        <div class="fila">
                                            <div class="celda">
                                                <span>Cliente*:</span>
                                            </div>
                                            <div class="celda">
                                                <asp:DropDownList ID="ddl_cxub_cliente" Width="220px" runat="server">
                                                </asp:DropDownList>                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="centrarcontenido">
                                    <asp:Button ID="btn_cxu_buscar" runat="server" Text="Buscar" CssClass="buttonPlan" Width="80px" OnClick="btn_cxu_buscar_Click" />
                                    <asp:Button ID="btn_cxu_bcancelar" runat="server" Text="Cancelar" CssClass="buttonPlan" Width="80px" OnClick="btn_cxu_cancelar_Click" />
                                </div>
                            </asp:Panel>

                            <cc1:ModalPopupExtender ID="mp_ClientexUsuario" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="btn_cxu_bcancelar" PopupControlID="ClientxUsuario_Buscar"
                                TargetControlID="btn_cxu_consultar" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>


                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Tipo de Perfiles">
                        <HeaderTemplate>
                            Tipo de Perfil
                        </HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <div class="centrarcontenido">
                                <span class="labelsTit2">Gestionar tipo de perfil</span>
                            </div>
                            <br />
                            <asp:Panel ID="panel_principal" runat="server" >
                            <div class="centrar">
                                <table align="center">
                                <tr>
                                    <td>
                                        
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" CssClass="labels" Text="Código *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTipoReporte_Codigo" runat="server" Enabled="False" MaxLength="4" Width="50px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" CssClass="labels" Text="Descripción* "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTipoReporte_Descripcion" runat="server" MaxLength="40" Width="332px" Enabled="False"></asp:TextBox>
                                                    </td>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:Label ID="Label5" runat="server" CssClass="labels" Text="Cliente * "></asp:Label>
                                                        </td>
                                                        <td>
                                                           <asp:DropDownList ID="ddlTipoReporte_Cliente" runat="server" Width="155px" 
                                                                Enabled="False" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlTipoReporte_Cliente_SelectedIndexChanged">
                                                        </asp:DropDownList></td>
                                                    </tr>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="margin-left: 40px">
                                                         <asp:Label ID="Label23" runat="server" CssClass="labels" Text="Tipo Levantamiento* "></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTipoReporte_TipoLevantamiento" runat="server" MaxLength="40" Width="332px" Enabled="False"></asp:TextBox></td>
                                                </tr>
                                                  <tr>
                                                    <td valign="top" style="margin-left: 40px">
                                                         <asp:Label ID="Label10" runat="server" CssClass="labels" Text="Reporte* "></asp:Label></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTipoReporte_Reporte" runat="server" Width="155px" Enabled="False">
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="margin-left: 40px">
                                                         <asp:Label ID="Label9" runat="server" CssClass="labels" Text="Canal* "></asp:Label></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTipoReporte_Canal" runat="server" Width="155px" Enabled="False">
                                                        </asp:DropDownList></td>
                                                </tr>
                                            </table>
                                            <br />
                                        
                                    </td>
                                </tr>
                            </table>
                            </div>
                            <br />
                            <div class="centrarcontenido">
                               <asp:Button ID="btnTipoReporte_Crear" runat="server" CssClass="buttonPlan" 
                                            Text="Crear" Width="95px" onclick="btnTipoReporte_Crear_Click"
                                            /><asp:Button ID="btnTipoReporte_Guardar" runat="server" CssClass="buttonPlan"
                                                Text="Guardar" Visible="False" Width="95px" 
                                            onclick="btnTipoReporte_Guardar_Click"  /><asp:Button
                                                    ID="btnTipoReporte_Consultar" runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px" />
                                                    <asp:Button
                                                                ID="btnTipoReporte_Cancelar" runat="server" 
                                            CssClass="buttonPlan" Text="Cancelar" Width="95px" onclick="btnTipoReporte_Cancelar_Click" />
                            </div>
                            </asp:Panel>
                            <asp:Button ID="Button6" runat="server" CssClass="alertas" Width="0px" />

                            <asp:Panel ID="ptipoperfil_Buscar" runat="server" CssClass="busqueda" DefaultButton="ptipoperfil_Consultar"
                                Style="display: none" Height="170px" Width="350px">
                                <br />
                                <div class="centrarcontenido">
                                    <span class="labelsTit2">
                                        buscar Tipos de Perfil.
                                    </span>
                                </div>
                                <br />
                                <div class="centrar">
                                    <div class="tabla centrar">
                                        <div class="fila">
                                            <div class="celda">
                                                <span>Descripción:</span>
                                            </div>
                                            <div class="celda">
                                                <asp:TextBox ID="txtptipoperfil_descripcion" Width="220px" runat="server"></asp:TextBox>                                                
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="celda">
                                                <span>Cliente:</span>
                                            </div>
                                            <div class="celda">
                                                <asp:DropDownList ID="ddlptipoperfil_Cliente" Width="220px" runat="server">
                                                </asp:DropDownList>                                                
                                            </div>
                                        </div>

                                         <div class="fila">
                                            <div class="celda">
                                                <span>Reporte:</span>
                                            </div>
                                            <div class="celda">
                                                <asp:DropDownList ID="ddlptipoperfil_Reporte" Width="220px" runat="server">
                                                </asp:DropDownList>                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="centrarcontenido">
                                    <asp:Button ID="ptipoperfil_Consultar" runat="server" Text="Buscar" 
                                        CssClass="buttonPlan" Width="80px" onclick="ptipoperfil_Consultar_Click"  />
                                    <asp:Button ID="ptipoperfil_Cancelar" runat="server" Text="Cancelar" CssClass="buttonPlan" Width="80px"  />
                                </div>
                            </asp:Panel>

                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="ptipoperfil_Cancelar" PopupControlID="ptipoperfil_Buscar"
                                TargetControlID="btnTipoReporte_Consultar" DynamicServicePath="">
                            </cc1:ModalPopupExtender>

            <div id="div" runat="server" aling="center"  
                                style="width: 100%; height: auto;" class="centrarcontenido">
                            <telerik:RadGrid ID="gv_TipoPerfil" runat="server" 
                                                AutoGenerateColumns="False" PageSize="2000"
                    Skin="Vista" Font-Size="Small" AllowPaging="True" GridLines="None" OnCancelCommand="gv_TipoPerfil_CancelCommand"
                    OnEditCommand="gv_TipoPerfil_EditCommand" OnPageIndexChanged="gv_TipoPerfil_PageIndexChanged"
                    ShowFooter="True" OnPageSizeChanged="gv_TipoPerfil_PageSizeChanged" 
                                    onupdatecommand="gv_TipoPerfil_UpdateCommand">
                    <MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E" Font-Size="Smaller">
                        <Columns>

                            <telerik:GridBoundColumn DataField="id_Tipo_Reporte" HeaderText="id_Tipo_Reporte" UniqueName="id_Tipo_Reporte"
                                ReadOnly="True">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TipoReporte_Descripcion" HeaderText="TipoReporte_Descripcion" UniqueName="TipoReporte_Descripcion"
                                ReadOnly="True">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Company_Name" HeaderText="Company_Name" UniqueName="Company_Name"
                                ReadOnly="True">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Tipo_levantamiento" HeaderText="Tipo_levantamiento" UniqueName="Tipo_levantamiento"
                                >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Report_NameReport" HeaderText="Report_NameReport" UniqueName="Report_NameReport"
                                ReadOnly="True">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Channel_Name" HeaderText="Channel_Name" UniqueName="Channel_Name"
                                ReadOnly="True">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn Visible="False" HeaderText="Validado" UniqueName="TemplateColumn" 
                                ReadOnly="True">
                                <HeaderTemplate>
                                 Estado
                                </HeaderTemplate>
                                <ItemTemplate>
                                     <asp:Label ID="lbl_id_Tipo_Reporte" runat="server" Text='<%# Bind("id_Tipo_Reporte") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbl_TipoReporte_Descripcion" runat="server" Text='<%# Bind("TipoReporte_Descripcion") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbl_company_id" runat="server" Text='<%# Bind("company_id") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbl_report_id" runat="server" Text='<%# Bind("report_id") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lblcod_channel" runat="server" Text='<%# Bind("cod_channel") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png"
                                CancelText="Cancelar" UpdateText="Actualizar">
                            </telerik:GridEditCommandColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn UniqueName="EditCommandColumn1" ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png">
                            </EditColumn>
                        </EditFormSettings>
                        <AlternatingItemStyle BackColor="#F7F7F7" ForeColor="#333333" />
                    </MasterTableView>
                    <PagerStyle PageSizeLabelText="Tamaño de pagina:" />
                </telerik:RadGrid>
                                <div class="centrarcontenido">
                                    <asp:Button ID="btngv_TipoPerfilCancelar" Visible="False" runat="server" CssClass="buttonPlan" 
                                         Text="Cancelar" Width="95px" onclick="btngv_TipoPerfilCancelar_Click"  />
                                    </table>
                                </div>
                            
                            </div>


                        </ContentTemplate>
                    </cc1:TabPanel>

                </cc1:TabContainer>
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
                            <br />
                            <br />
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
    </asp:UpdatePanel>
   </form>
</body>
</html>