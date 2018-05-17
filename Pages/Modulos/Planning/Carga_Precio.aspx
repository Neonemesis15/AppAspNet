<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carga_Precio.aspx.cs" Inherits="SIGE.Pages.Modulos.Planning.Carga_Precio" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
         <title></title>
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
    <link href="../../css/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">    
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <cc2:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc2:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <asp:Button ID="btndipararalerta" runat="server" Text="" Height="0px" CssClass="alertas"
                    Width="0" Enabled="False" />
                <table bgcolor="#7F99CC" style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="LblTitCargarArchivo" runat="server" CssClass="labels" Text="Cargar Masiva"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <div align="center">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="LblSelPresupuestoPDV" runat="server" CssClass="labelsN" Text="Presupuesto : "></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LblPlanning" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="LblPresupuestoPDV" runat="server" CssClass="labelsN"></asp:Label>
                            </td>
                        </tr>
                            <tr>
                            <td colspan="2">
                              
                              <asp:DropDownList ID="ddlPeriodo" runat="server" Width="400px" AutoPostBack="True">
                        </asp:DropDownList>
                              
                            </td>
                        </tr>
                    </table>
                </div>
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
                            AutoPostBack="True" Enabled="true">
                            <asp:ListItem Value="Uno a Uno"></asp:ListItem>
                            <asp:ListItem Value="Masiva ">Masiva</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td id="td_download" valign="middle" style="display:none" runat="server">
                    <a class="button" href="PDV_Planning/FORMATO_PLANNIG_CARGAMASIVA_PRECIO.xls"><span>Descargar Formato</span></a>
                    </td>
                </tr>
            </table>
            <br />
            <table frame="box" align="center" style="display:Block;" id="OpcMasiva" runat="server" >
                <tr>
                    <td>
                        <asp:Label ID="LblCargarArchivo" runat="server" Text="Nombre de Archivo:" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpPrecio" runat="server" Width="337px" />
                    </td>
                </tr>
                <tr>
                    
                 
                    <td align="center" colspan="2" >
                        <asp:Button ID="BtnCargaArchivo" runat="server" CssClass="buttonNewPlan" Text="Cargar Archivo"
                            OnClick="BtnCargaArchivo_Click"  Height="25px" Width="164px" />
                    </td>
                  
               
                </tr>
            </table>
                <br />
                <table frame="box" align="center" style="display:none;" id="OpcUnoAUno" runat="server" >
                <tr>
                    <td valign="top">
                        <asp:Label ID="Label1" runat="server" Text="Producto:" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProducto" runat="server" Width="250px" >
                        </asp:DropDownList>
                    </td>
                   
                </tr>
                 <tr>
                     <td valign="top">
                         <asp:Label ID="LblPDVName0" runat="server" Text="Punto de Venta" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                     </td>
                     <td>
                         <asp:DropDownList ID="ddlPtoVenta" runat="server" Width="250px" >
                        </asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <td valign="top">
                         <asp:Label ID="LblPDVDir" runat="server" Text="Precio" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                     </td>
                     <td>
                         <telerik:RadNumericTextBox runat="server" ID="txtPrecio" >
                             <NumberFormat DecimalDigits="2" />
                         </telerik:RadNumericTextBox>
                     </td>
                 </tr>
                <tr>
                 <td align="center" colspan="2">
                        <asp:Button ID="BtnCargaUnoaUno" runat="server" CssClass="buttonNewPlan" 
                            Height="25px" Width="164px" 
                            Text="Guardar" onclick="BtnCargaUnoaUno_Click"  />
                    </td>
                </tr>
            </table>
                <br />
                <br />
            </div>
            <%--panel de mensaje de usuario   --%>
            <asp:Panel ID="Pmensaje" runat="server" Height="169px"  Width="332px" >
            <div runat="server" id="divMensaje"   >
                <table align="center" width="332px" >
                    <tr >
                        <td align="center"  valign="top">
                            <br />
                           <asp:Image runat="server" ID="ImgMensaje" />
                        </td>
                        <td style="width: 238px; height: 119px;" valign="top">
                            <br />
                            <asp:Label ID="lblencabezado" runat="server" CssClass="labels"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="lblmensajegeneral" runat="server" CssClass="labels"></asp:Label>
                            <br />
                            <br />
                            <div align="center">
                            <asp:Button  runat="server" ID="btnMensaje" Text="Aceptar" CssClass="buttonNewPlan" />
                            </div>
                        </td>

                    </tr>
                </table>
                </div>
            </asp:Panel>
            <cc2:ModalPopupExtender ID="ModalPopupMensaje" runat="server" Enabled="True" TargetControlID="btndipararalerta"
                PopupControlID="Pmensaje" OkControlID="btnMensaje"   BackgroundCssClass="modalBackground">
            </cc2:ModalPopupExtender>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnCargaArchivo" />
        </Triggers>
    </asp:UpdatePanel>
    <cc1:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server"   
       DisplayAfter="3"
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
    </cc1:ModalUpdateProgress>
    </form>
</body>
</html>