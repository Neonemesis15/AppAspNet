<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryInsertUserControl.ascx.cs" Inherits="SIGE.Pages.Modulos.Administrativo.ProductUserControls.CategoryInsertUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .MensajeInsertError
    {
	    background-image: url('~/images/MensajeSup.jpg'); 
    }
</style>

<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->
<!-- BUTTON INSERT - INI -->
<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->
<asp:Button ID="BtnCrearProductType" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px"/>
<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->
<!-- BUTTON INSERT - FIN -->
<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->


<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->
<!-- PANEL_INSERT - INI -->
<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->
<asp:Panel ID = "InsertCategoryPanel" 
    runat = "server" 
    CssClass = "busqueda"
    Height = "262px" 
    Style = "display: none" 
    Width = "343px" 
    >
    
    <!-- HEADER - INI -->
    <br /><br />
    <div class="centrarcontenido">
        <span class="labelsTit2">Gestión de Categorías de Productos</span>&nbsp;
    </div>
    <br /><br />
    <!-- HEADER - FIN -->

    <!-- BODY - INI -->
    <div class = "centrar">
        <div class = "tabla centrar">
            <fieldset>
                <legend style = ""> Categoría de Producto </legend>
                <div class = "fila">
                    <div class = "celdaLeft">
                        <span class = "labels"> Código* </span>
                    </div>
                    <div class = "celdaLeft">
                        <asp:TextBox 
                            ID = "TxtCodProductType" 
                            runat = "server" 
                            BackColor = "#DDDDDD" 
                            ReadOnly = "True"
                            Width = "190px" 
                            Enabled = "False">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="fila">
                    <div class="celdaLeft">
                        <asp:Label 
                            ID = "LblNomProductType" 
                            runat = "server" 
                            CssClass = "labels" 
                            Text = "Nombre*: ">
                        </asp:Label>
                    </div>
                    <div class = "celdaLeft">
                        <asp:TextBox 
                            ID = "TxtNomProductType" 
                            runat = "server" 
                            MaxLength = "50" 
                            Width = "190px" 
                            Enabled = "True">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="fila">
                    <div class="celdaLeft">
                        <asp:Label 
                            ID = "Lblgroupcategory" 
                            runat = "server" 
                            CssClass = "labels" 
                            Text = "Grupo: ">
                        </asp:Label>
                    </div>
                    <div class = "celdaLeft">
                        <asp:TextBox 
                            ID = "TxtgroupCategory" 
                            runat = "server" 
                            MaxLength = "50" 
                            Width = "190px" 
                            Enabled = "True">
                        </asp:TextBox>
                    </div>
                </div>
                <div class = "fila">
                    <div class = "celdaLeft">
                        <asp:Label 
                            ID = "lbl_cliente" 
                            runat = "server" 
                            CssClass = "labels" 
                            Text = "Cliente*: ">
                        </asp:Label>
                    </div>
                    <div class = "celdaLeft">
                        <asp:DropDownList 
                            ID = "cmb_categorias_cliente" 
                            runat = "server" 
                            Width = "198px" 
                            Enabled = "True">
                        </asp:DropDownList>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    <!-- BODY - FIN -->

    <!-- OPCIONES - INI -->
    <br /><br />
    <div class="tabla centrar"><div class="fila"><div class="centrar centrarcontenido">
        
        <asp:Button 
            ID="BtnSaveProductType"
            runat="server" 
            CssClass="buttonPlan" 
            Text="Guardar"
            Visible="True" 
            Width="95px"
            OnClick="BtnSaveProductType_Click" />

        <asp:Button 
            ID="BtnCancelBTypeProduct" 
            runat="server" 
            CssClass="buttonPlan" 
            Text="Cancelar" 
            Width="80px" />

    </div> </div> </div>
    <!-- OPCIONES - FIN -->



</asp:Panel>
<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->
<!-- PANEL_INSERT - FIN -->

<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->



<cc1:ModalPopupExtender 
    ID="InsertCategoryModalPopupExtender" 
    TargetControlID="BtnCrearProductType"
    PopupControlID="InsertCategoryPanel"
    runat="server" 
    BackgroundCssClass="modalBackground"
    Enabled="True" 
    DynamicServicePath="">
</cc1:ModalPopupExtender>


                                        


<!-- PANEL DE MENSAJES - INI -->

<asp:Panel 
    ID = "AlertasInsertPanel"
    DropShadow = "True"
    runat = "server" 
    Style = "display: none;" 
    DefaultButton = "BtnAceptarAlert" 
    Height = "215px" 
    Width = "375px">
    <table align="center">
        <tr>
            <td 
                align = "center" 
                class = "style50" 
                valign = "top">
            <br /></td>
            <td 
                class = "style49" 
                valign = "top">
            <br />

                <asp:Label 
                    ID = "LblAlert" 
                    runat = "server" 
                    Text = "Señor Usuario" 
                    CssClass = "labelsMensaje">
                </asp:Label>
                
                <br /><br />
                
                <asp:Label 
                    ID = "LblFaltantes" 
                    runat = "server" 
                    CssClass = "labelsMensaje">
                </asp:Label>

            </td>
        </tr>
    </table>

    <table align="center">
        <tr><td>
            <asp:Button 
                ID = "BtnAceptarAlert" 
                runat = "server" 
                CssClass = "buttonPlan" 
                Text = "Aceptar" />
        </td></tr>
    </table>

</asp:Panel>


<cc1:ModalPopupExtender 
    ID = "ModalPopupAlertasInsert" 
    runat = "server"
    TargetControlID = "BtndisparaalertasInsert"
    PopupControlID = "AlertasInsertPanel"
    DropShadow = "True"
    BackgroundCssClass = "modalBackground"
    Enabled = "True" 
    OkControlID = "BtnAceptarAlert">
</cc1:ModalPopupExtender>

<!-- PANEL DE MENSAJES- FIN -->

<asp:Button 
    ID = "BtndisparaalertasInsert" 
    runat = "server" 
    CssClass = "alertas" 
    Text = "" 
    Visible = "true" 
    Width = "0px" />

