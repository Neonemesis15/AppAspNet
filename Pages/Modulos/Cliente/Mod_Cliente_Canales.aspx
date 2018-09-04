<%@ Page Language="C#" AutoEventWireup="true" Debug="true"  CodeBehind="Mod_Cliente_Canales.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Mod_Cliente_Canales"  %>
    <%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/Webfotter.css" rel="stylesheet" type="text/css" />
    <link href="../../css/StiloPOP.css" rel="stylesheet" type="text/css" />
     <link href="../../css/MenuBiblioteca.css" rel="stylesheet" type="text/css" />

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

    
    
</head>
<body runat="server" >
    <div align="center">
         <form id="form1" runat="server">

                  
                    <asp:Label ID="SelModulo" runat="server" Text="Módulo Alternativo" ForeColor="Black"
                        Font-Names="Verdana" Font-Size="12px" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="cmbselModulos" runat="server" Font-Names="Verdana" Font-Size="11px"
                        AutoPostBack="True" OnSelectedIndexChanged="cmbselModulos_SelectedIndexChanged">
                    </asp:DropDownList>
                
                    <asp:Label ID="SelCliente" runat="server" Text="Cliente" ForeColor="Black"
                        Font-Names="Verdana" Font-Size="12px" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="cmbcliente" runat="server" Font-Names="Verdana" Font-Size="11px">
                    </asp:DropDownList>
                    <asp:Button ID="GO" runat="server" Text="Ir" Font-Size="10px" 
                        OnClick="GO_Click" />
                   
                </div>

    <div  style="height: 150px;">
    
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Pages/ImgBooom/logotipo.png" ImageAlign="Left">
        </asp:Image>
    </div>
   
    
        

          <cc1:ToolkitScriptManager ID="ToolKitScriptManager" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true">
        </cc1:ToolkitScriptManager>
        <div align="center" >
        </div>
         <asp:Panel ID="pmodulos" runat="server">

     
         
         
         
         </asp:Panel>
         
       
        <div class="HeaderRight">
            <asp:Image ID="Imgcliente" runat="server" Height="49px" Width="218px" />
            <br />
            <asp:Label ID="lblUsuario" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="#114092"></asp:Label>
            <asp:Label ID="usersession" runat="server" Visible="False" Font-Names="Verdana" Font-Size="9pt"
                ForeColor="#114092"></asp:Label>
            <br />
            <asp:Label ID="lblcompany" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="#114092"></asp:Label>
            <br />
            <asp:ImageButton ID="ImgCloseSession" runat="server" Height="16px" ImageUrl="~/Pages/images/SesionClose.png"
                Width="84px" OnClick="ImgCloseSession_Click" />
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            
               
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Panel ID="PanelClienteCanales" runat="server">
            <table align="center" style="height: 306px; position: absolute; top: 100px; width: 720px;
                left: 15%; width: 92%; height: 80%;">
                <tr>
                    <td align="center">
                        <div id="CarrosTabla" class="tamañocarrusel" style="margin-left: auto; margin-right: auto;
                            width: 977px; border: 0px solid #000;">
                            <div class="button-next">
                                <a href="javascript:stepcarousel.stepBy('carousel', 1)">
                                    <img id="derecha" runat="server" src="../../img/derecha.png" alt="Siguiente" style="height: 50px;" />
                                </a>
                            </div>
                            <div class="button-prev">
                                <a href="javascript:stepcarousel.stepBy('carousel', -1)">
                                    <img id="izquierda" runat="server" src="../../img/izquierda.png" alt="Atras" style="height: 50px;" />
                                </a>
                            </div>
                            <div id="carousel" class="stepcarousel" align="center">
                                <div class="belt" align="center">
                                    <asp:ListView runat="server" ID="ListViewCanales" OnSelectedIndexChanged="ListViewCanales_SelectedIndexChanged"
                                        Style="vertical-align:middle" DataKeyNames="codigo_canal" OnPagePropertiesChanging="ListViewCanales_PagePropertiesChanging"
                                        OnSelectedIndexChanging="ListViewCanales_SelectedIndexChanging">
                                        <LayoutTemplate>
                                            <table runat="server" id="table1" align="center">
                                                <tr runat="server" id="itemPlaceholder">
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr id="Tr2" runat="server" class="panel">
                                                <td id="Td0" runat="server" align="center">
                                                    <asp:Label ID="CodCanal" runat="server" Text='<%#Eval("codigo_canal") %>' Visible="false" />
                                                    <asp:Label ID="NameCanal" runat="server" Text='<%#Eval("nombre_canal") %>' Visible="false" />
                                                    <asp:LinkButton runat="server" ID="SelectCategoryButton" Text="" CssClass='<%#Eval("nombre_canal") %>'
                                                         CommandName="Select" Width="184px" Height="238px" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                               
                                <br />
                                 <div  id= "dvmay" runat="server" align="center">
                                 <div style="max-height: 200px; overflow: auto; display: none;   background-color: Transparent;"
                                            id="divscanal" runat="server">
                                          <table>
                                          <tr>
                                          <td>
                                        <div class="LabelMarca">
                                                <div>
                                                  <asp:Label ID="lblscanal" runat="server">
                                                  </asp:Label>
                                                    
                                                    </div>
                                            </div>
                                            <asp:Menu ID="MenuScanal" runat="server" CssSelectorClass="menu" Orientation="Vertical"
                                                StaticDisplayLevels="3" Width="100%" OnMenuItemClick="MenuScanal_MenuItemClick">
                                            </asp:Menu> 
                                          </td>
                                          <td>
                                           <div id="lbmarcas" runat="server" class="LabelMarca" style="display:none;">
                                          <div>
                                            <asp:Label ID="lblSubnivel" runat="server">
                                                  </asp:Label>
                                          </div> 
                                          </div> 
                                             <asp:Menu ID="MenSNivel" runat="server" CssSelectorClass="menu" Orientation="Vertical"
                                                StaticDisplayLevels="3" Width="100%" OnMenuItemClick="MenSNivel_MenuItemClick">
                                            </asp:Menu>          
                                          </td>
                                          </tr>
                                          </table> 
                                           
                                                                           
                                        </div>
                                        </div>

                            

                          
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="POP">
            <table>
                <tr>
                    <td>
                        <asp:ListView runat="server" ID="Biblioteca" OnSelectedIndexChanged="Biblioteca_SelectedIndexChanged"
                            Style="vertical-align: middle" DataKeyNames="Report_Id" OnPagePropertiesChanging="Biblioteca_PagePropertiesChanging"
                            OnSelectedIndexChanging="Biblioteca_SelectedIndexChanging">
                            <LayoutTemplate>
                                <table runat="server" id="table1" align="center">
                                    <tr runat="server" id="itemPlaceholder">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr1" runat="server">
                                    <td id="Td1" runat="server" align="center">
                                        <asp:Label ID="CodReporte" runat="server" Text='<%#Eval("Report_Id") %>' Visible="false" />
                                        <asp:Label ID="NameReporte" runat="server" Text='<%#Eval("Report_NameReport") %>'
                                            Visible="false" />
                                        <asp:LinkButton runat="server" ID="SelectBUTTONPOP" Text="" CssClass="listviewpop"
                                            CommandName="Select" Width="96px" Height="116px" Font-Overline="false" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                    <td>
                        <asp:Button ID="ImgBtnInfGerencial" runat="server" Width="96px" Height="116px" CssClass="infGerecial"
                            onclick="ImgBtnInfGerencial_Click" />
                    <asp:Button ID="ImgBtnResumen_Ejecutivo" runat="server" Width="96px" Height="116px" CssClass="ResEjecutivo"
                    />
                    </td>
                     <td>
                        <asp:Button ID="btnActivapromo" runat="server" Width="96px" Height="116px" CssClass="btnActivapromo" />
                    </td>
                </tr>
            </table>
        </div>
       
        </form>
      
    <div class="LogoRight">
        <asp:Image ID="Image1" runat="server" 
            ImageUrl="~/Pages/ImgBooom/logo_luckyn.png" Height="54px">
        </asp:Image>
    </div>
</body>
</html>
