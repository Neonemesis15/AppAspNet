<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestiónProducto.aspx.cs" Inherits="SIGE.Pages.Modulos.Administrativo.GestiónProducto" Culture="Auto" UICulture="Auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

            <cc1:TabContainer ID="TabAdministradorProductos" runat="server" ActiveTabIndex="7"
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
                        
                        <!-- HEADER - INI -->
                        <br /><br />
                        <div class="centrarcontenido">
                            <span class="labelsTit2">Gestión de Categorías de Productos</span>&nbsp;
                        </div>
                        <br /><br />
                        <!-- HEADER - FIN -->

                        <!-- BODY - INI -->
                        <div class="centrar">
                            <div class="tabla centrar">
                                <fieldset>
                                    <legend style="">Categoría de Producto</legend>
                                    <div class="fila">
                                        <div class="celda">
                                            <span class="labels">Código*</span>&nbsp;
                                        </div>
                                        <div class="celda">
                                            <asp:TextBox ID="TxtCodProductType" runat="server" BackColor="#DDDDDD" ReadOnly="True"
                                                Width="190px" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda">
                                            <asp:Label ID="LblNomProductType" runat="server" CssClass="labels" Text="Nombre *"></asp:Label>
                                        </div>
                                        <div class="celda">
                                            <asp:TextBox ID="TxtNomProductType" runat="server" MaxLength="50" Width="190px" Enabled="False">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda">
                                            <asp:Label ID="Lblgroupcategory" runat="server" CssClass="labels" Text="Grupo ">
                                            </asp:Label>
                                        </div>
                                        <div class="celda">
                                            <asp:TextBox ID="TxtgroupCategory" runat="server" MaxLength="50" Width="190px" Enabled="False">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda">
                                            <asp:Label ID="lbl_cliente" runat="server" CssClass="labels" Text="Cliente *">
                                            </asp:Label>
                                        </div>
                                        <div class="celda">
                                            <asp:DropDownList ID="cmb_categorias_cliente" runat="server" Width="180px" 
                                                Enabled="False">
                                            </asp:DropDownList>
                                        </div>
                                    </div><br /><br />
                                </fieldset>
                            </div>
                        </div>
                        <!-- BODY - FIN -->

                        <!-- PANEL_RESULTADO_BUSQUEDA- INI -->
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
                                                <asp:TextBox ID="TxtNomProductType" runat="server" Width="150px" >       
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
                        <!-- PANEL_RESULTADO_BUSQUEDA- FIN -->

                        <cc1:ModalPopupExtender ID="MopopConsulCate" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="CosultaGVCategoria"
                            TargetControlID="btnPopupGVcategoria" DynamicServicePath="">
                        </cc1:ModalPopupExtender>

                        <asp:Button ID="btnPopupGVcategoria" runat="server" CssClass="alertas" Width="0px" />
                        
                        <!-- SECCION_OPCIONES- INI -->
                        <br /><br />
                        <div class="tabla centrar">
                            <div class="fila">
                                <div class="celda centrarcontenido">
                                    <asp:Button ID="BtnCrearProductType" runat="server" CssClass="buttonPlan" Text="Crear"
                                        Width="95px" OnClick="BtnCrearProductType_Click" />
                                    <asp:Button ID="BtnSaveProductType"
                                        runat="server" CssClass="buttonPlan" Text="Guardar" Visible="False" Width="95px"
                                        OnClick="BtnSaveProductType_Click" />
                                    <asp:Button ID="BtnConsultaProductType" runat="server"
                                        CssClass="buttonPlan" Text="Consultar" Width="95px"/>
                                    <asp:Button ID="BtnCancelProductType" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                        Width="95px" OnClick="BtnCancelProductType_Click" />
                                    <asp:Button ID="BtnCargaMasivaCate" runat="server" CssClass="buttonPlan"
                                        Text="Carga Masiva" Width="95px"/>
                                </div>
                            </div>
                        </div>
                        <!-- SECCION_OPCIONES- FIN -->

                        <!-- PANEL_BUSQUEDA - INI -->
                        <asp:Panel ID="BuscarProductCateg" runat="server" CssClass="busqueda" DefaultButton="BtnBTypeProduct"
                            Height="211px" Style="display: none" Width="343px" >

                            <!-- HEADER_TITLE - INI -->
                            <br /><br />
                            <div class="tabla centrar">
                                <div class="fila">
                                    <div class="celda">
                                        <asp:Label ID="LbltitBProductcat" runat="server" CssClass="labelsTit2" 
                                            Text="Buscar Categoria de Producto" />
                                    </div>
                                </div>
                            </div>
                            <br /><br />
                            <!-- HEADER_TITLE - FIN -->


                            <!-- FILTROS - INI -->
                            <div class="tabla centrar">
                                <div class="fila">
                                    <div class="celda">
                                        <asp:Label ID="LblBcodProductcat" runat="server" CssClass="labels" Text="Código:" />
                                    </div>
                                    <div class="celda">
                                        <asp:TextBox ID="TxtBCodTypeProduct" runat="server" MaxLength="4" Width="80px">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <asp:Label ID="LblBNomProductcat" runat="server" CssClass="labels" Text="Nombre:" />
                                    </div>
                                    <div class="celda">
                                        <asp:TextBox ID="TxtBNomTypeProduct" runat="server" MaxLength="50" 
                                            Width="180px">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <asp:Label ID="lblClieId" runat="server" CssClass="labels" Text="Cliente:" />
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="cmb_Cliente" runat="server" Width="180px"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <!-- FILTROS - FIN -->

                            <!-- OPCIONES_BUSQUEDA - INI -->
                            <br /><br />
                            <div class="centrar centrarcontenido">
                                <asp:Button ID="BtnBTypeProduct" runat="server" CssClass="buttonPlan" Text="Buscar"
                                    Width="80px" OnClick="BtnBTypeProduct_Click" />
                                <asp:Button ID="BtnCancelBTypeProduct" runat="server" CssClass="buttonPlan" Text="Cancelar" 
                                    Width="80px" />
                            </div>
                            <!-- OPCIONES_BUSQUEDA - FIN -->
                        </asp:Panel>
                        <!-- PANEL_BUSQUEDA - FIN -->

                        <cc1:ModalPopupExtender ID="IbtnProductType" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True" OkControlID="BtnCancelBTypeProduct" PopupControlID="BuscarProductCateg"
                            TargetControlID="BtnConsultaProductType" DynamicServicePath="">
                        </cc1:ModalPopupExtender>

                        <!-- PANEL_CARGA_MASIVA - INI -->
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
                        <!-- PANEL_CARGA_MASIVA - FIN -->

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





                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_SUBCATEGORIA - INI -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <cc1:TabPanel runat="server" HeaderText="SubCategoría" ID="Panel_SubCategoria"> 
                    <HeaderTemplate>SubCategoría</HeaderTemplate>
                    <ContentTemplate>
                        <br /><br />
                        <div class="centrarcontenido">
                            <span class="labelsTit2">Gestión de SubCategorias de Producto</span>
                            &nbsp;
                        </div>
                        <br /><br />
                        <div class="centrar">
                            <div class="tabla centrar">
                                <fieldset>
                                    <legend>SubCategoria de Producto</legend>
                                    <div class="fila">
                                        <div class="celda">
                                            <asp:Label ID="labelCodSubCategoria" runat="server" CssClass="labels" Text="Código*"></asp:Label>
                                        </div>
                                        <div class="celda">
                                            <asp:TextBox ID="TxtCodSubCategoria" runat="server" BackColor="#DDDDDD" ReadOnly="True"
                                            Width="80px" Enabled="False">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda">
                                            <asp:Label ID="LblNomSubCategoria" runat="server" CssClass="labels" Text="Nombre *"></asp:Label>
                                        </div>
                                        <div class="celda">
                                            <asp:TextBox ID="TxtNomSubCategory" runat="server" MaxLength="50" Width="250px" Enabled="False"></asp:TextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtNomSubCategory"
                                                Display="None" ErrorMessage="No debe iniciar con espacio en blanco, &lt;br /&gt;No ingrese caracteres especiales "
                                                ValidationExpression="([a-zA-Z0-9][a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s]{0,49})">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda">
                                            <asp:Label ID="lblCatSubCategoria" runat="server" CssClass="labels" Text="Categoria *"></asp:Label>
                                        </div>
                                        <div class="celda">
                                            <asp:DropDownList ID="cmbCateSubCategoria" runat="server" Width="255px" Enabled="False">
                                            </asp:DropDownList>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtNomSubCategory"
                                            Display="None" ErrorMessage="No debe iniciar con espacio en blanco, &lt;br /&gt;No ingrese caracteres especiales "
                                            ValidationExpression="([a-zA-Z0-9][a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\s]{0,49})"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </fieldset>
                                &nbsp;
                            </div> 
                        </div>
                        <div class="centrar">
                            <div class="tabla centrar">
                                <div class="fila">
                                    <div class="celda">
                                        <asp:Label ID="Label5" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </div>
                                    <div class="celda">
                                        <asp:RadioButtonList ID="RBtSubCategoy" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br /><br />
                        <asp:Panel ID="ConsultaGVSubcategoria" runat="server" Style="display: block"  >  
                            <div class="centrarcontenido">
                                <div class="p" style="width:780px; height: 315px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
	                                font-family : arial, Helvetica, sans-serif;">
                                    <asp:GridView ID="GvConsultaSubcategoria" runat="server" AutoGenerateColumns="False" 
                                        Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                        Width="100%" onrowediting="GvConsultaSubcategoria_RowEditing" 
                                        onrowcancelingedit="GvConsultaSubcategoria_RowCancelingEdit" onrowupdating="GvConsultaSubcategoria_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Cod.SubCategoria">
                                                <EditItemTemplate>
                                                    <asp:Label ID="LblsubCategory" runat="server" Text='<%# Bind("id_Subcategory") %>'>
                                                    </asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblsubCategory" runat="server" Text='<%# Bind("id_Subcategory") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="70px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Categoria">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="cmbCateSubCategory" runat="server" Width="130px" >                                       
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCatsubcategoria" runat="server" Width="130px" Text='<%# Bind("Product_Category") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SubCategoria">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TxtSubcategoria" runat="server" Width="130px"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSubcategoria" runat="server"  Width="130px" Text='<%# Bind("Name_Subcategory") %>'></asp:Label>                                                      
                                                </ItemTemplate>
                                            </asp:TemplateField>                                                                             
                                            <asp:TemplateField HeaderText="Estado" >
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="Chesubcategoria" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Chesubcategoria" runat="server"  Enabled="false" Checked ='<%# Bind("Subcategory_Status") %>' ></asp:CheckBox> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="True" />
                                        </Columns>
                                    </asp:GridView>
                                    <br /><br />                                                 
                                    <div class="centrar">
                                        <div class="centrar centrarcontenido">
                                            <asp:Label ID="Label3" runat="server" CssClass="labels" Text="Exportar a Excel "></asp:Label>
                                        </div>
                                        <iframe id="iframeSubcategory" runat="server" src="pruebaexcel.aspx" frameborder="0"  width="64px" height="64px"  ></iframe>
                                        <div class="centrar">                                       
                                            <asp:Button ID="BtncancelsubC" runat="server" CssClass="buttonPlan"
                                            Text="Cancelar" Width="80px" onclick="btnCancelar_Click" />
                                        </div> 
                                    </div>  
                                </div>
                            </div>
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalCoSub" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="ConsultaGVSubcategoria"
                            TargetControlID="btnPopupGVCSubcategoria" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="btnPopupGVCSubcategoria" runat="server" CssClass="alertas" Width="0px" />                       
                        <div class="centrarcontenido">
                            <asp:Button ID="BtnCrearSubCategory" runat="server" CssClass="buttonPlan" Text="Crear"
                                    Width="95px" OnClick="BtnCrearSubCategory_Click" />
                            <asp:Button ID="BtnGuardarSubCategory"
                                        runat="server" CssClass="buttonPlan" Text="Guardar" Visible="False" Width="95px"
                                        OnClick="BtnGuardarSubCategory_Click" />
                            <asp:Button ID="BtnConsultarSubCategory"
                                            runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px" />
                            <asp:Button ID="BtnCancelarSubCategory"
                                                        runat="server" CssClass="buttonPlan" Text="Cancelar" Width="95px" OnClick="BtnCancelarSubCategory_Click" />
                            <asp:Button ID="BtnCargaMasuSubcategoria" runat="server" CssClass="buttonPlan"
                                                            Text="Carga Masiva" Width="95px" onclick="BtnCargaMasuSubcategoria_Click"/>
                        </div>    
                        <asp:Panel ID="BuscarSubCategoria" runat="server" CssClass="busqueda" DefaultButton="BtnBusSubCategory"
                            Style="display: none;" Height="202px" Width="343px">
                            <br /><br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblBSubCategory" runat="server" CssClass="labelsTit2" Text="Buscar SubCategoria de Producto" />
                                    </td>
                                </tr>
                            </table>
                            <br /><br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNomSubCategory" runat="server" CssClass="labels" Text="Nombre:" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtBNomSubCategory" runat="server" MaxLength="50" Width="210px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCategoria" runat="server" CssClass="labels" Text="Categoria:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbBCategoriaSC" runat="server" Width="215px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <br /><br />
                            <div align="center">
                                <asp:Button ID="BtnBusSubCategory" runat="server" CssClass="buttonPlan" Text="Buscar"
                                    Width="80px" OnClick="BtnBusSubCategory_Click" />
                                <asp:Button ID="CancelaSubCategory"
                                        runat="server" CssClass="buttonPlan" Text="Cancelar" Width="80px" />
                            </div>
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalPopupSubCategoria" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True" OkControlID="CancelaSubCategory" PopupControlID="BuscarSubCategoria"
                            TargetControlID="BtnConsultarSubCategory" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="CargaMasivaSubCategoria" runat="server" Style="vertical-align: middle; " BackColor="#7F99CC" BorderColor="#7F99CC"
                            BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt">
                            <div style="position:absolute; z-index:1;  width:100%;" align="right" > 
                                <asp:ImageButton ID="ImgCMSubCategoria" runat="server" AlternateText="Cerrar Ventana"
                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px"/>
                            </div>
                            <div  align="center">  
                                <div>
                                    <asp:Label ID="Lblexexcelsubcategoria" runat="server" CssClass="labels" Text="Exportar a Excel "></asp:Label>
                                </div>         
                                <iframe id="IfCMSubcategoria" runat="server" height="230px" src="" width="500px">
                                </iframe>                                       
                            </div>                                                           
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalCMSubcategoria" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="CargaMasivaSubCategoria"
                            TargetControlID="btnPopupSubCategoria" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="btnPopupSubCategoria" runat="server" CssClass="alertas" Width="0px" />
                    </ContentTemplate>
                </cc1:TabPanel> 
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_SUBCATEGORIA - FIN -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->






                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_MARCA - INI -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <cc1:TabPanel runat="server" HeaderText="Marcas" ID="Panel_Marcas">
                    <HeaderTemplate>Marcas</HeaderTemplate>
                    <ContentTemplate>
                        
                        <!-- HEADER - INI -->
                        <br /><br />
                        <div class="centrarcontenido">
                            <span class="labelsTit2">Gestión de Marcas de Producto</span>&nbsp;
                        </div>
                        <br /><br />
                        <!-- HEADER - FIN -->

                        <!-- BODY - INI -->
                        <div class="centrar">  
                            <div class="tabla centrar">                                                                      
                                <fieldset>
                                    <legend>Marca de Producto</legend>
                                        <div class="fila">
                                            <div class="celda">
                                                <span class="labels">Código*</span>&nbsp;
                                            </div>
                                            <div class="celda">
                                                <asp:TextBox ID="TxtCodBrand" runat="server" BackColor="#DDDDDD" ReadOnly="True"
                                                    Width="80px" Enabled="False"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="celda">
                                                <span class="labels">Cliente*</span>&nbsp;
                                            </div>
                                            <div class="celda">
                                                <asp:DropDownList ID="cmbClienteMarca" runat="server" Height="21px" Width="195px"
                                                    Enabled="False" AutoPostBack="True" 
                                                    onselectedindexchanged="cmbClienteMarca_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="celda">
                                                <span class="labels">Categoría*</span>&nbsp;
                                            </div>
                                            <div class="celda">
                                                <asp:DropDownList ID="cmbCategoryMarca" runat="server" Height="21px" Width="195px"
                                                    Enabled="False">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="celda">
                                                <span class="labels">Nombre*</span>&nbsp;
                                            </div>
                                            <div class="celda">
                                                <asp:TextBox ID="TxtNomBrand" runat="server" MaxLength="50" Width="190px" Enabled="False">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                </fieldset>&nbsp;
                            </div>                                   
                        </div>
                        <!-- BODY - FIN -->

                        <!-- PANEL_RESULTADO_BUSQUEDA- INI -->
                        <div class="centrarcontenido">
                            <asp:Panel ID="CosultaGVMarca" runat="server" Style="display: block;"  >                        
                                <div class="p" style=  "width:780px; height: 300px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
	                                font-family : arial, Helvetica, sans-serif;"> 
                                    <asp:GridView ID="GVConsultaMarca" runat="server" AutoGenerateColumns="False"  
                                        Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                        Width="100%"  
                                        onpageindexchanging="GVConsultaMarca_PageIndexChanging" 
                                        onrowediting="GVConsultaMarca_RowEditing" 
                                        onrowcancelingedit="GVConsultaMarca_RowCancelingEdit" 
                                        onrowupdating="GVConsultaMarca_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Cod.Marca">
                                                <EditItemTemplate>
                                                        <asp:Label ID="TxtCodBrand" runat="server"  Text='<%# Bind("id_Brand") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCodBrand2" runat="server" Text='<%# Bind("id_Brand") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="70px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cliente">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="cmbClienteMarca" runat="server" Width="150px" >       
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClientecMarca" runat="server" Width="150px" ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Categoria">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="cmbCategoryMarca" runat="server" Width="150px" >       
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblidcategoria" runat="server"  Width="150px" ></asp:Label>                                                      
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Marca">
                                                <EditItemTemplate>                                                   
                                                    <asp:TextBox ID="TxtNomBrand" runat="server" Width="150px" Enabled="true"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcMArca" runat="server" Width="150px"  Text='<%# Bind("Name_Brand") %>'></asp:Label>                                                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>                                     
                                            <asp:TemplateField HeaderText="Estado" >
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="CheckEMarca" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckEMarca" runat="server"  Enabled="false" Checked ='<%# Bind("Brand_Status") %>'  ></asp:CheckBox> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="True" />
                                        </Columns>                                                
                                    </asp:GridView>
                                    <br /><br />
                                    <div class="centrarcontenido">
                                        <div>
                                            <asp:Label ID="Lablexecelmarca" runat="server" CssClass="labels" Text="Exportar a Excel "></asp:Label>
                                        </div>
                                        <iframe id="iframeexcel" runat="server" src="pruebaexcel.aspx" frameborder="0"  width="64px" height="64px"></iframe>
                                        <div>                                       
                                            <asp:Button ID="btnCancelar" runat="server" CssClass="buttonPlan"
                                                Text="Cancelar" Width="80px" onclick="btnCancelar_Click" />
                                        </div> 
                                    </div>                                                               
                                </div>
                            </asp:Panel>
                        </div>
                        <!-- PANEL_RESULTADO_BUSQUEDA- FIN -->

                        <cc1:ModalPopupExtender ID="ModalGVConsultaMarca" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="CosultaGVMarca"
                            TargetControlID="btnPopupGVCMarcas" DynamicServicePath="">
                        </cc1:ModalPopupExtender>

                        <asp:Button ID="btnPopupGVCMarcas" runat="server" CssClass="alertas" Width="0px" />

                        <!-- SECCION_OPCIONES- INI -->
                        <br /><br />
                        <div class="centrarcontenido">
                            <asp:Button ID="BtnCrearBrand" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px" 
                                OnClick="BtnCrearBrand_Click" />
                            <asp:Button ID="BtnSaveBrand" runat="server" CssClass="buttonPlan" Text="Guardar" Visible="False" Width="95px" 
                                OnClick="BtnSaveBrand_Click" />
                            <asp:Button ID="BtnConsultaBrand" runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px" />
                            <asp:Button ID="BtnCancelBrand" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="95px" 
                                OnClick="BtnCancelBrand_Click" />
                            <asp:Button ID="BtnCargaMasiva" runat="server" CssClass="buttonPlan" Text="Carga Masiva" Width="95px" 
                                OnClick="BtnCargaMasiva_Click" />
                        </div>
                        <!-- SECCION_OPCIONES- FIN -->

                        <!-- PANEL_BUSQUEDA - INI -->
                        <asp:Panel ID="BuscarBrand" runat="server" CssClass="busqueda" DefaultButton="BtnBBrand" 
                            Style=" display: none;" Height="202px" Width="343px">
                            <div class="centrarcontenido">
                                <span class="labelsTit2">Buscar Marca de Producto</span>&nbsp;
                            </div>
                            <br /><br />
                            <div class="centrar">
                                <div class="tabla centrar">
                                    <div class="fila">
                                        <div class="celda">
                                            <asp:Label ID="LblBCodBrand" runat="server" CssClass="labels" Text="Código:" />
                                        </div>
                                        <div class="celda">
                                            <asp:TextBox ID="TxtBCodBrand" runat="server" MaxLength="5" Width="80px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="ReqBCodBrand" runat="server" ControlToValidate="TxtBCodBrand"
                                            Display="None" ErrorMessage="el código debe numérico" ValidationExpression="([0-9]{1,4})">
                                            </asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender
                                                ID="VCEReqBCodBrand" runat="server" Enabled="True" TargetControlID="ReqBCodBrand">
                                            </cc1:ValidatorCalloutExtender>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda">
                                            <asp:Label ID="LblBuscarCategory" runat="server" CssClass="labels" Text="Categoria*"></asp:Label>
                                        </div>
                                        <div class="celda">
                                            <asp:DropDownList ID="cmbBuscarCategoryM" runat="server" Height="21px" Width="180px">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda">
                                            <asp:Label ID="LblBNomBrand" runat="server" CssClass="labels" Text="Nombre:" />
                                        </div>
                                        <div class="celda">
                                            <asp:TextBox ID="TxtBNomBrand" runat="server" MaxLength="50" Width="180px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br /><br />
                            <div class="centrarcontenido">
                                <asp:Button ID="BtnBBrand" runat="server" CssClass="buttonPlan" Text="Buscar" Width="80px" 
                                    OnClick="BtnBBrand_Click" />
                                <asp:Button ID="BtnCancelBBrand" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="80px" />
                            </div>
                        </asp:Panel>
                        <!-- PANEL_BUSQUEDA - FIN -->

                        <cc1:ModalPopupExtender ID="IbtnBrand" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" PopupControlID="BuscarBrand"
                            CancelControlID="BtnCancelBBrand"
                            TargetControlID="BtnConsultaBrand" DynamicServicePath="" Enabled="True">
                        </cc1:ModalPopupExtender>
                        
                        <!-- PANEL_CARGA_MASIVA - INI -->
                        <asp:Panel ID="CargaMasiva" runat="server" Style="vertical-align: middle; " BackColor="#7F99CC" BorderColor="#7F99CC"
                            BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt">
                            <div style="position:absolute; z-index:1;  width:100%;" align="right" > 
                                <asp:ImageButton ID="BtnCargaMAsivaMarca" runat="server" AlternateText="Cerrar Ventana"
                                    ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                    ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px"  
                                    OnClick="BtnCargaMAsivaMarca_Click"  />
                            </div>
                            <div  align="center">                                   
                                <iframe id="IfCargaMasivaGProductos" runat="server" height="230px" src="" width="500px">
                                </iframe>                                       
                            </div>                                                           
                        </asp:Panel>
                        <!-- PANEL_CARGA_MASIVA - FIN -->

                        <cc1:ModalPopupExtender ID="ModalCMasiva" runat="server" BackgroundCssClass="modalBackground" DropShadow="True" 
                            Enabled="True"  PopupControlID="CargaMasiva" TargetControlID="btnPopupMarcas" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="btnPopupMarcas" runat="server" CssClass="alertas" Width="0px" />                          
                    </ContentTemplate>  
                </cc1:TabPanel>
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_MARCA - FIN -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->








                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_SUBMARCA - INI -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <cc1:TabPanel runat="server" HeaderText="SubMarcas" ID="Panel_Submarcas">
                    <HeaderTemplate>
                        SubMarcas
                    </HeaderTemplate>
                    <ContentTemplate>
                        <br /><br />
                        <div class="centrarcontenido">
                            <span class="labelsTit2">Gestión de SubMarcas de Producto</span>
                            &nbsp;
                        </div>                                      
                        <br /><br />
                        <table align="center">
                            <tr>
                                <td>
                                    <fieldset id="FlSubBrand" runat="server">
                                        <legend style="">SubMarca de Producto</legend>
                                        <br /><br />
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblCodSubMarca" runat="server" CssClass="labels" Text="Código*"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtCodSubMarca" runat="server" BackColor="#DDDDDD" ReadOnly="True"
                                                        Width="80px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblCategoriaSubMarca" runat="server" CssClass="labels" Text="Categoria*"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbCategoriaSubmarca" runat="server" Width="255px" Enabled="False"
                                                        AutoPostBack="True" OnSelectedIndexChanged="cmbCategoriaSubmarca_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblSelBrand" runat="server" CssClass="labels" Text="Marca *"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CmbSelBrand" runat="server" Width="255px" Enabled="False">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblNomSubMarca" runat="server" CssClass="labels" Text="Nombre *"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtNomSubMarca" runat="server" MaxLength="50" Width="250px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br /><br />
                                    </fieldset>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="LblStatusSubBrand" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RbtnStatusSubBrand" runat="server" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False">
                                        <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                        <asp:ListItem>Deshabilitado</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <br /><br />
                        <asp:Panel ID="ConsultaGVSubmarca" runat="server" Style="display: block"  >  
                            <div class="centrarcontenido">
                                <div class="p" style="width:780px; height: 315px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
	                                    font-family : arial, Helvetica, sans-serif;">
                                    <asp:GridView ID="GvConsultaSubmarca" runat="server" AutoGenerateColumns="False" 
                                            Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                            Width="100%" onrowcancelingedit="GvConsultaSubmarca_RowCancelingEdit" 
                                            onrowediting="GvConsultaSubmarca_RowEditing" 
                                            onrowupdating="GvConsultaSubmarca_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Cod.SubMarca">
                                                <EditItemTemplate>
                                                        <asp:Label ID="LblsubCategory" runat="server" Text='<%# Bind("id_SubBrand") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblsubCategory" runat="server" Text='<%# Bind("id_SubBrand") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="70px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Categoria">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="cmbCateSubMarca" runat="server" Width="130px" AutoPostBack="true"
                                                        Enabled="true" onselectedindexchanged="cmbCateSubMarca_SelectedIndexChanged" >                                       
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="cmbCateSubMarca" runat="server" Width="130px" Enabled="false"  ></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Marca">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="cmbMarcaSubMarca" runat="server" Width="130px"  Enabled="true"></asp:DropDownList>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="cmbMarcaSubMarca" runat="server" Enabled="false"  Width="130px" ></asp:DropDownList>                                                      
                                                </ItemTemplate>
                                            </asp:TemplateField>      
                                            <asp:TemplateField HeaderText="SubMarca">
                                                <EditItemTemplate>
                                                    <asp:Textbox ID="txtSubMarca" runat="server" Width="130px"  Enabled="true"></asp:Textbox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:label ID="lblSubMarca" runat="server"  Width="130px" Enabled="false" Text ='<%# Bind("Name_SubBrand") %>' ></asp:label>                                                      
                                                </ItemTemplate>
                                            </asp:TemplateField>                                                                            
                                            <asp:TemplateField HeaderText="Estado" >
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="ChecksubMarca" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChecksubMarca" runat="server"  Enabled="false" Checked ='<%# Bind("SubBrand_Status") %>' ></asp:CheckBox> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="True" />
                                        </Columns>
                                    </asp:GridView>
                                    <br /><br />
                                    <asp:Label ID="lblexexcelsubmarca" runat="server" Text='Exportar a Excel'></asp:Label>
                                    <br /><br />
                                    <iframe id="iframeexcelsubmarca" runat="server" src="pruebaexcel.aspx" frameborder="0"  width="64px" height="64px"  ></iframe> 
                                    <br /><br />
                                    <asp:Button ID="btncgvCSubmarca" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="80px" onclick="btnCancelar_Click" /> 
                                    <br /><br />
                                </div>
                            </div>
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalpConsGVSubmarca" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="ConsultaGVSubmarca"
                            TargetControlID="btnPopupGVCSubMarca" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="btnPopupGVCSubMarca" runat="server" CssClass="alertas"
                            Width="0px" />     
                        <div class="centrarcontenido">
                            <asp:Button ID="BtnCrearSubBrand" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px" OnClick="BtnCrearSubBrand_Click" />
                            <asp:Button ID="BtnsaveSubBrand" runat="server" CssClass="buttonPlan" Text="Guardar" Visible="False" Width="95px" OnClick="BtnsaveSubBrand_Click" />
                            <asp:Button ID="BtnSearchSubBrand" runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px" />
                            <asp:Button ID="BtnCancelSubBrand" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="95px" OnClick="BtnCancelSubBrand_Click" />
                            <asp:Button ID="BtnCMSubmarca" runat="server" CssClass="buttonPlan" Text="Carga Masiva" Width="95px"  onclick="BtnCMSubmarca_Click" />
                        </div>
                        <asp:Panel ID="BuscarSubBrand" runat="server" CssClass="busqueda" DefaultButton="BtnBSubBrand"
                            Style="display: none;" Height="202px" Width="343px">
                            <br /><br />
                            <div class="centrarcontenido">
                                <asp:Label ID="LblTitBSubBrand" runat="server" CssClass="labelsTit2" Text="Buscar SubMarca de Producto" />
                            </div>
                            <br /><br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblCategorySubmarca" runat="server" CssClass="labels" Text="Categoria:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbCategorySubmarca" runat="server" Width="215px" AutoPostBack="True"
                                            OnSelectedIndexChanged="cmbCategorySubmarca_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblBSelBrand" runat="server" CssClass="labels" Text="Marca:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CmbBSelBrand" runat="server" Width="215px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblBNomSubBrand" runat="server" CssClass="labels" Text="Nombre:" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtBNomSubBrand" runat="server" MaxLength="50" Width="210px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br /><br />
                            <div class="centrarcontenido"> 
                                <asp:Button ID="BtnBSubBrand" runat="server" CssClass="buttonPlan" Text="Buscar" Width="80px" OnClick="BtnBSubBrand_Click" />
                                <asp:Button ID="BtnCancelBSubBrand" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="80px" />
                            </div>       
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="IbtnSubBrand" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True" PopupControlID="BuscarSubBrand"
                            TargetControlID="BtnSearchSubBrand" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="PnCaMaSubmarca" runat="server" Style="vertical-align: middle; " BackColor="#7F99CC" BorderColor="#7F99CC"
                            BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt">
                            <div style="position:absolute; z-index:1;  width:100%;" align="right" > 
                                <asp:ImageButton ID="btncerracmsubmarca" runat="server" AlternateText="Cerrar Ventana"
                                            ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                            ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px"/>
                            </div>
                            <div  align="center">                                                                
                                <iframe id="IframeCMSubmarca" runat="server" height="230px" src="" width="500px">
                                </iframe>                                       
                            </div>                                                           
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalCaMaPOSubmarca" runat="server" BackgroundCssClass="modalBackground"
                        DropShadow="True" Enabled="True"  PopupControlID="PnCaMaSubmarca"
                        TargetControlID="btnPopupSubMarca" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="btnPopupSubMarca" runat="server" CssClass="alertas"
                        Width="0px" />                     
                    </ContentTemplate>
                </cc1:TabPanel>
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_SUBMARCA - FIN -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->







                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_PRESENTACION - INI -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <cc1:TabPanel runat="server" HeaderText="Presentación" ID="Panel_Presentación">
                    <HeaderTemplate>
                        Presentación
                    </HeaderTemplate>
                    <ContentTemplate>
                        <br /><br />
                        <div class="centrarcontenido">
                            <span class="labelsTit2">Gestión de Presentación de Producto</span>
                            &nbsp;
                        </div>  
                        <br /><br />
                        <div class="centrar">
                            <div class="tabla centrar">
                                <fieldset>
                                <legend style="">Presentación de Producto</legend>
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labels">Código*</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:TextBox ID="TxtCodPresen" runat="server" BackColor="#DDDDDD" ReadOnly="True"
                                                    Width="80px"></asp:TextBox>

                                    </div>
                                    <div class="celda">
                                        <span class="labels">Categoria *</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="cmbCategoryPresent" runat="server" Width="180px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="cmbCategoryPresent_SelectedIndexChanged" 
                                            Enabled="False"></asp:DropDownList>

                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labels">Marca *</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="cmbMarcaPresent" runat="server" Enabled="False" 
                                            Width="180px"></asp:DropDownList>
                                    </div>
                                    <div class="celda">
                                        <span class="labels">SubCategoria</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="cmbSubCategoryPresent" runat="server" Enabled="False" 
                                            Width="180px"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labels">Empaquetamiento*</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:TextBox ID="TexEmpPresent" runat="server" Enabled="False" MaxLength="50" Width="174px"
                                            ToolTip="Ingresar tipo de Empaque del producto"></asp:TextBox>
                                    </div>
                                    <div class="celda">
                                        <span class="labels">Unidad por Empaque*</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:TextBox ID="TexUnidadEpresent" runat="server" Enabled="False" MaxLength="50"
                                            Width="174px" CausesValidation="True" 
                                            ToolTip="ingresar cantidad de unidades por Empaque"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="validator_uni_empaque" runat="server" 
                                            ControlToValidate="TexUnidadEpresent" 
                                            ErrorMessage="Ingrese sólo valores numéricos" Display="None" 
                                            ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender33" runat="server" Enabled="True"
                                                    TargetControlID="validator_uni_empaque">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>                                           
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labels">Contenido Neto *</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:TextBox ID="TxtConteNeto" runat="server" Enabled="False" MaxLength="10" Width="174px"
                                            ToolTip="Ingrese Peso de Producto"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="validator_cont_neto" runat="server" 
                                            ControlToValidate="TxtConteNeto" 
                                            ErrorMessage="Ingrese sólo valores numéricos" Display="None" 
                                            ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                    TargetControlID="validator_cont_neto">
                                        </cc1:ValidatorCalloutExtender>
                                    </div> 
                                    <div class="celda">
                                        <span class="labels">Unidad de medida *</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="CmbUnidNeto" runat="server" Enabled="False" Width="180px"
                                            ToolTip="Selecciona unidad de medida del producto"></asp:DropDownList>
                                    </div>                                         
                                </div>  
                            </div>
                        </div>
                        <div class="tabla centrar">
                            <fieldset>
                                <div class="fila">
                                    <div class="celda">
                                            <span class="labels">Nombre *</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                            <asp:TextBox ID="TxtNomPresen" runat="server" Enabled="False" ToolTip="Nombre de la Presentación." Width="180px"></asp:TextBox>
                                    </div>                                            
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labels">Estado</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:RadioButtonList ID="RbtnPresenStatus" runat="server" Enabled="False" 
                                            Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="Black" 
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>                                            
                                </div>
                            </fieldset>
                            &nbsp;
                        </div>    
                        <br /><br />
                        <asp:Panel ID="grid_presentacion_p" runat="server" Style="display: block" >
                            <div class="p" style="width:780px; height: 330px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
	                                    font-family : arial, Helvetica, sans-serif;">
                                <asp:GridView ID="grid_bpresentacion" runat="server" AutoGenerateColumns="False" 
                                                EnableModelValidation="True" PageSize="5" 
                                                onrowcancelingedit="grid_bpresentacion_RowCancelingEdit" 
                                                onrowdeleting="grid_bpresentacion_RowDeleting" 
                                                onrowediting="grid_bpresentacion_RowEditing" 
                                                onrowupdating="grid_bpresentacion_RowUpdating">
                                    <Columns>
                                        <asp:BoundField DataField="id_ProductPresentation" HeaderText="Código" 
                                            ReadOnly="True" />
                                        <asp:TemplateField HeaderText="Categoría">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_bcategoria" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bcategoria" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subcategoría">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_bsubcategoria" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bsubcategoria" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Marca">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_bmarca" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bmarca" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Empaquetamiento">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_bempaque" runat="server" 
                                                    Text='<%# Bind("Empaquetamiento") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bempaque" runat="server" 
                                                    Text='<%# Bind("Empaquetamiento") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unidad de Empaque">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_buniemp" runat="server" 
                                                    Text='<%# Bind("Unidad_Empaque") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_buniemp" runat="server" Text='<%# Bind("Unidad_Empaque") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre Presentación">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_bnpresent" runat="server" 
                                                    Text='<%# Bind("ProductPresentationName") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bnpresent" runat="server" 
                                                    Text='<%# Bind("ProductPresentationName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contenido Neto">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_bcneto" runat="server" 
                                                    Text='<%# Bind("ProductPresentation_Neto") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bcneto" runat="server" 
                                                    Text='<%# Bind("ProductPresentation_Neto") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unidad de Medida">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_bumedida" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bumedida" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estado">
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="cbx_estado" runat="server" 
                                                    Checked='<%# Bind("ProductPresentation_Status") %>' />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbx_estado" runat="server" 
                                                    Checked='<%# Bind("ProductPresentation_Status") %>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                    </Columns>
                                </asp:GridView>
                                <br /><br />
                                <div class="centrarcontenido">
                                    <div class="centrarcontenido">
                                        <span class="labels">Exportar a Excel</span>
                                        &nbsp;
                                    </div>
                                    <iframe id="iframe2" runat="server" src="pruebaexcel.aspx" frameborder="0"  width="64px" height="64px"  ></iframe>
                                    <div class="centrarcontenido">                                       
                                        <asp:Button ID="btncancelgvpresent" runat="server" CssClass="buttonPlan"
                                        Text="Cancelar" OnClick="btnCancelar_Click" Width="80px" />
                                    </div> 
                                </div>  
                            </div>                            
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="mpe_grid_presentacion" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True" PopupControlID="grid_presentacion_p"
                            TargetControlID="btnPopUpGrid" DynamicServicePath=""></cc1:ModalPopupExtender>
                        <asp:Button ID="btnPopUpGrid" runat="server" CssClass="alertas"
                            Width="0px" />
                        <div class="centrarcontenido">
                            <asp:Button ID="BtnCrearPresen" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px" OnClick="BtnCrearPresen_Click" />
                            <asp:Button ID="BtnSavePresen" runat="server" CssClass="buttonPlan" Text="Guardar" Visible="False" Width="95px" OnClick="BtnSavePresen_Click" />
                            <asp:Button ID="BtnConsultaPresen" runat="server" CssClass="buttonPlan" 
                                Text="Consultar" Width="95px"/>
                            <asp:Button ID="BtnCancelPresent" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="95px" OnClick="BtnCancelPresent_Click" />
                        </div>
                        <asp:Panel ID="BuscarPresentacion" runat="server" CssClass="busqueda" DefaultButton="BtnBPresent"
                            Height="219px" Width="343px" Style="display: none">
                            <div class="centrarcontenido">
                                <span class="labelsTit2">Buscar Presentación</span>
                                &nbsp;
                            </div>
                            <br /><br />
                            <div class="centrar">
                                <div class="tabla centrar">
                                    <div class="fila">
                                        <div class="celda">
                                            <span class="labels">Categoria:</span>
                                            &nbsp;
                                        </div>
                                        <div class="celda">
                                            <asp:DropDownList ID="cmbBCategoriaPresent" runat="server" Width="200px" AutoPostBack="True"
                                                OnSelectedIndexChanged="cmbBCategoriaPresent_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda">
                                            <span class="labels">Marca:</span>
                                            &nbsp;
                                        </div>
                                        <div class="celda">
                                            <asp:DropDownList ID="cmbBMarcaPresent" runat="server" Width="200px"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda">
                                            <span class="labels">Nombre:</span>
                                            &nbsp;
                                        </div>
                                        <div class="celda">
                                            <asp:TextBox ID="TxtBNomPresen" runat="server" MaxLength="50" Width="193px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br /><br />
                            <div class="centrarcontenido">
                                <asp:Button ID="BtnBPresent" runat="server" CssClass="buttonPlan" OnClick="BtnBPresent_Click" Text="Buscar" Width="80px" />
                                <asp:Button ID="BtnCancelBPresen" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="80px" />
                            </div>
                        </asp:Panel>

                        <cc1:ModalPopupExtender ID="IbtnPresen_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True" OkControlID="BtnCancelBPresen" PopupControlID="BuscarPresentacion"
                            TargetControlID="BtnConsultaPresen" DynamicServicePath=""></cc1:ModalPopupExtender>                        
                    </ContentTemplate>            
                </cc1:TabPanel>
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_PRESENTACION - FIN -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->







                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_FAMILIA - INI -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <cc1:TabPanel runat="server" HeaderText="Familia" ID="Panel_ProductFamily">
                    <HeaderTemplate>
                        Familia
                    </HeaderTemplate>
                    <ContentTemplate>
                        <br /><br />
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="LblTitFamily" runat="server" CssClass="labelsTit2" Text="Gestión Familia de Producto"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br /><br />
                        <table align="center">
                            <tr>
                                <td>                                     
                                    <fieldset id="FlFamilia" runat="server">
                                        <legend style="">Familia</legend>                                        
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblCodFamily" runat="server" CssClass="labels" Text="Código*"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtCodFamily" runat="server" BackColor="White" Width="80px" Enabled="False"></asp:TextBox>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblClienteFamily" runat="server" CssClass="labels" Text="Cliente*"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbClienteFamily" runat="server" Width="255px" Enabled="False"
                                                        AutoPostBack="True" OnSelectedIndexChanged="cmbClienteFamily_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblCategoriaFamily" runat="server" CssClass="labels" Text="Categoria *"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbCategoryFamily" runat="server" Width="255px" Enabled="False"
                                                        AutoPostBack="True" OnSelectedIndexChanged="cmbCategoryFamily_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblSubCategoryFamily" runat="server" CssClass="labels" Text="SubCategoria *"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CmbSubCategoryFamily" runat="server" Width="255px" Enabled="False"
                                                        AutoPostBack="True" OnSelectedIndexChanged="CmbSubCategoryFamily_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblMarcaFamily" runat="server" CssClass="labels" Text="Marca *"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CmbMarcaFamily" runat="server" Width="255px" Enabled="False"
                                                        AutoPostBack="True" OnSelectedIndexChanged="CmbMarcaFamily_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblSubmarcaFamily" runat="server" CssClass="labels" Text="SubMarca*"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CmbSubmarcaFamily" runat="server" Width="255px" Enabled="False">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblNomFamily" runat="server" CssClass="labels" Text="Nombre *"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtNomFamily" runat="server" MaxLength="50" Width="250px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblDescripción" runat="server" CssClass="labels" Text="Descripción: *"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtDescripcionFamily" runat="server" MaxLength="50" Width="250px"
                                                        Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblPesofamily" runat="server" CssClass="labels" Text="Peso *"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPesoFamily" runat="server" MaxLength="50" Width="80px" Enabled="False"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="ReqPesoFamilia" runat="server" 
                                                                        ControlToValidate="txtPesoFamily" Display="None" 
                                                                        ErrorMessage="Requiere que el Peso contenga solo  números" 
                                                                        ValidationExpression="([0-9]{1,9})"></asp:RegularExpressionValidator>
                                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender32" runat="server" 
                                                                        Enabled="True" TargetControlID="ReqPesoFamilia"></cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                        </table>
                                        <br /><br />
                                    </fieldset>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="ConsultaGVFamilia" runat="server" Style="display: block"  >  
                            <table align="center">
                                <tr>
                                    <td>
                                        <div class="p" style="width:780px; height: 330px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
	                                        font-family : arial, Helvetica, sans-serif;">
                                            <asp:GridView ID="GvConsFamilia" runat="server" AutoGenerateColumns="False" 
                                                Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                                Width="100%" onrowediting="GvConsFamilia_RowEditing" 
                                                onrowcancelingedit="GvConsFamilia_RowCancelingEdit" 
                                                onrowupdating="GvConsFamilia_RowUpdating" >                     
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Código">
                                                        <EditItemTemplate>
                                                                <asp:label ID="txtCodigo" runat="server" Width="80px" Enabled="false" ></asp:label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:label ID="txtCodigo" runat="server" Width="80px" Enabled="false" Text ='<%# Bind("id_ProductFamily") %>'  ></asp:label>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>    
                                                    <asp:TemplateField HeaderText="Cliente">
                                                        <EditItemTemplate>
                                                                <asp:DropDownList ID="cmbcliefamilia" runat="server" Width="130px"  Enabled="false"
                                                                    AutoPostBack="True"></asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                                <asp:DropDownList ID="cmbcliefamilia" runat="server" Width="130px" 
                                                                    Enabled="false" onselectedindexchanged="cmbcliefamilia_SelectedIndexChanged" ></asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Categoria">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="cmbcatefamilia" runat="server" Width="130px" Enabled="true"
                                                                AutoPostBack="True" onselectedindexchanged="cmbcatefamilia_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="cmbcatefamilia" runat="server" Width="130px" Enabled="false" ></asp:DropDownList>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>   
                                                    <asp:TemplateField HeaderText="SubCategoria">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="cmbsubcatefamilia" runat="server" Width="130px" 
                                                                Enabled="true" AutoPostBack="True"  onselectedindexchanged="cmbsubcatefamilia_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="cmbsubcatefamilia" runat="server" Width="130px" Enabled="false" ></asp:DropDownList>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Marca">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="cmbmarcafamilia" runat="server" Width="130px" 
                                                                Enabled="true"  AutoPostBack="True" 
                                                                onselectedindexchanged="cmbmarcafamilia_SelectedIndexChanged" >
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="cmbmarcafamilia" runat="server" Width="130px" Enabled="false" >
                                                            </asp:DropDownList>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SubMarca">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="cmbSubmarcafamilia" runat="server" Width="130px" 
                                                                Enabled="true"  AutoPostBack="True"    onselectedindexchanged="cmbSubmarcafamilia_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="cmbSubmarcafamilia" runat="server" Width="130px" Enabled="false" ></asp:DropDownList>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                       
                                                    <asp:TemplateField HeaderText="Nombre">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtNomFamilia" runat="server" Width="130px" Enabled="true" ></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:label ID="txtNomFamilia" runat="server" Width="130px" Enabled="false" Text ='<%# Bind("name_Family") %>'  ></asp:label>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>    
                                                    <asp:TemplateField HeaderText="Descripción">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtDescripFamilia" runat="server" Width="130px" Enabled="true" ></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:label ID="txtDescripFamilia" runat="server" Width="130px" Enabled="false" Text ='<%# Bind("family_Descripcion") %>'  ></asp:label>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>    
                                                    <asp:TemplateField HeaderText="Peso">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="cmbsubpesofamilia" runat="server" Width="130px" Enabled="true" ></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:label ID="cmbsubpesofamilia" runat="server" Width="130px" Enabled="false" Text ='<%# Bind("family_Peso") %>'  ></asp:label>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                                                     
                                                    <asp:TemplateField HeaderText="Estado" >
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="CheckFamilia" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckFamilia" runat="server"  Enabled="false" Checked ='<%# Bind("family_status") %>' ></asp:CheckBox> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowEditButton="True" />
                                                </Columns>
                                            </asp:GridView>
                                            <br /><br />                                                 
                                            <div align="center">
                                                <div>
                                                    <asp:Label ID="Lblexexcel" runat="server" CssClass="labels" Text="Exportar a Excel "></asp:Label>
                                                </div>
                                                <iframe id="iframeGVCfamilia" runat="server" src="pruebaexcel.aspx" frameborder="0"  width="64px" height="64px"  ></iframe>
                                                <div align="center">                                       
                                                    <asp:Button ID="BtnGVCfamilia" runat="server" CssClass="buttonPlan"
                                                    Text="Cancelar" Width="80px" onclick="btnCancelar_Click" />
                                                </div> 
                                            </div>  
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalPOPFamilia" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="ConsultaGVFamilia"
                            TargetControlID="btnPopupGVCFamilia" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="btnPopupGVCFamilia" runat="server" CssClass="alertas"
                            Width="0px" />  
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="BtnCrearFamily" runat="server" CssClass="buttonPlan" Text="Crear"
                                        Width="95px" OnClick="BtnCrearFamily_Click" /><asp:Button ID="BtnsaveFamily" runat="server"
                                            CssClass="buttonPlan" Text="Guardar" Visible="False" Width="95px" OnClick="BtnsaveFamily_Click" />
                                    <asp:Button
                                                ID="BtnSearchFamily" runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px" />
                                    <asp:Button ID="BtnCancelFamily" runat="server"
                                                            CssClass="buttonPlan" Text="Cancelar" Width="95px" OnClick="BtnCancelFamily_Click" />
                                    <asp:Button ID="BtnCarMasivaFamilia" runat="server" CssClass="buttonPlan" Text="Carga Masiva" Width="95px" 
                                                                onclick="BtnCarMasivaFamilia_Click"/>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="BuscarFamily" runat="server" CssClass="busqueda" DefaultButton="BtnBFamily"
                            Style="display: none" Height="202px" Width="343px">
                            <br /><br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblTitBFamily" runat="server" CssClass="labelsTit2" Text="Buscar Familia" />
                                    </td>
                                </tr>
                            </table>
                            <br /><br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblBClienteFamily" runat="server" CssClass="labels" Text="Cliente*"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CmbBClientFamily" runat="server" Width="215px" AutoPostBack="True"
                                            OnSelectedIndexChanged="CmbBClientFamily_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblCategoriaBFamily" runat="server" CssClass="labels" Text="Categoria:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbCategoryBFamily" runat="server" Width="215px" AutoPostBack="True"
                                            OnSelectedIndexChanged="cmbCategoryBFamily_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblBMarcaFamilia" runat="server" CssClass="labels" Text="Marca:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CmbBMarcaFamily" runat="server" Width="215px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblBNomFamily" runat="server" CssClass="labels" Text="Nombre:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtBNomFamily" runat="server" MaxLength="50" Width="210px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br /><br />
                            <asp:Button ID="BtnBFamily" runat="server" CssClass="buttonPlan" Text="Buscar" Width="80px"
                                OnClick="BtnBFamily_Click" />
                            <asp:Button ID="BtnCancelBFamily" runat="server" CssClass="buttonPlan"
                                    Text="Cancelar" Width="80px" />
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="IbtnFamily" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True" OkControlID="BtnCancelBFamily" PopupControlID="BuscarFamily"
                            TargetControlID="BtnSearchFamily" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="CMFamilia" runat="server" Style="vertical-align: middle; " BackColor="#7F99CC" BorderColor="#7F99CC"
                            BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt">
                            <div style="position:absolute; z-index:1;  width:100%;" align="right" > 
                                <asp:ImageButton ID="imgbtnCMFamilia" runat="server" AlternateText="Cerrar Ventana"
                                                ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px"/>
                            </div>
                            <div  align="center">                                   
                                <iframe id="iframeCMFamilia" runat="server" height="230px" src="" width="500px">
                                </iframe>                                       
                            </div>                                                           
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalPoCMFamilia" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="CMFamilia"
                            TargetControlID="btnPoCMFamilia" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="btnPoCMFamilia" runat="server" CssClass="alertas"
                            Width="0px" />
                    </ContentTemplate>
                </cc1:TabPanel>
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_FAMILIA - FIN -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->







                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_SUBFAMILIA - INI -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <cc1:TabPanel ID="PanelSubFamilia" runat="server" HeaderText="SubFamilia de Producto">
                    <HeaderTemplate>
                        SubFamilia
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="centrarcontenido">
                            <p><span class="labelsTit2">Gestión de SubFamilia de Producto</span></p>
                        </div>
                        <div class="centrar" style="margin-top: 30px; margin-bottom:30px">                   
                            <div class="tabla centrar" >
                                <fieldset>   
                                    <legend style="">SubFamilia</legend>
                                    <div class="fila">
                                        <div class="celda"><span>Código: </span></div>
                                        <div class="celda"><asp:TextBox ID="txtcodigosubfamilia" Width="110px" 
                                                enabled="False" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda"><span>*Cliente: </span></div>
                                        <div class="celda"><asp:DropDownList ID="ddl_sf_cliente" Width="170px" enabled="False" 
                                                runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddl_sf_cliente_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda"><span>*Categoría: </span></div>
                                        <div class="celda"><asp:DropDownList ID="ddl_sf_categoria" Width="170px" enabled="False" 
                                                runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddl_sf_categoria_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda"><span>SubCategoría: </span></div>
                                        <div class="celda">
                                            <asp:DropDownList ID="ddl_sf_subcategoria" Width="170px" enabled="False" 
                                                runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda"><span>*Marca: </span></div>
                                        <div class="celda"><asp:DropDownList ID="ddl_sf_marca" Width="170px" enabled="False" 
                                                runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddl_sf_marca_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda"><span>SubMarca: </span></div>
                                        <div class="celda">
                                            <asp:DropDownList ID="ddl_sf_submarca" Width="170px" enabled="False" 
                                                runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda"><span>*Familia: </span></div>
                                        <div class="celda">
                                            <asp:DropDownList ID="ddlfamilias" Width="170px" enabled="False" 
                                                runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="fila">
                                        <div class="celda"><span>*Nombre: </span></div>
                                        <div class="celda"><asp:TextBox ID="txtnomsubfamilia" Width="170px" enabled="False" 
                                                runat="server"></asp:TextBox>
                                        </div>
                                    </div>                                        
                                </fieldset>
                                &nbsp;
                            </div>                                
                        </div>
                        <div class="centrar" style="margin-top: 30px; margin-bottom:30px">
                            <div class="centrarcontenido">
                                <asp:Button ID="btncreasubfam" runat="server" CssClass="buttonPlan" 
                                    Width="95px" Text="Crear" onclick="btncreasubfam_Click" />
                                <asp:Button ID="btnguardasubfam" runat="server" CssClass="buttonPlan" 
                                    Width="95px" Text="Guardar" visible="False" onclick="btnguardasubfam_Click" />
                                <asp:Button ID="btnconsultasubfam" runat="server" CssClass="buttonPlan" Width="95px" Text="Consultar" />
                                <asp:Button ID="btncancelsubfam" runat="server" CssClass="buttonPlan" 
                                    Width="95px" Text="Cancelar" onclick="btncancelsubfam_Click" />
                                <asp:Button ID="btncmasivasubfam" runat="server" CssClass="buttonPlan" 
                                    Width="95px" Text="Carga Masiva" Visible="False" />
                            </div>                                
                        </div>

                        <asp:Panel ID="GVSubFamilia" runat="server" Style=" display: block;">
                            <div class="p" style="width:780px; height: 330px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
	                                    font-family : arial, Helvetica, sans-serif;">
                                <asp:GridView ID="grid_subfamily" runat="server" AutoGenerateColumns="False" 
                                    Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                    Width="100%" onrowcancelingedit="grid_subfamily_RowCancelingEdit" 
                                    onrowdeleting="grid_subfamily_RowDeleting" 
                                    onrowediting="grid_subfamily_RowEditing" 
                                    onrowupdating="grid_subfamily_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Código">
                                            <EditItemTemplate> 
                                                <asp:Label ID="lbl_bsubfam_cod" runat="server" Text='<%# Bind("id_ProductSubFamily") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bsubfam_cod" runat="server" Text='<%# Bind("id_ProductSubFamily") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Familia">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_bsubfam_family" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bsubfam_family" Text='<%# Bind("id_ProductFamily") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_bsubfam_nom" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bsubfam_nom" runat="server" Text='<%# Bind("subfam_nombre") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estado">
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="cbx_bsubfam_status" runat="server" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbx_bsubfam_status" Enabled="false" runat="server" Checked='<%# Bind("subfam_status") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                    </Columns>
                                </asp:GridView>
                                <div align="center">
                                    <br /><br />
                                    <div>
                                    </div>
                                    <div align="center">                                       
                                        <asp:Button ID="btncancelgvsubfam" runat="server" CssClass="buttonPlan"
                                        Text="Cancelar" Width="80px" onclick="btnCancelar_Click" />
                                    </div> 
                                </div>   
                            </div>
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="popup_grid_consultasubfam" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="GVSubFamilia" 
                            TargetControlID="tagetbtnpres" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="tagetbtnpres" runat="server"
                                        Width="0px" CssClass="alertas" />
                        <asp:Panel ID="Panel_buscarSubFam" runat="server" CssClass="busqueda" DefaultButton="btnbuscarsubfam"
                            Height="220px" Width="343px" Style="display: none;">
                            <br /><br />
                            <div class="centrarcontenido">Buscar Sub Familias</div>
                            <br /><br />
                            <div class="tabla centrar">
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labels">Cliente</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="ddl_bsf_cliente" Width="180px" runat="server" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="ddl_bsf_cliente_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labels">Categoría</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="ddl_bsf_categoria" Width="180px" runat="server" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="ddl_bsf_categoria_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labels">Marca</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="ddl_bsf_marca" Width="180px" runat="server" 
                                            AutoPostBack="True" onselectedindexchanged="ddl_bsf_marca_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>                                    
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labels">Familia</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:DropDownList ID="ddl_bsubfam" Width="180px" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="celda">
                                        <span class="labels">Nombre SubFamilia</span>
                                        &nbsp;
                                    </div>
                                    <div class="celda">
                                        <asp:TextBox ID="txt_bsubfam" runat="server" Width="174px"></asp:TextBox>
                                    </div>
                                </div>                                                                
                            </div>
                            <br /><br />
                            <div class="centrarcontenido">
                                <asp:Button ID="btnbuscarsubfam" runat="server" CssClass="buttonPlan" 
                                        Width="95px" Text="Buscar" 
                                        onclick="btnbuscarsubfam_Click" />
                                <asp:Button ID="btn_cancel_bsubfam" runat="server" CssClass="buttonPlan" 
                                        Width="95px" Text="Cancelar" onclick="btncancelsubfam_Click"/>
                            </div>
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalBuscarSubFamily" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="Panel_buscarSubFam" 
                            TargetControlID="btnconsultasubfam" DynamicServicePath="">
                        </cc1:ModalPopupExtender>          
                    </ContentTemplate>
                </cc1:TabPanel>
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_SUBFAMILIA - FIN -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->






                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_PRODUCTO - INI -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <cc1:TabPanel runat="server" HeaderText="Producto" ID="PanelProducto">
                    <HeaderTemplate>
                        Producto
                    </HeaderTemplate>        
                    <ContentTemplate>
                        <br /><br />
                        <div class="centrarcontenido">
                            <span class="labelsTit2">Administración de Productos</span>
                        </div>
                        <br />                                      
                        <div class="centrar">
                            <div class="fila">
                                <div class="fila"> <%--Clasificacion--%>
                                    <div class="celda"> 
                                        <fieldset id="FlsInfClasifProd" runat="server" style="height: 113px;">
                                            <legend style="">Clasificación</legend>
                                            <div class="tabla">
                                                <div class="fila"><%--Categoria--%>
                                                    <div class="celda">
                                                        <asp:Label ID="LblSelCateg" runat="server" CssClass="labels" Text="Categoria *"></asp:Label>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="cmbTipoCateg" runat="server" AutoPostBack="True" Width="170px"
                                                            Enabled="False" OnSelectedIndexChanged="cmbTipoCateg_SelectedIndexChanged" Style="margin-top: 2px">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="fila"><%--SubCategoria--%>
                                                    <div class="celda">
                                                        <asp:Label ID="LblSubcategoria" runat="server" CssClass="labels" Text="SubCategoria"></asp:Label>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="CmbSubCategoriaProduct" runat="server" AutoPostBack="True"
                                                            Width="170px" Enabled="False" OnSelectedIndexChanged="CmbSubCategoriaProduct_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="fila"><%--Familia--%>
                                                    <div class="celda">
                                                        <asp:Label ID="LblPFamily" runat="server" CssClass="labels" Text="Familia "></asp:Label>
                                                    </div>
                                                    <div class="celda">       
                                                        <asp:DropDownList ID="cmbPFamily" runat="server" CausesValidation="True" Width="170px"
                                                            Enabled="False">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="fila"><%--Marca--%>
                                                    <div class="celda">
                                                        <asp:Label ID="LblSelFabricante" runat="server" CssClass="labels" Text="Marca*"></asp:Label>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="cmbFabricante" runat="server" Width="170px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <%--<div class="fila">
                                                    <div class="celda">
                                                        <asp:Label ID="LblSubBrand" runat="server" CssClass="labels" Text="SubMarca"></asp:Label>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="cmbSelSubBrand" runat="server" Width="170px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>--%>
                                                <%--<div class="fila">
                                                    <div class="celda">
                                                        <asp:Label ID="LblPres" runat="server" CssClass="labels" Text="Presentación  "></asp:Label>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="cmbPres" runat="server" CausesValidation="True" Width="170px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>--%>
                                                <%--<div class="fila">
                                                    <div class="celda">
                                                        <asp:Label ID="Label7" runat="server" CssClass="labels" Text="Sub Familia "></asp:Label>
                                                        </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="ddl_psubfamily" runat="server" CausesValidation="True" Width="170px"
                                                            Enabled="False">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>--%>
                                                <%--<div class="fila">
                                                    <div class="celda">
                                                        <asp:Label ID="LblFactor" runat="server" CssClass="labels" Text="Factor de Conversión *"
                                                            Visible="False"></asp:Label>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:TextBox ID="TxtFactor" runat="server" AutoPostBack="True" CausesValidation="True"
                                                            Visible="False" MaxLength="12" Width="110px" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>--%>
                                                <%--<div class="fila">
                                                    <div class="celda">
                                                        <asp:Label ID="lblUMedida" runat="server" CssClass="labels" Text="U. Medida "></asp:Label>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="cmbUMedida" runat="server" CausesValidation="True" Width="170px"
                                                            Enabled="False">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div> --%>
                                                <%--<div class="fila">
                                                    <div class="celda">
                                                        <asp:Label ID="LblPeso" runat="server" CssClass="labels" Text="Peso *"></asp:Label>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:TextBox ID="TxtPeso" runat="server" MaxLength="6" Width="110px" 
                                                            Enabled="False"></asp:TextBox>
                                                        <asp:Label ID="LblTon" runat="server" CssClass="labels"></asp:Label>
                                                    </div>
                                                </div> --%>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="fila"> <%--Especificaciones--%>
                                    <div class="celda"> 
                                        <fieldset id="Fieldset3" runat="server" style="height: 113px;">
                                        <legend style="">Especificaciones</legend>
                                        <div class="tabla">
                                            <div class="fila"><%--Tipo--%>
                                                <div class="celda">
                                                    <asp:Label ID="lblEspTipo" runat="server" CssClass="labels" Text="Tipo"></asp:Label>
                                                </div>
                                                <div class="celda">
                                                    <asp:DropDownList ID="cmbTipoProducto" runat="server" Width="170px"
                                                        Enabled="False" Style="margin-top: 2px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="fila"><%--Formato--%>
                                                <div class="celda">
                                                    <asp:Label ID="LblEspFormato" runat="server" CssClass="labels" Text="Formato____"></asp:Label>
                                                </div>
                                                <div class="celda">
                                                    <asp:DropDownList ID="cmbFormatoProducto" runat="server" 
                                                        Width="170px" Enabled="False">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="celda"> <%--Información Basica--%>
                                    <fieldset id="FlsInfBasiProd" runat="server" style="height:260px; width:355px;">
                                        <legend style="">Información básica</legend>                                            
                                        <div class="tabla">
                                            <div class="fila"><%--CodigoProducto--%>
                                                <div class="celda">
                                                    <asp:Label ID="LblCodProducto" runat="server" CssClass="labels" Text="Código de Producto * "></asp:Label>
                                                </div>
                                                <div class="celda">
                                                    <asp:TextBox ID="TxtCodProducto"  runat="server" MaxLength="30" Width="195px" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                            <%--<div class="fila">
                                                <div class="celda">
                                                    <asp:Label ID="LblCodEAN" runat="server" CssClass="labels" Text="Código de EAN * "
                                                        Visible="False"></asp:Label>
                                                </div>
                                                <div class="celda">
                                                    <asp:TextBox ID="TxtCodEAN" runat="server" MaxLength="30" Width="165px" Visible="False"
                                                        Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>--%>
                                            <%--<div class="fila">
                                                <div class="celda">
                                                    <asp:Label ID="LblSelCompany" runat="server" CssClass="labels" Text="Cliente *"></asp:Label>
                                                </div>
                                                <div class="celda">
                                                    <asp:TextBox ID="TxtCompan" runat="server" Enabled="False" ReadOnly="True" Width="165px"></asp:TextBox>
                                                </div>
                                            </div>--%>
                                            <div class="fila"><%--NombreProducto--%>
                                                <div class="celda">
                                                    <asp:Label ID="LblNomProducto" runat="server" CssClass="labels" Text="Nombre de Producto * "></asp:Label>
                                                </div>
                                                <div class="celda">
                                                    <asp:TextBox ID="TxtNomProducto" runat="server" Enabled="False" Width="195px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="fila"><%--AliasProducto--%>
                                                <div class="celda">
                                                    <asp:Label ID="LblAliProducto" runat="server" CssClass="labels" Text="Alias del Producto  "></asp:Label>
                                                </div>
                                                <div class="celda">
                                                    <asp:TextBox ID="txtAlias" runat="server" Enabled="False" Width="195px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="fila"><%--Precio de Venta--%>
                                                <div class="celda">
                                                    <asp:Label ID="LblInfPrecioVenta" runat="server" CssClass="labels" Text="Precio de Venta:"></asp:Label>
                                                </div>
                                                <div class="celda">
                                                    <%--<asp:TextBox ID="TxtInfPrecioVenta" runat="server" MaxLength="6" Width="55px" Enabled="False"></asp:TextBox>--%>
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" 
                                                    runat="server" ID="RadNumericTxtInfPrecioVenta" 
                                                    EmptyMessage="--Ingrese Precio--" MinValue="0" Width="115px" 
                                                    ShowSpinButtons="true" NumberFormat-DecimalDigits="2" MaxValue="100" NumberFormat-PositivePattern="S/. n" Enabled="false"></telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                            <div class="fila"><%--Precio Oferta--%>
                                                <div class="celda">
                                                    <asp:Label ID="LblInfPrecioOferta" runat="server" CssClass="labels" Text="Precio de Oferta:"></asp:Label>
                                                </div>
                                                <div class="celda">
                                                    <%--<asp:TextBox ID="TxtInfPrecioOferta" runat="server" MaxLength="6" Width="55px" Enabled="False"></asp:TextBox>--%>
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" 
                                                    runat="server" ID="RadNumericTxtInfPrecioOferta" 
                                                    EmptyMessage="--Ingrese Precio--" MinValue="0" Width="115px" 
                                                    ShowSpinButtons="true" NumberFormat-DecimalDigits="2" MaxValue="100" NumberFormat-PositivePattern="S/. n" Enabled="false"></telerik:RadNumericTextBox>
                                                <br />
                                                </div>
                                            </div>
                                            <div class="fila"><%--Stock--%>
                                                <div class="celda">
                                                    <asp:Label ID="LblInfStock" runat="server" CssClass="labels" Text="Stock"></asp:Label>
                                                </div>
                                                <div class="celda">
                                                   <%-- <asp:TextBox ID="TxtInfStock" runat="server" MaxLength="6" Width="55px" Enabled="False"></asp:TextBox>--%>
                                                    <asp:CheckBox ID="CheckBoxInfStock" runat="server" Text="Si queda Stock" CssClass="labels" Enabled="false"></asp:CheckBox>
                                                </div>
                                            </div>      
                                        </div>
                                        <div class="tabla">
                                            <%--<div class="fila">
                                                <div class="celda">
                                                    <asp:Label ID="LblPrecioPDV" runat="server" CssClass="labels" Text="Precio de Lista"></asp:Label>
                                                    <asp:TextBox ID="TxtPrecioPDV" runat="server" MaxLength="6" Width="55px" Enabled="False"></asp:TextBox>
                                                    <asp:Label ID="LblPrecioReventa" runat="server" CssClass="labels" Text="PVP "></asp:Label>                                          
                                                    <asp:TextBox ID="TxtPrecioReventa" runat="server" MaxLength="6" Width="55px" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>--%>                              
                                            <%--<div class="fila">
                                                <asp:Label ID="lblCaracterisitcas" runat="server" CssClass="labels" Text="Caracteristicas "></asp:Label>
                                                <br /><br />
                                                <asp:TextBox ID="TxtCaracteristicas" runat="server" MaxLength="255" Width="333px"
                                                    Height="50px" Enabled="False"></asp:TextBox>
                                            </div>--%>
                                            <%--<div class="fila">
                                                <asp:Label ID="lblBeneficios" runat="server" CssClass="labels" Text="Beneficios "></asp:Label>
                                                <br /><br />
                                                <asp:TextBox ID="TxtBeneficios" runat="server" MaxLength="255" Width="333px" Height="40px"
                                                    Enabled="False"></asp:TextBox>
                                            </div>--%>
                                        </div>
                                        <div class="tabla">
                                            <div class="fila"><%--Promocion--%>
                                                <asp:Label ID="LblInfPromocion" runat="server" CssClass="labels" Text="Promoción "></asp:Label>
                                                &nbsp;
                                                <asp:TextBox ID="TxtInfPromocion" runat="server" MaxLength="255" Width="333px" Height="70px"
                                                    Enabled="False" TextMode="MultiLine" ></asp:TextBox>
                                            </div> 
                                        </div>                         
                                    </fieldset> 
                                </div>
                            </div>
                        </div>
                        <fieldset id="FlsInfDimTamProd" runat="server" visible="False">
                            <legend style="">Dimensiones</legend>
                            <br /><br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblAlto" runat="server" CssClass="labels" Text="Alto *"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblAncho" runat="server" CssClass="labels" Text="Ancho *"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblUMedDim" runat="server" CssClass="labels" Text="Unidad de medida *"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TxtAlto" runat="server" MaxLength="6" Width="55px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtAncho" runat="server" MaxLength="6" Width="55px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbAltoPro" runat="server" CausesValidation="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="TitEstadoProducto" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RBtnListStatusProducto" runat="server" Font-Bold="True"
                                        Font-Names="Arial" Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal"
                                        Enabled="False">
                                        <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                        <asp:ListItem>Deshabilitado</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="CosultaGVProducto" runat="server" CssClass="modalPopupGVProducto" Style="display: block">
                            <div class="headerGVProducto">
                                Listado de Productos
                            </div>
                            <div class="bodyGVProducto">
                                <br/>
                                <table align="center">
                                    <tr>
                                        <td>
                                            <div class="p" style="width:750px; height: 280px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
                                                font-family : arial, Helvetica, sans-serif;"> 
                                                <asp:GridView ID="GVConsulProduct" runat="server" AutoGenerateColumns="False"  
                                                    Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                                    style="margin-bottom: 0px" 
                                                    onrowediting="GVConsulProduct_RowEditing" 
                                                    onrowcancelingedit="GVConsulProduct_RowCancelingEdit" 
                                                    onrowupdating="GVConsulProduct_RowUpdating"
                                                    HeaderStyle-BackColor="#3AC0F2" 
                                                    HeaderStyle-ForeColor="White"
                                                    RowStyle-BackColor="#A1DCF2" 
                                                    AlternatingRowStyle-BackColor="White" 
                                                    AlternatingRowStyle-ForeColor="#000"
                                                    AllowPaging="true"
                                                    OnPageIndexChanging="OnPageIndexChangingProducts">
                                                    <headerstyle height="20" />
                                                    <rowstyle height="20" />
                                                    <alternatingrowstyle height="20" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Categoria" >
                                                            <EditItemTemplate>                                                       
                                                                <asp:DropDownList ID="cmbTipoCateg" runat="server" Width="130px" Enabled="true" 
                                                                    AutoPostBack="True" onselectedindexchanged="cmbTipoCateg_SelectedIndexChanged1" >        
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTipoCateg" runat="server" Width="130px" Enabled="false"  Text='<%# Bind("categoria") %>'></asp:Label>                                                                                
                                                                <asp:Label ID="lblTipoCategCod" runat="server" Width="130px" Visible="false"  Text='<%# Bind("cod_categoria") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                               
                                                        <asp:TemplateField HeaderText="SubCategoria"  >
                                                            <EditItemTemplate>                                                   
                                                                <asp:DropDownList ID="CmbSubCategoriaProduct" runat="server" Width="130px" Enabled="true"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubCategoriaProduct" runat="server" Width="130px"  Enabled="false"  Text='<%# Bind("subcategoria") %>' ></asp:Label>
                                                                <asp:Label ID="lblSubCategoriaProductCod" runat="server" Width="130px"  Visible="false"  Text='<%# Bind("cod_subcategoria") %>' ></asp:Label>                                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Familia" >
                                                            <EditItemTemplate>                                                   
                                                                <asp:DropDownList ID="cmbPFamily" runat="server" Width="150px" Enabled="true" 
                                                                        AutoPostBack="True" onselectedindexchanged="cmbPFamily_SelectedIndexChanged1"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPFamily" runat="server" Width="150px" Enabled="false" Text='<%# Bind("familia") %>'  ></asp:Label>                                                                                
                                                                <asp:Label ID="lblPFamilyCod" runat="server" Width="150px" Visible="false" Text='<%# Bind("cod_familia") %>'  ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Marca" >
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="cmbFabricante" runat="server" Width="130px"  
                                                                    Enabled="true" AutoPostBack="True" 
                                                                    onselectedindexchanged="cmbFabricante_SelectedIndexChanged1" >        
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFabricante" runat="server"  Width="130px" Enabled="false" Text='<%# Bind("marca") %>' ></asp:Label>
                                                                <asp:Label ID="lblFabricanteCod" runat="server"  Width="130px" Visible="false" Text='<%# Bind("cod_marca") %>' ></asp:Label>                                                  
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="SubMarca" >
                                                            <EditItemTemplate>                                                   
                                                                <asp:DropDownList ID="cmbSelSubBrand" runat="server" Width="130px" Enabled="true"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSelSubBrand" runat="server" Width="130px" Enabled="false" Text='<%# Bind("Name_SubBrand") %>'  ></asp:Label>                                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField> --%>
                                                        <%--<asp:TemplateField HeaderText="Presentación" Visible="False">
                                                            <EditItemTemplate>                                                   
                                                                <asp:DropDownList ID="cmbPres" runat="server" Width="180px" Enabled="true"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPres" runat="server" Width="180px"   Enabled="false"></asp:Label>                                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField> --%>
                                                        <%--<asp:TemplateField HeaderText="SubFamilia">
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddl_pgsubfamily" runat="server" Enabled="true" Width="150px"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_pgsubfamily" runat="server" Enabled="false" Width="150px" Text='<%# Bind("subfam_nombre") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <%--<asp:TemplateField HeaderText="Peso" >
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TxtPeso" runat="server" Width="150px" ></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="TxtPeso" runat="server" Text='<%# Bind("Product_Peso_gr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <%--<asp:TemplateField HeaderText="U. Medida" >
                                                            <EditItemTemplate>                                                   
                                                                <asp:DropDownList ID="cmbUMedida" runat="server" Width="150px" Enabled="true"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="cmbUMedida" runat="server" Width="150px" Enabled="false" ></asp:DropDownList>                                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Código">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TxtCodigoProducto" runat="server" Width="80px" Text='<%# Eval("codigo") %>' Enabled="false"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodigoProducto" runat="server" Width="80px" Text='<%# Eval("codigo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SKU">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TxtCodProducto" runat="server" Width="80px" Text='<%# Bind("codigoint") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodProducto" runat="server" Width="80px" Text='<%# Bind("codigoint") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Cliente" >
                                                            <EditItemTemplate>                                                   
                                                                <asp:label ID="TxtCompan" runat="server" Width="130px" Enabled="true"></asp:label>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="TxtCompan" runat="server" Width="130px" Enabled="false" ></asp:Label>                                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField> --%> 
                                                        <asp:TemplateField HeaderText="Nombre Producto">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TxtNomProducto" runat="server" Width="300px" ></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNomProducto" runat="server" width="300px" Text='<%# Bind("nombre") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Alias Producto">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TxtAliasProducto" runat="server" Width="220px" ></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAliasProducto" runat="server" width="220px" Text='<%# Bind("alias") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo">
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="cmb_TipProducto" runat="server" Enabled="true" Width="150px"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TipProducto" runat="server" Enabled="false" Width="150px" Text='<%# Bind("tipo") %>' ></asp:Label>
                                                                <asp:Label ID="lbl_TipProductoCod" runat="server" Visible="false" Width="150px" Text='<%# Bind("cod_tipo") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                        <asp:TemplateField HeaderText="Formato">
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="cmb_ForProducto" runat="server" Enabled="true" Width="150px"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ForProducto" runat="server" Enabled="false" Width="150px" Text='<%# Bind("formato") %>' ></asp:Label>
                                                                <asp:Label ID="lbl_ForProductoCod" runat="server" Visible="false" Width="150px" Text='<%# Bind("cod_formato") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>    
                                                        <asp:TemplateField HeaderText="Precio Venta" >
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TxtPrecioPDV" runat="server" Width="100px" ></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPrecioPDV" runat="server" width="100px" Text='<%# Bind("precio_venta") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Precio Oferta" >
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TxtPrecioOferta" runat="server" Width="100px" ></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPrecioOferta" runat="server" width="100px" Text='<%# Bind("precio_oferta") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>        
                                                        <asp:TemplateField HeaderText="Stock" >
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TxtStockProducto" runat="server" Width="100px" ></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblStockProducto" runat="server" width="100px" Text='<%# Bind("stock") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Promocion" >
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TxtPromoProducto" runat="server" Width="220" ></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblPromoProducto" runat="server" width="220" Text='<%# Bind("promocion") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                       
                                                        <%--<asp:TemplateField HeaderText="Precio Lista" >
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TxtPrecioPDV" runat="server" Width="100px" ></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="TxtPrecioPDV" runat="server" width="100px" Text='<%# Bind("Product_PriceList") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <%--<asp:TemplateField HeaderText="Precio PVP">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TxtPrecioReventa" runat="server" Width="100px" ></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="TxtPrecioReventa" runat="server" width="100px" Text='<%# Bind("Product_PriceReSale") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <%--<asp:TemplateField HeaderText="Estado" >
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="CheckEProducto" runat="server"  Enabled="true" Checked ='<%# Bind("Product_Status") %>'></asp:CheckBox> 
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckEProducto" runat="server"  Enabled="false" Checked ='<%# Bind("Product_Status") %>'  ></asp:CheckBox> 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:CommandField ShowEditButton="True" />
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />                                             
                                                </asp:GridView>                                                              
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="footerGVProducto" align="center">
                                <div style="background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;font-family : Verdana;">
                                    <iframe id="iframe1" runat="server" src="pruebaexcel.aspx" frameborder="0"  width="64px" height="64px"></iframe>
                                    <div><asp:Label ID="lblexportExcel" runat="server" CssClass="labels" Text="Exportar a Excel "></asp:Label></div>
                                    <br/>
                                    <div align="center">                                       
                                        <asp:Button ID="Button1" runat="server" CssClass="cancel" Text="Cancelar" Width="80px" OnClick="btnCancelar_Click"/>
                                    </div> 
                                </div>
                            </div> 
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalPCProducto" runat="server" BackgroundCssClass="modalBackgroundGVProducto"
                            DropShadow="True" Enabled="True"  PopupControlID="CosultaGVProducto"
                            TargetControlID="btnPopupGVCProducto" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="btnPopupGVCProducto" runat="server" CssClass="alertas" Width="0px" />
                        <br />
                        <table align="center">
                            <tr>
                                <td >
                                    <asp:Button ID="btnCrearProducto" runat="server" CssClass="btn btn-primary btn-sm" Text="Crear" OnClick="btnCrearProducto_Click" />
                                    <asp:Button ID="BtnSaveProd" runat="server" CssClass="buttonOk" Text="Guardar" Visible="False"  OnClick="BtnSaveProd_Click" />
                                    <asp:Button ID="btnConsultarProducto" runat="server" CssClass="buttonOk" Text="Consultar" />
                                    <asp:Button ID="btnCancelarProducto" runat="server" CssClass="buttonOk" Text="Cancelar" OnClick="btnCancelarProducto_Click" />
                                    <asp:Button ID="btncMasivaProducto" runat="server" CssClass="buttonOk" Text="Carga Masiva" OnClick="btncMasivaProducto_Click"  />
                                    <asp:Button ID="btnBuscarLotes" runat="server" CssClass="buttonOk" Text="Ver Lote(s)"  />
                                </td>
                            </tr>
                        </table>

                        <asp:Panel ID="BuscarProducto" DefaultButton="BtnBProductos" runat="server" CssClass="modalPopupFindProducts" Width="343px" style="display:none" >
                             <div class="headerFindProducts">
                                Buscar Producto
                            </div>
                            <div class="bodyFindProducts">
                                <table align="center">
                                    <tr>
                                        <td><asp:Label ID="LblBcompañia" runat="server" CssClass="labels" Text="Cliente:"></asp:Label></td>
                                        <td><asp:DropDownList ID="cbmbcompañia" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="cbmbcompañia_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="LblBCategoriaProducto" runat="server" CssClass="labels" Text="Categoria:" /></td>
                                        <td><asp:DropDownList ID="cmbBCategoriaProduct" runat="server" Width="250px"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="LblBCodProd" runat="server" CssClass="labels" Text="Marca:" /></td>
                                        <td><asp:DropDownList ID="cmbBBrand" runat="server" Width="250px"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td><asp:Label ID="LblNomPro" runat="server" CssClass="labels" Text="SKU Producto:" /></td>
                                        <td><asp:TextBox ID="TxtSKUProducto" runat="server" MaxLength="50" Width="245px"></asp:TextBox>/td>
                                    </tr>--%>
                                </table>
                            </div>
                            <div class="footerFindProducts" align="right">
                                <asp:Button ID="BtnBProductos" runat="server" CssClass="ok" Text="Buscar" OnClick="BtnBProductos_Click" />
                                <asp:Button ID="BtnCancelarProductos" runat="server" CssClass="cancel" Text="Cancelar"/>
                            </div>       
                        </asp:Panel>

                        <cc1:ModalPopupExtender 
                            ID="IbtnProductos_ModalPopupExtender"
                            PopupControlID="BuscarProducto"
                            TargetControlID="btnConsultarProducto" 
                            DynamicServicePath=""
                            runat="server" 
                            CancelControlID="BtnCancelarProductos"
                            BackgroundCssClass="modalBackgroundFindProducts" DropShadow="True" Enabled="True">
                        </cc1:ModalPopupExtender>
                        
                        <asp:Panel ID="CargaMasivaProductos" runat="server" Style="vertical-align: middle; " BackColor="#7F99CC" BorderColor="#7F99CC"
                            BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt">  
                            <div style="position:absolute; z-index:1;  width:100%;" align="right" > 
                                <asp:ImageButton ID="BtnCerrarProducto" runat="server" AlternateText="Cerrar Ventana"
                                    ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                    ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px"/>
                            </div>
                            <div  align="center">                                   
                                <iframe id="IframeCargarProduct" runat="server" height="230px" src="" width="500px"></iframe>                                       
                            </div>                                                           
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalPCargaMProducto" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="CargaMasivaProductos"
                            TargetControlID="btnPopupProducto" DynamicServicePath="">
                        </cc1:ModalPopupExtender>
                        
                        <asp:Panel  ID="pnBuscarLotes" 
			                        DefaultButton="btnPnBuscarLote" 
			                        runat="server" CssClass="busqueda" Height="219px" Width="343px" style="display:none" >
	                        <br />
	                        <div align="center">
		                        <asp:Label ID="lblBuscarLote" runat="server" CssClass="labelsTit2" Text="Buscar Lote" />
	                        </div>
	                        <br />                  
	                        <%--<table align="center">
		                        <tr>
			                        <td>
				                        <asp:Label ID="lblBuscarLote_FechaInicio" runat="server" CssClass="labels" Text="Fecha Inicio:" />
			                        </td>
			                        <td>
				                        <asp:TextBox ID="txtBuscarLote_FechaInicio" runat="server" ReadOnly="true"></asp:TextBox>
				                        <asp:ImageButton ID="imgBuscarLote_FechaInicio" ImageUrl="../../images/calendar.gif" ImageAlign="Bottom" runat="server" />
				                        <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="imgBuscarLote_FechaInicio" Format="dd/MM/yyyy"></cc1:CalendarExtender>
			                        </td>
		                        </tr>
		                        <tr>
			                        <td>
				                        <asp:Label ID="lblBuscarLote_FechaFin" runat="server" CssClass="labels" Text="Fecha Fin:" />
			                        </td>
			                        <td>
				                        <asp:TextBox ID="txtBuscarLote_FechaFin" runat="server" ReadOnly="true"></asp:TextBox>
				                        <asp:ImageButton ID="imgBuscarLote_FechaFin" ImageUrl="../../images/calendar.gif" ImageAlign="Bottom" runat="server" />
				                        <cc1:CalendarExtender ID="Calendar2" PopupButtonID="imgPopup" runat="server" TargetControlID="imgBuscarLote_FechaFin" Format="dd/MM/yyyy"></cc1:CalendarExtender>
			                        </td>
		                        </tr>
	                        </table>--%>
	                        <br />
	                        <div align="center">
		                        <asp:Button ID="btnPnBuscarLote" runat="server" CssClass="buttonPlan" Text="Buscar"
			                        Width="80px" OnClick="btnBuscarLote_Click" />
		                        <asp:Button ID="btnPnBuscarLoteCancelar"
			                        runat="server" CssClass="buttonPlan" Text="Cancelar" Width="80px" />
	                        </div>        
                        </asp:Panel>
                        <cc1:ModalPopupExtender 
	                        ID="mpeBuscarLote" 
	                        OkControlID="btnPnBuscarLoteCancelar" 
	                        PopupControlID="pnBuscarLotes"
	                        TargetControlID="btnBuscarLotes" 
	                        DynamicServicePath=""
	                        runat="server" BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True">
                        </cc1:ModalPopupExtender>


                        <asp:Button ID="btnPopupProducto" runat="server" CssClass="alertas" Width="0px" />
                    </ContentTemplate>  
                </cc1:TabPanel>
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_PRODUCTO - FIN -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->






                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_PRODUCTO_ANCLA - INI -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <cc1:TabPanel runat="server" HeaderText="Producto Ancla" ID="TabProducAncla">
                    <HeaderTemplate>Producto Ancla</HeaderTemplate>
                    <ContentTemplate>
                        
                        <br /><br />
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" CssClass="labelsTit2" Text="Gestión Producto Ancla"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br /><br />

                        <table align="center">
                            <tr>
                                <td>
                                    <br />
                                    <br />
                                    <fieldset id="Fieldset2" runat="server">
                                        <legend style="">Producto Ancla</legend>
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCodigoAncla" runat="server" CssClass="labels" Text="Cliente*"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbClienteAncla" runat="server" Enabled="False" Width="255px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="cmbClienteAncla_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblOficina" runat="server" CssClass="labels" Text="Oficina*"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbOficinaPancla" runat="server" Enabled="False" Width="255px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="cmbOficinaPancla_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCategoriaancla" runat="server" CssClass="labels" Text="Categoria *"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbCategoryAncla" runat="server" Width="255px" Enabled="False"
                                                        AutoPostBack="True" OnSelectedIndexChanged="cmbCategoryAncla_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSubcatAncla" runat="server" CssClass="labels" Text="SubCategoria* "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbSubcateAncla" runat="server" Width="255px" Enabled="False"
                                                        AutoPostBack="True" OnSelectedIndexChanged="cmbSubcateAncla_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblMarcaAncla" runat="server" CssClass="labels" Text="Marca:*"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbMarcaAncla" runat="server" Width="255px" Enabled="False"
                                                        AutoPostBack="True" OnSelectedIndexChanged="cmbMarcaAncla_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblProducAncla" runat="server" CssClass="labels" Text="Producto Ancla *"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbproductAncla" runat="server" Width="255px" Enabled="False"
                                                        AutoPostBack="True" OnSelectedIndexChanged="cmbproductAncla_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" CssClass="labels" Text="Precio*"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtPrecioprodAncla" runat="server" Width="100px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblPesoPancla" runat="server" CssClass="labels" Text="Peso*"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtPesoPancla" runat="server" Width="100px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                    </fieldset>
                                &nbsp;
                                </td>
                            </tr>
                        </table>
                        

                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RBTproductoAncla" runat="server" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False"
                                        AutoPostBack="True">
                                        <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                        <asp:ListItem>Deshabilitado</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                            
                        <asp:Panel ID="ConsultaGVPancla" runat="server" Style="display: block"  >  
                            <table align="center">
                                <tr>
                                    <td>
                                        <div class="p" style="width:780px; height: 330px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
    	                                    font-family : arial, Helvetica, sans-serif;">
                                            <asp:GridView ID="GVConsultaPancla" runat="server" AutoGenerateColumns="False" 
                                                Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                                Width="100%" onrowediting="GVConsultaPancla_RowEditing" 
                                                onrowcancelingedit="GVConsultaPancla_RowCancelingEdit" onrowupdating="GVConsultaPancla_RowUpdating" 
                                                    >                                               
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Cliente">
                                                        <EditItemTemplate>
                                                                <asp:DropDownList ID="cmbcliepancla" runat="server" Width="130px"  Enabled="false"
                                                                    onselectedindexchanged="cmbcliepancla_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                                <asp:DropDownList ID="cmbcliepancla" runat="server" Width="130px" Enabled="false" ></asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Oficina">
                                                        <EditItemTemplate>
                                                                <asp:DropDownList ID="cmboficipancla" runat="server" Width="130px"   Enabled="false"
                                                                AutoPostBack="True" 
                                                                    onselectedindexchanged="cmboficipancla_SelectedIndexChanged" ></asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                                <asp:DropDownList ID="cmboficipancla" runat="server" Width="130px" Enabled="false" ></asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Categoria">
                                                    <EditItemTemplate>
                                                            <asp:DropDownList ID="cmbcatepancla" runat="server" Width="130px" Enabled="false"
                                                                AutoPostBack="True" 
                                                                onselectedindexchanged="cmbcatepancla_SelectedIndexChanged"  ></asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                        <asp:DropDownList ID="cmbcatepancla" runat="server" Width="130px" Enabled="false" ></asp:DropDownList>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>   
                                                        <asp:TemplateField HeaderText="SubCategoria">
                                                    <EditItemTemplate>
                                                            <asp:DropDownList ID="cmbsubcatepancla" runat="server" Width="130px" 
                                                                Enabled="true" onselectedindexchanged="cmbsubcatepancla_SelectedIndexChanged" AutoPostBack="True"  ></asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                        <asp:DropDownList ID="cmbsubcatepancla" runat="server" Width="130px" Enabled="false" ></asp:DropDownList>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Marca">
                                                    <EditItemTemplate>
                                                            <asp:DropDownList ID="cmbmarcapancla" runat="server" Width="130px" 
                                                                Enabled="true" onselectedindexchanged="cmbmarcapancla_SelectedIndexChanged" AutoPostBack="True" ></asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                        <asp:DropDownList ID="cmbmarcapancla" runat="server" Width="130px" Enabled="false" ></asp:DropDownList>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Producto">
                                                    <EditItemTemplate>
                                                            <asp:DropDownList ID="cmbprodupancla" runat="server" Width="220px" 
                                                                Enabled="true" onselectedindexchanged="cmbprodupancla_SelectedIndexChanged"  AutoPostBack="True"></asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                        <asp:DropDownList ID="cmbprodupancla" runat="server" Width="220px" Enabled="false" ></asp:DropDownList>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>     
                                                        <asp:TemplateField HeaderText="Precio">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="cmbsubpreciopancla" runat="server" Width="130px" Enabled="true" ></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                        <asp:TextBox ID="cmbsubpreciopancla" runat="server" Width="130px" Enabled="false" ></asp:TextBox>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>    
                                                    <asp:TemplateField HeaderText="Peso">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="cmbsubpesopancla" runat="server" Width="130px" Enabled="false" ></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                        <asp:TextBox ID="cmbsubpesopancla" runat="server" Width="130px" Enabled="false" ></asp:TextBox>                                                     
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                                                     
                                                    <asp:TemplateField HeaderText="Estado" >
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="Checkpancla" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Checkpancla" runat="server"  Enabled="false" Checked ='<%# Bind("pancla_Status") %>' ></asp:CheckBox> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowEditButton="True" />
                                                </Columns>
                                            </asp:GridView>
                                            <br />                                                 
                                            <br />                                                 
                                            <div align="center">
                                            <div>
                                            <asp:Label ID="Lbleepancla" runat="server" CssClass="labels" Text="Exportar a Excel "></asp:Label>
                                            </div>
                                                <iframe id="iframepanclaconsulta" runat="server" src="pruebaexcel.aspx" frameborder="0"  width="64px" height="64px"  ></iframe>
                                                <div align="center">                                       
                                                <asp:Button ID="Button2" runat="server" CssClass="buttonPlan"
                                                Text="Cancelar" Width="80px" onclick="btnCancelar_Click" />
                                                </div> 
                                                </div>  
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        
                        <cc1:ModalPopupExtender ID="ModalPopancla" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="ConsultaGVPancla"
                            TargetControlID="btnPopupGVCpancla" DynamicServicePath="">
                        </cc1:ModalPopupExtender>

                        <asp:Button ID="btnPopupGVCpancla" runat="server" CssClass="alertas" Width="0px" /> 

                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="BtnCrearAncla" runat="server" CssClass="buttonPlan" Text="Crear"
                                        Width="95px" OnClick="BtnCrearAncla_Click" /><asp:Button ID="BtnGuardarAncla" runat="server"
                                            CssClass="buttonPlan" Text="Guardar" Visible="False" Width="95px" OnClick="BtnGuardarAncla_Click" />
                                    <asp:Button ID="BtnConsultarAncla" runat="server" CssClass="buttonPlan" Text="Consultar"
                                        Width="95px" />
                                    <asp:Button ID="BtnCancelarAncla" runat="server" CssClass="buttonPlan" Text="Cancelar" 
                                        Width="95px" OnClick="BtnCancelarAncla_Click" />
                                    <asp:Button ID="BtnCaMasivaPancla" runat="server" CssClass="buttonPlan" Text="Carga Masiva" 
                                        Width="95px" OnClick="BtnCaMasivaPancla_Click"  />
                                </td>
                            </tr>
                        </table>


                        <asp:Panel ID="BuscarProductAncla" runat="server" CssClass="busqueda" DefaultButton="btnBProductAncla"
                            Style="display: none;" Height="202px" Width="343px">
                            <br /><br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl" runat="server" CssClass="labelsTit2" Text="Buscar Producto Ancla" />
                                    </td>
                                </tr>
                            </table>

                            <br /><br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCliente" runat="server" CssClass="labels" Text="Cliente:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CmbBClientePAncla" runat="server" Width="215px" AutoPostBack="True"
                                            OnSelectedIndexChanged="CmbBClientePAncla_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblBCategoriapancla" runat="server" CssClass="labels" Text="Categoria:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CmbBCategoriaPAncla" runat="server" Width="215px" AutoPostBack="True"
                                            OnSelectedIndexChanged="CmbBCategoriaPAncla_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblOficinaBPancla" runat="server" CssClass="labels" Text="Oficina:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbOficinaBPancla" runat="server" Width="215px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <br /><br />

                            <div align="center">
                                <asp:Button ID="btnBProductAncla" runat="server" CssClass="buttonPlan" OnClick="btnBProductAncla_Click"
                                    Text="Buscar" Width="80px" />
                                <asp:Button ID="BtnCancelarPAncla" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="80px" />
                            </div>

                        </asp:Panel>

                        <cc1:ModalPopupExtender ID="ModalPopup_BPAncla" runat="server" BackgroundCssClass="modalBackground"
                            OkControlID="BtnCancelarPAncla" PopupControlID="BuscarProductAncla" DropShadow="True"
                            TargetControlID="BtnConsultarAncla" DynamicServicePath="" Enabled="True">
                        </cc1:ModalPopupExtender>

                        <asp:Panel ID="CMPancla" runat="server" Style="vertical-align: middle; " BackColor="#7F99CC" 
                            BorderColor="#7F99CC" BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt">
                           
                            <div style="position:absolute; z-index:1;  width:100%;" align="right" > 
                                <asp:ImageButton ID="btncerrar" runat="server" AlternateText="Cerrar Ventana"
                                    ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                    ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px" />
                            </div>
                            
                            <div  align="center">                                   
                                <iframe id="iframepancla" runat="server" height="230px" src="" width="500px">
                                </iframe>                                       
                            </div>                                                           
                        </asp:Panel>

                        <cc1:ModalPopupExtender ID="ModalPpancla" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="CMPancla"
                            TargetControlID="btnPopupPancla" DynamicServicePath="">
                        </cc1:ModalPopupExtender>

                        <asp:Button ID="btnPopupPancla" runat="server" CssClass="alertas" Width="0px" />
                    
                    </ContentTemplate>
                    
                </cc1:TabPanel>
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_PRODUCTO_ANCLA - FIN -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->





                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_PRODUCTO_ANCLA_02 - INI -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <cc1:TabPanel runat="server" HeaderText="Producto Ancla" ID="panel_Category_Copany">
                    <HeaderTemplate>Clientes X Categoria</HeaderTemplate>
                    <ContentTemplate>
                        <br /><br />
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" CssClass="labelsTit2" Text="Gestión Clientes por Categoria">
                                    </asp:Label>
                                </td>
                            </tr>
                        </table><br/>

                        <panel runat="server" id="paneel" >
                            
                            <table align="center" >
                                <tr>
                                    <td>
                                        <br /><br />
                                        <fieldset id="Fieldset1" runat="server">
                                               
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" CssClass="labels" Text="Categoria *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCategoryCompany_Categoria" runat="server" Enabled="False" 
                                                            Width="255px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" CssClass="labels" Text="Clientes *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBoxList ID="cbxl_cxu_cliente" runat="server" Enabled="False">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br /><br />
                                        </fieldset>&nbsp;
                                    </td>
                                </tr>
                            </table>

                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False"
                                            AutoPostBack="True">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                                
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCategoryCompany_Crear" runat="server" CssClass="buttonPlan" Text="Crear" 
                                            Width="95px" OnClick="btnCategoryCompany_Crear_Click"/>
                                        <asp:Button ID="btnCategoryCompany_Guardar" runat="server" Width="95px"
                                            CssClass="buttonPlan" Text="Guardar" Visible="False" OnClick="btnCategoryCompany_Guardar_Click"/>
                                        <asp:Button ID="btnCategoryCompany_Consultar" runat="server" CssClass="buttonPlan" Text="Consultar" 
                                            Width="95px"/>
                                        <asp:Button ID="btnCategoryCompany_Cancelar" runat="server" CssClass="buttonPlan" Text="Cancelar" 
                                            Width="95px" OnClick="btnCategoryCompany_Cancelar_Click"/>
                                    </td>
                                </tr>
                            </table>

                        </panel>
                        
                        <div id="div" runat="server" aling="center" style="width: 100%; height: auto;" class="centrarcontenido">
                        
                            <telerik:RadGrid ID="gv_CategoryCompany" runat="server" AutoGenerateColumns="False" PageSize="2000"
                                Skin="Vista" Font-Size="Small" AllowPaging="True" GridLines="None" 
                                OnCancelCommand="gv_CategoryCompany_CancelCommand"
                                OnEditCommand="gv_CategoryCompany_EditCommand" OnPageIndexChanged="gv_CategoryCompany_PageIndexChanged"
                                ShowFooter="True" OnPageSizeChanged="gv_CategoryCompany_PageSizeChanged" 
                                onitemdatabound="gv_CategoryCompany_ItemDataBound" 
                                onupdatecommand="gv_CategoryCompany_UpdateCommand">
                                <MasterTableView NoMasterRecordsText="Sin Datos para mostrar." ForeColor="#00579E" Font-Size="Smaller">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="id_Cate_Comp" HeaderText="ID" UniqueName="id_Cate_Comp"
                                            ReadOnly="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Company_Name" HeaderText="Cliente" UniqueName="Company_Name"
                                            ReadOnly="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDropDownColumn Visible="False"   HeaderText="Cliente" UniqueName="gddlCliente">
                                        </telerik:GridDropDownColumn>
                                        <telerik:GridBoundColumn DataField="Product_Category" HeaderText="Categoria" UniqueName="Categoria"
                                            ReadOnly="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDropDownColumn  Visible="False"  HeaderText="Categoria" UniqueName="gddlCategoria">
                                        </telerik:GridDropDownColumn>
                                        <telerik:GridTemplateColumn HeaderText="Validado" UniqueName="TemplateColumn" ReadOnly="True">
                                            <HeaderTemplate>Estado</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_validar" runat="server" Checked='<%# Eval("Status")%>' />
                                                <asp:Label ID="lbl_validar" runat="server"></asp:Label>
                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%# Bind("id_Cate_Comp") %>' Visible="false">
                                                    </asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" 
                                            CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                            EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png"
                                            CancelText="Cancelar" UpdateText="Actualizar">
                                        </telerik:GridEditCommandColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <EditColumn UniqueName="EditCommandColumn1" ButtonType="ImageButton" 
                                            CancelImageUrl="~/Pages/images/cancel_edit_icon.png"
                                            EditImageUrl="~/Pages/images/edit_icon.gif" UpdateImageUrl="~/Pages/images/save_icon.png">
                                        </EditColumn>
                                    </EditFormSettings>
                                    <AlternatingItemStyle BackColor="#F7F7F7" ForeColor="#333333" />
                                </MasterTableView>
                                <PagerStyle PageSizeLabelText="Tamaño de pagina:" />
                            </telerik:RadGrid>


                            <div class="centrarcontenido">
                                <asp:Button ID="btnGrillaCategoryCompany" runat="server" CssClass="buttonPlan" 
                                    Text="Cancelar" Width="95px" OnClick="btnGrillaCategoryCompany_Click" />
                            </div>
                        </div>

                        <asp:Panel ID="PCategoryCompany_Buscar" runat="server" CssClass="busqueda"
                            Style="display: none;" Height="202px" Width="343px">
                            
                            <br /><br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" CssClass="labelsTit2" Text="Buscar Producto Ancla" />
                                    </td>
                                </tr>
                            </table>
                            
                            <br /><br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" CssClass="labels" Text="Cliente:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPCategoryCompany_Cliente" runat="server" Width="215px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" CssClass="labels" Text="Categoria:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPCategoryCompany_Categoria" runat="server" Width="215px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <br /><br />


                            <div align="center">
                                <asp:Button ID="btnCategoryCompany_Buscar" runat="server" CssClass="buttonPlan" 
                                    Text="Buscar" Width="80px" OnClick="btnCategoryCompany_Buscar_Click"/>
                                <asp:Button ID="btnCancel" runat="server" CssClass="buttonPlan"
                                    Text="Cancelar" Width="80px" />
                            </div>

                        </asp:Panel>

                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            OkControlID="btnCancel" PopupControlID="PCategoryCompany_Buscar" DropShadow="True"
                            TargetControlID="btnCategoryCompany_Consultar" DynamicServicePath="" Enabled="True">
                        </cc1:ModalPopupExtender>


                        <asp:Panel ID="pCategoryCompany_Grilla" runat="server" Style="display:Block;"  >  
                            <table align="center"><tr><td><br /><br /></td></tr></table>
                        </asp:Panel>

                        <cc1:ModalPopupExtender ID="mpopupCategoryCompany" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" Enabled="True"  PopupControlID="pCategoryCompany_Grilla"
                            TargetControlID="btnCategoryCompanyg" DynamicServicePath="">
                        </cc1:ModalPopupExtender>

                        <asp:Button ID="btnCategoryCompanyg" runat="server" CssClass="alertas" Width="0px" />         

                    </ContentTemplate>    
                </cc1:TabPanel>
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!-- TAB_PANEL_PRODUCTO_ANCLA_02 - FIN -->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->
                <!---------------------------------------------------------------------------->

            </cc1:TabContainer>



            <asp:Panel ID="Alertas" runat="server" Style="display: none;" DefaultButton="BtnAceptarAlert"
                Height="215px" Width="375px">
                <table align="center">
                    <tr>
                        <td align="center" class="style50" valign="top">
                            <br />
                            &nbsp;
                        </td>
                        <td class="style49" valign="top">
                            <br />
                            <asp:Label ID="LblAlert" runat="server" Text="Señor Usuario" CssClass="labelsMensaje"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="LblFaltantes" runat="server" CssClass="labelsMensaje"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button ID="BtnAceptarAlert" runat="server" CssClass="buttonPlan" Text="Aceptar" />
                        </td>
                    </tr>
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