﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_Stock_RptPdvRelevados.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_Stock.Reporte_v2_Stock_RptPdvRelevados" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:UpdatePanel ID="upcompa" runat="server">
    <ContentTemplate>
        <rsweb:ReportViewer ID="RReleva" runat="server" Font-Names="Verdana"
           Font-Size="8pt" ProcessingMode="Remote"
        ShowParameterPrompts="False" ToolTip="PDV Relevados" style="width:auto;"
        SizeToReportContent="True" 
        ZoomMode="Percent" ShowZoomControl="true" Visible="False">
        </rsweb:ReportViewer>
    </ContentTemplate>
</asp:UpdatePanel>
<%-- Font-Size="8pt" ProcessingMode="Remote" Height="450px" Width="100%" ShowParameterPrompts="False"
            ToolTip="PDV Relevados" ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="false"
            ZoomMode="Percent" Visible="False">--%>