<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_Precio_InformePrecio.ascx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_Precio.Reporte_v2_Precio_InformePrecio" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<link href="../../../../css/Cliente_V2.css" rel="stylesheet" type="text/css" />

<p>
    &nbsp;</p>
   <%-- <div   align="center"  style="background-color:Blue;">--%>
        <%-- <asp:Button ID="btn_consultar0" runat="server" CssClass="buttonEditPlan" 
                                Height="24px" Text="Consultar" Width="160px" 
             onclick="btn_consultar0_Click" Visible="false" />--%>
    
    
      
  <%--  </div>--%>
 <asp:UpdatePanel ID= "reporprecio" runat="server">
 <ContentTemplate>
 
 <rsweb:ReportViewer ID="reportinPrecios" runat="server"
Font-Size="8pt" ProcessingMode="Remote"
            ShowParameterPrompts="False" ToolTip="Panel de Precios" style="width:auto;"
         SizeToReportContent="True" 
            ZoomMode="Percent" ShowZoomControl="true" Visible="False">
</rsweb:ReportViewer>
 
 </ContentTemplate>
 
 </asp:UpdatePanel>


                     



                        