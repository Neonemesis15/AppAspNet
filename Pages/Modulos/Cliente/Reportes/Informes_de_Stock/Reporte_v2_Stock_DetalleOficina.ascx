<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_Stock_DetalleOficina.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_Stock.Reporte_v2_Stock_DetalleOficina" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:UpdatePanel ID="upcompa" runat="server">
    <ContentTemplate>
        <rsweb:ReportViewer ID="ReportstockDetalleOfic" runat="server" Font-Names="Verdana"
            Font-Size="8pt" ProcessingMode="Remote" Height="1800px" Width="1450px" ShowParameterPrompts="False"
            ToolTip="Dias Giro" ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="True"
            ZoomMode="PageWidth" Visible="False">
        </rsweb:ReportViewer>
    </ContentTemplate>
</asp:UpdatePanel>
