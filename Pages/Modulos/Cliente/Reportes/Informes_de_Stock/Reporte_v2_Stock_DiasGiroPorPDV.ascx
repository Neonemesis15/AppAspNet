<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_Stock_DiasGiroPorPDV.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_Stock.Reporte_v2_Stock_DiasGiroPorPDV" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="ReportstockdiagiroPDV" runat="server" Font-Names="Verdana"
                         Font-Size="8pt" ProcessingMode="Remote"
        ShowParameterPrompts="False" ToolTip="Dias Giro  por Oficina" style="width:auto;"
        SizeToReportContent="True" 
        ZoomMode="Percent" ShowZoomControl="true" Visible="False">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</p>
<%--
            Font-Size="8pt" ProcessingMode="Remote" Height="450px" Width="100%" ShowParameterPrompts="False"
            ToolTip="Dias Giro  por Oficina" ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="false"
            ZoomMode="Percent" Visible="False">--%>