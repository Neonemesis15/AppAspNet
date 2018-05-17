<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ope_Digitacion.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Operativo.Ope_Digitacion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Digitación Operativa</title>
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
</head> 
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    <div align="right">
        <asp:Label ID="lblUsuario" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="#114092"></asp:Label>
        <asp:Label ID="usersession" runat="server" Visible="False" Font-Names="Verdana" Font-Size="9pt"
            ForeColor="#114092"></asp:Label>
        <br />
        <asp:ImageButton ID="ImgCloseSession" runat="server" Height="16px" ImageUrl="~/Pages/images/SesionClose.png"
            Width="84px" OnClick="ImgCloseSession_Click" />
    </div>
    <div class="Header" align="center">
        <asp:Image ID="ImgLogoLucky" runat="server" ImageUrl="~/Pages/ImgBooom/logotipo.png">
        </asp:Image>
    </div>
    <asp:UpdatePanel ID="UPDigitacion" runat="server">
        <ContentTemplate>
            <div>
                <div align="center" style="height: 30px;">
                    <asp:ImageButton ID="ImgNewDigitacion" runat="server" ImageUrl="~/Pages/images/BtnAgregar.png"
                        BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="2px" OnClick="ImgNewDigitacion_Click" />
                    <asp:ImageButton ID="ImgSearchDigitacion" runat="server" ImageUrl="~/Pages/images/BtnSearchReg.png"
                        BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="2px" OnClick="ImgSearchDigitacion_Click" />
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Pages/images/BtnSaveReg.png"
                        BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="2px" OnClick="ImageButton1_Click"
                        Visible="False" />
                    <asp:ImageButton ID="ImgCancelDigitacion" runat="server" ImageUrl="~/Pages/images/BtnCancelReg.png"
                        BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="2px" Visible="False" OnClick="ImgCancelDigitacion_Click" />
                </div>
                <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="Lbl_Campaña" runat="server" Text="Campaña" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="CmbCampaña" runat="server" AutoCompleteMode="Suggest" Font-Names="verdana"
                                Visible="false" Font-Size="9pt" AutoPostBack="True" OnSelectedIndexChanged="CmbCampaña_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="TxtCodPlanning" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Lbl_Formato" runat="server" Text="Formato" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="CmbFormato" runat="server" Font-Names="verdana" Font-Size="9pt"
                                Visible="false" AutoPostBack="True" OnSelectedIndexChanged="CmbFormato_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td>
                            <fieldset id="Filtros" runat="server" align="center">
                                <legend>Filtros</legend>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblFecha" runat="server" Text="Fecha"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFecha" runat="server"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="TxtFecha_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFecha" UserDateFormat="DayMonthYear">
                                            </cc1:MaskedEditExtender>
                                            <cc1:CalendarExtender ID="TxtFecha_CalendarExtender" runat="server" Enabled="True"
                                                FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal" PopupPosition="TopLeft"
                                                TargetControlID="TxtFecha">
                                            </cc1:CalendarExtender>
                                            <asp:ImageButton ID="ImageButtonCal" runat="server" Height="16px" ImageUrl="~/Pages/images/calendario.JPG"
                                                Width="16px" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblFechaFin" runat="server" Text="Hasta"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFechaFin" runat="server"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="TxtFechaFin_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechaFin"
                                                UserDateFormat="DayMonthYear">
                                            </cc1:MaskedEditExtender>
                                            <cc1:CalendarExtender ID="TxtFechaFin_CalendarExtender" runat="server" Enabled="True"
                                                FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCalFin" PopupPosition="TopLeft"
                                                TargetControlID="TxtFechaFin">
                                            </cc1:CalendarExtender>
                                            <asp:ImageButton ID="ImageButtonCalFin" runat="server" Height="16px" ImageUrl="~/Pages/images/calendario.JPG"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblOperativo" runat="server" Text="Promotor / Mercaderista"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="CmbOperativo" runat="server" Font-Names="verdana" Font-Size="9pt"
                                                Width="400px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblPDV" runat="server" Text="PDV"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="TxtPDV" runat="server" Width="395px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblLugarImpulso" runat="server" Text="Lugar de Impulso / Dirección"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="TxtLugarDireccion" runat="server" Width="395px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblNombreClieMayorista" runat="server" Text="Nombre del Cliente Mayorista"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="TxtNombreClieMayorista" runat="server" Width="395px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblZonaMayor" runat="server" Text="Zona Mayorista"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="CmbZonaMayor" runat="server" Font-Names="verdana" Font-Size="9pt"
                                                Width="400px" OnSelectedIndexChanged="CmbZonaMayor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblMercado" runat="server" Text="Mercado"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="CmbMercado" runat="server" Font-Names="verdana" Font-Size="9pt"
                                                Width="400px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblCodCliente" runat="server" Text="Codigo Cliente"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="TxtCliente" runat="server" AutoPostBack="True" OnTextChanged="TxtCliente_TextChanged"></asp:TextBox>
                                            <asp:ImageButton ID="ImgSelCliente" runat="server" ImageUrl="~/Pages/images/last.png"
                                                OnClick="ImgSelCliente_Click" Visible="False" Width="16px" />
                                            <asp:Label ID="LblCliente" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblCiudad" runat="server" Text="Ciudad"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="CmbCiudad" runat="server" Font-Names="verdana" Font-Size="9pt"
                                                Width="400px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblCategoria" runat="server" Text="Categoria"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="CmbCategoria" runat="server" AutoPostBack="True" Font-Names="verdana"
                                                Font-Size="9pt" OnSelectedIndexChanged="CmbCategoria_SelectedIndexChanged" Width="400px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <asp:ImageButton ID="ImgEditDigitacion" runat="server" ImageUrl="~/Pages/images/BtnSearchReg.png"
                                    BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="2px" Visible="False" OnClick="ImgEditDigitacion_Click" />
                                <asp:ImageButton ID="ImgHabEditDigitacion" runat="server" ImageUrl="~/Pages/images/BtnEditReg.png"
                                    BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="2px" Visible="False" OnClick="ImgHabEditDigitacion_Click" />
                                <asp:ImageButton ID="ImgUpdateDigitacion" runat="server" ImageUrl="~/Pages/images/BtnUpdateReg.png"
                                    BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="2px" Visible="False" OnClick="ImgUpdateDigitacion_Click" />
                                <asp:ImageButton ID="ImgGuardarCabeceraOpeDigitacion" runat="server" ImageUrl="~/Pages/images/BtnSaveReg.png"
                                    BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="2px" Visible="False" OnClick="ImgGuardarCabeceraOpeDigitacion_Click" />
                                <asp:ImageButton ID="ImgCancelEditDigitacion" runat="server" ImageUrl="~/Pages/images/BtnCancelReg.png"
                                    BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="2px" Visible="False" OnClick="ImgCancelEditDigitacion_Click1" />
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td>
                            <fieldset id="FiltrosDetalle" runat="server" align="center">
                                <legend>Filtros Detalles</legend>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Txt_ID_HDCanjeMulticategoriaCAB" runat="server" Width="40px" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="TxtNumRegistro_DET" runat="server" Width="40px" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblCodClienteDetalle" runat="server" Text="Codigo Cliente"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtClienteDetalle" runat="server" AutoPostBack="True" OnTextChanged="TxtClienteDetalle_TextChanged"></asp:TextBox>
                                            <asp:ImageButton ID="ImgSelClienteDetalle" runat="server" ImageUrl="~/Pages/images/last.png"
                                                OnClick="ImgSelClienteDetalle_Click" Visible="False" Width="16px" />
                                            <asp:Label ID="LblClienteDetalle" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblFacturaBoleta" runat="server" Text="Factura o Boleta"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFacturaBoleta" runat="server" Width="395px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="ReqFacturaBoleta" runat="server" ControlToValidate="TxtFacturaBoleta"
                                                Display="None" ErrorMessage="La factura debe contener solo números" ValidationExpression="([0-9]{1,9})">
                                            </asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                                                TargetControlID="ReqFacturaBoleta">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblNombreClienteMinorista" runat="server" Text="Nombre del Cliente Minorista"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtNombreClienteMinorista" runat="server" Width="395px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblDNI" runat="server" Text="LE / DNI"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtDNI" runat="server" Width="395px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="ReqDNI" runat="server" ControlToValidate="TxtDNI"
                                                Display="None" ErrorMessage="El DNI o LE debe contener solo números" ValidationExpression="([0-9]{1,9})">
                                            </asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                                                TargetControlID="ReqDNI">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblRuc" runat="server" Text="RUC"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtRuc" runat="server" Width="395px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="ReqRuc" runat="server" ControlToValidate="TxtRuc"
                                                Display="None" ErrorMessage="Requiere que el RUC contenga solo  números" ValidationExpression="([0-9]{1,9})">
                                            </asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                                TargetControlID="ReqRuc">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblTelefono" runat="server" Text="Telefono"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtTelefono" runat="server" Width="395px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="ReqTelefono" runat="server" ControlToValidate="TxtTelefono"
                                                Display="None" ErrorMessage="Requiere que la cantidad contenga solo  números"
                                                ValidationExpression="([0-9]{1,9})">
                                            </asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                TargetControlID="ReqTelefono">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblMontoSoles" runat="server" Text="Monto Soles"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtMontoSoles" runat="server" Width="395px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="ReqMontoSoles" runat="server" ControlToValidate="TxtMontoSoles"
                                                Display="None" ErrorMessage="Requiere que la cantidad contenga solo  números"
                                                ValidationExpression="([0-9]{1,9})">
                                            </asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender32" runat="server" Enabled="True"
                                                TargetControlID="ReqMontoSoles">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <asp:ImageButton ID="ImgGuardarDetalle" runat="server" ImageUrl="~/Pages/images/BtnSaveReg.png"
                                    BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="2px" Visible="False" OnClick="ImgGuardarHDCCanjeDetalle_Click" />
                                <asp:ImageButton ID="ImgCancelarDetalle" runat="server" ImageUrl="~/Pages/images/BtnCancelReg.png"
                                    BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="2px" Visible="False" OnClick="ImgCancelarDetalle_Click" />
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td>
                            <asp:GridView ID="GvDigitacion" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                EnableModelValidation="True" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GvDigitacion_SelectedIndexChanged">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Categoria">
                                        <ItemTemplate>
                                            <asp:Label ID="LblCateg" runat="server" Text='<%# Bind("categoria") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblCateg" runat="server" Text='<%# Bind("categoria") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Marca">
                                        <ItemTemplate>
                                            <asp:Label ID="LblMarca" runat="server" Text='<%# Bind("marca") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblMarca" runat="server" Text='<%# Bind("marca") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EXHIBICIÓN PRIMARIA" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtExhPrim" runat="server" Text='<%# Bind("ExhPrim") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtExhPrim" runat="server" Text='<%# Bind("ExhPrim") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EXHIBICIÓN SECUNDARIA" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtExhSec" runat="server" Text='<%# Bind("ExhSec") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtExhSec" runat="server" Text='<%# Bind("ExhSec") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                        ShowSelectButton="True" HeaderText="Eliminar" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>


                            <asp:GridView ID="GVDigitacionPrecios" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                Font-Names="Verdana" Font-Size="9pt" OnSelectedIndexChanged="GVDigitacionPrecios_SelectedIndexChanged">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SubCategoria">
                                        <ItemTemplate>
                                            <asp:Label ID="LblSubCatg" runat="server" Text='<%# Bind("Subcategoria") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblSubCatg" runat="server" Text='<%# Bind("Subcategoria") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sku">
                                        <ItemTemplate>
                                            <asp:Label ID="LblSku" runat="server" Text='<%# Bind("Sku") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblSku" runat="server" Text='<%# Bind("Sku") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Producto">
                                        <ItemTemplate>
                                            <asp:Label ID="LblProd" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblProd" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PRECIO DEL FABRICANTE AL MAYORISTA POR CAJA (Costo)"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtCosto" runat="server" Width="50px" Text='<%# Bind("Costo") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtCosto" runat="server" Width="50px" Text='<%# Bind("Costo") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PRECIO DEL MAYORISTA AL MINORISTA POR CAJA (Precio de Reventa)"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtReventa" runat="server" Width="50px" Text='<%# Bind("Reventa") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtReventa" runat="server" Width="50px" Text='<%# Bind("Reventa") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PRECIO DE REVENTA POR UNIDADES" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtReventaUnid" runat="server" Text='<%# Bind("ReventaUnid") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtReventaUnid" runat="server" Text='<%# Bind("ReventaUnid") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OBSERVACIONES" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtObservaciones" runat="server" Text='<%# Bind("Observacion") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtObservaciones" runat="server" Text='<%# Bind("Observacion") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                        ShowSelectButton="True" HeaderText="Eliminar" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>


                            <asp:GridView ID="GvDigitacionDG" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                EnableModelValidation="True" ForeColor="#333333" GridLines="None" Font-Names="Verdana"
                                Font-Size="9pt" OnSelectedIndexChanged="GvDigitacionDG_SelectedIndexChanged">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sku">
                                        <ItemTemplate>
                                            <asp:Label ID="LblSku" runat="server" Text='<%# Bind("Sku") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblSku" runat="server" Text='<%# Bind("Sku") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Producto">
                                        <ItemTemplate>
                                            <asp:Label ID="LblProd" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblProd" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Local 1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtLocal1" runat="server" Text='<%# Bind("Local1") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtLocal1" runat="server" Text='<%# Bind("Local1") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Local 2" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtLocal2" runat="server" Text='<%# Bind("Local2") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtLocal2" runat="server" Text='<%# Bind("Local2") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Local 3" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtLocal3" runat="server" Text='<%# Bind("Local3") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtLocal3" runat="server" Text='<%# Bind("Local3") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Local 4" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtLocal4" runat="server" Text='<%# Bind("Local4") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtLocal4" runat="server" Text='<%# Bind("Local4") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Local 5" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtLocal5" runat="server" Text='<%# Bind("Local5") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtLocal5" runat="server" Text='<%# Bind("Local5") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtTotal" runat="server" Text='<%# Bind("Total") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtTotal" runat="server" Text='<%# Bind("Total") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                        ShowSelectButton="True" HeaderText="Eliminar" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>


                            <asp:GridView ID="GvDigitacionAGCocina" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                Font-Names="Verdana" Font-Size="9pt" OnSelectedIndexChanged="GvDigitacionAGCocina_SelectedIndexChanged">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sku">
                                        <ItemTemplate>
                                            <asp:Label ID="LblSku" runat="server" Text='<%# Bind("Sku") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblSku" runat="server" Text='<%# Bind("Sku") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Producto">
                                        <ItemTemplate>
                                            <asp:Label ID="LblProd" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblProd" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Precio" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtPrecio" runat="server" Text='<%# Bind("Precio") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtPrecio" runat="server" Text='<%# Bind("Precio") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Inicial" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtStockini" runat="server" Text='<%# Bind("Stockini") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtStockini" runat="server" Text='<%# Bind("Stockini") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ingresos" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtIngresos" runat="server" Text='<%# Bind("Ingresos") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtIngresos" runat="server" Text='<%# Bind("Ingresos") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Total" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtStockTot" runat="server" Text='<%# Bind("StockTot") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtStockTot" runat="server" Text='<%# Bind("StockTot") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Final" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtStockFin" runat="server" Text='<%# Bind("StockFin") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtStockFin" runat="server" Text='<%# Bind("StockFin") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ventas" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="Txtventas" runat="server" Text='<%# Bind("Ventas") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Txtventas" runat="server" Text='<%# Bind("Ventas") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                        ShowSelectButton="True" HeaderText="Eliminar" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>


                            <asp:GridView ID="GvDigitacionMimaskot" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                Font-Names="Verdana" Font-Size="9pt" OnSelectedIndexChanged="GvDigitacionMimaskot_SelectedIndexChanged">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sku">
                                        <ItemTemplate>
                                            <asp:Label ID="LblSku" runat="server" Text='<%# Bind("Sku") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblSku" runat="server" Text='<%# Bind("Sku") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Producto">
                                        <ItemTemplate>
                                            <asp:Label ID="LblProd" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblProd" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Inicial" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtStockini" runat="server" Width="50px" Text='<%# Bind("Stockini") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtStockini" runat="server" Width="50px" Text='<%# Bind("Stockini") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ingresos" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtIngresos" runat="server" Width="50px" Text='<%# Bind("Ingresos") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtIngresos" runat="server" Width="50px" Text='<%# Bind("Ingresos") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Total" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtStockTot" runat="server" Width="50px" Text='<%# Bind("StockTot") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtStockTot" runat="server" Width="50px" Text='<%# Bind("StockTot") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Final" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtStockFin" runat="server" Width="50px" Text='<%# Bind("StockFin") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtStockFin" runat="server" Width="50px" Text='<%# Bind("StockFin") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ventas" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="Txtventas" runat="server" Width="50px" Text='<%# Bind("Ventas") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Txtventas" runat="server" Width="50px" Text='<%# Bind("Ventas") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Precio Reventa" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtPreReventa" runat="server" Width="50px" Text='<%# Bind("PreReventa") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtPreReventa" runat="server" Width="50px" Text='<%# Bind("PreReventa") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mejor Prc Reventa" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtMejorpreRev" runat="server" Width="50px" Text='<%# Bind("MejorpreRev") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtMejorpreRev" runat="server" Width="50px" Text='<%# Bind("MejorpreRev") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Precio Costo" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtPrecioCsto" runat="server" Width="50px" Text='<%# Bind("PrecioCsto") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtPrecioCsto" runat="server" Width="50px" Text='<%# Bind("PrecioCsto") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                        ShowSelectButton="True" HeaderText="Eliminar" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                            <asp:GridView ID="GvDigitacionPromGalletas" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                Font-Names="Verdana" Font-Size="9pt" OnSelectedIndexChanged="GvDigitacionPromGalletas_SelectedIndexChanged">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sku">
                                        <ItemTemplate>
                                            <asp:Label ID="LblSku" runat="server" Text='<%# Bind("Sku") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblSku" runat="server" Text='<%# Bind("Sku") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Producto">
                                        <ItemTemplate>
                                            <asp:Label ID="LblProd" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LblProd" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Inicial" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtStockini" runat="server" Width="50px" Text='<%# Bind("Stockini") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtStockini" runat="server" Width="50px" Text='<%# Bind("Stockini") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ingresos 1 semana" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtIngresos_1sem" runat="server" Width="50px" Text='<%# Bind("Ingresos_1sem") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtIngresos_1sem" runat="server" Width="50px" Text='<%# Bind("Ingresos_1sem") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ingresos 2 semana" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtIngresos_2sem" runat="server" Width="50px" Text='<%# Bind("Ingresos_2sem") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtIngresos_2sem" runat="server" Width="50px" Text='<%# Bind("Ingresos_2sem") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Ingresos" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtTotIngresos" runat="server" Width="50px" Text='<%# Bind("Tot_Ingr") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtTotIngresos" runat="server" Width="50px" Text='<%# Bind("Tot_Ingr") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Total" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtStockTot" runat="server" Width="50px" Text='<%# Bind("StockTot") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtStockTot" runat="server" Width="50px" Text='<%# Bind("StockTot") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Final" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtStockFin" runat="server" Width="50px" Text='<%# Bind("StockFin") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtStockFin" runat="server" Width="50px" Text='<%# Bind("StockFin") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ventas" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="Txtventas" runat="server" Width="50px" Text='<%# Bind("Ventas") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Txtventas" runat="server" Width="50px" Text='<%# Bind("Ventas") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Precio" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtPrecio" runat="server" Width="50px" Text='<%# Bind("Precio") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtPrecio" runat="server" Width="50px" Text='<%# Bind("Precio") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                        ShowSelectButton="True" HeaderText="Eliminar" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                            <table align="center">
                                <tr align="center">
                                    <td>
                                        <br />
                                        <asp:GridView ID="GvDigitacionHojaControl" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                            Font-Names="Verdana" Font-Size="9pt" Caption=".">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Platos_NC">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TxtPlatos_NC" runat="server" Width="50px" Text='<%# Bind("Platos_NC") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TxtPlatos_NC" runat="server" Width="50px" Text='<%# Bind("Platos_NC") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Taza_MM">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TxtTaza_MM" runat="server" Width="50px" Text='<%# Bind("Taza_MM") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TxtTaza_MM" runat="server" Width="50px" Text='<%# Bind("Taza_MM") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Taza_NC">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TxtTaza_NC" runat="server" Width="50px" Text='<%# Bind("Taza_NC") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TxtTaza_NC" runat="server" Width="50px" Text='<%# Bind("Taza_NC") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <%--     <Columns>
                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" SelectText=""
                                                    ShowSelectButton="True" HeaderText="Eliminar" />
                                            </Columns>--%>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </td>
                                    <td>
                                        <br />
                                    </td>
                                    <td>
                                        <br />
                                    </td>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td>
                                        <asp:GridView ID="GvIntermedia" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                            Font-Size="Medium" CellPadding="4" GridLines="None" 
                                            OnSelectedIndexChanged="GvIntermedia_SelectedIndexChanged">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="N° Cab">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblNumCab" runat="server" Text='<%# Bind("[NUM_CAB]") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="LblNumCab" runat="server" Text='<%# Bind("[NUM_CAB]") %>' Width="100px"></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="N° Reg">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblNumReg" runat="server" Text='<%# Bind("NUM_REGISTRO") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="LblNumReg" runat="server" Text='<%# Bind("NUM_REGISTRO") %>' Width="100px"></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="N° Factura/Boleta">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblFacturaBoleta0" runat="server" Text='<%# Bind("[NUM_FACTURA_BOLETA]") %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="LblFacturaBoleta1" runat="server" Text='<%# Bind("[NUM_FACTURA_BOLETA]") %>'
                                                            Width="100px"></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombre Cliente Minorista">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblNomClieMino0" runat="server" Text='<%# Bind("[NOM_CLIE_MINO]") %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="LblNomClieMino1" runat="server" Text='<%# Bind("[NOM_CLIE_MINO]") %>'
                                                            Width="150px"></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DNI">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDNI0" runat="server" Text='<%# Bind("[DNI]") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="LblDNI1" runat="server" Text='<%# Bind("[DNI]") %>' Width="100px"></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Teléfono">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTelefono" runat="server" Text='<%# Bind("[TELEFONO]") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="LblTelefono" runat="server" Text='<%# Bind("[TELEFONO]") %>' Width="100px"></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PDV">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPDV" runat="server" Text='<%# Bind("[ID_CLIENTE]") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="LblPDV" runat="server" Text='<%# Bind("[ID_CLIENTE]") %>' Width="100px"></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Monto en S./">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblMontoSoles0" runat="server" Text='<%# Bind("[MONTO_SOLES]") %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="LblMontoSoles1" runat="server" Text='<%# Bind("[MONTO_SOLES]") %>'
                                                            Width="100px"></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowSelectButton="True" />
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td>
                                        <asp:GridView ID="GvDigitacionHojaControlAbarrotes" runat="server" EnableModelValidation="True"
                                            AutoGenerateColumns="False" Font-Size="Medium" 
                                            OnSelectedIndexChanged="GvDigitacionHojaControlAbarrotes_SelectedIndexChanged" 
                                            CellPadding="4" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="N° Det">
                                                    <ItemTemplate>
                                                        <asp:Label Width="50px" ID="LblNumDet" runat="server" Text='<%# Bind("[NUM_DETALLE]") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label Width="50px" ID="LblNumDet" runat="server" Text='<%# Bind("[NUM_DETALLE]") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Empaque">
                                                    <ItemTemplate>
                                                        <asp:Label Width="150px" ID="LblEmpaque" runat="server" Text='<%# Bind("EMPAQUE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label Width="150px" ID="LblEmpaque" runat="server" Text='<%# Bind("EMPAQUE") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grupo">
                                                    <ItemTemplate>
                                                        <asp:Label Width="250px" ID="LblGrupo" runat="server" Text='<%# Bind("GRUPO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label Width="250px" ID="LblGrupo" runat="server" Text='<%# Bind("GRUPO") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Presentacion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPresentacion" runat="server" Text='<%# Bind("PRESENTACION") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="LblPresentacion" runat="server" Text='<%# Bind("PRESENTACION") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Marca">
                                                    <ItemTemplate>
                                                        <asp:Label Width="120px" ID="LblMarca" runat="server" Text='<%# Bind("MARCA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label Width="120px" ID="LblMarca" runat="server" Text='<%# Bind("MARCA") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SKU">
                                                    <ItemTemplate>
                                                        <asp:Label Width="70px" ID="LblSKU" runat="server" Text='<%# Bind("SKU") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label Width="70px" ID="LblSKU" runat="server" Text='<%# Bind("SKU") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombre del Producto">
                                                    <ItemTemplate>
                                                        <asp:Label Width="450px" ID="LblNombreProducto" runat="server" Text='<%# Bind("[NOMBRE PRODUCTO]") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label Width="450px" ID="LblNombreProducto" runat="server" Text='<%# Bind("[NOMBRE PRODUCTO]") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cantidad">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="40px" ID="TxtN1" runat="server" Text='<%# Bind("[N1]") %>' Font-Size="Small"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="ReqNumProductos" runat="server" ControlToValidate="TxtN1"
                                                            Display="None" ErrorMessage="Requiere que la cantidad contenga solo números"
                                                            ValidationExpression="([0-9]{1,9})">
                                                        </asp:RegularExpressionValidator>
                                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender32" runat="server" Enabled="True"
                                                            TargetControlID="ReqNumProductos">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox Width="40px" ID="TxtN1" runat="server" Text='<%# Bind("[N1]") %>' Font-Size="Small"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowSelectButton="True" SelectText="Eliminar" />
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td>
                                        <asp:GridView ID="GvDigitacionHojaControlAbarrotes_Premios" runat="server" AutoGenerateColumns="False"
                                            EnableModelValidation="True" Font-Size="Medium" 
                                            OnSelectedIndexChanged="GvDigitacionHojaControlAbarrotes_Premios_SelectedIndexChanged" 
                                            CellPadding="4" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="N° Reg">
                                                    <ItemTemplate>
                                                        <asp:Label Width="100px" ID="Lbl_ID_HCCCABxPremio" runat="server" Text='<%# Bind("ID_HCCCABxPremio") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label Width="100px" ID="Lbl_ID_HCCCABxPremio" runat="server" Text='<%# Bind("ID_HCCCABxPremio") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="N° Id">
                                                    <ItemTemplate>
                                                        <asp:Label Width="100px" ID="Lbl_Id_Premio" runat="server" Text='<%# Bind("id_HCCanjePremios") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label Width="100px" ID="Lbl_Id_Premio" runat="server" Text='<%# Bind("id_HCCanjePremios") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Premio">
                                                    <ItemTemplate>
                                                        <asp:Label Width="150px" ID="LblPremioDescr" runat="server" Text='<%# Bind("[DESCRIPCION]") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label Width="150px" ID="LblPremioDescr" runat="server" Text='<%# Bind("[DESCRIPCION]") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cantidad">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="50px" ID="TxtCantidad_Premio" runat="server" Text='<%# Bind("[CANTIDAD]") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="ReqNumPremio" runat="server" ControlToValidate="TxtCantidad_Premio"
                                                            Display="None" ErrorMessage="Requiere que la cantidad contenga solo  números"
                                                            ValidationExpression="([0-9]{1,9})">
                                                        </asp:RegularExpressionValidator>
                                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender32" runat="server" Enabled="True"
                                                            TargetControlID="ReqNumPremio">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox Width="50px" ID="TxtCantidad_Premio" runat="server" Text='<%# Bind("[CANTIDAD]") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField SelectText="Eliminar" ShowSelectButton="True" />
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td>
                                        <asp:GridView ID="GvCompetencia" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                                            EnableModelValidation="True" Font-Size="Medium">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="N°">
                                                    <ItemTemplate>
                                                        <asp:Label Width="50px" ID="LblNumRegistro" runat="server" Text='<%# Bind("[DESCRIPCION]") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mercado">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="100px" ID="TxtCantidad_Premio" runat="server" Text='<%# Bind("[CANTIDAD]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Empresa">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="100px" ID="TxtCantidad_Premio" runat="server" Text='<%# Bind("[CANTIDAD]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Marca">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="100px" ID="TxtCantidad_Premio" runat="server" Text='<%# Bind("[CANTIDAD]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actividad">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="100px" ID="TxtCantidad_Premio" runat="server" Text='<%# Bind("[CANTIDAD]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grupo Objetivo">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="100px" ID="TxtCantidad_Premio" runat="server" Text='<%# Bind("[CANTIDAD]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inicio Promocion">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="100px" ID="TxtCantidad_Premio" runat="server" Text='<%# Bind("[CANTIDAD]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Termino Promocion">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="100px" ID="TxtCantidad_Premio" runat="server" Text='<%# Bind("[CANTIDAD]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cantidad de Personal">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="100px" ID="TxtCantidad_Premio" runat="server" Text='<%# Bind("[CANTIDAD]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mecanica">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="100px" ID="TxtCantidad_Premio" runat="server" Text='<%# Bind("[CANTIDAD]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Material de Apoyo">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="100px" ID="TxtCantidad_Premio" runat="server" Text='<%# Bind("[CANTIDAD]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fotos">
                                                    <ItemTemplate>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Impacto en Ventas/Comentarios/Observaciones">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="180px" ID="TxtCantidad_Premio" runat="server" 
                                                            Text='<%# Bind("[CANTIDAD]") %>' Height="35px" TextMode="MultiLine"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                          
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <%--panel de mensaje de usuario   --%>
            <asp:Panel ID="Panel_Mensaje" runat="server" Height="169px" Style="display: none;"
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
                Enabled="True" PopupControlID="Panel_Mensaje" TargetControlID="btndipararalerta">
            </cc1:ModalPopupExtender>
            <asp:Button ID="btndipararalerta" runat="server" CssClass="alertas" Enabled="False"
                Height="0px" Text="" Width="0" />
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
            <%-- nuevo popup 07/07/2011 Por Pablo Salas. PopPup para Consultar en HDCCanjeMulticategoria--%>
            <asp:Button ID="BtnConfirmacion2" runat="server" Text="" Height="0px" CssClass="alertas"
                Width="0" Enabled="false" />
            <cc1:ModalPopupExtender ID="ModalConfirmacion2" runat="server" Enabled="True" TargetControlID="BtnConfirmacion2"
                PopupControlID="PanelConfirmacion2" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelConfirmacion2" runat="server" Width="332px" CssClass="altoverow"
                Style="display: none;">
                <table align="center" style="width: 95%;">
                    <tr>
                        <td align="center" valign="top">
                            <br />
                            <asp:Label ID="LblSrUsuarioConsulta" runat="server" Text="Sr. Usuario"></asp:Label>
                            <br />
                            <asp:Label ID="LblMensajeConfirmaConsulta" runat="server"></asp:Label>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="BtnSiConfirmaConsulta" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                            Text="SI" OnClick="BtnSiConfirmaConsulta_Click" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="BtnNoConfirmaConsulta" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                            Text="NO" OnClick="BtnNoConfirmaConsulta_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%-- fin de poppup 07/07/2011 --%>
            <%-- nuevo popup 08/07/2011 Por Pablo Salas. PopPup3 para Consultar en HDCCanjeMulticategoria--%>
            <asp:Button ID="BtnConfirmacion3" runat="server" Text="" Height="0px" CssClass="alertas"
                Width="0" Enabled="false" />
            <cc1:ModalPopupExtender ID="ModalPopupConfirmacion3" runat="server" Enabled="True"
                TargetControlID="BtnConfirmacion3" PopupControlID="PanelConfirmacion3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelConfirmacion3" runat="server" Width="332px" CssClass="altoverow"
                Style="display: none;">
                <table align="center" style="width: 95%;">
                    <tr>
                        <td align="center" valign="top">
                            <br />
                            <asp:Label ID="LblSrUsuarioConfirmacion3" runat="server" Text="Sr. Usuario"></asp:Label>
                            <br />
                            <asp:Label ID="LblMensajeConfirmaConfirmacion3" runat="server"></asp:Label>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="BtnSiConfirmaConfirmacion3" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                            Text="SI" OnClick="BtnSiConfirmaConfirmacion3_Click" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="BtnNoConfirmaConfirmacion3" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                            Text="NO" OnClick="BtnNoConfirmaConfirmacion3_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%-- fin de poppup 07/07/2011 --%>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
