<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master" AutoEventWireup="true" CodeBehind="Report_Alicorp_DataValidada.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Alicorp_DataValidada" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--Referencias al usrcontrol--%>
<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Src="MasterPage/DefaultHeader.ascx" TagName="DefaultHeader" TagPrefix="uc1" %>
<%@ Register Src="MasterPage/DefaultMenu.ascx" TagName="DefaultMenu" TagPrefix="uc1" %>
<%@ Register Src="MasterPage/DefaultSidebar2.ascx" TagName="DefaultSidebar2" TagPrefix="uc1" %>
<%--end al usercontrol--%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Data Mercaderistas</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <uc1:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <uc1:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<%--<asp:Content ID="SideBar1" ContentPlaceHolderID="Sidebar1ContentPlaceHolder" runat="Server">
    <uc1:DefaultSidebar2 ID="DefaultSidebar1Content" runat="server" />
</asp:Content>--%>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <%--    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>
            <table style="width: 100%; height: auto;" align="center" class="style1">
                <tr>
                    <td class="style4" colspan="2">
                        REPORTE VALIDACION DE VENTAS
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        Canal :
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="cmbcanal" runat="server" AutoPostBack="True" Height="25px"
                            OnSelectedIndexChanged="cmbcanal_SelectedIndexChanged" Width="275px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        Campaña :
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="cmbplanning" runat="server" Height="25px"
                            Width="275px" Enabled="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        Año :
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="cmbAño" runat="server" Height="25px" Width="275px" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        Mes :
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="cmbMes" runat="server" AutoPostBack="True" Height="25px"
                             Width="275px" onselectedindexchanged="cmbMes_SelectedIndexChanged"
                            >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        Periodo :
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="cmbPeriodo" runat="server" Height="25px" Width="275px" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        &nbsp;
                    </td>
                    <td class="style2">
                        <div>
                            <asp:Button ID="btn_buscar" runat="server" Text="Consultar Resumen" CssClass="buttonGreen"
                                Height="25px" Width="164px" OnClick="btn_buscar_Click" />
                            <asp:Button ID="BtnDetalle" runat="server" CssClass="buttonGreen" Height="25px" Text="Consultar Detalle"
                            Width="164px"  OnClick="BtnDetalle_Click"  />
                           
                        </div>
                        <div>
                            <asp:Label ID="lblmensaje" runat="server" Style="text-align: left"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
            <div id="div_gvValidado" runat="server" style="width: 100%; height: auto;">
            <telerik:RadGrid ID="gvValidado" runat="server" Skin="Vista" GridLines="None" >
                                </telerik:RadGrid>
            
            </div>
            
            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3"
                AssociatedUpdatePanelID="UpdatePanel_contenido" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">
                        <div>z
                            Cargando...
                        </div>
                        <br />
                        <div>
                            <img alt="Procesando" src="../../../images/loading5.gif" style="vertical-align: middle" />
                        </div>
                    </div>
                </ProgressTemplate>
            </cc2:ModalUpdateProgress>

        </ContentTemplate>

        
    </asp:UpdatePanel>
    <table style="width: 100%;display:none">
        <tr>
            <td class="style10">
                &nbsp;
            </td>
            <td class="style10">
                &nbsp;<asp:ImageButton ID="btn_img_exporttoexcel" runat="server" Height="42px" ImageUrl="~/Pages/Modulos/Operativo/Reports/Image/icono_excel.jpg"
                    OnClick="btn_img_exporttoexcel_Click" Width="38px" />
                &nbsp;Exportar a excel
            </td>
            <td class="style10">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="gv_layoutToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False">
                    <RowStyle ForeColor="#000066" />
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style2
        {
            text-align: left;
        }
        .style4
        {
            text-align: center;
            font-weight: bold;
            font-size: medium;
            vertical-align: center;
        }
        .style5
        {
            text-align: right;
            width: 158px;
        }
        .style6
        {
            text-align: left;
            width: 158px;
        }
    </style>
</asp:Content>
