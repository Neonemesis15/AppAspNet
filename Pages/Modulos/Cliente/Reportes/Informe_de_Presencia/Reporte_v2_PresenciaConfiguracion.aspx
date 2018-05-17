<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v2_PresenciaConfiguracion.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Informe_de_Presencia.Reporte_v2_PresenciaConfiguracion" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/Pages/Modulos/Cliente/Reportes/Informes_validacion/UC_ValidarPeriodos.ascx"
    TagName="UC_ValidarPeriodos" TagPrefix="uc1" %>
<%@ Register Src="Form_Presencia_Objetivos.ascx" TagName="Form_Presencia_Objetivos"
    TagPrefix="uc5" %>
<%@ Register Src="Form_Presencia_PrecSugeridos.ascx" TagName="Form_Presencia_PrecSugeridos"
    TagPrefix="uc6" %>
<%@ Register Src="UC_Form_TextEditor.ascx" TagName="UC_Form_TextEditor" TagPrefix="uc1" %>
<%-- Referencias de master--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<%-- end de master--%>
<%--  referecias ajax--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%-- end referecias ajax--%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Reporte de Presencia
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="ScriptReferences" ContentPlaceHolderID="ScriptIncludePlaceHolder"
    runat="server">
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content_body" ContentPlaceHolderID="SheetContentPlaceHolder" runat="server">
    <telerik:RadSplitter ID="RadSplinter_presencia" runat="server" Orientation="Vertical"
        Width="100%" Height="560px">
        <telerik:RadPane ID="RadPane_presencia" runat="server" Width="300px" MaxWidth="300"
            Collapsed="false">
            <telerik:RadPanelBar ID="RadPanelBar_menu" runat="server" ExpandMode="FullExpandedItem"
                Height="525px" Width="300px" Skin="Outlook">
                <CollapseAnimation Type="InCubic" />
                <Items>
                    <telerik:RadPanelItem runat="server" Text="Reportes" Selected="true" Expanded="true">
                        <Items>
                            <telerik:RadPanelItem runat="server" Value="menu" Height="500px">
                                <ItemTemplate>
                                    <telerik:RadMenu ID="rad_menu" runat="server" Flow="Vertical" EnableRoundedCorners="true"
                                        EnableShadows="true" Style="padding-top: 10px; padding-bottom: 100px; z-index: inherit;"
                                        Skin="Outlook" Width="100%">
                                    </telerik:RadMenu>
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>
                    <%-- <telerik:RadPanelItem runat="server" Text="Favoritos">
                        <Items>
                            <telerik:RadPanelItem runat="server" Value="favorito" Height="500px">
                                <ItemTemplate>
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>--%>
                    <telerik:RadPanelItem runat="server" NavigateUrl="~/Pages/Modulos/Cliente/Reportes/Informe_de_Presencia/Reporte_v2_PresenciaConfiguracion.aspx"
                        Text="Configuración" Value="configuracion" Visible="false">
                    </telerik:RadPanelItem>
                </Items>
                <ExpandAnimation Type="InCubic" />
            </telerik:RadPanelBar>
        </telerik:RadPane>
        <telerik:RadSplitBar ID="RadSplitBar_presencia" runat="server" CollapseMode="Forward"
            ToolTip="Filtros/Menu" CollapseExpandPaneText="Filtros/Menu" />
        <telerik:RadPane ID="RadPane_contenidoReportes" runat="server" Scrolling="Both">
            <div>
                <asp:UpdatePanel ID="up_objetivo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td align="left">
                                    <telerik:RadTabStrip ID="RadTabStrip_Config" runat="server" MultiPageID="RadMultiPage_config"
                                        SelectedIndex="2" Skin="Default">
                                        <Tabs>
                                            <telerik:RadTab Text="Resumen Ejecutivo" PageViewID="RadPageView_RE" runat="server"
                                                Owner="" Selected="true">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="Objetivos de presencia" PageViewID="RadPageView_objetivos"
                                                runat="server" Owner="">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="Precios sugeridos" PageViewID="RadPageView_precSugerido" runat="server"
                                                Owner="">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="Parametrización" PageViewID="RadPageView_Parametrizacion" runat="server"
                                                Owner="">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #C0C0C0; width: 100%;">
                                    <telerik:RadMultiPage ID="RadMultiPage_config" runat="server" SelectedIndex="2">
                                        <telerik:RadPageView ID="RadPageView_RE" runat="server" Selected="true">
                                            <div id="div_viewHtmlFormat" runat="server">
                                            </div>
                                            <div>
                                                <uc1:UC_Form_TextEditor ID="UC_Form_TextEditor1" runat="server" />
                                            </div>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView_objetivos" runat="server">
                                            <uc5:Form_Presencia_Objetivos ID="Form_Presencia_Objetivos1" runat="server" />
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView_precSugerido" runat="server" Width="100%">
                                            <uc6:Form_Presencia_PrecSugeridos ID="Form_Presencia_PrecSugeridos1" runat="server" />
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView_Parametrizacion" runat="server" Width="100%"
                                            Visible="false">
                                            <div style="width: 100%; margin: auto">
                                                <%--   <span>Año:</span><telerik:RadComboBox ID="rcmb_añoPr" runat="server">
                                                        </telerik:RadComboBox>
                                                        <span>Mes:</span><telerik:RadComboBox ID="rcmb_mesPr" runat="server">
                                                        </telerik:RadComboBox>--%>
                                                <span>Gráfico:</span><telerik:RadComboBox ID="rcmb_grafico" runat="server" Width="200px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="rcmb_grafico_SelectedIndexChanged">
                                                </telerik:RadComboBox>
                                                <br />
                                                <br />
                                                <p>
                                                    <span>
                                                        <asp:Label ID="lbl_grafico_text" runat="server"></asp:Label></span></p>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Filtrar por categoria:
                                                                <div>
                                                                    <telerik:RadListBox ID="rlb_category" runat="server" Height="200px" Width="300px"
                                                                        EmptyMessage="Sin cateorias" Culture="es-PE" OnSelectedIndexChanged="rlb_category_SelectedIndexChanged"
                                                                        AutoPostBack="true">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                Lista de productos:
                                                                <div>
                                                                    <telerik:RadListBox ID="rlb_productos" runat="server" Height="200px" Width="300px"
                                                                        AllowTransfer="true" TransferToID="rlb_lstproductos" EmptyMessage="Sin productos"
                                                                        Culture="es-PE">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                Productos seleccionados:
                                                                <div>
                                                                    <telerik:RadListBox ID="rlb_lstproductos" runat="server" Height="200px" Width="300px"
                                                                        EmptyMessage="Sin productos seleccionados" Culture="es-PE">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <p>
                                                    <asp:Label ID="lbl_mensaje" runat="server"></asp:Label></p>
                                                <asp:Button ID="btn_guardar_params" runat="server" Text="Actualizar" CssClass="buttonSearch"
                                                    Height="25px" Width="164px" OnClick="btn_guardar_params_Click" />
                                            </div>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <cc2:ModalUpdateProgress ID="ModalUpdateProgress_objetivo" runat="server" DisplayAfter="3"
                    AssociatedUpdatePanelID="up_objetivo" BackgroundCssClass="modalProgressGreyBackground"
                    DynamicLayout="true">
                    <ProgressTemplate>
                        <div>
                            <img alt="Procesando" src="../../../../images/loading5.gif" style="vertical-align: middle" />
                        </div>
                    </ProgressTemplate>
                </cc2:ModalUpdateProgress>
                <cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender_objetivos" runat="server"
                    TargetControlID="up_objetivo">
                    <Animations> 
                                <OnUpdated>
                                    <Sequence>
                                      <FadeOut Duration="0.2" Fps="30" />
                                      <FadeIn  Duration="0.2" Fps="30" />
                                    </Sequence>
                              </OnUpdated>
                    </Animations>
                </cc1:UpdatePanelAnimationExtender>
                <br />
                <br />
            </div>
        </telerik:RadPane>
    </telerik:RadSplitter>
</asp:Content>
