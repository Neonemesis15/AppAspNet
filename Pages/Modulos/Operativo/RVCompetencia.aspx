<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RVCompetencia.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.RVCompetencia" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>        
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="1058px"  
            ProcessingMode="Remote" Width="1300px" ShowPrintButton="False">
             
            <ServerReport ReportPath="" 
                ReportServerUrl=""/>
            
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
