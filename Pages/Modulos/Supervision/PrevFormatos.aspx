<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrevFormatos.aspx.cs" Inherits="SIGE.Pages.Modulos.Supervision.PrevFormatos" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="559px" Width="1035px" 
            ZoomMode="FullPage">
            <localreport reportpath="PrevFormato.rdlc">
                <datasources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                        Name="FormatosDataSet_UP_WEB_CONSULTARPLANNINGCONTENIDOFORMATOS" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" 
                        Name="FormatosDataSet_UP_WEB_CONSULTARPLANNINGPDV" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource3" 
                        Name="FormatosDataSet_UP_WEB_CONSULTARPLANNINGCONTENIDOFORMATOSPIE" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource4" 
                        Name="FormatosDataSet_UP_WEB_CONSULTARPLANNINGCONTENIDOFORMATOSCUERPO" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource5" 
                        Name="BDLUCKYCODataSet_Planning" />
                </datasources>
            </localreport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
            TypeName="SIGE.FormatosDataSetTableAdapters.UP_WEB_CONSULTARPLANNINGCONTENIDOFORMATOSTableAdapter">
            <SelectParameters>
                <asp:SessionParameter Name="id_planning" SessionField="Planning" Type="Int32" />
                <asp:SessionParameter Name="id_cod_Point" SessionField="id_cod_point" 
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
            
            TypeName="SIGE.FormatosDataSetTableAdapters.UP_WEB_CONSULTARPLANNINGPDVTableAdapter">
            <SelectParameters>
                <asp:SessionParameter Name="id_planning" SessionField="Planning" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
            
            
            TypeName="SIGE.FormatosDataSetTableAdapters.UP_WEB_CONSULTARPLANNINGCONTENIDOFORMATOSPIETableAdapter">
            <SelectParameters>
                <asp:SessionParameter Name="id_planning" SessionField="Planning" Type="Int32" />
                <asp:SessionParameter DefaultValue="id_cod_point" Name="id_cod_Point" 
                    SessionField="id_cod_point" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
            
            
            
            TypeName="SIGE.FormatosDataSetTableAdapters.UP_WEB_CONSULTARPLANNINGCONTENIDOFORMATOSCUERPOTableAdapter">
            <SelectParameters>
                <asp:SessionParameter Name="id_planning" SessionField="Planning" Type="Int32" />
                <asp:SessionParameter DefaultValue="id_cod_point" Name="id_cod_Point" 
                    SessionField="id_cod_point" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
            
            
            TypeName="SIGE.BDLUCKYCODataSetTableAdapters.PlanningTableAdapter" 
            DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="Original_id_planning" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Planning_Name" Type="String" />
                <asp:Parameter Name="cod_Strategy" Type="Int32" />
                <asp:Parameter Name="Planning_CodChannel" Type="String" />
                <asp:Parameter Name="Planning_StartActivity" Type="DateTime" />
                <asp:Parameter Name="Planning_EndActivity" Type="DateTime" />
                <asp:Parameter Name="Planning_PictureFolder" Type="String" />
                <asp:Parameter Name="Planning_Budget" Type="String" />
                <asp:Parameter Name="Planning_Status" Type="Boolean" />
                <asp:Parameter Name="Planning_CreateBy" Type="String" />
                <asp:Parameter Name="Planning_DateBy" Type="DateTime" />
                <asp:Parameter Name="Planning_ModiBy" Type="String" />
                <asp:Parameter Name="Planning_DateModiBy" Type="DateTime" />
                <asp:Parameter Name="Original_id_planning" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="Planning_Name" Type="String" />
                <asp:Parameter Name="cod_Strategy" Type="Int32" />
                <asp:Parameter Name="Planning_CodChannel" Type="String" />
                <asp:Parameter Name="Planning_StartActivity" Type="DateTime" />
                <asp:Parameter Name="Planning_EndActivity" Type="DateTime" />
                <asp:Parameter Name="Planning_PictureFolder" Type="String" />
                <asp:Parameter Name="Planning_Budget" Type="String" />
                <asp:Parameter Name="Planning_Status" Type="Boolean" />
                <asp:Parameter Name="Planning_CreateBy" Type="String" />
                <asp:Parameter Name="Planning_DateBy" Type="DateTime" />
                <asp:Parameter Name="Planning_ModiBy" Type="String" />
                <asp:Parameter Name="Planning_DateModiBy" Type="DateTime" />
            </InsertParameters>
        </asp:ObjectDataSource>
    </div>
    <table align="center">
        <tr>
            <td>
                <asp:Button ID="BtnRegresar" runat="server" Text="Regresar" CssClass="button" 
                    onclick="BtnRegresar_Click" />
                    <br />
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
