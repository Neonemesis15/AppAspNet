<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_SF_M_Stock_Ventas_X_Familia.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informes_SF_M_Stock.uc_SF_M_Stock_Ventas_X_Familia" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc3" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<link href="../../../../css/Cliente_V2.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .style3
    {
        width: 300px;
        font-weight: bold;
    }
</style>
<asp:UpdatePanel ID="UpFiltrosPrecios" runat="server">
    <ContentTemplate>
        <div align="center">
            <table style="width: 100%; height: auto;" >
                <tr>
                    <td class="style3">
                        Reporte de Stock - Ventas por Familia
                    </td>
                </tr>
            </table>
            <fieldset style="width: auto">
                <legend>Consultar Reporte Stock - Ventas por Familia</legend>
                <table>
                    <tr>
                        <td align="right">
                            Oficina:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddl_Oficina" runat="server" Skin="Vista" Width="150px" Style="text-align: left"
                                AutoPostBack="True" OnSelectedIndexChanged="ddl_Oficina_SelectedIndexChanged"
                                EnableTextSelection="False">
                            </telerik:RadComboBox>
                        </td>
                        <td align="right">
                            Fuerza de Venta :
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddl_FuerzaDeVenta" runat="server" Skin="Vista" Width="150px"
                                Style="text-align: left" AutoPostBack="True" EnableTextSelection="False">
                            </telerik:RadComboBox>
                        </td>
                        <td align="right">
                            Categoria:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddl_Categoria" runat="server" Skin="Vista" Width="150px"
                                Style="text-align: left" AutoPostBack="True" OnSelectedIndexChanged="ddl_Categoria_SelectedIndexChanged"
                                EnableTextSelection="False">
                            </telerik:RadComboBox>
                        </td>
                        <td align="right">
                            Familia :
                        </td>
                        <td>
                        <%-- <telerik:RadComboBox ID="ddl_Familia" runat="server" Skin="Vista" Width="150px"
                        Style="text-align: left" AutoPostBack="True" 
                        onselectedindexchanged="ddl_Familia_SelectedIndexChanged" EnableTextSelection="False">
                        </telerik:RadComboBox>--%>
                            <cc3:DropDownCheckBoxes ID="ddl_Familia" runat="server" AddJQueryReference="true"
                                UseButtons="true" UseSelectAllNode="true" Style="top: 0px; left: 315px; height: 16px;
                                width: 500px" onselectedindexchanged="ddl_Familia_SelectedIndexChanged" >
                                <Texts SelectBoxCaption="--Seleccione Familia(s)--" />
                            </cc3:DropDownCheckBoxes>
                        </td>
                        <td align="right">
                            Año :
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddl_Year" runat="server" Skin="Vista" Width="150px" Style="text-align: left"
                                AutoPostBack="True" EnableTextSelection="False">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Corporación :
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddl_Corporacion" runat="server" Skin="Vista" Width="150px"
                                Style="text-align: left" AutoPostBack="True" OnSelectedIndexChanged="ddl_Corporacion_SelectedIndexChanged"
                                EnableTextSelection="False">
                            </telerik:RadComboBox>
                        </td>
                        <td align="right">
                            Supervisor :
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddl_Supervisor" runat="server" Skin="Vista" Width="150px"
                                Style="text-align: left" AutoPostBack="True" EnableTextSelection="False">
                            </telerik:RadComboBox>
                        </td>
                        <td align="right">
                            Marca :
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddl_Marca" runat="server" Skin="Vista" Width="150px" Style="text-align: left"
                                AutoPostBack="True" OnSelectedIndexChanged="ddl_Marca_SelectedIndexChanged" EnableTextSelection="False">
                            </telerik:RadComboBox>
                        </td>
                        <td align="right">
                            SubFamilia:&nbsp;
                        </td>
                        <td>
                           <%-- <telerik:RadComboBox ID="ddl_Subfamilia" runat="server" Skin="Vista" Width="150px"
                                Style="text-align: left" AutoPostBack="True" EnableTextSelection="False">
                            </telerik:RadComboBox>--%>
                             <cc3:DropDownCheckBoxes ID="ddl_Subfamilia" runat="server" 
                             AddJQueryReference= "true"
                                UseButtons="true" UseSelectAllNode="true" Style="top: 0px; left: 315px; height: 16px;
                                width: 241px" onselectedindexchanged="ddl_Subfamilia_SelectedIndexChanged">
                                <Texts SelectBoxCaption="--Seleccione Subfamilia(s)--" />
                            </cc3:DropDownCheckBoxes>
                        </td>
                        <td align="right">
                            Mes :
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddl_Month" runat="server" Skin="Vista" Width="150px" Style="text-align: left"
                                AutoPostBack="True" OnSelectedIndexChanged="ddl_Month_SelectedIndexChanged" EnableTextSelection="False">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Cadena :
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddl_NodoComercial" runat="server" Skin="Vista" Width="150px"
                                Style="text-align: left" AutoPostBack="True" OnSelectedIndexChanged="ddl_NodoComercial_SelectedIndexChanged"
                                EnableTextSelection="False">
                            </telerik:RadComboBox>
                        </td>
                        <td align="right">
                            Mercaderista
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddl_Mercaderista" runat="server" Skin="Vista" Width="150px"
                                Style="text-align: left" AutoPostBack="True" EnableTextSelection="False">
                            </telerik:RadComboBox>
                        </td>
                        <td align="right">
                        </td>
                        <td>
                        </td>
                        <td align="right">
                            Producto:</td>
                        <td>
                        <cc3:DropDownCheckBoxes ID="ddl_Producto" runat="server" 
                             AddJQueryReference= "true"
                                UseButtons="true" UseSelectAllNode="true" Style="top: 0px; left: 315px; height: 16px;
                                width: 241px" onselectedindexchanged="ddl_Producto_SelectedIndexChanged1">
                                <Texts SelectBoxCaption="--Seleccione Producto(s)--" />
                            </cc3:DropDownCheckBoxes>
                        </td>
                        <td>
                            Periodo :
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddl_Periodo" runat="server" Skin="Vista" Width="150px" Style="text-align: left"
                                AutoPostBack="True" OnSelectedIndexChanged="ddl_Periodo_SelectedIndexChanged"
                                EnableTextSelection="False">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Punto de Venta :
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddl_PuntoDeVenta" runat="server" Skin="Vista" Width="150px"
                                Style="text-align: left" AutoPostBack="True" EnableTextSelection="False">
                            </telerik:RadComboBox>
                        </td>
                        <td align="right">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                        <td align="right">
                            Días:
                        </td>
                        <td>
                        <%-- <telerik:RadComboBox ID="ddl_Dia" runat="server" Skin="Vista" Width="150px" Style="text-align: left"
                                AutoPostBack="True" EnableTextSelection="False">
                            </telerik:RadComboBox>--%>

                            <cc3:DropDownCheckBoxes ID="ddl_Dia" runat="server" 
                             AddJQueryReference= "true"
                                UseButtons="true" UseSelectAllNode="true" Style="top: 0px; left: 315px; height: 16px;
                                width: 241px">
                                <Texts SelectBoxCaption="--Seleccione Día(s)--" />
                            </cc3:DropDownCheckBoxes>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="10" align="center">
                            <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="buttonRed" Width="164px"
                                OnClick="btn_buscar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblmensaje" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div id="div_Reporting" runat="server" aling="center" style="width: 100%; height: auto;">
            <rsweb:ReportViewer ID="Reporte_SF_M_Stock_Ventas_X_Familia" runat="server" Font-Names="Verdana"
                Font-Size="8pt" ProcessingMode="Remote" Style="width: auto" ShowParameterPrompts="False"
                ToolTip="Ventas por Familia" ZoomMode="FullPage" Visible="True" ZoomPercent="100">
            </rsweb:ReportViewer>
            <br />
            <br />
        </div>
        <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" AssociatedUpdatePanelID="UpFiltrosPrecios"
            BackgroundCssClass="modalProgressGreyBackground">
            <ProgressTemplate>
                <div class="modalPopup">
                    Procesando, por favor espere...
                </div>
            </ProgressTemplate>
        </cc2:ModalUpdateProgress>
    </ContentTemplate>
</asp:UpdatePanel>
