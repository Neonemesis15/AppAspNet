<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Operativo/Reports/MasterPage/design/MasterPage2.master"
    AutoEventWireup="true" CodeBehind="Report_Ventas_Masisa.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.Report_Ventas_Masisa" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc3" %>
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
    <asp:UpdatePanel ID="UpdatePanel_contenido" runat="server">
        <ContentTemplate>
            <div align="center">
            <table style="width: 100%; height: auto;" align="center" class="art-Block-body">
                <tr>
                    <td class="style3" >
                        REPORTE DE VENTAS
                    </td>
                </tr>
            </table>

            <fieldset    style="width:850px"  >
            <legend  > Consultar Reporte Ventas</legend>

            <table>
            <tr><td class="style8">Canal :</td>
           <td class="style2"><asp:DropDownList ID="cmbcanal" runat="server" Height="25px" 
                   Width="275px" 
                            AutoPostBack="True" Font-Bold="False" Font-Italic="False" 
                   onselectedindexchanged="cmbcanal_SelectedIndexChanged">
                        </asp:DropDownList>&nbsp;</td></tr>
                        <tr>
                                 <td align="right">
                                     Punto de Venta:</td>
                                 <td>
                                     <asp:DropDownList ID="cmb_pdv" runat="server" AutoPostBack="True" 
                                         CausesValidation="True" Enabled="False" Height="25px" Width="275px">
                                     </asp:DropDownList>
                                 </td>
                             </tr>
                
                             <tr>
                         
                                 <td align="right">
                                     Año : </td>
                                 <td align="left">
                                     <asp:DropDownList ID="cmb_año" runat="server" AutoPostBack="True" 
                                         CausesValidation="True" Height="25px" 
                                         OnSelectedIndexChanged="cmb_año_SelectedIndexChanged" 
                                         Style="text-align: left" Width="275px">
                                     </asp:DropDownList>
                                 </td>
                             </tr>
             
                             <tr>
                         
                                 <td align="right">
                                     Mes :</td>
                                 <td>
                                     <asp:DropDownList ID="cmb_mes" runat="server" AutoPostBack="True" 
                                         CausesValidation="True" Height="25px" 
                                         OnSelectedIndexChanged="cmbPeriodoxMes_SelectedIndexChanged" 
                                         Style="text-align: left" Width="275px">
                                     </asp:DropDownList>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="right">
                                     Semana :</td>
                                 <td>
                                     <asp:DropDownList ID="cmb_periodo" runat="server" AutoPostBack="True" 
                                         Height="25px" OnSelectedIndexChanged="cmb_periodo_SelectedIndexChanged" 
                                         Style="text-align: left" Width="275px">
                                     </asp:DropDownList>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="right">
                                     Día:</td>
                                 <td>
                                     <telerik:RadDatePicker ID="calendar_day" runat="server" Culture="es-PE" 
                                         Width="238px">
                                         <calendar usecolumnheadersasselectors="False" userowheadersasselectors="False" 
                                             viewselectortext="x">
                                         </calendar>
                                         <dateinput labelcssclass="" width="">
                                         </dateinput>
                                         <datepopupbutton cssclass="" hoverimageurl="" imageurl="" />
                                     </telerik:RadDatePicker>
                                 </td>
                             </tr>
                             
                             <tr>
                                 <td align="center" colspan="2">
                                     <asp:Button ID="btn_buscar" runat="server" CssClass="buttonRed" Height="25px" 
                                         OnClick="btn_buscar_Click" Text="Buscar" Width="164px" />
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     &nbsp;</td>
                                 <td>
                                     <asp:Label ID="lblmensaje" runat="server"></asp:Label>
                                 </td>
                             </tr>
            

            </table>

            </fieldset>
            <telerik:RadGrid ID="gv_ventas" runat="server" AutoGenerateColumns="False" PageSize="100"
                    Skin="Vista" Font-Size="Small" AllowPaging="True" GridLines="None" OnCancelCommand="gv_ventas_CancelCommand"
                    OnEditCommand="gv_ventas_EditCommand" OnPageIndexChanged="gv_ventas_PageIndexChanged"
                    ShowFooter="True" OnPageSizeChanged="gv_ventas_PageSizeChanged" OnUpdateCommand="gv_ventas_UpdateCommand"
                    OnDataBound="gv_ventas_DataBound">
                    <MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E"
                        AlternatingItemStyle-BackColor="#F7F7F7" Font-Size="Smaller" AlternatingItemStyle-ForeColor="#333333">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ROWID" HeaderText="N°" UniqueName="ROWID" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Product_Category" HeaderText="Categoria" UniqueName="Product_Category"
                                ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Product_Name" HeaderText="Producto" UniqueName="Product_Name"
                                ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn DataField="venta" DataType="System.Double" EmptyDataText="0"
                                HeaderText="Venta en Unidades" UniqueName="Venta">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn DataField="Precio" DataType="System.Double" EmptyDataText="0"
                                HeaderText="Precio" UniqueName="Precio" ReadOnly="True" Visible="False">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn DataField="Total" HeaderText="Venta en Soles" 
                                UniqueName="Total" DataType="System.Double" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn UniqueName="EditCommandColumn1" ButtonType="ImageButton" CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png">
                            </EditColumn>
                        </EditFormSettings>
                        <AlternatingItemStyle BackColor="#F7F7F7" ForeColor="#333333" />
                    </MasterTableView>
                    <PagerStyle PageSizeLabelText="Tamaño de pagina:" />
                </telerik:RadGrid>
            
                                    &nbsp;


            </div>
                
            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3"
                AssociatedUpdatePanelID="UpdatePanel_contenido" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">
                        <div>
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
    <div style="margin:auto; width:40px;">
     &nbsp;
      &nbsp;
    <asp:ImageButton ID="ImageButton1" runat="server" Height="42px" ImageUrl="~/Pages/Modulos/Operativo/Reports/Image/icono_excel.jpg"
                    OnClick="btn_img_exporttoexcel_Click" Width="39px" />
                Exportar a excel

    </div>
    <table style="width: 100%;">
        <tr>
            <td class="style10">
                &nbsp;
            </td>
            <%--<td class="style10">
                &nbsp;
                <asp:ImageButton ID="btn_img_exporttoexcel" runat="server" Height="42px" ImageUrl="~/Pages/Modulos/Operativo/Reports/Image/icono_excel.jpg"
                    OnClick="btn_img_exporttoexcel_Click" Width="39px" />
                Exportar a excel
            </td>--%>
            <td class="style10">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp; &nbsp; &nbsp;
                <asp:GridView ID="gv_stockToExcel" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False">
                    <RowStyle ForeColor="#000066" />
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        *{
            margin: 0;
            text-align: center;
        }
        *{
	        margin: 0;
        }
    </style>
    </asp:Content>
