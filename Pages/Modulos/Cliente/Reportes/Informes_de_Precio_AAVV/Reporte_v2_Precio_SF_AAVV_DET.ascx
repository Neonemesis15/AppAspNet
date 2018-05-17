<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_Precio_SF_AAVV_DET.ascx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reporte_v2_Precio_SF_AAVV_DET" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <link href="../../../../css/Cliente_V2.css" rel="stylesheet" type="text/css" />

    <asp:UpdatePanel ID="upcompa" runat="server">
    <ContentTemplate>
    
    
    
          <rsweb:ReportViewer ID="ReportPrecioDet" runat="server" Font-Names="Verdana" 
            Font-Size="8pt"  ProcessingMode="Remote"  style="width:auto"
            ShowParameterPrompts="False" ToolTip="Comparativo"  
           SizeToReportContent="True" 
            ZoomMode="FullPage" Visible="False">
  
</rsweb:ReportViewer>
    
    
    </ContentTemplate>
    
    
    
    </asp:UpdatePanel>
