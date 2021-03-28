<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestiónProducto.aspx.cs" Inherits="SIGE.Pages.Modulos.Administrativo.GestiónProducto" Culture="Auto" UICulture="Auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register src="ProductUserControls/CategorySearchUserControl.ascx" tagname="CategorySearchUserControl" tagprefix="uc2" %>

<%@ Register src="ProductUserControls/CategoryInsertUserControl.ascx" tagname="CategoryInsertUserControl" tagprefix="uc1" %>

<!--
-- Author       : Magaly Jiminez (MJ)
-- Create date  : 13/08/2010
-- Description  : Permite al actor Administrador de SIGE realizar todos los procesos para la administracion de Gestión De Productos.
--
-- Change History:
-- 08/11/2018 Pablo Salas Alvarez (PSA): Refactoring Producto.
-->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>Gestión Producto</title>
    <style type="text/css">
        .style49
        {
            width: 296px;
            height: 145px;
        }
        .style50
        {
            height: 145px;
            width: 62px;
        }
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }

        /*Estilos CSS para GridView*/
        .GridPager a, .GridPager span
        {
            display: block;
            height: 15px;
            width: 15px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }
        .GridPager a
        {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }
        .GridPager span
        {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }

    </style>
    <link href="../../css/StiloAdministrativo.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: transparent;">
    <form id="form1" runat="server">
    
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="release" EnableScriptGlobalization="True" 
        EnableScriptLocalization="True"> 
    </cc1:ToolkitScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- <table align="center">
                <tr>
                    <td>
                        <asp:Label ID="LblUpProgressUP" runat="server" Text="." ForeColor="#C7CEDA"></asp:Label>
                    </td>
                    <td>
                        <asp:Panel ID="PProgresso" runat="server">
                            <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                                <ProgressTemplate>
                                    <div style="text-align: center;">
                                        <img alt="Procesando" src="../../images/loading1.gif" style="vertical-align: middle" />
                                        Por favor espere...
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </td>
                </tr>
            </table>--%>
            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server"
                    AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">
                        <div>Cargando...</div><br />
                        <div><img alt="Procesando" src="../../images/loading5.gif" style="vertical-align: middle" /></div>
                    </div>
                </ProgressTemplate>
            </cc2:ModalUpdateProgress>

            <cc1:TabContainer ID="TabAdministradorProductos" runat="server" ActiveTabIndex="0"
                Width="100%" Height="460px" Font-Names="Verdana" allowtransparency="true" style="margin-top: 0px; Overflow:auto">

                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_CATEGORIA - INI -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <cc1:TabPanel runat="server" HeaderText="Gestión Productos" ID="Panel_CategProduct">
                    <HeaderTemplate>Categorias</HeaderTemplate>
                    <ContentTemplate>
                        
                                              
                        <!---------------------------------------------------------------------------->
                        <!---------------------------------------------------------------------------->
                        <!-- PANEL_RESULTADO_BUSQUEDA- INI -->
                        <!---------------------------------------------------------------------------->
                        <!---------------------------------------------------------------------------->
                        <asp:Panel ID="CosultaGVCategoria" runat="server" Style="display: block"  >                        
                            <div class="centrar centrarcontenido">                                
                                
                                <div class="p" style="width:780px; height: 315px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
	                                    font-family : arial, Helvetica, sans-serif;"> 

                                    <!-- GRILLA_DATOS - INI -->
                                    <asp:GridView ID="GVConsultaCategoria" runat="server" AutoGenerateColumns="False"  
                                            Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                            Width="100%" 
                                            onrowediting="GVConsultaCategoria_RowEditing" 
                                            onpageindexchanging="GVConsultaCategoria_PageIndexChanging" 
                                            onrowcancelingedit="GVConsultaCategoria_RowCancelingEdit" 
                                            onrowupdating="GVConsultaCategoria_RowUpdating">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Cod.Categoria">
                                                <EditItemTemplate>
                                                        <asp:Label ID="LblCodProductType" runat="server"  Text='<%# Bind("id_ProductCategory") %>'>
                                                        </asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCodProductType" runat="server" Text='<%# Bind("id_ProductCategory") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="70px" />
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="Categoria">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TxtNomProductType" runat="server" Width="150px">
                                                    </asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblNomProductType" runat="server"  Width="150px" Text='<%# Bind("Product_Category") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="Grupo Categoria">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TxtgroupCategory" runat="server" Width="150px" >       
                                                    </asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <asp:Label ID="Lblgroupcategory" runat="server"  Width="150px" Text='<%# Bind("Group_Category") %>'>
                                                </asp:Label>                                                      
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                        
                                            <asp:TemplateField HeaderText="Cod.Cliente" Visible="False">
                                                <EditItemTemplate>
                                                        <asp:Label ID="LblCodClie" runat="server"  Text='<%# Bind("Company_id") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCodClie" runat="server" Text='<%# Bind("Company_id") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="70px" />
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="Cliente">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="cmbCliente_Edit" runat="server" Width="150px" >       
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblClienteID" runat="server" Width="150px" Text='<%# Bind("Company_name") %>'>></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                        
                                            <asp:TemplateField HeaderText="Estado" >
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="CheckECategoria" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckECategoria" runat="server"  Enabled="false" Checked ='<%# Bind("ProductCategory_Status") %>' >
                                                    </asp:CheckBox> 
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:CommandField ShowEditButton="True" />

                                        </Columns>                                                
                                    </asp:GridView><br /><br />
                                    <!-- GRILLA_DATOS - FIN -->

                                    <!-- EXPORTAR_EXCEL - INI -->
                                    <div class="centrar">
                                        <div class="centrar centrarcontenido">
                                            <asp:Label ID="Lblcateeexc" runat="server" CssClass="labels" Text="Exportar a Excel "></asp:Label>
                                        </div>
                                        <iframe id="iframeexcelCategoria" runat="server" src="pruebaexcel.aspx" frameborder="0"  width="64px" height="64px">
                                        </iframe>
                                        <div class="centrar centrarcontenido">                                       
                                            <asp:Button ID="btnCCategoria" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="80px" 
                                                onclick="BtnCancelProductType_Click" />
                                        </div> 
                                    </div>
                                    <!-- EXPORTAR_EXCEL - FIN -->

                                </div>
                            </div> 
                        </asp:Panel>
                        <!---------------------------------------------------------------------------->
                        <!---------------------------------------------------------------------------->
                        <!-- PANEL_RESULTADO_BUSQUEDA- FIN -->
                        <!---------------------------------------------------------------------------->
                        <!---------------------------------------------------------------------------->

                        <cc1:ModalPopupExtender ID="MopopConsulCate" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="CosultaGVCategoria"
                            TargetControlID="btnPopupGVcategoria" DynamicServicePath="">
                        </cc1:ModalPopupExtender>

                        <asp:Button ID="btnPopupGVcategoria" runat="server" CssClass="alertas" Width="0px" />
                        



                        <!---------------------------------------------------------------------------->
                        <!---------------------------------------------------------------------------->
                        <!-- SECCION_OPCIONES- INI -->
                        <!---------------------------------------------------------------------------->
                        <!---------------------------------------------------------------------------->
                        <br /><br />
                        <div class="tabla centrar">
                            <div class="fila">
                                <div class="celda centrarcontenido">

                                    
                                   
                                    
                                 <uc1:CategoryInsertUserControl ID="CategoryUserControl1" runat="server" />
                                  
                                    <!---------------------------------------------------------------------------->
                                    <!---------------------------------------------------------------------------->
                                    <!-- >>>>>>>>>>>>>>>>>>> H  U  E  C  O  <<<<<<<<<<<<<<<-->
                                    <!-- BtnSaveProductType ->
                                    <!---------------------------------------------------------------------------->
                                    <!---------------------------------------------------------------------------->
                                    
                                    
                                    <uc2:CategorySearchUserControl ID="CategorySearchUserControl1" runat="server" />
                                    
                                    
                                   

                                    <asp:Button ID="BtnCancelProductType" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                        Width="95px" OnClick="BtnCancelProductType_Click" />
                                    
                                    <asp:Button ID="BtnCargaMasivaCate" runat="server" CssClass="buttonPlan"
                                        Text="Carga Masiva" Width="95px"/>


                                </div>
                            </div>
                        </div>
                        <!---------------------------------------------------------------------------->
                        <!---------------------------------------------------------------------------->
                        <!-- SECCION_OPCIONES- FIN -->
                        <!---------------------------------------------------------------------------->
                        <!---------------------------------------------------------------------------->





                        
                        
                        <!---------------------------------------------------------------------------->
                        <!---------------------------------------------------------------------------->
                        <!-- PANEL_CARGA_MASIVA - INI -->
                        <!---------------------------------------------------------------------------->
                        <!---------------------------------------------------------------------------->
                        <asp:Panel ID="CarMasivaCategoria" runat="server" Style="vertical-align: middle;" 
                            BackColor="#7F99CC" BorderColor="#7F99CC"
                            BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt">    
                            <div style="position:absolute; z-index:1;  width:100%;" align="right" > 
                                <asp:ImageButton ID="ImCMCategoria" runat="server" AlternateText="Cerrar Ventana"
                                    ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                    ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                            </div>
                            <div class="centrar">                                   
                                <iframe id="IfCargaMCategoria" runat="server" height="230px" src="" width="500px">
                                </iframe>                                       
                            </div>                                                           
                        </asp:Panel>
                        <!---------------------------------------------------------------------------->
                        <!---------------------------------------------------------------------------->
                        <!-- PANEL_CARGA_MASIVA - FIN -->
                        <!---------------------------------------------------------------------------->
                        <!---------------------------------------------------------------------------->

                        <cc1:ModalPopupExtender ID="ModalCMasivaCategoria" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="CarMasivaCategoria"
                            TargetControlID="btnPopupCategoria" DynamicServicePath="">
                        </cc1:ModalPopupExtender>

                        <asp:Button ID="btnPopupCategoria" runat="server" CssClass="alertas" Width="0px" />
                        





                        <!-- PANEL_CATEGORIA_CLIENTE - INI -->
                        <asp:Panel ID="Asignar_categoria_x_cliente" runat="server" Display = "block">
                        </asp:Panel>
                        <!-- PANEL_CATEGORIA_CLIENTE - FIN -->

                        <cc1:ModalPopupExtender ID="Modal_categoria_x_cliente" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="CarMasivaCategoria"
                            TargetControlID="btnPopupCategoria" DynamicServicePath="">
                        </cc1:ModalPopupExtender>

                    </ContentTemplate>
                </cc1:TabPanel>                     
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_CATEGORIA - FIN -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->

            </cc1:TabContainer>

            

            <asp:Panel ID="Alertas" runat="server" Style="display: none;" 
                DefaultButton="BtnAceptarAlert" Height="215px" Width="375px">

                <table align="center">
                    <tr>
                        <td align="center" class="style50" valign="top"><br /></td>
                        <td class="style49" valign="top"><br />
                            <asp:Label ID="LblAlert" runat="server" Text="Señor Usuario" CssClass="labelsMensaje"></asp:Label><br /><br />
                            <asp:Label ID="LblFaltantes" runat="server" CssClass="labelsMensaje"></asp:Label>
                        </td>
                    </tr>
                </table>

                <table align="center">
                    <tr><td><asp:Button ID="BtnAceptarAlert" runat="server" CssClass="buttonPlan" Text="Aceptar" /></td></tr>
                </table>

            </asp:Panel>


            <cc1:ModalPopupExtender ID="ModalPopupAlertas" runat="server" BackgroundCssClass="modalBackground"
                DropShadow="True" Enabled="True" OkControlID="BtnAceptarAlert" PopupControlID="Alertas"
                TargetControlID="Btndisparaalertas">
            </cc1:ModalPopupExtender>

            <asp:Button ID="Btndisparaalertas" runat="server" CssClass="alertas" Text="" Visible="true" Width="0px" />

        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>