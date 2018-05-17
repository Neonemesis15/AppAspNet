<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestiónServicio.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Administrativo.GestiónServicio" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión Servicio</title>
</head>
<body style="background: transparent;">
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div>
        <cc1:TabContainer ID="TabAdministrativoServicio" runat="server" ActiveTabIndex="0"
            Width="900px" Height="452px" Font-Names="Verdana">
            <cc1:TabPanel runat="server" HeaderText="Gestión Servicio " ID="Panel_Servicio">
                <HeaderTemplate>
                    Gestión Servicio
                </HeaderTemplate>
                <ContentTemplate>
                    <br />
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="LblTitServ" runat="server" CssClass="labelsTit" Text="Administración de Servicios"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <fieldset id="FSServicios" runat="server" style="border-color: White; border-width: 1px;
                                    width: 505px;">
                                    <legend style="color: White; font-weight: bold">Servicios</legend>
                                    <br />
                                    <table align="center" style="width: 524px">
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblCodServ" runat="server" CssClass="labels" Text="Código de Servicio "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtCodServ" runat="server" BackColor="#DDDDDD" Enabled="true" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblNomServ" runat="server" CssClass="labels" Text="Nombre de Servicio * "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtNomServ" runat="server" MaxLength="50" Width="332px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="ReqNomserv" runat="server" ControlToValidate="TxtNomServ"
                                                    Display="none" ErrorMessage="El Nombre del Servicio no debe empezar por espacio en blanco, &lt;br /&gt; no debe ser numérico ni poseer caracteres especiales"
                                                    ValidationExpression="([a-z-A-Z][\w\sñÑáéíóúÁÉÍÓÚ./]{0,49})">
                                                </asp:RegularExpressionValidator>
                                                <cc1:ValidatorCalloutExtender ID="VENomReq" runat="server" Enabled="True" TargetControlID="ReqNomserv">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="LblDesServ" runat="server" CssClass="labels" Text="Descripción * "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtDescServ" runat="server" Height="58px" MaxLength="255" TextMode="MultiLine"
                                                    Width="332px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="ReqDescServ" runat="server" ControlToValidate="TxtDescServ"
                                                    Display="none" ErrorMessage="No debe comenzar por número ni espacio en blanco y &lt;br /&gt; No ingrese caracteres especiales y no exceda 255 caracteres"
                                                    ValidationExpression="([a-zA-Z][\w\sñÑáéíóúÁÉÍÓÚ0-9.,;/]{0,254})">
                                                </asp:RegularExpressionValidator>
                                                <cc1:ValidatorCalloutExtender ID="VEDescServ" runat="server" Enabled="True" TargetControlID="ReqDescServ">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblPais0" runat="server" CssClass="labels" Text="País *"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbcontryServ" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <caption>
                                            <br />
                                        </caption>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="TitEstadoServ" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RBtnListStatusServ" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="10pt" ForeColor="White">
                                    <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                    <asp:ListItem>Deshabilitado</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="btnCrearServ" runat="server" CssClass="button" Text="Crear" Width="95px" />
                                <asp:Button ID="btnsaveServ" runat="server" CssClass="button" Text="Guardar" Visible="False"
                                    Width="95px" />
                                <asp:Button ID="btnConsultarServ" runat="server" CssClass="button" Text="Consultar"
                                    Width="95px" />
                                <asp:Button ID="btnEditServicios" runat="server" CssClass="button" Text="Editar"
                                    Visible="False" Width="95px" />
                                <asp:Button ID="btnActualizarServ" runat="server" CssClass="button" Text="Actualizar"
                                    Visible="False" Width="95px" />
                                <asp:Button ID="btnCancelServ" runat="server" CssClass="button" Text="Cancelar" Width="95px" />
                                <asp:Button ID="btnPreg3" runat="server" CssClass="button" Text="|&lt;&lt;" Visible="False" />
                                <asp:Button ID="btnAreg3" runat="server" CssClass="button" Text="&lt;&lt;" Visible="False" />
                                <asp:Button ID="btnSreg3" runat="server" CssClass="button" Text="&gt;&gt;" Visible="False" />
                                <asp:Button ID="btnUreg3" runat="server" CssClass="button" Text="&gt;&gt;|" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Gestión Item Servicio Por Cliente" ID="Panel_Item">
                <HeaderTemplate>
                    Gestión Item Servicio por Cliente
                </HeaderTemplate>
                <ContentTemplate>
                    <br />
                    <br />
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="LblTitItems" runat="server" CssClass="labelsTit" Text="Administración de Formatos de levantamiento por cliente"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <fieldset id="Fieldset11" runat="server" style="border-color: White; border-width: 1px;
                                    width: 505px;">
                                    <legend style="color: White; font-weight: bold">Formato de levantamiento</legend>
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblCodItemS" runat="server" CssClass="labels" Text="Código: * "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtcodItemS" runat="server" BackColor="#DDDDDD" Enabled="true" MaxLength="50"
                                                    Width="80px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="ReqCodItemS" runat="server" ControlToValidate="TxtcodItemS"
                                                    Display="none" ErrorMessage="No ingrese caracteres especiales &lt;br /&gt; No exceda el limite de 50 caracteres"
                                                    ValidationExpression="([a-zA-Z][a-zA-Z0-9\s]{1,50})">
                                                </asp:RegularExpressionValidator>
                                                <cc1:ValidatorCalloutExtender ID="VCReqcodItems" runat="server" Enabled="True" TargetControlID="ReqCodItemS">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblNameItem" runat="server" CssClass="labels" Text="Nombre: * "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtNameItem" runat="server" MaxLength="50" TextMode="MultiLine"
                                                    Width="230px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="ReqNameItem" runat="server" ControlToValidate="TxtNameItem"
                                                    Display="none" ErrorMessage="El formato no debe empezar por espacio en blanco, &lt;br /&gt; no debe ser numérico ni poseer caracteres especiales. No exceda el límite de 50 caracteres"
                                                    ValidationExpression="([a-z-A-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,49})"></asp:RegularExpressionValidator>
                                                <cc1:ValidatorCalloutExtender ID="VCNameItem" runat="server" Enabled="True" TargetControlID="ReqNameItem">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblSelPaisServ" runat="server" CssClass="labels" Text="País * "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="CmbSelPaisServ" runat="server" AutoPostBack="True">
                                                    <%-- onselectedindexchanged="CmbSelPaisServ_SelectedIndexChanged" Width="235px">--%>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblSelServ" runat="server" CssClass="labels" Text="Servicio * "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbSelServ" runat="server" Width="235px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblSelCliente" runat="server" CssClass="labels" Text="Cliente*"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="CmbSelCliente" runat="server" Width="235px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblSelCanalFormat" runat="server" CssClass="labels" Text="Canal*"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="CmbSelCanalFormat" runat="server" Width="235px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblSelInformeFormat" runat="server" CssClass="labels" Text="Informe * "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="CmbSelInformeFormat" runat="server" Width="235px">
                                                </asp:DropDownList>
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
                                <asp:Label ID="LblEstadoitem" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RbtnListStatusitem" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="10pt" ForeColor="White" RepeatDirection="Horizontal">
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
                                <asp:Button ID="BtnCrearitem" runat="server" CssClass="button" Text="Crear" Width="95px" />
                                <asp:Button ID="Btnsaveitem" runat="server" CssClass="button" Text="Guardar" Visible="False"
                                    Width="95px" />
                                <asp:Button ID="BtnConsultarItem" runat="server" CssClass="button" Text="Consultar"
                                    Width="95px" />
                                <asp:Button ID="btnEditItemSer" runat="server" CssClass="button" Text="Editar" Visible="False"
                                    Width="95px" />
                                <asp:Button ID="BtnActualizarItem" runat="server" CssClass="button" Text="Actualizar"
                                    Visible="False" Width="95px" />
                                <asp:Button ID="BtnCancelarItem" runat="server" CssClass="button" Text="Cancelar"
                                    Width="95px" />
                                <asp:Button ID="btnPreg4" runat="server" CssClass="button" Text="|&lt;&lt;" Visible="False" />
                                <asp:Button ID="btnAreg4" runat="server" CssClass="button" Text="&lt;&lt;" Visible="False" />
                                <asp:Button ID="btnSreg4" runat="server" CssClass="button" Text="&gt;&gt;" Visible="False" />
                                <asp:Button ID="btnUreg4" runat="server" CssClass="button" Text="&gt;&gt;|" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Gestión Estructura Formato" ID="Panel_Formato">
                <HeaderTemplate>
                    Gestión Formato
                </HeaderTemplate>
                <ContentTemplate>
                    <br />
                    <br />
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="LblEstrucForm" runat="server" CssClass="labelsTit" Text="Administración de Formatos"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="LblCodItem" runat="server" CssClass="labels" Text="Código de Estructura "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtCodEstr" runat="server" BackColor="#DDDDDD" Enabled="false" ReadOnly="true"
                                    Width="70"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblPaisEstruc" runat="server" CssClass="labels" Text="País * "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="CmbPaisEstruc" runat="server" AutoPostBack="True">
                                    <%--Width="200px" onselectedindexchanged="CmbPaisEstruc_SelectedIndexChanged">--%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblEstrategia" runat="server" CssClass="labels" Text="Servicio * "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbSelestrategia" runat="server" AutoPostBack="True">
                                    <%--CausesValidation="True" 
                                                            onselectedindexchanged="cmbSelestrategia_SelectedIndexChanged" Width="315px">--%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblSelClieForm" runat="server" CssClass="labels" Text="Cliente * "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="CmbSelClieForm" runat="server" AutoPostBack="True">
                                    <%--onselectedindexchanged="CmbSelClieForm_SelectedIndexChanged" Width="315px">--%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblChannelForm" runat="server" CssClass="labels" Text="Canal * "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="CmbChannelForm" runat="server" AutoPostBack="True">
                                    <%-- Width="315px" 
                                                            onselectedindexchanged="CmbChannelForm_SelectedIndexChanged">--%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblEstrategiaItem" runat="server" CssClass="labels" Text="Formato * "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbSelestritem" runat="server" CausesValidation="True">
                                    <%-- Width="315px">--%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblDesEstruc" runat="server" CssClass="labels" Text="Nombre de estructura *"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtDescEstruc" runat="server" MaxLength="30" Width="310px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="ReqDescEstruc" runat="server" ControlToValidate="TxtDescEstruc"
                                    Display="none" ErrorMessage="No debe iniciar con número y no ingrese caracteres especiales"
                                    ValidationExpression="([a-zA-Z][a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]{0,30})">
                                </asp:RegularExpressionValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server" Enabled="True"
                                    TargetControlID="ReqDescEstruc">
                                </cc1:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblUbicFormato" runat="server" CssClass="labels" Text="Ubicación dentro del formato *"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RbtnListUbic" runat="server" CellPadding="5" CellSpacing="5"
                                    Font-Bold="true" Font-Names="Arial" Font-Size="10pt" ForeColor="White" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Encabezado" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Detalle" Value="2" Enabled="false"></asp:ListItem>
                                    <asp:ListItem Text="Pie de Pagina" Value="3"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="LblEstadoitemform" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RBtnListStatusitemform" runat="server" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="10pt" ForeColor="White">
                                    <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                    <asp:ListItem>Deshabilitado</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="BtnCrearEstruc" runat="server" CssClass="button" Text="Crear" Width="95px" />
                                <asp:Button ID="btnSaveEstruc" runat="server" CssClass="button" Text="Guardar" Visible="False"
                                    Width="95px" />
                                <asp:Button ID="BtnConsulEstruc" runat="server" CssClass="button" Text="Consultar"
                                    Width="95px" />
                                <asp:Button ID="BtnEditEstruc" runat="server" CssClass="button" Text="Editar" Visible="False"
                                    Width="95px" />
                                <asp:Button ID="BtnActuEstruc" runat="server" CssClass="button" Text="Actualizar"
                                    Visible="False" Width="95px" />
                                <asp:Button ID="BtnCancelEstruc" runat="server" CssClass="button" Text="Cancelar"
                                    Width="95px" />
                                <asp:Button ID="btnPreg11" runat="server" CssClass="button" Text="|&lt;&lt;" Visible="False" />
                                <asp:Button ID="btnAreg11" runat="server" CssClass="button" Text="&lt;&lt;" Visible="False" />
                                <asp:Button ID="btnSreg11" runat="server" CssClass="button" Text="&gt;&gt;" Visible="False" />
                                <asp:Button ID="btnUreg11" runat="server" CssClass="button" Text="&gt;&gt;|" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
    </form>
</body>
</html>
