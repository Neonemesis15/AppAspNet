<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_Stock_TotalDiasGiroOficina.ascx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_Stock.Reporte_v2_Stock_TotalDiasGiroOficina" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <link href="../../../../css/Cliente_V2.css" rel="stylesheet" type="text/css" />

    <asp:UpdatePanel ID="upcompa" runat="server">
    <ContentTemplate>    
        <rsweb:ReportViewer ID="ReportstockdiagiroOfic" runat="server" Font-Names="Verdana"
        Font-Size="8pt" ProcessingMode="Remote"
        ShowParameterPrompts="False" ToolTip="Dias Giro" style="width:auto;"
        SizeToReportContent="True" 
        ZoomMode="Percent" ShowZoomControl="true" Visible="False">
        </rsweb:ReportViewer>
    </ContentTemplate>
    </asp:UpdatePanel>
    
<%--            Font-Size="8pt" ProcessingMode="Remote" Height="450px" Width="100%" ShowParameterPrompts="False"
            ToolTip="Dias Giro" ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="false"
            ZoomMode="Percent" Visible="False">--%>