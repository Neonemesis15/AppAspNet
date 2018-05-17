﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_IndexPrice.ascx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informe_de_Presencia.Reporte_v2_IndexPrice" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
      <link href="../../../../css/StiloAdministrativo.css" rel="stylesheet" type="text/css" />
     <asp:Button ID="Btndisparaalertas" runat="server" CssClass="alertas" Text="" Visible="true"
                Width="0px" />
<asp:UpdatePanel ID="upIP" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <ContentTemplate>
        <rsweb:ReportViewer ID="ReportIndexPrice" runat="server" Font-Names="Verdana" Font-Size="8pt"
            ProcessingMode="Remote" Height="100%" Width="100%" ShowParameterPrompts="False"
            ToolTip="Wholesalers Reports" ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="false"
            ZoomMode="Percent" Visible="False" >
        </rsweb:ReportViewer>
    </ContentTemplate>
       <Triggers>
    <asp:AsyncPostBackTrigger ControlID="Btndisparaalertas" />
    </Triggers>
</asp:UpdatePanel>
