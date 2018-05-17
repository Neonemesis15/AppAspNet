<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Form_Stock_RangoDiasGiro.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_Stock.Form_Stock_RangoDiasGiro" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<code>
    <link href="../../../../css/Cliente_V2.css" rel="stylesheet" type="text/css" />
</code>--%>
<div align="center">
    <div align="left">
        Año:<telerik:RadComboBox ID="cmb_año" runat="server">
        </telerik:RadComboBox>
        Mes:<telerik:RadComboBox ID="cmb_mes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged">
        </telerik:RadComboBox>
        Periodo:<telerik:RadComboBox ID="cmb_periodo" runat="server" Enabled="false">
        </telerik:RadComboBox>
        <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="buttonSearch"
            Height="25px" Width="164px" OnClick="btn_buscar_Click" />
    </div>
    <br />
    <div id="div_gv_RangoDg">
        <div align="left">
            Agregar
            <asp:ImageButton ID="btnImg_add" runat="server" ImageUrl="~/Pages/images/add.png" />
        </div>
        <telerik:RadGrid ID="gv_RangoDG" runat="server" AutoGenerateColumns="False" GridLines="None"
            Skin="Outlook" onitemcommand="gv_RangoDG_ItemCommand" >
            <MasterTableView NoMasterRecordsText="Sin resultados.">
                <RowIndicatorColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>
                <ExpandCollapseColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="Categoria" HeaderText="Categoria" UniqueName="Categoria">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="Rango de Días Giro Xplora" UniqueName="TemplateColumn1">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txt_min_dg_xplora" runat="server" Culture="es-PE"
                                DbValue='<%# Eval("DiasGiro_Min_xplora") %>' Width="120px" MinValue="0" InvalidStyleDuration="1000"
                                NumberFormat-DecimalDigits="0">
                            </telerik:RadNumericTextBox>
                            a
                            <telerik:RadNumericTextBox ID="txt_max_dg_xplora" runat="server" Culture="es-PE"
                                DbValue='<%# Eval("DiasGiro_Max_xplora") %>' Width="120px" MinValue="0" InvalidStyleDuration="1000"
                                NumberFormat-DecimalDigits="0">
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Rango de Días Giro Cliente" UniqueName="TemplateColumn2">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txt_min_dg_cliente" runat="server" Culture="es-PE"
                                DbValue='<%# Eval("DiasGiro_Min_company") %>' Width="120px" MinValue="0" InvalidStyleDuration="1000"
                                NumberFormat-DecimalDigits="0">
                            </telerik:RadNumericTextBox>
                            a
                            <telerik:RadNumericTextBox ID="txt_max_dg_cliente" runat="server" Culture="es-PE"
                                DbValue='<%# Eval("DiasGiro_Max_company") %>' Width="120px" MinValue="0" InvalidStyleDuration="1000"
                                NumberFormat-DecimalDigits="0">
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                        <ItemTemplate>
                            <asp:Label ID="lbl_id_ReportsPlanning" runat="server" Visible="false" Text='<%# Bind("id_ReportsPlanning") %>'></asp:Label>
                            <asp:Label ID="lbl_id_ProductCategory" runat="server" Visible="false" Text='<%# Eval("id_ProductCategory") %>'></asp:Label>
                            <asp:ImageButton ID="btnImg_delete" runat="server" 
                                ImageUrl="~/Pages/images/delete.png" CommandName="ELIMINAR" OnClientClick="return confirm('¿Confirma que desea eliminar el registro indicado?');"  />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="True" />
                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div>
        <asp:Button ID="btn_guardar" runat="server" Text="Guardar" CssClass="buttonGuardar"
            Height="25px" Width="164px" OnClick="btn_guardar_Click" OnClientClick="return confirm('¿Esta seguro de guardar los cambios?');" />
        <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="buttonCancelar"
            Height="25px" Width="164px" OnClick="btn_cancelar_Click" />
    </div>
    <div>
        <asp:Label ID="lbl_msj" runat="server"></asp:Label>
    </div>
    <div id="div_addRangDG">
        <cc1:ModalPopupExtender ID="ModalPopupExtender_addRangDG" runat="server" BackgroundCssClass="modalBackground"
            DropShadow="True" Enabled="True" PopupControlID="Panel_addRangDG" TargetControlID="btnImg_add"
            CancelControlID="btn_closeRangDG" DynamicServicePath="">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel_addRangDG" runat="server" BackColor="White" BorderColor="#0099CB"
            BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt" Height="260px"
            Width="460px" Style="display: none;">
            <div>
                <asp:ImageButton ID="btn_closeRangDG" runat="server" BackColor="Transparent" Height="22px"
                    ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" /></div>
            <div align="center">
                <div style="font-family: verdana; font-size: medium; color: #D01887;">
                    Agregar el rango de Días Giro a un periodo</div>
                <br />
                <table >
                    <tr>
                        <td  align="right">
                            Categoria:
                        </td>
                        <td align="left">
                            <telerik:RadComboBox ID="cmb_categoria" runat="server" Width="238px" Skin="Vista">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td  align="right">
                            Año:
                        </td>
                        <td align="left">
                            <telerik:RadComboBox ID="cmb_año2" runat="server">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td  align="right">
                            Mes:
                        </td>
                        <td align="left">
                            <telerik:RadComboBox ID="cmb_mes2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmb_mes2_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td  align="right">
                            Periodo:
                        </td>
                        <td align="left">
                            <telerik:RadComboBox ID="cmb_periodo2" runat="server" Enabled="false">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td  align="right">
                            Rango DG Xplora:
                        </td>
                        <td align="left">
                            <telerik:RadNumericTextBox ID="txt_min2_dg_xplora" runat="server" Culture="es-PE"
                                Width="120px" MinValue="0" InvalidStyleDuration="1000" NumberFormat-DecimalDigits="0" EmptyMessage="Min">
                            </telerik:RadNumericTextBox>
                            a
                            <telerik:RadNumericTextBox ID="txt_max2_dg_xplora" runat="server" Culture="es-PE"
                                Width="120px" MinValue="0" InvalidStyleDuration="1000" NumberFormat-DecimalDigits="0" EmptyMessage="Max">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  align="right">
                            Rango DG
                            <asp:Label ID="lbl_cliente" runat="server"></asp:Label>:
                        </td>
                        <td align="left">
                            <telerik:RadNumericTextBox ID="txt_min2_dg_cliente" runat="server" Culture="es-PE"
                                Width="120px" MinValue="0" InvalidStyleDuration="1000" NumberFormat-DecimalDigits="0" EmptyMessage="Min">
                            </telerik:RadNumericTextBox>
                            a
                            <telerik:RadNumericTextBox ID="txt_max2_dg_cliente" runat="server" Culture="es-PE"
                                Width="120px" MinValue="0" InvalidStyleDuration="1000" NumberFormat-DecimalDigits="0" EmptyMessage="Max">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <asp:Button ID="btn_nuevo" runat="server" Text="Guardar" OnClick="btn_nuevo_Click"
                    CssClass="buttonGuardar" Height="25px" Width="164px"/>
                <br />
                <asp:Label ID="lbl_msjpopup" runat="server" ></asp:Label>
            </div>
        </asp:Panel>
    </div>
</div>
