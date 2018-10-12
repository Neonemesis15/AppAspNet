<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestiónPaís.aspx.cs" Inherits="SIGE.Pages.Modulos.Administrativo.GestiónPaís" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión País</title>
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
<body style="background: transparent;">
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
         <%-- <table align="center">
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

                <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
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
                <cc1:TabContainer ID="TabAdministradorPais" runat="server" ActiveTabIndex="0" 
                    Width="100%" Height="405px" allowtransparency="true" Enabled="true">   
                    <cc1:TabPanel runat="server" HeaderText="País" ID="TabPanelPaís" allowtransparency="true">
                        <HeaderTemplate>
                            País
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table align="center">
                                <tr><td><br /><asp:Label ID="LblTitAdminPaises" runat="server" 
                                    Class="labelsTit2" Text="Gestión de Países"></asp:Label></td></tr>
                            </table>
                            <table align="center">
                                <tr>
                                    <td>
                                        <fieldset id="Fieldset4" runat="server">
                                            <legend style="">Información Básica</legend>
                                            <table width="500px">
                                                <tr>
                                                    <td><asp:Label ID="LblCodPais" runat="server" CssClass="labels" Text="Código de país*"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="TxtCodPais" runat="server" BackColor="#DDDDDD" MaxLength="3" ReadOnly="True" 
                                                            Width="80px" Enabled="False"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="ReqCodPais" runat="server" ControlToValidate="TxtCodPais"
                                                            Display="None" ErrorMessage="Requiere que el código contenga mínimo 3 números"
                                                            ValidationExpression="([0-9]{3,3})"></asp:RegularExpressionValidator>
                                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender32" runat="server" Enabled="True"
                                                            TargetControlID="ReqCodPais">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                <tr>
                                                    <td><asp:Label ID="LblNomPais" runat="server" CssClass="labels" Text="Nombre del País*"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="TxtNomPais" runat="server" MaxLength="50" Width="166px" Enabled="False"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="ReqNomPais" runat="server" ControlToValidate="TxtNomPais"
                                                            Display="None" ErrorMessage="No ingrese caracteres especiales ni números &lt;br /&gt; No inicie con espacio en blanco "
                                                            ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,50})"></asp:RegularExpressionValidator>
                                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender33" runat="server" Enabled="True"
                                                            TargetControlID="ReqNomPais"></cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="Lblidioma" runat="server" Text="Idioma*"></asp:Label></td>
                                                    <td><asp:DropDownList ID="cmbidioma" runat="server" Height="21px" Width="170px" 
                                                        Enabled="False"></asp:DropDownList></td>
                                                </tr>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td><br />
                                        <fieldset id="Fieldset5" runat="server">
                                            <legend style="">Jeraquía División Política</legend>
                                            <table align="center">
                                                <tr>
                                                    <td><asp:CheckBox ID="chkDepto" runat="server" ForeColor="Black" Text="Departamento" 
                                                        Enabled="False" /></td>
                                                    <td><asp:CheckBox ID="chkciudad" runat="server" ForeColor="Black" Text="Ciudad / Provincia" 
                                                        Enabled="False" /></td>
                                                </tr>
                                                <tr>
                                                    <td><asp:CheckBox ID="chkdistrito" runat="server" ForeColor="Black" 
                                                        Text="Distrito" Enabled="False" /></td>
                                                    <td><asp:CheckBox ID="chkbarrio" runat="server" ForeColor="Black" Text="Barrio" 
                                                        Enabled="False" /></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td><asp:Label ID="StatusPais" runat="server" CssClass="labels" Text="Estado"></asp:Label></td>
                                    <td><asp:RadioButtonList ID="RBtnListStatusPais" runat="server" Font-Bold="True" Font-Names="Arial" 
                                        Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table align="center">
                                <tr><td>
                                        <asp:Button ID="btnCrearPais" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px"
                                            OnClick="btnCrearPais_Click" />
                                        <asp:Button ID="btnsavePais" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="btnsavePais_Click" />
                                        <asp:Button ID="btnConsultarPais" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" />
                                        <asp:Button ID="btnEditPais" runat="server" CssClass="buttonPlan" Text="Editar" Visible="False"
                                            Width="95px" OnClick="btnEditPais_Click" />
                                        <asp:Button ID="btnActualizarPais" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="btnActualizarPais_Click" />
                                        <asp:Button ID="btnCancelPais" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="btnCancelPais_Click" />
                                        <asp:Button ID="btnPreg19" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" onclick="btnPreg19_Click" />
                                        <asp:Button ID="btnAreg19" runat="server" CssClass="buttonPlan" Text="&lt;&lt;" 
                                            Visible="False" onclick="btnAreg19_Click" />
                                        <asp:Button ID="btnSreg19" runat="server" CssClass="buttonPlan" Text="&gt;&gt;" 
                                            Visible="False" onclick="btnSreg19_Click" />
                                        <asp:Button ID="btnUreg19" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False" onclick="btnUreg19_Click" /></td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarDivPol" runat="server" CssClass="busqueda" DefaultButton="btnBuscarDivPol"
                                Style="display: none;" Height="202px" Width="343px"><br />
                                <table align="center">
                                    <tr><td><asp:Label ID="LblTitBDivPol" runat="server" CssClass="labelsTit2" Text="Buscar País" /></td></tr>
                                </table><br />
                                <table align="center">
                                    <tr>
                                        <td><asp:Label ID="LblbCodPais" runat="server" CssClass="labels" Text="Código País:" /></td>
                                        <td>
                                            <asp:TextBox ID="TxtBcodPais" runat="server" MaxLength="3" Width="80px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="Reqbcodpais" runat="server" ControlToValidate="TxtBcodPais"
                                                Display="None" ErrorMessage="el código debe contener 3 dígitos" 
                                                ValidationExpression="([{0-9]{3,3})"></asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="VCEReqBcodpais" runat="server" Enabled="True" TargetControlID="Reqbcodpais">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="LblBNomPais" runat="server" CssClass="labels" Text="Nombre País:" /></td>
                                        <td>
                                            <asp:TextBox ID="TxtBNomPais" runat="server" MaxLength="50" Width="190px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="ReqBnompais" runat="server" ControlToValidate="TxtBNomPais"
                                                Display="None" ErrorMessage="No ingrese caracteres especiales ni nùmeros &lt;br /&gt; No inicie con espacio en blanco "
                                                ValidationExpression="([a-zA-Z][{a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,50})"></asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="VCEReqBnompais" runat="server" Enabled="True" TargetControlID="ReqBnompais">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                </table><br />
                                <table align="center">
                                    <tr><td>
                                            <asp:Button ID="btnBuscarDivPol" runat="server" CssClass="buttonPlan" OnClick="btnBuscarDivPol_Click"
                                                Text="Buscar" Width="80px" />
                                            <asp:Button ID="btnCancelarDivPol" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                                Width="80px" /></td></tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnDivPol_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="btnCancelarDivPol" PopupControlID="BuscarDivPol"
                                TargetControlID="btnConsultarPais" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Departamento" ID="TabPanelDepartamento">
                        <HeaderTemplate>
                            Departamento
                        </HeaderTemplate>
                        <ContentTemplate>                           
                            <table align="center">
                                <tr>
                                    <td>
                                        <br />
                                        <asp:Label ID="LbltitDeptos" runat="server" Class="labelsTit2" Text="Gestión de Departamentos"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />                          
                            <table align="center">
                                <tr>
                                    <td>
                                        <br />
                                        <fieldset id="Fieldset6" runat="server">
                                            <legend style="">Información Básica</legend>
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSDivPais" runat="server" CssClass="labels" Text="País*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbSDivPais" runat="server" Width="195px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblDivCodDept" runat="server" CssClass="labels" Text="Código de Departamento*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtDivCodDept" runat="server" BackColor="#DDDDDD" ReadOnly="True"
                                                            Width="190px" Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblDivNomDept" runat="server" CssClass="labels" Text="Nombre del departamento*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtDivNomDept" runat="server" MaxLength="50" Width="190px" Enabled="False"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="ReqNomDept" runat="server" ControlToValidate="TxtDivNomDept"
                                                            Display="None" ErrorMessage="No ingrese caracteres especiales ni números &lt;br /&gt; No debe iniciar con espacio en blanco"
                                                            ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,50})"></asp:RegularExpressionValidator>
                                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender35" runat="server" Enabled="True"
                                                            TargetControlID="ReqNomDept">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </fieldset>
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
                                        <asp:Label ID="LblDivEstDept" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RBtnDivEstDept" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False">
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
                                        <asp:Button ID="BtnCrearDivDept" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" OnClick="BtnCrearDivDept_Click"></asp:Button>
                                        <asp:Button ID="BtnSaveDivDept" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnSaveDivDept_Click" />
                                        <asp:Button ID="BtnConsulDivDept" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px"></asp:Button>
                                        <asp:Button ID="BtnEditDivDept" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" OnClick="BtnEditDivDept_Click" />
                                        <asp:Button ID="BtnActualizarDivDept" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="BtnActualizarDivDept_Click" />
                                        <asp:Button ID="BtnCancelDivDept" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtnCancelDivDept_Click"></asp:Button>
                                        <asp:Button ID="pregdept" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;" 
                                            Visible="False" onclick="pregdept_Click">
                                        </asp:Button>
                                        <asp:Button ID="aregdept" runat="server" CssClass="buttonPlan" Text="&lt;&lt;" 
                                            Visible="False" onclick="aregdept_Click">
                                        </asp:Button>
                                        <asp:Button ID="sregdept" runat="server" CssClass="buttonPlan" Text="&gt;&gt;" 
                                            Visible="False" onclick="sregdept_Click">
                                        </asp:Button>
                                        <asp:Button ID="uregdept" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|" 
                                            Visible="False" onclick="uregdept_Click">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarDpto" runat="server" CssClass="busqueda" DefaultButton="btnBuscardepto"
                                Height="211px" Style="display: none;" Width="371px">
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblTitBDepto" runat="server" CssClass="labelsTit2" Text="Buscar Departamentos" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblSelPaisDept" runat="server" CssClass="labels" Text="País:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CmbSelPaisDept" runat="server" Width="130px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblbCodDepto" runat="server" CssClass="labels" Text="Código Dpto:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtbCodDepto" runat="server" MaxLength="3" Width="125px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="ReqBcodDepto" runat="server" ControlToValidate="TxtbCodDepto"
                                                Display="None" ErrorMessage="Requiere que el código contenga mínimo 3 números"
                                                ValidationExpression="([0-9]{3,3})"></asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="VCEReqBcodDepto" runat="server" Enabled="True"
                                                TargetControlID="ReqBcodDepto">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblbNomDepto" runat="server" CssClass="labels" Text="Nombre Dpto:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtbNomDepto" runat="server" MaxLength="50" Width="125px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="ReqBNomDepto" runat="server" ControlToValidate="TxtbNomDepto"
                                                Display="None" ErrorMessage="No ingrese caracteres especiales ni números &lt;br /&gt; No debe iniciar con espacio en blanco"
                                                ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,49})"></asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="VCEReqBNomDepto" runat="server" Enabled="True"
                                                TargetControlID="ReqBNomDepto">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnBuscardepto" runat="server" CssClass="buttonPlan" OnClick="btnBuscardepto_Click"
                                                Text="Buscar" Width="80px" />
                                            <asp:Button ID="BtnCancelBDepto" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                                Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnDeptos_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="btnCancelBDepto" PopupControlID="BuscarDpto"
                                TargetControlID="BtnConsulDivDept" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Ciudad" ID="TabPanelCiudad">
                        <HeaderTemplate>
                            Ciudad
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table align="center">
                                <tr>
                                    <td>
                                        <br />
                                        <asp:Label ID="Lbltitciudad" runat="server" CssClass="labelsTit2" Text="Gestión de Ciudades"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                           
                            <br />
                            <table align="center">
                                <tr>
                                    <td class="style48">
                                        <br />
                                        <fieldset id="Fieldset7" runat="server">
                                            <legend style="">Información Básica</legend>
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSSDivPais" runat="server" CssClass="labels" Text="País"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbSSDivPais" runat="server" AutoPostBack="True" CausesValidation="True"
                                                            Width="195px" Enabled="False" OnSelectedIndexChanged="CmbSSDivPais_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSSDivDept" runat="server" CssClass="labels" Text="Departamento"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbSSDivDept" runat="server" Width="195px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblDivCodCiudad" runat="server" CssClass="labels" Text="Código de Ciudad"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtDivcodCiudad" runat="server" BackColor="#DDDDDD" ReadOnly="True"
                                                            Width="190px" Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblDivNomCiudad" runat="server" CssClass="labels" Text="Nombre de Ciudad"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtDivNomCiudad" runat="server" MaxLength="50" Width="190px" Enabled="False"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="ReqDivNomciudad" runat="server" ControlToValidate="TxtDivNomCiudad"
                                                            Display="None" ErrorMessage="No ingrese caracteres especiales ni nùmeros &lt;br /&gt; No inicie con espacio en blanco "
                                                            ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,49})"></asp:RegularExpressionValidator>
                                                        <cc1:ValidatorCalloutExtender ID="VCEReqDivnomciudad" runat="server" Enabled="True"
                                                            TargetControlID="ReqDivNomciudad">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </td>
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
                                        <asp:Label ID="LblEstadoDivCiudad" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RbtnEstdivCiudad" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" 
                                            Enabled="False">
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
                                        <asp:Button ID="BtnCrearDivCiudad" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" OnClick="BtnCrearDivCiudad_Click"></asp:Button>
                                        <asp:Button ID="BtnSaveDivCiudad" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnSaveDivCiudad_Click" />
                                        <asp:Button ID="BtnConsultarDivCiudad" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" />
                                        <asp:Button ID="BtnEditDivCiudad" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" OnClick="BtnEditDivCiudad_Click" />
                                        <asp:Button ID="BtnActualizarDivCiudad" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="BtnActualizarDivCiudad_Click" Height="21px">
                                        </asp:Button>
                                        <asp:Button ID="BtnCancelDivCiudad" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtnCancelDivCiudad_Click" />
                                        <asp:Button ID="PregCiudad" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" onclick="PregCiudad_Click" />
                                        <asp:Button ID="AregCiudad" runat="server" CssClass="buttonPlan" Text="&lt;&lt;"
                                            Visible="False" onclick="AregCiudad_Click" />
                                        <asp:Button ID="SregCiudad" runat="server" CssClass="buttonPlan" Text="&gt;&gt;"
                                            Visible="False" onclick="SregCiudad_Click" />
                                        <asp:Button ID="UregCiudad" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False" onclick="UregCiudad_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarCiudad" runat="server" CssClass="busqueda" style="display:none;"
                                DefaultButton="btnBuscarCiudad" Height="248px" Width="343px">
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBuscaCiudad" runat="server" CssClass="labelsTit2" Text="Buscar Provincia/Ciudad" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblSelPaisCiudad" runat="server" CssClass="labels" Text="País:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CmbSelPaisCiudad" runat="server" AutoPostBack="True" CausesValidation="True"
                                                OnSelectedIndexChanged="CmbSelPaisCiudad_SelectedIndexChanged" 
                                                Width="130px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblSelDeptoCiudad" runat="server" CssClass="labels" Text="Departamento:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CmbSelDeptoCiudad" runat="server" Width="130px">
                                            </asp:DropDownList>
                                        </td>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblbCodCiud" runat="server" CssClass="labels" Text="Código Ciudad:" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtbCodciud" runat="server" MaxLength="3" Width="125px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="ReqbCodCiud" runat="server" ControlToValidate="TxtbCodciud"
                                                    Display="None" ErrorMessage="Requiere que el código contenga mínimo 3 números"
                                                    ValidationExpression="([0-9]{3,3})"></asp:RegularExpressionValidator>
                                                <cc1:ValidatorCalloutExtender ID="VCEReqbCodciud" runat="server" Enabled="True" TargetControlID="ReqbCodCiud">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblbNomCiud" runat="server" CssClass="labels" Text="Nombre Ciudad:" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtbNomCiud" runat="server" MaxLength="50" Width="125px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="ReqbNomCiud" runat="server" ControlToValidate="TxtbNomCiud"
                                                    Display="None" ErrorMessage="No ingrese caracteres especiales ni nùmeros &lt;br /&gt; No inicie con espacio en blanco "
                                                    ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,49})"></asp:RegularExpressionValidator>
                                                <cc1:ValidatorCalloutExtender ID="VCEReqbNomCiud" runat="server" Enabled="True" TargetControlID="ReqbNomCiud">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnBuscarCiudad" runat="server" CssClass="buttonPlan" OnClick="btnBuscarCiudad_Click"
                                                Text="Buscar" Width="80px" />
                                            <asp:Button ID="BtnCancelBciudad" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                                Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnCiudad_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="btnCancelBCiudad" PopupControlID="BuscarCiudad"
                                TargetControlID="BtnConsultarDivCiudad" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Distrito" ID="TabPanelDistrito">
                        <HeaderTemplate>
                            Distrito
                        </HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LbltitDistr" runat="server" CssClass="labelsTit2" Text="Gestión de Distritos"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td class="style48">
                                        <br />
                                        <fieldset id="Fieldset8" runat="server">
                                            <legend style="">Información Básica</legend>
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSDDivPais" runat="server" CssClass="labels" Text="País*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbSDDivPais" runat="server" AutoPostBack="True" Width="195px" 
                                                            Enabled="False" 
                                                            onselectedindexchanged="CmbSDDivPais_SelectedIndexChanged" > 
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSDDept" runat="server" CssClass="labels" Text="Departamento"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbSDDept" runat="server" AutoPostBack="True" Width="195px" 
                                                            Enabled="False" onselectedindexchanged="CmbSDDept_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSDCiudad" runat="server" CssClass="labels" Text="Ciudad"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbSDCiudad" runat="server" Width="195px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblCodDistrito" runat="server" CssClass="labels" Text="Código de distrito*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtCodDistrito" runat="server" BackColor="#DDDDDD" MaxLength="3"
                                                            ReadOnly="True" Width="190px" Enabled="False"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="ReqCodDistrito" runat="server" ControlToValidate="TxtCodDistrito"
                                                            Display="None" ErrorMessage="Requiere que el código contenga mínimo 3 números"
                                                            ValidationExpression="([0-9]{3,3})"></asp:RegularExpressionValidator>
                                                        <cc1:ValidatorCalloutExtender ID="VCEReqCoddistrito" runat="server" Enabled="True"
                                                            TargetControlID="ReqCodDistrito">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblNomDistrito" runat="server" CssClass="labels" Text="Nombre de distrito*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtNomDistrito" runat="server" MaxLength="50" Width="190px" Enabled="False"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="ReqNomDistrito" runat="server" ControlToValidate="TxtNomDistrito"
                                                            Display="None" ErrorMessage="No ingrese caracteres especiales ni números &lt;br /&gt; No inicie con espacio en blanco"
                                                            ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{1,49})"></asp:RegularExpressionValidator>
                                                        <cc1:ValidatorCalloutExtender ID="VCEReqNomDistrito" runat="server" Enabled="True"
                                                            TargetControlID="ReqNomDistrito">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </td>
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
                                        <asp:Label ID="LblEstadoDistrito" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RBtnEstadoDistrito" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False">
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
                                        <asp:Button ID="BtnCrearDistrito" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" OnClick="BtnCrearDistrito_Click"></asp:Button>
                                        <asp:Button ID="BtnSaveDistrito" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnSaveDistrito_Click" />
                                        <asp:Button ID="BtnconsulDistrito" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px"></asp:Button>
                                        <asp:Button ID="BtnEditDistrito" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" OnClick="BtnEditDistrito_Click" />
                                        <asp:Button ID="BtnActualizaDistrito" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="BtnActualizaDistrito_Click" />
                                        <asp:Button ID="BtnCancelDistrito" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtnCancelDistrito_Click"></asp:Button>
                                        <asp:Button ID="PregDistri" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" OnClick="PregDistri_Click"></asp:Button>
                                        <asp:Button ID="AregDistri" runat="server" CssClass="buttonPlan" Text="&lt;&lt;"
                                            Visible="False" Width="24px" OnClick="AregDistri_Click"></asp:Button>
                                        <asp:Button ID="SregDistri" runat="server" CssClass="buttonPlan" Text="&gt;&gt;"
                                            Visible="False" OnClick="SregDistri_Click"></asp:Button>
                                        <asp:Button ID="UregDistri" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False" OnClick="UregDistri_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Buscardistrito" runat="server" CssClass="busqueda" DefaultButton="btnBuscarDistrito" style="display:none;"
                                Height="232px"  Width="343px">
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBuscadistrito" runat="server" CssClass="labelsTit2" Text="Buscar Distrito" />
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblSelPaisDistrito" runat="server" CssClass="labels" Text="País:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CmbSelPaisDistrito" runat="server" AutoPostBack="True" CausesValidation="True"
                                                 Width="130px" 
                                                onselectedindexchanged="CmbSelPaisDistrito_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblSelDeptoDistrito" runat="server" CssClass="labels" Text="Departamento:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CmbSelDeptoDistrito" runat="server" AutoPostBack="True" CausesValidation="True"
                                                Width="130px" 
                                                onselectedindexchanged="CmbSelDeptoDistrito_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblSelCiudadDistrito" runat="server" CssClass="labels" Text="Ciudad:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CmbSelCiudadDistrito" runat="server" Width="130px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" CssClass="labels" Text="Codigo Distrito:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtbCodDistr" runat="server" MaxLength="3" Width="125px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="ReqbCodDistr" runat="server" ControlToValidate="TxtbCodDistr"
                                                Display="None" ErrorMessage="Requiere que el código contenga mínimo 3 números"
                                                ValidationExpression="([0-9]{3,3})"></asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="VCEReqBcoddistri" runat="server" Enabled="True"
                                                TargetControlID="ReqbCodDistr">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblbNomDistrito" runat="server" CssClass="labels" Text="Nombre Distrito:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtbNomDistrito" runat="server" MaxLength="50" Width="125px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="ReqbNomdistrito" runat="server" ControlToValidate="TxtbNomDistrito"
                                                Display="None" ErrorMessage="No ingrese caracteres especiales ni números &lt;br /&gt; No inicie con espacio en blanco"
                                                ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,49})"></asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="VCEReqBnomdistri" runat="server" Enabled="True"
                                                TargetControlID="ReqbNomdistrito">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnBuscarDistrito" runat="server" CssClass="buttonPlan"
                                                Text="Buscar" Width="80px" onclick="btnBuscarDistrito_Click" />
                                            <asp:Button ID="BtnCancelBdistrito" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                                Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnDistrito_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="btnCancelBdistrito" PopupControlID="Buscardistrito"
                                TargetControlID="BtnconsulDistrito" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Barrio" ID="Panel_Barrio">
                        <HeaderTemplate>
                            Barrio
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table align="center">
                                <tr>
                                    <td>
                                        <br />
                                        <asp:Label ID="LbltitBarrio" runat="server" CssClass="labelsTit2" Text="Gestión de Barrios"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                          
                            <table align="center">
                                <tr>
                                    <td>                                        
                                        <fieldset id="Fieldset9" runat="server">
                                            <legend style="">Información Básica</legend>
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSBPais" runat="server" CssClass="labels" Text="País *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbSBpais" runat="server" AutoPostBack="True" 
                                                            Enabled="False" OnSelectedIndexChanged="CmbSBpais_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSBDept" runat="server" CssClass="labels" Text="Departamento"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbSBDept" runat="server" AutoPostBack="True" 
                                                            Enabled="False" onselectedindexchanged="CmbSBDept_SelectedIndexChanged" 
                                                            Height="18px" Width="117px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSBciudad" runat="server" CssClass="labels" Text="Ciudad"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbSBciudad" runat="server" AutoPostBack="True" 
                                                            Enabled="False" onselectedindexchanged="CmbSBciudad_SelectedIndexChanged" 
                                                            Height="18px" Width="117px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSBdistr" runat="server" CssClass="labels" Text="Distrito"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbSBdistr" runat="server" Width="195px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblCodBarrio" runat="server" CssClass="labels" Text="Código de barrio *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtCodBarr" runat="server" BackColor="#DDDDDD" MaxLength="3" ReadOnly="True"
                                                            Width="190px" Enabled="False"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="ReqCodBarr" runat="server" ControlToValidate="TxtCodBarr"
                                                            Display="None" ErrorMessage="Requiere que el código contenga mínimo 3 números"
                                                            ValidationExpression="([0-9]{3,3})"></asp:RegularExpressionValidator>
                                                        <cc1:ValidatorCalloutExtender ID="VCEReqCodBarr" runat="server" Enabled="True" TargetControlID="ReqCodBarr">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblNomBarr" runat="server" CssClass="labels" Text="Nombre de barrio *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtNomBarr" runat="server" MaxLength="50" Width="190px" Enabled="False"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="ReqNomBarri" runat="server" ControlToValidate="TxtNomBarr"
                                                            Display="None" ErrorMessage="No ingrese caracteres especiales ni números &lt;br /&gt; No inicie con espacio en blanco"
                                                            ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,49})"></asp:RegularExpressionValidator>
                                                        <cc1:ValidatorCalloutExtender ID="VCEReqNomBarri" runat="server" Enabled="True" TargetControlID="ReqNomBarri">
                                                        </cc1:ValidatorCalloutExtender>
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
                                        <asp:Label ID="LblEstadoBarrio" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RbtnEstadoBarrios" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False">
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
                                        <asp:Button ID="BtnCrearBarrios" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" OnClick="BtnCrearBarrios_Click"></asp:Button>
                                        <asp:Button ID="BtnSaveBarrios" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnSaveBarrios_Click" />
                                        <asp:Button ID="BtnConsulBarrios" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px"></asp:Button>
                                        <asp:Button ID="BtnEditBarrios" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" OnClick="BtnEditBarrios_Click" />
                                        <asp:Button ID="BtnActualizaBarrios" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="BtnActualizaBarrios_Click" />
                                        <asp:Button ID="BtncancelBarrios" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtncancelBarrios_Click"></asp:Button>
                                        <asp:Button ID="PregBarrio" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" onclick="PregBarrio_Click" />
                                        <asp:Button ID="AregBarrio" runat="server" CssClass="buttonPlan" Text="&lt;&lt;"
                                            Visible="False" onclick="AregBarrio_Click" />
                                        <asp:Button ID="SregBarrio" runat="server" CssClass="buttonPlan" Text="&gt;&gt;"
                                            Visible="False" onclick="SregBarrio_Click" />
                                        <asp:Button ID="UregBarrio" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False" onclick="UregBarrio_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarBarrio" runat="server" CssClass="busqueda" 
                            DefaultButton="btnBuscarBarrio" Height="264px" style="display:none;"
                            Width="343px">
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblBuscabarrio" runat="server" CssClass="labelsTit2"
                                            Text="Buscar Barrio" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblSelPaisBarrio" runat="server" CssClass="labels" 
                                            Text="País:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CmbSelPaisBarrio" runat="server" AutoPostBack="True" 
                                            CausesValidation="True" 
                                            onselectedindexchanged="CmbSelPaisBarrio_SelectedIndexChanged" Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblSelDeptoBarrio" runat="server" CssClass="labels" 
                                            Text="Departamento:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CmbSelDeptoBarrio" runat="server" AutoPostBack="True" 
                                            CausesValidation="True" 
                                             Width="130px" 
                                            onselectedindexchanged="CmbSelDeptoBarrio_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblSelCiudadBarrio" runat="server" CssClass="labels" 
                                            Text="Ciudad:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CmbSelCiudadBarrio" runat="server" AutoPostBack="True" 
                                            CausesValidation="True" 
                                            onselectedindexchanged="CmbSelCiudadBarrio_SelectedIndexChanged" Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblSelDistritoBarrio" runat="server" CssClass="labels" 
                                            Text="Distrito:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CmbSelDistritoBarrio" runat="server" Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblbNomBarrio" runat="server" CssClass="labels" 
                                            Text="Nombre Barrio:" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtbNomBarrio" runat="server" MaxLength="50" Width="125px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="ReqbNomBarrio" runat="server" 
                                            ControlToValidate="TxtbNomBarrio" Display="None" 
                                            ErrorMessage="No ingrese caracteres especiales ni números &lt;br /&gt; No inicie con espacio en blanco" 
                                            ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,49})"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender ID="VCEReqNomBarrio" runat="server" 
                                            Enabled="True" TargetControlID="ReqbNomBarrio">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnBuscarBarrio" runat="server" CssClass="buttonPlan" 
                                            onclick="btnBuscarBarrio_Click" Text="Buscar" Width="80px" />
                                        <asp:Button ID="BtnCancelBBarrio" runat="server" CssClass="buttonPlan"
                                            Text="Cancelar" Width="80px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="IbtnBarrio_ModalPopupExtender" runat="server" 
                            BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" 
                            OkControlID="btnCancelBBarrio" PopupControlID="BuscarBarrio" 
                            TargetControlID="BtnConsulBarrios" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
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
