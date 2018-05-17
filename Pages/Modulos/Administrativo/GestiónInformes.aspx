<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestiónInformes.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Administrativo.GestiónInformes" %>
    <%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión Informes</title>
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
        .style52
        {
            width: 91px;
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
                <cc1:TabContainer ID="TabAdministradorInformes" runat="server" ActiveTabIndex="2"
                    Width="100%" Height="460px" Font-Names="Verdana" BorderColor="#003300" 
                    style="margin-top: 0px">
                    <cc1:TabPanel runat="server" HeaderText="Gestión Informes " ID="Panel_informe"><HeaderTemplate>Reportes</HeaderTemplate><ContentTemplate><br /><table align="center"><tr><td><asp:Label ID="LblTitReport" runat="server" Text="Gestión Reportes"></asp:Label></td></tr><caption><br /></caption></table><table align="center"><tr><td><br /><fieldset id="Fieldset1" runat="server"><legend>Reportes</legend><br /><br /><table align="center"><tr><td><asp:Label ID="LblCodReport" runat="server" Text="Código del Reporte"></asp:Label></td><td><asp:TextBox ID="TxtCodReport" runat="server" BackColor="#DDDDDD" ReadOnly="True"
                                                            Width="80px" Enabled="False"></asp:TextBox></td></tr><tr><td><asp:Label ID="LblNomReport" runat="server" Text="Nombre del Reporte * "></asp:Label></td><td><asp:TextBox ID="txtNomReport" runat="server" Enabled="False" ></asp:TextBox></td></tr><tr><td><asp:Label ID="Lblordenr" runat="server" Text="orden reporte * "  
                                                            Visible="False"></asp:Label></td><td><asp:TextBox ID="txtOrderReport" runat="server"  Enabled="False" 
                                                            Visible="False"></asp:TextBox></td></tr><tr><td><asp:Label ID="LblDescReport" runat="server" Text="Descripción del Reporte * "></asp:Label></td><td><asp:TextBox ID="TxtDescReport" runat="server" Height="38px" MaxLength="255" TextMode="MultiLine"
                                                            Width="290px" Enabled="False"></asp:TextBox><asp:RegularExpressionValidator ID="ReqDescReport" runat="server" ControlToValidate="TxtDescReport"
                                                            Display="None" ErrorMessage="No debe comenzar por número ni espacio en blanco &lt;br /&gt; No ingrese caracteres especiales y no exceda 255 caracteres"
                                                            ValidationExpression="([a-zA-Z][\w\sñÑáéíóúÁÉÍÓÚ0-9.,;]{0,254})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender29" runat="server" Enabled="True"
                                                            TargetControlID="ReqDescReport"></cc1:ValidatorCalloutExtender></td></tr></table><table align="center"></table></fieldset> </td></tr></table><br /><br /><table align="center"><tr><td><asp:Label ID="TitEstadoReport" runat="server" Text="Estado"></asp:Label></td><td><asp:RadioButtonList ID="RBtnListStatusReport" runat="server" RepeatDirection="Horizontal"
                                            Enabled="False"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr></table><br /><br /><table align="center"><tr><td><asp:Button ID="btnCrearReporte" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" OnClick="btnCrearReporte_Click" /><asp:Button ID="BtnGuardarReporte" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnGuardarReporte_Click" /><asp:Button ID="btnConsultarReporte" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" /><asp:Button ID="btnEditReport" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" OnClick="btnEditReport_Click" /><asp:Button ID="btnActualizarReporte" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="btnActualizarReporte_Click" /><asp:Button ID="btnCancelarReporte" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="btnCancelarReporte_Click" /><asp:Button ID="btnPreg9" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;" Visible="False"
                                            OnClick="btnPreg9_Click" /><asp:Button ID="btnAreg9" runat="server" CssClass="buttonPlan" Text="&lt;&lt;" Visible="False"
                                            OnClick="btnAreg9_Click" /><asp:Button ID="btnSreg9" runat="server" CssClass="buttonPlan" Text="&gt;&gt;" Visible="False"
                                            OnClick="btnSreg9_Click" /><asp:Button ID="btnUreg9" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|" Visible="False"
                                            OnClick="btnUreg9_Click" /></td></tr></table><asp:Panel ID="BuscarReporte" runat="server" CssClass="busqueda" Style="display: none;"
                                DefaultButton="BtnBReportes" Height="202px" Width="343px">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160; &#160;&#160;&#160;&#160; <asp:Label ID="LblBuscarReporte" runat="server" CssClass="labelsTit2" Text="Buscar Informe" /><br /><br /><br /><table align="center"><tr><td><asp:Label ID="LblnameReporte" runat="server" CssClass="labels" Text="Informe:" /></td><td><asp:DropDownList ID="CmbNameReporte" runat="server" Width="200px"></asp:DropDownList></td></tr></table><br /><br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160; <asp:Button ID="BtnBReportes" runat="server" CssClass="buttonPlan" OnClick="BtnBReportes_Click"
                                    Text="Buscar" Width="80px" /><asp:Button ID="BtnCancelarReportes" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" /></asp:Panel><cc1:ModalPopupExtender ID="IbtnReportes_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="BtnCancelarReportes" PopupControlID="BuscarReporte"
                                TargetControlID="btnConsultarReporte" DynamicServicePath=""></cc1:ModalPopupExtender></ContentTemplate></cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Gestión Reporte para Servicio" ID="Panel_info_Servicio"><HeaderTemplate>Repor. para Servicio</HeaderTemplate><ContentTemplate><br /><table align="center"><tr><td><asp:Label ID="LblTitRpvsEs" runat="server" Text="Gestión de Reportes para Servicio"></asp:Label></td></tr></table><br /><br /><br /><table align="center"><tr><td><fieldset id="Fieldset12" runat="server"><legend>Asociación de Reportes </legend><br /><table align="center"><tr><td><asp:Label ID="LblCodRpvsEs" runat="server" Text="Código Asociación"></asp:Label></td><td><asp:TextBox ID="TxtAsociaRpvsEs" runat="server" BackColor="#DDDDDD" Enabled="False"
                                                            ReadOnly="True" Width="70px"></asp:TextBox></td></tr><tr><td><asp:Label ID="LblSelPaisSer" runat="server" Text="País * "></asp:Label></td><td><asp:DropDownList ID="CmbSelPaisSer" runat="server" AutoPostBack="True" Width="235px"
                                                            Enabled="False" OnSelectedIndexChanged="CmbSelPaisSer_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblSelServicio" runat="server" Text="Servicio * "></asp:Label></td><td><asp:DropDownList ID="cmbSelServicio" runat="server" Width="235px" 
                                                            Enabled="False" AutoPostBack="True"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblSelRep" runat="server" Text="Reporte * "></asp:Label></td><td><asp:DropDownList ID="cmbSelReporte" runat="server" AutoPostBack="True" Width="235px"
                                                            Enabled="False"></asp:DropDownList></td></tr></table><br /></fieldset> </td></tr></table><br /><br /><br /><table align="center"><tr><td><asp:Label ID="LblEstadoAsociarRpvsEs" runat="server" Text="Estado"></asp:Label></td><td><asp:RadioButtonList ID="RBtnListStatusAsociarRpvsEs" runat="server" RepeatDirection="Horizontal"
                                            Enabled="False"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr></table><br /><br /><br /><table align="center"><tr><td><asp:Button ID="BtnCrearARpvsEs" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" OnClick="BtnCrearARpvsEs_Click" /><asp:Button ID="BtnSaveARpvsEs" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnSaveARpvsEs_Click" /><asp:Button ID="BtnConsultaARpvsEs" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" /><asp:Button ID="btnEditRepVSSer" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" OnClick="btnEditRepVSSer_Click" /><asp:Button ID="BtnActualizaARpvsEs" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="BtnActualizaARpvsEs_Click" /><asp:Button ID="BtnCancelARpvsEs" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtnCancelARpvsEs_Click" /><asp:Button ID="btnPreg10" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" OnClick="btnPreg10_Click" /><asp:Button ID="btnAreg10" runat="server" CssClass="buttonPlan" Text="&lt;&lt;" Visible="False"
                                            OnClick="btnAreg10_Click" /><asp:Button ID="btnSreg10" runat="server" CssClass="buttonPlan" Text="&gt;&gt;" Visible="False"
                                            OnClick="btnSreg10_Click" /><asp:Button ID="btnUreg10" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False" OnClick="btnUreg10_Click" /></td></tr></table><asp:Panel ID="BuscarSerVsrepor" runat="server" CssClass="busqueda" Style="display: none"
                                DefaultButton="btnBuscarRepxser" Height="202px" Width="343px">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160; <asp:Label ID="LbltitBServsReport" runat="server" CssClass="labelsTit2" Text="Buscar Informe Vs Servicio" /><br /><br /><table align="center"><tr><td><asp:Label ID="LblBSelPaisSvsInf" runat="server" CssClass="labels" Text="País: "></asp:Label></td><td><asp:DropDownList ID="CmbBSelPaisSvsInf" runat="server" Width="205px" AutoPostBack="True"
                                                OnSelectedIndexChanged="CmbBSelPaisSvsInf_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblSelSer" runat="server" CssClass="labels" Text="Servicio: "></asp:Label></td><td><asp:DropDownList ID="cmbSelSer" runat="server" Width="205px" 
                                                onselectedindexchanged="cmbSelSer_SelectedIndexChanged" 
                                                AutoPostBack="True"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblSnomRep" runat="server" CssClass="labels" Text="Informe:" /></td><td><asp:DropDownList ID="cmbSnomRep" runat="server" Width="205px"></asp:DropDownList></td></tr></table><br /><br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160; <asp:Button ID="btnBuscarRepxser" runat="server" CssClass="buttonPlan" Text="Buscar"
                                    Width="80px" OnClick="btnBuscarRepxser_Click" /><asp:Button ID="btnCancelarRepxser" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" /></asp:Panel><cc1:ModalPopupExtender ID="IbtnSerVsrepor_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="btnCancelarRepxser" PopupControlID="BuscarSerVsrepor"
                                TargetControlID="BtnConsultaARpvsEs" DynamicServicePath=""></cc1:ModalPopupExtender></ContentTemplate></cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Gestión Reportes VS Canal" ID="Tab_Report_Channel"><HeaderTemplate>Repor. para Canal</HeaderTemplate><ContentTemplate><br /><table align="center"><tr><td><asp:Label ID="LblReportChannel" runat="server" Text="Gestión de Reportes a Canal"></asp:Label></td></tr></table><br /><br /><br /><table align="center"><tr><td><fieldset id="Fieldset2" runat="server"><legend>Asociación Reportes a Canal </legend><br /><table align="center"><tr><td><asp:Label ID="LblCodARC" runat="server" Text="Codigo*" Visible="False"></asp:Label></td><td><asp:TextBox ID="TxtCodAC" runat="server" BackColor="#DDDDDD" Enabled="False" ReadOnly="True"
                                                            Width="70px" Visible="False"></asp:TextBox></td></tr><tr><td><asp:Label ID="Label6" runat="server" Text="Cliente*"></asp:Label></td><td><asp:DropDownList ID="CmbCienteRC" runat="server" AutoPostBack="True" Width="235px"
                                                            Enabled="False" OnSelectedIndexChanged="CmbCienteRC_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblChannel" runat="server" Text="Canal* "></asp:Label></td><td><asp:DropDownList ID="cmbCanalRC" runat="server" AutoPostBack="True" Width="235px"
                                                            Enabled="False"></asp:DropDownList></td></tr><tr>
                            <td style="margin-left: 80px"><asp:Label ID="lblReportRC" runat="server" Text="Reporte* "></asp:Label></td><td   >
                            <asp:DropDownList ID="cmbReportesRC" runat="server" Width="235px" 
                                Enabled="False" AutoPostBack="True" 
                                onselectedindexchanged="cmbReportesRC_SelectedIndexChanged"></asp:DropDownList></td></tr>
                        <tr>
                            <td style="margin-left: 80px">
                                <asp:Label ID="lblReportRC0" runat="server" Text="Alias* "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlias" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 80px">
                                <asp:Label ID="lblReportRC1" runat="server" Text="Tipo Reporte "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmb_TipoReporte" runat="server" Enabled="False" 
                                    Width="235px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        </table><br /></fieldset> </td></tr></table><br /><br /><br /><table align="center"><tr><td><asp:Label ID="LblEstadoRC" runat="server" Text="Estado"></asp:Label></td><td><asp:RadioButtonList ID="RBTEsatadoRC" runat="server" RepeatDirection="Horizontal"
                                            Enabled="False"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr></table><br /><br /><br /><table align="center"><tr><td><asp:Button ID="BtnCrearRC" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px"
                                            OnClick="BtnCrearRC_Click" /><asp:Button ID="BtnGuardarRC" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnGuardarRC_Click" /><asp:Button ID="BtnConsultarRC" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" /><asp:Button ID="BtnEditarRC" runat="server" CssClass="buttonPlan" Text="Editar" Visible="False"
                                            Width="95px" OnClick="BtnEditarRC_Click" /><asp:Button ID="BtnActualizarRC" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="BtnActualizarRC_Click" /><asp:Button ID="BtnCancelar" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtnCancelar_Click" /><asp:Button ID="BtnPriRC" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;" Visible="False" /><asp:Button ID="BtnAnRC" runat="server" CssClass="buttonPlan" Text="&lt;&lt;" Visible="False" /><asp:Button ID="BtnSigRC" runat="server" CssClass="buttonPlan" Text="&gt;&gt;" Visible="False" /><asp:Button ID="BtnUlRC" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|" Visible="False" /></td></tr></table><asp:Panel ID="BuscarRC" runat="server" CssClass="busqueda" Style="display: none;"
                                DefaultButton="BtnBuscarRC" Height="202px" Width="343px">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160; <asp:Label ID="LblTutuloRC" runat="server" CssClass="labelsTit2" Text="Buscar Reporte VS Canal" /><br /><br /><table align="center"><tr><td><asp:Label ID="LblBCliente" runat="server" CssClass="labels" Text="Cliente: "></asp:Label></td><td><asp:DropDownList ID="cmbBuscarClienteRC" runat="server" Width="205px" AutoPostBack="True"
                                                OnSelectedIndexChanged="cmbBuscarClienteRC_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="lblBuscarCanalRC" runat="server" CssClass="labels" Text="Canal: "></asp:Label></td><td><asp:DropDownList ID="cmbBuscarCanal" runat="server" Width="205px" AutoPostBack="True"
                                                OnSelectedIndexChanged="cmbBuscarCanal_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblBRepor" runat="server" CssClass="labels" Text="Reporte:" /></td><td><asp:DropDownList ID="cmbBuscarReportRC" runat="server" Width="205px"></asp:DropDownList></td></tr></table><br /><br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160; <asp:Button ID="BtnBuscarRC" runat="server" CssClass="buttonPlan" Text="Buscar" Width="80px"
                                    OnClick="BtnBuscarRC_Click" /><asp:Button ID="BtnCancelarRC" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" /></asp:Panel><cc1:ModalPopupExtender ID="IbtnReporteChannel_ModalPopupExtender" runat="server"
                                BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" OkControlID="BtnCancelarRC"
                                PopupControlID="BuscarRC" TargetControlID="BtnConsultarRC" DynamicServicePath=""></cc1:ModalPopupExtender></ContentTemplate></cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Asignación. Reporte a Usuario" ID="PanelUsuariovsInforme"><HeaderTemplate>Asig.Reporte a Usuario</HeaderTemplate><ContentTemplate><br /><table align="center"><tr><td><asp:Label ID="Label1" runat="server" Text="Asignación de Reporte a Usuario"></asp:Label></td></tr></table><br /><table align="center"><tr><td><fieldset id="Fieldset3" runat="server"><legend>Asignación de Reporte a Usuario </legend><br /><div><table align="center"><tr><td><asp:Label ID="LblCliente" runat="server" Text="Cliente* "></asp:Label></td><td><asp:DropDownList ID="CmbClienteUI" runat="server" Enabled="False" Height="20px"
                                                                Width="300px" AutoPostBack="True" OnSelectedIndexChanged="CmbClienteUI_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="Label5" runat="server" Text="Usuario* "></asp:Label></td><td><asp:DropDownList ID="cmbUsuarioInfo" runat="server" AutoPostBack="True" Enabled="False"
                                                                Height="20px" OnSelectedIndexChanged="cmbUsuarioInfo_SelectedIndexChanged" Width="300px"></asp:DropDownList></td></tr></table></div><br /><table style="border: 1; border-style: solid;"><tr><td  valign ="top"><table align="center" width="280px"><tr><td><asp:Label ID="Label2" runat="server" Text="Código*" Visible="False"></asp:Label></td><td><asp:TextBox ID="TextCodUI" runat="server" AutoCompleteType="Disabled" BackColor="#DDDDDD"
                                                                        Enabled="False" Height="15px" Width="174px" Visible="False"></asp:TextBox></td></tr><tr><td><asp:Label ID="lblClienteacceder" runat="server" Text="Acceder a:* "></asp:Label></td><td><asp:DropDownList ID="cmbClienteAcceder" runat="server" Enabled="False" Height="20px"
                                                                Width="180px" AutoPostBack="True" 
                                                                onselectedindexchanged="cmbClienteAcceder_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td >Canal* </td><td><asp:DropDownList ID="cmbCanalUsu" runat="server" Enabled="False" Height="21px" 
                                                                        Width="180px" AutoPostBack="True" 
                                                                        onselectedindexchanged="cmbCanalUsu_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td >SubCanal </td><td><asp:DropDownList ID="cmbSubCanalUsu" runat="server" Enabled="False" Height="21px" 
                                                                        Width="180px" AutoPostBack="True" 
                                                                        onselectedindexchanged="cmbSubCanalUsu_SelectedIndexChanged"
                                                                        ></asp:DropDownList></td></tr><tr><td >SubNivel </td><td><asp:DropDownList ID="cmbSubNivel" runat="server" Enabled="False" Height="21px" 
                                                                        Width="180px" 
                                                                        ></asp:DropDownList></td></tr><tr><td >Servicio* </td><td><asp:DropDownList ID="cmbServicioUsu" runat="server" AutoPostBack="True" Enabled="False"
                                                                        Height="21px" OnSelectedIndexChanged="cmbServicioUsu_SelectedIndexChanged" Style="margin-bottom: 0px"
                                                                        Width="180px"></asp:DropDownList></td></tr></table></td><td><table align="center"  border="1"><tr valign="top"><td><asp:Label ID="Informe" runat="server" Text="Reporte*"></asp:Label><div class="ScrollInforme" style="width:250px;" ><asp:CheckBoxList ID="Checkinforme" runat="server" Enabled="False"></asp:CheckBoxList></div></td></tr></table></td></tr></table><br /></fieldset> </td></tr></table><table align="center"><tr><td><asp:Label ID="Label4" runat="server" Text="Estado"></asp:Label></td><td><asp:RadioButtonList ID="RadioInfoUsu" runat="server" RepeatDirection="Horizontal"
                                            Enabled="False" Height="27px"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr><caption><br /></caption></table><table align="center"><tr><td><asp:Button ID="BtnCrearUsuInfo" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" OnClick="BtnCrearUsuInfo_Click" /><asp:Button ID="BtnGuardarUsuInfo" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnGuardarUsuInfo_Click" /><asp:Button ID="BtnConsultarUsuInfo" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" /><asp:Button ID="BtnEditarUsuInfo" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" OnClick="BtnEditarUsuInfo_Click" /><asp:Button ID="BtnActuUsuInfo" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="BtnActuUsuInfo_Click" /><asp:Button ID="BtnCancelUsuInfo" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtnCancelUsuInfo_Click" /><asp:Button ID="PregUsuInfo" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" OnClick="PregUsuInfo_Click"></asp:Button><asp:Button ID="AregUsuInfo" runat="server" CssClass="buttonPlan" Text="&lt;&lt;"
                                            Visible="False" Width="24px" OnClick="AregUsuInfo_Click"></asp:Button><asp:Button ID="SregUsuInfo" runat="server" CssClass="buttonPlan" Text="&gt;&gt;"
                                            Visible="False" OnClick="SregUsuInfo_Click"></asp:Button><asp:Button ID="UregUsuInfo" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False" OnClick="UregUsuInfo_Click"></asp:Button></td></tr></table><br /><table align="center"></table><asp:Panel ID="BuscarAsignacionInfoUser" runat="server" CssClass="busqueda" DefaultButton="BtnBuscarAInformesUsers" Style="display: none;"
                                Height="202px" Width="363px"><table align="center"><tr><td><asp:Label ID="LblBAIU" runat="server" CssClass="labelsTit2" Text="Buscar Asignación de Informes a Usuario" /></td></tr></table><br /><table align="center"><tr><td><asp:Label ID="LblClienteAIU" runat="server" CssClass="labels" Text="Cliente:" /></td><td><asp:DropDownList ID="cmbClienteBUI" runat="server" Width="205px" OnSelectedIndexChanged="cmbClienteBUI_SelectedIndexChanged"
                                                AutoPostBack="True"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblUsuario" runat="server" CssClass="labels" Text="Usuario:" /></td><td><asp:DropDownList ID="cmbUsuarioBAIU" runat="server" Width="205px" OnSelectedIndexChanged="cmbUsuarioBAIU_SelectedIndexChanged"
                                                AutoPostBack="True"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblbCanal" runat="server" CssClass="labels" Text="Canal:" /></td><td><asp:DropDownList ID="cmbBCanalUI" runat="server" Width="205px" AutoPostBack="True"
                                                OnSelectedIndexChanged="cmbBCanalUI_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblServicio" runat="server" CssClass="labels" Text="Servicio:" /></td><td><asp:DropDownList ID="cmbServicioBAIU" runat="server" Width="205px"></asp:DropDownList></td></tr></table><br /><table align="center"><tr><td><asp:Button ID="BtnBuscarAInformesUsers" runat="server" CssClass="buttonPlan" Text="Buscar"
                                                Width="80px" OnClick="BtnBuscarAInformesUsers_Click" /><asp:Button ID="BtnCancelarAreportUsers" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                                Width="80px" /></td></tr></table></asp:Panel><cc1:ModalPopupExtender ID="IbtnModalPopupAsignacionReportUser" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="BtnCancelarAreportUsers" PopupControlID="BuscarAsignacionInfoUser"
                                TargetControlID="BtnConsultarUsuInfo" DynamicServicePath=""></cc1:ModalPopupExtender></ContentTemplate></cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Asig. Reporte a Oficina" ID="Tab_Reporte_Oficina"><HeaderTemplate>Asig. Reporte a Oficina</HeaderTemplate><ContentTemplate><br /><table align="center"><tr><td><asp:Label ID="LblTitulo" runat="server" Text="Asignación de Reporte a Oficina"></asp:Label></td></tr></table><br /><table align="center"><tr><td><fieldset id="Fieldset5" runat="server"><legend>Asignación de Reporte a Oficinas </legend><br /><div><table align="center"><tr><td><asp:Label ID="cmbClienteReportOf" runat="server" Text="Cliente* "></asp:Label></td><td><asp:DropDownList ID="cmbClienteRO" runat="server" Enabled="False" Height="20px"
                                                                Width="250px" AutoPostBack="True" 
                                                                onselectedindexchanged="cmbClienteRO_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="lblReportOfic" runat="server" Text="Reporte* "></asp:Label></td><td><asp:DropDownList ID="CmbReporteOficina" runat="server" Enabled="False" Height="20px"
                                                                Width="250px" AutoPostBack="True" 
                                                                onselectedindexchanged="CmbReporteOficina_SelectedIndexChanged"></asp:DropDownList></td></tr></table></div><br /><table align="center" width="70%" border="1"><tr valign="top"><td><asp:Label ID="cmbOficinas" runat="server" Text="Oficinas*"></asp:Label><div class="ScrollInforme"><asp:CheckBoxList ID="ChekROficinas" runat="server" Enabled="False"></asp:CheckBoxList></div></td></tr></table><br /></fieldset> </td></tr></table><table align="center"><tr><td><asp:Label ID="LblestadoROficina" runat="server" Text="Estado"></asp:Label></td><td><asp:RadioButtonList ID="RbtERficina" runat="server" RepeatDirection="Horizontal"
                                            Enabled="False" Height="27px"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr><caption><br /></caption></table><table align="center"><tr><td><asp:Button ID="CrearRO" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" onclick="CrearRO_Click"  /><asp:Button ID="GuardarRO" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" onclick="GuardarRO_Click" /><asp:Button ID="ConsultarRO" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" /><asp:Button ID="EditarRO" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" onclick="EditarRO_Click"  /><asp:Button ID="ActualizarRO" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" onclick="ActualizarRO_Click"  /><asp:Button ID="CancelarRO" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" onclick="CancelarRO_Click" /></td></tr></table><br /><table align="center"></table><asp:Panel ID="BuscarRO" runat="server" CssClass="busqueda" DefaultButton="BucarRO" Style="display: none;"
                                Height="202px" Width="363px"><table align="center"><br /><tr><td><asp:Label ID="lbltitulob" runat="server" CssClass="labelsTit2" Text="Buscar Asignación de Reporte a Oficinas" /></td></tr></table><table align="center"><tr><td><asp:Label ID="LblBClienteRO" runat="server" CssClass="labels" Text="Cliente:" /></td><td><asp:DropDownList ID="cmbBCliRO" runat="server" Width="205px" 
                                                AutoPostBack="True"  onselectedindexchanged="cmbBCliRO_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="lblBReportRO" runat="server" CssClass="labels" Text="Reporte:" /></td><td><asp:DropDownList ID="cmbBReporRO" runat="server" Width="205px"                                                 ></asp:DropDownList></td></tr></table><br /><table align="center"><br /><tr><td><asp:Button ID="BucarRO" runat="server" CssClass="buttonPlan" Text="Buscar"
                                                Width="80px" onclick="BucarRO_Click"/><asp:Button ID="CancelarBRO" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                                Width="80px" /></td></tr></table></asp:Panel><cc1:ModalPopupExtender ID="Popup_ReportOficina" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="CancelarBRO" PopupControlID="BuscarRO"
                                TargetControlID="ConsultarRO" DynamicServicePath=""></cc1:ModalPopupExtender></ContentTemplate></cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Asig.de oficinas" ID="TabAsignaciónCobertura"><HeaderTemplate>Asig. de Oficinas</HeaderTemplate><ContentTemplate><br /><table align="center"><tr><td><asp:Label ID="Lbltitulo1" runat="server" Text="Asignación de Oficinas"></asp:Label></td></tr></table><br /><table align="center"><tr><td><fieldset id="Fieldset4" runat="server"><legend>Asignación de Oficinas </legend><br /><table align="center"><tr><td><asp:Label ID="LblCodidClieuserR" runat="server" Text="Codigo* " 
                                                                Visible="False"></asp:Label></td><td><asp:TextBox ID="TxtcodigoClieUR" runat="server" AutoCompleteType="Disabled" BackColor="#DDDDDD"
                                                                Enabled="False" Height="15px" Width="174px" Visible="False"></asp:TextBox></td></tr><tr><td><asp:Label ID="lblclienteCity" runat="server" Text="Cliente* "></asp:Label></td><td><asp:DropDownList ID="cmbClientecity" runat="server" Enabled="False" Height="20px"
                                                                Width="310px" AutoPostBack="True" OnSelectedIndexChanged="cmbClientecity_SelectedIndexChanged"></asp:DropDownList></td></tr></table><table style="border: 1; border-style: solid;"><tr><td><asp:Label ID="LblUsuarioCity" runat="server" Text="Usuario* "></asp:Label></td><td><asp:DropDownList ID="cmbUsuarioCity" runat="server" AutoPostBack="True" Enabled="False"
                                                                Height="20px" Width="280px" OnSelectedIndexChanged="cmbUsuarioCity_SelectedIndexChanged"></asp:DropDownList></td><td><asp:Label ID="lblCanalCity" runat="server" Text="Canal* "></asp:Label></td><td><asp:DropDownList ID="cmbCanalCity" runat="server" AutoPostBack="True" Enabled="False"
                                                                Height="20px" Width="280px" 
                                                                onselectedindexchanged="cmbCanalCity_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblServicioCity" runat="server" Text="Servicio* "></asp:Label></td><td><asp:DropDownList ID="cmbServicioCity" runat="server" AutoPostBack="True" Enabled="False"
                                                                    Height="20px" Width="280px" 
                                                                    onselectedindexchanged="cmbServicioCity_SelectedIndexChanged"></asp:DropDownList></td><td><asp:Label ID="LblReportes" runat="server" Text="Reporte* "></asp:Label></td><td><asp:DropDownList ID="cmbReporteCity" runat="server" AutoPostBack="True" Enabled="False"
                                                                    Height="20px" Width="280px" OnSelectedIndexChanged="cmbReporteCity_SelectedIndexChanged"></asp:DropDownList></td></tr></table><caption><br /></caption><!-- cerrando tag de tabla --><table align="center" width="40%" border="1"><tr valign="top"><td><asp:Label ID="LblCiudad" runat="server" Text="Oficinas*"></asp:Label><div>
                        <asp:CheckBox runat="server" ID="chkTodos" Text="Seleccionar Todos" Enabled="false" 
                            AutoPostBack="True" oncheckedchanged="chkTodos_CheckedChanged" /></div><div class="ScrollInforme"><asp:CheckBoxList ID="CheckCiudades" runat="server" Enabled="False"></asp:CheckBoxList></div></td></tr></table><br /></fieldset> </td></tr></table><table align="center"><tr><td><asp:Label ID="lblEstadoCity" runat="server" Text="Estado"></asp:Label></td><td><asp:RadioButtonList ID="RBTasigCity" runat="server" RepeatDirection="Horizontal"
                                            Enabled="False" Height="27px"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr><caption><br /></caption></table><table align="center"><tr><td><asp:Button ID="btnCrearCity" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px"
                                            OnClick="btnCrearCity_Click" /><asp:Button ID="BtnGuardarcity" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnGuardarcity_Click" /><asp:Button ID="BtnConsultarCity" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" /><asp:Button ID="BtnEditarCity" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" OnClick="BtnEditarCity_Click" /><asp:Button ID="BtnActualizarCity" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="BtnActualizarCity_Click" /><asp:Button ID="BtnCancelarCity" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtnCancelarCity_Click" /></td></tr></table><br /><table align="center"></table><asp:Panel ID="BuscarCity" runat="server" CssClass="busqueda" DefaultButton="BtnBuscarCity" Style="display: none;"
                                Height="202px" Width="363px"><table align="center"><tr><td><asp:Label ID="LbltituBuscar" runat="server" CssClass="labelsTit2" Text="Buscar Asignación Oficinas" /></td></tr></table><br /><table align="center"><tr><td><asp:Label ID="LblBClienteCity" runat="server" CssClass="labels" Text="Cliente:" /></td><td><asp:DropDownList ID="cmbBCliCity" runat="server" Width="205px" 
                                                AutoPostBack="True" onselectedindexchanged="cmbBCliCity_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblBUsuCity" runat="server" CssClass="labels" Text="Usuario:" /></td><td><asp:DropDownList ID="cmbBUSUCity" runat="server" Width="205px" 
                                                AutoPostBack="True" onselectedindexchanged="cmbBUSUCity_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblBCanalCity" runat="server" CssClass="labels" Text="Canal:" /></td><td><asp:DropDownList ID="cmbBCanalCity" runat="server" Width="205px" 
                                                AutoPostBack="True" onselectedindexchanged="cmbBCanalCity_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblBServicio" runat="server" CssClass="labels" Text="Servicio:" /></td><td><asp:DropDownList ID="cmbBservicioCity" runat="server" Width="205px" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="cmbBservicioCity_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblBReporCity" runat="server" CssClass="labels" Text="Reporte:" /></td><td><asp:DropDownList ID="cmbBreport" runat="server" Width="205px"></asp:DropDownList></td></tr></table><table align="center"><tr><td><asp:Button ID="BtnBuscarCity" runat="server" CssClass="buttonPlan" Text="Buscar"
                                                Width="80px" OnClick="BtnBuscarCity_Click" /><asp:Button ID="BtnCancelAsigCity" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                                Width="80px" /></td></tr></table></asp:Panel><cc1:ModalPopupExtender ID="ModalPopupCity" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="BtnCancelAsigCity" PopupControlID="BuscarCity"
                                TargetControlID="BtnConsultarCity" DynamicServicePath=""></cc1:ModalPopupExtender></ContentTemplate></cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Gestión Reporte" ID="TabGestionReporte"><HeaderTemplate>Param. X Reporte</HeaderTemplate><ContentTemplate><br /><table align="center"><tr><td><asp:Label ID="LblGReportes" runat="server" Text="Parametros X Reporte"></asp:Label></td></tr></table><br /><table align="center"><tr><td><fieldset id="Fields6" runat="server"><legend>Parametros X Reporte</legend><br /><div><table align="center"><tr><td><asp:Label ID="lblcodigoGR" runat="server" CssClass="labels" Text="Codigo:" 
                                                     visible="False"/></td><td><asp:TextBox ID="TxtCodgoGR" runat="server" Width="205px" visible="False" Enabled="False"

                                                    AutoPostBack="True"  ></asp:TextBox></td></tr><tr><td><asp:Label ID="lblCompanyGR" runat="server" Text="Cliente* "></asp:Label></td><td><asp:DropDownList ID="cmbCompanyGR" runat="server" Enabled="False" 
                                                                Height="20px" AutoPostBack="True"
                                                                Width="250px"  onselectedindexchanged="cmbCompanyGR_SelectedIndexChanged" ></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblCanalGR" runat="server" Text="Canal* "></asp:Label></td><td><asp:DropDownList ID="CmbCanalGR" runat="server" Enabled="False" Height="20px" AutoPostBack="True"
                                                                Width="250px"   onselectedindexchanged="CmbCanalGR_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="lblReportGV" runat="server" Text="Reporte* "></asp:Label></td><td><asp:DropDownList ID="cmbReportGv" runat="server" Enabled="False" Height="20px" AutoPostBack="True"
                                                                Width="250px" onselectedindexchanged="cmbReportGv_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="lbltipoReporte" runat="server" Text="Tipo Reporte "></asp:Label></td><td><asp:DropDownList ID="CmbtipoReporte" runat="server" Enabled="False" Height="20px"
                                                                Width="250px" ></asp:DropDownList></td></tr></table></div><br />
                                                                <table align="center">
                                                                <tr>
                                                                <td><asp:CheckBox ID="chkVCategory" runat="server" ForeColor="Black" Text="Vista Categoria"
                                                            Enabled="False" />
                                                            </td>
                                                            <td><asp:CheckBox ID="chkVMarca" runat="server" ForeColor="Black" Text="Vista Marca"
                                                            Enabled="False" />
                                                            </td>
                                                            </tr>
                                                            <tr>
                                                            <td><asp:CheckBox ID="chkVSubMarca" runat="server" ForeColor="Black" Text="vista SubMarca" Enabled="False" />
                                                            </td>
                                                            <td><asp:CheckBox ID="chkVFamilia" runat="server" ForeColor="Black" Text="Vista Familia" Enabled="False" />
                                                            </td>
                                                            </tr>
                                                            <tr >
                                                            <td><asp:CheckBox ID="chkVProducto" runat="server" ForeColor="Black" Text="Vista Producto" Enabled="False" />
                                                            </td>
                                                            <td><asp:CheckBox ID="chkVSubFamilia" runat="server" ForeColor="Black" Text="Vista SubFamilia" Enabled="False" />
                                                            </td>
                                                            </tr>
                                                            </table><br /><br /></fieldset> </td></tr>
                                                            </table>
                                                            <!-- Inicio de GridView -->
                            <asp:Panel ID="Panel_GrillaParametrosXReporte" runat="server" Style="display: block"  >                        
                                        <div class="centrar centrarcontenido">                                
                                        <div class="p" style="width:780px; height: 315px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
	                                        font-family : arial, Helvetica, sans-serif;"> 
                                            <asp:GridView ID="GVGestionProdXReport" runat="server" AutoGenerateColumns="False"  
                                                Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                                Width="100%" onpageindexchanging="GVGestionProdXReport_PageIndexChanging" 
                                                onrowcancelingedit="GVGestionProdXReport_RowCancelingEdit" 
                                                onrowediting="GVGestionProdXReport_RowEditing" onrowupdating="GVGestionProdXReport_RowUpdating"  
                                                >                                               
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Codigo">
                                                        <EditItemTemplate>
                                                                <asp:Label ID="Lblid_RelInfo_prodcutos" runat="server"  Text='<%# Bind("id_RelInfo_prodcutos") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lblid_RelInfo_prodcutos" runat="server" Text='<%# Bind("id_RelInfo_prodcutos") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company_id" Visible="False">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="LblCompany_id" runat="server" Width="150px" Text='<%# Bind("Company_id") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCompany_id" runat="server" Width="150px" Text='<%# Bind("Company_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Cliente">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="LblCompany_Name" runat="server" Width="150px" Text='<%# Bind("Company_Name") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCompany_Name" runat="server" Width="150px" Text='<%# Bind("Company_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="cod_Channel" Visible="False">
                                                        <EditItemTemplate>
                                                            <<asp:Label ID="Lblcod_Channel" runat="server" Width="150px" Text='<%# Bind("cod_Channel") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                          <asp:Label ID="Lblcod_Channel" runat="server" Width="150px" Text='<%# Bind("cod_Channel") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Canal">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="LblChannel_Name" runat="server" Width="150px" Text='<%# Bind("Channel_Name") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                          <asp:Label ID="LblChannel_Name" runat="server" Width="150px" Text='<%# Bind("Channel_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Report_id" Visible="False">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="LblReport_id" runat="server" Width="150px" Text='<%# Bind("Report_id") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                           <asp:Label ID="LblReport_id" runat="server" Width="150px" Text='<%# Bind("Report_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Reporte">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="LblReport_NameReport" runat="server" Width="150px" Text='<%# Bind("Report_NameReport") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                           <asp:Label ID="LblReport_NameReport" runat="server" Width="150px" Text='<%# Bind("Report_NameReport") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                      <asp:TemplateField HeaderText="Tipo Reporte">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="LblTypeReport_Name" runat="server" Width="150px" Text='<%# Bind("TypeReport_Name") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                           <asp:Label ID="LblTypeReport_Name" runat="server" Width="150px" Text='<%# Bind("TypeReport_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vista Categoria" >
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="CheckPXRVista_Categoria" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckPXRVista_Categoria" runat="server"   Enabled="false" Checked ='<%# Bind("Vista_Categoria") %>'  ></asp:CheckBox> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vista Marca" >
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="CheckPXRVista_Marca" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckPXRVista_Marca" runat="server"  Enabled="false" Checked ='<%# Bind("Vista_Marca") %>'  ></asp:CheckBox> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Vista SubMarca" >
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="CheckPXRVista_SubMarca" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckPXRVista_SubMarca" runat="server"  Enabled="false" Checked ='<%# Bind("Vista_SubMarca") %>'  ></asp:CheckBox> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Vista Familia" >
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="CheckPXRVista_Familia" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckPXRVista_Familia" runat="server"  Enabled="false" Checked ='<%# Bind("Vista_Familia") %>'  ></asp:CheckBox> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Vista SubFamilia" >
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="CheckPXRVista_SubFamilia" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckPXRVista_SubFamilia" runat="server"  Enabled="false" Checked ='<%# Bind("Vista_SubFamilia") %>'  ></asp:CheckBox> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Vista Producto" >
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="CheckPXRVista_Producto" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckPXRVista_Producto" runat="server"  Enabled="false" Checked ='<%# Bind("Vista_Producto") %>'  ></asp:CheckBox> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Estado" >
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="CheckPXREstado" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckPXREstado" runat="server"  Enabled="false" Checked ='<%# Bind("RelInfo_Status") %>'  ></asp:CheckBox> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowEditButton="True" />
                                                </Columns>                                                
                                            </asp:GridView>
                                            <br />                                                 
                                                <div class="centrar">
                                                    <div class="centrar centrarcontenido">
                                                    <asp:Label ID="Lblcateeexc" runat="server" CssClass="labels" Text="Exportar a Excel "></asp:Label>
                                                    </div>
                                                    <iframe id="iframeexcelCategoria" runat="server" src="pruebaexcel.aspx" frameborder="0"  width="64px" height="64px"  ></iframe>
                                                    <div class="centrar centrarcontenido">                                       
                                                        <asp:Button ID="btnCanGvProdXReport" runat="server" CssClass="buttonPlan" 
                                                            Text="Cancelar" Width="80px"  />
                                                    </div> 
                                                </div>                                                               
                                            </div>
                                    </div> 
                            </asp:Panel>
                            <!-- Fin de GriedView -->
                            <cc1:ModalPopupExtender ID="MopopGrillaParametrosXReporte" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True"  PopupControlID="Panel_GrillaParametrosXReporte"
                                TargetControlID="btnPopupGVProdReport" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btnPopupGVProdReport" runat="server" CssClass="alertas" Width="0px" />
                            <!---->
                                                            

                                                            <table align="center"><tr><td><asp:Label ID="LblestadoGR" runat="server" Text="Estado"></asp:Label></td><td><asp:RadioButtonList ID="RadioButtonGR" runat="server" RepeatDirection="Horizontal"
                                            Enabled="False" Height="27px"><asp:ListItem Selected="True">Habilitado</asp:ListItem><asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList></td></tr><caption><br /></caption></table><table align="center"><tr><td><asp:Button ID="BtnCrearGR" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px"  OnClick="BtnCrearGR_Click" /><asp:Button ID="BtnGuardarGR" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnGuardarGR_Click"/><asp:Button ID="BtnConsultarGR" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px"/><asp:Button ID="BtnEditarGR" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px"  OnClick="BtnEditarGR_Click" /><asp:Button ID="BtnActualizarGR" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px"  OnClick="BtnActualizarGR_Click"  /><asp:Button ID="btneliminar" runat="server" CssClass="buttonPlan" Visible="False"
                                            OnClick="btneliminar_Click" Text="Eliminar" Width="95px" /><asp:Button ID="BtnCancelarGR" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px"  OnClick="BtnCancelarGR_Click" /><asp:Button ID="primero" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" OnClick="primero_Click" /><asp:Button ID="sig" runat="server" CssClass="buttonPlan" Text="&lt;&lt;" Visible="False"
                                            OnClick="sig_Click" /><asp:Button ID="ant" runat="server" CssClass="buttonPlan" Text="&gt;&gt;" Visible="False"
                                            OnClick="ant_Click" /><asp:Button ID="ultimo" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False" OnClick="ultimo_Click" /></td></tr></table><br /><asp:Panel ID="BuscarGR" runat="server" CssClass="busqueda" DefaultButton="BuscarGRI" Style="display: none;"
                                Height="202px" Width="363px"><table align="center"><br /><tr><td><asp:Label ID="lblBGR" runat="server" CssClass="labelsTit2" Text="Buscar Parametros X Reporte" /></td></tr></table><table align="center"><tr><td><asp:Label ID="Label12" runat="server" CssClass="labels" Text="Cliente:" /></td><td><asp:DropDownList ID="CMBBClienteRG" runat="server" Width="205px" onselectedindexchanged="CMBBClienteRG_SelectedIndexChanged"
                                                AutoPostBack="True"  ></asp:DropDownList></td></tr><tr><td><asp:Label ID="LblCanal" runat="server" CssClass="labels" Text="Canal:" /></td><td><asp:DropDownList runat="server" AutoPostBack="True" Width="205px" ID="CmbCanalBRG" OnSelectedIndexChanged="CmbCanalBRG_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><asp:Label ID="Label13" runat="server" CssClass="labels" Text="Reporte:" /></td><td><asp:DropDownList ID="CMBReporRG" runat="server" Width="205px"     ></asp:DropDownList></td></tr><br /></table><table align="center"><br /><tr><td><asp:Button ID="BuscarGRI" runat="server" CssClass="buttonPlan" Text="Buscar"
                                                Width="80px" OnClick="BuscarGRI_Click"/><asp:Button ID="CancelarGR" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                                Width="80px" /></td></tr></table></asp:Panel><cc1:ModalPopupExtender ID="ModalBuscarGR" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="CancelarGR" PopupControlID="BuscarGR"
                                TargetControlID="BtnConsultarGR" DynamicServicePath=""></cc1:ModalPopupExtender></ContentTemplate></cc1:TabPanel>
<!-- Comienzo del panel de Tipo de Reporte -->
          <cc1:TabPanel ID="Panel_TipoReporte" runat="server" Visible="false" Enabled="false">
        <HeaderTemplate ></HeaderTemplate><ContentTemplate>
        <br />
        <table align="center">
        <tr>
        <td>
        <asp:Label ID="lblTipoReportes" runat="server" Text="Tipos de Reportes"></asp:Label>
        </td>
        </tr><caption>
        <br /></caption>
        </table>
        <table align="center">
        <tr>
        <td>
        <br />
        <fieldset id="Fieldset9" runat="server">
        <legend>Tipos de Reportes</legend>
        <br /><br />
        <table align="center"><tr><td class="style52" ><asp:Label ID="lblCódigo" runat="server" Text="Código del Tipo Reporte"></asp:Label>
        </td><td><asp:TextBox ID="txtCodigoTipoReporte" runat="server" BackColor="#DDDDDD" ReadOnly="True"
                                                            Width="80px" Enabled="False"></asp:TextBox>
                                                            
                                                           </td></tr><tr><td class="style52" >
                <asp:Label ID="Label9" runat="server" Text="Nombre del tipo de Reporte* "></asp:Label>
                                                            </td><td><asp:TextBox ID="txtNombreTipoReporte" runat="server" 
                                                            Width="200px" Enabled="False"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtNombreTipoReporte" Display="None" 
                        ErrorMessage="No debe comenzar por número ni espacio en blanco &lt;br /&gt; No ingrese caracteres especiales y no exceda 255 caracteres" 
                        ValidationExpression="([a-zA-Z][\w\sñÑáéíóúÁÉÍÓÚ0-9.,;]{0,254})"></asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                        Enabled="True" TargetControlID="RegularExpressionValidator1">
                    </cc1:ValidatorCalloutExtender>
                </td></tr></table>

                  <table align="center"></table>
                  </fieldset> 
                 </td>
                </tr>
                 </table>
                 <br /><br /><table align="center"><tr><td>
                 <asp:Label ID="Label11" runat="server" Text="Estado"></asp:Label>
                     </td><td>
                <asp:RadioButtonList ID="rbtEstadoTiposReporte" runat="server" RepeatDirection="Horizontal" Enabled="False"><asp:ListItem Selected="True">Habilitado</asp:ListItem>
                <asp:ListItem>Deshabilitado</asp:ListItem></asp:RadioButtonList>
                </td></tr>
                </table><br /><br />
                <table align="center">
                <tr>
                <td>
                <asp:Button ID="BtnCrearTiposReporte" runat="server" CssClass="buttonPlan" 
                        Text="Crear" Width="95px" onclick="BtnCrearTiposReporte_Click"/>
                <asp:Button ID="BtnGuardarTiposReporte" runat="server" CssClass="buttonPlan" Text="Guardar" Visible="False" Width="95px"  />
                <asp:Button ID="BtnConsultarTiposReporte" runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px" />
                <asp:Button ID="BtnEditarTiposReporte" runat="server" CssClass="buttonPlan" 
                        Text="Editar" Visible="False" Width="95px" 
                        onclick="BtnEditarTiposReporte_Click" />
                <asp:Button ID="BtnActualizarTiposReporte" runat="server" CssClass="buttonPlan" Text="Actualizar" Visible="False" Width="95px"  />
                <asp:Button ID="BtnCancelarTiposReporte" runat="server" CssClass="buttonPlan" 
                        Text="Cancelar"  Width="95px" onclick="BtnCancelarTiposReporte_Click"  />
                </td>
                </tr>
                </table>
                <div>
                <asp:Panel ID="PanelBusqueda_TiposReporte" runat="server"  CssClass="busqueda" Style="display: none;" DefaultButton="BtnBuscarTiposReportePopup" Height="202px" Width="343px"> 
                <asp:Label ID="Label3" runat="server" CssClass="labelsTit2" Text="Buscar Tipos de Reporte" />
                <br /><br /><br />
                <table align="center"><tr><td>
                <asp:Label ID="Label14" runat="server" CssClass="labels" Text="Informe:" />
                </td>
                <td><asp:DropDownList ID="ddlTiposReporte" runat="server" Width="200px"></asp:DropDownList></td>
                </tr>
                </table><br /><br />
                <asp:Button ID="BtnBuscarTiposReportePopup" runat="server" CssClass="buttonPlan" 
                        Text="Buscar" Width="80px" onclick="BtnBuscarTiposReportePopup_Click" />
                <asp:Button ID="BtnCancelarTiposReportePopup" runat="server" CssClass="buttonPlan" Text="Cancelar"  Width="80px" />
                </asp:Panel>
                </div>
                 <cc1:ModalPopupExtender ID="mopoTiposReporte" runat="server" BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" OkControlID="BtnCancelarTiposReportePopup" PopupControlID="PanelBusqueda_TiposReporte"
                   TargetControlID="BtnConsultarTiposReporte" DynamicServicePath="">
                   </cc1:ModalPopupExtender>
                   </ContentTemplate>
                 </cc1:TabPanel>
               <!-- Fin del panel de Tipo de Reporte -->
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
               
            <asp:Panel ID="ConfirmDialog" runat="server" Style="display: none;" Height="215px" Width="375px">
                <div class="dialogcontainer">
                    <ul class="ultable">
                        <li class="rowcontainer">
                            <asp:Label ID="lblMessage" runat="server" CssClass="labelsMensaje" Text=""></asp:Label>
                        </li>                        
                        <li class="rowcontainer">
                            <asp:Button ID="btnAcept" runat="server" CssClass="buttonPlan" Text="Aceptar" 
                                Width="80px" onclick="btnAcept_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="80px" />
                        </li>
                    </ul>
                </div>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupConfirm" runat="server" BackgroundCssClass="modalBackground"
                DropShadow="True" Enabled="True" PopupControlID="ConfirmDialog" TargetControlID="btnShowMessage"> 
            </cc1:ModalPopupExtender>
            <asp:Button ID="btnShowMessage" runat="server" CssClass="alertas" Text="" Visible="true" Width="0px" />
            
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
