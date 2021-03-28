<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategorySearchUserControl.ascx.cs" Inherits="SIGE.Pages.Modulos.Administrativo.ProductUserControls.CategorySearchUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->
<!-- BUTTON SEARCH - INI -->
<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->
<asp:Button ID="BtnConsultaProductType" runat="server" CssClass="buttonPlan" Text="Consultar" Width="95px"/>
<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->
<!-- BUTTON SEARCH - FIN -->
<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->




<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->
<!-- PANEL_BUSQUEDA - INI -->
<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->
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
                <asp:TextBox ID="TxtBCodTypeProduct" runat="server" MaxLength="4" Width="80px"></asp:TextBox>
            </div>
        </div>
        <div class="fila">
            <div class="celda">
                <asp:Label ID="LblBNomProductcat" runat="server" CssClass="labels" Text="Nombre:" />
            </div>
            <div class="celda">
                <asp:TextBox ID="TxtBNomTypeProduct" runat="server" MaxLength="50" 
                    Width="180px"></asp:TextBox>
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
<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->
<!-- PANEL_BUSQUEDA - FIN -->
<!---------------------------------------------------------------------------->
<!---------------------------------------------------------------------------->

<cc1:ModalPopupExtender 
    ID="IbtnProductType" 
    TargetControlID="BtnConsultaProductType"
    PopupControlID="BuscarProductCateg"
    runat="server" 
    BackgroundCssClass="modalBackground"
    DropShadow="True" 
    Enabled="True" 
    OkControlID="BtnCancelBTypeProduct" 
    DynamicServicePath="">
</cc1:ModalPopupExtender>

                        