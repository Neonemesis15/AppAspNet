<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Brief_Campaña.aspx.cs" Inherits="SIGE.Pages.Modulos.Planning.Reports.Brief_Campaña" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
    
      function pageLoad() {
      }
    
    </script>
</head>
<body >
    <form id="form1" runat="server" >
    <div style="height: 449px">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <br />
        <br />
        <br />
        <rsweb:ReportViewer ID="ReportBrief" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="1800px" ProcessingMode="Remote" 
            ShowParameterPrompts="False" ToolTip="Brief de Campaña" Width="1450px" 
            ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="True" 
            ZoomMode="PageWidth" >
            <ServerReport ReportServerUrl="" />
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
