<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Reporte_v1_Cliente.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reporte_v1_Cliente" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%-- Referencias de master--%>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultHeader.ascx" %>
<%-- end de master--%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
  .:::. Descarga de Reportes
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="ReporteV1" runat="server" ContentPlaceHolderID="SheetContentPlaceHolder">
    <link href="../../css/StyleModCliente.css" rel="stylesheet" type="text/css" />
    <%--  <link href="../../css/navegadores.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" language="javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../js/carousel.js"></script>
    <script type="text/javascript" language="javascript">
        /*
        * Función para inicializar el carusel
        * se parametrizan todas las opciones del carusel.
        */
        stepcarousel.setup(
            {
                galleryid: 'carousel',
                beltclass: 'belt',
                panelclass: 'panel',
                autostep: { enable: false, moveby: 1, pause: 3000 },
                panelbehavior: { speed: 500, wraparound: true, persist: true },
                statusvars: ['statusA', 'statusB', 'statusC'],
                contenttype: ['external']
            }
        );
        /*
        *   Función para realziar los llamados http a las páginas, y recibe por parametro:
        *   @param: url            String Dirección de la página que retorna el contenido a cargar.
        *   @param: objetivoHtml   String El elemento html que contendrá el resultado de la carga.
        *   @param: parametros     JSON {par1: 'a',par2: 'b'}
        *   @param: mensaje        1,0 Si es 1 entonces mostrará una animación de cargando.
        */
    </script>
    <br />
    <br />
    <br />
    <table>
        <tr>
            <td style="width: 225px;">
            </td>
            <td>
                <div align="center">
                    <div align="center">
                        <asp:UpdatePanel ID="upreports" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="PanelClienteReportes" runat="server">
                                    <div id="CarrosTabla" align="center" style="max-width: 821px;">
                                        <div class="button-next" id="btnnext" runat="server" visible="false">
                                            <a href="javascript:stepcarousel.stepBy('carousel', 1)">
                                                <img src="../../img/derecha.png" alt="Siguiente" style="height: 50px;" />
                                            </a>
                                        </div>
                                        <div class="button-prev" id="btnprev" runat="server" visible="false">
                                            <a href="javascript:stepcarousel.stepBy('carousel', -1)">
                                                <img src="../../img/izquierda.png" alt="Atras" style="height: 50px;" />
                                            </a>
                                        </div>
                                        <div id="carousel" class="stepcarousel" align="center">
                                            <div class="belt" align="center">
                                                <asp:ListView runat="server" ID="ListViewReportes" OnSelectedIndexChanged="ListViewReportes_SelectedIndexChanged"
                                                    DataKeyNames="Report_Id" OnPagePropertiesChanging="ListViewReportes_PagePropertiesChanging"
                                                    OnSelectedIndexChanging="ListViewReportes_SelectedIndexChanging">
                                                    <LayoutTemplate>
                                                        <table runat="server" id="table1" align="center">
                                                            <tr runat="server" id="itemPlaceholder">
                                                            </tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr id="Tr1" runat="server" class="panel">
                                                            <td id="Td1" runat="server">
                                                                <br />
                                                                <asp:Label ID="CodReporte" runat="server" Text='<%#Eval("Report_Id") %>' Visible="false" />
                                                                <asp:LinkButton runat="server" ID="SelectReporteButton" Text="" CssClass='<%# "p" + Eval("Report_NameReport") %>'
                                                                    CommandName="Select" Width="163px" Height="94px" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="ListViewReportes" />
                                <asp:PostBackTrigger ControlID="ListViewReportes" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <table align="center" width="100%">
        <tr>
            <td width="60%" valign="top">
                <br />
                <br />
                <br />
                <div align="center">
                    <asp:UpdatePanel ID="upinfo" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="PanelInfo" runat="server" CssClass="central">
                                <br />
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbltipoRport" runat="server" Text="Use esta Opcion si quiere Filtrar sus Busquedas por Fecha"
                                                ForeColor="#FF3300" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table id="tbbusca" runat="server" visible="false">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblfechaini" runat="server" Text="Desde"></asp:Label>
                                            <asp:TextBox ID="txtfecha" runat="server" AutoPostBack="True" OnTextChanged="txtfecha_TextChanged"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="txtfecha_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfecha" UserDateFormat="DayMonthYear">
                                            </cc1:MaskedEditExtender>
                                            <asp:ImageButton ID="ImageButtonCal" runat="server" Enabled="True" Height="16px"
                                                ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                            <cc1:CalendarExtender ID="txtfecha_CalendarExtender" runat="server" Enabled="True"
                                                FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal" PopupPosition="TopLeft"
                                                TargetControlID="txtfecha">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFecfin" runat="server" Text="Hasta"></asp:Label>
                                            <asp:TextBox ID="txtfechafin" runat="server" OnTextChanged="txtfechafin_TextChanged"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="txtfechafin_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfechafin"
                                                UserDateFormat="DayMonthYear">
                                            </cc1:MaskedEditExtender>
                                            <asp:ImageButton ID="ImageButtonCal1" runat="server" Enabled="True" Height="16px"
                                                ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />
                                            <cc1:CalendarExtender ID="txtfechafin_CalendarExtender" runat="server" Enabled="True"
                                                FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal1" PopupPosition="TopLeft"
                                                TargetControlID="txtfechafin">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnsearchr" runat="server" ImageUrl="~/Pages/Modulos/Cliente/imgs/Search.png"
                                                OnClick="btnsearchr_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: auto;">
                                    <tr>
                                        <td bgcolor="#CCD4E1">
                                        </td>
                                        <td bgcolor="#CCD4E1" valign="middle">
                                            <asp:GridView ID="gvLink_informes" runat="server" AutoGenerateColumns="False" Style="width: 464px;"
                                                OnSelectedIndexChanged="gvLink_informes_SelectedIndexChanged" ForeColor="#333333"
                                                GridLines="None" CellPadding="8" HorizontalAlign="Center" AllowPaging="True"
                                                PageSize="4" OnPageIndexChanging="gvLink_informes_PageIndexChanging" EmptyDataRowStyle-ForeColor="#990099"
                                                Font-Names="Verdana" Font-Size="10pt">
                                                <RowStyle BackColor="#CCD4E1" Font-Bold="True" ForeColor="#666666" CssClass="FondoHeaderGrid"
                                                    HorizontalAlign="Left" Font-Size="10pt" />
                                                <EmptyDataRowStyle Font-Names="Verdana" Font-Size="10pt" ForeColor="#CC0099" />
                                                <Columns>
                                                    <asp:BoundField DataField="nom_reporte" HeaderText="NOMBRE DEL INFORME" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ruta_reporte" HeaderText="Archivo" Visible="false" />
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/ImgBooom/iconodescarga.png"
                                                        SelectText="" ShowSelectButton="True" ItemStyle-HorizontalAlign="Right">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCD4E1" Font-Bold="True" ForeColor="#333333" Font-Names="Verdana"
                                                    Font-Size="10pt" />
                                                <PagerStyle BackColor="#CCD4E1" Font-Bold="True" ForeColor="#333333" Font-Names="Verdana"
                                                    Font-Size="10pt" />
                                                <SelectedRowStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" Font-Names="Verdana"
                                                    Font-Size="10pt" />
                                                <HeaderStyle Font-Bold="True" ForeColor="Magenta" Font-Size="12px" Font-Names="Verdana"
                                                    CssClass="FondoHeaderGrid" />
                                                <EditRowStyle BackColor="#99CCFF" Font-Names="Verdana" Font-Size="10pt" />
                                                <AlternatingRowStyle BackColor="#CCD4E1" Font-Bold="True" ForeColor="#666666" HorizontalAlign="Left"
                                                    Font-Names="Verdana" Font-Size="10pt" />
                                            </asp:GridView>
                                            <asp:GridView ID="gvLink_informes_invisible" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" OnSelectedIndexChanged="gvLink_informes_SelectedIndexChanged"
                                                ForeColor="#333333" GridLines="None" Visible="False">
                                                <RowStyle ForeColor="#0033CC" BackColor="#D8D8D8" />
                                                <Columns>
                                                    <asp:BoundField DataField="nom_reporte" HeaderText="Nombre del Informe" />
                                                    <asp:BoundField DataField="ruta_reporte" HeaderText="Archivo" />
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/ViewDetails.png"
                                                        SelectText="" ShowSelectButton="True" />
                                                </Columns>
                                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#99CCFF" />
                                                <AlternatingRowStyle BackColor="#D8D8D8" ForeColor="#0033CC" />
                                            </asp:GridView>
                                        </td>
                                        <td bgcolor="#CCD4E1">
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnsearchr" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td class="style1">
                <asp:UpdatePanel ID="upcanales" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel" runat="server">
                            <div style="display: none;">
                                <iframe id="iframeExcel" runat="server" style="display: none;" src=""></iframe>
                                <%--panel de mensaje de usuario   --%>
                                <asp:Button ID="btndipararalerta" runat="server" Text="" Height="0px" CssClass="alertas"
                                    Width="0" Enabled="False" />
                                <asp:Panel ID="PCanal" runat="server" Height="169px" Width="332px" Style="display: none;">
                                    <table align="center">
                                        <tr>
                                            <td align="center" style="height: 119px; width: 79px;" valign="top">
                                                <br />
                                            </td>
                                            <td style="width: 238px; height: 119px;" valign="top">
                                                <br />
                                                <asp:Label ID="lblencabezado" runat="server" CssClass="labelsTit"></asp:Label>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblmensajegeneral" runat="server" CssClass="labels"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table align="center">
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnaceptar" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                                    Text="Aceptar" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <cc1:ModalPopupExtender ID="ModalPopupCanal" runat="server" Enabled="True" TargetControlID="btndipararalerta"
                                    PopupControlID="PCanal" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PanelCorreos" runat="server" Height="324px" Width="547px">
                                                <table align="center" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                                                width="6"> </img>
                                                        </td>
                                                        <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                            <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                                            </img>
                                                        </td>
                                                        <td>
                                                            <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                                                width="6"> </img>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                            <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                                            </img>
                                                        </td>
                                                        <td bgcolor="#CCD4E1" valign="top">
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Image ID="ImgMail" runat="server" Height="48px" ImageUrl="~/Pages/images/mailreminder.png"
                                                                            Width="63px" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="LblTitEnvioMail" runat="server" CssClass="labelsTit" Text="Novedades / Solicitudes"
                                                                            ForeColor="#0033CC"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table align="center">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="LblMailSolicitante" runat="server" CssClass="labels" Text="De" ForeColor="#0033CC"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtSolicitante" runat="server" Enabled="False" Width="400px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="LblMailPara" runat="server" CssClass="labels" Text="Para" ForeColor="#0033CC"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtEmail" runat="server" Enabled="False" Width="400px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="LblMotivo" runat="server" CssClass="labels" Text="Asunto" ForeColor="#0033CC"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtMotivo" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="Reqmotivo" runat="server" ControlToValidate="TxtMotivo"
                                                                            Display="None" ErrorMessage="Sr. Usuario, por favor no ingrese caracteres especiales y recuerde que no debe inicial con número"
                                                                            ValidationExpression="([a-zA-Z][a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]{0,49})"> </asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                                ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="Reqmotivo">
                                                                            </cc1:ValidatorCalloutExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="LblMensaje" runat="server" CssClass="labels" Text="Mensaje" ForeColor="#0033CC"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtMensaje" runat="server" Height="70px" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImgEnviarMail" runat="server" AlternateText="Enviar" BorderStyle="None"
                                                                            ImageUrl="../../images/BtnEnviarMail.png" onmouseout="this.src = '../../images/BtnEnviarMail.png'"
                                                                            onmouseover="this.src = '../../images/BtnEnviarMailDown.png'" Style="margin-left: 0px"
                                                                            OnClick="ImgEnviarMail_Click" /><asp:ImageButton ID="ImgCancelMail" runat="server"
                                                                                AlternateText="Cancelar" BorderStyle="None" ImageUrl="../../images/BtnCancelReg.png"
                                                                                onmouseout="this.src = '../../images/BtnCancelReg.png'" onmouseover="this.src = '../../images/BtncancelRegDown.png'"
                                                                                Style="margin-left: 0px" OnClick="ImgCancelMail_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                            <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                                            </img>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                                                width="6"> </img>
                                                        </td>
                                                        <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                            <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                                                width="1"> </img>
                                                        </td>
                                                        <td>
                                                            <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                                                width="6"> </img>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ScriptIncludePlaceHolder">
    <script type="text/javascript" src="<%= ResolveUrl("~/script.js") %>"></script>
    <style type="text/css">
        .style1
        {
            width: 4%;
        }
    </style>
</asp:Content>
