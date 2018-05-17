<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_Precio_ComparativoPrecioEnCiudades.ascx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Precio_ComparativoPrecioEnCiudades" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <link href="../../../../css/Cliente_V2.css" rel="stylesheet" type="text/css" />


    
    <asp:UpdatePanel ID="upcompaciuda" runat="server">
    <ContentTemplate>
    <div>
    
    
    
    </div>
    
     <rsweb:ReportViewer ID="ReportCompaCi" runat="server" Font-Names="Verdana" 
            Font-Size="8pt"  ProcessingMode="Remote"   style="width:auto;"
            ShowParameterPrompts="False" ToolTip="Comparativo"  
            ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="True" 
            ZoomMode="PageWidth" Visible="False">
  
</rsweb:ReportViewer>
    
    </ContentTemplate>
    
    
    </asp:UpdatePanel>
    
    
  
         
