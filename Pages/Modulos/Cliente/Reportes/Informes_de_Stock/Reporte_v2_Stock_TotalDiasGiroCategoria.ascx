<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_Stock_TotalDiasGiroCategoria.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_Stock.Reporte_v2_Stock_TotalDiasGiroCategoria" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="ReportstockdiagiroCatego" runat="server" Font-Names="Verdana" Font-Size="8pt"
                ProcessingMode="Remote" Height="1800px" Width="1450px" ShowParameterPrompts="False"
                ToolTip="Comparativo" ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="True"
                ZoomMode="PageWidth" Visible="False">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</p>
