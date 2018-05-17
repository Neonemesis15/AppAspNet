﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_SOD_SODporPDVMayorista.ascx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_SOD.Reporte_v2_SOD_SODporPDVMayorista" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <link href="../../../../css/Cliente_V2.css" rel="stylesheet" type="text/css" />
      <link href="../../../../css/StiloAdministrativo.css" rel="stylesheet" type="text/css" />
     <asp:Button ID="Btndisparaalertas" runat="server" CssClass="alertas" Text="" Visible="true"
                Width="0px" />
    <asp:UpdatePanel ID="upevomaysod" runat="server">
    <ContentTemplate>
    
    
    
          <rsweb:ReportViewer ID="Reportevosodxpdv" Font-Names="Verdana" runat="server" Font-Size="8pt"  ProcessingMode="Remote" 
            ShowParameterPrompts="False" ToolTip="SOD por PDV Mayorista" style="width:auto;"
             SizeToReportContent="True" 
            ZoomMode="FullPage" Visible="False">
  
</rsweb:ReportViewer>
    
    
    </ContentTemplate>
      <Triggers>
    <asp:AsyncPostBackTrigger ControlID="Btndisparaalertas" />
    </Triggers>
    
    
    </asp:UpdatePanel>