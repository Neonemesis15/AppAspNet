<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_Precio_InformePrecio_3M.ascx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_Precio.Reporte_v2_Precio_InformePrecio_3M" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<link href="../../../../css/Cliente_V2.css" rel="stylesheet" type="text/css" />
 <asp:UpdatePanel ID= "reporprecio" runat="server">
 <ContentTemplate>
 
 <rsweb:ReportViewer ID="reportePrecios" runat="server"
Font-Size="8pt" ProcessingMode="Remote"
            ShowParameterPrompts="False" ToolTip="Panel de Precios" style="width:auto;"
         SizeToReportContent="True" 
            ZoomMode="Percent" ShowZoomControl="true" Visible="False">
</rsweb:ReportViewer>
 
 </ContentTemplate>
 
 </asp:UpdatePanel>
