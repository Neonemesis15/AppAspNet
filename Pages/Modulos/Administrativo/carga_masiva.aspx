<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="carga_masiva.aspx.cs" Inherits="SIGE.Pages.Modulos.Administrativo.carga_masiva" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
      <link href="../../css/layout.css" rel="stylesheet" type="text/css" />

</head>
<body class="CargaArchivos">
    <form id="form1" runat="server" enctype="multipart/form-data"> 
    <cc2:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc2:ToolkitScriptManager>   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <div>    
      <asp:Button ID="btndipararalerta" runat="server" Text="" Height="0px" CssClass="alertas"
                    Width="0" Enabled="False" />
        <table align="center" bgcolor="#7F99CC" style="width: 420px">
            <tr>
                <td align="left">
                    &nbsp;&nbsp;
                    <asp:Label ID="LblTitCargarArchivo" runat="server" CssClass="labels"  
                    Text="Cargar nuevo archivo" ></asp:Label>
                </td>            
            </tr>        
        </table>
        
        <br />
        
                <table align="center" id="tblProductos" runat="server" visible="false" >
                    <tr>
                        <td>
                            <asp:Label ID="LblCargarArchivo" runat="server" 
                            Text="Nombre de Archivo:" Font-Names="Arial" Font-Size="8pt" 
                            ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpProducto" runat="server" Width="237px" />
                        </td>   
                        <td style="width:"auto">
                         <a class="button" href="../../formatos/Formato_Carga_Rutas.xls"><span>Descargar Formato</span></a>
                        </td>                 
                    </tr>
                
                </table>                
               
                <table align="center" id="tblEmpaques" runat="server" visible="false" >
                    <tr>
                        <td>
                            <asp:Label ID="LblCargarArchivo0" runat="server" Font-Names="Arial" 
                                Font-Size="8pt" ForeColor="Black" Text="Nombre de Archivo:"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpProducto0" runat="server" Width="239px" />
                        </td>
                         <td style="width:"auto">
                         <a   class="button" href="../../formatos/Formato_Carga_Rutas.xls"><span>Descargar Formato</span></a>
                        </td> 
                    </tr>
                </table>
             <table align="center" id="tblPtoVenta" runat="server" visible="false" >
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" 
                                Font-Size="8pt" ForeColor="Black" Text="Nombre de Archivo:"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpPtoVenta" runat="server" Width="239px" />
                        </td>
                         <td style="width:"auto">
                         <a class="button" href="../../formatos/Formato_Pto_Venta.xls"><span>Descargar Formato</span></a>
                          <a class="button" href="Archivos/DATOS_CARGA_PTOVENTA.xls"><span>Descargar Datos</span></a>
                        </td> 
                    </tr>
                </table>
                 <table align="center" id="tblPtoVentaCliente" runat="server" visible="false" >
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" 
                                Font-Size="8pt" ForeColor="Black" Text="Nombre de Archivo:"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpPtoVentaCliente" runat="server" Width="239px" />
                        </td>
                         <td style="width:"auto">
                         <a class="button" href="../../formatos/Formato_Pto_Venta_Cliente.xls"><span>Descargar Formato</span></a>
                          <a class="button" href="Archivos/DATOS_CARGA_PTOVENTA_CLIENTE.xls"><span>Descargar Datos</span></a>
                        </td> 
                    </tr>
                </table>
                <table align="center" style="width: 348px">
                    <tr>
                        <td align="center">
                            <asp:Label ID="LblVacio" runat="server" Font-Bold="True" Font-Names="Arial" 
                            Font-Size="7.5pt"></asp:Label>
                        </td>
                    </tr>
                    <tr >
                        <td align="center">
                            <asp:Button ID="btnloadproduct" runat="server" onclick="btnloadproduct_Click" 
                            Text="Cargar Productos" Height="22px" Width="117px" Visible="false" />
                                
                                <cc2:ConfirmButtonExtender ID="btnloadproduct_ConfirmButtonExtender" 
                                    runat="server"  ConfirmText="Realmente desea Cargar los Datos" 
                                    Enabled="True" TargetControlID="btnloadproduct" ConfirmOnFormSubmit="True">
                                </cc2:ConfirmButtonExtender>
                                <asp:Button ID="btnloadproduct0" runat="server" Height="22px" 
                                onclick="btnloadproduct0_Click" Text="Cargar Empaquetamientos" 
                                Width="165px" Visible="false" />
                                 <cc2:ConfirmButtonExtender ID="ConfirmButtonExtender1" 
                                    runat="server"  ConfirmText="Realmente desea Cargar los Datos" 
                                    Enabled="True" TargetControlID="btnloadproduct0" ConfirmOnFormSubmit="True">
                                </cc2:ConfirmButtonExtender>

                                  <asp:Button ID="btnloadptoventa" runat="server" Height="22px" 
                                onclick="btnloadptoventa_Click" Text="Cargar PDV" 
                                Width="165px" Visible="false" />
                                <asp:Button ID="btnloadptoventaCliente" runat="server" Height="22px" 
                                onclick="btnloadptoventaCliente_Click" Text="Cargar" 
                                Width="165px" Visible="false" />
                        </td>
                    </tr>
                </table>
                  </div>
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
             
                
                
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnloadproduct" />
                <asp:PostBackTrigger ControlID="btnloadproduct0" />
                <asp:PostBackTrigger  ControlID="btnloadptoventa" />
                <asp:PostBackTrigger  ControlID="btnloadptoventaCliente" />
                
            </Triggers>
   </asp:UpdatePanel>
    </form>
</body>
</html>
