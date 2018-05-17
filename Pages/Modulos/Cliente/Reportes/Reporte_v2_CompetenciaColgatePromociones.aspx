<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/design/MasterPage.master" AutoEventWireup="true" CodeBehind="Reporte_v2_CompetenciaColgatePromociones.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_CompetenciaColgatePromociones" %>


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
	
	
    Reporte de Promociones
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
<!--script type="text/javascript" src="../../../js/jquery-1.4.2.min.js"></script-->
<script type="text/javascript">

    jQuery(document).ready(function () {

        $('td img').css('height', '100px');
        $('td img').css('width', '100px');



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
        <telerik:RadPane ID="RadPane_contenidoReportes" runat="server" Width="100%" Height="400px">
         
    <div id="Titulo_reporte_Efectividad" style="text-align: left;width:100%" >
                <div style="text-align: center; font-family: Verdana; font-style: normal; font-size: large;
                    color: #D01887;">
                    C&P VS COMPETENCIA - PROMOTION
                    </div>
            </div>
            <br />
     <div id="div1">
                <asp:Panel runat="server" ID="panel1">
                        
                        <%
                            
                            
                            string cadena = "";
                            int var = 0;
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            { %>
                                <div style="background-color:#333399;font-size:15px;color:White;width:100%;height:25px;font-weight:bolder"> 
           <%  
                                cadena = ds.Tables[0].Rows[j][0].ToString();
                                Response.Write(cadena); %>
            </div>
                 <br />     
                 <table style="width:100%;height:20%" cellpadding="0" cellspacing="0">
               <tr>
              <td style="background-color:#953735;color:White;width:45%" 
                       class="centrarcontenido">
              COLGATE & PALMOLIVE
              </td>
              <td style="width:10%">
              
              </td>
              <td style="background-color:#31849B;color:White;width:45%;margin:auto" class="centrarcontenido">
              COMPETENCIA
              </td>
              </tr>
              </table>  

          
      <%   
               for (int i =var; i < ds.Tables[1].Rows.Count; i++)
               {
                   if (cadena != ds.Tables[1].Rows[i][20].ToString())
                   {
                       var = i;
                       break;
                   }
                   if (ds.Tables[1].Rows[i][9] != DBNull.Value)
                   {
                       byte[] colgate = (byte[])ds.Tables[1].Rows[i][9];
                       grabarimg(i.ToString(), colgate);
                   }
                   if (ds.Tables[1].Rows[i][19] != DBNull.Value)
                   {
                       byte[] competencia = (byte[])ds.Tables[1].Rows[i][19];

                       grabarimgcompetencia(i.ToString(), competencia);
                   }
                   // string año = DateTime.Now.Year.ToString();
                   //Response.Write(i);
             
              %>
            
           

              <table style="width:100%">
           
              <tr>
              <td colspan="3">
                  &nbsp;</td>
              </tr>


              <tr>
              <td style="width:45%">
              <table style="border-color:Black;border-width:2px;width:100%" border="2" cellpadding="0" cellspacing="0">
              <tr style="background-color:#5A5A5A;color:White;border-color:Black">
              <td>
                  Producto</td>
              <td>
                  Promoción</td>
              <td>
                  Foto
              </td>
              </tr>
           
              <tr style="color:Black">
              <td style="background-color:#BFBFBF">
                     <% Response.Write(ds.Tables[1].Rows[i][3].ToString()); %></td>
              <td style="background-color:#EEECE1">
                   <% Response.Write(ds.Tables[1].Rows[i][4].ToString()); %></td>
              <td rowspan="5">
               <a runat="server" id="link" class="class"> <img runat="server" id="imag"  /> </a></td>
              </tr>
           
              <tr style="background-color:#FFFFCC">
              <td style="color:Black">
                  Vigencia de la prom:</td>
              <td>
                   <% Response.Write(ds.Tables[1].Rows[i][5].ToString()); %></td>
              </tr>
           
              <tr style="background-color:#FFFFCC" >
              <td style="color:Black" >
                  Precio Promocional:</td>
              <td>
                   <% Response.Write(ds.Tables[1].Rows[i][6].ToString()); %> </td>
              </tr>
           
              <tr style="background-color:#FFFFCC" >
              <td style="color:Black">
                  Precio Regular:</td>
              <td>
                  <% Response.Write(ds.Tables[1].Rows[i][7].ToString()); %> </td>
              </tr>
           
              <tr style="background-color:#FFFFCC">
              <td style="color:Black">
                  Ciudad:</td>
              <td>
                  <% Response.Write(ds.Tables[1].Rows[i][8].ToString()); %>  </td>
              </tr>
           
              </table>
              </td>
                <td style="width:10%">
              
              </td>
               <td style="width:45%">
              <table style="border-color:Black;border-width:2px;width:100%" border="2" cellpadding="0" cellspacing="0">
              <tr style="background-color:#5A5A5A;color:White;border-color:Black">
              <td>
                  Producto</td>
              <td>
                  Promoción</td>
              <td>
                  Foto
              </td>
              </tr>
           
              <tr style="color:Black">
              <td style="background-color:#BFBFBF">
                     <% Response.Write(ds.Tables[1].Rows[i][13].ToString()); %></td>
              <td style="background-color:#EEECE1">
                   <% Response.Write(ds.Tables[1].Rows[i][14].ToString()); %></td>
              <td rowspan="5">
              <a id="linkCompe" runat="server" class="class">  <img runat="server" id="Img2"  /></a></td>
              </tr>
           
              <tr style="background-color:#FFFFCC">
              <td style="color:Black">
                  Vigencia de la prom:</td>
              <td>
                   <% Response.Write(ds.Tables[1].Rows[i][15].ToString()); %></td>
              </tr>
           
              <tr style="background-color:#FFFFCC" >
              <td style="color:Black" >
                  Precio Promocional:</td>
              <td>
                   <% Response.Write(ds.Tables[1].Rows[i][16].ToString()); %> </td>
              </tr>
           
              <tr style="background-color:#FFFFCC" >
              <td style="color:Black">
                  Precio Regular:</td>
              <td>
                  <% Response.Write(ds.Tables[1].Rows[i][17].ToString()); %> </td>
              </tr>
           
              <tr style="background-color:#FFFFCC">
              <td style="color:Black">
                  Ciudad:</td>
              <td>
                  <% Response.Write(ds.Tables[1].Rows[i][18].ToString()); %>  </td>
              </tr>
           
              </table>
              </td>
              </tr>
              </table>

              <br />       
               <%
               }

                            }
        %>
 
</asp:Panel>
</div>
            
        </telerik:RadPane>
    </telerik:RadSplitter>




 
   

   
 
</asp:Content>