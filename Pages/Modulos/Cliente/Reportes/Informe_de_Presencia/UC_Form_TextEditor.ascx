<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_Form_TextEditor.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informe_de_Presencia.UC_Form_TextEditor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div align="left">
    <br />
    <table style="text-align: left;">
        <tr>
            <td>
                Año
            </td>
            <td>
                <telerik:RadComboBox ID="cmb_año" runat="server">
                </telerik:RadComboBox>
            </td>
            <td>
                Mes
            </td>
            <td>
                <telerik:RadComboBox ID="cmb_mes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>
            <td>
                Periodo
            </td>
            <td>
                <telerik:RadComboBox ID="cmb_periodo" runat="server">
                </telerik:RadComboBox>
            </td>
            <td>
                <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="buttonSearch"
                    Height="25px" Width="164px" OnClick="btn_buscar_Click" />
            </td>
        </tr>
    </table>
</div>
<div align="left">
    <br />
    <asp:Button ID="btn_save" runat="server" Text="Guardar" Height="25px" Width="164px"
        OnClick="btn_save_Click" CssClass="buttonGuardar" />
</div>
<div align="center">
    <telerik:RadEditor ID="RadEditor_ResumenEjecutivo" runat="server" ToolsFile="~/Pages/Modulos/Cliente/Reportes/Informe_de_Presencia/BasicTools.xml"
        Language="es-PE" AllowScripts="True" Width="100%">
    </telerik:RadEditor>
</div>
