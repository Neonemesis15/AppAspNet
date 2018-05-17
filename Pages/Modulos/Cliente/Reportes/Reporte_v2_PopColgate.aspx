<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master" AutoEventWireup="true" CodeBehind="Reporte_v2_PopColgate.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_PopColgate" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%-- Referencias de master--%>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Informe_de_Competencia/Reporte_v2_Competencia_ResumenEjecutivo.ascx"
    TagName="Reporte_v2_Competencia_ResumenEjecutivo" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc3" %>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Reporte de Competencia
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <!--script type="text/javascript" src="../../../js/jquery-1.4.2.min.js"></script-->
    <script type="text/javascript">

        jQuery(document).ready(function () {

            $('td img').css('height', '320px');
            $('td img').css('width', '320px');



            /* Apply fancybox to multiple items */

            $("a.class").fancybox({
                'titleShow': false,
                'transitionIn': 'elastic',
                'transitionOut': 'elastic',
                'easingIn': 'easeOutBack',
                'easingOut': 'easeInBack'
            });

        });

    </script>
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <%-- Start body--%>
    <telerik:RadSplitter ID="RadSplinter_presencia" runat="server" Orientation="Vertical"
        Width="100%" Height="530px">
        <telerik:RadPane ID="RadPane_presencia" runat="server" Width="300px" MaxWidth="300">
            <telerik:RadPanelBar ID="RadPanelBar_menu" runat="server" ExpandMode="FullExpandedItem"
                Height="525px" Width="300px" Skin="Outlook">
                <CollapseAnimation Type="InCubic" />
                <Items>
                    <telerik:RadPanelItem runat="server" Text="Filtros" Selected="true" Expanded="true">
                        <Items>
                            <telerik:RadPanelItem runat="server" Value="filtro" Height="500px">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                Año
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmb_año" runat="server">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Mes
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmb_mes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmb_mes_SelectedIndexChanged">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Periodo
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmb_periodo" runat="server">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="text-align: center">
                                        <asp:Button ID="btngnerar" runat="server" Text="Generar Informe" CssClass="buttonOcultar"
                                            Height="16px" Width="140px" Visible="true" OnClick="btngnerar_Click" ToolTip="Permite Generar el Informe" />
                                    </div>
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem runat="server" Text="Menu">
                        <Items>
                            <telerik:RadPanelItem runat="server" Value="menu" Height="500px">
                                <ItemTemplate>
                                    <telerik:RadMenu ID="rad_menu" runat="server" Flow="Vertical" EnableRoundedCorners="true"
                                        EnableShadows="true" Style="padding-top: 10px; padding-bottom: 100px; z-index: inherit;"
                                        Skin="Outlook">
                                    </telerik:RadMenu>
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>
                </Items>
                <ExpandAnimation Type="InCubic" />
            </telerik:RadPanelBar>
        </telerik:RadPane>
        <telerik:RadSplitBar ID="RadSplitBar_presencia" runat="server" CollapseMode="Forward" />
        <telerik:RadPane ID="RadPane_contenidoReportes" runat="server" Height="400px">
            <div id="Titulo_reporte_Efectividad" style="text-align: left;">
                <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
                    color: #D01887;">
                    MATERIAL POP
                </div>
            </div>
            <br />
            <div id="div">
                <asp:Panel runat="server" ID="panel">

                <% 
     
                    string cadena="";
                    string Company="";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                   {
               %>



                      <div style="background-color:#333399;font-size:15px;color:White;width:100%;height:25px;font-weight:bolder;text-align:left;">
                      <%  
                       try
                       {
                           cadena = ds.Tables[0].Rows[i]["Cadena"].ToString();
                           Response.Write(cadena);
                       }
                       catch
                       {
                           Response.Write(cadena);
                       }
                              %>  
                      </div>
                      <br />
                     

                      <table>
                      <tr>
                       <!----crear tablas hacia la derecha---->

                       <%
                      
                           
                       llenargrilla1(cadena,"");
                           
                           
                       for (int j = 0; j < ds.Tables[1].Rows.Count;j++ )
                       {
                           

                          byte[] imageCol = (byte[])ds.Tables[1].Rows[j]["Foto"];

                          grabarimg(cadena  + "_" + j.ToString(), imageCol);
                           
                           
                       %>
                   
                      <td  valign="top">
                      
                     
                      <table cellpadding="0" cellspacing="0" border="1" style="color:Black;text-align:center">
                      <tr >
                      <td style="background-color:#DDD9C3" >
                      Tipo de Material POP:
                      </td>
                      <td>
                      <%
                     Response.Write(ds.Tables[1].Rows[i]["Material_POP"].ToString());
                      %>
                      </td>
                      </tr>
                      <tr >
                      <td style="background-color:#DDD9C3">
                      Vigencia:
                      </td>
                      <td>
                      <%
                           Response.Write("Del " + ds.Tables[1].Rows[i]["Fecha_ini"].ToString() + " al " + ds.Tables[1].Rows[i]["Fecha_fin"].ToString());
                      %>
                      </td>
                      </tr>
                      <tr>
                      <td colspan="2">
                       <a runat="server" id="link" class="class"> <img runat="server" id="imag"  /> </a>
                      </td>
                      </tr>

                      </table>
                       </td>
                          <% }
                                
                       %>
                        <!----fin crear tablas hacia la derecha---->
                      </tr>
                      </table>
                        <%
                   }
                 %>
                </asp:Panel>
            </div>
        </telerik:RadPane>
    </telerik:RadSplitter>
</asp:Content>

