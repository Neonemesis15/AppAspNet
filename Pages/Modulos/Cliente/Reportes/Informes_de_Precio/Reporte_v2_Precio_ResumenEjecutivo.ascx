<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_Precio_ResumenEjecutivo.ascx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_Precio.Reporte_v2_Precio_ResumenEjecutivo" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
 <link href="../../../../css/Cliente_V2.css" rel="stylesheet" type="text/css" />

<p>
    
    
    
    
    </p>
    

       <asp:UpdatePanel ID="upindicce"  runat="server">
             <ContentTemplate>
             
             <rsweb:ReportViewer ID="Reportresumen" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" ProcessingMode="Remote" 
            ShowParameterPrompts="False" ToolTip="Resumen" style="width:auto;"
            ZoomPercent="100"  SizeToReportContent="True" 
            ZoomMode="FullPage" Visible="False">
  
</rsweb:ReportViewer>
             
             </ContentTemplate>
             </asp:UpdatePanel>
                              