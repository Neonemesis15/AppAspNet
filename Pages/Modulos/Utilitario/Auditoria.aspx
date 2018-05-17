<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Utilitario/MasterPage/design/MasterPage.master" AutoEventWireup="true" CodeBehind="Auditoria.aspx.cs" Inherits="SIGE.Pages.Modulos.Utilitario.Auditoria" %>

<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Utilitario/MasterPage/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Utilitario/MasterPage/DefaultHeader.ascx" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultSidebar1" Src="~/Pages/Modulos/Utilitario/MasterPage/DefaultSidebar1.ascx" %>
<%@ Register Assembly="obout_Show_Net" Namespace="OboutInc.Show" TagPrefix="obshow" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
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
    
    
    <asp:UpdatePanel ID="UpReport" runat="server">
        <ContentTemplate>

        <div align="center" >
        <br />
    <asp:Label Text="Reporte de Auditoria" runat="server" ID="lbl" Font-Size="X-Large" ForeColor="Blue" ></asp:Label>
    <br />
    <br />
            <div id="div_gvFoto" runat="server" aling="center" style="width: 100%; height: auto;">
                
                <telerik:RadGrid ID="gv_Foto" runat="server" AutoGenerateColumns="False" PageSize="2000"
                    Skin="Vista" Font-Size="Small" AllowPaging="True" GridLines="None" OnCancelCommand="gv_Foto_CancelCommand"
                    OnEditCommand="gv_Foto_EditCommand" OnPageIndexChanged="gv_Foto_PageIndexChanged"
                    ShowFooter="True" OnPageSizeChanged="gv_Foto_PageSizeChanged" 
                     OnItemCommand="gv_Foto_ItemCommand">
                    <MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E"
                        AlternatingItemStyle-BackColor="#F7F7F7" Font-Size="Smaller" AlternatingItemStyle-ForeColor="#333333">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" UniqueName="Fecha" ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Hora" HeaderText="Hora" UniqueName="Hora" ReadOnly="true">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="canal" HeaderText="canal" UniqueName="canal" ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Marca" HeaderText="Marca" UniqueName="Marca" ReadOnly="true">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="PDV" HeaderText="PDV" UniqueName="PDV" ReadOnly="true">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" UniqueName="Observaciones" ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Foto" UniqueName="TemplateColumn" ReadOnly="true">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkBtn_viewphoto" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                    CommandName="VERFOTO" Font-Underline="True">Ver</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                    <asp:Label ID="lbl_id_reg_foto" runat="server" Text='<%# Bind("ID") %>' Visible="False"></asp:Label>
                                    <telerik:RadBinaryImage ID="RadBinaryImage_foto" runat="server" Width="110px" Height="90px"
                                         DataValue='<%# Eval("Foto") %>' AutoAdjustImageControlSize="False" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            
                        </Columns>

                        <AlternatingItemStyle BackColor="#F7F7F7" ForeColor="#333333" />
                    </MasterTableView>
                    <PagerStyle PageSizeLabelText="Tamaño de pagina:" />
                </telerik:RadGrid>
                <br />
            </div>


            <div>
                <asp:Button ID="btn_view" runat="server" CssClass="alertas" Text="ocultar" Width="95px" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender_viewfoto" runat="server" DropShadow="True"
                    TargetControlID="btn_view" PopupControlID="panel_viewfoto" CancelControlID="ImageButtonCancel_viewfoto">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="panel_viewfoto" runat="server" style="display:none"   BackColor="White"
                    BorderColor="#AEDEF9" BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana"
                    Font-Size="10pt" Height="520px" Width="590px">
                    <div>
                        <asp:ImageButton ID="ImageButtonCancel_viewfoto" runat="server" BackColor="Transparent"
                            Height="22px" ImageAlign="Right" ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                    </div>
                    <div align="center" style="font-family: verdana; font-size: medium; color: #005DA3;">
                        <div>
                            Foto</div>
                        <br />
                        <div>
                            <telerik:RadBinaryImage ID="RadBinaryImage_viewFoto" runat="server" Width="570px"
                                Height="400px" AutoAdjustImageControlSize="False"  AlternateText="Subir foto"
                                GenerateEmptyAlternateText="true" />
                        </div>
                        <div align="center"  >
                                    <asp:imagebutton id="ImageButton3"
				runat="server" Width="50px" Height="50px" 
                onclick="ImageButton3_Click" ImageUrl="~/Pages/images/sub_black_rotate_ccw.ico"
                ></asp:imagebutton>
                            <asp:imagebutton id="ImageButton4" 
				runat="server" Width="50px" Height="50px" ImageUrl="~/Pages/images/sub_black_rotate_cw.ico" onclick="ImageButton4_Click" 
                ></asp:imagebutton>
                                    Guardar<asp:ImageButton 
                                        ID="ibtnGuardarImagen" runat="server" ImageUrl="~/Pages/images/save_icon.png" onclick="ibtnGuardarImagen_Click"
                       />
                        </div>
                    </div>
                    </asp:Panel>
                        </div>
                
            </div>

            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="3"
                AssociatedUpdatePanelID="UpReport" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">
                        <div>
                            Cargando...
                        </div>
                        
                    </div>
                </ProgressTemplate>
            </cc2:ModalUpdateProgress>
            </ContentTemplate>
            </asp:UpdatePanel>

          
            


</asp:Content>