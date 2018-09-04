<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pruebas-reportes.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Pruebas_reportes" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    
</head>

<body>
    
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Button ID="btngenerar" runat="server" onclick="btngenerar_Click" 
            Text="Generar" />
    <asp:UpdatePanel ID="uppruebareport" runat="server">
    <ContentTemplate>
    
    
    
    <div>
    <table>
    <tr>
    <td>
        <asp:DropDownList ID="cmbcategorias" runat="server" AutoPostBack="True">
        </asp:DropDownList>
    
    
    </td>
    <td>
        <asp:TextBox ID="txtfecini" runat="server"></asp:TextBox>
        
         
    
    </td>
    <td>
       <asp:TextBox ID="txtfecfin" runat="server"></asp:TextBox>
    
        
    
    </td>
    
    </tr>
    
    
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    
        <rsweb:ReportViewer ID="reportPrueba" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="1800px" ProcessingMode="Remote" 
        ToolTip="Brief de Campaña" Width="1450px" 
            ZoomPercent="100" DocumentMapWidth="100%" SizeToReportContent="True" 
            ZoomMode="PageWidth"  >
            <ServerReport 
                ReportServerUrl="" />
        </rsweb:ReportViewer>
    
    
    </form>
</body>
</html>
