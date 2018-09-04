<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mod_Cliente_Biblioteca.aspx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Mod_Cliente_Biblioteca" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title title="Biblioteca POP">Biblioteca Materiales</title>
    <link href="../../css/StiloPOP.css" rel="stylesheet" type="text/css" />
    <script src="../../js/carrosel%20pop/jquery-1.4.2.min.js" type="text/javascript"></script>
    <link href="../../js/carrosel%20pop/skin.css" rel="stylesheet" type="text/css" />
    <link href="../../css/MenuBiblioteca.css" rel="stylesheet" type="text/css" />

    <script src="../../js/carrosel%20pop/jquery.jcarousel.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        jQuery(document).ready(function() {
            jQuery('#mycarousel').jcarousel({
                vertical: true,
                scroll: 2
            });
        });       
    </script>

    <style type="text/css">
        #mycarousel
        {
            position: relative;
            left: -500px; /* importante para q no se vea cuando se esta pintando inicialmente*/
            width: 180px;
        }
    </style>
</head>
<body>
    <div>
        <asp:Label ID="usersession" runat="server" Visible="False"></asp:Label>
        <asp:Image ID="Image3" runat="server" ImageUrl="~/Pages/ImgBooom/logotipo.png" ImageAlign="Left">
        </asp:Image>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Pages/ImgBooom/biblioteca.png"
            Height="69px" Width="358px" ImageAlign="Right"></asp:Image>
    </div>   
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolKitScriptManager" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    <br />
    <br />
    <br />
    <br />
    <br />
    <table width="100%">
        <tr>
            <td valign="top">
                <div>
                    <ul id="mycarousel" class="jcarousel jcarousel-skin-tango">
                        <asp:ListView runat="server" ID="ListViewCanales" DataKeyNames="id_catego" OnSelectedIndexChanged="ListViewCanales_SelectedIndexChanged"
                            OnPagePropertiesChanging="ListViewCanales_PagePropertiesChanging" OnSelectedIndexChanging="ListViewCanales_SelectedIndexChanging">
                            <LayoutTemplate>
                                <li runat="server" id="itemPlaceholder"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li id="Tr1" runat="server" class="listviewcateg">
                                    <asp:Label ID="idCategory" runat="server" Text='<%#Eval("id_catego") %>' Visible="false" />
                                    <asp:Label ID="nameCategory" runat="server" Text='<%#Eval("name_catego") %>' Visible="false" />
                                    <asp:LinkButton runat="server" ID="Button1" Text='<%#Eval("name_catego") %>' CommandName="Select"
                                        Height="80px" Width="100px" CssClass="centradotext" Font-Underline="False" />
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                </div>
            </td>
            <td valign="top" height="300px">
                <asp:UpdatePanel ID="uppanel" runat="server">
                    <ContentTemplate>
                        <div>
                            <table align="center" >
                                <tr>
                                    <td style="padding-left: 150px">
                                        <asp:Label ID="LblCategoria" runat="server" Text="CATEGORIA:" Font-Size="14pt" ForeColor="Gray"
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="LblCategoriaSel" runat="server" Text="Todas" Font-Size="14pt" ForeColor="#FBD6E5"
                                            Visible="false" Font-Bold="True" Font-Underline="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblMarca" runat="server" Text="MARCA:" Font-Size="14pt" ForeColor="Gray"
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="LblMarcaSel" runat="server" Text="Todas" Font-Bold="True" Font-Size="14pt"
                                            Visible="false" ForeColor="#FBD6E5" Font-Underline="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblMaterial" runat="server" Text="MATERIAL:" Font-Size="14pt" ForeColor="Gray"
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="LblMaterialSel" runat="server" Text="Todos" Font-Bold="True" Font-Size="14pt"
                                            Visible="false" ForeColor="#FF99FF" Font-Underline="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center" width="900px">
                                <tr>
                                    <td valign="top" style="width: 0px; height: 90%; max-height: 90%;">
                                        <div style="max-height: 200px; overflow: auto; display: none; background-color: Transparent;"
                                            id="divMarcas" runat="server">
                                            <div class="LabelMarca">
                                                <div>
                                                    MARCAS</div>
                                            </div>
                                            <asp:Menu ID="MenuMarcas" runat="server" CssSelectorClass="menu" Orientation="Vertical"
                                                OnMenuItemClick="MenuMarcas_MenuItemClick" StaticDisplayLevels="3" Width="100%">
                                            </asp:Menu>
                                        </div>
                                        <br />
                                        <div style="max-height: 200px; overflow: auto; display: none; background-color: Transparent;"
                                            id="divMaterial" runat="server">
                                            <div class="LabelMarca">
                                                <div>
                                                    MATERIAL</div>
                                            </div>
                                            <asp:Menu ID="MenuMaterial" runat="server" CssSelectorClass="menu" Orientation="Vertical"
                                                StaticDisplayLevels="3" Width="100%" OnMenuItemClick="MenuMaterial_MenuItemClick">
                                            </asp:Menu>
                                        </div>
                                    </td>
                                    <td align="center" valign="top">
                                        <div id="ListBiblioteca" runat="server" align="center">
                                            <asp:ListView ID="ListViewBiblioteca" runat="server" GroupItemCount="3" DataKeyNames="Ruta_URL"
                                                OnPagePropertiesChanging="ListViewBiblioteca_PagePropertiesChanging" OnSelectedIndexChanged="ListViewBiblioteca_SelectedIndexChanged"
                                                OnSelectedIndexChanging="ListViewBiblioteca_SelectedIndexChanging">
                                                <EmptyItemTemplate>
                                                    <td id="Td1" runat="server" align="center" valign="top" />
                                                </EmptyItemTemplate>
                                                <ItemTemplate>
                                                    <td id="Td2" runat="server" align="center" valign="top">
                                                        <div id="caja1" style="width: 200px; height: 152px; background-image: url('../../ImgBooom/filtrodefoto.png');">
                                                            <asp:Label ID="Cod_Biblioteca" runat="server" Text='<%# Eval("id_infoPOP") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="NameMaterial" runat="server" Text='<%# Eval("POP_name") %>' Visible="false"></asp:Label>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("Ruta_URL") %>'
                                                                Height="152px" Width="200px" CommandName="Select" oncontextmenu="return false"
                                                                onkeydown="return false" CssClass="FotoPOP" />
                                                        </div>
                                                        <table align="center">
                                                            <tr>
                                                                <td align="center" valign="middle">
                                                                    <div class="labelBiblioteca">
                                                                        <asp:Label ID="infoPOPClientPhotos_nameLabel" runat="server" Text='<%# Eval("infoPOPClientPhotos_name") %>' />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </td>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <td id="Td1" runat="server" align="center" valign="top">
                                                        <div id="caja1" style="width: 200px; height: 152px; background-image: url('../../ImgBooom/filtrodefoto.png');">
                                                            <asp:Label ID="Cod_Biblioteca" runat="server" Text='<%# Eval("id_infoPOP") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="NameMaterial" runat="server" Text='<%# Eval("POP_name") %>' Visible="false"></asp:Label>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("Ruta_URL") %>'
                                                                oncontextmenu="return false" onkeydown="return false" CssClass="FotoPOP" Height="152px"
                                                                Width="200px" CommandName="Select" />
                                                        </div>
                                                        <table align="center">
                                                            <tr>
                                                                <td align="center" valign="middle">
                                                                    <div class="labelBiblioteca">
                                                                        <asp:Label ID="infoPOPClientPhotos_nameLabel" runat="server" Text='<%# Eval("infoPOPClientPhotos_name") %>' />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </td>
                                                </AlternatingItemTemplate>
                                                <EmptyDataTemplate>
                                                    <table id="Table1" runat="server" align="center" style="">
                                                        <tr>
                                                            <td align="center" valign="top">
                                                                No se han encontrado registros para mostrar
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table id="Table2" runat="server">
                                                        <tr id="Tr2" runat="server">
                                                            <td id="Td3" runat="server" align="center" valign="top">
                                                                <table id="groupPlaceholderContainer" runat="server" border="0" style="">
                                                                    <tr id="groupPlaceholder" runat="server">
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr3" runat="server">
                                                            <td id="Td4" runat="server" align="center" valign="middle">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <GroupTemplate>
                                                    <tr id="itemPlaceholderContainer" runat="server">
                                                        <td id="itemPlaceholder" runat="server" align="center" valign="top">
                                                        </td>
                                                    </tr>
                                                </GroupTemplate>
                                                <SelectedItemTemplate>
                                                    <td id="Td2" runat="server" align="center" valign="top">
                                                        <div id="caja1" style="width: 200px; height: 152px; background-image: url('../../ImgBooom/filtrodefoto.png');">
                                                            <asp:Label ID="Cod_Biblioteca" runat="server" Text='<%# Eval("id_infoPOP") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="NameMaterial" runat="server" Text='<%# Eval("POP_name") %>' Visible="false"></asp:Label>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("Ruta_URL") %>'
                                                                oncontextmenu="return false" onkeydown="return false" CssClass="FotoPOP" Height="152px"
                                                                Width="200px" CommandName="Select" />
                                                        </div>
                                                        <table align="center">
                                                            <tr>
                                                                <td align="center" valign="middle">
                                                                    <div class="labelBiblioteca">
                                                                        <asp:Label ID="infoPOPClientPhotos_nameLabel" runat="server" Text='<%# Eval("infoPOPClientPhotos_name") %>' />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </td>
                                                </SelectedItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        <div id="CarrouselBiblioteca" runat="server" align="center" style="vertical-align: middle;
                                            display: none; width: 98%; min-width: 98%; max-width: 98%; height: 99%; max-height: 99%;">
                                            <table align="center" width="99%" style="max-width: 99%;">
                                                <tr>
                                                    <td style="max-width: 60px; width: 60px;" valign="middle">
                                                        <asp:DataPager runat="server" ID="BeforeListDataPager" PagedControlID="ListViewBibliotecaPOP"
                                                            PageSize="1" OnPreRender="BeforeListDataPager_PreRender">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField ButtonType="Image" ShowFirstPageButton="False" ShowNextPageButton="false"
                                                                    ShowPreviousPageButton="true" PreviousPageImageUrl="~/Pages/img/izquierda.png"
                                                                    ShowLastPageButton="False" ButtonCssClass="tamañonavegadores" />
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </td>
                                                    <td  style="max-width: 92%; width: 0%;" valign="middle" align="center">
                                                        <asp:ListView ID="ListViewBibliotecaPOP" GroupItemCount="1" runat="server" DataKeyNames="Ruta_URL"
                                                            OnPagePropertiesChanging="ListViewBibliotecaPOP_PagePropertiesChanging">
                                                            <EmptyItemTemplate>
                                                                <td id="Td1" runat="server" align="center" valign="top" />
                                                            </EmptyItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <table id="Table1" runat="server" align="center" style="">
                                                                    <tr>
                                                                        <td align="center" valign="top">
                                                                            No se alcanzó a procesar información. Por favor inténtelo nuevamente
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <table align="center" style="max-width: 82%; width: 0%;">
                                                                    <tr runat="server" id="groupPlaceholder">
                                                                    </tr>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <GroupTemplate>
                                                                <tr>
                                                                    <td runat="server" id="itemPlaceholder">
                                                                    </td>
                                                                </tr>
                                                            </GroupTemplate>
                                                            <GroupSeparatorTemplate>
                                                                <tr id="Tr4" runat="server">
                                                                    <td colspan="5" style="min-width: 100px; max-width: 500px; height: 330px; max-height: 330px;">
                                                                        <div>
                                                                            <hr>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </GroupSeparatorTemplate>
                                                            <ItemTemplate>
                                                                <td style="min-width: 100px; max-width: 500px; height: 330px; max-height: 330px; " align="center">
                                                                    <div >
                                                                        <asp:Label ID="infoPOPClientPhotos_nameLabel" runat="server" Visible="true" Font-Bold="True"
                                                                            Font-Size="14pt" ForeColor="#FF99FF" Text='<%# Eval("infoPOPClientPhotos_name") %>' />
                                                                    </div>
                                                                    <asp:Image ID="ImagenPOP" runat="server" ImageUrl='<%# Eval("Ruta_URL") %>' CssClass="FotoPOPDetalle"
                                                                        oncontextmenu="return false" onkeydown="return false" />
                                                                    <br />
                                                                    <br />
                                                                    <div class="LblDetalle">
                                                                        <asp:Label ID="Label1" Font-Size="10" runat="server" Text='<%# Eval("infoPOP_Descripción") %>'
                                                                            Width="500px" Style="max-width: 500px;"></asp:Label>
                                                                    </div>
                                                                </td>
                                                            </ItemTemplate>
                                                            <ItemSeparatorTemplate>
                                                                <td id="Td6" class="itemSeparator" runat="server" style="min-width: 100px; max-width: 500px;
                                                                    height: 330px; max-height: 330px;">
                                                                    &nbsp;
                                                                </td>
                                                            </ItemSeparatorTemplate>
                                                        </asp:ListView>
                                                    </td>
                                                    <td style="max-width: 60px; width: 60px;" valign="middle">
                                                        <asp:DataPager runat="server" ID="AfterListDataPager" PagedControlID="ListViewBibliotecaPOP"
                                                            PageSize="1" OnPreRender="BeforeListDataPager_PreRender">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField ButtonType="Image" ShowFirstPageButton="False" ShowNextPageButton="True"
                                                                    ButtonCssClass="tamañonavegadores" ShowPreviousPageButton="false" NextPageImageUrl="~/Pages/img/derecha.png"
                                                                    ShowLastPageButton="False" />
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ListViewCanales" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td style="width: 5%;" valign="top">
                <div>
                    <asp:Button ID="BtnRegresar" runat="server" CssClass="Regresar" OnClick="BtnRegresar_Click" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
