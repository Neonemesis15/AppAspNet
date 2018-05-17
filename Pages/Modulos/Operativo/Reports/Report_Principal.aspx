<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/Report_Principal.Master" AutoEventWireup="true" CodeBehind="Report_Principal.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Principal1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table style="width: 100%; height: auto;" align="center" class="style7">
            <tr>
                <td class="style12">
                    REPORTES</td>
            </tr>
             <tr>
                <td class="style16">
                    <asp:LinkButton ID="LinkButton5" runat="server" onclick="LinkButton5_Click" >Reporte Stock Por dia</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style16">
                    <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click">Reporte Precios</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style11">
                    <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click">Reporte Quiebre</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style19">
                    <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">Reporte Fotografico</asp:LinkButton>
                </td>
            </tr>

             <tr>
                <td class="style19">
                    <asp:LinkButton ID="LinkSegmento" runat="server" onclick="LinkSegmento_Click">Seguimiento Segmentacion</asp:LinkButton>
                </td>
            </tr>
              <tr>
                <td class="style19">
                    <asp:LinkButton ID="Likrptsegnv" runat="server" onclick="Likrptsegnv_Click">Seguimiento Visitas No Efectivas</asp:LinkButton>
                </td>
            </tr>
             <tr>
                <td class="style19">
                    <asp:LinkButton ID="Likrptsegmen" runat="server" onclick="Likrptsegmen_Click">Consolidado Segmentacion</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style20">
                    <asp:LinkButton ID="LinkButton8" runat="server" onclick="LinkButton8_Click">Reporte SOD</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style18">
                    <asp:LinkButton ID="LinkButton7" runat="server" onclick="LinkButton7_Click">Reporte Layout</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style21">
                    <asp:LinkButton ID="LinkButton9" runat="server" onclick="LinkButton9_Click">Reporte Exhibicion</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style19">
                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Reporte Competencia</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style19">
                    <asp:LinkButton ID="LinkButton6" runat="server" onclick="LinkButton6_Click">Reporte Stock</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style19">
                    <asp:LinkButton ID="liksgingr" runat="server" onclick="liksgingr_Click">Seguimiento Ingresos</asp:LinkButton>
                </td>
            </tr>

            
        </table>
</asp:Content>




<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style7
        {
            height: 328px;
        width: 465px;
    }
        .style11
        {
            height: 40px;
        }
    .style12
    {
        text-align: center;
        height: 23px;
            font-weight: bold;
            font-size: large;
            
        }
    .style16
    {
        height: 36px;
    }
    .style18
    {
        height: 39px;
    }
    .style19
    {
        height: 38px;
    }
    .style20
    {
        height: 34px;
    }
    .style21
    {
        height: 37px;
    }
    </style>





</asp:Content>





