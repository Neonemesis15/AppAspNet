<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="carga_PDV.aspx.cs" Inherits="SIGE.Pages.Modulos.Planning.carga_PDV" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
    <link href="../../css/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">    
    </script>
    <style type="text/css">
        .style2
        {
            height: 25px;
        }
        .style3
        {
            height: 38px;
        }
    </style>
</head>
<body class="CargaArchivos">
    <form id="form1" runat="server" enctype="multipart/form-data">
    <cc2:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc2:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpCargaPuntosDeVenta" runat="server">
        <ContentTemplate>
            <table align="center" style="display: none;">
                <tr>
                    <td>
                        <asp:Label ID="LblCanal" runat="server" Text=""></asp:Label>
                        <asp:Label ID="LblCountry" runat="server" Text=""></asp:Label>
                        <asp:DropDownList ID="cmbSelDpto" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="cmbSelProvince" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="cmbSelDistrict" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="cmbSelComunity" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="CmbTipMerc" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="cmbNodoCom" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="CmbSelSegPDV" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="cmbTipDocCli" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="CmbMalla" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="CmbSector" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="CmbOficina" runat="server">
                        </asp:DropDownList>
                         <asp:DropDownList ID="cmbDexPDV" runat="server">
                        </asp:DropDownList>
                        <asp:Label ID="LblCiudad" runat="server" Text="------"></asp:Label>
                        <asp:Label ID="LblTipoAgrupacion" runat="server" Text="------"></asp:Label>
                        <asp:Label ID="LblAgrupacion" runat="server" Text="------"></asp:Label>
                        <asp:Label ID="LblOficina" runat="server" Text="------"></asp:Label>
                        <asp:Label ID="LblMalla" runat="server" Text="------"></asp:Label>
                        <asp:Label ID="LblSector" runat="server" Text="------"></asp:Label>


                    </td>
                </tr>
            </table>

            <asp:Button ID="btndipararalerta" runat="server" Text="" Height="0px" CssClass="alertas"
                Width="0" Enabled="False" />
            <table bgcolor="#7F99CC" style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="LblTitCargarArchivo" runat="server" CssClass="labels" Text="Cargar nuevo archivo"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <table align="center">
                <tr>
                    <td>
                        <asp:Label ID="LblPlannigPDV" runat="server" CssClass="labels" Text="Planning No"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtCodPlanningPDV" runat="server" BackColor="#CCCCCC" Enabled="False"
                            ForeColor="White"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="LblSelPresupuestoPDV" runat="server" CssClass="labels" Text="Presupuesto"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="CmbSelPresupuestoPDV" runat="server" Width="500px" AutoPostBack="True"
                            OnSelectedIndexChanged="CmbSelPresupuestoPDV_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
           
            <table align="center" class="altoverow">
                <tr>
                    <td valign="middle">
                        <asp:Label   ID="LblTipoCarga" runat="server" Text="Tipo de Carga que va a realizar"></asp:Label>
                    </td>
                    <td style="padding-left: 30px">
                        <asp:RadioButtonList ID="RbtnSelTipoCarga" runat="server" 
                            RepeatDirection="Vertical"  Width="200px" 
                            onselectedindexchanged="RbtnSelTipoCarga_SelectedIndexChanged" 
                            AutoPostBack="True" Enabled="False">
                            <asp:ListItem Value="Uno a Uno"></asp:ListItem>
                            <asp:ListItem Value="Masiva ">Masiva</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td id="td_download" valign="middle" style="display:none" runat="server">
                    <table>
                    <tr>
                    <td ><a class="button" href="../../formatos/FORMATO_CARGA_PTOVENTA_PLANNING.xls"><span>Descargar Formato</span></a></td>
                    </tr>
                    <tr>
                    <td ><a class="button" href="PDV_Planning/DATOS_CARGA_PTOVENTA.xls"><span>Descargar Datos</span></a></td>
                    </tr>
                    </table>
                    
                    </td>
                </tr>
            </table>
                      
            <table frame="box" align="center" style="display:Block;" id="OpcMasiva" runat="server" >
                <tr>
                    <td>
                        <asp:Label ID="LblCargarArchivo" runat="server" Text="Nombre de Archivo:" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpPDV" runat="server" Width="337px" />
                    </td>
                </tr>
                <tr>
                    
                 
                    <td align="center" colspan="2" >
                        <asp:Button ID="BtnCargaArchivo" runat="server" CssClass="buttonNewPlan" Text="Cargar Archivo"
                            OnClick="BtnCargaArchivo_Click"  Height="25px" Width="164px" />
                    </td>
                  
               
                </tr>
            </table>
             <table frame="box" align="center" style="display:Block;" id="OpcUnoAUno" runat="server" >
                <tr>
                    <td valign="top">
                        <asp:Label ID="Label1" runat="server" Text="Digite código de Punto de Venta:" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                    </td>
                    <td>
                       <asp:TextBox ID="TxtCodigoPDV" runat="server" Enabled="false"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" runat="server" 
                            ImageUrl="~/Pages/images/last.png" onclick="ImageButton1_Click" />
                    </td>
                   
                </tr>
                 <tr>
                     <td valign="top">
                         <asp:Label ID="LblPDVName0" runat="server" Text="Punto de Venta" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                     </td>
                     <td>
                         <asp:Label ID="LblNamePDV" runat="server" Width="500px" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Font-Bold="True">------------------------------------------------------------------------------------------------</asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td valign="top">
                         <asp:Label ID="LblPDVDir" runat="server" Text="Dirección" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                     </td>
                     <td>
                         <asp:Label ID="LblDirPDV" runat="server" Width="500px" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Font-Bold="True">------------------------------------------------------------------------------------------------</asp:Label>
                     </td>
                 </tr>
                <tr>
                 <td align="center" colspan="2">
                        <asp:Button ID="BtnCargaUnoaUno" runat="server" CssClass="buttonNewPlan" 
                            Height="25px" Width="164px" 
                            Text="Agregar" Visible="False" onclick="BtnCargaUnoaUno_Click"  />
                    </td>
                </tr>
            </table>
            <br />
            
          
            <asp:GridView ID="Gvlog" runat="server" Visible="False">
            </asp:GridView>
            <%--panel de mensaje de usuario   --%>
            <asp:Panel ID="Pmensaje" runat="server" Height="169px" Width="332px" Style="display: none;">
                <table align="center">
                    <tr>
                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                            <br />
                        </td>
                        <td style="width: 238px; height: 119px;" valign="top">
                            <br />
                            <asp:Label ID="lblencabezado" runat="server" CssClass="labels"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="lblmensajegeneral" runat="server" CssClass="labels"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnaceptar" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                Text="Aceptar" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc2:ModalPopupExtender ID="ModalPopupMensaje" runat="server" Enabled="True" TargetControlID="btndipararalerta"
                PopupControlID="Pmensaje" BackgroundCssClass="modalBackground">
            </cc2:ModalPopupExtender>
            <%--
            <cc1:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" 
                AssociatedUpdatePanelID="UpCargaPuntosDeVenta"  BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div>

                    <!--codigo anterior comentado -->
                            <telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />
                            <telerik:RadProgressArea Width="250" Height="10" ID="RadProgressArea1" runat="server" Culture="es-PE"  Localization-CurrentFileName="Cargando Informes"
                                DisplayCancelButton="false" ProgressIndicators="FilesCountBar,
                          FilesCountPercent" Skin="Outlook" EnableAjaxSkinRendering="False" />
                          <!--codigo anterior comentado -->

                          <telerik:RadProgressManager ID="RadProgressManager1" runat="server" ClientIDMode="AutoID"  />
                    <telerik:RadProgressArea ID="RadProgressArea1" runat="server" Culture="es-PE" 
                        Localization-ElapsedTime="Tiempo transacurrido: "
                        Skin="WebBlue" EnableAjaxSkinRendering="False" Language="es-PE" 
                             Localization-TransferSpeed="Velocidad: " 
                             Localization-CurrentFileName="Procesando Informes:"  Localization-Total="" 
                             Localization-TotalFiles="" Localization-Uploaded="" 
                             Localization-UploadedFiles="" Height="160px" 
                             ProgressIndicators="FilesCountBar, FilesCountPercent, CurrentFileName, TimeElapsed">
                       
                    </telerik:RadProgressArea>
                    </div>
                    <!--codigo anterior comentado -->
                     <div class="modalPopup">
                            Procesando, por favor espere...
                            <img alt="Procesando" src="../../../images/progress_bar.gif" style="vertical-align: middle" />
                        </div>
                        <!--codigo anterior comentado -->
                </ProgressTemplate>
            </cc1:ModalUpdateProgress>  --%>

        </ContentTemplate>
        <Triggers>
            <%-- <asp:PostBackTrigger ControlID="btnloadPdv" />--%>
            <asp:PostBackTrigger ControlID="BtnCargaArchivo" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
