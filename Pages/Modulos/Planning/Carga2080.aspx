<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carga2080.aspx.cs" Inherits="SIGE.Pages.Modulos.Planning.Carga2080" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
</head>
<body> 
    <form id="form1" runat="server" enctype="multipart/form-data">
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
                            <asp:Label ID="LblTitCargarArchivo" runat="server" CssClass="labels" Text="Carga de Archivos de Interface 20 y 80 "></asp:Label>
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
                    </table>
                </div>
                <br />
                <table align="center">
                    <tr>
                        <td>                            
                        <asp:Label ID="Lbl" runat="server" Text="Seleccione el periodo al cual desea que pertenezca el archivo que va a cargar"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="p" style="width: 560px; height: 135px;">
                                <asp:GridView ID="GvRep" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    EnableModelValidation="True" Font-Names="Verdana" Font-Size="9pt" ForeColor="#333333"
                                    GridLines="None" OnSelectedIndexChanged="GvRep_SelectedIndexChanged">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                    Text="Usar.."></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Año" HeaderText="Año" />
                                        <asp:BoundField DataField="Mes_" HeaderText="Mes_" />
                                        <asp:BoundField DataField="Asignado" HeaderText="Asignado" />
                                        <asp:BoundField DataField="Periodos" HeaderText="Periodos" />
                                        <asp:BoundField DataField="Fecha_inicio" HeaderText="Fecha_inicio" />
                                        <asp:BoundField DataField="Fecha_fin" HeaderText="Fecha_fin" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                            </div>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblAño" runat="server" Text="Año"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtAño" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblMes" runat="server" Text="Mes"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtMes" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblPer" runat="server" Text="Periodo"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPeriodo" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblFecha_ini" runat="server" Text="Fecha inicial"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtFechaini" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblFecha_fin" runat="server" Text="Fecha Final"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtFechaFin" runat="server" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>                   
                </table>
                <div align="center">
                    <asp:GridView ID="GvlogArchivo2080" runat="server" Visible="false">
                    </asp:GridView>
                </div>
                <table frame="box" align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblCargarArchivo" Visible="false" runat="server" Text="Nombre de Archivo:" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="Black" CssClass="labelsN"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="FileUp2080" Visible="false" runat="server" Width="337px" />
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
            <cc2:ModalPopupExtender ID="ModalConfirmacion" runat="server" Enabled="True" TargetControlID="BtnConfirmacion"
                PopupControlID="PanelConfirmacion" BackgroundCssClass="modalBackground">
            </cc2:ModalPopupExtender>
            <asp:Button ID="BtnConfirmacion" runat="server" Text="" Height="0px" CssClass="alertas"
                Width="0" Enabled="false" />
               
 <table align="center">
                    <tr>
                        <td align="center">
                            <asp:Button ID="BtnCargaArchivo" Visible="false" runat="server" CssClass="buttonNewPlan"
                                Text="Cargar Archivo" Height="25px" Width="164px" OnClick="BtnCargaArchivo_Click" />

                                                                          
                        </td>
                    </tr>
                </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnCargaArchivo" />

        </Triggers>
    </asp:UpdatePanel>


            <cc1:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3" AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
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
