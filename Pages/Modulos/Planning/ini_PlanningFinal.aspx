<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ini_PlanningFinal.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Planning.ini_PlanningFinal" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Planning</title>
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
    <link href="../../css/layout.css" rel="stylesheet" type="text/css" />
    <link href="../../css/Webfotter.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        
    </style>
</head>
<body>
    <div class="Header" align="center" style="height: 150px;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Pages/ImgBooom/logotipo.png" ></asp:Image>
    </div>
    <div class="FondoLuckyMarca">
    </div>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    
    <div class="HeaderRight">
        <asp:Label ID="lblUsuario" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="#114092"></asp:Label>
        <asp:Label ID="usersession" runat="server" Visible="False" Font-Names="Verdana" Font-Size="9pt"
            ForeColor="#114092"></asp:Label>
        <br />
        <asp:ImageButton ID="ImgCloseSession" runat="server" Height="16px" ImageUrl="~/Pages/images/SesionClose.png"
            Width="84px" OnClick="ImgCloseSession_Click" />
    </div>
    <div class="ContendorPlanning">
        <asp:UpdatePanel ID="UpPlanning" runat="server">
            <ContentTemplate>
                <asp:Button ID="btndipararalerta" runat="server" CssClass="alertas" Enabled="False"
                    Height="0px" Text="" Width="0" />
                    <asp:Button ID="btndipararalertavista" runat="server" CssClass="alertas" Enabled="False"
                    Height="0px" Text="" Width="0" />
                <asp:Button ID="btndipararmensaje" runat="server" CssClass="alertas" Enabled="False"
                    Height="0px" Text="" Width="0" />
                <asp:Button ID="btndipararmensajecompe" runat="server" CssClass="alertas" Enabled="False"
                    Height="0px" Text="" Width="0" />
                <br />
                <br />
                <br />                
                <%--barra de progreso que indica al usuario que se esta procesando--%>
                <table align="center" width="100%">
                    <tr align="center">
                        <td>
                            <asp:Label ID="LblUpProgressUP" runat="server" ForeColor="White" Text="."></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Panel ID="Panel1" runat="server">
                                <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <div style="text-align: center;">
                                            <img alt="Procesando" src="../../images/loading1.gif" style="width: 90px; height: 13px" />
                                            <asp:Label ID="lblProgress" runat="server" ForeColor="Black" Text=" Procesando, por favor espere ..."></asp:Label>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <%--botones de navegación del modulo--%>
                <div align="center">
                    <asp:Button ID="BtnAsignaPresupuesto" runat="server" CssClass="buttonNewPlan" Height="25px"
                        OnClick="BtnAsignaPresupuesto_Click" Text="Asignar Presupuesto" Width="164px" />
                    <asp:Button ID="BtnDescripcionCampaña" runat="server" CssClass="buttonNewPlan" Height="25px"
                        OnClick="BtnDescripcionCampaña_Click" Text="Descripción Campaña" Width="164px" />
                    <asp:Button ID="BtnResponsables" runat="server" CssClass="buttonNewPlan" Height="25px"
                        OnClick="BtnResponsables_Click" Text="Responsables" Width="164px" />
                    <asp:Button ID="BtnAsignaPersonal" runat="server" CssClass="buttonNewPlan" Height="25px"
                        OnClick="BtnAsignaPersonal_Click" Text="Asignar Personal" Width="164px" />
                    <asp:Button ID="BtnPDV" runat="server" CssClass="buttonNewPlan" Height="25px" OnClick="BtnPDV_Click"
                        Text="Puntos de Venta" Width="164px" />
                    <asp:Button ID="BtnPaneles" runat="server" CssClass="buttonNewPlan" Height="25px" 
                        Text="Paneles" Width="164px" onclick="BtnPaneles_Click" />
                    <asp:Button ID="BtnAsignaPDVaOpe" runat="server" CssClass="buttonNewPlan" Height="25px"
                        OnClick="BtnAsignaPDVaOpe_Click" Text="Asignar Ruta" Width="164px" />
                    <asp:Button ID="BtnProductos" runat="server" CssClass="buttonNewPlan" Height="25px"
                        OnClick="BtnProductos_Click" Text="Levantamiento infor." Width="164px" />
                    <asp:Button ID="BtnReportes" runat="server" CssClass="buttonNewPlan" Height="25px"
                        Text="Asignación de Períodos" Width="164px" OnClick="BtnReportes_Click" />
                    <asp:Button ID="btnPanelPtoVenta" runat="server" CssClass="buttonNewPlan" Height="25px"
                        Text="Panel Mercaderista" Width="164px" OnClick="btnPanelPtoVenta_Click" />
                        
                </div>
                <br />
                <%--informacion de planning--%>
                <asp:Panel ID="PanelASignaPresupuesto" runat="server">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="90%">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td bgcolor="White">
                                    <asp:Panel ID="PanelPlanning" runat="server" Style="vertical-align: middle;">
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNumpla" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtnumpla" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC"
                                                        Enabled="False" ForeColor="White"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEstadoplanning" runat="server" CssClass="labelsN" Text="Estado"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="Rblisstatus" runat="server" Enabled="False" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Text="Activo"></asp:ListItem>
                                                        <asp:ListItem Text="Inactivo"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblpresu" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="cmbpresupuesto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbpresupuesto_SelectedIndexChanged"
                                                        Width="500px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table align="center" width="99%">
                                            <tr>
                                                <td>
                                                    <fieldset id="AsignaPresupuesto" runat="server">
                                                        <legend>Actividad o Campaña</legend>
                                                        <br />
                                                        <table align="center" frame="box">
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:Label ID="lblnameplaning" runat="server" CssClass="labelsN" Text="Campaña"></asp:Label>
                                                                </td>
                                                                <td valign="top">
                                                                    <asp:TextBox ID="txtnamepresu" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                                        Width="300px"></asp:TextBox>
                                                                </td>
                                                                <td colspan="2" valign="top">
                                                                    <asp:Label ID="lblfechsoli" runat="server" CssClass="labelsN" Text="Fecha de Solicitud"></asp:Label>
                                                                </td>
                                                                <td valign="top">
                                                                    <asp:TextBox ID="txt_FechaSolicitud" runat="server" AutoPostBack="True" Enabled="False"
                                                                        OnTextChanged="txt_FechaSolicitud_TextChanged"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="txt_FechaSolicitud_MaskedEditExtender" runat="server"
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechaSolicitud"
                                                                        UserDateFormat="DayMonthYear">
                                                                    </cc1:MaskedEditExtender>
                                                                    <cc1:CalendarExtender ID="txt_FechaSolicitud_CalendarExtender" runat="server" Enabled="True"
                                                                        FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal" PopupPosition="TopLeft"
                                                                        TargetControlID="txt_FechaSolicitud">
                                                                    </cc1:CalendarExtender>
                                                                    <asp:ImageButton ID="ImageButtonCal" runat="server" Enabled="False" Height="16px"
                                                                        ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblfecentregafin" runat="server" CssClass="labelsN" Text="Fecha Entrega Final"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_FechaEntrega" runat="server" AutoPostBack="True" Enabled="False"
                                                                        OnTextChanged="txt_FechaEntrega_TextChanged"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="txt_FechaEntrega_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechaEntrega"
                                                                        UserDateFormat="DayMonthYear">
                                                                    </cc1:MaskedEditExtender>
                                                                    <cc1:CalendarExtender ID="txt_FechaEntrega_CalendarExtender" runat="server" Enabled="True"
                                                                        FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal2" PopupPosition="TopLeft"
                                                                        TargetControlID="txt_FechaEntrega">
                                                                    </cc1:CalendarExtender>
                                                                    <asp:ImageButton ID="ImageButtonCal2" runat="server" Enabled="False" Height="16px"
                                                                        ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:Label ID="lblclient" runat="server" CssClass="labelsN" Text="Cliente"></asp:Label>
                                                                </td>
                                                                <td valign="top">
                                                                    <asp:TextBox ID="txtcliente" runat="server" BackColor="Silver" Enabled="False" Width="300px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblperiodopre" runat="server" CssClass="labelsN" Text="Periodo de Preproduccion"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblfecinipre" runat="server" CssClass="labelsN" Text=" Inicio"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_FechainiPre" runat="server" AutoPostBack="True" Enabled="False"
                                                                        OnTextChanged="txt_FechainiPre_TextChanged"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="txt_FechainiPre_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechainiPre"
                                                                        UserDateFormat="DayMonthYear">
                                                                    </cc1:MaskedEditExtender>
                                                                    <cc1:CalendarExtender ID="txt_FechainiPre_CalendarExtender" runat="server" Enabled="True"
                                                                        FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal3" PopupPosition="TopLeft"
                                                                        TargetControlID="txt_FechainiPre">
                                                                    </cc1:CalendarExtender>
                                                                    <asp:ImageButton ID="ImageButtonCal3" runat="server" Enabled="False" Height="16px"
                                                                        ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblfefinpre" runat="server" CssClass="labelsN" Text="Fin"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_Fechafinpre" runat="server" AutoPostBack="True" Enabled="False"
                                                                        OnTextChanged="txt_Fechafinpre_TextChanged"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="txt_Fechafinpre_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Fechafinpre"
                                                                        UserDateFormat="DayMonthYear">
                                                                    </cc1:MaskedEditExtender>
                                                                    <cc1:CalendarExtender ID="txt_Fechafinpre_CalendarExtender" runat="server" Enabled="True"
                                                                        FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal4" PopupPosition="TopLeft"
                                                                        TargetControlID="txt_Fechafinpre">
                                                                    </cc1:CalendarExtender>
                                                                    <asp:ImageButton ID="ImageButtonCal4" runat="server" Enabled="False" Height="16px"
                                                                        ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:Label ID="lblservice" runat="server" CssClass="labelsN" Text="Servicio"></asp:Label>
                                                                </td>
                                                                <td valign="top">
                                                                    <asp:TextBox ID="txtservice" runat="server" BackColor="#CCCCCC" Enabled="False" Width="300px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblperiEjecu" runat="server" CssClass="labelsN" Text="Periodo de Ejecucion"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblfecini" runat="server" CssClass="labelsN" Text=" Inicio"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_FechainiPla" runat="server" AutoPostBack="True" Enabled="False"
                                                                        OnTextChanged="txt_FechainiPla_TextChanged"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="txt_FechainiPla_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechainiPla"
                                                                        UserDateFormat="DayMonthYear">
                                                                    </cc1:MaskedEditExtender>
                                                                    <cc1:CalendarExtender ID="txt_FechainiPla_CalendarExtender" runat="server" Enabled="True"
                                                                        FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal5" PopupPosition="TopLeft"
                                                                        TargetControlID="txt_FechainiPla">
                                                                    </cc1:CalendarExtender>
                                                                    <asp:ImageButton ID="ImageButtonCal5" runat="server" Enabled="False" Height="16px"
                                                                        ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblfecfin" runat="server" CssClass="labelsN" Text="Fin"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_FechaPlafin" runat="server" AutoPostBack="True" Enabled="False"
                                                                        OnTextChanged="txt_FechaPlafin_TextChanged"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="txt_FechaPlafin_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechaPlafin"
                                                                        UserDateFormat="DayMonthYear">
                                                                    </cc1:MaskedEditExtender>
                                                                    <cc1:CalendarExtender ID="txt_FechaPlafin_CalendarExtender" runat="server" Enabled="True"
                                                                        FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal6" PopupPosition="TopLeft"
                                                                        TargetControlID="txt_FechaPlafin">
                                                                    </cc1:CalendarExtender>
                                                                    <asp:ImageButton ID="ImageButtonCal6" runat="server" Enabled="False" Height="16px"
                                                                        ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7" valign="top">
                                                                    <table align="center" width="99%">
                                                                        <tr>
                                                                            <td align="center" valign="top" width="60%">
                                                                                <asp:Label ID="LbDuracion" runat="server" CssClass="labelsN" Text="Duración del proyecto"></asp:Label>
                                                                                <br />
                                                                                <asp:TextBox ID="TxtDuracion" runat="server" Height="70px" TextMode="MultiLine" Width="330px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left" valign="top" width="30%">
                                                                                <asp:Label ID="lblcanal" runat="server" CssClass="labelsN" Text="Canal"></asp:Label>
                                                                                <br />
                                                                                <div class="p" style="width: 220px; height: 70px;">
                                                                                    <asp:RadioButtonList ID="RbtnCanal" runat="server">
                                                                                    </asp:RadioButtonList>
                                                                                </div>
                                                                            </td>
                                                                            <td align="right" valign="bottom" width="10%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Button ID="BtnSavePlanning" runat="server" CssClass="buttonSavePlan" Height="25px"
                                                                                                OnClick="BtnSavePlanning_Click" Text="Guardar" Width="164px" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Button ID="btncancelcara" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                                                                Height="25px" OnClick="btncancelcara_Click" Text="Deshacer" Width="164px" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <%--informacion de descripción del planning--%>
                <asp:Panel ID="PanelDescCampaña" runat="server">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td bgcolor="White">
                                    <asp:Panel ID="PanelDesc" runat="server" Style="vertical-align: middle;">
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblPlannigDesc" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtCodPlanningDesc" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                        ForeColor="White"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblSelPresupuestoDesc" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CmbSelPresupuestoDesc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CmbSelPresupuestoDesc_SelectedIndexChanged"
                                                        Width="500px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table align="center" width="99%">
                                            <tr>
                                                <td>
                                                    <fieldset id="Fieldset1" runat="server">
                                                        <legend>Descripción Campaña</legend>
                                                        <br />
                                                        <table>
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:Label ID="lblobj" runat="server" CssClass="labelsN" Text="Objetivo de la Campaña"></asp:Label>
                                                                    <asp:Label ID="lblolbli13" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtobj" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="150px"
                                                                        TextMode="MultiLine" Width="265px"></asp:TextBox>
                                                                </td>
                                                                <td valign="top">
                                                                    <asp:Label ID="lblmanda" runat="server" CssClass="labelsN" Text="Mandatorios de  Campaña"></asp:Label>
                                                                    <asp:Label ID="lblolbli14" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtmanda" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="150px"
                                                                        TextMode="MultiLine" Width="265px"></asp:TextBox>
                                                                </td>
                                                                <td valign="top">
                                                                    <asp:Label ID="lblmeca" runat="server" CssClass="labelsN" Text="Mecanica"></asp:Label>
                                                                    <asp:Label ID="lblolbli15" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                    <br />
                                                                    <asp:TextBox ID="Txtmeca" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="150px"
                                                                        TextMode="MultiLine" Width="265px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="BtnSaveDescCampaña" runat="server" CssClass="buttonSavePlan" Height="25px"
                                                                                    OnClick="BtnSaveDescCampaña_Click" Text="Guardar" Width="164px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="BtnClearDescrip" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                                                    Height="25px" OnClick="BtnClearDescrip_Click" Text="Deshacer" Width="164px" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" valign="top">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblcontacto" runat="server" CssClass="labelsN" Text="Contacto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtcontacto" runat="server" MaxLength="60" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblolbli17" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                                <asp:RegularExpressionValidator ID="Reqcontacto" runat="server" ControlToValidate="txtcontacto"
                                                                                    Display="None" ErrorMessage="No ingrese caracteres especiales ni nùmeros &lt;br /&gt; No inicie con espacio en blanco "
                                                                                    ValidationExpression="([a-zA-Z]{1,1}[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,59})"></asp:RegularExpressionValidator>
                                                                                <cc1:ValidatorCalloutExtender ID="Reqcontacto_ValidatorCalloutExtender" runat="server"
                                                                                    Enabled="True" TargetControlID="Reqcontacto">
                                                                                </cc1:ValidatorCalloutExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblarea" runat="server" CssClass="labelsN" Text="Area Involucrada"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtarea" runat="server" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblolbli27" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                                <asp:RegularExpressionValidator ID="ReqTxtArea" runat="server" ControlToValidate="txtarea"
                                                                                    Display="None" ErrorMessage="No ingrese caracteres especiales ni nùmeros &lt;br /&gt; No inicie con espacio en blanco "
                                                                                    ValidationExpression="([a-zA-Z]{1,1}[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,59})"></asp:RegularExpressionValidator>
                                                                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                                                    TargetControlID="ReqTxtArea">
                                                                                </cc1:ValidatorCalloutExtender>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <%--informacion de Responsables del planning--%>
                <asp:Panel ID="PanelResponsablesCampaña" runat="server">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td bgcolor="White">
                                    <asp:Panel ID="PanelRespons" runat="server" Style="vertical-align: middle; padding: 10px">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblPlanningRes" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtPlanningRes" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                        ForeColor="White"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblSelPresupuestoRes" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CmbSelPresupuestoRes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CmbSelPresupuestoRes_SelectedIndexChanged"
                                                        Width="500px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table>
                                                        <tr>
                                                            <td valign="top">
                                                                <asp:Label ID="LblSelEjecutivo" runat="server" Font-Bold="True" Font-Names="verdana"
                                                                    Font-Size="10pt" Text="Supervisor Controler"></asp:Label>
                                                            </td>
                                                            <td valign="top">
                                                                <div style="width: 500px; height: 65px;" class="p">
                                                                    <asp:CheckBoxList ID="ChkSelEjecutivos" runat="server" Width="450px">
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <%-- <asp:DropDownList ID="CmbSelEjecutivo" runat="server">
                                                                </asp:DropDownList>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="BtnSaveRespons" runat="server" CssClass="buttonSavePlan" Height="25px"
                                                                    OnClick="BtnSaveRespons_Click" Text="Guardar" Width="164px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="BtnClearRespons" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                                    Height="25px" OnClick="BtnClearRespons_Click" Text="Deshacer" Width="164px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblSelSupervisores" runat="server" Font-Bold="True" Font-Names="verdana"
                                                        Font-Size="10pt" Text="Supervisor(es)"></asp:Label>
                                                    &nbsp;
                                                    <asp:ImageButton ID="ImgButtonAddSupervisores" runat="server" ImageUrl="~/Pages/images/add.png"
                                                        ToolTip="Otros Supervisores" Visible="False" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblSelMercadersitas" runat="server" Font-Bold="True" Font-Names="verdana"
                                                        Font-Size="10pt" Text="Mercaderista(s)"></asp:Label>
                                                    &nbsp;
                                                    <asp:ImageButton ID="ImgButtonAddMercaderistas" runat="server" ImageUrl="~/Pages/images/add.png"
                                                        ToolTip="Otros Mercadersitas" Visible="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <div class="ScrollPersonalPlanning">
                                                        &nbsp;&nbsp;
                                                        <asp:Button ID="BtnAllSup" runat="server" CssClass="buttonsinfondo" OnClick="BtnAllSup_Click"
                                                            Text="Todos" Visible="False" />
                                                        <asp:Button ID="BtnNoneSup" runat="server" CssClass="buttonsinfondo" OnClick="BtnNoneSup_Click"
                                                            Text="Ninguno" Visible="False" />
                                                        <br />
                                                        <asp:CheckBoxList ID="ChkListSupervisores" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td valign="top">
                                                    <div class="ScrollPersonalPlanning">
                                                        &nbsp;&nbsp;
                                                        <asp:Button ID="BtnAllOpe" runat="server" CssClass="buttonsinfondo" OnClick="BtnAllOpe_Click"
                                                            Text="Todos" Visible="False" />
                                                        <asp:Button ID="BtnNoneOpe" runat="server" CssClass="buttonsinfondo" OnClick="BtnNoneOpe_Click"
                                                            Text="Ninguno" Visible="False" />
                                                        <br />
                                                        <asp:CheckBoxList ID="ChkListMercaderistas" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PanelNewSupervisor" runat="server" BackColor="White" BorderColor="#7F99CC"
                    BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="490px"
                    Style="display: none;">
                    <div>
                        <asp:Label ID="LblListadoSupervisores" runat="server" Text="Asignar nuevos Supervisores"></asp:Label>
                        <asp:ImageButton ID="BtnclosePanel1" runat="server" BackColor="Transparent" Height="22px"
                            ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" OnClick="BtnclosePanel1_Click"
                            Width="23px" />
                    </div>
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <div style="overflow: auto; width: 500px; height: 400px;">
                                    <asp:ListBox ID="LstNewSupervisor" runat="server" Height="100%" SelectionMode="Multiple"
                                        ToolTip="oprima (ctrl + click) si desea selección no consecutiva" Width="100%">
                                    </asp:ListBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div align="center">
                        <asp:Button ID="btnAddSupervisores" runat="server" OnClick="btnAddSupervisores_Click"
                            Text="Agregar" />
                    </div>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPanelNewSupervisor" runat="server" BackgroundCssClass="modalBackground"
                    Enabled="True" PopupControlID="PanelNewSupervisor" TargetControlID="ImgButtonAddSupervisores">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="PanelNewMercaderista" runat="server" BackColor="White" BorderColor="#7F99CC"
                    BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="490px"
                    Style="display: none;">
                    <div>
                        <asp:Label ID="LblListadoMercaderistas" runat="server" Text="Asignar nuevos Mercaderistas"></asp:Label>
                        <asp:ImageButton ID="BtnclosePanel2" runat="server" BackColor="Transparent" Height="22px"
                            ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" OnClick="BtnclosePanel2_Click"
                            Width="23px" />
                    </div>
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <div style="overflow: auto; width: 500px; height: 400px;">
                                    <asp:ListBox ID="LstNewMercaderista" runat="server" Height="100%" SelectionMode="Multiple"
                                        ToolTip="oprima (ctrl + click) si desea selección no consecutiva" Width="100%">
                                    </asp:ListBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div align="center">
                        <asp:Button ID="btnAddMercaderistas" runat="server" OnClick="btnAddMercaderistas_Click"
                            Text="Agregar" />
                    </div>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPanelNewMercaderista" runat="server" BackgroundCssClass="modalBackground"
                    Enabled="True" PopupControlID="PanelNewMercaderista" TargetControlID="ImgButtonAddMercaderistas">
                </cc1:ModalPopupExtender>
                <%--informacion de asignacion de personal--%>
                <asp:Panel ID="PanelAsignaPersonal" runat="server">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td bgcolor="White">
                                    <asp:Panel ID="PanelAsigna" runat="server" Style="vertical-align: middle;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblPanningAsig" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtPlanningAsig" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                        ForeColor="White"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblSelPresupuestoAsig" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CmbSelPresupuestoAsig" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CmbSelPresupuestoAsig_SelectedIndexChanged"
                                                        Width="500px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <table align="center">
                                            <tr>
                                                <td valign="top" style="width: 320px;">
                                                    <asp:Label ID="lblsupervisor" runat="server" CssClass="labelsN" Text="Supervisores"></asp:Label>
                                                    <asp:Label ID="lblolbli30" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                    <br />
                                                    <asp:RadioButtonList ID="Lisboxsupervi" runat="server" CssClass="p" Height="200px"
                                                        RepeatLayout="Flow" Width="320px">
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td valign="top" style="width: 320px;">
                                                    <asp:Label ID="lblpersoncalle" runat="server" CssClass="labelsN" Text="Mercaderistas"></asp:Label>
                                                    <asp:Label ID="lblolbli31" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                    <br />
                                                    <asp:ListBox ID="LstBoxMercaderistas" runat="server" CssClass="p" Height="200px"
                                                        SelectionMode="Multiple" Width="320px"></asp:ListBox>
                                                </td>
                                                <td align="center" valign="top" width="30px">
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="BtnMasAsing" runat="server" CssClass="pagnext" OnClick="BtnMasAsing_Click"
                                                        Text=" ." ToolTip="Asignar" Width="25px" />
                                                </td>
                                                <td valign="top">
                                                    <asp:Label ID="LblAsignacion" runat="server" CssClass="labelsN" Text="Listado de asignación para esta campaña"></asp:Label>
                                                    <br />
                                                    <div style="width: 600px; height: 200px;" class="p">
                                                        <asp:GridView ID="GvAsignados" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                            Font-Names="Verdana" Font-Size="8pt" GridLines="None" OnSelectedIndexChanged="GvAsignados_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                    ShowSelectButton="True" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                </td>
                                                <td valign="top">
                                                </td>
                                                <td>
                                                </td>
                                                <td align="center">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="BtnSaveAsig" runat="server" CssClass="buttonSavePlan" Enabled="false"
                                                                    Height="25px" OnClick="BtnSaveAsig_Click" Text="Guardar" Width="164px" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="BtnClearAsig" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                                    Height="25px" OnClick="BtnClearAsig_Click" Text="Deshacer" Width="164px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <%--informacion de asignacion de puntos de venta--%>
                <asp:Panel ID="PanelAsignaPDV" runat="server">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td bgcolor="White">
                                    <asp:Panel ID="PanelPDV" runat="server" Style="vertical-align: middle;">
                                        <div align="center">
                                            <iframe id="ifcarga" runat="server" height="305px" src="" width="1000px"></iframe>
                                        </div>
                                        <div style="display: none;">
                                            <asp:GridView ID="GVPDV" runat="server" Height="250px" Visible="False" Width="100%">
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>

                    <%--informacion de configuración de paneles--%>
                <asp:Panel ID="PanelPanelesPlanning" runat="server">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td bgcolor="White">
                                    <asp:Panel ID="PanelPaneles" runat="server" Style="vertical-align: middle;">
                                    
                                    <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblPlannigPanel" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtCodPlanningPanel" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                        ForeColor="White"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblSelPresupuestoPanel" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CmbSelPresupuestoPanel" runat="server" AutoPostBack="True" 
                                                        Width="500px" 
                                                        onselectedindexchanged="CmbSelPresupuestoPanel_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                             <tr runat="server" id="parametros" Visible="false">
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" CssClass="labelsN" Text="Ciudad:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCiudad"  runat="server" AutoPostBack="True" 
                                                        Width="200px"  onselectedindexchanged="ddlCiudad_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" CssClass="labelsN" Text="Mercado:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMercado"  runat="server" AutoPostBack="True" 
                                                        Width="500px" onselectedindexchanged="ddlMercado_SelectedIndexChanged"  >
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="colgate" visible="false" >
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" CssClass="labelsN" Text="Tipo Panel:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoPanel" runat="server"  
                                                        Width="200px" >
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                  
                                                </td>
                                                <td>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                         <table width="100%" >
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblReportesPanel" runat="server" CssClass="labelsN" Text="Reportes"></asp:Label>
                                                    <br />
                                                    <div style="border: 4px solid #B5C5E1; overflow: auto; width: 250px; height: 190px; ">
                                                        <asp:RadioButtonList ID="RbtnListReportPanel" runat="server" 
                                                            AutoPostBack="True" 
                                                            onselectedindexchanged="RbtnListReportPanel_SelectedIndexChanged">
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </td>
                                                <td>
                                                 <asp:Label ID="LblPDVPanel" runat="server" CssClass="labelsN" Text="Puntos de Venta"></asp:Label>
                                                    <br />
                                                         <div style="border: 4px solid #B5C5E1;  height: 190px;">
                                                        
                                                 <div  style="overflow: auto; width: 750px; height: 150px; ">                                                
                                                    <asp:GridView ID="GvPDVPaneles" runat="server" EmptyDataText="No existen puntos de venta disponibles para este planning"
                                                        Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal"  AutoGenerateColumns="False"
                                                        EnableModelValidation="True" 
                                                       >
                                                        <Columns>
                                                            <asp:TemplateField>                                                               
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No."  ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Código" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labelcodpdv" runat="server" Text='<%# Bind("Código") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="Labelcodpdv" runat="server" Text='<%# Bind("Código") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Punto de Venta" ItemStyle-Width="480px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labelnompdv" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="Labelnompdv" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Oficina" ItemStyle-Width="70px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labelofpanel" runat="server" Text='<%# Bind("Oficina") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="Labelofpanel" runat="server" Text='<%# Bind("Oficina") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField> 
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                               
                                             
                                                            <asp:Label ID="LblSelRapida" runat="server" Text="Selección rápida. Ingrese código de Punto" Font-Names="Arial"
                                                        Font-Size="8pt" ForeColor="Black" Visible= "false"></asp:Label>
                                                       
                                                            <asp:TextBox ID="TxtCodigoPDV" runat="server" Visible= "false" ></asp:TextBox>
                                                            <asp:ImageButton ID="ImgSelRapida" runat="server" Visible= "false"
                                                            ImageUrl="~/Pages/images/last.png" onclick="ImgSelRapida_Click" />
                                                             <asp:Label ID="Lblmsj" runat="server" Text="" Font-Names="Arial"
                                                        Font-Size="8pt" ForeColor="Red" ></asp:Label>
                                                        <br />
                                                     
                                                </div>
                                                </td>
                                            </tr>

                                        </table>

                                  <table align="center">
                                            <tr>
                                            <td align="right">Año: <asp:DropDownList ID="ddlAño" runat="server">    
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                
                                                Mes: <asp:DropDownList ID="ddlmes" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlmes_SelectedIndexChanged"> 
                                                    <asp:ListItem Value="01">Enero</asp:ListItem>
                                                    <asp:ListItem Value="02">Febrero</asp:ListItem>
                                                    <asp:ListItem Value="03">Marzo</asp:ListItem>
                                                    <asp:ListItem Value="04">Abril</asp:ListItem>
                                                    <asp:ListItem Value="05">Mayo</asp:ListItem>
                                                    <asp:ListItem Value="06">Junio</asp:ListItem>
                                                    <asp:ListItem Value="07">Julio</asp:ListItem>
                                                    <asp:ListItem Value="08">Agosto</asp:ListItem>
                                                    <asp:ListItem Value="09">Septiembre</asp:ListItem>
                                                    <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                    <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                    <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                                </asp:DropDownList>
                                                
                                            </td>
                                             <td align="left">
                                                     Seleccione un Periodo: <asp:DropDownList ID="ddlPeriodo" runat="server" 
                                                         Width="100px">
                                                     </asp:DropDownList>
                                                 </td>
                                            </tr>

                                        </table>
                                        <table align="center">
                                            <tr>
                                                <td>
                                                Copiar PDV
                                                <asp:RadioButtonList ID="RbtnSelTipoCarga" runat="server" 
                                                    RepeatDirection="Horizontal"  Width="200px"  
                                                        onselectedindexchanged="RbtnSelTipoCarga_SelectedIndexChanged" 
                                                            AutoPostBack="True" Enabled="true">
                                                        <asp:ListItem Value="1" >Si</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                 </asp:RadioButtonList>
                                                
                                                    
                                                </td>
                                                <td>
                                                   
                                                </td>
                                            </tr>
                                         <tr runat="server" id="copiarPDV" visible="false">
                                            <td align="right">Año: <asp:DropDownList ID="ddlAño2" runat="server">
                                                <asp:ListItem>2011</asp:ListItem>
                                                <asp:ListItem>2010</asp:ListItem>
                                                <asp:ListItem>2012</asp:ListItem>
                                                </asp:DropDownList>
                                                </td>
                                            <td align="left">
                                                
                                                Mes: <asp:DropDownList ID="ddlmes2" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlmes2_SelectedIndexChanged"> 
                                                    <asp:ListItem Value="01">Enero</asp:ListItem>
                                                    <asp:ListItem Value="02">Febrero</asp:ListItem>
                                                    <asp:ListItem Value="03">Marzo</asp:ListItem>
                                                    <asp:ListItem Value="04">Abril</asp:ListItem>
                                                    <asp:ListItem Value="05">Mayo</asp:ListItem>
                                                    <asp:ListItem Value="06">Junio</asp:ListItem>
                                                    <asp:ListItem Value="07">Julio</asp:ListItem>
                                                    <asp:ListItem Value="08">Agosto</asp:ListItem>
                                                    <asp:ListItem Value="09">Septiembre</asp:ListItem>
                                                    <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                    <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                    <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                                </asp:DropDownList>
                                                
                                            </td>
                                            <td align="right">
                                                     Seleccione un Periodo:<asp:DropDownList ID="ddlperiodo2" runat="server" 
                                                         Width="100px">
                                                     </asp:DropDownList>
                                             </td>
                                            </tr>
                                            <tr  runat="server" id="copiarPDVG" visible="false">
                                                <td>
                                                    <asp:Button ID="btnguardarPDV" runat="server" CssClass="buttonSavePlan" 
                                                        Height="25px" Text="Guardar PDV" Width="164px" onclick="btnguardarPDV_Click" />
                                                </td>
                                            </tr>

                                        </table>
                                        
                                         <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnSavePanel" runat="server" CssClass="buttonSavePlan" Enabled="false"
                                                        Height="25px" Text="Guardar" Width="164px" onclick="BtnSavePanel_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnClearPanel" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                     Enabled="false" Height="25px" Text="Limpiar" Width="164px" 
                                                        onclick="BtnClearPanel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </asp:Panel>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>

                <%--informacion de asignacion de productos--%>
                <asp:Panel ID="PanelAsignaProductos" runat="server">
                <div class="centrar">
                    <div class="tabla centrar" style="border:none">
                        <table border="0" cellpadding="0" cellspacing="0" class="borde_panel">
                            <tr>
                                <td bgcolor="White">
                                    <asp:Panel ID="PanelProducto" runat="server" Style="vertical-align: middle;" Height="350px">
                                        <div class="tabla" style="border:none">
                                            <div class="fila">
                                                <div class="celda">
                                                    <asp:Label ID="LblPanningAsigProd" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                </div>
                                                <div class="celda">
                                                    <asp:TextBox ID="TxtPlanningAsigProd" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                        ForeColor="White"></asp:TextBox>
                                                </div>
                                                <div class="celda">
                                                    <asp:Label ID="LblSelPresupuestoAsigProd" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                                </div>
                                                <div class="celda">
                                                    <%--<asp:TextBox ID="Txt" runat="server" 
                                                         Width="500px">
                                                    </asp:TextBox>--%>
                                                </div>
                                                <div class="celda">
                                                    <asp:DropDownList ID="CmbSelPresupuestoAsigProd" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="CmbSelPresupuestoAsigProd_SelectedIndexChanged" Width="500px">
                                                    </asp:DropDownList>
                                                                       
                                                </div>
                                            </div>
                                        </div>
                                        <div class="centrarcontenido" style=" padding:5px">
                                            <asp:Label ID="LblReporteAsociado" runat="server" CssClass="labelsTitN" 
                                                Visible="False"></asp:Label>
                                        </div>
                                        <div id="div_masopciones" runat="server"  style=" display:none;"  >
                                        <table align="center"> 
                                            <tr>
                                                <td>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="SelMasOpcion" runat="server" Text="Seleccione opción:"></asp:Label>
                                                    <br />
                                                     <asp:RadioButtonList ID="RbtMasopciones" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="RbtMasopciones_SelectedIndexChanged"  >                                            
                                                    </asp:RadioButtonList>
                                                </td> 
                                            </tr>
                                        </table>
                                           
                                        </div>
                                        <div class="centrar">
                                        <div id="divselproductos" class="centrar" runat="server" style="display:none;">
                                            <div class="centrarcontenido">
                                                <asp:Button ID="BtnProdPropio" runat="server" CssClass="buttonNewPlan" Enabled="False"
                                                    Height="25px" OnClick="BtnProdPropio_Click" Text="Productos Propios" Width="164px" />
                                                <asp:Button ID="BtnProdCompe" runat="server" CssClass="buttonNewPlan" Enabled="False"
                                                    Height="25px" OnClick="BtnProdCompe_Click" Text="Productos Competidor" Width="164px" />
                                            </div>
                                            <div class="centrarcontenido">                                                
                                                <asp:Label ID="LblSelCompetidor" runat="server" CssClass="labelsN" Text="Seleccione Competidor"
                                                    Visible="False"></asp:Label>                                                            
                                                <asp:DropDownList ID="CmbCompetidores" runat="server" AutoPostBack="True" CausesValidation="True"
                                                    OnSelectedIndexChanged="CmbCompetidores_SelectedIndexChanged" Visible="False"
                                                    Width="400">
                                                </asp:DropDownList>
                                            </div>
                                          
                                        
                                            <div id="panel_contenedor" class="autosize" style=" padding: 5px">
                                            <div id="categorias" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">Categorias</span></div>
                                                    <br />
                                                    <asp:RadioButtonList ID="rbliscatego" runat="server" AutoPostBack="True" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" OnSelectedIndexChanged="rbliscatego_SelectedIndexChanged"
                                                        RepeatLayout="Flow" Width="250px">
                                                    </asp:RadioButtonList>
                                                    <asp:CheckBoxList ID="Chklistcatego" runat="server" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" 
                                                         RepeatLayout="Flow" Width="250px">
                                                    </asp:CheckBoxList>
                                            </div> 
                                            <div id="Marcas" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">Marca</span></div>
                                                    <br />
                                                    <asp:RadioButtonList ID="rblmarca" runat="server" AutoPostBack="True" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" OnSelectedIndexChanged="rblmarca_SelectedIndexChanged"
                                                        RepeatLayout="Flow" Width="250px">
                                                    </asp:RadioButtonList>
                                                    <asp:CheckBoxList ID="Chklistmarca" runat="server" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" 
                                                         RepeatLayout="Flow" Width="250px">
                                                    </asp:CheckBoxList>
                                            </div> 
                                            <div id="submarcas" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><asp:Label ID="lblsumarca" runat="server" CssClass="labelsN" Text="SubMarca"></asp:Label></div>
                                                    <br />
                                                    <asp:RadioButtonList ID="rblsubmarca" runat="server" AutoPostBack="True" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" OnSelectedIndexChanged="rblsubmarca_SelectedIndexChanged"
                                                        RepeatLayout="Flow" Width="250px">
                                                    </asp:RadioButtonList>
                                            </div> 
                                            <div id="Familias" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">Familias</span></div>
                                                    <br />
                                                    <asp:RadioButtonList ID="rblfamilia" runat="server" AutoPostBack="True" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px"
                                                        RepeatLayout="Flow" Width="250px" 
                                                        onselectedindexchanged="rblfamilia_SelectedIndexChanged">
                                                    </asp:RadioButtonList>                                                 
                                                    <asp:CheckBoxList ID="ChkListFamilias" runat="server" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" 
                                                         RepeatLayout="Flow" Width="250px">
                                                    </asp:CheckBoxList>
                                            </div> 
                                            <div id="SubFamilias" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">SubFamilias</span></div>
                                                    <br />
                                                    <asp:RadioButtonList ID="rblsubfamilia" runat="server" AutoPostBack="True" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px"
                                                        RepeatLayout="Flow" Width="250px" 
                                                        onselectedindexchanged="rblsubfamilia_SelectedIndexChanged">
                                                    </asp:RadioButtonList>
                                                    <asp:CheckBoxList ID="ChkListSubFamilias" runat="server" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" 
                                                         RepeatLayout="Flow" Width="250px">
                                                    </asp:CheckBoxList>
                                            </div> 
                                            <div id="productos" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">Productos</span></div>
                                                    <br />
                                                    <asp:CheckBoxList ID="ChkProductos" runat="server" CssClass="p" Font-Names="Verdana"
                                                        Font-Size="8pt" Height="130px" RepeatLayout="Flow" Width="380px">
                                                    </asp:CheckBoxList>
                                            </div> 
                                        </div>                          

                                            <div class="centrarcontenido">
                                                            <asp:Button ID="BtnSaveProd" runat="server" CssClass="buttonSavePlan" Enabled="false"
                                                                Height="25px" OnClick="BtnSaveProd_Click" Text="Guardar" Width="164px" />

                                                            <asp:Button ID="BtnClearProd" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                                Enabled="false" Height="25px" OnClick="BtnClearProd_Click" Text="Otro Reporte" Width="164px" />

                                                            <asp:Button ID="BtnCargaLevanInform" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                                    Height="25px" Text="Carga Masiva" Width="164px" OnClick="BtnCargaLevanInform_Click" />

                                                                    <asp:Button ID="btnCargaPrecio" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                                    Height="25px" Text="Carga Masiva Precio" Visible="false"  Width="164px" 
                                                                onclick="btnCargaPrecio_Click"  />
                                        </div>
                                       </div>


                                       <!------------------------------------------------------------------------------------------------------------------------------------->

                                       <div id="div_Elementos" class="centrar" runat="server" style="display:none;">
                                            <div id="Div2" class="autosize" style=" padding: 5px;margin:auto">
                                            <div id="Div5" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">Elementos de Visibilidad</span></div>
                                                    <br />
                                                    <asp:CheckBoxList ID="chklist" runat="server" CssClass="p" Font-Names="Verdana"
                                                        Font-Size="8pt" Height="130px" RepeatLayout="Flow" Width="380px">
                                                    </asp:CheckBoxList>
                                            </div> 

                                           

                                       </div>

                                        <div class="centrarcontenido">
                                                            <asp:Button ID="btnsave_Elemento" runat="server" CssClass="buttonSavePlan" Enabled="true"
                                                                Height="25px" OnClick="btnsave_Elemento_Click" Text="Guardar" Width="164px" />

                        
                                        </div>
                                       </div>

                                       <!-------------------------------------------------------------------------------------------------------------------------------------->
                                        
                                        <div id="div_Pres_Colgate" class="centrar" runat="server" style="display:none;">
                                            <div id="Div4" class="autosize" style=" padding: 5px;margin:auto">
                                            <div id="Div3" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">Categoria</span></div>
                                                    <br /> 
                                                    <asp:RadioButtonList ID="rbl_Cate_Pres_Colgate" runat="server" AutoPostBack="True" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" OnSelectedIndexChanged="rbliscatego_SelectedIndexChanged"
                                                        RepeatLayout="Flow" Width="250px">
                                                    </asp:RadioButtonList>
                                            </div> 

                                             <div id="Div1" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">Productos</span></div>
                                                    <br />
                                                    <asp:CheckBoxList ID="chklist_Cate_Pres_Colgate" runat="server" CssClass="p" Font-Names="Verdana"
                                                        Font-Size="8pt" Height="130px" RepeatLayout="Flow" Width="380px">
                                                    </asp:CheckBoxList>
                                            </div> 

                                       </div>

                                        <div class="centrarcontenido">
                                                            <asp:Button ID="Button1" runat="server" CssClass="buttonSavePlan" Enabled="true"
                                                                Height="25px" OnClick="btnsave_Elemento_Click" Text="Guardar" Width="164px" />

                        
                                        </div>
                                       </div>

                                       <!-------------------------------------------------------------------------------------------------------------------------------------->

                                        <!-------------------------------------------------------------------------------------------------------------------------------------->
                                        
                                 <%--       <div id="div_exhibidor" class="centrar" runat="server" style="display:none;">
                                            <div id="Div7" class="autosize" style=" padding: 5px;margin:auto">
                                              <div id="Div6" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">Misiles</span></div>
                                                    <br />
                                                    <asp:CheckBoxList ID="chk_exhibidor" runat="server" CssClass="p" Font-Names="Verdana"
                                                        Font-Size="8pt" Height="130px" RepeatLayout="Flow" Width="380px">
                                                    </asp:CheckBoxList>
                                            </div> 

                                       </div>

                                        <div class="centrarcontenido">
                                                            <asp:Button ID="BtnExhibidor" runat="server" CssClass="buttonSavePlan" Enabled="true"
                                                                Height="25px" OnClick="BtnExhibidor_Click" Text="Guardar" Width="164px" />

                        
                                        </div>
                                       </div>--%>

                                       <!-------------------------------------------------------------------------------------------------------------------------------------->

                                      <!-------------------------------------------------------------------------------------------------------------------------------------->
                                        
                                        <div id="div_Observaciones" class="centrar" runat="server" style="display:none;">
                                            <div id="Div9" class="autosize" style=" padding: 5px;margin:auto">
                                              <div id="Div10" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">Observaciones</span></div>
                                                    <br />
                                                    <asp:CheckBoxList ID="chklist_Observaciones" runat="server" CssClass="p" Font-Names="Verdana"
                                                        Font-Size="8pt" Height="130px" RepeatLayout="Flow" Width="380px">
                                                    </asp:CheckBoxList>
                                            </div> 

                                       </div>

                                        <div class="centrarcontenido">
                                                            <asp:Button ID="btnObservaciones" runat="server" CssClass="buttonSavePlan" Enabled="true"
                                                                Height="25px" OnClick="btnObservaciones_Click" Text="Guardar" Width="164px" />

                        
                                        </div>
                                       </div>

                                       <!-------------------------------------------------------------------------------------------------------------------------------------->




                                        </div>
                                    </asp:Panel>
                                </td>                                
                            </tr>                            
                        </table>
                    </div>
                </div>
                </asp:Panel>

                <asp:Panel ID="PanelCargaMasivaProductos" runat="server">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td >
                                    <asp:Panel ID="PanelCargMasivaProd" runat="server" Style="vertical-align: middle;">
                                       <div>
                                            <asp:ImageButton ID="BtnCloseMasivaProd" runat="server" AlternateText="Cerrar Ventana"
                                                ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" onclick="BtnCloseMasivaProd_Click" 
                                                 />
                                        </div>
                                        <div align="center"> 
                                            <iframe id="IframeCargaMasivaProd" runat="server" height="305px" src="" width="1000px">
                                            </iframe>
                                        </div>
                                        <div style="display: none;">
                                            <asp:GridView ID="GvMasivaProd" runat="server" Height="250px" Visible="False"
                                                Width="100%">
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panelreporteproducto" CssClass="borde_panel" runat="server" BackColor="White" BorderColor="#7F99CC"
                    BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" 
                    Style="display: none;">
                    <div>                       
                        <div><asp:ImageButton ID="ImgCloseVistas" runat="server" BackColor="Transparent" Height="22px"
                            ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" 
                            Width="23px" onclick="ImgCloseVistas_Click" /></div>
                        <div class="centrarcontenido" style=" padding-top: 10px"><asp:Label ID="LblReporteproducto" runat="server" CssClass="labelsN" Text="REPORTES"></asp:Label></div>
                    </div>
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <div style="overflow: auto; width: 250px; height: 140px;">
                                    <asp:RadioButtonList ID="RbtnListInfProd" runat="server">
                                    </asp:RadioButtonList>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="centrarcontenido">
                        <asp:Button ID="BtnSelInfProd" runat="server" CssClass="buttonNewPlan" Width="164px" Height="25px" Text="Seleccionar" OnClick="BtnSelInfProd_Click" />
                        <asp:Button ID="btnPanelMaterialPOP" Visible="false" runat="server" CssClass="buttonNewPlan" Height="25px"
                        Text="Asig. de material POP" Width="164px" OnClick="btnPanelMaterialPOP_Click" />
                        
                    </div>
                    <br />
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPanelreporteproducto" runat="server" BackgroundCssClass="modalBackground"
                    Enabled="True" PopupControlID="Panelreporteproducto" TargetControlID="BtnDisparareporteproducto">
                </cc1:ModalPopupExtender>
                <asp:Button ID="BtnDisparareporteproducto" runat="server" CssClass="alertas" Enabled="False"
                    Height="0px" Text="" Width="0" />
                <%--informacion de asignacion de puntos de venta a operativos--%>
                <asp:Panel ID="PanelAsignacionPDVaoper" runat="server">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td bgcolor="White">
                                    <asp:Panel ID="PanelPDVOpe" runat="server" Style="vertical-align: middle;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <table align="center">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblPanningAsigPDVOPE" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtPlanningAsigPDVOPE" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                                    ForeColor="White" AutoCompleteType="Disabled"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblSelPresupuestoAsigPDVOPE" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="CmbSelPresupuestoAsigPDVOPE" runat="server" AutoPostBack="True"
                                                                    Width="520px" OnSelectedIndexChanged="CmbSelPresupuestoAsigPDVOPE_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblSelOpePlanning" runat="server" CssClass="labelsN" Font-Bold="True"
                                                                    Text="Seleccione Mercaderista"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="CmbSelOpePlanning" runat="server" Width="520px" Enabled="False">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtF_iniPDVOPE"
                                                                                UserDateFormat="DayMonthYear">
                                                                            </cc1:MaskedEditExtender>
                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                                PopupButtonID="BtnCalF_iniPDVOPE" PopupPosition="TopLeft" TargetControlID="TxtF_iniPDVOPE">
                                                                            </cc1:CalendarExtender>
                                                                            <asp:Label ID="LblF_iniPDVOPE" runat="server" CssClass="labelsN" Font-Bold="True"
                                                                                Text="Fecha Inicial"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TxtF_iniPDVOPE" runat="server" Enabled="False" Width="100px"></asp:TextBox>
                                                                            <asp:ImageButton ID="BtnCalF_iniPDVOPE" runat="server" Enabled="False" Height="16px"
                                                                                ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                                                        </td>
                                                                        <td>
                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtF_finPDVOPE"
                                                                                UserDateFormat="DayMonthYear">
                                                                            </cc1:MaskedEditExtender>
                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                                PopupButtonID="BtnCalF_finPDVOPE" PopupPosition="TopLeft" TargetControlID="TxtF_finPDVOPE">
                                                                            </cc1:CalendarExtender>
                                                                            <asp:Label ID="LblF_finPDVOPE" runat="server" CssClass="labelsN" Font-Bold="True"
                                                                                Text="Fecha Final"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TxtF_finPDVOPE" runat="server" Enabled="False" Width="100px"></asp:TextBox>
                                                                            <asp:ImageButton ID="BtnCalF_finPDVOPE" runat="server" Enabled="False" Height="16px"
                                                                                ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <fieldset id="Fieldset5" runat="server">
                                                        <legend style="">Filtros</legend>
                                                        <table align="center">
                                                            <tr>
                                                                <td>
                                                                    <table align="center">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="LblSelCity" runat="server" Text="Ciudad" CssClass="labelsN"></asp:Label>
                                                                                <asp:Label ID="LblOblSelCity" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="CmbSelCity" runat="server" AutoPostBack="True" Width="180px"
                                                                                    Enabled="False" OnSelectedIndexChanged="CmbSelCity_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="LblSelTipoAgrup" runat="server" Text="Tipo Agrupación" CssClass="labelsN"></asp:Label>
                                                                                <asp:Label ID="LblOblSelTipoAgrup" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="CmbSelTipoAgrup" runat="server" AutoPostBack="True" Width="180px"
                                                                                    Enabled="False" OnSelectedIndexChanged="CmbSelTipoAgrup_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="LblSelAgrup" runat="server" Text="Agrupación" CssClass="labelsN"></asp:Label>
                                                                                <asp:Label ID="LblOblSelAgrup" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="CmbSelAgrup" runat="server" AutoPostBack="True" Width="180px"
                                                                                    Enabled="False" OnSelectedIndexChanged="CmbSelAgrup_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="LblSelOficina" runat="server" Text="Oficina" CssClass="labelsN"></asp:Label>
                                                                                <asp:Label ID="LblOblSelOficina" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="CmbSelOficina" runat="server" AutoPostBack="True" Width="180px"
                                                                                    Enabled="False" OnSelectedIndexChanged="CmbSelOficina_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="LblSelMalla" runat="server" Text="Región" CssClass="labelsN"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="CmbSelMalla" runat="server" AutoPostBack="True" Width="180px"
                                                                                    Enabled="False" OnSelectedIndexChanged="CmbSelMalla_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="LblSelSector" runat="server" Text="Zona" CssClass="labelsN"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="CmbSelSector" runat="server" AutoPostBack="True" Width="180px"
                                                                                    Enabled="False" OnSelectedIndexChanged="CmbSelSector_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </table>
                                        <table align="center" width="80%">
                                            <tr>
                                                <td>
                                                    <fieldset id="Fieldset4" runat="server">
                                                        <legend style="">Puntos de Venta</legend>
                                                        <table align="center">
                                                            <tr>
                                                                <td style="width: 400px;">
                                                                    &nbsp;&nbsp;
                                                                    <asp:Button ID="BtnAllPDV" runat="server" CssClass="buttonsinfondo" Text="Todos"
                                                                        Visible="False" OnClick="BtnAllPDV_Click" />
                                                                    <asp:Button ID="BtnNonePDV" runat="server" CssClass="buttonsinfondo" Text="Ninguno"
                                                                        Visible="False" OnClick="BtnNonePDV_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" style="border-color: Black; border-style: solid; border-width: 1px;
                                                                    width: 350px; min-width: 350px; height: 126px;">
                                                                    <asp:CheckBoxList ID="ChkListPDV" runat="server" CssClass="ScrollPersonalPlanning"
                                                                        Font-Names="Verdana" Font-Size="8pt" Height="132px" RepeatLayout="Flow" Width="350px"
                                                                        Enabled="False">
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="BtnAsigPDVOPE" runat="server" CssClass="pagnext" OnClick="BtnAsigPDVOPE_Click"
                                                                        Text=" ." ToolTip="Asignar" Width="25px" Enabled="False" />
                                                                </td>
                                                                <td valign="top" style="border-color: Black; height: 131px; border-style: solid;
                                                                    border-width: 1px; width: 800px; min-width: 800px;">
                                                                    <div style="width: 800px; height: 131px;" class="p">
                                                                        <asp:GridView ID="GvAsignaPDVOPE" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                            Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" Width="785px" OnSelectedIndexChanged="GvAsignaPDVOPE_SelectedIndexChanged">
                                                                            
                                                                            <Columns>
                                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                    ShowSelectButton="True" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
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
                                                    <asp:Button ID="BtnSaveAsigPDVOPE" runat="server" CssClass="buttonSavePlan" Enabled="false"
                                                        Height="25px" Text="Guardar" Width="164px" OnClick="BtnSaveAsigPDVOPE_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnClearAsigPDVOPE" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                        Enabled="false" Height="25px" OnClick="BtnClearAsigPDVOPE_Click" Text="Deshacer"
                                                        Width="164px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnCargaPDVOPE" runat="server" BorderStyle="Solid" CssClass="buttonFilePlan"
                                                        Height="25px" Enabled="false" Text="Desde Archivo..." Width="164px" OnClick="BtnCargaPDVOPE_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PanelCargaMasivaAsignapdv" runat="server">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td >
                                    <asp:Panel ID="PanelMasivaPDVOpe" runat="server" Style="vertical-align: middle;">
                                       <div>
                                            <asp:ImageButton ID="BtnCloseMasivaPDVOPE" runat="server" AlternateText="Cerrar Ventana"
                                                ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" 
                                                onclick="BtnCloseMasivaPDVOPE_Click" />
                                        </div>
                                        <div align="center"> 
                                            <iframe id="IframeMasivaPDVOpe" runat="server" height="305px" src="" width="1100px">
                                            </iframe>
                                        </div>
                                        <div style="display: none;">
                                            <asp:GridView ID="GvMasivapsdvope" runat="server" Height="250px" Visible="False"
                                                Width="100%">
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <%--informacion de reportes de la campaña--%>
                <asp:Panel ID="PanelReportesCampaña" runat="server">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td bgcolor="White">
                                    <asp:Panel ID="PanelReportes" runat="server" Style="vertical-align: middle;">
                                        
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblPanningReportes" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtPlanningReportes" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                        ForeColor="White"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblSelPresupuestoReportes" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                                </td>
                                                <td style="width:100%">
                                                    <asp:DropDownList ID="CmbSelPresupuestoReportes" runat="server" AutoPostBack="True"
                                                        Width="500px" OnSelectedIndexChanged="CmbSelPresupuestoReportes_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    
                                            <asp:ImageButton ID="BtnCarga80" Visible="false" ToolTip="Cargar Archivo 80" 
                                                        runat="server" ImageUrl="~/Pages/images/a80.png" Width="20px" Height="20px" 
                                                        ImageAlign="Right" onclick="BtnCarga80_Click" />
                                            <asp:ImageButton ID="BtnCarga20" Visible="false" ToolTip="Cargar Archivo 20" 
                                                        runat="server" ImageUrl="~/Pages/images/a20.png"  Width="20px" Height="20px" 
                                                        ImageAlign="Right" onclick="BtnCarga20_Click" />
                                      
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                        <table align="center">
                                            <tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="LblSelReportesCampaña" runat="server" Text="Seleccione Informe a usar en la campaña"></asp:Label>
                                                    <br />
                                                    <div style="overflow: auto; width: 250px; height: 150px; border-style: solid; border-width: 1px;">
                                                        <asp:RadioButtonList ID="RBtnListInformes" runat="server" Width="100%">
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </td>
                                                <td style="width: 180px;">
                                                    <asp:Label ID="LblSelAñoCampaña" runat="server" Text="Año"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="CmbSelAñoCampaña" Width="150px" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="CmbSelAñoCampaña_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:Label ID="LblSelMesCampaña" runat="server" Text="Mes"></asp:Label>
                                                    <br />
                                                    <div style="overflow: auto; width: 150px; height: 110px; border-style: solid; border-width: 1px;">
                                                        <asp:RadioButtonList ID="RbtnListmeses" runat="server" AutoPostBack="True" 
                                                            onselectedindexchanged="RbtnListmeses_SelectedIndexChanged">
                                                        </asp:RadioButtonList>
                                                       <%-- <asp:CheckBoxList ID="ChkListMeses" runat="server" Width="100%">
                                                        </asp:CheckBoxList>--%>
                                                    </div>
                                                </td>
                                                <td style="width: 120px;">
                                                    <asp:Label ID="LblSelFrecuencia" runat="server" Text="Periodos"></asp:Label>
                                                    <br />
                                                     <asp:Button ID="BtnAllPer" runat="server" CssClass="buttonsinfondo"
                                                            Text="Todos" Visible="true" onclick="BtnAllPer_Click" />
                                                        <asp:Button ID="BtnNonePer" runat="server" CssClass="buttonsinfondo" 
                                                            Text="Ninguno" Visible="true" onclick="BtnNonePer_Click" />
                                                        <br />
                                                    <div style="overflow: auto; width: 120px; height: 130px; border-style: solid; border-width: 1px;">
                                                        <asp:CheckBoxList ID="ChklstFrecuencia" runat="server" Width="100%">                                                          
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td align="center" valign="top" width="30px">
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="BtnAddFrecuencia" runat="server" CssClass="pagnext" Text=" ." ToolTip="Asignar"
                                                        Width="25px" OnClick="BtnAddFrecuencia_Click" />
                                                </td>
                                                <td style="width: 600px;">
                                                    <asp:Label ID="LblAsigInfor" runat="server" Text="Asignaciones"></asp:Label>
                                                    <br />
                                                    <div style="overflow: auto; width: 600px; height: 150px; border-style: solid; border-width: 1px;">
                                                        <asp:GridView ID="GVFrecuencias" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                            Font-Names="Verdana" Font-Size="8pt" GridLines="None" AutoGenerateColumns="False"
                                                            OnSelectedIndexChanged="GVFrecuencias_SelectedIndexChanged" EnableModelValidation="True">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                    ShowSelectButton="True" />
                                                                <asp:BoundField DataField="Cod_" HeaderText="Cod_" />
                                                                <asp:BoundField DataField="Informe" HeaderText="Informe" ItemStyle-Width="100px"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Mes_" HeaderText="Mes_" />
                                                                <asp:BoundField DataField="Asignado" HeaderText="Asignado" ItemStyle-Width="80px"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Año" HeaderText="Año" HeaderStyle-HorizontalAlign="Left">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Periodos" HeaderText="Periodos" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Fecha inicio">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TxtFechaini" runat="server" Width="90px" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="TxtFechaini_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechaini"
                                                                            UserDateFormat="DayMonthYear">
                                                                        </cc1:MaskedEditExtender>
                                                                        <%-- <asp:ImageButton ID="BtnCalTxtFechaini" runat="server" Height="16px" ImageUrl="~/Pages/images/calendario.JPG"
                                                                            Width="16px" />
                                                                        <cc1:CalendarExtender ID="CalExtTxtFechaini" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                            PopupButtonID="BtnCalTxtFechaini" PopupPosition="TopLeft" TargetControlID="TxtFechaini">
                                                                        </cc1:CalendarExtender>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Fecha fin">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TxtFechafin" runat="server" Width="90px" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="TxtFechafin_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechafin"
                                                                            UserDateFormat="DayMonthYear">
                                                                        </cc1:MaskedEditExtender>
                                                                        <%-- <asp:ImageButton ID="BtnCalTxtFechafin" runat="server" Height="16px" ImageUrl="~/Pages/images/calendario.JPG"
                                                                            Width="16px" />
                                                                        <cc1:CalendarExtender ID="CalExtTxtFechafin" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                            PopupButtonID="BtnCalTxtFechafin" PopupPosition="TopLeft" TargetControlID="TxtFechafin">
                                                                        </cc1:CalendarExtender>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnSaveReportes" runat="server" CssClass="buttonSavePlan" Enabled="false"
                                                        Height="25px" Text="Guardar" Width="164px" OnClick="BtnSaveReportes_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnClearReportes" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                        Height="25px" Text="Deshacer" Width="164px" OnClick="BtnClearReportes_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>


                 <!-----------------------Panel Punto de Venta------------------------------------>
                 <asp:Panel ID="Panel2" runat="server">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td bgcolor="White">
                                    <asp:Panel ID="panelPuntoVenta" runat="server" Style="vertical-align: middle;">
                                        
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtPlanningPanelPuntoVenta" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                        ForeColor="White"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                                </td>
                                                <td style="width:100%">
                                                    <asp:DropDownList ID="CmbSelPresupuestoPanelesPtoVenta" runat="server" AutoPostBack="True"
                                                        Width="500px" OnSelectedIndexChanged="CmbSelPresupuestoPanelesPtoVenta_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    
                                            <asp:ImageButton ID="ImageButton1" Visible="false" ToolTip="Cargar Archivo 80" 
                                                        runat="server" ImageUrl="~/Pages/images/a80.png" Width="20px" Height="20px" 
                                                        ImageAlign="Right" onclick="BtnCarga80_Click" />
                                            <asp:ImageButton ID="ImageButton2" Visible="false" ToolTip="Cargar Archivo 20" 
                                                        runat="server" ImageUrl="~/Pages/images/a20.png"  Width="20px" Height="20px" 
                                                        ImageAlign="Right" onclick="BtnCarga20_Click" />
                                      
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                        <table align="center">
                                            <tr>
                                                <td style="width: 300px;">
                                                    
                                                    
                                                    <div style="overflow: auto; width: 200px; height: 250px; border-style: solid; border-width: 1px;">
                                                    <asp:Label ID="Label6" runat="server" Text="Informes"></asp:Label>
                                                    <asp:DropDownList ID="RBtnListPanelPtoVenta" runat="server" Width="200px"  OnSelectedIndexChanged="RBtnListPanelPtoVenta_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="Label16" runat="server" Text="Punto de venta"></asp:Label>
                                                    <asp:DropDownList ID="ddlPanelPtoventa" Width="200px" runat="server"  >
                                                    </asp:DropDownList>
                                                     <asp:Label ID="Label13" runat="server" Text="Año"></asp:Label>
                                                    <asp:DropDownList ID="ddlPanelPtoventa_Año" Width="200px" runat="server" >
                                                    </asp:DropDownList>
                                                    <asp:Label ID="Label15" runat="server" Text="Mes"></asp:Label>
                                                    <asp:DropDownList ID="ddlPanelPtoventa_Mes" Width="200px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanelPtoventa_Mes_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                    <asp:Label ID="Label14" runat="server" Text="Periodos"></asp:Label>
                                                    <asp:DropDownList ID="ddlPanelPtoventa_Periodo" Width="200px" runat="server" >
                                                    </asp:DropDownList>
                                                        <%--<asp:RadioButtonList ID="RBtnListPanelPtoVenta" runat="server" Width="100%"  onselectedindexchanged="RBtnListPanelPtoVenta_SelectedIndexChanged"  AutoPostBack="true" >
                                                        </asp:RadioButtonList>--%>
                                                    </div>
                                                </td>
                                                       <td align="center" valign="top" width="30px">
                                                   
                                                    <div id="div_masopciones1" runat="server"  style=" display:none;"  >
                                        <table align="center"> 
                                            <tr>
                                                <td>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label7" runat="server" Text="Seleccione opción:"></asp:Label>
                                                    <br />
                                                     <asp:RadioButtonList ID="RbtMasopciones1" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="RbtMasopciones1_SelectedIndexChanged"  >                                            
                                                    </asp:RadioButtonList>
                                                </td> 
                                            </tr>
                                        </table>
                                           
                                        </div>
                                                   

                                                </td>
                                         
                                                <td>
                                                   
                                                  
                                                        <!------------------------------------------------------------------ ------>
                                        <div id="panel_contenedor1" class="autosize" style=" padding: 5px">
                                            <div id="categorias1" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">Categorias</span></div>
                                                    <br />
                                                    <asp:RadioButtonList ID="rbliscatego1" runat="server" AutoPostBack="True" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" OnSelectedIndexChanged="rbliscatego1_SelectedIndexChanged"
                                                        RepeatLayout="Flow" Width="250px">
                                                    </asp:RadioButtonList>
                                                    <asp:CheckBoxList ID="Chklistcatego1" runat="server" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" 
                                                         RepeatLayout="Flow" Width="250px">
                                                    </asp:CheckBoxList>
                                            </div> 
                                            <div id="Marcas1" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">Marca</span></div>
                                                    <br />
                                                    <asp:RadioButtonList ID="rblmarca1" runat="server" AutoPostBack="True" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" OnSelectedIndexChanged="rblmarca1_SelectedIndexChanged"
                                                        RepeatLayout="Flow" Width="250px">
                                                    </asp:RadioButtonList>
                                                    <asp:CheckBoxList ID="Chklistmarca1" runat="server" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" 
                                                         RepeatLayout="Flow" Width="250px">
                                                    </asp:CheckBoxList>
                                            </div> 
                                            <div id="submarcas1" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><asp:Label ID="Label9" runat="server" CssClass="labelsN" Text="SubMarca"></asp:Label></div>
                                                    <br />
                                                    <asp:RadioButtonList ID="rblsubmarca1" runat="server" AutoPostBack="True" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" OnSelectedIndexChanged="rblsubmarca1_SelectedIndexChanged"
                                                        RepeatLayout="Flow" Width="250px">
                                                    </asp:RadioButtonList>
                                            </div> 
                                            <div id="Familias1" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">Familias</span></div>
                                                    <br />
                                                    <asp:RadioButtonList ID="rblfamilia1" runat="server" AutoPostBack="True" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px"
                                                        RepeatLayout="Flow" Width="250px" 
                                                        onselectedindexchanged="rblfamilia1_SelectedIndexChanged">
                                                    </asp:RadioButtonList>                                                 
                                                    <asp:CheckBoxList ID="ChkListFamilias1" runat="server" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" 
                                                         RepeatLayout="Flow" Width="250px">
                                                    </asp:CheckBoxList>
                                            </div> 
                                            <div id="SubFamilias1" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">SubFamilias</span></div>
                                                    <br />
                                                    <asp:RadioButtonList ID="rblsubfamilia1" runat="server" AutoPostBack="True" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px"
                                                        RepeatLayout="Flow" Width="250px" 
                                                        onselectedindexchanged="rblsubfamilia1_SelectedIndexChanged">
                                                    </asp:RadioButtonList>
                                                    <asp:CheckBoxList ID="ChkListSubFamilias1" runat="server" CssClass="p"
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" 
                                                         RepeatLayout="Flow" Width="250px">
                                                    </asp:CheckBoxList>
                                            </div> 
                                            <div id="productos1" runat="server" class="panel_vista autosize borde_panel">
                                                    <div class="centrarcontenido"><span class="labelsN">Productos</span></div>
                                                    <br />
                                                    <asp:CheckBoxList ID="ChkProductos1" runat="server" CssClass="p" Font-Names="Verdana"
                                                        Font-Size="8pt" Height="130px" RepeatLayout="Flow" Width="380px">
                                                    </asp:CheckBoxList>
                                            </div> 
                                        </div>

                                        <!------------------------------------------------------------------ ------>
                                                       <%--    <asp:CheckBoxList ID="chkPanelPtoVenta_Produtos" runat="server" Width="100%">                                                          
                                                        </asp:CheckBoxList>--%>
                                                
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />

                                        

                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnSavePanelPtoVenta" runat="server" CssClass="buttonSavePlan" Enabled="false"
                                                        Height="25px" Text="Guardar" Width="164px" OnClick="BtnSavePanelPtoVenta_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button5" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                        Height="25px" Text="Deshacer" Width="164px" OnClick="BtnClearReportes_Click" />
                                                </td>
                                                <td>
                                                <asp:Button ID="BtnMasivoPanelPtoVenta" runat="server" BorderStyle="Solid" CssClass="buttonFilePlan"
                                                        Height="25px" Enabled="false" Text="Desde Archivo..." Width="164px" OnClick="BtnMasivoPanelPtoVenta_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                     </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>


                <!------------------------------------------------------------------------------------------------------------------------------------>

                   <asp:Panel ID="Panel3" runat="server">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td bgcolor="White">

                                    <asp:Panel ID="Panel_AsignacionMaterial_POP" runat="server" Style="vertical-align: middle;">
                                        <table>
                                        <tr>
                                        <td>
                                       <asp:TextBox ID="txtAsignasionMatePOP" runat="server" Enabled="false" ></asp:TextBox>
                                        </td>
                                        <td>
                                        
                                         <asp:DropDownList ID="ddlAsignasionMatePOP" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlAsignasionMatePOP_SelectedIndexChanged" Width="500px">
                                                    </asp:DropDownList>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td>
                                        <asp:RadioButtonList ID="rblAsignasionMatePOP" runat="server" AutoPostBack="True" 
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" OnSelectedIndexChanged="rblAsignasionMatePOP_SelectedIndexChanged"
                                                        RepeatLayout="Flow" Width="250px">
                                                    </asp:RadioButtonList>
                                        </td>
                                        <td>
                                        <asp:CheckBoxList ID="chkAsignasionMatePOP" runat="server" 
                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" 
                                                         RepeatLayout="Flow" Width="250px">
                                                    </asp:CheckBoxList>
                                        </td>
                                        </tr>
                                        </table> <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnGuardarAsignasionMatePOP" runat="server" CssClass="buttonSavePlan" Enabled="false"
                                                        Height="25px" Text="Guardar" Width="164px" OnClick="btnGuardarAsignasionMatePOP_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnLimpiarAsignasionMatePOP" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                        Height="25px" Text="Deshacer" Width="164px" OnClick="btnLimpiarAsignasionMatePOP_Click" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>                                       <div style="display: none;">
                                            <asp:GridView ID="GridView2" runat="server" Height="250px" Visible="False" Width="100%">
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>

                <!------------------------------------------------------------------------------------------------------------------------------------->

                


                <!----------------------------------------------------------------------------------------------------------------------------------------->



                <asp:Panel ID="PanelPtoVenta_Masivo" runat="server">
                    <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td >
                                    <asp:Panel ID="Panel4" runat="server" Style="vertical-align: middle;">
                                       <div>
                                            <asp:ImageButton ID="BtnCloseMasivaPanelPtoVenta" runat="server" AlternateText="Cerrar Ventana"
                                                ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" 
                                                onclick="BtnCloseMasivaPanelPtoVenta_Click" />
                                        </div>
                                        <div align="center"> 
                                            <iframe id="IframePanelPtoVenta_Masiva" runat="server" height="305px" src="" width="1100px">
                                            </iframe>
                                        </div>
                                        <div style="display: none;">
                                            <asp:GridView ID="GridView1" runat="server" Height="250px" Visible="False"
                                                Width="100%">
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>


                <asp:Panel ID="PanelCarga2080" runat="server">
               <div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                        width="6"> </img>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                    </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                            <tr>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                                <td >
                                    <asp:Panel ID="PanelMasiva2080" runat="server" Style="vertical-align: middle;">
                                       <div>
                                            <asp:ImageButton ID="BtnCloseMasiva2080" runat="server" AlternateText="Cerrar Ventana"
                                                ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" 
                                                onclick="BtnCloseMasiva2080_Click"/>
                                        </div>
                                        <div align="center"> 
                                            <iframe id="IframeMasiva2080" runat="server" height="335px" src="" width="1000px">
                                            </iframe>
                                        </div>
                                        <div style="display: none;">
                                            <asp:GridView ID="GvMasiva2080" runat="server" Height="250px" Visible="False"
                                                Width="100%">
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </td>
                                <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                    </img>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="6"> </img>
                                </td>
                                <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                        width="1"> </img>
                                </td>
                                <td>
                                    <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                        width="6"> </img>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
               
                <%--panel de mensaje de usuario   --%>
                <asp:Panel ID="PCanal" runat="server" Height="169px" Style="display: none;" Width="332px">
                    <table align="center">
                        <tr>
                            <td align="center" style="height: 119px; width: 79px;" valign="top">
                                <br />
                            </td>
                            <td style="width: 220px; height: 119px;" valign="top">
                                <br />
                                <asp:Label ID="lblencabezado" runat="server" CssClass="labelsTit"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblmensajegeneral" runat="server" CssClass="labels"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table align="center" width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnaceptar" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                    Text="Aceptar" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupCanal" runat="server" BackgroundCssClass="modalBackground"
                    Enabled="True" PopupControlID="PCanal" TargetControlID="btndipararalerta">
                </cc1:ModalPopupExtender>

                 <%--panel de mensaje de usuario valida vista   --%>
                <asp:Panel ID="PMensajeVista" runat="server" Height="169px" Style="display: none;" Width="332px">
                    <table align="center">
                        <tr>
                            <td align="center" style="height: 119px; width: 79px;" valign="top">
                                <br />
                            </td>
                            <td style="width: 220px; height: 119px;" valign="top">
                                <br />
                                <asp:Label ID="LblTitMensajeVista" runat="server" CssClass="labelsTit"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="LblMsjMensajeVista" runat="server" CssClass="labels"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table align="center" width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="BtnAceptaMensajeVista" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                    Text="Aceptar" onclick="BtnAceptaMensajeVista_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalMensajeVista" runat="server" BackgroundCssClass="modalBackground"
                    Enabled="True" PopupControlID="PMensajeVista" TargetControlID="btndipararalertavista">
                </cc1:ModalPopupExtender>
                <%--panel de mensaje de usuario en productos
               <asp:Panel ID="PanelMensajeProducto" runat="server" Height="169px" Width="332px"
                    Style="display: none;">
                    <table align="center">
                        <tr>
                            <td align="center" style="height: 119px; width: 79px;" valign="top">
                                <br />
                            </td>
                            <td style="width: 238px; height: 119px;" valign="top">
                                <br />
                                <asp:Label ID="LblTitMensaje" runat="server" CssClass="labelsTit"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="LblcontenMensaje" runat="server" CssClass="labels"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td align="center">
                                <asp:Button ID="BtnAceptMensaje" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                    Text="Aceptar" OnClick="BtnAceptMensaje_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupMensajeProd" runat="server" Enabled="True"
                    TargetControlID="btndipararmensaje" PopupControlID="PanelMensajeProducto" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                panel de mensaje de usuario en productos competencia
                <asp:Panel ID="PanelMensajeProductoCompe" runat="server" Height="169px" Width="332px"
                    Style="display: none;">
                    <table align="center">
                        <tr>
                            <td align="center" style="height: 119px; width: 79px;" valign="top">
                                <br />
                            </td>
                            <td style="width: 238px; height: 119px;" valign="top">
                                <br />
                                <asp:Label ID="LblTitMensajeCompe" runat="server" CssClass="labelsTit"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="LblcontenMensajeCompe" runat="server" CssClass="labels"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td align="center">
                                <asp:Button ID="BtnAceptaMensajeCompe" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                    Text="Aceptar" OnClick="BtnAceptaMensajeCompe_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupMensajeProdCompe" runat="server" Enabled="True"
                    TargetControlID="btndipararmensajecompe" PopupControlID="PanelMensajeProductoCompe"
                    BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3" AssociatedUpdatePanelID="UpPlanning" BackgroundCssClass="modalProgressGreyBackground">
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
    </div>
    <div class="Regresa" align="left">
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="BtnRegresar" runat="server" CssClass="Regresar" OnClick="BtnRegresar_Click" />
    </div>
   
    </form>
</body>
</html>
