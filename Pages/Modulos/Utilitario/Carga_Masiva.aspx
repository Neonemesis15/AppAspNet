<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Utilitario/MasterPage/design/MasterPage.master" AutoEventWireup="true" CodeBehind="Carga_Masiva.aspx.cs" Inherits="SIGE.Pages.Modulos.Utilitario.Carga_Masiva" %>

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


   
    <div>    
      <asp:Button ID="btndipararalerta" runat="server" Text="" Height="0px" CssClass="alertas"
                    Width="0" Enabled="False" />
        <table align="center" bgcolor="#7F99CC" style="width: 420px">
            <tr>
                <td align="left">
                    &nbsp;&nbsp;
                    <asp:Label ID="LblTitCargarArchivo" runat="server" CssClass="labels"  
                    Text="Cargar nuevo archivo" ></asp:Label>
                </td>            
            </tr>        
        </table>
        
        <br />
        
                <table align="center" id="tblProductos" runat="server" >
                    <tr>
                        <td>
                            <asp:Label ID="LblCargarArchivo" runat="server" 
                            Text="Elementos de visibilidad:" Font-Names="Arial" Font-Size="8pt" 
                            ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUp" runat="server" Width="237px" />
                        </td>   
                        <td style="display:none">
                         <a class="button" href="../../formatos/Formato_Carga_Rutas.xls"><span>Descargar Formato</span></a>
                        </td>                 
                    </tr>
                <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" 
                            Text="Presencia Colgate o Competencia:" Font-Names="Arial" Font-Size="8pt" 
                            ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpPresencia" runat="server" Width="237px" />
                        </td>   
                        <td style="display:none">
                         <a class="button" href="../../formatos/Formato_Carga_Rutas.xls"><span>Descargar Formato</span></a>
                        </td>                 
                    </tr>
                    
                </table>                
               
                
             
                 
                <table align="center" style="width: 348px">
                    <tr>
                        <td align="center">
                            <asp:Label ID="LblVacio" runat="server" Font-Bold="True" Font-Names="Arial" 
                            Font-Size="7.5pt"></asp:Label>
                        </td>
                    </tr>
                    <tr >
                        <td align="center">
                            <asp:Button ID="btnCargar" runat="server" 
                            Text="Cargar Pres.Colgate,Competencia o exhibicion" Height="22px" Width="117px" 
                                onclick="btnCargar_Click" />
                                <td align="center">
                            <asp:Button ID="btnCargarPresencia" runat="server" 
                            Text="Cargar" Height="22px" Width="117px" onclick="btnCargarPresencia_Click"  />
                        </td>
                    </tr>
                </table>
                  </div>
                 <%--panel de mensaje de usuario   --%>
            <asp:Panel ID="Pmensaje" runat="server" Height="169px" Width="332px" Style="display: none;">
                <table align="center">
                    <tr>
                        <td align="center" style="height: 119px; width: 79px;" valign="top">
                            <br />
                        </td>
                        <td style="width: 238px; height: 119px;" valign="top">
                            <br />
                            <asp:Label ID="lblencabezado" runat="server" CssClass="labels"></asp:Label>
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

             
                



</asp:Content>