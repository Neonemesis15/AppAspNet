<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cargar_Informes.aspx.cs"
    Debug="true" Inherits="SIGE.Pages.Modulos.Planning.Cargar_Informes" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title> 
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        </style>
</head>
<body class="backcolorxplora">
    <form id="form1" runat="server">
    <asp:Label ID="usersession" runat="server" Visible="false"></asp:Label>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="center">
                <asp:Button ID="BtnCrearPlanning" runat="server" CssClass="buttonPlan" Text="&lt;&lt; Nuevo Planning &gt;&gt;"
                    OnClick="BtnCrearPlanning_Click" Width="200px" Visible="False" />
                <asp:Button ID="btnSerch" runat="server" CssClass="buttonPlan" Text="&lt;&lt; Cargar informes &gt;&gt;"
                    OnClick="btnSerch_Click" Width="200px" />
                <asp:Button ID="BtnBuscarInforme" runat="server" CssClass="buttonPlan" Text="&lt;&lt; Buscar informes &gt;&gt;"
                    Width="200px" />
            </div>
            <br />
            <table bgcolor="#7F99CC" style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="LblTitCargarArchivo" runat="server" CssClass="labels"></asp:Label>
                    </td>
                </tr>
            </table>
            <%--barra de progreso que indica al usuario que se esta procesando--%>
            <table align="center">
                <tr>
                    <td>
                        <asp:Label ID="LblUpProgressUP" runat="server" Text="." ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Panel ID="Panel1" runat="server">
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
            <%-- <asp:Panel ID="InfoPlanningBasico" runat="server" BackColor="Silver" BorderStyle="Solid"
                BorderWidth="1px" BorderColor="Black" style="display:none;">
                <cc1:TabContainer ID="TabContainerPlanning" runat="server" ActiveTabIndex="0" Width="100%">
                    <cc1:TabPanel ID="TabInfobasica" runat="server" HeaderText="Información Básica">
                        <HeaderTemplate>
                            Información Básica</HeaderTemplate>
                        <ContentTemplate>
                            <table align="center" frame="box">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNumpla" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
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
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="lblpresu" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:DropDownList ID="cmbpresupuesto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbpresupuesto_SelectedIndexChanged"
                                            Width="500px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblnameplaning" runat="server" CssClass="labelsN" Text="Campaña"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtnamepresu" runat="server" BackColor="#CCCCCC" Enabled="False"
                                            Width="300px"></asp:TextBox>
                                    </td>
                                    <td colspan="2" valign="top">
                                        <asp:Label ID="lblfechsoli" runat="server" CssClass="labelsN" Text="Fecha de Solicitud"></asp:Label>
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="TxtFechaSolicitud" runat="server" AutoPostBack="True" CausesValidation="True"
                                            Enabled="False" OnTextChanged="TxtFechaSolicitud_TextChanged"></asp:TextBox><cc1:MaskedEditExtender
                                                ID="TxtFechaSolicitud_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechaSolicitud"
                                                UserDateFormat="DayMonthYear">
                                            </cc1:MaskedEditExtender>
                                        <cc1:CalendarExtender ID="TxtFechaSolicitud_CalendarExtender" runat="server" Enabled="True"
                                            PopupButtonID="ImgBtnCal" TargetControlID="TxtFechaSolicitud">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="ImgBtnCal" runat="server" Enabled="False" Height="16px" ImageUrl="~/Pages/images/calendario.JPG"
                                            Width="16px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblfecentregafin" runat="server" CssClass="labelsN" Text="Fecha Entrega Final"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtFechaEntrega" runat="server" AutoPostBack="True" CausesValidation="True"
                                            Enabled="False" OnTextChanged="TxtFechaEntrega_TextChanged"></asp:TextBox><cc1:MaskedEditExtender
                                                ID="TxtFechaEntrega_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechaEntrega"
                                                UserDateFormat="DayMonthYear">
                                            </cc1:MaskedEditExtender>
                                        <cc1:CalendarExtender ID="TxtFechaEntrega_CalendarExtender" runat="server" Enabled="True"
                                            PopupButtonID="ImgBtnCal1" TargetControlID="TxtFechaEntrega">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="ImgBtnCal1" runat="server" Enabled="False" Height="16px" ImageUrl="~/Pages/images/calendario.JPG"
                                            Width="16px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
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
                                        <asp:TextBox ID="TxtFechainipre" runat="server" AutoPostBack="True" CausesValidation="True"
                                            Enabled="False" OnTextChanged="TxtFechainipre_TextChanged"></asp:TextBox><cc1:MaskedEditExtender
                                                ID="TxtFechainipre_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechainipre"
                                                UserDateFormat="DayMonthYear">
                                            </cc1:MaskedEditExtender>
                                        <cc1:CalendarExtender ID="TxtFechainipre_CalendarExtender" runat="server" Enabled="True"
                                            PopupButtonID="ImgBtnCal2" TargetControlID="TxtFechainipre">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="ImgBtnCal2" runat="server" Enabled="False" Height="16px" ImageUrl="~/Pages/images/calendario.JPG"
                                            Width="16px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblfefinpre" runat="server" CssClass="labelsN" Text="Fin"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtFechafinpre" runat="server" AutoPostBack="True" CausesValidation="True"
                                            Enabled="False" OnTextChanged="TxtFechafinpre_TextChanged"></asp:TextBox><cc1:MaskedEditExtender
                                                ID="TxtFechafinpre_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechafinpre"
                                                UserDateFormat="DayMonthYear">
                                            </cc1:MaskedEditExtender>
                                        <cc1:CalendarExtender ID="TxtFechafinpre_CalendarExtender" runat="server" Enabled="True"
                                            PopupButtonID="ImgBtnCal3" TargetControlID="TxtFechafinpre">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="ImgBtnCal3" runat="server" Enabled="False" Height="16px" ImageUrl="~/Pages/images/calendario.JPG"
                                            Width="16px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
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
                                        <asp:TextBox ID="TxtFechainiPla" runat="server" AutoPostBack="True" CausesValidation="True"
                                            Enabled="False" OnTextChanged="TxtFechainiPla_TextChanged"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="TxtFechainiPla_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechainiPla"
                                            UserDateFormat="DayMonthYear">
                                        </cc1:MaskedEditExtender>
                                        <cc1:CalendarExtender ID="TxtFechainiPla_CalendarExtender" runat="server" Enabled="True"
                                            PopupButtonID="ImgBtnCal4" TargetControlID="TxtFechainiPla">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="ImgBtnCal4" runat="server" Enabled="False" Height="16px" ImageUrl="~/Pages/images/calendario.JPG"
                                            Width="16px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblfecfin" runat="server" CssClass="labelsN" Text="Fin"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtFechaplanfin" runat="server" AutoPostBack="True" CausesValidation="True"
                                            Enabled="False" OnTextChanged="TxtFechaplanfin_TextChanged"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="TxtFechaplanfin_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechaplanfin"
                                            UserDateFormat="DayMonthYear">
                                        </cc1:MaskedEditExtender>
                                        <cc1:CalendarExtender ID="TxtFechaplanfin_CalendarExtender" runat="server" Enabled="True"
                                            PopupButtonID="ImgBtnCal5" TargetControlID="TxtFechaplanfin">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="ImgBtnCal5" runat="server" Enabled="False" Height="16px" ImageUrl="~/Pages/images/calendario.JPG"
                                            Width="16px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center" frame="box">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblcontacto" runat="server" CssClass="labelsN" Text="Contacto"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcontacto" runat="server" MaxLength="60" Width="300px" Enabled="False"></asp:TextBox><asp:Label
                                            ID="lblolbli17" runat="server" ForeColor="#CC3300" Text="*"></asp:Label><asp:RegularExpressionValidator
                                                ID="Reqcontacto" runat="server" ControlToValidate="txtcontacto" Display="None"
                                                ErrorMessage="No ingrese caracteres especiales ni nùmeros &lt;br /&gt; No inicie con espacio en blanco "
                                                ValidationExpression="([a-zA-Z]{1,1}[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{0,59})"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender ID="Reqcontacto_ValidatorCalloutExtender" runat="server"
                                            Enabled="True" TargetControlID="Reqcontacto">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblarea" runat="server" CssClass="labelsN" Text="Area Involucrada"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtarea" runat="server" Width="300px" Enabled="False"></asp:TextBox><asp:Label
                                            ID="lblolbli27" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblmailconta" runat="server" CssClass="labelsN" Text="Email"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtmail" runat="server" MaxLength="255" Width="300px" Enabled="False"></asp:TextBox><asp:Label
                                            ID="lblolbli25" runat="server" ForeColor="#CC3300" Text="*"></asp:Label><asp:RegularExpressionValidator
                                                ID="ReqMailConta" runat="server" ControlToValidate="txtmail" Display="None" ErrorMessage="Formato de email invalido: Este debe ser user@mail.xyz"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender ID="Validamailconta" runat="server" Enabled="True"
                                            TargetControlID="ReqMailConta">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <div align="center">
                                <asp:Button ID="BtnSavePlanning" runat="server" CssClass="buttonPlan" OnClick="BtnSavePlanning_Click"
                                    Text="Continuar &gt;&gt;" Visible="False" /></div>
                            <br />
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabCaracteristicas" runat="server" HeaderText="Características">
                        <HeaderTemplate>
                            Características</HeaderTemplate>
                        <ContentTemplate>
                            <table align="center">
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="lblobj" runat="server" CssClass="labelsN" Text="Objetivo de la Campaña"></asp:Label><asp:Label
                                            ID="lblolbli13" runat="server" ForeColor="#CC3300" Text="*"></asp:Label><br />
                                        <asp:TextBox ID="txtobj" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="150px"
                                            TextMode="MultiLine" Width="250px"></asp:TextBox>
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="lblmanda" runat="server" CssClass="labelsN" Text="Mandatorios de  Campaña"></asp:Label><asp:Label
                                            ID="lblolbli14" runat="server" ForeColor="#CC3300" Text="*"></asp:Label><br />
                                        <asp:TextBox ID="txtmanda" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="150px"
                                            TextMode="MultiLine" Width="250px"></asp:TextBox>
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="lblmeca" runat="server" CssClass="labelsN" Text="Mecanica"></asp:Label><asp:Label
                                            ID="lblolbli15" runat="server" ForeColor="#CC3300" Text="*"></asp:Label><br />
                                        <asp:TextBox ID="Ttxtmeca" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="150px"
                                            TextMode="MultiLine" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div align="center">
                                <asp:Button ID="BtnFinalizarPlanning" runat="server" Text="Finalizar" CssClass="buttonPlan"
                                    OnClick="BtnFinalizarPlanning_Click" /></div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </asp:Panel>--%>
            <asp:Panel ID="PConsulta" runat="server" CssClass="CargaArchivos" Font-Names="Verdana"
                Font-Size="12pt">
                
                <table >
                    <tr>
                        <td valign="top">
                            <asp:Label ID="lblcampaña" runat="server" Text="Seleccione Campaña" Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                        </td>
                        <td valign="top">
                            <asp:DropDownList ID="cmbPlanning" runat="server" Font-Names="Verdana" Font-Size="10pt"
                                AutoPostBack="True" OnSelectedIndexChanged="cmbPlanning_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table border="1" width="100%">
                    <tr>
                        <td  valign="top" style=" width:300px" >
                            <table style="height: 140px">                               
                                <tr style="height: 35px">
                                    <td valign="top">
                                        <asp:Label ID="LblCliente" runat="server" Text="Cliente" Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                                    </td>
                                    <td valign="top">
                                        <asp:CheckBoxList ID="RbtnCliente" runat="server" Font-Names="Verdana" Font-Size="10pt"
                                            Enabled="False">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr style="height: 35px">
                                    <td valign="top">
                                        <asp:Label ID="lblservices" runat="server" Text="Servicio:" Font-Names="Verdana"
                                            Font-Size="10pt"></asp:Label>
                                    </td>
                                    <td valign="top">
                                        <asp:CheckBoxList ID="rblservice" runat="server" Font-Names="Verdana" Font-Size="10pt"
                                            Enabled="False">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr style="height: 35px">
                                    <td valign="top">
                                        <asp:Label ID="LblCanales" runat="server" Text="Canal:" Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="ChkListCanal" runat="server" Font-Names="Verdana" Font-Size="10pt"
                                            Enabled="False">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                 <tr style="height: 55px">
                                    <td valign="top">
                                        <asp:Label ID="LblSubCanal" runat="server" Text="SubCanal:" Font-Names="Verdana" Font-Size="10pt"
                                         Visible="false"></asp:Label>
                                    </td>
                                    <td>                                        
                                         <div class="p" style="width: 210px; height: 50px; background-color:transparent; ">
                                            <asp:RadioButtonList ID="RbtnSubcanales" runat="server" Font-Names="Verdana" 
                                                Font-Size="10pt" Visible="false" AutoPostBack="True" 
                                                onselectedindexchanged="RbtnSubcanales_SelectedIndexChanged">
                                            </asp:RadioButtonList>
                                         </div>
                                    </td>
                                </tr>
                                <tr style="height: 35px">
                                    <td valign="top">
                                        <asp:Label ID="LblSubnivel" runat="server" Text="SubNivel:" Font-Names="Verdana" Font-Size="10pt"
                                         Visible="false"></asp:Label>
                                    </td>
                                    <td>                                        
                                         <div class="p" style="width: 210px; height: 50px; background-color:transparent; ">
                                            <asp:RadioButtonList ID="RbtnSubNivel" runat="server" Font-Names="Verdana" 
                                                Font-Size="10pt" Visible="false" AutoPostBack="True" 
                                                 onselectedindexchanged="RbtnSubNivel_SelectedIndexChanged">
                                            </asp:RadioButtonList>
                                         </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <table align="center">
                                <tr>
                                    <td>
                                        <table align="center">
                                            <tr>
                                               
                                                <td valign="top" style="height: 150px; width: 360px;">
                                                    <asp:Label ID="LblReportes" runat="server" Text="Indique Reporte:" Font-Names="Verdana"
                                                        Font-Size="10pt"></asp:Label>
                                                    <br />
                                                    <div class="p" style="width: 360px; height: 150px;">
                                                        <asp:RadioButtonList ID="RbtnListReport" runat="server" Font-Names="Verdana" Font-Size="10pt"
                                                            AutoPostBack="True" OnSelectedIndexChanged="RbtnListReport_SelectedIndexChanged">
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </td>
                                                <td valign="top" style="height: 150px; width: 360px;">
                                                    <asp:Label ID="lblcity" runat="server" Font-Names="verdana" Font-Size="10pt" Text="Cobertura:"></asp:Label>
                                                    <br />
                                                    <div class="p" style="width: 360px; height: 150px;">
                                                        <asp:Button ID="BtnAllCobertura" runat="server" CssClass="buttonsinfondo" Text="Todas"
                                                            Visible="false" OnClick="BtnAllCobertura_Click" />
                                                        <asp:Button ID="BtnNone" runat="server" CssClass="buttonsinfondo" Text="Ninguno"
                                                            Visible="false" OnClick="BtnNone_Click" />
                                                        <asp:CheckBoxList ID="Chkcityorig" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                        <asp:Label ID="LblNombReporte" runat="server" Text="Nombre del informe" Font-Names="Verdana"
                                            Font-Size="10pt"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TxtNameReport" runat="server" MaxLength="100" Width="300px" Font-Names="Verdana"
                                            Font-Size="10pt"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblCargarArchivo" runat="server" Text="Archivo:" Font-Names="Verdana"
                                            Font-Size="10pt" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:FileUpload ID="FileUpPDV" runat="server" Width="350px" Font-Names="Verdana"
                                                    Font-Size="10pt" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblMesInforme" runat="server" Font-Names="verdana" Font-Size="10pt"
                                            Text="Mes informe"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CmbSelMes" runat="server" AppendDataBoundItems="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
               
                <br />
                <br />
                <br />
                <div align="center">
                    <asp:Button ID="BtnSaveInfo" runat="server" CssClass="buttonPlan" OnClick="BtnSaveInfo_Click"
                        Text="Guardar Informe" />
                </div>
                <br />
                <br />
            </asp:Panel>
            <%--panel de mensaje de usuario   --%>
            <asp:Button ID="btndisparaalertaelimina" runat="server" Text="" Height="0px" CssClass="alertas"
                Width="0" Enabled="false" />
            <asp:Panel ID="PanelMensajeUsuarioElimina" runat="server" Height="169px" Width="332px"
                Style="display: none;">
                <table align="center">
                    <tr>
                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                            <br />
                        </td>
                        <td style="width: 238px; height: 119px;" valign="top">
                            <br />
                            <asp:Label ID="Label3" runat="server" CssClass="labelsTit"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="Label4" runat="server" CssClass="labels"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td align="center">
                            <asp:Button ID="BtnAceptaMensajeElimina" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                Text="Aceptar" OnClick="BtnAceptaMensajeElimina_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupMensajeEliminar" runat="server" Enabled="True"
                TargetControlID="btndisparaalertaelimina" PopupControlID="PanelMensajeUsuarioElimina"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Button ID="btndipararalerta" runat="server" Text="" Height="0px" CssClass="alertas"
                Width="0" Enabled="false" />
            <asp:Panel ID="PanelMensajesUsuario" runat="server" Height="169px" Width="332px"
                Style="display: none;">
                <table align="center">
                    <tr>
                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                            <br />
                        </td>
                        <td style="width: 238px; height: 119px;" valign="top">
                            <br />
                            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="White"
                                Font-Bold="true"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td align="center">
                            <asp:Button ID="Button3" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                Text="Aceptar" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupCanal" runat="server" Enabled="True" TargetControlID="btndipararalerta"
                PopupControlID="PanelMensajesUsuario" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Button ID="BtnConfirmacion" runat="server" Text="" Height="0px" CssClass="alertas"
                Width="0" Enabled="false" />
            <asp:Panel ID="PanelConfirmacion" runat="server" Width="332px" CssClass="altoverow"
                Style="display: none;">
                <table align="center" style="width: 95%;">
                    <tr>
                        <td align="center" valign="top">
                            <br />
                            <asp:Label ID="LblSrUsuario" runat="server" Text="Sr. Usuario"></asp:Label>
                            <br />
                            <asp:Label ID="LblMensajeConfirm" runat="server"></asp:Label>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="BtnSiConfirma" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                            Text="SI" OnClick="BtnSiConfirma_Click" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="BtnNoConfirma" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                            Text="NO" OnClick="BtnNoConfirma_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalConfirmacion" runat="server" Enabled="True" TargetControlID="BtnConfirmacion"
                PopupControlID="PanelConfirmacion" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelBuscaInforme" runat="server" Font-Names="Verdana" Font-Size="10pt"
                Height="340px" BackColor="White" BorderColor="#7F99CC" BorderStyle="Solid" BorderWidth="6px"
                Style="display: block;">
                <div>
                    <asp:Label ID="LblListadoinformes" runat="server" Text="Informes Cargados"></asp:Label>
                    <asp:ImageButton ID="BtnCOlv" runat="server" AlternateText="Oprima aquí para cancelar solicitud"
                        BackColor="Transparent" Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png"
                        Width="23px" OnClick="BtnCOlv_Click" />
                </div>
                <br />
                <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="LblClienteBuscar" runat="server" Text="Cliente"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="CmbClienteBuscar" runat="server" Width="400px" AutoPostBack="True"
                                OnSelectedIndexChanged="CmbClienteBuscar_SelectedIndexChanged" Enabled="false" >
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="LblCanal" runat="server" Text="Canal"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="CmbCanal" runat="server" Width="200px" AutoPostBack="True"
                                OnSelectedIndexChanged="CmbCanal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="LblBuscarSelMes" runat="server" Text="Mes"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="CmbBuscarSelMes" runat="server" Width="100px" AutoPostBack="True"
                                OnSelectedIndexChanged="CmbBuscarSelMes_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="LblBuscarSelAño" runat="server" Text="Año" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="CmbBuscarSelAño" runat="server" Width="100px" AutoPostBack="True"
                                Visible="False" OnSelectedIndexChanged="CmbBuscarSelAño_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <table align="center">
                    <tr>
                        <td>
                            <asp:GridView ID="gvLink_informes" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                EmptyDataText="...La consulta no produjo ningún resultado..." EmptyDataRowStyle-VerticalAlign="Middle"
                                EmptyDataRowStyle-HorizontalAlign="Center" Font-Names="Verdana" Font-Size="10pt"
                                ForeColor="#333333" GridLines="Vertical" OnPageIndexChanging="gvLink_informes_PageIndexChanging"
                                OnSelectedIndexChanged="gvLink_informes_SelectedIndexChanged" PageSize="5" Font-Strikeout="False">
                                <RowStyle BackColor="#CCD4E1" CssClass="FondoHeaderGrid" Font-Bold="True" Font-Names="Verdana"
                                    Font-Size="8pt" ForeColor="#666666" HorizontalAlign="Left" />
                                <EmptyDataRowStyle Font-Overline="True" Font-Names="Verdana" Font-Size="10pt" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="Campaña" HeaderText="Campaña" ItemStyle-Width="300">
                                        <ItemStyle Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Canal" HeaderText="Canal"></asp:BoundField>
                                    <asp:BoundField DataField="Informe" HeaderText="Informe" ItemStyle-Width="350">
                                        <ItemStyle Width="350px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Archivo" HeaderText="Archivo" ItemStyle-Width="450">
                                        <ItemStyle Width="450px" />
                                    </asp:BoundField>
                                    <asp:CommandField ButtonType="Image" ItemStyle-Width="15" SelectImageUrl="~/Pages/images/delete.png"
                                        SelectText="eliminar informe" ShowSelectButton="True">
                                        <ItemStyle Width="15px" />
                                    </asp:CommandField>
                                </Columns>
                                <FooterStyle BackColor="#CCD4E1" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                                    ForeColor="#333333" />
                                <PagerStyle BackColor="#CCD4E1" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                                    ForeColor="#333333" />
                                <SelectedRowStyle BackColor="Silver" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                                    ForeColor="#333333" />
                                <HeaderStyle CssClass="FondoHeaderGrid" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt"
                                    ForeColor="Magenta" />
                                <EditRowStyle BackColor="#99CCFF" Font-Names="Verdana" Font-Size="8pt" />
                                <AlternatingRowStyle BackColor="#CCD4E1" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                                    ForeColor="#666666" HorizontalAlign="Left" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalBuscar" runat="server" Enabled="True" TargetControlID="BtnBuscarInforme"
                PopupControlID="PanelBuscaInforme" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Button ID="BtnConfirmaEliminar" runat="server" Text="" Height="0px" CssClass="alertas"
                Width="0" Enabled="false" />
            <asp:Panel ID="PanelConfirmaElimina" runat="server" Width="332px" CssClass="altoverow"
                Style="display: none;">
                <table align="center" style="width: 95%;">
                    <tr>
                        <td align="center" valign="top">
                            <br />
                            <asp:Label ID="LblTitConfirmaElimina" runat="server" Text="Sr. Usuario"></asp:Label>
                            <br />
                            <asp:Label ID="LblTxtConfirmaElimina" runat="server"></asp:Label>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="BtnSiConfirmaElimina" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                            Text="SI" OnClick="BtnSiConfirmaElimina_Click" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="BtnNoConfirmaElimina" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                            Text="NO" OnClick="BtnNoConfirmaElimina_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalConfirmaElimina" runat="server" Enabled="True" TargetControlID="BtnConfirmaEliminar"
                PopupControlID="PanelConfirmaElimina" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnSaveInfo" />
        </Triggers>
    </asp:UpdatePanel>
   <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="3" 
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
    <br />
    <br />
    <br />
    </form>
</body>
</html>
