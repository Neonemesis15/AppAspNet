<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Revporte_v2_CumplimientoLayout.ascx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informe_de_Exibicion.Revporte_v2_CumplimientoLayout" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<rsweb:ReportViewer ID="ReportCumplimientoLayout" runat="server" Font-Names="Verdana" 
            Font-Size="8pt"  ProcessingMode="Remote"  style="width:auto"
            ShowParameterPrompts="False" ToolTip="Cumplimiento"  
           SizeToReportContent="True" 
            ZoomMode="FullPage" Visible="False">
  
</rsweb:ReportViewer>