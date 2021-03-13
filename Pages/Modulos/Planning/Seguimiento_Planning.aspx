<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Seguimiento_Planning.aspx.cs" Inherits="SIGE.Pages.Modulos.Planning.Seguimiento_Planning" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!--
-- Author       : Carlos Hernandez Rincon (CHR)
-- Create date  : 01/01/2010
-- Description  : Página para realizar seguimiento del Planning.
--
-- Change History:
-- 26/10/2018 Pablo Salas Alvarez (PSA): Refactoring.
-->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
    <link href="../../css/layout.css" rel="stylesheet" type="text/css" />
    <link href="../../css/Webfotter.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function pulsar(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 13) return false;
        }
    </script>
</head>
<body onkeypress="return pulsar(event)">
    <form id="form1" runat="server">
        <div class="Header" align="center" style="height: 150px;"><asp:Image ID="Image2" runat="server" 
            ImageUrl="~/Pages/ImgBooom/logotipo.png"></asp:Image></div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true" 
            EnableScriptLocalization="true"> </cc1:ToolkitScriptManager>
        <div class="HeaderRight">
            <asp:Label ID="lblUsuario" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="#114092"></asp:Label>
            <asp:Label ID="usersession" runat="server" Visible="False" Font-Names="Verdana" Font-Size="9pt" ForeColor="#114092"></asp:Label>
            <br />
            <asp:ImageButton ID="ImgCloseSession" runat="server" Height="16px" ImageUrl="~/Pages/images/SesionClose.png" Width="84px" OnClick="ImgCloseSession_Click" />
        </div>
        <asp:UpdatePanel ID="UpPlanning" runat="server">
            <ContentTemplate>
                <div class="menuplanning">

                    <!-- PANEL MENU PRINCIPAL - INI -->
                    <asp:UpdatePanel ID="UpPanelMenuConsultas" runat="server">
                        <ContentTemplate>
                            
                            <!-- GIF LOADING... - INI -->
                            <table align="center">
                                <tr>
                                    <td> <asp:Label ID="LblUpProgressUP" runat="server" Text="." ForeColor="White"></asp:Label> </td>
                                    <td> <asp:Panel ID="PProgresso" runat="server">
                                            <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
                                                <ProgressTemplate>
                                                    <div style="text-align: center;">
                                                        <img src="../../images/loading1.gif" alt="Procesando" 
                                                            style="width: 90px; height: 13px" />
                                                        <asp:Label ID="lblProgress" runat="server" 
                                                            Text=" Procesando, por favor espere ..." ForeColor="Black"></asp:Label>
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <!-- GIF LOADING.. - FIN -->

                            <table align="center" border="0" cellpadding="0" cellspacing="0">
                                
                                <!-- CABECERA - INI -->
                                <tr>
                                    <td>
                                        <img alt="sup1" height="6" 
                                            src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png" 
                                            width="6"></img>
                                    </td>
                                    <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg"> 
                                        <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" 
                                            width="1"> </img> 
                                    </td>
                                    <td> 
                                        <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png" 
                                            width="6"></img> 
                                    </td>
                                </tr>
                                <!-- CABECERA - FIN -->

                                <!-- BODY - INI -->
                                <tr>
                                    <!-- HEADER - INI -->
                                    <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg"> 
                                        <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6"></img> 
                                    </td>
                                    <!-- HEADER - FIN -->
                                    
                                    <!-- BODY - INI -->
                                    <td bgcolor="#CCD4E1" valign="top">
                                        
                                        <asp:Panel ID="PnlMenuPlanning" runat="server" BackColor="White" Width="600px">
                                            <div>        

                                                <!-- FILTROS - INI -->
                                                <table>
                                                    <tr>
                                                        <td><asp:Label ID="LblSelCampaña" runat="server" Text="Campaña" 
                                                            CssClass="labelsN"></asp:Label></td>
                                                        <td><asp:DropDownList ID="CmbSelCampaña" runat="server" Width="535px" 
                                                            AutoPostBack="True" CausesValidation="True" 
                                                            OnSelectedIndexChanged="CmbSelCampaña_SelectedIndexChanged">
                                                        </asp:DropDownList></td>
                                                    </tr>
                                                </table>
                                                <!-- FILTROS - FIN -->
                                                
                                                <!-- OPCIONES - INI -->
                                                <table align="center">
                                                    <tr>
                                                        <td><asp:ImageButton ID="ImgIrABudget" runat="server" 
                                                                ImageUrl="~/Pages/ImgBooom/iconodescarga.png" ToolTip="Ver información..." 
                                                                Visible="False" OnClick="ImgIrABudget_Click" /> </td>
                                                        <td><span class="labelsN">Asignación de Presupuesto</span> </td>
                                                        <td><asp:Image ID="ImgAsigBudget" runat="server" 
                                                                ImageUrl="~/Pages/images/Esperando.png" Width="70%" /></td>
                                                        <td rowspan="10" style="display: none;"> 
                                                            <div><asp:ImageButton ID="ImgBtnInformeTotal" runat="server" 
                                                                    Visible="false" ImageUrl="~/Pages/images/Digitar.png"  
                                                                    Width="100px" Height="100px" 
                                                                    ToolTip="Ver Información completa de la campaña"  
                                                                    OnClick="ImgBtnInformeTotal_Click" />
                                                            </div> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td> <asp:ImageButton ID="ImgIrADesc" runat="server" 
                                                                ImageUrl="~/Pages/ImgBooom/iconodescarga.png" 
                                                                ToolTip="Ver información..." Visible="False" OnClick="ImgIrADesc_Click" /> </td>
                                                        <td> <span class="labelsN">Descripción de la Campaña</span> </td>
                                                        <td> <asp:Image ID="ImgDescCamp" runat="server" 
                                                                ImageUrl="~/Pages/images/Esperando.png" Width="70%" /> </td>
                                                    </tr>
                                                    <tr>
                                                        <td> <asp:ImageButton ID="ImgIrAResponsables" runat="server" 
                                                                ImageUrl="~/Pages/ImgBooom/iconodescarga.png"  
                                                            ToolTip="Ver información..." Visible="False" 
                                                            OnClick="ImgIrAResponsables_Click" /> </td>
                                                        <td> <span class="labelsN">Responsables</span> </td>
                                                        <td> <asp:Image ID="ImgResponsables" runat="server" 
                                                            ImageUrl="~/Pages/images/Esperando.png"  Width="70%" /> </td>
                                                    </tr>
                                                    <tr>
                                                        <td> <asp:ImageButton ID="ImgIrAASigPersonal" runat="server" 
                                                            ImageUrl="~/Pages/ImgBooom/iconodescarga.png" 
                                                            ToolTip="Ver información..." Visible="False" 
                                                            OnClick="ImgIrAASigPersonal_Click" /> </td>
                                                        <td> <span class="labelsN">Asignación de Personal</span>  </td>
                                                        <td> <asp:Image ID="ImgAsigPersonal" runat="server" 
                                                            ImageUrl="~/Pages/images/Esperando.png"  Width="70%" /> </td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:ImageButton ID="ImgIrAPDV" runat="server" 
                                                            ImageUrl="~/Pages/ImgBooom/iconodescarga.png" 
                                                            ToolTip="Ver información..." Visible="False" OnClick="ImgIrAPDV_Click" /></td>
                                                        <td><span class="labelsN">Puntos de Venta</span> </td>
                                                        <td><asp:Image ID="ImgPDV" runat="server" ImageUrl="~/Pages/images/Esperando.png" 
                                                            Width="70%" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td> <asp:ImageButton ID="ImgIrAPaneles" runat="server" 
                                                            ImageUrl="~/Pages/ImgBooom/iconodescarga.png" 
                                                        ToolTip="Ver información..." Visible="False" OnClick="ImgIrAPaneles_Click"/></td>
                                                        <td> <span class="labelsN">Páneles</span></td>
                                                        <td> <asp:Image ID="ImgPaneles" runat="server" ImageUrl="~/Pages/images/Esperando.png"  
                                                            Width="70%" />  </td>
                                                    </tr>
                                                    <tr>
                                                        <td> <asp:ImageButton ID="ImgIrAAsignapdv" runat="server" 
                                                            ImageUrl="~/Pages/ImgBooom/iconodescarga.png" 
                                                            ToolTip="Ver información..." Visible="False" OnClick="ImgIrAAsignapdv_Click"/></td>
                                                        <td> <span class="labelsN">Asignación de Rutas</span></td>
                                                        <td> <asp:Image ID="ImgAsignaPDV" runat="server" 
                                                            ImageUrl="~/Pages/images/Esperando.png" Width="70%"/></td>
                                                    </tr>
                                                    <tr>
                                                        <td> <asp:ImageButton ID="ImgIrAProductos" runat="server" 
                                                            ImageUrl="~/Pages/ImgBooom/iconodescarga.png" 
                                                            ToolTip="Ver información..." Visible="False" OnClick="ImgIrAProductos_Click"/></td>
                                                        <td> <span class="labelsN">Levantamiento de Información</span></td>
                                                        <td> <asp:Image ID="ImgProductos" runat="server" 
                                                            ImageUrl="~/Pages/images/Esperando.png"  Width="70%" /> </td>
                                                    </tr>
                                                    <tr>
                                                        <td> <asp:ImageButton ID="ImgIrAReportesCampaña" runat="server" 
                                                            ImageUrl="~/Pages/ImgBooom/iconodescarga.png"  
                                                            ToolTip="Ver información..." Visible="False" 
                                                            OnClick="ImgIrAReportesCampaña_Click" />  </td>
                                                        <td><span class="labelsN">Asignación de Períodos</span></td>
                                                        <td><asp:Image ID="ImgReportes" runat="server" 
                                                                ImageUrl="~/Pages/images/Esperando.png" Width="70%" /></td>
                                                    </tr>
                                                    <tr id="OpcionGestionNiveles" runat="server" style="display: none;">
                                                        <td> <asp:ImageButton ID="ImgIrAGestionNiveles" runat="server" 
                                                            ImageUrl="~/Pages/ImgBooom/iconodescarga.png"  
                                                            ToolTip="Ver información..." Visible="False" 
                                                            OnClick="ImgIrAGestionNiveles_Click" /></td>
                                                        <td><span class="labelsN">Gestión de Niveles</span> </td>
                                                        <td><asp:Image id="ImgGestionNiveles" runat="server" 
                                                                ImageUrl="../../images/Esperando.png" width="70%" alt="" /> </td>
                                                    </tr>
                                                    <tr id="OpcionGestionFuerzaVenta" runat="server" style="display: none;">
                                                        <td><asp:ImageButton ID="ImgIrAGestionFuerzaVenta" runat="server" 
                                                                ImageUrl="~/Pages/ImgBooom/iconodescarga.png" 
                                                                ToolTip="Ver información..." Visible="False" 
                                                                OnClick="ImgIrAGestionFuerzaVenta_Click" /> </td>
                                                        <td><span class="labelsN">Gestión de Fuerza de Venta</span> </td>
                                                        <td><asp:Image id="ImgGestionFuerzaVenta" runat="server" 
                                                                ImageUrl="../../images/Esperando.png" width="70%" alt="" />  </td>
                                                    </tr>
                                                    <tr style="display: none;">
                                                        <td><asp:ImageButton ID="ImgIrABreaf" runat="server" 
                                                                ImageUrl="~/Pages/ImgBooom/iconodescarga.png" 
                                                                ToolTip="Ver información..." Visible="False" OnClick="ImgIrABreaf_Click" /> </td>
                                                        <td><asp:Label ID="LblBreaf" runat="server" Text="Breaf" CssClass="labelsN"></asp:Label>  </td>
                                                        <td><asp:Image ID="ImgBreaf" runat="server" 
                                                                ImageUrl="~/Pages/images/Esperando.png" Width="70%" /> </td>
                                                    </tr>
                                                    <tr runat="server" id="MenuProductoAncla"  visible="false">
                                                        <td><asp:ImageButton ID="ImgProdAncla" runat="server" 
                                                                ImageUrl="~/Pages/ImgBooom/iconodescarga.png" 
                                                                ToolTip="Ver información..."  Visible="false" 
                                                                OnClick="ImgProdAncla_Click" /> </td>
                                                        <td><asp:Label ID="Label8" runat="server" Text="Producto Ancla" 
                                                                CssClass="labelsN"></asp:Label> </td>
                                                        <td><asp:Image ID="Image3" runat="server" Visible="false" 
                                                            ImageUrl="~/Pages/images/Esperando.png" Width="70%" /> </td>
                                                    </tr>
                                                    <tr runat="server" id="MenuObjetivoSODMay" visible="false"  >
                                                        <td><asp:ImageButton ID="ImgObjetivoSODMay" runat="server" 
                                                                ImageUrl="~/Pages/ImgBooom/iconodescarga.png" 
                                                                ToolTip="Ver información..."   OnClick="ImgObjetivoSODMay_Click" /></td>
                                                        <td><asp:Label ID="Label14" runat="server" Text="Objetivo SOD MAY" 
                                                                CssClass="labelsN"></asp:Label></td>
                                                        <td><asp:Image ID="Image4" runat="server" Visible="false" 
                                                                ImageUrl="~/Pages/images/Esperando.png" Width="70%" /></td>
                                                    </tr>
                                                </table>
                                                <!-- OPCIONES - FIN -->

                                            </div>
                                        </asp:Panel>

                                        <!-- DIV - INFORMACION DE LA CAMPAÑA - INI -->
                                        <div style="width: 100%; height: 800px; display: none;">
                                            <asp:GridView ID="GvInformacionPlanning" runat="server" Width="900px" 
                                                AutoGenerateColumns="False"  EnableModelValidation="True" BackColor="White" 
                                                BorderColor="#336666" BorderStyle="Double"  BorderWidth="3px" CellPadding="4" 
                                                Font-Names="Arial" Font-Size="10pt" GridLines="Horizontal"> 
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Datos generales de la campaña" ItemStyle-VerticalAlign="Top">
                                                        <ItemTemplate>                                                            
                                                            <table>
                                                                
                                                                <!-- PERSONAL DE LA CAMPAÑA - INI -->
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GVInfogenStaff" runat="server" 
                                                                                BackColor="White" BorderColor="#336666"  BorderStyle="Double" 
                                                                                BorderWidth="3px" CellPadding="4" Font-Names="Arial" Font-Size="10pt"  
                                                                                GridLines="Horizontal"> 
                                                                            <HeaderStyle BackColor="#336666" Font-Bold="True" 
                                                                                ForeColor="White" />
                                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <!-- PERSONAL DE LA CAMPAÑA - FIN -->


                                                                <!-- PUNTOS DE VENTA DE LA CAMPAÑA - INI -->
                                                                <tr>
                                                                    <tr><td><br/></td></tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImgButtonExpande" runat="server" 
                                                                            ImageUrl="~/Pages/images/add.png" ToolTip="Expandir" />
                                                                        <asp:Label ID="infopdv" runat="server" Text="Puntos de Venta">
                                                                        </asp:Label><br />
                                                                        <asp:GridView ID="gvinfopdv" runat="server" BackColor="White" 
                                                                                BorderColor="#336666"  BorderStyle="Double" BorderWidth="3px" 
                                                                                CellPadding="4" Font-Names="Arial" Font-Size="10pt" 
                                                                                GridLines="Horizontal">
                                                                            <HeaderStyle BackColor="#336666" Font-Bold="True" 
                                                                                ForeColor="White" />
                                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <!-- PUNTOS DE VENTA DE LA CAMPAÑA - FIN -->

                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="White" ForeColor="#333333" />
                                            </asp:GridView>
                                        </div>
                                        <!-- DIV - INFORMACION DE LA CAMPAÑA - FIN -->
                                    </td>
                                    <!-- BODY - FIN -->
                                    
                                    <!-- FOOTER - INI -->
                                    <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg"> 
                                        <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6"></img> 
                                    </td>
                                    <!-- FOOTER - FIN -->
                                </tr>
                                <!-- BODY - FIN -->

                                <!-- PIE - INI -->
                                <tr>
                                    <td> <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png" 
                                            width="6"> </img> </td> 
                                    <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                        <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png" 
                                            width="1"></img></td>
                                    <td><img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png" 
                                            width="6"> </img> </td>
                                </tr>
                                <!-- PIE - FIN -->

                            </table>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!-- PANEL MENU PRINCIPAL - FIN -->


                    <asp:UpdatePanel ID="UpPanelPresupuesto" runat="server">
                        <ContentTemplate>
                            <%--informacion de planning--%>
                            <asp:Panel ID="PanelASignaPresupuesto" runat="server" Style="display: none;" Width="1250px">
                                <div>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="90%">
                                        <tr>
                                            <td> <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png" width="6"> </img> </td>
                                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg"> <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1"></img> </td>
                                            <td> <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"  width="6"> </img> </td>
                                        </tr>
                                        <tr>
                                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg"><img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6"> </img> </td>
                                            <td bgcolor="White">
                                                <asp:Panel ID="PanelPlanning" runat="server" Style="vertical-align: middle;">
                                                    <div> <asp:ImageButton ID="BtnCOlv" runat="server" AlternateText="Cerrar Ventana" ToolTip="Cerrar Ventana"  BackColor="Transparent" Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png"  Width="23px" OnClick="BtnCOlv_Click" /> </div> <br />
                                                    <div class="centrarcontenido labelsTit2"> Asignación de Presupuesto</div> <br />
                                                    <table align="center">
                                                        <tr>
                                                            <td> <asp:Label ID="lblNumpla" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label> </td>
                                                            <td> <asp:TextBox ID="txtnumpla" runat="server" BackColor="#CCCCCC" Enabled="False" ForeColor="White"></asp:TextBox> </td>
                                                            <td> <asp:Label ID="lblEstadoplanning" runat="server" CssClass="labelsN" Text="Estado"></asp:Label> </td>
                                                            <td> <asp:RadioButtonList ID="Rblisstatus" runat="server" Enabled="False" RepeatDirection="Horizontal"> 
    																<asp:ListItem Selected="True" Text="Activo" Value="True"></asp:ListItem> 
                                                                    <asp:ListItem Text="Inactivo" Value="False"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="lblpresu" runat="server" CssClass="labelsN" Text="Campaña"></asp:Label></td>
                                                            <td colspan="3"> <asp:Label ID="LblTxtPresupuesto" runat="server" Text=" presupuesto asignado" Width="500px" Enabled="False"></asp:Label> </td>
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
                                                                            <td valign="top"> <asp:Label ID="lblnameplaning" runat="server" CssClass="labelsN" Text="Campaña"></asp:Label> </td>
                                                                            <td valign="top"> <asp:TextBox ID="txtnamepresu" runat="server" BackColor="#CCCCCC" Enabled="False"  Width="300px"></asp:TextBox> </td>
                                                                            <td colspan="2" valign="top"> <asp:Label ID="lblfechsoli" runat="server" CssClass="labelsN" Text="Fecha de Solicitud"></asp:Label> </td>
                                                                            <td valign="top">
                                                                                <asp:TextBox ID="txt_FechaSolicitud" runat="server" AutoPostBack="True" Enabled="False" OnTextChanged="txt_FechaSolicitud_TextChanged"></asp:TextBox>
                                                                                <cc1:MaskedEditExtender ID="txt_FechaSolicitud_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""  CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""  CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechaSolicitud"  UserDateFormat="DayMonthYear"> </cc1:MaskedEditExtender>
                                                                                <cc1:CalendarExtender ID="txt_FechaSolicitud_CalendarExtender" runat="server" Enabled="True"  FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal" PopupPosition="TopLeft"  TargetControlID="txt_FechaSolicitud">  </cc1:CalendarExtender>
                                                                                <asp:ImageButton ID="ImageButtonCal" runat="server" Height="16px" ImageUrl="~/Pages/images/calendario.JPG"  Width="16px" />
                                                                            </td>
                                                                            <td> <asp:Label ID="lblfecentregafin" runat="server" CssClass="labelsN" Text="Fecha Entrega Final"></asp:Label> </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txt_FechaEntrega" runat="server" AutoPostBack="True" Enabled="False"  OnTextChanged="txt_FechaEntrega_TextChanged"></asp:TextBox> 
                                                                                <cc1:MaskedEditExtender ID="txt_FechaEntrega_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""  CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""  CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""  Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechaEntrega"  UserDateFormat="DayMonthYear"> </cc1:MaskedEditExtender>
                                                                                <cc1:CalendarExtender ID="txt_FechaEntrega_CalendarExtender" runat="server" Enabled="True"  FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal2" PopupPosition="TopLeft"  TargetControlID="txt_FechaEntrega"> </cc1:CalendarExtender>
                                                                                <asp:ImageButton ID="ImageButtonCal2" runat="server" Enabled="False" Height="16px"  ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top"> <asp:Label ID="lblclient" runat="server" CssClass="labelsN" Text="Cliente"></asp:Label>  </td>
                                                                            <td valign="top">  <asp:TextBox ID="txtcliente" runat="server" BackColor="Silver" Enabled="False" Width="300px"></asp:TextBox> </td>
                                                                            <td> <asp:Label ID="lblperiodopre" runat="server" CssClass="labelsN" Text="Periodo de Preproduccion"></asp:Label> </td>
                                                                            <td> <asp:Label ID="lblfecinipre" runat="server" CssClass="labelsN" Text=" Inicio"></asp:Label> </td>
                                                                            <td> 
                                                                                <asp:TextBox ID="txt_FechainiPre" runat="server" AutoPostBack="True" Enabled="False" OnTextChanged="txt_FechainiPre_TextChanged"></asp:TextBox>
                                                                                <cc1:MaskedEditExtender ID="txt_FechainiPre_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""  CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""  CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""  Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechainiPre"  UserDateFormat="DayMonthYear"> </cc1:MaskedEditExtender>
                                                                                <cc1:CalendarExtender ID="txt_FechainiPre_CalendarExtender" runat="server" Enabled="True" FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal3" PopupPosition="TopLeft"  TargetControlID="txt_FechainiPre">  </cc1:CalendarExtender>
                                                                                <asp:ImageButton ID="ImageButtonCal3" runat="server" Enabled="False" Height="16px" ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:Label ID="lblfefinpre" runat="server" CssClass="labelsN" Text="Fin"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txt_Fechafinpre" runat="server" AutoPostBack="True" Enabled="False" OnTextChanged="txt_Fechafinpre_TextChanged"></asp:TextBox>
                                                                                <cc1:MaskedEditExtender ID="txt_Fechafinpre_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""  CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""  CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""  Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Fechafinpre" UserDateFormat="DayMonthYear">  </cc1:MaskedEditExtender>
                                                                                <cc1:CalendarExtender ID="txt_Fechafinpre_CalendarExtender" runat="server" Enabled="True"  FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal4" PopupPosition="TopLeft"  TargetControlID="txt_Fechafinpre"> </cc1:CalendarExtender>
                                                                                <asp:ImageButton ID="ImageButtonCal4" runat="server" Enabled="False" Height="16px" ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top"> <asp:Label ID="lblservice" runat="server" CssClass="labelsN" Text="Servicio"></asp:Label> </td>
                                                                            <td valign="top"> <asp:TextBox ID="txtservice" runat="server" BackColor="#CCCCCC" Enabled="False" Width="300px"></asp:TextBox> </td>
                                                                            <td> <asp:Label ID="lblperiEjecu" runat="server" CssClass="labelsN" Text="Periodo de Ejecucion"></asp:Label></td>
                                                                            <td> <asp:Label ID="lblfecini" runat="server" CssClass="labelsN" Text=" Inicio"></asp:Label></td>
                                                                            <td> 
    																			<asp:TextBox ID="txt_FechainiPla" runat="server" AutoPostBack="True" Enabled="False"  OnTextChanged="txt_FechainiPla_TextChanged"></asp:TextBox> 
                                                                                <cc1:MaskedEditExtender ID="txt_FechainiPla_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""  CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""  CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""  Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechainiPla"  UserDateFormat="DayMonthYear">  </cc1:MaskedEditExtender>
                                                                                <cc1:CalendarExtender ID="txt_FechainiPla_CalendarExtender" runat="server" Enabled="True"  FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal5" PopupPosition="TopLeft"  TargetControlID="txt_FechainiPla"> </cc1:CalendarExtender>
                                                                                <asp:ImageButton ID="ImageButtonCal5" runat="server" Enabled="False" Height="16px" ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:Label ID="lblfecfin" runat="server" CssClass="labelsN" Text="Fin"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txt_FechaPlafin" runat="server" AutoPostBack="True" Enabled="False" OnTextChanged="txt_FechaPlafin_TextChanged"></asp:TextBox>
                                                                                <cc1:MaskedEditExtender ID="txt_FechaPlafin_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""  CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""  CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""  Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechaPlafin"  UserDateFormat="DayMonthYear">  </cc1:MaskedEditExtender>
                                                                                <cc1:CalendarExtender ID="txt_FechaPlafin_CalendarExtender" runat="server" Enabled="True"  FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal6" PopupPosition="TopLeft"  TargetControlID="txt_FechaPlafin"> </cc1:CalendarExtender>
                                                                                <asp:ImageButton ID="ImageButtonCal6" runat="server" Enabled="False" Height="16px" ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="7" valign="top">
                                                                                <table align="center" width="99%">
                                                                                    <tr>
                                                                                        <td align="center" valign="top" width="60%"> 
                                                                                            <asp:Label ID="LbDuracion" runat="server" CssClass="labelsN" Text="Duración del proyecto"></asp:Label> <br />
                                                                                            <asp:TextBox ID="TxtDuracion" runat="server" Height="120px" TextMode="MultiLine" Width="300px" Enabled="False"></asp:TextBox>
                                                                                        </td>
                                                                                        <td align="left" valign="top" width="30%">
                                                                                            <asp:Label ID="lblcanal" runat="server" CssClass="labelsN" Text="Canal"></asp:Label> <br />
                                                                                            <div class="p" style="width: 220px; height: 120px;"> <asp:RadioButtonList ID="RbtnCanal" runat="server" Enabled="False"> </asp:RadioButtonList> </div>
                                                                                        </td>
                                                                                        <td align="right" valign="bottom" width="10%">
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Button ID="BtnEditPlanning" runat="server" CssClass="buttonEditPlan" Height="25px"  Text="Editar" Width="164px" OnClick="BtnEditPlanning_Click" />
                                                                                                        <asp:Button ID="BtnUpdatePlanning" runat="server" CssClass="buttonSavePlan" Height="25px"  Text="Actualizar" Width="164px" Visible="False" OnClick="BtnUpdatePlanning_Click" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr><td><asp:Button ID="btncancelcara" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"  Height="25px" Text="Deshacer" Width="164px" OnClick="btncancelcara_Click" /> </td> </tr>
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
                            <cc1:ModalPopupExtender ID="ModalPanelASignaPresupuesto" runat="server" 
                                BackgroundCssClass="modalBackground" Enabled="True" PopupControlID="PanelASignaPresupuesto" 
                                TargetControlID="btndisparaasignapresupuesto"> </cc1:ModalPopupExtender>
                            <asp:Button ID="btndisparaasignapresupuesto" runat="server" CssClass="alertas" Enabled="False" Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UP_Gestion_de_Niveles" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="Panel_Gestion_De_niveles" runat="server"> 

                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalGestion_de_Niveles" runat="server" BackgroundCssClass="modalBackground" Enabled="True" PopupControlID="Panel_Gestion_De_niveles" TargetControlID="btn_show_modal_gn"> </cc1:ModalPopupExtender>
                            <asp:Button ID="btn_show_modal_gn" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpPanelDescripcion" runat="server">
                        <ContentTemplate>
                            <%--informacion de descripción del planning--%>
                            <asp:Panel ID="PanelDescCampaña" runat="server" Style="display: none;">
                                <div>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td><img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                                    width="6"> </img></td>
                                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1"></img>
                                            </td>
                                            <td><img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png" width="6"> </img></td>
                                        </tr>
                                        <tr>
                                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6"></img>
                                            </td>
                                            <td bgcolor="White">
                                                <asp:Panel ID="PanelDesc" runat="server" Style="vertical-align: middle;">
                                                    <div>
                                                        <asp:ImageButton ID="ImgCloseDescr" runat="server" AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="BtnCOlv_Click" />
                                                    </div>
                                                    <br />
                                                    <div class="centrarcontenido labelsTit2">
                                                        Descripción de Campaña</div>
                                                    <br />
                                                    <table align="center">
                                                        <tr>
                                                            <td><asp:Label ID="LblPlannigDesc" runat="server" CssClass="labelsN" 
                                                                Text="Planning No"></asp:Label></td>
                                                            <td><asp:TextBox ID="TxtCodPlanningDesc" runat="server" BackColor="#CCCCCC" Enabled="False" 
                                                                ForeColor="White"></asp:TextBox></td>
                                                            <td><asp:Label ID="LblSelPresupuestoDesc" runat="server" CssClass="labelsN" Text="Campaña"></asp:Label></td>
                                                            <td><asp:Label ID="LblTxtPresupuestoDesc" runat="server" Text=" presupuesto asignado"
                                                                Width="500px" Enabled="False"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table align="center" width="99%">
                                                        <tr>
                                                            <td>
                                                                <fieldset id="Fieldset1" runat="server">
                                                                    <legend>Descripción</legend><br />
                                                                    <table>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <asp:Label ID="lblobj" runat="server" CssClass="labelsN" Text="Objetivo de la Campaña"></asp:Label>
                                                                                <asp:Label ID="lblolbli13" runat="server" ForeColor="#CC3300" Text="*"></asp:Label><br />
                                                                                <asp:TextBox ID="txtobj" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="150px"
                                                                                    TextMode="MultiLine" Width="265px" Enabled="False" Style="resize:none;"></asp:TextBox>
                                                                            </td>
                                                                            <td valign="top">
                                                                                <asp:Label ID="lblmanda" runat="server" CssClass="labelsN" Text="Mandatorios de  Campaña"></asp:Label>
                                                                                <asp:Label ID="lblolbli14" runat="server" ForeColor="#CC3300" Text="*"></asp:Label><br />
                                                                                <asp:TextBox ID="txtmanda" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="150px"
                                                                                    TextMode="MultiLine" Width="265px" Enabled="False" Style="resize:none;"></asp:TextBox>
                                                                            </td>
                                                                            <td valign="top">
                                                                                <asp:Label ID="lblmeca" runat="server" CssClass="labelsN" Text="Mecanica"></asp:Label>
                                                                                <asp:Label ID="lblolbli15" runat="server" ForeColor="#CC3300" Text="*"></asp:Label><br />
                                                                                <asp:TextBox ID="Txtmeca" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="150px"
                                                                                    TextMode="MultiLine" Width="265px" Enabled="False" Style="resize:none;"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Button ID="BtnEditDescripcion" runat="server" CssClass="buttonEditPlan" Height="25px"
                                                                                                Text="Editar" Width="164px" OnClick="BtnEditDescripcion_Click" />
                                                                                            <asp:Button ID="BtnUpdateDescripcion" runat="server" CssClass="buttonSavePlan" Height="25px"
                                                                                                Text="Actualizar" Width="164px" Visible="False" OnClick="BtnUpdateDescripcion_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td><asp:Button ID="BtnClearDescrip" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                                                                Height="25px" Text="Deshacer" Width="164px" OnClick="BtnClearDescrip_Click" />
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
                                                                                            <asp:TextBox ID="txtcontacto" runat="server" MaxLength="60" Width="300px" Enabled="False"></asp:TextBox>
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
                                                                                            <asp:TextBox ID="txtarea" runat="server" Width="300px" Enabled="False"></asp:TextBox>
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
                            <cc1:ModalPopupExtender ID="ModalPanelDescCampaña" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelDescCampaña" TargetControlID="btndisparadesccampaña">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btndisparadesccampaña" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpPanelResponsables" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="PanelResponsablesCampaña" runat="server" Style="display: none;">
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
                                                <asp:Panel ID="PanelRespons" runat="server" Style="vertical-align: middle;">
                                                    <div>
                                                        <asp:ImageButton ID="ImgCloseResponsables" runat="server" AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="BtnCOlv_Click" />
                                                    </div><br />
                                                    <div class="centrarcontenido labelsTit2">Responsables de Campaña</div><br />
                                                    <table>
                                                        <tr>
                                                            <td><asp:Label ID="LblPlanningRes" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label></td>
                                                            <td><asp:TextBox ID="TxtPlanningRes" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                                    ForeColor="White"></asp:TextBox></td>
                                                            <td><asp:Label ID="LblSelPresupuestoRes" runat="server" CssClass="labelsN" Text="Campaña:"></asp:Label></td>
                                                            <td><asp:Label ID="LblTxtPresupuestoRes" runat="server" Text="Presupuesto" Width="500px"
                                                                    Enabled="False"></asp:Label></td>
                                                        </tr>
                                                    </table><br />
                                                    <table width="100%">
                                                        <tr>
                                                            <td valign="top"><asp:Label ID="LblSelEjecutivo" runat="server" Font-Bold="True" Font-Names="verdana"
                                                                    Font-Size="10pt" Text="Supervisor Controler"></asp:Label></td>
                                                            <td><asp:Label ID="LblSelSupervisores" runat="server" Font-Bold="True" Font-Names="verdana"
                                                                    Font-Size="10pt" Text="Supervisor(es)"></asp:Label></td>
                                                            <td><asp:Label ID="LblSelMercadersitas" runat="server" Font-Bold="True" Font-Names="verdana"
                                                                    Font-Size="10pt" Text="Mercaderista(s)"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <div style="border: 1px solid #000000; width: 380px; height: 200px;" class="p">
                                                                    <asp:GridView ID="GvEjecutivosAsignados" runat="server" 
                                                                        EmptyDataText="Se han quitado todas las asignaciones de Ejecutivos de la Campaña"
                                                                        Font-Names="Verdana" Font-Size="8pt" GridLines="None" AutoGenerateColumns="False"
                                                                        OnSelectedIndexChanged="GvEjecutivosAsignados_SelectedIndexChanged" Enabled="False">
                                                                        <Columns>
                                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                ShowSelectButton="True" />
                                                                            <asp:BoundField DataField="Person_id" HeaderText="Cod." />
                                                                            <asp:BoundField DataField="name_user" HeaderText="Ejecutivo" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="GvEliminaEjecutivos" runat="server" EmptyDataText="ejecutivos para desasignar"
                                                                        Font-Names="Verdana" Font-Size="8pt" GridLines="None" AutoGenerateColumns="False"
                                                                        Visible="False">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="Person_id" HeaderText="Cod." />
                                                                            <asp:BoundField DataField="name_user" HeaderText="Ejecutivo" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                            <td valign="top">
                                                                <div style="border: 1px solid #000000; width: 380px; height: 200px;" class="p">
                                                                    <asp:GridView ID="GvSupervisoresAsignados" runat="server" 
                                                                        EmptyDataText="Se han quitado todas las asignaciones de Supervisor de la Campaña"
                                                                        Font-Names="Verdana" Font-Size="8pt" GridLines="None" AutoGenerateColumns="False"
                                                                        OnSelectedIndexChanged="GvSupervisoresAsignados_SelectedIndexChanged" Enabled="False">
                                                                        <Columns>
                                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                ShowSelectButton="True" />
                                                                            <asp:BoundField DataField="Person_id" HeaderText="Cod." />
                                                                            <asp:BoundField DataField="name_user" HeaderText="Supervisor" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="GvEliminaSupervisor" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                        EmptyDataText="No se ha realizado ninguna asignación" Font-Names="Verdana" Font-Size="8pt"
                                                                        GridLines="None" Visible="False">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="Person_id" HeaderText="Cod." />
                                                                            <asp:BoundField DataField="name_user" HeaderText="Supervisor" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                            <td valign="top">
                                                                <div style="border: 1px solid #000000; width: 380px; height: 200px;" class="p">
                                                                    <asp:GridView ID="GvMercaderistasAsignados" runat="server" 
                                                                        EmptyDataText="Se han quitado todas las asignaciones de Mercaderista de la Campaña"
                                                                        Font-Names="Verdana" Font-Size="8pt" GridLines="None" AutoGenerateColumns="False"
                                                                        OnSelectedIndexChanged="GvMercaderistasAsignados_SelectedIndexChanged" Enabled="False">
                                                                        <Columns>
                                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                ShowSelectButton="True" />
                                                                            <asp:BoundField DataField="Person_id" HeaderText="Cod." />
                                                                            <asp:BoundField DataField="name_user" HeaderText="Mercaderista" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="GvEliminaMercaderista" runat="server" AutoGenerateColumns="False"
                                                                        EmptyDataText="No se ha realizado ninguna asignación" Font-Names="Verdana" Font-Size="8pt"
                                                                        GridLines="None" Visible="False">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="Person_id" HeaderText="Cod." />
                                                                            <asp:BoundField DataField="name_user" HeaderText="Mercaderista" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="ImgButtonAddEjecutivos" runat="server" CssClass="buttonAddPersonal"
                                                                    Height="25px" Text="Controler" Width="164px" Enabled="False" />
                                                            </td>
                                                            <td>
                                                                &nbsp;<asp:Button ID="ImgButtonAddSupervisores" runat="server" CssClass="buttonAddPersonal"
                                                                    Height="25px" Text="Supervisor" Width="164px" Enabled="False" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="ImgButtonAddMercaderistas" runat="server" CssClass="buttonAddPersonal"
                                                                    Height="25px" Text="Mercaderista" Width="164px" Enabled="False" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <br />
                                                    <table align="center">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="BtnEditResponsables" runat="server" CssClass="buttonEditPlan" Height="25px"
                                                                    Text="Editar" Width="164px" OnClick="BtnEditResponsables_Click" />
                                                                <asp:Button ID="BtnUpdateResponsables" runat="server" CssClass="buttonSavePlan" Height="25px"
                                                                    Text="Actualizar" Width="164px" Visible="False" OnClick="BtnUpdateResponsables_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="BtnClearRespons" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                                    Height="25px" Text="Deshacer" Width="164px" OnClick="BtnClearRespons_Click" />
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
                            <cc1:ModalPopupExtender ID="ModalPanelResponsablesCampaña" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelResponsablesCampaña" TargetControlID="BtnDisparaResponsables">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="BtnDisparaResponsables" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                            <asp:Panel ID="PanelNewSupervisor" runat="server" BackColor="White" BorderColor="#7F99CC"
                                BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="490px"
                                Style="display: none;">
                                <div>
                                    <asp:Label ID="LblListadoSupervisores" runat="server" Text="Asignar nuevos Supervisores"></asp:Label>
                                    <asp:ImageButton ID="BtnclosePanelSupervisor" runat="server" BackColor="Transparent"
                                        Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px"
                                        OnClick="BtnclosePanelSupervisor_Click" />
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
                                    <asp:Button ID="btnAddSupervisores" runat="server" Text="Agregar" OnClick="btnAddSupervisores_Click" />
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
                                    <asp:ImageButton ID="BtnclosePanelMercaderista" runat="server" BackColor="Transparent"
                                        Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px"
                                        OnClick="BtnclosePanelMercaderista_Click" />
                                </div>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <div style="overflow: auto; width: 500px; height: 400px;">
                                                <asp:ListBox ID="LstNewMercaderista" runat="server" Height="100%" SelectionMode="Multiple"
                                                    ToolTip="Oprima (ctrl + click) si desea selección no consecutiva" Width="100%">
                                                </asp:ListBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <div align="center">
                                    <asp:Button ID="btnAddMercaderistas" runat="server" Text="Agregar" OnClick="btnAddMercaderistas_Click" />
                                </div>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalPanelNewMercaderista" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelNewMercaderista" TargetControlID="ImgButtonAddMercaderistas">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="PanelNewEjecutivo" runat="server" BackColor="White" BorderColor="#7F99CC"
                                BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="490px"
                                Style="display: none;">
                                <div>
                                    <asp:Label ID="LblListadoEjecutivo" runat="server" Text="Asignar nuevos Ejecutivos"></asp:Label>
                                    <asp:ImageButton ID="BtnclosePanelejecutivo" runat="server" BackColor="Transparent"
                                        Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" ToolTip="Cerrar Ventana"
                                        Width="23px" OnClick="BtnclosePanelejecutivo_Click" />
                                </div>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <div style="overflow: auto; width: 500px; height: 400px;">
                                                <asp:ListBox ID="LstNewejecutivo" runat="server" Height="100%" SelectionMode="Multiple"
                                                    ToolTip="oprima (ctrl + click) si desea selección no consecutiva" Width="100%">
                                                </asp:ListBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <div align="center">
                                    <asp:Button ID="btnAddEjecutivos" runat="server" Text="Agregar" OnClick="btnAddEjecutivos_Click" />
                                </div>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalPanelEjecutivo" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelNewEjecutivo" TargetControlID="ImgButtonAddEjecutivos">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpPanelASignacionPersonal" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="PanelAsignaPersonal" runat="server" Style="display: none;">
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
                                                    <div>
                                                        <asp:ImageButton ID="ImgCloseASignaPersonal" runat="server" AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="BtnCOlv_Click" />
                                                    </div>
                                                    <br />
                                                    <div class="centrarcontenido labelsTit2">
                                                        Asignación de Personal</div>
                                                    <br />
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
                                                                <asp:Label ID="LblSelPresupuestoAsig" runat="server" CssClass="labelsN" Text="Campaña"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblTxtPresupuestoAsig" runat="server" Text="Presupuesto" Width="500px"
                                                                    Enabled="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table align="center" width="90%" style="border-style: solid; border-width: 1px;
                                                        border-color: #CCD4E1">
                                                        <tr>
                                                            <td valign="top" style="width: 320px;">
                                                                <asp:Label ID="lblsupervisor" runat="server" CssClass="labelsN" Text="Supervisores"></asp:Label>
                                                                <br />
                                                                <asp:DropDownList ID="CmbSelSupervisoresAsig" runat="server" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="CmbSelSupervisoresAsig_SelectedIndexChanged" Width="320px"
                                                                    Enabled="False">
                                                                </asp:DropDownList>
                                                                <br />
                                                                <br />
                                                                <asp:Label ID="lblpersoncalle" runat="server" CssClass="labelsN" Text="Mercaderistas sin asignar"></asp:Label>
                                                                <br />
                                                                <asp:ListBox ID="LstBoxMercaderistas" runat="server" CssClass="p" Height="200px"
                                                                    SelectionMode="Multiple" Width="320px" Enabled="False"></asp:ListBox>
                                                            </td>
                                                            <td align="center" valign="top" width="30px">
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <asp:Button ID="BtnMasAsing" runat="server" CssClass="pagnext" Text=" ." ToolTip="Asignar"
                                                                    Width="25px" OnClick="BtnMasAsing_Click" Enabled="false" />
                                                            </td>
                                                            <td valign="top" style="width: 320px; height: 200px;">
                                                                <asp:Label ID="LblAsignacion" runat="server" CssClass="labelsN" Text="Mercaderistas asignados"></asp:Label>
                                                                <br />
                                                                <div class="p" style="width: 360px; height: 248px;">
                                                                    <asp:GridView ID="GvAsignados" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                                                        GridLines="None" AutoGenerateColumns="False" OnSelectedIndexChanged="GvAsignados_SelectedIndexChanged"
                                                                        Enabled="False">
                                                                        <Columns>
                                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                ShowSelectButton="True" />
                                                                            <asp:BoundField DataField="Person_id" HeaderText="Cod." />
                                                                            <asp:BoundField DataField="Nombre" HeaderText="Mercaderista" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table align="center">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="BtnEditAsigna" runat="server" CssClass="buttonEditPlan" Height="25px"
                                                                    Text="Editar" Width="164px" OnClick="BtnEditAsigna_Click" />
                                                                <asp:Button ID="BtnSaveAsig" runat="server" CssClass="buttonSavePlan" Enabled="false"
                                                                    Height="25px" Text="Actualizar" Width="164px" OnClick="BtnSaveAsig_Click" Visible="False" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="BtnClearAsig" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                                    Height="25px" Text="Deshacer" Width="164px" OnClick="BtnClearAsig_Click" />
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
                            <cc1:ModalPopupExtender ID="ModalPanelAsignaPersonal" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelAsignaPersonal" TargetControlID="BtnDisparaASignaPersonal">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="BtnDisparaASignaPersonal" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpPanelPDV" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="PanelPDV" runat="server" Style="display: none;">
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
                                                <asp:Panel ID="PanelPuntosVenta" runat="server" Style="vertical-align: middle;">
                                                    <div>
                                                        <asp:ImageButton ID="ImgClosePDV" runat="server" AlternateText="Cerrar Ventana" ToolTip="Cerrar Ventana"
                                                            BackColor="Transparent" Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png"
                                                            Width="23px" OnClick="BtnCOlv_Click" />
                                                    </div>
                                                    <br />
                                                    <div class="centrarcontenido labelsTit2">
                                                        Puntos de venta Campaña</div>
                                                    <br />
                                                    <div>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="LblPlanningPDV" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtPlanningPDV" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                                        ForeColor="White"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LblSelPresupuestoPDV" runat="server" CssClass="labelsN" Text="Campaña:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LblTxtPresupuestoPDV" runat="server" Text="Presupuesto" Width="500px"
                                                                        Enabled="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <asp:Button ID="BtnAddPDV" runat="server" CssClass="buttonAddPersonal" Height="25px"
                                                            Text="Agregar PDV" Width="164px" />
                                                        <br />
                                                        <br />
                                                        <fieldset id="Fieldset10" runat="server">
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
                                                                                    <asp:DropDownList ID="CmbSelCity" runat="server" 
                                                                                        AutoPostBack="True" Width="180px"
                                                                                        OnSelectedIndexChanged="CmbSelCity_SelectedIndexChanged">
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
                                                                                        OnSelectedIndexChanged="CmbSelTipoAgrup_SelectedIndexChanged">
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
                                                                                        OnSelectedIndexChanged="CmbSelAgrup_SelectedIndexChanged">
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
                                                                                        OnSelectedIndexChanged="CmbSelOficina_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label3" runat="server" Text="Región" CssClass="labelsN"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="CmbSelMalla" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CmbSelMalla_SelectedIndexChanged"
                                                                                        Width="180px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label4" runat="server" Text="Zona" CssClass="labelsN"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="CmbSelSector" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CmbSelSector_SelectedIndexChanged"
                                                                                        Width="180px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                        <br />
                                                        <div class="p" style="width: 1000px; height: 250px;">
                                                            <asp:GridView ID="GvPDV" runat="server" AutoGenerateColumns="False" Font-Names="Verdana"
                                                                Font-Size="8pt" EmptyDataText="No ha cargado puntos de venta para esa Región y Zona"
                                                                EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-VerticalAlign="Middle"
                                                                Width="985px" ToolTip="Para eliminar el punto de venta oprima el botón rojo ..."
                                                                OnSelectedIndexChanged="GvPDV_SelectedIndexChanged">
                                                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <Columns>
                                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText="Eliminar punto de venta"
                                                                        ShowSelectButton="True" />
                                                                    <asp:BoundField DataField="Código" HeaderText="Cod.PDV">
                                                                        <ItemStyle Width="80px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Punto de Venta" HeaderText="Punto de Venta" />
                                                                    <asp:BoundField DataField="Dirección" HeaderText="Dirección" />
                                                                    <asp:BoundField DataField="Región" HeaderText="Región" />
                                                                    <asp:BoundField DataField="Zona" HeaderText="Zona" />
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:GridView ID="GVPDVDelete" runat="server" AutoGenerateColumns="False" Font-Names="Verdana"
                                                                Visible="false" Font-Size="8pt" EmptyDataText="No ha cargado puntos de venta para esa Región y Zona"
                                                                EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-VerticalAlign="Middle"
                                                                Width="985px" ToolTip="Para eliminar el punto de venta oprima el botón rojo ...">
                                                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Código" HeaderText="Cod.PDV">
                                                                        <ItemStyle Width="80px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Punto de Venta" HeaderText="Punto de Venta" />
                                                                    <asp:BoundField DataField="Dirección" HeaderText="Dirección" />
                                                                    <asp:BoundField DataField="Región" HeaderText="Región" />
                                                                    <asp:BoundField DataField="Zona" HeaderText="Zona" />
                                                                    <%--<asp:BoundField DataField="id_MPOSPlanning" HeaderText="id_MPOSPlanning" />--%>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
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
                            <cc1:ModalPopupExtender ID="ModalPanelPDV" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelPDV" TargetControlID="BtnDisparaPDV">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="BtnDisparaPDV" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                            <asp:Panel ID="PanelIframe" runat="server" Style="display: none;">
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
                                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                <asp:Panel ID="PanelIframePDV" runat="server" Style="vertical-align: middle;">
                                                    <div>
                                                        <asp:ImageButton ID="ImgClosePanelPDV" runat="server" AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                                                    </div>
                                                    <br />
                                                    <div align="center">
                                                        <iframe id="ifcarga" runat="server" height="450px" src="" width="1000px"></iframe>
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
                            <cc1:ModalPopupExtender ID="ModalPopupcargapdv" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelIframe" OkControlID="ImgClosePanelPDV" TargetControlID="BtnAddPDV">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpPanelPaneles" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="PanelPanelesPlanning" runat="server" >
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
                                                <asp:Panel ID="PanelPaneles" runat="server" Style="vertical-align: middle; display:none"  >
                                                    <div>
                                                        <asp:ImageButton ID="ImgClosePanelPlanning" runat="server" AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="BtnCOlv_Click" />
                                                    </div>
                                                    <br />
                                                    <table  align="center">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblPlannigPanel" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtCodPlanningPanel" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                                    ForeColor="White"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblTitPresupuestoPanel" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblSelPresupuestoPanel" runat="server" Text="Presupuesto" Width="500px"
                                                                    Enabled="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="colgate" visible="false">
                                                            <td>
                                                                <asp:Label ID="Label23" runat="server" CssClass="labelsN" Text="Tipo Panel"></asp:Label>
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
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblReportesPanel" runat="server" CssClass="labelsN" Text="Reportes"></asp:Label>
                                                                <br />
                                                                <div style="border: 4px solid #B5C5E1; overflow: auto; width: 250px; height: 275px;">
                                                                    <asp:RadioButtonList ID="RbtnListReportPanel" runat="server" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="RbtnListReportPanel_SelectedIndexChanged">
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblPDVPanel" runat="server" CssClass="labelsN" Text="Puntos de Venta"></asp:Label>
                                                                <br />
                                                                <div style="border: 4px solid #B5C5E1; height: 275px;">
                                                                    <div style="overflow: auto; width: 750px; height: 237px;">
                                                                        <asp:GridView ID="GvPDVPaneles" runat="server" EmptyDataText="No existen puntos de venta disponibles para este planning"
                                                                            Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" AutoGenerateColumns="False"
                                                                            EnableModelValidation="True">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="No." ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Left">
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
                                                                                <asp:TemplateField HeaderText="Punto de Venta" ItemStyle-Width="280px">
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
                                                                                <asp:TemplateField HeaderText="Perido" Visible="false" ItemStyle-Width="200px" >
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LabelPeriodo" runat="server" Text='<%# Bind("Periodo")   %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:Label ID="LabelPeriodo" runat="server" Text='<%# Bind("Periodo")  %>'></asp:Label>
                                                                                    </EditItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Tipo Panel" Visible="false" ItemStyle-Width="200px" >
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LabelTipoPanel" runat="server" Text='<%# Bind("tipopanel")   %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:Label ID="LabelTipoPanel" runat="server" Text='<%# Bind("tipopanel")  %>'></asp:Label>
                                                                                    </EditItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                       Año: <asp:DropDownList ID="ddlAño" runat="server">
                                                    <asp:ListItem>2012</asp:ListItem>
                                                    <asp:ListItem>2011</asp:ListItem>
                                                    <asp:ListItem>2010</asp:ListItem>
                                                    </asp:DropDownList>
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
                                                    Perido: <asp:DropDownList ID="ddlPeriodo" runat="server" 
                                                             Width="100px">
                                                         </asp:DropDownList>
                                                                    <br />
                                                                
                                                                    <asp:Button ID="BtnEliminarRegpanel" runat="server" Text="Eliminar puntos" OnClick="BtnEliminarRegpanel_Click"
                                                                        Visible="false" />
                                                                    <asp:Button ID="BtnAddPanel" runat="server" Text="Agregar Puntos" Visible="false"
                                                                        OnClick="BtnAddPanel_Click" />
                                                                    <asp:Button ID="btnActualizarPanel" runat="server" Text="Actualizar puntos"  Visible="false"
                                                                      OnClick="btnActualizarPanel_Click"  />
                                                                    <asp:Button ID="BtnSavePaneles" runat="server" Text="Guardar Puntos" Visible="false"
                                                                        OnClick="BtnSavePaneles_Click" />
                                                                    <asp:Button ID="BtnCancelPaneles" runat="server" Text="Cancelar" Visible="false"
                                                                        OnClick="BtnCancelPaneles_Click" />
                                                                    <div align="right">
                                                                        <asp:Label ID="LblTitCountReg" runat="server" Text="Registros encontrados: " Font-Names="Arial"
                                                                            Font-Size="8pt" Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblcountreg" runat="server" Text="" Font-Names="Arial" Font-Size="8pt"
                                                                            Visible="false"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
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
                            <cc1:ModalPopupExtender ID="ModalPanelPaneles" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelPanelesPlanning" TargetControlID="BtnDisparaPaneles">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="BtnDisparaPaneles" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>



                    <!---------------------------------------------------------------------------->
                    <!---------------------------------------------------------------------------->
                    <!---------------------------------------------------------------------------->                        
                    <!-- PANEL ASIGNACION DE PDV Y TRABAJADORES - INI -->
                    <!---------------------------------------------------------------------------->
                    <!---------------------------------------------------------------------------->
                    <!---------------------------------------------------------------------------->                        
                    <asp:UpdatePanel ID="UpPanelPanelAsignacionPDVaoper" runat="server">
                        <ContentTemplate> 

                            <!-- SECCION CARGA POR SISTEMA - INI -->
                            <asp:Panel ID="PanelAsignacionPDVaoper" runat="server" Style="display: none;">
                                <div>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0">
                                        
                                        <!-- CABECERA - INI -->
                                        <tr>
                                            <td><img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png" width="6"></img></td>
                                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg"><img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1"></img></td>
                                            <td><img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png" width="6"></img></td>
                                        </tr>
                                        <!-- CABECERA - FIN -->

                                        <!-- BODY - INI -->
                                        <tr>
                                            
                                            <!-- HEADER - INI -->
                                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6"></img>
                                            </td>
                                            <!-- HEADER - FIN -->

                                            <!-- BODY - INI -->
                                            <td bgcolor="White">
                                                <asp:Panel ID="PanelPDVOpe" runat="server" Style="vertical-align: middle;">
                                                    
                                                    <!-- DIV CERRAR - INI -->
                                                    <div>
                                                        <asp:ImageButton ID="ImgCloseAsignacionPDVaoper" runat="server" 
                                                            AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" 
                                                            ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="BtnCOlv_Click" />
                                                    </div>
                                                    <!-- DIV CERRAR - FIN -->

                                                    <!-- INFO PLANNING - INI -->
                                                    <table align="center">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblPanningAsigPDVOPE" runat="server" CssClass="labelsN" 
                                                                    Text="Planning No">
                                                                </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtPlanningAsigPDVOPE" runat="server" 
                                                                    BackColor="#CCCCCC" Enabled="False"
                                                                    ForeColor="White" AutoCompleteType="Disabled">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblSelPresupuestoAsigPDVOPE" runat="server" 
                                                                    CssClass="labelsN" Text="Presupuesto">
                                                                </asp:Label>
                                                            </td>
                                                            <td colspan="4">
                                                                <asp:Label ID="LblTxtPresupuestoAsigPDVOPE" runat="server" 
                                                                    Text="Presupuesto" Width="500px" Enabled="False">
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <!-- INFO PLANNING - FIN -->

                                                    <!-- FILTRO MERCADERISTA - INI -->
                                                    <table align="center">
                                                        <tr>
                                                            <td><asp:Label ID="LblSelOpePlanning" runat="server" CssClass="labelsN" 
                                                                    Font-Bold="True"
                                                                    Text="Seleccione Mercaderista"></asp:Label>
                                                            </td>
                                                            <td><asp:DropDownList ID="CmbSelOpePlanning" runat="server" 
                                                                    Width="520px" Enabled="False"
                                                                    AutoPostBack="True" 
                                                                    OnSelectedIndexChanged="CmbSelOpePlanning_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <!-- FILTRO MERCADERISTA - FIN -->

                                                    <!-- SECCION PDV ASIGNADOS - INI -->
                                                    <asp:UpdatePanel ID="asignacionesPDV" runat="server">
                                                        <ContentTemplate>
                                                            <table align="center">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <fieldset id="Fieldset3" runat="server">
                                                                            <legend style="">Asignaciones existentes</legend>
                                                                            <table>
                                                                                <tr>
                                                                                    <td valign="top" style="border-color: Black; height: 70px; border-style: solid; border-width: 1px; width: 1050px; min-width: 1050px;">
                                                                                        <div style="width: 1050px; height: 130px;" class="p">
                                                                                            <!-- SECCION GRILLA PDV ASIGNADOS - INI -->
                                                                                            <asp:GridView ID="GvAsignaPDVOPE" 
                                                                                                runat="server" 
                                                                                                EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                Font-Names="Verdana" Font-Size="8pt" GridLines="Both" Width="1035px" AutoGenerateColumns="False" 
                                                                                                EnableModelValidation="True" OnRowEditing="GvAsignaPDVOPE_RowEditing" OnRowCancelingEdit="GvAsignaPDVOPE_RowCancelingEdit"
                                                                                                OnRowUpdating="GvAsignaPDVOPE_RowUpdating">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="Eliminar">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <%-- <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                                            ShowSelectButton="True" /> --%>
                                                                                                    <asp:TemplateField HeaderText="No.">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <EditItemTemplate>
                                                                                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                                                                                        </EditItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Código">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Código") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <EditItemTemplate>
                                                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Código") %>'></asp:Label>
                                                                                                        </EditItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Nombre">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <EditItemTemplate>
                                                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                                                                                        </EditItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Región">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Región") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <EditItemTemplate>
                                                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Región") %>'></asp:Label>
                                                                                                        </EditItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Zona">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Zona") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <EditItemTemplate>
                                                                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Zona") %>'></asp:Label>
                                                                                                        </EditItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Fecha inicio">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("[Fecha inicio]") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <EditItemTemplate>
                                                                                                            <asp:TextBox ID="TxtFechaini" runat="server" Text='<%# Bind("[Fecha inicio]") %>'
                                                                                                                Width="100px" Font-Names="Verdana" Font-Size="8pt" Enabled="false"></asp:TextBox>
                                                                                                            <asp:ImageButton ID="BtnCalTxtFechaini" runat="server" Height="16px" ImageUrl="~/Pages/images/calendario.JPG"
                                                                                                                Width="16px" />
                                                                                                            <cc1:CalendarExtender ID="CalExtTxtFechaini" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                                                                PopupButtonID="BtnCalTxtFechaini" PopupPosition="TopLeft" TargetControlID="TxtFechaini">
                                                                                                            </cc1:CalendarExtender>
                                                                                                        </EditItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Fecha fin">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("[Fecha fin]") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <EditItemTemplate>
                                                                                                            <asp:TextBox ID="TxtFechafin" runat="server" Text='<%# Bind("[Fecha fin]") %>' Width="100px"
                                                                                                                Font-Names="Verdana" Font-Size="8pt" Enabled="false"></asp:TextBox>
                                                                                                            <asp:ImageButton ID="BtnCalTxtFechafin" runat="server" Height="16px" ImageUrl="~/Pages/images/calendario.JPG"
                                                                                                                Width="16px" />
                                                                                                            <cc1:CalendarExtender ID="CalExtTxtFechafin" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                                                                PopupButtonID="BtnCalTxtFechafin" PopupPosition="TopLeft" TargetControlID="TxtFechafin">
                                                                                                            </cc1:CalendarExtender>
                                                                                                        </EditItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:CommandField ShowEditButton="True" />
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                            <!-- SECCION GRILLA PDV ASIGNADOS - FIN -->
                                                                                        </div>
                                                                                        <!-- SECCION OPCIONES PDV ASIGNADOS - INI -->
                                                                                        <asp:Button ID="Button1" runat="server" CssClass="buttonPlan" OnClick="Button1_Click" Text="Eliminar" Visible="false" />
                                                                                        <asp:Button ID="BtnAllAsigPDV" runat="server" Visible="false" CssClass="buttonsinfondo" Text="Todos" 
                                                                                            OnClick="BtnAllAsigPDV_Click" />
                                                                                        <asp:Button ID="BtnNoneasigPDV" runat="server" Visible="false" CssClass="buttonsinfondo" Text="Ninguno" 
                                                                                            OnClick="BtnNoneasigPDV_Click" />
                                                                                        <!-- SECCION OPCIONES PDV ASIGNADOS - FIN -->
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </fieldset>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <!-- SECCION PDV ASIGNADOS - FIN -->

                                                    <!-- SECCION NUEVA ASIGNACION DE PDV - INI -->
                                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                        <ContentTemplate>
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <fieldset id="Fieldset2" runat="server">
                                                                            <legend style="">Nueva asignacion de Puntos de Venta</legend>
                                                                            
                                                                            <!-- FILTROS - INI -->
                                                                            <table align="center">
                                                                                <tr>
                                                                                    <td>

                                                                                        <!-- FILTROS 01- INI -->
                                                                                        <table align="center">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="LblSelCityRutas" runat="server" Text="Ciudad" CssClass="labelsN"></asp:Label>
                                                                                                    <asp:Label ID="OblLblSelCityRutas" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="CmbSelCityRutas" runat="server" AutoPostBack="True" Width="180px"
                                                                                                        Enabled="False" OnSelectedIndexChanged="CmbSelCityRutas_SelectedIndexChanged">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="LblSelTipoAgrupRutas" runat="server" Text="Tipo Agrupación" CssClass="labelsN"></asp:Label>
                                                                                                    <asp:Label ID="OblLblSelTipoAgrupRutas" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="CmbSelTipoAgrupRutas" runat="server" AutoPostBack="True" Width="180px"
                                                                                                        Enabled="False" OnSelectedIndexChanged="CmbSelTipoAgrupRutas_SelectedIndexChanged">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="LblSelAgrupRutas" runat="server" Text="Agrupación" CssClass="labelsN"></asp:Label>
                                                                                                    <asp:Label ID="OblLblSelAgrupRutas" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="CmbSelAgrupRutas" runat="server" AutoPostBack="True" Width="180px"
                                                                                                        Enabled="False" OnSelectedIndexChanged="CmbSelAgrupRutas_SelectedIndexChanged">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <!-- FILTROS 01- FIN -->

                                                                                    </td>
                                                                                    <td>
                                                                                        
                                                                                        <!-- FILTROS 02- INI -->
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="LblSelOficinaRutas" runat="server" Text="Oficina" CssClass="labelsN"></asp:Label>
                                                                                                    <asp:Label ID="OblLblSelOficinaRutas" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="CmbSelOficinaRutas" runat="server" AutoPostBack="True" Width="180px"
                                                                                                        Enabled="False" OnSelectedIndexChanged="CmbSelOficinaRutas_SelectedIndexChanged">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="LblSelMalla" runat="server" Text="Región" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="CmbSelMallasRutas" runat="server" AutoPostBack="True" Width="180px"
                                                                                                        Enabled="False" AppendDataBoundItems="True" OnSelectedIndexChanged="CmbSelMallasRutas_SelectedIndexChanged">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="LblSelSector" runat="server" Text="Zona" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="CmbSelSectorRutas" runat="server" AutoPostBack="True" Width="180px"
                                                                                                        Enabled="False" OnSelectedIndexChanged="CmbSelSectorRutas_SelectedIndexChanged">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <!-- FILTROS 02- FIN -->

                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <!-- FILTROS - FIN -->

                                                                            <!-- OPCIONES - INI -->
                                                                            <table align="center">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="BtnAllPDV" runat="server" CssClass="buttonsinfondo" Text="Todos" Visible="False" OnClick="BtnAllPDV_Click" />
                                                                                        <asp:Button ID="BtnNonePDV" runat="server" CssClass="buttonsinfondo" Text="Ninguno" Visible="False" OnClick="BtnNonePDV_Click" />
                                                                                        <asp:Label ID="blank" runat="server" Text="-" ForeColor="White"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top" style="border-color: Black; border-style: solid; border-width: 1px; width: 475px; min-width: 475px; height: 70px;">
                                                                                        <asp:CheckBoxList ID="ChkListPDV" runat="server" CssClass="ScrollPersonalPlanning"
                                                                                            Font-Names="Verdana" Font-Size="8pt" RepeatLayout="Flow" Width="475px" Enabled="False" Height="70px">
                                                                                        </asp:CheckBoxList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <!-- OPCIONES - FIN -->

                                                                            <!-- OPCIONES FECHA - INI -->
                                                                            <table align="center">
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
                                                                            <!-- OPCIONES FECHA - FIN -->

                                                                        </fieldset>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="BtnAsigPDVOPE" runat="server" CssClass="pagnext" Text=" ." ToolTip="Asignar" Width="25px" Enabled="False" 
                                                                            OnClick="BtnAsigPDVOPE_Click" />
                                                                    </td>
                                                                    <td>
                                                                        <fieldset id="Fieldset5" runat="server">
                                                                            <legend style="">Asignaciones a crear</legend>
                                                                            
                                                                            <!-- TABLE PDV DISPONIBLES - INI -->
                                                                            <table>
                                                                                <tr>
                                                                                    <td valign="top" style="border-color: Black; height: 201px; border-style: solid; border-width: 1px; width: 570px; min-width: 570px;">
                                                                                        <div style="width: 570px; height: 190px;" class="p">
                                                                                            <!-- GRILLA PDV DISPONIBLES - INI -->
                                                                                            <asp:GridView ID="GvNewAsignaPDVOPE" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" Width="555px" 
                                                                                                OnSelectedIndexChanged="GvNewAsignaPDVOPE_SelectedIndexChanged">
                                                                                                <Columns>
                                                                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                                        ShowSelectButton="True" />
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                            <!-- GRILLA PDV DISPONIBLES - FIN -->
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <!-- TABLE PDV DISPONIBLES - FIN -->
                                                                            <!-- OPCIONES INI -->
                                                                            <table align="center">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="BtnEditAsigPDVOPE" runat="server" CssClass="buttonEditPlan" Height="25px"
                                                                                            Text="Editar" Width="164px" OnClick="BtnEditAsigPDVOPE_Click" />
                                                                                        <asp:Button ID="BtnUpdateAsigPDVOPE" runat="server" CssClass="buttonSavePlan" Visible="false"
                                                                                            Height="25px" Text="Actualizar" Width="164px" OnClick="BtnUpdateAsigPDVOPE_Click" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="BtnClearAsigPDVOPE" runat="server" CssClass="buttonClearPlan" Height="25px"
                                                                                            Text="Limpiar" Width="164px" OnClick="BtnClearAsigPDVOPE_Click" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="BtnCargaPDVOPE" runat="server" BorderStyle="Solid" CssClass="buttonFilePlan"
                                                                                            Height="25px" Enabled="true" Text="Desde Archivo..." Width="164px" OnClick="BtnCargaPDVOPE_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <!-- OPCIONES FIN -->
                                                                        </fieldset>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <!-- SECCION NUEVA ASIGNACION DE PDV - FIN -->
                                                </asp:Panel>
                                            </td>
                                            <!-- BODY - FIN -->

                                            <!-- FOOTER - INI -->
                                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                                </img>
                                            </td>
                                            <!-- FOOTER - FIN -->
                                        </tr>
                                        <!-- BODY - FIN -->

                                        <!-- PIE - INI -->
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
                                        <!-- PIE - FIN -->
                                        
                                    </table>
                                </div>
                            </asp:Panel>

                            <cc1:ModalPopupExtender ID="ModalPanelAsignacionPDVaoper" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelAsignacionPDVaoper" TargetControlID="BtnDisparaAsignacionPDVaoper">
                            </cc1:ModalPopupExtender>

                            <asp:Button ID="BtnDisparaAsignacionPDVaoper" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                            <!-- SECCION CARGA POR SISTEMA - FIN -->


                            <!-- SECCION CARGA POR EXCEL - INI -->
                            <asp:Panel ID="PanelCargaMasivaAsignapdv" runat="server" Style="display: none;">
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
                                                <asp:Panel ID="PanelMasivaPDVOpe" runat="server" Style="vertical-align: middle;">
                                                    <div>
                                                        <asp:ImageButton ID="BtnCloseMasivaPDVOPE" runat="server" AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                                                    </div>
                                                    <div align="center">
                                                        <iframe id="IframeMasivaPDVOpe" runat="server" height="520px" src="" width="1160px">
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

                            <cc1:ModalPopupExtender ID="ModalPopupPanelCargaMasivaPDVOPE" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelCargaMasivaAsignapdv" OkControlID="BtnCloseMasivaPDVOPE"
                                TargetControlID="BtnDisparaMasivaPDVOPE">
                            </cc1:ModalPopupExtender>

                            <asp:Button ID="BtnDisparaMasivaPDVOPE" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                            <!-- SECCION CARGA POR EXCEL - FIN -->

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!---------------------------------------------------------------------------->
                    <!---------------------------------------------------------------------------->
                    <!---------------------------------------------------------------------------->                        
                    <!-- PANEL ASIGNACION DE PDV Y TRABAJADORES - FIN -->
                    <!---------------------------------------------------------------------------->
                    <!---------------------------------------------------------------------------->
                    <!---------------------------------------------------------------------------->



                    <asp:UpdatePanel ID="UpPanelProductos" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="PanelProductos" runat="server" Style="display: none;">
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
                                                <asp:Panel ID="PanelProduct" runat="server" Style="vertical-align: middle;">
                                                    <div>
                                                        <asp:ImageButton ID="ImgCloseProductos" runat="server" AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="BtnCOlv_Click" />
                                                    </div>
                                                    <table align="center">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblPanningAsigProd" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtPlanningAsigProd" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                                    ForeColor="White"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblSelPresupuestoAsigProd" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblTxtPresupuestoAsigProd" runat="server" Text="Presupuesto" Width="500px"
                                                                    Enabled="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label24" runat="server" CssClass="labelsN" Text="Categoria"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlLevantamiento_Categoria" runat="server"  AutoPostBack="true"
                                                                 OnSelectedIndexChanged="llenaLevantamiento_Categoria_SelectedIndexChanged"></asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label25" runat="server" CssClass="labelsN" Text="Subcategoria" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlLevantamiento_Subcategoria" runat="server"  Visible="false"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label28" runat="server" CssClass="labelsN" Text="Marca"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlLevantamiento_Marca" runat="server"  AutoPostBack="true"
                                                                  OnSelectedIndexChanged="ddlLevantamiento_Marca_SelectedIndexChanged"  ></asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                
                                                            </td>
                                                            <td>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <cc1:TabContainer ID="TabAdministradorInformes" runat="server" ActiveTabIndex="0"
                                                        Width="100%" Height="310px" Font-Names="Verdana" BorderColor="#003300">
                                                        <cc1:TabPanel runat="server" HeaderText="Gestión por Productos" ID="Panel_Productos">
                                                            <HeaderTemplate>
                                                                Productos
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table align="center">
                                                                    <tr>
                                                                        <td>
                                                                            <fieldset id="Fieldset6" runat="server">
                                                                                <legend style="">Productos Propios</legend>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td valign="top" style="border-color: Black; height: 100px; border-style: solid;
                                                                                            border-width: 1px; width: 800px; min-width: 800px;">
                                                                                            <div style="width: 800px; height: 100px;" class="p">
                                                                                                <asp:GridView ID="gvproductospropios" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" Width="785px" AutoGenerateColumns="False"
                                                                                                    OnSelectedIndexChanged="gvproductospropios_SelectedIndexChanged" OnRowDeleting="gvproductospropios_RowDeleting" EnableModelValidation="True"
                                                                                                    AllowSorting="True">
                                                                                                    <Columns>
                                                                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                                            ShowSelectButton="True" />
                                                                                                            <asp:BoundField DataField="id_ProductsPlanning" HeaderText="id_ProductsPlanning" ItemStyle-Width="0px"  />
                                                                                                        <asp:BoundField DataField="SKU" HeaderText="SKU" />
                                                                                                        <asp:BoundField DataField="Categoría" HeaderText="Categoría" />
                                                                                                        <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                                                                                        <asp:BoundField DataField="Producto" HeaderText="Producto" />
                                                                                                        <asp:BoundField DataField="Reporte" HeaderText="Reporte" />
                                                                                                        <asp:BoundField DataField="flag_mandatorio" HeaderText="Mandatorio"  Visible="false"  />
                                                                                                        <asp:CommandField ShowDeleteButton="True" DeleteText="Cambiar Mandatorio" Visible="false" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                                <asp:GridView ID="gvproductospropiosDEL" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" Width="785px" Visible="False"
                                                                                                    EnableModelValidation="True">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="id_ProductsPlanning" HeaderText="id_ProductsPlanning" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <fieldset id="Fieldset7" runat="server">
                                                                                <legend style="">Productos Competidor</legend>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td valign="top" style="border-color: Black; height: 100px; border-style: solid;
                                                                                            border-width: 1px; width: 800px; min-width: 800px;">
                                                                                            <div style="width: 800px; height: 100px;" class="p">
                                                                                                <asp:GridView ID="Gvproductoscompetidor" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" Width="785px" AutoGenerateColumns="False"
                                                                                                    OnSelectedIndexChanged="Gvproductoscompetidor_SelectedIndexChanged" EnableModelValidation="True"
                                                                                                    AllowSorting="True">
                                                                                                    <Columns>
                                                                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                                            ShowSelectButton="True" />
                                                                                                        <asp:BoundField DataField="SKU" HeaderText="SKU" />
                                                                                                        <asp:BoundField DataField="Categoría" HeaderText="Categoría" />
                                                                                                        <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                                                                                        <asp:BoundField DataField="Producto" HeaderText="Producto" />
                                                                                                        <asp:BoundField DataField="Competidor" HeaderText="Competidor" />
                                                                                                        <asp:BoundField DataField="Reporte" HeaderText="Reporte" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                                <asp:GridView ID="GvproductoscompetidorDEL" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" Width="785px" Visible="False"
                                                                                                    EnableModelValidation="True">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="id_ProductsPlanning" HeaderText="id_ProductsPlanning" />
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
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                        <cc1:TabPanel runat="server" HeaderText="Gestion por Categorías" ID="Panel_Categorias">
                                                            <HeaderTemplate>
                                                                Categorías
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table align="center">
                                                                    <tr>
                                                                        <td>
                                                                            <fieldset id="Fieldset14" runat="server">
                                                                                <legend style="">Categorías Propios</legend>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td valign="top" style="border-color: Black; height: 100px; border-style: solid;
                                                                                            border-width: 1px; width: 800px; min-width: 800px;">
                                                                                            <div style="width: 800px; height: 100px;" class="p">
                                                                                                <asp:GridView ID="gvcategoriaspropias" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" AutoGenerateColumns="false"
                                                                                                    OnSelectedIndexChanged="gvcategoriaspropias_SelectedIndexChanged" AllowSorting="True">
                                                                                                    <Columns>
                                                                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                                            ShowSelectButton="True" />
                                                                                                        <asp:BoundField DataField="Cod" HeaderText="Cod" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Categoría" HeaderText="Categoría" ItemStyle-Width="720px"
                                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Reporte" HeaderText="Reporte" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                                <asp:GridView ID="gvcategoriaspropiasDEL" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" Width="785px" Visible="false"
                                                                                                    AllowSorting="True">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="id_CategoryPlanning" HeaderText="id_CategoryPlanning" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <fieldset id="Fieldset15" runat="server">
                                                                                <legend style="">Categorías Competidor</legend>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td valign="top" style="border-color: Black; height: 100px; border-style: solid;
                                                                                            border-width: 1px; width: 800px; min-width: 800px;">
                                                                                            <div style="width: 800px; height: 100px;" class="p">
                                                                                                <asp:GridView ID="GvCategoriascompetidor" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" AutoGenerateColumns="false"
                                                                                                    OnSelectedIndexChanged="GvCategoriascompetidor_SelectedIndexChanged" AllowSorting="True">
                                                                                                    <Columns>
                                                                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                                            ShowSelectButton="True" />
                                                                                                        <asp:BoundField DataField="Cod" HeaderText="Cod" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Categoría" HeaderText="Categoría" ItemStyle-Width="400px"
                                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Competidor" HeaderText="Competidor" ItemStyle-Width="400px"
                                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Reporte" HeaderText="Reporte" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                                <asp:GridView ID="GvCategoriascompetidorDEL" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" Width="785px" Visible="false"
                                                                                                    AllowSorting="True">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="id_CategoryPlanning" HeaderText="id_CategoryPlanning" />
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
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                        <cc1:TabPanel runat="server" HeaderText="Gestion por Marcas" ID="Panel_Marcas">
                                                            <HeaderTemplate>
                                                                Marcas
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table align="center">
                                                                    <tr>
                                                                        <td>
                                                                            <fieldset id="Fieldset4" runat="server">
                                                                                <legend style="">Marcas Propios</legend>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td valign="top" style="border-color: Black; height: 100px; border-style: solid;
                                                                                            border-width: 1px; width: 800px; min-width: 800px;">
                                                                                            <div style="width: 800px; height: 100px;" class="p">
                                                                                                <asp:GridView ID="gvmarcaspropias" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" AutoGenerateColumns="false"
                                                                                                    OnSelectedIndexChanged="gvmarcaspropias_SelectedIndexChanged">
                                                                                                    <Columns>
                                                                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                                            ShowSelectButton="True" />
                                                                                                        <asp:BoundField DataField="Categoría" HeaderText="Categoría" ItemStyle-Width="300px"
                                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-Width="485px" HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Reporte" HeaderText="Reporte" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                                <asp:GridView ID="gvmarcaspropiasDEL" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" Width="785px" Visible="false">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="id_BrandPlanning" HeaderText="id_BrandPlanning" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <fieldset id="Fieldset11" runat="server">
                                                                                <legend style="">Marcas Competidor</legend>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td valign="top" style="border-color: Black; height: 100px; border-style: solid;
                                                                                            border-width: 1px; width: 800px; min-width: 800px;">
                                                                                            <div style="width: 800px; height: 100px;" class="p">
                                                                                                <asp:GridView ID="GvMarcascompetidor" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" AutoGenerateColumns="false"
                                                                                                    OnSelectedIndexChanged="GvMarcascompetidor_SelectedIndexChanged" AllowSorting="True">
                                                                                                    <Columns>
                                                                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                                            ShowSelectButton="True" />
                                                                                                        <asp:BoundField DataField="Categoría" HeaderText="Categoría" ItemStyle-Width="300px"
                                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-Width="185px" HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Competidor" HeaderText="Competidor" ItemStyle-Width="300px"
                                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Reporte" HeaderText="Reporte" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                                <asp:GridView ID="GvmarcascompetidorDEL" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" Width="785px" Visible="false">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="id_BrandPlanning" HeaderText="id_BrandPlanning" />
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
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                        <cc1:TabPanel runat="server" HeaderText="Gestion por Familias" ID="Panel_Familias">
                                                            <HeaderTemplate>
                                                                Familias
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table align="center">
                                                                    <tr>
                                                                        <td>
                                                                            <fieldset id="Fieldset12" runat="server">
                                                                                <legend style="">Familias Propios</legend>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td valign="top" style="border-color: Black; height: 100px; border-style: solid;
                                                                                            border-width: 1px; width: 800px; min-width: 800px;">
                                                                                            <div style="width: 800px; height: 100px;" class="p">
                                                                                                <asp:GridView ID="gvFamiliaspropias" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" AutoGenerateColumns="false"
                                                                                                    OnSelectedIndexChanged="gvFamiliaspropias_SelectedIndexChanged" AllowSorting="True">
                                                                                                    <Columns>
                                                                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                                            ShowSelectButton="True" />
                                                                                                        <asp:BoundField DataField="Categoría" HeaderText="Categoría" ItemStyle-Width="300px"
                                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-Width="485px" HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Familia" HeaderText="Familia" ItemStyle-Width="485px"
                                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Reporte" HeaderText="Reporte" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                                <asp:GridView ID="gvFamiliaspropiasDEL" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" Width="785px" Visible="false"
                                                                                                    AllowSorting="True">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="id_FamilyPlanning" HeaderText="id_FamilyPlanning" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <fieldset id="Fieldset13" runat="server">
                                                                                <legend style="">Familias Competidor</legend>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td valign="top" style="border-color: Black; height: 100px; border-style: solid;
                                                                                            border-width: 1px; width: 800px; min-width: 800px;">
                                                                                            <div style="width: 800px; height: 100px;" class="p">
                                                                                                <asp:GridView ID="GvFamiliascompetidor" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" AutoGenerateColumns="false"
                                                                                                    OnSelectedIndexChanged="GvFamiliascompetidor_SelectedIndexChanged" AllowSorting="True">
                                                                                                    <Columns>
                                                                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                                            ShowSelectButton="True" />
                                                                                                        <asp:BoundField DataField="Categoría" HeaderText="Categoría" ItemStyle-Width="300px"
                                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-Width="185px" HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Familia" HeaderText="Familia" ItemStyle-Width="185px"
                                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Competidor" HeaderText="Competidor" ItemStyle-Width="300px"
                                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Reporte" HeaderText="Reporte" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                                <asp:GridView ID="GvFamiliascompetidorDEL" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" Width="785px" Visible="false"
                                                                                                    AllowSorting="True">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="id_BrandPlanning" HeaderText="id_BrandPlanning" />
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
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                        <cc1:TabPanel ID="Panel_SubFamilias" runat="server" HeaderText="Gestion por SubFamilias"  style="display:none">
                                                            <HeaderTemplate>
                                                            SubFamilias
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <div class="centrar">
                                                                    <div class="tabla centrar">
                                                                        <div class="fila">                                                                        
                                                                            <div class="celda">
                                                                            <fieldset>
                                                                            <legend>SubFamilias Propios</legend>
                                                                                <asp:GridView ID="gv_subfamilias_propios" runat="server" EmptyDataText="No se ha realizado ninguna asignación">
                                                                                </asp:GridView>
                                                                            </fieldset>
                                                                            </div>
                                                                        </div>
                                                                        <div class="fila">
                                                                            <div class="celda">
                                                                            </div>
                                                                        </div>
                                                                        <div class="fila">
                                                                            <div class="celda">
                                                                            <fieldset>
                                                                            <legend>SubFamilias Competidor</legend>
                                                                                <asp:GridView ID="gv_subfamilias_competidor" runat="server" EmptyDataText="No se ha realizado ninguna asignación">
                                                                                </asp:GridView>
                                                                            </fieldset>
                                                                            </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                    <cc1:TabPanel runat="server" HeaderText="Gestion por Mat.POP" ID="Panel_MatePOP">
                                                            <HeaderTemplate>
                                                                Material POP
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table align="center">
                                                                    <tr>
                                                                        <td>
                                                                            <fieldset id="Fieldset18" runat="server">
                                                                                <legend style="">Material POP</legend>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td valign="top" style="border-color: Black; height: 100px; border-style: solid;
                                                                                            border-width: 1px; width: 800px; min-width: 800px;">
                                                                                            <div style="width: 800px; height: 100px;" class="p">
                                                                                                <asp:GridView ID="gvMatPOP" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" AutoGenerateColumns="false"
                                                                                                    OnSelectedIndexChanged="gvMatPOP_SelectedIndexChanged" AllowSorting="True">
                                                                                                    <Columns>
                                                                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                                            ShowSelectButton="True" />
                                                                                                        <asp:BoundField DataField="id_MPointOfPurchase" HeaderText="cod" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" Visible="true" />
                                                                                                        <asp:BoundField DataField="Material POP" HeaderText="Material POP" ItemStyle-Width="300px"
                                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Tipo de Material POP" HeaderText="Tipo de Material POP" ItemStyle-Width="485px" HeaderStyle-HorizontalAlign="Left" />
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
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>


                                                        <cc1:TabPanel runat="server" HeaderText="Gestion por Observaciones" ID="TabPanel1">
                                                            <HeaderTemplate>
                                                                Observaciones
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table align="center">
                                                                    <tr>
                                                                        <td>
                                                                            <fieldset id="Fieldset19" runat="server">
                                                                                <legend style="">Observaciones</legend>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td valign="top" style="border-color: Black; height: 100px; border-style: solid;
                                                                                            border-width: 1px; width: 800px; min-width: 800px;">
                                                                                            <div style="width: 800px; height: 100px;" class="p">
                                                                                                <asp:GridView ID="gvObservaciones" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                                                    Font-Names="Verdana" Font-Size="8pt" GridLines="Horizontal" AutoGenerateColumns="false"
                                                                                                    OnSelectedIndexChanged="gvObservaciones_SelectedIndexChanged" AllowSorting="True">
                                                                                                    <Columns>
                                                                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                                            ShowSelectButton="True" />
                                                                                                        <asp:BoundField DataField="id" HeaderText="cod" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" Visible="true" />
                                                                                                        <asp:BoundField DataField="Observacion" HeaderText="Observacion" ItemStyle-Width="300px"
                                                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                                                        <asp:BoundField DataField="Report_NameReport" HeaderText="Reporte" ItemStyle-Width="485px" HeaderStyle-HorizontalAlign="Left" />
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
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>



                                                    </cc1:TabContainer>
                                                </asp:Panel>
                                                <table>
                                                 <tr>
                                                        <td>
                                                             <asp:ImageButton ID="btn_img_exporttoexcel" runat="server" 
                                                                Height="42px" ImageUrl="~/Pages/Modulos/Operativo/Reports/Image/icono_excel.jpg" 
                                                                onclick="btn_img_exporttoexcel_Click" Width="39px"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="BtnAddProductOtros" runat="server" CssClass="buttonAddPersonal" Height="25px"
                                                                Text="adicionar otros" Width="164px" Enabled="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                                </img>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                                    width="6" alt =""> </img>
                                            </td>
                                            <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                                    width="1" alt =""> </img>
                                            </td>
                                            <td>
                                                <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                                    width="6" alt =""></img>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalPanelProductos" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelProductos" TargetControlID="BtnDisparaProductos">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="BtnDisparaProductos" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                            <asp:Panel ID="PanelAsignaProductos" runat="server" Style="display: none;">
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
                                                <div>
                                                    <asp:ImageButton ID="ImgCloseAddProd" runat="server" AlternateText="Cerrar Ventana"
                                                        ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                        ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="ImgCloseAddProd_Click" />
                                                </div>
                                                <asp:Panel ID="PanelProducto" runat="server" Style="vertical-align: middle; min-width: 850px;
                                                    min-height: 450px;">
                                                    <br />
                                                    <br />
                                                    <div class="centrarcontenido" style=" padding:5px">
                                                        <asp:Label ID="LblReporteAsociado" runat="server" CssClass="labelsTitN" Visible="False"></asp:Label>
                                                    </div>
                                                    <fieldset id="Fieldset8" runat="server">
                                                        <legend style="">Adicionar a la campaña</legend>
                                                        <div id="div_masopciones" runat="server" style="display: none;">
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <br />
                                                                        <br />
                                                                        <asp:Label ID="SelMasOpcion" runat="server" Text="Seleccione opción:"></asp:Label>
                                                                        <br />
                                                                        <asp:RadioButtonList ID="RbtMasopciones" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RbtMasopciones_SelectedIndexChanged">
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="divselproductos" runat="server" style="display: none;">
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="BtnProdPropio" runat="server" CssClass="buttonNewPlan" Height="25px"
                                                                            Text="Prod. Propios" Width="164px" OnClick="BtnProdPropio_Click" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="BtnProdCompe" runat="server" CssClass="buttonNewPlan" Height="25px"
                                                                            Text="Prod. Competidor" Width="164px" OnClick="BtnProdCompe_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LblSelCompetidor" runat="server" CssClass="labelsN" Text="Seleccione Competidor"
                                                                            Visible="False"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 27px;">
                                                                        <asp:DropDownList ID="CmbCompetidores" runat="server" AutoPostBack="True" CausesValidation="True"
                                                                            Visible="False" Width="400" OnSelectedIndexChanged="CmbCompetidores_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <%--<table align="center">
                                                            <tr style="height: 200px;">
                                                                <td id="categorias" runat="server"  style="width: 260px; border-color: Black; border-width: 1px; border-style: solid;"
                                                                    valign="top">
                                                                    <asp:Label ID="lblCatego" runat="server" CssClass="labelsN" Text="Categorias"></asp:Label>
                                                                    <asp:Label ID="lblolbli1" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                    <br />
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rbliscatego" runat="server" AutoPostBack="True" CssClass="p"
                                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" RepeatLayout="Flow" Width="250px"
                                                                        OnSelectedIndexChanged="rbliscatego_SelectedIndexChanged">
                                                                    </asp:RadioButtonList>
                                                                     <asp:CheckBoxList ID="Chklistcatego" runat="server" CssClass="p"
                                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" 
                                                                         RepeatLayout="Flow" Width="250px">
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                                <td id="Marcas" runat="server" style="width: 260px; border-color: Black; border-width: 1px; border-style: solid;"
                                                                    valign="top">
                                                                    <asp:Label ID="lblmarca" runat="server" CssClass="labelsN" Text="Marca"></asp:Label>
                                                                    <br />
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblmarca" runat="server" AutoPostBack="True" CssClass="p"
                                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" RepeatLayout="Flow" Width="250px"
                                                                        OnSelectedIndexChanged="rblmarca_SelectedIndexChanged">
                                                                    </asp:RadioButtonList>
                                                                    <asp:CheckBoxList ID="Chklistmarca" runat="server" CssClass="p"
                                                            Font-Names="Arial" Font-Size="8pt" Height="130px" 
                                                             RepeatLayout="Flow" Width="250px">
                                                        </asp:CheckBoxList>
                                                                </td>
                                                                <td id="submarcas" runat="server" style="width: 260px; border-color: Black; border-width: 1px;
                                                                    border-style: solid; display: none;" valign="top">
                                                                    <asp:Label ID="lblsumarca" runat="server" CssClass="labelsN" Text="SubMarca"></asp:Label>
                                                                    <br />
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblsubmarca" runat="server" AutoPostBack="True" CssClass="p"
                                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" RepeatLayout="Flow" Width="250px"
                                                                        OnSelectedIndexChanged="rblsubmarca_SelectedIndexChanged">
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                <td id="Familias" runat="server" style="width: 260px; border-color: Black; border-width: 1px; border-style: solid;"
                                                                    valign="top">
                                                                    <asp:Label ID="lblFamilia" runat="server" CssClass="labelsN" Text="Familias"></asp:Label>
                                                                    <br />
                                                                    <br />                                                    
                                                                    <asp:CheckBoxList ID="ChkListFamilias" runat="server" CssClass="p"
                                                                        Font-Names="Arial" Font-Size="8pt" Height="130px" 
                                                                         RepeatLayout="Flow" Width="250px">
                                                                    </asp:CheckBoxList>

                                                                </td>
                                                                <td id="productos" runat="server" style="width: 390px; border-color: Black; border-width: 1px; border-style: solid;"
                                                                    valign="top">
                                                                    <asp:Label ID="lblProducts" runat="server" CssClass="labelsN" Text="Productos"></asp:Label>
                                                                    <asp:Label ID="lblolbli4" runat="server" ForeColor="#CC3300" Text="*"></asp:Label>
                                                                    <br />
                                                                    <br />
                                                                    <asp:CheckBoxList ID="ChkProductos" runat="server" CssClass="p" Font-Names="Verdana"
                                                                        Font-Size="8pt" Height="130px" RepeatLayout="Flow" Width="380px">
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                            </tr>
                                                        </table> --%>
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
                                             <table align="center">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="BtnSaveProd" runat="server" CssClass="buttonSavePlan" Height="25px"
                                                                    Text="Guardar" Width="164px" Enabled="False" OnClick="BtnSaveProd_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="BtnClearProd" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                                    Height="25px" Text="Otro Reporte" Width="164px" OnClick="BtnClearProd_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="BtnCargaLevanInform" runat="server" BorderStyle="Solid" CssClass="buttonClearPlan"
                                                                    Height="25px" Text="Carga Masiva" Width="164px" />
                                                            </td>
                                                        </tr>
                                                    </table>    
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



                                                        <br />
                                                    </fieldset>
                                                    <br />
                                                   
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
                            <asp:Button ID="BtnCloseAddProductos" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                            <cc1:ModalPopupExtender ID="ModalPanelAsignaProductos" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelAsignaProductos" TargetControlID="BtnCloseAddProductos">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="PanelCargaMasivaProductos" runat="server" Style="display: none;">
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
                                            <td>
                                                <asp:Panel ID="PanelCargMasivaProd" runat="server" Style="vertical-align: middle;">
                                                    <div style="position: absolute; z-index: 1; width: 99%" align="right">
                                                        <asp:ImageButton ID="BtnCloseMasivaProd" runat="server" AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="BtnCloseMasivaProd_Click" />
                                                    </div>
                                                    <div align="center">
                                                        <iframe id="IframeCargaMasivaProd" runat="server" height="450px" src="CargaLevInformacion.aspx"
                                                            width="1230px"></iframe>
                                                    </div>
                                                    <div style="display: none;">
                                                        <asp:GridView ID="GvMasivaProd" runat="server" Height="250px" Visible="False" Width="100%">
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
                            <cc1:ModalPopupExtender ID="ModalPanelMasivaProd" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelCargaMasivaProductos" TargetControlID="BtnCargaLevanInform">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panelreporteproducto" style="display:none"  CssClass="borde_panel" runat="server" BackColor="White" BorderColor="#7F99CC"
                                Width="840PX" Height="420PX" BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana"
                                Font-Size="10pt" >
                                <div>
                                    <div class="centrarcontenido"><asp:Label ID="LblReporteproducto" runat="server" Text="Reportes"></asp:Label></div>
                                    <asp:ImageButton ID="ImgCloseVistas" runat="server" BackColor="Transparent" Height="22px"
                                        ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="ImgCloseVistas_Click" />
                                </div>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <div style="border: 4px solid #B5C5E1; overflow: auto; width: 250px; height: 190px;">
                                                <asp:RadioButtonList ID="RbtnListInfProd" runat="server">
                                                </asp:RadioButtonList>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <br />
                                <div align="center">
                                    <asp:Button ID="BtnSelInfProd" runat="server" Text="Seleccionar" OnClick="BtnSelInfProd_Click" />
                                   
                                    <br />
                                </div>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalPanelreporteproducto" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="Panelreporteproducto" TargetControlID="BtnAddProductOtros">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="BtnDisparareporteproducto" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   

                    <asp:UpdatePanel ID="UpPanelReportes" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="PanelReportesPlanning" runat="server" Style="display: none;">
                                <div>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                                    width="6"> </img>
                                            </td>
                                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1" >
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
                                                <asp:Panel ID="PanelReportesPlan" runat="server" Style="vertical-align: middle;">
                                                    <div>
                                                        <asp:ImageButton ID="ImgCloseReportplan" runat="server" AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="BtnCOlv_Click" />
                                                    </div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblPanningAsigReportes" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtPlanningAsigReportes" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                                    ForeColor="White"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblSelPresupuestoAsigReportes" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblTxtPresupuestoAsigReportes" runat="server" Text="Presupuesto" Width="500px"
                                                                    Enabled="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <fieldset id="Fieldset9" runat="server">
                                                        <legend style="">Asignación de Informes por campaña</legend>
                                                        <br />
                                                        <table align="center">
                                                            <tr>
                                                                <td style="width: 900px;">
                                                                    <div style="overflow: auto; width: 900px; height: 250px; border-style: solid; border-width: 1px;">
                                                                        <asp:GridView ID="GVReportesAsignados" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                            Font-Names="Verdana" Font-Size="8pt" GridLines="None" AutoGenerateColumns="False"
                                                                            OnSelectedIndexChanged="GVReportesAsignados_SelectedIndexChanged" OnRowCancelingEdit="GVReportesAsignados_RowCancelingEdit"
                                                                            OnRowEditing="GVReportesAsignados_RowEditing" OnRowUpdating="GVReportesAsignados_RowUpdating"
                                                                            EnableModelValidation="True">
                                                                            <Columns>
                                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                                                    ShowSelectButton="True" />
                                                                                <asp:TemplateField HeaderText="Cod_">
                                                                                    <EditItemTemplate>
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Cod_") %>'></asp:Label>
                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Cod_") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Informe">
                                                                                    <EditItemTemplate>
                                                                                        <asp:Label ID="LblReporte" runat="server" Text='<%# Bind("Informe") %>'></asp:Label>
                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblReporte" runat="server" Text='<%# Bind("Informe") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="150px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Año">
                                                                                    <EditItemTemplate>
                                                                                        <asp:Label ID="LblAño" runat="server" Text='<%# Bind("Año") %>'></asp:Label>
                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblAño" runat="server" Text='<%# Bind("Año") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Mes_">
                                                                                    <EditItemTemplate>
                                                                                        <asp:Label ID="LblMes" runat="server" Text='<%# Bind("Mes_") %>'></asp:Label>
                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblMes" runat="server" Text='<%# Bind("Mes_") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Asignado">
                                                                                    <EditItemTemplate>
                                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Asignado") %>'></asp:Label>
                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Asignado") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="150px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Periodos">
                                                                                    <EditItemTemplate>
                                                                                        <asp:Label ID="LblPeriodo" runat="server" Text='<%# Bind("Periodos") %>'></asp:Label>
                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblPeriodo" runat="server" Text='<%# Bind("Periodos") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Fecha inicio">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="TxtFechaini" runat="server" Text='<%# Bind("Fecha_inicio") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="TxtFechaini" runat="server" Text='<%# Bind("Fecha_inicio") %>' Width="90px"
                                                                                            Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>
                                                                                        <cc1:MaskedEditExtender ID="TxtFechaini_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechaini"
                                                                                            UserDateFormat="DayMonthYear">
                                                                                        </cc1:MaskedEditExtender>
                                                                                    </EditItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Fecha fin">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="TxtFechafin" runat="server" Text='<%# Bind("Fecha_fin") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="TxtFechafin" runat="server" Width="90px" Text='<%# Bind("Fecha_fin") %>'
                                                                                            Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>
                                                                                        <cc1:MaskedEditExtender ID="TxtFechafin_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechafin"
                                                                                            UserDateFormat="DayMonthYear">
                                                                                        </cc1:MaskedEditExtender>
                                                                                    </EditItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:CommandField ShowEditButton="true" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <asp:GridView ID="GVReportesAsignadosDEL" runat="server" EmptyDataText="No se ha realizado ninguna asignación"
                                                                            Font-Names="Verdana" Font-Size="8pt" GridLines="None" AutoGenerateColumns="False"
                                                                            Visible="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="id_ReportsPlanning" HeaderText="id_ReportsPlanning" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                    &nbsp;<asp:Button ID="BtnaddReportes" runat="server" CssClass="buttonAddPersonal"
                                                                        Height="25px" Text="   Adicionar reportes" Width="164px" Enabled="true" />
                                                                    <asp:ImageButton ID="BtnCarga80" Visible="false" ToolTip="Cargar Archivo 80" runat="server"
                                                                        ImageUrl="~/Pages/images/a80.png" Width="20px" Height="20px" ImageAlign="Right"
                                                                        OnClick="BtnCarga80_Click" />
                                                                    <asp:ImageButton ID="BtnCarga20" Visible="false" ToolTip="Cargar Archivo 20" runat="server"
                                                                        ImageUrl="~/Pages/images/a20.png" Width="20px" Height="20px" ImageAlign="Right"
                                                                        OnClick="BtnCarga20_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </fieldset>
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
                            <cc1:ModalPopupExtender ID="ModalPanelReportPlan" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelReportesPlanning" TargetControlID="BtnDisparaReportesPlan">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="BtnDisparaReportesPlan" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                            <asp:Panel ID="PanelReportesCampaña" runat="server" Style="display: none;">
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
                                                    <div>
                                                        <asp:ImageButton ID="ImgCloseReportes" runat="server" AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="ImgCloseReportes_Click" />
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblPanningAsigReports" runat="server" CssClass="labelsN" Text="Planning No"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtPlanningAsigReports" runat="server" BackColor="#CCCCCC" Enabled="False"
                                                                    ForeColor="White"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblSelPresupuestoAsigReports" runat="server" CssClass="labelsN" Text="Presupuesto"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblTxtPresupuestoAsigReports" runat="server" Text="Presupuesto" Width="500px"
                                                                    Enabled="false"></asp:Label>
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
                                                                    <asp:RadioButtonList ID="RbtnListmeses" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RbtnListmeses_SelectedIndexChanged">
                                                                    </asp:RadioButtonList>
                                                                    <%--<asp:CheckBoxList ID="ChkListMeses" runat="server" Width="100%">
                                                                    </asp:CheckBoxList>--%>
                                                                </div>
                                                            </td>
                                                            <td style="width: 120px;">
                                                                <asp:Label ID="LblSelFrecuencia" runat="server" Text="Periodos"></asp:Label>
                                                                <br />
                                                                <asp:Button ID="BtnAllPer" runat="server" CssClass="buttonsinfondo" Text="Todos"
                                                                    Visible="true" OnClick="BtnAllPer_Click" />
                                                                <asp:Button ID="BtnNonePer" runat="server" CssClass="buttonsinfondo" Text="Ninguno"
                                                                    Visible="true" OnClick="BtnNonePer_Click" />
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
                                                            <td style="width: 450px;">
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
                                                    <br />
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
                            <cc1:ModalPopupExtender ID="ModalPanelReportes" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelReportesCampaña" OkControlID="ImgCloseReport"
                                TargetControlID="BtnaddReportes">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="ImgCloseReport" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                            <asp:Panel ID="PanelCarga2080" runat="server" Style="display: none;">
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
                                            <td>
                                                <asp:Panel ID="PanelMasiva2080" runat="server" Style="vertical-align: middle;">
                                                    <div style="position: absolute; z-index: 1; width: 99%;">
                                                        <asp:ImageButton ID="BtnCloseMasiva2080" runat="server" AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="BtnCloseMasiva2080_Click" />
                                                    </div>
                                                    <div align="center">
                                                        <iframe id="IframeMasiva2080" runat="server" height="395px" src="" width="1255px">
                                                        </iframe>
                                                    </div>
                                                    <div style="display: none;">
                                                        <asp:GridView ID="GvMasiva2080" runat="server" Height="250px" Visible="False" Width="100%">
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
                            <cc1:ModalPopupExtender ID="ModalPopupCarga2080" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PanelCarga2080" OkControlID="BtnCloseMasiva2080"
                                TargetControlID="Btndispara2080">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="Btndispara2080" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <%--panel de mensaje de usuario   --%>
                            <asp:Panel ID="MensajeAsignacionPresupuesto" runat="server" Height="169px" Style="display: none;"
                                Width="332px">
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
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnaceptar" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                                Text="Aceptar" OnClick="btnaceptar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MPMensajeAsignacionPresupuesto" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="MensajeAsignacionPresupuesto" TargetControlID="btndipararalerta">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btndipararalerta" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="MensajeDescripcionCampaña" runat="server" Height="169px" Style="display: none;"
                                Width="332px">
                                <table align="center">
                                    <tr>
                                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                                            <br />
                                        </td>
                                        <td style="width: 220px; height: 119px;" valign="top">
                                            <br />
                                            <asp:Label ID="lblencabezadoDesc" runat="server" CssClass="labelsTit"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lblmensajegeneralDesc" runat="server" CssClass="labels"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnaceptarDesc" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                                Text="Aceptar" OnClick="btnaceptarDesc_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MPMensajeDescripcionCampaña" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="MensajeDescripcionCampaña" TargetControlID="btndisparaalertaDesc">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btndisparaalertaDesc" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="MensajeResponsablesCampaña" runat="server" Height="169px" Style="display: none;"
                                Width="332px">
                                <table align="center">
                                    <tr>
                                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                                            <br />
                                        </td>
                                        <td style="width: 220px; height: 119px;" valign="top">
                                            <br />
                                            <asp:Label ID="lblencabezadoResp" runat="server" CssClass="labelsTit"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lblmensajegeneralResp" runat="server" CssClass="labels"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="BbtnaceptarResp" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                                Text="Aceptar" OnClick="BbtnaceptarResp_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MPMensajeResponsablesCampaña" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="MensajeResponsablesCampaña" TargetControlID="btndisparaalertaResp">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btndisparaalertaResp" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="MensajePuntosDV" runat="server" Height="169px" Style="display: none;"
                                Width="332px">
                                <table align="center">
                                    <tr>
                                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                                            <br />
                                        </td>
                                        <td style="width: 220px; height: 119px;" valign="top">
                                            <br />
                                            <asp:Label ID="lblencabezadoMPDV" runat="server" CssClass="labelsTit"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lblmensajegeneralMPDV" runat="server" CssClass="labels"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnaceptarMPDV" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                                Text="Aceptar" OnClick="btnaceptarMPDV_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MPMensajeMPDV" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="MensajePuntosDV" TargetControlID="btndisparaalertaMPDV">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btndisparaalertaMPDV" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="MensajeAsignaPDVOPE" runat="server" Height="169px" Style="display: none;"
                                Width="332px">
                                <table align="center">
                                    <tr>
                                        <td align="center" style="height: 119px; width: 79px;" valign="top"><br />
                                        </td>
                                        <td style="width: 220px; height: 119px;" valign="top"><br />
                                            <asp:Label ID="lblencabezadoPDVOPE" runat="server" CssClass="labelsTit"></asp:Label><br /><br />
                                            <asp:Label ID="lblmensajegeneralPDVOPE" runat="server" CssClass="labels"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="BtnaceptaPDVOPE" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                                Text="Aceptar" OnClick="BtnaceptaPDVOPE_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MPMensajeAsignaPDVOPE" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="MensajeAsignaPDVOPE" TargetControlID="btndisparaalertaPDVOPE">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btndisparaalertaPDVOPE" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="MensajeProductos" runat="server" Height="169px" Style="display: none;"
                                Width="332px">
                                <table align="center">
                                    <tr>
                                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                                            <br />
                                        </td>
                                        <td style="width: 220px; height: 119px;" valign="top">
                                            <br />
                                            <asp:Label ID="lblencabezadoProductos" runat="server" CssClass="labelsTit"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lblmensajegeneralProductos" runat="server" CssClass="labels"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="BtnaceptaProductos" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                                Text="Aceptar" OnClick="BtnaceptaProductos_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MPMensajeProductos" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="MensajeProductos" TargetControlID="btndisparaalertaProductos">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btndisparaalertaProductos" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="PMensajeVista" runat="server" Height="169px" Style="display: none;"
                                Width="332px">
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
                                                Text="Aceptar" OnClick="BtnAceptaMensajeVista_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalMensajeVista" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PMensajeVista" TargetControlID="btndipararalertavista">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btndipararalertavista" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="BtnConfirmacion" runat="server" Text="" Height="0px" CssClass="alertas"
                                Width="0" Enabled="false" />
                            <asp:Panel ID="PanelConfirmacion" runat="server" Width="332px" CssClass="altoverow"
                                Style="display: none;">
                                <table align="center" style="width: 95%;">
                                    <tr>
                                        <td align="center" valign="top"><br />
                                            <asp:Label ID="LblSrUsuario" runat="server" Text="Sr. Usuario"></asp:Label>
                                            <br />
                                            <asp:Label ID="LblMensajeConfirm" runat="server"></asp:Label>
                                            <br /><br />
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
                                            </table><br />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalConfirmacion" runat="server" Enabled="True" TargetControlID="BtnConfirmacion"
                                PopupControlID="PanelConfirmacion" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="BtnConfirmacionProd" runat="server" Text="" Height="0px" CssClass="alertas"
                                Width="0" Enabled="false" />
                            <asp:Panel ID="PanelConfirmaProd" runat="server" Width="332px" CssClass="altoverow"
                                Style="display: none;">
                                <table align="center" style="width: 95%;">
                                    <tr>
                                        <td align="center" valign="top">
                                            <br />
                                            <asp:Label ID="LblSrUsuarioProd" runat="server" Text="Sr. Usuario"></asp:Label>
                                            <br />
                                            <asp:Label ID="LblMensajeConfirProd" runat="server"></asp:Label>
                                            <br />
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="BtnSiConfirmaProd" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                                            Text="SI" OnClick="BtnSiConfirmaProd_Click" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Button ID="BtnNoConfirmaProd" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                                            Text="NO" OnClick="BtnNoConfirmaProd_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalConfirmaProd" runat="server" Enabled="True" TargetControlID="BtnConfirmacionProd"
                                PopupControlID="PanelConfirmaProd" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="BtnConfirmacionReport" runat="server" Text="" Height="0px" CssClass="alertas"
                                Width="0" Enabled="false" />
                            <asp:Panel ID="PanelConfirmaReport" runat="server" Width="332px" CssClass="altoverow"
                                Style="display: none;">
                                <table align="center" style="width: 95%;">
                                    <tr>
                                        <td align="center" valign="top">
                                            <br />
                                            <asp:Label ID="LblSrUsuarioReport" runat="server" Text="Sr. Usuario"></asp:Label>
                                            <br />
                                            <asp:Label ID="LblMensajeConfirReport" runat="server"></asp:Label>
                                            <br />
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="BtnSiConfirmaReport" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                                            Text="SI" OnClick="BtnSiConfirmaReport_Click" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Button ID="BtnNoConfirmaReport" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                                            Text="NO" OnClick="BtnNoConfirmaReport_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalConfirmaReport" runat="server" Enabled="True" TargetControlID="BtnConfirmacionReport"
                                PopupControlID="PanelConfirmaReport" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="MensajeAsignaReports" runat="server" Height="169px" Style="display: none;"
                                Width="332px">
                                <table align="center">
                                    <tr>
                                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                                            <br />
                                        </td>
                                        <td style="width: 220px; height: 119px;" valign="top">
                                            <br />
                                            <asp:Label ID="lblencabezadoReports" runat="server" CssClass="labelsTit"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lblmensajegeneralReports" runat="server" CssClass="labels"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="BtnaceptaReports" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                                Text="Aceptar" OnClick="BtnaceptaReports_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MPMensajeAsignaReports" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="MensajeAsignaReports" TargetControlID="btndisparaalertaReports">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btndisparaalertaReports" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="MensajeAsignaRepDel" runat="server" Height="169px" Style="display: none;"
                                Width="332px">
                                <table align="center">
                                    <tr>
                                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                                            <br />
                                        </td>
                                        <td style="width: 220px; height: 119px;" valign="top">
                                            <br />
                                            <asp:Label ID="lblencabezadoRepDel" runat="server" CssClass="labelsTit"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lblmensajegeneralRepDel" runat="server" CssClass="labels"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="BtnaceptaReportsDel" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                                Text="Aceptar" OnClick="BtnaceptaReportsDel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MPMensajeAsignaRepDel" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="MensajeAsignaRepDel" TargetControlID="btndisparaalertaReportsDel">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btndisparaalertaReportsDel" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <%--panel de mensaje de usuario paneles   --%>
                            <asp:Panel ID="MensajePaneles" runat="server" Height="169px" Style="display: none;"
                                Width="332px">
                                <table align="center">
                                    <tr>
                                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                                            <br />
                                        </td>
                                        <td style="width: 220px; height: 119px;" valign="top">
                                            <br />
                                            <asp:Label ID="lblencabezadoPaneles" runat="server" CssClass="labelsTit"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lblmensajegeneralPaneles" runat="server" CssClass="labels"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="BtnaceptaPaneles" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                                Text="Aceptar" OnClick="BtnaceptaPaneles_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MPMensajePaneles" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="MensajePaneles" TargetControlID="btndipararpaneles">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btndipararpaneles" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <%--panel de mensaje de usuario paneles   --%>
                            <asp:Panel ID="MensajeSeguimiento" runat="server" Height="169px" Style="display: none;"
                                Width="332px">
                                <table align="center">
                                    <tr>
                                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                                            <br />
                                        </td>
                                        <td style="width: 220px; height: 119px;" valign="top">
                                            <br />
                                            <asp:Label ID="lblencabezadoSeguimiento" runat="server" CssClass="labelsTit"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lblmensajegeneralSeguimiento" runat="server" CssClass="labels"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="BtnaceptaSeguimiento" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                                Text="Aceptar" onclick="btnAceptaSeguimiento"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MPMensajeSeguimiento" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="MensajeSeguimiento" TargetControlID="btndipararseguimiento">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btndipararseguimiento" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UP_GestionNiveles" runat="server">
                        <ContentTemplate>

                            <asp:Panel ID="Panel_Registrar_PDVNivel" runat="server" Style="display: none">
                            <div class="p borde_panel" style="width: 650px; height: 260px; background-color: #FFFFFF; padding: 2px 2px; font-size: 9pt;
    	                                                font-family : arial, Helvetica, sans-serif;">
                            <br />
                            <div class="centrarcontenido">
                             <span class="labelsN">NUEVOS REGISTROS PARA NIVELES.</span>
                            </div>
                            <br />
                            <div class="centrar">
                            <div class="tabla centrar" style=" padding: 10px">
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labelsN" style=" padding-right:10px">Agrup. Comercial</span>
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="ddl_gestionniveles_nodo" runat="server" 
                                            onselectedindexchanged="ddl_gestionniveles_nodo_SelectedIndexChanged" AutoPostBack="true" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labelsN" style=" padding-right:10px">Punto de Venta</span>
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="ddl_gestionniveles_pdv" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labelsN" style=" padding-right:10px">Nivel</span>
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="ddl_gestionniveles_nivel" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labelsN" style=" padding-right:10px">Mes Inicio</span>
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="ddl_gestionniveles_mesini" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labelsN" style=" padding-right:10px">Mes Fin</span>
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="ddl_gestionniveles_mesfin" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labelsN" style=" padding-right:10px">Frecuencia</span>
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="ddl_gestionniveles_frecuencia" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            </div>
                            <br />
                            <div class="centrarcontenido">
                            <asp:Button ID="btn_gestionniveles_crear" CssClass="buttonNewPlan" Width="164px" Height="25px" runat="server" Text="Crear" 
                                onclick="btn_gestionniveles_crear_Click"/>
                            <asp:Button ID="btn_gestionniveles_guardar" CssClass="buttonNewPlan" Width="164px" Height="25px" runat="server" Text="Guardar" 
                                onclick="btn_gestionniveles_guardar_Click" Visible="false" />
                            <asp:Button ID="btn_gestionniveles_listacomp" CssClass="buttonNewPlan" Width="164px" Height="25px" runat="server" Text="Lista PDV Evaluados" 
                                onclick="btn_gestionniveles_listacomp_Click" />
                            <asp:Button ID="btn_gestionniveles_listanuev" CssClass="buttonNewPlan" Width="164px" Height="25px" runat="server" Text="Lista PDV Nuevos" 
                                onclick="btn_gestionniveles_listanuev_Click" />
                                <asp:Button ID="btn_gestionniveles_salir" CssClass="buttonNewPlan" Width="164px" Height="25px" runat="server" Text="Salir"
                                onclick="btn_gestionniveles_salir_Click" />
                                </div>
                            </div>
                            <br />
                            <br />                        
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MP_AgregarPDVNivel" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="Panel_Registrar_PDVNivel" TargetControlID="btn_agrega_pdvnivel">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btn_agrega_pdvnivel" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                            <asp:Panel id="P_GestionNiveles" runat="server" Style="display: none">
                            <div class="p borde_panel" style="width: 1000px; height: auto; max-height: 550px; background-color: #FFFFFF; padding: 10px; font-size: 9pt;
    	                                                font-family : arial, Helvetica, sans-serif;"> 
                                    <br />
                                    <div class="centrarcontenido">
                                        <span class="labelsN">LISTADO DE PUNTOS DE VENTA Y NIVELES</span>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="centrarcontenido">
                                        <asp:GridView ID="rgv_lista_pdv_nivel" runat="server" 
                                            EnableModelValidation="True" AutoGenerateColumns="False" Width="100%" 
                                            onrowcommand="rgv_lista_pdv_nivel_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ver">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnk_detalle" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                        CommandName="DETALLE" Font-Underline="True">Detalle</asp:LinkButton>
                                                    </ItemTemplate>                                                
                                                    <ItemStyle Width="60px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ID_MPOSPLANNING" HeaderText="N°" 
                                                    ItemStyle-Width="70">
                                                <ItemStyle Width="70px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PDV_NAME" HeaderText="Punto de Venta" 
                                                    ItemStyle-Width="120">
                                                <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="COMMERCIAL_NODE_NAME" HeaderText="Agrup. Comercial" 
                                                    ItemStyle-Width="170">
                                                <ItemStyle Width="170px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PDV_ADDRESS" HeaderText="Dirección"/>
                                                <asp:BoundField DataField="NAME_DISTRICT" HeaderText="Distrito"/>
                                                <asp:BoundField DataField="NIVEL1" HeaderText="Nivel 1"/>
                                                <asp:BoundField DataField="NIVEL2" HeaderText="Nivel 2"/>
                                                <asp:BoundField DataField="EVALUACION" HeaderText="Evaluación" 
                                                    ItemStyle-Width="75">
                                                <ItemStyle Width="75px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                        <br />
                                    </div>
                                    <div class="centrarcontenido">
                                        <asp:Button ID="btn_salir_gestionniveles" CssClass="buttonNewPlan" Width="164px" Height="25px" runat="server" Text="Salir" 
                                            onclick="btn_salir_gestionniveles_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MP_GestionNiveles" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="P_GestionNiveles" TargetControlID="btn_gestionnivel">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btn_gestionnivel" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                            <asp:Panel id="P_Detalle" runat="server" Style="display: none">
                                <div class="p borde_panel" style="width: 680px; height:auto; background-color: #FFFFFF; padding: 10px; font-size: 9pt;
    	                                                    font-family : arial, Helvetica, sans-serif;">
                                    <div class="centrarcontenido">
                                    <asp:GridView ID="gv_detalle_pdv_nivel" runat="server" Width="100%" 
                                            EnableModelValidation="True" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="id_planivel" HeaderText="N° Reg." />
                                            <asp:BoundField DataField="pdv_name" HeaderText="Nombre PDV" />
                                            <asp:BoundField DataField="Segment_Name" HeaderText="Nivel" />
                                            <asp:BoundField DataField="mes_ini" HeaderText="Mes Inicio" />
                                            <asp:BoundField DataField="mes_fin" HeaderText="Mes Final" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Evaluación" />
                                            <asp:BoundField DataField="txt_Frecuencia" HeaderText="Frecuencia" />
                                            <asp:BoundField DataField="Dateby" HeaderText="Fecha Registro" />
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                        <asp:Button ID="btn_detalleniveles_salir" CssClass="buttonNewPlan" Width="164px" Height="25px" runat="server" Text="Salir" 
                                        onclick="btn_detalleniveles_salir_Click"/>
                                    </div>
                                </div>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MP_Detalle" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="P_Detalle" TargetControlID="btn_detalle_pdv_nivel">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btn_detalle_pdv_nivel" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>                    
                    </asp:UpdatePanel>
                    

                    <asp:UpdatePanel ID="UP_GestionFuerzaVenta" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="P_FuerzaVenta" runat="server" Style=" display:none">

                                <div class="p borde_panel autosize" style="width: 650px; height: 300px">
                                            <asp:ImageButton ID="ImgCerrarGestionFuerzaVenta" runat="server" AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                                            <br />
                                            <br />
                                            
                                            <table style=" width:450px; margin:auto; border:0px;" >
                                            <tbody>
                                            <tr>
                                                <td><div class="centrar">
                                                <div class="tabla centrar" style="padding-left:10px; padding-top:30px;">
                                                    <div class="fila">
                                                        <div class="celda"><span>Nombre Contacto</span></div>
                                                        <div class="celda"><asp:TextBox ID="txt_fuerzaventa_nombre" runat="server"></asp:TextBox></div>
                                                    </div>
                                                    <div class="fila">
                                                        <div class="celda"><span>Tipo de Contacto</span></div>
                                                        <div class="celda">
                                                            <asp:DropDownList ID="ddl_fuerzaventa_tipo" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                                                                    
                                                        <div style=" width:165px; margin-left:85px">
                                                            <asp:Button ID="btn_fuerzaventa_guardar" runat="server" Text="Registrar Contacto" 
                                                                CssClass="buttonNewPlan" Width="164px" Height="26" onclick="btn_fuerzaventa_guardar_Click1"/>
                                                        </div>
                                                    
                                                </div>
                                            </div></td>
                                            </tr>
                                            <tr>
                                                <td><div class="centrar">
                                                <div class="tabla centrar">
                                                <asp:GridView ID="gv_gestion_fuerzaventa" runat="server" Width="100%" 
                                                EnableModelValidation="True" AutoGenerateColumns="false">
                                                <Columns>
                                                <asp:BoundField DataField="id_PointOfSale_Contact" HeaderText="N° Reg." />
                                                <asp:BoundField DataField="pdv_contact_name" HeaderText="Nombre Contacto" />
                                                <asp:BoundField DataField="pdv_contact_type_description" HeaderText="Tipo de Contacto" />
                                                </Columns>
                                                </asp:GridView>
                                                </div>
                                            </div></td>
                                            </tr>
                                            </tbody>
                                            </table>
                                            
                                            
                                </div>
                                
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MP_P_FuerzaVenta" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="P_FuerzaVenta" TargetControlID="btn_fuerzaventa">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btn_fuerzaventa" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <!--Panel de Productos Anclas -->
                    <asp:UpdatePanel ID="UP_Producto_Ancla" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="ConsultaGVPancla" runat="server" Style="display: none"  >  
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

                                            <asp:Panel ID="Panel1" runat="server" Style="vertical-align: middle;">
                                                    <div>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" AlternateText="Cerrar Ventana"
                                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="BtnCOlv_Click" />
                                                    </div>


                                                                   
                            <table id="ProdAnclaCrear" align="center" runat="server">
                                    <tr>
                                        <td>
                                            <br />
                                            <br />
                                            <fieldset id="Fieldset16" runat="server">
                                                <legend style="">Producto Ancla</legend>
                                                <table align="center">
                                                 <tr>
                                                        <td>
                                                             <asp:Label ID="Label9" runat="server" CssClass="labelsN" Text="Reportes"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlReporteProdAncla" runat="server" >
                                                                    </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                 <tr>
                                                        <td colspan="2" >
                                                                                       Año: <asp:DropDownList ID="ddlAñoProdAncla" runat="server">
                                                    <asp:ListItem>2011</asp:ListItem>
                                                    <asp:ListItem>2010</asp:ListItem>
                                                    </asp:DropDownList>
                                                                           Mes: <asp:DropDownList ID="ddlmesProdAncla" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="ddlmesProdAncla_SelectedIndexChanged"> 
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
                                                    Perido: <asp:DropDownList ID="ddlPeriodoProdAncla" runat="server" 
                                                             Width="100px">
                                                         </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblCodigoAncla" runat="server"  Text="Cliente*"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbClienteAncla" runat="server" Enabled="False" Width="255px"
                                                                AutoPostBack="True" OnSelectedIndexChanged="cmbClienteAncla_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblOficina" runat="server"  Text="Oficina*"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbOficinaPancla" runat="server" Enabled="False" Width="255px"
                                                                AutoPostBack="True" OnSelectedIndexChanged="cmbOficinaPancla_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblCategoriaancla" runat="server"  Text="Categoria *"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbCategoryAncla" runat="server" Width="255px" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="cmbCategoryAncla_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblSubcatAncla" runat="server"  Text="SubCategoria* "></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbSubcateAncla" runat="server" Width="255px" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="cmbSubcateAncla_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblMarcaAncla" runat="server"  Text="Marca:*"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbMarcaAncla" runat="server" Width="255px" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="cmbMarcaAncla_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblProducAncla" runat="server"  Text="Producto Ancla *"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbproductAncla" runat="server" Width="255px" Enabled="False"
                                                                AutoPostBack="True" OnSelectedIndexChanged="cmbproductAncla_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server"  Text="Precio*"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtPrecioprodAncla" runat="server" Width="100px" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblPesoPancla" runat="server"  Text="Peso*"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtPesoPancla" runat="server" Width="100px" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <br />
                                            </fieldset>
                                        &nbsp;
                                        </td>
                                    </tr>
                                </table>


                                <table id="ProdAnclaConsultar" align="center" runat="server" visible="false">
                                    <tr>
                                        <td>
                                            <div class="p" style="width:780px; height: 330px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
    	                                        font-family : arial, Helvetica, sans-serif;">
                                                <asp:GridView ID="GVConsultaPancla" runat="server" AutoGenerateColumns="False" 
                                                    Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                                    Width="100%" onrowediting="GVConsultaPancla_RowEditing" 
                                                    onrowcancelingedit="GVConsultaPancla_RowCancelingEdit" onrowupdating="GVConsultaPancla_RowUpdating" 
                                                        >                                               
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Cliente">
                                                            <EditItemTemplate>
                                                                    <asp:DropDownList ID="cmbcliepancla" runat="server" Width="130px"  Enabled="false"
                                                                        onselectedindexchanged="cmbcliepancla_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                    <asp:DropDownList ID="cmbcliepancla" runat="server" Width="130px" Enabled="false" ></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="70px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Oficina">
                                                            <EditItemTemplate>
                                                                    <asp:DropDownList ID="cmboficipancla" runat="server" Width="130px"   Enabled="false"
                                                                    AutoPostBack="True" 
                                                                        onselectedindexchanged="cmboficipancla_SelectedIndexChanged" ></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                    <asp:DropDownList ID="cmboficipancla" runat="server" Width="130px" Enabled="false" ></asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Categoria">
                                                        <EditItemTemplate>
                                                                <asp:DropDownList ID="cmbcatepancla" runat="server" Width="130px" Enabled="false"
                                                                    AutoPostBack="True" 
                                                                    onselectedindexchanged="cmbcatepancla_SelectedIndexChanged"  ></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                            <asp:DropDownList ID="cmbcatepancla" runat="server" Width="130px" Enabled="false" ></asp:DropDownList>                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>   
                                                            <asp:TemplateField HeaderText="SubCategoria">
                                                        <EditItemTemplate>
                                                                <asp:DropDownList ID="cmbsubcatepancla" runat="server" Width="130px" 
                                                                    Enabled="true" onselectedindexchanged="cmbsubcatepancla_SelectedIndexChanged" AutoPostBack="True"  ></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                            <asp:DropDownList ID="cmbsubcatepancla" runat="server" Width="130px" Enabled="false" ></asp:DropDownList>                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Marca">
                                                        <EditItemTemplate>
                                                                <asp:DropDownList ID="cmbmarcapancla" runat="server" Width="130px" 
                                                                    Enabled="true" onselectedindexchanged="cmbmarcapancla_SelectedIndexChanged" AutoPostBack="True" ></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                            <asp:DropDownList ID="cmbmarcapancla" runat="server" Width="130px" Enabled="false" ></asp:DropDownList>                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                            <asp:TemplateField HeaderText="Producto">
                                                        <EditItemTemplate>
                                                                <asp:DropDownList ID="cmbprodupancla" runat="server" Width="220px" 
                                                                    Enabled="true" onselectedindexchanged="cmbprodupancla_SelectedIndexChanged"  AutoPostBack="True"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                            <asp:DropDownList ID="cmbprodupancla" runat="server" Width="220px" Enabled="false" ></asp:DropDownList>                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>     
                                                            <asp:TemplateField HeaderText="Precio">
                                                        <EditItemTemplate>
                                                                <asp:TextBox ID="cmbsubpreciopancla" runat="server" Width="130px" Enabled="true" ></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                            <asp:TextBox ID="cmbsubpreciopancla" runat="server" Width="130px" Enabled="false" ></asp:TextBox>                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>    
                                                        <asp:TemplateField HeaderText="Peso">
                                                        <EditItemTemplate>
                                                                <asp:TextBox ID="cmbsubpesopancla" runat="server" Width="130px" Enabled="false" ></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                            <asp:TextBox ID="cmbsubpesopancla" runat="server" Width="130px" Enabled="false" ></asp:TextBox>                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                                                     
                                                        <asp:TemplateField HeaderText="Estado" >
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="Checkpancla" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Checkpancla" runat="server"  Enabled="false" Checked ='<%# Bind("pancla_Status") %>' ></asp:CheckBox> 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Periodo">
                                                            <EditItemTemplate>
                                                                    <asp:Label ID="lblPeriodo" runat="server" Width="130px" Text='<%# Bind("Periodo") %>'  Enabled="false"
                                                                       ></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                    <asp:Label ID="lblPeriodo" runat="server" Width="130px" Text='<%# Bind("Periodo") %>' Enabled="false" ></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="70px" />
                                                        </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="id_ReportsPlanning" Visible="false">
                                                            <EditItemTemplate>
                                                                    <asp:Label ID="lblid_ReportsPlanning" runat="server" Width="130px" Text='<%# Bind("id_ReportsPlanning") %>'  Enabled="false"
                                                                       ></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                    <asp:Label ID="lblid_ReportsPlanning" runat="server" Width="130px" Text='<%# Bind("id_ReportsPlanning") %>' Enabled="false" ></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="70px" />
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowEditButton="True" />
                                                    </Columns>
                                                </asp:GridView>
                                                <br />                                                 
                                                <br />                                                 
                                                <div align="center">
                                                <div>
                                                <asp:Label ID="Lbleepancla" runat="server" CssClass="labels" Text="Exportar a Excel "></asp:Label>
                                                </div>
                                                  
                                                    <div align="center">                                       
                                                    <asp:Button ID="Button2" runat="server" CssClass="buttonPlan"
                                                    Text="Cancelar" Width="80px" onclick="btnCancelar_Click" />
                                                    </div> 
                                                    </div>  
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                              <asp:Panel ID="BuscarProductAncla" runat="server" CssClass="busqueda" DefaultButton="btnBProductAncla"
                                    Style="display: none;" Height="202px" Width="343px">
                                    <br />
                                    <br />
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl" runat="server" CssClass="labelsTit2" Text="Buscar Producto Ancla" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCliente" runat="server" CssClass="labels" Text="Cliente:" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="CmbBClientePAncla" runat="server" Width="215px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="CmbBClientePAncla_SelectedIndexChanged" Enabled="false" >
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblBCategoriapancla" runat="server" CssClass="labels" Text="Categoria:" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="CmbBCategoriaPAncla" runat="server" Width="215px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="CmbBCategoriaPAncla_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblOficinaBPancla" runat="server" CssClass="labels" Text="Oficina:" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbOficinaBPancla" runat="server" Width="215px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <div align="center">
                                        <asp:Button ID="btnBProductAncla" runat="server" CssClass="buttonPlan" OnClick="btnBProductAncla_Click"
                                            Text="Buscar" Width="80px" /><asp:Button ID="BtnCancelarPAncla" runat="server" CssClass="buttonPlan"
                                                Text="Cancelar" Width="80px" /></div>
                                </asp:Panel>
                                <cc1:ModalPopupExtender ID="ModalPopup_BPAncla" runat="server" BackgroundCssClass="modalBackground"
                                    OkControlID="BtnCancelarPAncla" PopupControlID="BuscarProductAncla" DropShadow="True"
                                    TargetControlID="BtnConsultarAncla" DynamicServicePath="" Enabled="True">
                                </cc1:ModalPopupExtender>
                                   <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnCrearAncla" runat="server" CssClass="buttonPlan" Text="Crear"
                                                Width="95px" OnClick="BtnCrearAncla_Click" /><asp:Button ID="BtnGuardarAncla" runat="server"
                                                    CssClass="buttonPlan" Text="Guardar" Visible="False" Width="95px" OnClick="BtnGuardarAncla_Click"  /><asp:Button
                                                        ID="BtnConsultarAncla" runat="server" CssClass="buttonPlan" Text="Consultar"
                                                        Width="95px" /><asp:Button ID="BtnCancelarAncla" OnClick="BtnCancelarAncla_Click"
                                                                    runat="server" CssClass="buttonPlan" Text="Cancelar" Width="95px"
                                                                    />
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
                                     <cc1:ModalPopupExtender ID="ModalPopancla" runat="server" BackgroundCssClass="modalBackground"
                                    DropShadow="True" Enabled="True"  PopupControlID="ConsultaGVPancla"
                                    TargetControlID="btnPopupGVCpancla" DynamicServicePath="">
                                </cc1:ModalPopupExtender>
                                    <asp:Button ID="btnPopupGVCpancla" runat="server" CssClass="alertas"
                                    Width="0px" />  

                        <asp:Panel ID="PMensajeProdAncla" runat="server" Height="169px" Style="display: none;"
                                Width="332px">
                                <table align="center">
                                    <tr>
                                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                                            <br />
                                        </td>
                                        <td style="width: 220px; height: 119px;" valign="top">
                                            <br />
                                            <asp:Label ID="lblEncabezadoProdAncla" runat="server" CssClass="labelsTit"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lblMensajeProdAncla" runat="server" CssClass="labels"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnaceptarProductoAncla" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                                Text="Aceptar" OnClick="btnaceptarProductoAncla_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MensajemodalPProductoAncla" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PMensajeProdAncla" TargetControlID="btnmodalPProductoAncla">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btnmodalPProductoAncla" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <!--Panel de Objetivos SOD MAY  -->
                    <asp:UpdatePanel ID="UP_Objetivos_SOD_MAY" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pObjetivosSODMAY" runat="server" Style="display: none"  >  
                                <div>
                                <table align="center" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td><img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                                width="6"> </img></td>
                                        <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                            <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1"></img></td>
                                        <td><img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                                width="6"> </img></td>
                                    </tr>
                                    <tr>
                                        <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                            <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                            </img>
                                        </td>
                                        <td bgcolor="White">
                                            <asp:Panel ID="Panel3" runat="server" Style="vertical-align: middle;">
                                                <div><asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Cerrar Ventana"
                                                        ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                        ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" OnClick="BtnCOlv_Click" />
                                                </div>               
                                                <table id="tblcrear" align="center" runat="server">
                                                        <tr>
                                                            <td><br /><br />
                                                                <fieldset id="Fieldset17" runat="server">
                                                                    <legend style="">Objetivos SOD MAY</legend>
                                                                    <table align="center">
                                                                        <tr>
                                                                            <td><asp:Label ID="Label10" runat="server" CssClass="labelsN" Text="Reportes"></asp:Label></td>
                                                                            <td><asp:DropDownList ID="ddlReporteObjetivoSODMAY" runat="server" ></asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2" >
                                                                                Año: 
                                                                                <asp:DropDownList ID="ddlAñoObjetivosSODMAY" runat="server">
                                                                                    <asp:ListItem>2011</asp:ListItem>
                                                                                    <asp:ListItem>2010</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                Mes: 
                                                                                <asp:DropDownList ID="ddlMesObjetivosSODMAY" runat="server" AutoPostBack="True" 
                                                                                    onselectedindexchanged="ddlMesObjetivosSODMAY_SelectedIndexChanged"> 
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
                                                                                Perido: 
                                                                                <asp:DropDownList ID="dllPeridoObjetivosSODMAY" runat="server" Width="100px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><asp:Label ID="Label11" runat="server"  Text="Cliente*"></asp:Label></td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlClienteObjetivosSODMAY" runat="server" Enabled="False" Width="255px"
                                                                                    AutoPostBack="True" OnSelectedIndexChanged="cmbClienteAncla_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><asp:Label ID="Label12" runat="server"  Text="Oficina*"></asp:Label></td>
                                                                            <td><asp:DropDownList ID="ddlMallaObjetivosSODMAY" runat="server" Enabled="False" Width="255px"></asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><asp:Label ID="Label13" runat="server"  Text="Categoria *"></asp:Label></td>
                                                                            <td><asp:DropDownList ID="ddlCategoriaObjetivosSODMAY" runat="server" Width="255px" Enabled="False"
                                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCategoriaObjetivosSODMAY_SelectedIndexChanged">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><asp:Label ID="Label15" runat="server"  Text="Marca:*"></asp:Label></td>
                                                                            <td><asp:DropDownList ID="ddlMarcaObjetivosSODMAY" runat="server" Width="255px" Enabled="False">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><asp:Label ID="Label17" runat="server"  Text="Objetivo Categoria*"></asp:Label></td>
                                                                            <td><asp:TextBox ID="txtObjetivoCategoria" runat="server" Width="100px" Enabled="False"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><asp:Label ID="Label18" runat="server"  Text="Objetivo Marca*"></asp:Label></td>
                                                                            <td><asp:TextBox ID="txtObjetivoMarca" runat="server" Width="100px" Enabled="False"></asp:TextBox></td>
                                                                        </tr>
                                                                    </table><br /><br />
                                                                </fieldset>
                                                            &nbsp;
                                                            </td>
                                                        </tr>
                                                </table>


                                                <table id="tblConsultar" align="center" runat="server" visible="false">
                                                        <tr>
                                                            <td>
                                                                <div class="p" style="width:780px; height: 330px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
                        	                                        font-family : arial, Helvetica, sans-serif;">
                                                                    <asp:GridView ID="gvObjetivosSODMAY" runat="server" AutoGenerateColumns="False" 
                                                                        Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                                                        Width="100%" onrowediting="gvObjetivosSODMAY_RowEditing" 
                                                                        onrowcancelingedit="gvObjetivosSODMAY_RowCancelingEdit" onrowupdating="gvObjetivosSODMAY_RowUpdating">                                               
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Cliente">
                                                                                <EditItemTemplate>
                                                                                        <asp:Label ID="lblclieObjetivosSODMAY" runat="server" Width="130px"  Enabled="false"  Text='<%# Bind("Company_Name") %>'  ></asp:Label>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                        <asp:Label ID="lblclieObjetivosSODMAY" runat="server" Width="130px" Enabled="false" Text='<%# Bind("Company_Name") %>'  ></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="70px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Canal">
                                                                                <EditItemTemplate>
                                                                                        <asp:Label ID="lblCanalObjetivosSODMAY" runat="server" Width="130px"   Enabled="false" Text='<%# Bind("Channel_Name") %>' ></asp:Label>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                        <asp:Label ID="lblCanalObjetivosSODMAY" runat="server" Width="130px" Enabled="false" Text='<%# Bind("Channel_Name") %>' ></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Malla">
                                                                                <EditItemTemplate>
                                                                                        <asp:Label ID="lblMallaObjetivosSODMAY" runat="server" Width="130px"   Enabled="false" Text='<%# Bind("malla") %>'></asp:Label>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                        <asp:Label ID="lblMallaObjetivosSODMAY" runat="server" Width="130px" Enabled="false"  Text='<%# Bind("malla") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Categoria">
                                                                            <EditItemTemplate>
                                                                                    <asp:Label ID="lblcateObjetivosSODMAY" runat="server" Width="130px" Enabled="false"  Text='<%# Bind("Product_Category") %>'  ></asp:Label>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                <asp:Label ID="lblcateObjetivosSODMAY" runat="server" Width="130px" Enabled="false"  Text='<%# Bind("Product_Category") %>' ></asp:Label>                                                     
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>   
                                                                            <asp:TemplateField HeaderText="Marca">
                                                                            <EditItemTemplate>
                                                                                    <asp:Label ID="lblMarcaObjetivosSODMAY" runat="server" Width="130px"  Enabled="false" Text='<%# Bind("Name_Brand") %>'></asp:Label>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                <asp:Label ID="lblMarcaObjetivosSODMAY" runat="server" Width="130px" Enabled="false" Text='<%# Bind("Name_Brand") %>' ></asp:Label>                                                     
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>  
                                                                            <asp:TemplateField HeaderText="Obj. Categoria">
                                                                            <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtObjCategoria" runat="server" Width="130px" Enabled="true" Text='<%# Bind("Value_Categoria") %>' ></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                <asp:TextBox ID="txtObjCategoria" runat="server" Width="130px" Enabled="false" Text='<%# Bind("Value_Categoria") %>' ></asp:TextBox>                                                     
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>    
                                                                            <asp:TemplateField HeaderText="Obj. Marca">
                                                                            <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtObjMarca" runat="server" Width="130px" Enabled="true" Text='<%# Bind("Value_Marca") %>' ></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                <asp:TextBox ID="txtObjMarca" runat="server" Width="130px" Enabled="false" Text='<%# Bind("Value_Marca") %>' ></asp:TextBox>                                                     
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>                                                                                     
                                                                            <asp:TemplateField HeaderText="Periodo">
                                                                                <EditItemTemplate>
                                                                                        <asp:Label ID="lblPeriodo" runat="server" Width="130px" Text='<%# Bind("Periodo") %>'  Enabled="false"
                                                                                           ></asp:Label>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                        <asp:Label ID="lblPeriodo" runat="server" Width="130px" Text='<%# Bind("Periodo") %>' Enabled="false" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="70px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Estado" >
                                                                                <EditItemTemplate>
                                                                                    <asp:CheckBox ID="CheckObjetivosSODMAY" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="CheckObjetivosSODMAY" runat="server"  Enabled="false" Checked ='<%# Bind("Status") %>' ></asp:CheckBox> 
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="id_ClienteObjetivosSODMAY" Visible="false">
                                                                                <EditItemTemplate>
                                                                                        <asp:Label ID="lblid_ClienteObjetivosSODMAY" runat="server" Width="130px" Text='<%# Bind("company_id") %>'  Enabled="false"
                                                                                           ></asp:Label>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                        <asp:Label ID="lblid_ClienteObjetivosSODMAY" runat="server" Width="130px" Text='<%# Bind("company_id") %>' Enabled="false" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="70px" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="cod_ChannelObjetivosSODMAY" Visible="false">
                                                                                <EditItemTemplate>
                                                                                        <asp:Label ID="lblcod_ChannelObjetivosSODMAY" runat="server" Width="130px" Text='<%# Bind("cod_channel") %>'  Enabled="false"
                                                                                           ></asp:Label>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                        <asp:Label ID="lblcod_ChannelObjetivosSODMAY" runat="server" Width="130px" Text='<%# Bind("cod_channel") %>' Enabled="false" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="70px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="id_mallaObjetivosSODMAY" Visible="false">
                                                                                <EditItemTemplate>
                                                                                        <asp:Label ID="lblid_mallaObjetivosSODMAYY" runat="server" Width="130px" Text='<%# Bind("id_malla") %>'  Enabled="false"
                                                                                           ></asp:Label>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                        <asp:Label ID="lblid_mallaObjetivosSODMAYY" runat="server" Width="130px" Text='<%# Bind("id_malla") %>' Enabled="false" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="70px" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="id_ProductCategoryObjetivosSODMAY" Visible="false">
                                                                                <EditItemTemplate>
                                                                                        <asp:Label ID="lblid_ProductCategoryObjetivosSODMAY" runat="server" Width="130px" Text='<%# Bind("id_ProductCategory") %>'  Enabled="false"
                                                                                           ></asp:Label>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                        <asp:Label ID="lblid_ProductCategoryObjetivosSODMAY" runat="server" Width="130px" Text='<%# Bind("id_ProductCategory") %>' Enabled="false" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="70px" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="id_BrandObjetivosSODMAY" Visible="false">
                                                                                <EditItemTemplate>
                                                                                        <asp:Label ID="lblid_BrandObjetivosSODMAY" runat="server" Width="130px" Text='<%# Bind("id_Brand") %>'  Enabled="false"
                                                                                           ></asp:Label>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                        <asp:Label ID="lblid_BrandObjetivosSODMAY" runat="server" Width="130px" Text='<%# Bind("id_Brand") %>' Enabled="false" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="70px" />
                                                                            </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="id_ReportsPlanning" Visible="false">
                                                                                <EditItemTemplate>
                                                                                        <asp:Label ID="lblid_ReportsPlanning" runat="server" Width="130px" Text='<%# Bind("id_ReportsPlanning") %>'  Enabled="false"
                                                                                           ></asp:Label>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                        <asp:Label ID="lblid_ReportsPlanning" runat="server" Width="130px" Text='<%# Bind("id_ReportsPlanning") %>' Enabled="false" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="70px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="id_objMay" Visible="false">
                                                                                <EditItemTemplate>
                                                                                        <asp:Label ID="lblid_objMay" runat="server" Width="130px" Text='<%# Bind("id_iobjsodmay") %>'  Enabled="false"
                                                                                           ></asp:Label>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                        <asp:Label ID="lblid_objMay" runat="server" Width="130px" Text='<%# Bind("id_iobjsodmay") %>' Enabled="false" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="70px" />
                                                                            </asp:TemplateField>
                                                                            <asp:CommandField ShowEditButton="True" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <br /><br />                                                 
                                                                    <div align="center">
                                                                        <div><asp:Label ID="Label19" runat="server" CssClass="labels" Text="Exportar a Excel "></asp:Label></div>
                                                                        <div align="center">
                                                                            <asp:Button ID="btnCancelarObjetivosSODMAYgrilla" runat="server" CssClass="buttonPlan"
                                                                            Text="Cancelar" Width="80px" onclick="btnCancelarObjetivosSODMAYgrilla_Click" />
                                                                        </div> 
                                                                    </div>  
                                                                </div>
                                                            </td>
                                                        </tr>
                                                </table>

                                                <asp:Panel ID="pBuscarObjetivosSODMAY" runat="server" CssClass="busqueda" Style="display: none;" Height="202px" Width="343px">
                                                    <br /><br />
                                                    <table align="center">
                                                        <tr><td><asp:Label ID="Label20" runat="server" CssClass="labelsTit2" Text="Buscar Objetivo May" /></td></tr>
                                                    </table><br /><br />
                                                    <table align="center">
                                                        <tr>
                                                            <td><asp:Label ID="Label21" runat="server" CssClass="labels" Text="Cliente:" /></td>
                                                            <td><asp:DropDownList ID="ddlClienteObjetivosSODMAYbuscar" runat="server" Width="215px" Enabled="false" ></asp:DropDownList></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label22" runat="server" CssClass="labels" Text="Malla:" /></td>
                                                            <td><asp:DropDownList ID="ddlMallaObjetivosSODMAYbuscar" runat="server" Width="215px" ></asp:DropDownList></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label16" runat="server" CssClass="labels" Text="Categoria:" /></td>
                                                            <td><asp:DropDownList ID="ddlCategoriaObjetivosSODMAYbuscar" runat="server" Width="215px"  AutoPostBack="true"
                                                                     OnSelectedIndexChanged="ddlCategoriaObjetivosSODMAYbuscar_SelectedIndexChanged">
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label26" runat="server" CssClass="labels" Text="Marca:" /></td>
                                                            <td><asp:DropDownList ID="ddlMarcaObjetivosSODMAYbuscar" runat="server" Width="215px" ></asp:DropDownList></td>
                                                        </tr>
                                                    </table><br /><br />
                                                    <div align="center">
                                                        <asp:Button ID="btnBuscarObjetivosSODMAYbuscar" runat="server" CssClass="buttonPlan" 
                                                            Text="Buscar" Width="80px" onclick="btnBuscarObjetivosSODMAYbuscar_Click" />
                                                        <asp:Button ID="BtnCancelarObjetivosSODMAYbuscar" runat="server" CssClass="buttonPlan"
                                                                Text="Cancelar" Width="80px" />
                                                    </div>
                                                </asp:Panel>

                                                <cc1:ModalPopupExtender ID="modalPObjetivoSODMAYbuscar" runat="server" BackgroundCssClass="modalBackground"
                                                    OkControlID="BtnCancelarObjetivosSODMAYbuscar" PopupControlID="pBuscarObjetivosSODMAY" DropShadow="True"
                                                    TargetControlID="btnConsultarObjetivosSODMAY" DynamicServicePath="" Enabled="True">
                                                </cc1:ModalPopupExtender>

                                                <table align="center">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnCrearObjetivosSODMAY" runat="server" CssClass="buttonPlan" Text="Crear"
                                                                Width="95px" OnClick="btnCrearObjetivosSODMAY_Click" />
                                                            <asp:Button ID="btnGuardarObjetivosSODMAY" runat="server"
                                                                CssClass="buttonPlan" Text="Guardar" Visible="False" Width="95px" OnClick="btnGuardarObjetivosSODMAY_Click"  /><asp:Button
                                                                ID="btnConsultarObjetivosSODMAY" runat="server" CssClass="buttonPlan" Text="Consultar"
                                                                Width="95px" />
                                                            <asp:Button ID="btnCancelarObjetivosSODMAY" OnClick="btnCancelarObjetivosSODMAY_Click"
                                                                runat="server" CssClass="buttonPlan" Text="Cancelar" Width="95px"/>
                                                            <asp:Button ID="btnCargaMasisvaObjetivosSODMAY" runat="server" CssClass="buttonPlan" Text="Carga Masiva"
                                                                Width="95px" OnClick="btnCargaMasisvaObjetivosSODMAY_Click" />
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
                            <cc1:ModalPopupExtender ID="modalPObjetivoSODMAY" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True"  PopupControlID="pObjetivosSODMAY"
                                TargetControlID="btnpObjetivosSODMAY" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btnpObjetivosSODMAY" runat="server" CssClass="alertas"
                            Width="0px" />  
                            <asp:Panel ID="PMensajeObjetivosSODMAY" runat="server" Height="200px" Style="display: none;"
                                Width="332px">
                                <table align="center">
                                    <tr>
                                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                                            <br />
                                        </td>
                                        <td style="width: 220px; height: 119px;" valign="top">
                                            <br />
                                            <asp:Label ID="lbltituloObjetivosSODMAY" runat="server" CssClass="labelsTit"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lblMensajeObjetivosSODMAY" runat="server" CssClass="labels"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnaceptarObjetivosSODMAY" runat="server" BorderStyle="Solid" CssClass="buttonPlan"
                                                Text="Aceptar" OnClick="btnaceptarObjetivosSODMAY_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MensajemodalObjetivosSODMAY" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="PMensajeObjetivosSODMAY" TargetControlID="btnmodalMensajeObjetivosSODMAY">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btnmodalMensajeObjetivosSODMAY" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                            <div id="divCargaMasivaObjetivosSODMAY" runat="server" style="display:none">
                                <asp:Panel ID="pCargamasivaObjetivosSODMAY" runat="server" style="display:none">
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
                                                            <asp:ImageButton ID="ImageButton3" runat="server" AlternateText="Cerrar Ventana"
                                                                ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                                ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" onclick="BtnCerrarCargaMasiva_Click" 
                                                                 />
                                                        </div>
                                                        <div align="center"> 
                                                            <iframe id="IframeCargaMasivaObjetivosSODMAY" runat="server" height="305px" src="" width="1000px">
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
                            </div>
                            <cc1:ModalPopupExtender ID="modalpCargamasiva" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="pCargamasivaObjetivosSODMAY" TargetControlID="btnpCarga">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btnpCarga" runat="server" CssClass="alertas" Enabled="False"
                                Height="0px" Text="" Width="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>
                <div class="footer" style="width: 100%; left: 0px;"><br /><br /><br />
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Pages/ImgBooom/logo_lucky.png"
                        ImageAlign="Right"></asp:Image>
                </div>
                <div class="Regresa" align="left" id="botonregresar" runat="server"><br /><br /><br /><br />
                    <asp:Button ID="BtnRegresar" runat="server" CssClass="Regresar" OnClick="BtnRegresar_Click" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btn_img_exporttoexcel" />
            </Triggers>
        </asp:UpdatePanel>
        <div>
            <asp:GridView ID="gv_stockToExcel" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False">
                <RowStyle ForeColor="#000066" />
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:GridView ID="gv_stockToExcel2" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False">
                <RowStyle ForeColor="#000066" />
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:GridView ID="gv_stockToExcel3" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False">
                <RowStyle ForeColor="#000066" />
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:GridView ID="gv_stockToExcel4" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False">
                <RowStyle ForeColor="#000066" />
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:GridView ID="gv_stockToExcel5" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False">
                <RowStyle ForeColor="#000066" />
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:GridView ID="gv_stockToExcel6" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False">
                <RowStyle ForeColor="#000066" />
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <cc2:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
            AssociatedUpdatePanelID="UpPlanning" BackgroundCssClass="modalProgressGreyBackground">
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
    </form>
</body>
</html>