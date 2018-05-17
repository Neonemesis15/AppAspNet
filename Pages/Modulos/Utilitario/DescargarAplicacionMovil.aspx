<%@ Page Title="Descargar App Móvil" Language="C#" MasterPageFile="~/Pages/Modulos/Utilitario/MasterPage/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="DescargarAplicacionMovil.aspx.cs" Inherits="SIGE.Pages.Modulos.Utilitario.DescargarAplicacionMovil" EnableSessionState="True"%>

<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Utilitario/MasterPage/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Utilitario/MasterPage/DefaultHeader.ascx" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultSidebar1" Src="~/Pages/Modulos/Utilitario/MasterPage/DefaultSidebar1.ascx" %>
<%@ Register Assembly="obout_Show_Net" Namespace="OboutInc.Show" TagPrefix="obshow" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="server">
     <art:defaultheader ID="DefaultHeader" runat="server" />
            <style type="text/css">
        .style1
        {
            width: 128px;
        }
    </style>
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:defaultmenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <div style="text-align: left;">
        <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
            color: #D01887;">
            DESCARGA DE APLICACIONES MOVILES
        </div>
    </div>
    <br />
    <div align="center">
        <table>
            <tr>
                <td>
                    Programa de Instalación:
                </td>
                <td>
                    <a href="../../../Instalador_AppMovil/IDENJAL/iDENJavaApplicationLoader.exe">Descargar</a>
                </td>
            </tr>
            <tr>
                <td>
                    Driver del Equipo i465:
                </td>
                <td>
                    <a href="../../../Instalador_AppMovil/DriverMotorola/MotoHelper_2.0.51_Driver_5.2.0.exe">
                        Descargar</a>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div align="center">
        <telerik:RadGrid ID="RadGridControlVersion" runat="server" AutoGenerateColumns="False"
            GridLines="None" Width="700px">
            <MasterTableView NoMasterRecordsText="Sin Datos para mostrar.">
                <RowIndicatorColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>
                <ExpandCollapseColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn HeaderText="Nombre App Móvil" UniqueName="nombreApp" DataField="nombre">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Versión Actual" UniqueName="versionApp" DataField="version">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="rutaApp" ReadOnly="true" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("ruta") %>'> Descargar </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle Width="100px"></ItemStyle>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <br />
    <br />
</asp:Content>
