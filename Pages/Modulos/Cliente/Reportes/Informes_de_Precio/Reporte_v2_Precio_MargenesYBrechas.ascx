﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_Precio_MargenesYBrechas.ascx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_Precio.Reporte_v2_Precio_MargenesYBrechas" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
  
    <link href="../../../../css/Cliente_V2.css" rel="stylesheet" type="text/css" />

<p>
   <%--<div   align="center" style="background-color:Blue;">
         <asp:Button ID="btn_consultar0" runat="server" CssClass="buttonEditPlan" 
                                Height="24px" Text="Consultar" Width="160px" 
             onclick="btn_consultar0_Click" />
    
    
    
    </div>--%>

</p>
<asp:UpdatePanel ID="upbrecha" runat="server" UpdateMode="Always">
<ContentTemplate>
<rsweb:ReportViewer ID="ReportBrechas" runat="server"  Font-Names="Verdana" 
            Font-Size="8pt"  ProcessingMode="Remote" 
            ShowParameterPrompts="False" ToolTip="Margenes" style="width:auto;"
            ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="True" 
            ZoomMode="FullPage" Visible="False">
</rsweb:ReportViewer>

</ContentTemplate>
</asp:UpdatePanel>

