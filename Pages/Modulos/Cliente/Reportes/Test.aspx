<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Test" %>
 <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
        hola mundo
    <form id="form1" runat="server">
      <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"  AsyncPostBackTimeout="360000" ScriptMode="Release">
    </cc1:ToolkitScriptManager> 
     
    <asp:UpdatePanel ID="upWS" runat="server">
    <ContentTemplate>
        <%--<rsweb:ReportViewer ID="reporteHistorical" runat="server" Font-Names="Verdana"
            Font-Size="8pt" ProcessingMode="Remote" Height="1800px" Width="2500px" ShowParameterPrompts="False"
            ToolTip="Wholesalers Reports" ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="True"
            ZoomMode="PageWidth" Visible="True" PromptAreaCollapsed="False" ViewStateMode="Inherit">
        </rsweb:ReportViewer>
--%>
            
    <rsweb:ReportViewer ID="reporteHistorical" runat="server" ProcessingMode="Remote" Height="500px" Width="100%" ZoomPercent="100"
     ShowParameterPrompts="false" ZoomMode="Percent" DocumentMapWidth="100%" >
    </rsweb:ReportViewer> 
    </ContentTemplate>    
</asp:UpdatePanel>
</form>
</body>
</html>
