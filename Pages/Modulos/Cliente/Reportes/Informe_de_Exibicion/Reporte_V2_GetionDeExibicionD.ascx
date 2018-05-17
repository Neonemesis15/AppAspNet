<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_V2_GetionDeExibicionD.ascx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informe_de_Exibicion.Reporte_V2_GetionDeExibicionD" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <link href="../../../../css/Cliente_V2.css" rel="stylesheet" type="text/css" />
     
 <%--<asp:UpdatePanel ID="upgestion" runat="server">
       <ContentTemplate>--%>
       

       
 <rsweb:ReportViewer ID="ReporGT" runat="server" Font-Names="Verdana" Font-Size="8pt"
                ProcessingMode="Remote" Height="1800px" Width="1450px" ShowParameterPrompts="False"
                ToolTip="Comparativo" ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="True"
                ZoomMode="PageWidth" Visible="False">
  
</rsweb:ReportViewer>
    
    
    <%--</ContentTemplate>
    
    
    
  </asp:UpdatePanel>--%>
