<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ini_operativo.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Operativo.ini_operativo" Culture="auto" UICulture="auto"
    EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI.Compatibility" Namespace="eWorld.UI.Compatibility"
    TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <title>Modulo Operativo</title>

    <script type="text/javascript">
        function pageLoad() 
        {
        }
    </script>

    <script type="text/javascript">
        function descargar(url) {        
        document.location = url;
        }        
    </script>

    <script src="../../js/Print.js" type="text/javascript"></script>

    <script src="../../js/Print2.js" type="text/javascript"></script>

    <style type="text/css">
        .style49
        {
            width: 238px;
            height: 119px;
        }
        .style50
        {
            height: 119px;
            width: 79px;
        }
        #Lucky
        {
            height: 95px;
            width: 396px;
        }
        #IframeCompe
        {
            width: 1297px;
            height: 502px;
        }
    </style>
</head>
<body class="backcolor1">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" />
        <asp:UpdatePanel ID="updatepanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="panel1" runat="server">
                    <table>
                        <tr>
                            <td style="width: 1024px">
                                <asp:Label ID="LbltitOperativo" runat="server" Font-Names="Bauhaus 93" Font-Size="18pt"
                                    Text="Operativo SIGE" Width="357px"></asp:Label>
                                <asp:Button ID="Btndisparaalertas" runat="server" CssClass="alertas" Text="" Visible="true"
                                    Width="0px" />
                                <asp:Button ID="BtndisparaalertasFotos" runat="server" CssClass="alertas" Text=""
                                    Visible="true" Width="0px" />
                            </td>
                            <td align="right" style="width: 1024px">
                                <object id="Lucky" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0">
                                    <param name="allowScriptAccess" value="sameDomain" />
                                    <param name="movie" value="../../images/SIGEV2.swf" />
                                    <param name="loop" value="false" />
                                    <param name="quality" value="high" />
                                    <param name="wmode" value="transparent" />
                                    <param name="bgcolor" value="#ffffff" />
                                    <embed align="middle" allowscriptaccess="sameDomain" bgcolor="#ffffff" loop="false"
                                        name="../../images/SIGEV2.swf" pluginspage="http://www.macromedia.com/go/getflashplayer"
                                        quality="high" src="../../images/SIGEV2.swf" style="height: 88px; width: 283px"
                                        type="application/x-shockwave-flash" wmode="transparent" />
                                </object>
                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td>
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblUpProgressUP" runat="server" Text="." ForeColor="#919FFF"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="PProgresso" runat="server">
                                                <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
                                                    <ProgressTemplate>
                                                        <div style="text-align: center;">
                                                            <img src="../../images/loading1.gif" alt="Procesando" style="width: 90px; height: 13px" />
                                                            <asp:Label ID="lblProgress" runat="server" Text=" Procesando, por favor espere ..."
                                                                ForeColor="White">  </asp:Label>
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                <cc1:TabContainer ID="TabCOperativo" runat="server" ActiveTabIndex="0" AutoPostBack="True"
                                    OnActiveTabChanged="TabCOperativo_ActiveTabChanged">
                                    <cc1:TabPanel ID="TabPAsignacion" runat="server" HeaderText="Asignaciones pendientes">
                                        <HeaderTemplate>
                                            Asignaciones pendientes</HeaderTemplate>
                                        <ContentTemplate>
                                            <br />
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
                                                                    <fieldset id="Fieldset1" runat="server" style="border-color: White; border-width: 1px;
                                                                        background-color: #CCD4E1;">
                                                                        <table align="center">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblAsigPend" runat="server" Text="Asignaciones pendientes" CssClass="labelsN"></asp:Label>
                                                                                    <asp:Label ID="usersession" runat="server" Visible="False"></asp:Label>
                                                                                    <br />
                                                                                    <asp:GridView ID="GVPlanning" runat="server" AllowPaging="True" AutoGenerateSelectButton="True"
                                                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                                                        CellPadding="3" EmptyDataText="Sr. Usuario , actualmente no tiene asignaciones pendientes"
                                                                                        Height="109px" OnPageIndexChanging="GVPlanning_PageIndexChanging" OnSelectedIndexChanged="GVPlanning_SelectedIndexChanged"
                                                                                        Width="1100px" EnableModelValidation="True">
                                                                                        <RowStyle Font-Names="Arial" Font-Size="9px" ForeColor="#000066" />
                                                                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        <FooterStyle BackColor="White" Font-Names="Arial" Font-Size="9px" ForeColor="#000066" />
                                                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Arial" Font-Size="9px"
                                                                                            ForeColor="White" />
                                                                                        <PagerSettings Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                                                                                        <PagerStyle BackColor="White" Font-Names="Arial" Font-Size="9px" ForeColor="#000066"
                                                                                            HorizontalAlign="Left" />
                                                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" Font-Names="Arial" Font-Size="9px"
                                                                                            ForeColor="White" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </fieldset>
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
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPFormatos" runat="server" HeaderText="Formatos">
                                        <HeaderTemplate>
                                            Formatos</HeaderTemplate>
                                        <ContentTemplate>
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
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="LblPlanningForm" runat="server" Text="Planning: "></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="CmbPlanningForm" runat="server" Enabled="False" Width="440px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td>
                                                                    <asp:Label ID="LblPlanningOp" runat="server" Text="Personal Operativo: "></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <div class="ScrollOperativos">
                                                                        <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt"
                                                                            ForeColor="Black" OnCheckedChanged="ChkAll_CheckedChanged" Text="Todos" ToolTip="Seleccione los Operativos a los cuales desea imprimir y/o digitar formatos" />
                                                                        <br />
                                                                        <asp:CheckBoxList ID="LstPlanningOp" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                            RepeatLayout="Flow" ToolTip="Seleccione los Operativos a los cuales desea imprimir formato">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td>
                                                                    <asp:Label ID="LblForm" runat="server" Text="Digitar e imprimir Formatos : "></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImgBtnFormatoPropio" runat="server" AlternateText="Digitar o imprimir formato"
                                                                        BorderStyle="None" Height="36px" ImageUrl="../../images/Digitar.png" onmouseout="this.src = '../../images/Digitar.png'"
                                                                        onmouseover="this.src = '../../images/DigitarDown.png'" Style="margin-left: 0px"
                                                                        ToolTip="Haga Click para digitar o imprimir formato propio" Width="36px" OnClick="ImgBtnFormatoPropio_Click" />
                                                                    <asp:GridView ID="GVFormaPropio" runat="server" AllowPaging="True" BackColor="White"
                                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EmptyDataText="Sr. Usuario , el planning seleccionado no tiene formatos creados"
                                                                        OnPageIndexChanging="GVFormaPropio_PageIndexChanging" PageSize="4" Width="440px">
                                                                        <RowStyle Font-Names="Arial" Font-Size="9px" ForeColor="#000066" />
                                                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <FooterStyle BackColor="White" Font-Names="Arial" Font-Size="9px" ForeColor="#000066" />
                                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Arial" Font-Size="9px"
                                                                            ForeColor="White" />
                                                                        <PagerSettings Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                                                                        <PagerStyle BackColor="White" Font-Names="Arial" Font-Size="9px" ForeColor="#000066"
                                                                            HorizontalAlign="Left" />
                                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" Font-Names="Arial" Font-Size="9px"
                                                                            ForeColor="White" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="LblInfoFotos" runat="server" Text="Informe Fotográfico: "></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImgInfoFotos" runat="server" AlternateText="Cargar fotos" BorderStyle="None"
                                                                        Height="26px" ImageUrl="../../images/CargaFotos.png" OnClick="ImgInfoFotos_Click"
                                                                        onmouseout="this.src = '../../images/CargaFotos.png'" onmouseover="this.src = '../../images/CargaFotosDown.png'"
                                                                        Style="margin-left: 0px" ToolTip="Haga Click para almacenar las fotografias del informe "
                                                                        Width="26px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="LblFormComercio" runat="server" Text="Actividades en el comercio : "></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:GridView ID="GVFormaCompetencia" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                                        BorderStyle="None" BorderWidth="1px" CaptionAlign="Left" CellPadding="3" EmptyDataText="Sr. Usuario , el planning seleccionado no tiene formato de actividades en el comercio"
                                                                        OnSelectedIndexChanged="GVFormaCompetencia_SelectedIndexChanged" PageSize="1"
                                                                        Width="440px">
                                                                        <RowStyle Font-Names="Arial" Font-Size="9px" ForeColor="#000066" />
                                                                        <Columns>
                                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/LupaGif.gif"
                                                                                SelectText="Previsualizar" ShowSelectButton="True" />
                                                                        </Columns>
                                                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <FooterStyle BackColor="White" Font-Names="Arial" Font-Size="9px" ForeColor="#000066" />
                                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Arial" Font-Size="9px"
                                                                            ForeColor="White" />
                                                                        <PagerSettings Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                                                                        <PagerStyle BackColor="White" Font-Names="Arial" Font-Size="9px" ForeColor="#000066"
                                                                            HorizontalAlign="Left" />
                                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" Font-Names="Arial" Font-Size="9px"
                                                                            ForeColor="White" />
                                                                    </asp:GridView>
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
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPinfoFoto" runat="server" HeaderText="Fotografias Actividad">
                                        <HeaderTemplate>
                                            Fotografías Actividad</HeaderTemplate>
                                        <ContentTemplate>
                                            <br />
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
                                                                    <fieldset id="Fieldset2" runat="server" style="border-color: White; border-width: 1px;
                                                                        background-color: #CCD4E1; width: 1113px; height: 500px;">
                                                                        <legend style="color: White; font-weight: bold">Fotográfias</legend>
                                                                        <br />
                                                                        <table align="center">
                                                                            <tr>
                                                                                <td>
                                                                                    <iframe id="ifFotosActividad" runat="server" scrolling="auto" width="1000px" height="400px"
                                                                                        src=""></iframe>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </fieldset>
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
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPPrevCompetencia" runat="server" HeaderText="Actividades en el comercio">
                                        <HeaderTemplate>
                                            Actividades en el comercio</HeaderTemplate>
                                        <ContentTemplate>
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
                                                        <table width="500">
                                                            <tr>
                                                                <td>
                                                                    <div id="print_area">
                                                                        <asp:UpdatePanel ID="upformcompe" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:Label ID="LblformatoActCom" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                                    Font-Size="12pt" Text="Actividades en el Comercio"></asp:Label>
                                                                                <br />
                                                                                <iframe id="IframeCompe" runat="server" src="" style="width: 1310px; height: 1160px"
                                                                                    align="middle"></iframe>
                                                                                <asp:GridView ID="GVFormatoCompetencia" runat="server" Visible="false" BackColor="White"
                                                                                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="2" Font-Names="Arial"
                                                                                    Font-Size="8pt" ForeColor="Black" PageSize="30" Width="1302px">
                                                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                                                                    <PagerStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" HorizontalAlign="Right" />
                                                                                    <RowStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial"
                                                                                        Font-Size="8pt" />
                                                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                                </asp:GridView>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
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
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPDatos" runat="server" HeaderText="Digitación">
                                        <HeaderTemplate>
                                            Digitación</HeaderTemplate>
                                        <ContentTemplate>
                                            <br />
                                            <table align="center" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                                            width="6"></img>
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
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="LblTitFormato" runat="server" CssClass="labelsTitN"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Lblid_planning" runat="server" CssClass="labelsN" Text="Planning:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LblName_planning" runat="server" CssClass="boton"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="LblFechaEjec" runat="server" CssClass="labelsN" Text="Fecha Ejecución:"
                                                                        ToolTip="Ingrese la fecha en la cual se registro el formato en calle"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="TxtFechaEjec" runat="server" Width="70px" AutoPostBack="True" CausesValidation="True"
                                                                                    OnTextChanged="TxtFechaEjec_TextChanged"></asp:TextBox>
                                                                                <cc1:MaskedEditExtender ID="TxtFechaEjec_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechaEjec"
                                                                                    UserDateFormat="DayMonthYear">
                                                                                </cc1:MaskedEditExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImgCalendar" runat="server" Enabled="False" ImageUrl="~/Pages/images/calendario.JPG" />
                                                                                <asp:Label ID="LblObl" runat="server" Text="*" Font-Bold="True" Font-Size="10px"
                                                                                    ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <cc1:CalendarExtender ID="TxtFechaEjec_CalendarExtender" runat="server" Enabled="True"
                                                                        Format="dd/MM/yyyy" PopupButtonID="ImgCalendar" TargetControlID="TxtFechaEjec">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <table align="center" style="border-style: solid; border-width: 1px thin thin 1px;
                                                            border-color: #000000 #C0C0C0 #C0C0C0 #000000;">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Lblcinfo_nameCompany" runat="server" CssClass="labelsN" Text="Empresa:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txtcinfo_nameCompany" runat="server" CssClass="StiloCombo" MaxLength="50"
                                                                                                Width="150px"></asp:TextBox>
                                                                                            <asp:Label ID="LblObl1" runat="server" Text="*" Font-Bold="True" Font-Size="10px"
                                                                                                ForeColor="Red"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RegularExpressionValidator ID="Reqcinfo_nameCompany" runat="server" ControlToValidate="Txtcinfo_nameCompany"
                                                                                                Display="None" ErrorMessage="El Nombre de la empresa no debe empezar por espacio en blanco, y no debe contener caracteres especiales"
                                                                                                ValidationExpression="([a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ]{1,1}[a-zA-Z0-9.,ñÑáéíóúÁÉÍÓÚ&amp;\s]{0,49})"></asp:RegularExpressionValidator>
                                                                                            <cc1:ValidatorCalloutExtender ID="VCReqcinfo_nameCompany" runat="server"
                                                                                                Enabled="True" TargetControlID="Reqcinfo_nameCompany">
                                                                                            </cc1:ValidatorCalloutExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Lblcinfo_Brand" runat="server" CssClass="labelsN" Text="Marca:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txtcinfo_Brand" runat="server" CssClass="StiloCombo" MaxLength="50"
                                                                                                Width="150px"></asp:TextBox>
                                                                                            <asp:Label ID="LblObl4" runat="server" Text="*" Font-Bold="True" Font-Size="10px"
                                                                                                ForeColor="Red"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RegularExpressionValidator ID="Reqcinfo_Brand" runat="server" ControlToValidate="Txtcinfo_Brand"
                                                                                                Display="None" ErrorMessage="La Marca no debe empezar por espacio en blanco ni valor numérico, y no debe contener caracteres especiales"
                                                                                                ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s]{0,49})"></asp:RegularExpressionValidator>
                                                                                            <cc1:ValidatorCalloutExtender ID="VCReqcinfo_Brand" runat="server" Enabled="True"
                                                                                                TargetControlID="Reqcinfo_Brand">
                                                                                            </cc1:ValidatorCalloutExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Lblcinfo_CodStrategy" runat="server" CssClass="labelsN" Text="Servicio:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="Cmbcinfo_CodStrategy" runat="server" CssClass="StiloCombo"
                                                                                                Width="250px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="LblObl5" runat="server" Text="*" Font-Bold="True" Font-Size="10px"
                                                                                                ForeColor="Red"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Lblcinfo_Name_Activity" runat="server" CssClass="labelsN" Text="Nombre Actividad:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txtcinfo_Name_Activity" runat="server" CssClass="StiloCombo" MaxLength="100"
                                                                                                Width="150px"></asp:TextBox>
                                                                                            <asp:Label ID="LblObl6" runat="server" Text="*" Font-Bold="True" Font-Size="10px"
                                                                                                ForeColor="Red"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RegularExpressionValidator ID="Reqcinfo_Name_Activity" runat="server" ControlToValidate="Txtcinfo_Name_Activity"
                                                                                                Display="None" ErrorMessage="La Actividad no debe empezar por espacio en blanco ni valor numérico, y no debe contener caracteres especiales"
                                                                                                ValidationExpression="([a-zA-Z][a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s]{0,99})"></asp:RegularExpressionValidator>
                                                                                            <cc1:ValidatorCalloutExtender ID="VCReqcinfo_Name_Activity" runat="server" Enabled="True"
                                                                                                TargetControlID="Reqcinfo_Name_Activity">
                                                                                            </cc1:ValidatorCalloutExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Lblcinfo_Cod_Channel" runat="server" CssClass="labelsN" Text="Grupo Objetivo:"
                                                                                    ToolTip="Canal en el cual se esta ejecutando la actividad"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="Cmbcinfo_Cod_Channel" runat="server" CssClass="StiloCombo"
                                                                                                Width="250px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Lblcinfo_FeciniPromo" runat="server" CssClass="labelsN" Text="Fecha inicio:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txtcinfo_FeciniPromo" runat="server" CssClass="StiloCombo" Width="130px"
                                                                                                AutoPostBack="True" CausesValidation="True" OnTextChanged="Txtcinfo_FeciniPromo_TextChanged"></asp:TextBox>
                                                                                            <cc1:MaskedEditExtender ID="Txtcinfo_FeciniPromo_MaskedEditExtender" runat="server"
                                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txtcinfo_FeciniPromo">
                                                                                            </cc1:MaskedEditExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImgCalendarFI" runat="server" Enabled="False" ImageUrl="~/Pages/images/calendario.JPG" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <cc1:CalendarExtender ID="Txtcinfo_FeciniPromo_CalendarExtender" runat="server" Enabled="True"
                                                                                    Format="dd/MM/yyyy" PopupButtonID="ImgCalendarFI" TargetControlID="Txtcinfo_FeciniPromo">
                                                                                </cc1:CalendarExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Lblcinfo_FecfinPromo" runat="server" CssClass="labelsN" Text="Fecha fin:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txtcinfo_FecfinPromo" runat="server" CssClass="StiloCombo" Width="130px"
                                                                                                AutoPostBack="True" CausesValidation="True" OnTextChanged="Txtcinfo_FecfinPromo_TextChanged"></asp:TextBox>
                                                                                            <cc1:MaskedEditExtender ID="Txtcinfo_FecfinPromo_MaskedEditExtender" runat="server"
                                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txtcinfo_FecfinPromo">
                                                                                            </cc1:MaskedEditExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImgCalendarFF" runat="server" Enabled="False" ImageUrl="~/Pages/images/calendario.JPG" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <cc1:CalendarExtender ID="Txtcinfo_FecfinPromo_CalendarExtender" runat="server" Enabled="True"
                                                                                    Format="dd/MM/yyyy" PopupButtonID="ImgCalendarFF" TargetControlID="Txtcinfo_FecfinPromo">
                                                                                </cc1:CalendarExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Lblcinfo_Vigente" runat="server" CssClass="labelsN" Text="Vigente:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:RadioButtonList ID="RBtncinfo_Vigente" runat="server" CssClass="StiloCombo"
                                                                                    RepeatDirection="Horizontal">
                                                                                    <asp:ListItem>Si</asp:ListItem>
                                                                                    <asp:ListItem>No</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Lblcinfo_PersonnelCantid" runat="server" CssClass="labelsN" Text="Cantidad de personal:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txtcinfo_PersonnelCantid" runat="server" CssClass="StiloCombo" MaxLength="4"
                                                                                                Width="130px"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RegularExpressionValidator ID="Reqcinfo_PersonnelCantid" runat="server" ControlToValidate="Txtcinfo_PersonnelCantid"
                                                                                                Display="None" ErrorMessage="La cantidad de personal debe ser un valor numérico de máximo cuatro dígitos"
                                                                                                ValidationExpression="([0-9]{1,4})"></asp:RegularExpressionValidator>
                                                                                            <cc1:ValidatorCalloutExtender ID="VCReqcinfo_PersonnelCantid" runat="server" Enabled="True"
                                                                                                TargetControlID="Reqcinfo_PersonnelCantid">
                                                                                            </cc1:ValidatorCalloutExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr valign="top">
                                                                            <td rowspan="3">
                                                                                <asp:Label ID="Lblcinfo_Mecanica" runat="server" CssClass="labelsN" Text="Mecánica:"></asp:Label>
                                                                            </td>
                                                                            <td rowspan="3">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txtcinfo_Mecanica" runat="server" CssClass="StiloCombo" MaxLength="255"
                                                                                                Width="306px" Height="53px" TextMode="MultiLine"></asp:TextBox>
                                                                                            <asp:Label ID="LblObl2" runat="server" Text="*" Font-Bold="True" Font-Size="10px"
                                                                                                ForeColor="Red"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Lblcinfo_id_ProductCategory" runat="server" CssClass="labelsN" Text="Categoría:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="Cmbcinfo_id_ProductCategory" runat="server" CssClass="StiloCombo"
                                                                                                Width="135px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr valign="top">
                                                                            <td>
                                                                                <asp:Label ID="Lblcinfo_id_MPointOfPurchase" runat="server" CssClass="labelsN" Text="Material POP:"></asp:Label>
                                                                            </td>
                                                                            <td rowspan="3">
                                                                                <div class="StiloComboScrool">
                                                                                    <asp:CheckBoxList ID="ChkCinfo_id_MPointOfPurchase" runat="server" CssClass="StiloCombo"
                                                                                        Enabled="False">
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td rowspan="2">
                                                                            </td>
                                                                        </tr>
                                                                        <tr valign="top">
                                                                            <td>
                                                                                <asp:Label ID="Lblcinfo_Comment_Observa" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                                    Font-Size="10pt" ForeColor="Black" Text="Impacto en Ventas / Comentarios / Observaciones"
                                                                                    Width="189px"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txtcinfo_Comment_Observa" runat="server" CssClass="StiloCombo" EnableTheming="True"
                                                                                                Height="53px" MaxLength="255" TextMode="MultiLine" Width="306px"></asp:TextBox>
                                                                                            <asp:Label ID="LblObl3" runat="server" Text="*" Font-Bold="True" Font-Size="10px"
                                                                                                ForeColor="Red"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="StatusActCom" runat="server" Text="Estado" Font-Bold="True" Font-Names="Arial"
                                                                                    Font-Size="10pt" ForeColor="Black"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:RadioButtonList ID="RbtnStatusActCom" runat="server" CssClass="StiloCombo" RepeatDirection="Horizontal"
                                                                                    Enabled="False">
                                                                                    <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                                                                    <asp:ListItem>Deshabilitado</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <br />
                                                                </td>
                                                                <td id="tdfotos" runat="server" style="border: 1px solid #000000; display: none;"
                                                                    valign="top">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="LblCargaFotosCom" runat="server" CssClass="labelsN" Text="Registros Fotográficos ."></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImgBtnFileUpload" runat="server" AlternateText="Cargar fotos"
                                                                                                BorderStyle="None" Height="26px" ImageUrl="../../images/CargaFotos.png" OnClick="ImageButton1_Click"
                                                                                                onmouseout="this.src = '../../images/CargaFotos.png'" onmouseover="this.src = '../../images/CargaFotosDown.png'"
                                                                                                Style="margin-left: 0px" ToolTip="Haga Click para ver las fotografias que ha cargado para esta actividad"
                                                                                                Width="26px" />
                                                                                            <asp:ImageButton ID="ImgBtnFileUploadSearch" runat="server" AlternateText="Cargar fotos"
                                                                                                BorderStyle="None" Height="26px" ImageUrl="../../images/CargaFotos.png" OnClick="ImageButtonSearch_Click"
                                                                                                onmouseout="this.src = '../../images/CargaFotos.png'" onmouseover="this.src = '../../images/CargaFotosDown.png'"
                                                                                                Style="margin-left: 0px" ToolTip="Haga Click para ver las fotografías de esta consulta"
                                                                                                Width="26px" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <iframe id="ifcarga" runat="server" height="120px" scrolling="auto" src="" width="260px">
                                                                                </iframe>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <div class="ScrollFotos">
                                                                                    <asp:GridView ID="GVFotografias" runat="server" AutoGenerateColumns="False" PageSize="1">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Fotografías">
                                                                                                <ItemTemplate>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td valign="top">
                                                                                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Image ID="Image1" runat="server" Height="152" Width="206px" CssClass="imagenstrech"
                                                                                                                    ImageAlign="Middle" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:ImageButton ID="BtnImgTerminarCarga" runat="server" AlternateText="Terminar Carga fotográfica"
                                                                                    BorderStyle="None" ImageUrl="~/Pages/images/BtnFinCargaFoto.png" onmouseout="this.src = '../../images/BtnFinCargaFoto.png'"
                                                                                    onmouseover="this.src = '../../images/BtnFinCargaFotoDown.png'" OnClick="BtnImgTerminarCarga_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="IbtnCrearOpe" runat="server" AlternateText="Nuevo registro"
                                                                        BorderStyle="None" ImageUrl="~/Pages/images/BtnNewReg.png" onmouseout="this.src = '../../images/BtnNewReg.png'"
                                                                        onmouseover="this.src = '../../images/BtnNewRegDown.png'" Style="margin-left: 0px"
                                                                        OnClick="IbtnCrearOpe_Click" />
                                                                    <asp:ImageButton ID="IbtnSaveOpe" runat="server" AlternateText="Guardar registro"
                                                                        BorderStyle="None" ImageUrl="../../images/BtnSaveReg.png" OnClick="IbtnSaveOpe_Click"
                                                                        onmouseout="this.src = '../../images/BtnSaveReg.png'" onmouseover="this.src = '../../images/BtnSaveRegDown.png'"
                                                                        Style="margin-left: 0px" Visible="False" />
                                                                    <asp:ImageButton ID="IbtnSearchOpe" runat="server" AlternateText="Consultar registro"
                                                                        BorderStyle="None" ImageUrl="~/Pages/images/BtnSearchReg.png" onmouseout="this.src = '../../images/BtnSearchReg.png'"
                                                                        onmouseover="this.src = '../../images/BtnSearchDownReg.png'" Style="margin-left: 0px" />
                                                                    <asp:ImageButton ID="IbtnEditOpe" runat="server" AlternateText="Editar registro"
                                                                        BorderStyle="None" ImageUrl="~/Pages/images/BtnEditReg.png" onmouseout="this.src = '../../images/BtnEditReg.png'"
                                                                        onmouseover="this.src = '../../images/BtnEditRegDown.png'" Style="margin-left: 0px"
                                                                        Visible="False" OnClick="IbtnEditOpe_Click" />
                                                                    <asp:ImageButton ID="IbtnActualizaOpe" runat="server" AlternateText="Actualizar registro"
                                                                        BorderStyle="None" ImageUrl="~/Pages/images/BtnUpdateReg.png" onmouseout="this.src = '../../images/BtnUpdateReg.png'"
                                                                        onmouseover="this.src = '../../images/BtnUpdateRegDown.png'" Style="margin-left: 0px"
                                                                        Visible="False" OnClick="IbtnActualizaOpe_Click" />
                                                                    <asp:ImageButton ID="IbtnCancelReg" runat="server" AlternateText="Cancelar" BorderStyle="None"
                                                                        ImageUrl="../../images/BtnCancelReg.png" OnClick="IbtnCancelReg_Click" onmouseout="this.src = '../../images/BtnCancelReg.png'"
                                                                        onmouseover="this.src = '../../images/BtncancelRegDown.png'" Style="margin-left: 0px" />
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
                                            <cc1:ModalPopupExtender ID="ModalPopupBuscarActCom" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="True" Enabled="True" OkControlID="ImgCloseSearchActCom" PopupControlID="BuscarActCom"
                                                TargetControlID="IbtnSearchOpe" DynamicServicePath="">
                                            </cc1:ModalPopupExtender>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPPrevPropios" runat="server" HeaderText="Actividades Propias">
                                        <HeaderTemplate>
                                            Actividades Propias</HeaderTemplate>
                                        <ContentTemplate>
                                            <table id="formato" runat="server" align="center" border="0" cellpadding="0" cellspacing="0">
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png"
                                                            width="6"> </img>
                                                    </td>
                                                    <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg" runat="server">
                                                        <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1">
                                                        </img>
                                                    </td>
                                                    <td runat="server">
                                                        <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png"
                                                            width="6"> </img>
                                                    </td>
                                                </tr>
                                                <tr runat="server">
                                                    <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg" runat="server">
                                                        <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                                        </img>
                                                    </td>
                                                    <td valign="top" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdatePanel ID="uppanelexcel" runat="server">
                                                                        <ContentTemplate>
                                                                            <div align="right">
                                                                                <asp:ImageButton ID="ImgExpExcel" runat="server" Height="26px" ImageUrl="~/Pages/images/imgExpExcel.png"
                                                                                    Width="26px" OnClick="ImgExpExcel_Click" />
                                                                                <asp:LinkButton ID="LkbPuntosDeVenta" runat="server" OnClick="LkbPuntosDeVenta_Click"
                                                                                    Visible="false">Puntos de Venta...                                                                                          
                                                                                </asp:LinkButton>
                                                                                <asp:LinkButton ID="LkbProductos" runat="server" OnClick="LkbProductos_Click" Visible="false">Productos...                                                                                          
                                                                                </asp:LinkButton>
                                                                                <asp:LinkButton ID="LkbMarcas" runat="server" OnClick="LkbMarcas_Click" Visible="false">Marcas...
                                                                                </asp:LinkButton>
                                                                                <asp:LinkButton ID="LkbEncabezado" runat="server" OnClick="LkbEncabezado_Click" Visible="false">Encabezado...
                                                                                </asp:LinkButton>
                                                                                <asp:LinkButton ID="LkbPie" runat="server" OnClick="LkbPie_Click" Visible="false">Pie...
                                                                                </asp:LinkButton>
                                                                                <%--<a id="Downloadpdv" runat="server" visible="false" href="javascript:descargar('PictureComercio/PDV.csv')" >Puntos de Venta</a> 
                                                                                    <a id="Downloadpro" runat="server" visible="false" href="javascript:descargar('PictureComercio/Product.csv')" >Productos</a> 
                                                                                    <a id="Downloadmar" runat="server" visible="false" href="javascript:descargar('PictureComercio/Marca.csv')" >Marcas</a> 
                                                                                    <a id="Downloadenc" runat="server" visible="false" href="javascript:descargar('PictureComercio/encabezado.csv')" >Encabezado</a> 
                                                                                    <a id="Downloadpie" runat="server" visible="false" href="javascript:descargar('PictureComercio/pie.csv')" >Pie</a> --%>
                                                                                <a id="OpenExcel" runat="server" visible="false" href="javascript:descargar('PictureComercio/OpenExcel.exe')">
                                                                                    OpenExcel</a>
                                                                            </div>
                                                                            <div id="print_area2" runat="server">
                                                                                <asp:Panel ID="GridViewPlaceHolder" runat="server">
                                                                                    <cc1:TabContainer ID="TabContenedor" runat="server" ActiveTabIndex="0">
                                                                                        <cc1:TabPanel ID="TabPersonal" runat="server">
                                                                                            <HeaderTemplate>
                                                                                                Personal Operativo</HeaderTemplate>
                                                                                            <ContentTemplate>
                                                                                                <asp:GridView ID="GvPersonalExcel" runat="server" BackColor="White" BorderColor="#336666"
                                                                                                    BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal"
                                                                                                    EmptyDataText="No fue seleccionado ningún operativo para construir los formatos"
                                                                                                    Width="700px">
                                                                                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                                                                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                                                                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                                                </asp:GridView>
                                                                                            </ContentTemplate>
                                                                                        </cc1:TabPanel>
                                                                                        <cc1:TabPanel ID="TabPuntosVenta" runat="server">
                                                                                            <HeaderTemplate>
                                                                                                Relación Puntos de Venta</HeaderTemplate>
                                                                                            <ContentTemplate>
                                                                                                <asp:GridView ID="GvPDVVsPersonalExcel" runat="server" BackColor="White" BorderColor="#336666"
                                                                                                    BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal">
                                                                                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                                                                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                                                                                </asp:GridView>
                                                                                            </ContentTemplate>
                                                                                        </cc1:TabPanel>
                                                                                        <cc1:TabPanel ID="TabProducAsociados" runat="server">
                                                                                            <HeaderTemplate>
                                                                                                Relación de Productos</HeaderTemplate>
                                                                                            <ContentTemplate>
                                                                                                <asp:GridView ID="GvProductoXPDVVsPersonalExcel" runat="server" BackColor="White"
                                                                                                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                                                                                                    GridLines="Horizontal">
                                                                                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                                                                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                                                                                </asp:GridView>
                                                                                            </ContentTemplate>
                                                                                        </cc1:TabPanel>
                                                                                        <cc1:TabPanel ID="TabProducCompeAsociados" runat="server">
                                                                                            <HeaderTemplate>
                                                                                                Relación de Productos de la competencia</HeaderTemplate>
                                                                                            <ContentTemplate>
                                                                                                <asp:GridView ID="GvProductoCompeExcel" runat="server" BackColor="White" BorderColor="#336666"
                                                                                                    BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal">
                                                                                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                                                                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                                                                                </asp:GridView>
                                                                                            </ContentTemplate>
                                                                                        </cc1:TabPanel>
                                                                                        <cc1:TabPanel ID="TabContenidoFormato" runat="server">
                                                                                            <HeaderTemplate>
                                                                                                Encabezado y Pie del formato</HeaderTemplate>
                                                                                            <ContentTemplate>
                                                                                                <asp:Panel ID="PanelDinamico" runat="server">
                                                                                                    <asp:Label ID="LbltitFormatoExcel" runat="server" Text="Label" Font-Bold="True"> </asp:Label><br />
                                                                                                    <asp:Label ID="LblCampañaExcel" runat="server" Text="Label" Font-Bold="True"> </asp:Label><br />
                                                                                                    <br />
                                                                                                    <asp:GridView ID="GvFormatoEncExcel" runat="server" BackColor="White" BorderColor="#336666"
                                                                                                        BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal"
                                                                                                        Width="700px">
                                                                                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                                                                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                                                                                    </asp:GridView>
                                                                                                    <asp:GridView ID="GvFormatoPieExcel" runat="server" BackColor="White" BorderColor="#336666"
                                                                                                        BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal"
                                                                                                        Width="700px">
                                                                                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                                                                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                                                                                    </asp:GridView>
                                                                                                </asp:Panel>
                                                                                            </ContentTemplate>
                                                                                        </cc1:TabPanel>
                                                                                    </cc1:TabContainer></asp:Panel>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg" runat="server">
                                                        <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6">
                                                        </img>
                                                    </td>
                                                </tr>
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                                            width="6"> </img>
                                                    </td>
                                                    <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg"
                                                        runat="server">
                                                        <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png"
                                                            width="1"> </img>
                                                    </td>
                                                    <td runat="server">
                                                        <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png"
                                                            width="6"> </img>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPDatosPropios" runat="server" HeaderText="Digitación Actividades Propias">
                                        <HeaderTemplate>
                                            Digitación Actividades Propias</HeaderTemplate>
                                        <ContentTemplate>
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LbltitDigita" runat="server" CssClass="labelsTitN"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <fieldset>
                                                <table align="center">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblOperaDigita" runat="server" Text="Operativo" CssClass="labelsN"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="CmbOperaDigita" runat="server" Width="350px" CssClass="StiloCombo"
                                                                AutoPostBack="True" OnSelectedIndexChanged="CmbOperaDigita_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="OblOperaDigita" runat="server" Text="*" Font-Bold="True" Font-Size="10px"
                                                                ForeColor="Red"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblFecOperaDigita" runat="server" CssClass="labelsN" Text="Fecha Ejecución:"
                                                                ToolTip="Ingrese la fecha en la cual se registro el formato en calle"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtFecOperaDigita" runat="server" Width="70px" AutoPostBack="True"
                                                                            CausesValidation="True" OnTextChanged="TxtFecOperaDigita_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImgBtnFecOperaDigita" runat="server" Enabled="False" ImageUrl="~/Pages/images/calendario.JPG" />
                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFecOperaDigita"
                                                                            UserDateFormat="DayMonthYear">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblOblFecOpera" runat="server" Text="*" Font-Bold="True" Font-Size="10px"
                                                                            ForeColor="Red"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                PopupButtonID="ImgBtnFecOperaDigita" TargetControlID="TxtFecOperaDigita">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblPdvDigita" runat="server" Text="Punto de venta" CssClass="labelsN"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="CmbPdvDigita" runat="server" Width="350px" CssClass="StiloCombo"
                                                                OnSelectedIndexChanged="CmbPdvDigita_SelectedIndexChanged" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="OblPdvDigita" runat="server" Text="*" Font-Bold="True" Font-Size="10px"
                                                                ForeColor="Red"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="StatusActPropia" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                Font-Size="10pt" ForeColor="Black" Text="Estado"></asp:Label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <asp:RadioButtonList ID="RbtnStatusActPropia" runat="server" CssClass="StiloCombo"
                                                                Enabled="False">
                                                                <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                                                <asp:ListItem>Deshabilitado</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblProdDigita" runat="server" CssClass="labelsN" Text="Producto"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="CmbProdDigita" runat="server" AutoPostBack="True" CssClass="StiloCombo"
                                                                OnSelectedIndexChanged="CmbProdDigita_SelectedIndexChanged" Width="350px">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="OblProdDigita" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="Red"
                                                                Text="*"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="IbtnCrearCaptura" runat="server" AlternateText="Nuevo registro"
                                                            BorderStyle="None" ImageUrl="~/Pages/images/BtnNewReg.png" onmouseout="this.src = '../../images/BtnNewReg.png'"
                                                            onmouseover="this.src = '../../images/BtnNewRegDown.png'" />
                                                        <asp:ImageButton ID="IbtnSaveCaptura" runat="server" AlternateText="Guardar registro"
                                                            BorderStyle="None" ImageUrl="../../images/BtnSaveReg.png" onmouseout="this.src = '../../images/BtnSaveReg.png'"
                                                            onmouseover="this.src = '../../images/BtnSaveRegDown.png'" Visible="False" OnClick="IbtnSaveCaptura_Click" />
                                                        <asp:ImageButton ID="IbtnSearchCaptura" runat="server" AlternateText="Consultar registro"
                                                            BorderStyle="None" ImageUrl="~/Pages/images/BtnSearchReg.png" onmouseout="this.src = '../../images/BtnSearchReg.png'"
                                                            onmouseover="this.src = '../../images/BtnSearchDownReg.png'" />
                                                        <asp:ImageButton ID="IbtnEditCaptura" runat="server" AlternateText="Editar registro"
                                                            BorderStyle="None" ImageUrl="~/Pages/images/BtnEditReg.png" onmouseout="this.src = '../../images/BtnEditReg.png'"
                                                            onmouseover="this.src = '../../images/BtnEditRegDown.png'" Visible="False" OnClick="IbtnEditCaptura_Click" />
                                                        <asp:ImageButton ID="IbtnActualizaCaptura" runat="server" AlternateText="Actualizar registro"
                                                            BorderStyle="None" ImageUrl="~/Pages/images/BtnUpdateReg.png" onmouseout="this.src = '../../images/BtnUpdateReg.png'"
                                                            onmouseover="this.src = '../../images/BtnUpdateRegDown.png'" Visible="False"
                                                            OnClick="IbtnActualizaCaptura_Click" />
                                                        <asp:ImageButton ID="IbtnCancelCaptura" runat="server" AlternateText="Cancelar" BorderStyle="None"
                                                            ImageUrl="../../images/BtnCancelReg.png" onmouseout="this.src = '../../images/BtnCancelReg.png'"
                                                            onmouseover="this.src = '../../images/BtncancelRegDown.png'" OnClick="IbtnCancelCaptura_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Panel ID="PanelTipoDigitacion" runat="server" Style="display: none;" CssClass="CargaArchivos"
                                                Height="225px" Width="316px">
                                                <table align="center">
                                                    <tr align="center">
                                                        <td>
                                                            <asp:Label ID="LblSelTipDigitacion" runat="server" Text="Seleccione informes a digitar"
                                                                CssClass="labelsTitN"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table align="center">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBoxList ID="ChkTipDigitacion" runat="server" Font-Names="Arial" Font-Size="10pt"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ChkTipDigitacion_SelectedIndexChanged">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table align="center">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="BtnContinuarTip" runat="server" Text="Continuar" CssClass="button"
                                                                OnClick="BtnContinuarTip_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="BtnCancelTip" runat="server" Text="Cancelar" CssClass="button" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <cc1:ModalPopupExtender ID="ModalPopupTipDigitacion" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="True" Enabled="True" OkControlID="BtnCancelTip" PopupControlID="PanelTipoDigitacion"
                                                TargetControlID="IbtnCrearCaptura" DynamicServicePath="">
                                            </cc1:ModalPopupExtender>
                                            <asp:Panel ID="Paneldigitacion" runat="server" Style="display: none;">
                                                <cc1:TabContainer ID="TabDigitar" runat="server" Width="1190px" ActiveTabIndex="0">
                                                    <cc1:TabPanel ID="TabGeneral" runat="server">
                                                        <HeaderTemplate>
                                                            Información General</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GvAlmacenPlanning" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False">
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Información solicitada (*)">
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCaptura" runat="server" Text="Item de Formato" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCaptura" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqTxtCaptura" runat="server" ControlToValidate="TxtCaptura"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})"> 
                                                                                                    </asp:RegularExpressionValidator>
                                                                                                    <cc1:ValidatorCalloutExtender ID="VCEReqTxtCaptura" runat="server" Enabled="True"
                                                                                                        TargetControlID="ReqTxtCaptura">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                    <cc1:TabPanel ID="TabVentas" runat="server">
                                                        <HeaderTemplate>
                                                            Ventas</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GvDatosIndicadorVentas" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})">
                                                                                                    </asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador"
                                                                                                        runat="server" Enabled="True" TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                        <br />
                                                                        <asp:GridView ID="GvVentasCompe1" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})">
                                                                                                    </asp:RegularExpressionValidator>
                                                                                                    <cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador" runat="server" Enabled="True"
                                                                                                        TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                        <br />
                                                                        <asp:GridView ID="GvVentasCompe2" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})">
                                                                                                    </asp:RegularExpressionValidator>
                                                                                                    <cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador" runat="server" Enabled="True"
                                                                                                        TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                        <br />
                                                                        <asp:GridView ID="GvVentasCompe3" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})">
                                                                                                    </asp:RegularExpressionValidator>
                                                                                                    <cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador" runat="server" Enabled="True"
                                                                                                        TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                    <cc1:TabPanel ID="TabPrecios" runat="server">
                                                        <HeaderTemplate>
                                                            Precios</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GvDatosIndicadorPrecios" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})">
                                                                                                    </asp:RegularExpressionValidator>
                                                                                                    <cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador" runat="server" Enabled="True"
                                                                                                        TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                        <br />
                                                                        <asp:GridView ID="GvPreciosCompe1" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})">
                                                                                                    </asp:RegularExpressionValidator>
                                                                                                    <cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador" runat="server" Enabled="True"
                                                                                                        TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                        <br />
                                                                        <asp:GridView ID="GvPreciosCompe2" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})">
                                                                                                    </asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador"
                                                                                                        runat="server" Enabled="True" TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                        <br />
                                                                        <asp:GridView ID="GvPreciosCompe3" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})">
                                                                                                    </asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador"
                                                                                                        runat="server" Enabled="True" TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                    <cc1:TabPanel ID="TabCobertura" runat="server">
                                                        <HeaderTemplate>
                                                            Disponibilidad</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GvDatosIndicadorCobertura" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})"> </asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                                                            ID="VCEReqCapturaIndicador" runat="server" Enabled="True" TargetControlID="ReqCapturaIndicador">
                                                                                                        </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                        <br />
                                                                        <asp:GridView ID="GvCoberturaCompe1" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox><asp:RegularExpressionValidator
                                                                                                        ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})"> </asp:RegularExpressionValidator>
                                                                                                    <cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador" runat="server" Enabled="True"
                                                                                                        TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                        <br />
                                                                        <asp:GridView ID="GvCoberturaCompe2" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox><asp:RegularExpressionValidator
                                                                                                        ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})"> </asp:RegularExpressionValidator>
                                                                                                    <cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador" runat="server" Enabled="True"
                                                                                                        TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                        <br />
                                                                        <asp:GridView ID="GvCoberturaCompe3" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})"> </asp:RegularExpressionValidator>
                                                                                                    <cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador" runat="server" Enabled="True"
                                                                                                        TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                    <cc1:TabPanel ID="TabSOD" runat="server">
                                                        <HeaderTemplate>
                                                            SOD</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <table align="center">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblMarcaProdSel" runat="server" CssClass="labelsN" Visible="false"
                                                                                        Text="Marca Producto :"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblTxtMarcaProdSel" runat="server" CssClass="labelsN" Visible="false"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <asp:GridView ID="GvDatosIndicadorSOD" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN">
                                                                                                    </asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})"> </asp:RegularExpressionValidator>
                                                                                                    <cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador" runat="server" Enabled="True"
                                                                                                        TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                        <br />
                                                                        <table align="center">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblMarcaProdCompeSel" runat="server" CssClass="labelsN" Visible="false"
                                                                                        Text="Marca Producto Competidor :"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblTxtMarcaProdCompeSel" runat="server" CssClass="labelsN" Visible="false"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <asp:GridView ID="GvSODCompe1" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})"> </asp:RegularExpressionValidator>
                                                                                                    <cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador" runat="server" Enabled="True"
                                                                                                        TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                        <br />
                                                                        <table align="center">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblMarcaProdCompeSel1" runat="server" CssClass="labelsN" Visible="false"
                                                                                        Text="Marca Producto Competidor :"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblTxtMarcaProdCompeSel1" runat="server" CssClass="labelsN" Visible="false"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <asp:GridView ID="GvSODCompe2" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})"> 
                                                                                                    </asp:RegularExpressionValidator>
                                                                                                    <cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador" runat="server" Enabled="True"
                                                                                                        TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                        <br />
                                                                        <table align="center">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblMarcaProdCompeSel2" runat="server" CssClass="labelsN" Visible="false"
                                                                                        Text="Marca Producto Competidor :"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblTxtMarcaProdCompeSel2" runat="server" CssClass="labelsN" Visible="false"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <asp:GridView ID="GvSODCompe3" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                                            Enabled="False" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td width="300">
                                                                                                    <asp:Label ID="LblCapturaIndicador" runat="server" Text="Item de Indicador" CssClass="labelsN"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtCapturaIndicador" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="ReqCapturaIndicador" runat="server" ControlToValidate="TxtCapturaIndicador"
                                                                                                        Display="none" ErrorMessage="Sr. Usuario , no exceda 255 caracteres" ValidationExpression="([\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,255})"> </asp:RegularExpressionValidator>
                                                                                                    <cc1:ValidatorCalloutExtender ID="VCEReqCapturaIndicador" runat="server" Enabled="True"
                                                                                                        TargetControlID="ReqCapturaIndicador">
                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                </cc1:TabContainer>
                                            </asp:Panel>
                                            <cc1:ModalPopupExtender ID="ModalPopupBuscarActPropia" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="True" Enabled="True" OkControlID="ImgCloseSearchActPropia" PopupControlID="BuscarActLevPropia"
                                                TargetControlID="IbtnSearchCaptura" DynamicServicePath="">
                                            </cc1:ModalPopupExtender>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPEMail" runat="server" HeaderText="Novedades Solicitudes">
                                        <HeaderTemplate>
                                            Novedades / Solicitudes</HeaderTemplate>
                                        <ContentTemplate>
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
                                                        <asp:Panel ID="PSolicitudes" runat="server" Width="593px" Height="397px">
                                                            <br />
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Image ID="ImgMail" runat="server" Height="62px" ImageUrl="~/Pages/images/mailreminder.png"
                                                                            Width="65px" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="LblTitEnvioMail" runat="server" CssClass="labelsTit" Text="Novedades / Solicitudes"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table align="center">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="LblMailSolicitante" runat="server" CssClass="labels" Text="De"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtSolicitante" runat="server" Enabled="False" Width="400px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="LblMailPara" runat="server" CssClass="labels" Text="Para"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtEmail" runat="server" Enabled="False" Width="400px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="LblMotivo" runat="server" CssClass="labels" Text="Asunto"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtMotivo" runat="server" MaxLength="50" Width="400px"></asp:TextBox><asp:RegularExpressionValidator
                                                                            ID="Reqmotivo" runat="server" ControlToValidate="TxtMotivo" Display="None" ErrorMessage="Sr. Usuario, por favor no ingrese caracteres especiales y recuerde que no debe inicial con número"
                                                                            ValidationExpression="([a-zA-Z][a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]{0,49})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                                ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="Reqmotivo">
                                                                            </cc1:ValidatorCalloutExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="LblMensaje" runat="server" CssClass="labels" Text="Mensaje"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtMensaje" runat="server" Height="113px" TextMode="MultiLine" Width="400px"></asp:TextBox><asp:RegularExpressionValidator
                                                                            ID="ReqMensaje" runat="server" ControlToValidate="TxtMensaje" Display="None"
                                                                            ErrorMessage="Sr. Usuario, recuerde que no debe inicial con número. Máximo ingrese 255 caracteres"
                                                                            ValidationExpression="([a-zA-Z][\W\wa-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]{0,255})"></asp:RegularExpressionValidator><cc1:ValidatorCalloutExtender
                                                                                ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="ReqMensaje">
                                                                            </cc1:ValidatorCalloutExtender>
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
                                                        </asp:Panel>
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
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="sesion" runat="server" HeaderText="Cerrar Sesión">
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="Alertas" runat="server" DefaultButton="BtnAceptarAlert" Height="169px"
                        Style="display: none;" Width="332px">
                        <table align="center">
                            <tr>
                                <td align="center" class="style50" valign="top">
                                    <br />
                                </td>
                                <td class="style49" valign="top">
                                    <br />
                                    <asp:Label ID="LblAlert" runat="server" CssClass="labels" Font-Bold="True"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="LblFaltantes" runat="server" CssClass="labels"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table align="center">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="BtnAceptarAlert" runat="server" CssClass="button" Text="Aceptar" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalPopupAlertas" runat="server" BackgroundCssClass="modalBackground"
                        DropShadow="True" Enabled="True" OkControlID="BtnAceptarAlert" PopupControlID="Alertas"
                        TargetControlID="Btndisparaalertas">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="AlertFotos" runat="server" CssClass="MensajesFotosConfirm" Height="169px"
                        Style="display: none;" Width="332px">
                        <table align="center">
                            <tr>
                                <td align="center" class="style50" valign="top">
                                    <br />
                                </td>
                                <td class="style49" valign="top">
                                    <br />
                                    <asp:Label ID="Label1" runat="server" CssClass="labelsTit" Text="Requerimiento Fotográfico"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label2" runat="server" CssClass="labels" Text="Sr. Usuario, desea registrar fotográficas a esta actividad ?"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table align="center">
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="ImgBtnSI" runat="server" ImageUrl="~/Pages/images/BtnSI.png"
                                        OnClick="ImgBtnSI_Click" onmouseout="this.src = '../../images/BtnSI.png'" onmouseover="this.src = '../../images/BtnSIDown.png'" />
                                    <asp:ImageButton ID="ImgBtnNO" runat="server" ImageUrl="~/Pages/images/BtnNO.png"
                                        OnClick="ImgBtnNO_Click" onmouseout="this.src = '../../images/BtnNO.png'" onmouseover="this.src = '../../images/BtnNODown.png'" />
                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalPopupFotos" runat="server" BackgroundCssClass="modalBackground"
                        DropShadow="True" Enabled="True" OkControlID="Label3" PopupControlID="AlertFotos"
                        TargetControlID="BtndisparaalertasFotos">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="BuscarActCom" runat="server" CssClass="CargaArchivos" Width="750px"
                        Height="400px" Style="display: none;">
                        <div dir="rtl">
                            <asp:ImageButton ID="ImgCloseSearchActCom" runat="server" ImageUrl="../../images/Exitiframe.gif"
                                Height="19px" Width="19px" ToolTip="Cerrar Ventana" />
                        </div>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="LblTitBActCom" runat="server" CssClass="labelsTitN" Text="Buscar Actividad del Comercio" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:GridView ID="GVConsultaActCom" runat="server" AllowPaging="True" AutoGenerateSelectButton="True"
                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                        CaptionAlign="Left" CellPadding="3" EmptyDataText="Sr. Usuario , actualmente este planning no tiene actividades del comercio digitadas"
                                        Width="717px" OnPageIndexChanging="GVConsultaActCom_PageIndexChanging" OnSelectedIndexChanged="GVConsultaActCom_SelectedIndexChanged">
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle BackColor="White" Font-Names="Arial" Font-Size="9px" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Arial" Font-Size="9px"
                                            ForeColor="White" />
                                        <PagerSettings Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                                        <PagerStyle BackColor="White" Font-Names="Arial" Font-Size="9px" ForeColor="#000066"
                                            HorizontalAlign="Left" />
                                        <RowStyle Font-Names="Arial" Font-Size="9px" ForeColor="#000066" />
                                        <SelectedRowStyle Font-Names="Arial" Font-Size="9px" ForeColor="#000066" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="BuscarActLevPropia" runat="server" CssClass="CargaArchivos" Width="750px"
                        Height="400px" Style="display: none;">
                        <div dir="rtl">
                            <asp:ImageButton ID="ImgCloseSearchActPropia" runat="server" ImageUrl="../../images/Exitiframe.gif"
                                Height="19px" Width="19px" ToolTip="Cerrar Ventana" />
                        </div>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="LblTitBActLevPropia" runat="server" CssClass="labelsTitN" Text="Buscar Actividad Propias" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:GridView ID="GVConsultaActLevPropia" runat="server" AllowPaging="True" AutoGenerateSelectButton="True"
                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                        CaptionAlign="Left" CellPadding="3" EmptyDataText="Sr. Usuario , actualmente este planning no tiene actividades propias digitadas"
                                        Width="717px" OnPageIndexChanging="GVConsultaActLevPropia_PageIndexChanging"
                                        OnSelectedIndexChanged="GVConsultaActLevPropia_SelectedIndexChanged">
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle BackColor="White" Font-Names="Arial" Font-Size="9px" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Arial" Font-Size="9px"
                                            ForeColor="White" />
                                        <PagerSettings Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                                        <PagerStyle BackColor="White" Font-Names="Arial" Font-Size="9px" ForeColor="#000066"
                                            HorizontalAlign="Left" />
                                        <RowStyle Font-Names="Arial" Font-Size="9px" ForeColor="#000066" />
                                        <SelectedRowStyle Font-Names="Arial" Font-Size="9px" ForeColor="#000066" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--<a id="Downloadpdv" runat="server" visible="false" href="javascript:descargar('PictureComercio/PDV.csv')" >Puntos de Venta</a> 
                                                                                    <a id="Downloadpro" runat="server" visible="false" href="javascript:descargar('PictureComercio/Product.csv')" >Productos</a> 
                                                                                    <a id="Downloadmar" runat="server" visible="false" href="javascript:descargar('PictureComercio/Marca.csv')" >Marcas</a> 
                                                                                    <a id="Downloadenc" runat="server" visible="false" href="javascript:descargar('PictureComercio/encabezado.csv')" >Encabezado</a> 
                                                                                    <a id="Downloadpie" runat="server" visible="false" href="javascript:descargar('PictureComercio/pie.csv')" >Pie</a> --%>
    </form>
</body>
</html>
