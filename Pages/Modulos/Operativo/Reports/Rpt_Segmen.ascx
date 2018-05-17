<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Rpt_Segmen.ascx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Rpt_Segmen" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
   

    <asp:UpdatePanel ID="upseg" runat="server">
    <ContentTemplate>
    
    
    
          <rsweb:ReportViewer ID="Reportsegmento" runat="server" Font-Names="Verdana" 
            Font-Size="8pt"  ProcessingMode="Remote"  style="width:auto"
            ShowParameterPrompts="False" ToolTip="Comparativo"  
           SizeToReportContent="True" 
            ZoomMode="FullPage" Visible="False">
  
</rsweb:ReportViewer>
    
    
    </ContentTemplate>
    
    
    
    </asp:UpdatePanel>
