<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestiónGeneral.aspx.cs"    Inherits="SIGE.Pages.Modulos.Administrativo.GestiónGeneral" %>

<%@ Register Assembly="Telerik.Web.UI, Version=2009.2.826.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4"
    Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión General</title>
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
            .style51
            {
                width: 93px;
            }
            .style54
            {
                width: 118px;
            }
        </style>
    <link href="../../css/StiloAdministrativo.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: transparent;">
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="release"> 
        </cc1:ToolkitScriptManager>

        <!-- se agrego el parametro ScriptMode=release a ToolkitScriptManager soluciona problema de event handler exception - 25/05/2011 Angel Ortiz -->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
            </cc2:ModalUpdateProgress>
            
            <div>
                <cc1:TabContainer ID="TabAdminGeneral" runat="server" ActiveTabIndex="4" Width="100%"
                    Height="460px" allowtransparency="true" Enabled="true" style="overflow:auto">

                    <%-- 
                    <cc1:TabPanel runat="server" HeaderText="Gestión Indicadores" ID="Panel_Indicadores">
                <HeaderTemplate>
                    Gestión Indicadores
                </HeaderTemplate>
                <ContentTemplate>
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="LblTitIndicador" runat="server" CssClass="labelsTit" Text="Administración de Indicadores"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    <table align="center" class="CargaArchivos" style="border: 1px solid #000000; width: 590px;">
                        <tr>
                            <td colspan="2">
                                <table align="center" style="border: 1px solid #000000;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="Nombre del indicador * "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtNameIndicador" runat="server" MaxLength="40" Width="400px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="ReqNameIndicador" runat="server" ControlToValidate="TxtNameIndicador"
                                                Display="none" ErrorMessage="no puede iniciar con número ni espacio en blanco y no ingrese caracteres especiales."
                                                ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s]{0,40})">
                                            </asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender30" runat="server" Enabled="True"
                                                TargetControlID="ReqNameIndicador">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <fieldset id="LbCampos" runat="server">
                                    <legend style="color: #000000;">Descripción * </legend>
                                    <table align="center" width="300px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TxtDescIndic" runat="server" MaxLength="255" TextMode="MultiLine"
                                                    Width="280px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="ReqDescIndic" runat="server" ControlToValidate="TxtDescIndic"
                                                    Display="none" ErrorMessage="No debe iniciar con numero ni espacios en blanco . No ingrese caracteres especiales. &lt;br /&gt; No exceda el limite de 255 caracteres"
                                                    ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s]{1,254})">
                                                </asp:RegularExpressionValidator>
                                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender31" runat="server" Enabled="True"
                                                    TargetControlID="ReqDescIndic">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset id="LbPaisServind" runat="server">
                                    <legend style="color: #000000;">País * </legend>
                                    <table align="center" width="300px">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="CmbSelPaisServInd" runat="server" AutoPostBack="True">
                                                   CausesValidation="True" Width="280px" >
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset id="Lbservicioind" runat="server">
                                    <legend style="color: #000000;">Servicio * </legend>
                                    <table align="center" width="300px">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="cmbSelServicioInd" runat="server" AutoPostBack="True">
                                                    CausesValidation="True" 
                                                                            OnSelectedIndexChanged="cmbSelServicioInd_SelectedIndexChanged" Width="280px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset id="LbInforme" runat="server">
                                    <legend style="color: #000000;">Informe * </legend>
                                    <table align="center" width="300px">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="CmbSelInfoIndi" runat="server" Width="280px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td>
                                <fieldset id="Fieldset1" runat="server">
                                    <legend style="color: #000000;">Fórmula * </legend>
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TxtFormula" runat="server" MaxLength="255" Width="325px"></asp:TextBox>
                                                <br />
                                                <asp:Label ID="lblTipoope" runat="server" Text="Tipo:"></asp:Label>
                                                <asp:RadioButtonList ID="RBtnLTipo" runat="server" RepeatDirection="Vertical">
                                                    <asp:ListItem Selected="True" Text="Operando ($)" Value="$"></asp:ListItem>
                                                    <asp:ListItem Text="Dato Calculado (#)" Value="#"></asp:ListItem>
                                                    <asp:ListItem Text="Parámetro (@)" Value="@"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                 &nbsp;<asp:Button ID="BtnAceptExpr" runat="server" onclick="BtnAceptExpr_Click" 
                                                                            Text="Aceptar" />
                                                <cc1:TextBoxWatermarkExtender ID="txtformula_TextBoxWatermarkExtender" runat="server"
                                                    Enabled="True" TargetControlID="TxtFormula" WatermarkCssClass="modalBack" WatermarkText="Ingrese un parámetro y oprima &lt;Aceptar&gt;">
                                                </cc1:TextBoxWatermarkExtender>
                                                <asp:RegularExpressionValidator ID="ReqTxtExpress" runat="server" ControlToValidate="TxtFormula"
                                                    Display="none" ErrorMessage="No puede contener caracteres especiales , no exceda el limite de 255 caracteres, no puede contener espacios en blanco"
                                                    ValidationExpression="([a-zA-ZñÑáéíóúÁÉÍÓÚ0-9]{1,255})">
                                                </asp:RegularExpressionValidator>
                                                <cc1:ValidatorCalloutExtender ID="VCEReqTxtExpress" runat="server" Enabled="True"
                                                    TargetControlID="ReqTxtExpress">
                                                </cc1:ValidatorCalloutExtender>
                                                <br />
                                                <br />
                                                <asp:Button ID="SelSum" runat="server" Height="26" Text="+" Width="26" />
                                                &nbsp;
                                                <asp:Button ID="SelRes" runat="server" Height="26" Text="-" Width="26" />
                                                &nbsp;
                                                <asp:Button ID="SelMul" runat="server" Height="26" Text="*" Width="26" />
                                                &nbsp;
                                                <asp:Button ID="SelDiv" runat="server" Height="26" Text="/" Width="26" />
                                                &nbsp;
                                                <asp:Button ID="SelParIz" runat="server" Height="26" Text="(" Width="26" />
                                                &nbsp;
                                                <asp:Button ID="SelParDe" runat="server" Height="26" Text=")" Width="26" />
                                                &nbsp;
                                                <asp:Button ID="BtnAtrasIndi" runat="server" Height="26" Text="Borrar" />
                                                &nbsp;
                                                <asp:Button ID="BtnFinIndicador" runat="server" Height="26" Text="Fin" />
                                                <br />
                                                <br />
                                                <asp:Label ID="LblExprfinal" runat="server" Text="Expresión final* "></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtExpress" runat="server" BackColor="#DDDDDD" ReadOnly="True" TextMode="MultiLine"
                                                    Width="325px"></asp:TextBox>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="LblEstadoInd" runat="server"  Text="Estado"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RBtnEstadoInd" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="10pt" ForeColor="White" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                    <asp:ListItem>Deshabilitado</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="BtnCrearInd" runat="server"  Text="Crear" Width="95px" />
                                <asp:Button ID="BtnSaveInd" runat="server"  Text="Guardar" Visible="False"
                                    Width="95px" />
                                <asp:Button ID="btnCosulInd" runat="server"  Text="Consultar" Width="95px" />
                                <asp:Button ID="BtnEditInd" runat="server"  Text="Editar" Visible="False"
                                    Width="95px" />
                                <asp:Button ID="btnActualInd" runat="server"  Text="Actualizar"
                                    Visible="False" Width="95px" />
                                <asp:Button ID="btnCancelInd" runat="server"  Text="Cancelar" Width="95px" />
                                <asp:Button ID="btnPreg12" runat="server"  Text="|&lt;&lt;" Visible="False" />
                                <asp:Button ID="btnAreg12" runat="server"  Text="&lt;&lt;" Visible="False" />
                                <asp:Button ID="btnSreg12" runat="server"  Text="&gt;&gt;" Visible="False" />
                                <asp:Button ID="btnUreg12" runat="server"  Text="&gt;&gt;|" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            --%>

                    <cc1:TabPanel runat="server" HeaderText="Gestión Cliente " ID="Cliente" allowtransparency="true">
                        <HeaderTemplate>Gestión Cliente</HeaderTemplate>
                        <ContentTemplate><br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LbltitClien" runat="server" Text="Gestión Clientes"></asp:Label>
                                    </td>
                                </tr>
                            </table><br />

                            <table align="center">
                                <tr>
                                    <td>
                                        <fieldset ID="ContenUbicCli" runat="server"><legend style="">Ubicación</legend>
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblPaisCli" runat="server" CssClass="labels" Text="País*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbPaisCli" runat="server" AutoPostBack="True" Enabled="False" onselectedindexchanged="cmbPaisCli_SelectedIndexChanged" Width="134px"></asp:DropDownList>
                                                    </td>
                                                    <td rowspan="2">
                                                        <asp:Image ID="imglogoCliente" runat="server" Height="51px" visible="False" Width="158px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblTelCli" runat="server" CssClass="labels" Text="Teléfono"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtTelCli" runat="server" Enabled="False" MaxLength="12" Width="129px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblDirCli" runat="server" CssClass="labels" Text="Dirección"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="TxtDirCli" runat="server" Enabled="False" MaxLength="150" Width="300px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblMailCli" runat="server" CssClass="labels" Text="Correo electrónico*"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="TxtMailCli" runat="server" Enabled="False" Width="300px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblLogoCliente" runat="server" CssClass="labels" Text="Logo Cliente*"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:UpdatePanel ID="UpdateCliente" runat="server">
                                                            <ContentTemplate>
                                                                <asp:FileUpload ID="FileCliente" runat="server" Enabled="False" Width="300px" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="btnsaveCli" /><asp:PostBackTrigger ControlID="btnActuCli" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset> 
                                    </td>
                                </tr>
                            </table>
                            
                            <table align="center">
                                <tr>
                                    <td>
                                        <fieldset id="ContenClie" runat="server"><legend style="">Cliente</legend>
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblTipDocCli" runat="server" CssClass="labels" Text="Tipo de Documento*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbTipDocCli" runat="server" Width="144px" 
                                                            Enabled="False"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblCodCliente" runat="server" CssClass="labels" Text="Código:"></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtCodCliente" runat="server" BackColor="#DDDDDD" ReadOnly="True" Width="108px" Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblNumDocCli" runat="server" CssClass="labels" Text="Número de Documento*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtNumdocCli" runat="server" MaxLength="11" Width="139px" 
                                                            Enabled="False"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblTipoClie" runat="server" CssClass="labels" Text="Tipo*"></asp:Label>&nbsp;&nbsp;&nbsp; 
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbTipoClie" runat="server" Enabled="False"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblNomClie" runat="server" CssClass="labels" Text="Nombre*"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="TxtNomClie" runat="server" MaxLength="50" Width="348px" 
                                                            Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset> 
                                    </td>
                                </tr>
                            </table>
                            
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="TitEstadoClie" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RBtnListStatusClie" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" 
                                            Enabled="False">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            
                            <table align="center">
                                <tr>
                                    <td valign="top">
                                        <asp:Button ID="btnCrearClie" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px" onclick="btnCrearClie_Click" />
                                        <asp:Button ID="btnsaveCli" runat="server" CssClass="buttonPlan" Text="Guardar" Visible="False" Width="95px" onclick="btnsaveCli_Click" />
                                        <asp:Button ID="btnConsultarclie" runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px" />
                                        <asp:Button ID="btnEditCli" runat="server" CssClass="buttonPlan" Text="Editar" Visible="False" Width="95px" onclick="btnEditCli_Click" />
                                        <asp:Button ID="btnActuCli" runat="server" CssClass="buttonPlan" Text="Actualizar" Visible="False" Width="95px" onclick="btnActuCli_Click" />
                                        <asp:Button ID="btnCancelClie" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="95px" onclick="btnCancelClie_Click" />
                                        <asp:Button ID="btnPreg" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;" Visible="False" onclick="btnPreg_Click" />
                                        <asp:Button ID="btnAreg" runat="server" CssClass="buttonPlan" Text="&lt;&lt;" Visible="False" onclick="btnAreg_Click" />
                                        <asp:Button ID="btnSreg" runat="server" CssClass="buttonPlan" Text="&gt;&gt;" Visible="False" onclick="btnSreg_Click" />
                                        <asp:Button ID="btnUreg" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|" Visible="False" onclick="btnUreg_Click" />
                                    </td>
                                </tr>
                            </table>
                        
                            <asp:Panel ID="BuscarComp" runat="server" CssClass="busqueda" 
                                DefaultButton="BtnBuscarComp" Height="215px" style=" display:none;"
                                Width="350px">
                                <table align="center">
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Label ID="LblBCom" runat="server" CssClass="labelsTit2" Text="Buscar Cliente"></asp:Label>
                                        </td>
                                    </tr>
                                <br />
                                </table><br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblDocCli" runat="server" CssClass="labels" Text="Documento"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDocCli" runat="server" MaxLength="11"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblSPais" runat="server" CssClass="labels" Text="País"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbSPais" runat="server" Width="155px"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblCompañia" runat="server" CssClass="labels" Text="Nombre"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCompañia" runat="server" MaxLength="50" Width="155px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table><br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnBuscarComp" runat="server" CssClass="buttonPlan" Text="Buscar" Width="80px" onclick="BtnBuscarComp_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnCancelarComp" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="80px" />        
                                        </td>
                                    </tr>
                                </table>   
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnComp_ModalPopupExtender" runat="server" 
                                                    BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" 
                                                    OkControlID="btnCancelarComp" PopupControlID="BuscarComp" 
                                TargetControlID="btnConsultarclie" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="Panel_TipoCanal" runat="server" HeaderText="Tipo Canal">
                        <HeaderTemplate>Tipo Canal</HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <div class="centrarcontenido">
                                <span class="labelsTit2">Gestión Tipo de Canal</span> 
                            </div>
                            <br />
                            <div class="centrar">
                                <div class="tabla centrar">
                                   <fieldset style=" padding:10px">
                                        <legend>Tipo de Canal </legend>
                                        <br />
                                        <div class="fila">
                                            <div class="celda">
                                                <span class="labels">Nombre*</span> 
                                            </div>
                                            <div class="celda">
                                                <asp:TextBox ID="txt_chtype_nombre" runat="server" Enabled="False">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="celda" style="vertical-align:top">
                                                <span class="labels">Descripción</span> 
                                            </div>
                                            <div class="celda">
                                                <asp:TextBox ID="txt_chtype_desc" runat="server" Enabled="False" Height="109px" 
                                                Rows="10" TextMode="MultiLine" Width="378px" style = "resize:none">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </fieldset> 
                                </div>
                            </div>
                            <br />
                            <div class = "centrarcontenido">
                                <asp:Button ID="btn_chtype_crear" CssClass="buttonPlan" runat="server" 
                                    Width="95px" Text="Crear" onclick="btn_chtype_crear_Click" />
                                <asp:Button ID="btn_chtype_guardar" CssClass="buttonPlan" runat="server" 
                                    Width="95px" Text="Guardar" Visible="False" onclick="btn_chtype_guardar_Click"/>
                                <asp:Button ID="btn_chtype_consultar" CssClass="buttonPlan" runat="server" Width="95px" Text="Consultar" />
                                <asp:Button ID="btn_chtype_cancelar" CssClass="buttonPlan" runat="server" 
                                    Width="95px" Text="Cancelar" onclick="btn_chtype_cancelar_Click" />
                            </div>
                            <asp:Panel ID="Buscar_tipocanal" runat="server" CssClass="busqueda" DefaultButton="btn_bchtype_buscar"
                                Style="display: none;" Height="245px" Width="395px">
                                <br />
                                <div class="centrarcontenido">
                                    <span class="labelsTit2">Buscar tipo de canal</span> 
                                </div>
                                <br />
                                <div class="centrar">
                                    <div class="tabla centrar">
                                        <div class="fila">
                                            <div class="celda">
                                                <span class="labels">Nombre*</span> 
                                            </div>
                                            <div class="celda">
                                                <asp:TextBox ID="txt_bchtype_nombre" runat="server">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="celda" style="vertical-align:top">
                                                <span class="labels">Descripción</span> 
                                            </div>
                                            <div class="celda">
                                                <asp:TextBox ID="txt_bchtype_descripcion" runat="server" Height="105px" 
                                                Rows="10" TextMode="MultiLine" Width="278px" style = "resize:none">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="centrarcontenido">
                                    <asp:Button ID="btn_bchtype_buscar" CssClass="buttonPlan" runat="server" 
                                    Width="95px" Text="Buscar" onclick="btn_bchtype_buscar_Click" />
                                    <asp:Button ID="btn_bchtype_cancelar" CssClass="buttonPlan" runat="server" 
                                    Width="95px" Text="Cancelar" />
                                </div>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="Modal_buscartipocanal" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="btn_bchtype_cancelar" PopupControlID="Buscar_tipocanal"
                                TargetControlID="btn_chtype_consultar" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="consulta_gvtipocanal" runat="server" style= "display: block">
                                <div class="centrar centrarcontenido">
                                    <div class="p" style="width:780px; height: 315px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
	                                        font-family : arial, Helvetica, sans-serif;">
                                        <telerik:RadGrid ID="rgv_tipocanal" runat="server" AutoGenerateColumns="False" 
                                            GridLines="None" oncancelcommand="rgv_tipocanal_CancelCommand" 
                                            oneditcommand="rgv_tipocanal_EditCommand" 
                                            onupdatecommand="rgv_tipocanal_UpdateCommand">
                                            <MasterTableView EditMode="InPlace">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="chtype_id" HeaderText="Código" ReadOnly="true"
                                                    UniqueName="chtype_id">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="chtype_nombre" HeaderText="Nombre" 
                                                    UniqueName="chtype_nombre">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="chtype_descripcion" 
                                                    HeaderText="Descripción" UniqueName="chtype_descripcion">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridCheckBoxColumn DataField="chtype_status" HeaderText="Estado" 
                                                    UniqueName="chtype_status">
                                                    </telerik:GridCheckBoxColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" 
                                                    UpdateText="Actualizar">
                                                    </telerik:GridEditCommandColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                        <br /><br />
                                        <div class="centrarcontenido">
                                            <asp:Button ID="Button1" CssClass="buttonPlan" runat="server" Width="95px" Text="Salir" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="mp_consulta_gvtipocanal" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True"  PopupControlID="consulta_gvtipocanal"
                                TargetControlID="btnpopuprgv" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btnpopuprgv" runat="server" CssClass="alertas" Width="0px" />
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel runat="server" HeaderText="Gestión Canal" ID="Panel_Canal"><HeaderTemplate>Gestión Canal</HeaderTemplate><ContentTemplate><br /><br /><br /><br /><br /><br /><table align="center"><tr><td><asp:Label 
                        ID="lblTitCanales" runat="server" CssClass="labelsTit2" 
                        Text="Administración de Canales"></asp:Label></td></tr></table><br /><br /><br /><br /><table align="center"><tr><td><fieldset 
                            ID="Fieldset13" runat="server"><legend style="">Canal</legend><br /><br /><table 
                            align="center"><tr><td><asp:Label ID="LblCodCanal" runat="server" 
                                CssClass="labels" Text="Código * "></asp:Label></td><td><asp:TextBox 
                                    ID="TxtCodCanal" runat="server" Enabled="False" MaxLength="4"></asp:TextBox></td></tr><tr><td><asp:Label 
                                ID="LblClienteCanal" runat="server" CssClass="labels" Text="Cliente*"></asp:Label></td><td><asp:DropDownList 
                                    ID="CmbClienteCanal" runat="server" Enabled="False" Width="337px"></asp:DropDownList></td></tr><tr><td><asp:Label 
                                ID="LblNomCanal" runat="server" CssClass="labels" Text="Nombre * "></asp:Label></td><td><asp:TextBox 
                                    ID="TxtNomCanal" runat="server" Enabled="False" MaxLength="40" Width="332px"></asp:TextBox></td></tr><tr><td 
                                valign="top"><span class="labels">Tipo Canal *</span> </td><td><asp:DropDownList 
                                    ID="ddl_channel_type" runat="server" Enabled="False" Width="145px"></asp:DropDownList></td></tr><tr><td 
                                valign="top"><asp:Label ID="LblDescCanal" runat="server" CssClass="labels" 
                                Text="Descripción * "></asp:Label></td><td><asp:TextBox ID="TxtDescCanal" 
                                    runat="server" Enabled="False" Height="68px" MaxLength="255" 
                                    TextMode="MultiLine" Width="334px"></asp:TextBox></td></tr></table><br /><br /></fieldset> </td></tr></table><br /><br /><br /><br /><table align="center"><tr><td><asp:Label ID="TitEstadoCanal" runat="server" CssClass="labels" Text="Estado"></asp:Label></td><td><asp:RadioButtonList ID="RBtnListStatusCanal" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" Enabled="False" RepeatDirection="Horizontal"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr></table><br /><br /><table align="center"><tr><td><asp:Button ID="btnCrearCanal" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" onclick="btnCrearCanal_Click" /><asp:Button ID="btnsavecanal" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" onclick="btnsavecanal_Click" /><asp:Button ID="btnConsultarCanal" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" /><asp:Button ID="btnEditCanal" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" onclick="btnEditCanal_Click" /><asp:Button ID="btnActualizarCanal" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" onclick="btnActualizarCanal_Click" /><asp:Button ID="btnCancelCanal" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" onclick="btnCancelCanal_Click" /><asp:Button ID="btnPreg6" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;" 
                                            Visible="False" onclick="btnPreg6_Click" /><asp:Button ID="btnAreg6" runat="server" CssClass="buttonPlan" Text="&lt;&lt;" 
                                            Visible="False" onclick="btnAreg6_Click" /><asp:Button ID="btnSreg6" runat="server" CssClass="buttonPlan" Text="&gt;&gt;" 
                                            Visible="False" onclick="btnSreg6_Click" /><asp:Button ID="btnUreg6" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|" 
                                            Visible="False" onclick="btnUreg6_Click" /></td></tr></table><asp:Panel ID="Buscarcanal" runat="server" CssClass="busqueda" 
                            DefaultButton="BtnBCanales" Height="202px"  style="display:none" Width="343px"><br /><br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160;&#160; <table align="center"><tr><td><asp:Label ID="LblBcanal" runat="server" CssClass="labelsTit2" Text="Buscar Canal" /></td></tr></table><br /><br /><table align="center"><tr><td><asp:Label ID="LblBCodCanal" runat="server" CssClass="labels" Text="Código:" /></td><td><asp:TextBox ID="TxtBCodCanal" runat="server" MaxLength="4" Width="100px"></asp:TextBox></td></tr><tr><td><asp:Label ID="LblBCanales" runat="server" CssClass="labels" 
                                            Text="Canal:" /></td><td><asp:TextBox ID="TxtBCanal" runat="server" MaxLength="50" Width="200px"></asp:TextBox></td></tr></table><br /><br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160; <asp:Button ID="BtnBCanales" runat="server" CssClass="buttonPlan" 
                                onclick="BtnBCanales_Click" Text="Buscar" Width="80px" /><asp:Button ID="BtncancelarCanales" runat="server" CssClass="buttonPlan"
                                Text="Cancelar" Width="80px" /></asp:Panel><cc1:ModalPopupExtender ID="IbtnCanales_ModalPopupExtender" runat="server" 
                            BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" 
                            OkControlID="BtnCancelarCanales" PopupControlID="Buscarcanal" 
                            TargetControlID="btnConsultarCanal" DynamicServicePath=""></cc1:ModalPopupExtender></ContentTemplate></cc1:TabPanel>
                    
                    <cc1:TabPanel runat="server" HeaderText="SubCanal" ID="Panel_SubCanal"><HeaderTemplate>SubCanal</HeaderTemplate><ContentTemplate><br /><br /><table align="center"><tr><td><asp:Label 
                        ID="titulo" runat="server" CssClass="labelsTit2" Text="Gestión SubCanal"></asp:Label></td></tr></table><br /><br /><br /><br /><br /><br /><table align="center"><tr><td><br /><br /><fieldset 
                            ID="Fieldset1" runat="server"><legend style="">SubCanal</legend><br /><br /><table 
                            align="center"><tr><td><asp:Label ID="labelCodSubCanal" runat="server" 
                                CssClass="labels" Text="Código*"></asp:Label></td><td><asp:TextBox 
                                    ID="TxtCodSubCanal" runat="server" BackColor="#DDDDDD" Enabled="False" 
                                    ReadOnly="True" Width="80px"></asp:TextBox></td></tr><tr><td><asp:Label 
                                ID="Lblclientesubchannel" runat="server" CssClass="labels" Text="Cliente *"></asp:Label></td><td><asp:DropDownList 
                                    ID="cmbClientesubchannel" runat="server" AutoPostBack="True" Enabled="False" 
                                    onselectedindexchanged="cmbClientesubchannel_SelectedIndexChanged" 
                                    Width="255px"></asp:DropDownList></td></tr><tr><td><asp:Label 
                                ID="lblCanal" runat="server" CssClass="labels" Text="Canal *"></asp:Label></td><td><asp:DropDownList 
                                    ID="cmbcanalSub" runat="server" Enabled="False" Width="255px"></asp:DropDownList><asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator4" runat="server" 
                                    ControlToValidate="TxtNomSubCanal" Display="None" 
                                    ErrorMessage="No debe iniciar con espacio en blanco, &lt;br /&gt;No ingrese caracteres especiales " 
                                    ValidationExpression="([a-zA-Z0-9][a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s]{0,49})"></asp:RegularExpressionValidator></td></tr><tr><td><asp:Label 
                                ID="LblNomSubCanalma" runat="server" CssClass="labels" Text="Nombre *"></asp:Label></td><td><asp:TextBox 
                                    ID="TxtNomSubCanal" runat="server" Enabled="False" MaxLength="50" Width="250px"></asp:TextBox><asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator1" runat="server" 
                                    ControlToValidate="TxtNomSubCanal" Display="None" 
                                    ErrorMessage="No debe iniciar con espacio en blanco, &lt;br /&gt;No ingrese caracteres especiales " 
                                    ValidationExpression="([a-zA-Z0-9][a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s]{0,49})"></asp:RegularExpressionValidator></td></tr></table><br /><br /></fieldset> </td></tr></table><table align="center"><tr><td><asp:Label ID="Label5" runat="server" CssClass="labels" Text="Estado"></asp:Label></td><td><asp:RadioButtonList ID="RBtSubCanal" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr></table><br /><br /><br /><br /><br /><br /><table align="center"><tr><td><asp:Button ID="BtnCrearSubCanal" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" onclick="BtnCrearSubCanal_Click"  /><asp:Button ID="BtnGuardarSubCanal" runat="server" CssClass="buttonPlan" Text="Guardar" Visible="False" Width="95px" onclick="BtnGuardarSubCanal_Click"/><asp:Button ID="BtConsultarSubCanal" runat="server" CssClass="buttonPlan" 
                                            Text="Consultar" Width="95px" onclick="BtConsultarSubCanal_Click" /><asp:Button
                                                        ID="BtnEditarSubCanal" runat="server" CssClass="buttonPlan" Text="Editar"
                                                        Visible="False" Width="95px" 
                                            onclick="BtnEditarSubCanal_Click"  /><asp:Button ID="BtnActualizarSubCanal"
                                                            runat="server" CssClass="buttonPlan" Text="Actualizar" 
                                            Visible="False" Width="95px" onclick="BtnActualizarSubCanal_Click1"
                                                             /><asp:Button ID="BtnCancelarSubCanal"
                                                                runat="server" CssClass="buttonPlan" Text="Cancelar" 
                                            Width="95px" onclick="BtnCancelarSubCanal_Click1" /><asp:Button ID="BtnPreSubCanal" runat="server" 
                                            CssClass="buttonPlan" Text="|&lt;&lt;"
                                                                    Visible="False" onclick="BtnPreSubCanal_Click1"  /><asp:Button ID="BtnAntSubCanal"
                                                                        runat="server" CssClass="buttonPlan" 
                                            Text="&lt;&lt;" Visible="False" onclick="BtnAntSubCanal_Click" /><asp:Button
                                                                            ID="BtnSigSubCanal" runat="server" 
                                            CssClass="buttonPlan" Text="&gt;&gt;"
                                                                            Visible="False" 
                                            onclick="BtnSigSubCanal_Click" /><asp:Button ID="BtnUltSubCanal"
                                                                                runat="server" CssClass="buttonPlan" 
                                            Text="&gt;&gt;|" Visible="False" onclick="BtnUltSubCanal_Click"/></td></tr></table><asp:Panel ID="BuscarSubCanal" runat="server" CssClass="busqueda" DefaultButton="BtnBusSubCanal"
                                Style="display: none;" Height="202px" Width="343px"><br /><br /><table align="center"></table><br /><br /><table align="center"><tr><td><asp:Label 
                            ID="lblBSubCanal" runat="server" CssClass="labelsTit2" Text="Buscar SubCanal" /></td></tr><tr><td><asp:Label 
                                ID="LblBClienteSUB" runat="server" CssClass="labels" Text="Cliente:" /></td><td><asp:DropDownList 
                                    ID="CMBBClienteChannel" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="CMBBClienteChannel_SelectedIndexChanged" Width="215px"></asp:DropDownList></td></tr><tr><td><asp:Label ID="lblCanalB" runat="server" CssClass="labels" Text="Canal:" /></td><td><asp:DropDownList ID="cmbBCanalSC" runat="server" Width="215px"></asp:DropDownList></td></tr><tr><td><asp:Label ID="lblNomSubCanal" runat="server" CssClass="labels" Text="Nombre:" /></td><td><asp:TextBox ID="TxtBNomSubCanal" runat="server" MaxLength="50" Width="210px"></asp:TextBox></td></tr></table><br /><br /><div align="center"><asp:Button ID="BtnBusSubCanal" runat="server" CssClass="buttonPlan" Text="Buscar"
                                        Width="80px" OnClick="BtnBusSubCanal_Click" /><asp:Button ID="CancelaSubCanal"
                                            runat="server" CssClass="buttonPlan" Text="Cancelar" Width="80px" 
                                        onclick="CancelaSubCanal_Click" /></div></asp:Panel><cc1:ModalPopupExtender ID="ModalPopupSubCanal" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="CancelaSubCanal" PopupControlID="BuscarSubCanal"
                                TargetControlID="BtConsultarSubCanal" DynamicServicePath=""></cc1:ModalPopupExtender></ContentTemplate></cc1:TabPanel>

                    <cc1:TabPanel runat="server" HeaderText="Gestión Material POP" ID="Panel_Material_POP"><HeaderTemplate>Gestión Material POP</HeaderTemplate><ContentTemplate><br /><br /><table align="center"><tr><td><asp:Label 
                        ID="LblTitPop" runat="server" CssClass="labelsTit2" 
                        Text="Administración de Material POP"></asp:Label></td></tr></table><br /><br /><table align="center"><tr><td><fieldset 
                            ID="Fieldset4" runat="server"><legend style="">Material POP</legend><table 
                            width="570px"><tr><td class="style51"><asp:Label ID="LblCodPOP" runat="server" 
                                CssClass="labels" Text="Código de POP * "></asp:Label></td><td><asp:TextBox 
                                    ID="TxtCodPOP" runat="server" BackColor="#DDDDDD" Enabled="False" 
                                    ReadOnly="True"></asp:TextBox></td></tr><tr><td class="style51"><asp:Label 
                                ID="LblNamePOP" runat="server" CssClass="labels" Text="Nombre de POP * "></asp:Label></td><td><asp:TextBox 
                                    ID="TxtNamePOP" runat="server" Enabled="False" MaxLength="50" Width="332px"></asp:TextBox><asp:RegularExpressionValidator 
                                    ID="ReqnamePOP" runat="server" ControlToValidate="TxtNamePOP" Display="None" 
                                    ErrorMessage="El Material POP no debe empezar por espacio en blanco ni número, &lt;br /&gt; ni poseer caracteres especiales" 
                                    ValidationExpression="([a-z-A-Z][a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]{0,50})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender 
                                    ID="ValidatorCalloutExtender3" runat="server" Enabled="True" 
                                    TargetControlID="ReqnamePOP"></cc1:ValidatorCalloutExtender></td></tr><tr><td 
                                class="style51"><asp:Label ID="Label1" runat="server" CssClass="labels" 
                                Text="Tipo de Material * "></asp:Label></td><td><asp:DropDownList 
                                    ID="ddltipomaterial" runat="server" Enabled="false" Width="200px"></asp:DropDownList></td></tr><tr><td 
                                class="style51" valign="top"><asp:Label ID="LblDesPOP" runat="server" 
                                CssClass="labels" Text="Descripción del POP * "></asp:Label></td><td><asp:TextBox 
                                    ID="TxtDescPOP" runat="server" Enabled="False" Height="68px" MaxLength="255" 
                                    TextMode="MultiLine" Width="334px"></asp:TextBox><asp:RegularExpressionValidator 
                                    ID="ReqDescPOP" runat="server" ControlToValidate="TxtDescPOP" Display="None" 
                                    ErrorMessage="No debe comenzar por número ni espacio en blanco y &lt;br /&gt; No ingrese caracteres especiales y no exceda 255 caracteres" 
                                    ValidationExpression="([a-zA-Z][\w\sñÑáéíóúÁÉÍÓÚ0-9.,;]{0,254})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender 
                                    ID="VCEReqDescPOP" runat="server" Enabled="True" TargetControlID="ReqDescPOP"></cc1:ValidatorCalloutExtender></td></tr></table><br /><br /></fieldset> <br /><br /><br /><br /><table 
                            align="center"><tr><td><asp:Label ID="LblEstadoPOP" runat="server" 
                                    CssClass="labelsTit2" Text="Estado"></asp:Label></td><td><asp:RadioButtonList 
                                        ID="RBtnListStatusPOP" runat="server" Enabled="False" Font-Bold="True" 
                                        Font-Size="10pt" ForeColor="Black"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr></table><br /><br /><br /><br /><table 
                            align="center"><tr><td><asp:Button ID="BtnCrearPOP" runat="server" 
                                    CssClass="buttonPlan" onclick="BtnCrearPOP_Click" Text="Crear" Width="95px" /><asp:Button 
                                    ID="BtnSavePOP" runat="server" CssClass="buttonPlan" onclick="BtnSavePOP_Click" 
                                    Text="Guardar" Visible="False" Width="95px" /><asp:Button 
                                    ID="BtnConsultaPOP" runat="server" CssClass="buttonPlan" Text="Consultar" 
                                    Width="95px" /><asp:Button ID="btnEditMPOP" runat="server" 
                                    CssClass="buttonPlan" onclick="btnEditMPOP_Click" Text="Editar" Visible="False" 
                                    Width="95px" /><asp:Button ID="BtnActualizaPOP" runat="server" 
                                    CssClass="buttonPlan" onclick="BtnActualizaPOP_Click" Text="Actualizar" 
                                    Visible="False" Width="95px" /><asp:Button ID="BtnCancelaPOP" 
                                    runat="server" CssClass="buttonPlan" Height="21px" 
                                    onclick="BtnCancelaPOP_Click" Text="Cancelar" Width="95px" /><asp:Button 
                                    ID="btnPreg5" runat="server" CssClass="buttonPlan" onclick="btnPreg5_Click" 
                                    Text="|&lt;&lt;" Visible="False" /><asp:Button ID="btnAreg5" runat="server" 
                                    CssClass="buttonPlan" onclick="btnAreg5_Click" Text="&lt;&lt;" 
                                    Visible="False" /><asp:Button ID="btnSreg5" runat="server" 
                                    CssClass="buttonPlan" onclick="btnSreg5_Click" Text="&gt;&gt;" 
                                    Visible="False" /><asp:Button ID="btnUreg5" runat="server" 
                                    CssClass="buttonPlan" onclick="btnUreg5_Click" Text="&gt;&gt;|" 
                                    Visible="False" /></td></tr></table></td></tr></table>
                                    
                                    <asp:Panel ID="BuscarMPOP" runat="server" CssClass="busqueda" 
                            DefaultButton="btnBuscarMPOP"  style="display:none" Height="202px" 
                            Width="343px"><br /><br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160; <asp:Label ID="LbltitBMPOP" runat="server" CssClass="labelsTit2"
                                Text="Buscar Material POP" /><br /><br /><br /><br /><br /><br /><table align="center"><tr><td><asp:Label ID="LblSelMPOP" runat="server" CssClass="labels" 
                                            Text="Material POP:" /></td><td><asp:TextBox ID="TxtSMPOP" runat="server" MaxLength="50" Width="200px"></asp:TextBox><asp:RegularExpressionValidator ID="ReqSMPOP" runat="server" 
                                            ControlToValidate="TxtSMPOP" Display="None" 
                                            ErrorMessage="No ingrese caracteres especiales &lt;br /&gt; No debe iniciar con cero ni espacio en blanco" 
                                            ValidationExpression="([a-zA-Z][{a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]{0,50})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender ID="VCEReqMPOP" runat="server" Enabled="True" 
                                            TargetControlID="ReqSMPOP"></cc1:ValidatorCalloutExtender></td></tr></table><br /><br /><br /><br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160; <asp:Button ID="btnBuscarMPOP" runat="server" CssClass="buttonPlan" 
                                onclick="btnBuscarMPOP_Click" Text="Buscar" Width="80px" /><asp:Button ID="btnCancelarMPOP" runat="server" CssClass="buttonPlan" 
                                Text="Cancelar" Width="80px" /></asp:Panel><cc1:ModalPopupExtender ID="IbtnMPOP_ModalPopupExtender" runat="server" 
                            BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" 
                            OkControlID="btnCancelarMPOP" PopupControlID="BuscarMPOP" 
                            TargetControlID="BtnConsultaPOP" DynamicServicePath=""></cc1:ModalPopupExtender></ContentTemplate></cc1:TabPanel>

                    <cc1:TabPanel runat="server" HeaderText="Gestión Servicio " ID="Panel_Servicio"><HeaderTemplate>Servicio</HeaderTemplate><ContentTemplate><br /><br /><br /><br /><table align="center"></table><br /><br /><br /><br /><table align="center"><tr><td><fieldset 
                        ID="FSServicios" runat="server"><legend style="">Servicios</legend><br /><br /><table 
                        align="center"><tr><td><asp:Label ID="LblCodServ" runat="server" 
                            CssClass="labels" Text="Código de Servicio "></asp:Label></td><td><asp:TextBox 
                                ID="TxtCodServ" runat="server" BackColor="#DDDDDD" Enabled="False" 
                                ReadOnly="True"></asp:TextBox></td></tr><tr><td><asp:Label 
                            ID="LblNomServ" runat="server" CssClass="labels" Text="Nombre de Servicio * "></asp:Label></td><td><asp:TextBox 
                                ID="TxtNomServ" runat="server" Enabled="False" MaxLength="50" Width="332px"></asp:TextBox><asp:RegularExpressionValidator 
                                ID="ReqNomserv" runat="server" ControlToValidate="TxtNomServ" Display="None" 
                                ErrorMessage="El Nombre del Servicio no debe empezar por espacio en blanco, &lt;br /&gt; no debe ser numérico ni poseer caracteres especiales" 
                                ValidationExpression="([a-z-A-Z][\w\sñÑáéíóúÁÉÍÓÚ./]{0,49})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender 
                                ID="VENomReq" runat="server" Enabled="True" TargetControlID="ReqNomserv"></cc1:ValidatorCalloutExtender></td></tr><tr><td 
                            valign="top"><asp:Label ID="LblDesServ" runat="server" CssClass="labels" 
                            Text="Descripción * "></asp:Label></td><td><asp:TextBox ID="TxtDescServ" 
                                runat="server" Enabled="False" Height="58px" MaxLength="255" 
                                TextMode="MultiLine" Width="332px"></asp:TextBox><asp:RegularExpressionValidator 
                                ID="ReqDescServ" runat="server" ControlToValidate="TxtDescServ" Display="None" 
                                ErrorMessage="No debe comenzar por número ni espacio en blanco y &lt;br /&gt; No ingrese caracteres especiales y no exceda 255 caracteres" 
                                ValidationExpression="([a-zA-Z][\w\sñÑáéíóúÁÉÍÓÚ0-9.,;/]{0,254})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender 
                                ID="VEDescServ" runat="server" Enabled="True" TargetControlID="ReqDescServ"></cc1:ValidatorCalloutExtender></td></tr><tr><td><asp:Label 
                            ID="LblPais0" runat="server" CssClass="labels" Text="País *"></asp:Label></td><td><asp:DropDownList 
                                ID="cmbcontryServ" runat="server" Enabled="False"></asp:DropDownList></td></tr><caption><br /><br /></caption></table></fieldset> </td></tr></table><br /><br /><br /><br /><table align="center"><tr><td><asp:Label 
                        ID="LblTitServ" runat="server" CssClass="labelsTit2" 
                        Text="Administración de Servicios"></asp:Label></td></tr><tr><td><asp:Label 
                            ID="TitEstadoServ" runat="server" CssClass="labels" Text="Estado"></asp:Label></td><td><asp:RadioButtonList 
                                ID="RBtnListStatusServ" runat="server" Enabled="False" Font-Bold="True" 
                                Font-Names="Arial" Font-Size="10pt" ForeColor="Black"><asp:ListItem 
                                Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr></table><br /><br /><br /><br /><table align="center"><tr><td><asp:Button 
                            ID="btnCrearServ" runat="server" CssClass="buttonPlan" 
                            onclick="btnCrearServ_Click" Text="Crear" Width="95px" /><asp:Button 
                            ID="btnsaveServ" runat="server" CssClass="buttonPlan" 
                            onclick="btnsaveServ_Click" Text="Guardar" Visible="False" Width="95px" /><asp:Button 
                            ID="btnConsultarServ" runat="server" CssClass="buttonPlan" Text="Consultar" 
                            Width="95px" /><asp:Button ID="btnEditServicios" runat="server" 
                            CssClass="buttonPlan" onclick="btnEditServicios_Click" Text="Editar" 
                            Visible="False" Width="95px" /><asp:Button ID="btnActualizarServ" 
                            runat="server" CssClass="buttonPlan" onclick="btnActualizarServ_Click" 
                            Text="Actualizar" Visible="False" Width="95px" /><asp:Button 
                            ID="btnCancelServ" runat="server" CssClass="buttonPlan" 
                            onclick="btnCancelServ_Click" Text="Cancelar" Width="95px" /><asp:Button 
                            ID="btnPreg3" runat="server" CssClass="buttonPlan" onclick="btnPreg3_Click" 
                            Text="|&lt;&lt;" Visible="False" /><asp:Button ID="btnAreg3" runat="server" 
                            CssClass="buttonPlan" onclick="btnAreg3_Click" Text="&lt;&lt;" 
                            Visible="False" /><asp:Button ID="btnSreg3" runat="server" 
                            CssClass="buttonPlan" onclick="btnSreg3_Click" Text="&gt;&gt;" 
                            Visible="False" /><asp:Button ID="btnUreg3" runat="server" 
                            CssClass="buttonPlan" onclick="btnUreg3_Click" Text="&gt;&gt;|" 
                            Visible="False" /></td></tr></table><asp:Panel ID="BuscarServicio" runat="server" CssClass="busqueda"  style=" display:none;"
                            DefaultButton="BtnBServicio" Height="202px"  Width="343px"><asp:Label ID="LblBuscarServicio" runat="server" CssClass="labelsTit2"
                                Text="Buscar Servicio" /><br /><br /><br /><br /><br /><br /><table align="center"><tr><td><asp:Label ID="LblBServicio" runat="server" CssClass="labels" 
                                            Text="Nombre de Servicio:" /></td><td><asp:TextBox ID="TxtBServicio" runat="server" MaxLength="50" Width="180px"></asp:TextBox><asp:RegularExpressionValidator ID="ReqBServicio" runat="server" 
                                            ControlToValidate="TxtBServicio" Display="None" 
                                            ErrorMessage="El Nombre del Servicio no debe empezar por espacio en blanco, &lt;br /&gt; no debe ser numérico ni poseer caracteres especiales" 
                                            ValidationExpression="([a-z-A-Z][\w\sñÑáéíóúÁÉÍÓÚ./]{0,49})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender ID="VCEReqBServicio" runat="server" 
                                            Enabled="True" TargetControlID="ReqBServicio"></cc1:ValidatorCalloutExtender></td></tr><tr><td><asp:Label ID="LblBPais" runat="server" CssClass="labels" Text="País:" /></td><td><asp:DropDownList ID="cmbBPais" runat="server" Width="185px"></asp:DropDownList></td></tr></table><br /><br /><br /><br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160; <asp:Button ID="BtnBServicio" runat="server" CssClass="buttonPlan" 
                                onclick="BtnBServicio_Click" Text="Buscar" Width="80px" /><asp:Button ID="BtnCancelarServicio" runat="server" CssClass="buttonPlan" 
                                Text="Cancelar" Width="80px" /></asp:Panel><cc1:ModalPopupExtender ID="IbtnServicio_ModalPopupExtender" runat="server" 
                            BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" 
                            OkControlID="BtnCancelarServicio" PopupControlID="BuscarServicio" 
                            TargetControlID="btnConsultarServ" DynamicServicePath=""></cc1:ModalPopupExtender></ContentTemplate></cc1:TabPanel>

                    <cc1:TabPanel runat="server" HeaderText="Oficina" ID="Panel_Oficinas"><HeaderTemplate>Oficina</HeaderTemplate><ContentTemplate><br /><br /><table align="center"><tr><td><asp:Label ID="LblTitAdminBrand" runat="server" CssClass="labelsTit2" Text="Gestión de Oficinas"></asp:Label></td></tr></table><br /><br /><br /><br /><br /><br /><table align="center"><tr><td><br /><br /><fieldset 
                        ID="FlOficina" runat="server"><legend style="">Oficinas</legend><br /><br /><table 
                        align="center"><tr><td><asp:Label ID="LblCodOficina" runat="server" 
                            CssClass="labels" Text="Código*"></asp:Label></td><td><asp:TextBox 
                                ID="TxtCodOficina" runat="server" BackColor="#DDDDDD" Enabled="False" 
                                ReadOnly="True" Width="80px"></asp:TextBox></td></tr><tr><td><asp:Label 
                            ID="LabelClienteO" runat="server" CssClass="labels" Text="Cliente*"></asp:Label></td><td><asp:DropDownList 
                                ID="cmbClienteOficina" runat="server" Enabled="False" Height="21px" 
                                Width="195px"></asp:DropDownList></td></tr><tr><td><asp:Label 
                            ID="LblNomOficina" runat="server" CssClass="labels" Text="Nombre *"></asp:Label></td><td><asp:TextBox 
                                ID="TxtNomOficina" runat="server" Enabled="False" MaxLength="50" Width="190px"></asp:TextBox></td></tr><tr><td><asp:Label 
                            ID="lblAbreviatura" runat="server" CssClass="labels" Text="Abreviatura"></asp:Label></td><td><asp:TextBox 
                                ID="texAbreviatura" runat="server" Enabled="False" MaxLength="50" Width="190px"></asp:TextBox></td></tr><tr><td><asp:Label 
                            ID="LblOrden" runat="server" CssClass="labels" Text="Orden "></asp:Label></td><td><asp:TextBox 
                                ID="txtOrden" runat="server" Enabled="False" MaxLength="9" Width="190px"></asp:TextBox><asp:RegularExpressionValidator 
                                ID="ReqIdOrden" runat="server" ControlToValidate="txtOrden" Display="none" 
                                ErrorMessage="Requiere que el Orden solo contenga números" 
                                ValidationExpression="([0-9]{1,9})">
                                                                        </asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender 
                                ID="ValidatorCalloutExtender2" runat="server" Enabled="True" 
                                TargetControlID="ReqIdOrden"></cc1:ValidatorCalloutExtender></td></tr></table><br /><br /></fieldset> </td></tr></table><table align="center"><tr><td><asp:Label 
                            ID="LblOficinaStatus" runat="server" CssClass="labels" Text="Estado"></asp:Label></td><td><asp:RadioButtonList 
                                ID="RbtnOficinaStatus" runat="server" Enabled="False" Font-Bold="True" 
                                Font-Names="Arial" Font-Size="10pt" ForeColor="Black" 
                                RepeatDirection="Horizontal"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr></table><br /><br /><br /><br /><br /><br /><br /><br /><table align="center"><tr><td><asp:Button 
                            ID="BtnCrearOficina" runat="server" CssClass="buttonPlan" 
                            onclick="BtnCrearOficina_Click" Text="Crear" Width="95px" /><asp:Button 
                            ID="BtnSaveOficina" runat="server" CssClass="buttonPlan" 
                            onclick="BtnSaveOficina_Click" Text="Guardar" Visible="False" Width="95px" /><asp:Button 
                            ID="BtnConsultaOficina" runat="server" CssClass="buttonPlan" Text="Consultar" 
                            Width="95px" /><asp:Button ID="BtnEditOficina" runat="server" 
                            CssClass="buttonPlan" onclick="BtnEditOficina_Click" Text="Editar" 
                            Visible="False" Width="95px" /><asp:Button ID="BtnActualizaOficina" 
                            runat="server" CssClass="buttonPlan" onclick="BtnActualizaOficina_Click" 
                            Text="Actualizar" Visible="False" Width="95px" /><asp:Button 
                            ID="BtnCancelOficina" runat="server" CssClass="buttonPlan" 
                            onclick="BtnCancelOficina_Click" Text="Cancelar" Width="95px" /><asp:Button 
                            ID="PregOficina" runat="server" CssClass="buttonPlan" 
                            onclick="PregOficina_Click" Text="|&lt;&lt;" Visible="False" /><asp:Button 
                            ID="AregOficina" runat="server" CssClass="buttonPlan" 
                            onclick="AregOficina_Click" Text="&lt;&lt;" Visible="False" /><asp:Button 
                            ID="SregOficina" runat="server" CssClass="buttonPlan" 
                            onclick="SregOficina_Click" Text="&gt;&gt;" Visible="False" /><asp:Button 
                            ID="UregOficina" runat="server" CssClass="buttonPlan" 
                            onclick="UregOficina_Click" Text="&gt;&gt;|" Visible="False" /></td></tr></table><asp:Panel ID="BuscarOficina" runat="server" CssClass="busqueda" DefaultButton="BtnBOficina"
                                Height="202px" Width="343px" style="display:none"><br /><br /><table align="center"><tr><td><asp:Label ID="LblBOficina" runat="server" CssClass="labelsTit2" Text="Buscar Oficina" /></td></tr></table><br /><br /><table align="center"><tr><td><asp:Label ID="LblBCodOficina" runat="server" CssClass="labels" Text="Código:" /></td><td><asp:TextBox ID="TxtBCodOficina" runat="server" MaxLength="5" Width="80px"></asp:TextBox></td></tr><tr><td><asp:Label ID="LblBNomOficina" runat="server" CssClass="labels" Text="Nombre*:" /></td><td><asp:TextBox ID="TxtBNomOficina" runat="server" MaxLength="50" Width="180px"></asp:TextBox></td></tr></table><br /><br /><br /><br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160; <asp:Button ID="BtnBOficina" runat="server" CssClass="buttonPlan" Text="Buscar" 
                                    Width="80px" onclick="BtnBOficina_Click"
                                   /><asp:Button ID="BtnCancelBOficina" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" /></asp:Panel><cc1:ModalPopupExtender ID="IbtnOficina" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="BtnCancelBOficina" PopupControlID="BuscarOficina"
                                TargetControlID="BtnConsultaOficina" DynamicServicePath=""></cc1:ModalPopupExtender></ContentTemplate></cc1:TabPanel>
                    
                    <cc1:TabPanel ID="Panel_Corporacion" runat="server" HeaderText="Corporación"><HeaderTemplate>Corporación</HeaderTemplate><ContentTemplate><br /><br /><table align="center"><tr><td><asp:Label ID="Label2" runat="server" CssClass="labelsTit2" Text="Gestión de Corporaciones"></asp:Label></td></tr></table><br /><br /><br /><br /><br /><br /><table align="center"><tr 
                        runat="server"><td runat="server"><br /><br /><fieldset ID="Fieldset2" 
                            runat="server"><legend style="">Corporaciones</legend><br /><br /><table 
                            align="center"><tr><td class="style54"><asp:Label ID="Label3" runat="server" 
                                CssClass="labels" Text="Código*"></asp:Label></td><td><asp:TextBox 
                                    ID="TxtCodCorporacion" runat="server" BackColor="#DDDDDD" Enabled="False" 
                                    ReadOnly="True" Width="80px"></asp:TextBox></td></tr><tr><td 
                                class="style54"><asp:Label ID="Label6" runat="server" CssClass="labels" 
                                Text="Nombre*"></asp:Label></td><td><asp:TextBox ID="TxtNombreCorporacion" 
                                    runat="server" Enabled="False" MaxLength="50" Width="190px"></asp:TextBox></td></tr><tr><td 
                                class="style54">&nbsp;</td><td>&nbsp;</td></tr></table><br /><br /></fieldset> </td></tr></table><br /><br /><asp:Panel ID="PanelGvCorporacion1" runat="server" Style="display: block"  ><div class="centrar centrarcontenido" style="padding-left:40px"><div class="p" style="width:922px; height: 315px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
	                                        font-family : arial, Helvetica, sans-serif;"><asp:GridView ID="GVCorporacion1" runat="server" AutoGenerateColumns="False"  
                                                Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                                Width="67%" onpageindexchanging="GVCorporacion1_PageIndexChanging" 
                                                onrowcancelingedit="GVCorporacion1_RowCancelingEdit" 
                                                onrowediting="GVCorporacion1_RowEditing" onrowupdating="GVCorporacion1_RowUpdating" 
                                                ><Columns><asp:TemplateField HeaderText="Cod.Corporación"><EditItemTemplate><asp:Label ID="LblCodGvCorporacion" runat="server"  Text='<%# Bind("corp_id") %>'></asp:Label></EditItemTemplate><ItemTemplate><asp:Label ID="LblCodGvCorporacion" runat="server" Text='<%# Bind("corp_id") %>'></asp:Label></ItemTemplate><ItemStyle Width="70px" /></asp:TemplateField><asp:TemplateField HeaderText="Nombre"><EditItemTemplate><asp:TextBox ID="TxtNomGvCorporacion" runat="server" Width="150px" >       
                                                            </asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="LblNomGvCorporacion" runat="server"  Width="150px" Text='<%# Bind("corp_name") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Estado" ><EditItemTemplate><asp:CheckBox ID="CheckGvCorporacion" runat="server"  Enabled="true" ></asp:CheckBox></EditItemTemplate><ItemTemplate><asp:CheckBox ID="CheckGvCorporacion" runat="server"  Enabled="false" Checked ='<%# Bind("corp_status") %>'  ></asp:CheckBox></ItemTemplate></asp:TemplateField><asp:CommandField ShowEditButton="True" /></Columns></asp:GridView><br /><div class="centrar"><div class="centrar centrarcontenido"><asp:Label ID="Lblcateeexc" runat="server" CssClass="labels" Text="Exportar a Excel "></asp:Label></div><iframe id="iframeexcelCategoria" runat="server" src="pruebaexcel.aspx" frameborder="0"  width="64px" height="64px"  ></iframe><div class="centrar centrarcontenido"><asp:Button ID="btnCanCorporacion" runat="server" CssClass="buttonPlan" 
                                                            Text="Cancelar" Width="80px" onclick="btnCanCorporacion_Click"  /></div></div></div></div></asp:Panel><!-- Inicio de GridView --><cc1:ModalPopupExtender ID="MopopConsulGvCorporacion" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True"  PopupControlID="PanelGvCorporacion1"
                                TargetControlID="btnPopupGVCorporacion" DynamicServicePath=""></cc1:ModalPopupExtender><asp:Button ID="btnPopupGVCorporacion" runat="server" CssClass="alertas" Width="0px" /><table align="center"><tr><td><asp:Button ID="BtnCrearCorporacion" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" onclick="BtnCrearCorporacion_Click"   /><asp:Button ID="BtnSaveCorporacion" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" onclick="BtnSaveCorporacion_Click" /><asp:Button ID="BtnConsultaCorporacion" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" /><asp:Button ID="BtnEditCorporacion" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" /><asp:Button ID="BtnActualizaCorporacion" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px"  /><asp:Button ID="BtnCancelCorporacion" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" onclick="BtnCancelCorporacion_Click"   /><asp:Button ID="PregCorporacion" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False"  /><asp:Button ID="AregCorporacion" runat="server" CssClass="buttonPlan" 
                                            Text="&lt;&lt;" Visible="False"    /><asp:Button ID="SregCorporacion" runat="server" CssClass="buttonPlan" 
                                            Text="&gt;&gt;" Visible="False"       /><asp:Button ID="UregCorporacion" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False"  /></td></tr><tr><td><asp:Label ID="Label9" 
                                runat="server" CssClass="labels" Text="Estado"></asp:Label></td><td><asp:RadioButtonList 
                                    ID="RbtnCorporacionStatus" runat="server" Enabled="False" Font-Bold="True" 
                                    Font-Names="Arial" Font-Size="10pt" ForeColor="Black" 
                                    RepeatDirection="Horizontal"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr></table><br /><br /><br /><br /><br /><br /><br /><br /><table   align="center"></table><asp:Panel ID="BuscarCorporacion" runat="server" CssClass="busqueda" DefaultButton="BtnBCorporacion"
                                Height="202px" Width="343px" style="display:none"><br /><br /><table align="center"></table><br /><br /><table align="center"><tr><td><asp:Label 
                        ID="Label10" runat="server" CssClass="labelsTit2" Text="Buscar Corporación" /></td></tr><tr><td><asp:Label 
                            ID="Label12" runat="server" CssClass="labels" Text="Nombre*:" /></td><td><asp:TextBox 
                                ID="TxtBNombreCorporacion" runat="server" MaxLength="50" Width="180px"></asp:TextBox></td></tr></table><br /><br /><br /><br /><asp:Button ID="BtnBCorporacion" runat="server" CssClass="buttonPlan" Text="Buscar" 
                                    Width="80px" onclick="BtnBCorporacion_Click" 
                                   /><asp:Button ID="BtnBCancelarCorporacion" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" /></asp:Panel><cc1:ModalPopupExtender ID="IbtnCorporacion" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="BtnCancelCorporacion" PopupControlID="BuscarCorporacion"
                                TargetControlID="BtnConsultaCorporacion" DynamicServicePath=""></cc1:ModalPopupExtender></ContentTemplate></cc1:TabPanel>

                    <cc1:TabPanel runat="server" HeaderText="Producto Ancla" ID="panel_Asignar_Competidires"><HeaderTemplate>Asignar Competidores</HeaderTemplate><ContentTemplate><br />
                    <table align="center"><tr><td><asp:Label ID="lblclientesxCompetidores" runat="server" CssClass="labelsTit2" Text="Gestión Competidores por Cliente"></asp:Label></td></tr></table>
                    <panel  runat="server"  id="paneel" ><table align="center"><tr><td><br /><fieldset 
                        ID="Fieldset3" runat="server"><table align="center"><tr><td><asp:Label 
                            ID="lblClientes" runat="server" CssClass="labels" Text="Cliente *"></asp:Label></td><td><asp:DropDownList 
                                ID="ddlAsignacionCompetencia_Cliente" runat="server" Enabled="False" Width="255px"></asp:DropDownList></td></tr><tr><td><asp:Label 
                                ID="Label4" runat="server" CssClass="labels" Text="Competidores*"></asp:Label></td><td><asp:CheckBoxList 
                                    ID="chkAsignacionCompetencia_competidores" runat="server" Enabled="False"></asp:CheckBoxList></td></tr></table><br /><br /><br /><br /></fieldset> &nbsp; </td></tr></table><table align="center" ><tr><td><asp:Label 
                            ID="Label16" runat="server" CssClass="labels" Text="Estado"></asp:Label></td><td><asp:RadioButtonList 
                                ID="RadioButtonList1" runat="server" AutoPostBack="True" Enabled="False" 
                                Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="Black" 
                                RepeatDirection="Horizontal"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr></table><table align="center"><tr><td>
                            <asp:Button ID="btnAsignarCompetidores_Crear" runat="server" 
                                CssClass="buttonPlan" Text="Crear" Width="95px" onclick="btnAsignarCompetidores_Crear_Click"
                                              /><asp:Button ID="btnAsignarCompetidores_Guardar" 
                                runat="server" Width="95px"
                                                CssClass="buttonPlan" Text="Guardar" Visible="False" onclick="btnAsignarCompetidores_Guardar_Click" 
                                               /><asp:Button
                                                    ID="btnAsignarCompetidores_Consultar" runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px"
                                                     />
                            <asp:Button ID="btnAsignarCompetidores_Cancelar"
                                                                runat="server" CssClass="buttonPlan" Text="Cancelar" 
                                            Width="95px" onclick="btnAsignarCompetidores_Cancelar_Click"  
                                                                 /></td></tr></table></panel>
                                                                 
                                </ContentTemplate>
                                
                                </cc1:TabPanel>

                    <cc1:TabPanel runat="server" HeaderText="Producto Ancla" ID="panel_TipoMPOP"><HeaderTemplate>Gestion de Tipo de Material POP</HeaderTemplate><ContentTemplate><br />
                    <table align="center"><tr><td><asp:Label ID="Label7" runat="server" CssClass="labelsTit2" Text="Gestion de Tipo de Material POP"></asp:Label></td></tr></table>
                    <panel  runat="server"  id="paneel_1" ><table align="center"><tr><td><br /><fieldset 
                        ID="Fieldset5" runat="server"><table align="center"><tr><td><asp:Label 
                            ID="Label8" runat="server" CssClass="labels" Text="Nombre del Tipo Material POP *"></asp:Label></td><td>
                            <asp:TextBox runat="server" ID="txtNombre_tipoMaterialPOP"  Enabled="false"></asp:TextBox>
                            </td></tr></table><br /></fieldset> &nbsp; </td></tr></table><table align="center" ><tr><td><asp:Label 
                            ID="Label13" runat="server" CssClass="labels" Text="Estado"></asp:Label></td><td><asp:RadioButtonList 
                                ID="rblEstado_tipoMaterialPOP" runat="server" AutoPostBack="True" Enabled="False" 
                                Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="Black" 
                                RepeatDirection="Horizontal"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr></table><table align="center"><tr><td>
                            <asp:Button ID="btnCrear_TipoMaterialPOP" runat="server" 
                                CssClass="buttonPlan" Text="Crear" Width="95px" onclick="btnCrear_TipoMaterialPOP_Crear_Click"
                                              />
                          <asp:Button ID="btnGuardar_TipoMaterialPOP" runat="server" Width="95px"
                           CssClass="buttonPlan" Text="Guardar" Visible="False" onclick="btnGuardar_TipoMaterialPOP_Click" />
                           <asp:Button ID="btnActualizar_TipoMaterialPOP" runat="server" Width="95px"
                           CssClass="buttonPlan" Text="Actualizar" Visible="False" onclick="btnActualizar_TipoMaterialPOP_Click" />
                                               <asp:Button
                                                    ID="btnConsultar_TipoMaterialPOP" runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px"
                                                     />
                            <asp:Button ID="btnCancelar_TipoMaterialPOP"
                                                                runat="server" CssClass="buttonPlan" Text="Cancelar" 
                                            Width="95px" onclick="btnCancelar_TipoMaterialPOP_Cancelar_Click"  
                                                                 /></td></tr></table></panel>

                                <asp:Panel ID="pTipoMaterialPOP" runat="server" CssClass="busqueda" 
                            DefaultButton="btnBuscarTipoMPOP"  style="display:none" Height="202px" 
                            Width="343px"><br /><br /> <asp:Label ID="Label11" runat="server" CssClass="labelsTit2"
                                Text="Buscar Material POP" /><table align="center"><tr><td><asp:Label ID="Label14" runat="server" CssClass="labels" 
                                            Text="Tipo de Material POP:" /></td><td><asp:TextBox ID="txtTipoMaterialPOP_Buscar" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                            
                                            </td></tr></table><br /><br /><br /><br /> 
                                            
                                            <asp:Button ID="btnBuscarTipoMPOP" runat="server" CssClass="buttonPlan" 
                                onclick="btnBuscarTipoMPOP_Click" Text="Buscar" Width="80px" /><asp:Button ID="btnCancelarTipoMPOP" runat="server" CssClass="buttonPlan" 
                                Text="Cancelar" Width="80px" />
                                </asp:Panel>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                            BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" 
                            OkControlID="btnCancelarTipoMPOP" PopupControlID="pTipoMaterialPOP" 
                            TargetControlID="btnConsultar_TipoMaterialPOP" DynamicServicePath="">
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
            
            <asp:Button ID="Btndisparaalertas" runat="server" CssClass="alertas" Text="" Visible="true" Width="0px" />
        </ContentTemplate>   
    </asp:UpdatePanel>
    </form>
</body>
</html>
